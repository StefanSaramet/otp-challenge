namespace Otp.Challenge.PasswordGeneration.Response;

public class OtpResponse
{
    public OtpResponse(string otpPass, int remainingSeconds)
    {
        OtpPass = otpPass;
        RemainingSeconds = remainingSeconds;
    }

    public string OtpPass { get; }
    public int RemainingSeconds { get; }
}
