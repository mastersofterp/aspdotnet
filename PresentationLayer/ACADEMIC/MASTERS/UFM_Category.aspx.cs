using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class ACADEMIC_MASTERS_UFM_Category : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ExamController objEC = new ExamController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    /// Fill Dropdown lists                
                    ViewState["action"] = "add";
                    FillListView();
                    FillCheckBoxList();
                }
               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_MASTERS_UFM_Category.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillCheckBoxList()
    {
        DataSet ds = objCommon.FillDropDown("ACD_EXAM_NAME", "EXAMNAME", "FLDNAME", "EXAMNAME <>''", "FLDNAME");
        chkExam.DataSource = ds;
        chkExam.DataTextField = "EXAMNAME";
        chkExam.DataValueField = "FLDNAME";
        chkExam.DataBind();
    }

    private void FillListView()
    {
        DataSet ds = objCommon.FillDropDown("ACD_UFM_MASTER", "UFMNO", "UFM_NAME,UFM_DESC,UFM_PUNISHMENT", "UFMNO>0", "UFMNO");
        lvUFM.DataSource = ds;
        lvUFM.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ret=-99;
            string category = txtCategory.Text.Trim();
            string cateDesc = txtCatDesc.Text.Trim();
            string CatPunishemnt = txtCatPunishment.Text.Trim();
            string col_code=Session["colcode"].ToString();
            int debarred = Convert.ToInt32(ddlDebarred.SelectedValue);
            bool extermark = false;
            bool s1 = false;
            bool s2 = false;
            bool s3 = false;
            bool s4 = false;
            bool s5 = false;
            bool s6 = false;
            bool s7 = false;
            bool s8 = false;
            bool s9 = false;
            bool s10 = false;

            foreach (ListItem item in chkExam.Items)
            {
                if (item.Value.ToUpper() == "EXTERMARK" && item.Selected==true)
                    extermark = true;
                else if (item.Value.ToUpper() == "S1" && item.Selected == true)
                    s1 = true;
                else if (item.Value.ToUpper() == "S2" && item.Selected == true)
                    s2 = true;
                else if (item.Value.ToUpper() == "S3" && item.Selected == true)
                    s3 = true;
                else if (item.Value.ToUpper() == "S4" && item.Selected == true)
                    s4 = true;
                else if (item.Value.ToUpper() == "S5" && item.Selected == true)
                    s5 = true;
                else if (item.Value.ToUpper() == "S6" && item.Selected == true)
                    s6 = true;
                else if (item.Value.ToUpper() == "S7" && item.Selected == true)
                    s7 = true;
                else if (item.Value.ToUpper() == "S8" && item.Selected == true)
                    s8 = true;
                else if (item.Value.ToUpper() == "S9" && item.Selected == true)
                    s9 = true;
                else if (item.Value.ToUpper() == "S10" && item.Selected == true)
                    s10 = true;
            }
            
            if (ViewState["action"].ToString() == "edit")
            {
                ret = objEC.UFMCategoryInsUpd(Convert.ToInt32(ViewState["UFMNO"].ToString()), category, cateDesc, CatPunishemnt, col_code, extermark, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,debarred);
                ViewState["action"] = "add";
            }
            else
            {
                ret = objEC.UFMCategoryInsUpd(0, category, cateDesc, CatPunishemnt, col_code, extermark, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10,debarred);
            }
            if (ret == 1)
            {
                objCommon.DisplayMessage("Record Saved Successfully", this.Page);
                FillListView();
                Clear();
            }
            else
                objCommon.DisplayMessage("Trainsaction Failed", this.Page);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_MASTERS_UFM_Category.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Clear();
        ImageButton editButton = sender as ImageButton;
        int ufmno = Int32.Parse(editButton.CommandArgument);

        DataSet ds = objCommon.FillDropDown("ACD_UFM_MASTER", "UFMNO", "UFM_NAME,UFM_DESC,UFM_PUNISHMENT,isnull(EXTERMARK,0)EXTERMARK,isnull(S1,0)S1,isnull(S2,0)S2,isnull(S3,0)S3,isnull(S4,0)S4,isnull(S5,0)S5,isnull(S6,0)S6,isnull(S7,0)S7,isnull(S8,0)S8,isnull(S9,0)S9,isnull(S10,0)S10,isnull(DEBARRED,0)DEBARRED", "UFMNO=" + ufmno, "UFMNO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtCategory.Text = ds.Tables[0].Rows[0]["UFM_NAME"].ToString();
            txtCatDesc.Text = ds.Tables[0].Rows[0]["UFM_DESC"].ToString();
            txtCatPunishment.Text = ds.Tables[0].Rows[0]["UFM_PUNISHMENT"].ToString();
            ViewState["UFMNO"] = ds.Tables[0].Rows[0]["UFMNO"].ToString();
            ViewState["action"] = "edit";

            if (ds.Tables[0].Rows[0]["EXTERMARK"].ToString() == "True")
                chkExam.Items.FindByValue("EXTERMARK").Selected = true;
            if (ds.Tables[0].Rows[0]["S1"].ToString() == "True")
                chkExam.Items.FindByValue("S1").Selected = true;
            if (ds.Tables[0].Rows[0]["S2"].ToString() == "True")
                chkExam.Items.FindByValue("S2").Selected = true;
            if (ds.Tables[0].Rows[0]["S3"].ToString() == "True")
                chkExam.Items.FindByValue("S3").Selected = true;
            if (ds.Tables[0].Rows[0]["S4"].ToString() == "True")
                chkExam.Items.FindByValue("S4").Selected = true;
            if (ds.Tables[0].Rows[0]["S5"].ToString() == "True")
                chkExam.Items.FindByValue("S5").Selected = true;
            if (ds.Tables[0].Rows[0]["S6"].ToString() == "True")
                chkExam.Items.FindByValue("S6").Selected = true;
            if (ds.Tables[0].Rows[0]["S7"].ToString() == "True")
                chkExam.Items.FindByValue("S7").Selected = true;
            if (ds.Tables[0].Rows[0]["S8"].ToString() == "True")
                chkExam.Items.FindByValue("S8").Selected = true;
            if (ds.Tables[0].Rows[0]["S9"].ToString() == "True")
                chkExam.Items.FindByValue("S9").Selected = true;
            if (ds.Tables[0].Rows[0]["S10"].ToString() == "True")
                chkExam.Items.FindByValue("S10").Selected = true;
            ddlDebarred.SelectedValue = ds.Tables[0].Rows[0]["DEBARRED"].ToString();
           
        }
        
    }

    private void Clear()
    {
        txtCatDesc.Text = string.Empty;
        txtCategory.Text = string.Empty;
        txtCatPunishment.Text = string.Empty;
        ddlDebarred.SelectedIndex = 0;
        foreach (ListItem item in chkExam.Items)
            item.Selected = false;
    }
}
