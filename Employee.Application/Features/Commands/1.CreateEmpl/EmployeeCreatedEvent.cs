using Employee.Domain.a_Common;


namespace Employee.Application.Features.Commands._1.CreateEmpl
{
    public class EmployeeCreatedEvent : BaseEvent
    {
        public Domain.b_Entities.Employee Employee { get;}

        public EmployeeCreatedEvent(Domain.b_Entities.Employee employee)
        {
            Employee = employee;
        }
    }
}
