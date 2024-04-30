using Employee.Domain.a_Common;


namespace Employee.Application.Features.Commands._3.UpdateEmpl
{
    public class EmployeeUpdatedEvent : BaseEvent
    {
        public Domain.b_Entities.Employee Employee { get; }
        public EmployeeUpdatedEvent(Domain.b_Entities.Employee employee)
        {
            Employee = employee;    
        }
    }
}
