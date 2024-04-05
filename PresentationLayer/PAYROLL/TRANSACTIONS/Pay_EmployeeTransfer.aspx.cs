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


public partial class PAYROLL_TRANSACTIONS_Pay_EmployeeTransfer : System.Web.UI.Page
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
                Session["usertype"] == null || Session["userfullname"] == null)
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
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                //   ShowEmpDetails();   
                ViewState["action"] = "add";

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));

                string CollegeNo = objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + Convert.ToInt32(Session["idno"]));

                ViewState["usertype"] = ua_type;
               // txttransferdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                FillDropDown();
                if (ViewState["usertype"].ToString() != "1")
                {
                    //CHECK ACTIVITY FOR EXAM REGISTRATION

                    objCommon.FillDropDownList(ddlAuthoUser, "[dbo].[PAYROLL_RESIGNATION_PASSING_AUTHORITY] A", "A.REGPASSID", "A.PANAME", "A.COLLEGE_NO=" + CollegeNo + " AND PASSTYPE=2", "");
                    DivSerach.Visible = false;
                    pnlId.Visible = true;
                    ShowEmpDetails(Convert.ToInt32(Session["idno"]));
                  //  txttransferdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                  //  txttransferdate.Enabled = false;
                   
                }
                else if (ViewState["usertype"].ToString() == "1")
                {
                    //pnlId.Visible = true;
                   
                    //txttransferdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                   // txttransferdate.Enabled = false;
                }
            }
        }
        else
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_EmployeeTransfer.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_EmployeeTransfer.aspx");
        }
    }
    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlnewcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
    }
    protected void Btnsubmit_Click(object sender, EventArgs e)
    {
        int cs = 0;
        try
        {
                if (ViewState["action"] != null)
                {
                    if (txttransferdate.Text != "") //yes
                    {
                        objEM.EMPTRANSFERDATE = Convert.ToDateTime(txttransferdate.Text);
                    }
                    else //no
                    {
                        //objEM.RESIGNATIONDATE = DBNull.Value;
                    }
                    if (txtTransferremark.Text != "")
                    {
                        objEM.EMPTRANSFERRESASON = txtTransferremark.Text.Trim();
                    }
                    else
                    {
                        objEM.EMPTRANSFERRESASON = "";
                    }
                    //objEM.IdNo = Convert.ToInt32(lblIDNo.Text.Trim());
                 
                    objEM.EMPTRANSFERIDNO = Convert.ToInt32(ViewState["IDNO"]);
                    objEM.OLDEMPCOLLEGENO = Convert.ToInt32(txtcollegeno.Text.Trim());
                    objEM.NEWEMPCOLLEGENO = Convert.ToInt32(ddlnewcollege.SelectedValue);
                    objEM.EMPTRANSFERRESASON = txtTransferremark.Text.Trim();
                    objEM.UA_NO = Convert.ToInt32(Session["userno"]);
                    objEM.USER_UATYPE = Convert.ToInt32(Session["usertype"]);
                    //HERE UPDATE THE EMPLOYEE ASSET DETAILE
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        objEM.EMPLOYEETRANSFERID = 0;
                        cs = Convert.ToInt32(objECC.SaveEmployeeTransfertoCollege(objEM));
                        if (cs == 1)
                        {
                            string MSG = "Records Saved Sucessfully";
                            MessageBox(MSG);
                            BindEmployeeTransferDetails();
                            ddlnewcollege.SelectedIndex = 0;
                            txtTransferremark.Text = "";
                        }
                        else if (cs == 2627)
                        {
                            string MSG = "Employee available in Selected College";
                            MessageBox(MSG);
                        }
                        else 
                        {
                            string MSG = "Records Saved Failed";
                            MessageBox(MSG);
                        }
                    }
                    else
                    {
                        objEM.EMPLOYEETRANSFERID = EmpTransferId;
                        cs = Convert.ToInt32(objECC.UpdateEmployeeTransfertoCollege(objEM));
                        if (cs == 2)
                        {
                            string MSG = "Records Updated Sucessfully";
                            MessageBox(MSG);
                            BindEmployeeTransferDetails();
                            ddlnewcollege.SelectedIndex = 0;
                            txtTransferremark.Text = "";
                        }
                        else
                        {
                            string MSG = "Records Saved Failed";
                            MessageBox(MSG);
                        }

                    }
                }
         
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_EmployeeResignation.aspx.Btnsubmit_Click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void Search_Click(object sender, System.EventArgs e)
    {
        string searchtext = string.Empty;
        string category = string.Empty;

        if (rdoSelection.SelectedValue == "IDNO")
        {
            category = "IDNO";
        }
        else if (rdoSelection.SelectedValue == "NAME")
        {
            category = "NAME";
        }
        else if (rdoSelection.SelectedValue == "PFILENO")
        {
            category = "PFILENO";
        }
        else if (rdoSelection.SelectedValue == "EMPLOYEEID")
        {
            category = "EMPLOYEEID";
        }

        searchtext = txtSearch.Text.ToString();

        DataTable dt = objECC.RetrieveEmpDetailsForaSsetAllotment(searchtext, category);
        if (dt.Rows.Count > 0)
        {
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }
    }
    protected void btnCanceModal_Click(object sender, System.EventArgs e)
    {

    }
    protected void lnkId_Click(object sender, System.EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;

        ViewState["IDNO"] = lblIDNo.Text = lnk.CommandArgument.ToString();

        DivSerach.Visible = false;
        ShowEmpDetails(Convert.ToInt32(ViewState["IDNO"].ToString()));
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    private void ShowEmpDetails(int idno)
    {
        EmpCreateController objECC = new EmpCreateController();

        DataTableReader dtr = objECC.ShowEmpDetails(idno);
        if (dtr != null)
        {
            if (dtr.Read())
            {

                pnlId.Visible = true;
                ViewState["IDNO"] = dtr["idno"].ToString();
                lblIDNo.Text = dtr["EmployeeId"].ToString();
                txtcollegeno.Text = dtr["COLLEGE_NO"].ToString();
                // lblEmpcode.Text = dtr["seq_no"].ToString();
                lbltitle.Text = dtr["title"].ToString();
                lblFName.Text = dtr["fname"].ToString();
                lblMname.Text = dtr["mname"].ToString();
                lblLname.Text = dtr["lname"].ToString();
                lblDepart.Text = dtr["SUBDEPT"].ToString();
                lblDesignation.Text = dtr["SUBDESIG"].ToString();
                objCommon.FillDropDownList(ddlAuthoUser, "[dbo].[PAYROLL_RESIGNATION_PASSING_AUTHORITY] A", "A.REGPASSID", "A.PANAME", "A.COLLEGE_NO=" + txtcollegeno.Text + "", "");
                lblMob.Text = dtr["PHONENO"].ToString();
                lblEmail.Text = dtr["EMAILID"].ToString();
                lblcollegename.Text = dtr["COLLEGE_NAME"].ToString();
                txtNoticePrirod.Text = dtr["Notice_Period"].ToString();
                imgPhoto.ImageUrl = "../../showimage.aspx?id=" + dtr["idno"].ToString() + "&type=emp";
                imgPhoto.Visible = true;
                lblDOJ.Text = Convert.ToDateTime(dtr["DOJ"]).ToString("dd/MM/yyyy");

            }
            BindEmployeeTransferDetails();
        }
        else
        {
            objCommon.DisplayMessage("Employee Not Found !!", this.Page);
            pnlId.Visible = false;
            imgPhoto.Visible = false;
        }
    }
    public void BindEmployeeTransferDetails()
    {
        bool status;
        if (Session["usertype"] != null)
        {
            DataSet ds = null;
            if (Session["usertype"].ToString() == "1")
            {
                ds = objECC.GetAllEmpTransferDetail(Convert.ToInt32(ViewState["IDNO"]));
            }
            else
            {
                ds = objECC.GetAllEmpTransferDetail(Convert.ToInt32(ViewState["IDNO"]));
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmp.DataSource = ds.Tables[0];
                lvEmp.DataBind();
            }
            else
            {
                lvEmp.DataSource = null;
                lvEmp.DataBind();
            }
        }
        else
        {
            return;
        }
    }
    static int EmpTransferId = 0;
    bool status;
    string strstatus;
    protected void btneditasset_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;

            EmpTransferId = int.Parse(btnEdit.CommandArgument);

            // ShowDetails(NoDuesNo);
            DataSet ds = objECC.GetAllEmpTransferDetailByID(EmpTransferId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlnewcollege.SelectedValue = ds.Tables[0].Rows[0]["EmployeeNewCollegeId"].ToString();
                txtTransferremark.Text = ds.Tables[0].Rows[0]["Transfer_Remark"].ToString();
                txttransferdate.Text = ds.Tables[0].Rows[0]["EmployeeTransferDate"].ToString();
            }
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_EmployeeResignation.aspx.btneditasset_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

}