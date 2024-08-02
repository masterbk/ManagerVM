using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Response
{
    public class GetModuleQuizResponseDataModel
    {
        public long? Attemptsallowed { get; set; }
        public long? Completiontracking { get; set; }
        public long? Courseid { get; set; }
        public string Description { get; set; }
        public long? Expectcompletedon { get; set; }
        public long? Graceperiod { get; set; }
        public dynamic Gradecategory { get; set; }
        public long? Gradetopass { get; set; }
        public long? Gradingmethod { get; set; }
        public long? Id { get; set; }
        public long? Maxgrade { get; set; }
        public long? Minattempt { get; set; }
        public string Name { get; set; }
        public string Overduehandling { get; set; }
        /// <summary>
        /// Link ảnh
        /// </summary>
        public string Picture { get; set; }
        public long? Requireattempt { get; set; }
        /// <summary>
        /// String of array
        /// </summary>
        public string Requiregrade { get; set; }
        public long? Requireview { get; set; }
        public string Restrictaccess { get; set; }
        public string Timeactivity { get; set; }
        public long? Timeclose { get; set; }
        public long? Timelimit { get; set; }
        public long? Timeopen { get; set; }
    }
}
