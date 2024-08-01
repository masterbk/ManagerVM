using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Response
{
    public class GetStudentDetailResponseDataModel
    {
        public string Avatar { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public long? DateOfBirth { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public long Id { get; set; }
        public string Job { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int SuspendedStatus { get; set; }
        public string UserName { get; set; }
        public dynamic UserPartner { get; set; }
        public string UserType { get; set; }
    }
}
