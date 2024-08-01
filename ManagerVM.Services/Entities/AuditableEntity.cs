using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Data.Entities
{
    public class AuditableEntity: BaseEntity
    {
        public long TenantId { get; set; }
    }
}
