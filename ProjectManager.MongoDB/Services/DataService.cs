using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ProjectManager.Domain.Models;
using ProjectManager.Domain.Services;
using System.Threading.Tasks;

namespace ProjectManager.MongoDB.Services
{
    public class DataService<T> : IDataService<T> where T : DomainBase
    {
        protected readonly IMongoCollection<T> modelCollection;

        public DataService(IMongoCollection<T> modelCollection)
        {
            this.modelCollection = modelCollection;
        }

        public DataService(IDatabaseAccess databaseAccess, string collectionName, BsonClassMap<T> customClassMap)
        {
            BsonClassMap.RegisterClassMap(customClassMap);

            modelCollection = databaseAccess.GetCollection<T>(collectionName);
        }

        public async Task<T> Create(T model)
        {
            await modelCollection.InsertOneAsync(model);

            return model;
        }

        public async Task<T> DeleteById(string id)
        {
            return await modelCollection.FindOneAndDeleteAsync(model => model.Id == id);
        }

        public async Task<T> GetById(string id)
        {
            return await Task.Run(() =>
            {
                return modelCollection.Find(model => model.Id == id).SingleOrDefault();
            });
        }

        public async Task<T> UpdateById(string id, T model)
        {
            if (model.Id == null) model.Id = id;

            return await modelCollection.FindOneAndReplaceAsync(storedModel => storedModel.Id == id, model);
        }
    }
}
