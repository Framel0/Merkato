using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Merkato.Lib.Models
{
    public partial class MerkatoDbContext : DbContext
    {
        public MerkatoDbContext()
        {
        }

        public MerkatoDbContext(DbContextOptions<MerkatoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActiveList> ActiveList { get; set; }
        public virtual DbSet<ActivityAssignment> ActivityAssignment { get; set; }
        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<AgentAvailability> AgentAvailability { get; set; }
        public virtual DbSet<AgentAvailabilityDetails> AgentAvailabilityDetails { get; set; }
        public virtual DbSet<AgentGrade> AgentGrade { get; set; }
        public virtual DbSet<AgentSelfRating> AgentSelfRating { get; set; }
        public virtual DbSet<AgentStatus> AgentStatus { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<BankBranch> BankBranch { get; set; }
        public virtual DbSet<Batch> Batch { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientProduct> ClientProduct { get; set; }
        public virtual DbSet<ClientRating> ClientRating { get; set; }
        public virtual DbSet<ClientRequest> ClientRequest { get; set; }
        public virtual DbSet<ClientRequestDetails> ClientRequestDetails { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Dependant> Dependant { get; set; }
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<JobRating> JobRating { get; set; }
        public virtual DbSet<JobTitle> JobTitle { get; set; }
        public virtual DbSet<LanguageProficiency> LanguageProficiency { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatus { get; set; }
        public virtual DbSet<Mechanism> Mechanism { get; set; }
        public virtual DbSet<Outlet> Outlet { get; set; }
        public virtual DbSet<OutletRating> OutletRating { get; set; }
        public virtual DbSet<Parameters> Parameters { get; set; }
        public virtual DbSet<PerformanceAppraisal> PerformanceAppraisal { get; set; }
        public virtual DbSet<ProductMechanism> ProductMechanism { get; set; }
        public virtual DbSet<Qualification> Qualification { get; set; }
        public virtual DbSet<RatingType> RatingType { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<ScheduleStatus> ScheduleStatus { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<SkillsProficiency> SkillsProficiency { get; set; }
        public virtual DbSet<TestResult> TestResult { get; set; }
        public virtual DbSet<Training> Training { get; set; }
        public virtual DbSet<TrainingDefinition> TrainingDefinition { get; set; }
        public virtual DbSet<TrainingDetails> TrainingDetails { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<YesOrNo> YesOrNo { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-MNOQPSQ;Database=MerkatoDB; User Id=sa; password=password;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActiveList>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<ActivityAssignment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ActivityId).ValueGeneratedOnAdd();

                entity.Property(e => e.AssignmentDate).HasColumnType("datetime");

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.ActivityAssignment)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_ActivityAssignment_ActivityAssignment");
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.Property(e => e.AccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AgentAppPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AgentAppUserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CertificatePictureUrl).HasMaxLength(50);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmpCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeEmergencyContactAlternativeEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeEmergencyContactCellPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeEmergencyContactName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExistingMedicalHistory).HasColumnType("text");

                entity.Property(e => e.FirebaseRegistrationId).IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstPictureUrl).HasMaxLength(50);

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdPictureUrl).HasMaxLength(50);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MomoPhoneNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NhifcardPictureUrl)
                    .HasColumnName("NHIFCardPictureUrl")
                    .HasMaxLength(50);

                entity.Property(e => e.Nhifno)
                    .HasColumnName("NHIFNo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note).HasColumnType("text");

                entity.Property(e => e.NssfcardPictureUrl)
                    .HasColumnName("NSSFCardPictureUrl")
                    .HasMaxLength(50);

                entity.Property(e => e.Nssfno)
                    .HasColumnName("NSSFNo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OtherInformations).IsUnicode(false);

                entity.Property(e => e.PayrollNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonalEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PincertificatePictureUrl)
                    .HasColumnName("PINCertificatePictureUrl")
                    .HasMaxLength(50);

                entity.Property(e => e.Pinno)
                    .HasColumnName("PINNo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PmscontactPhoneNo)
                    .HasColumnName("PMSContactPhoneNo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pmsrelative).HasColumnName("PMSRelative");

                entity.Property(e => e.PmsrelativeDepartment)
                    .HasColumnName("PMSRelativeDepartment")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PmsrelativeName)
                    .HasColumnName("PMSRelativeName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PmsrelativeRelationship).HasColumnName("PMSRelativeRelationship");

                entity.Property(e => e.PmsstaffWhoKnowsYourHouse)
                    .HasColumnName("PMSStaffWhoKnowsYourHouse")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryEmergencyContactEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryEmergencyContactName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrimaryEmergencyContactPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReportTo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ResumePictureUrl).HasMaxLength(50);

                entity.Property(e => e.SecondaryEmergencyContactEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecondaryEmergencyContactName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecondaryEmergencyContactPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SurName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Education)
                    .WithMany(p => p.Agent)
                    .HasForeignKey(d => d.EducationId)
                    .HasConstraintName("FK_Agent_Agent");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Agent)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Agent_Location");
            });

            modelBuilder.Entity<AgentAvailability>(entity =>
            {
                entity.Property(e => e.BatchNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Days)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentAvailability)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_AgentAvailability_Agent");
            });

            modelBuilder.Entity<AgentAvailabilityDetails>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AgentAvailabillity)
                    .WithMany(p => p.AgentAvailabilityDetails)
                    .HasForeignKey(d => d.AgentAvailabillityId)
                    .HasConstraintName("FK_AgentAvailabilityDetails_AgentAvailability");
            });

            modelBuilder.Entity<AgentGrade>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AgentSelfRating>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.RatingDate).HasColumnType("datetime");

                entity.Property(e => e.Score).ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentSelfRating)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_AgentSelfRating_Agent");
            });

            modelBuilder.Entity<AgentStatus>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BankBranch>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.BankBranch)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankBranch_Bank");
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.BatchNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.ClientAppPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientAppUserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientCode).HasMaxLength(10);

                entity.Property(e => e.ClientName)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstContactEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstContactName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstContactPhone).HasMaxLength(10);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.PhoneNumber).HasMaxLength(30);

                entity.Property(e => e.SecondContactEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecondContactName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecondContactPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientProduct>(entity =>
            {
                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientProduct)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientProduct_Client");
            });

            modelBuilder.Entity<ClientRating>(entity =>
            {
                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.RatingDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientRequest>(entity =>
            {
                entity.Property(e => e.BatchNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Days)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.OutletId).HasColumnName("outletId");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientRequest)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_ActivityDetails_Client");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.ClientRequest)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK_ActivityDetails_Gender");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.ClientRequest)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("FK_ActivityDetails_AgentGrade");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.ClientRequest)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_ActivityDetails_LanguageProficiency");

                entity.HasOne(d => d.Mechanism)
                    .WithMany(p => p.ClientRequest)
                    .HasForeignKey(d => d.MechanismId)
                    .HasConstraintName("FK_ActivityDetails_ProductMechanism");

                entity.HasOne(d => d.Outlet)
                    .WithMany(p => p.ClientRequest)
                    .HasForeignKey(d => d.OutletId)
                    .HasConstraintName("FK_ActivityDetails_Outlet");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.ClientRequest)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK_ActivityDetails_SkillsProficiency");
            });

            modelBuilder.Entity<ClientRequestDetails>(entity =>
            {
                entity.Property(e => e.AssignmentDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AssignedAgent)
                    .WithMany(p => p.ClientRequestDetails)
                    .HasForeignKey(d => d.AssignedAgentId)
                    .HasConstraintName("FK_ClientRequestDetails_Agent");

                entity.HasOne(d => d.ClientRequest)
                    .WithMany(p => p.ClientRequestDetails)
                    .HasForeignKey(d => d.ClientRequestId)
                    .HasConstraintName("FK_ClientRequestDetails_ClientRequest");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.EmailFrom).HasMaxLength(150);

                entity.Property(e => e.EmailPort).HasMaxLength(50);

                entity.Property(e => e.EmailServer).HasMaxLength(50);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Logo).HasMaxLength(50);

                entity.Property(e => e.NbDecimal).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.RequireSsl)
                    .HasColumnName("RequireSSL")
                    .HasMaxLength(10);

                entity.Property(e => e.Ssf)
                    .HasColumnName("SSF")
                    .HasMaxLength(50);

                entity.Property(e => e.ThousandSeparator).HasMaxLength(50);

                entity.Property(e => e.Tin)
                    .HasColumnName("TIN")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dependant>(entity =>
            {
                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecondaryEmergencyContactRelationship).HasMaxLength(10);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.Property(e => e.Activity1).HasMaxLength(10);

                entity.Property(e => e.Activity2).HasMaxLength(10);

                entity.Property(e => e.Activity3).HasMaxLength(10);

                entity.Property(e => e.AvailabilityDate).HasColumnType("datetime");

                entity.Property(e => e.BatchId).HasMaxLength(10);

                entity.Property(e => e.Ic)
                    .HasColumnName("IC")
                    .IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Shift1).HasMaxLength(10);

                entity.Property(e => e.Shift2).HasMaxLength(10);

                entity.Property(e => e.Shift3).HasMaxLength(10);

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.BatchNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobRating>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LanguageProficiency>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mechanism>(entity =>
            {
                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Mechanism)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Mechanism_Client");
            });

            modelBuilder.Entity<Outlet>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhoneNber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Supervisor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Outlet)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Outlet_Location");
            });

            modelBuilder.Entity<OutletRating>(entity =>
            {
                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.RatingDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Outlet)
                    .WithMany(p => p.OutletRating)
                    .HasForeignKey(d => d.OutletId)
                    .HasConstraintName("FK_OutletRating_Outlet");
            });

            modelBuilder.Entity<Parameters>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(125);
            });

            modelBuilder.Entity<PerformanceAppraisal>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.RatingDate).HasColumnType("datetime");

                entity.Property(e => e.RatingType).HasMaxLength(10);

                entity.Property(e => e.Score).ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductMechanism>(entity =>
            {
                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Mechanism)
                    .WithMany(p => p.ProductMechanism)
                    .HasForeignKey(d => d.MechanismId)
                    .HasConstraintName("FK_ProductMechanism_Mechanism");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductMechanism)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductMechanism_ClientProduct");
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RatingType>(entity =>
            {
                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name).HasMaxLength(10);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.Activity1).HasMaxLength(10);

                entity.Property(e => e.Activity2).HasMaxLength(10);

                entity.Property(e => e.Activity3).HasMaxLength(10);

                entity.Property(e => e.AvailabilityDate).HasColumnType("datetime");

                entity.Property(e => e.BatchId).HasMaxLength(10);

                entity.Property(e => e.Ic)
                    .HasColumnName("IC")
                    .IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Shift1).HasMaxLength(10);

                entity.Property(e => e.Shift2).HasMaxLength(10);

                entity.Property(e => e.Shift3).HasMaxLength(10);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ScheduleStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SkillsProficiency>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Training>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TrainingDefinition>(entity =>
            {
                entity.Property(e => e.TrainingName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TraningMaterial).HasMaxLength(250);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.TrainingDefinition)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_TrainingDefinition_JobTitle");
            });

            modelBuilder.Entity<TrainingDetails>(entity =>
            {
                entity.Property(e => e.AnswerCorrect).IsUnicode(false);

                entity.Property(e => e.AnswerValues).HasColumnType("text");

                entity.Property(e => e.Question).HasColumnType("text");

                entity.HasOne(d => d.Training)
                    .WithMany(p => p.TrainingDetails)
                    .HasForeignKey(d => d.TrainingId)
                    .HasConstraintName("FK_TrainingDetails_TrainingDefinition");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastDateModified).HasColumnType("datetime2(3)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<YesOrNo>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
