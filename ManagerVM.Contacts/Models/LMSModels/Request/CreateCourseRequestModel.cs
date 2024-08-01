using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Request
{
    public class CreateCourseRequestModel
    {
        public string fullName { get; set; }
        public string idnumber { get; set; }
        public long? startDate { get; set; }
        public long? endDate { get; set; }
        public string shortName { get; set; }
        public long? categoryId { get; set; }
        public string description { get; set; }
        public long? img { get; set; }
        public long[] references { get; set; }

    }
}
