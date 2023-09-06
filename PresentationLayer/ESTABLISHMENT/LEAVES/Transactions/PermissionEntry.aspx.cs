using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Transactions_PermissionEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objShift = new LeavesController();
    Leaves objLeaves = new Leaves();

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
                // CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlSave.Visible = false;
                pnllist.Visible = true;
                GetUserInfo();
                BindListViewApp();
                btnSubmit.Enabled = true;


            }


        }
        //blank div tag
        // divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
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

    protected void lnkNew_Click(object sender, EventArgs e)
    {

        pnlSave.Visible = true;
        pnllist.Visible = false;
        Boolean IsLeaveWisePassingPath = Convert.ToBoolean(objCommon.LookUp("PAYROLL_LEAVE_REF", "isnull(IsLeaveWisePassingPath,0)as IsLeaveWisePassingPath", ""));

        ViewState["IsLeaveWisePassingPath"] = IsLeaveWisePassingPath;

        DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "SUBDEPTNO,STNO,COLLEGE_NO,STNO,STAFFNO,PHONENO,isnull(ALTERNATE_EMAILID,'') as ALTERNATE_EMAILID", "IDNO", "IDNO=" + Convert.ToInt32(Session["idno"]) + "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["SUBDEPTNO"] = ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString();
            ViewState["STNO"] = ds.Tables[0].Rows[0]["STNO"].ToString();
            ViewState["COLLEGE_NO"] = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
            ViewState["STAFFNO"] = ds.Tables[0].Rows[0]["STAFFNO"].ToString();
            ViewState["EMAILID"] = ds.Tables[0].Rows[0]["ALTERNATE_EMAILID"].ToString();
            ViewState["PHONENO"] = ds.Tables[0].Rows[0]["PHONENO"].ToString();
        }


        GetPAPath1();
        //btnSubmit.Enabled = true;
        ViewState["action"] = "add";
    }

    protected void lnkbut_Click(object sender, EventArgs e)
    {
        pnllist.Visible = false;
        pnlStatus.Visible = true;
        BindListViewApp();

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PERTNO = int.Parse(btnEdit.CommandArgument);
            if (PERTNO == 0)
            {
                return;
            }
            string STATUS = objCommon.LookUp("PAYROLL_LEAVE_PERMISSION_ENTRY", "STATUS", "PERTNO=" + PERTNO);
            if (STATUS == "A")
            {
                btnSubmit.Enabled = false;
                MessageBox("You Can not edit Application is already Approved.");
                return;
            }
            else if (STATUS == "R")
            {
                btnSubmit.Enabled = false;
                MessageBox("You Can not edit Application is already Rejected.");
                return;
            }
            else
            {
                btnSubmit.Enabled = true;
            }
            DataSet dsPER = new DataSet();

            dsPER = objCommon.FillDropDown("PAYROLL_PERMISSION_APP_PASS_ENTRY", "STATUS", "PANO", "PERTNO=" + PERTNO + " ", "PANO");
            int total = dsPER.Tables[0].Rows.Count;
            for (int i = 0; i < total; i++)
            {
                //Code to avoid modification of record if 1st authority has approved leave (in case of more than 1 authority)
                string status = dsPER.Tables[0].Rows[i]["STATUS"].ToString();
                if (status == "F")
                {
                    MessageBox("Approval In Progress, Not Allow To Modify");
                    return;
                }
            }


            Boolean IsLeaveWisePassingPath = Convert.ToBoolean(objCommon.LookUp("PAYROLL_LEAVE_REF", "isnull(IsLeaveWisePassingPath,0)as IsLeaveWisePassingPath", ""));

            ViewState["IsLeaveWisePassingPath"] = IsLeaveWisePassingPath;

            DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "SUBDEPTNO,STNO,COLLEGE_NO,STNO,STAFFNO,PHONENO,isnull(ALTERNATE_EMAILID,'') as ALTERNATE_EMAILID", "IDNO", "IDNO=" + Convert.ToInt32(Session["idno"]) + "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["SUBDEPTNO"] = ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString();
                ViewState["STNO"] = ds.Tables[0].Rows[0]["STNO"].ToString();
                ViewState["COLLEGE_NO"] = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                ViewState["STAFFNO"] = ds.Tables[0].Rows[0]["STAFFNO"].ToString();
                ViewState["EMAILID"] = ds.Tables[0].Rows[0]["ALTERNATE_EMAILID"].ToString();
                ViewState["PHONENO"] = ds.Tables[0].Rows[0]["PHONENO"].ToString();
            }

            ShowDetails(PERTNO);
            GetPAPath1();
            ViewState["action"] = "edit";
            ViewState["PERTNO"] = PERTNO;
            pnlSave.Visible = true;
            pnllist.Visible = false;
            pnlStatus.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CPDA_Transactions_CPDAForProgramAttended.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        //ShowReport("Application_Form", "leave_app.rpt");
        try
        {
            Button btnReport = sender as Button;
            //int PERTNO = int.Parse(btnApply.ToolTip.ToString());
            int PERTNO = int.Parse(btnReport.CommandArgument);
            ViewState["PERTNO"] = PERTNO;


            //  string ds = objCommon.LookUp("PAYROLL_LEAVE_APP_PASS_ENTRY", "COUNT(*)as 'AP'", "LETRNO ='" + letrno + "' and STATUS='A'");

            string ds = objCommon.LookUp("PAYROLL_LEAVE_PERMISSION_ENTRY", "COUNT(*)as 'AP'", "PERTNO =" + PERTNO);

            // string ds = objCommon.LookUp("PAYROLL_HQ_LEAVE_APP_PASS_ENTRY", "COUNT(*)as 'AP'", "HQLETRNO='" + HQLETRNO);

            if (Convert.ToInt32(ds) > 0)
            {
                ShowReport("Application_form", "ESTB_PermissionEntry.rpt");

            }
            else
            {
                objCommon.DisplayMessage("Report not available", this.Page);
                MessageBox("Report not available");
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.btnApply_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;

            int idnorpt = Convert.ToInt32(Session["idno"]);
            int stnorpt = Convert.ToInt32(ViewState["STNO"]);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_EMPNO=" + Convert.ToInt32(Session["idno"]) + "," + "@P_PERTNO=" + Convert.ToInt32(ViewState["PERTNO"].ToString()) + "" + "";
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void dpinfo_PreRender(object sender, EventArgs e)
    {
        BindListViewApp();
    }

    public void BindListViewApp()
    {
        DataSet ds = new DataSet();
        ds = objShift.GetPermissionDetails(Convert.ToInt32(Session["idno"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvCPDA.DataSource = ds;
            lvCPDA.DataBind();
            dpinfo.Visible = true;
        }
        else
        {
            lvCPDA.DataSource = null;
            lvCPDA.DataBind();
            dpinfo.Visible = false;
        }
    }


    protected void btnBackPanel_Click(object sender, EventArgs e)
    {
        pnllist.Visible = true;
        pnlStatus.Visible = false;
        btnSubmit.Enabled = true;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnllist.Visible = true;
        pnlSave.Visible = false;
        btnSubmit.Enabled = true;
        clear();
        ViewState["PERTNO"] = null;
        ViewState["action"] = null;
        ViewState["SUBDEPTNO"]=null; 
        ViewState["STNO"] = null;
        ViewState["COLLEGE_NO"]=null;
        ViewState["STAFFNO"] = null;
        ViewState["EMAILID"] = null;
        ViewState["PHONENO"] = null;
        ViewState["IsLeaveWisePassingPath"] = null;


    }

    public void GetUserInfo()
    {
        DataSet ds = null;
        try
        {
            ds = objShift.GetUserPermissionDetails(Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["usertype"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["idno"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                lblUserName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                lbldept.Text = ds.Tables[0].Rows[0]["SUBDEPT"].ToString();
                lbldesgin.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CPDA_Transactions_CPDAForProgramAttended.GetUserInfo-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            bool result = CheckPurpose();


            objLeaves.COLLEGE_CODE = Session["colcode"].ToString();
            objLeaves.APRDT = System.DateTime.Now;
            objLeaves.DATE = Convert.ToDateTime(txtFromDate.Text);

            if (txtreson.Text != string.Empty)
            {
                objLeaves.REASON = txtreson.Text;
            }
            else
            {
                objLeaves.REASON = string.Empty;
            }
            objLeaves.CreatedBy = Convert.ToInt32(Session["userno"].ToString());
            objLeaves.ModifiedBy = Convert.ToInt32(Session["userno"].ToString());

            if (rblleavetype.SelectedValue == "0")
            {
                objLeaves.Dayselection = 0;
            }
            else
            {
                objLeaves.Dayselection = 1;
            }

            objLeaves.STATUS = "P";

            if (txtPath.Text.Equals(string.Empty))
            {
                objLeaves.PAPNO = 0;
            }
            else
            {
                objLeaves.PAPNO = Convert.ToInt32(ViewState["papno"]);
            }
            objLeaves.EMPNO = Convert.ToInt32(Session["idno"]);

            if (ViewState["action"] != null)
            {
                if (result == true)
                {
                    //objCommon.DisplayMessage("Record Already Exist", this);
                    MessageBox("Record Already Exist");
                    return;
                }
                if (ViewState["action"].Equals("add"))
                {
                    DataSet ds2 = null;

                    ds2 = objShift.GetPermissionCount(Convert.ToInt32(Session["idno"]), Convert.ToDateTime(txtFromDate.Text)); // DateTime fromdt
                    int PermissionValid = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_REF", "isnull(PERMISSIONINMONTH,0) as PERMISSIONINMONTH", ""));

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        int count = Convert.ToInt32(ds2.Tables[0].Rows[0]["count"].ToString());
                        if (count >= PermissionValid)
                        {
                            MessageBox("You have reached the Limit for Permission of " +PermissionValid +" per month");
                            return;
                        }

                    }

                    CustomStatus cs = (CustomStatus)objShift.AddPermissionEntry(objLeaves);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        MessageBox("Record Saved Successfully");
                        clear();
                        pnlSave.Visible = false;
                        pnllist.Visible = true;
                        ViewState["action"] = null;

                    }

                }
                else if (ViewState["action"].Equals("edit"))
                {

                    if (result == true)
                    {

                        DataSet ds2 = null;
                        ds2 = objShift.GetPermissionCount(Convert.ToInt32(Session["idno"]), Convert.ToDateTime(txtFromDate.Text)); // DateTime fromdt
                        int PermissionValid = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_REF", "isnull(PERMISSIONINMONTH,0) as PERMISSIONINMONTH", ""));
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            int count = Convert.ToInt32(ds2.Tables[0].Rows[0]["count"].ToString());
                            if (count >= PermissionValid)
                            {
                                MessageBox("You have reached the Limit for Permission of "+ PermissionValid +" per month");
                                return;
                            }

                        }
                    }

                    objLeaves.PERTNO = Convert.ToInt32(ViewState["PERTNO"].ToString());
                    CustomStatus cs = (CustomStatus)objShift.UpdatePermission(objLeaves);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        //MessageBox("Record Updated Sucessfully");
                        clear();
                        pnlSave.Visible = false;
                        pnllist.Visible = true;
                        MessageBox("Record Updated Successfully");
                        ViewState["action"] = null;
                    }
                }
                ViewState["PERTNO"] = null;

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_PermissionEntry.aspx.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txtreson.Text = string.Empty;
        rblleavetype.SelectedValue = "0";
    }

    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        // int STNONEW = Convert.ToInt32(ddlStafftype.SelectedValue);
        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd")));
        //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
        Fdate = Fdate.Substring(0, 10);

        int Empno = Convert.ToInt32(Session["idno"]);

        //dsPURPOSE = objCommon.FillDropDown("PAYROLL_LEAVE_PERMISSION_ENTRY", "*", "", "DATE='" + Fdate + "' ", ""); //   + "' AND STNO =" + STNONEW + "", "");
        // dsPURPOSE = objCommon.FillDropDown("PAYROLL_LEAVE_PERMISSION_ENTRY", "*", "", "DATE='" + Fdate + "'AND Dayselection= " + Convert.ToInt32(rblleavetype.SelectedValue) , "");

        dsPURPOSE = objCommon.FillDropDown("PAYROLL_LEAVE_PERMISSION_ENTRY", "*", "", "DATE='" + Fdate + "'AND Dayselection= " + Convert.ToInt32(rblleavetype.SelectedValue) + "AND EMPNO=" + Empno + " AND REMARK ='" + txtreson.Text + "'", "");

        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }


    public void clear()
    {
        txtFromDate.Text = string.Empty;
        txtreson.Text = string.Empty;
        rblleavetype.SelectedValue = "0";
    }

    private void ShowDetails(Int32 PERTNO)
    {
        DataSet ds = null;
        try
        {
            ds = objShift.RetrieveSinglepermission(PERTNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PERTNO"] = PERTNO;
                txtFromDate.Text = ds.Tables[0].Rows[0]["DATE"].ToString();
                txtreson.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                //if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Dayselection"]) == true)//full day leave

                int Dayselection = Convert.ToInt32(ds.Tables[0].Rows[0]["Dayselection"]);
                //full day leave
                if (Dayselection  ==1)
                {
                    rblleavetype.SelectedValue = "1";

                    // ddlLeaveFNAN.Visible = false;

                }
                else
                {
                    //ddlLeaveFNAN.Visible = true;    
                    rblleavetype.SelectedValue = "0";

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CPDA_Master_BLOCKNAME.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void GetPAPath1()
    {
        try
        {

            string path = string.Empty;
            string userno = Session["userno"].ToString();            
            int useridno = Convert.ToInt32(Session["idno"]);            
            int collegeno = Convert.ToInt32(ViewState["COLLEGE_NO"]);          

            DataSet dspath = new DataSet();           
            if (Convert.ToBoolean(ViewState["IsLeaveWisePassingPath"]) == true)
            {
                dspath = null;
                dspath = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "*", "", "idno=" + useridno + " AND COLLEGE_NO=" + collegeno + " AND Leavevalue=" + ViewState["LEAVENO"] + " ", "");
            }
            else
            {
                dspath = null;
                dspath = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY_PATH", "*", "", "idno=" + useridno + " AND COLLEGE_NO=" + collegeno + " AND isnull(Leavevalue,0) =" + 0 + " ", "");
            }
            if (dspath.Tables[0].Rows.Count > 0)
            {
                ViewState["papno"] = dspath.Tables[0].Rows[0]["PAPNO"].ToString();


                string pano1 = dspath.Tables[0].Rows[0]["PAN01"].ToString();
                string pano2 = dspath.Tables[0].Rows[0]["PAN02"].ToString();
                string pano3 = dspath.Tables[0].Rows[0]["PAN03"].ToString();
                string pano4 = dspath.Tables[0].Rows[0]["PAN04"].ToString();
                string pano5 = dspath.Tables[0].Rows[0]["PAN05"].ToString();


                string uano1 = string.Empty;
                string uano2 = string.Empty;
                string uano3 = string.Empty;
                string uano4 = string.Empty;
                string uano5 = string.Empty;
                string paname1 = string.Empty;
                string paname2 = string.Empty;
                string paname3 = string.Empty;
                string paname4 = string.Empty;
                string paname5 = string.Empty;

                DataSet dsauth1 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano1, "");
                if (dsauth1.Tables[0].Rows.Count > 0)
                {
                    uano1 = dsauth1.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname1 = dsauth1.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth2 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano2, "");
                if (dsauth2.Tables[0].Rows.Count > 0)
                {
                    uano2 = dsauth2.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname2 = dsauth2.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth3 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano3, "");
                if (dsauth3.Tables[0].Rows.Count > 0)
                {
                    uano3 = dsauth3.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname3 = dsauth3.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth4 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano4, "");
                if (dsauth4.Tables[0].Rows.Count > 0)
                {
                    uano4 = dsauth4.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname4 = dsauth4.Tables[0].Rows[0]["PANAME"].ToString();
                }

                DataSet dsauth5 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano5, "");
                if (dsauth5.Tables[0].Rows.Count > 0)
                {
                    uano5 = dsauth5.Tables[0].Rows[0]["UA_NO"].ToString();
                    paname5 = dsauth5.Tables[0].Rows[0]["PANAME"].ToString();
                }


                if (userno == uano1)
                {
                    path = paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
                }
                else if (userno == uano2)
                {
                    path = paname3 + "->" + paname4 + "->" + paname5;
                }
                else if (userno == uano3)
                {
                    path = paname4 + "->" + paname5;
                }
                else if (userno == uano4)
                {
                    path = paname5;
                }
                else if (userno == uano5)
                {
                    path = paname5;
                }
                else
                {
                    path = paname1 + "->" + paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
                }
                txtPath.Text = path;

            }
            else
            {
                MessageBox("Sorry! Authority Not found");
                txtPath.Text = string.Empty;
                btnSubmit.Enabled = false;
                return;
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.GetPAPath ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }

    }

   /* protected void GetPAPath1()
    {
        try
        {

            string path = string.Empty;
            string userno = Session["userno"].ToString();
            DataSet dsAuth = new DataSet();
            int idno = Convert.ToInt32(Session["idno"]);
            dsAuth = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "*", "", "UA_NO=" + userno, "");
            //string pano = dsAuth.Tables[0].Rows[0]["PANO"].ToString();

            DataSet dsdept = new DataSet();
            dsdept = objCommon.FillDropDown("USER_ACC", "*", "", "UA_NO=" + userno, "");
            string dept = dsdept.Tables[0].Rows[0]["UA_EMPDEPTNO"].ToString();

            DataSet dspath = new DataSet();
            dspath = objCommon.FillDropDown("PAYROLL_PERMISSION_PASSING_AUTHORITY_PATH", "*", "", "IDNO=" + idno + "", "");

            if (dspath.Tables[0].Rows.Count > 0)
            {
                ViewState["papno"] = dspath.Tables[0].Rows[0]["PAPNO"].ToString();
                btnSubmit.Enabled = true;
            }
            else
            {
                MessageBox("Authority Not Found");
                btnSubmit.Enabled = false;
                return;
            }           

            string pano1 = dspath.Tables[0].Rows[0]["PAN01"].ToString();
            string pano2 = dspath.Tables[0].Rows[0]["PAN02"].ToString();
            string pano3 = dspath.Tables[0].Rows[0]["PAN03"].ToString();
            string pano4 = dspath.Tables[0].Rows[0]["PAN04"].ToString();
            string pano5 = dspath.Tables[0].Rows[0]["PAN05"].ToString();


            string uano1 = string.Empty;
            string uano2 = string.Empty;
            string uano3 = string.Empty;
            string uano4 = string.Empty;
            string uano5 = string.Empty;
            string paname1 = string.Empty;
            string paname2 = string.Empty;
            string paname3 = string.Empty;
            string paname4 = string.Empty;
            string paname5 = string.Empty;

            DataSet dsauth1 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano1, "");
            if (dsauth1.Tables[0].Rows.Count > 0)
            {
                uano1 = dsauth1.Tables[0].Rows[0]["UA_NO"].ToString();
                paname1 = dsauth1.Tables[0].Rows[0]["PANAME"].ToString();
            }

            DataSet dsauth2 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano2, "");
            if (dsauth2.Tables[0].Rows.Count > 0)
            {
                uano2 = dsauth2.Tables[0].Rows[0]["UA_NO"].ToString();
                paname2 = dsauth2.Tables[0].Rows[0]["PANAME"].ToString();
            }

            DataSet dsauth3 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano3, "");
            if (dsauth3.Tables[0].Rows.Count > 0)
            {
                uano3 = dsauth3.Tables[0].Rows[0]["UA_NO"].ToString();
                paname3 = dsauth3.Tables[0].Rows[0]["PANAME"].ToString();
            }

            DataSet dsauth4 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano4, "");
            if (dsauth4.Tables[0].Rows.Count > 0)
            {
                uano4 = dsauth4.Tables[0].Rows[0]["UA_NO"].ToString();
                paname4 = dsauth4.Tables[0].Rows[0]["PANAME"].ToString();
            }

            DataSet dsauth5 = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANAME", "PANO=" + pano5, "");
            if (dsauth5.Tables[0].Rows.Count > 0)
            {
                uano5 = dsauth5.Tables[0].Rows[0]["UA_NO"].ToString();
                paname5 = dsauth5.Tables[0].Rows[0]["PANAME"].ToString();
            }
            if (userno == uano1)
            {
                path = paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
            }
            else if (userno == uano2)
            {
                path = paname3 + "->" + paname4 + "->" + paname5;
            }
            else if (userno == uano3)
            {
                path = paname4 + "->" + paname5;
            }
            else if (userno == uano4)
            {
                path = paname5;
            }
            else if (userno == uano5)
            {
                path = paname5;
            }
            else
            {
                //path = paname1 + "->" + paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;          
                //  path =paname1 + "->" + paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
                if (paname2 == string.Empty)
                {
                    path = paname1;
                }
                else if (paname3 == string.Empty)
                {
                    path = paname1 + "->" + paname2;
                }
                else if (paname4 == string.Empty)
                {
                    path = paname1 + "->" + paname2 + "->" + paname3;
                }
                else if (paname5 == string.Empty)
                {
                    path = paname1 + "->" + paname2 + "->" + paname3 + "->" + paname4;
                }
                else
                {
                    path = paname1 + "->" + paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
                }

            }
            txtPath.Text = path;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.GetPAPath ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }


    }*/
}