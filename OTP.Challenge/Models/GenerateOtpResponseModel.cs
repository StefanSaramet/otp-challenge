namespace OTP.Challenge.Models;

public class GenerateOtpResponseModel
{
    public string Otp { get; init; }
    public int ValidFor { get; init; }
}
