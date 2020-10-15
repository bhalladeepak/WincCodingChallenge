using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Winc.Shared.EntityFrameworkCore.Entities;

namespace Winc.Shared.EntityFrameworkCore.Repository
{
    public interface IGenericRepository
    {
        IQueryable<TEntity> Query<TEntity>(bool tracking = false) where TEntity : BaseEntity;
        IQueryable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> predicate, bool tracking = false) where TEntity : BaseEntity;

        Task<List<TEntity>> GetAllAsync<TEntity>() where TEntity : BaseEntity;
        //Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null, bool tracking = false) where TEntity : BaseEntity;
        Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                        bool tracking = false) where TEntity : BaseEntity;

        //Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool tracking = false) where TEntity : BaseEntity;
        Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                       bool tracking = false) where TEntity : BaseEntity;

        //Task<TEntity> GetById<TEntity>(Guid Id, bool tracking = false) where TEntity : BaseEntity;

        Task InsertAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;


        Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<bool> DeleteAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;


        void Update<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<bool> Delete<TEntity>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity;

        Task SaveChangesAsync();
    }
}
