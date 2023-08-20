using ManageEmployees.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using ManageEmployees.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using ManageEmployees.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using ManageEmployees.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using ManageEmployees.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployees.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeaveAllocationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDTO>>> Get(bool isLoggedInUser = false)
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListQuerry());
            return Ok(leaveAllocations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDetailsDTO>> Get(int id)
        {
            var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailsQuerry { Id = id });
            return Ok(leaveAllocation);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Post(CreateLeaveAllocationCommand leaveAllocation)
        {
            var response = await _mediator.Send(leaveAllocation);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateLeaveAllocationCommand leaveAllocation)
        {
            await _mediator.Send(leaveAllocation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveAllocationCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
