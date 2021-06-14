using FrontEnd.Data;
using FrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Repositories
{
    //public class RepositoryWrapper : IRepositoryWrapper
    //{
    //    ApplicationDbContext _repoContext;
    //    public RepositoryWrapper(ApplicationDbContext repoContext)
    //    {
    //        _repoContext = repoContext;
    //    }

    //    IResultRepository _results;

    //    public IResultRepository Results
    //    {
    //        get
    //        {
    //            if (_results == null)
    //            {
    //                _results = new ResultRepository(_repoContext);
    //            }
    //            return _results;
    //        }
    //    }

    //    public void Save()
    //    {
    //        _repoContext.SaveChanges();
    //    }
    //}
}
