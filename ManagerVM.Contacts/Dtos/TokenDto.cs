using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Dtos
{
    public class TokenDto
    {
        public TokenDto(string accessToken, long expiresIn)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
        }

        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
        public string TokenType { get; set; } = "Bearer";
    }
}
