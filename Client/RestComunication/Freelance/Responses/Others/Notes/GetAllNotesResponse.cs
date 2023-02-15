using Base.Messages;
using Freelance_ApplicationService.DTOs.Others;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.Others.Notes
{
    public class GetAllNotesResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<NoteDTO> Body { get; set; }
    }
}
