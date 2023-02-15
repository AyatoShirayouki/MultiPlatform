using Base.Messages;
using Freelance_ApplicationService.DTOs.Others;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.Others.FreelanceFiles
{
    public class GetAllFreelanceFilesResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<FreelanceFileDTO> Body { get; set; }
    }
}
