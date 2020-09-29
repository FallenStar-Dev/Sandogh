using Sandogh.DataLayer.Context;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.DataLayer.Repository
{
    interface IPersonRepository<T> : IGenericRepository<T> where T : Person
    {

    }
}
