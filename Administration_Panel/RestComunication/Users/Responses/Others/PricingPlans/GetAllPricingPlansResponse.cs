using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Users_ApplicationService.DTOs;

namespace Client.RestComunication.Users.Responses.Others.PricingPlans
{
    public class GetAllPricingPlansResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<PricingPlanDTO> Body { get; set; }
    }
}
