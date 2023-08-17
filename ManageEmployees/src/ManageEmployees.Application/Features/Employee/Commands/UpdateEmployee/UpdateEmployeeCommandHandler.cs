using AutoMapper;
using ManageEmployees.Application.Contracts.Logging;
using ManageEmployees.Application.Contracts.Persistence;
using ManageEmployees.Application.Exceptions;
using MediatR;

namespace ManageEmployees.Application.Features.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAppLogger<UpdateEmployeeCommandHandler> _logger;
        public UpdateEmployeeCommandHandler(IMapper mapper, IEmployeeRepository employeeRepository,
            IAppLogger<UpdateEmployeeCommandHandler> logger)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateEmployeeCommandValidator(_employeeRepository);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation errors in update request for {0} - {1}",
                    nameof(Employee), request.Id);
                throw new BadRequestException("Invalid Employee", validationResult);
            }
            var employeeToUpdate = _mapper.Map<Domain.Employee>(request);
            await _employeeRepository.UpdateAsync(employeeToUpdate);
            return Unit.Value;
        }
    }
}
