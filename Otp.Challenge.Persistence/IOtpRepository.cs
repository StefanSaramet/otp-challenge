namespace Otp.Challenge.Persistence;

public interface IOtpRepository
{
    bool Insert(Entities.Otp entity);
    Entities.Otp GetByTimeFrame(TimeOnly time);
    Entities.Otp GetById(Guid userId);

    bool Delete(Guid id);
}
