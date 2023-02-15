using Base.Messages;
using Freelance_ApplicationService.DTOs.JobRelated;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.JobRelated.JobApplications
{
    public class GetAllJobApplicationsResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<JobApplicationDTO> Body { get; set; }
    }
}
