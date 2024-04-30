using Employee.Application.Common;


namespace Employee.Application.Features.Queries
{
    public class GetAllEmployeesDto : IMapFrom<Domain.b_Entities.Employee>
    {
        public int Id { get; init; }
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

}
