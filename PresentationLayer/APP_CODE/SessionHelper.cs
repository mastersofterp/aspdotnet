using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SessionHelper
/// </summary>

namespace SessionHelper
{
    public static class Users
    {
        # region Private Constants
        private const string userName = "userName";
        private const string loginId = "loginId";
        private const string error = "error";
        private const string collName = "coll_name";
        private const string userNo = "userNo";
        private const string userType = "userType";
        private const string teacherNo = "teacherNo";
        public const string connectionString = "connectionString";

        # endregion
        //#region Private Static Member Variables
        //    private static HttpContext thisContext;
        //#endregion
        //#region Clears Session
        ///// <summary>
        ///// Clears Session
        ///// </summary>
        //public static void ClearSession()
        //{
        //    HttpContext.Current.Session.Clear();
        //}
        ///// <summary>
        ///// Abandons Session
        ///// </summary>
        //public static void Abandon()
        //{
        //    ClearSession();
        //    HttpContext.Current.Session.Abandon();
        //}
        //#endregion
        #region Users
        /// <summary>
        /// Gets/Sets Session for UserId
        /// </summary>
        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session[userName] == null)
                    return "";
                else
                    return HttpContext.Current.Session[userName].ToString();
            }
        }

        public static int LoginId
        {
            get
            {
                if (HttpContext.Current.Session[loginId] == null)
                    return 0;
                else
                    return Convert.ToInt32( HttpContext.Current.Session[loginId].ToString());
            }
        }
        public static string Error
        {
            get
            {
                if (HttpContext.Current.Session[error] == null)
                    return "";
                else
                    return HttpContext.Current.Session[error].ToString();
            }
        }
        public static string CollName
        {
            get
            {
                if (HttpContext.Current.Session[collName] == null)
                    return "";
                else
                    return HttpContext.Current.Session[collName].ToString();
            }
        }
        public static int UserNo
        {
            get
            {
                if (HttpContext.Current.Session[userNo] == null)
                    return 0;
                else
                    return Convert.ToInt32( HttpContext.Current.Session[userNo].ToString());
            }
        }
        public static int UserType
        {
            get
            {
                if (HttpContext.Current.Session[userType] == null)
                    return 0;
                else
                    return Convert.ToInt32( HttpContext.Current.Session[userType].ToString());
            }
        }
        public static int TeacherNo
        {
            get
            {
                if (HttpContext.Current.Session[teacherNo] == null)
                    return 0;
                else
                    return Convert.ToInt32( HttpContext.Current.Session[teacherNo].ToString());
            }
        }
        #endregion
    }

    public static class Connection
    {
        #region Connection
        public static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            }
        }
        #endregion
    }
}
 