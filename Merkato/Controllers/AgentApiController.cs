using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Merkato.Lib.Models;
using Merkato.Lib.Models.ServiceModel;
using Merkato.Lib.ViewModels;
namespace Merkato.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]

    public class AgentApiController : ControllerBase
    {
        private readonly MerkatoDbContext _context;

        EmailNotificationSender _mailSender;

        DataManager dataManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public AgentApiController(MerkatoDbContext context)
        {
            _context = context;
            _mailSender = new EmailNotificationSender();
            dataManager = new DataManager();
        }

        // GET: api/AgentApi
        /// <summary>
        /// Get All Agents
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IEnumerable<Agent> GetAgent()
        {          
            return _context.Agent;
        }

        // GET: api/AgentApi/5
        /// <summary>
        /// Get One Agent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agent = await _context.Agent.FindAsync(id);

            if (agent == null)
            {
                return NotFound();
            }

            return Ok(agent);
        }

        // PUT: api/AgentApi/5
        /// <summary>
        /// Update and Agent
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agent"></param>
        /// <returns></returns>
        [HttpPost("UpdateAgent/{id}")]
        public async Task<ApiResult<Agent>> UpdateAgent([FromRoute] int id, [FromBody] Agent agent)
        {
            ApiResult<Agent> result = new ApiResult<Agent>();

            if (!ModelState.IsValid)
            {
                result.Successfull = 0;
                result.Error = "All the fields are not filled";
                result.InternalError = ModelState.Values.ToString();
                return result;
            }

            if (id != agent.Id)
            {
                result.Successfull = 0;
                result.Error = "Bad Request The agent doesn't exist";
                result.InternalError = ModelState.Values.ToString();
                return result;
            }

            //Agent agent = new Agent
            //{
            //    Id = model.Id,
            //    //EmpCode = model.EmpCode,
            //    //FirstName = model.FirstName,
            //    //MiddleName = model.MiddleName,
            //    //SurName = model.SurName,
            //    Dob = model.Dob,
            //    Gender = model.Gender,
            //    CurrentAddress = model.CurrentAddress,
            //    //IdNumber = model.IdNumber,
            //    //DepartmentId = model.DepartmentId,
            //    JobTitleId = model.JobTitleId,
            //    //ReportTo = model.ReportTo,
            //    //PayrollNo = model.PayrollNo,
            //    NextOfKin = model.NextOfKin,
            //    NextOfKinRelationship = model.NextOfKinRelationship,
            //    NextOfKinPhoneNo = model.NextOfKinPhoneNo,
            //    NextOfKinEmail = model.NextOfKinEmail,
            //    NextOfKinWork = model.NextOfKinWork,
            //    NextOfKinWorkContact = model.NextOfKinWorkContact,
            //    //Pmsrelative = model.Pmsrelative,
            //    PmsrelativeName = model.PmsrelativeName,
            //    //PmsrelativeRelationship = model.PmsrelativeRelationship,
            //    PmsrelativeDepartment = model.PmsrelativeDepartment,
            //    Nssfno = model.Nssfno,
            //    Nhifno = model.Nhifno,
            //    Pinno = model.Pinno,
            //    //FirstPictureUrl = model.FirstPictureUrl,
            //    //NssfcardPictureUrl = model.NssfcardPictureUrl,
            //    //NhifcardPictureUrl = model.NhifcardPictureUrl,
            //    //PincertificatePictureUrl = model.PincertificatePictureUrl,
            //    //IdPictureUrl = model.IdPictureUrl,
            //    //CertificatePictureUrl = model.CertificatePictureUrl,
            //    //ResumePictureUrl = model.ResumePictureUrl,
            //    MainBankId = model.MainBankId,
            //    BankBranchId = model.BankBranchId,
            //    AccountName = model.AccountName,
            //    AccountNo = model.AccountNo,
            //    //MaritalStatus = model.MaritalStatus,
            //    //PhoneNo = model.PhoneNo,
            //    //Address = model.Address,
            //    //NbDependants = model.NbDependants,
            //    //ResidentialAddress = model.ResidentialAddress,
            //    //PersonalEmail = model.PersonalEmail,
            //    //EmployeeEmergencyContactName = model.EmployeeEmergencyContactName,
            //    //EmployeeEmergencyContactResidential = model.EmployeeEmergencyContactResidential,
            //    //EmployeeEmergencyContactNearestPlace = model.EmployeeEmergencyContactNearestPlace,
            //    //EmployeeEmergencyContactCellPhone = model.EmployeeEmergencyContactCellPhone,
            //    //EmployeeEmergencyContactAlternativeEmail = model.EmployeeEmergencyContactAlternativeEmail,
            //    //PrimaryEmergencyContactName = model.PrimaryEmergencyContactName,
            //    //PrimaryEmergencyContactRelationshipName = model.PrimaryEmergencyContactRelationshipName,
            //    //PrimaryEmergencyContactPlaceOfWork = model.PrimaryEmergencyContactPlaceOfWork,
            //    //PrimaryEmergencyContactDayPhone = model.PrimaryEmergencyContactDayPhone,
            //    //PrimaryEmergencyContactEveningPhone = model.PrimaryEmergencyContactEveningPhone,
            //    //SecondaryEmergencyContactName = model.SecondaryEmergencyContactName,
            //    //SecondaryEmergencyContactRelationshipName = model.SecondaryEmergencyContactRelationshipName,
            //    //SecondaryEmergencyContactPlaceOfWork = model.SecondaryEmergencyContactPlaceOfWork,
            //    //SecondaryEmergencyContactDayPhone = model.SecondaryEmergencyContactDayPhone,
            //    //SecondaryEmergencyContactEveningPhone = model.SecondaryEmergencyContactEveningPhone,
            //    ExistingMedicalHistory = model.ExistingMedicalHistory,
            //    //PmsstaffWhoKnowsYourHouse = model.PmsstaffWhoKnowsYourHouse,
            //    //OtherInformations = model.OtherInformations,
            //    LocationId = model.LocationId,
            //    MomoPhoneNo = model.MomoPhoneNo,
            //    //Note = model.Note,
            //    //PmscontactPhoneNo = model.PmscontactPhoneNo,
            //    //Status = model.Status,
            //    //Supervisor = model.Supervisor,
            //    //EducationId = model.EducationId,
            //    //AgentAppUserName = model.PersonalEmail,
            //    //AgentAppPassword = model.AgentAppPassword
            //};

            //_context.Entry(agent).State = EntityState.Modified;

            try
            {
                _context.Agent.Update(agent);
                await _context.SaveChangesAsync();
                result.Successfull = 1;
                result.Model = agent;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    result.Successfull = 0;
                    result.Error = "Agent Not Found";
                    result.InternalError = ModelState.Values.ToString();
                    return result;
                }
                else
                {
                    throw;
                }
            }

            return result;
        }

        // POST: api/AgentApi
        /// <summary>
        /// Create an Agent
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<IActionResult> PostEmployee([FromBody] Agent employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Agent.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        /// <summary>
        /// Login into the Application
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ApiResult<Agent>> Login([FromBody] LoginModel login)
        {
            ApiResult<Agent> result = new ApiResult<Agent>();

            result.Successfull = 0;
            try
            {
                var agent = await _context.Agent.Where(c => c.PersonalEmail.Equals(login.AgentUserName) && c.AgentAppPassword.Equals(login.AgentPassword)).SingleOrDefaultAsync();

                if(agent!=null)
                {
                    result.Model = agent;
                    result.Successfull = 1;
                }
                else
                {
                    result.Successfull = 0;
                    result.Error = "Wrong UserName or password";
                }
             
            }
            catch (Exception ex)
            {
                result.Successfull = 0;
                result.InternalError = ex.Message;
                result.Error = "Login failed";
            }

            return result;
        }

        /// <summary>
        /// Agent Registration Request
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<ApiResult<Agent>> Register([FromBody] RegisterModel model)
        {
            ApiResult<Agent> result = new ApiResult<Agent>();
            if (!ModelState.IsValid)
            {
                result.Successfull = 0;
                result.Error = "All the fields are not filled";
                result.InternalError=ModelState.Values.ToString();
                return result;
            }
            try
            {
                Agent agent = new Agent
                {
                    FirstName = model.FirstName,
                    SurName = model.SurName,
                    MiddleName = model.MiddleName,
                    PhoneNo = model.PhoneNo,
                    PersonalEmail = model.PersonalEmail,
                    AgentAppPassword = model.AgentAppPassword

                };

                if (!AgentExists(agent.PersonalEmail))
                {
                    _context.Agent.Add(agent);
                    await _context.SaveChangesAsync();

                    result.Model = agent;
                    result.Successfull = 1;



                    _mailSender.SendEmailWithVerification(agent.PersonalEmail, "Merkato Notification",
                 $"Merkato has been accepted your request, kindly go ahead and complete your registration.{Environment.NewLine} -Username :{agent.PersonalEmail} {Environment.NewLine} -Password:{agent.AgentAppPassword}"
               + $"{Environment.NewLine} -Application link http://173.248.135.167/Merkato"
               + $"{Environment.NewLine}  {Environment.NewLine} {Environment.NewLine} Merkato Team", _context, false);

                    await dataManager.PreApprovedAgent(agent.PersonalEmail, _context);

                }



                else
                {
                    result.Error = "Email already registered";
                    result.Successfull = 0;
                }
             
            }
            catch(Exception ex)
            {
                result.Error = "Failed to  Create agent";
                result.InternalError = ex.Message;
                result.Successfull = 0;
            }              

            return result;
        }

        private bool AgentExists(string email)
        {
            return _context.Agent.Any(e => e.PersonalEmail == email);
        }


        // DELETE: api/AgentApi/5
        /// <summary>
        /// Delete Agent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Agent.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Agent.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Agent.Any(e => e.Id == id);
        }
    }
}