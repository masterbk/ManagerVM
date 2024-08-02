using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Request
{
    public class CreateModuleQuizRequestModel
    {
        /// <summary>
        /// Id khóa học
        /// </summary>
        public long? courseid { get; set; }
        public long? section { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public long? timeopen { get; set; }
        public long? timeclose { get; set; }
        public long? timelimit { get; set; }
        public string overduehandling { get; set; }
        public long? gradetopass { get; set; }
        public long? attemptsallowed { get; set; }
        public long? gradingmethod { get; set; }
        public string restrictaccess { get; set; }
        public long? expectcompletedon { get; set; }
        /// <summary>
        /// Id file ảnh
        /// </summary>
        public long? picture { get; set; }
    }
}
