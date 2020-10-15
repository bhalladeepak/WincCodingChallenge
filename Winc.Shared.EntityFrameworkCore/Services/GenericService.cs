using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Winc.Shared.EntityFrameworkCore.Entities;
using Winc.Shared.EntityFrameworkCore.Repository;

namespace Winc.Shared.EntityFrameworkCore.Services
{
    public abstract class GenericService : IGenericService
    {
        protected IGenericRepository _genericRepo;

        public GenericService(IGenericRepository genericRepository)
        {
            _genericRepo = genericRepository;
        }

        public virtual async Task<TEntity> GetByIdAsync<TEntity>(Guid id,
                                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                                 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                                 bool tracking = false) where TEntity : BaseEntity
        {
            return await _genericRepo.GetAsync<TEntity>(x => (x as AuditableEntity).Id == id, orderBy, include, tracking);
        }


        public virtual async Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
                                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                            bool tracking = false) where TEntity : BaseEntity
        {
            return await _genericRepo.GetAsync<TEntity>(predicate, orderBy, include, tracking);
        }


        public virtual async Task<IList<TEntity>> GetAllAsync<TEntity>() where TEntity : BaseEntity
        {
            return await _genericRepo.GetAllAsync<TEntity>();
        }


        public virtual async Task SaveAsync(AuditableEntity entity, bool saveChangesAsAtomic = true)
        {

            if (entity == null)
            {
                return;
            }

            if (entity.Id != Guid.Empty)
            {
                _genericRepo.Update(entity);
            }
            else
            {
                await _genericRepo.InsertAsync(entity);
            }

            if (saveChangesAsAtomic == true)
            {
                await _genericRepo.SaveChangesAsync();
            }
        }

    }
}
