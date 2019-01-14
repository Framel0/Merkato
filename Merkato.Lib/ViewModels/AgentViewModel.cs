using  Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class AgentViewModel: Agent
    {

        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        public List<SelectListItem> GenderList { get; set; }
        public List<SelectListItem> BankList { get; set; }
        public List<SelectListItem> JobTitleList { get; set; }
        public List<SelectListItem> MaritalStatusList { get; set; }
        public List<SelectListItem> DependantList { get; set; }
        public List<SelectListItem> BankBranchList { get; set; }

        public List<SelectListItem> YesOrNoList { get; set; }
        public List<SelectListItem> LocationList { get; set; }
        public List<SelectListItem> EducationList { get; set; }


        public AgentViewModel()
        {

        }
        public AgentViewModel(MerkatoDbContext _context)
        {
            DepartmentList = new List<SelectListItem>();
            StatusList = new List<SelectListItem>();
            BankList = new List<SelectListItem>();
            JobTitleList = new List<SelectListItem>();
            MaritalStatusList = new List<SelectListItem>();
            DependantList = new List<SelectListItem>();
            BankBranchList = new List<SelectListItem>();
            GenderList = new List<SelectListItem>();
            YesOrNoList = new List<SelectListItem>();
            LocationList = new List<SelectListItem>();
            EducationList = new List<SelectListItem>();

            loadLists(_context);
            this.FirstPictureUrl = "PictureNA.jpg";
            Id = 0;

        }

        public void loadLists(MerkatoDbContext _context)
        {
            DepartmentList = _context.Department.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            StatusList = _context.AgentStatus.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            BankList = _context.Bank.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            JobTitleList = _context.JobTitle.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            MaritalStatusList = _context.MaritalStatus.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            DependantList = _context.Dependant.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            BankBranchList = _context.BankBranch.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            GenderList = _context.Gender.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            YesOrNoList = _context.YesOrNo.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            LocationList = _context.Location.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            EducationList = _context.Education.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
        }

        public AgentViewModel(MerkatoDbContext _context, Agent E) : this(_context)
        {
            this.Id = E.Id;
            this.EmpCode = E.EmpCode;
            this.FirstName = E.FirstName;
            this.MiddleName = E.MiddleName;
            this.SurName = E.SurName;
            this.Dob = E.Dob;
            this.Gender = E.Gender;
            this.CurrentAddress = E.CurrentAddress;
            this.IdNumber = E.IdNumber;
            this.DepartmentId = E.DepartmentId;
            this.JobTitleId = E.JobTitleId;
            this.ReportTo = E.ReportTo;
            this.PayrollNo = E.PayrollNo;
            
            this.Pmsrelative = E.Pmsrelative;
            this.PmsrelativeName = E.PmsrelativeName;
            this.PmsrelativeRelationship = E.PmsrelativeRelationship;
            this.PmsrelativeDepartment = E.PmsrelativeDepartment;
            this.Nssfno = E.Nssfno;
            this.Nhifno = E.Nhifno;
            this.Pinno = E.Pinno;
            this.FirstPictureUrl = E.FirstPictureUrl;
            this.NssfcardPictureUrl = E.NssfcardPictureUrl;
            this.NhifcardPictureUrl = E.NhifcardPictureUrl;
            this.PincertificatePictureUrl = E.PincertificatePictureUrl;
            this.IdPictureUrl = E.IdPictureUrl;
            this.CertificatePictureUrl = E.CertificatePictureUrl;
            this.ResumePictureUrl = E.ResumePictureUrl;
            this.MainBankId = E.MainBankId;
            this.BankBranchId = E.BankBranchId;
            this.AccountName = E.AccountName;
            this.AccountNo = E.AccountNo;
            this.MaritalStatus = E.MaritalStatus;
            this.PhoneNo = E.PhoneNo;
            this.Address = E.Address;
            this.NbDependants = E.NbDependants;
            this.ResidentialAddress = E.ResidentialAddress;
            this.PersonalEmail = E.PersonalEmail;
            this.EmployeeEmergencyContactName = E.EmployeeEmergencyContactName;
            this.EmployeeEmergencyContactResidential = E.EmployeeEmergencyContactResidential;
            this.EmployeeEmergencyContactNearestPlace = E.EmployeeEmergencyContactNearestPlace;
            this.EmployeeEmergencyContactCellPhone = E.EmployeeEmergencyContactCellPhone;
            this.EmployeeEmergencyContactAlternativeEmail = E.EmployeeEmergencyContactAlternativeEmail;
            this.PrimaryEmergencyContactName = E.PrimaryEmergencyContactName;
            this.PrimaryEmergencyContactRelationship = E.PrimaryEmergencyContactRelationship;
            this.PrimaryEmergencyContactPlaceOfWork = E.PrimaryEmergencyContactPlaceOfWork;
            this.PrimaryEmergencyContactPhone = E.PrimaryEmergencyContactPhone;
            this.PrimaryEmergencyContactEmail = E.PrimaryEmergencyContactEmail;
            this.SecondaryEmergencyContactName = E.SecondaryEmergencyContactName;
            this.SecondaryEmergencyContactRelationship = E.SecondaryEmergencyContactRelationship;
            this.SecondaryEmergencyContactPlaceOfWork = E.SecondaryEmergencyContactPlaceOfWork;
            this.SecondaryEmergencyContactPhone = E.SecondaryEmergencyContactPhone;
            this.SecondaryEmergencyContactEmail = E.SecondaryEmergencyContactEmail;
            this.ExistingMedicalHistory = E.ExistingMedicalHistory;
            this.PmsstaffWhoKnowsYourHouse = E.PmsstaffWhoKnowsYourHouse;
            this.OtherInformations = E.OtherInformations;
            this.Status = E.Status;
            this.Supervisor = E.Supervisor;
            this.LocationId = E.LocationId;
            this.AgentAppUserName = E.AgentAppUserName;
            this.AgentAppPassword = E.AgentAppPassword;
            this.PmscontactPhoneNo = E.PmscontactPhoneNo;
            this.MomoPhoneNo = E.MomoPhoneNo;
            this.IsActive = E.IsActive;
            this.EducationId = E.EducationId;
            this.Note = E.Note;
        }
        public Agent GetModel()
        {
            Agent e = new Agent();
            e.Id = this.Id;
            e.EmpCode = this.EmpCode;
            e.FirstName = this.FirstName;
            e.MiddleName = this.MiddleName;
            e.SurName = this.SurName;
            e.Dob = this.Dob;
            e.Gender = this.Gender;
            e.CurrentAddress = this.CurrentAddress;
            e.IdNumber = this.IdNumber;
            e.DepartmentId = this.DepartmentId;
            e.JobTitleId = this.JobTitleId;
            e.ReportTo = this.ReportTo;
            e.PayrollNo = this.PayrollNo;
            e.Pmsrelative = this.Pmsrelative;
            e.PmsrelativeName = this.PmsrelativeName;
            e.PmsrelativeRelationship = this.PmsrelativeRelationship;
            e.PmsrelativeDepartment = this.PmsrelativeDepartment;
            e.Nssfno = this.Nssfno;
            e.Nhifno = this.Nhifno;
            e.Pinno = this.Pinno;
            e.FirstPictureUrl = this.FirstPictureUrl;
            e.NssfcardPictureUrl = this.NssfcardPictureUrl;
            e.NhifcardPictureUrl = this.NhifcardPictureUrl;
            e.PincertificatePictureUrl = this.PincertificatePictureUrl;
            e.IdPictureUrl = this.IdPictureUrl;
            e.CertificatePictureUrl = this.CertificatePictureUrl;
            e.ResumePictureUrl = this.ResumePictureUrl;
            e.MainBankId = this.MainBankId;
            e.BankBranchId = this.BankBranchId;
            e.AccountName = this.AccountName;
            e.AccountNo = this.AccountNo;
            e.MaritalStatus = this.MaritalStatus;
            e.PhoneNo = this.PhoneNo;
            e.Address = this.Address;
            e.NbDependants = this.NbDependants;
            e.ResidentialAddress = this.ResidentialAddress;
            e.PersonalEmail = this.PersonalEmail;
            e.EmployeeEmergencyContactName = this.EmployeeEmergencyContactName;
            e.EmployeeEmergencyContactResidential = this.EmployeeEmergencyContactResidential;
            e.EmployeeEmergencyContactNearestPlace = this.EmployeeEmergencyContactNearestPlace;
            e.EmployeeEmergencyContactCellPhone = this.EmployeeEmergencyContactCellPhone;
            e.EmployeeEmergencyContactAlternativeEmail = this.EmployeeEmergencyContactAlternativeEmail;
            e.PrimaryEmergencyContactName = this.PrimaryEmergencyContactName;
            e.PrimaryEmergencyContactRelationship = this.PrimaryEmergencyContactRelationship;
            e.PrimaryEmergencyContactPlaceOfWork = this.PrimaryEmergencyContactPlaceOfWork;
            e.PrimaryEmergencyContactPhone = this.PrimaryEmergencyContactPhone;
            e.PrimaryEmergencyContactEmail = this.PrimaryEmergencyContactEmail;
            e.SecondaryEmergencyContactName = this.SecondaryEmergencyContactName;
            e.SecondaryEmergencyContactRelationship = this.SecondaryEmergencyContactRelationship;
            e.SecondaryEmergencyContactPlaceOfWork = this.SecondaryEmergencyContactPlaceOfWork;
            e.SecondaryEmergencyContactPhone = this.SecondaryEmergencyContactPhone;
            e.SecondaryEmergencyContactEmail = this.SecondaryEmergencyContactEmail;
            e.ExistingMedicalHistory = this.ExistingMedicalHistory;
            e.PmsstaffWhoKnowsYourHouse = this.PmsstaffWhoKnowsYourHouse;
            e.OtherInformations = this.OtherInformations;
            e.Status = this.Status;
            e.Supervisor = this.Supervisor;
            e.LocationId = this.LocationId;
            e.AgentAppUserName = this.AgentAppUserName;
            e.AgentAppPassword = this.AgentAppPassword;
            e.PmscontactPhoneNo = this.PmscontactPhoneNo;
            e.MomoPhoneNo = this.MomoPhoneNo;
            e.IsActive = this.IsActive;
            e.EducationId = this.EducationId;
            e.Note = this.Note;

            return e;
        }
    }
}
