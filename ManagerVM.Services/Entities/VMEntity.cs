using ManagerVM.Contacts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Data.Entities
{
    public class VMEntity: AuditableEntity
    {
        public string Name { get; set; }
        public long RoomId { get; set; }
        public string InstanceId { get; set; }
        public string Address { get; set; }
        public long OpenStackId { get; set; }
        [ForeignKey("OpenStackId")]
        public virtual OpenStackEntity OpenStackEntity { get; set; }
        [ForeignKey("TenantId")]
        public virtual TenantEntity TenantEntity { get; set; }
        public VMStatus Status { get; set; }
        public bool InstallAllServiceSuccess { get; set; }
        public string HostName { get ; set; }
        public HostStatus HostStatus { get; set; }
    }
}
