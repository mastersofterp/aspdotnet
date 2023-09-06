//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : ESATE
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 29-SEP-2016
// DESCRIPTION   : ARREAR SUBMISSION FORM
//=========================================================================
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
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

public partial class ESTATE_Transaction_ArrearSubmission : System.Web.UI.Page
{
    Common objcommon = new Common();
    BillPayment objB = new BillPayment();
    BillPaymentController objBillCon = new BillPaymentController();

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
                CheckPageAuthorization();
                BindListView();
            }
        }
        divMsg.InnerHtml = string.Empty;
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
    
    protected void btnreset_Click(object sender, EventArgs e)
    {
        BindListView();
    }  

    protected void BindListView()
    {
        try
        {
           // string eDate = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");
            DataSet ds = null;
            ds = objBillCon.GetFirstBillProcessData();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                btnSubmitData.Enabled = true;
                lvBillProcess.DataSource = ds;
                lvBillProcess.DataBind();              
                trshowbutton.Visible = true;
            }
            else
            {
                lvBillProcess.DataSource = null ;
                lvBillProcess.DataBind();
                trshowbutton.Visible = false;
            }
           
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    //this is used to submit data
    protected void btnSubmitData_Click(object sender, EventArgs e)
    {
        try
        {         
            DataTable ArrearTbl = new DataTable("ArrTbl");
            ArrearTbl.Columns.Add("PID", typeof(int));
            ArrearTbl.Columns.Add("QA_ID", typeof(int));
            ArrearTbl.Columns.Add("ARREAR", typeof(string));
            ArrearTbl.Columns.Add("ARREAR_INTEREST", typeof(string));

            DataRow dr = null;
            foreach (ListViewItem i in lvBillProcess.Items)
            {
                HiddenField QA_ID = (HiddenField)i.FindControl("hdnQAID");
                Label lblPID = (Label)i.FindControl("lblname");
                TextBox txtA = (TextBox)i.FindControl("txtArrear");
                TextBox txtInst = (TextBox)i.FindControl("txtArrearInst");

                dr = ArrearTbl.NewRow();
                dr["PID"] = lblPID.ToolTip;
                dr["QA_ID"] = QA_ID.Value;
                dr["ARREAR"] = txtA.Text;
                dr["ARREAR_INTEREST"] = txtInst.Text;

                ArrearTbl.Rows.Add(dr);
              
            }
            objB.ARREAR_TABLE = ArrearTbl;

            CustomStatus cs = (CustomStatus)objBillCon.InsertArrear(objB);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objcommon.DisplayMessage(this.updpnl, "Record Save Successfully.", this.Page);
            }

            else
            {
                objcommon.DisplayMessage(this.updpnl, "Sorry!Transaction Fail.", this.Page);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }  
}