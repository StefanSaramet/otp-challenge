using Microsoft.AspNetCore.Mvc;
using Otp.Challenge.PasswordGeneration;
using Otp.Challenge.Persistence;
using OTP.Challenge.Models;

namespace OTP.Challenge.Controllers;

[ApiController]
[Route("[controller]")]
public class OtpController : ControllerBase
{
    private readonly IOtpGenerator _otpGenerator;
    private readonly IOtpRepository _otpRepository;

    public OtpController(
        IOtpGenerator otpGenerator,
        IOtpRepository otpRepository)
    {
        this._otpGenerator = otpGenerator;
        this._otpRepository = otpRepository;
    }

    [HttpPost("generate/{userId}")]
    public GenerateOtpResponseModel GenerateOtp(Guid userId)
    {
        //generate new otp
        var otp = _otpGenerator.Compute(userId);

        return new GenerateOtpResponseModel
        {
            Otp = otp.OtpPass,
            ValidUntil = new TimeOnly(0, 0, otp.RemainingSeconds).ToLongTimeString()
        };
    }    
}
