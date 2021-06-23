using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Interfaces
{
    public interface IResult
    {
        public int ID { get; set; }
        public string Period { get; set; }
        public string Country { get; set; }
    }
}
