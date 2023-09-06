//=================================================================================
// PROJECT NAME  : PERSONNEL REQUIREMENT MANAGEMENT                                
// MODULE NAME   :                                                                 
// PAGE NAME     : TO CREATE MODULE                                                
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
using System.Reflection;
using mastersofterp_MAKAUAT;

public partial class domain : System.Web.UI.Page
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

    protected void Page_Load(object sender, System.EventArgs e)
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

                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}

                //Bind the ListView with Domain
                BindListViewDomain();

                if (Request.QueryString["action"] == null)
                {
                    pnlAdd.Visible = false;
                    pnlList.Visible = true;
                }
                else
                {
                    if (Request.QueryString["action"].ToString().Equals("add"))
                    {   //add
                        pnlAdd.Visible = true;
                        pnlList.Visible = false;
                    }
                    else
                    {   //edit
                        txtDomainName.Text = Request.QueryString["title"].ToString();
                        txtSqnNo.Text = Request.QueryString["as_seq_no"].ToString();
                        lblactive.Text = Request.QueryString["Status"].ToString();
                        if (lblactive.Text == "Active")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(true);", true);
                           
                        }
                        else
                        {
                           
                            ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(false);", true);
                        }
                       
                        //pnlAdd.Visible = true;
                        pnlList.Visible = false;
                    }
                }
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
        }
        //this.ClearQueryString();
    }

    private void BindListViewDomain()
    {
        try
        {
            Acc_SectionController objAC = new Acc_SectionController();
            DataSet dsDomain = objAC.GetAllAcc_Sections();

            if (dsDomain.Tables[0].Rows.Count > 0)
            {
                lvDomain.DataSource = dsDomain;
                lvDomain.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "domain.BindListViewDomain-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    //{
    //    //try
    //    //{
    //    //    ImageButton btnDel = sender as ImageButton;
    //    //    int as_no = int.Parse(btnDel.CommandArgument);

    //    //    Acc_SectionController objAC = new Acc_SectionController();

    //    //    CustomStatus cs = (CustomStatus)objAC.DeleteAcc_Section(as_no);
    //    //    if (cs.Equals(CustomStatus.RecordDeleted))
    //    //        Response.Redirect(Request.Url.ToString());
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    if (Convert.ToBoolean(Session["error"]) == true)
    //    //        objCommon.ShowError(Page, "Domain.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
    //    //    else
    //    //        objCommon.ShowError(Page, "Server UnAvailable");
    //    //}
    //}

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int as_no = int.Parse(btnEdit.CommandArgument);

            RepeaterItem lst = btnEdit.NamingContainer as RepeaterItem;
            Label lbl = lst.FindControl("lblTitle") as Label;
            Label lblsq = lst.FindControl("lblSeqNo") as Label;
            Label lblstatus = lst.FindControl("lblstatus") as Label;
            string pageurl = Request.Url.ToString() + "&action=edit&title=" + lbl.Text + "&as_no=" + as_no + "&as_seq_no=" + lblsq.Text + "&Status=" + lblstatus.Text;
            Response.Redirect(pageurl);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "domain.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            //Check whether to add or update
            if (Request.QueryString["action"] != null)
            {
                //Set all properties
                Acc_SectionController objAC = new Acc_SectionController();
                Acc_Section objAS = new Acc_Section();
                bool status =false;
                objAS.As_Title = txtDomainName.Text.Trim();
                if (txtSqnNo.Text.Trim() != "")
                    objAS.As_SrNo = Convert.ToDecimal(txtSqnNo.Text.Trim());
                else
                    objAS.As_SrNo = 0;

                if (hfdStat.Value == "true")
                {
                    status = true;
                }
                else
                {
                    status = false;
                     
                }

                if (Request.QueryString["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objAC.AddAcc_Section(objAS, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.UpdShow, "Record Saved Successfully.", this.Page);
                        BindListViewDomain();
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                    }
                    Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                }
                else
                {
                    //Edit Domain
                    if (Request.QueryString["as_no"] != null)
                    {
                        objAS.As_No = Convert.ToInt32(Request.QueryString["as_no"].ToString());

                        CustomStatus cs = (CustomStatus)objAC.UpdateAcc_Section(objAS, status);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(this.UpdShow, "Record Updated Successfully.", this.Page);
                            //BindListViewDomain();
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            //this.ClearQueryString();
                        }
                        else
                        {
                        }
                        Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "domain.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        int index = Request.Url.ToString().IndexOf("&action");
        string pageurl = Request.Url.ToString().Remove(index);
        Response.Redirect(pageurl);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString() + "&action=add");
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain
        BindListViewDomain();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }

    private void ClearQueryString()
    {
        PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
        isreadonly.SetValue(this.Request.QueryString, false, null);
        //this.Request.QueryString.Remove("action");
        //this.Request.QueryString.Remove("title");
        this.Request.QueryString.Remove("as_no");
        this.Request.QueryString.Remove("as_seq_no");
        this.Request.QueryString.Remove("Status");
    }


    protected void btnConnect_Click(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("reff", "DEV_PASS", "", "", "");
        string pass = ds.Tables[0].Rows[0]["DEV_PASS"].ToString();
        string db_pwd = clsTripleLvlEncyrpt.DecryptPassword(pass);
        if (txtPass.Text.Trim() == db_pwd)
        {
            popup.Visible = false;
            Session["AuthFlag"] = 1;
            BindListViewDomain();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "javascript:window.close();", true);
        }
        else
            objCommon.DisplayMessage("Password does not match!", this.Page);
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            Response.Redirect("~/principalHome.aspx", false);
        }
        else if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "14")
        {
            Response.Redirect("~/studeHome.aspx", false);
        }
        else if (Session["usertype"].ToString() == "3")
        {
            Response.Redirect("~/homeFaculty.aspx", false);
        }
        else if (Session["usertype"].ToString() == "5")
        {
            Response.Redirect("~/homeNonFaculty.aspx", false);
        }
        else
        {
            Response.Redirect("~/home.aspx", false);
        }
    }
}
