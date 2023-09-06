//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : TO CREATE UPLOAD FILE                                           
// CREATION DATE : 
// CREATED BY    : NIRAJ D. PHALKE & ASHWINI BARBATE                               
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

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

public partial class createUpload : System.Web.UI.Page
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
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
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
                    //lblHelp.Text = objCommon.GetPageHelp( int.Parse( Request.QueryString["pageno"].ToString()));
                }

                //Bind the ListView with News
                BindListViewUpload();

                pnlAdd.Visible = false;
                pnlList.Visible = true;
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
        }
    }

    private void BindListViewUpload()
    {
        try
        {
            NewsController objNc = new NewsController();
            //Upload Stored Procedure
            DataSet ds = objNc.GetAllNews("PKG_NEWS_SP_ALL_UPLOADS");
            lvUpload.DataSource = ds;
            lvUpload.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "createUpload.BindListViewUpload-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            NewsController objNC = new NewsController();
            News objNews = new News();
            objNews.NewsTitle = txtTitle.Text.Trim();
            objNews.NewsDesc = ftbDesc.Text.Trim();
            objNews.Link = txtLinkName.Text.Trim();
            objNews.Category = "DOWNLOADS";
            objNews.Filename = fuFile.FileName;
            objNews.ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text);

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                //Add New Record
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objNC.Add(objNews, fuFile);
                    if (cs.Equals(CustomStatus.RecordSaved))
                        Response.Redirect(Request.Url.ToString());
                    else
                        if (cs.Equals(CustomStatus.FileExists))
                            lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                }
                else
                {
                    //Update Record
                    if (ViewState["newsid"] != null)
                    {
                        //set additional properties
                        objNews.NewsID = Convert.ToInt32(ViewState["newsid"].ToString());
                        objNews.OldFilename = hdnFile.Value;

                        //set status
                        DateTime dt = Convert.ToDateTime(txtExpiryDate.Text);
                        if (dt < DateTime.Now)
                            objNews.Status = 0;
                        else
                            objNews.Status = 1;

                        CustomStatus cs = (CustomStatus)objNC.Update(objNews, fuFile);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                            Response.Redirect(Request.Url.ToString());
                        else
                            if (cs.Equals(CustomStatus.FileExists))
                                lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "createUpload.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear all textboxes
        txtTitle.Text = string.Empty;
        txtLinkName.Text = string.Empty;
        txtExpiryDate.Text = string.Empty;
        ftbDesc.Text = string.Empty;
        pnlAdd.Visible = false;
        pnlList.Visible = true;
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int newsid = int.Parse(btnDel.CommandArgument);

            NewsController objNC = new NewsController();

            CustomStatus cs = (CustomStatus)objNC.Delete(newsid);
            if (cs.Equals(CustomStatus.RecordDeleted))
                Response.Redirect(Request.Url.ToString());

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "createUpload.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int newsid = int.Parse(btnEdit.CommandArgument);

            ShowDetail(newsid);

            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "createUpload.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int newsid)
    {
        NewsController objNC = new NewsController();
        SqlDataReader dr = objNC.GetSingleNews(newsid);

        //Show News Detail
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["newsid"] = int.Parse(dr["NewsID"].ToString());
                txtTitle.Text = dr["NewsTitle"] == null ? "" : dr["NewsTitle"].ToString();
                txtLinkName.Text = dr["Link"] == null ? "" : dr["Link"].ToString();
                txtExpiryDate.Text = dr["EXPIRY_DATE"] == null ? "" : Convert.ToDateTime(dr["EXPIRY_DATE"].ToString()).ToString("dd-MMM-yyyy");
                hdnFile.Value = dr["Filename"] == null ? "" : dr["Filename"].ToString();
                ftbDesc.Text = dr["NewsDesc"] == null ? "" : dr["NewsDesc"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }

    public string GetFileName(object filename)
    {
        if (filename != null && filename.ToString() != string.Empty)
            return filename.ToString();
        else
            return "None";
        //return "<span style='color:White'>None<span>";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != string.Empty)
            return "~/upload_files/" + filename.ToString();
        else
            return "";
    }

    public string GetStatus(object status)
    {
        if (status.ToString().ToLower().Equals("expired"))
            return "<span style='color:Red'>Expired</span>";
        else
            return "<span style='color:Green'>Active</span>";

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        pnlAdd.Visible = true;
        pnlList.Visible = false;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createnotice.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createnotice.aspx");
        }
    }
}
