namespace Administration_Panel.RestComunication.Admins
{
	public class AdminsRouter
	{
        //Admins
        public string Admins_GetAll = "/api/Admins/GetAll";
        public string Admins_GetById = "/api/Admins/GetById?";
        public string Admins_Login = "/api/Admins/Login?";
        public string Admins_SignUp = "/api/Admins/SignUp";
        public string Admins_Update = "/api/Admins/Update";
        public string Admins_Delete = "/api/Admins/Delete?";
        public string Admins_Logout = "/api/Admins/Logout?";
        public string Admins_FillCountriesRegionsAndCitiesActionMSSQL = "/api/Admins/FillCountriesRegionsAndCitiesActionMSSQL?";
        public string Admins_FillCountriesRegionsAndCitiesActionPostgreSQL = "/api/Admins/FillCountriesRegionsAndCitiesActionPostgreSQL?";
        public string Admins_FillCategoriesActionMSSQL = "/api/Admins/FillCategoriesActionMSSQL?";
        public string Admins_FillCategoriesActionPostgreSQL = "/api/Admins/FillCategoriesActionPostgreSQL?";

        //AdminFiles
        public string AdminFiles_GetAll = "/api/AdminFiles/GetAll";
        public string AdminFiles_GetById = "/api/AdminFiles/GetById?";
        public string AdminFiles_Save = "/api/AdminFiles/Save";
        public string AdminFiles_Delete = "/api/AdminFiles/Delete?";
    }
}
