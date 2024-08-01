using ManagerVM.Contacts.Models.LMSModels.Request;
using ManagerVM.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Response
{
    /// <summary>
    /// wsfunction: local_cms_api_course_module_view_scorm
    /// </summary>
    public class GetModuleScormResponseDataModel
    {
        public long? attemptsgrading { get; set; }
        public long? completiontracking { get; set; }
        /// <summary>
        /// Id khóa học
        /// </summary>
        public long? courseid { get; set; }
        public string description { get; set; }
        public long? expectcompletedon { get; set; }
        /// <summary>
        /// Tên file Scorm
        /// </summary>
        public string file { get; set; }
        /// <summary>
        /// Link file Scorm
        /// </summary>
        public string filelink { get; set; }
        public long? forcenewattempt { get; set; }
        public long? grademethod { get; set; }
        public long? id { get; set; }
        public long? lockafterfinalattempt { get; set; }
        public long? maxattempt { get; set; }
        public long? maxgrade { get; set; }
        public string name { get; set; }
        /// <summary>
        /// Link ảnh đại diện
        /// </summary>
        public string picture { get; set; }
        public long? requiregrade { get; set; }
        public int? requireminscore { get; set; }
        public int MyProperty { get; set; }
        public int requireview { get; set; }
        public string restrictaccess { get; set; }
        public Restrictaccess restrictaccessObj
        {
            get
            {
                return string.IsNullOrWhiteSpace(restrictaccess) ? null : restrictaccess.ParseTo<Restrictaccess>();
            }
        }
        public string timeactivity { get; set; }
        public long? timeclose { get; set; }
        public long? timeopen { get; set; }
    }
}
