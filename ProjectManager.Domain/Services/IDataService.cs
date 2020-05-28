using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Services
{
    public interface IDataService<T> where T : DomainBase
    {
        Task<T> GetById(string id);

        Task<T> Create(T model);

        Task<T> UpdateById(string id, T model);

        Task<T> DeleteById(string id);
    }
}
