using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Response
{
    public class GetClassReponseDataModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public long CategoryId { get; set; }
        public int StudentLimit { get; set; }
        public int StatusId { get; set; }
        public string Description { get; set; }
    }
}
