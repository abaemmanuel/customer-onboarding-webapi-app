using CustomerOnboarding.Core.Entities;
using CustomerOnboarding.Repositories.Repository;
using CustomerOnboarding.Repository.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomerOnboarding.Repositories.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly CustomerOnboardingContext _context;
        public Repository(CustomerOnboardingContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<T>> GetAll()
        {
            return  _context.Set<T>().AsNoTracking();
        }

        public async Task<IQueryable<T>> GetByWhere(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).AsNoTracking();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}
