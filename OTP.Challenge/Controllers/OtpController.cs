using Microsoft.AspNetCore.Mvc;
using OTP.Challenge.Models;

namespace OTP.Challenge.Controllers;

[ApiController]
[Route("[controller]")]
public class OtpController : ControllerBase
{
    public OtpController()
    {

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
