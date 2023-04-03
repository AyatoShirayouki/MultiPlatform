using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Client.RestComunication.Freelance.Responses.Others.SkillsToUsers
{
	public class DeleteSkillsToUsersResponse : BaseResponseMessage
	{
		[JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
		public string Body { get; set; }
	}
}
