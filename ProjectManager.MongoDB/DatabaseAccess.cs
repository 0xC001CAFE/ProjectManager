using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.MongoDB
{
    public class DatabaseAccess : IDatabaseAccess
    {
        private readonly IMongoDatabase database;

        public DatabaseAccess(string databaseName)
        {
            var client = new MongoClient();
            database = client.GetDatabase(databaseName);

            #region Add custom conventions for MongoDB

            var customConventions = new ConventionPack
            {
                new StringIdStoredAsObjectIdConvention()
            };

            ConventionRegistry.Register("CustomConventions", customConventions, type => true);

            #endregion
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return database.GetCollection<T>(name);
        }
    }
}
