using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.MongoDB
{
    public interface IDatabaseAccess
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
