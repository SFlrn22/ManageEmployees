using AutoMapper;
using Blazored.LocalStorage;
using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Models.LeaveTypes;
using ManageEmployees.BlazorUI.Services.Base;

namespace ManageEmployees.BlazorUI.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly IMapper _mapper;

        public LeaveTypeService(IClient client, IMapper mapper, ILocalStorageService localStorage) :
            base(client, localStorage)
        {
            _mapper = mapper;
        }

        public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
        {
            try
            {
                var createLeaveTypeCommand = _mapper.Map<CreateLeaveTypeCommand>(leaveType);
                await _client.LeaveTypesPOSTAsync(createLeaveTypeCommand);
                return new Response<Guid>()
                {
                    Success = true
                };

            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<Response<Guid>> DeleteLeaveType(int id)
        {
            try
            {
                await _client.LeaveTypesDELETEAsync(id);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
        {
            var leaveType = await _client.LeaveTypesGETAsync(id);
            var data = _mapper.Map<LeaveTypeVM>(leaveType);
            return data;
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {
            var leaveTypes = await _client.LeaveTypesAllAsync();
            var data = _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
            return data;
        }

        public async Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
        {
            try
            {
                var data = _mapper.Map<UpdateLeaveTypeCommand>(leaveType);
                await _client.LeaveTypesPUTAsync(id.ToString(), data);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }
    }
}
