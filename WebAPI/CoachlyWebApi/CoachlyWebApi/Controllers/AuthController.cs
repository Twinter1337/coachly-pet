using AutoMapper;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Models.DTOs.TrainerDtos;
using CoachlyBackEnd.Models.DTOs.UserDtos;
using CoachlyBackEnd.Models.Enums;
using CoachlyBackEnd.Services.Other;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CoachlyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtAuthService _jwtService;
    private readonly PasswordHashService _passwordHashService;
    private readonly CoachlyDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly OtpService _otpService;
    private readonly IMemoryCache _cache;

    public AuthController(
        JwtAuthService jwtService,
        PasswordHashService passwordHashService,
        CoachlyDbContext dbContext,
        IMapper mapper,
        OtpService otpService,
        IMemoryCache cache)
    {
        _jwtService = jwtService;
        _passwordHashService = passwordHashService;
        _dbContext = dbContext;
        _mapper = mapper;
        _otpService = otpService;
        _cache = cache;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Email);
        if (user == null || !_passwordHashService.VerifyPassword(user.PasswordHash, dto.Password))
        {
            return Unauthorized("Invalid credentials.");
        }

        _cache.Set($"pendingLogin:{dto.Email}", user.Id, TimeSpan.FromMinutes(5));

        var otpSent = await _otpService.SendOtpToEmailAsync(dto.Email);
        if (!otpSent) return StatusCode(500, "Не вдалося надіслати OTP");

        return Ok("OTP надіслано на email.");
    }

    [HttpPost("register-client")]
    public IActionResult RegisterClient([FromBody] UserCreateDto dto)
    {
        if (_dbContext.Users.Any(u => u.Email == dto.Email))
        {
            return BadRequest("User already exists.");
        }

        var user = _mapper.Map<User>(dto);

        user.PasswordHash = _passwordHashService.HashPassword(user.PasswordHash);

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return Ok("User registered successfully.");
    }

    [HttpPost("register-trainer")]
    public IActionResult RegisterTrainer([FromBody] TrainerRegisterDto dto)
    {
        if (_dbContext.Users.Any(u => u.Email == dto.User.Email))
        {
            return BadRequest("User already exists as client.");
        }

        dto.User.PasswordHash = _passwordHashService.HashPassword(dto.User.PasswordHash);

        var user = _mapper.Map<User>(dto.User);
        var trainer = _mapper.Map<Trainer>(dto);

        _dbContext.Users.Add(user);
        user.Role = UserRole.Trainer;
        _dbContext.SaveChanges();
        
        trainer.UserId = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email)?.Id;

        if (trainer.UserId == null)
        {
            return BadRequest("Trainer creation error.");
        }

        _dbContext.Trainers.Add(trainer);
        _dbContext.SaveChanges();

        return Ok("Trainer registered successfully.");
    }

    [HttpPost("verify-otp")]
    public IActionResult VerifyOtp([FromBody] VerifyOtpDto dto)
    {
        string failKey = $"otpFails:{dto.Email}";
        _cache.TryGetValue(failKey, out int attempts);
        if (attempts >= 5)
            return Forbid("To many attempts. Please try again later.");

        if (!_otpService.VerifyOtp(dto.Email, dto.OtpCode))
        {
            _cache.Set(failKey, ++attempts, TimeSpan.FromMinutes(5));
            return Unauthorized("Incorrect OTP code.");
        }

        _cache.Remove(failKey);

        if (!_cache.TryGetValue($"pendingLogin:{dto.Email}", out int userId))
        {
            return Unauthorized("Code is expired.");
        }

        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return Unauthorized("User does not exist.");

        var token = _jwtService.GenerateJwtToken(user);
        _cache.Remove($"pendingLogin:{dto.Email}");

        return Ok(new { token });
    }
}