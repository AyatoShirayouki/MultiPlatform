using Client.RestComunication.Freelance.Responses.Bookmarks.BookmarkedJobs;
using Client.RestComunication.Freelance.Responses.Bookmarks.BookmarkedTasks;
using Client.RestComunication.Freelance.Responses.Bookmarks.BookmarkedUsers;
using Client.RestComunication.Freelance.Responses.JobRelated.FilesToJobs;
using Client.RestComunication.Freelance.Responses.JobRelated.JobApplications;
using Client.RestComunication.Freelance.Responses.JobRelated.Jobs;
using Client.RestComunication.Freelance.Responses.JobRelated.JobTypes;
using Client.RestComunication.Freelance.Responses.JobRelated.SkillsToJobs;
using Client.RestComunication.Freelance.Responses.JobRelated.TagsToJobs;
using Client.RestComunication.Freelance.Responses.Others.Categories;
using Client.RestComunication.Freelance.Responses.Others.FreelanceFiles;
using Client.RestComunication.Freelance.Responses.Others.Notes;
using Client.RestComunication.Freelance.Responses.Others.Reviews;
using Client.RestComunication.Freelance.Responses.Others.Skills;
using Client.RestComunication.Freelance.Responses.Others.SkillsToCategories;
using Client.RestComunication.Freelance.Responses.Others.SkillsToUsers;
using Client.RestComunication.Freelance.Responses.Others.Tags;
using Client.RestComunication.Freelance.Responses.TaskRelated.FilesToTasks;
using Client.RestComunication.Freelance.Responses.TaskRelated.SkillsToTasks;
using Client.RestComunication.Freelance.Responses.TaskRelated.TaskBids;
using Client.RestComunication.Freelance.Responses.TaskRelated.Tasks;
using Freelance_ApplicationService.DTOs.Bookmarks;
using Freelance_ApplicationService.DTOs.JobRelated;
using Freelance_ApplicationService.DTOs.Others;
using Freelance_ApplicationService.DTOs.TaskRelated;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Client.RestComunication.Freelance
{
    public class FreelanceRequestExecutor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public FreelanceRequestExecutor (IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //BookmarkedJobs
        public async Task<GetAllBookmarkedJobsResponse> GetAllBookmarkedJobsAction(HttpClient httpClient, string requestQuery)
        {
            GetAllBookmarkedJobsResponse _getAllBookmarkedJobsRespponse = new GetAllBookmarkedJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllBookmarkedJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllBookmarkedJobsRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllBookmarkedJobsRespponse;
        }

        public async Task<DeleteBookmarkedJobsResponse> DeleteBookmarkedJobsAction(HttpClient httpClient, string requestQuery)
        {
            DeleteBookmarkedJobsResponse _deleteBookmarkedJobsResponse = new DeleteBookmarkedJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteBookmarkedJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteBookmarkedJobsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteBookmarkedJobsResponse;
        }

        public async Task<SaveBookmarkedJobsResponse> SaveBookmarkedJobsAction(HttpClient httpClient, BookmarkedJobDTO request, string requestQuery)
        {
            SaveBookmarkedJobsResponse _saveBookmarkedJobsResponse = new SaveBookmarkedJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveBookmarkedJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveBookmarkedJobsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveBookmarkedJobsResponse;
        }

        public async Task<GetBookmarkedJobsByIdResponse> GetBookmarkedJobsByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetBookmarkedJobsByIdResponse _getBookmarkedJobsByIdResponse = new GetBookmarkedJobsByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetBookmarkedJobsByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getBookmarkedJobsByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getBookmarkedJobsByIdResponse;
        }

        //BookmarkedTasks
        public async Task<GetAllBookmarkedTasksResponse> GetAllBookmarkedTasksAction(HttpClient httpClient, string requestQuery)
        {
            GetAllBookmarkedTasksResponse _getAllBookmarkedTasksRespponse = new GetAllBookmarkedTasksResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllBookmarkedTasksResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllBookmarkedTasksRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllBookmarkedTasksRespponse;
        }

        public async Task<DeleteBookmarkedTasksResponse> DeleteBookmarkedTasksAction(HttpClient httpClient, string requestQuery)
        {
            DeleteBookmarkedTasksResponse _deleteBookmarkedTasksResponse = new DeleteBookmarkedTasksResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteBookmarkedTasksResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteBookmarkedTasksResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteBookmarkedTasksResponse;
        }

        public async Task<SaveBookmarkedTasksResponse> SaveBookmarkedTasksAction(HttpClient httpClient, BookmarkedTaskDTO request, string requestQuery)
        {
            SaveBookmarkedTasksResponse _saveBookmarkedTasksResponse = new SaveBookmarkedTasksResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveBookmarkedTasksResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveBookmarkedTasksResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveBookmarkedTasksResponse;
        }

        public async Task<GetBookmarkedTasksByIdResponse> GetBookmarkedTasksByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetBookmarkedTasksByIdResponse _getBookmarkedTasksByIdResponse = new GetBookmarkedTasksByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetBookmarkedTasksByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getBookmarkedTasksByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getBookmarkedTasksByIdResponse;
        }

        //BookmarkedUsers
        public async Task<GetAllBookmarkedUsersResponse> GetAllBookmarkedUsersAction(HttpClient httpClient, string requestQuery)
        {
            GetAllBookmarkedUsersResponse _getAllBookmarkedUsersRespponse = new GetAllBookmarkedUsersResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllBookmarkedUsersResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllBookmarkedUsersRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllBookmarkedUsersRespponse;
        }

        public async Task<DeleteBookmarkedUsersResponse> DeleteBookmarkedUsersAction(HttpClient httpClient, string requestQuery)
        {
            DeleteBookmarkedUsersResponse _deleteBookmarkedUsersResponse = new DeleteBookmarkedUsersResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteBookmarkedUsersResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteBookmarkedUsersResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteBookmarkedUsersResponse;
        }

        public async Task<SaveBookmarkedUsersResponse> SaveBookmarkedUsersAction(HttpClient httpClient, BookmarkedUserDTO request, string requestQuery)
        {
            SaveBookmarkedUsersResponse _saveBookmarkedUsersResponse = new SaveBookmarkedUsersResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveBookmarkedUsersResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveBookmarkedUsersResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveBookmarkedUsersResponse;
        }

        public async Task<GetBookmarkedUsersByIdResponse> GetBookmarkedUsersByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetBookmarkedUsersByIdResponse _getBookmarkedUsersByIdResponse = new GetBookmarkedUsersByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetBookmarkedUsersByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getBookmarkedUsersByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getBookmarkedUsersByIdResponse;
        }

        //FilesToJobs
        public async Task<GetAllFilesToJobsResponse> GetAllFilesToJobsAction(HttpClient httpClient, string requestQuery)
        {
            GetAllFilesToJobsResponse _getAllFilesToJobsRespponse = new GetAllFilesToJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllFilesToJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllFilesToJobsRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllFilesToJobsRespponse;
        }

        public async Task<DeleteFilesToJobsResponse> DeleteFilesToJobsAction(HttpClient httpClient, string requestQuery)
        {
            DeleteFilesToJobsResponse _deleteFilesToJobsResponse = new DeleteFilesToJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteFilesToJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteFilesToJobsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteFilesToJobsResponse;
        }

        public async Task<SaveFilesToJobsResponse> SaveFileToJobsAction(HttpClient httpClient, FileToJobDTO request, string requestQuery)
        {
            SaveFilesToJobsResponse _saveFilesToJobsResponse = new SaveFilesToJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveFilesToJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveFilesToJobsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveFilesToJobsResponse;
        }

        public async Task<GetFilesToJobsByIdResponse> GetFilesToJobsByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetFilesToJobsByIdResponse _getFilesToJobsByIdResponse = new GetFilesToJobsByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetFilesToJobsByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getFilesToJobsByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getFilesToJobsByIdResponse;
        }

        //JobApplications
        public async Task<GetAllJobApplicationsResponse> GetAllJobApplicationsAction(HttpClient httpClient, string requestQuery)
        {
            GetAllJobApplicationsResponse _getAllJobApplicationsRespponse = new GetAllJobApplicationsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllJobApplicationsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllJobApplicationsRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllJobApplicationsRespponse;
        }

        public async Task<DeleteJobApplicationsResponse> DeleteJobApplicationsAction(HttpClient httpClient, string requestQuery)
        {
            DeleteJobApplicationsResponse _deleteJobApplicationsResponse = new DeleteJobApplicationsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteJobApplicationsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteJobApplicationsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteJobApplicationsResponse;
        }

        public async Task<SaveJobApplicationsResponse> SaveJobApplicationsAction(HttpClient httpClient, JobApplicationDTO request, string requestQuery)
        {
            SaveJobApplicationsResponse _saveJobApplicationsResponse = new SaveJobApplicationsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveJobApplicationsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveJobApplicationsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveJobApplicationsResponse;
        }

        public async Task<GetJobApplicationsByIdResponse> GetJobApplicationsByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetJobApplicationsByIdResponse _getJobApplicationsByIdResponse = new GetJobApplicationsByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetJobApplicationsByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getJobApplicationsByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getJobApplicationsByIdResponse;
        }

        //Jobs
        public async Task<GetAllJobsResponse> GetAllJobsAction(HttpClient httpClient, string requestQuery)
        {
            GetAllJobsResponse _getAllJobsRespponse = new GetAllJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllJobsRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllJobsRespponse;
        }

        public async Task<DeleteJobsResponse> DeleteJobsAction(HttpClient httpClient, string requestQuery)
        {
            DeleteJobsResponse _deleteJobsResponse = new DeleteJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteJobsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteJobsResponse;
        }

        public async Task<SaveJobsResponse> SaveJobsAction(HttpClient httpClient, JobDTO request, string requestQuery)
        {
            SaveJobsResponse _saveJobsResponse = new SaveJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveJobsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveJobsResponse;
        }

        public async Task<GetJobsByIdResponse> GetJobsByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetJobsByIdResponse _getJobsByIdResponse = new GetJobsByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetJobsByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getJobsByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getJobsByIdResponse;
        }

        //JobTypes
        public async Task<GetAllJobTypesResponse> GetAllJobTypesAction(HttpClient httpClient, string requestQuery)
        {
            GetAllJobTypesResponse _getAllJobTypesRespponse = new GetAllJobTypesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllJobTypesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllJobTypesRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllJobTypesRespponse;
        }

        public async Task<DeleteJobTypesResponse> DeleteJobTypesAction(HttpClient httpClient, string requestQuery)
        {
            DeleteJobTypesResponse _deleteJobTypesResponse = new DeleteJobTypesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteJobTypesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteJobTypesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteJobTypesResponse;
        }

        public async Task<SaveJobTypesResponse> SaveJobTypesAction(HttpClient httpClient, JobTypeDTO request, string requestQuery)
        {
            SaveJobTypesResponse _saveJobTypesResponse = new SaveJobTypesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveJobTypesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveJobTypesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveJobTypesResponse;
        }

        public async Task<GetJobTypesByIdResponse> GetJobTypesByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetJobTypesByIdResponse _getJobTypesByIdResponse = new GetJobTypesByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetJobTypesByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getJobTypesByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getJobTypesByIdResponse;
        }

        //SkillsToJobs
        public async Task<GetAllSkillsToJobsResponse> GetAllSkillsToJobsAction(HttpClient httpClient, string requestQuery)
        {
            GetAllSkillsToJobsResponse _getAllSkillsToJobsRespponse = new GetAllSkillsToJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllSkillsToJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllSkillsToJobsRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllSkillsToJobsRespponse;
        }

        public async Task<DeleteSkillsToJobsResponse> DeleteSkillsToJobsAction(HttpClient httpClient, string requestQuery)
        {
            DeleteSkillsToJobsResponse _deleteSkillsToJobsResponse = new DeleteSkillsToJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteSkillsToJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteSkillsToJobsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteSkillsToJobsResponse;
        }

        public async Task<SaveSkillsToJobsResponse> SaveSkillsToJobsAction(HttpClient httpClient, SkillToJobDTO request, string requestQuery)
        {
            SaveSkillsToJobsResponse _saveSkillsToJobsResponse = new SaveSkillsToJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveSkillsToJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveSkillsToJobsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveSkillsToJobsResponse;
        }

        public async Task<GetSkillsToJobsByIdResponse> GetSkillsToJobsByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetSkillsToJobsByIdResponse _getSkillsToJobsByIdResponse = new GetSkillsToJobsByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetSkillsToJobsByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getSkillsToJobsByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getSkillsToJobsByIdResponse;
        }

        //TagsToJobs
        public async Task<GetAllTagsToJobsResponse> GetAllTagsToJobsAction(HttpClient httpClient, string requestQuery)
        {
            GetAllTagsToJobsResponse _getAllTagsToJobsRespponse = new GetAllTagsToJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllTagsToJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllTagsToJobsRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllTagsToJobsRespponse;
        }

        public async Task<DeleteTagsToJobsResponse> DeleteTagsToJobsAction(HttpClient httpClient, string requestQuery)
        {
            DeleteTagsToJobsResponse _deleteTagsToJobsResponse = new DeleteTagsToJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteTagsToJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteTagsToJobsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteTagsToJobsResponse;
        }

        public async Task<SaveTagsToJobsResponse> SaveTagsToJobsAction(HttpClient httpClient, TagToJobDTO request, string requestQuery)
        {
            SaveTagsToJobsResponse _saveTagsToJobsResponse = new SaveTagsToJobsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveTagsToJobsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveTagsToJobsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveTagsToJobsResponse;
        }

        public async Task<GetTagsToJobsByIdResponse> GetTagsToJobsByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetTagsToJobsByIdResponse _getTagsToJobsByIdResponse = new GetTagsToJobsByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetTagsToJobsByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getTagsToJobsByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getTagsToJobsByIdResponse;
        }

        //Categories
        public async Task<GetAllCategoriesResponse> GetAllCategoriesAction(HttpClient httpClient, string requestQuery)
        {
            GetAllCategoriesResponse _getAllCategoriesRespponse = new GetAllCategoriesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllCategoriesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllCategoriesRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllCategoriesRespponse;
        }

        public async Task<DeleteCategoriesResponse> DeleteCategoriesAction(HttpClient httpClient, string requestQuery)
        {
            DeleteCategoriesResponse _deleteCategoriesResponse = new DeleteCategoriesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteCategoriesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteCategoriesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteCategoriesResponse;
        }

        public async Task<SaveCategoriesResponse> SaveCategoriesAction(HttpClient httpClient, CategoryDTO request, string requestQuery)
        {
            SaveCategoriesResponse _saveCategoriesResponse = new SaveCategoriesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveCategoriesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveCategoriesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveCategoriesResponse;
        }

        public async Task<GetCategoriesByIdResponse> GetCategoriesByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetCategoriesByIdResponse _getCategoriesByIdResponse = new GetCategoriesByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetCategoriesByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getCategoriesByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getCategoriesByIdResponse;
        }

        //FreelanceFiles
        public async Task<GetAllFreelanceFilesResponse> GetAllFreelanceFilesAction(HttpClient httpClient, string requestQuery)
        {
            GetAllFreelanceFilesResponse _getAllFreelanceFilesRespponse = new GetAllFreelanceFilesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllFreelanceFilesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllFreelanceFilesRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllFreelanceFilesRespponse;
        }

        public async Task<DeleteFreelanceFilesResponse> DeleteFreelanceFilesAction(HttpClient httpClient, string requestQuery)
        {
            DeleteFreelanceFilesResponse _deleteFreelanceFilesResponse = new DeleteFreelanceFilesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteFreelanceFilesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteFreelanceFilesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteFreelanceFilesResponse;
        }

        public async Task<SaveFreelanceFilesResponse> SaveFreelanceFilesAction(HttpClient httpClient, FreelanceFileDTO request, string requestQuery)
        {
            SaveFreelanceFilesResponse _saveFreelanceFilesResponse = new SaveFreelanceFilesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveFreelanceFilesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveFreelanceFilesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveFreelanceFilesResponse;
        }

        public async Task<GetFreelanceFilesByIdResponse> GetFreelanceFilesByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetFreelanceFilesByIdResponse _getFreelanceFilesByIdResponse = new GetFreelanceFilesByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetFreelanceFilesByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getFreelanceFilesByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getFreelanceFilesByIdResponse;
        }

        //Notes
        public async Task<GetAllNotesResponse> GetAllNotesAction(HttpClient httpClient, string requestQuery)
        {
            GetAllNotesResponse _getAllNotesRespponse = new GetAllNotesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllNotesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllNotesRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllNotesRespponse;
        }

        public async Task<DeleteNotesResponse> DeleteNotesAction(HttpClient httpClient, string requestQuery)
        {
            DeleteNotesResponse _deleteNotesResponse = new DeleteNotesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteNotesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteNotesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteNotesResponse;
        }

        public async Task<SaveNotesResponse> SaveNotesAction(HttpClient httpClient, NoteDTO request, string requestQuery)
        {
            SaveNotesResponse _saveNotesResponse = new SaveNotesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveNotesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveNotesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveNotesResponse;
        }

        public async Task<GetNotesByIdResponse> GetNotesByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetNotesByIdResponse _getNotesByIdResponse = new GetNotesByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetNotesByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getNotesByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getNotesByIdResponse;
        }

        //Reviews
        public async Task<GetAllReviewsResponse> GetAllReviewsAction(HttpClient httpClient, string requestQuery)
        {
            GetAllReviewsResponse _getAllReviewsRespponse = new GetAllReviewsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllReviewsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllReviewsRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllReviewsRespponse;
        }

        public async Task<DeleteReviewsResponse> DeleteReviewsAction(HttpClient httpClient, string requestQuery)
        {
            DeleteReviewsResponse _deleteReviewsResponse = new DeleteReviewsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteReviewsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteReviewsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteReviewsResponse;
        }

        public async Task<SaveReviewsResponse> SaveReviewsAction(HttpClient httpClient, ReviewDTO request, string requestQuery)
        {
            SaveReviewsResponse _saveReviewsResponse = new SaveReviewsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveReviewsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveReviewsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveReviewsResponse;
        }

        public async Task<GetReviewsByIdResponse> GetReviewsByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetReviewsByIdResponse _getReviewsByIdResponse = new GetReviewsByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetReviewsByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getReviewsByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getReviewsByIdResponse;
        }

        //Skills
        public async Task<GetAllSkillsResponse> GetAllSkillsAction(HttpClient httpClient, string requestQuery)
        {
            GetAllSkillsResponse _getAllSkillsRespponse = new GetAllSkillsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllSkillsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllSkillsRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllSkillsRespponse;
        }

        public async Task<DeleteSkillsResponse> DeleteSkillsAction(HttpClient httpClient, string requestQuery)
        {
            DeleteSkillsResponse _deleteSkillsResponse = new DeleteSkillsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteSkillsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteSkillsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteSkillsResponse;
        }

        public async Task<SaveSkillsResponse> SaveSkillsAction(HttpClient httpClient, SkillDTO request, string requestQuery)
        {
            SaveSkillsResponse _saveSkillsResponse = new SaveSkillsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveSkillsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveSkillsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveSkillsResponse;
        }

        public async Task<GetSkillsByIdResponse> GetSkillsByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetSkillsByIdResponse _getSkillsByIdResponse = new GetSkillsByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetSkillsByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getSkillsByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getSkillsByIdResponse;
        }

        //SkillsToCategories
        public async Task<GetAllSkillsToCategoriesResponse> GetAllSkillsToCategoriesAction(HttpClient httpClient, string requestQuery)
        {
            GetAllSkillsToCategoriesResponse _getAllSkillsToCategoriesRespponse = new GetAllSkillsToCategoriesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllSkillsToCategoriesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllSkillsToCategoriesRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllSkillsToCategoriesRespponse;
        }

        public async Task<DeleteSkillsToCategoriesResponse> DeleteSkillsToCategoriesAction(HttpClient httpClient, string requestQuery)
        {
            DeleteSkillsToCategoriesResponse _deleteSkillsToCategoriesResponse = new DeleteSkillsToCategoriesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteSkillsToCategoriesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteSkillsToCategoriesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteSkillsToCategoriesResponse;
        }

        public async Task<SaveSkillsToCategoriesResponse> SaveSkillsToCategoriesAction(HttpClient httpClient, SkillToCategoryDTO request, string requestQuery)
        {
            SaveSkillsToCategoriesResponse _saveSkillsToCategoriesResponse = new SaveSkillsToCategoriesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveSkillsToCategoriesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveSkillsToCategoriesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveSkillsToCategoriesResponse;
        }

        public async Task<GetSkillsToCategoriesByIdResponse> GetSkillsToCategoriesByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetSkillsToCategoriesByIdResponse _getSkillsToCategoriesByIdResponse = new GetSkillsToCategoriesByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetSkillsToCategoriesByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getSkillsToCategoriesByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getSkillsToCategoriesByIdResponse;
        }

		//SkillsToUsers
		public async Task<GetAllSkillsToUsersResponse> GetAllSkillsToUsersAction(HttpClient httpClient, string requestQuery)
		{
			GetAllSkillsToUsersResponse _getAllSkillsToUsersRespponse = new GetAllSkillsToUsersResponse();

			httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
			httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

			using (var response = await httpClient.GetAsync(requestQuery))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();

				if (!string.IsNullOrEmpty(apiResponse))
				{
					var convert = JsonConvert.DeserializeObject<GetAllSkillsToUsersResponse>(apiResponse);

					if (convert != null)
					{
						_getAllSkillsToUsersRespponse = convert;

						_session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
						_session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
					}
				}
			}

			return _getAllSkillsToUsersRespponse;
		}

		public async Task<DeleteSkillsToUsersResponse> DeleteSkillsToUsersAction(HttpClient httpClient, string requestQuery)
		{
			DeleteSkillsToUsersResponse _deleteSkillsToUsersResponse = new DeleteSkillsToUsersResponse();

			httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
			httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

			using (var response = await httpClient.DeleteAsync(requestQuery))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();

				if (!string.IsNullOrEmpty(apiResponse))
				{
					var convert = JsonConvert.DeserializeObject<DeleteSkillsToUsersResponse>(apiResponse);

					if (convert != null)
					{
						_deleteSkillsToUsersResponse = convert;

						_session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
						_session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
					}
				}
			}

			return _deleteSkillsToUsersResponse;
		}

		public async Task<SaveSkillsToUsersResponse> SaveSkillsToUsersAction(HttpClient httpClient, SkillToUserDTO request, string requestQuery)
		{
			SaveSkillsToUsersResponse _saveSkillsToUsersResponse = new SaveSkillsToUsersResponse();

			httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
			httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

			httpClient.BaseAddress = new Uri(requestQuery);
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var content = JsonConvert.SerializeObject(request);
			var buffer = System.Text.Encoding.UTF8.GetBytes(content);
			var byteContent = new ByteArrayContent(buffer);
			byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			using (var response = await httpClient.PostAsync("", byteContent))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();

				if (!string.IsNullOrEmpty(apiResponse))
				{
					var convert = JsonConvert.DeserializeObject<SaveSkillsToUsersResponse>(apiResponse);

					if (convert != null)
					{
						_saveSkillsToUsersResponse = convert;

						_session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
						_session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
					}
				}
			}

			return _saveSkillsToUsersResponse;
		}

		public async Task<GetSkillsToUsersByIdResponse> GetSkillsToUsersByIdAction(HttpClient httpClient, string requestQuery)
		{
			GetSkillsToUsersByIdResponse _getSkillsToUsersByIdResponse = new GetSkillsToUsersByIdResponse();

			httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
			httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

			using (var response = await httpClient.GetAsync(requestQuery))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();

				if (!string.IsNullOrEmpty(apiResponse))
				{
					var convert = JsonConvert.DeserializeObject<GetSkillsToUsersByIdResponse>(apiResponse);

					if (convert != null)
					{
						_getSkillsToUsersByIdResponse = convert;

						_session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
						_session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
					}
				}
			}

			return _getSkillsToUsersByIdResponse;
		}

		//Tags
		public async Task<GetAllTagsResponse> GetAllTagsAction(HttpClient httpClient, string requestQuery)
        {
            GetAllTagsResponse _getAllTagsRespponse = new GetAllTagsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllTagsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllTagsRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllTagsRespponse;
        }

        public async Task<DeleteTagsResponse> DeleteTagsAction(HttpClient httpClient, string requestQuery)
        {
            DeleteTagsResponse _deleteTagsResponse = new DeleteTagsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteTagsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteTagsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteTagsResponse;
        }

        public async Task<SaveTagsResponse> SaveTagsAction(HttpClient httpClient, TagDTO request, string requestQuery)
        {
            SaveTagsResponse _saveTagsResponse = new SaveTagsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveTagsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveTagsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveTagsResponse;
        }

        public async Task<GetTagsByIdResponse> GetTagsByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetTagsByIdResponse _getTagsByIdResponse = new GetTagsByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetTagsByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getTagsByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getTagsByIdResponse;
        }

        //FilesToTasks
        public async Task<GetAllFilesToTasksResponse> GetAllFilesToTasksAction(HttpClient httpClient, string requestQuery)
        {
            GetAllFilesToTasksResponse _getAllFilesToTasksRespponse = new GetAllFilesToTasksResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllFilesToTasksResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllFilesToTasksRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllFilesToTasksRespponse;
        }

        public async Task<DeleteFilesToTasksResponse> DeleteFilesToTasksAction(HttpClient httpClient, string requestQuery)
        {
            DeleteFilesToTasksResponse _deleteFilesToTasksResponse = new DeleteFilesToTasksResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteFilesToTasksResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteFilesToTasksResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteFilesToTasksResponse;
        }

        public async Task<SaveFilesToTasksResponse> SaveFilesToTasksAction(HttpClient httpClient, FileToTaskDTO request, string requestQuery)
        {
            SaveFilesToTasksResponse _saveFilesToTasksResponse = new SaveFilesToTasksResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveFilesToTasksResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveFilesToTasksResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveFilesToTasksResponse;
        }

        public async Task<GetFilesToTasksByIdResponse> GetFilesToTasksByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetFilesToTasksByIdResponse _getFilesToTasksByIdResponse = new GetFilesToTasksByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetFilesToTasksByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getFilesToTasksByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getFilesToTasksByIdResponse;
        }

        //SkillsToTasks
        public async Task<GetAllSkillsToTasksResponse> GetAllSkillsToTasksAction(HttpClient httpClient, string requestQuery)
        {
            GetAllSkillsToTasksResponse _getAllSkillsToTasksRespponse = new GetAllSkillsToTasksResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllSkillsToTasksResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllSkillsToTasksRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllSkillsToTasksRespponse;
        }

        public async Task<DeleteSkillsToTasksResponse> DeleteSkillsToTasksAction(HttpClient httpClient, string requestQuery)
        {
            DeleteSkillsToTasksResponse _deleteSkillsToTasksResponse = new DeleteSkillsToTasksResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteSkillsToTasksResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteSkillsToTasksResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteSkillsToTasksResponse;
        }

        public async Task<SaveSkillsToTasksResponse> SaveSkillsToTasksAction(HttpClient httpClient, SkillToTaskDTO request, string requestQuery)
        {
            SaveSkillsToTasksResponse _saveSkillsToTasksResponse = new SaveSkillsToTasksResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveSkillsToTasksResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveSkillsToTasksResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveSkillsToTasksResponse;
        }

        public async Task<GetSkillsToTasksByIdResponse> GetSkillsToTasksByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetSkillsToTasksByIdResponse _getSkillsToTasksByIdResponse = new GetSkillsToTasksByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetSkillsToTasksByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getSkillsToTasksByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getSkillsToTasksByIdResponse;
        }

        //TaskBids
        public async Task<GetAllTaskBidsResponse> GetAllTaskBidsAction(HttpClient httpClient, string requestQuery)
        {
            GetAllTaskBidsResponse _getAllTaskBidsRespponse = new GetAllTaskBidsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllTaskBidsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllTaskBidsRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllTaskBidsRespponse;
        }

        public async Task<DeleteTaskBidsResponse> DeleteTaskBidsAction(HttpClient httpClient, string requestQuery)
        {
            DeleteTaskBidsResponse _deleteTaskBidsResponse = new DeleteTaskBidsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteTaskBidsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteTaskBidsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteTaskBidsResponse;
        }

        public async Task<SaveTaskBidsResponse> SaveTaskBidsAction(HttpClient httpClient, TaskBidDTO request, string requestQuery)
        {
            SaveTaskBidsResponse _saveTaskBidsResponse = new SaveTaskBidsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveTaskBidsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveTaskBidsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveTaskBidsResponse;
        }

        public async Task<GetTaskBidsByIdResponse> GetTaskBidsByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetTaskBidsByIdResponse _getTaskBidsByIdResponse = new GetTaskBidsByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetTaskBidsByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getTaskBidsByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getTaskBidsByIdResponse;
        }

        //Tasks
        public async Task<GetAllTasksResponse> GetAllTasksAction(HttpClient httpClient, string requestQuery)
        {
            GetAllTasksResponse _getAllTasksRespponse = new GetAllTasksResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllTasksResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllTasksRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllTasksRespponse;
        }

        public async Task<DeleteTasksResponse> DeleteTasksAction(HttpClient httpClient, string requestQuery)
        {
            DeleteTasksResponse _deleteTasksResponse = new DeleteTasksResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteTasksResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteTasksResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteTasksResponse;
        }

        public async Task<SaveTasksResponse> SaveTasksAction(HttpClient httpClient, TaskDTO request, string requestQuery)
        {
            SaveTasksResponse _saveTasksResponse = new SaveTasksResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveTasksResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveTasksResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveTasksResponse;
        }

        public async Task<GetTasksByIdResponse> GetTasksByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetTasksByIdResponse _getTasksByIdResponse = new GetTasksByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetTasksByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getTasksByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getTasksByIdResponse;
        }
    }
}
