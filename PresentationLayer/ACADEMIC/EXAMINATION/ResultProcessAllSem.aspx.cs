using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;

using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class ACADEMIC_EXAMINATION_ResultProcessAllSem : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                
               
            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ResultProcess.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ResultProcess.aspx");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtRegNo.Text = string.Empty;
        //Response.Redirect(Request.Url.ToString());
        divStudInfo.Visible = false;
        flesetStuid.Visible = false;
        btnProcessResult.Visible = false;
        
    }


    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

   

    protected void btnProcessResult_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtRegNo.Text.Trim() != string.Empty)
            {

                int studentId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtRegNo.Text.Trim() + "'"));
                if (studentId > 0)
                {

                    MarksEntryController objProcessResult = new MarksEntryController();

                    CustomStatus cs = (CustomStatus)objProcessResult.ProcessResultAllSem(studentId);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ShowMessage("Result Processed Successfully!!!");

                    }
                    else
                        ShowMessage("Error in Processing Result!");
                }
            }
            else

                ShowMessage("Please enter Registration number");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_RESULTPROCESSINGALLSEM.btnProcessResult_Click() -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   

    private void DisplayInformation(int studentId)
    {
        try
        {
            ResultProcessing resultController = new ResultProcessing();

            /// Display student's personal and academic data in 
            /// student information section
            DataSet ds = resultController.GetStudentInfoByIdResultProcess(studentId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                // show student information
                this.PopulateStudentInfoSection(dr);
            }
            else
            {
                ShowMessage("Unable to retrieve student's record.");
                return;
            }

           
            //divStudentSearch.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Refund.DisplayStudentInfo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void PopulateStudentInfoSection(DataRow dr)
    {
        try
        {
            // Bind data with labels
            lblStudName.Text = dr["STUDNAME"].ToString();
            lblSex.Text = dr["SEX"].ToString();
            lblRegNo.Text = dr["REGNO"].ToString();
         
            lblDegree.Text = dr["DEGREENAME"].ToString();
            lblBranch.Text = dr["BRANCH_NAME"].ToString();
            lblYear.Text = dr["YEARNAME"].ToString();
            lblSemester.Text = dr["SEMESTERNAME"].ToString();
            lblBatch.Text = dr["BATCHNAME"].ToString();

            /// Save important data in view state to be used 
            /// in further transactions for this student 
            /// and also while saving the refund record.
            //ViewState["StudentId"] = dr["IDNO"].ToString();
            //ViewState["DegreeNo"] = dr["DEGREENO"].ToString();
            //ViewState["BranchNo"] = dr["BRANCHNO"].ToString();
            //ViewState["YearNo"] = dr["YEAR"].ToString();
            //ViewState["SemesterNo"] = dr["SEMESTERNO"].ToString();
            //ViewState["AdmBatchNo"] = dr["ADMBATCH"].ToString();
            //ViewState["PaymentTypeNo"] = dr["PTYPE"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_EXAMINATION.PopulateStudentInfoSection() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtRegNo.Text.Trim() != string.Empty)
            {
                divStudInfo.Visible = true;
                flesetStuid.Visible = true;
                btnProcessResult.Visible = true;
                int studentId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtRegNo.Text.Trim() + "'"));
                if (studentId > 0)
                {
                    this.DisplayInformation(studentId);
                }
                else
                    ShowMessage("No student found having Registration number.: " + txtRegNo.Text.Trim());
            }
            else
                ShowMessage("Please enter Registration number");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Examinatio_ResultProcessAllSem.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
