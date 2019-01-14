using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Merkato.Lib.Models
{
    public partial class Agent
    {
        public Agent()
        {
            ActivityAssignment = new HashSet<ActivityAssignment>();
            AgentAvailability = new HashSet<AgentAvailability>();
            AgentSelfRating = new HashSet<AgentSelfRating>();
            ClientRequestDetails = new HashSet<ClientRequestDetails>();
        }

        public int Id { get; set; }
        public string EmpCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string SurName { get; set; }
        public DateTime? Dob { get; set; }
        public short? Gender { get; set; }
        public string CurrentAddress { get; set; }
        public string IdNumber { get; set; }
        public int? DepartmentId { get; set; }
        public int? JobTitleId { get; set; }
        public string ReportTo { get; set; }
        public string PayrollNo { get; set; }
        public short? Pmsrelative { get; set; }
        public string PmsrelativeName { get; set; }
        public short? PmsrelativeRelationship { get; set; }
        public string PmsrelativeDepartment { get; set; }
        public string Nssfno { get; set; }
        public string Nhifno { get; set; }
        public string Pinno { get; set; }
        public string FirstPictureUrl { get; set; }
        public string NssfcardPictureUrl { get; set; }
        public string NhifcardPictureUrl { get; set; }
        public string PincertificatePictureUrl { get; set; }
        public string IdPictureUrl { get; set; }
        public string CertificatePictureUrl { get; set; }
        public string ResumePictureUrl { get; set; }
        public int? MainBankId { get; set; }
        public int? BankBranchId { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public short? MaritalStatus { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public short? NbDependants { get; set; }
        public string ResidentialAddress { get; set; }
        public string PersonalEmail { get; set; }
        public string EmployeeEmergencyContactName { get; set; }
        public string EmployeeEmergencyContactResidential { get; set; }
        public string EmployeeEmergencyContactNearestPlace { get; set; }
        public string EmployeeEmergencyContactCellPhone { get; set; }
        public string EmployeeEmergencyContactAlternativeEmail { get; set; }
        public string PrimaryEmergencyContactName { get; set; }
        public short? PrimaryEmergencyContactRelationship { get; set; }
        public string PrimaryEmergencyContactPlaceOfWork { get; set; }
        public string PrimaryEmergencyContactPhone { get; set; }
        public string PrimaryEmergencyContactEmail { get; set; }
        public string SecondaryEmergencyContactName { get; set; }
        public short? SecondaryEmergencyContactRelationship { get; set; }
        public string SecondaryEmergencyContactPlaceOfWork { get; set; }
        public string SecondaryEmergencyContactPhone { get; set; }
        public string SecondaryEmergencyContactEmail { get; set; }
        public string ExistingMedicalHistory { get; set; }
        public string PmsstaffWhoKnowsYourHouse { get; set; }
        public string OtherInformations { get; set; }
        public int? LocationId { get; set; }
        public string MomoPhoneNo { get; set; }
        public string Note { get; set; }
        public string PmscontactPhoneNo { get; set; }
        public short? Status { get; set; }
        public short? Supervisor { get; set; }
        public int? EducationId { get; set; }
        public string AgentAppUserName { get; set; }
        public string AgentAppPassword { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }
        public string FirebaseRegistrationId { get; set; }

        public Education Education { get; set; }
        public Location Location { get; set; }
        public ICollection<ActivityAssignment> ActivityAssignment { get; set; }
        public ICollection<AgentAvailability> AgentAvailability { get; set; }
        public ICollection<AgentSelfRating> AgentSelfRating { get; set; }
        public ICollection<ClientRequestDetails> ClientRequestDetails { get; set; }

        [NotMapped]
        public String DOBString
        {

            get
            {
                return Dob.HasValue ? Dob.Value.ToString("dd-MM-yyyy") : "";
            }
            set
            {
                Dob = DateTime.ParseExact(value, "dd-MM-yyyy", null);

            }
        }

    }
}
