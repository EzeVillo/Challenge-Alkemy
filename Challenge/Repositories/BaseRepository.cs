using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Challenge.Entities;
using Challenge.Helpers;
using Challenge.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Repositories
{
    public abstract class BaseRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : Entity
        where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private DbSet<TEntity> _dbSet;

        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet ??= _dbContext.Set<TEntity>(); }
        }

        protected BaseRepository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TEntity> GetAllEntities()
        {
            return DbSet.ToList();
        }

        public TEntity Get(int id)
        {
            return DbSet.Find(id);
        }

        public TEntity Add(TEntity entity)
        {
            DbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _dbContext.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }

        public TEntity Delete(int id)
        {
            TEntity entity = _dbContext.Find<TEntity>(id);
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public IEnumerable<TEntity> FindBy(QueryParameters<TEntity> queryParameters)
        {
            Expression<Func<TEntity, bool>> whereTrue = x => true;
            var where = queryParameters.Where ?? whereTrue;
            if (queryParameters.OrderByAscending)
            {
                if (queryParameters.OrderBy != null)
                {
                    return DbSet.Where(where).OrderBy(queryParameters.OrderBy)
                    .Skip((queryParameters.Pagina - 1) * queryParameters.Top)
                    .Take(queryParameters.Top).ToList();
                }
                return DbSet.Where(where).OrderBy(x => x.Id)
                .Skip((queryParameters.Pagina - 1) * queryParameters.Top)
                .Take(queryParameters.Top).ToList();
            }
            if (queryParameters.OrderBy != null)
            {
                return DbSet.Where(where).OrderByDescending(queryParameters.OrderBy)
                .Skip((queryParameters.Pagina - 1) * queryParameters.Top)
                .Take(queryParameters.Top).ToList();
            }
            return DbSet.Where(where).OrderByDescending(x => x.Id)
            .Skip((queryParameters.Pagina - 1) * queryParameters.Top)
            .Take(queryParameters.Top).ToList();
        }
    }
}