using Microsoft.AspNetCore.Mvc;
using Otp.Challenge.PasswordGeneration;
using Otp.Challenge.Persistence;
using OTP.Challenge.Models;
using Quartz.Spi;

namespace OTP.Challenge.Controllers;

[ApiController]
[Route("[controller]")]
public class OtpController : ControllerBase
{
    private readonly IOtpGenerator _otpGenerator;
    private readonly IOtpRepository _otpRepository;
    private readonly IJobFactory _jobFactory;

    public OtpController(
        IOtpGenerator otpGenerator,
        IOtpRepository otpRepository, 
        IJobFactory jobFactory)
    {
        this._otpGenerator = otpGenerator;
        this._otpRepository = otpRepository;
        this._jobFactory = jobFactory;
    }

    [HttpPost("generate/{userId}")]
    public GenerateOtpResponseModel GenerateOtp(Guid userId)
    {
        //generate new otp
        var otp = _otpGenerator.Compute(userId);

        var test = _otpGenerator.IsValid(userId, otp.OtpPass);
        //start quartz job
        //var job = _jobFactory.NewJob()
        
        return new GenerateOtpResponseModel
        {
            Otp = otp.OtpPass,
            ValidUntil = new TimeOnly(0, 0, otp.RemainingSeconds).ToLongTimeString()
        };
    }

    [HttpPost("stop/{userId}")]
    public void StopOtpGeneration(Guid userId)
    {
        //stop quartz job
    }
}
