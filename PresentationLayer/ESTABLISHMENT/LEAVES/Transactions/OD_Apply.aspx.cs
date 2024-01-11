//==============================================
//MODIFIED BY: Swati Ghate
//MODIFIED DATE:10-11-2014
//PURPOSE: TO UPDATE THE DESIGN & DISPLAY STATUS DATA
//==============================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;
using System.Globalization;
public partial class ESTABLISHMENT_LEAVES_Transactions_OD_Apply : System.Web.UI.Page
{
    string date = "";
    int counter = 0;
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();
    Leaves objlv = new Leaves();
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

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                int useridno = Convert.ToInt32(Session["idno"]);
                ViewState["COLLEGE_NO"] = objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + useridno + " ");
                pnlAdd.Visible = false;
                //pnllist.Visible = true;
                //pnlODStatus.Visible = false;
                //pnlODInfo.Visible = false;
                pnlbtn.Visible = false;
                btnSave.Enabled = true;
                BindListViewODapplStatus();
                BindListViewODStatus();
                FillPurpose();
                rblODType.SelectedValue = "0";
                trfrmto.Visible = false;

                lblJoinindt.Text = "<b>Slip Date</b>";

                trinout.Visible = true;
                // trcomment.Visible = true;
                trEventRange.Visible = false;
                txtJoindt.Text = DateTime.Now.ToString();
                CheckPageAuthorization();
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
                Response.Redirect("~/notauthorized.aspx?page=OD_Apply.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OD_Apply.aspx");
        }
    }

    protected void BindListViewODapplStatus()
    {
        try
        {
            DataSet ds = objApp.GetODApplStatus(Convert.ToInt32(Session["idno"]));//Session["idno"]
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpPager.Visible = false;
            }
            else
            {
                //dpPager.Visible = true;
            }
            //lvStatus.DataSource = ds.Tables[0];
            //lvStatus.DataBind();
            //lvStatus.Visible = true;
            lvODinfo.DataSource = ds.Tables[0];
            lvODinfo.DataBind();
            lvODinfo.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.BindListViewLeaveapplStatus ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }


    public void clear()
    {
        //txtDate.Text = string.Empty;
        txtFromdt.Text = string.Empty;
        txtTodt.Text = string.Empty;
        txtNodays.Text = string.Empty;
        txtJoindt.Text = DateTime.Now.ToString();
        txtIn.Text = string.Empty;
        txtInstruct.Text = string.Empty;
        txtInTime.Text = string.Empty;
        txtOut.Text = string.Empty;
        txtOutTime.Text = string.Empty;
        txtPlace.Text = string.Empty;
        //ddlPurpose.SelectedIndex = 0;
        txtRegAmt.Text = string.Empty;
        txtEventFrm.Text = string.Empty;
        txtEventTo.Text = string.Empty;
        txtTopic.Text = string.Empty;
        txtOrganised.Text = string.Empty;
        // ddlEventType.SelectedIndex = 0;
        // ddlPurpose.SelectedIndex = 0;
        //txtTADA.Text = string.Empty;
        //ddlPath.SelectedIndex = 0;
        ddlEventType.SelectedIndex = 0;
        ddlPurpose.SelectedIndex = 0;
        ViewState["FileName"] = null;
    }
    protected void BindListViewODStatus()
    {
        try
        {
            DataSet ds = objApp.GetODStatus(Convert.ToInt32(Session["idno"].ToString()));//Session["idno"]
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpODinfo.Visible = false;
                //pnlODInfo.Visible = false;
            }
            else
            {
                //dpODinfo.Visible = true;
                //pnlODInfo.Visible = true;
            }
            lvODinfo.DataSource = ds.Tables[0];
            lvODinfo.DataBind();
            lvODinfo.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.BindListViewLeaveStatus ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }
    protected void btnHidePanel_Click(object sender, EventArgs e)
    {
        //pnlODStatus.Visible = false;
        pnlAdd.Visible = false;
        pnllist.Visible = true;
        lnkbut.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        OD objOD = new OD();
        ServiceBookController objServiceBook = new ServiceBookController();

        objOD.APRDT = System.DateTime.Now;

        objOD.COLLEGE_CODE = Session["colcode"].ToString();

        if (rblODType.SelectedValue == "1")
        {
            if (Convert.ToDateTime(txtFromdt.Text) > Convert.ToDateTime(txtTodt.Text))
            {
                MessageBox("To Date should be Greater Than Or Equals To From Date");
                return;
            }
            //if (Convert.ToDateTime(txtEventFrm.Text) > Convert.ToDateTime(txtEventTo.Text))
            //{
            //    MessageBox("Event To Date should be Greater Than Or Euals To Event From Date");
            //    return;
            //}
            else
            {
                objOD.FROMDT = txtFromdt.Text.Trim().ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtFromdt.Text);

                objOD.TODT = txtTodt.Text.Trim().ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtTodt.Text);

                objOD.EVENT_FROMDT = txtEventFrm.Text.Trim().ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtEventFrm.Text);
                objOD.EVENT_TODT = txtEventTo.Text.Trim().ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtEventTo.Text);

                objOD.NO_DAYS = Convert.ToDouble(txtNodays.Text);
                objOD.ODTYPE = "ODA";
                objOD.PURPOSE = ddlPurpose.SelectedItem.Text;
                // objOD.PURPOSE = txtodpurpose.Text;

                objOD.DATE = System.DateTime.Now;
                //Code to Avoid duplicate record IN OD APPLICATION for same date with same In & Out time    
                DateTime fromdt = Convert.ToDateTime(txtFromdt.Text);
                string strfromdt = String.Format("{0:MM/dd/yyyy}", fromdt);
                objOD.ODTRNO = Convert.ToInt32(ViewState["letrno"]);
                //DataSet ds = objCommon.FillDropDown("PAYROLL_OD_APP_ENTRY", "*", "", "ODTRNO<>" + objOD.ODTRNO + " AND STATUS<>'D' AND  EMPNO=" + Convert.ToInt32(Session["idno"]) + " AND CONVERT(DATE,'" + strfromdt + "') BETWEEN FROM_DATE AND TO_DATE  AND  ODTYPE='ODA'", "");
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    MessageBox("Record Already Exists!");
                //    return;
                //}


            }
        }
        else if (rblODType.SelectedValue == "0")
        {
            if (txtJoindt.Text != string.Empty)
            {
                objOD.DATE = Convert.ToDateTime(txtJoindt.Text);
                objOD.FROMDT = Convert.ToDateTime(txtJoindt.Text);
                objOD.TODT = Convert.ToDateTime(txtJoindt.Text);
                objOD.NO_DAYS = 1;
                objOD.ODTYPE = "ODS";
                //objOD.PURPOSE = "-";
                //objOD.PURPOSENO = -1;
                // objOD.PURPOSE = "-";


                objOD.DATE = System.DateTime.Now;
                //Code to Avoid duplicate record IN OD SLIP for same date with same In & Out time               
                DateTime joindt = Convert.ToDateTime(txtJoindt.Text);
                string strJoindate = String.Format("{0:MM/dd/yyyy}", joindt);
                objOD.ODTRNO = Convert.ToInt32(ViewState["letrno"]);
                // DataSet ds = objCommon.FillDropDown("PAYROLL_OD_APP_ENTRY", "OUT_TIME", "IN_TIME", "ODTRNO<>" + objOD.ODTRNO + " AND STATUS<>'D' AND EMPNO=" + Convert.ToInt32(Session["idno"]) + " AND DATE=CONVERT(DATE,'" + strJoindate + "') AND  ODTYPE='ODS'  AND (OUT_TIME='"+ txtOutTime.Text +"' )OR (IN_TIME='"+ txtInTime.Text +"' ) AND (IN_TIME='"+ txtOutTime.Text +"') OR (OUT_TIME='"+ txtInTime.Text +"' )", "");
                //AND( '01:00 AM' BETWEEN OUT_TIME AND OUT_TIME )OR ('02:00 AM' BETWEEN OUT_TIME AND OUT_TIME )
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    //MessageBox("Record Already Exists!");
                //   // return;
                //}
                DateTime IN = DateTime.Parse(txtInTime.Text);
                DateTime OUT = DateTime.Parse(txtOutTime.Text);
                string dateout = "";
                dateout = DateTime.ParseExact(txtOutTime.Text, "h:mm tt", CultureInfo.InvariantCulture).ToString("HH:mm:ss");
                string datein = "";
                datein = DateTime.ParseExact(txtInTime.Text, "h:mm tt", CultureInfo.InvariantCulture).ToString("HH:mm:ss");

                if (Convert.ToDateTime(dateout) > Convert.ToDateTime(datein))
                {
                    MessageBox("In time must be greater than Out time!");
                    return;
                }
                DataSet ds = objCommon.FillDropDown("PAYROLL_OD_APP_ENTRY", "OUT_TIME", "IN_TIME", "ODTRNO<>" + objOD.ODTRNO + " AND STATUS<>'D' AND EMPNO=" + Convert.ToInt32(Session["idno"]) + " AND FROM_DATE=CONVERT(DATE,'" + strJoindate + "') AND  ODTYPE='ODS'  AND ((CAST('" + dateout + "' as time) between OUT_TIME and IN_TIME) OR (CAST('" + datein + "' as time) between OUT_TIME and IN_TIME) OR (CAST(OUT_TIME as time) between '" + dateout + "' and '" + datein + "') OR (CAST(IN_TIME as time) between '" + dateout + "' and '" + datein + "'))", "");
                if (ds.Tables[0].Rows.Count > 0)           
                {
                    MessageBox("In Time Or Out Time Already Exist");
                    txtInTime.Text = string.Empty;
                    txtOutTime.Text = string.Empty;
                    return;
                }               
            }
            DateTime t1 = DateTime.Parse(txtInTime.Text);
            DateTime t2 = DateTime.Parse(txtOutTime.Text);

            if (t1.TimeOfDay <= t2.TimeOfDay)
            {
                MessageBox("In Time Should Not Be Less Than Or Equal To Out Time");
                txtInTime.Text = string.Empty;
                return;
            }

        }

        //if (!txtJoindt.Text.Trim().Equals(string.Empty)) objOD.JOINDT = Convert.ToDateTime(txtJoindt.Text);
        objOD.JOINDT = txtJoindt.Text.Trim().ToString().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtJoindt.Text);
        objOD.EMPNO = Convert.ToInt32(Session["idno"]);
        objOD.IN_TIME = txtInTime.Text;
        objOD.INSTRBY = txtInstruct.Text;
        objOD.INTIME = txtIn.Text;

        objOD.OUT_TIME = txtOutTime.Text;
        objOD.OUTTIME = txtOut.Text;
        objOD.PAPNO = Convert.ToInt32(ViewState["papno"]);
        objOD.PLACE = txtPlace.Text;
        objOD.PURPOSENO = Convert.ToInt32(ddlPurpose.SelectedValue);
        objOD.EVENT = Convert.ToInt32(ddlEventType.SelectedValue);
        //objOD.PURPOSE = txtodpurpose.Text;
        //objOD.EVENTTYPE = txteventtype.Text;
        objOD.TOPIC = txtTopic.Text;
        objOD.ORGANISED_BY = txtOrganised.Text;
        objOD.STATUS = 'P';
        if (txtRegAmt.Text == string.Empty)
            objOD.REG_AMT = 0;
        else
            objOD.REG_AMT = Convert.ToDouble(txtRegAmt.Text);
        //if (txtTADA.Text == string.Empty)
        objOD.TADA_AMT = Convert.ToInt32(rblTA.SelectedValue);
        //else
        //objOD.TADA_AMT = Convert.ToDouble(txtTADA.Text);
        if (fuUploadImage.HasFile)
        {
            objOD.FileName = Convert.ToString(fuUploadImage.PostedFile.FileName.ToString());

        }
        objOD.ODTRNO = Convert.ToInt32(ViewState["letrno"]);
        //Added By Shrikant Bharne on 24/01/2023
        Boolean IsRequiredDocumentforOD = Convert.ToBoolean(objCommon.LookUp("Payroll_leave_ref", "isnull(IsRequiredDocumentforOD,0) as IsRequiredDocumentforOD", ""));
        if (IsRequiredDocumentforOD && fuUploadImage.HasFile == false)
        {
            MessageBox("Sorry ! Please Select Document");
            return;
        }

        //


        if (ViewState["action"] != null)
        {
            if (ViewState["action"].Equals("add"))
            {

                if (objOD.ODTYPE == "ODA")
                {
                    // DataSet ds = objCommon.FillDropDown("PAYROLL_OD_APP_ENTRY", "*", "", "ODTRNO<>" + objOD.ODTRNO + " AND STATUS<>'D' AND  EMPNO=" + Convert.ToInt32(Session["idno"]) + " AND ('" + Convert.ToDateTime(txtFromdt.Text).ToString("dd-MMM-yyyy") + "' BETWEEN FROM_DATE AND TO_DATE OR '" + Convert.ToDateTime(txtTodt.Text).ToString("dd-MMM-yyyy") + "' BETWEEN FROM_DATE AND TO_DATE )", "");
                    DataSet ds = objCommon.FillDropDown("PAYROLL_OD_APP_ENTRY", "*", "", "ODTRNO<>" + objOD.ODTRNO + " AND STATUS NOT IN ('D','R') AND  EMPNO=" + Convert.ToInt32(Session["idno"]) + " AND ('" + Convert.ToDateTime(txtFromdt.Text).ToString("dd-MMM-yyyy") + "' BETWEEN FROM_DATE AND TO_DATE OR '" + Convert.ToDateTime(txtTodt.Text).ToString("dd-MMM-yyyy") + "' BETWEEN FROM_DATE AND TO_DATE )", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox("Record Already Exists!");
                        return;
                    }
                }
                //else if (objOD.ODTYPE == "ODS")
                //{
                //    string tIN = DateTime.ParseExact(txtOutTime.Text, "h:mm tt", CultureInfo.InvariantCulture).ToString("HH:mm:ss");
                //    string tOUT = DateTime.ParseExact(txtInTime.Text, "h:mm tt", CultureInfo.InvariantCulture).ToString("HH:mm:ss");
                //    //DataSet ds = objCommon.FillDropDown("PAYROLL_OD_APP_ENTRY", "*", "", "ODTRNO<>" + objOD.ODTRNO + " AND STATUS NOT IN ('D','R') AND  EMPNO=" + Convert.ToInt32(Session["idno"]) + " AND ('" + Convert.ToDateTime(txtJoindt.Text).ToString("dd-MMM-yyyy") + "' BETWEEN FROM_DATE AND TO_DATE OR '" + Convert.ToDateTime(txtJoindt.Text).ToString("dd-MMM-yyyy") + "' BETWEEN FROM_DATE AND TO_DATE )", "");
                //    DataSet ds = objCommon.FillDropDown("PAYROLL_OD_APP_ENTRY", "*", "", "ODTRNO<>" + objOD.ODTRNO + " AND STATUS NOT IN ('D','R') AND  EMPNO=" + Convert.ToInt32(Session["idno"]) + " AND ('" + Convert.ToDateTime(txtJoindt.Text).ToString("dd-MMM-yyyy") + "' BETWEEN FROM_DATE AND TO_DATE OR '" + Convert.ToDateTime(txtJoindt.Text).ToString("dd-MMM-yyyy") + "' BETWEEN FROM_DATE AND TO_DATE )" + "' AND OUT_TIME='" + tOUT + "' AND IN_TIME='" + tIN + "'", "");
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        MessageBox("Record Already Exists!");
                //        return;
                //    }
                //}

                DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
                string userno = Session["userno"].ToString();
                //DataSet dsAuth = new DataSet();
                //dsAuth = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "*", "", "UA_NO=" + userno, "");

                //if (dsAuth.Tables[0].Rows.Count > 0)
                //{
                //    CustomStatus cs = (CustomStatus)objApp.AddAPP_ENTRY_ODauth(objOD, Convert.ToInt32(userno), Aprdate);
                //    if (cs.Equals(CustomStatus.RecordSaved))
                //    {
                //        MessageBox("Record Saved Successfully");
                //        clear();
                //        pnlAdd.Visible = false;
                //        pnllist.Visible = false;
                //        //pnlODStatus.Visible = false;
                //        lnkbut.Visible = true;
                //        lnkNew.Visible = true;
                //        // pnlShowBudg.Visible = false;
                //        // BindListViewODapplStatus();
                //        BindListViewODStatus();

                //    }
                //}
                //else
                //{
                    CustomStatus cs = (CustomStatus)objApp.AddAPP_ENTRY_OD(objOD);
                    if (fuUploadImage.HasFile)
                    {
                        int idno = Convert.ToInt32(Session["idno"]);
                        objOD.FileName = Convert.ToString(fuUploadImage.PostedFile.FileName.ToString());
                        objServiceBook.upload_new_files("LEAVE_CERTIFICATE_DOCUMENT", idno, "ODTRNO", "PAYROLL_OD_APP_ENTRY", "ODTRNO_", fuUploadImage);

                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        MessageBox("Record Saved Successfully");
                        clear();
                        pnlAdd.Visible = false;
                        pnllist.Visible = false;
                        //pnlODStatus.Visible = false;
                        lnkbut.Visible = true;
                        lnkNew.Visible = true;
                        // pnlShowBudg.Visible = false;
                        //BindListViewODapplStatus();
                        BindListViewODStatus();
                        pnlbtn.Visible = false;
                    }
               // }
            }
            else if (ViewState["action"].Equals("edit"))
            {
                if (fuUploadImage.HasFile)
                {
                    objOD.FileName = Convert.ToString(fuUploadImage.PostedFile.FileName.ToString());
                    ViewState["FileName"] = objOD.FileName;
                }
                if (ViewState["FileName"] != null)
                {
                    objOD.FileName = ViewState["FileName"].ToString();

                }
                else
                {
                    objOD.FileName = string.Empty;
                }
                int idno = Convert.ToInt32(Session["idno"]);
                CustomStatus cs = (CustomStatus)objApp.UpdateAppEntry(objOD);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    //MessageBox("Record Updated Sucessfully");
                    if (fuUploadImage.HasFile)
                    {
                        objServiceBook.update_upload("LEAVE_CERTIFICATE_DOCUMENT", idno, ViewState["FileName"].ToString(), objOD.ODTRNO, "ODTRNO_", fuUploadImage);
                    }
                    clear();
                    pnlAdd.Visible = false;
                    pnllist.Visible = false;
                    //pnlODStatus.Visible = false;
                    lnkbut.Visible = true;
                    lnkNew.Visible = true;
                    // pnlShowBudg.Visible = false;
                    MessageBox("Record Updated Successfully");

                    pnlbtn.Visible = false;
                    btnBack.Visible = false;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;


                    //BindListViewODapplStatus();
                }
            }
            ViewState["letrno"] = null;

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
        //txtodpurpose.Text = txteventtype.Text = string.Empty;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        clear();
        pnlAdd.Visible = false;
        pnllist.Visible = false;
        lnkbut.Visible = true;
        pnlbtn.Visible = false;
        lnkNew.Visible = true;

        //pnlODStatus.Visible = false;

    }
    protected void lnkbut_Click(object sender, EventArgs e)
    {
        BindListViewODapplStatus();
        //pnlODStatus.Visible = true;
        //pnlODInfo.Visible = true;
        lnkNew.Visible = false;
        pnlAdd.Visible = false;
        pnllist.Visible = true;
        lnkbut.Visible = false;
        pnlbtn.Visible = true;

        pnlbtn.Visible = true;
        btnBack.Visible = true;
        btnSave.Visible = false;
        btnCancel.Visible = false;
    }
    protected void lnkNew_Click(object sender, EventArgs e)
    {
        lnkbut.Visible = false;
        lnkNew.Visible = false;
        pnlbtn.Visible = true;

        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;
        GetPAPath1();

        if (Convert.ToInt32(ViewState["papno"]) != 0)
        {
            pnlAdd.Visible = true;
            pnllist.Visible = false;
            ViewState["action"] = "add";
            trfrmto.Visible = false;



            trinout.Visible = true;
            // trcomment.Visible = true;                    
            //GetPAPath(Convert.ToInt32(Session["userno"]));

            // GetBudgetEmp();
            //  pnlShowBudg.Visible = false;
            clear();
            //Code to display OdSlip part
            rblODType.SelectedValue = "0";
            trfrmto.Visible = false;



            //trcomment.Visible = false;
            lblJoinindt.Text = "<b>Slip Date</b>";
            txtJoindt.Text = DateTime.Now.ToString();
            lnkbut.Visible = false;
        }

    }

    protected void GetPAPath1()
    {
        try
        {

            string path = string.Empty;
            string userno = Session["userno"].ToString();//  
            DataSet dsAuth = new DataSet();
            dsAuth = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "*", "", "UA_NO=" + userno, "");
            //string pano = dsAuth.Tables[0].Rows[0]["PANO"].ToString();

            DataSet dsdept = new DataSet();
            dsdept = objCommon.FillDropDown("PAYROLL_EMPMAS", "SUBDEPTNO", "IDNO", "IDNO=" + Convert.ToInt32(Session["Idno"]), "");
            int dept = Convert.ToInt32(dsdept.Tables[0].Rows[0]["SUBDEPTNO"].ToString());

            DataSet dspath = new DataSet();

            dspath = objCommon.FillDropDown("PAYROLL_OD_PASSING_AUTHORITY_PATH", "*", "", "IDNO=" + Convert.ToInt32(Session["Idno"]), "");
            // dspath = objCommon.FillDropDown("PAYROLL_OD_PASSING_AUTHORITY_PATH", "*", "", "DEPTNO=" + Convert.ToInt32(dept), "");
            if (dspath.Tables[0].Rows.Count > 0)
            {
                ViewState["papno"] = dspath.Tables[0].Rows[0]["PAPNO"].ToString();
            }
            else
            {
                MessageBox("OD Passing Path Not found");
                ViewState["papno"] = "0";
                return;
            }
            //for (int i = 0; i < dspath.Tables[0].Columns.Count; i++)
            //{
            //    if (dspath.Tables[0].Rows[0][i].ToString() == pano)
            //    {
            //        string colname = dspath.Tables[0].Columns[i].ColumnName;


            //    }
            //    string nextcol = dspath.Tables[0].Columns[i + 1].ColumnName;
            //}

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
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.GetPAPath ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }



    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewODapplStatus();
    }
    protected void dpODinfo_PreRender(object sender, EventArgs e)
    {
        BindListViewODStatus();
    }
    protected void btnReport_click(object sender, EventArgs e)
    {

        try
        {
            int idtrno = 0;
            Button btnReport = sender as Button;
            idtrno = Convert.ToInt32(btnReport.CommandArgument);


            //Button btnReport = ((Button)lvODinfo.FindControl("btnReport"));
            //idtrno = Convert.ToInt32(btnReport.CommandArgument); 
            //foreach (ListViewDataItem item in lvODinfo.Items)
            //{
            //    Button btnReport = item.FindControl("btnReport") as Button;

            //    idtrno = Convert.ToInt32(btnReport.CommandArgument);
            //    ShowReport("Leave_Od_Apply", "ESTB_ODAPPLY.rpt", idtrno);
            //}
            ShowReport("Leave_Od_Apply", "ODLeaveReport.rpt", idtrno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Od_Apply.btnShowReport_Click->" + ex.Message + ' ' + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void ShowReport(string reportTitle, string rptFileName, int idtrno)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT," + rptFileName;
            url += "&param=@P_ODTRNO=" + idtrno + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EMPNO=" + Convert.ToInt32(Session["idno"]) + ",@username=" + Session["userfullname"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.ShowReport->" + ex.Message + ' ' + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void GetPAPath(int EmpNo)
    {
        //ddlPath.Items.Clear();
        try
        {
            DataSet ds = null;
            ds = objApp.GetPAPath_EmpNO(EmpNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ddlPath.DataSource = ds;
                //ddlPath.DataValueField = ds.Tables[0].Columns[0].ToString();
                //ddlPath.DataTextField = ds.Tables[0].Columns[1].ToString();
                //ddlPath.DataBind();
                //ddlPath.SelectedIndex = 0;
                txtPath.Text = ds.Tables[0].Rows[0]["PAPATH"].ToString();
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

    //To get budget info
    //private void GetBudgetEmp()
    //{
    //    DataSet dsbudg = null;
    //    int deptno = 0;
    //    int idno = Convert.ToInt32(Session["idno"]);
    //    deptno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS","SUBDEPTNO","IDNO="+idno));
    //    dsbudg = objApp.GetBudget(deptno);
    //    if (dsbudg.Tables[0].Rows.Count > 0)
    //    {
    //        lblBudgAllot.Text = dsbudg.Tables[0].Rows[0]["BUDG_ALLOT"].ToString();
    //        lblBudgUtil.Text = dsbudg.Tables[0].Rows[0]["BUDG_UTILIZED"].ToString();
    //        lblBudgBal.Text = dsbudg.Tables[0].Rows[0]["BUDG_BAL"].ToString();
    //    }
    //    else
    //    {
    //        lblBudgAllot.Text = "0.00";
    //        lblBudgUtil.Text = "0.00";
    //        lblBudgBal.Text = "0.00";
    //    }

    //}

    protected void txtTodt_TextChanged(object sender, EventArgs e)
    {

        DataSet ds = null;
        try
        {
            if (rblODType.SelectedValue == "1")
            {
                if (txtTodt.Text.ToString() != string.Empty && txtTodt.Text.ToString() != "__/__/____" && txtFromdt.Text.ToString() != string.Empty && txtFromdt.Text.ToString() != "__/__/____")
                {
                    DateTime dtfrm = Convert.ToDateTime(txtFromdt.Text);
                    DateTime dtto = Convert.ToDateTime(txtTodt.Text);
                    if (dtfrm > dtto)
                    {
                        MessageBox("To Date Must Be Equal To Or Greater Than From Date");
                        btnSave.Enabled = false;
                        return;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                    }
                }
                int day = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_REF", "ISNULL(OD_DAYS_APP,0)", ""));

                DateTime dt = Convert.ToDateTime(txtFromdt.Text);
                string todt = dt.ToShortDateString();
                DateTime today = System.DateTime.Now.Date;
                string todate = today.ToShortDateString();
                DateTime sub = today.AddDays(-day);
                string validdate = sub.ToShortDateString();
                // Code for OD Application Capping
                if (Convert.ToDateTime(txtFromdt.Text) < (Convert.ToDateTime(validdate)))
                {
                    //if (day == 0)
                    //{
                    //    MessageBox("OD Application Not Allow After The Current Date");

                    //}
                    //else
                    //{
                    //    MessageBox("Not Allowed, Please check From Date, it should not be less than " + day + " day of today");

                    //}
                    //btnSave.Enabled = false;
                    //return;
                }
                //End OD Appl. Capping

            }




            if (txtTodt.Text.ToString() != string.Empty && txtTodt.Text.ToString() != "__/__/____" && txtFromdt.Text.ToString() != string.Empty && txtFromdt.Text.ToString() != "__/__/____")
            {
                int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + Convert.ToInt32(Session["idno"])));

                ds = objApp.GetNoofdaysOD(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), stno, Convert.ToInt32(ViewState["COLLEGE_NO"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtNodays.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    string join = string.Empty;
                    string JOI = Convert.ToString(ds.Tables[1].Rows[0][0]);
                    DateTime joindt = Convert.ToDateTime(JOI);
                    string day = joindt.DayOfWeek.ToString();
                    if (day == "Sunday")
                    {
                        join = joindt.AddDays(1).ToString();
                        txtJoindt.Text = join;
                    }
                    else
                    {

                        txtJoindt.Text = Convert.ToString(ds.Tables[1].Rows[0][0]);
                    }

                    int days = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_REF", "ISNULL(OD_DAYS_APP,0)", ""));

                    DateTime today = System.DateTime.Now.Date;
                    objlv.JOINDT = Convert.ToDateTime(txtJoindt.Text);
                    objlv.FROMDT = Convert.ToDateTime(txtFromdt.Text);
                    objlv.NO_DAYS = days;
                    objlv.STNO = stno;
                    objlv.COLLEGE_NO = Convert.ToInt32(ViewState["COLLEGE_NO"].ToString());
                    objlv.IsAllowBeforeApplication = false;
                    //=========================

                    int allow_days = objApp.GetAllowDays(objlv);
                    DateTime CheckDate;
                    CheckDate = objlv.JOINDT.AddDays(allow_days);
                    if (today > CheckDate)//10>7
                    {
                        // not allow.
                        // MessageBox("Not Allowed ! Please check From Date Fromdate should not be more than before" + allow_days + " day of today");
                        //    string Fdate = (String.Format("{0:u}", Convert.ToDateTime(frmdate.ToString())));
                        //Fdate = Fdate.Substring(0, 10);

                        //MessageBox("Not Allowed ! Please check From Date. Application allow to fill up to " +   CheckDate.ToShortDateString());
                        MessageBox("Not Allowed ! This application not allow to fill after " + CheckDate.ToShortDateString());
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        //allow
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.txtTodt_TextChanged->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void DateInfo()
    {
        DataSet ds = null;
        try
        {

            if (rblODType.SelectedValue == "1")
            {
                int days = Convert.ToInt32(objCommon.LookUp("PAY_LEAVE_REF", "OD_DAYS", ""));
                DateTime dt = Convert.ToDateTime(txtTodt.Text);
                string todt = dt.ToShortDateString();
                DateTime today = System.DateTime.Now.Date;
                string todate = today.ToShortDateString();
                DateTime sub = today.AddDays(-days);
                string validdate = sub.ToShortDateString();
                DateTime frmdt = Convert.ToDateTime(txtFromdt.Text);
                string fromdt = frmdt.ToShortDateString();
                int frm = Convert.ToInt32(fromdt.Substring(0, 2));
                int todaydt = Convert.ToInt32(todate.Substring(0, 2));

                //if (frm < todaydt)
                //{

                if (dt < sub)
                {

                    MessageBox("Not Allowed ,Please check To Date Todate is not more than before" + days + "day of today");
                    btnSave.Enabled = false;

                }
                //}

                else
                {
                    btnSave.Enabled = true;
                }

            }



            if (txtTodt.Text.ToString() != string.Empty && txtTodt.Text.ToString() != "__/__/____" && txtFromdt.Text.ToString() != string.Empty && txtFromdt.Text.ToString() != "__/__/____")
            {
                int stno = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "STNO", "IDNO=" + Convert.ToInt32(Session["idno"])));

                ds = objApp.GetNoofdaysOD(Convert.ToDateTime(txtFromdt.Text), Convert.ToDateTime(txtTodt.Text), stno, Convert.ToInt32(ViewState["COLLEGE_NO"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtNodays.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    string join = string.Empty;
                    string JOI = Convert.ToString(ds.Tables[1].Rows[0][0]);
                    DateTime joindt = Convert.ToDateTime(JOI);
                    string day = joindt.DayOfWeek.ToString();
                    if (day == "Sunday")
                    {
                        join = joindt.AddDays(1).ToString();
                        txtJoindt.Text = join;
                    }
                    else
                    {

                        txtJoindt.Text = Convert.ToString(ds.Tables[1].Rows[0][0]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_OD_Apply.txtTodt_TextChanged->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            Int32 ODTRNO = int.Parse(btnDelete.CommandArgument);
            //DataSet ds = objCommon.FillDropDown("PAYROLL_OD_APP_PASS_ENTRY", "STATUS", "PANO", "ODTRNO=" + ODTRNO + " ", "PANO");
            //int total = ds.Tables[0].Rows.Count;
            //for (int i = 0; i < total; i++)
            //{
            //    //Code to avoid deletion of record if 1st authority has approved leave
            //    string status = ds.Tables[0].Rows[i]["STATUS"].ToString();
            //    if (status == "A")
            //    {
            //        MessageBox("Approval In Progress, Not Allow To Delete");
            //        return;
            //    }
            //}
            DataSet ds = new DataSet();
            string status = string.Empty;
            ds = objCommon.FillDropDown("PAYROLL_OD_APP_ENTRY", "*", "", "ODTRNO=" + ODTRNO, "");

            status = ds.Tables[0].Rows[0]["STATUS"].ToString();
            if (status == "A")
            {
                btnSave.Enabled = false;
                MessageBox("The OD leave is approved, you cannot delete it.");
                return;
            }
            else if (status == "R")
            {
                btnSave.Enabled = false;
                MessageBox("The OD leave is rejected, you cannot delete it.");
                return;
            }
            else
            {
                btnSave.Enabled = true;
            }

            ds = objCommon.FillDropDown("PAYROLL_OD_APP_PASS_ENTRY", "*", "", "ODTRNO=" + ODTRNO, "");

            status = ds.Tables[0].Rows[0]["STATUS"].ToString();
            if (status == "A" || status == "F")
            {
                MessageBox("The OD leave is in approval process, you cannot delete it.");
                return;
            }  

            CustomStatus cs = (CustomStatus)objApp.DeleteODAppEntry(ODTRNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                MessageBox("Record Deleted Sucessfully");
                ViewState["action"] = null;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.btnDelete_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            ImageButton btnEdit = sender as ImageButton;
            Int32 ODTRNO = int.Parse(btnEdit.CommandArgument);
            //            Int32 lno = int.Parse(btnEdit.ToolTip);
            ViewState["letrno"] = ODTRNO;
            string status = string.Empty;
            // ViewState["LNO"] = lno;

            ds = objCommon.FillDropDown("PAYROLL_OD_APP_ENTRY", "*", "", "ODTRNO=" + ODTRNO, "");

            status = ds.Tables[0].Rows[0]["STATUS"].ToString();
            if (status == "A")
            {
                btnSave.Enabled = false;
                MessageBox("The OD leave is approved, you cannot modify it.");
                return;
            }
            else if (status == "R")
            {
                btnSave.Enabled = false;
                MessageBox("The OD leave is rejected, you cannot modify it.");
                return;
            }
            else
            {
                btnSave.Enabled = true;
            }

            ds = objCommon.FillDropDown("PAYROLL_OD_APP_PASS_ENTRY", "*", "", "ODTRNO=" + ODTRNO, "");
            //ds = objCommon.FillDropDown("PAYROLL_OD_APP_PASS_ENTRY", "STATUS", "PANO", "ODTRNO=" + ODTRNO + " ", "PANO");
            //int total = ds.Tables[0].Rows.Count;
            //for (int i = 0; i < total; i++)
            //{
            //Code to avoid modification of record if 1st authority has approved leave (in case of more than 1 authority)
            status = ds.Tables[0].Rows[0]["STATUS"].ToString();
            if (status == "A" || status == "F")
            {
                MessageBox("The OD leave is in approval process, you cannot modify it.");
                return;
            }            // }
            


            ShowDetailsFromAppl(ODTRNO);
            //GetPAPath(Convert.ToInt32(Session["userno"]));
            GetPAPath1();
            ViewState["action"] = "edit";

            pnlAdd.Visible = true;
            pnllist.Visible = false;
            pnlbtn.Visible = true;
            //pnlODStatus.Visible = false;
            lnkbut.Visible = false;
            lnkNew.Visible = false;

            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnBack.Visible = true;



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.btnEdit_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }

    }

    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }


    protected void ShowDetailsFromAppl(int ODTRNO)
    {
        DataSet ds = null;
        try
        {

            ds = objApp.GetODAppEntryByNOForEdit(ODTRNO);
            if (ds.Tables[0].Rows.Count > 0)
            {





                string od_type = ds.Tables[0].Rows[0]["ODTYPE"].ToString();
                if (od_type == "ODS")
                {
                    rblODType.SelectedValue = "0";
                    trfrmto.Visible = false;
                    trEventRange.Visible = false;
                    lblJoinindt.Text = "Slip Date";

                    trinout.Visible = true;


                    txtOutTime.Text = ds.Tables[0].Rows[0]["OUT_TIME"].ToString();
                    txtIn.Text = ds.Tables[0].Rows[0]["INTIME"].ToString();
                    txtInTime.Text = ds.Tables[0].Rows[0]["IN_TIME"].ToString();
                    txtOut.Text = ds.Tables[0].Rows[0]["OUTTIME"].ToString();

                }
                else
                {
                    rblODType.SelectedValue = "1";
                    trfrmto.Visible = true;
                    lblJoinindt.Text = "Joining Date";
                    trinout.Visible = false;
                    //trcomment.Visible = false;
                    // trEventRange.Visible = true;


                    txtFromdt.Text = ds.Tables[0].Rows[0]["From_date"].ToString();
                    txtTodt.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                    txtEventFrm.Text = ds.Tables[0].Rows[0]["EVENT_FRMDT"].ToString();
                    txtEventTo.Text = ds.Tables[0].Rows[0]["EVENT_TODT"].ToString();
                    txtNodays.Text = ds.Tables[0].Rows[0]["NO_OF_DAYS"].ToString();


                }

                //int purno =Convert.ToInt32( ds.Tables[0].Rows[0]["PURPOSENO"]);
                //if (purno > 0)
                //{ 
                //    ddlPurpose.SelectedValue = ds.Tables[0].Rows[0]["PURPOSENO"].ToString();
                //}
                //ddlEventType.SelectedValue = ds.Tables[0].Rows[0]["PROGRAM_NO"].ToString();
                int tada_status = Convert.ToInt32(ds.Tables[0].Rows[0]["TADA_AMT"]);
                if (tada_status == 1)
                {
                    rblTA.SelectedValue = "1";
                }
                else
                {
                    rblTA.SelectedValue = "0";
                }
                ddlPurpose.SelectedValue = ds.Tables[0].Rows[0]["PURPOSENO"].ToString();
                ddlEventType.SelectedValue = ds.Tables[0].Rows[0]["PROGRAM_NO"].ToString();
                txtRegAmt.Text = ds.Tables[0].Rows[0]["REG_AMT"].ToString();

                //txtTADA.Text = ds.Tables[0].Rows[0]["TADA_AMT"].ToString();
                txtPlace.Text = ds.Tables[0].Rows[0]["PLACE"].ToString();
                txtTopic.Text = ds.Tables[0].Rows[0]["TOPIC"].ToString();
                txtOrganised.Text = ds.Tables[0].Rows[0]["ORGANISED_BY"].ToString();
                txtInstruct.Text = ds.Tables[0].Rows[0]["INSTRUCTED_BY"].ToString();
                txtJoindt.Text = ds.Tables[0].Rows[0]["JOINDT"].ToString();
                ViewState["FileName"] = ds.Tables[0].Rows[0]["FileName"].ToString();


            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Aprval_Estb.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }

    private void FillPurpose()
    {
        objCommon.FillDropDownList(ddlPurpose, "PAYROLL_OD_PURPOSE", "purposeno", "PURPOSE", "", "");
        objCommon.FillDropDownList(ddlEventType, "PROGRAM_TYPE", "PROGRAM_NO", "PROGRAM_TYPE", "", "");
    }


    protected void rblODType_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear();
        if (rblODType.SelectedValue == "0")
        {
            trfrmto.Visible = false;

            lblJoinindt.Text = "<b>Slip Date</b>";

            trinout.Visible = true;

            // trcomment.Visible = true;
            trEventRange.Visible = false;
            txtJoindt.Text = DateTime.Now.ToString();

        }
        else
        {
            trfrmto.Visible = true;

            lblJoinindt.Text = "<b> Joining Date </b>";
            trpurpose.Visible = true;

            trinout.Visible = false;
            // trcomment.Visible = false;
            // trEventRange.Visible = true;
        }
    }
    protected void txtJoindt_TextChanged(object sender, EventArgs e)
    {
        if (rblODType.SelectedValue == "0")
        {
            DateTime Test;
            if (DateTime.TryParseExact(txtJoindt.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
            {
                int day = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_REF", "OD_DAYS", ""));

                DateTime dt = Convert.ToDateTime(txtJoindt.Text);
                //string slipdate = dt.ToShortDateString();
                //string todaydate = System.DateTime.Now.ToShortDateString();
                //if (slipdate != todaydate)
                //{
                //    MessageBox("Not Allowed,Please Check Slip Date");
                //    btnSave.Enabled = false;
                //}
                //else
                //{
                //    btnSave.Enabled = true;
                //}


                string todt = dt.ToShortDateString();
                DateTime today = System.DateTime.Now.Date;
                string todate = today.ToShortDateString();
                DateTime sub = today.AddDays(-day);
                string validdate = sub.ToShortDateString();
                //DateTime frmdt = Convert.ToDateTime(txtFromdt.Text);
                //string fromdt = frmdt.ToShortDateString();
                //int frm = Convert.ToInt32(fromdt.Substring(0, 2));
                //int todaydt = Convert.ToInt32(todate.Substring(0, 2));

                if (dt < sub)
                {

                    MessageBox("Not Allowed ,Please check, Slip Date is not less than " + day + "day from today");
                    btnSave.Enabled = false;

                }
                else
                {
                    btnSave.Enabled = true;
                }
            }
            else
            {
                txtJoindt.Text = string.Empty;
            }


        }
        else if (rblODType.SelectedValue == "1")
        {


        }
    }
    protected void txtEventFrm_TextChanged(object sender, EventArgs e)
    {
        if (txtEventFrm.Text.ToString() != string.Empty && txtEventFrm.Text.ToString() != "__/__/____" && txtEventTo.Text.ToString() != string.Empty && txtEventTo.Text.ToString() != "__/__/____")
        {
            if (Convert.ToDateTime(txtEventFrm.Text) > Convert.ToDateTime(txtEventTo.Text))
            {
                MessageBox("Event To Date should be Greater Than Or Equals To Event From Date");
                return;
            }
        }
    }
    protected void txtEventTo_TextChanged(object sender, EventArgs e)
    {
        if (txtEventFrm.Text.ToString() != string.Empty && txtEventFrm.Text.ToString() != "__/__/____" && txtEventTo.Text.ToString() != string.Empty && txtEventTo.Text.ToString() != "__/__/____")
        {
            if (Convert.ToDateTime(txtEventFrm.Text) > Convert.ToDateTime(txtEventTo.Text))
            {
                MessageBox("Event To Date should be Greater Than Or Equals To Event From Date");
                return;
            }
        }
    }
    protected void txtFromdt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //txtTodt.Text = txtNodays.Text = txtJoindt.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Application.txtTodt_TextChanged->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
}
