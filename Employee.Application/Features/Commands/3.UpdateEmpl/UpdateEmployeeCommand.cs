using AutoMapper;
using Employee.Application.Common;
using Employee.Application.Features.Commands._1.CreateEmpl;
using Employee.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Application.Features.Commands._3.UpdateEmpl
{
    public record UpdateEmployeeCommand : IRequest<string>, IMapFrom<Domain.b_Entities.Employee>
    {
        public int Id { get; set; }
        public string firstName { get; init; }
        public string lastName { get; init; }
        public string email { get; init; }
        public string phoneNumber { get; init; }
        public DateTime hireDate { get; init; }
        public double salary { get; init; }
        public int comissionPCT { get; init; }
        public int managerId { get; init; }
        public string jobId { get; init; }
        public string departement { get; init; }
    }

    internal class UpdateEmployeeCommandHanler : IRequestHandler<UpdateEmployeeCommand, string>
    {
        #region Object Reference
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public UpdateEmployeeCommandHanler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Ovveride Methods
        public async Task<string> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Repository<Domain.b_Entities.Employee>().GetByIdAsync(request.Id);
            if (employee == null)
            {
                return string.Format("Employee does not exist");
            }
            else
            {
                employee.FirstName = request.firstName;
                employee.LastName = request.lastName;
                employee.Email = request.email;
                employee.PhoneNumber = request.phoneNumber;
                employee.Salary = request.salary;
                employee.HireDate = request.hireDate;
                employee.ComissionPCT = request.comissionPCT;
                employee.ManagerId = request.managerId;
                employee.JobId = request.jobId;
                employee.Departement = request.departement;

                await _unitOfWork.Repository<Domain.b_Entities.Employee>().UpdateAsync(employee);
                employee.AddDomainEvent(new EmployeeUpdatedEvent(employee));

                await _unitOfWork.Save(cancellationToken);
                return string.Format("Employee Id {0} Updated successfull !!!", request.Id);
            }
        }
        #endregion
    }
}
