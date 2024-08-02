using ManagerVM.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Request
{
    public class CreateClassRequestModel
    {
        public int statusId { get; set; }
        public int studentLimit { get; set; }
        public long categoryId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string userIds { get; set; }
        public string courseIds { get; set; }
    }
}
