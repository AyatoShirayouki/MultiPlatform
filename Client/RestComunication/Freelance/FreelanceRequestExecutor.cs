using Client.RestComunication.Freelance.Responses.Bookmarks.BookmarkedJobs;
using Freelance_ApplicationService.DTOs.Bookmarks;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Client.RestComunication.Freelance
{
    public class FreelanceRequestExecutor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public FreelanceRequestExecutor(IHttpContextAccessor httpContextAccessor)
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

                    if (convert.Body != null)
                    {
                        _getAllBookmarkedJobsRespponse.Code = JsonConvert.DeserializeObject<GetAllBookmarkedJobsResponse>(apiResponse).Code;
                        _getAllBookmarkedJobsRespponse.Body = JsonConvert.DeserializeObject<GetAllBookmarkedJobsResponse>(apiResponse).Body;
                        _getAllBookmarkedJobsRespponse.JwtSuccess = JsonConvert.DeserializeObject<GetAllBookmarkedJobsResponse>(apiResponse).JwtSuccess;
                        _getAllBookmarkedJobsRespponse.JwtErrors = JsonConvert.DeserializeObject<GetAllBookmarkedJobsResponse>(apiResponse).JwtErrors;
                        _getAllBookmarkedJobsRespponse.Error = JsonConvert.DeserializeObject<GetAllBookmarkedJobsResponse>(apiResponse).Error;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllBookmarkedJobsRespponse;
        }

        public async Task<DeleteBookmarkedJobsResponse> DeleteBookmarkedJobAction(HttpClient httpClient, string requestQuery)
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

                    if (convert.Code != null)
                    {
                        _deleteBookmarkedJobsResponse.Code = JsonConvert.DeserializeObject<DeleteBookmarkedJobsResponse>(apiResponse).Code;
                        _deleteBookmarkedJobsResponse.Body = JsonConvert.DeserializeObject<DeleteBookmarkedJobsResponse>(apiResponse).Body;
                        _deleteBookmarkedJobsResponse.JwtSuccess = JsonConvert.DeserializeObject<DeleteBookmarkedJobsResponse>(apiResponse).JwtSuccess;
                        _deleteBookmarkedJobsResponse.JwtErrors = JsonConvert.DeserializeObject<DeleteBookmarkedJobsResponse>(apiResponse).JwtErrors;
                        _deleteBookmarkedJobsResponse.Error = JsonConvert.DeserializeObject<DeleteBookmarkedJobsResponse>(apiResponse).Error;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteBookmarkedJobsResponse;
        }

        public async Task<SaveBookmarkedJobsResponse> SaveBookmarkedJobAction(HttpClient httpClient, BookmarkedJobDTO request, string requestQuery)
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

                    if (convert.Code != null)
                    {
                        _saveBookmarkedJobsResponse.Code = JsonConvert.DeserializeObject<SaveBookmarkedJobsResponse>(apiResponse).Code;
                        _saveBookmarkedJobsResponse.Body = JsonConvert.DeserializeObject<SaveBookmarkedJobsResponse>(apiResponse).Body;
                        _saveBookmarkedJobsResponse.JwtSuccess = JsonConvert.DeserializeObject<SaveBookmarkedJobsResponse>(apiResponse).JwtSuccess;
                        _saveBookmarkedJobsResponse.JwtErrors = JsonConvert.DeserializeObject<SaveBookmarkedJobsResponse>(apiResponse).JwtErrors;
                        _saveBookmarkedJobsResponse.Error = JsonConvert.DeserializeObject<SaveBookmarkedJobsResponse>(apiResponse).Error;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveBookmarkedJobsResponse;
        }

        public async Task<GetBookmarkedJobsByIdResponse> GetBookmarkedJobByIdAction(HttpClient httpClient, string requestQuery)
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

                    if (convert.Body != null)
                    {
                        _getBookmarkedJobsByIdResponse.Code = JsonConvert.DeserializeObject<GetBookmarkedJobsByIdResponse>(apiResponse).Code;
                        _getBookmarkedJobsByIdResponse.Body = JsonConvert.DeserializeObject<GetBookmarkedJobsByIdResponse>(apiResponse).Body;
                        _getBookmarkedJobsByIdResponse.JwtSuccess = JsonConvert.DeserializeObject<GetBookmarkedJobsByIdResponse>(apiResponse).JwtSuccess;
                        _getBookmarkedJobsByIdResponse.JwtErrors = JsonConvert.DeserializeObject<GetBookmarkedJobsByIdResponse>(apiResponse).JwtErrors;
                        _getBookmarkedJobsByIdResponse.Error = JsonConvert.DeserializeObject<GetBookmarkedJobsByIdResponse>(apiResponse).Error;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getBookmarkedJobsByIdResponse;
        }
    }
}
