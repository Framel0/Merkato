using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Merkato.Lib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Merkato.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[ApiController]
    public class AgentSetupApiController : ControllerBase
    {

        private readonly MerkatoDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public AgentSetupApiController(MerkatoDbContext context)
        {
            _context = context;
        }

        //GET Educations
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        //GET Job Titles
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getJobTitles")]
        public ApiResult<IEnumerable<JobTitle>> GetJobTitle()
        {
            var result = new ApiResult<IEnumerable<JobTitle>>();

            try
            {
                var list = _context.JobTitle;

                result.Successfull = 1;
                result.Model = list;


            }
            catch (Exception ex)
            {
                result.Successfull = 0;
                result.InternalError = ex.Message;
                result.Error = "Unable to get Job Titles";
            }

            return result;
        }

        //GET Job Titles
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getBankBranches")]
        public ApiResult<IEnumerable<BankBranch>> GetBankBranches()
        {
            var result = new ApiResult<IEnumerable<BankBranch>>();

            try
            {
                var list = _context.BankBranch;

                result.Successfull = 1;
                result.Model = list;


            }
            catch (Exception ex)
            {
                result.Successfull = 0;
                result.InternalError = ex.Message;
                result.Error = "Unable to get Bank Branches";
            }

            return result;
        }

        //GET Banks
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getBanks")]
        public ApiResult<IEnumerable<Bank>> GetBank()
        {
            var result = new ApiResult<IEnumerable<Bank>>();

            try
            {
                var list = _context.Bank;

                result.Successfull = 1;
                result.Model = list;


            }
            catch (Exception ex)
            {
                result.Successfull = 0;
                result.InternalError = ex.Message;
                result.Error = "Unable to get Banks";
            }

            return result;
        }

        //GET Dependants
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getDependants")]
        public ApiResult<IEnumerable<Dependant>> getDependant()
        {
            var result = new ApiResult<IEnumerable<Dependant>>();

            try
            {
                var list = _context.Dependant;

                result.Successfull = 1;
                result.Model = list;


            }
            catch (Exception ex)
            {
                result.Successfull = 0;
                result.InternalError = ex.Message;
                result.Error = "Unable to get Dependants";
            }

            return result;
        }

        //GET Dependants
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getMaritalStatus")]
        public ApiResult<IEnumerable<MaritalStatus>> getMaritalStatus()
        {
            var result = new ApiResult<IEnumerable<MaritalStatus>>();

            try
            {
                var list = _context.MaritalStatus;

                result.Successfull = 1;
                result.Model = list;


            }
            catch (Exception ex)
            {
                result.Successfull = 0;
                result.InternalError = ex.Message;
                result.Error = "Unable to get Marital Status";
            }

            return result;
        }

        // GET: api/LocationApi
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getLocations")]
        public ApiResult<IEnumerable<Location>> GetLocation()
        {
            var result = new ApiResult<IEnumerable<Location>>();

            try
            {
                var list = _context.Location;

                result.Successfull = 1;
                result.Model = list;


            }
            catch (Exception ex)
            {
                result.Successfull = 0;
                result.InternalError = ex.Message;
                result.Error = "Unable to get Locations";
            }

            return result;
        }
    }
}