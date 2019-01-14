using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Merkato.Lib.Models;
using Merkato.Models;

namespace Merkato.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TrainingApiController : ControllerBase
    {
        private readonly MerkatoDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public TrainingApiController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainingApi
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TrainingDefinition> GetTrainingDefinition()
        {
            return _context.TrainingDefinition;
        }

        // GET: api/TrainingApi/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingDefinition([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingDefinition = await _context.TrainingDefinition.FindAsync(id);

            if (trainingDefinition == null)
            {
                return NotFound();
            }

            return Ok(trainingDefinition);
        }

        //GET Educations
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTraining/{jobId}")]
        public ApiResult<IEnumerable<TrainingModel>> GetTrainingByJob([FromRoute] int jobId)
        {
            var result = new ApiResult<IEnumerable<TrainingModel>>();

            try
            {
                var list = _context.TrainingDetails.Include(a=>a.Training)
                    .Select(b=> new TrainingModel
                    {
                        TrainingId= b.Training.Id,
                        JobId = b.Training.JobId,
                        TrainingName = b.Training.TrainingName,
                        TraningMaterial = b.Training.TraningMaterial,
                        Question = b.Question,
                        AnswerValues = b.AnswerValues,
                        AnswerCorrect = b.AnswerCorrect,
                        AnswerPoint = b.AnswerPoint
                    })
                    .Where(c=>c.JobId==jobId).ToList();

                result.Successfull = 1;
                result.Model = list;


            }
            catch (Exception ex)
            {
                result.Successfull = 0;
                result.InternalError = ex.Message;
                result.Error = "Unable to get Trainings";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet("GetTrainingMaterial/{jobId}")]
        public async Task<ApiResult<TrainingMaterialModel>> GetJobMaterial([FromRoute] int jobId)
        {
            var result = new ApiResult<TrainingMaterialModel>();

            try
            {
                var material = await _context.TrainingDefinition
                    .Select(a => new TrainingMaterialModel
                    {
                        JobId = a.JobId,
                        TraningMaterial = a.TraningMaterial
                    })
                    .Where(c => c.JobId==jobId).SingleOrDefaultAsync(); ;

                result.Successfull = 1;
                result.Model = material;


            }
            catch (Exception ex)
            {
                result.Successfull = 0;
                result.InternalError = ex.Message;
                result.Error = "Unable to get Material";
            }

            return result;
        }


        // PUT: api/TrainingApi/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trainingDefinition"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingDefinition([FromRoute] int id, [FromBody] TrainingDefinition trainingDefinition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainingDefinition.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainingDefinition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingDefinitionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TrainingApi
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trainingDefinition"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostTrainingDefinition([FromBody] TrainingDefinition trainingDefinition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TrainingDefinition.Add(trainingDefinition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingDefinition", new { id = trainingDefinition.Id }, trainingDefinition);
        }

        // DELETE: api/TrainingApi/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingDefinition([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingDefinition = await _context.TrainingDefinition.FindAsync(id);
            if (trainingDefinition == null)
            {
                return NotFound();
            }

            _context.TrainingDefinition.Remove(trainingDefinition);
            await _context.SaveChangesAsync();

            return Ok(trainingDefinition);
        }

        private bool TrainingDefinitionExists(int id)
        {
            return _context.TrainingDefinition.Any(e => e.Id == id);
        }
    }
}