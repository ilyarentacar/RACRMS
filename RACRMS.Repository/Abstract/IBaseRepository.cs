using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.Repository.Abstract
{
    public interface IBaseRepository<T>
        where T : class, new()
    {
        IQueryable<T> Select(Expression<Func<T, bool>> predicate = null);

        Task InsertAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task InsertRangeAsync(List<T> entities);
        void UpdateRange(List<T> entities);
        void DeleteRange(List<T> entities);
    }
}
