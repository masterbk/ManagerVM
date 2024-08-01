using ManagerVM.Contacts.Dtos;
using ManagerVM.Contacts.Models.OpenStackResponses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ManagerVM.Services.Helper
{
    public interface IOpenStackClient
    {
        public Task<T> CreateVMAsync<T>(string name, string openStackEndPoint, CancellationToken cancellationToken);
        public Task<T> GetListVMAsync<T>(string openStackEndPoint, string name = "");
        public Task<bool> DeleteVMAsync(string openStackEndPoint, string vmId);
        public VMDto CheckServiceStatusAsync(string serviceEndPoint, int port, string openStackEndPoint);
        public Task<bool> InstallServiceAsync(string openStackEndPoint, string vmId, string script);
    }

    public class OpenStackClient : IOpenStackClient
    {
        private readonly HttpClient _httpClient;
        private readonly IAppLogger<OpenStackClient> _appLogger;
        public OpenStackClient(HttpClient HttpClient, IAppLogger<OpenStackClient> appLogger)
        {
            _httpClient = HttpClient;
            _httpClient.Timeout = TimeSpan.FromMinutes(30);
            _appLogger = appLogger;
        }

        public VMDto CheckServiceStatusAsync(string serviceEndPoint, int port, string openStackEndPoint)
        {
            throw new NotImplementedException();
        }

        public async Task<T> CreateVMAsync<T>(string name, string openStackEndPoint, CancellationToken cancellationToken)
        {
            var res = await _httpClient.PostAsync($"{openStackEndPoint}/compute?name={name}", new StringContent("{}", Encoding.UTF8, "application/json"));
            res.EnsureSuccessStatusCode();

            string text = await res.Content.ReadAsStringAsync(cancellationToken);
            return text.ParseTo<T>();
        }

        public async Task<bool> DeleteVMAsync(string openStackEndPoint, string vmId)
        {
            var res = await _httpClient.DeleteAsync($"{openStackEndPoint}/compute/{vmId}");
            res.EnsureSuccessStatusCode();

            return res.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<T> GetListVMAsync<T>(string openStackEndPoint, string name = "")
        {
            var res = await _httpClient.GetAsync($"{openStackEndPoint}/compute?name={name}");
            res.EnsureSuccessStatusCode();

            string text = await res.Content.ReadAsStringAsync();
            return text.ParseTo<T>();
        }

        public async Task<bool> InstallServiceAsync(string openStackEndPoint, string vmId, string script)
        {
            try
            {
                var scriptObj = new
                {
                    script = script
                };

                var res = await _httpClient.PostAsync($"{openStackEndPoint}/compute/svc/{vmId}", new StringContent(scriptObj.ToJson(), Encoding.UTF8, "application/json"));
                res.EnsureSuccessStatusCode();

                return res.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _appLogger.LogError($"[InstallServiceAsync]url = {openStackEndPoint}/compute/svc/{vmId}, script = {script}");
                throw ex;
            }
        }
    }
}
