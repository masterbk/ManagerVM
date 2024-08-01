using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Data.Entities
{
    public class TenantEntity: BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public List<OpenStackInTenantEntity> OpenStackInTenants { get; set; }
        public List<VMEntity> VMEntities { get; set; }
    }
}
