using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Users_ApplicationService.DTOs.AddressInfo;

namespace Client.RestComunication.Users.Responses.AddressInfo.Cities
{
    public class GetAllCitiesResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<CityDTO> Body { get; set; }
    }
}
