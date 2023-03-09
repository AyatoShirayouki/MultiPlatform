using Base.Messages;
using Newtonsoft.Json;

namespace Administration_Panel.RestComunication.Admins.Responses.ScriptExecutors
{
    public class FillPricingPlansAndFeaturesMSSQLResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public string Body { get; set; }
    }
}
