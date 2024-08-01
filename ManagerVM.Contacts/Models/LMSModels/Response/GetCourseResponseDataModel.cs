using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Response
{
    public class GetCourseResponseDataModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Categoryname { get; set; }
        public string Shortname { get; set; }
        public string Idnumber { get; set; }
        public long? Startdate { get; set; }
        public long? Enddate { get; set; }
        public string Description { get; set; }
        public long? Timemodified { get; set; }
        public bool ExistInClass { get; set; }
    }
}
