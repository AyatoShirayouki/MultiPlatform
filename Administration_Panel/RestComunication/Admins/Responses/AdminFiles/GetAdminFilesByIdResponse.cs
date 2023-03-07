using Admins_ApplicationService.DTOs;
using Base.Messages;
using Freelance_ApplicationService.DTOs.Bookmarks;
using Newtonsoft.Json;

namespace Administration_Panel.RestComunication.Admins.Responses.AdminFiles
{
    public class GetAdminFilesByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public AdminFileDTO Body { get; set; }
    }
}
