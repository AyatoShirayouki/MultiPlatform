namespace Administration_Panel.RestComunication.Admins
{
	public class AdminsRequestBuilder
	{
        private readonly AdminsRouter _router = new AdminsRouter();

        //Admin
        public string GetAdminsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Admins_GetById + $"id={id}";
        }
        public string UpdateAdminsRequestBuilder(string uri)
        {
            return uri + _router.Admins_Update;
        }
        public string GetAllAdminsRequestBuilder(string uri)
        {
            return uri + _router.Admins_GetAll;
        }
        public string DeleteAdminsByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.Admins_Delete + $"id={id}";
        }

        //Script Executors
        public string FillCountriesRegionsAndCitiesActionMSSQLRequestBuilder(string uri)
        {
            return uri + _router.Admins_FillCountriesRegionsAndCitiesActionMSSQL;
        }
        public string FillCountriesRegionsAndCitiesActionPostgreSQLRequestBuilder(string uri)
        {
            return uri + _router.Admins_FillCountriesRegionsAndCitiesActionPostgreSQL;
        }
        public string FillCategoriesActionMSSQLRequestBuilder(string uri)
        {
            return uri + _router.Admins_FillCategoriesActionMSSQL;
        }
        public string FillCategoriesActionPostgreSQLRequestBuilder(string uri)
        {
            return uri + _router.Admins_FillCategoriesActionPostgreSQL;
        }
        public string FillPricingPlansAndFeaturesActionMSSQLRequestBuilder(string uri)
        {
            return uri + _router.Admins_FillPricingPlansAndFeaturesActionMSSQL;
        }
        public string FillPricingPlansAndFeaturesActionPostgreSQLRequestBuilder(string uri)
        {
            return uri + _router.Admins_FillPricingPlansAndFeaturesActionPostgreSQL;
        }

        //AdminFiles
        public string DeleteAdminFilesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.AdminFiles_Delete + $"id={id}";
        }
        public string GetAllAdminFilesRequestBuilder(string uri)
        {
            return uri + _router.AdminFiles_GetAll;
        }
        public string SaveAdminFilesRequestBuilder(string uri)
        {
            return uri + _router.AdminFiles_Save;
        }
        public string GetAdminFilesByIdRequestBuilder(string uri, int id)
        {
            return uri + _router.AdminFiles_GetById + $"id={id}";
        }

        //Authentication
        public string LoginRequestBuilder(string uri, string email, string password)
        {
            return uri + _router.Admins_Login + $"email={email}&password={password}";
        }
        public string SignUpRequestBuilder(string uri)
        {
            return uri + _router.Admins_SignUp;
        }
    }
}
