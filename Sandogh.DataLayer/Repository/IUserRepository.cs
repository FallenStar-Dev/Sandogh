using Sandogh.DataLayer.Context;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.DataLayer.Repository
{
    public interface IUserRepository
    {
        public Sp_Login_Result Login(string username, string password);
    }
}
