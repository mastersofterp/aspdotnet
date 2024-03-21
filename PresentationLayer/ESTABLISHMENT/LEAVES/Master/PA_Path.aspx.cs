using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Master_PA_Path : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    LeavesController objPAPath = new LeavesController();

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
        divMsg.InnerHtml = string.Empty;
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
        if (Session["usertype"].ToString() != "1" && Session["usertype"].ToString() != "8")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillDepartment();
        FillPAuthority();
    }

    protected void BindListViewPAPath()
    {
        try
        {
            DataSet dsLeave;
            dsLeave = objCommon.FillDropDown("PAYROLL_LEAVE_REF", "isnull(IsLeaveWisePassingPath,0) as IsLeaveWisePassingPath", "", "", "");
            if (dsLeave.Tables[0].Rows.Count > 0)
            {
                Boolean IsLeaveWisePassingPath = Convert.ToBoolean(dsLeave.Tables[0].Rows[0]["IsLeaveWisePassingPath"].ToString());
                ViewState["LeaveWisePath"] = dsLeave.Tables[0].Rows[0]["IsLeaveWisePassingPath"].ToString();
                if (IsLeaveWisePassingPath == true)
                {
                    pnlLeaveWisePath.Visible = true;
                }
                else
                {
                    pnlLeaveWisePath.Visible = false;
                }


                DataSet ds = objPAPath.GetAllPAPath(Convert.ToInt32(ddlCollege.SelectedValue));
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
                if (IsLeaveWisePassingPath == true)
                {

                    Control ctrHeader = lvPAPath.FindControl("divLvName");
                    ctrHeader.Visible = true;

                    foreach (ListViewItem lvRow in lvPAPath.Items)
                    {
                        Control ckBox = (Control)lvRow.FindControl("tdLName");
                        ckBox.Visible = true;
                    }
                }
                //else
                //{
                //    Control ctrHeader = lvPAPath.FindControl("divLvName");
                //    ctrHeader.Visible = false;

                //    foreach (ListViewItem lvRow in lvPAPath.Items)
                //    {
                //        Control ckBox = (Control)lvRow.FindControl("tdLName");
                //        ckBox.Visible = false;
                //    }
                //}


            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.BindListViewPAPath ->" + ex.Message + " " + ex.StackTrace);
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
        chkleave.Items.Clear();
        ViewState["LeaveWisePath"] = null;
        ddlCollege.Enabled = true;
        ddlDept.Enabled = true;
        chkcopypath.Checked = false;

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
        ddlCollege.Enabled = true;
        ddlDept.Enabled = true;
        fillleave();
        DataSet ds;
        ds = objCommon.FillDropDown("PAYROLL_LEAVE_REF", "isnull(IsLeaveWisePassingPath,0) as IsLeaveWisePassingPath", "", "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            Boolean IsLeaveWisePassingPath = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsLeaveWisePassingPath"].ToString());
            ViewState["LeaveWisePath"] = ds.Tables[0].Rows[0]["IsLeaveWisePassingPath"].ToString();
            if (IsLeaveWisePassingPath == true)
            {
                pnlLeaveWisePath.Visible = true;
                divcopypath.Visible = false;
            }
            else
            {
                pnlLeaveWisePath.Visible = false;
                divcopypath.Visible = true;
            }
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
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.FillLeave ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillEmployee()
    {
        // DataSet dsEmp = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO = P.IDNO)", "E.IDNO", "isnull(TITLE,'')+' '+isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'') AS NAME", "E.COLLEGE_NO ="+Convert.ToInt32(ddlCollege.SelectedValue)+" and E.SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "  AND P.PSTATUS='Y'", "NAME");
        // DataSet dsEmp = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO = P.IDNO)", "E.IDNO", "isnull(TITLE,'')+' '+isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'') AS NAME", "E.SUBDEPTNO =" + Convert.ToInt32(ddlDept.SelectedValue)+"  AND P.PSTATUS='Y'", "NAME");
        DataSet dsEmp = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO = P.IDNO)", "E.IDNO", "isnull(TITLE,'')+' '+isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'') AS NAME", "E.SUBDEPTNO =" + Convert.ToInt32(ddlDept.SelectedValue) + "  AND P.PSTATUS='Y'", "NAME");
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
            //if (Convert.ToInt32(ddlCollege.SelectedValue) > 0)
            //{
            //objCommon.FillDropDownList(ddlDept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT D ON(E.SUBDEPTNO=D.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "D.SUBDEPT", "E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "D.SUBDEPT");
            objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "DISTINCT SUBDEPTNO", "SUBDEPT", "SUBDEPTNO > 0", "SUBDEPT");
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.Fill_Department ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillPAuthority()
    {
        try
        {
            //objCommon.FillDropDownList(ddlPA01, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "PANAME");
            objCommon.FillDropDownList(ddlPA01, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", "COLLEGE_NO > 0" + "", "PANAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.FillPAuthority ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear();
        clearnew();
        ViewState["action"] = "add";
    }

    private void clearnew()
    {
        ddlCollege.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
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
        // ddlEmployee.SelectedIndex = 0;
        lblEmpName.Text = string.Empty;
        ViewState["IDNO"] = null;
        ViewState["Leave"] = null;
        pnlEmpList.Visible = false;
        trEmp.Visible = false;
        //  chkleave.Items.Clear();   
        foreach (ListItem item in chkleave.Items)
        {
            //check anything out here
            if (item.Selected)
                item.Selected = false;
        }
        ddlCollege.Enabled = true;
        ddlDept.Enabled = true;
        chkcopypath.Checked = false;

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Clear();
        clearnew();
        pnlAdd.Visible = false; pnlEmpList.Visible = false;
        pnlList.Visible = true;
        pnlbtn.Visible = false;
        pnlPAPaList.Visible = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToBoolean(ViewState["LeaveWisePath"]) == false)
            {
                pnlbtn.Visible = false;
                Leaves objLeaves = new Leaves();
                DataTable dtEmpRecord = new DataTable();
                dtEmpRecord.Columns.Add("IDNO");
                objLeaves.LNO = 0;
                objLeaves.DEPTNO = Convert.ToInt32(ddlDept.SelectedValue);
                //objLeaves.PAN01 = Convert.ToInt32(ddlPA01.SelectedValue);
                //objLeaves.PAN02 = Convert.ToInt32(ddlPA02.SelectedValue);
                //objLeaves.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
                //objLeaves.PAN04 = Convert.ToInt32(ddlPA04.SelectedValue);
                //objLeaves.PAN05 = Convert.ToInt32(ddlPA05.SelectedValue);
                if (ddlPA01.SelectedValue != string.Empty)
                {
                    objLeaves.PAN01 = Convert.ToInt32(ddlPA01.SelectedValue);
                }
                else
                {
                    objLeaves.PAN01 = 0;
                }

                //  objLeaves.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
                if (ddlPA02.SelectedValue != string.Empty)
                {
                    objLeaves.PAN02 = Convert.ToInt32(ddlPA02.SelectedValue);
                }
                else
                {
                    objLeaves.PAN02 = 0;
                }

                if (ddlPA03.SelectedValue != string.Empty)
                {
                    objLeaves.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
                }
                else
                {
                    objLeaves.PAN03 = 0;
                }
                if (ddlPA04.SelectedValue != string.Empty)
                {
                    objLeaves.PAN04 = Convert.ToInt32(ddlPA04.SelectedValue);
                }
                else
                {
                    objLeaves.PAN04 = 0;
                }
                if (ddlPA05.SelectedValue != string.Empty)
                {
                    objLeaves.PAN05 = Convert.ToInt32(ddlPA05.SelectedValue);
                }
                else
                {
                    objLeaves.PAN05 = 0;
                }
                objLeaves.PAPATH = Convert.ToString(txtPAPath.Text);
                objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                objLeaves.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);

                if (chkcopypath.Checked == true)
                {
                    objLeaves.IsCopypath = true;
                }
                else
                {
                    objLeaves.IsCopypath = false;
                }
                int count = 0; int count_record = 0;
                if (trEmp.Visible == true)
                {
                    DataRow dr = dtEmpRecord.NewRow();
                    int idno = Convert.ToInt32(ViewState["IDNO"]);
                    dr["IDNO"] = idno;
                    dtEmpRecord.Rows.Add(dr);
                    dtEmpRecord.AcceptChanges();
                    objLeaves.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());

                }
                else
                {
                    foreach (ListViewItem lvitem in lvEmployees.Items)
                    {
                        CheckBox chkIdNo = lvitem.FindControl("chkIdNo") as CheckBox;
                        if (chkIdNo.Checked == true)
                        {
                            count = count + 1;
                            objLeaves.EMPNO = Convert.ToInt32(chkIdNo.ToolTip);
                            if (ViewState["action"].ToString().Equals("add"))
                            {
                                DataSet ds = objCommon.FillDropDown("payroll_leave_passing_authority_path", "PAPNO", "IDNO", "IDNO=" + objLeaves.EMPNO + " ", "");
                                if (ds.Tables[0].Rows.Count <= 0)
                                {
                                    count_record = count_record + 1;
                                    DataRow dr = dtEmpRecord.NewRow();
                                    dr["IDNO"] = objLeaves.EMPNO;
                                    dtEmpRecord.Rows.Add(dr);
                                    dtEmpRecord.AcceptChanges();
                                }

                            }
                            else
                            {

                                objLeaves.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());
                                DataSet ds = objCommon.FillDropDown("payroll_leave_passing_authority_path", "PAPNO", "IDNO", "IDNO=" + objLeaves.EMPNO + " AND PAPNO!=" + objLeaves.PAPNO + " ", "");
                                if (ds.Tables[0].Rows.Count <= 0)
                                {
                                    count_record = count_record + 1;
                                    DataRow dr = dtEmpRecord.NewRow();
                                    dr["IDNO"] = objLeaves.EMPNO;
                                    dtEmpRecord.Rows.Add(dr);
                                    dtEmpRecord.AcceptChanges();
                                }
                            }

                        }
                    }
                    if (count == 0)
                    {
                        MessageBox("Please Select Atleast one employee");
                        pnlbtn.Visible = true;
                        return;
                    }
                    else if (count > 0 && count_record == 0)
                    {
                        MessageBox("Sorry ! Record Already exists");
                        pnlbtn.Visible = true;
                        return;
                    }
                }
                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        // CustomStatus cs=(CustomStatus) objPAPath.AddPAPath(objLeaves);
                        CustomStatus cs = (CustomStatus)objPAPath.AddPAPath(objLeaves, dtEmpRecord);
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
                            objLeaves.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());
                            // CustomStatus CS = (CustomStatus)objPAPath.UpdatePAPath(objLeaves);
                            CustomStatus CS = (CustomStatus)objPAPath.UpdatePAPath(objLeaves, dtEmpRecord);
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
            }
            else
            {
                pnlbtn.Visible = false;
                Leaves objLeaves = new Leaves();
                DataTable dtEmpRecordLeaveNew = new DataTable();
                dtEmpRecordLeaveNew.Columns.Add("IDNO");
                dtEmpRecordLeaveNew.Columns.Add("Leave");
                objLeaves.LNO = 0;
                objLeaves.DEPTNO = Convert.ToInt32(ddlDept.SelectedValue);
                //objLeaves.PAN01 = Convert.ToInt32(ddlPA01.SelectedValue);
                //objLeaves.PAN02 = Convert.ToInt32(ddlPA02.SelectedValue);
                //objLeaves.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
                //objLeaves.PAN04 = Convert.ToInt32(ddlPA04.SelectedValue);
                //objLeaves.PAN05 = Convert.ToInt32(ddlPA05.SelectedValue);
                if (ddlPA01.SelectedValue != string.Empty)
                {
                    objLeaves.PAN01 = Convert.ToInt32(ddlPA01.SelectedValue);
                }
                else
                {
                    objLeaves.PAN01 = 0;
                }

                //  objLeaves.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
                if (ddlPA02.SelectedValue != string.Empty)
                {
                    objLeaves.PAN02 = Convert.ToInt32(ddlPA02.SelectedValue);
                }
                else
                {
                    objLeaves.PAN02 = 0;
                }

                if (ddlPA03.SelectedValue != string.Empty)
                {
                    objLeaves.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
                }
                else
                {
                    objLeaves.PAN03 = 0;
                }
                if (ddlPA04.SelectedValue != string.Empty)
                {
                    objLeaves.PAN04 = Convert.ToInt32(ddlPA04.SelectedValue);
                }
                else
                {
                    objLeaves.PAN04 = 0;
                }
                if (ddlPA05.SelectedValue != string.Empty)
                {
                    objLeaves.PAN05 = Convert.ToInt32(ddlPA05.SelectedValue);
                }
                else
                {
                    objLeaves.PAN05 = 0;
                }
                objLeaves.PAPATH = Convert.ToString(txtPAPath.Text);
                objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
                objLeaves.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
                int count = 0; int count_record = 0;

                string Leavevalue = "";
                // string Leaveno ="";
                for (int i = 0; i < chkleave.Items.Count; i++)
                {
                    if (chkleave.Items[i].Selected)
                    {
                        Leavevalue += chkleave.Items[i].Value + ",";
                        //Leavevalue = Leavevalue + "," + chkleave.Items[i].Value;                   
                    }
                }
                Leavevalue = Leavevalue.TrimEnd(',');

                if (Leavevalue == string.Empty)
                {
                    MessageBox("Please Select Leave");
                    pnlbtn.Visible = true;
                    return;
                }

                if (trEmp.Visible == true)
                {
                    //DataRow dr = dtEmpRecordLeaveNew.NewRow();
                    int idno = Convert.ToInt32(ViewState["IDNO"]);
                    //dr["IDNO"] = idno;
                    int Leave = Convert.ToInt32(ViewState["Leave"]);
                    //dr["Leave"] = Leave;
                    for (int i = 0; i < chkleave.Items.Count; i++)
                    {
                        if (chkleave.Items[i].Selected)
                        {
                            count_record = count_record + 1;
                            DataRow dr = dtEmpRecordLeaveNew.NewRow();
                            dr["IDNO"] = idno;
                            dr["Leave"] = chkleave.Items[i].Value;
                            dtEmpRecordLeaveNew.Rows.Add(dr);
                            dtEmpRecordLeaveNew.AcceptChanges();

                        }
                    }
                    //dtEmpRecordLeaveNew.Rows.Add(dr);
                    //dtEmpRecordLeaveNew.AcceptChanges();
                    objLeaves.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());

                   
                }
                else
                {
                    foreach (ListViewItem lvitem in lvEmployees.Items)
                    {
                        CheckBox chkIdNo = lvitem.FindControl("chkIdNo") as CheckBox;
                        if (chkIdNo.Checked == true)
                        {
                            count = count + 1;
                            objLeaves.EMPNO = Convert.ToInt32(chkIdNo.ToolTip);
                            if (ViewState["action"].ToString().Equals("add"))
                            {                                                                                                                               
                                DataSet ds = objCommon.FillDropDown("payroll_leave_passing_authority_path", "PAPNO", "IDNO", "IDNO=" + objLeaves.EMPNO + " AND Leavevalue IN (" + Leavevalue + ")", "");
                                //if (ds.Tables[0].Rows.Count <= 0)
                                //{
                                //count_record = count_record + 1;
                                //DataRow dr = dtEmpRecord.NewRow();
                                //dr["IDNO"] = objLeaves.EMPNO;
                                //dtEmpRecord.Rows.Add(dr);
                                //dtEmpRecord.AcceptChanges();
                                if (ds.Tables[0].Rows.Count <= 0)
                                {
                                    for (int i = 0; i < chkleave.Items.Count; i++)
                                    {
                                        if (chkleave.Items[i].Selected)
                                        {
                                            count_record = count_record + 1;
                                            DataRow dr = dtEmpRecordLeaveNew.NewRow();
                                            dr["IDNO"] = objLeaves.EMPNO;
                                            dr["Leave"] = chkleave.Items[i].Value;
                                            dtEmpRecordLeaveNew.Rows.Add(dr);
                                            dtEmpRecordLeaveNew.AcceptChanges();

                                        }
                                    }
                                }
                                //}

                            }
                            else
                            {

                                objLeaves.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());
                                DataSet ds = objCommon.FillDropDown("payroll_leave_passing_authority_path", "PAPNO", "IDNO", "IDNO=" + objLeaves.EMPNO + " AND PAPNO!=" + objLeaves.PAPNO + " ", "");
                                if (ds.Tables[0].Rows.Count <= 0)
                                {
                                    //count_record = count_record + 1;
                                    //DataRow dr = dtEmpRecord.NewRow();
                                    //dr["IDNO"] = objLeaves.EMPNO;
                                    //dtEmpRecord.Rows.Add(dr);
                                    //dtEmpRecord.AcceptChanges();
                                    for (int i = 0; i < chkleave.Items.Count; i++)
                                    {
                                        if (chkleave.Items[i].Selected)
                                        {
                                            count_record = count_record + 1;
                                            DataRow dr = dtEmpRecordLeaveNew.NewRow();
                                            dr["IDNO"] = objLeaves.EMPNO;
                                            dr["Leave"] = chkleave.Items[i].Value;
                                            dtEmpRecordLeaveNew.Rows.Add(dr);
                                            dtEmpRecordLeaveNew.AcceptChanges();

                                        }
                                    }
                                }
                            }

                        }
                    }
                    if (count == 0)
                    {
                        MessageBox("Please Select Atleast one employee");
                        pnlbtn.Visible = true;
                        return;
                    }
                    else if (count > 0 && count_record == 0)
                    {
                        MessageBox("Sorry! Record Already exists");
                        pnlbtn.Visible = true;
                        return;
                    }
                }
                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        // CustomStatus cs=(CustomStatus) objPAPath.AddPAPath(objLeaves);
                        CustomStatus cs = (CustomStatus)objPAPath.AddLeaveWisePAPath(objLeaves, dtEmpRecordLeaveNew);
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
                            objLeaves.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());
                            // CustomStatus CS = (CustomStatus)objPAPath.UpdatePAPath(objLeaves);
                            CustomStatus CS = (CustomStatus)objPAPath.UpdateLeaveWisePAPath(objLeaves, dtEmpRecordLeaveNew);
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
            }
            BindListViewPAPath();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
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
            //fillleave();
            DataSet ds;
            ds = objCommon.FillDropDown("PAYROLL_LEAVE_REF", "isnull(IsLeaveWisePassingPath,0) as IsLeaveWisePassingPath", "", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Boolean IsLeaveWisePassingPath = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsLeaveWisePassingPath"].ToString());
                ViewState["LeaveWisePath"] = ds.Tables[0].Rows[0]["IsLeaveWisePassingPath"].ToString();
                if (IsLeaveWisePassingPath == true)
                {
                    pnlLeaveWisePath.Visible = true;
                    chkleave.Items.Clear();
                    fillleave();
                }
                else
                {
                    pnlLeaveWisePath.Visible = false;
                }
            }
            ShowDetails(PAPNO);
            divcopypath.Visible = false;

            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;
            pnlPAPaList.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.btnEdit_Click ->" + ex.Message + " " + ex.StackTrace);
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
            CustomStatus cs = (CustomStatus)objPAPath.DeletePAPath(PAPNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                MessageBox("Record Deleted Successfully");
                ViewState["action"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.btnDelete_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(Int32 PAPNO)
    {
        DataSet ds = null;
        try
        {
            ds = objPAPath.GetSinglePAPath(PAPNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PAPNO"] = PAPNO;
                //ddlLeavename.SelectedValue = ds.Tables[0].Rows[0]["LNO"].ToString();

                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                //ddlCollege.Enabled = false;
                FillDepartment();
                ddlDept.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();
                //ddlDept.SelectedValue = ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString(); // Modified by Sonal Banode on 20-02-2023
                //ddlDept.Enabled = false;

                //===========
                FillPAuthority();
                FillEmployee();



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
                ViewState["Leave"] = ds.Tables[0].Rows[0]["Leavevalue"].ToString();
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

                if (ds.Tables[0].Rows.Count > 0 && ds != null && ds.Tables.Count > 0)
                {
                    // domainCount = 1;                   
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string[] Leavevalue = ds.Tables[0].Rows[i]["Leavevalue"].ToString().Split(',');

                        // string val = ds.Tables[0].Rows[i]["AL_ASNO"].ToString();
                        for (int j = 0; j < Leavevalue.Length; j++)
                        {
                            foreach (ListItem item in chkleave.Items)
                            {
                                if (item.Value == Leavevalue[j])
                                {
                                    item.Selected = true;
                                    item.Enabled = true;
                                }
                            }
                        }
                    }

                }
                chkleave.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillPAuthority();
        FillEmployee();
        //  objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "FNAME+' '+MNAME+' '+LNAME AS NAME", "SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "", "");
        pnlEmpList.Visible = true;
        lvEmployees.Visible = true;
        trEmp.Visible = false;
        txtPAPath.Text = string.Empty;
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
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddlPA01_click ->" + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddlPA02_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddlPA03_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddlPA04_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
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
                    //string swhere = "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    string swhere = "PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA02, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                    //string swhere = "COLLEGE_NO>0"+ " and PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    //objCommon.FillDropDownList(ddlPA02, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                }
                else
                {

                    ddlPA02.Enabled = true;
                    //string swhere = "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    string swhere = "PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA02, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                    //string swhere = "COLLEGE_NO>0"+ " and PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    //objCommon.FillDropDownList(ddlPA02, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");



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
                    //string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    string swhere = "PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA03, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                    //string swhere = " COLLEGE_NO>0"+ " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    //objCommon.FillDropDownList(ddlPA03, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");


                    txtPAPath.Text = ddlPA01.SelectedItem.ToString(); //added by sagar
                }
                else
                {
                    ddlPA03.Enabled = true;
                    //string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    string swhere = "PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA03, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                    //string swhere = " COLLEGE_NO>0"+ " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    //objCommon.FillDropDownList(ddlPA03, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

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

                    //string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    string swhere = "PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA04, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                    //string swhere = " COLLEGE_NO>0"+ " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    //objCommon.FillDropDownList(ddlPA04, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString();
                }
                else
                {
                    ddlPA04.Enabled = true;
                    //string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    string swhere = "PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA04, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                    //string swhere = " COLLEGE_NO>0"+ " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    //objCommon.FillDropDownList(ddlPA04, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
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

                    //string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    string swhere = "PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA05, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                    //string swhere = " COLLEGE_NO>0"+ " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    //objCommon.FillDropDownList(ddlPA05, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");


                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString();
                }
                else
                {
                    ddlPA05.Enabled = true;
                    //string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    string swhere = "PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA05, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                    //string swhere = " COLLEGE_NO>0"+ " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    //objCommon.FillDropDownList(ddlPA05, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

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
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddlPA05_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToBoolean(ViewState["LeaveWisePath"]) == false)
            {
                ShowReport("Leave", "ESTB_PAPAth.rpt");
            }
            else
            {
                ShowReport("Leave", "ESTB_LeaveWisePAPAth.rpt");
            }
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
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + "@username=" + Session["username"].ToString();
            string collegeno = Session["college_nos"].ToString();
            string[] values = collegeno.Split(',');
            if (values.Length > 1)
            {
                url += "&param=@P_COLLEGE_CODE=0," + "@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + "@username=" + Session["username"].ToString();
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["college_nos"].ToString() + "," + "@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + "@username=" + Session["username"].ToString();
            }
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
    private void fillleave()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("Payroll_leave_name", "LVNO", "Leave_Name", "LVNO>0", "LVNO");            
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkleave.DataTextField = "Leave_Name";
                    chkleave.DataValueField = "LVNO";
                    chkleave.DataSource = ds.Tables[0];
                    chkleave.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.Fill_Department ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
}
