using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Request
{
    public class CreateStudentRequestModel
    {
        public int suspendedStatus { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public long? dateOfBirth { get; set; }
        public string phone { get; set; }
        public string department { get; set; }
        public string job { get; set; }
        public int gender { get; set; }
        public string cityName { get; set; }
        public string countryCode { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
}
