using ManagerVM.Contacts.Models.LMSModels.Request;
using ManagerVM.Contacts.Models.LMSModels.Response;
using ManagerVM.Services.Features.User.Queries;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Helper
{
    public interface ILMSClient
    {
        /// <summary>
        ///  Lấy token
        /// </summary>
        /// <returns></returns>
        public Task<string> GetTokenAsync(string hostName);

        /// <summary>
        ///  Lấy token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<GetTokenReponseModel> GetTokenAsync(string hostName, GetTokenRequestModel model);

        /// <summary>
        ///  Lấy thông tin lĩnh vực
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        public Task<ResponseModel<GetCategoryReponseDataModel>> GetCategoryOfClassAsync(string hostName, long cateId, string token = "");

        /// <summary>
        /// Lấy thông tin lớp học
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public Task<ResponseModel<GetClassReponseDataModel>> GetClassAsync(string hostName, long classId, string token = "");

        /// <summary>
        /// Lấy danh sách học viên trong lớp học
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public Task<ResponseModel<GetListReponseDataModel<GetStudentResponseDataModel>>> GetStudentsAsync(string hostName, long classId, string token = "");

        /// <summary>
        /// Lấy danh thông tin học viên
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public Task<ResponseModel<GetStudentDetailResponseDataModel>> GetStudentDetailAsync(string hostName, long studentId, string token = "");

        /// <summary>
        /// Lấy danh sách khóa học của lớp học
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public Task<ResponseModel<GetListReponseDataModel<GetCourseResponseDataModel>>> GetCourseOfClassAsync(string hostName, long classId, string token = "");

        /// <summary>
        /// Lấy chi tiết khóa học
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public Task<ResponseModel<GetCourseDetailResponseDataModel>> GetCourseDetailAsync(string hostName, long courseId, string token = "");


        /// <summary>
        /// Lấy thông tin chi tiết module Scorm
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public Task<ResponseModel<GetModuleScormResponseDataModel>> GetModuleScormlAsync(string hostName, long moduleid, string token = "");

        /// <summary>
        /// Upload media
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Task<ResponseModel<List<UploadMediaResponseDataModel>>> UploadMediaAsync(string hostName, byte[] bytes, string fileName, string token = "");

        /// <summary>
        /// Tạo mới lĩnh vực
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseModel<List<CreateCategoryResponseDataModel>>> CreateCategoryAsync(string hostName, CreateCategoryRequestModel model);

        /// <summary>
        /// Tạo mới khóa học
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseModel<CreateCourseResponseDataModel>> CreateCourseAsync(string hostName, CreateCourseRequestModel model);

        /// <summary>
        /// Tạo mới học viên
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseModel<T>> CreateStudentAsync<T>(string hostName, CreateStudentRequestModel model);

        /// <summary>
        /// Tạo mới lớp học
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseModel<T>> CreateClassAsync<T>(string hostName, CreateClassRequestModel model);

        /// <summary>
        /// Tạo mới module Scorm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseModel<CreateModuleScormResponseDataModel>> CreateModuleScormAsync(string hostName, CreateModuleScormRequestModel model);

        /// <summary>
        /// Tải về media
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<byte[]> DownloadMediaAsync(string url, string token = "");
    }

    public class LMSClient : ILMSClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public LMSClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel<List<CreateCategoryResponseDataModel>>> CreateCategoryAsync(string hostName, CreateCategoryRequestModel model)
        {
            var token = await GetTokenAsync(hostName);
            var form = model.ToFormData(new Dictionary<string, string>
            {
                { "moodlewsrestformat", "json" },
                { "wsfunction", "local_cms_api_category_create" },
                { "wstoken", token },
                { "service", "vtc_cms_api" }
            });

            var _httpClient = _httpClientFactory.CreateClient();

            _httpClient.BaseAddress = new Uri(hostName);

            var res = await _httpClient.PostAsync("/webservice/rest/server.php", form);
            res.EnsureSuccessStatusCode();

            var strContent = await res.Content.ReadAsStringAsync();

            return strContent?.ParseTo<ResponseModel<List<CreateCategoryResponseDataModel>>>();
        }

        public async Task<ResponseModel<T>> CreateClassAsync<T>(string hostName, CreateClassRequestModel model)
        {
            var token = await GetTokenAsync(hostName);
            var form = model.ToFormData(new Dictionary<string, string>
            {
                { "moodlewsrestformat", "json" },
                { "wsfunction", "local_cms_api_categoryclass_create" },
                { "wstoken", token },
                { "service", "vtc_cms_api" }
            });

            var _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(hostName);
            var res = await _httpClient.PostAsync("/webservice/rest/server.php", form);
            res.EnsureSuccessStatusCode();

            var strContent = await res.Content.ReadAsStringAsync();

            return strContent?.ParseTo<ResponseModel<T>>();
        }

        public async Task<ResponseModel<CreateCourseResponseDataModel>> CreateCourseAsync(string hostName, CreateCourseRequestModel model)
        {
            var token = await GetTokenAsync(hostName);
            var form = model.ToFormData(new Dictionary<string, string>
            {
                { "moodlewsrestformat", "json" },
                { "wsfunction", "local_cms_api_course_create" },
                { "wstoken", token },
                { "service", "vtc_cms_api" }
            });

            var _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(hostName);
            var res = await _httpClient.PostAsync("/webservice/rest/server.php", form);
            res.EnsureSuccessStatusCode();

            var strContent = await res.Content.ReadAsStringAsync();

            return strContent?.ParseTo<ResponseModel<CreateCourseResponseDataModel>>();
        }

        public async Task<ResponseModel<T>> CreateStudentAsync<T>(string hostName, CreateStudentRequestModel model)
        {
            var token = await GetTokenAsync(hostName);
            var form = model.ToFormData(new Dictionary<string, string>
            {
                { "moodlewsrestformat", "json" },
                { "wsfunction", "local_cms_api_user_create" },
                { "wstoken", token },
                { "service", "vtc_cms_api" }
            });

            var _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(hostName);
            var res = await _httpClient.PostAsync("/webservice/rest/server.php", form);
            res.EnsureSuccessStatusCode();

            var strContent = await res.Content.ReadAsStringAsync();

            return strContent?.ParseTo<ResponseModel<T>>();
        }

        public async Task<ResponseModel<GetCategoryReponseDataModel>> GetCategoryOfClassAsync(string hostName, long cateId, string token = "")
        {
            token = string.IsNullOrWhiteSpace(token) ? await GetTokenAsync(hostName) : token;

            var _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(hostName);
            var res = await _httpClient.GetAsync($"/webservice/rest/server.php?id={cateId}&moodlewsrestformat=json&service=vtc_cms_api&wsfunction=local_cms_api_category_info&wstoken={token}");
            res.EnsureSuccessStatusCode();

            var strcontent = await res.Content.ReadAsStringAsync();

            return strcontent?.ParseTo<ResponseModel<GetCategoryReponseDataModel>>();
        }

        public async Task<ResponseModel<GetClassReponseDataModel>> GetClassAsync(string hostName, long classId, string  token = "")
        {
            token = string.IsNullOrWhiteSpace(token) ? await GetTokenAsync(hostName) : token;

            var _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(hostName);
            var res = await _httpClient.GetAsync($"/webservice/rest/server.php?id={classId}&moodlewsrestformat=json&service=vtc_cms_api&wsfunction=local_cms_api_categoryclass_view&wstoken={token}");
            res.EnsureSuccessStatusCode();

            var strcontent = await res.Content.ReadAsStringAsync();

            return strcontent?.ParseTo<ResponseModel<GetClassReponseDataModel>>();
        }

        public async Task<ResponseModel<GetListReponseDataModel<GetCourseResponseDataModel>>> GetCourseOfClassAsync(string hostName, long classId, string token = "")
        {
            token = string.IsNullOrWhiteSpace(token) ? await GetTokenAsync(hostName) : token;

            var _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(hostName);
            var res = await _httpClient.GetAsync($"/webservice/rest/server.php?classId={classId}&methodType=inClass&moodlewsrestformat=json&pageIndex=1&pageSize=1000&service=vtc_cms_api&wsfunction=local_cms_api_categoryclass_courselist&wstoken={token}");
            res.EnsureSuccessStatusCode();

            var strcontent = await res.Content.ReadAsStringAsync();

            return strcontent?.ParseTo<ResponseModel<GetListReponseDataModel<GetCourseResponseDataModel>>>();
        }

        public async Task<ResponseModel<GetListReponseDataModel<GetStudentResponseDataModel>>> GetStudentsAsync(string hostName, long classId, string token = "")
        {
            token = string.IsNullOrWhiteSpace(token) ? await GetTokenAsync(hostName) : token;

            var _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(hostName);
            var res = await _httpClient.GetAsync($"/webservice/rest/server.php?classId={classId}&moodlewsrestformat=json&pageIndex=1&pageSize=10000&service=vtc_cms_api&wsfunction=local_cms_api_categoryclass_studentlist&wstoken={token}");
            res.EnsureSuccessStatusCode();

            var strcontent = await res.Content.ReadAsStringAsync();

            return strcontent?.ParseTo<ResponseModel<GetListReponseDataModel<GetStudentResponseDataModel>>>();
        }

        public async Task<GetTokenReponseModel> GetTokenAsync(string hostName, GetTokenRequestModel model)
        {
            var _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(hostName);
            var res = await _httpClient.GetAsync($"/login/token.php?username={model.UserName}&password={model.Password.UrlEndcode()}&service=vtc_cms_api");
            res.EnsureSuccessStatusCode();

            var strContent = await res.Content.ReadAsStringAsync();

            return strContent?.ParseTo<GetTokenReponseModel>();
        }

        public async Task<ResponseModel<List<UploadMediaResponseDataModel>>> UploadMediaAsync(string hostName, byte[] bytes, string fileName, string token = "")
        {
            token = string.IsNullOrWhiteSpace(token) ? await GetTokenAsync(hostName) : token;
            var _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(hostName);

            MultipartFormDataContent form = new MultipartFormDataContent();

            ByteArrayContent byteContent = new ByteArrayContent(bytes);
            byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            form.Add(byteContent,"file", fileName);

            var res = await _httpClient.PostAsync($"/webservice/uploadvtc.php?token={token}", form);
            res.EnsureSuccessStatusCode();

            var content = await res.Content.ReadAsStringAsync();

            var obj = content.ParseTo<ResponseModel<dynamic>>();

            return content?.ParseTo<ResponseModel<List<UploadMediaResponseDataModel>>>();
        }

        public async Task<string> GetTokenAsync(string hostName)
        {
            var res = await GetTokenAsync(hostName, new GetTokenRequestModel
            {
                UserName = "admin",
                Password = "Vtc@123#"
            });

            return res?.Token??"";
        }

        public async Task<ResponseModel<GetCourseDetailResponseDataModel>> GetCourseDetailAsync(string hostName, long courseId, string token = "")
        {
            token = string.IsNullOrWhiteSpace(token) ? await GetTokenAsync(hostName) : token;

            var _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(hostName);
            var res = await _httpClient.GetAsync($"/webservice/rest/server.php?id={courseId}&moodlewsrestformat=json&service=vtc_cms_api&wsfunction=local_cms_api_course_info&wstoken={token}");
            res.EnsureSuccessStatusCode();

            var strContent = await res.Content.ReadAsStringAsync();

            return strContent?.ParseTo<ResponseModel<GetCourseDetailResponseDataModel>>();
        }

        public async Task<ResponseModel<GetStudentDetailResponseDataModel>> GetStudentDetailAsync(string hostName, long studentId, string token = "")
        {
            token = string.IsNullOrWhiteSpace(token) ? await GetTokenAsync(hostName) : token;

            var _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(hostName);
            var res = await _httpClient.GetAsync($"webservice/rest/server.php?id={studentId}&moodlewsrestformat=json&service=vtc_cms_api&wsfunction=local_cms_api_user_view&wstoken={token}");
            res.EnsureSuccessStatusCode();

            var strContent = await res.Content.ReadAsStringAsync();

            return strContent?.ParseTo<ResponseModel<GetStudentDetailResponseDataModel>>();
        }

        public async Task<byte[]> DownloadMediaAsync(string url, string token = "")
        {
            var _httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            // Kiểm tra xem yêu cầu có thành công hay không
            response.EnsureSuccessStatusCode();

            // Đọc nội dung phản hồi dưới dạng mảng byte
            byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

            return imageBytes;
        }

        public async Task<ResponseModel<GetModuleScormResponseDataModel>> GetModuleScormlAsync(string hostName, long moduleid, string token = "")
        {
            token = string.IsNullOrWhiteSpace(token) ? await GetTokenAsync(hostName) : token;

            var _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(hostName);
            var res = await _httpClient.GetAsync($"webservice/rest/server.php?moduleid={moduleid}&moodlewsrestformat=json&service=vtc_cms_api&wsfunction=local_cms_api_course_module_view_scorm&wstoken={token}");
            res.EnsureSuccessStatusCode();

            var strContent = await res.Content.ReadAsStringAsync();

            return strContent?.ParseTo<ResponseModel<GetModuleScormResponseDataModel>>();
        }

        public async Task<ResponseModel<CreateModuleScormResponseDataModel>> CreateModuleScormAsync(string hostName, CreateModuleScormRequestModel model)
        {
            var token = await GetTokenAsync(hostName);
            var form = model.ToFormData(new Dictionary<string, string>
            {
                { "moodlewsrestformat", "json" },
                { "wsfunction", "local_cms_api_course_module_create_scorm" },
                { "wstoken", token },
                { "service", "vtc_cms_api" }
            });

            var _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(hostName);
            var res = await _httpClient.PostAsync("/webservice/rest/server.php", form);
            res.EnsureSuccessStatusCode();

            var strContent = await res.Content.ReadAsStringAsync();

            return strContent?.ParseTo<ResponseModel<CreateModuleScormResponseDataModel>>();
        }
    }
}
