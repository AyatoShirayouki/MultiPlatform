namespace Client.RestComunication.Users
{
    public class UsersRouter
    {
        //Authentication
        public string Users_SignUp = "/api/Users/SignUp";
        public string Users_Login = "/api/Users/Login?";
        public string Users_Logout = "/api/Users/Logout?";

        //Addresses
        public string Addresses_GetAll = "/api/Addresses/GetAll";
        public string Addresses_GetById = "/api/Addresses/GetById?";
        public string Addresses_Save = "/api/Addresses/Save";
        public string Addresses_Delete = "/api/Addresses/Delete?";

        //Countries
        public string Countries_GetAll = "/api/Countries/GetAll";
        public string Countries_GetById = "/api/Countries/GetById?";
        public string Countries_Save = "/api/Countries/Save";
        public string Countries_Delete = "/api/Countries/Delete?";

		//Regions
		public string Regions_GetAll = "/api/Regions/GetAll";
        public string Regions_GetById = "/api/Regions/GetById?";
        public string Regions_Save = "/api/Regions/Save";
        public string Regions_Delete = "/api/Regions/Delete?";
		public string Regions_GetRegionsByCountryId = "/api/Regions/GetRegionsByCountryId?";

		//Cities
		public string Cities_GetAll = "/api/Cities/GetAll";
        public string Cities_GetById = "/api/Cities/GetById?";
        public string Cities_Save = "/api/Cities/Save";
        public string Cities_Delete = "/api/Cities/Delete?";
		public string Cities_GetCitiesByRegionAndCountryId = "/api/Cities/GetCitiesByRegionAndCountryId?";

		//Degrees
		public string Degrees_GetAll = "/api/Degrees/GetAll";
        public string Degrees_GetById = "/api/Degrees/GetById?";
        public string Degrees_Save = "/api/Degrees/Save";
        public string Degrees_Delete = "/api/Degrees/Delete?";

        //UserEducations
        public string UserEducations_GetAll = "/api/UserEducations/GetAll";
        public string UserEducations_GetById = "/api/UserEducations/GetById?";
        public string UserEducations_Save = "/api/UserEducations/Save";
        public string UserEducations_Delete = "/api/UserEducations/Delete?";

        //AcademicFields
        public string AcademicFields_GetAll = "/api/AcademicFields/GetAll";
        public string AcademicFields_GetById = "/api/AcademicFields/GetById?";
        public string AcademicFields_Save = "/api/AcademicFields/Save";
        public string AcademicFields_Delete = "/api/AcademicFields/Delete?";

        //EducationalFacilities
        public string EducationalFacilities_GetAll = "/api/EducationalFacilities/GetAll";
        public string EducationalFacilities_GetById = "/api/EducationalFacilities/GetById?";
        public string EducationalFacilities_Save = "/api/EducationalFacilities/Save";
        public string EducationalFacilities_Delete = "/api/EducationalFacilities/Delete?";

        //EducationalFacilityTypes
        public string EducationalFacilityTypes_GetAll = "/api/EducationalFacilityTypes/GetAll";
        public string EducationalFacilityTypes_GetById = "/api/EducationalFacilityTypes/GetById?";
        public string EducationalFacilityTypes_Save = "/api/EducationalFacilityTypes/Save";
        public string EducationalFacilityTypes_Delete = "/api/EducationalFacilityTypes/Delete?";

        //PricingPlanFeatures
        public string PricingPlanFeatures_GetAll = "/api/PricingPlanFeatures/GetAll";
        public string PricingPlanFeatures_GetById = "/api/PricingPlanFeatures/GetById?";
        public string PricingPlanFeatures_Save = "/api/PricingPlanFeatures/Save";
        public string PricingPlanFeatures_Delete = "/api/PricingPlanFeatures/Delete?";

        //PricingPlans
        public string PricingPlans_GetAll = "/api/PricingPlans/GetAll";
        public string PricingPlans_GetById = "/api/PricingPlans/GetById?";
        public string PricingPlans_Save = "/api/PricingPlans/Save";
        public string PricingPlans_Delete = "/api/PricingPlans/Delete?";

        //Users
        public string Users_GetAll = "/api/Users/GetAll";
        public string Users_GetById = "/api/Users/GetById?";
        public string Users_Delete = "/api/Users/Delete?";
        public string Users_Update = "/api/Users/Update";

        //UserFiles
        public string UserFiles_GetAll = "/api/UserFiles/GetAll";
        public string UserFiles_GetById = "/api/UserFiles/GetById?";
        public string UserFiles_Delete = "/api/UserFiles/Delete?";
        public string UserFiles_Save = "/api/UserFiles/Save";
    }
}
