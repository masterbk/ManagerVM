using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Request
{
    public class CreateCategoryRequestModel
    {
        public string name { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public long imgId { get; set; }
    }
}
