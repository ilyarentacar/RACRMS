﻿using Microsoft.EntityFrameworkCore;
using RACRMS.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RACRMS.Repository.Concrete
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class, new()
    {
        protected readonly DbContext dbContext;

        public BaseRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Delete(T entity)
        {
            try
            {
                dbContext.Entry(entity).State = EntityState.Detached;
                dbContext.Attach(entity);
                dbContext.Remove(entity);
            }
            catch
            {
                throw;
            }
        }

        public void DeleteRange(List<T> entities)
        {
            try
            {
                dbContext.RemoveRange(entities);
            }
            catch
            {
                throw;
            }
        }

        public async Task InsertAsync(T entity)
        {
            try
            {
                dbContext.Entry(entity).State = EntityState.Detached;
                dbContext.Attach(entity);
                await dbContext.AddAsync(entity);
            }
            catch
            {
                throw;
            }
        }

        public async Task InsertRangeAsync(List<T> entities)
        {
            try
            {
                await dbContext.AddRangeAsync(entities);
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<T> Select(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                if (predicate == null)
                    return dbContext.Set<T>().AsQueryable().AsNoTracking();

                return dbContext.Set<T>().Where(predicate).AsNoTracking();
            }
            catch
            {
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                dbContext.Entry(entity).State = EntityState.Detached;
                dbContext.Attach(entity);
                dbContext.Update(entity);
            }
            catch
            {
                throw;
            }
        }

        public void UpdateRange(List<T> entities)
        {
            try
            {
                dbContext.UpdateRange(entities);
            }
            catch
            {
                throw;
            }
        }
    }
}