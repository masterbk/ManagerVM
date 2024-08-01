using ManagerVM.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Data.Entities
{
    public class UserEntity: BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string FullName { get; set; }
        public long TenantId { get; set; }
        public bool IsAdmin { get; set; }
        public bool CheckPasswordValid(string password)
        {
            var hashPw = $"{Salt}{password}".ToSHA256Hash();
            return hashPw == Password;
        }
    }
}
