using AutoMapper;
using ManageEmployees.Application.Contracts.Persistence;
using ManageEmployees.Application.Exceptions;
using MediatR;

namespace ManageEmployees.Application.Features.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        public CreateEmployeeCommandHandler(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }
        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEmployeeCommandValidator(_employeeRepository);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                throw new BadRequestException("Invalid attempt to add Employee", validationResult);
            var employeeToCreate = _mapper.Map<Domain.Employee>(request);
            await _employeeRepository.CreateAsync(employeeToCreate);
            return employeeToCreate.Id;
        }
    }
}
