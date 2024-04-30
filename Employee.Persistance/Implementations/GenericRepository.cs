using Employee.Application.Interfaces;
using Employee.Domain.a_Common;
using Employee.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Persistance.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity
    {

        #region Object Database

        private readonly EmployeeDbContext _dbContext;
        #endregion

        #region Constructor
        public GenericRepository(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Ovverides methods

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);

        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            T exist = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }
        #endregion
    }
}
