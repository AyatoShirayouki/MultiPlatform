using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Users_ApplicationService.DTOs;

namespace Client.RestComunication.Users.Responses.Others.UserFiles
{
    public class GetAllUserFilesResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<UserFileDTO> Body { get; set; }
    }
}
