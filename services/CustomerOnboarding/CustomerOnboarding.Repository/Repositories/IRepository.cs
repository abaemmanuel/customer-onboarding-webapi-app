using CustomerOnboarding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Repositories.Repository
{
    public interface IRepository< T > where T: Entity 
    {
        Task<IQueryable<T>> GetAll();
        Task<IQueryable<T>> GetByWhere(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
} 
}
