using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;

public partial class ESTABLISHMENT_LEAVES_Transactions_LeaveDocumentDown : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeave = new LeavesController();

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
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    Page.Title = Session["coll_name"].ToString();

                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    pnlStatusList.Visible = false;
                    CheckPageAuthorization();

                }
            }

        }
        catch (Exception ex)
        {
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Holidays.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Holidays.aspx");
        }
    }


    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindLVLeaveApprStatusAll();
        }
        catch (Exception ex)
        {
        }
    }

    protected void BindLVLeaveApprStatusAll()
    {
        try
        {
            DateTime FDATE = Convert.ToDateTime(txtFromdt.Text);
            DateTime TDATE = Convert.ToDateTime(txtTodt.Text);

            DataSet ds = objLeave.GetAllLeaveDocument(FDATE, TDATE);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpPager.Visible = false;
            }
            else
            {
                //dpPager.Visible = true;
            }
            lvApprStatus.DataSource = ds;
            lvApprStatus.DataBind();
            pnlStatusList.Visible = true;
            //btnHidePanel.Visible = true;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    public string GetFileNamePath(object filename, object letrno, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/LEAVE_CERTIFICATE_DOCUMENT/" + idno.ToString() + "/LETRNO_" + letrno + "." + extension[1].ToString().Trim());
        else
            return "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtFromdt.Text = txtTodt.Text = string.Empty;
            pnlStatusList.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }
}