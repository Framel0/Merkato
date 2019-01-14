using  Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class EmployeeViewModel:Employee
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

        public EmployeeViewModel()
        {

        }
        public EmployeeViewModel(MerkatoDbContext _context)
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

            loadLists(_context);
            //this.PictureUrl = "PictureNA.jpg";
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
        }

        public EmployeeViewModel(MerkatoDbContext _context, Employee E) : this(_context)
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
            this.AppointmentDate = E.AppointmentDate;
            this.DepartmentId = E.DepartmentId;
            this.JobTitleId = E.JobTitleId;
            this.ReportTo = E.ReportTo;
            this.PayrollNo = E.PayrollNo;
            this.NextOfKin = E.NextOfKin;
            this.NextOfKinRelationship = E.NextOfKinRelationship;
            this.Pmsrelative = E.Pmsrelative;
            this.PmsrelativeName = E.PmsrelativeName;
            this.PmsrelativeRelationship = E.PmsrelativeRelationship;
            this.Nssfno = E.Nssfno;
            this.Nhifno = E.Nhifno;
            this.Pinno = E.Pinno;
            this.FirstPictureUrl = E.FirstPictureUrl;
            this.SecondPictureUrl = E.SecondPictureUrl;
            this.NssfcardPictureUrl = E.NssfcardPictureUrl;
            this.NhifcardPictureUrl = E.NhifcardPictureUrl;
            this.PincertificatePictureUrl = E.PincertificatePictureUrl;
            this.IdPictureUrl = E.IdPictureUrl;
            this.CertificatePictureUrl1 = E.CertificatePictureUrl1;
            this.CertificatePictureUrl2 = E.CertificatePictureUrl2;
            this.CertificatePictureUrl3 = E.CertificatePictureUrl3;
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
            this.PrimaryEmergencyContactRelationshipName = E.PrimaryEmergencyContactRelationshipName;
            this.PrimaryEmergencyContactPlaceOfWork = E.PrimaryEmergencyContactPlaceOfWork;
            this.PrimaryEmergencyContactDayPhone = E.PrimaryEmergencyContactDayPhone;
            this.PrimaryEmergencyContactEveningPhone = E.PrimaryEmergencyContactEveningPhone;
            this.SecondaryEmergencyContactName = E.SecondaryEmergencyContactName;
            this.SecondaryEmergencyContactRelationshipName = E.SecondaryEmergencyContactRelationshipName;
            this.SecondaryEmergencyContactPlaceOfWork = E.SecondaryEmergencyContactPlaceOfWork;
            this.SecondaryEmergencyContactDayPhone = E.SecondaryEmergencyContactDayPhone;
            this.SecondaryEmergencyContactEveningPhone = E.SecondaryEmergencyContactEveningPhone;
            this.ExistingMedicalHistory = E.ExistingMedicalHistory;
            this.PmsstaffWhoKnowsYourHouse = E.PmsstaffWhoKnowsYourHouse;
            this.OtherInformations = E.OtherInformations;
            this.Status = E.Status;
        }
        public Employee GetModel()
        {
            Employee e = new Employee();
            e.Id = this.Id;
            e.EmpCode = this.EmpCode;
            e.FirstName = this.FirstName;
            e.MiddleName = this.MiddleName;
            e.SurName = this.SurName;
            e.Dob = this.Dob;
            e.Gender = this.Gender;
            e.CurrentAddress = this.CurrentAddress;
            e.IdNumber = this.IdNumber;
            e.AppointmentDate = this.AppointmentDate;
            e.DepartmentId = this.DepartmentId;
            e.JobTitleId = this.JobTitleId;
            e.ReportTo = this.ReportTo;
            e.PayrollNo = this.PayrollNo;
            e.NextOfKin = this.NextOfKin;
            e.NextOfKinRelationship = this.NextOfKinRelationship;
            e.Pmsrelative = this.Pmsrelative;
            e.PmsrelativeName = this.PmsrelativeName;
            e.PmsrelativeRelationship = this.PmsrelativeRelationship;
            e.Nssfno = this.Nssfno;
            e.Nhifno = this.Nhifno;
            e.Pinno = this.Pinno;
            e.FirstPictureUrl = this.FirstPictureUrl;
            e.SecondPictureUrl = this.SecondPictureUrl;
            e.NssfcardPictureUrl = this.NssfcardPictureUrl;
            e.NhifcardPictureUrl = this.NhifcardPictureUrl;
            e.PincertificatePictureUrl = this.PincertificatePictureUrl;
            e.IdPictureUrl = this.IdPictureUrl;
            e.CertificatePictureUrl1 = this.CertificatePictureUrl1;
            e.CertificatePictureUrl2 = this.CertificatePictureUrl2;
            e.CertificatePictureUrl3 = this.CertificatePictureUrl3;
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
            e.PrimaryEmergencyContactRelationshipName = this.PrimaryEmergencyContactRelationshipName;
            e.PrimaryEmergencyContactPlaceOfWork = this.PrimaryEmergencyContactPlaceOfWork;
            e.PrimaryEmergencyContactDayPhone = this.PrimaryEmergencyContactDayPhone;
            e.PrimaryEmergencyContactEveningPhone = this.PrimaryEmergencyContactEveningPhone;
            e.SecondaryEmergencyContactName = this.SecondaryEmergencyContactName;
            e.SecondaryEmergencyContactRelationshipName = this.SecondaryEmergencyContactRelationshipName;
            e.SecondaryEmergencyContactPlaceOfWork = this.SecondaryEmergencyContactPlaceOfWork;
            e.SecondaryEmergencyContactDayPhone = this.SecondaryEmergencyContactDayPhone;
            e.SecondaryEmergencyContactEveningPhone = this.SecondaryEmergencyContactEveningPhone;
            e.ExistingMedicalHistory = this.ExistingMedicalHistory;
            e.PmsstaffWhoKnowsYourHouse = this.PmsstaffWhoKnowsYourHouse;
            e.OtherInformations = this.OtherInformations;
            e.Status = this.Status;


            return e;
        }
    }
}
