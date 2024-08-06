using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Response
{
    public class GetQuestionsOfQuizResponseDataModel
    {
        public int curentversion { get; set; }
        public int id { get; set; }
        public float maxmark { get; set; }
        public string name { get; set; }
        public int page { get; set; }
        public GetQuestionCategoriesResponseDataModel questionCategory { get; set; }
        public string questiontext { get; set; }
        public int? slot { get; set; }
        public int? slotid { get; set; }
        public string versions { get; set; }
        public string questionType { get; set; }
    }


}
