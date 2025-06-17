using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.Payment;
using CoachlyBackEnd.Services.CRUD.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CoachlyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ICrudService<Payment> _paymentService;
        private readonly IMapper _mapper;

        public PaymentController(ICrudService<Payment> paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            try
            {
                var payment = await _paymentService.GetAllEntitiesAsync();

                return Ok(_mapper.Map<IEnumerable<PaymentDto>>(payment));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            try
            {
                var payment = await _paymentService.GetEntityByIdAsync(id);

                if (payment == null)
                {
                    return NoContent();
                }

                return Ok(_mapper.Map<PaymentDto>(payment));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error retrieving Payments: {e.Message}");
            }
        }
    }
}
