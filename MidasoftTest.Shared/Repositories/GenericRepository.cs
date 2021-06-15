using Microsoft.EntityFrameworkCore;
using MidasoftTest.Data.DataContext;
using MidasoftTest.Data.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MidasoftTest.Data.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly MidasoftContext _context;

        public GenericRepository(MidasoftContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> whereCondition = null) where T : class
        {
            return await _context.Set<T>().Where(whereCondition).ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(Expression<Func<T, bool>> condition) where T : class
        {
            return await _context.Set<T>().Where(condition).FirstOrDefaultAsync();
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
