namespace Administration_Panel.RestComunication.Admins
{
	public class AdminsRequestBuilder
	{
        private readonly AdminsRouter _router = new AdminsRouter();

        //Admin
        public string GetAdminsByIdRequestHelper(string uri, int id)
        {
            return uri + _router.Admins_GetById + $"id={id}";
        }
        public string UpdateAdminsRequestHelper(string uri)
        {
            return uri + _router.Admins_Update;
        }
        public string GetAllAdminsRequestHelper(string uri)
        {
            return uri + _router.Admins_GetAll;
        }
        public string DeleteAdminsByIdRequestHelper(string uri, int id)
        {
            return uri + _router.Admins_Delete + $"id={id}";
        }
        public string FillCountriesRegionsAndCitiesActionMSSQLRequestHelper(string uri)
        {
            return uri + _router.Admins_FillCountriesRegionsAndCitiesActionMSSQL;
        }
        public string FillCountriesRegionsAndCitiesActionPostgreSQLRequestHelper(string uri)
        {
            return uri + _router.Admins_FillCountriesRegionsAndCitiesActionPostgreSQL;
        }
        public string FillCategoriesActionMSSQLRequestHelper(string uri)
        {
            return uri + _router.Admins_FillCategoriesActionMSSQL;
        }
        public string FillCategoriesActionPostgreSQLRequestHelper(string uri)
        {
            return uri + _router.Admins_FillCategoriesActionPostgreSQL;
        }

        //AdminFiles
        public string DeleteAdminFilesByIdRequestHelper(string uri, int id)
        {
            return uri + _router.AdminFiles_Delete + $"id={id}";
        }
        public string GetAllAdminFilesRequestHelper(string uri)
        {
            return uri + _router.AdminFiles_GetAll;
        }
        public string SaveAdminFilesRequestHelper(string uri)
        {
            return uri + _router.AdminFiles_Save;
        }
        public string GetAdminFilesByIdRequestHelper(string uri, int id)
        {
            return uri + _router.AdminFiles_GetById + $"id={id}";
        }
    }
}
