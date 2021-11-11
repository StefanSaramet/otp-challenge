using Otp.Challenge.PasswordGeneration.Response;

namespace Otp.Challenge.PasswordGeneration
{
    public interface IOtpGenerator
    {
        OtpResponse Compute(Guid userId);
        OtpResponse Compute(Guid userId, DateTime datetime);
        bool IsValid(Guid userId, string password);
    }
}