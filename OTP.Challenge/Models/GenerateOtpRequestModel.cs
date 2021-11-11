namespace OTP.Challenge.Models;

public class GenerateOtpRequestModel
{
    public Guid UserId { get; set; }
    public DateTime RequestedDate { get; set; }
}
