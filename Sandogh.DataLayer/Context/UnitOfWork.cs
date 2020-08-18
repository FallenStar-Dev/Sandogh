using Sandogh.DataLayer.Repository;
using Sandogh.DataLayer.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.DataLayer.Context
{
    public class UnitOfWork : IDisposable
    {
        private readonly Sandogh_DBEntities db = new Sandogh_DBEntities();

        private IUserRepository _UserRepository;
        public IUserRepository UserRepository => _UserRepository ??= new UserRepository(db);

        public void Dispose() => db.Dispose();
    }
}
