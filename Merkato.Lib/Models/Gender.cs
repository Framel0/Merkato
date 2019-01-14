using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class Gender
    {
        public Gender()
        {
            ClientRequest = new HashSet<ClientRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public ICollection<ClientRequest> ClientRequest { get; set; }
    }
}
