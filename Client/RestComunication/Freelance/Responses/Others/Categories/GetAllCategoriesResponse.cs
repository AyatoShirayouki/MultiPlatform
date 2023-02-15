using Base.Messages;
using Freelance_ApplicationService.DTOs.Others;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.Others.Categories
{
    public class GetAllCategoriesResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public List<CategoryDTO> Body { get; set; }
    }
}
