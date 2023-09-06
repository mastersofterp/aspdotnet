//=========================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH  (Laboratory Test)     
// CREATION DATE : 23-MAY-2016
// CREATED BY    : MRUNAL SINGH 
//========================================================================== 
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Web.UI.HtmlControls;

public partial class Health_LaboratoryTest_EmpLabTest : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    HealthTransactionController objHelTranTransaction = new HealthTransactionController();
    LabMaster objLab = new LabMaster();
    LabController objLabController = new LabController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }                   
                    ViewState["action"] = "add";
                    txtPatientName.Text = objCommon.LookUp("PAYROLL_EMPMAS", "PFILENO +' - '+ ISNULL(TITLE,'')+ ' ' +ISNULL(FNAME,'')+ ' ' +ISNULL(MNAME,'')+ ' ' +ISNULL(LNAME,'') AS NAME", "IDNO=" + Convert.ToInt32(Session["idno"]));
                    objCommon.FillDropDownList(ddlVisitDate, "HEALTH_PATIENT_DETAILS", "OPDID", "CONVERT(VARCHAR(30),OPDTIME, 113) AS OPDDT", "PATIENT_CODE IN ('E','D') AND TEST_GIVEN=1 AND PID=" + Convert.ToInt32(Session["idno"]), "OPDID DESC");
                    BindlistView(Convert.ToInt32(ddlVisitDate.SelectedValue));
                }
            }           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_EmpLabTest.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillEmployeeInfo(int idno)
    {
        try
        {
            
            //objCommon.FillDropDownList(ddlVisitDate, "HEALTH_PATIENT_DETAILS", "OPDID", "CONVERT(VARCHAR(30),OPDTIME, 113) AS OPDDT", "PATIENT_CODE IN ('E','D') AND TEST_GIVEN=1 AND PID=" + idno, "OPDID DESC");

           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_EmpLabTest.FillEmployeeInfo -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }  

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }


   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlVisitDate.SelectedIndex = 0;       
        lblRefBy.Text = string.Empty;
        ViewState["OBSERNO"] = null;
        ViewState["action"] = "add";       
    }




    protected void ddlVisitDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string DoctorName = string.Empty;
            if (ddlVisitDate.SelectedIndex > 0)
            {              
               
                DoctorName = objCommon.LookUp("HEALTH_PATIENT_DETAILS P INNER JOIN HEALTH_DOCTORMASTER D ON (P.DRID = D.DRID)", "D.DRNAME", "P.OPDID=" + ddlVisitDate.SelectedValue);
                lblRefBy.Text = DoctorName;
                DataSet ds = objCommon.FillDropDown("HEALTH_PATIENT_DETAILS", "PATIENT_CODE", "DEPENDENT_ID", "OPDID=" + Convert.ToInt32(ddlVisitDate.SelectedValue), "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int DependentID = Convert.ToInt32(ds.Tables[0].Rows[0]["DEPENDENT_ID"].ToString());
                    if (DependentID != 0)
                    {
                        DataSet dsD = objCommon.FillDropDown("PAYROLL_SB_FAMILYINFO", "FNNO", "RELATION + ' - ' + MEMNAME AS NAME", "FNNO=" + DependentID, "");
                        if (dsD.Tables[0].Rows.Count > 0)
                        {
                            lblDeptName.Text = dsD.Tables[0].Rows[0]["NAME"].ToString();
                            trDependent.Visible = true;
                        }
                        else
                        {
                            trDependent.Visible = false;
                        }
                    }
                    else
                    {
                        trDependent.Visible = false;
                    }
                }

                ddlVisitDate.Focus();
                BindlistView(Convert.ToInt32(ddlVisitDate.SelectedValue));                
            }
            else
            {
                lblRefBy.Text = string.Empty;
                DoctorName = string.Empty;               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_LaboratoryTest_EmpLabTest.ddlVisitDate_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   

    private void BindlistView(int OPDID)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("HEALTH_TEST_OBSERVATION O INNER JOIN HEALTH_TEST_TITLE T ON (O.TITLENO = T.TITLENO) INNER JOIN HEALTH_PATIENT_DETAILS P ON (O.OPDID = P.OPDID) INNER JOIN HEALTH_DOCTORMASTER D ON (P.DRID = D.DRID)", "O.OBSERNO, O.TEST_SAMPLE_DT, O.TEST_DUE_DT, O.COMMON_REMARK, O.PATIENT_NAME", "T.TITLE, D.DRNAME, P.OPDDATE", "O.PATIENT_ID=" + Convert.ToInt32(Session["idno"]) + "AND (P.OPDID=" + OPDID + " OR " + OPDID + "= 0)", "O.OBSERNO DESC");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvObservation.Visible = true;
                lvObservation.DataSource = ds;
                lvObservation.DataBind();
            }
            else
            {
                lvObservation.DataSource = null;
                lvObservation.DataBind();
                lvObservation.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_EmpLabTest.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
 
   

    

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnPrint = sender as Button;
            ViewState["OBSERNO"] = int.Parse(btnPrint.CommandArgument);
            ShowReport("TestObservation", "rptTestObservationReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_EmpLabTest.btnPrint_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_OBSERNO=" + Convert.ToInt32(ViewState["OBSERNO"]);
            ScriptManager.RegisterClientScriptBlock(updActivity, updActivity.GetType(), "Window", "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_EmpLabTest.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}