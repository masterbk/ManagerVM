using ManagerVM.Contacts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Dtos
{
    public class VMDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public VMStatus Status { get; set; }
    }
}
