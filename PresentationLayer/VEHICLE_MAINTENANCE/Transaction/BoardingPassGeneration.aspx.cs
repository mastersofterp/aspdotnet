//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 22-AUG-2015
// DESCRIPTION   : USE TO GENERATE BORDING PASS OF STUDENTS AND EMPLOYEES
//=========================================================================
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class VEHICLE_MAINTENANCE_Transaction_BoardingPassGeneration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();
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
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                    trDegree.Visible = true;
                    pnlDate.Visible = false;    
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_BoardingPassGeneration.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
   
    private void BindlistView()
    {
        try
        {
            DataSet ds = null;
            Label lblName = (Label)lvList.FindControl("lblFields");
            if (ddlSchool.SelectedIndex > 0 && ddlDept.SelectedIndex > 0)
            {
                ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO) LEFT OUTER JOIN VEHICLE_BOARDING_PASS BP ON (BP.IDNO = E.IDNO) AND BP.STATUS='E'", "E.IDNO, E.TITLE+ ' ' +E.FNAME+' ' +E.MNAME+ ' '+E.LNAME AS NAME", "E.PFILENO AS REGNO, E.TRANSPORT, CASE WHEN BP.STATUS = 'E' THEN 'YES' ELSE 'NO' END AS STATUS ", "P.PSTATUS='Y' AND TRANSPORT=1 AND E.SUBDEPTNO=" + ddlDept.SelectedValue, "E.IDNO");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvList.Visible = true;
                    lvList.DataSource = ds;
                    lvList.DataBind();
                    pnlDate.Visible = true;
                    
                    lblName.Text = "EMPLOYEE CODE";  
                }
            }
            else if (ddlDegree.SelectedIndex > 0 && ddlBranch.SelectedIndex > 0)
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN VEHICLE_BOARDING_PASS BP ON (BP.IDNO = S.IDNO) AND BP.STATUS='S'", "S.IDNO, S.STUDNAME AS NAME", "S.REGNO, S.TRANSPORT, CASE WHEN BP.STATUS = 'S' THEN 'YES' ELSE 'NO' END AS STATUS", "TRANSPORT=1 AND S.DEGREENO=" + ddlDegree.SelectedValue + " AND S.BRANCHNO=" + ddlBranch.SelectedValue, "S.IDNO");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvList.Visible = true;
                    lvList.DataSource = ds;
                    lvList.DataBind();
                    pnlDate.Visible = true;
                  //  Label lblName = (Label)lvList.FindControl("lblFields");
                    lblName.Text = "ENROLLMENT NO.";
                }
            }
            else
            {
                pnlDate.Visible = false;   
            }            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_BoardingPassGeneration.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            lvList.Visible = true;
            BindlistView();
            txtApproved.Text = Session["userfullname"].ToString();
            txtApproved.Attributes.Add("readonly", "readonly");
            txtAllot.Text = System.DateTime.Now.ToString();//.ToString("dd-MM-yyyy");
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_BoardingPassGeneration.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
   
    private void Clear()
    {
        ddlSchool.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        trSchool.Visible = false;
        trDept.Visible = false;
        trDegree.Visible = false;
        trBranch.Visible = false;      
        lvList.DataSource = null;
        lvList.DataBind();
        lvList.Visible = false;
        txtAllot.Text = string.Empty;
        txtApproved.Text = string.Empty;
        txtExpiry.Text = string.Empty;
       // pnlDate.Visible = false;
       
    }

    private void ShowReport(string IdNo, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=BordingPass.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_IDNO=" + IdNo + ",@P_DATE_OF_ALLOTMENT=" + txtAllot.Text.Trim() + ",@P_COLLEGE_CODE=" + Session["colcode"] +",@P_STATUS=" + objVM.STATUS;

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_BoardingPassGeneration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string IdNo = string.Empty;

            foreach (ListViewDataItem dti in lvList.Items)
            {
                CheckBox chkSelect = dti.FindControl("chkAccept") as CheckBox;

                if (chkSelect.Checked)
                {

                    if (IdNo.Equals(string.Empty))
                    {
                        IdNo = chkSelect.ToolTip;
                    }
                    else
                    {
                        IdNo = IdNo + "$" + chkSelect.ToolTip;
                    }
                }
            }
            if (IdNo.Equals(string.Empty))
            {

                objCommon.DisplayMessage("Please Select Atleast One Member From The List.", this.Page);
                return;
            }

             ShowReport(IdNo, "rptBordingPass.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_BoardingPassGeneration.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void rdblist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblist.SelectedValue == "1")   // STUDENTS
        {
            trSchool.Visible = false;
            trDept.Visible = false;
            trDegree.Visible = true;
            ddlSchool.SelectedIndex = 0;
            ddlDept.SelectedIndex = 0;
            txtExpiry.Text = string.Empty;
            lvList.DataSource = null;
            lvList.DataBind();
            lvList.Visible = false;
           
        }
        else if (rdblist.SelectedValue == "2")   // EMPLOYEE
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            trSchool.Visible = true;         
            trDegree.Visible = false;
            trBranch.Visible = false;
            txtExpiry.Text = string.Empty;
            lvList.DataSource = null;
            lvList.DataBind();
            lvList.Visible = false;
            objCommon.FillDropDownList(ddlSchool, "PAYROLL_LEAVE_SCHOOL", "SCHOOL_NO", "SCHOOL_NAME", "", "SCHOOL_NO");                     
        }
    }



    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            trBranch.Visible = true;      
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO >0 AND DEGREENO="+ ddlDegree.SelectedValue, "BRANCHNO");         
        }
    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchool.SelectedIndex > 0)
        {
            trDept.Visible = true;
            objCommon.FillDropDownList(ddlDept, "PAYROLL_LEAVE_SCHOOL_DEPARTMENT LD INNER JOIN PAYROLL_SUBDEPT PS  ON (PS.SUBDEPTNO = LD.SUBDEPTNO) INNER JOIN PAYROLL_LEAVE_SCHOOL LS ON (LS.SCHOOL_NO = LD.SCHOOL_NO)", "PS.SUBDEPTNO", "PS.SUBDEPT", "LS.SCHOOL_NO=" + ddlSchool.SelectedValue, "PS.SUBDEPTNO");
        }
    }


    protected void dpPager_PreRender(object sender, EventArgs e)
    {

        BindlistView();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtExpiry.Text != string.Empty && txtAllot.Text != string.Empty)
            {

                if (DateTime.Compare(Convert.ToDateTime(txtAllot.Text), Convert.ToDateTime(txtExpiry.Text)) == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Allotment Date Can Not Be Greater Than Expiry Date.');", true);
                        txtAllot.Focus();
                        return;
                    }  

                string IdNo = string.Empty;
              
                foreach (ListViewDataItem dti in lvList.Items)
                {
                    CheckBox chkSelect = dti.FindControl("chkAccept") as CheckBox;

                    if (chkSelect.Checked)
                    {

                        if (IdNo.Equals(string.Empty))
                        {
                            IdNo = chkSelect.ToolTip;
                        }
                        else
                        {
                            IdNo = IdNo + "$" + chkSelect.ToolTip;
                        }
                    }
                }
                if (IdNo.Equals(string.Empty))
                {
                  
                    objCommon.DisplayMessage("Please Select Atleast One Member From The List.", this.Page); 
                    return;
                }

                objVM.EXPIRY_DATE = txtExpiry.Text.Trim().ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtExpiry.Text);
                objVM.ALLOT_DATE = txtAllot.Text.Trim().ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtAllot.Text);
                objVM.USERNO = Convert.ToInt32(Session["userno"]);
                if (rdblist.SelectedItem.Value == "1")
                {
                    objVM.STATUS ='S';
                }
                else
                {
                    objVM.STATUS = 'E';
                }

                CustomStatus cs = (CustomStatus)objVMC.AddBoardingPassDetails(objVM, IdNo);
                if (Convert.ToInt32(cs) != -99)
                {
                   
                    objCommon.DisplayMessage("Boarding Pass Details Saved Successfully.", this.Page);
                    ShowReport(IdNo, "rptBordingPass.rpt");
                }
               

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_BoardingPassGeneration.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}