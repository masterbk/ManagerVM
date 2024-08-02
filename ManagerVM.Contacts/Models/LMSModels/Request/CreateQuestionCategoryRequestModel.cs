using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Request
{
    /// <summary>
    /// wsfunction: local_cms_api_questionbank_categorycreate
    /// </summary>
    public class CreateQuestionCategoryRequestModel
    {
        public string name { get; set; }
        public string idnumber { get; set; }
        public string description { get; set; }
    }
}
