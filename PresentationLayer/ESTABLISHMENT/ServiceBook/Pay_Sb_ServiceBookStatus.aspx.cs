using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_ServiceBookStatus : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    public int _idnoEmp;

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    //To set Master Page
    //    if (Session["masterpage"] != null)
    //        objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
    //    else
    //        objCommon.SetMasterPage(Page, "");

    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //Check Session
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();
            }

        }
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BindListViewStatus();
    }

    protected void lvStatus_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;
            Label lblsts = (Label)e.Item.FindControl("lblStatus");

            if (rowView["STATUS"].ToString() == "1")
            {
                lblsts.Text = "SUBMIT";
                lblsts.ToolTip = "SUBMIT";
            }
            else
            {
                lblsts.Text = "NOT-SUBMIT";
                lblsts.ToolTip = "NOT-SUBMIT";
            }
            string per = rowView["PER"].ToString();
            lblPer.Text = "Percentage : " + per + "%";
            lblPer.ToolTip = rowView["PER"].ToString() + "%";
        }
    }

    protected void BindListViewStatus()
    {
        DataSet ds = null;
        if (_idnoEmp != 0)
        {
            int emp_UserType = int.Parse(objCommon.LookUp("PAYROLL_EMPMAS", "UA_TYPE", "IDNO = '" + _idnoEmp + "'"));
            // ds = objAttandance.GetStatus(Convert.ToInt32(ddlstaff.SelectedValue));
            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                ds = objServiceBook.BindServiceBookStatus(emp_UserType, _idnoEmp);
            }
            else
            {
                ds = objServiceBook.BindServiceBookStatus(Convert.ToInt32(Session["usertype"]), _idnoEmp);
            }
            lvStatus.DataSource = ds;
            lvStatus.DataBind();
        }
    }
}