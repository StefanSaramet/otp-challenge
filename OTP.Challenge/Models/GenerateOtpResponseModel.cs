namespace OTP.Challenge.Models;

public class GenerateOtpResponseModel
{
    public string Otp { get; set; }
    public TimeOnly ValidUntil { get; set; }
}
