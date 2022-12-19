using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities.Education.EducationDetails;

namespace Users_Data.Entities.Education
{
    public class UserEducation : BaseEntity
    {
        public string? Speacialty { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? ParentUser { get; set; }

        [Required]
        public int DegreeId { get; set; }

        [ForeignKey("DegreeId")]
        public Degree? ParentDegree { get; set; }

        [Required]
        public int AcademicFieldId { get; set; }

        [ForeignKey("AcademicFieldId")]
        public AcademicField? ParentAcademicField { get; set; }

        [Required]
        public int EducationalFacilityId { get; set; }

        [ForeignKey("EducationalFacilityId")]
        public EducationalFacility? ParentEducationalFacility { get; set; }
    }
}
