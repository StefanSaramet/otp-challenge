using Microsoft.AspNetCore.Mvc;
using Otp.Challenge.PasswordGeneration;
using OTP.Challenge.Models;

namespace OTP.Challenge.Controllers;

[ApiController]
[Route("[controller]")]
public class OtpController : ControllerBase
{
    private readonly IOtpGenerator _otpGenerator;

    public OtpController(
        IOtpGenerator otpGenerator)
    {
        this._otpGenerator = otpGenerator;
    }

    [HttpGet("generate/{userId}")]
    public GenerateOtpResponseModel GenerateOtp(Guid userId)
    {
        //generate new otp
        var otp = _otpGenerator.Compute(userId);

        return new GenerateOtpResponseModel
        {
            Otp = otp.OtpPass,
            ValidFor = otp.RemainingSeconds
        };
    }    
}
