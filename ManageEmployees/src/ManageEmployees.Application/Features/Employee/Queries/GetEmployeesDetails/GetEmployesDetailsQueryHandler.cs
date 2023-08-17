using AutoMapper;
using ManageEmployees.Application.Contracts.Logging;
using ManageEmployees.Application.Contracts.Persistence;
using ManageEmployees.Application.Exceptions;
using MediatR;

namespace ManageEmployees.Application.Features.Employee.Queries.GetEmployeesDetails
{
    public class GetEmployesDetailsQueryHandler : IRequestHandler<GetEmployeeDetailsQuery, EmployeeDetailsDTO>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAppLogger<GetEmployesDetailsQueryHandler> _logger;

        public GetEmployesDetailsQueryHandler(IMapper mapper, IEmployeeRepository employeeRepository,
            IAppLogger<GetEmployesDetailsQueryHandler> logger)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        public async Task<EmployeeDetailsDTO> Handle(GetEmployeeDetailsQuery request, CancellationToken cancellationToken)
        {
            var employeeDetails = await _employeeRepository.GetByIdAsync(request.Id);
            if (employeeDetails == null)
                throw new NotFoundException(nameof(employeeDetails), request.Id);
            var data = _mapper.Map<EmployeeDetailsDTO>(employeeDetails);
            _logger.LogInformation("Details about employee with id {0} were retrieved successfully", request.Id);
            return data;
        }
    }
}
