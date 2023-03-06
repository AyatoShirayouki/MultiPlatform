using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Users_ApplicationService.DTOs.AddressInfo;
using Users_ApplicationService.DTOs.Education;

namespace Client.RestComunication.Users.Responses.Education.Degrees
{
    public class GetAllDegreesResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<DegreeDTO> Body { get; set; }
    }
}
