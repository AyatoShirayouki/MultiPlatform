using Base.Messages;
using Freelance_ApplicationService.DTOs.JobRelated;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.JobRelated.SkillsToJobs
{
    public class GetAllSkillsToJobsResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<SkillToJobDTO> Body { get; set; }
    }
}
