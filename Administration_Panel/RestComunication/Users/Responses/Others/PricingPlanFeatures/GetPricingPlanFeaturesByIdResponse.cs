using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Users_ApplicationService.DTOs;
using Users_ApplicationService.DTOs.Education;

namespace Client.RestComunication.Users.Responses.Others.PricingPlanFeatures
{
    public class GetPricingPlanFeaturesByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public PricingPlanFeatureDTO Body { get; set; }
    }
}
