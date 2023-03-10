using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Users_ApplicationService.DTOs;

namespace Client.RestComunication.Users.Responses.Others.Users
{
    public class GetUsersByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public UserDTO Body { get; set; }
    }
}
