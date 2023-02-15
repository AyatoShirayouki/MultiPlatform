using Base.Messages;
using Freelance_ApplicationService.DTOs.JobRelated;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.JobRelated.TagsToJobs
{
    public class GetAllTagsToJobsResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<TagToJobDTO> Body { get; set; }
    }
}
