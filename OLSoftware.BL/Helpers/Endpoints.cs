namespace OLSoftware.BL.Helpers
{
    public class Endpoints
    {
        public static string URL_BASE { get; set; } = "https://olsoftware-api.azurewebsites.net/";
        public static string URL_BASE_FUNCTION { get; set; } = "https://olsoftware-function.azurewebsites.net/";

        #region Customer
        public static string GET_CUSTOMERS { get; set; } = "api/Customers/GetAll/";
        public static string GET_CUSTOMER { get; set; } = "api/Customers/GetById/";
        public static string POST_CUSTOMER { get; set; } = "api/Customers/";
        public static string PUT_CUSTOMER { get; set; } = "api/Customers/";
        public static string DELETE_CUSTOMER { get; set; } = "api/Customers/";
        #endregion

        #region Projects
        public static string GET_PROJECTS_REPORT { get; set; } = "api/ReportFunction?code=auWFjsj/9UazkoRVHU3RgSquOPrjXZnFCCyrrd1/OW55P9enaj9ojg==";
        public static string GET_PROJECTS { get; set; } = "api/Projects/GetAll/";
        public static string GET_PROJECT { get; set; } = "api/Projects/GetById/";
        public static string POST_PROJECT { get; set; } = "api/Projects/";
        public static string PUT_PROJECT { get; set; } = "api/Projects/";
        public static string DELETE_PROJECT { get; set; } = "api/Projects/";
        #endregion

        #region ProjectState
        public static string GET_PROJECT_STATES { get; set; } = "api/ProjectStates/GetAll/";
        #endregion

        #region Language
        public static string GET_LANGUAGES { get; set; } = "api/Languages/GetAll/";
        #endregion

        #region ProjectLanguage
        public static string GET_PROJECT_LANGUAGES { get; set; } = "api/ProjectLanguages/GetAll/";
        public static string POST_PROJECT_LANGUAGE { get; set; } = "api/ProjectLanguages/";
        #endregion
    }
}
