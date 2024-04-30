using Employee.Application.Interfaces;


namespace Employee.Persistance.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region Object Repositories

        private readonly IGenericRepository<Domain.b_Entities.Employee> _repository;
        #endregion

        #region Constructor
        public EmployeeRepository(IGenericRepository<Domain.b_Entities.Employee> repository)
        {
            _repository = repository;
        }
        #endregion
    }
}
