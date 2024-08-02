using Azure;
using ManagerVM.Contacts.Models;
using ManagerVM.Contacts.Models.LMSModels.Request;
using ManagerVM.Contacts.Models.LMSModels.Response;
using ManagerVM.Data;
using ManagerVM.Helper;
using ManagerVM.Services.Features.VM.Notifications.DatabaseInstall;
using ManagerVM.Services.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services.Features.VM.Handlers.SyncDatabase
{
    public class SyncCategory : INotificationHandler<InitializationSuccessfulDatabaseNotification>
    {
        private readonly VMDbContext _dbContext;
        private readonly ILMSClient _lmsClient;
        private readonly MoodleConfig _moodleConfig;
        public SyncCategory(VMDbContext dbContext, ILMSClient lmsClient, MoodleConfig moodleConfig)
        {
            _dbContext = dbContext;
            _lmsClient = lmsClient;
            _moodleConfig = moodleConfig;
        }

        public async Task Handle(InitializationSuccessfulDatabaseNotification notification, CancellationToken cancellationToken)
        {
            var listCategory = new Dictionary<string, long>();

            var vm = await _dbContext.VMEntities.AsNoTracking().FirstOrDefaultAsync(f=>f.InstanceId.Equals(notification.VMInstanceId), cancellationToken);

            if (vm == null)
            {
                throw new Exception($"[{nameof(SyncCategory)}]VM not exist! (VmID = {notification.VMInstanceId})");
            }

            //Lấy token MainServer
            var tokenMainServer = (await _lmsClient.GetTokenAsync(_moodleConfig.MainServer.HostName, new Contacts.Models.LMSModels.Request.GetTokenRequestModel
            {
                Password = _moodleConfig.MainServer.Password,
                UserName = _moodleConfig.MainServer.UserName
            })).Token;

            //Lấy thông tin lớp học từ MainServer
            var room = await _lmsClient.GetClassAsync(_moodleConfig.MainServer.HostName, vm.RoomId, tokenMainServer);

            if (room?.StatusCode == 1)
            {
                var newRoom = new CreateClassRequestModel
                {
                    code = room.Data.Code,
                    name = room.Data.Name,
                    statusId = room.Data.StatusId,
                    studentLimit = room.Data.StudentLimit
                };

                //Lấy thông tin lĩnh vực từ MainServer
                var categoryRoom = (await _lmsClient.GetCategoryOfClassAsync(_moodleConfig.MainServer.HostName, room.Data.CategoryId, tokenMainServer)).Data;
                
                //Tải Img của lĩnh vực lên server mới
                var imageCategoryRoom = await _lmsClient.DownloadMediaAsync(categoryRoom.Img);//Convert.FromBase64String(categoryRoom.Img.Replace("data:image/svg+xml;base64,",""));
                var resUploadImageCategoryRoom = await _lmsClient.UploadMediaAsync(vm.HostName, imageCategoryRoom.Item1, $"image{imageCategoryRoom.Item2}");

                //Tạo lĩnh vực của lớp học trên server mới
                var resCreateCategoryRoom = await _lmsClient.CreateCategoryAsync(vm.HostName, new Contacts.Models.LMSModels.Request.CreateCategoryRequestModel
                {
                    name = categoryRoom.Name,
                    code = categoryRoom.Code,
                    description = categoryRoom.Description,
                    img = resUploadImageCategoryRoom.Data[0].Itemid
                });

                listCategory.Add(categoryRoom.Code, resCreateCategoryRoom.Data[0].Id);

                newRoom.categoryId = resCreateCategoryRoom.Data[0].Id;

                var listNewCourseId = new List<long>();
                //Lấy thông tin khóa học từ MainServer
                var course = await _lmsClient.GetCourseOfClassAsync(_moodleConfig.MainServer.HostName, vm.RoomId, tokenMainServer);

                //Xử lý tạo danh sách khóa học
                foreach (var item in course.Data.Items)
                {
                    //Lấy thông tin chi tiết khóa học
                    var courseDetail = await _lmsClient.GetCourseDetailAsync(_moodleConfig.MainServer.HostName,
                        item.Id, tokenMainServer);

                    var newCourse = new CreateCourseRequestModel
                    {
                        description = courseDetail.Data.Description,
                        enddate = courseDetail.Data.Enddate,
                        fullname = courseDetail.Data.Fullname,
                        idnumber = courseDetail.Data.Idnumber,
                        shortname = courseDetail.Data.Shortname,
                        startdate = courseDetail.Data.Startdate
                    };

                    //Lấy thông tin lĩnh vực của khóa
                    if (courseDetail.Data.Categoryid > 0)
                    {
                        var categoryCourse = (await _lmsClient.GetCategoryOfClassAsync(_moodleConfig.MainServer.HostName,
                        courseDetail.Data.Categoryid.Value, tokenMainServer)).Data;

                        if (!listCategory.Keys.Contains(categoryCourse.Code))
                        {
                            //Tải Img của lĩnh vực lên server mới
                            var imageCategoryCourse = await _lmsClient.DownloadMediaAsync(categoryCourse.Img);//Convert.FromBase64String(categoryCourse.Img.Replace("data:image/svg+xml;base64,", ""));
                            var resUploadImageCategoryCourse = await _lmsClient.UploadMediaAsync(vm.HostName, imageCategoryCourse.Item1, $"image{imageCategoryCourse.Item2}");

                            //Tạo mới lĩnh vực của khóa học
                            var resCreateCategoryCourse = await _lmsClient.CreateCategoryAsync(vm.HostName, new Contacts.Models.LMSModels.Request.CreateCategoryRequestModel
                            {
                                name = categoryCourse.Name,
                                code = categoryCourse.Code,
                                description = categoryCourse.Description,
                                img = resUploadImageCategoryCourse.Data[0].Itemid
                            });

                            newCourse.categoryid = resCreateCategoryCourse.Data[0].Id;
                        }
                        else
                        {
                            newCourse.categoryid = listCategory[categoryCourse.Code];
                        }
                    }

                    //Tải ảnh đại diện của khóa học lên server mới
                    if (!string.IsNullOrWhiteSpace(courseDetail.Data.Img))
                    {
                        var imageCourse = await _lmsClient.DownloadMediaAsync(courseDetail.Data.Img);

                        //Tải lên image của khóa học
                        var resUploadImageCourse = await _lmsClient.UploadMediaAsync(vm.HostName,
                            imageCourse.Item1, $"Image{imageCourse.Item2}");

                        newCourse.img = resUploadImageCourse.Data[0].Itemid;
                    }

                    //Xử lý tài liệu liên quan
                    if(courseDetail.Data.References?.Count() > 0)
                    {
                        foreach (var item1 in courseDetail.Data.References)
                        {
                            
                        }
                    }

                    //Tạo mới khóa học
                    var resCreateCourse = await _lmsClient.CreateCourseAsync(vm.HostName, newCourse);

                    //Tạo mới hoạt động
                    //Tạo mới Module Scorm
                    var scormModule = courseDetail.Data.Modules?.FirstOrDefault(f => f.modname == "scorm");
                    if (scormModule != null)
                    {
                        var scormModuleDetail = (await _lmsClient.GetModuleScormlAsync(_moodleConfig.MainServer.HostName,
                            scormModule.id, tokenMainServer))?.Data;

                        var newScormModule = new CreateModuleScormRequestModel
                        {
                            attemptsgrading = scormModuleDetail?.attemptsgrading??0,
                            completiontracking = scormModuleDetail?.completiontracking ?? 0,
                            courseid = resCreateCourse.Data[0].Id,
                            description = scormModuleDetail?.description,
                            expectcompletedon = scormModuleDetail?.expectcompletedon ?? 0,
                            forcenewattempt = scormModuleDetail?.forcenewattempt ?? 0,
                            grademethod = scormModuleDetail?.grademethod ?? 0,
                            lockafterfinalattempt = scormModuleDetail?.lockafterfinalattempt ?? 0,
                            maxattempt = scormModuleDetail?.maxattempt ?? 0,
                            maxgrade = scormModuleDetail?.maxgrade ?? 0,
                            name = scormModuleDetail?.name,
                            requiregrade = scormModuleDetail?.requiregrade ?? 0,
                            requireview = scormModuleDetail?.requireview ?? 0,
                            restrictaccess = null,// scormModuleDetail?.restrictaccess,
                            section = 0,//scormModuleDetail?.section,
                            timeactivity = scormModuleDetail?.timeactivity,
                            timeclose = scormModuleDetail?.timeclose ?? 0,
                            timeopen = scormModuleDetail?.timeopen ?? 0
                        };

                        if (!string.IsNullOrWhiteSpace(scormModuleDetail.filelink))
                        {
                            var fileScormData = await _lmsClient.DownloadMediaAsync(scormModuleDetail.filelink, tokenMainServer);
                            var uploadFileScorm = await _lmsClient.UploadMediaAsync(vm.HostName, fileScormData.Item1, $"File_Scorm{fileScormData.Item2}");
                            newScormModule.file = uploadFileScorm.Data[0].Itemid;
                        }

                        if (!string.IsNullOrWhiteSpace(scormModuleDetail.picture))
                        {
                            var pictureData = await _lmsClient.DownloadMediaAsync(scormModuleDetail.picture, tokenMainServer);
                            var uploadPictue = await _lmsClient.UploadMediaAsync(vm.HostName, pictureData.Item1, $"pictue{pictureData.Item2}");
                            newScormModule.picture = uploadPictue.Data[0].Itemid;
                        }
                        
                        var resCreateMpdule = await _lmsClient.CreateModuleScormAsync(vm.HostName, newScormModule);
                        if(resCreateMpdule?.StatusCode != 1)
                        {
                            throw new Exception("Create Module Scorm fail!");
                        }
                    }

                    //Tạo mới khóa học QUIZ

                    listNewCourseId.Add(resCreateCourse.Data[0].Id);
                }

                newRoom.courseIds = listNewCourseId.ToJson();

                //Lấy danh sách học viên
                var studens = await _lmsClient.GetStudentsAsync(_moodleConfig.MainServer.HostName,
                     vm.RoomId, tokenMainServer);

                var listNewStudentId = new List<long>();

                foreach (var item in studens.Data.Items)
                {
                    //Lấy thông tin chi tiết học viên
                    var studentDetail = (await _lmsClient.GetStudentDetailAsync(_moodleConfig.MainServer.HostName,
                        item.Id, tokenMainServer)).Data;

                    //Tạo mới học viên
                    var resCreateStudent = await _lmsClient.CreateStudentAsync<CreateStudentResponseDataModel>(vm.HostName,
                        new Contacts.Models.LMSModels.Request.CreateStudentRequestModel
                        {
                            cityName = studentDetail.CityName,
                            countryCode = studentDetail.CountryCode,
                            dateOfBirth = studentDetail.DateOfBirth,
                            department = studentDetail.Department,
                            description = studentDetail.Description,
                            email = studentDetail.Email,
                            firstName = studentDetail.FirstName,
                            lastName = studentDetail.LastName,
                            gender = 0,
                            job = studentDetail.Job,
                            password = "Vtc@123#",
                            phone = studentDetail.Phone,
                            suspendedStatus = studentDetail.SuspendedStatus,
                            userName = studentDetail.UserName
                        });

                    listNewStudentId.Add(resCreateStudent.Data.Id);
                }

                newRoom.userIds = listNewStudentId.ToJson();

                var resCreateRoom = await _lmsClient.CreateClassAsync<dynamic>(vm.HostName, newRoom);

                var st = resCreateRoom.StatusCode;
            }
        }
    }
}
