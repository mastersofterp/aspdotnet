using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class STORES_Transactions_Str_ToolKit_Allocation : System.Web.UI.Page
{


    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    STR_DEPT_REQ_CONTROLLER objStrDept = new STR_DEPT_REQ_CONTROLLER();
    STR_INDENT objstrInd = new STR_INDENT();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
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
                    //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAllo.Visible = false;
                GetRefNo();
                //ItemSubGroup();
                BindToolKit();
                BindYear();
                BindDegree();


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
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }



    private void GetRefNo()
    {
        int deptno = Convert.ToInt32(Session["strdeptcode"]);
        DataSet ds = new DataSet();

        ds = objStrDept.ToolkitRefNo(deptno);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtRefNo.Text = ds.Tables[0].Rows[0]["REFNO"].ToString();
            }
        }
    }

    private void BindToolKit()
    {
        objCommon.FillDropDownList(ddltoolkit, "Store_ToolKit", "TK_NO", "TK_NAME", "", "TK_NAME");
    }


    private void BindYear()
    {
        objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "", "YEAR");
    }



    private void BindDegree()
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_Degree", "DEGREENO", "DEGREENAME", "DegreeNO NOT IN (0)", "DEGREENAME");
    }


    private void GetSitudents()
    {
        int yearno = Convert.ToInt32(ddlYear.SelectedValue);
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        DataSet ds = new DataSet();

        ds = objStrDept.GetStudentList(yearno, degreeno);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvitemInvoice.DataSource = ds;
                lvitemInvoice.DataBind();
                lvitemInvoice.Visible = true;
                pnlAllo.Visible = true;
                
            }
            else
            {
                lvitemInvoice.DataSource = null;
                lvitemInvoice.DataBind();
                lvitemInvoice.Visible = false;
                objCommon.DisplayMessage("Data Not Found", Page);
                pnlAllo.Visible = false;
            }
        }
        else
        {
            lvitemInvoice.DataSource = null;
            lvitemInvoice.DataBind();
            lvitemInvoice.Visible = false;
            objCommon.DisplayMessage("Data Not Found", Page);
            pnlAllo.Visible = false;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            GetSitudents();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int yearno = Convert.ToInt32(ddlYear.SelectedValue);
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int toolkit = Convert.ToInt32(ddltoolkit.SelectedValue);
        int collegeid = Convert.ToInt32(Session["colcode"]);
        string str = string.Empty;
        int count = 0;
        foreach (ListViewItem i in lvitemInvoice.Items)
        {
            CheckBox chk = i.FindControl("chkSelect") as CheckBox;
            if (chk.Checked == true)
            {
                str += chk.ToolTip.ToString() + ",";
                count++;
            }
        }
        if (count == 0)
        {
            objCommon.DisplayMessage("Please Select Atleast One Student", Page);
            return;
        }
        int result = objStrDept.InsertToolKitAlllocation(yearno, degreeno, toolkit, collegeid, str.Trim(','));

        if (result > 0)
        {
            objCommon.DisplayMessage("Record Saved Successfully", Page);
            Clear();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        lvitemInvoice.Visible = false;
        ddlDegree.SelectedValue = "0";
        ddlYear.SelectedValue = "0";
        ddltoolkit.SelectedValue = "0";
        GetRefNo();
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
    }
}