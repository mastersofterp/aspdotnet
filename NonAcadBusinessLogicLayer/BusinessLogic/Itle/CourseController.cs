using System;
using System.Data;
using System.Web;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data.SqlClient;

namespace IITMS
{
    namespace NITPRM
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This CourseController is used to control Course table.
            /// </summary>
            public partial class CourseController 
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetCourseByUaNo(int session, int uano, int utype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_UANO", uano);
                        objParams[2] = new SqlParameter("@P_USERTYPE", utype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_COURSES_BYUANO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCourseByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }
             

            }

        }//END: BusinessLayer.BusinessLogic

    }//END: NITPRM  

}//END: IITMS
