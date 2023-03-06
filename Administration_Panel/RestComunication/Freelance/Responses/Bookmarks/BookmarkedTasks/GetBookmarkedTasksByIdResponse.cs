using Base.Messages;
using Freelance_ApplicationService.DTOs.Bookmarks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.Bookmarks.BookmarkedTasks
{
    public class GetBookmarkedTasksByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public BookmarkedTaskDTO Body { get; set; }
    }
}
