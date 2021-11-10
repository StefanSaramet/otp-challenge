using Microsoft.AspNetCore.Mvc;
using Otp.Challenge.Persistence;
using OTP.Challenge.Models;
using Quartz.Spi;

namespace OTP.Challenge.Controllers;

[ApiController]
[Route("[controller]")]
public class OtpController : ControllerBase
{
    private readonly IOtpRepository otpRepository;
    private readonly IJobFactory jobFactory;

    public OtpController(
        IOtpRepository otpRepository, 
        IJobFactory jobFactory)
    {
        this.otpRepository = otpRepository;
        this.jobFactory = jobFactory;
    }

    [HttpPost("generate")]
    public GenerateOtpResponseModel GenerateOtp([FromBody] GenerateOtpRequestModel otpRequestModel)
    {
        //generate new otp
        //start quartz job
        //persist otp
        return new GenerateOtpResponseModel();
    }

    [HttpPost("stop/{userId}")]
    public void StopOtpGeneration(Guid userId)
    {
        //stop quartz job
        //delete otp for userid
    }
}
