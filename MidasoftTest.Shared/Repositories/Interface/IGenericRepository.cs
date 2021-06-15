using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MidasoftTest.Data.Repositories.Interface
{
    public interface IGenericRepository
    {
        Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> whereCondition = null) where T : class;
        Task<T> GetByIdAsync<T>(Expression<Func<T, bool>> condition) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        Task<T> CreateAsync<T>(T entity) where T : class;
    }
}
