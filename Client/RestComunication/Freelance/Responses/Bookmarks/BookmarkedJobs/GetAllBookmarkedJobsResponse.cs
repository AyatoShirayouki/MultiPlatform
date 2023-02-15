using Base.Messages;
using Freelance_ApplicationService.DTOs.Bookmarks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.Bookmarks.BookmarkedJobs
{
    public class GetAllBookmarkedJobsResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<BookmarkedJobDTO> Body { get; set; }
    }
}
