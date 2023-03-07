using Admins_ApplicationService.DTOs;
using Base.Messages;
using Newtonsoft.Json;

namespace Administration_Panel.RestComunication.Admins.Responses.Authentication
{
    public class LoginAdminsResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public AdminDTO Body { get; set; }
    }
}
