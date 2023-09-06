using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class PAYROLL_TRANSACTIONS_Emp_Bulk_Paylevel_Update : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AttendanceController objAttendance = new AttendanceController();
    ChangeInMasterFileController ObjChangeMstFile = new ChangeInMasterFileController();

    //ConnectionStrings
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["mast erpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        // CheckRef();

        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
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
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                pnlSelect.Visible = true;
                pnlMonthlyChanges.Visible = false;

                //FillPayHead(Convert.ToInt32(Session["userno"].ToString()));
                FillStaff();


            }
        }
        else
        {
            divMsg.InnerHtml = string.Empty;
        }

    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Emp_Bulk_Update.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Emp_Bulk_Update.aspx");
        }
    }

    protected void lvMonthlyChanges_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        // TextBox      txtEiditfiled;
        //TextBox      txtEiditfiledDT;
        //DropDownList ddlPayRule;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            TextBox txtEiditfiled = (TextBox)e.Item.FindControl("txtEditField");
            TextBox txtEditFieldDT = (TextBox)e.Item.FindControl("txtEditFieldDT");
            DropDownList ddleditfield = (DropDownList)e.Item.FindControl("ddleditfield");
            DropDownList ddlScale = (DropDownList)e.Item.FindControl("ddlScale");
            DropDownList ddlDesig = (DropDownList)e.Item.FindControl("ddlDesignation");

            HiddenField hdneditfield = (HiddenField)e.Item.FindControl("hdneditfield");
            HiddenField hdnidno = (HiddenField)e.Item.FindControl("hdnidno");
            HiddenField hdnpayrule = (HiddenField)e.Item.FindControl("hdnpayrule");
            HiddenField hdnDesigNo = (HiddenField)e.Item.FindControl("hdnDesigNo");

            CheckBox chkShiftManagment = (CheckBox)e.Item.FindControl("chkShiftManagment");
            DropDownList ddlStaffType = (DropDownList)e.Item.FindControl("ddlStaffType");
            HiddenField hdnStaffNo = (HiddenField)e.Item.FindControl("hdnStaffType");



            DropDownList ddlPaylevel = (DropDownList)e.Item.FindControl("ddlpayLevel");
            DropDownList ddlCellNo = (DropDownList)e.Item.FindControl("ddlCellNo");

            HiddenField hdnPaylevel = (HiddenField)e.Item.FindControl("hdnPaylevel");
            HiddenField hdnCellNo = (HiddenField)e.Item.FindControl("hdnCellNo");
            //Label lblpaylevel = (Label)e.Item.FindControl("lblpaylevel");
            //Label lblCellNo = (Label)e.Item.FindControl("lblCellNo");
            //lblCellNo.Text = "Cell No.";
            //lblpaylevel.Text = "Pay level";

            if (ddlPayhead.SelectedItem.Text.Equals("PAYRULE"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                ddlScale.Visible = false;
                ddlScale.Visible = false;
                //lblCellNo.Visible = false;
               // lblpaylevel.Visible = false;
                ddlPaylevel.Visible = false;
                ddlCellNo.Visible = false;
                objCommon.FillDropDownList(ddleditfield, "payroll_rule", "RULENO", "PAYRULE", "RULENO>0", "RULENO");
                ddleditfield.SelectedValue = hdneditfield.Value;
                chkShiftManagment.Visible = false;
                ddlStaffType.Visible = false;


            }

            else if (ddlPayhead.SelectedItem.Text.Equals("IS_SHIFT_MANAGMENT"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                ddlScale.Visible = false;
                ddlScale.Visible = false;
                chkShiftManagment.Visible = true;
                ddleditfield.Visible = false;
                ddlStaffType.Visible = false;
                //lblCellNo.Visible = false;
               // lblpaylevel.Visible = false;
                ddlPaylevel.Visible = false;
                ddlCellNo.Visible = false;
            }

            else if (ddlPayhead.SelectedItem.Text.Equals("STNO"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                ddlScale.Visible = false;
                ddlScale.Visible = false;
                chkShiftManagment.Visible = false;
                ddleditfield.Visible = false;
                ddlStaffType.Visible = true;
             //   lblCellNo.Visible = false;
              //  lblpaylevel.Visible = false;
                ddlPaylevel.Visible = false;
                ddlCellNo.Visible = false;
                objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0", "STNO");
                ddlStaffType.SelectedValue = hdnStaffNo.Value;
            }
            else if (ddlPayhead.SelectedItem.Text.Equals("PAYLEVELID"))
            {

                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                ddleditfield.Visible = false;
                ddlScale.Visible = false;
                chkShiftManagment.Visible = false;
                ddlStaffType.Visible = false;
            //    lblCellNo.Visible = true;
             //   lblpaylevel.Visible = true;

                objCommon.FillDropDownList(ddlPaylevel, "Payroll_Paylevel", "Paylevel_No PAYLEVELNO", "Paylevel_Name", "Staff_No=" + Convert.ToInt32(ddlStaff.SelectedValue) + "", "Paylevel_No");
                ddlPaylevel.SelectedValue = hdnPaylevel.Value;
                if (ddlPaylevel.Items.Count > 1)
                {
                    objCommon.FillDropDownList(ddlCellNo, "CellNumInfo", "CellNo Id", "CellNo name", "CellNo>0", "CellNo");
                    ddlCellNo.SelectedValue = hdnCellNo.Value;
                }
            }


            else if (ddlPayhead.SelectedItem.Text.Equals("DOI"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = true;
                ddleditfield.Visible = false;
                ddlScale.Visible = false;
                chkShiftManagment.Visible = false;
                ddlStaffType.Visible = false;
              //  lblCellNo.Visible = false;
              //  lblpaylevel.Visible = false;
                ddlPaylevel.Visible = false;
                ddlCellNo.Visible = false;
            }
            else if (ddlPayhead.SelectedItem.Text.Equals("SCALENO"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                //objCommon.FillDropDownList(ddleditfield, "payroll_rule", "RULENO", "PAYRULE", "RULENO>0", "RULENO");
                ddleditfield.Visible = false;
             //   lblCellNo.Visible = false;
            //    lblpaylevel.Visible = false;
                ddlPaylevel.Visible = false;
                ddlCellNo.Visible = false;
                //ddleditfield.Enabled = true;
                //objCommon.FillDropDownList(ddlScale, "PAYROLL_SCALE", "SCALENO", "SCALE", "SCALENO>0", "SCALENO");
                objCommon.FillDropDownList(ddlScale, "PAYROLL_SCALE S INNER JOIN PAYROLL_RULE R ON(S.RULENO = R.RULENO)", "S.SCALENO", "S.SCALE+'--'+R.PAYRULE", "S.SCALENO>0", "S.SCALENO");
                ddlScale.SelectedValue = hdnidno.Value;
                chkShiftManagment.Visible = false;
                ddlStaffType.Visible = false;

            }
            else if (ddlPayhead.SelectedItem.Text.Equals("SUBDESIGNO"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                ddlDesig.Visible = true;
               // lblCellNo.Visible = false;
              //  lblpaylevel.Visible = false;
                ddlPaylevel.Visible = false;
                ddlCellNo.Visible = false;
                objCommon.FillDropDownList(ddlDesig, "PAYROLL_SUBDESIG", "SUBDESIGNO", "SUBDESIG", "SUBDESIGNO>0", "SUBDESIGNO");
                ddlDesig.SelectedValue = hdnDesigNo.Value;
                ddleditfield.Visible = false;
                ddlScale.Visible = false;
                chkShiftManagment.Visible = false;
                ddlStaffType.Visible = false;
            }
            else
            {
                txtEiditfiled.Visible = true;
                txtEditFieldDT.Visible = false;
                ddleditfield.Visible = false;
                ddlScale.Visible = false;
                chkShiftManagment.Visible = false;
                ddlStaffType.Visible = false;
               // lblCellNo.Visible = false;
               // lblpaylevel.Visible = false;
                ddlPaylevel.Visible = false;
                ddlCellNo.Visible = false;

            }

        }
    }


    private void BindListViewList(string payHead, int staffNo, int collegeNo)
    {
        try
        {
            if (!(Convert.ToInt32(ddlStaff.SelectedIndex) == 0))
            {

                pnlMonthlyChanges.Visible = true;
                DataSet ds = GetEmployeesForEditFields_7CPC(Convert.ToInt32(ddlStaff.SelectedValue), ddlPayhead.SelectedItem.Text, collegeNo);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                }
                else
                {
                    btnCancel.Visible = true;
                    btnSave.Visible = true;
                }

                lvMonthlyChanges.DataSource = ds;
                lvMonthlyChanges.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;

            foreach (ListViewDataItem lvitem in lvMonthlyChanges.Items)
            {

                if (ddlPayhead.SelectedItem.Text.Equals("DOI"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    // DropDownList ddl = lvitem. FindControl("ddleditfield") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(txt.Text), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }

                }
                else if (ddlPayhead.SelectedItem.Text.Equals("PAYRULE"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddleditfield") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }

                }



                //SACHIN 17 JULY FOR SHIFT MANAGMENT

                else if (ddlPayhead.SelectedItem.Text.Equals("IS_SHIFT_MANAGMENT"))
                {
                    string Check = string.Empty;
                    CheckBox chkShiftManagment = lvitem.FindControl("chkShiftManagment") as CheckBox;

                    if (chkShiftManagment.Checked == true)
                        Check = "1";
                    else
                        Check = "0";

                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    //DropDownList ddl = lvitem.FindControl("ddleditfield") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Check, Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }

                }

                 //END OF SACHIN 17 JULY FOR SHIFT MANAGMENT



                 //SACHIN 17 Aug 2017 FOR Staff Type

                else if (ddlPayhead.SelectedItem.Text.Equals("STNO"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddlStaffType") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }

                }

                 //END OF SACHIN 17 Aug 2017 FOR Staff Type



                else if (ddlPayhead.SelectedItem.Text.Equals("SCALENO"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddlScale") as DropDownList;

                    DropDownList ddleditfield = lvitem.FindControl("ddleditfield") as DropDownList;

                    //CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayPaymasFieldsScaleRule(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip), Convert.ToString(ddleditfield.SelectedValue));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }

                }
                else if (ddlPayhead.SelectedItem.Text.Equals("PAYLEVELID"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;
                    DropDownList ddlPaylevel = lvitem.FindControl("ddlpayLevel") as DropDownList;
                    DropDownList ddlCellNo = lvitem.FindControl("ddlCellNo") as DropDownList;

                    if (ddlPaylevel.SelectedIndex > 0)
                    {
                        CustomStatus cs = (CustomStatus)UpdatePaylevel(ddlPayhead.SelectedItem.Text, Convert.ToString(ddlPaylevel.SelectedValue), Convert.ToInt32(txt.ToolTip), Convert.ToInt32(ddlPaylevel.SelectedValue), Convert.ToInt32(ddlCellNo.SelectedValue));
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            count = 1;
                        }
                    }

                }
                else if (ddlPayhead.SelectedItem.Text.Equals("SUBDESIGNO"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddlDesignation") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }

                }
                else if (ddlPayhead.SelectedItem.Text.Equals("RFIDNO"))
                {
                    TextBox txt = lvitem.FindControl("txtEditField") as TextBox;

                    int rfidnochk = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS", "COUNT(1)", "RFIDNO=" + Convert.ToInt32(txt.Text) + " AND IDNO<>" + Convert.ToInt32(txt.ToolTip)));

                    if (rfidnochk == 0)
                    {
                        //Add the item to the ListView Control
                        CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(txt.Text), Convert.ToInt32(txt.ToolTip));
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            count = 1;
                        }
                    }
                    else
                    {
                        //Warn user of duplicate entry...
                        objCommon.DisplayMessage("RFIDNO = " + Convert.ToString(txt.Text) + " already exists. Duplicate Entry For IDNO=" + Convert.ToInt32(txt.ToolTip), this.Page);
                        return;
                    }
                }

                //else if(ddlPayhead.SelectedItem.Text.Equals("NPA"))
                //{
                //    TextBox txt = lvitem.FindControl("txtEditField") as TextBox;
                //    if(txt.Text == "Y" || txt.Text == "N")
                //    {
                //        CustomStatus cs = (CustomStatus)ObjChangeMstFile. UpdatePayEmpmasFields(ddlPayhead. SelectedItem. Text, Convert. ToString(txt.Text ), Convert. ToInt32(txt. ToolTip));
                //        if (cs. Equals(CustomStatus. RecordUpdated))
                //        {
                //          count = 1;
                //        }
                //    }
                //    else
                //    {
                //        objCommon.DisplayMessage("Please Enter 'Y' Or 'N' NPA status for idno"+ Convert.ToInt32(txt.ToolTip) +"", this);
                //        break;
                //    }
                //}
                else
                {
                    TextBox txt = lvitem.FindControl("txtEditField") as TextBox;


                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(txt.Text), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }
                }
            }
            if (count == 1)
            {
                //lblerror.Text = null;
                //lblmsg.Text = "Record Updated Successfully";
                objCommon.DisplayMessage("Record Updated Successfully", this);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        foreach (ListViewDataItem lvitem in lvMonthlyChanges.Items)
        {
            TextBox txt = lvitem.FindControl("txtEditField") as TextBox;
            txt.Text = string.Empty;
        }
        ddlStaff.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
        pnlMonthlyChanges.Visible = false;
        ddlPayhead.SelectedIndex = 0;

    }

    protected void FillPayHead(int uaNo)
    {
        try
        {
            PayHeadPrivilegesController objPayHead = new PayHeadPrivilegesController();
            DataSet ds = null;
            ds = objPayHead.EditPayHeadUser(uaNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPayhead.DataSource = ds;
                ddlPayhead.DataValueField = ds.Tables[0].Columns[1].ToString();
                ddlPayhead.DataTextField = ds.Tables[0].Columns[2].ToString();
                ddlPayhead.DataBind();
                ddlPayhead.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillStaff()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
            //objCommon.FillDropDownList(ddlPayhead, "INFORMATION_SCHEMA.COLUMNS A", "ROW_NUMBER()over(order by ORDINAL_POSITION)as ORDINAL_POSITION", "COLUMN_NAME", "TABLE_NAME in ('PAYROLL_EMPMAS','PAYROLL_PAYMAS') AND DATA_TYPE in  ('NVARCHAR','INT','NCHAR','BIT')  AND COLUMN_NAME NOT IN('COLLEGE_NO','STAFFNO','TITLE','COLLEGE_CODE','RESADD1','TOWNADD1','IDMARK1','IDMARK2','HEIGHT','EMAILID','ACTIVE','DOB','DOJ','RDT','STDATE','IDNO' ,'SUBDEPTNO','BANKNO','DESIG_COLLNO','DESIG_UNIVNO','SUPPLINO','SEQ_NO','DESIGNATURENO','ODOI','DOJ','PAY','OBASIC' ,'SBDATE','DPCAL','COLLEGE_CODE','CASTENO','CATEGORYNO','RELIGIONNO','NATIONALITYNO''SUBDEPTNO','DESIG_COLLNO','DESIG_UNIVNO','NATIONALITYNO','BANKCITYNO' ,'CLNO','QTRNO','ACATNO','NUDESIGNO','SHIFTNO','Expire_dateof_Extention','RELIEVING_DATE','DOI','APPOINTNO','SERVICEBOOK_EMPLOYEE_LOCK','EMPLOYEE_LOCK','EPF_EXTRA_STATUS','QRENT_YN','QUARTER','CELLNUMBER','CELLNO','PAYLEVELNO') and ORDINAL_POSITION not in(79,80,81)", "A.COLUMN_NAME");
            ddlPayhead.Items.Insert(0, "PAYLEVELID");
                
           // ddlPayhead.SelectedValue = "48";
            ddlPayhead.Enabled = false;
            // 'SUBDESIGNO','PAYRULE','SCALENO',
            //objCommon.FillDropDownList(ddlPayhead, "INFORMATION_SCHEMA.COLUMNS A", "ROW_NUMBER()over(order by ORDINAL_POSITION)as ORDINAL_POSITION", "COLUMN_NAME", "TABLE_NAME in ('PAYROLL_EMPMAS','PAYROLL_PAYMAS') AND DATA_TYPE in  ('NVARCHAR','INT','NCHAR','BIT')  AND COLUMN_NAME NOT IN('COLLEGE_NO','STAFFNO','TITLE','COLLEGE_CODE','RESADD1','TOWNADD1','IDMARK1','IDMARK2','HEIGHT','EMAILID','ACTIVE','DOB','DOJ','RDT','STDATE','IDNO' ,'SUBDEPTNO','BANKNO','DESIG_COLLNO','DESIG_UNIVNO','SUPPLINO','SUBDESIGNO','SEQ_NO','DESIGNATURENO','ODOI','DOJ','PAY','OBASIC' ,'SBDATE','DPCAL','COLLEGE_CODE','CASTENO','CATEGORYNO','RELIGIONNO','NATIONALITYNO''SUBDEPTNO','DESIG_COLLNO','DESIG_UNIVNO','SUBDESIGNO','STNO','NATIONALITYNO','BANKCITYNO' ,'CLNO','QTRNO','ACATNO','NUDESIGNO','SHIFTNO','Expire_dateof_Extention','RELIEVING_DATE','DOI','PAYRULE','SCALENO','APPOINTNO','SERVICEBOOK_EMPLOYEE_LOCK','EMPLOYEE_LOCK','EPF_EXTRA_STATUS','QRENT_YN','QUARTER') and ORDINAL_POSITION not in(79,80,81)", "A.COLUMN_NAME");

            // objCommon.FillDropDownList(ddlPayhead, "INFORMATION_SCHEMA.COLUMNS A", "ROW_NUMBER()over(order by ORDINAL_POSITION)as ORDINAL_POSITION", "COLUMN_NAME", "TABLE_NAME in ('PAYROLL_EMPMAS','PAYROLL_PAYMAS') AND DATA_TYPE in  ('NVARCHAR','INT','NCHAR')  AND COLUMN_NAME NOT IN('COLLEGE_NO','STAFFNO','TITLE','COLLEGE_CODE','RESADD1','TOWNADD1','IDMARK1','IDMARK2','HEIGHT','EMAILID','ACTIVE','DOB','DOJ','RDT','STDATE','IDNO' ,'SUBDEPTNO','BANKNO','DESIG_COLLNO','DESIG_UNIVNO','SUPPLINO','SUBDESIGNO','SEQ_NO','DESIGNATURENO','ODOI','DOJ','PAY','OBASIC' ,'SBDATE','DPCAL','COLLEGE_CODE','CASTENO','CATEGORYNO','RELIGIONNO','NATIONALITYNO''SUBDEPTNO','DESIG_COLLNO','DESIG_UNIVNO','SUBDESIGNO','STNO','NATIONALITYNO','BANKCITYNO' ,'CLNO','QTRNO','ACATNO','NUDESIGNO','SHIFTNO','Expire_dateof_Extention','RELIEVING_DATE','DOI','PAYRULE','SCALENO','APPOINTNO') and ORDINAL_POSITION not in(79,80,81)", "A.COLUMN_NAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblmsg.Text = string.Empty;
        lblerror.Text = string.Empty;
        BindListViewList(ddlPayhead.SelectedValue, Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));

        //to display employee count in footer
        txtEmpoyeeCount.Text = Convert.ToString(lvMonthlyChanges.Items.Count);

        //Used in javascript to display payhead desc
        //hidPayhead.Value = ddlPayhead.SelectedItem.ToString();

        //display the total amount of payhead in footer 
        //txtPayheadName.Text = "Total Amount of " + ddlPayhead.SelectedItem.ToString() + " = ";
        //this.TotalPayheadAmount();
    }


    protected void TotalPayheadAmount()
    {
        decimal totalPayheadAmount = 0;

        foreach (ListViewDataItem lvitem in lvMonthlyChanges.Items)
        {
            TextBox txt = lvitem.FindControl("txtDays") as TextBox;
            totalPayheadAmount = totalPayheadAmount + Convert.ToDecimal(txt.Text);
        }

        txtAmount.Text = totalPayheadAmount.ToString();

    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            message = message.Replace("'", "\'");
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindListViewList(ddlPayhead.SelectedValue, Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
            pnlMonthlyChanges.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPayhead.SelectedItem.Text.Equals("NPA"))
            {
                // lblNPAStatus.Visible = true;
            }
            //BindListViewList(ddlPayhead.SelectedValue, Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
            pnlMonthlyChanges.Visible = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlPayhead_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMonthlyChanges.Visible = false;
    }

    // TO ADD BULK UPDATE EMPLOYEE FACILITY
    private DataSet GetEmployeesForEditFields_7CPC(int Staff, string payheadfield, int collegeNo)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[3];
            objParams[0] = new SqlParameter("@P_Staff", Staff);
            objParams[1] = new SqlParameter("@P_PAYHEAD", payheadfield);
            objParams[2] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_EMPLOYEE_PAYMAS_FIELD_7CPC", objParams);
        }
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForAmountDeduction-> " + ex.ToString());
        }
        return ds;
    }

    private int UpdatePaylevel(string payHead, string field, int idNo, int Paylevelno, int CellNo)
    {
        int retStatus = 0;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = null;
            //Add New File
            objParams = new SqlParameter[5];
            objParams[0] = new SqlParameter("@P_PayHead", payHead);
            objParams[1] = new SqlParameter("@P_FIELD", field);
            objParams[2] = new SqlParameter("@P_IDNO", idNo);
            objParams[3] = new SqlParameter("@P_Paylevel", Paylevelno);
            objParams[4] = new SqlParameter("@P_CellNo", CellNo);

            if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_PAYMAS_FIELDS_PAYLEVELNO", objParams, false) != null)
                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

        }
        catch (Exception ex)
        {
            retStatus = Convert.ToInt32(CustomStatus.Error);
            throw new IITMSException("IITMS.NITPRM.BusinessLayer.ChangeInMasterFileController.UpdatePayHeadAmount-> " + ex.ToString());
        }
        return retStatus;
    }
}
