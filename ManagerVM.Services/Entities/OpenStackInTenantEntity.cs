using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Data.Entities
{
    public class OpenStackInTenantEntity
    {
        [Key]
        public long Id { get; set; }
        public long OpenStackId { get; set; }
        [ForeignKey("OpenStackId")]
        public virtual OpenStackEntity OpenStackEntity { get; set; }
        public long TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantEntity { get; set; }
    }
}
