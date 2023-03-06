using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Users_ApplicationService.DTOs;

namespace Client.RestComunication.Users.Responses.Others.PricingPlans
{
    public class GetPricingPlansByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public PricingPlanDTO Body { get; set; }
    }
}