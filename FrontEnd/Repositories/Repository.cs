using FrontEnd.Data;
using FrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Repositories
{
    [ExcludeFromCodeCoverage]
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext RepositoryContext { get; set; }
        public Repository(ApplicationDbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        public T Create(T entity)
        {
            return RepositoryContext.Set<T>().Add(entity).Entity;
        }
    }
}
