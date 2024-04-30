using AutoMapper;
using AutoMapper.QueryableExtensions;
using Employee.Application.Common;
using Employee.Application.Features.Queries;
using Employee.Application.Interfaces;
using MediatR;


namespace Employee.Application.Features.Commands._2.DeleteEmpl
{
    public record EmployeeDeletedCommand : IRequest<string>, IMapFrom<Domain.b_Entities.Employee>
    {
        public int Id { get; set; }

        public EmployeeDeletedCommand()
        {
                
        }

        public EmployeeDeletedCommand(int id)
        {
            this.Id = id;
        }
    }
    internal class EmployeeDeletedCommandHandler : IRequestHandler<EmployeeDeletedCommand, string>
    {
        #region Object Reference
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public EmployeeDeletedCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Implementation Methods
        public async Task<string> Handle(EmployeeDeletedCommand request, CancellationToken cancellationToken)
        {
            var empl = await _unitOfWork.Repository<Domain.b_Entities.Employee>().GetByIdAsync(request.Id);

            if (empl != null)
            {
                await _unitOfWork.Repository<Domain.b_Entities.Employee>().DeleteAsync(empl);
                empl.AddDomainEvent(new EmployeeDeletedEvent(empl));

                await _unitOfWork.Save(cancellationToken);

                return string.Format("Employee Deleted succesfull !!");
            }
            else
            {
                return string.Format("Employee Id {0} does not exist", request.Id);
            }
        }
        #endregion
    }
}
