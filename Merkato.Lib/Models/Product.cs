using System;
using System.Collections.Generic;

namespace  Merkato.Lib.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public short? UnitId { get; set; }
        public short? Status { get; set; }
    }
}
