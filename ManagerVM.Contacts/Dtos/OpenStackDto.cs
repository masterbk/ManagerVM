using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Dtos
{
    public class OpenStackDto
    {
        public long Id { get; set; }
        public string EndPointUrl { get; set; }
        public string SecretKey { get; set; }
    }
}
