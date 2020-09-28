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
    public class PersonRepository : GenericRepository<Person>, IPersonRepository<Person>
    {
        private readonly Sandogh_DBEntities _db;
        private readonly DbSet<PersonRepository> _dbSet;
        public PersonRepository(Sandogh_DBEntities db) : base(db)
        {
            _db = db;
            _dbSet = _db.Set<PersonRepository>();
        }
       

        
    }
}
