using  Merkato.Lib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
//now pls show me the controller of the grid

namespace  Merkato.Lib.ViewModels
{
    /// <summary>
    /// Agent minimum View Model
    /// </summary>
    public class AgentSmallViewModel
    {
        public int Id { get; set; }
        public string EmpCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string SurName { get; set; }
        public short? Gender { get; set; }
        public string PhoneNo { get; set; }
        public string PersonalEmail { get; set; }
        public DateTime? Dob { get; set; }
        public short? Supervisor { get; set; }
        public string DOBString { get; set; }

        public short? Status { get; set; }

        public string LocationName { get; set; }

        List<AgentStatus> AgentStatuses { get; set; }
        public AgentSmallViewModel(Agent model,List<AgentStatus> agentStatuses)
        {
            this.AgentStatuses = agentStatuses;
            this.Id = model.Id;
            this.EmpCode = model.EmpCode;
            this.FirstName = model.FirstName;
            this.SurName = model.SurName;
            this.MiddleName = model.MiddleName;
            this.Status = model.Status;
            this.Dob = model.Dob;
            this.Gender = model.Gender;          
            this.DOBString = model.DOBString;
            this.PhoneNo = model.PhoneNo;
            this.PersonalEmail = model.PersonalEmail;
            this.Supervisor = model.Supervisor;
            //this.LocationName = model.Location.Name;
            //you will complete later with the rest of the properties you want to show in the grid
        }

        /// <summary>
        /// The new field that will show the status
        /// </summary>
        public String StatusString
        {
            get
            {
                AgentStatus a = AgentStatuses.SingleOrDefault(p => p.Id == this.Status);
                if (a != null)
                {
                    return a.Name;
                }
                else
                {
                    return "";
                }
            }
        }

        public string GenderString
        {
            
            get
            {
                return Gender.HasValue?Gender == 1 ? "Male" : "Female":"";
            }
                 
        }


    }
}
