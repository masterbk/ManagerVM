using ManagerVM.Contacts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.OpenStack.Quries
{
    public class GetOpenStackByTenantIdQuery: IRequest<OpenStackDto>
    {
        public long TenantId { get; set; }
    }
}
