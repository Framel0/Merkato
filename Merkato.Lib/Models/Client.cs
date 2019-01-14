using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class Client
    {
        public Client()
        {
            ClientProduct = new HashSet<ClientProduct>();
            ClientRequest = new HashSet<ClientRequest>();
            Mechanism = new HashSet<Mechanism>();
        }

        public int Id { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstContactName { get; set; }
        public string FirstContactEmail { get; set; }
        public string FirstContactPhone { get; set; }
        public string SecondContactName { get; set; }
        public string SecondContactEmail { get; set; }
        public string SecondContactPhone { get; set; }
        public string ClientAppUserName { get; set; }
        public string ClientAppPassword { get; set; }
        public short? Status { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public ICollection<ClientProduct> ClientProduct { get; set; }
        public ICollection<ClientRequest> ClientRequest { get; set; }
        public ICollection<Mechanism> Mechanism { get; set; }
    }
}
