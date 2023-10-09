//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : TO CREATE FILE OF LOGGING DETAILS                               
// CREATION DATE : 20-APRIL-2009
// CREATED BY    : SHEETAL RAUT 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

//using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Administration_logDetais : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Bind the ListView with Notice
                if (Session["usertype"].ToString() == "1")
                {
                    Username.Visible = true;
                    dateuser.Visible = true;
                }
                else
                {
                    Username.Visible = false;
                    dateuser.Visible = false;
                }
               // BindListView();
                PopulateDropDownList();
                this.GetDropDownText();
            }

        }
        else
        {
            // Clear message div
            divMsg.InnerHtml = string.Empty;

            /// Check if postback is caused by reconcile chalan or delete chalan buttons
            /// if yes then call corresponding methods
            //if (Request.Params["__EVENTTARGET"] != null && Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            //{
            //    if (Request.Params["__EVENTTARGET"].ToString() == "GetDropDownText")
            //        this.GetDropDownText();

            //}
        }
    }
    public void GetDropDownText()
    {
        DataSet ds = objCommon.FillDropDown("USER_ACC", "DISTINCT UA_NO", "UA_NAME", "UA_NAME LIKE '%" + Convert.ToString(txtUsername.Text.Trim()) + "%'", "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlName.Items.Clear();
            ddlName.Items.Add("Please Select");
            ddlName.SelectedItem.Value = "0";
            ddlName.DataSource = ds;
            ddlName.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlName.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlName.DataBind();
        }
        else
        {
            ddlName.Items.Clear();
            ddlName.Items.Add("Please Select");
            ddlName.SelectedItem.Value = "0";

        }
    }
    private void PopulateDropDownList()
    {
        try
        {
            DataSet ds = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALL_USERS");
            ddlName.DataSource = ds;
            ddlName.DataTextField = ds.Tables[0].Columns[0].ToString();
            ddlName.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "logDetails.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        try
        {
            LogFile objLog = new LogFile();

            DataSet ds = LogTableController.GetAllLogFile(objLog,Session["username"].ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvList.DataSource = ds;
                lvList.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.WriteErrorDetails(ex.Message);
                objUCommon.ShowError(Page, "logDetails.BindListView-> " + ex.Message + " " + ex.StackTrace);
            }
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindDetailsListView(int LogId)
    {
        try
        {
            DataSet dsLogDetails = LogTableController.GetAllLogDetails(LogId);

            if (dsLogDetails.Tables.Count>0 && dsLogDetails.Tables[0].Rows.Count > 0)
            {
                lvDetails.DataSource = dsLogDetails;
                lvDetails.DataBind();
                ViewState["LogDetails"] = dsLogDetails;
                //lvList.Visible = false;
                lvDetails.Visible = true;
                lvList.Visible = false;
                divDetails.Visible = true;
               // dpPager.Visible = false;
               // Session["LogId"] = LogId;
                btnBack.Visible = true;
                btnExcelDetails.Visible = true;
            }
            else
            {
               // objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
                ClientScript.RegisterStartupScript(GetType(), "alert", "<script>alert('No Record Found')</script>");
                lvDetails.Visible = false;
                lvList.Visible = true;
                divDetails.Visible = false;
              //  dpPager.Visible = true;
                btnBack.Visible = false;
                btnExcelDetails.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.WriteErrorDetails(ex.Message);
                objUCommon.ShowError(Page, "logDetails.BindDetailsListView-> " + ex.Message + " " + ex.StackTrace);
            }
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewAll()
    {
        try
        {
            LogFile objLog = new LogFile();
            DataSet ds = LogTableController.GetAllLogFile(objLog, Session["username"].ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvList.DataSource = ds;
                lvList.DataBind();
                //lvDetails.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.WriteErrorDetails(ex.Message);
                objUCommon.ShowError(Page, "logDetails.BindListView-> " + ex.Message + " " + ex.StackTrace);
            }
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void txtUsername_TextChanged(object sender, EventArgs e)
    {

    }
    
    protected void btnShow_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    LogFile objLog = new LogFile();
        //    objLog.Ua_Name = ddlName.Text.Trim();
        //    SqlDataReader dr = LogTableController.GetLogDetail(objLog.Ua_Name);

        //    string filename = Server.MapPath("~/error/LogDetails.txt");
        //    using (StreamWriter sw = File.CreateText(filename))
        //    {
        //        // Add some text to the file
        //        sw.WriteLine(" LogFile");
        //        sw.WriteLine("=========");
        //        sw.WriteLine(); 
        //        //sw.WriteLine(" Date : " + DateTime.Today);
        //        //sw.WriteLine();
        //        sw.WriteLine(" User Name : " + objLog.Ua_Name);
        //        sw.WriteLine();
        //        sw.WriteLine(" Login Time \t\t\t Logout Time \t\t\t\t IP Address \t\t\t\t Mac Address");
        //        sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
        //        while (dr.Read())
        //        {
        //            sw.WriteLine(" " + dr["logintime"] + " \t\t " + dr["logouttime"] + " \t\t\t " + dr["IPADDRESS"] +" \t\t\t\t" + dr["MACADDRESS"]);
        //        }
        //        if (dr != null) dr.Close();
        //    }
        
        //    //Download
        //    //=======================================
        //    Response.Clear();
        //    Response.ContentType = "text/comma-separated-values";            
        //    Response.AddHeader("Content-Disposition", "attachment; filename=LogDetails.txt");
        //    Response.Flush();
        //    Response.WriteFile(Server.MapPath("~/error/LogDetails.txt"));
        //    Response.End();
        //    Response.Close();            
        //    //=======================================

        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "logDetails.btnShow_click-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}

        DateTime? FromDate;
        if (!string.IsNullOrEmpty(txtFromdt.Text.Trim()))
        {
            FromDate = Convert.ToDateTime(txtFromdt.Text);
        }
        else
        {
            FromDate = null;
        }

        DateTime? ToDate;
        if (!string.IsNullOrEmpty(txtTodt.Text.Trim()))
        {
            ToDate = Convert.ToDateTime(txtTodt.Text);
        }
        else
        {
            ToDate = null;
        }

        if (FromDate != null && ToDate != null)
        {
            if (Convert.ToDateTime(txtFromdt.Text) <= Convert.ToDateTime(txtTodt.Text))
            {
                lvList.DataSource = null;
                lvList.DataBind();
                LogFile objLog = new LogFile();
                DataSet ds1 = LogTableController.GetAllLogFileDate(objLog, ddlName.SelectedItem.ToString(), txtFromdt.Text, txtTodt.Text);

                if (ds1.Tables.Count>0 && ds1.Tables[0].Rows.Count > 0)
                {
                    lvList.DataSource = ds1;
                    lvList.DataBind();
                    lvList.Visible = true;
                   // dpPager.Visible = true;
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "<script>alert('No Record Found')</script>");
                    lvList.DataSource = null;
                    lvList.DataBind();
                  //  dpPager.Visible = false;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "<script>alert('From Date should be Smaller than To Date')</script>");
                lvList.DataSource = null;
                lvList.DataBind();
              //  dpPager.Visible = false;

            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "<script>alert('Please Enter From date as well as To date')</script>");
            lvList.DataSource = null;
            lvList.DataBind();
           // dpPager.Visible = false;
        }
    }

    protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LogFile objLog = new LogFile();
        //objLog.Ua_Name = ddlName.Text.Trim();
        //SqlDataReader dr = LogTableController.GetLogDetail(objLog.Ua_Name);      
    }
   
    protected void dpPager_PreRender(object sender, EventArgs e)
    {


        //LogFile objLog = new LogFile();
        //DataSet ds1 = new DataSet();
        //if (txtFromdt.Text == "")
        //{
        //    ds1 = LogTableController.GetAllLogFile(objLog, Session["username"].ToString());
        //}
        //else
        //{
        //     ds1 = LogTableController.GetAllLogFileDate(objLog, ddlName.SelectedItem.ToString(), txtFromdt.Text, txtTodt.Text);
        //}
        //if (ds1.Tables[0].Rows.Count > 0)
        //{
        //    lvList.DataSource = ds1;
        //    lvList.DataBind();
        //    dpPager.Visible = true;
        //    //dpPager2.Visible = false;
        //}
        //else
        //{
        //    lvList.DataSource = null;
        //    lvList.DataBind();
        //    dpPager.Visible = false;
        //}
    }
    //protected void dpPager_PreRenderDetails(object sender, EventArgs e)
    //{

    //    DataSet dsDetails = new DataSet();
    //    if (Session["LogId"] != null )
    //    {
    //        if (Convert.ToInt32(Session["LogId"]) != 0)
    //        dsDetails = LogTableController.GetAllLogDetails(Convert.ToInt32(Session["LogId"].ToString()));
    //    }
       
    //    if (dsDetails.Tables[0].Rows.Count > 0)
    //    {
    //       lvDetails.DataSource = dsDetails;
    //       lvDetails.DataBind();
    //        dpPager.Visible = false;
    //        dpPager2.Visible = true;
    //    }
    //    else
    //    {
    //        lvDetails.DataSource = null;
    //        lvDetails.DataBind();
    //        dpPager.Visible = false;
    //    }
    //}
    public string GetLogTime(object logtime)
    {
        if (!logtime.ToString().Equals(""))
        {
            DateTime dt = Convert.ToDateTime(logtime);
            return dt.ToLongDateString() + " " + dt.ToShortTimeString();
        }
        else
            return "Not Logged Off";
    }

    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    ShowReport();
    //}

    //private void ShowReport()
    //{
    //    //ReportDocument customerReport = new ReportDocument();
    //    //string reportPath = Server.MapPath("~") + "\\Reports\\" + "RptRegUser.rpt";

    //    //customerReport.Load(reportPath);
    //    //SQLHelper objSH = new SQLHelper(_uaims_constr);
    //    //DataSet ds = objSH.ExecuteDataSet("SELECT * FROM LOGFILE");
    //    //customerReport.SetDatabaseLogon("nitjprm", "nitjprm");
    //    //customerReport.SetDataSource(ds);
    //    //crViewer.ReportSource = customerReport;
    //}

    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        //Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=logdetails.aspx");
    //        }
    //    }
    //    else
    //    {
    //        //Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=logdetails.aspx");
    //    }
    //}
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnDetails_Click(object sender, EventArgs e)
    {
        string LogId = ((System.Web.UI.WebControls.Button)(sender)).CommandArgument.ToString();
        if (LogId != null)
        {
            if (Convert.ToInt32(LogId) != 0)
            {
                //updLogin.Visible = false;
                //lvList.Visible = false;
                pnlDetails.Visible = true;
                //lvDetails.Visible = true;
              
                BindDetailsListView(Convert.ToInt32(LogId));
                //ClientScript.RegisterStartupScript(GetType(), "alert", "<script>Modalbox.show($('divdemo2'){title: this.title, width: 600,overlayClose:false})</script>");

            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    //protected void btnDownload_Click(object sender, EventArgs e)
    //{
        #region Old Logic
    //try
    //{
    //    LogFile objLog = new LogFile();
    //    objLog.Ua_Name = ddlName.Text.Trim();
    //    SqlDataReader dr = LogTableController.GetLogDetail(objLog.Ua_Name);

    //    string filename = Server.MapPath("~/error/LogDetails.txt");
    //    using (StreamWriter sw = File.CreateText(filename))
    //    {
    //        // Add some text to the file
    //        sw.WriteLine(" LogFile");
    //        sw.WriteLine("=========");
    //        sw.WriteLine(); 
    //        //sw.WriteLine(" Date : " + DateTime.Today);
    //        //sw.WriteLine();
    //        sw.WriteLine(" User Name : " + objLog.Ua_Name);
    //        sw.WriteLine();
    //        sw.WriteLine(" Login Time \t\t\t Logout Time \t\t\t\t IP Address \t\t\t\t Mac Address");
    //        sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
    //        while (dr.Read())
    //        {
    //            sw.WriteLine(" " + dr["logintime"] + " \t\t " + dr["logouttime"] + " \t\t\t " + dr["IPADDRESS"] +" \t\t\t\t" + dr["MACADDRESS"]);
    //        }
    //        if (dr != null) dr.Close();
    //    }

    //    //Download
    //    //=======================================
    //    Response.Clear();
    //    Response.ContentType = "text/comma-separated-values";            
    //    Response.AddHeader("Content-Disposition", "attachment; filename=LogDetails.txt");
    //    Response.Flush();
    //    Response.WriteFile(Server.MapPath("~/error/LogDetails.txt"));
    //    Response.End();
    //    Response.Close();            
    //    //=======================================

    //}
    //catch (Exception ex)
    //{
    //    if (Convert.ToBoolean(Session["error"]) == true)
    //        objUCommon.ShowError(Page, "logDetails.btnDownload_Click-> " + ex.Message + " " + ex.StackTrace);
    //    else
    //        objUCommon.ShowError(Page, "Server UnAvailable");
    //}
    #endregion Old Logic  

    //    try
    //    {
    //        if (Convert.ToDateTime(txtFromdt.Text) <= Convert.ToDateTime(txtTodt.Text))
    //        {
    //            LogFile objLog = new LogFile();

    //            objLog.Ua_Name = ddlName.Text.Trim();
    //            //SqlDataReader dr = LogTableController.GetLogDetail(objLog.Ua_Name);
    //            DataSet ds1 = LogTableController.GetAllLogFileDate(objLog, ddlName.SelectedItem.ToString(), txtFromdt.Text, txtTodt.Text);

    //            if (ds1 != null)
    //            {
    //                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
    //                {
    //                    DataTableReader dr = null;
    //                    dr = ds1.Tables[0].CreateDataReader();
    //                    string filename = Server.MapPath("~/error/LogDetails.txt");
    //                    using (StreamWriter sw = File.CreateText(filename))
    //                    {
    //                        // Add some text to the file
    //                        sw.WriteLine(" LogFile");
    //                        sw.WriteLine("=========");
    //                        sw.WriteLine();
    //                        //sw.WriteLine(" Date : " + DateTime.Today);
    //                        //sw.WriteLine();
    //                        sw.WriteLine(" User Name : " + objLog.Ua_Name);
    //                        sw.WriteLine();
    //                        sw.WriteLine(" Login Time \t\t\t Logout Time \t\t\t\t IP Address \t\t\t\t Mac Address");
    //                        sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
    //                        while (dr.Read())
    //                        {
    //                            sw.WriteLine(" " + dr["logintime"] + " \t\t " + dr["logouttime"] + " \t\t\t " + dr["IPADDRESS"] + " \t\t\t\t" + dr["MACADDRESS"]);
    //                        }
    //                        if (dr != null) dr.Close();
    //                    }

    //                    //Download
    //                    //=======================================
    //                    Response.Clear();
    //                    Response.ContentType = "text/comma-separated-values";
    //                    Response.AddHeader("Content-Disposition", "attachment; filename=LogDetails.txt");
    //                    Response.Flush();
    //                    Response.WriteFile(Server.MapPath("~/error/LogDetails.txt"));
    //                    Response.End();
    //                    Response.Close();
    //                    //=======================================
    //                }
    //                else
    //                {
    //                    ShowMessage("No Details found based on given selection.");
    //                }
    //            }
    //        }
    //        else
    //        {
    //            //ClientScript.RegisterStartupScript(GetType(), "alert", "<script>alert('From Date should be Smaller than To Date')</script>");
    //            ShowMessage("From Date should be Smaller than To Date.");
    //            lvList.DataSource = null;
    //            lvList.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "logDetails.btnDownload_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void btnDownload_Click(object sender, EventArgs e)    // Modified By Shrikant Waghmare on 04-10-2023 Because LogFile was not Downloading
    {
        #region Old Logic
        //try
        //{
        //    LogFile objLog = new LogFile();
        //    objLog.Ua_Name = ddlName.Text.Trim();
        //    SqlDataReader dr = LogTableController.GetLogDetail(objLog.Ua_Name);

        //    string filename = Server.MapPath("~/error/LogDetails.txt");
        //    using (StreamWriter sw = File.CreateText(filename))
        //    {
        //        // Add some text to the file
        //        sw.WriteLine(" LogFile");
        //        sw.WriteLine("=========");
        //        sw.WriteLine(); 
        //        //sw.WriteLine(" Date : " + DateTime.Today);
        //        //sw.WriteLine();
        //        sw.WriteLine(" User Name : " + objLog.Ua_Name);
        //        sw.WriteLine();
        //        sw.WriteLine(" Login Time \t\t\t Logout Time \t\t\t\t IP Address \t\t\t\t Mac Address");
        //        sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
        //        while (dr.Read())
        //        {
        //            sw.WriteLine(" " + dr["logintime"] + " \t\t " + dr["logouttime"] + " \t\t\t " + dr["IPADDRESS"] +" \t\t\t\t" + dr["MACADDRESS"]);
        //        }
        //        if (dr != null) dr.Close();
        //    }

        //    //Download
        //    //=======================================
        //    Response.Clear();
        //    Response.ContentType = "text/comma-separated-values";            
        //    Response.AddHeader("Content-Disposition", "attachment; filename=LogDetails.txt");
        //    Response.Flush();
        //    Response.WriteFile(Server.MapPath("~/error/LogDetails.txt"));
        //    Response.End();
        //    Response.Close();            
        //    //=======================================

        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "logDetails.btnDownload_Click-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
        #endregion Old Logic  

        try
        {
            if (Convert.ToDateTime(txtFromdt.Text) <= Convert.ToDateTime(txtTodt.Text))
            {
                LogFile objLog = new LogFile();
                objLog.Ua_Name = ddlName.Text.Trim();

                DataSet ds1 = LogTableController.GetAllLogFileDate(objLog, ddlName.SelectedItem.ToString(), txtFromdt.Text, txtTodt.Text);

                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    DataTableReader dr = null;
                    dr = ds1.Tables[0].CreateDataReader();
                    string filename = Server.MapPath("~/error/LogDetails.txt");


                    using (StreamWriter sw = File.CreateText(filename))
                    {
                        sw.WriteLine(" LogFile");
                        sw.WriteLine("=========");
                        sw.WriteLine();
                        sw.WriteLine(" User Name : " + objLog.Ua_Name);
                        sw.WriteLine();
                        sw.WriteLine(" Login Time \t\t\t Logout Time \t\t\t\t IP Address \t\t\t\t Mac Address");
                        sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");

                        while (dr.Read())
                        {
                            sw.WriteLine(" " + dr["logintime"] + " \t\t " + dr["logouttime"] + " \t\t\t " + dr["IPADDRESS"] + " \t\t\t\t" + dr["MACADDRESS"]);
                        }
                    }

                    Response.Clear();
                    Response.ContentType = "text/comma-separated-values";
                    Response.AddHeader("Content-Disposition", "attachment; filename=LogDetails.txt");
                    Response.TransmitFile(filename);
                    Response.End();
                }
                else
                {
                    ShowMessage("No Details found based on the given selection.");
                }
            }
            else
            {
                ShowMessage("From Date should be Smaller than To Date.");
                lvList.DataSource = null;
                lvList.DataBind();
            }
        }
        catch (Exception ex)
        {
            ShowMessage("An error occurred: " + ex.Message);
        }
    }

    protected void btnExcelDetails_Click(object sender, EventArgs e)
    {
        DataSet dsLogDetails=(DataSet)ViewState["LogDetails"];
        GridView gv = new GridView();
        if (dsLogDetails.Tables.Count > 0 && dsLogDetails.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = dsLogDetails;
            gv.DataBind();
            string Attachment = "Attachment;filename=LoginDetails.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", Attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.HeaderStyle.Font.Bold = true;
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}
