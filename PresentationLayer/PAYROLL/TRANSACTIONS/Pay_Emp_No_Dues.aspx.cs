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



public partial class PAYROLL_TRANSACTIONS_Pay_Emp_Nodues : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpMaster objEM = new EmpMaster();
    EmpCreateController objECC = new EmpCreateController();

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
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //   ShowEmpDetails();   
                ViewState["action"] = "add";
                BindEmployeeList();

            }
        }
        else
        {

        }

    }

    private void ShowStauts()
    {
        string Authoid = "";
        // select * from  where IDNO=@P_IDNO
        Authoid = Convert.ToString(objCommon.LookUp("PAYROLL_NODUES_AUTHORITY_TYPE_EMP_DETAIL", "AUTHO_TYP_ID", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString())));

        if (Authoid == "" || Authoid == "0")
        {
            objCommon.DisplayMessage("Authority Type Not Map to Employee !!", this.Page);
            ViewState["AuthoTypeId"] = Authoid.ToString();
            DivSerach.Visible = true;
            pnlId.Visible = false;
            return;
        }
        else
        {
            ViewState["AuthoTypeId"] = Authoid.ToString();
            DivSerach.Visible = false;
            pnlId.Visible = true;
        }

    }


    private void ShowEmpDetails(int idno)
    {
        EmpCreateController objECC = new EmpCreateController();

        DataTableReader dtr = objECC.ShowEmpDetails(idno);
        if (dtr != null)
        {
            if (dtr.Read())
            {
                ShowStauts();
                // pnlId.Visible = true;

                // objCommon.FillDropDownList(ddlAuthorityType, "PAYROLL_NODUES_AUTHORITY_TYPE", "AUTHO_TYP_ID", "AUTHORITY_TYP_NAME", "", "AUTHO_TYP_ID");
                // ddlAuthorityType.SelectedIndex = 1;
                if (ViewState["AuthoTypeId"].ToString() != "")
                {
                    objCommon.FillDropDownList(ddlAuthorityName, "[dbo].[PAYROLL_NODUES_AUTHORITY_DETAIL] D LEFT join [dbo].[PAYROLL_NODUES_AUTHORITY_NAME] A ON A.AUTHO_ID=D.AUTHO_ID", "A.AUTHO_ID", "A.AUTHORITY_NAME", "AUTHO_TYP_ID=" + ViewState["AuthoTypeId"].ToString() + " AND D.ISACTIVE=1", "A.AUTHO_ID");

                    if (Session["usertype"].ToString() == "1")
                    {
                        ddlAuthorityName.Enabled = true;
                    }
                    else
                    {
                        string AuthNameId = "";
                        AuthNameId = Convert.ToString(objCommon.LookUp("[dbo].[PAYROLL_NODUES_AUTHORITY_DETAIL] D LEFT join [dbo].[PAYROLL_NODUES_AUTHORITY_NAME] A ON A.AUTHO_ID=D.AUTHO_ID", "A.AUTHO_ID", "AUTHO_TYP_ID=" + ViewState["AuthoTypeId"].ToString() + " AND isnull(AUTHO_UA_NO,0)=" + Convert.ToInt32(Session["userno"].ToString()) + " AND D.ISACTIVE=1"));
                        if (AuthNameId != "")
                        {
                            ddlAuthorityName.SelectedValue = AuthNameId;
                            ddlAuthorityName.Enabled = false;
                        }
                        else
                        {
                            objCommon.DisplayMessage("Department Head Not Mapped to Authority!!", this.Page);
                            DivSerach.Visible = true;
                            pnlId.Visible = false;
                            return;
                        }
                    }

                    ViewState["IDNO"] = lblIDNo.Text = dtr["idno"].ToString();
                    lblEmpcode.Text = dtr["EmployeeId"].ToString();
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


            }
            BindNoDuesDetails();
        }
        else
        {
            objCommon.DisplayMessage("Employee Not Found !!", this.Page);
            pnlId.Visible = false;
            imgPhoto.Visible = false;
        }
    }



    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Emp_No_Dues.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Emp_No_Dues.aspx");
        }
    }


    protected void ddlAuthoName_SelectedIndexChanged(object sender, EventArgs e)
    {

        //      Select isnull(ISACTIVE,0) ISACTIVE, A.AUTHO_ID,AUTHORITY_NAME
        //from [dbo].[PAYROLL_NODUES_AUTHORITY_DETAIL] D LEFT join [dbo].[PAYROLL_NODUES_AUTHORITY_NAME] A ON A.AUTHO_ID=D.AUTHO_ID
        objCommon.FillDropDownList(ddlAuthorityName, "[dbo].[PAYROLL_NODUES_AUTHORITY_DETAIL] D LEFT join [dbo].[PAYROLL_NODUES_AUTHORITY_NAME] A ON A.AUTHO_ID=D.AUTHO_ID", "A.AUTHO_ID", "A.AUTHORITY_NAME", "AUTHO_TYP_ID=" + ViewState["AuthoTypeId"].ToString() + " AND  D.ISACTIVE=1", "A.AUTHO_ID");

    }

    public string GetIPAddress()
    {
        IPHostEntry Host = default(IPHostEntry);
        string Hostname = null;
        Hostname = System.Environment.MachineName;
        Host = Dns.GetHostEntry(Hostname);
        foreach (IPAddress IP in Host.AddressList)
        {
            if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                ipAddress = Convert.ToString(IP);
            }
        }
        return ipAddress;
    }

    string ipAddress = string.Empty;
    protected void Btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {


            //Select Count(*) from PAY_EMP_NODUES_ENTRY where IDNO=123 


            if (txtRemark.Text == "" && ddlNoDues.SelectedIndex == 0)
            {
                objCommon.DisplayMessage("Please Select or Enter Proper Details!!", this.Page);
                ddlNoDues.Focus();
                return;
            }
            else
            {
                if (ViewState["action"] != null)
                {

                    if (ddlNoDues.SelectedValue.ToString() == "1") //yes
                    {
                        objEM.Nodues_Status = 1;
                    }
                    else //no
                    {
                        objEM.Nodues_Status = 0;
                    }

                    objEM.Remark = txtRemark.Text;
                    objEM.IdNo = Convert.ToInt32(ViewState["IDNO"].ToString());

                    objEM.Created_By = Convert.ToInt32(Session["userno"]);
                    objEM.IPADDRESS = GetIPAddress();

                    objEM.AUTHO_ID = Convert.ToInt32(ddlAuthorityName.SelectedValue);
                    objEM.AUTHO_TYPE_ID = Convert.ToInt32(ViewState["AuthoTypeId"].ToString());

                    if (ViewState["action"].ToString().Equals("edit"))
                    {
                        //int Authoid = 0;
                        //Authoid = Convert.ToInt32(objCommon.LookUp("PAY_EMP_NODUES_ENTRY", "AUTHO_ID", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString()) + " and NODUES_NO=" + Convert.ToInt32(NoDuesNo)));

                        //if (Authoid != (Convert.ToInt32(ddlAuthorityName.SelectedValue)))
                        //{
                        //    int idnocount3 = 0;
                        //    idnocount3 = Convert.ToInt32(objCommon.LookUp("PAY_EMP_NODUES_ENTRY", "COUNT(1)", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString()) + " and AUTHO_ID=" + Convert.ToInt32(ddlAuthorityName.SelectedValue)));
                        //    if (idnocount3 > 0)
                        //    {
                        //        objCommon.DisplayMessage("Please select another authority name , this authority name already enter by others !!", this.Page);
                        //        ddlAuthorityName.Focus();
                        //        return;
                        //    }
                        //}


                        objEM.Nodues_No = NoDuesNo;
                        string output = objECC.UpdateNoDuesEntry(objEM);
                        if (output != "-99")
                        {
                            objCommon.DisplayMessage("No Dues Details Modified Successfully!!", this.Page);
                            ViewState["action"] = "add";
                            ShowEmpDetails(Convert.ToInt32(ViewState["IDNO"].ToString()));

                            ddlNoDues.SelectedIndex = 0;
                            txtRemark.Text = "";
                            ddlAuthorityName.SelectedIndex = 0;
                            ddlAuthorityType.SelectedIndex = 1;
                        }
                        else
                            objCommon.DisplayMessage("Error!!", this.Page);
                    }
                    else
                    {
                        int idnocount = 0;
                        idnocount = Convert.ToInt32(objCommon.LookUp("PAY_EMP_NODUES_ENTRY", "COUNT(1)", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString()) + " and AUTHO_ID=" + Convert.ToInt32(ddlAuthorityName.SelectedValue)));
                        if (idnocount > 0)
                        {
                            objCommon.DisplayMessage("Please select another authority name , this authority name already enter by others !!", this.Page);
                            ddlAuthorityName.Focus();
                            return;
                        }

                        string output = objECC.InsertNoDuesEntry(objEM);
                        if (output != "-99")
                        {
                            if (output == "1")
                            {
                                objCommon.DisplayMessage("No Dues Details Added Successfully!!", this.Page);
                                ShowEmpDetails(Convert.ToInt32(ViewState["IDNO"].ToString()));
                                ddlNoDues.SelectedIndex = 0;
                                txtRemark.Text = "";
                                ddlAuthorityName.SelectedIndex = 0;
                                ddlAuthorityType.SelectedIndex = 1;
                            }
                            else
                            {
                                objCommon.DisplayMessage("No Dues Details Already Added!!", this.Page);
                                ShowEmpDetails(Convert.ToInt32(ViewState["IDNO"].ToString()));
                                ddlNoDues.SelectedIndex = 0;
                                txtRemark.Text = "";
                                ddlAuthorityName.SelectedIndex = 0;
                                ddlAuthorityType.SelectedIndex = 1;
                            }
                        }
                        else
                            objCommon.DisplayMessage("Error!!", this.Page);
                    }
                }
            }
        }
        catch { }
    }


    private void BindEmployeeList()
    {
        DataTable dt = objECC.RetrieveEmpDetailsNoDuesNew("AAA", "ALLEMPLOYEE");
        if (dt.Rows.Count > 0)
        {
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
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

        DataTable dt = objECC.RetrieveEmpDetailsNoDuesNew(searchtext, category);
        if (dt.Rows.Count > 0)
        {
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }

        //DataTableReader dtr = objECC.RetrieveEmpDetailsNoDues(searchtext, category);
        // if (dtr != null)
        // {
        //     if (dtr.Read())
        //     {
        //         ViewState["IDNO"] = lblIDNo.Text = dtr["idno"].ToString();
        //         ShowEmpDetails(Convert.ToInt32( ViewState["IDNO"].ToString()));
        //     }
        // }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;

        ViewState["IDNO"] = lblIDNo.Text = lnk.CommandArgument.ToString();

        DivSerach.Visible = false;
        ShowEmpDetails(Convert.ToInt32(ViewState["IDNO"].ToString()));

    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void BtnCancelSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }


    public void BindNoDuesDetails()
    {
        if (Session["usertype"] != null)
        {
            DataSet ds = null;
            if (Session["usertype"].ToString() == "1")
            {
                ds = objECC.GetAllNoDuesDetails(Convert.ToInt32(ViewState["IDNO"]));
            }
            else
            {
                ds = objECC.GetUANOWiseNoDuesDetails(Convert.ToInt32(Session["userno"]), Convert.ToInt32(ViewState["IDNO"]));
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
    //to show no dues details on edit button
    private void ShowDetails(int NoDuesNo)
    {
        try
        {
            DataSet dsdues = objECC.GetNoDuesNOWiseDetails(NoDuesNo);
            if (dsdues.Tables[0].Rows.Count > 0)
            {
                if (dsdues.Tables[0].Rows[0]["NODUES_STATUS"].ToString() == "0")
                {
                    ddlNoDues.SelectedValue = "2"; //No
                }
                else
                {
                    ddlNoDues.SelectedValue = "1"; //Yes
                }


                txtRemark.Text = dsdues.Tables[0].Rows[0]["REMARK"] == DBNull.Value ? "" : dsdues.Tables[0].Rows[0]["REMARK"].ToString();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_NoDuesEntry.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    static int NoDuesNo = 0;
    protected void btnEditNoDues_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            NoDuesNo = int.Parse(btnEdit.CommandArgument);

            // ShowDetails(NoDuesNo);
            DataSet dsdues = objECC.GetNoDuesNOWiseDetails(NoDuesNo);
            if (dsdues.Tables[0].Rows.Count > 0)
            {
                if (dsdues.Tables[0].Rows[0]["NODUES_STATUS"].ToString() == "0")
                {
                    ddlNoDues.SelectedValue = "2"; //No
                }
                else
                {
                    ddlNoDues.SelectedValue = "1"; //Yes
                }


                txtRemark.Text = dsdues.Tables[0].Rows[0]["REMARK"] == DBNull.Value ? "" : dsdues.Tables[0].Rows[0]["REMARK"].ToString();
                //ddlAuthorityType.SelectedValue=dsdues.Tables[0].Rows[0]["AUTHO_TYP_ID"].ToString();
                objCommon.FillDropDownList(ddlAuthorityName, "[dbo].[PAYROLL_NODUES_AUTHORITY_DETAIL] D LEFT join [dbo].[PAYROLL_NODUES_AUTHORITY_NAME] A ON A.AUTHO_ID=D.AUTHO_ID", "A.AUTHO_ID", "A.AUTHORITY_NAME", "AUTHO_TYP_ID=" + ViewState["AuthoTypeId"].ToString() + " AND D.ISACTIVE=1", "A.AUTHO_ID");
                //ddlAuthoName_SelectedIndexChanged(sender,e);
                ddlAuthorityName.SelectedValue = dsdues.Tables[0].Rows[0]["AUTHO_ID"].ToString();

            }


            ViewState["action"] = "edit";
            ddlNoDues.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_NoDuesEntry.btnEditNoDues_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

}



