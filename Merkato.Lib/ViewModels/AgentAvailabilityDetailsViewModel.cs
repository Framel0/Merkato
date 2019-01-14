using  Merkato.Lib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class AgentAvailabilityDetailsViewModel : AgentAvailabilityDetails
    {
        public string BatchNo { get; set; }
        public string AgentName { get; set; }
        public bool NbAgentFirstShift { get; set; }
        public bool NbAgentSecondShift { get; set; }
        public bool NbAgentThirdShift { get; set; }
        public bool NbAgentFourthShift { get; set; }

        public string IsFirstShiftActive
        {
            get
            {
                return (bool)this.NbAgentFirstShift ? "Yes" : "NO";
            }
        }

        public string IsSecondShiftActive
        {
            get
            {
                return (bool)this.NbAgentSecondShift ? "Yes" : "NO";
            }
        }

        public string IsThirdShiftActive
        {
            get
            {
                return (bool)this.NbAgentThirdShift ? "Yes" : "NO";
            }
        }

        public string IsFourthShiftActive
        {
            get
            {
                return (bool)this.NbAgentFourthShift ? "Yes" : "NO";
            }
        }
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

       

        public AgentAvailabilityDetailsViewModel()
        {

        }

        public AgentAvailabilityDetailsViewModel(MerkatoDbContext context)
        {

        }

        public AgentAvailabilityDetailsViewModel(MerkatoDbContext context, AgentAvailabilityDetails availabilityDetails) : this(context)
        {
            this.Id = availabilityDetails.Id;
            this.Date = availabilityDetails.Date;
            this.AgentAvailabillityId = availabilityDetails.AgentAvailabillityId;
            this.UserId = availabilityDetails.UserId;
            this.LastDateModified = availabilityDetails.LastDateModified;
        }

        public AgentAvailabilityDetails GetModel()
        {
            AgentAvailabilityDetails details = new AgentAvailabilityDetails();

            details.Id = this.Id;
            details.Date = this.Date;
            details.AgentAvailabillityId = this.AgentAvailabillityId;
            details.UserId = this.UserId;
            details.LastDateModified = this.LastDateModified;


            return details;
        }
    }
}
