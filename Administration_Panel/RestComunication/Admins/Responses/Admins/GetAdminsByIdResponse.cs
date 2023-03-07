using Admins_ApplicationService.DTOs;
using Base.Messages;
using Newtonsoft.Json;

namespace Administration_Panel.RestComunication.Admins.Responses.Admins
{
    public class GetAdminsByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public AdminDTO Body { get; set; }
    }
}
