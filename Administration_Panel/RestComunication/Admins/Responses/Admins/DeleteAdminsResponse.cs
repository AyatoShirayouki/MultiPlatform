using Base.Messages;
using Newtonsoft.Json;

namespace Administration_Panel.RestComunication.Admins.Responses.Admins
{
    public class DeleteAdminsResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public string Body { get; set; }
    }
}
