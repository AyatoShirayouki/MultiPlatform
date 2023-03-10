using Base.Messages;
using Freelance_ApplicationService.DTOs.TaskRelated;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.TaskRelated.SkillsToTasks
{
    public class GetAllSkillsToTasksResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<SkillToTaskDTO> Body { get; set; }
    }
}
