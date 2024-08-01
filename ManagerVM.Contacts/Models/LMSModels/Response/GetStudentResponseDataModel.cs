using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Response
{
    public class GetStudentResponseDataModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public int SuspendedStatus { get; set; }
    }
}
