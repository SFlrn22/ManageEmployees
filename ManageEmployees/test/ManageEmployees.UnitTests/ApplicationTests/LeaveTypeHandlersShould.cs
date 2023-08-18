using AutoMapper;
using ManageEmployees.Application.Contracts;
using ManageEmployees.Application.Contracts.Logging;
using ManageEmployees.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using ManageEmployees.Application.MappingProfiles;
using ManageEmployees.Domain;
using NSubstitute;
using Shouldly;

namespace ManageEmployees.UnitTests.ApplicationTests
{
    public class LeaveTypeHandlersShould
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;
        private readonly List<LeaveType> leaveTypes = new List<LeaveType>();
        public LeaveTypeHandlersShould()
        {
            leaveTypes = new List<LeaveType> {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Test1",
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 15,
                    Name = "Test2",
                },
                new LeaveType
                {
                    Id = 3,
                    DefaultDays = 20,
                    Name = "Test3",
                }
            };
            _leaveTypeRepository = Substitute.For<ILeaveTypeRepository>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _logger = Substitute.For<IAppLogger<GetLeaveTypesQueryHandler>>();
        }
        [Fact]
        public async Task GetLeaveType_ReturnsDTOList()
        {
            // ARRANGE
            _leaveTypeRepository.GetAsync().Returns(leaveTypes);
            _logger.LogInformation(Arg.Any<string>());
            var handler = new GetLeaveTypesQueryHandler(_mapper, _leaveTypeRepository, _logger);

            //ACT
            var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

            //ASSERT
            result.ShouldBeOfType<List<LeaveTypeDTO>>();
            result.Count.ShouldBe(3);
        }
    }
}
