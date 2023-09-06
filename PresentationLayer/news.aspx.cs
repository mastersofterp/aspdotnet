//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   :                                                                 
// PAGE NAME     : TO CREATE NEWS                                                  
// CREATION DATE : 13-April-2009                                                   
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


public partial class news : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    NewsController objNC = new NewsController();
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

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                else
                {
                    //lblHelp.Text = "No Help Added";                
                }
                //Bind the ListView with News
                BindListViewNews();
                pnlAdd.Visible = false;
                pnlList.Visible = true;
            }
        }
    }

    /// <summary>
    /// This method binds the Listview with the DataSource
    /// </summary>
    private void BindListViewNews()
    {
        try
        {
            NewsController objNc = new NewsController();
            DataSet dsNews = objNc.GetAllNews("PKG_NEWS_SP_ALL_NEWS");

            if (dsNews.Tables[0].Rows.Count > 0)
            {
                lvNews.DataSource = dsNews;
                lvNews.DataBind();
            }
        }
        catch (Exception ex)
        { 
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "news.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (fuFile.HasFile)
            {
                string ext=System.IO.Path.GetExtension(fuFile.FileName).ToLower();
                if (ext == ".pdf" || ext == ".doc" || ext == ".docx")
                {

                }
                else
                {
                    objCommon.DisplayMessage("Please Upload only Pdf and Word files", this.Page);
                    return;
                }

                if (fuFile.FileName.ToString().Length > 50)
                {
                    objCommon.DisplayMessage("Upload File Name is too long", this.Page);
                    return;
                }
            }
            NewsController objNC = new NewsController();
            News objNews = new News();
            objNews.NewsTitle = txtTitle.Text.Trim();
            objNews.NewsDesc = ftbDesc.Text.Trim();
            objNews.Link = txtLinkName.Text.Trim();
            objNews.Category = "NEWS";
            objNews.Filename = fuFile.FileName;
            objNews.ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text);
            objNews.Status = 1;

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                //Add New Record
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objNC.Add(objNews, fuFile);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage("Record Saved Successfully!!!", this.Page);
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                    }
                       
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

                        CustomStatus cs = (CustomStatus)objNC.Update(objNews, fuFile);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                             objCommon.DisplayMessage("Record Updated Successfully!!!", this.Page);
                             pnlAdd.Visible = false;
                             pnlList.Visible = true;                            
                        }                           
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
                objUCommon.ShowError(Page, "News.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }  
    private void Clear()
    {
        txtTitle.Text = string.Empty;
        txtLinkName.Text = string.Empty;
        txtExpiryDate.Text = string.Empty;
        ftbDesc.Text = string.Empty;   
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear all textboxes
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;
    } 

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int newsid = int.Parse(btnDel.CommandArgument);

            CustomStatus cs = (CustomStatus)objNC.Delete(newsid);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage("Record Deleted Successfully!!!", this.Page);
                pnlAdd.Visible = false;
                pnlList.Visible = true;
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "News.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
                objUCommon.ShowError(Page, "News.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int newsid)
    {
        try
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
                    txtExpiryDate.Text = dr["EXPIRY_DATE"] == null ? "" : Convert.ToDateTime(dr["EXPIRY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    hdnFile.Value = dr["Filename"] == null ? "" : dr["Filename"].ToString();
                    ftbDesc.Text = dr["NewsDesc"] == null ? "" : dr["NewsDesc"].ToString();
                }
            }
            if (dr != null) dr.Close();
        }
         catch (Exception ex)
         {
             if (Convert.ToBoolean(Session["error"]) == true)
                 objUCommon.ShowError(Page, "News.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
             else
                 objUCommon.ShowError(Page, "Server UnAvailable");
         }
    }

    public string GetFileName(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return filename.ToString();
        else
            return "None";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "~/upload_files/" + filename.ToString();
        else
            return "";
    }

    public string GetStatus(object status)
    {
        if (status.ToString().Equals("Expired"))
            return "<span style='color:Red'>Expired</span>";
        else
            return "<span style='color:Green'>Active</span>";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        Clear();
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewNews();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=news.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=news.aspx");
        }
    }
}
