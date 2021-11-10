using Otp.Challenge.PasswordGeneration.Response;

namespace Otp.Challenge.PasswordGeneration
{
    public interface IOtpGenerator
    {
        OtpResponse Compute(Guid userId);
        bool IsValid(Guid userId, string password);
    }
}