//======================================================================================
// PROJECT NAME  : CCMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ITChallanTranEntry.aspx                                                  
// CREATION DATE : 19-April-2011                                                      
// CREATED BY    : Mrunal Bansod                                                   
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================


using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Pay_ITChallanTranEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITMasController objITMas = new ITMasController();

    string UsrStatus = string.Empty;
    string colvalues = string.Empty;
    string colheads = string.Empty;
    string colMonth = string.Empty;
    string colmonValues = string.Empty;

    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }


    //Checking logon Status and redirection to Login Page if user is not logged in
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

                FillDropdown();
                pnlNewEmp.Visible = false;
                pnlPrint.Visible = false;
                ddlChallanNo.SelectedIndex = 0;

                txtFrom.Enabled = false;
                //imgFrom.Enabled = false;
                txtTo.Enabled = false;
                //imgTo.Enabled = false;
                ViewState["action"] = "add";
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_ITChallanTranEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ITChallanTranEntry.aspx");
        }
    }



    #endregion

    #region Action

    //Retrive Data on click event of image button
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        
        
        try
        {
            pnlNewEmp.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            int schIdno = int.Parse(btnEdit.CommandArgument);
            ShowDetail(schIdno);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        
    }

    //Deletes the challan entry Delete Button Click
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btnDel = sender as ImageButton;
            int SCHIDNO = int.Parse(btnDel.CommandArgument);
            DeleteChalanTranEntry(SCHIDNO);
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //call method to display details
    protected void ddlChallanNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetChalanEntryDetail(Convert.ToInt32(ddlChallanNo.SelectedItem.Text));
        GetChalanEntryDetail(Convert.ToInt32(ddlChallanNo.SelectedValue));
        pnlList.Visible = true;
        BindListView();
    }

    //display panel
    protected void lnkAddNew_Click(object sender, EventArgs e)
    {
        pnlNewEmp.Visible = true;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlNewEmp.Visible = false;
        clear();

    }

    //Add/Update
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        try
        {
            ITChalanTranEntry objChalanTran = new ITChalanTranEntry();

            //objChalanTran.CHIDNO = Convert.ToInt32(ddlChallanNo.SelectedValue);
            //objChalanTran.IDNO = Convert.ToInt32(ddlEmployee.SelectedValue);
            //if (!txtChDate.Text.Trim().Equals(string.Empty)) objChalanTran.PAYDATE = Convert.ToDateTime(txtChDate.Text.Trim());
            //if (!txtGS.Text.Trim().Equals(string.Empty)) objChalanTran.GSAMT = Convert.ToDecimal(txtGS.Text.Trim());
            //if (!txtChalAmt.Text.Trim().Equals(string.Empty)) objChalanTran.CHAMT = Convert.ToDecimal(txtChalAmt.Text.Trim());
            //if (!txtCharges.Text.Trim().Equals(string.Empty)) objChalanTran.CHSCHARGE = Convert.ToDecimal(txtCharges.Text.Trim());
            //if (!txtEduCess.Text.Trim().Equals(string.Empty)) objChalanTran.CHEDUCESS = Convert.ToDecimal(txtEduCess.Text.Trim());
            //objChalanTran.COLLEGECODE = Session["colcode"].ToString();

           


            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {

                    foreach (ListViewDataItem lvitem in lvChallan.Items)
                    {
                        TextBox txtGross = lvitem.FindControl("txtGrossAmt") as TextBox;
                        TextBox txtchalanamt = lvitem.FindControl("txtCHAMT") as TextBox;
                        TextBox txtsurcharge = lvitem.FindControl("txtCHSCHARGE") as TextBox;
                        TextBox txteducess = lvitem.FindControl("txtCHEDUCESS") as TextBox;
                        Label lblIdNo = lvitem.FindControl("lblIDNO") as Label;
                        Label lblName = lvitem.FindControl("lblName") as Label;
                        Label lblDesignation = lvitem.FindControl("lblDesignation") as Label;

                        objChalanTran.CHIDNO = Convert.ToInt32(ddlChallanNo.SelectedValue);
                        objChalanTran.IDNO = Convert.ToInt32(lblIdNo.Text);
                        if (!txtChDate.Text.Trim().Equals(string.Empty)) objChalanTran.PAYDATE = Convert.ToDateTime(txtChDate.Text.Trim());
                        if (!txtGross.Text.Trim().Equals(string.Empty)) objChalanTran.GSAMT = Convert.ToDecimal(txtGross.Text.Trim());
                        if (!txtchalanamt.Text.Trim().Equals(string.Empty)) objChalanTran.CHAMT = Convert.ToDecimal(txtchalanamt.Text.Trim());
                        if (!txtsurcharge.Text.Trim().Equals(string.Empty)) objChalanTran.CHSCHARGE = Convert.ToDecimal(txtCharges.Text.Trim());
                        if (!txteducess.Text.Trim().Equals(string.Empty)) objChalanTran.CHEDUCESS = Convert.ToDecimal(txteducess.Text.Trim());
                        objChalanTran.COLLEGECODE = Session["colcode"].ToString();

                        CustomStatus cs = (CustomStatus)objITMas.AddITChalanTranEntry(objChalanTran);

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindListView();
                            clear();
                            //objCommon.DisplayMessage(this.updpanel, "Record Saved Successfully!", this.Page);
                            MessageBox("Record Saved Successfully!");
                        }
                        else

                            //objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                            MessageBox("Exception Occured!");
                    }


                    

                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                    if (ViewState["lblSchIdno"] != null)
                    {

                        objChalanTran.SCHIDNO = Convert.ToInt32(ViewState["lblSchIdno"].ToString().Trim());
                        CustomStatus cs = (CustomStatus)objITMas.UpdateChalanTranEntry(objChalanTran);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            //objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);
                            MessageBox("Record Updated Successfully!");
                            BindListView();
                            ViewState["action"] = "add";

                        }
                    }
                }

               
            }
            clear();
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlChallanNo.SelectedIndex = 0;
        txtMonYear.Text = string.Empty;
        txtChDate.Text = string.Empty;
        txtChallanNo.Text = string.Empty;
        txtChequeDDNo.Text = string.Empty;
        txtTaxDeposited.Text = string.Empty;
        txtSurcharge.Text = string.Empty;
        txtEducationCess.Text = string.Empty;
        txtBSRCode.Text = string.Empty;
        pnlList.Visible = false;

    }

    //to visible panel for Showing Report
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        pnlList.Visible = false;
        pnlPrint.Visible = true;
        BindListViewChalanNo();
        BindListViewAllMonth();
        lvMonth.Enabled = false;
        lvChalanNo.Enabled = false;




    }

    protected void btnPBack_Click(object sender, EventArgs e)
    {
        pnlPrint.Visible = false;
        clearPrint();
        txtFrom.Enabled = false;
        //imgFrom.Enabled = false;
        txtTo.Enabled = false;
        //imgTo.Enabled = false;
    }

    protected void txtTo_TextChanged(object sender, EventArgs e)
    {
        BindListViewMonth();

    }

    protected void chkPeriod_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPeriod.Checked == true)
        {
            txtFrom.Enabled = true;
            //imgFrom.Enabled = true;
            txtTo.Enabled = true;
            //imgTo.Enabled = true;
        }
        else
        {
            BindListViewAllMonth();
            txtFrom.Enabled = false;
            //imgFrom.Enabled = false;
            txtTo.Enabled = false;
            //imgTo.Enabled = false;
        }

    }

    protected void chkSelectChNo_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSelectChNo.Checked == true)
        {
            lvMonth.Enabled = false;
            foreach (ListViewDataItem lvitem in lvMonth.Items)
            {
                CheckBox chk = lvitem.FindControl("ChkMonth") as CheckBox;
                chk.Checked = false;
            }
            chkSelectMonth.Checked = false;
            lvChalanNo.Enabled = true;

        }
        else
        {
            foreach (ListViewDataItem lvitem in lvMonth.Items)
            {
                CheckBox chk = lvitem.FindControl("ChkMonth") as CheckBox;
                chk.Checked = false;
            }
            lvMonth.Enabled = true;
            lvChalanNo.Enabled = false;


        }
    }

    protected void chkSelectMonth_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSelectMonth.Checked == true)
        {
            lvChalanNo.Enabled = false;
            foreach (ListViewDataItem lvitem in lvChalanNo.Items)
            {
                CheckBox chk = lvitem.FindControl("chkChalan") as CheckBox;
                chk.Checked = false;
            }
            chkSelectChNo.Checked = false;
            lvMonth.Enabled = true;
        }
        else
        {
            foreach (ListViewDataItem lvitem in lvMonth.Items)
            {
                CheckBox chk = lvitem.FindControl("ChkMonth") as CheckBox;
                chk.Checked = false;
            }
            lvChalanNo.Enabled = true;
            lvMonth.Enabled = false;
        }
    }

    //To generate report
    protected void btnShow_Click(object sender, EventArgs e)
    {
        int checkcount = 0;
        int checkMonthCount = 0;
        colheads = string.Empty;
        colvalues = string.Empty;
        colMonth = string.Empty;
        colmonValues = string.Empty;
        //pnlList.Visible = true;


        //if (TwoCharReport == "YR")
        //{

            if (chkSelectMonth.Checked == false && chkSelectChNo.Checked == false)
            {
                MessageBox("Select Chalan No. or Month");
            }
            else if (chkSelectChNo .Checked == true || chkSelectMonth .Checked == true )
            {

                //For Selected Chalan No.
                if (chkSelectChNo.Checked == true)
                {


                    foreach (ListViewDataItem lvitem in lvChalanNo.Items)
                    {

                        CheckBox chk = lvitem.FindControl("chkChalan") as CheckBox;
                        Label lbl = lvitem.FindControl("lblch") as Label;
                        if (chk.Checked)
                        {
                            checkcount += 1;
                            colvalues = colvalues + objITMas.GetChalanNoForReport(chk.ToolTip.ToString().ToUpper().Trim(), checkcount);
                            colheads = colheads + lbl.Text + "$";
                        }


                    }
                    if (checkcount == 0)
                    {
                        MessageBox("Please Select Atleast One ChalanNo.");
                        return;
                    }

                    colvalues = colvalues.Substring(0, colvalues.Length - 1);
                    colheads = colheads.Substring(0, colheads.Length - 1);


                }



                //For Selected Month.
                if (chkSelectMonth.Checked == true)
                {


                    foreach (ListViewDataItem lvitem in lvMonth.Items)
                    {

                        CheckBox chk = lvitem.FindControl("ChkMonth") as CheckBox;
                        Label lbl = lvitem.FindControl("lblMonth") as Label;
                        if (chk.Checked)
                        {
                            checkMonthCount += 1;
                            colmonValues = colmonValues + objITMas.GetChalanNoForReport(chk.ToolTip.ToString().ToUpper().Trim(), checkMonthCount);
                            //colMonth = colMonth + lbl.Text + "$";
                        }


                    }
                    if (checkMonthCount == 0)
                    {
                        MessageBox("Please Select Atleast One Month");
                        return;
                    }

                    colmonValues = colmonValues.Substring(0, colmonValues.Length - 1);
                    //colMonth = colMonth.Substring(0, colMonth.Length - 1);

                }


                ShowReport("Annexure - I", "Pay_ITChalanTran_Annexure-I.rpt");
            }
        //}

        //else
        //{
        //    objCommon.DisplayMessage(updpanel, Common.Message.NoReport, this);
        //    return;
        //}
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
            try
            {
                ITChalanTranEntry objChalanTran = new ITChalanTranEntry();

                //objChalanTran.CHIDNO = Convert.ToInt32(ddlChallanNo.SelectedValue);
                //objChalanTran.IDNO = Convert.ToInt32(ddlEmployee.SelectedValue);
                //if (!txtChDate.Text.Trim().Equals(string.Empty)) objChalanTran.PAYDATE = Convert.ToDateTime(txtChDate.Text.Trim());
                //if (!txtGS.Text.Trim().Equals(string.Empty)) objChalanTran.GSAMT = Convert.ToDecimal(txtGS.Text.Trim());
                //if (!txtChalAmt.Text.Trim().Equals(string.Empty)) objChalanTran.CHAMT = Convert.ToDecimal(txtChalAmt.Text.Trim());
                //if (!txtCharges.Text.Trim().Equals(string.Empty)) objChalanTran.CHSCHARGE = Convert.ToDecimal(txtCharges.Text.Trim());
                //if (!txtEduCess.Text.Trim().Equals(string.Empty)) objChalanTran.CHEDUCESS = Convert.ToDecimal(txtEduCess.Text.Trim());
                //objChalanTran.COLLEGECODE = Session["colcode"].ToString();




                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {

                        foreach (ListViewDataItem lvitem in lvChallan.Items)
                        {
                            TextBox txtGross = lvitem.FindControl("txtGrossAmt") as TextBox;
                            TextBox txtchalanamt = lvitem.FindControl("txtCHAMT") as TextBox;
                            TextBox txtsurcharge = lvitem.FindControl("txtCHSCHARGE") as TextBox;
                            TextBox txteducess = lvitem.FindControl("txtCHEDUCESS") as TextBox;
                            Label lblIdNo = lvitem.FindControl("lblIDNO") as Label;
                            Label lblName = lvitem.FindControl("lblName") as Label;
                            Label lblDesignation = lvitem.FindControl("lblDesignation") as Label;

                            objChalanTran.CHIDNO = Convert.ToInt32(ddlChallanNo.SelectedValue);
                            objChalanTran.IDNO = Convert.ToInt32(lblIdNo.Text);
                            if (!txtChDate.Text.Trim().Equals(string.Empty)) objChalanTran.PAYDATE = Convert.ToDateTime(txtChDate.Text.Trim());
                            if (!txtGross.Text.Trim().Equals(string.Empty)) objChalanTran.GSAMT = Convert.ToDecimal(txtGross.Text.Trim());
                            if (!txtchalanamt.Text.Trim().Equals(string.Empty)) objChalanTran.CHAMT = Convert.ToDecimal(txtchalanamt.Text.Trim());
                            if (!txtsurcharge.Text.Trim().Equals(string.Empty)) objChalanTran.CHSCHARGE = Convert.ToDecimal(txtsurcharge.Text.Trim());
                            if (!txteducess.Text.Trim().Equals(string.Empty)) objChalanTran.CHEDUCESS = Convert.ToDecimal(txteducess.Text.Trim());
                            objChalanTran.COLLEGECODE = Session["colcode"].ToString();

                            CustomStatus cs = (CustomStatus)objITMas.AddITChalanTranEntry(objChalanTran);

                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                BindListView();
                                clear();
                                //objCommon.DisplayMessage(this.updpanel, "Record Saved Successfully!", this.Page);
                                MessageBox("Record Saved Successfully!");
                                pnlList.Visible = false;
                            }
                            else

                                //objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                                MessageBox("Exception Occured!");
                        }




                    }
                    else if (ViewState["action"].ToString().Equals("edit"))
                    {
                        if (ViewState["lblSchIdno"] != null)
                        {

                            objChalanTran.SCHIDNO = Convert.ToInt32(ViewState["lblSchIdno"].ToString().Trim());
                            CustomStatus cs = (CustomStatus)objITMas.UpdateChalanTranEntry(objChalanTran);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                //objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);
                                MessageBox("Record Updated Successfully!");
                                //BindListView();
                                ViewState["action"] = "add";

                            }
                        }
                    }


                }
                clear();
            }


            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }

    }

    #endregion

    #region Methods
    
   
    //To fill Drpodownlist with chalan no. and employee name
    protected void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlChallanNo, "PAYROLL_ITCHALAN C INNER JOIN PAYROLL_STAFF S ON(C.STAFFNO = S.STAFFNO)", "CHIDNO", "CHALANNO+'-->'+MON+'-->'+S.STAFF", "CHIDNO>0", "CHIDNO");
            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "TITLE+' '+ FNAME + ' ' + MNAME + ' ' + LNAME ", "IDNO>0 ", "FNAME");
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ITChallanTranEntry.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //get details from table on selecting Chalan no. and display on respective textboxes 
    protected void GetChalanEntryDetail(int chalanNo)
    {
        try
        {


            DataTableReader dtr = objITMas.GetChalanEntryDetail(chalanNo);
            if (dtr != null)
            {
                if (dtr.Read())
                {

                    if (dtr["ITVALUE"].ToString() == "1")
                        chkIT.Checked = true;
                    else if (dtr["ITVALUE"].ToString() == "0")
                        chkIT.Checked = false;


                    txtMonYear.Text = dtr["MON"].ToString();
                    txtChDate.Text = dtr["CHALANDT"].ToString();
                    txtChallanNo.Text = dtr["CHALANNO"].ToString();
                    txtChequeDDNo.Text = dtr["CHQDDNO"].ToString();
                    txtTaxDeposited.Text = dtr["CHAMT"].ToString();
                    txtSurcharge.Text = dtr["CHSCHARGE"].ToString();
                    txtEducationCess.Text = dtr["CHEDUCESS"].ToString();
                    txtBSRCode.Text = dtr["BSRNO"].ToString();


                }
                dtr.Close();
            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //Bind List view
    private void BindListView()
    {
        DataSet ds;

        ds = objITMas.GetChallanTranEntry(Convert.ToInt32(ddlChallanNo.SelectedValue));
        lvChallan.DataSource = ds;
        lvChallan.DataBind();

    }

  

    //Display IT related details of Employee on edit button 
    protected void ShowDetail(int schIdno)
    {
        

        try
        {


            DataTableReader dtr = objITMas.GetEmpInfo(schIdno);
            if (dtr != null)
            {
                ViewState["lblSchIdno"] = schIdno;
                if (dtr.Read())
                {

                    //ddlEmployee.SelectedItem.Text = dtr["NAME"].ToString();
                    //ddlEmployee.SelectedValue = dtr["IDNO"].ToString();
                    ddlEmployee.SelectedValue = dtr["IDNO"].ToString ();
                    txtGS.Text = dtr["GSAMT"].ToString();
                    txtChalAmt.Text = dtr["CHAMT"].ToString();
                    txtCharges.Text = dtr["CHSCHARGE"].ToString();
                    txtEduCess.Text = dtr["CHEDUCESS"].ToString();
                    //lblTotal.Text = dtr["TOTAL"].ToString();


                }
                dtr.Close();
            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }

    }

   
    //To delete particular entry
    private void DeleteChalanTranEntry(int SCHIDNO)
    {

        CustomStatus cs = (CustomStatus)objITMas.DeleteChalanTranEntry(SCHIDNO);
        if (cs.Equals(CustomStatus.RecordDeleted))
        {
           //objCommon.DisplayMessage(this.updpanel, "Record Deleted Successfully!", this.Page);
            MessageBox("Record Deleted Successfully!");
           ViewState["action"] = "add";
        }
    }

    
 
 

    //to clear textboxes on back button click
    private void clear()
    {
        ddlEmployee.SelectedIndex = 0;
        txtGS.Text = string.Empty;
        txtChalAmt.Text = string.Empty;
        txtCharges.Text = string.Empty;
        txtEduCess.Text = string.Empty;
        //lblTotal.Text = string.Empty;


    }
          
    //Fetch Defined Chalan No From The Database and display them in a listview
    private void BindListViewChalanNo()
    {
        try
        {
            DataSet ds = objITMas.GetChallaNo();

            if (ds.Tables[0].Rows.Count <= 0)
            {
                lvChalanNo .Visible = false;
            }
            else
            {
               
                lvChalanNo .Visible = true;
                lvChalanNo.DataSource = ds;
                lvChalanNo.DataBind();
                ds.Dispose();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    //Fetch Defined Month Year from Database and Display in ListView.
    private void BindListViewMonth()
    {
        try
        {
            string frmdate = txtFrom .Text.Trim();
            string todate = txtTo .Text.Trim();

            DataSet ds = objITMas.GetAllMonthYear(frmdate, todate);

            if (ds.Tables[0].Rows.Count <= 0)
            {
                lvMonth.Visible = false;
            }
            else
            {
                
                lvMonth.Visible = true;
                lvMonth.DataSource = ds;
                lvMonth.DataBind();
                ds.Dispose();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Fetch Defined Chalan No From The Database and display them in a listview
    private void BindListViewAllMonth()
    {
        try
        {
            DataSet ds = objITMas.GetMonth();

            if (ds.Tables[0].Rows.Count <= 0)
            {
                lvMonth.Visible = false;
            }
            else
            {

                lvMonth.Visible = true;
                lvMonth.DataSource = ds;
                lvMonth.DataBind();
                ds.Dispose();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

 
   

    private void clearPrint()
    {
        txtFrom.Text = string.Empty;
        txtTo.Text = string.Empty;
        foreach (ListViewDataItem lvitem in lvMonth.Items)
        {
            CheckBox chk = lvitem.FindControl("ChkMonth") as CheckBox;
            chk.Checked = false;
        }
        foreach (ListViewDataItem lvitem in lvChalanNo.Items)
        {
            CheckBox chk = lvitem.FindControl("chkChalan") as CheckBox;
            chk.Checked = false;
        }
        chkPeriod.Checked = false;
    }

   


   

  

    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    //function to generate report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PAYROLL," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UserId=" + Session["userfullname"].ToString() + ",@P_ReportName=" + reportTitle + ",@P_CHALAN=" + colvalues + ",@P_MONTH=" + colmonValues + ",@P_CHHEAD=" + colheads;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updpanel, this.updpanel.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    protected void btnShowEmployees_Click(object sender, EventArgs e)
    {
        //GetChalanEntryDetail(Convert.ToInt32(ddlChallanNo.SelectedValue));
        DataSet ds = objITMas.GetEmployeeDetailsForChalanEntries(Convert.ToInt32(ddlChallanNo.SelectedValue));
        pnlList.Visible = true;
        //BindListView();
        lvChallan.DataSource = ds;
        lvChallan.DataBind();
    }
}
