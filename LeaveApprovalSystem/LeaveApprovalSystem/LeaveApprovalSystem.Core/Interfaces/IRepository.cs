using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApprovalSystem.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdentifierAsync(string columnName, string identifier);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(string anotherTableName);
        Task<IEnumerable<T>> GetItemsIdAsync(int id, string anotherTableName);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }

}
