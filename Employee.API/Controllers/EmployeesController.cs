using Employee.Application.Features.Commands._1.CreateEmpl;
using Employee.Application.Features.Commands._2.DeleteEmpl;
using Employee.Application.Features.Commands._3.UpdateEmpl;
using Employee.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //1-
        private readonly IMediator _mediator;

        //2- Constructor
        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
        }
        #region Methods Verbs

        [HttpGet]
        public async Task<ActionResult<List<GetAllEmployeesDto>>> GetAll()
        {
            return await _mediator.Send(new GetAllEmployeesQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeByIdDto>> GetById(int id)
        {
            return await _mediator.Send(new GetEmployeeByIdQuery(id));
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(CreateEmployeeCommand employeeCommand)
        {
            return await _mediator.Send(employeeCommand);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>>Update(int id, UpdateEmployeeCommand employeeCommand)
        {
            if(id != employeeCommand.Id)
            {
                return BadRequest();
            }
            return await _mediator.Send(employeeCommand);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>>Delete(int id)
        {
            return await _mediator.Send(new  EmployeeDeletedCommand(id));
        }
        #endregion
    }
}
