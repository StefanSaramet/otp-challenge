namespace Otp.Challenge.Persistence.Entities;

public class Otp
{
    public Guid UserId { get; set; }
    public string Password { get; set; }
    public TimeOnly ValidUntil { get; set; }
}
