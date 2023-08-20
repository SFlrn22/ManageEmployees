using ManageEmployees.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using ManageEmployees.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using ManageEmployees.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using ManageEmployees.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using ManageEmployees.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using ManageEmployees.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using ManageEmployees.Application.Features.LeaveRequest.Queries.GetLeaveRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployees.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestDTO>>> Get(bool isLoggedInUser = false)
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestsQuery());
            return Ok(leaveRequests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDetailsDTO>> Get(int id)
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestDetailsQuery { Id = id });
            return Ok(leaveRequests);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Post(CreateLeaveRequestCommand leaveRequest)
        {
            var response = await _mediator.Send(leaveRequest);
            return CreatedAtAction(nameof(Get), new { Id = response });
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateLeaveRequestCommand leaveRequest)
        {
            await _mediator.Send(leaveRequest);
            return NoContent();
        }

        [HttpPut]
        [Route("CancelRequest")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequest)
        {
            await _mediator.Send(cancelLeaveRequest);
            return NoContent();
        }

        [HttpPut]
        [Route("UpdateApproval")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand updateApprovalRequest)
        {
            await _mediator.Send(updateApprovalRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveRequestCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
