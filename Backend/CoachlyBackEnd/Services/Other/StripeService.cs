using CoachlyBackEnd.Config;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.Payment;
using CoachlyBackEnd.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Stripe;
using PaymentMethod = CoachlyBackEnd.Models.Enums.PaymentMethod;

namespace CoachlyBackEnd.Services.Other;

public class StripeService
{
    private readonly CoachlyDbContext _context;
    private readonly StripeSettings _stripeSettings;

    public StripeService(CoachlyDbContext context, StripeSettings stripeSettings)
    {
        _context = context;
        _stripeSettings = stripeSettings;
    }

    public async Task<PaymentIntent> CreatePaymentIntent(PaymentRequestDto dto)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(dto.Amount * 100),
            Currency = dto.Currency.ToString().ToLower(),
            Metadata = new Dictionary<string, string?>
            {
                { "userId", dto.UserId.ToString() },
                { "subscriptionId", dto.SubscriptionId.ToString() },
                { "sessionId", dto.SessionId.ToString() }
            }
        };

        var client = new StripeClient(_stripeSettings.SecretKey);
        var service = new PaymentIntentService(client);
        var paymentIntent = await service.CreateAsync(options);
        return paymentIntent;
    }
    
    public async Task<Payment?> HandlePaymentSuccess(string paymentIntentId)
{
    var client = new StripeClient(_stripeSettings.SecretKey);
    var paymentIntentService = new PaymentIntentService(client);
    var paymentIntent = await paymentIntentService.GetAsync(paymentIntentId);
    
    // //--------------------------------------------------ONLY FOR TEST---------------------------------------------------
    // if (paymentIntent.Status != "succeeded")
    // {
    //     var paymentMethodService = new PaymentMethodService(client);
    //     var paymentMethod = await paymentMethodService.CreateAsync(new PaymentMethodCreateOptions
    //     {
    //         Type = "card",
    //         Card = new PaymentMethodCardOptions
    //         {
    //             Number = "4242424242424242",
    //             ExpMonth = 12,
    //             ExpYear = 2030,
    //             Cvc = "123",
    //         },
    //     });
    //
    //     var confirmOptions = new PaymentIntentConfirmOptions
    //     {
    //         PaymentMethod = paymentMethod.Id
    //     };
    //
    //     paymentIntent = await paymentIntentService.ConfirmAsync(paymentIntentId, confirmOptions);
    // }
    // //------------------------------------------------------------------------------------------------------------------
    
    if (paymentIntent.Status != "succeeded" || paymentIntent.AmountReceived == 0)
    {
        return null;
    }

    var existingPayment = await _context.Payments
        .FirstOrDefaultAsync(p => p.StripePaymentId == paymentIntent.Id);

    if (existingPayment != null)
        return existingPayment;

    var userId = int.Parse(paymentIntent.Metadata["userId"]);
    int? sessionId = int.TryParse(paymentIntent.Metadata["sessionId"], out var sId) ? sId : null;
    int? subscriptionId = int.TryParse(paymentIntent.Metadata["subscriptionId"], out var subId) ? subId : null;

    var payment = new Payment
    {
        StripePaymentId = paymentIntent.Id,
        Amount = paymentIntent.AmountReceived / 100m,
        Currency = (CurrencyType)Enum.Parse(typeof(CurrencyType), paymentIntent.Currency.ToUpper()),
        Method = PaymentMethod.Card,
        Status = PaymentStatus.Succeeded,
        UserId = userId,
    };

    _context.Payments.Add(payment);
    await _context.SaveChangesAsync();

    if (sessionId is not null)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId.Value);
        if (session != null)
        {
            session.PaymentId = payment.Id;
        }
    }
    else if (subscriptionId is not null)
    {
        var subscription = await _context.Subscribtions.FirstOrDefaultAsync(s => s.Id == subscriptionId.Value);
        if (subscription != null)
        {
            subscription.PaymentId = payment.Id;
        }
    }

    await _context.SaveChangesAsync();
    return payment;
}
    
    public async Task<Refund?> RefundPayment(string stripePaymentIntentId, decimal? amount = null)
    {
        var client = new StripeClient(_stripeSettings.SecretKey);
        var refundService = new RefundService(client);

        var options = new RefundCreateOptions
        {
            PaymentIntent = stripePaymentIntentId,
        };

        if (amount.HasValue)
        {
            options.Amount = (long)(amount.Value * 100);
        }

        var refund = await refundService.CreateAsync(options);
        
        var payment = await _context.Payments.FirstOrDefaultAsync(p => p.StripePaymentId == stripePaymentIntentId);
        if (payment != null)
        {
            payment.Status = PaymentStatus.Refunded;
            await _context.SaveChangesAsync();
        }

        return refund;
    }
}