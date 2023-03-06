using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Users_ApplicationService.DTOs.AddressInfo;

namespace Client.RestComunication.Users.Responses.AddressInfo.Cities
{
    public class GetCitiesByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public CityDTO Body { get; set; }
    }
}
