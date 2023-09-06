using System;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Net;
using System.Data.OleDb;
using System.Data.Common;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class PAYROLL_TRANSACTIONS_ApprovalofEmployeeResignation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpMaster objEM = new EmpMaster();
    EmpCreateController objECC = new EmpCreateController();
    int collegeno;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Making panels visible
                pnlSelection.Visible = true;
                BindEmployeeResignationDetails();

            }
        }
    }

    public void BindEmployeeResignationDetails()
    {
        if (Session["usertype"] != null)
        {
            DataSet ds = null;
            if (Session["usertype"].ToString() == "1")
            {
                ds = objECC.GetAllEmpResignationDetailALL();

            }
            else
            {
                ds = objECC.GetAllEmpResignationDetailALL();

            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmployeeresignation.DataSource = ds.Tables[0];
                lvEmployeeresignation.DataBind();
            }
            else
            {
                lvEmployeeresignation.DataSource = null;
                lvEmployeeresignation.DataBind();
            }
        }
        else
        {

            return;
        }
    }

    //protected void btnrejected_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int cs = 0;
    //        int empresignationid;
    //        Button btnapprove = sender as Button;
    //        empresignationid = int.Parse(btnapprove.CommandArgument);
    //        DataSet ds = objECC.GetAllEmpResignationIDWISE(empresignationid);

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            objEM.IdNo = Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"]);
    //            objEM.COLLEGE_NO = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_NO"]);
    //            objEM.RESIGNATIONDATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["RESIGNATIONDATE"]);
    //            objEM.RESIGNATIONREMARK = ds.Tables[0].Rows[0]["RESIGNATIONREMARK"].ToString();
    //            objEM.REGSTATUS = false;
    //            objEM.RESIGNATIONEMPID = empresignationid;
    //            cs = Convert.ToInt32(objECC.UpdateEmployeeAssetAllotment(objEM));
    //            if (cs == 1)
    //            {
    //                string MSG = "Status Updated Sucessfully";
    //                MessageBox(MSG);
    //                BindEmployeeResignationDetails();
    //            }
    //            else
    //            {
    //                string MSG = "Records Saved Failed";
    //                MessageBox(MSG);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_ApprovalofEmployeeResignation.aspx.btnrejected_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void Search_Click(object sender, System.EventArgs e)
    {
        if (Session["usertype"] != null)
        {
            DataSet ds = null;
            if (Session["usertype"].ToString() == "1")
            {
                DateTime fromdate = Convert.ToDateTime(txtfromdate.Text.Trim());
                DateTime todate = Convert.ToDateTime(txttodate.Text.Trim());
                ds = objECC.GetAllEmpResignationDetailDatewise(fromdate, todate);
            }
            else
            {
                DateTime fromdate = Convert.ToDateTime(txtfromdate.Text.Trim());
                DateTime todate = Convert.ToDateTime(txttodate.Text.Trim());
                ds = objECC.GetAllEmpResignationDetailDatewise(fromdate, todate);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmployeeresignation.DataSource = ds.Tables[0];
                lvEmployeeresignation.DataBind();
            }
            else
            {
                lvEmployeeresignation.DataSource = null;
                lvEmployeeresignation.DataBind();
            }
        }
        else
        {

            return;
        }
    }
    protected void dpPager_PreRender(object sender, System.EventArgs e)
    {

    }
    protected void lvEmployeeresignation_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        int cs;
        int empresignationid;
        if (e.CommandName == "Accept")
        {
            TextBox txtnoticemonth = (TextBox)e.Item.FindControl("txtnoticeperiod");
            if (txtnoticemonth.Text == "")
            {
                string msg = "Enter Notice period in Month";
                MessageBox(msg);
            }
            else if (txtnoticemonth.Text != "")
            {
                try
                {
                    // Button btnapprove = sender as Button;
                    empresignationid = Convert.ToInt32(e.CommandArgument);
                    DataSet ds = objECC.GetAllEmpResignationIDWISE(empresignationid);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objEM.IdNo = Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"]);
                        objEM.COLLEGE_NO = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_NO"]);
                        objEM.RESIGNATIONDATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["RESIGNATIONDATE"]);
                        objEM.RESIGNATIONREMARK = ds.Tables[0].Rows[0]["RESIGNATIONREMARK"].ToString();
                        objEM.REGSTATUS = true;
                        objEM.RESIGNATIONEMPID = empresignationid;
                        objEM.PHRRESIGNATIONREMARK = "";
                        objEM.PNOTICEPERIOD = Convert.ToInt32(txtnoticemonth.Text);
                        objEM.NUDESIGNO = 0;

                        cs = Convert.ToInt32(objECC.UpdateEmployeeResignationNew(objEM));
                        if (cs == 1)
                        {
                            string MSG = "Resignation Accepted Sucessfully";
                            MessageBox(MSG);
                            BindEmployeeResignationDetails();
                        }
                        else
                        {
                            string MSG = "Records Saved Failed";
                            MessageBox(MSG);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_ApprovalofEmployeeResignation.aspx.btnapproved_Click-> " + ex.Message + " " + ex.StackTrace);
                    else
                        objUCommon.ShowError(Page, "Server UnAvailable");
                }
            }
        }
        else if (e.CommandName == "Reject")
        {
            try
            {
                // Button btnapprove = sender as Button;
                empresignationid = Convert.ToInt32(e.CommandArgument);
                DataSet ds = objECC.GetAllEmpResignationIDWISE(empresignationid);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    objEM.IdNo = Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"]);
                    objEM.COLLEGE_NO = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_NO"]);
                    objEM.RESIGNATIONDATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["RESIGNATIONDATE"]);
                    objEM.RESIGNATIONREMARK = ds.Tables[0].Rows[0]["RESIGNATIONREMARK"].ToString();
                    objEM.REGSTATUS = false;
                    objEM.RESIGNATIONEMPID = empresignationid;
                    objEM.PHRRESIGNATIONREMARK = "";
                    objEM.PNOTICEPERIOD = 0;
                    objEM.NUDESIGNO = 0;

                    cs = Convert.ToInt32(objECC.UpdateEmployeeResignationNew(objEM));
                    if (cs == 1)
                    {
                        string MSG = "Resignation Rejected Sucessfully";
                        MessageBox(MSG);
                        BindEmployeeResignationDetails();
                    }
                    else
                    {
                        string MSG = "Records Saved Failed";
                        MessageBox(MSG);
                    }
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_ApprovalofEmployeeResignation.aspx.btnapproved_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }

        }
        //else if (e.CommandName == "View")
        //{

        //    int regid = Convert.ToInt32(e.CommandArgument);
        //    DataSet ds = objECC.GetAllEmpResignationIDWISE(regid);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //       // objEM.RESIGNATIONDATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["RESIGNATIONDATE"]);
        //        txtresignationdate.Text = ds.Tables[0].Rows[0]["RESIGNATIONDATE"].ToString();

        //       // objEM.RESIGNATIONREMARK = ds.Tables[0].Rows[0]["RESIGNATIONREMARK"].ToString();
        //        txtresignationresaon.Text = ds.Tables[0].Rows[0]["RESIGNATIONREMARK"].ToString();


        //      //  ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "showPopup();", true);

        //    }
        //    panel1modelextender.Show();
        //}
        //protected void lnkFake_Command(object sender, CommandEventArgs e)
        //{
        //    if (e.CommandName == "View")
        //    {
        //        int regid=Convert.ToInt32(e.CommandArgument);
        //        DataSet ds = objECC.GetAllEmpResignationIDWISE(regid);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            objEM.RESIGNATIONDATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["RESIGNATIONDATE"]);
        //            txtresignationdate.Text = objEM.REGRELEVINGDATE.ToString();

        //            objEM.RESIGNATIONREMARK = ds.Tables[0].Rows[0]["RESIGNATIONREMARK"].ToString();
        //            txtresignationremark.Text = objEM.RESIGNATIONREMARK.ToString();


        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "showPopup();", true);

        //        }
        //        Panel1_ModalPopupExtender.Show();

        //    }

        //}

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            panel1modelextender.Show();
            Button btnView = sender as Button;

            int regid = Convert.ToInt32(btnView.CommandArgument);
            DataSet ds = objECC.GetAllEmpResignationIDWISE(regid);
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtresignationdate.Text = ds.Tables[0].Rows[0]["RESIGNATIONDATE"].ToString();

                txtresignationresaon.Text = ds.Tables[0].Rows[0]["RESIGNATIONREMARK"].ToString();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_NoDuesEntry.btnEditNoDues_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ShowModal(object sender, EventArgs e)
    {
        // ListViewItem item = (sender as ImageButton).NamingContainer as ListViewItem;
        // lblresignationdate1.Text = (item.FindControl("lblresgnationdate") as Label).Text;
        panel1modelextender.Show();
    }
}