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

public partial class ACCOUNT_AccountPassingAuthorityPath : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AccountPassingAuthority objPA = new AccountPassingAuthority();
    AccountPassingAuthorityController objPAController = new AccountPassingAuthorityController();

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
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                pnlAdd.Visible = false; pnlEmpList.Visible = false;
                pnlbtn.Visible = false;
                pnlList.Visible = true;
                FillLeave();
                FillCollege();
                FillDepartment();
                FillPAuthority();
                BindListViewPAPath();

                //Set Report Parameters 
                // objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_PAPAth.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
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
                Response.Redirect("~/notauthorized.aspx?page=PA_Path.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PA_Path.aspx");
        }
    }

    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepartment();
        FillPAuthority();
    }

    protected void BindListViewPAPath()
    {
        try
        {
            DataSet ds = objPAController.GetAllPAPath(Convert.ToInt32(ddlCollege.SelectedValue));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                btnShowReport.Visible = false;
                //dpPager.Visible = false;
            }
            else
            {
                btnShowReport.Visible = true;
                //dpPager.Visible = true;
            }
            lvPAPath.DataSource = ds;
            lvPAPath.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.BindListViewPAPath ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain  
        BindListViewPAPath();
    }

    private void Clear()
    {
        //ddlLeavename.SelectedIndex = 0;
        ddlDept.SelectedIndex = ddlCollege.SelectedIndex = 0;
        ddlPA01.SelectedIndex = 0;
        ddlPA02.SelectedIndex = 0;
        ddlPA03.SelectedIndex = 0;
        ddlPA04.SelectedIndex = 0;
        ddlPA05.SelectedIndex = 0;
        ddlPA02.Enabled = false;
        ddlPA03.Enabled = false;
        ddlPA04.Enabled = false;
        ddlPA05.Enabled = false;
        txtPAPath.Text = string.Empty;
        trEmp.Visible = false;
        lblEmpName.Text = string.Empty;

        ddlemp.SelectedValue = "0";
        pnlEmpList.Visible = false;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        pnlbtn.Visible = true;
        ddlPA02.Enabled = false;
        ddlPA03.Enabled = false;
        ddlPA04.Enabled = false;
        ddlPA05.Enabled = false;
        ViewState["action"] = "add";
        pnlPAPaList.Visible = false;
    }

    private void FillEmployeeList()
    {
        try
        {
            objCommon.FillDropDownList(ddlemp, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO = P.IDNO)", "E.IDNO", "isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'') AS NAME", "E.COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue) + "  AND P.PSTATUS='Y'", "isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'')");
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void FillLeave()
    {
        try
        {
            //objCommon.FillDropDownList(ddlLeavename, "PAYROLL_LEAVE", "LNO", "LEAVENAME", "", "LEAVENAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.FillLeave ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillEmployee()
    {
        DataSet dsEmp = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO = P.IDNO)", "E.IDNO", "isnull(TITLE,'')+' '+isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'') AS NAME", "E.COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue) + " and E.SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "  AND P.PSTATUS='Y'", "NAME");
        if (dsEmp.Tables[0].Rows.Count > 0)
        {
            lvEmployees.DataSource = dsEmp;
            lvEmployees.DataBind();
            //pnlEmpList.Visible = true;
            //lvEmployees.Visible = true;
        }
        else
        {
            lvEmployees.DataSource = null;
            lvEmployees.DataBind();
        }

        //  objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "FNAME+' '+MNAME+' '+LNAME AS NAME", "SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " AND SCHOOL_NO=" + Convert.ToInt32(ddlSchool.SelectedValue) + " ", "");

    }
    private void FillDepartment()
    {
        try
        {
            if (Convert.ToInt32(ddlCollege.SelectedValue) > 0)
            {
                objCommon.FillDropDownList(ddlDept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT D ON(E.SUBDEPTNO=D.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "D.SUBDEPT", "E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "D.SUBDEPT");
                //objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPT");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.Fill_Department ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillPAuthority()
    {
        try
        {
            objCommon.FillDropDownList(ddlPA01, "ACC_PASSING_AUTHORITY", "PANO", "PANAME", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "PANAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.FillPAuthority ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlEmpList.Visible = false;
        pnlList.Visible = true;
        pnlbtn.Visible = false;
        pnlPAPaList.Visible = true;
        lblEmpName.Text = "";
        trEmp.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            pnlbtn.Visible = false;
            //Leaves objLeaves = new Leaves();
            DataTable dtEmpRecord = new DataTable();
            dtEmpRecord.Columns.Add("IDNO");
            objPA.LNO = 0;
            objPA.DEPTNO = Convert.ToInt32(ddlDept.SelectedValue);
            //objLeaves.PAN01 = Convert.ToInt32(ddlPA01.SelectedValue);
            //objLeaves.PAN02 = Convert.ToInt32(ddlPA02.SelectedValue);
            //objLeaves.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
            //objLeaves.PAN04 = Convert.ToInt32(ddlPA04.SelectedValue);
            //objLeaves.PAN05 = Convert.ToInt32(ddlPA05.SelectedValue);
            if (ddlPA01.SelectedValue != string.Empty)
            {
                objPA.PAN01 = Convert.ToInt32(ddlPA01.SelectedValue);
            }
            else
            {
                objPA.PAN01 = 0;
            }

            //  objLeaves.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
            if (ddlPA02.SelectedValue != string.Empty)
            {
                objPA.PAN02 = Convert.ToInt32(ddlPA02.SelectedValue);
            }
            else
            {
                objPA.PAN02 = 0;
            }

            if (ddlPA03.SelectedValue != string.Empty)
            {
                objPA.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
            }
            else
            {
                objPA.PAN03 = 0;
            }
            if (ddlPA04.SelectedValue != string.Empty)
            {
                objPA.PAN04 = Convert.ToInt32(ddlPA04.SelectedValue);
            }
            else
            {
                objPA.PAN04 = 0;
            }
            if (ddlPA05.SelectedValue != string.Empty)
            {
                objPA.PAN05 = Convert.ToInt32(ddlPA05.SelectedValue);
            }
            else
            {
                objPA.PAN05 = 0;
            }
            objPA.PAPATH = Convert.ToString(txtPAPath.Text);
            objPA.College_Code = Convert.ToInt32(Session["colcode"]);
            objPA.College_No = Convert.ToInt32(ddlCollege.SelectedValue);

            //int count = 0; int count_record = 0;
            if (trEmp.Visible == true)
            {
                DataRow dr = dtEmpRecord.NewRow();
                int idno = Convert.ToInt32(ViewState["IDNO"]);
                dr["IDNO"] = idno;
                dtEmpRecord.Rows.Add(dr);
                dtEmpRecord.AcceptChanges();
                objPA.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());

            }
            else
            {
                DataRow dr = dtEmpRecord.NewRow();
                int idno = Convert.ToInt32(ddlemp.SelectedValue);
                dr["IDNO"] = idno;
                dtEmpRecord.Rows.Add(dr);
                dtEmpRecord.AcceptChanges();

                #region
                //foreach (ListViewItem lvitem in lvEmployees.Items)
                //{
                //    CheckBox chkIdNo = lvitem.FindControl("chkIdNo") as CheckBox;
                //    if (chkIdNo.Checked == true)
                //    {
                //        count = count + 1;
                //        objPA.EMPNO = Convert.ToInt32(chkIdNo.ToolTip);
                //        if (ViewState["action"].ToString().Equals("add"))
                //        {
                //            DataSet ds = objCommon.FillDropDown("ACC_PASSING_AUTHORITY_PATH", "PAPNO", "IDNO", "IDNO=" + objPA.EMPNO + " ", "");
                //            if (ds.Tables[0].Rows.Count <= 0)
                //            {
                //                count_record = count_record + 1;
                //                DataRow dr = dtEmpRecord.NewRow();
                //                dr["IDNO"] = objPA.EMPNO;
                //                dtEmpRecord.Rows.Add(dr);
                //                dtEmpRecord.AcceptChanges();
                //            }

                //        }
                //        else
                //        {

                //            objPA.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());
                //            DataSet ds = objCommon.FillDropDown("ACC_PASSING_AUTHORITY_PATH", "PAPNO", "IDNO", "IDNO=" + objPA.EMPNO + " AND PAPNO!=" + objPA.PAPNO + " ", "");
                //            if (ds.Tables[0].Rows.Count <= 0)
                //            {
                //                count_record = count_record + 1;
                //                DataRow dr = dtEmpRecord.NewRow();
                //                dr["IDNO"] = objPA.EMPNO;
                //                dtEmpRecord.Rows.Add(dr);
                //                dtEmpRecord.AcceptChanges();
                //            }
                //        }

                //    }
                //}
                //if (count == 0)
                //{
                //    MessageBox("Please Select Atleast one employee");
                //    return;
                //}
                //else if (count > 0 && count_record == 0)
                //{
                //    MessageBox("Sorry! Record Already exists");
                //    return;
                //}
            #endregion
            }
            
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    string status = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH", "ISNULL(PAPNO,0)", "IDNO=" + ddlemp.SelectedValue + " AND DEPTNO=" + objPA.DEPTNO + " ");
                    if (status != "")
                    {
                        MessageBox("Sorry! Record Already exists");
                        pnlbtn.Visible = true;
                        return;
                    }
                    // CustomStatus cs=(CustomStatus) objPAPath.AddPAPath(objLeaves);
                    CustomStatus cs = (CustomStatus)objPAController.AddPAPath(objPA, dtEmpRecord);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        MessageBox("Record Saved Successfully");
                        pnlAdd.Visible = false; pnlEmpList.Visible = false;
                        pnlList.Visible = true;
                        pnlPAPaList.Visible = true;
                        ViewState["action"] = null;
                        Clear();
                    }
                }
                else
                {
                    if (ViewState["PAPNO"] != null)
                    {                        
                        objPA.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());
                        string stauts = objCommon.LookUp("ACC_PASSING_AUTHORITY_PATH", "ISNULL(PAPNO,0)", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + " AND DEPTNO=" + objPA.DEPTNO + "AND PAPNO!=" + objPA.PAPNO + " ");
                        // CustomStatus CS = (CustomStatus)objPAPath.UpdatePAPath(objLeaves);
                        if (stauts != "")
                        {
                            MessageBox("Sorry! Record Already exists");
                            pnlbtn.Visible = true;
                            return;
                        }
                        CustomStatus CS = (CustomStatus)objPAController.UpdatePAPath(objPA, dtEmpRecord);
                        if (CS.Equals(CustomStatus.RecordUpdated))
                        {
                            MessageBox("Record Updated Successfully");
                            pnlAdd.Visible = false; pnlEmpList.Visible = false;
                            pnlList.Visible = true;
                            pnlPAPaList.Visible = true;
                            ViewState["action"] = null;
                            Clear();
                        }
                    }
                }
            }
            BindListViewPAPath();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            pnlbtn.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            int PAPNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(PAPNO);

            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;
            pnlPAPaList.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.btnEdit_Click ->" + ex.Message + " " + ex.StackTrace);
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
            int PAPNO = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objPAController.DeletePAPath(PAPNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                MessageBox("Record Deleted Successfully");
                ViewState["action"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.btnDelete_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(Int32 PAPNO)
    {
        DataSet ds = null;
        try
        {
            ds = objPAController.GetSinglePAPath(PAPNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PAPNO"] = PAPNO;
                //ddlLeavename.SelectedValue = ds.Tables[0].Rows[0]["LNO"].ToString();

                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                FillDepartment();
                ddlDept.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();

                //===========
                FillPAuthority();
                FillEmployee();
                FillEmployeeList();


                //foreach (ListViewItem lvitem in lvEmployees.Items)
                //{
                //    CheckBox chkIdNo = lvitem.FindControl("chkIdNo") as CheckBox;

                //    if (Convert.ToInt32(chkIdNo.ToolTip) == Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()))
                //    {
                //        chkIdNo.Checked = true;
                //        break;
                //    }

                //}
                // ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["IDNO"].ToString();
                lblEmpName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                ViewState["IDNO"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                trEmp.Visible = true;
                //===============



                txtPAPath.Text = ds.Tables[0].Rows[0]["PAPATH"].ToString();
                ddlPA01.SelectedValue = ds.Tables[0].Rows[0]["PAN01"].ToString();
                this.EnableDisable(1);

                if (!(ds.Tables[0].Rows[0]["PAN02"].ToString().Trim().Equals("0")))
                {
                    ddlPA02.SelectedValue = ds.Tables[0].Rows[0]["PAN02"].ToString();
                    this.EnableDisable(2);
                    ddlPA02.Enabled = true;


                }
                if (!(ds.Tables[0].Rows[0]["PAN03"].ToString().Trim().Equals("0")))
                {
                    ddlPA03.SelectedValue = ds.Tables[0].Rows[0]["PAN03"].ToString();
                    this.EnableDisable(3);
                    ddlPA03.Enabled = true;


                }
                if (!(ds.Tables[0].Rows[0]["PAN04"].ToString().Trim().Equals("0")))
                {
                    ddlPA04.SelectedValue = ds.Tables[0].Rows[0]["PAN04"].ToString();
                    this.EnableDisable(4);
                    ddlPA04.Enabled = true;

                }
                if (!(ds.Tables[0].Rows[0]["PAN05"].ToString().Trim().Equals("0")))
                {
                    ddlPA05.SelectedValue = ds.Tables[0].Rows[0]["PAN05"].ToString();
                    this.EnableDisable(5);
                    ddlPA05.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["action"].ToString() != "edit")
        {
            FillPAuthority();
            FillEmployee();
            FillEmployeeList();
            trEmp.Visible = false;
            pnlEmpList.Visible = true;
            lvEmployees.Visible = true;

            txtPAPath.Text = string.Empty;
        }
        //  objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "FNAME+' '+MNAME+' '+LNAME AS NAME", "SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "", "");

    }

    protected void ddlPA01_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(1);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.ddlPA01_click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void ddlPA02_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(2);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.ddlPA02_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlPA03_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(3);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.ddlPA03_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void ddlPA04_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(4);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.ddlPA04_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void EnableDisable(int index)
    {

        switch (index)
        {
            case 1:
                if (ddlPA01.SelectedIndex == 0)
                {
                    ddlPA02.SelectedIndex = 0;
                    ddlPA02.Enabled = false;
                    ddlPA03.SelectedIndex = 0;
                    ddlPA03.Enabled = false;
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;
                    string swhere = "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA02, "ACC_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                }
                else
                {

                    ddlPA02.Enabled = true;
                    string swhere = "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA02, "ACC_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    ddlPA03.SelectedIndex = 0;
                    ddlPA03.Enabled = false;
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString();
                }

                break;

            case 2:
                if (ddlPA02.SelectedIndex == 0)
                {
                    ddlPA03.SelectedIndex = 0;
                    ddlPA03.Enabled = false;
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;
                    string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA03, "ACC_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString(); //added by sagar
                }
                else
                {
                    ddlPA03.Enabled = true;
                    string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA03, "ACC_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;

                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString();
                }
                break;
            case 3:


                if (ddlPA03.SelectedIndex == 0)
                {
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;

                    string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA04, "ACC_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString();
                }
                else
                {
                    ddlPA04.Enabled = true;
                    string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA04, "ACC_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString();
                }

                break;
            case 4:

                if (ddlPA04.SelectedIndex == 0)
                {
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;

                    string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA05, "ACC_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString();
                }
                else
                {
                    ddlPA05.Enabled = true;
                    string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA05, "ACC_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString() + "->" + ddlPA04.SelectedItem.ToString();
                }

                break;
            case 5:
                if (!(ddlPA04.SelectedIndex == 0))
                {
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString() + "->" + ddlPA04.SelectedItem.ToString() + "->" + ddlPA05.SelectedItem.ToString();
                }
                break;

        }

    }


    protected void ddlPA05_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(5);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.ddlPA05_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Leave", "ESTB_PAPAth.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.btnShowReport_Click->" + ex.Message + ' ' + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            //string url=Request.Url.ToString().Substring(0,(Request.Url.ToString().IndexOf("Establishment")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,LEAVES," + rptFileName;
            // url += "&param=@username=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + "@username=" + Session["username"].ToString();

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
}