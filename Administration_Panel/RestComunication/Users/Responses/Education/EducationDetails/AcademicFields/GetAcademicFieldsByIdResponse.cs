using Base.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Users_ApplicationService.DTOs.Education;
using Users_ApplicationService.DTOs.Education.EducationDetails;

namespace Client.RestComunication.Users.Responses.Education.EducationDetails.AcademicFields
{
    public class GetAcademicFieldsByIdResponse : BaseResponseMessage
    {
        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public AcademicFieldDTO Body { get; set; }
    }
}
