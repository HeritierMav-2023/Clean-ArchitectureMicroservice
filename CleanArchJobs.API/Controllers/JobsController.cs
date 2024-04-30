using CleanArchJobs.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchJobs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        //1-reference repository
        private readonly IJobsRepository _jobRepository;

        //2-Constructeur DI
        public JobsController(IJobsRepository jobRepositorie)
        {
            _jobRepository = jobRepositorie; 
        }

        #region Methods Verbs

        [HttpGet]
        public ActionResult<Domain.Entities.Jobs> GetAllsJob()
        {
            var data = _jobRepository.GetAll();
            if (data != null)
            {
                return Ok(data);
            }
            return NotFound();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Domain.Entities.Jobs>> GetData(int Id)
        {
            return await _jobRepository.GetByIdAsync(Id);

        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Domain.Entities.Jobs job)
        {
            if (job == null)
            {
                return NotFound();
            }
            return await _jobRepository.AddAsync(job);
        }

        [HttpPut]
        public async Task<ActionResult<int>> Put(Domain.Entities.Jobs job)
        {
            try
            {
                if (job.Id == 0)
                    return null;
                else
                    return await _jobRepository.UpdateAsync(job);
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }

        }
        #endregion
    }
}
