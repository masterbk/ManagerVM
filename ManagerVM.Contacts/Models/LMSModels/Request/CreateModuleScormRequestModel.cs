using ManagerVM.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Request
{
    /// <summary>
    /// wsfunction: local_cms_api_course_module_create_scorm
    /// </summary>
    public class CreateModuleScormRequestModel
    {
        public long? grademethod { get; set; }
        public long? maxgrade { get; set; }
        public long? maxattempt { get; set; }
        public long? attemptsgrading { get; set; }
        public long? lockafterfinalattempt { get; set; }
        public long? forcenewattempt { get; set; }
        public long? completiontracking { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public long? timeopen { get; set; }
        public long? timeclose { get; set; }
        /// <summary>
        /// Id file Scorm
        /// </summary>
        public long? file { get; set; }
        /// <summary>
        /// Id file ảnh đại diện
        /// </summary>
        public long? picture { get; set; }
        public string timeactivity { get; set; }
        public long? expectcompletedon { get; set; }
        /// <summary>
        /// Id khóa học
        /// </summary>
        public long? courseid { get; set; }
        public long? section { get; set; }
        public long? requiregrade { get; set; }
        public long? requireview { get; set; }
        public Restrictaccess restrictaccessObj { get; set; }
        public string restrictaccess { get
            {
                return restrictaccessObj?.ToJson();
            }
        }
    }

    public class Restrictaccess
    {
        public long? typeofrestrict { get; set; }
        public long? match { get; set; }
        public RestrictItem[] items { get; set; }
    }

    public class RestrictItem
    {
        public long? restrict { get; set; }
        public long? itemid { get; set; }
        public string itemtype { get; set; }
    }
}
