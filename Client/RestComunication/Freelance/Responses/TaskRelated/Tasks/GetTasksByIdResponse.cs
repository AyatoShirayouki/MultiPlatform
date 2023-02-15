using Base.Messages;
using Freelance_ApplicationService.DTOs.TaskRelated;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.TaskRelated.Tasks
{
    public class GetTasksByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public TaskDTO Body { get; set; }
    }
}
