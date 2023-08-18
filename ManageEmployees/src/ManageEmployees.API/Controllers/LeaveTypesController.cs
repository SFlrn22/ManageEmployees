using ManageEmployees.Application.Features.LeaveType.Commands.CreateLeaveType;
using ManageEmployees.Application.Features.LeaveType.Commands.DeleteLeaveType;
using ManageEmployees.Application.Features.LeaveType.Commands.UpdateLeaveType;
using ManageEmployees.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using ManageEmployees.Application.Features.LeaveType.Queries.GetLeaveTypesDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployees.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDTO>>> Get()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());
            return Ok(leaveTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDTO>> Get(int id)
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypeDetailsQuery(id));
            return Ok(leaveTypes);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post(CreateLeaveTypeCommand leaveType)
        {
            var response = await _mediator.Send(leaveType);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateLeaveTypeCommand leaveType)
        {
            await _mediator.Send(leaveType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveTypeCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
