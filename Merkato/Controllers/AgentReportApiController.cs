using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Merkato.Lib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Merkato.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[ApiController]
    public class AgentReportApiController : Controller
    {
        private readonly MerkatoDbContext _context;

        public AgentReportApiController(MerkatoDbContext context)
        {
            _context = context;
        }

        //Get All Genders
        [HttpGet("getGenders")]
        public ApiResult<IEnumerable<Gender>> GetGenders()
        {
            var result = new ApiResult<IEnumerable<Gender>>();

            try
            {
                var list = _context.Gender;

                result.Successfull = 1;
                result.Model = list;

                 
            }
            catch (Exception ex)
            {
                result.Successfull = 0;
                result.InternalError = ex.Message;
                result.Error = "Unable to get Genders";
            }

            return result;
        }

        [HttpGet("getEducations")]
        public ApiResult<IEnumerable<Education>> GetEducation()
        {
            var result = new ApiResult<IEnumerable<Education>>();

            try
            {
                var list = _context.Education;

                result.Successfull = 1;
                result.Model = list;


            }
            catch (Exception ex)
            {
                result.Successfull = 0;
                result.InternalError = ex.Message;
                result.Error = "Unable to get Educations";
            }

            return result;
        }


        //Get All Marital Status
        [HttpGet("getMaritalStatus")]
        public IEnumerable<MaritalStatus> GetMaritalStatus()
        {
            return _context.MaritalStatus;
        }
        //Get All  Status
        [HttpGet("getStatus")]
        public IEnumerable<AgentStatus> GetStatus()
        {
            return _context.AgentStatus;
        }
    }
}