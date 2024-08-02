using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Request
{
    /// <summary>
    /// wsfunction: local_cms_api_questionbank_questioncreate
    /// </summary>
    public class CreateQuestionRequestModel
    {
        public int defaultmark { get; set; }
        public long? questionCategoryId { get; set; }
        public string name { get; set; }
        public string idNumber { get; set; }
        public string text { get; set; }
        public int single { get; set; }
        public int showstandardinstruction { get; set; }
        public string answers { get; set; }
        public string type { get; set; }
    }
}
