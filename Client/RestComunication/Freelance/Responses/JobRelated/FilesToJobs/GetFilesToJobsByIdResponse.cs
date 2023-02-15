using Base.Messages;
using Freelance_ApplicationService.DTOs.JobRelated;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.JobRelated.FilesToJobs
{
    public class GetFilesToJobsByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public FileToJobDTO Body { get; set; }
    }
}
