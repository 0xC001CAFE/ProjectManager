using MongoDB.Driver;

namespace ProjectManager.MongoDB
{
    public interface IDatabaseAccess
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
