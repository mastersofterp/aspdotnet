//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : BATCH MASTER                                                         
// CREATION DATE : 02-SEPT-2009                                                         
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class Academic_Masters_BatchMaster : System.Web.UI.Page
{
    #region Page Events
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
                this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

            }
            objCommon.FillDropDownList(ddlSubjectType, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO>0 AND ACTIVESTATUS=1", "SECTIONNO");
            BindListView();
            ViewState["action"] = "add";
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BatchMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BatchMaster.aspx");
        }
    }
    #endregion
    
    #region Form Events

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int ActiveStatus = 0;
        try
        {
            BatchController objBC = new BatchController();
            Batch objBatch = new Batch();
            objBatch.SubId = Convert.ToInt32(ddlSubjectType.SelectedValue);
            objBatch.BatchName = txtBatchName.Text.Trim();
            objBatch.CollegeCode = Session["colcode"].ToString();
            if (hfdStat.Value == "true") // Added By Rishabh on 24/01/2022
            {
                ActiveStatus = 1;
            }
            else
            {
                ActiveStatus = 0;
            }

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    string batchno = objCommon.LookUp("ACD_BATCH", "BATCHNO", "BATCHNAME='" + txtBatchName.Text.Trim() + "' and SECTIONNO=" + ddlSubjectType.SelectedValue + "");
                    if (batchno != null && batchno != string.Empty)
                    {
                        objCommon.DisplayMessage(this.updBatch, "Record Already Exist", this.Page);
                        return;
                    }

                    //Add Batch
                    CustomStatus cs = (CustomStatus)objBC.AddBatch(objBatch, ActiveStatus);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.updBatch, "Record Saved Successfully!", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(this.updBatch, "Record already exist", this.Page);
                        //Label1.Text = "Record already exist";
                    }
                    else 
                    {
                        objCommon.DisplayMessage(this.updBatch, "Oops, Something has gone wrong", this.Page);
                        //Label1.Text = "Record already exist";
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["batchno"] != null)
                    {
                        objBatch.BatchNo = Convert.ToInt32(ViewState["batchno"].ToString());

                        CustomStatus cs = (CustomStatus)objBC.UpdateBatch(objBatch, ActiveStatus);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = null;
                            Clear();
                            objCommon.DisplayMessage(this.updBatch, "Record Updated Successfully!", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            objCommon.DisplayMessage(this.updBatch, "Record already exist", this.Page);
                            //Label1.Text = "Record already exist";
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updBatch, "Oops, Something has gone wrong", this.Page);
                        }
                    }
                }

                BindListView();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_BatchMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = null;
        ViewState["action"] = "add";
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int batchcno = int.Parse(btnEdit.CommandArgument);
            //Label1.Text = string.Empty;

            ShowDetail(batchcno);
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_BatchMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Private Methods

    private void ShowDetail(int batchno)
    {
        BatchController objBatch = new BatchController();
        SqlDataReader dr = objBatch.GetBatchByNo(batchno);

        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["batchno"] = batchno.ToString();
                ddlSubjectType.Text = dr["SECTIONNO"] == null ? string.Empty : dr["SECTIONNO"].ToString();
                txtBatchName.Text = dr["BATCHNAME"] == null ? string.Empty : dr["BATCHNAME"].ToString();
                if (dr["ACTIVESTATUS"].ToString() == "1")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
                }
            }
        }
        if (dr != null) dr.Close();
    }

    private void Clear()
    {
        ddlSubjectType.SelectedIndex = 0;
        txtBatchName.Text = string.Empty;
        //Label1.Text = string.Empty;
    }

    private void BindListView()
    {
        try
        {
            BatchController objBC = new BatchController();
            DataSet ds = objBC.GetAllBatch();
            lvBatchName.DataSource = ds;
            lvBatchName.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_BatchMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("BatchMaster", "rptBatchMaster.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        //try
        //{
        //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        //    url += "Reports/CommonReport.aspx?";
        //    url += "pagetitle=" + reportTitle;
        //    url += "&path=~,Reports,Academic," + rptFileName;
        //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();

        //    divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //    divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //    divMsg.InnerHtml += " </script>";
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //}

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_RollListForScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
