using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Users_ApplicationService.DTOs.Education;
using Users_ApplicationService.DTOs.Education.EducationDetails;

namespace Client.RestComunication.Users.Responses.Education.UserEducations
{
    public class GetAllUserEducationsResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<UserEducationDTO> Body { get; set; }
    }
}
