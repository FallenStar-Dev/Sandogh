using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sandogh.DataLayer.Context;
using Sandogh.DataLayer.Repository;
namespace Sandogh.DataLayer.Services
{
    public class UserRepository : IUserRepository
    {
        readonly Sandogh_DBEntities Sandogh_DB;
        public UserRepository(Sandogh_DBEntities dBEntities) => Sandogh_DB = dBEntities;


        public Sp_Login_Result Login(string username, string password) => Sandogh_DB.Sp_Login(username, password).SingleOrDefault();
        public IList<Vw_UsersJob> GetAllUser()
        {
           return Sandogh_DB.Vw_UsersJob.ToList();
        }
    }
}
