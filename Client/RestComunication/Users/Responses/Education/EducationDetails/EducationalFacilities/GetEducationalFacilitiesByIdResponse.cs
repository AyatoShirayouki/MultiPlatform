using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Users_ApplicationService.DTOs.Education.EducationDetails;

namespace Client.RestComunication.Users.Responses.Education.EducationDetails.EducationalFacilities
{
    public class GetEducationalFacilitiesByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public EducationalFacilityDTO Body { get; set; }
    }
}
