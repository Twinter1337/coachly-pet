using AutoMapper;
using CoachlyBackEnd.Models.DTOs.Payment;
using CoachlyBackEnd.Services.Other;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using CoachlyBackEnd.Config;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.Enums;
using CoachlyBackEnd.Services.CRUD.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoachlyWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StripePaymentController : ControllerBase
{
    private readonly StripeService _stripeService;
    private readonly IMapper _mapper;
    private readonly StripeSettings _stripeSettings;
    private readonly CoachlyDbContext _dbContext;

    public StripePaymentController(
        StripeService stripeService,
        IMapper mapper,
        StripeSettings stripeSettings,
        CoachlyDbContext dbContext)
    {
        _stripeService = stripeService;
        _mapper = mapper;
        _stripeSettings = stripeSettings;
        _dbContext = dbContext;
    }

    [HttpPost("create-intent")]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentRequestDto dto)
    {
        var paymentIntent = await _stripeService.CreatePaymentIntent(dto);
        return Ok(new { clientSecret = paymentIntent.ClientSecret });
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmPayment([FromQuery] string paymentIntentId)
    {
        var payment = await _stripeService.HandlePaymentSuccess(paymentIntentId);
        if (payment == null)
            return BadRequest("Payment not found or failed to handle.");

        return Ok(_mapper.Map<PaymentDto>(payment));
    }

    [HttpPost("refund")]
    public async Task<IActionResult> Refund([FromQuery] string paymentIntentId)
    {
        var refund = await _stripeService.RefundPayment(paymentIntentId);
        if (refund == null)
            return BadRequest("Refund failed");
        
        var payment = await _dbContext.Payments.FirstOrDefaultAsync(p=>p.StripePaymentId == paymentIntentId);
        if (payment != null) payment.Status = PaymentStatus.Refunded;
        
        await _dbContext.SaveChangesAsync();

        return Ok(new
        {
            refund.Id,
            refund.Status,
            refund.Amount,
            refund.Currency
        });
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> HandleStripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        Event stripeEvent;

        try
        {
            stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _stripeSettings.WebhookSecret
            );
        }
        catch (Exception e)
        {
            return BadRequest($"Webhook Error: {e.Message}");
        }

        try
        {
            if (stripeEvent.Type == "payment_intent.succeeded")
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                if (paymentIntent != null)
                {
                    await _stripeService.HandlePaymentSuccess(paymentIntent.Id);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            return StatusCode(500, "Webhook error: " + ex.Message);
        }

        return Ok();
    }
}