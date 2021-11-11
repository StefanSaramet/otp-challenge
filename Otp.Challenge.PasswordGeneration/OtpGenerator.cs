using Otp.Challenge.PasswordGeneration.Response;
using OtpNet;

namespace Otp.Challenge.PasswordGeneration;

public class OtpGenerator : IOtpGenerator
{
    private Totp _generator;
    
    public OtpResponse Compute(Guid userId)
    {
        _generator = new Totp(userId.ToByteArray(), step: 30,OtpHashMode.Sha512);

        var pass = _generator.ComputeTotp(DateTime.UtcNow);
        var remainingSeconds = _generator.RemainingSeconds(DateTime.UtcNow);

        return new OtpResponse(pass, remainingSeconds);
    }

    public OtpResponse Compute(Guid userId, DateTime datetime)
    {
        _generator = new Totp(userId.ToByteArray(), step: 30, OtpHashMode.Sha512);

        var pass = _generator.ComputeTotp(datetime);
        var remainingSeconds = _generator.RemainingSeconds(datetime);

        return new OtpResponse(pass, remainingSeconds);
    }

    public bool IsValid(Guid userId, string password)
    {
        _generator = new Totp(userId.ToByteArray(), step: 30, OtpHashMode.Sha512);

        return _generator.RemainingSeconds() != 0;
    }
}
