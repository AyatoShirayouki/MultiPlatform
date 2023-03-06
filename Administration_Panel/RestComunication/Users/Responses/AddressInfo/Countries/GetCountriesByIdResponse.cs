using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Users_ApplicationService.DTOs.AddressInfo;

namespace Client.RestComunication.Users.Responses.AddressInfo.Countries
{
    public class GetCountriesByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public CountryDTO Body { get; set; }
    }
}
