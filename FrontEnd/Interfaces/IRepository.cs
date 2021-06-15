using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Interfaces
{
    public interface IRepository<T>
    {
        T Create(T entity);
    }
}
