using LiteDB;

namespace Otp.Challenge.Persistence;

public class OtpRepository : IOtpRepository
{
    private const string OtpCollectionName = "otpPasswords";
    private readonly LiteDatabase dB;

    public OtpRepository()
    {
        dB = new LiteDatabase("Otp.db");
    }

    public bool Delete(Guid id)
    {
        var collection = dB.GetCollection<Entities.Otp>(OtpCollectionName);
        
        return collection.DeleteMany(x => x.UserId == id) != 0;
    }

    public Entities.Otp GetByTimeFrame(TimeOnly time)
    {
        var collection = dB.GetCollection<Entities.Otp>(OtpCollectionName);

        return collection.FindOne(x => x.ValidUntil.IsBetween(time, time.Add(TimeSpan.FromSeconds(30))));
    }

    public bool Insert(Entities.Otp entity)
    {
        var collection = dB.GetCollection<Entities.Otp>(OtpCollectionName);
        collection.EnsureIndex(x => x.UserId, true);
        
        return collection.Upsert(entity);
    }
}
