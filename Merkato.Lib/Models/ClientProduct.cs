using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class ClientProduct
    {
        public ClientProduct()
        {
            ProductMechanism = new HashSet<ProductMechanism>();
        }

        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ProductName { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public Client Client { get; set; }
        public ICollection<ProductMechanism> ProductMechanism { get; set; }
    }
}
