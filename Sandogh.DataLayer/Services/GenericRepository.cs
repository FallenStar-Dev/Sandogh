using Sandogh.DataLayer.Context;
using Sandogh.DataLayer.Repository;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.DataLayer.Services
{
    public abstract class GenericRepository<T> : IGenericRepository<T>  where T:class
    {
        private readonly Sandogh_DBEntities _db;
        private readonly DbSet<T> _dbSet;
        
        public GenericRepository(Sandogh_DBEntities db)
        {
            _db = db;
            _dbSet = _db.Set<T>();            
        }  

        IEnumerable<T> IGenericRepository<T>.GetAll()
        {
            return _dbSet.ToList();
        }

        T IGenericRepository<T>.GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public virtual void Save()
        {
            _db.SaveChanges();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> whereParameter = null)
        {
            IQueryable<T> query = _dbSet;

            if (whereParameter != null)
            {
                query = query.Where(whereParameter);
            }

            return query.ToList();
        }
    }
}
