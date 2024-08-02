using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Response
{
    public class GetQuestionCategoriesResponseDataModel
    {
        public dynamic courseCategoryid { get; set; }
        public string courseCategoryname { get; set; }
        public long? id { get; set; }
        public string idnumber { get; set; }
        public string info { get; set; }
        public bool isAllowDelete { get; set; }
        public string name { get; set; }
        public int numberQuestions { get; set; }
        public string parentName { get; set; }
    }
}
