using CoachlyBackEnd.Models;
using Microsoft.AspNetCore.Identity;

namespace CoachlyBackEnd.Services.Other;

public class PasswordHashService
{
    private readonly PasswordHasher<User> _passwordHasher = new();

    public string HashPassword(string plainPassword)
    {
        return _passwordHasher.HashPassword(null!, plainPassword);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(null!, hashedPassword, providedPassword);
        return result == PasswordVerificationResult.Success;
    }
}