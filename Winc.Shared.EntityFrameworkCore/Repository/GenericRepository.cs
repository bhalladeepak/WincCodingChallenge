using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Winc.Shared.EntityFrameworkCore.Entities;

namespace Winc.Shared.EntityFrameworkCore.Repository
{
    public class GenericRepository : IGenericRepository
    {
        protected DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }


        public IQueryable<TEntity> Query<TEntity>(bool tracking = false) where TEntity : BaseEntity
        {
            if (tracking == true)
            {
                return _context.Set<TEntity>();
            }
            return _context.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> predicate,
                                                bool tracking = false) where TEntity : BaseEntity
        {
            return Query<TEntity>(tracking).Where(predicate);
        }


        private IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> predicate,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                    bool tracking = false) where TEntity : BaseEntity
        {

            var qry = Query<TEntity>(tracking).Where(predicate);

            if (include != null)
            {
                qry = include(qry);
            }

            if (predicate != null)
            {
                qry = qry.Where(predicate);
            }

            if (orderBy != null)
            {
                qry = orderBy(qry);
            }

            return qry;
        }


        //public async Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
        //                                   bool tracking = false) where TEntity : BaseEntity
        //{
        //    return await Query<TEntity>(predicate, tracking).SingleOrDefaultAsync();
        //}

        public async Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                    bool tracking = false) where TEntity : BaseEntity
        {

            return await Query<TEntity>(predicate, orderBy, include, tracking).SingleOrDefaultAsync();
        }


        public async Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                bool tracking = false) where TEntity : BaseEntity
        {
            return await Query(predicate, orderBy, include, tracking).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync<TEntity>() where TEntity : BaseEntity
        {
            return await Query<TEntity>(false).ToListAsync();
        }



        public async Task InsertAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            await _context.AddAsync<TEntity>(entity);
        }



        public async Task<bool> UpdateAsync<TEntity>(TEntity updated) where TEntity : BaseEntity
        {
            try
            {
                _context.Set<TEntity>().Attach(updated);
                _context.Entry(updated).State = EntityState.Modified;
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }
        public async Task<bool> DeleteAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }



        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            _context.Update<TEntity>(entity);
        }

        public virtual async Task<bool> Delete<TEntity>(Expression<Func<TEntity, bool>> predicate,
                                params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity
        {
            var results = _context.Set<TEntity>().Where(predicate);

            foreach (var includeExpression in includes)
                results = results.Include(includeExpression);
            try
            {
                _context.Set<TEntity>().RemoveRange(results);
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }

        //public void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity
        //{             
        //    _context.Remove<TEntity>(entity);
        //}

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
