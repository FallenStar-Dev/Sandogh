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
        private readonly Sandogh_DBEntities _db = new Sandogh_DBEntities(DataBaseConnection.MainConnectionString);
        private IUserRepository<User> _userGenericRepository;
        public IUserRepository<User> UserGenericRepository => _userGenericRepository ??= new UserRepository(_db);

        private IGenericRepository<Job> _jobGenericRepository;
        public IGenericRepository<Job> JobGenericRepository => _jobGenericRepository ??= new GenericRepository<Job>(_db);
        /* private IUserRepository _UserRepository;
         public IUserRepository UserRepository => _UserRepository ??= new UserRepository(db); */


        public void Save()
        {
            _db.SaveChanges();
        }

        #region Disposing
        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here

        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                _db?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion Disposing
    }
}
