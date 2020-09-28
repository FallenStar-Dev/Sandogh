using Sandogh.DataLayer.Context;
using Sandogh.DataLayer.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.DataLayer.Repository
{
    public interface IPersonRepository<T>: IGenericRepository<T> where T :Person
    {

    }
}
