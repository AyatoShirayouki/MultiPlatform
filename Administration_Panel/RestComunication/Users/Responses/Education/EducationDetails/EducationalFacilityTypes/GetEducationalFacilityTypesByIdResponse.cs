using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Users_ApplicationService.DTOs.Education.EducationDetails;

namespace Client.RestComunication.Users.Responses.Education.EducationDetails.EducationalFacilityTypes
{
    public class GetEducationalFacilityTypesByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public EducationalFacilityTypeDTO Body { get; set; }
    }
}
