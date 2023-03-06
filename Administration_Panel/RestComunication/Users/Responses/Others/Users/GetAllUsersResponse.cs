using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Users_ApplicationService.DTOs;

namespace Client.RestComunication.Users.Responses.Others.Users
{
    public class GetAllUsersResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<UserDTO> Body { get; set; }
    }
}
