using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Request
{
    public class CreateCourseRequestModel
    {
        public string fullname { get; set; }
        public string idnumber { get; set; }
        public long? startdate { get; set; }
        public long? enddate { get; set; }
        public string shortname { get; set; }
        public long? categoryid { get; set; }
        public string description { get; set; }
        public long? img { get; set; }
        public long[] references { get; set; }

    }
}
