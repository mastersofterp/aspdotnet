using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Master_Leave_Name_Short : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeaveName = new LeavesController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");

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
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                pnlList.Visible = true;

                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnBack.Visible = false;
                BindListViewLeaveName();
                //BindListViewHoliType();
                //Set Report Parameters 
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_Holidays.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
                // objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_PassAuthority.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
            }

        }
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Leaves objLeaves = new Leaves();
            objLeaves.LEAVENAMESHRT = Convert.ToString(txtleavename.Text);

            objLeaves.SHORTNAME = Convert.ToString(txtshortname.Text);

            objLeaves.MAXDAYS = Convert.ToDouble(txtmaxday.Text);

            if (rdbYearly.SelectedIndex == 0)
            {
                int yr = 0;
                //string yrstr = "Yearly";
                objLeaves.YEARLYPRD = Convert.ToInt32(yr);
                //objLeaves.HALFYEARLYPD = Convert.ToString(yrstr);
            }
            else if (rdbYearly.SelectedIndex == 1)
            {
                int hyr = 1;
                //string yrstr = "Half Yearly";
                objLeaves.YEARLYPRD = Convert.ToInt32(hyr);
                //objLeaves.HALFYEARLYPD = Convert.ToString(yrstr);

            }
            else
            {

                int sbl = 2;
                objLeaves.YEARLYPRD = Convert.ToInt32(sbl);

            }
            if (chkIsPayLeave.Checked == true)
            {
                objLeaves.IsPayLeave = true;
            }
            else
            {
                objLeaves.IsPayLeave = false;
            }
            if (chkIsCertificate.Checked == true)
            {
                objLeaves.IsCertificate = true;

                if (txtCertificatedays.Text != string.Empty)
                {
                    objLeaves.NO_DAYS = Convert.ToDouble(txtCertificatedays.Text);
                }
                else
                {
                    objLeaves.NO_DAYS = 0;
                }
            }
            else
            {
                objLeaves.IsCertificate = false;
                objLeaves.NO_DAYS = 0;
            }

            if (chkPrefix.Checked == true)
            {
                objLeaves.IsValidatedays = true;
            }
            else
            {
                objLeaves.IsValidatedays = false;
            }
            //Added by Sonal Banode on 30-09-2022 to get Iscompoff on edit
            if (chkComp.Checked == true)
            {
                objLeaves.IsCompOff = true;
            }
            else
            {
                objLeaves.IsCompOff = false;
            }
            //
           

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    DataSet ds = objCommon.FillDropDown("PayROLL_Leave_Name", "*", "", "(Leave_Name='" + txtleavename.Text + "' and Leave_ShortName='" + txtshortname.Text + "' AND LVNO<>" + objLeaves.LEAVENO + ") ", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //objCommon.DisplayUserMessage(updAll, "Record already exists", this);
                        MessageBox("Record already exists");
                        return;
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objLeaveName.AddLeaveShortName(objLeaves);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            MessageBox("Record Saved Successfully");
                            BindListViewLeaveName();
                            ViewState["action"] = null;
                            Clear();
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = false;

                            btnAdd.Visible = true;
                            btnShowReport.Visible = true;
                        }
                        else if (cs.Equals(CustomStatus.RecordFound))
                        {
                            MessageBox("Record Already Exits!");
                        } 
                    }
                }
                else
                {
                    if (ViewState["LEAVENO"] != null)
                    {

                        //DataSet ds = objCommon.FillDropDown("PayROLL_Leave_Name", "*", "", "(Leave_Name='" + txtleavename.Text + "' or Leave_ShortName='" + txtshortname.Text + "') and Yearly=" + rdbYearly.SelectedIndex + "  AND LVNO<>" + objLeaves.LEAVENO + " ", "");
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        //    //objCommon.DisplayMessage("Record already exists", this);
                        //    objCommon.DisplayUserMessage(updAll, "Record already exists", this);
                        //    return;
                        //}
                        //else
                        //{
                        //DataSet ds = objCommon.FillDropDown("PayROLL_Leave_Name", "*", "", "(Leave_Name='" + txtleavename.Text + "' and Leave_ShortName='" + txtshortname.Text + "' AND LVNO<>" + objLeaves.LEAVENO + ") ", "");
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        //    //objCommon.DisplayUserMessage(updAll, "Record already exists", this);
                        //    MessageBox("Record already exists");
                        //    return;
                        //}
                        //else
                        //{
                            objLeaves.LEAVENO = Convert.ToInt32(ViewState["LEAVENO"].ToString());
                            CustomStatus cs = (CustomStatus)objLeaveName.UpdateLeaveShortName(objLeaves);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                pnlAdd.Visible = false;
                                pnlList.Visible = true;
                                MessageBox("Record Updated Successfully");
                                BindListViewLeaveName();
                                ViewState["action"] = null;
                                Clear();
                                btnSave.Visible = false;
                                btnCancel.Visible = false;
                                btnBack.Visible = false;

                                btnAdd.Visible = true;
                                btnShowReport.Visible = true;
                            }
                        //}
                        //}
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int lvno = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objLeaveName.DeleteLeaveShortName(lvno);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = null;
                BindListViewLeaveName();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnDelete_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int LEAVENO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(LEAVENO);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;

            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnBack.Visible = true;

            btnAdd.Visible = false;
            btnShowReport.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void ShowDetails(Int32 LEAVENO)
    {
        DataSet ds = null;
        try
        {
            ds = objLeaveName.GetSingLeaveShortName(LEAVENO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["LEAVENO"] = LEAVENO;
                txtleavename.Text = ds.Tables[0].Rows[0]["Leave_Name"].ToString();
                txtshortname.Text = ds.Tables[0].Rows[0]["Leave_ShortName"].ToString();
               // txtmaxday.Text =Convert.ToDouble(ds.Tables[0].Rows[0]["Max_Days"].ToString());

                double MaxDay;
                MaxDay = Convert.ToDouble(ds.Tables[0].Rows[0]["Max_Days"]);
                txtmaxday.Text = MaxDay.ToString();
                double No_ofDaysforCertificate;


                rdbYearly.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["Yearly"]);
                Boolean ispayleave = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsPayLeave"]);
                Boolean iscertificate = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCertificate"]);
                if (ispayleave == true)
                {
                    chkIsPayLeave.Checked = true;
                }
                else
                {
                    chkIsPayLeave.Checked = false;
                }
                if (iscertificate == true)
                {
                    chkIsCertificate.Checked = true;
                    divNoofdaycertifi.Visible = true;
                    No_ofDaysforCertificate = Convert.ToDouble(ds.Tables[0].Rows[0]["DaysforCertificate"]);
                    txtCertificatedays.Text = No_ofDaysforCertificate.ToString();

                }
                else
                {
                    chkIsCertificate.Checked = false;
                    divNoofdaycertifi.Visible = false;
                    No_ofDaysforCertificate = '0';
                    //txtCertificatedays.Text = No_ofDaysforCertificate.ToString();
                    txtCertificatedays.Text = string.Empty;
                }
                Boolean isValidatedays = Convert.ToBoolean(ds.Tables[0].Rows[0]["isValidatedays"]);
                if (isValidatedays == true)
                {
                    chkPrefix.Checked = true;
                }
                else
                {
                    chkPrefix.Checked = false;
                }
                //Added by Sonal Banode on 30-09-2022 to get Iscompoff on edit
                Boolean IsCompOff = Convert.ToBoolean(ds.Tables[0].Rows[0]["ISCOMPOFF"]);
                if (IsCompOff == true)
                {
                    chkComp.Checked = true;
                }
                else
                {
                    chkComp.Checked = false;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        txtleavename.Text = string.Empty;
        txtshortname.Text = string.Empty;
        txtmaxday.Text = string.Empty;
        //rdbYearly.SelectedValue = "0";
        rdbYearly.SelectedValue = null;
        chkIsPayLeave.Checked = false;
        chkIsCertificate.Checked = false;
        chkPrefix.Checked = false;
        chkComp.Checked = false;
        divNoofdaycertifi.Visible = false;
        txtCertificatedays.Text = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;

        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnBack.Visible = false;

        btnAdd.Visible = true;
        btnShowReport.Visible = true;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        ViewState["action"] = "add";

        btnShowReport.Visible = false;
        btnAdd.Visible = false;

        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;
    }
    protected void BindListViewLeaveName()
    {
        try
        {
            DataSet ds = objLeaveName.GetAllLeaveShortName();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                btnShowReport.Visible = false;
                // dpPager.Visible = false;
            }
            else
            {
                btnShowReport.Visible = true;
                //  dpPager.Visible = true;
            }
            lvLeaveName.DataSource = ds;
            lvLeaveName.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain            
        BindListViewLeaveName();
    }


    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("LeaveType", "ESTB_LeaveType.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            //GetStudentIDs();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,LEAVES," + rptFileName;

            //            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_ORDTRNO=" + Convert.ToInt32(ddlOrder.SelectedValue) + "," + "@username=" + Session["userfullname"].ToString();
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //url += "&param=@username=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            string collegeno = Session["college_nos"].ToString();
            string[] values = collegeno.Split(',');
            if (values.Length > 1)
            {
                url += "&param=@username=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=0";
            }
            else
            {
                url += "&param=@username=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=" + Session["college_nos"].ToString();
            }
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
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
    protected void chkIsCertificate_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if(chkIsCertificate.Checked)
            {
                divNoofdaycertifi.Visible = true;
            }
            else
            {
                divNoofdaycertifi.Visible = false;
            }
        }
        catch (Exception ex)
        {
        }
    }
}
