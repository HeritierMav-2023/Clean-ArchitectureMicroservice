using Employee.Domain.a_Common;
using System;

namespace Employee.Application.Features.Commands._2.DeleteEmpl
{
    public class EmployeeDeletedEvent : BaseEvent
    {
        public Domain.b_Entities.Employee Employee { get; }
        public EmployeeDeletedEvent(Domain.b_Entities.Employee employee) 
        {
            Employee = employee;
        }
    }
}
