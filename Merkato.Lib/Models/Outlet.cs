using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class Outlet
    {
        public Outlet()
        {
            ClientRequest = new HashSet<ClientRequest>();
            OutletRating = new HashSet<OutletRating>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNber { get; set; }
        public string Supervisor { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int LocationId { get; set; }
        public short? Status { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public Location Location { get; set; }
        public ICollection<ClientRequest> ClientRequest { get; set; }
        public ICollection<OutletRating> OutletRating { get; set; }
    }
}
