using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_ApplicationService.DTOs.Education.EducationDetails
{
    public class EducationalFacilityDTO : BaseEntity
    {
        public string? Name { get; set; }
        public string? Website { get; set; }
        public int EducationalFacilityTypeId { get; set; }
        public int CountryId { get; set; }
    }
}
