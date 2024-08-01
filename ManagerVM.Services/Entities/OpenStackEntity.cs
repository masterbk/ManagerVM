using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Data.Entities
{
    public class OpenStackEntity: BaseEntity
    {
        public string EndPointUrl { get; set; }
        public string SecretKey { get; set; }
    }
}
