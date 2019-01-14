using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class Location
    {
        public Location()
        {
            Agent = new HashSet<Agent>();
            Outlet = new HashSet<Outlet>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public short? Status { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public ICollection<Agent> Agent { get; set; }
        public ICollection<Outlet> Outlet { get; set; }
    }
}
