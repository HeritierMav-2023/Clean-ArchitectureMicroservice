using Employee.Domain.a_Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.a_Common
{
    public interface IAuditableEntity : IEntity
    {
        int? CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        int? UpdatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
