using AutoMapper;
using Employee.Application.Common;
using Employee.Application.Interfaces;
using MediatR;


namespace Employee.Application.Features.Commands._1.CreateEmpl
{
    public record CreateEmployeeCommand: IRequest<string>, IMapFrom<Domain.b_Entities.Employee>
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public DateTime HireDate { get; init; }
        public double Salary { get; init; }
        public int ComissionPCT { get; init; }
        public int ManagerId { get; init; }
        public string JobId { get; init; }
        public string Departement { get; init; }
    }
    internal class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
    {
        #region Object Reference
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            //creation employee
            var employee = new Domain.b_Entities.Employee()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                HireDate = request.HireDate,
                Salary = request.Salary,
                ComissionPCT = request.ComissionPCT,
                ManagerId = request.ManagerId,
                JobId = request.JobId,
                Departement = request.Departement,
            };

            await _unitOfWork.Repository<Domain.b_Entities.Employee>().AddAsync(employee);
            employee.AddDomainEvent(new EmployeeCreatedEvent(employee));

            await _unitOfWork.Save(cancellationToken);

            //return 1;
            return string.Format( "Employee Created successfull !!");
        }
    }
}
