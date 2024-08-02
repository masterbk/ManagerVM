using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Request
{
    /// <summary>
    /// wsfunction: local_cms_api_course_module_add_questions_to_quiz
    /// </summary>
    public class AddQuestionsToQuizModel
    {
        public long? moduleid { get; set; }
        /// <summary>
        /// String mảng id Question
        /// </summary>
        public string questionids { get; set; }
    }
}
