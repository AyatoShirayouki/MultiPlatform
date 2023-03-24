namespace Client.RestComunication.Users
{
    public class UsersRequestBuilder
    {
        private UsersRouter _router = new UsersRouter();

        //Authentication
        public string LoginRequestBuilder(string uri, string email, string password)
        {
            return uri + _router.Users_Login + $"email={email}&password={password}";
        }
        public string SignUpRequestBuilder(string uri)
        {
            return uri + _router.Users_SignUp;
        }
        public string LogoutRequestBuilder(string uri, string userId)
        {
             return uri + _router.Users_Logout + $"userId={userId}";
        }

        //Users
        public string GetUsersByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Users_GetById + $"id={id}";
        }

        public string DeleteUsersByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Users_Delete + $"id={id}";
        }

        public string UpdateUsersRequestBuilder(string uri)
        {
            return uri + _router.Users_Update;
        }

        public string GetAllUsersRequestBuilder(string uri)
        {
            return uri + _router.Users_GetAll;
        }

        //UserEducations
        public string DeleteUserEducationsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.UserEducations_Delete + $"id={id}";
        }
        public string GetAllUserEducationsRequestBuilder(string uri)
        {
            return uri + _router.UserEducations_GetAll;
        }
        public string SaveUserEducationsRequestBuilder(string uri)
        {
            return uri + _router.UserEducations_Save;
        }
        public string GetUserEducationsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.UserEducations_GetById + $"id={id}";
        }

        //Countries
        public string DeleteCountriesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Countries_Delete + $"id={id}";
        }
        public string GetAllCountriesRequestBuilder(string uri)
        {
            return uri + _router.Countries_GetAll;
        }
        public string SaveCountriesRequestBuilder(string uri)
        {
            return uri + _router.Countries_Save;
        }
        public string GetCountriesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Countries_GetById + $"id={id}";
        }

        //Regions
        public string DeleteRegionsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Regions_Delete + $"id={id}";
        }
        public string GetAllRegionsRequestBuilder(string uri)
        {
            return uri + _router.Regions_GetAll;
        }
        public string SaveRegionsRequestBuilder(string uri)
        {
            return uri + _router.Regions_Save;
        }
        public string GetRegionsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Regions_GetById + $"id={id}";
        }
		public string GetRegionsByCountryIdRequestBuilder(string uri, int id)
		{
			return uri + _router.Regions_GetRegionsByCountryId + $"id={id}";
		}

		//Cities
		public string DeleteCitiesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Cities_Delete + $"id={id}";
        }
        public string GetAllCitiesRequestBuilder(string uri)
        {
            return uri + _router.Cities_GetAll;
        }
        public string SaveCitiesRequestBuilder(string uri)
        {
            return uri + _router.Cities_Save;
        }
        public string GetCitiesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Cities_GetById + $"id={id}";
        }
		public string GetCitiesByRegionAndCountryIdRequestBuilder(string uri, int regionId, int countryId)
		{
			return uri + _router.Cities_GetCitiesByRegionAndCountryId + $"regionId={regionId}&countryId={countryId}";
		}

		//Addresses
		public string DeleteAddressesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Addresses_Delete + $"id={id}";
        }
        public string GetAllAddressesRequestBuilder(string uri)
        {
            return uri + _router.Addresses_GetAll;
        }
        public string SaveAddressesRequestBuilder(string uri)
        {
            return uri + _router.Addresses_Save;
        }
        public string GetAddressesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Addresses_GetById + $"id={id}";
        }

        //Degrees
        public string DeleteDegreesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Degrees_Delete + $"id={id}";
        }
        public string GetAllDegreesRequestBuilder(string uri)
        {
            return uri + _router.Degrees_GetAll;
        }
        public string SaveDegreesRequestBuilder(string uri)
        {
            return uri + _router.Degrees_Save;
        }
        public string GetDegreesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Degrees_GetById + $"id={id}";
        }

        //AcademicFields
        public string DeleteAcademicFieldsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.AcademicFields_Delete + $"id={id}";
        }
        public string GetAllAcademicFieldsRequestBuilder(string uri)
        {
            return uri + _router.AcademicFields_GetAll;
        }
        public string SaveAcademicFieldsRequestBuilder(string uri)
        {
            return uri + _router.AcademicFields_Save;
        }
        public string GetAcademicFieldsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.AcademicFields_GetById + $"id={id}";
        }

        //EducationalFacilities
        public string DeleteEducationalFacilitiesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.EducationalFacilities_Delete + $"id={id}";
        }
        public string GetAllEducationalFacilitiesRequestBuilder(string uri)
        {
            return uri + _router.EducationalFacilities_GetAll;
        }
        public string SaveEducationalFacilitiesRequestBuilder(string uri)
        {
            return uri + _router.EducationalFacilities_Save;
        }
        public string GetEducationalFacilitiesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.EducationalFacilities_GetById + $"id={id}";
        }

        //EducationalFacilityTypes
        public string DeleteEducationalFacilityTypesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.EducationalFacilityTypes_Delete + $"id={id}";
        }
        public string GetAllEducationalFacilityTypesRequestBuilder(string uri)
        {
            return uri + _router.EducationalFacilityTypes_GetAll;
        }
        public string SaveEducationalFacilityTypesRequestBuilder(string uri)
        {
            return uri + _router.EducationalFacilityTypes_Save;
        }
        public string GetEducationalFacilityTypesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.EducationalFacilityTypes_GetById + $"id={id}";
        }

        //PricingPlans
        public string DeletePricingPlansByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.PricingPlans_Delete + $"id={id}";
        }
        public string GetAllPricingPlansRequestBuilder(string uri)
        {
            return uri + _router.PricingPlans_GetAll;
        }
        public string SavePricingPlansRequestBuilder(string uri)
        {
            return uri + _router.PricingPlans_Save;
        }
        public string GetPricingPlansByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.PricingPlans_GetById + $"id={id}";
        }

        //PricingPlanFeatures
        public string DeletePricingPlanFeaturesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.PricingPlanFeatures_Delete + $"id={id}";
        }
        public string GetAllPricingPlanFeaturesRequestBuilder(string uri)
        {
            return uri + _router.PricingPlanFeatures_GetAll;
        }
        public string SavePricingPlanFeaturesRequestBuilder(string uri)
        {
            return uri + _router.PricingPlanFeatures_Save;
        }
        public string GetPricingPlanFeaturesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.PricingPlanFeatures_GetById + $"id={id}";
        }

        //UserFiles
        public string DeleteUserFilesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.UserFiles_Delete + $"id={id}";
        }
        public string GetAllUserFilesRequestBuilder(string uri)
        {
            return uri + _router.UserFiles_GetAll;
        }
        public string SaveUserFilesRequestBuilder(string uri)
        {
            return uri + _router.UserFiles_Save;
        }
        public string GetUserFilesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.UserFiles_GetById + $"id={id}";
        }
    }
}
