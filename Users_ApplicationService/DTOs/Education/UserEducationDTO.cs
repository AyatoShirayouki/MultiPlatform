using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_ApplicationService.DTOs.Education
{
    public class UserEducationDTO : BaseEntity
    {
        public string? Speacialty { get; set; }
        public int UserId { get; set; }
        public int DegreeId { get; set; }
        public int AcademicFieldId { get; set; }
        public int EducationalFacilityId { get; set; }
    }
}
