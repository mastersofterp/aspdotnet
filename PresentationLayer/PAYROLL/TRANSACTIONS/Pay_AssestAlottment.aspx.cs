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


public partial class PAYROLL_TRANSACTIONS_Pay_AssestAlottment : System.Web.UI.Page
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
        if(!Page.IsPostBack)
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
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //   ShowEmpDetails();   
                ViewState["action"] = "add";

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() != "1")
                {
                    //CHECK ACTIVITY FOR EXAM REGISTRATION
                    DivSerach.Visible = false;
                    pnlId.Visible = true;
                    ShowEmpDetails(Convert.ToInt32(Session["idno"]));
                }
                else if (ViewState["usertype"].ToString() == "1")
                {
                    //pnlId.Visible = true;
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_AssestAlottment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_AssestAlottment.aspx");
        }
    }


    protected void Search_Click(object sender, EventArgs e)
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

        searchtext = txtSearch.Text.ToString();

        DataTable dt = objECC.RetrieveEmpDetailsForaSsetAllotment(searchtext, category);
        if (dt.Rows.Count > 0)
        {
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }
    }
    protected void btnCanceModal_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        ViewState["IDNO"] = lblIDNo.Text = lnk.CommandArgument.ToString();
        DivSerach.Visible = false;
        ShowEmpDetails(Convert.ToInt32(ViewState["IDNO"].ToString()));

    }
    
    protected void Btnsubmit_Click(object sender, EventArgs e)
    {
        int cs = 0;
        try
        {
            if (txtassestsremark.Text == "" && ddlasseststype.SelectedIndex == 0)
            {
                string MSG = "Please Select or Enter Proper Details!!";
                MessageBox(MSG);
                return;
            }
            else
            {
                if (ViewState["action"] != null)
                {
                    if (ddlasseststype.SelectedIndex > 0) //yes
                    {
                        objEM.ASSETID = Convert.ToInt32(ddlasseststype.SelectedValue);
                        objEM.ASSETNAME = ddlasseststype.SelectedItem.ToString();
                    }
                    else //no
                    {
                        objEM.ASSETID = 0;
                        //objEM.ASSETNAME = "";
                    }
                    if (txtassestsremark.Text != "")
                    {
                        objEM.ASSETREMARK = txtassestsremark.Text.Trim();
                    }
                    else
                    {
                        objEM.ASSETREMARK = "";
                    }
                   // objEM.ISAPPROVED = false;
                    objEM.IdNo = Convert.ToInt32(ViewState["IDNO"]);
                    objEM.COLLEGE_NO = Convert.ToInt32(txtcollegeno.Text.Trim());
                    //HERE UPDATE THE EMPLOYEE ASSET DETAILE
                    if (ViewState["action"].ToString().Equals("edit"))
                    {
                        objEM.ISAPPROVED = status;
                        objEM.ASSETALLOTID = Assetallotid;
                        cs = Convert.ToInt32(objECC.UpdateEmployeeAssetAllotment(objEM));
                        if (cs == 1)
                        {
                            string MSG = "Records Updated Successfully";
                            MessageBox(MSG);
                            BindAssetAllotmentDetails();
                            txtassestsremark.Text = "";
                            ddlasseststype.SelectedIndex = 0;
                        }
                        else
                        {
                            string MSG = "Records Updated Failed";
                            MessageBox(MSG);
                        }                       
                    }
                    else
                    {
                        cs = Convert.ToInt32(objECC.SaveEmployeeAssetAllotment(objEM));
                        if(cs == 0)
                        {
                            string MSG = "You already apply for selected Asset";
                            MessageBox(MSG);
                        }
                       if (cs == 1)
                        {
                            string MSG = "Records Saved Successfully";
                            MessageBox(MSG);
                            BindAssetAllotmentDetails();
                            txtassestsremark.Text = "";
                            ddlasseststype.SelectedIndex = 0;
                        }
                        else
                        {
                            string MSG = "Records Saved Failed";
                            MessageBox(MSG);
                        
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_AssestAlottment.aspx.Btnsubmit_Click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable"); 

        }
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
               // ViewState["IDNO"] = lblIDNo.Text = dtr["idno"].ToString();
                ViewState["IDNO"] =   dtr["idno"].ToString();
                lblIDNo.Text = dtr["EmployeeId"].ToString();
                txtcollegeno.Text = dtr["COLLEGE_NO"].ToString();
               // lblEmpcode.Text = dtr["seq_no"].ToString();
                lbltitle.Text = dtr["title"].ToString();
                lblFName.Text = dtr["fname"].ToString();
                lblMname.Text = dtr["mname"].ToString();
                lblLname.Text = dtr["lname"].ToString();
                lblDepart.Text = dtr["SUBDEPT"].ToString();
                lblDesignation.Text = dtr["SUBDESIG"].ToString();
                lblMob.Text = dtr["PHONENO"].ToString();
                lblEmail.Text = dtr["EMAILID"].ToString();
                if (dtr["DOJ"] != "")
                {
                    lblDOJ.Text = Convert.ToDateTime(dtr["DOJ"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    lblDOJ.Text = "";
                }
                imgPhoto.ImageUrl = "../../showimage.aspx?id=" + dtr["idno"].ToString() + "&type=emp";
                imgPhoto.Visible = true;


            }
            BindAssetAllotmentDetails();
        }
        else
        {
            objCommon.DisplayMessage("Employee Not Found !!", this.Page);
            pnlId.Visible = false;
            imgPhoto.Visible = false;
        }
    }
    public void BindAssetAllotmentDetails()
    {
        if (Session["usertype"] != null)
        {
            DataSet ds = null;
            if (Session["usertype"].ToString() == "1")
            {
                ds = objECC.GetAllEmpAssetDetail(Convert.ToInt32(ViewState["IDNO"]));
            }
            else
            {
                ds = objECC.GetAllEmpAssetDetail(Convert.ToInt32(ViewState["IDNO"]));
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstEmpAsset.DataSource = ds.Tables[0];
                lstEmpAsset.DataBind();
            }
            else
            {
                lstEmpAsset.DataSource = null;
                lstEmpAsset.DataBind();
            }
        }
        else
        {

            return;
        }
    }
    
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }
    static int Assetallotid = 0;
    static bool status;
    static  string strstatus;
    protected void btneditasset_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
             ImageButton btnEdit = sender as ImageButton;
             Assetallotid = int.Parse(btnEdit.CommandArgument);
            DataSet dsdues = objECC.GetAllEmpAssetDetailIDWISE(Assetallotid);
            if (dsdues.Tables[0].Rows[0]["ISAPPROVED_DETAILS"].ToString() == "APPROVED")
            {

                string message = "Your Application is already Approved.";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("');");
              //  ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sb.ToString(), true);
            }
            else
            {
                txtassestsremark.Text = dsdues.Tables[0].Rows[0]["ASSETREMARK"] == DBNull.Value ? "" : dsdues.Tables[0].Rows[0]["ASSETREMARK"].ToString();
                ddlasseststype.SelectedValue = dsdues.Tables[0].Rows[0]["ASSETID"].ToString();
                ViewState["action"] = "edit";
                ddlasseststype.Focus();
            }
        }
       catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_AssestAlottment.aspx.btneditasset_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        //try
        //{
        //    ImageButton btnEdit = sender as ImageButton;

        //    Assetallotid = int.Parse(btnEdit.CommandArgument);

        //    // ShowDetails(NoDuesNo);
        //    DataSet dsdues = objECC.GetAllEmpAssetDetailIDWISE(Assetallotid);
        //    if (dsdues.Tables[0].Rows.Count > 0)
        //    {
        //        if (dsdues.Tables[0].Rows[0]["ISAPPROVED_DETAILS"].ToString() == "PENDING")
        //        {
        //            status = false; //No
        //            strstatus = dsdues.Tables[0].Rows[0]["ISAPPROVED_DETAILS"].ToString();
        //            if (status == false)
        //            {
        //                txtassestsremark.Text = dsdues.Tables[0].Rows[0]["ASSETREMARK"] == DBNull.Value ? "" : dsdues.Tables[0].Rows[0]["ASSETREMARK"].ToString();
        //                ddlasseststype.SelectedValue = dsdues.Tables[0].Rows[0]["ASSETID"].ToString();

        //            }
        //        }
        //        else if (dsdues.Tables[0].Rows[0]["ISAPPROVED_DETAILS"].ToString() == "APPROVED")
        //        {
        //             status = true; //Yes
        //             strstatus = dsdues.Tables[0].Rows[0]["ISAPPROVED_DETAILS"].ToString();
        //            if (status == true)
        //            {
        //                txtassestsremark.Text = dsdues.Tables[0].Rows[0]["ASSETREMARK"] == DBNull.Value ? "" : dsdues.Tables[0].Rows[0]["ASSETREMARK"].ToString();
        //                ddlasseststype.SelectedValue = dsdues.Tables[0].Rows[0]["ASSETID"].ToString();

        //            }
        //        }
        //        ViewState["action"] = "edit";
        //        ddlasseststype.Focus();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_AssestAlottment.aspx.btneditasset_Click-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }
   
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindAssetAllotmentDetails();
    }

 
   
}