//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Club Faculty Student Event Marking Controller                                            
// CREATION DATE : 30-SEP-2022
// CREATED BY    : NIKHIL SHENDE                          
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace BusinessLogicLayer.BusinessLogic.Academic.StudentAchievement
{
    public class ClubFacultyStudentEventMarkingController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int InsertStudentActivityDetails_old(int Idno, int Regno, int EVENT_ID, int Create_Event_id, int UA_NO, int Created_By_Ua_No, int Club_No, string Event_Title)//, 
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_IDNO", Idno),
                            new SqlParameter("@P_REGNO",Regno),
                            new SqlParameter("@P_EVENT_ID", EVENT_ID),
                            new SqlParameter("@P_CREATE_EVENT_ID",Create_Event_id),
                            new SqlParameter("@P_UA_NO",UA_NO),
                            new SqlParameter("@P_CREATED_BY",Created_By_Ua_No),       
                            new SqlParameter("@P_CLUB_NO",Club_No),
                            new SqlParameter("@P_EVENT_TITLE",Event_Title),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_STUDENT_REGISTERED_DETAILS", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)

                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertCreateEventData-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet ActivityStudnetEvenMarkingReport()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACTIVITY_STUDENT_EVENT_MARKING_REPORT", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityOrganisedListView-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet RgisteredStudentListforFacluty(int Clubno, string EventTitle)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_CLUBNO", Clubno);
                sqlParams[1] = new SqlParameter("@P_EVENT_TITILE", EventTitle);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_REGISTERED_LIST_FOR_FACULTY", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityOrganisedListView-> " + ex.ToString());
            }
            return ds;
        }

        //Added Below Code for new Changes 
        public int InsertStudentActivityDetails(int Idno, int EVENT_ID, int Create_Event_id, int UA_NO, int Created_By_Ua_No, int Club_No, string Event_Title)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_IDNO", Idno),
                            //new SqlParameter("@P_REGNO",Regno),
                            new SqlParameter("@P_EVENT_ID", EVENT_ID),
                            new SqlParameter("@P_CLUBNO",Club_No),
                            new SqlParameter("@P_CREATE_EVENT_ID",Create_Event_id),
                            new SqlParameter("@P_UA_NO",UA_NO),
                            new SqlParameter("@P_CREATED_BY",Created_By_Ua_No),                                   
                            //new SqlParameter("@P_EVENT_TITLE",Event_Title),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_STUDENT_REGISTERED_DETAILS", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)

                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertStudentActivityDetails-> " + ex.ToString());
            }
            return retStatus;
        }
        public DataSet RgisteredStudentListforFaclutyByFacultyNO(int Clubno, string EventTitle, int FacultyNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@P_CLUBNO", Clubno);
                sqlParams[1] = new SqlParameter("@P_EVENT_TITILE", EventTitle);
                sqlParams[2] = new SqlParameter("@P_FACULTY_NO", FacultyNO);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_REGISTERED_LIST_FOR_FACULTY", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RgisteredStudentListforFaclutyByFacultyNO-> " + ex.ToString());
            }
            return ds;
        }

    }
}
