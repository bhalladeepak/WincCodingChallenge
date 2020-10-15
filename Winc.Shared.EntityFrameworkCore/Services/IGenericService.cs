using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Winc.Shared.EntityFrameworkCore.Entities;

namespace Winc.Shared.EntityFrameworkCore.Services
{
    public interface IGenericService
    {
        Task<TEntity> GetByIdAsync<TEntity>(Guid id,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool tracking = false) where TEntity : BaseEntity;


        Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool tracking = false) where TEntity : BaseEntity;

        Task<IList<TEntity>> GetAllAsync<TEntity>() where TEntity : BaseEntity;

        Task SaveAsync(AuditableEntity entity, bool saveChangesAsAtomic = true);

    }
}
