using Admins_ApplicationService.DTOs;
using Base.Messages;
using Newtonsoft.Json;

namespace Administration_Panel.RestComunication.Admins.Responses.Admins
{
    public class GetAllAdminsResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<AdminDTO> Body { get; set; }
    }
}
