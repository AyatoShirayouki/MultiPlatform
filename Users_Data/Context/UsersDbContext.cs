using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities;
using Users_Data.Entities.AddressInfo;
using Users_Data.Entities.Education;
using Users_Data.Entities.Education.EducationDetails;
using Utils.Global;

namespace Users_Data.Context
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }
        public DbSet<RefreshUserToken> RefreshUserTokens { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AcademicField> AcademicFields { get; set; }
        public DbSet<EducationalFacilityType> EducationalFacilityTypes { get; set; }
        public DbSet<EducationalFacility> EducationalFacilities { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<UserEducation> UserEducations { get; set; }
        public DbSet<PricingPlan> PricingPlans { get; set; }
        public DbSet<PricingPlanFeature> PricingPlanFeatures { get; set; }

        public UsersDbContext()
        {
            Users = this.Set<User>();
            UserFiles = this.Set<UserFile>();
            RefreshUserTokens = this.Set<RefreshUserToken>();
            Countries = this.Set<Country>();
            Regions = this.Set<Region>();
            Cities = this.Set<City>();
            Addresses = this.Set<Address>();
            AcademicFields = this.Set<AcademicField>();
            EducationalFacilityTypes = this.Set<EducationalFacilityType>();
            EducationalFacilities = this.Set<EducationalFacility>();
            Degrees = this.Set<Degree>();
            UserEducations = this.Set<UserEducation>();
            PricingPlans = this.Set<PricingPlan>();
            PricingPlanFeatures = this.Set<PricingPlanFeature>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GlobalVariables.Users_DB_CN);
        }
    }
}
