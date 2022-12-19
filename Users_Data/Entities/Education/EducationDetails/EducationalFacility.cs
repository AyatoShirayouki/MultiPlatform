using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities.AddressInfo;

namespace Users_Data.Entities.Education.EducationDetails
{
    public class EducationalFacility : BaseEntity
    {
        [MaxLength(255)]
        [Required]
        public string? Name { get; set; }

        [MaxLength(255)]
        [Required]
        public string? Website { get; set; }

        [Required]
        public int EducationalFacilityTypeId { get; set; }

        [ForeignKey("EducationalFacilityTypeId")]
        public EducationalFacilityType? ParentEducationalFacilityType { get; set; }

        [Required]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country? ParentCountry { get; set; }
    }
}
