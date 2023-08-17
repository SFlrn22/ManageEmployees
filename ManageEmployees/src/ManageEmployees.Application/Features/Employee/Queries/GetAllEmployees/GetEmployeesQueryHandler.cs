using AutoMapper;
using ManageEmployees.Application.Contracts.Logging;
using ManageEmployees.Application.Contracts.Persistence;
using MediatR;

namespace ManageEmployees.Application.Features.Employee.Queries.GetAllEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAppLogger<GetEmployeesQueryHandler> _logger;
        public GetEmployeesQueryHandler(IMapper mapper, IEmployeeRepository employeeRepository,
            IAppLogger<GetEmployeesQueryHandler> logger)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        public async Task<List<EmployeeDTO>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAsync();
            var data = _mapper.Map<List<EmployeeDTO>>(employees);
            _logger.LogInformation("Employees were retrieved successfully");
            return data;
        }
    }
}
