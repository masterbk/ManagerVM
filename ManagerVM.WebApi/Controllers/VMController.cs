using ManagerVM.Contacts.Dtos;
using ManagerVM.Contacts.Models;
using ManagerVM.Data.Helper;
using ManagerVM.Services;
using ManagerVM.Services.Features.VM.Commands;
using ManagerVM.Services.Features.VM.Notifications.DatabaseInstall;
using ManagerVM.Services.Features.VM.Queries;
using ManagerVM.Services.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ManagerVM.WebApi.Controllers
{
    public class VMController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILMSClient _client;
        public VMController(ISender sender, CurrentUserProvider currentUserProvider, IMediator mediator,
            ILMSClient client) : base(sender, currentUserProvider)
        {
            _mediator = mediator;
            _client = client;
        }

        /// <summary>
        /// Lấy dánh sách VM
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<List<VMDto>> GetListAsync([FromQuery]string keyword)
        {
            var res = await _sender.Send(new GetListVMQuery
            {
                Keyword = keyword,
                TenantId = _currentUserProvider.TenantId
            });

            return res;
        }

        /// <summary>
        /// Tạo mới VM
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<VMDto> CreateAsync([FromBody] CreateVMCommand command)
        {
            var res = await _sender.Send(command);

            return res;
        }

        /// <summary>
        /// Xóa VM
        /// </summary>
        /// <param name="vmId"></param>
        /// <returns></returns>
        [HttpDelete("{vmId}")]
        public async Task<bool> DeleteAsync([FromRoute] long vmId)
        {
            var command = new DeleteVMCommand
            {
                Id = vmId,
                TenantId = _currentUserProvider.IsAdmin ? 0 : _currentUserProvider.TenantId
            };

            var res = await _sender.Send(command);

            return res;
        }

        /// <summary>
        /// Kiểm tra dịch vụ trên VM
        /// </summary>
        /// <param name="vmId"></param>
        /// <param name="serviceModel"></param>
        /// <returns></returns>
        [HttpGet("svc/{vmId}")]
        public async Task<List<dynamic>> CheckServiceStatusAsync([FromRoute]long vmId,
            [FromQuery] ServiceModel serviceModel)
        {
            return default;
        }

        /// <summary>
        /// Cài đặt Docker
        /// </summary>
        /// <param name="vmId"></param>
        /// <returns></returns>
        [HttpPost("{vmId}/InstallDocker")]
        public async Task<bool> InstallDockerAsync([FromRoute]long vmId)
        {
            return await _sender.Send(new InstallServiceCommand { VmId = vmId, TenantId = _currentUserProvider.TenantId, Script = ServiceConstants.INSTALL_DOCKER });
        }

        /// <summary>
        /// Cài đặt LMS
        /// </summary>
        /// <param name="vmId"></param>
        /// <returns></returns>
        [HttpPost("{vmId}/InstallLMS")]
        public async Task<bool> InstallLMSAsync([FromRoute] long vmId)
        {
            return await _sender.Send(new InstallServiceCommand { VmId = vmId, TenantId = _currentUserProvider.TenantId, Script = ServiceConstants.INSTALL_LMS });
        }

        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="vmId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost("{vmId}/AddUser")]
        public async Task<bool> InstallLMSAsync([FromRoute] long vmId, [FromQuery]string userName)
        {
            return await _sender.Send(new InstallServiceCommand { VmId = vmId, TenantId = _currentUserProvider.TenantId, Script = string.Format(ServiceConstants.ADD_USER, userName) });
        }

        [AllowAnonymous]
        [HttpGet("Test/{vmInstanceId}")]
        public async Task<bool> Test([FromRoute]string vmInstanceId)
        {
            await _mediator.Publish(new InitializationSuccessfulDatabaseNotification
            {
                VMInstanceId = vmInstanceId
            });

            return true;
        }
    }
}
