using BusinessLogicLayer.BusinessLogic;
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

public partial class LEADMANAGEMENT_Masters_EnquiryStatus : System.Web.UI.Page
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
            objLM.ENQUIRYSTATUSNAME = txtEnquiryStatus.Text.Trim();
            if (chknlstatus.Checked)
            {
                objLM.ENQUIRYSTATUS = 1;
            }
            else
            {
                objLM.ENQUIRYSTATUS = 0;
            }

            //if (ViewState["Action"] != null)
            //{

            //    if (ViewState["Action"].ToString().Equals("add"))
            //    {

            //        CustomStatus cs = (CustomStatus)objLMC.AddEnquiryStatus(objLM);
            //        if (cs.Equals(CustomStatus.RecordSaved))
            //        {
            //            ViewState["Action"] = "add";
            //            Clear();                     
            //            objCommon.DisplayMessage(this.updBatch, "Enquiry added successfully", this.Page);
                        
            //        }
            //        else
            //        {
            //            ViewState["Action"] = "add";
            //            Clear();                     
            //            objCommon.DisplayMessage(this.updBatch, "Record already exist", this.Page);
                        
            //        }

            //    }
            //    else
            //    {
            //        if (ViewState["enquirystatusno"] != null)
            //        {
            //            objLM.ENQUIRYSTATUSNO = Convert.ToInt32(ViewState["enquirystatusno"].ToString());
            //            CustomStatus cs = (CustomStatus)objLMC.UpdateEnquiryStatus(objLM);
            //            if (cs.Equals(CustomStatus.RecordUpdated))
            //            {
            //                ViewState["Action"] = null;
            //                Clear();                         
            //                objCommon.DisplayMessage(this.updBatch, "Record Updated successfully", this.Page);
            //                btnSave.Text = "Submit";
            //            }
            //            else
            //            {
            //                ViewState["Action"] = null;
            //                Clear();
                           
            //                objCommon.DisplayMessage(this.updBatch, "Record already exist", this.Page);
            //            }

            //        }
            //    }

            //}
            //BindListView();



            //Check for add or edit
            if (ViewState["Action"] != null && ViewState["Action"].ToString().Equals("edit"))
            {
                //Edit 
                objLM.ENQUIRYSTATUSNO = Convert.ToInt32(ViewState["enquirystatusno"].ToString());
                CustomStatus cs = (CustomStatus)objLMC.UpdateEnquiryStatus(objLM);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ViewState["action"] = null;
                    Clear();
                    BindListView();
                    objCommon.DisplayMessage(this.updBatch, "Record Updated successfully", this.Page);
                    btnSave.Text = "Submit";
                }
            }
            else
            {
                //Add New
                CustomStatus cs = (CustomStatus)objLMC.AddEnquiryStatus(objLM);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ViewState["action"] = "add";
                    Clear();
                    BindListView();
                    objCommon.DisplayMessage(this.updBatch, "Enquiry added successfully", this.Page);
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

        txtEnquiryStatus.Text = string.Empty;
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
            DataSet ds = objLMC.GetAllEnquiryStatus();
            lvEnquiryStatus.DataSource = ds;
            lvEnquiryStatus.DataBind();
            foreach (ListViewItem item in lvEnquiryStatus.Items)
            {
                ///STATUS COLOR CHANGE
                Label Status = item.FindControl("divstatus") as Label;
                if (Status.Text.Trim() == "Active") 
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
        //Clear();
        //ViewState["Action"] = null;
        ViewState["action"] = null;

        Response.Redirect(Request.Url.ToString());
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("EnquiryStatus", "rptEnquiryStatus.rpt");
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
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString() + " ";


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
            ViewState["enquirystatusno"] = int.Parse(btnEdit.CommandArgument);
            int EnquiryStatusno = int.Parse(btnEdit.CommandArgument);
            ShowDetail(EnquiryStatusno);
            ViewState["Action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_BatchMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int EnquiryStatusno)
    {
        try
        {
            LMController objLMC = new LMController();

            SqlDataReader dr = objLMC.GetSingleEnquiryStatus(EnquiryStatusno);
            if (dr != null)
            {
                if (dr.Read())
                {
                    txtEnquiryStatus.Text = dr["ENQUIRYSTATUSNAME"] == null ? string.Empty : dr["ENQUIRYSTATUSNAME"].ToString();
                    //int stat = Convert.ToInt32(dr["ENQUIRYSTATUS"]);
                    if (Convert.ToString(dr["ENQUIRYSTATUS"]) == "0")
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