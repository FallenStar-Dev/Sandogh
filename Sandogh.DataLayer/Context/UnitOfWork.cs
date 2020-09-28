using Sandogh.Bussiness;
using Sandogh.DataLayer.Repository;
using Sandogh.DataLayer.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;


namespace Sandogh.DataLayer.Context
{
    public class UnitOfWork : IDisposable
    {
        private readonly Sandogh_DBEntities db = new Sandogh_DBEntities(GlobalVariables.MainConnectionString);
        private IUserRepository<User> _UserRepository;
        public IUserRepository<User> UserRepository => _UserRepository ??= new UserRepository(db);
        private IPersonRepository<Person> _PersonRepository;
        public IPersonRepository<Person> PersonRepository => _PersonRepository ??= new PersonRepository(db);

        /* private IUserRepository _UserRepository;
         public IUserRepository UserRepository => _UserRepository ??= new UserRepository(db); */
        public void Dispose() => db.Dispose();
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
