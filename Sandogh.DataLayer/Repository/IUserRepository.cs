
using Sandogh.DataLayer.Context;
using Sandogh.DataLayer.Services;

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.DataLayer.Repository
{
    public interface IUserRepository<T>: IGenericRepository<T> where T :User
    {
        public UserFullView Login(string username, string password);
        public UserFullView GetUserFullDetailsById(int id);
        public IList<UserFullView> GetAllUserFullDetails();
        public IList<UserSimpleView> GetAllUserSimpleDetails();
       // public Tbl_Users GetUserByID(int id);
       // public Tbl_Users GetUser();
       // public void InsertUser(Tbl_Users user);
        //public void EditUser(Tbl_Users user);
        //public void DeleteUser(Tbl_Users user);

    }
}
