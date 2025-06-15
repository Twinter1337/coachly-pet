using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace CoachlyBackEnd.Services.Other;

public class OtpService
{
    private readonly string _emailFrom;
    private readonly SmtpClient _smtpClient;
    private readonly IMemoryCache _memoryCache;
    private readonly Random _random = new();
    
    public OtpService(IMemoryCache memoryCache, IConfiguration config)
    {
        _memoryCache = memoryCache;
        _emailFrom = config["EmailSettings:From"]!;
        string? emailPassword = config["EmailSettings:Password"];

        _smtpClient = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(_emailFrom, emailPassword),
            EnableSsl = true
        };
    }

    public async Task<bool> SendOtpToEmailAsync(string emailTo)
    {
        try
        {
            string verificationCode = GenerateVerificationCode();

            _memoryCache.Set(emailTo, verificationCode, TimeSpan.FromMinutes(5));

            var message = new MailMessage(_emailFrom, emailTo)
            {
                Subject = "BeePC account verification",
                // Body = GenerateHtmlEmailBody(verificationCode),
                IsBodyHtml = true
            };

            await _smtpClient.SendMailAsync(message);
            return true;
        }
        catch (SmtpException ex)
        {
            Console.WriteLine($"SMTP Error: {ex.Message}");
            return false;
        }
    }

    public bool VerifyOtp(string email, string enteredCode)
    {
        if (_memoryCache.TryGetValue(email, out string? codeFromCache))
        {
            if (codeFromCache == enteredCode)
            {
                _memoryCache.Remove(email);
                return true;
            }
        }

        return false;
    }

    private string GenerateVerificationCode()
        => _random.Next(10000, 99999).ToString();

//     private string GenerateHtmlEmailBody(string code)
//     {
//         return $$"""
//                  <html>
//                      <head>
//                          <style>
//                              body {
//                                  background-color: #f5f5f5;
//                                  font-family: 'Segoe UI', sans-serif;
//                                  padding: 20px;
//                              }
//                              .container {
//                                  background-color: #ffffff;
//                                  padding: 30px;
//                                  max-width: 600px;
//                                  margin: auto;
//                                  border-radius: 10px;
//                                  box-shadow: 0 2px 10px rgba(0,0,0,0.1);
//                              }
//                              .header {
//                                  text-align: center;
//                              }
//                              .logo {
//                                  width: 100px;
//                                  margin-bottom: 20px;
//                              }
//                              .code {
//                                  font-size: 32px;
//                                  color: #eacf00;
//                                  font-weight: bold;
//                                  margin: 30px 0;
//                                  text-align: center;
//                              }
//                              .footer {
//                                  font-size: 12px;
//                                  color: #777;
//                                  text-align: center;
//                                  margin-top: 40px;
//                              }
//                          </style>
//                      </head>
//                      <body>
//                          <div class="container">
//                              <div class="header">
//                                  <img src="{{logoUrl}}" alt="BeePC Logo" class="logo" />
//                                  <h2>Verify your email address</h2>
//                              </div>
//                              <p>Hello 👋,</p>
//                              <p>Here is your one-time verification code for BeePC:</p>
//                              <div class="code" style="color: #eacf00;">{{code}}</div>
//                              <p>This code will expire in 5 minutes.</p>
//                              <p>If you didn't request this, please ignore the email.</p>
//                              <div class="footer">
//                                  BeePC © {{DateTime.UtcNow.Year}}. All rights reserved.
//                              </div>
//                          </div>
//                      </body>
//                  </html>
//                  """;
//     }
}