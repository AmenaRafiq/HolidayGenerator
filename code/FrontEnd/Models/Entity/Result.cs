using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models.Entity
{
    [ExcludeFromCodeCoverage]
    public class Result
    {
        public int ID { get; set; }
        public string Period {get; set; }
        public string Country { get; set; }
    }
}
