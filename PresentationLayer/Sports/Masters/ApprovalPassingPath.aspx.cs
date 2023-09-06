//=======================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : SPORTS AND EVENT MANAGEMENT 
// CREATED BY    : MRUNAL SINGH
// CREATED DATE  : 26-04-2017
// DESCRIPTION   : USED TO CREATE PASSING PATH FOR APPROVAL OF PLAN AND SCHEDULE OF EVENT
//========================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;  

public partial class Sports_Masters_ApprovalPassingPath : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    PassignAuthorityEnt objPAEnt = new PassignAuthorityEnt();
    PassingAuthorityController objPACon = new PassingAuthorityController();

  

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
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                this.CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                FillCollege();              
                FillPAuthority();              
                BindListViewPAPath();
               
            }
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

    protected void BindListViewPAPath()
    {
        try
        {

            DataSet ds = objPACon.GetAllPAPath(Convert.ToInt32(ddlCollegeGrid.SelectedValue));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                btnShowReport.Visible = false;
                rptPathList.DataSource = ds;
                rptPathList.DataBind();
                rptPathList.Visible = false;               
            }
            else
            {
                btnShowReport.Visible = true;
                rptPathList.DataSource = ds;
                rptPathList.DataBind();
                rptPathList.Visible = true;               
            }            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_ApprovalPassingPath.BindListViewPAPath ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain  
        BindListViewPAPath();
    }

    private void Clear()
    {  
        ddlPA01.SelectedIndex = 0;
        ddlPA02.SelectedIndex = 0;
        ddlPA03.SelectedIndex = 0;
        ddlPA04.SelectedIndex = 0;
        ddlPA05.SelectedIndex = 0;
        ddlPA02.Enabled = false;
        ddlPA03.Enabled = false;
        ddlPA04.Enabled = false;
        ddlPA05.Enabled = false;
        txtPAPath.Text = string.Empty;
        trEmp.Visible = false;
        trEvent.Visible = true;
        ddlCollege.SelectedIndex = 0;
        ddlEvent.SelectedIndex = 0;
        
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        //pnlList.Visible = false;
        ddlPA02.Enabled = false;
        ddlPA03.Enabled = false;
        ddlPA04.Enabled = false;
        ddlPA05.Enabled = false;
        ViewState["action"] = "add";
        divSubmit.Visible = true;
        divAddNew.Visible = false;
    }

  
    private void FillCollege()
    {
      
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
        objCommon.FillDropDownList(ddlCollegeGrid, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
       
        if (Session["usertype"].ToString() != "1")//Session["usertype"]
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);

            removeItem = ddlCollegeGrid.Items.FindByValue("0");
            ddlCollegeGrid.Items.Remove(removeItem);
        }
    }

    
    private void FillPAuthority()
    {
        try
        {          
            if (ddlCollege.SelectedValue != "0")
            {
                objCommon.FillDropDownList(ddlPA01, "SPRT_PASSING_AUTHORITY", "PANO", "PANAME", "STATUS=0 AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "PANAME");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_ApprovalPassingPath.FillPAuthority ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        divAddNew.Visible = true;
        divSubmit.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {         
            DataTable dtEmpRecord = new DataTable();
            dtEmpRecord.Columns.Add("EVENTID");          
        
            objPAEnt.PAN01 = Convert.ToInt32(ddlPA01.SelectedValue);
            objPAEnt.PAN02 = Convert.ToInt32(ddlPA02.SelectedValue);
            objPAEnt.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
            objPAEnt.PAN04 = Convert.ToInt32(ddlPA04.SelectedValue);
            objPAEnt.PAN05 = Convert.ToInt32(ddlPA05.SelectedValue);
            objPAEnt.PAPATH = Convert.ToString(txtPAPath.Text);
            objPAEnt.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objPAEnt.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objPAEnt.EVENTNO = Convert.ToInt32(ddlEvent.SelectedValue);

          
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objPAEnt.PAPNO = 0;
                    CustomStatus cs = (CustomStatus)objPACon.AddPAPath(objPAEnt);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        MessageBox("Record Already Exist.");
                        return;
                    }

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        MessageBox("Record Saved Successfully");
                        pnlAdd.Visible = false;                       
                        divSubmit.Visible = false;
                        divAddNew.Visible = true;
                        ViewState["action"] = null;
                        Clear();
                    }
                }
                else
                {
                    if (ViewState["PAPNO"] != null)
                    {
                        objPAEnt.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());

                        CustomStatus CS = (CustomStatus)objPACon.AddPAPath(objPAEnt);
                        if (CS.Equals(CustomStatus.RecordUpdated))
                        {
                            MessageBox("Record Updated Successfully");
                            pnlAdd.Visible = false;                            
                            divSubmit.Visible = false;
                            divAddNew.Visible = true;
                            ViewState["action"] = null;
                            Clear();
                        }
                    }
                }
            }
            BindListViewPAPath();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_ApprovalPassingPath.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PAPNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(PAPNO);

            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            //pnlList.Visible = false;
            ViewState["PAPNO"] = PAPNO;
            divAddNew.Visible = false;
            divSubmit.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_ApprovalPassingPath.btnEdit_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);        
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int PAPNO = int.Parse(btnDelete.CommandArgument);

             DataSet ds = objCommon.FillDropDown("SPRT_PLAN_SCHEDULE", "*", "", "PAPNO = " + PAPNO, "");
             if (ds.Tables[0].Rows.Count > 0)
             {
                 ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Authority Path Can Not Be Deleted. It is in use.');", true);
                 return;
             }
             else
             {
                 CustomStatus cs = (CustomStatus)objPACon.DeletePAPath(PAPNO);
                 if (cs.Equals(CustomStatus.RecordDeleted))
                 {
                     MessageBox("Record Deleted Successfully");
                     ViewState["action"] = null;
                 }
             }
             BindListViewPAPath();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_ApprovalPassingPath.btnDelete_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(Int32 PAPNO)
    {
        DataSet ds = null;
        try
        {
            ds = objPACon.GetSinglePAPath(PAPNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PAPNO"] = PAPNO;
              
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
              //   FillDepartment();
              //  ddlDept.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();
             
                lblEventName.Text = ds.Tables[0].Rows[0]["EVENTNAME"].ToString();
                ViewState["EVENTID"] = ds.Tables[0].Rows[0]["EVENTID"].ToString();
                trEmp.Visible = true;
                trEvent.Visible = false;  
                txtPAPath.Text = ds.Tables[0].Rows[0]["PAPATH"].ToString();
                FillPAuthority();
                ddlPA01.SelectedValue = ds.Tables[0].Rows[0]["PAN01"].ToString();
                this.EnableDisable(1);

                if (!(ds.Tables[0].Rows[0]["PAN02"].ToString().Trim().Equals("0")))
                {
                    ddlPA02.SelectedValue = ds.Tables[0].Rows[0]["PAN02"].ToString();
                    this.EnableDisable(2);
                    ddlPA02.Enabled = true;
                }
                if (!(ds.Tables[0].Rows[0]["PAN03"].ToString().Trim().Equals("0")))
                {
                    ddlPA03.SelectedValue = ds.Tables[0].Rows[0]["PAN03"].ToString();
                    this.EnableDisable(3);
                    ddlPA03.Enabled = true;
                }
                if (!(ds.Tables[0].Rows[0]["PAN04"].ToString().Trim().Equals("0")))
                {
                    ddlPA04.SelectedValue = ds.Tables[0].Rows[0]["PAN04"].ToString();
                    this.EnableDisable(4);
                    ddlPA04.Enabled = true;
                }
                if (!(ds.Tables[0].Rows[0]["PAN05"].ToString().Trim().Equals("0")))
                {
                    ddlPA05.SelectedValue = ds.Tables[0].Rows[0]["PAN05"].ToString();
                    this.EnableDisable(5);
                    ddlPA05.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_ApprovalPassingPath.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
       // FillDepartment();       
        objCommon.FillDropDownList(ddlEvent,"SPRT_EVENT_MASTER EM", "EM.EVENTID", "EM.EVENTNAME", "EM.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue), "");
        FillPAuthority();
       // pnlEmpList.Visible = true;
    }
    protected void ddlCollegeGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewPAPath();
    }
    protected void ddlPA01_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(1);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_ApprovalPassingPath.ddlPA01_click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void ddlPA02_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(2);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_ApprovalPassingPath.ddlPA02_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlPA03_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(3);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_ApprovalPassingPath.ddlPA03_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void ddlPA04_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(4);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_ApprovalPassingPath.ddlPA04_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void EnableDisable(int index)
    {

        switch (index)
        {
            case 1:
                if (ddlPA01.SelectedIndex == 0)
                {
                    
                    ddlPA02.SelectedValue = "0";
                    ddlPA02.Enabled = false;
                   
                    ddlPA03.SelectedValue = "0";
                    ddlPA03.Enabled = false;
                   
                    ddlPA04.SelectedValue = "0";
                    ddlPA04.Enabled = false;
                   
                    ddlPA05.SelectedValue = "0";
                    ddlPA05.Enabled = false;
                    string swhere = "STATUS=0 AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA02, "SPRT_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                }
                else
                {

                    ddlPA02.Enabled = true;
                    string swhere = "STATUS=0 AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA02, "SPRT_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");                    
                    ddlPA03.SelectedValue = "0";
                    ddlPA03.Enabled = false;                   
                    ddlPA04.SelectedValue = "0";
                    ddlPA04.Enabled = false;                   
                    ddlPA05.SelectedValue = "0";
                    ddlPA05.Enabled = false;
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString();
                }

                break;

            case 2:
                if (ddlPA02.SelectedIndex == 0)
                {
                    ddlPA03.SelectedValue = "0";
                    ddlPA03.Enabled = false;                   
                    ddlPA04.SelectedValue = "0";
                    ddlPA04.Enabled = false;                   
                    ddlPA05.SelectedValue = "0";
                    ddlPA05.Enabled = false;
                    string swhere = "STATUS=0 AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA03, "SPRT_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString();
                }
                else
                {
                    ddlPA03.Enabled = true;
                    string swhere = "STATUS=0 AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA03, "SPRT_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                                        
                    ddlPA04.SelectedValue = "0";
                    ddlPA04.Enabled = false;
                  
                    ddlPA05.SelectedValue = "0";
                    ddlPA05.Enabled = false;

                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString();
                }
                break;
            case 3:


                if (ddlPA03.SelectedIndex == 0)
                {                   
                    ddlPA04.SelectedValue = "0";
                    ddlPA04.Enabled = false;                   
                    ddlPA05.SelectedValue = "0";
                    ddlPA05.Enabled = false;
                    string swhere = "STATUS=0 AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and  PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA04, "SPRT_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString();
                }
                else
                {
                    ddlPA04.Enabled = true;
                    string swhere = "STATUS=0 AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and  PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA04, "SPRT_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString();
                }

                break;
            case 4:

                if (ddlPA04.SelectedIndex == 0)
                {                  
                    ddlPA05.SelectedValue = "0";
                    ddlPA05.Enabled = false;
                    string swhere = "STATUS=0 AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA05, "SPRT_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString();
                }
                else
                {
                    ddlPA05.Enabled = true;
                    string swhere = "STATUS=0 AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and  PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA05, "SPRT_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString() + "->" + ddlPA04.SelectedItem.ToString();
                }

                break;
            case 5:
                if (!(ddlPA04.SelectedIndex == 0))
                {
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString() + "->" + ddlPA04.SelectedItem.ToString() + "->" + ddlPA05.SelectedItem.ToString();
                }
                break;

        }

    }


    protected void ddlPA05_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(5);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_ApprovalPassingPath.ddlPA05_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("ApprovalPassingPath", "ApprovalPassingPath.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_ApprovalPassingPath.btnShowReport_Click->" + ex.Message + ' ' + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));          
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_COLLEGECODE=" + Session["colcode"].ToString() + "," + "@P_PAPNO=0";

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_ApprovalPassingPath.ShowReport->" + ex.Message + ' ' + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    


  
}