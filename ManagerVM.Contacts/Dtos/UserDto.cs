using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Dtos
{
    public class UserDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public long TenantId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
