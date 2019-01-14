using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class ClientRequest
    {
        public ClientRequest()
        {
            ClientRequestDetails = new HashSet<ClientRequestDetails>();
        }

        public int Id { get; set; }
        public string BatchNo { get; set; }
        public int? ClientId { get; set; }
        public int? OutletId { get; set; }
        public int? MechanismId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Days { get; set; }
        public short? NbAgentShift1 { get; set; }
        public short? NbAgentShift2 { get; set; }
        public short? NbAgentShift3 { get; set; }
        public short? NbAgentShift4 { get; set; }
        public int? GenderId { get; set; }
        public int? Age { get; set; }
        public int? SkillId { get; set; }
        public int? LanguageId { get; set; }
        public int? GradeId { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public Client Client { get; set; }
        public Gender Gender { get; set; }
        public AgentGrade Grade { get; set; }
        public LanguageProficiency Language { get; set; }
        public ProductMechanism Mechanism { get; set; }
        public Outlet Outlet { get; set; }
        public SkillsProficiency Skill { get; set; }
        public ICollection<ClientRequestDetails> ClientRequestDetails { get; set; }
    }
}
