//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH       
// CREATION DATE : 13-FEB-2016
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class Health_Master_DoctorMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HelMasterController objHelMaster = new HelMasterController();
    Health objHel = new Health();

    #region Page Events
    /// <summary>
    /// This Page_Load event checks whether the user has login or not by checking Session["userno"],Session["username"]   
    /// </summary>
    /// 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtPhoneNo.Attributes.Add("onkeyUp", "return IsNumeric();");
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                        trAdd.Visible = false;
                    trView.Visible = true;
                    //objCommon.FillDropDownList(ddlDoctor, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO)", "E.IDNO", "E.PFILENO +' - '+ isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "UA_TYPE IN (5) AND P.PSTATUS = 'Y'", "E.PFILENO");
                    objCommon.FillDropDownList(ddlDoctor, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO)", "E.IDNO", "E.PFILENO +' - '+ isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "P.PSTATUS = 'Y'", "E.PFILENO");
                }
                BindListView(0);
                ViewState["action"] = "add";
            }

            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_DoctorMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    /// <summary>
    /// Page_PreInit event calls SetMasterPage() method.
    /// This method sets the theme to the master page.
    /// </summary>
    /// 
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    #endregion

    #region Actions

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        try
        {
            string IP = Request.ServerVariables["REMOTE_HOST"];

            string[] EmpName = ddlDoctor.SelectedItem.Text.Split('-');

            // objHel.DRNAME = txtDoctorName.Text;

            objHel.EMP_CODE = EmpName[0];
            objHel.DRNAME = txtDoctorName.Text; //EmpName[1];
            objHel.EMP_IDNO = 0;// Convert.ToInt32(ddlDoctor.SelectedValue);
            objHel.DEGREE = txtDegree.Text;
            objHel.DESIG = txtDesignation.Text;
            objHel.PHONE = txtPhoneNo.Text;
            objHel.ADDRESS = txtAddress.Text;
            if (rdbActive.Checked == true)
            {
                objHel.STATUS = 'Y';
            }
            else if (rdbNotActive.Checked == true)
            {
                objHel.STATUS = 'N';
            }
            objHel.HOSPITALNAME = txtHospitalName.Text;
            objHel.HADDRESS = txtHospitalAddress.Text;
            objHel.HPHONE = txtHospitalPhone.Text;
            objHel.IP_ADDRESS = IP;
            objHel.MAC_ADDRESS = ""; /// GetMacAddress(IP);
            objHel.COLLEGE_CODE = Session["colcode"].ToString();

            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                if (ViewState["DRID"] != null)

                    objHel.DRID = Convert.ToInt32(ViewState["DRID"]);

                //int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("HEALTH_DOCTORMASTER", " COUNT(*)", "EMP_IDNO=" + Convert.ToInt32(ddlDoctor.SelectedValue) + " and DRID <> " + Convert.ToInt32(ViewState["DRID"].ToString())));
                //if (duplicateCkeck == 0)
                //{
                CustomStatus cs = (CustomStatus)objHelMaster.UpdateHelDoctors(objHel);

                if (cs.Equals(CustomStatus.RecordUpdated))
                    //objCommon.DisplayMessage(upddoctorentries, "Record Updated Successfully", this.Page);
                    ClearControls();
                trAdd.Visible = false;
                trView.Visible = true;
                MessageBox("Record Updated Successfully");
                //}
                //else
                //{
                //    objCommon.DisplayMessage(upddoctorentries, "Record Already Exists.", this);
                //    return;
                //}

            }
            else if (ViewState["action"] != null && ViewState["action"].ToString().Equals("add"))
            {

                int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("HEALTH_DOCTORMASTER", " COUNT(*)", "DRNAME='" + txtDoctorName.Text + "'"));
                if (duplicateCkeck == 0)
                {
                    CustomStatus cs = (CustomStatus)objHelMaster.AddHelDoctor(objHel);
                    if (cs.Equals(CustomStatus.RecordSaved))
                        //objCommon.DisplayMessage(upddoctorentries, "Record Saved Successfully", this.Page);
                        ClearControls();
                    trAdd.Visible = false;
                    trView.Visible = true;
                    MessageBox("Record Saved Successfully");
                }
                else
                {
                    //objCommon.DisplayMessage(upddoctorentries, "Record Already Exists.", this);
                    ClearControls();
                    trAdd.Visible = false;
                    trView.Visible = true;
                    MessageBox("Record  Already Exists.");
                    return;
                }
            }
            //trAdd.Visible = false;
            //trView.Visible = true;
            //ClearControls();
            ViewState["action"] = "add";
            BindListView(0);
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_DoctorMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        this.ClearControls();
    }

    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        this.ClearControls();
        trAdd.Visible = false;
        trView.Visible = true;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int DRID = int.Parse(btnEdit.CommandArgument);
            ShowDetail(DRID);
            ViewState["action"] = "edit";
            trAdd.Visible = true;
            trView.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_DoctorMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //this.ClearControls();
        trAdd.Visible = true;
        trView.Visible = false;
        ViewState["action"] = "add";
    }

    protected void Report_Click(object sender, EventArgs e)
    {
        string IP = Request.ServerVariables["REMOTE_HOST"];
        string MAC_ADDRESS = ""; // GetMacAddress(IP);
        ShowReport("Doctor", "rpt_DoctorList.rpt", IP, MAC_ADDRESS);
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListView(0);
    }

    #endregion

    #region Private Methods
    /// <summary>
    /// CheckPageAuthorization() method checks whether the user is authorised to access this Page
    /// If he is not authorised, user will be redirected to "notauthorized.aspx" page and message is displayed that "You Are Not Authorized To Use this page".
    /// </summary>
    /// 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CreateOperator.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CreateOperator.aspx");
        }
    }

    /// <summary>
    /// BindListView() method fetches all the records from HEL_DOCTORMASTER table with the help of GetDoctorByDRID() method.
    /// These records are bind to the listview "lvDoctor".
    /// </summary>
    /// 
    private void BindListView(int DRID)
    {
        try
        {
            DataSet ds = objHelMaster.GetDoctorByDRID(DRID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvDoctor.DataSource = ds.Tables[0];
                lvDoctor.DataBind();
            }
            else
            {
                lvDoctor.DataSource = null;
                lvDoctor.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_DoctorMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    /// <summary>
    /// GetMacAddress() method accepts the ipAddress and returns the MacAddres .
    /// </summary>
    /// 
    //public string GetMacAddress(string IPAddress)
    //{
    //    string strMacAddress = string.Empty;
    //    try
    //    {
    //        string strTempMacAddress = string.Empty;
    //        ProcessStartInfo objProcessStartInfo = new ProcessStartInfo();
    //        Process objProcess = new Process();
    //        objProcessStartInfo.FileName = "nbtstat";
    //        objProcessStartInfo.RedirectStandardInput = false;
    //        objProcessStartInfo.RedirectStandardOutput = true;
    //        objProcessStartInfo.Arguments = "-A " + IPAddress;
    //        objProcessStartInfo.UseShellExecute = false;
    //        objProcess = Process.Start(objProcessStartInfo);
    //        int Counter = -1;
    //        while (Counter <= -1)
    //        {
    //            Counter = strTempMacAddress.Trim().ToLower().IndexOf("mac address", 0);
    //            if (Counter > -1)
    //            {
    //                break;
    //            }
    //            strTempMacAddress = objProcess.StandardOutput.ReadLine();
    //        }
    //        objProcess.WaitForExit();
    //        strMacAddress = strTempMacAddress.Trim();
    //    }
    //    catch (Exception Ex)
    //    {
    //        //Console.WriteLine(Ex.ToString());
    //        //Console.ReadLine();
    //    }
    //    return strMacAddress;
    //}

    private void ClearControls()
    {
        txtAddress.Text = string.Empty;
        txtDegree.Text = string.Empty;
        txtDesignation.Text = string.Empty;
        txtDoctorName.Text = string.Empty;
        txtHospitalAddress.Text = string.Empty;
        txtHospitalName.Text = string.Empty;
        txtHospitalPhone.Text = string.Empty;
        txtPhoneNo.Text = string.Empty;
        rdbActive.Checked = true;
        rdbNotActive.Checked = false;
        ddlDoctor.SelectedIndex = 0;
        ViewState["DRID"] = null;

    }

    private void ShowDetail(int DRID)
    {
        try
        {
            ViewState["DRID"] = DRID;
            DataSet ds = objHelMaster.GetDoctorByDRID(DRID);
            if (ds.Tables[0].Rows.Count != null)
            {

                txtAddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                txtDegree.Text = ds.Tables[0].Rows[0]["DEGREE"].ToString();
                txtDesignation.Text = ds.Tables[0].Rows[0]["DESIG"].ToString();
                txtDoctorName.Text = ds.Tables[0].Rows[0]["DRNAME"].ToString();
                txtHospitalAddress.Text = ds.Tables[0].Rows[0]["HADDRESS"].ToString();
                txtHospitalName.Text = ds.Tables[0].Rows[0]["HOSPITALNAME"].ToString();
                txtHospitalPhone.Text = ds.Tables[0].Rows[0]["HPHONE"].ToString();
                txtPhoneNo.Text = ds.Tables[0].Rows[0]["PHONE"].ToString();
                if ((ds.Tables[0].Rows[0]["STATUS"].ToString()) == "Y")
                {
                    rdbActive.Checked = true;
                    rdbNotActive.Checked = false;
                }
                else if ((ds.Tables[0].Rows[0]["STATUS"].ToString()) == "N")
                {
                    rdbActive.Checked = false;
                    rdbNotActive.Checked = true;
                }
                ddlDoctor.SelectedValue = ds.Tables[0].Rows[0]["EMP_IDNO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_DoctorMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    /// <summary>
    /// ShowReport() method displays the report.
    /// It calls the "CommonReport.aspx" page.
    /// Page title and parameters like Session["userfullname"] are passed to the "CommonReport.aspx" page.
    /// </summary>
    /// 
    private void ShowReport(string reportTitle, string rptFileName, string IPAddress, string MacAddress)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("HEALTH")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            // url += "&param=@P_DRID=0,username=" + Session["userfullname"].ToString() + ",IP_ADDRESS=" + IPAddress + ",MAC_ADDRESS=" + MacAddress + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
            url += "&param=@P_DRID=0";
            ScriptManager.RegisterClientScriptBlock(upddoctorentries, upddoctorentries.GetType(), "Window", "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_DoctorMaster.ShowReport ->" + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    #endregion

    protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDoctor.SelectedIndex > 0)
            {
                DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDESIG D ON (E.SUBDESIGNO = D.SUBDESIGNO)", "IDNO", "isnull(Title,'')+' '+isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'')  AS NAME, D.SUBDESIG, E.PHONENO", "IDNO=" + ddlDoctor.SelectedValue, "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtDoctorName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();  //   ddlDoctor.SelectedItem.Text; // 
                    //txtDegree.Text = ds.Tables[0].Rows[0][""].ToString();
                    txtDesignation.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
                    txtPhoneNo.Text = ds.Tables[0].Rows[0]["PHONENO"].ToString();

                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_DoctorMaster.ShowReport ->" + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }


    }


    public string GetStatus(object status)
    {
        string s = Convert.ToString(status);
        if (s == "ACTIVE")
            return "<span style='color:Green'>ACTIVE</span>";
        else
            return "<span style='color:Red'>NOT ACTIVE</span>";
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}