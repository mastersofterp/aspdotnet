using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public partial class ESTABLISHMENT_LEAVES_Transactions_ClassArragnmentAcceptance : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();
    Leaves objLM = new Leaves();
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
                CheckPageAuthorization();

                //if (Request.QueryString["pageno"] != null)
                //{
                //    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                //int usernock = Convert.ToInt32(Session["userno"]);
                pnllistPending.Visible = true;
                BindLVClassApplPendingList();
                BindClassAcceptedChargeList();
            }
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
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

    protected void BindLVClassApplPendingList()
    {
        try
        {
            DataSet ds = objApp.GetPendListforClassApproval(Convert.ToInt32(Session["idno"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                DivNotePendingList.Visible = false;
                lvPendingList.DataSource = ds;
                lvPendingList.DataBind();
            }
            else
            {
                DivNotePendingList.Visible = true;
                lvPendingList.DataSource = null;
                lvPendingList.DataBind();
            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void BindClassAcceptedChargeList()
    {
        try
        {
            DataSet ds = objApp.GetAppectListforClassArrangementApproval(Convert.ToInt32(Session["idno"]));

            //DivStatusNote,pnlListAccepted

            if (ds.Tables[0].Rows.Count > 0)
            {
                //lvAcceptedList.Visible = true;
                DivNotePendingList.Visible = false;
                lvAcceptedList.DataSource = ds;
                lvAcceptedList.DataBind();
            }
            else
            {
                DivNotePendingList.Visible = true;
                lvAcceptedList.DataSource = null;
                lvAcceptedList.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Charge_Acceptance.BindPendingAcceptance ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        objLM.SRNO = Convert.ToInt32(Session["SRNO"]);
        objLM.STATUS = ddlSelect.SelectedValue.ToString().Trim();
        objLM.APP_REMARKS = txtRemarks.Text.ToString().Trim();

        CustomStatus cs = (CustomStatus)objApp.AddUpdateclassChargeAcceptance(objLM);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            MessageBox("Record Saved Successfully");

            BindLVClassApplPendingList();
            BindClassAcceptedChargeList();
            pnlListAccepted.Visible = pnllistPending.Visible = true;
            pnlAdd.Visible = false;
            txtRemarks.Text = string.Empty;
            ddlSelect.SelectedValue = "A";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        lblEmpName.Text = string.Empty;
        lblsubject.Text = string.Empty;
        lblDate.Text = string.Empty;
        lblPeriod.Text = string.Empty;
        ddlSelect.SelectedIndex = 0;
        txtRemarks.Text = string.Empty;
        lblCourse.Text = string.Empty;
        lblClass.Text = string.Empty;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlListAccepted.Visible = pnllistPending.Visible = true;
        pnlAdd.Visible = false;
        ddlSelect.SelectedIndex = 0;
        txtRemarks.Text = string.Empty;
    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {

            Button btnApproval = sender as Button;
            int SRNO = int.Parse(btnApproval.CommandArgument);
            // objLM.CHTRNO = int.Parse(btnApproval.CommandArgument);
            Session["SRNO"] = SRNO;


            pnlListAccepted.Visible = pnllistPending.Visible = false;
            pnlAdd.Visible = true;
            ShowDetails(SRNO);

            //objLM.CHTRNO = int.Parse(btnApproval.CommandArgument);
            //objLM.STATUS = "A".ToString().Trim();

            //CustomStatus cs = (CustomStatus)objLC.AddUpdateChargeAcceptance(objLM);
            //if (cs.Equals(CustomStatus.RecordSaved))
            //{
            //    MessageBox("Record Saved Successfully");

            //    BindPendingAcceptance();
            //    BindAcceptedChargeList();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Charge_Acceptance.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    private void ShowDetails(Int32 SRNO)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = objApp.GetClassHandoverDetailsBySrno(SRNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Boolean IsRequiredLoad = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsRequiredLoad"].ToString());
                if (IsRequiredLoad == true)
                {
                    lblEmpName.Text = ds.Tables[0].Rows[0]["APPLICANTNAME"].ToString();
                    //lblsubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();//LEAVE_NAME               
                    lblDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DATE"]).ToString("dd-MMM-yyyy");//LEAVE_NAME
                    lblPeriod.Text = ds.Tables[0].Rows[0]["TIME"].ToString();
                    divSubject.Visible = false;
                    divClass.Visible = true;
                    divCourse.Visible = true;
                    lblClass.Text = ds.Tables[0].Rows[0]["CLASS"].ToString();
                    lblCourse.Text = ds.Tables[0].Rows[0]["COURSE_NAME"].ToString();
                    lblsubject.Text = string.Empty;
                }
                else
                {
                    lblEmpName.Text = ds.Tables[0].Rows[0]["APPLICANTNAME"].ToString();
                    lblsubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();//LEAVE_NAME               
                    lblDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DATE"]).ToString("dd-MMM-yyyy");//LEAVE_NAME

                    lblPeriod.Text = ds.Tables[0].Rows[0]["TIME"].ToString();
                    divSubject.Visible = true;
                    divClass.Visible = false;
                    divCourse.Visible = false;
                    lblClass.Text = string.Empty;
                    lblCourse.Text = string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    //protected void lnkViewStatus_Click(object sender, EventArgs e)
    //{
    //    if (lnkViewStatus.Text == "View Accepted Status".ToString().Trim())
    //    {
    //        lnkViewStatus.Text = "Hide Accepted Status".ToString().Trim();
    //        //  BindAcceptedChargeList();
    //        // lvAcceptedList.Visible = true;

    //    }
    //    else
    //    {
    //        lnkViewStatus.Text = "View Accepted Status".ToString().Trim();
    //        // lvAcceptedList.Visible = false;

    //    }      
    //}
}