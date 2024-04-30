using Employee.Domain.a_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.b_Entities
{
    public class Employee : BaseAuditableEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public double Salary { get; set; }
        public int ComissionPCT { get; set; }
        public int ManagerId { get; set; }
        public string JobId { get; set; }
        public string Departement { get; set; }
    }
}
