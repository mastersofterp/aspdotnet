using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessEntities;


public partial class panelcreation : System.Web.UI.Page
{
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    PanelCreationController objpanel = new PanelCreationController();
    
    
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
                ViewState["action"] = "add";
                BindPanelCreationList();
                BindDropdown();
            }
        }
    }
    protected void BindDropdown()
    {
        objCommon.FillDropDownList(ddlAdmBatch, "acd_admbatch", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO");
        objCommon.FillListBox(lstbxStaff, "user_acc", "UA_NO", "UA_FULLNAME", "ua_type =" + 3 + "", "UA_NO");
        objCommon.FillDropDownList(ddlDegree, "vw_acd_college_degree_branch", "distinct degreeno", "DEGREE_CODE", "","");
        
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlProgram, "vw_acd_college_degree_branch", "branchno", "LONGNAME", "degreeno =" + Convert.ToInt32(ddlDegree.SelectedValue), "");
        }    
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdmBatch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlschedule, "acd_admbatch b INNER JOIN ACD_ENTRANCE_TEST_SCHEDULE a on (b.BATCHNO = a.ADMBATCH)", "SCHEDULE_NO", "(convert(nvarchar,a.SCHD_DATE,101) +' '+ SCHD_TIME) DATE_TIME", "BATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue), "");
        }
    }
    public void Clear()
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlProgram.Items.Clear();
        ddlProgram.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlschedule.Items.Clear();
        ddlschedule.Items.Insert(0, new ListItem("Please Select", "0"));
        txtpanelName.Text = string.Empty;
        ddlpanelfor.SelectedIndex = 0;
        lstbxStaff.ClearSelection();
        ViewState["action"] = "add";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void BindPanelCreationList()
    {
        try
        {
            PanelCreationController objpanel = new PanelCreationController();
            DataSet dspanel = objpanel.GetAllPanel();
            lvPanel.DataSource = dspanel;
            lvPanel.DataBind();
            Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ExamConfig.BindExamList -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            PanelCreation objpc = new PanelCreation();
            int userno = Convert.ToInt32(Session["userno"].ToString());
            objpc.Batchno = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            objpc.Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            objpc.Branchno = Convert.ToInt32(ddlProgram.SelectedValue);
            objpc.panelfor = Convert.ToInt32(ddlpanelfor.SelectedValue);
            objpc.Scheduleno = Convert.ToInt32(ddlschedule.SelectedValue);
            objpc.Panelname = txtpanelName.Text;
            string staff = "";
            foreach (ListItem item in lstbxStaff.Items)
            {
                if (item.Selected == true)
                {
                    staff += item.Value + ',';
                }
            }
            if (!string.IsNullOrEmpty(staff))
            {
                staff = staff.Substring(0, staff.Length - 1);
            }
            else
            {
                staff = "0";

            }
            objpc.staff = staff;

            objpc.panelid = 0;
            if (ViewState["action"].ToString() == "add")
            {
                objpc.panelid = 0;
            }
            else
            {
                objpc.panelid = Convert.ToInt32(ViewState["PANELID"]);
            }
             int status = objpanel.InsertPanelCreationData(objpc,userno);
            if (status == 1)
            {
                objCommon.DisplayMessage(this.updpanel, "Record Saved Sucessfully !", this.Page);
                BindPanelCreationList();
                Clear();
            }
            else if (status == 2)
            {
                objCommon.DisplayMessage(this.updpanel, "Record Updated Sucessfully!", this.Page);
                BindPanelCreationList();
                Clear();
            }
            else if (status == -1001)
            {
                objCommon.DisplayMessage(this.updpanel, "Record Already Exist!", this.Page);
                BindPanelCreationList();
                Clear();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PanelCreation.aspx.BindPanelCreationList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        Clear();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int panelid = int.Parse(btnEdit.CommandArgument);
        ViewState["PANELID"] = int.Parse(btnEdit.CommandArgument);
        ShowDetails(panelid);
        ViewState["action"] = "edit";
    }
    private void ShowDetails(int panelid)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_ADMP_Panel", "PANELID", "BATCHNO, DEGREENO,BRANCHNO,SCHEDULE_NO,PANEL_NAME,PANEL_FOR,STAFFNO", "PANELID =" + panelid + "", "PANELID");

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlAdmBatch.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["BATCHNO"].ToString());
                objCommon.FillDropDownList(ddlschedule, "acd_admbatch b INNER JOIN ACD_ENTRANCE_TEST_SCHEDULE a on (b.BATCHNO = a.ADMBATCH)", "SCHEDULE_NO", "(convert(nvarchar,a.SCHD_DATE,101) +' '+ SCHD_TIME) DATE_TIME", "BATCHNO=" + Convert.ToInt32(ddlAdmBatch.SelectedValue), "");
                ddlschedule.SelectedValue = ds.Tables[0].Rows[0]["SCHEDULE_NO"].ToString();
                txtpanelName.Text = ds.Tables[0].Rows[0]["PANEL_NAME"].ToString();
                ddlpanelfor.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PANEL_FOR"].ToString());
                char delimiterChars = ',';
                string staff = ds.Tables[0].Rows[0]["STAFFNO"].ToString();
                string[] stu = staff.Split(delimiterChars);
                for (int j = 0; j < stu.Length; j++)
                {
                    for (int i = 0; i < lstbxStaff.Items.Count; i++)
                    {
                        if (stu[j].Trim() == lstbxStaff.Items[i].Value.Trim())
                        {
                            lstbxStaff.Items[i].Selected = true;
                        }
                    }
                }
                ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                objCommon.FillDropDownList(ddlProgram, "vw_acd_college_degree_branch", "branchno", "LONGNAME", "degreeno =" + Convert.ToInt32(ddlDegree.SelectedValue), "");
                ddlProgram.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PanelCreation.BindPanelCreationList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}