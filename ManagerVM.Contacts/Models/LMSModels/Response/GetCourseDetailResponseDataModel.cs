using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Response
{
    public class GetCourseDetailResponseDataModel
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Shortname { get; set; }
        public string Idnumber { get; set; }
        public long? Startdate { get; set; }
        public long? Enddate { get; set; }
        public string Description { get; set; }
        public long? Timemodified { get; set; }
        public long? Categoryid { get; set; }
        public bool ChangeCategory { get; set; }
        /// <summary>
        /// Link ảnh đại diện
        /// </summary>
        public string Img { get; set; }
        public List<ReferenceObject> References { get; set; }
        public ModuleModel[] Modules { get; set; }
    }

    public class ReferenceObject
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public string Url { get; set; }
        public string Externalurl { get; set; }
    }

    public class ModuleModel
    {
        public long id { get; set; }
        public string intro { get; set; }
        public string modname { get; set; }
        public string name { get; set; }
        /// <summary>
        /// Link ảnh đại diện
        /// </summary>
        public string picture { get; set; }
        public int requiregrade { get; set; }
        public int visible { get; set; }
    }
}
