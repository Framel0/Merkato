using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class ProductMechanism
    {
        public ProductMechanism()
        {
            ClientRequest = new HashSet<ClientRequest>();
        }

        public int Id { get; set; }
        public int? MechanismId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public short? ProductOrder { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public Mechanism Mechanism { get; set; }
        public ClientProduct Product { get; set; }
        public ICollection<ClientRequest> ClientRequest { get; set; }
    }
}
