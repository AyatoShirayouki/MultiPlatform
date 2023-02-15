using Base.Messages;
using Freelance_ApplicationService.DTOs.TaskRelated;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.TaskRelated.FilesToTasks
{
    public class GetAllFilesToTasksResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<FileToTaskDTO> Body { get; set; }
    }
}
