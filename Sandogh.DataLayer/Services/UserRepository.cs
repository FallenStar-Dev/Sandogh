using Sandogh.DataLayer.Context;
using Sandogh.DataLayer.Repository;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.DataLayer.Services
{
    public class UserRepository : GenericRepository<User>, IUserRepository<User>
    {
        private readonly Sandogh_DBEntities _db;



        private readonly DbSet<UserRepository> _dbSet;
        public UserRepository(Sandogh_DBEntities db) : base(db)
        {
            _db = db;
            _dbSet = _db.Set<UserRepository>();
        }

        public IList<UserSimpleView> GetAllUserSimpleDetails()
        {
            return _db.UserSimpleViews.ToList();
        }

        public UserFullView GetUserFullDetailsByID(int id)
        {
            throw new NotImplementedException();
        }


        /* public IList<Vw_UsersJob> GetAllUserWithJobDetails()
         {
             return _db.Vw_UsersJob.ToList();
         }  */

        public UserFullView GetUserWithJobDetailsByID(int id)
        {
            return _db.UserFullViews.Find(id);
        }



        public UserFullView Login(string username, string password)
        {
            return _db.UserFullViews.Where
                (c => c.UserName.Equals(username) && c.Password.Equals(password)).SingleOrDefault();
        }



        IEnumerable<User> IGenericRepository<User>.GetAll()
        {
            return base.Get();
        }

        IList<UserFullView> IUserRepository<User>.GetAllUserFullDetails()
        {
            return _db.UserFullViews.ToList();
        }



    }
}
