using System;
using System.Collections.Generic;

namespace  Merkato.Lib.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string EmpCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string SurName { get; set; }
        public DateTime? Dob { get; set; }
        public short? Gender { get; set; }
        public string CurrentAddress { get; set; }
        public string IdNumber { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? JobTitleId { get; set; }
        public string ReportTo { get; set; }
        public string PayrollNo { get; set; }
        public string NextOfKin { get; set; }
        public short? NextOfKinRelationship { get; set; }
        public short? Pmsrelative { get; set; }
        public string PmsrelativeName { get; set; }
        public short? PmsrelativeRelationship { get; set; }
        public string Nssfno { get; set; }
        public string Nhifno { get; set; }
        public string Pinno { get; set; }
        public string FirstPictureUrl { get; set; }
        public string SecondPictureUrl { get; set; }
        public string NssfcardPictureUrl { get; set; }
        public string NhifcardPictureUrl { get; set; }
        public string PincertificatePictureUrl { get; set; }
        public string IdPictureUrl { get; set; }
        public string CertificatePictureUrl1 { get; set; }
        public string CertificatePictureUrl2 { get; set; }
        public string CertificatePictureUrl3 { get; set; }
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
        public string PrimaryEmergencyContactRelationshipName { get; set; }
        public string PrimaryEmergencyContactPlaceOfWork { get; set; }
        public string PrimaryEmergencyContactDayPhone { get; set; }
        public string PrimaryEmergencyContactEveningPhone { get; set; }
        public string SecondaryEmergencyContactName { get; set; }
        public string SecondaryEmergencyContactRelationshipName { get; set; }
        public string SecondaryEmergencyContactPlaceOfWork { get; set; }
        public string SecondaryEmergencyContactDayPhone { get; set; }
        public string SecondaryEmergencyContactEveningPhone { get; set; }
        public string ExistingMedicalHistory { get; set; }
        public string PmsstaffWhoKnowsYourHouse { get; set; }
        public string OtherInformations { get; set; }
        public short? Status { get; set; }
    }
}
