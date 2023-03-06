using Base.Messages;
using Freelance_ApplicationService.DTOs.Bookmarks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.Bookmarks.BookmarkedTasks
{
    public class GetAllBookmarkedTasksResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<BookmarkedTaskDTO> Body { get; set; }
    }
}
