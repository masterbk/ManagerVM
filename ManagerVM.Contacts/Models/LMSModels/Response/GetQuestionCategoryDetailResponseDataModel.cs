using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Response
{
    public class GetQuestionCategoryDetailResponseDataModel
    {
        public string description { get; set; }
        public long? id { get; set; }
        public string idnumber { get; set; }
        public bool isAllowUpdate { get; set; }
        public string name { get; set; }
        public int? numberQuestions { get; set; }
    }
}
