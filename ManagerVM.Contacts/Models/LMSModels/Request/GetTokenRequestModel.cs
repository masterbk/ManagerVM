using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Request
{
    public class GetTokenRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
