using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class Education
    {
        public Education()
        {
            Agent = new HashSet<Agent>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short? EduOder { get; set; }
        public short? Active { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public ICollection<Agent> Agent { get; set; }
    }
}
