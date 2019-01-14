using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.Models
{
    public  class CallResult<T>
    {
        public T Model { get; set; }

        public Int16 HasError { get; set; }
        public String Error { get; set; }

        public String InternalError { get; set; }

        public CallResult()
        {
            this.HasError = 0;
        }
    }
}
