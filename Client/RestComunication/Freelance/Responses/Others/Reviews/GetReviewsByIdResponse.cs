using Base.Messages;
using Freelance_ApplicationService.DTOs.Others;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.Others.Reviews
{
    public class GetReviewsByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public ReviewDTO Body { get; set; }
    }
}
