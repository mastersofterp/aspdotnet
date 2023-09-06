//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : BONAFIDE CERTIFICATE CONTROLLER                                      
// CREATION DATE : 18-Sept-2009                                                         
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class BonafideController
    {
        string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddBonafideCertificate(Bonafide objBonafide, int crno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_IDNO", objBonafide.Idno);
                objParams[1] = new SqlParameter("@P_ROOM_NO", objBonafide.Room_No);
                objParams[2] = new SqlParameter("@P_ADMBATCH", objBonafide.Adm_Batch);
                objParams[3] = new SqlParameter("@P_ISSUE_DATE", objBonafide.Issue_Date);
                objParams[4] = new SqlParameter("@P_ISSUER_NAME ", objBonafide.Issuer_Name);
                objParams[5] = new SqlParameter("@P_HOSTEL_SESSION_NO", objBonafide.Session_No);
                //objParams[6] = new SqlParameter("@P_CERTIFICATE_NO",objBonafide.Certificate_No);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE ", objBonafide.College_Code);
                objParams[7] = new SqlParameter("@P_CRNO", crno);
                objParams[8] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[9] = new SqlParameter("@P_BCID", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_BONAFIDE_CERTIFICATE_STUDENT_INSERT", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BonafideController.AddBonafideCertificate-> " + ex.ToString());
            }

            return retStatus;
        }
           //ADD HOSTEL RESIDENCE ISSUE DATE
        public int AddResidenceCertificate(Bonafide objBonafide,int crno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_IDNO", objBonafide.Idno);
                objParams[1] = new SqlParameter("@P_ROOM_NO", objBonafide.Room_No);
                objParams[2] = new SqlParameter("@P_ADMBATCH", objBonafide.Adm_Batch);
                objParams[3] = new SqlParameter("@P_ISSUE_DATE", objBonafide.Issue_Date);
                objParams[4] = new SqlParameter("@P_ISSUER_NAME ", objBonafide.Issuer_Name);
                objParams[5] = new SqlParameter("@P_HOSTEL_SESSION_NO", objBonafide.Session_No);
                //objParams[6] = new SqlParameter("@P_CERTIFICATE_NO",objBonafide.Certificate_No);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE ", objBonafide.College_Code);
                objParams[7] = new SqlParameter("@P_CRNO", crno);
                objParams[8] = new SqlParameter("@P_RCID", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_RESIDENCE_CERTIFICATE_STUDENT_INSERT", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BonafideController.AddResidenceCertificate-> " + ex.ToString());
            }

            return retStatus;
        }

        // add method 22/06/2022
        public DataSet GetStudentSearchForHostelBonafideCert(int hostelSessionNo, int admBatchNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_HOSTEL_SESSION_NO", hostelSessionNo);
                objParams[1] = new SqlParameter("@P_HOSTEL_NO", admBatchNo);
                objParams[2] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                ds = objSqlHelper.ExecuteDataSetSP("PKG_HOSTEL_STUDENT_SEARCH_BONAFIDE_CERTIFICATE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentSearchForHostelBonafideCert-> " + ex.ToString());
            }
            return ds;
        }

    }
}
