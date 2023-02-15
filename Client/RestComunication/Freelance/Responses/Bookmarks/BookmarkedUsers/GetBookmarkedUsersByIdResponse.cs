using Base.Messages;
using Freelance_ApplicationService.DTOs.Bookmarks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.Bookmarks.BookmarkedUsers
{
    public class GetBookmarkedUsersByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public BookmarkedUserDTO Body { get; set; }
    }
}
