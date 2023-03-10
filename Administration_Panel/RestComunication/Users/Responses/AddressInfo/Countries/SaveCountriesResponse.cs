using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Users.Responses.AddressInfo.Countries
{
    public class SaveCountriesResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public string Body { get; set; }
    }
}
