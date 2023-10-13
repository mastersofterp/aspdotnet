using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;

namespace BusinessLogicLayer.BusinessLogic
{
    /// <summary>
    /// This Payroll_Report_COntroller used for EMployee PaySlip.
    /// </summary>
    public class Payroll_Report_Controller
    {
        /// <summary>
        /// ConnectionString
        /// </summary>
        string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        //Create a Dataset For Employee Payslip

        public DataSet RetrieveEmployeePayslipDetails(string mon_year, int staff_member, int idno, int collegeNo, int EMPTYPENO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_MON_YEAR", mon_year);
                objParams[1] = new SqlParameter("@P_STAFF_NO", staff_member);
                objParams[2] = new SqlParameter("@P_IDNO", idno);
                objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                objParams[4] = new SqlParameter("@P_EMPTYPENO", EMPTYPENO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_REPORT_PAYSLIP", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Payroll_Report_Controller.RetrieveEmployeePayslipDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet RetrieveEmployeePayscaleDetails()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_ALL_SCALE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Payroll_Report_Controller.RetrieveEmployeePayscaleDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet RetrieveAnnualSalaryReportDetails(string from_date, string to_date, int idno, int staff_no, int collleg_no)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_FROM_DATE", from_date);
                objParams[1] = new SqlParameter("@P_TO_DATE", to_date);
                objParams[2] = new SqlParameter("@P_IDNO", idno);
                objParams[3] = new SqlParameter("@P_STAFF_NO", staff_no);
                objParams[4] = new SqlParameter("@P_COLLEGE_NO", collleg_no);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_REPORT_SALARY_ANNUAL_SUMMARY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Payroll_Report_Controller.RetrieveAnnualSalaryReportDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet RetrieveParticularColumnPayhead(string from_Date, string to_Date, int idno, int staff_No, string payHead, string month)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_FROM_DATE", from_Date);
                objParams[1] = new SqlParameter("@P_TO_DATE", to_Date);
                objParams[2] = new SqlParameter("@P_IDNO", idno);
                objParams[3] = new SqlParameter("@P_STAFFNO", staff_No);
                objParams[4] = new SqlParameter("@P_PAYHEAD", payHead);
                objParams[5] = new SqlParameter("@P_MONTH", month);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_REPORT_SELECTED_PAYHEAD", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Payroll_Report_Controller.RetrieveParticularColumnPayhead-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet RetrieveAnnual_Detail_Report(string From_Date, string To_Date, int Idno, int Staff_no, string Month)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_FROM_DATE", From_Date);
                objParams[1] = new SqlParameter("@P_TO_DATE", To_Date);
                objParams[2] = new SqlParameter("@P_IDNO", Idno);
                objParams[3] = new SqlParameter("@P_STAFF_NO", Staff_no);
                objParams[4] = new SqlParameter("@P_MONTH", Month);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_REPORT_SALARY_ANNUAL_DETAIL", objParams);
            }
            catch (Exception ex)
            {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Payroll_Report_Controller.RetrieveAnnual_Detail_Report-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet RetrieveLogfileReport(string monyear, int staffno,int process)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_MONYEAR", monyear);
                objParams[1] = new SqlParameter("@P_STAFFNO", staffno);
                objParams[2] = new SqlParameter("@P_PROCESS", process);
                ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GETALL_SAL_LOGFILE", objParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Payroll_Report_Controller.RetrieveLogfileReport-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet RettieveAnnualSalaryDetailReport(string from_date, string To_Date, int Staff_No)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams[0] = new SqlParameter("@P_FROM_DATE", from_date);
                objParams[1] = new SqlParameter("@P_TO_DATE", To_Date);
                objParams[2] = new SqlParameter("@P_STAFF_NO", Staff_No);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_REPORT_DROPDOWN_FILL_MONTH", objParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Payroll_Report_Controller.RettieveAnnualSalaryDEtailReport-> " + ex.ToString());

            }
            return ds;
            
        }




        //Sachin Ghagre 24 June 2017
        public DataSet GetOtherReportExcel(string selectedItem, string MonthYear, int Staff_No,string ShowInReport,string Head1,int College_No)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@P_PAYHEADS", selectedItem);
                objParams[1] = new SqlParameter("@P_MONYEAR", MonthYear);
                objParams[2] = new SqlParameter("@P_COLLEGE_NO", College_No);
                objParams[3] = new SqlParameter("@P_STAFFNO", Staff_No);
                objParams[4] = new SqlParameter("@P_SHOWINREPORT", ShowInReport);
                objParams[5] = new SqlParameter("@P_HEAD1", Head1);

                ds = objSQLHelper.ExecuteDataSetSP("[dbo].[PKG_PAY_OTHER_PAYHEAD_EXCEL]", objParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Payroll_Report_Controller.RettieveAnnualSalaryDEtailReport-> " + ex.ToString());

            }
            return ds;

        }

        //Sachin Ghagre 26 June 2017

        public DataSet GetOther_MultipleHeas_ReportExcel(string selectedItem, string MonthYear, int Staff_No, string ShowInReport, string Head1,string Head2, int College_No)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@P_PAYHEADS", selectedItem);
                objParams[1] = new SqlParameter("@P_MONYEAR", MonthYear);
                objParams[2] = new SqlParameter("@P_COLLEGE_NO", College_No);
                objParams[3] = new SqlParameter("@P_STAFFNO", Staff_No);
                objParams[4] = new SqlParameter("@P_SHOWINREPORT", ShowInReport);
                objParams[5] = new SqlParameter("@P_HEAD1", Head1);
                objParams[6] = new SqlParameter("@P_HEAD2", Head2);

                ds = objSQLHelper.ExecuteDataSetSP("[dbo].[PKG_PAY_OTHER_MULTIPLE_FEILD_EXCEL]", objParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Payroll_Report_Controller.RettieveAnnualSalaryDEtailReport-> " + ex.ToString());

            }
            return ds;

        }

        //Rohit Maske 04/10/2018

        public DataSet GetSalaryRegisterExcel(string monthYear, int StaffNo, int CollegeNo, string collCode, int idno, string reportingHead)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_MON_YEAR", monthYear);
                objParams[1] = new SqlParameter("@P_STAFF_NO", StaffNo);
                objParams[2] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
                objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collCode);
                objParams[4] = new SqlParameter("@P_IDNO", idno);//
                objParams[5] = new SqlParameter("@P_REPORTHEADING", reportingHead);
                ds = objHelper.ExecuteDataSetSP("PKG_PAYROLL_REPORT_PAYSLIP_UNICODE_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetSalaryRegister-> " + ex.ToString());

            }
            return ds;
        }

        //Rohit Maske 04/10/2018

        public DataSet GetSalaryRegisterExcel15Per(string monthYear, int StaffNo, int CollegeNo, string collCode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_TABNAME", monthYear);
                objParams[1] = new SqlParameter("@P_STAFF_NO", StaffNo);
                objParams[2] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
                objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collCode);

                ds = objHelper.ExecuteDataSetSP("PKG_PAY_NON_PLAN_REPORT_EXCEL_15PER", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetSalaryRegister-> " + ex.ToString());

            }
            return ds;
        }


        // Sachin 20 FEB 2018
        //for export to excel abstract Salary Report 

        public DataSet GetAtstractRegisterExcel(string monthYear, string StaffNo,int EmpTypeNo,int CollegeNo,string collCode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_MON_YEAR", monthYear);
                objParams[1] = new SqlParameter("@P_STAFF_NO", StaffNo);
                objParams[2] = new SqlParameter("@P_EMPTYPENO", EmpTypeNo);
                objParams[3] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
                objParams[4] = new SqlParameter("@P_COLLEGE_CODE", collCode);

                ds = objHelper.ExecuteDataSetSP("PKG_PAYROLL_REPORT_PAYSLIP_NEW", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetAtstractRegister-> " + ex.ToString());

            }
            return ds;
        }

        public DataSet RetrieveEmployeeAbstractSalaryMultipleStaff(int collegecode, string mon_year, string staff_member, int idno, int collegeno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                
                objParams[0] = new SqlParameter("@P_MON_YEAR", mon_year);
                objParams[1] = new SqlParameter("@P_STAFF_NO", staff_member);
                objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                objParams[4] = new SqlParameter("@P_IDNO", idno);
                
                ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_REPORT_PAYSLIP_ABSTRACT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Payroll_Report_Controller.RetrieveEmployeePayslipDetails-> " + ex.ToString());
            }
            return ds;
        }

    }
}