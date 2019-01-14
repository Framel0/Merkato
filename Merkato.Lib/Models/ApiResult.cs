using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.Models
{
    /// <summary>
    /// Api REsult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// Successfull or not (0 or 1)
        /// </summary>
        public Int16 Successfull { get; set; }

        /// <summary>
        /// If not sucessfull,show this error message
        /// </summary>
        public String Error { get; set; }

        /// <summary>
        /// Internal for debuggint
        /// </summary>
        public String InternalError { get; set; }


        /// <summary>
        /// Model to be returned
        /// </summary>
        public T Model { get; set; }
    }

}
