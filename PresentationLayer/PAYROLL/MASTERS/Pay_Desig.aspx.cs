using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PayRoll_Pay_Designation : System.Web.UI.Page
{
    Common objCommon = new Common();
    PayDesignationMas objDes = new PayDesignationMas();
    PayDesignationController objDesig = new PayDesignationController();

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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
                pnlSelect.Visible = true;
                BindListViewList(Convert.ToInt32(ddlStaffType.SelectedValue));
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Desig.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Desig.aspx");
        }
    }

    private void BindListViewList(Int32 staffno)
    {
        pnlList.Visible = true;
        DataSet ds = objDesig.GetDesigList(staffno);
        lvDesignation.DataSource = ds;
        lvDesignation.DataBind();

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int srno = int.Parse(btnEdit.CommandArgument);
        ShowDetails(srno);
    }

    private void ShowDetails(int srno)
    {
        foreach (ListViewDataItem item in lvDesignation.Items)
        {
            int staffNo;
            ImageButton imgNUDESIGNO = (ImageButton)item.FindControl("btnEdit");
            Label lblDesignation = (Label)item.FindControl("lblNUDesig");
            Label lblStaffType = (Label)item.FindControl("lblStaffType");
            Label lblShort = (Label)item.FindControl("lblNUDesigShort");
            if (Convert.ToInt32(imgNUDESIGNO.CommandArgument) == srno)
            {
                ViewState["srno"] = srno.ToString();
                staffNo=Convert.ToInt32(objCommon.LookUp("payroll_staff","staffno","staff='"+ lblStaffType.Text+"'"));
                ddlStaffType.SelectedValue=staffNo.ToString();
                txtDesig.Text = lblDesignation.Text;
                txtDesigShort.Text = lblShort.Text;
            }
        }
    }


    private void Clear()
    {
        txtDesigShort.Text = string.Empty;
        txtDesig.Text =string.Empty;
        ddlStaffType.SelectedIndex = 0;
        ViewState["srno"] = null; 
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objDes.DESIGNO = ViewState["srno"] == null ? 0 : Convert.ToInt32(ViewState["srno"].ToString());
        objDes.DESIGNATION = txtDesig.Text.ToUpper().Trim();
        objDes.DESIGSHORT = txtDesigShort.Text.ToUpper().Trim();
        objDes.STAFFNO = Convert.ToInt32(ddlStaffType.SelectedValue);
        objDes.COLLEGECODE = Session["colcode"].ToString();

        CustomStatus cs = (CustomStatus)objDesig.AddUpdateDesig(objDes);
        Clear();
        BindListViewList(Convert.ToInt32(ddlStaffType.SelectedValue));
    }
    protected void ddlStaffType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewList(Convert.ToInt32(ddlStaffType.SelectedValue));
    }
}
