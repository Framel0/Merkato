using  Merkato.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class ClientRequestDetailsViewModel:ClientRequestDetails
    {
        public string BatchNo { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string OutletName { get; set; }

        public string MechanismName { get; set; }

        public string LanguageName { get; set; }
        public Int16? NbAgentShift1 { get; set; }

        public Int16? NbAgentShift2 { get; set; }
        public Int16? NbAgentShift3 { get; set; }
        public Int16? NbAgentShift4 { get; set; }
        public String DateString
        {

            get
            {
                return Date.HasValue ? Date.Value.ToString("dd-MM-yyyy") : "";
            }
            set
            {
                Date = DateTime.ParseExact(value, "dd-MM-yyyy", null);

            }
        }
        public ClientRequestDetailsViewModel()
        {

        }

        public ClientRequestDetailsViewModel(MerkatoDbContext context)
        {

        }

        public ClientRequestDetailsViewModel(MerkatoDbContext context, ClientRequestDetails requestDetails):this(context)
        {
            this.Id = requestDetails.Id;
            this.ClientRequestId = requestDetails.ClientRequestId;
            this.Date = requestDetails.Date;
            this.AssignedAgentId = requestDetails.AssignedAgentId;
            this.AssignmentDate = requestDetails.AssignmentDate;
            this.UserId = requestDetails.UserId;
            this.LastDateModified = requestDetails.LastDateModified;
        }

        public ClientRequestDetails GetModel()
        {
            ClientRequestDetails details = new ClientRequestDetails();

            details.Id = this.Id;
            details.ClientRequestId = this.ClientRequestId;
            details.Date = this.Date;
            details.AssignedAgentId = this.AssignedAgentId;
            details.AssignmentDate = this.AssignmentDate;
            details.UserId = this.UserId;
            details.LastDateModified = this.LastDateModified;
            

            return details;
        }
    }
}
