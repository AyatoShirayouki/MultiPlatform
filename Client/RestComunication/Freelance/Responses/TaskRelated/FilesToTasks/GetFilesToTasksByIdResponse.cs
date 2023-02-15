using Base.Messages;
using Freelance_ApplicationService.DTOs.Others;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.TaskRelated.FilesToTasks
{
    public class GetFilesToTasksByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public FileTotaskDTO Body { get; set; }
    }
}
