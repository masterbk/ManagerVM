using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Response
{
    public class GetQuestionDetailResponseDataModel
    {
        public Question question { get; set; }
        public string type { get; set; }
    }

    public class Question
    {
        public Answer[] answers { get; set; }
        public int defaultmark { get; set; }
        public long? id { get; set; }
        public string idNumber { get; set; }
        public string name { get; set; }
        public QuestionCate questionCategory { get; set; }
        public Showstandardinstruction showstandardinstruction { get; set; }
        public int shuffleanswers { get; set; }
        public string text { get; set; }
    }

    public class Showstandardinstruction
    {
        public int key { get; set; }
        public string value { get; set; }
    }

    public class QuestionCate
    {
        public long? id { get; set; }
        public string name { get; set; }
    }

    public class Answer
    {
        public string answer { get; set; }
        public bool correct { get; set; }
    }
}
