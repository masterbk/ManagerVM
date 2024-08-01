using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Request
{
    public class CreateClassRequestModel
    {
        public int status { get; set; }
        public int studentLimit { get; set; }
        public long categoryId { get; set; }
        public string code { get; set; }
        public string name { get; set; }

        public long[] userIds { get; set; }
        public long[] courseIds { get; set; }
    }
}
