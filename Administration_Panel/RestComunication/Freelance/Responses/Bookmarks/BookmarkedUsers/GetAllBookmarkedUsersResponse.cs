using Base.Messages;
using Freelance_ApplicationService.DTOs.Bookmarks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.Bookmarks.BookmarkedUsers
{
    public class GetAllBookmarkedUsersResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<BookmarkedUserDTO> Body { get; set; }
    }
}
