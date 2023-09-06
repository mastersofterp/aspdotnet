using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Transactions_Estb_Leave_Taken : System.Web.UI.Page
{
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objlvtaken = new LeavesController();

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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //pnlAdd.Visible = false;
                //pnlList.Visible = true;
                //BindListViewEmployees();
            }

        }
        //blank div tag
        divMsg.InnerHtml = string.Empty;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            int empno = Convert.ToInt32(Session["idno"]);
            DateTime fdate = Convert.ToDateTime(txtFromDt.Text);
            DateTime tdate = Convert.ToDateTime(txtToDt.Text);
            DataSet ds = objlvtaken.EmployeeLeaveTakenDetails(empno,fdate,tdate);
            lvEmpLVTaken.DataSource = ds.Tables[0];
            lvEmpLVTaken.DataBind();
            

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.BindListViewHolidays -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void dpODinfo_PreRender(object sender, EventArgs e)
    {
       // BindListViewODStatus();
    }
    protected void txtToDt_TextChanged(object sender, EventArgs e)
    {
        DateTime fdt = Convert.ToDateTime(txtFromDt.Text.Trim()).Date;
        DateTime tdt = Convert.ToDateTime(txtToDt.Text.Trim()).Date;

        if (fdt > tdt)
        {
            MessageBox("From Date should be greater than To Date!");
            btnShow.Enabled = false;
        }
        else
        {
            btnShow.Enabled = true;
        }

    }

    //export grid view to excel file
    private void Export()
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=LeaveTakenDetails.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        lvEmpLVTaken.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnExportExl_Click(object sender, EventArgs e)
    {
        this.Export();
    }
}
