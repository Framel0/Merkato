using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class SkillsProficiency
    {
        public SkillsProficiency()
        {
            ClientRequest = new HashSet<ClientRequest>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short? Active { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public ICollection<ClientRequest> ClientRequest { get; set; }
    }
}
