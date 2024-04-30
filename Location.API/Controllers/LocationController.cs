using Location.Application.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Location.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        //1-reference repository
        private readonly ILocationRepository _locationRepository;

        //2- Constructeur
        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        #region Methods Verbs
        [HttpGet]
        public ActionResult<Domain.Entities.Location> GetLocationDetails()
        {
            var data = _locationRepository.GetAll();

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Domain.Entities.Location>> GetData(int Id)
        {
            return await _locationRepository.GetByIdAsync(Id);

        }
        [HttpPost]
        public async Task<ActionResult<int>>AddLocation(Domain.Entities.Location location)
        {
            if (location == null)
            {
                return NotFound();
            }
            return await _locationRepository.Add(location);
        }
        [HttpPut]
        public async Task<ActionResult<int>>EditLocation(Domain.Entities.Location location)
        {
            try
            {
                if (location.locationID == 0)
                    return 0;
                else
                    return await _locationRepository.UpdateAsync(location);
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }

        }

        //[HttpDelete("{id}")]
        //public  async Task<ActionResult> Delete(int id)
        //{
        //   // //var entity =  _locationRepository.GetByIdAsync(id);
        //   // //if (entity != null)
        //   //return _locationRepository.DeleteAsync(id);
        //}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _locationRepository.DeleteAsync(id);
            return Ok();
        }
        #endregion
    }
}
