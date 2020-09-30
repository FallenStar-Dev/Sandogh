using Sandogh.DataLayer.Context;
using Sandogh.DataLayer.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.DataLayer.Repository
{
     public interface IGenericRepository<T>
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> whereParameter = null);
        IEnumerable<T> GetAll();
        IEnumerable<Phone> GetPhones(int id);
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
       // void Delete(object id);
        void Save();
    }       
}
