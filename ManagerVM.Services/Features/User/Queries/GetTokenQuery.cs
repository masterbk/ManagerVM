using ManagerVM.Contacts.Dtos;
using ManagerVM.Data.Helper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.User.Queries
{
    public class GetTokenQuery: IRequest<TokenDto>
    {
        public UserDto User { get; set; }
    }

    public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, TokenDto>
    {
        private readonly DateTimeProvider _dateTimeProvider;
        public GetTokenQueryHandler(DateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task<TokenDto> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, request.User.UserName),
                new Claim(ClaimTypes.NameIdentifier, request.User.Id.ToString()),
                new Claim(AuthorizationConstants.CLAIM_TENANT_ID, request.User.TenantId.ToString())
            };

            if (request.User.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            var now = _dateTimeProvider.Now;
            var timeExpire = TimeSpan.FromDays(100);
            var expires = now.Add(timeExpire);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenDto(tokenHandler.WriteToken(token), (long)timeExpire.TotalSeconds) ;
        }
    }
}
