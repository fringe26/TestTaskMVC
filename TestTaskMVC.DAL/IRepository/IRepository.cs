using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskMVC.DomainModels.Models;

namespace TestTaskMVC.DAL.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> DetailsById(int? id);
        Task<bool> AddAsync(T item);
        Task<bool> Update(T item);
        Task<bool> DeleteAsync(int id);
    }
}
