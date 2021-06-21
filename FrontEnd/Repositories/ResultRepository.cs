using FrontEnd.Data;
using FrontEnd.Interfaces;
using FrontEnd.Models.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Repositories
{
    [ExcludeFromCodeCoverage]
    public class ResultRepository : Repository<Result>, IResultRepository
    {
        public ResultRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
