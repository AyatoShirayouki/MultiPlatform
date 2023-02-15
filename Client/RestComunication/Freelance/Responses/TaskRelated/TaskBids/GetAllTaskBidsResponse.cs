using Base.Messages;
using Freelance_ApplicationService.DTOs.TaskRelated;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.TaskRelated.TaskBids
{
    public class GetAllTaskBidsResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<TaskBidDTO> Body { get; set; }
    }
}
