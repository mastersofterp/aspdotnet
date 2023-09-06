using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LEADMANAGEMENT_Masters_SourceType : System.Web.UI.Page
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
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            BindListView();
            ViewState["action"] = "add";
           
        }
        // divMsg.InnerHtml = string.Empty;

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=QualifyMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=QualifyMaster.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            LMController objLMC = new LMController();
            LeadManage objLM = new LeadManage();
            objLM.SOURCETYPENAME = txtSourceType.Text.Trim();
            if (chknlstatus.Checked)
            {
                objLM.SOURCETYPESTATUS = 1;
            }
            else
            {
                objLM.SOURCETYPESTATUS = 0;
            }
            //Check for add or edit
            if (ViewState["Action"] != null && ViewState["Action"].ToString().Equals("edit"))
            {
                //Edit 
                objLM.SOURCETYPENO = Convert.ToInt32(ViewState["sourcetypeno"].ToString());
                CustomStatus cs = (CustomStatus)objLMC.UpdateSourceType(objLM);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    Clear();
                    BindListView();
                    objCommon.DisplayMessage(this.updBatch, "Record Updated successfully", this.Page);
                    btnSave.Text = "Submit";
                }
            }
            else
            {
                //Add New
                CustomStatus cs = (CustomStatus)objLMC.AddSourceType(objLM);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    Clear();
                    BindListView();
                    objCommon.DisplayMessage(this.updBatch, "Record added successfully", this.Page);
                }
                else
                {
                    //msgLbl.Text = "Record already exist";
                    objCommon.DisplayMessage(this.updBatch, "Record already exist", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_SessionCreate.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void Clear()
    {

        txtSourceType.Text = string.Empty;
        Label1.Text = string.Empty;
        chknlstatus.Checked = false;
        ViewState["action"] = "add";
        ViewState["Action"] = null;
    }

    private void BindListView()
    {
        try
        {
            LMController objLMC = new LMController();
            DataSet ds = objLMC.GetAllSourceType();
            lvSourceType.DataSource = ds;
            lvSourceType.DataBind();
         
            foreach (ListViewItem item in lvSourceType.Items)
            {
                ///STATUS COLOR CHANGE
                Label Status = item.FindControl("divstatus") as Label;
                if (Status.Text.Trim().ToUpper() == "ACTIVE")
                {
                    Status.Style.Add("color", "Green");
                }
                else
                {
                    Status.Style.Add("color", "black");
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_QualifyMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["Action"] = null;
    }
    //Added By Nikhil Vinod Lambe on 19022020
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("SourceType", "rptSourceType.rpt");
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("LEADMANAGEMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,LEADMANAGEMENT," + rptFileName;
            // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString() + ",@P_STATUS=" + radlStatus.SelectedValue.ToString();         
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString() +" ";


            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            ////        //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LEADMANAGEMENT_Masters_SourceType.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
       
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SourceTypeno = int.Parse(btnEdit.CommandArgument);
            ViewState["sourcetypeno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["Action"] = "edit";
            ShowDetail(SourceTypeno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "SourceType.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int SourceTypeno)
    {
        try
        {
            LMController objLMC = new LMController();

            SqlDataReader dr = objLMC.GetSingleSourceType(SourceTypeno);
            if (dr != null)
            {
                if (dr.Read())
                {
                    txtSourceType.Text = dr["SOURCETYPENAME"] == null ? string.Empty : dr["SOURCETYPENAME"].ToString();
                    //int stat = Convert.ToInt32(dr["ENQUIRYSTATUS"]);
                    if (Convert.ToString(dr["SOURCETYPESTATUS"]) == "0")
                    {
                        chknlstatus.Checked = false;
                    }
                    else
                    {
                        chknlstatus.Checked = true;
                    }

                }
                if (dr != null) dr.Close();
                ViewState["Action"] = "edit";
                btnSave.Text = "Update";

            }         
            else
            {
                ViewState["action"] = "add";
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
}