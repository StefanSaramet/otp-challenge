namespace Otp.Challenge.Persistence;

public interface IOtpRepository
{
    bool Insert(Entities.Otp entity);
    Entities.Otp GetByTimeFrame(TimeOnly time);

    bool Delete(Guid id);
}
