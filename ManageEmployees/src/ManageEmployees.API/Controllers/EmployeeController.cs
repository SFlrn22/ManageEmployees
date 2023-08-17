using ManageEmployees.Application.Features.Employee.Commands.CreateEmployee;
using ManageEmployees.Application.Features.Employee.Commands.DeleteEmployee;
using ManageEmployees.Application.Features.Employee.Commands.UpdateEmployee;
using ManageEmployees.Application.Features.Employee.Queries.GetAllEmployees;
using ManageEmployees.Application.Features.Employee.Queries.GetEmployeesDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployees.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<EmployeeDTO>> Get()
        {
            var employees = await _mediator.Send(new GetEmployeesQuery());
            return employees;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> Get(int id)
        {
            var employees = await _mediator.Send(new GetEmployeeDetailsQuery(id));
            return Ok(employees);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post(CreateEmployeeCommand employee)
        {
            var response = await _mediator.Send(employee);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateEmployeeCommand employee)
        {
            await _mediator.Send(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmployeeCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
