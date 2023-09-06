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

public partial class PAYROLL_TRANSACTIONS_Emp_Bulk_Update : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AttendanceController objAttendance = new AttendanceController();
    ChangeInMasterFileController ObjChangeMstFile = new ChangeInMasterFileController();
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
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlSelect.Visible = true;
                pnlMonthlyChanges.Visible = false;

                //FillPayHead(Convert.ToInt32(Session["userno"].ToString()));
                FillStaff();
                GETddlPayHead();
               
            }
        }
        else
        {
            divMsg.InnerHtml = string.Empty;
        }

    }

    protected void lvMonthlyChanges_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
       // TextBox      txtEiditfiled;
        //TextBox      txtEiditfiledDT;
        //DropDownList ddlPayRule;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            TextBox txtEiditfiled = (TextBox)e. Item. FindControl("txtEditField");
            TextBox txtEditFieldDT = (TextBox)e. Item. FindControl("txtEditFieldDT");
            DropDownList ddleditfield = (DropDownList)e. Item. FindControl("ddleditfield");
            DropDownList ddlScale = (DropDownList)e. Item. FindControl("ddlScale");
            HiddenField hdneditfield = (HiddenField)e. Item. FindControl("hdneditfield");
            HiddenField hdnidno = (HiddenField)e.Item.FindControl("hdnidno");
            HiddenField hdnpayrule = (HiddenField)e.Item.FindControl("hdnpayrule");

            DropDownList ddlAppointment = (DropDownList)e.Item.FindControl("ddlAppointment");
            HiddenField hdnAppointment = (HiddenField)e.Item.FindControl("hdnAppointment");

            DropDownList ddlBloodGroup = (DropDownList)e.Item.FindControl("ddlBloodGroup");
            HiddenField hdnBloodGrp = (HiddenField)e.Item.FindControl("hdnBloodGrp");

            DropDownList ddlStaffNo = (DropDownList)e.Item.FindControl("ddlStaffNo");
            HiddenField hdnStaffno = (HiddenField)e.Item.FindControl("hdnStaffno");

            DropDownList ddlDepartment = (DropDownList)e.Item.FindControl("ddlDepartment");
            HiddenField hdnDepartment = (HiddenField)e.Item.FindControl("hdnDepartment");

            DropDownList ddlDesignation = (DropDownList)e.Item.FindControl("ddlDesignation");
            HiddenField hdnDesignation = (HiddenField)e.Item.FindControl("hdnDesignation");

            DropDownList ddlDesignature = (DropDownList)e.Item.FindControl("ddlDesignature");
            HiddenField hdnDesignature = (HiddenField)e.Item.FindControl("hdnDesignature");
            HiddenField hdnStafftype = (HiddenField)e.Item.FindControl("hdnStafftype");

            DropDownList ddlmaindept = (DropDownList)e.Item.FindControl("ddlmaindept");
            HiddenField hdnmaindept = (HiddenField)e.Item.FindControl("hdnmaindept");
            //HiddenField hdnStafftype = (HiddenField)e.Item.FindControl("hdnStafftype");


            if (ddlPayhead. SelectedItem. Text. Equals("PAYRULE"))
            {
                txtEiditfiled. Visible = false;
                txtEditFieldDT. Visible = false;
                ddlScale.Visible = false;
                ddlScale.Visible = false;
                ddlAppointment.Visible = false;
                objCommon.FillDropDownList(ddleditfield, "payroll_rule", "RULENO", "PAYRULE", "RULENO>0 and  ACTIVESTATUS = 1", "RULENO");
                ddleditfield.SelectedValue = hdnpayrule.Value;    

                  
            }

                // 18 Jan 2018


            else if (ddlPayhead.SelectedItem.Text.Equals("STNO"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                ddlScale.Visible = false;
                ddlScale.Visible = false;
                ddlAppointment.Visible = false;
                ddleditfield.Visible = true;
                objCommon.FillDropDownList(ddleditfield, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO > 0", "STNO");
                ddleditfield.SelectedValue = hdnStafftype.Value;


            }

                
            else if (ddlPayhead.SelectedItem.Text.Equals("BLOODGRPNO"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                ddlScale.Visible = false;
                ddlScale.Visible = false;
                ddlAppointment.Visible = false;
                ddleditfield.Visible = true;
                objCommon.FillDropDownList(ddleditfield, "ACD_BLOODGRP", "BLOODGRPNO", "BLOODGRPNAME", "BLOODGRPNO>0", "BLOODGRPNO");
                ddleditfield.SelectedValue = hdnBloodGrp.Value;


            }


            else if (ddlPayhead.SelectedItem.Text.Equals("STAFFNO"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                ddlScale.Visible = false;
                ddlScale.Visible = false;
                ddlAppointment.Visible = false;
                ddleditfield.Visible = true;
                objCommon.FillDropDownList(ddleditfield, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
                ddleditfield.SelectedValue = hdnStaffno.Value;


            }



            else if (ddlPayhead.SelectedItem.Text.Equals("SUBDEPTNO"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                ddlScale.Visible = false;
                ddlScale.Visible = false;
                ddlAppointment.Visible = false;
                ddleditfield.Visible = true;
                objCommon.FillDropDownList(ddleditfield, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPTNO");
                ddleditfield.SelectedValue = hdnDepartment.Value;


            }

            else if (ddlPayhead.SelectedItem.Text.Equals("SUBDESIGNO"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                ddlScale.Visible = false;
                ddlScale.Visible = false;
                ddlAppointment.Visible = false;
                ddleditfield.Visible = true;
                objCommon.FillDropDownList(ddleditfield, "PAYROLL_SUBDESIG", "SUBDESIGNO", "SUBDESIG", "SUBDESIGNO>0", "SUBDESIGNO");
                ddleditfield.SelectedValue = hdnDesignation.Value;

            }

            else if (ddlPayhead.SelectedItem.Text.Equals("DESIGNATURENO"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                ddlScale.Visible = false;
                ddlScale.Visible = false;
                ddlAppointment.Visible = false;
                ddleditfield.Visible = true;
                objCommon.FillDropDownList(ddleditfield, "PAYROLL_DESIGNATURE", "DESIGNATURENO", "DESIGNATURE", "", "DESIGNATURENO");
                ddleditfield.SelectedValue = hdnDesignature.Value;

            }

            //end of 18 jan

            else if (ddlPayhead.SelectedItem.Text.Equals("DOI") || ddlPayhead.SelectedItem.Text.Equals("DOJ"))
            {
                txtEiditfiled. Visible = false;
                txtEditFieldDT. Visible = true;
                ddleditfield. Visible = false;
                ddlScale. Visible = false;
                ddlAppointment.Visible = false;       
            }
            else if (ddlPayhead.SelectedItem.Text.Equals("SCALENO"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                ddlAppointment.Visible = false;
                ddleditfield.Visible = false;
               // objCommon.FillDropDownList(ddleditfield, "payroll_rule", "RULENO", "PAYRULE", "RULENO>0  and  ACTIVESTATUS = 1", "RULENO");
              //  ddleditfield.SelectedValue = hdnpayrule.Value;
                //ddleditfield.Enabled = true;
                objCommon.FillDropDownList(ddlScale, "PAYROLL_SCALE", "SCALENO", "SCALE", "RULENO=" + hdnpayrule.Value + "", "SCALENO");
               // objCommon.FillDropDownList(ddlScale, "PAYROLL_SCALE S INNER JOIN PAYROLL_RULE R ON(S.RULENO = R.RULENO)", "S.SCALENO", "S.SCALE+'--'+R.PAYRULE", "S.SCALENO>0", "S.SCALENO");
                ddlScale.SelectedValue = hdnidno.Value;
                
            }


            else if (ddlPayhead.SelectedItem.Text.Equals("APPOINTNO"))
            {
                ddleditfield.Visible = false;
                ddlAppointment.Visible = true;              
                ddlScale.Visible = false;


                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;

                objCommon.FillDropDownList(ddleditfield, "payroll_rule", "RULENO", "PAYRULE", "RULENO>0 and  ACTIVESTATUS = 1", "RULENO");
                ddleditfield.SelectedValue = hdnpayrule.Value;
                ddleditfield.Enabled = false;
                objCommon.FillDropDownList(ddlAppointment, "PAYROLL_APPOINT", "APPOINTNO", "APPOINT", "APPOINTNO>0", "APPOINTNO");
                ddlAppointment.SelectedValue = hdnAppointment.Value;


            }

            else if (ddlPayhead.SelectedItem.Text.Equals("MAINDEPTNO"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                ddlScale.Visible = false;
                ddlScale.Visible = false;
                ddlAppointment.Visible = false;
                ddleditfield.Visible = true;
                objCommon.FillDropDownList(ddleditfield, "PAYROLL_MAINDEPT", "MAINDEPTNO", "MAINDEPT", "MAINDEPTNO>0", "MAINDEPTNO");
                ddleditfield.SelectedValue = hdnmaindept.Value;

            } // Added on 16-09-2022
            else if (ddlPayhead.SelectedItem.Text.Equals("EMPTYPENO"))
            {
                txtEiditfiled.Visible = false;
                txtEditFieldDT.Visible = false;
                ddlScale.Visible = false;
                ddlScale.Visible = false;
                ddlAppointment.Visible = false;
                ddleditfield.Visible = true;
                objCommon.FillDropDownList(ddleditfield, "PAYROLL_EMPLOYEETYPE", "EMPTYPENO", "EMPLOYEETYPE", "EMPTYPENO>0", "EMPTYPENO");
                ddleditfield.SelectedValue = hdnmaindept.Value;
            }
            else
            {
                txtEiditfiled.Visible = true;
                txtEditFieldDT.Visible = false;
                ddleditfield.Visible = false;
                ddlScale.Visible = false;
                ddlAppointment.Visible = false;

            }

        }
    }


    private void BindListViewList(string payHead, int staffNo, int collegeNo)
    {
        try
        {
            //if (!(Convert.ToInt32(ddlStaff.SelectedIndex) == 0))
            //{

                pnlMonthlyChanges.Visible = true;
                DataSet ds = ObjChangeMstFile.GetEmployeesForEditFields(Convert.ToInt32(ddlStaff.SelectedValue),ddlPayhead.SelectedItem.Text,collegeNo);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    lvMonthlyChanges.Visible = false;
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                }
                else
                {
                    lvMonthlyChanges.Visible = true;
                    btnCancel.Visible = true;
                    btnSave.Visible = true;
                }

                lvMonthlyChanges.DataSource = ds;
                lvMonthlyChanges.DataBind();
            }

        //}
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

                if (ddlPayhead.SelectedItem.Text.Equals("DOI") || ddlPayhead.SelectedItem.Text.Equals("DOJ"))
                {
                    TextBox  txt = lvitem. FindControl("txtEditFieldDT") as TextBox;

                   // DropDownList ddl = lvitem. FindControl("ddleditfield") as DropDownList;
                    if (txt.Text == "01/01/1900")
                    {
                        txt.Text = "";
                    }
                   

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile. UpdatePayEmpmasFields(ddlPayhead. SelectedItem. Text, Convert. ToString(txt.Text), Convert. ToInt32(txt. ToolTip));
                    if (cs. Equals(CustomStatus. RecordUpdated))
                    {
                        count = 1;
                    }
                
                }
                else if (ddlPayhead. SelectedItem. Text. Equals("PAYRULE"))
                {
                    TextBox txt = lvitem. FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem. FindControl("ddleditfield") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile. UpdatePayEmpmasFields(ddlPayhead. SelectedItem. Text, Convert. ToString(ddl. SelectedValue), Convert. ToInt32(txt. ToolTip));
                    if (cs. Equals(CustomStatus. RecordUpdated))
                    {
                        count = 1;
                    }

                }


                else if (ddlPayhead.SelectedItem.Text.Equals("BLOODGRPNO"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddleditfield") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }

                }

                else if (ddlPayhead.SelectedItem.Text.Equals("STNO"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddleditfield") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }

                }

                else if (ddlPayhead.SelectedItem.Text.Equals("STAFFNO"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddleditfield") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }

                }

                else if (ddlPayhead.SelectedItem.Text.Equals("SUBDEPTNO"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddleditfield") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }

                }

                else if (ddlPayhead.SelectedItem.Text.Equals("SUBDESIGNO"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddleditfield") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }

                }
                else if (ddlPayhead.SelectedItem.Text.Equals("DESIGNATURENO"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddleditfield") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }

                }

                else if (ddlPayhead.SelectedItem.Text.Equals("SCALENO"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddlScale") as DropDownList;

                    DropDownList ddleditfield = lvitem.FindControl("ddleditfield") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                   // CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayPaymasFieldsScaleRule(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip),Convert.ToString(ddleditfield.SelectedValue));
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
                        objCommon.DisplayMessage("RFIDNO = "+Convert.ToString(txt.Text)+" already exists. Duplicate Entry For IDNO=" + Convert.ToInt32(txt.ToolTip), this.Page);
                        return;
                    }
                }


               //10  jan 2018
                else if (ddlPayhead.SelectedItem.Text.Equals("APPOINTNO"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddlAppointment") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }

                }
                else if (ddlPayhead.SelectedItem.Text.Equals("MAINDEPTNO"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddleditfield") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }
                } // Added on 16-09-2022
                else if (ddlPayhead.SelectedItem.Text.Equals("EMPTYPENO"))
                {
                    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                    DropDownList ddl = lvitem.FindControl("ddleditfield") as DropDownList;

                    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        count = 1;
                    }
                }
                //else if (ddlPayhead.SelectedItem.Text.Equals("PFNO"))
                //{
                //    TextBox txt = lvitem.FindControl("txtEditFieldDT") as TextBox;

                //    DropDownList ddl = lvitem.FindControl("ddleditfield") as DropDownList;

                //    CustomStatus cs = (CustomStatus)ObjChangeMstFile.UpdatePayEmpmasFields(ddlPayhead.SelectedItem.Text, Convert.ToString(ddl.SelectedValue), Convert.ToInt32(txt.ToolTip));
                //    if (cs.Equals(CustomStatus.RecordUpdated))
                //    {
                //        count = 1;
                //    }
                //}
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
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
        pnlMonthlyChanges.Visible = false;
        ddlPayhead.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0; //add

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
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
           // objCommon.FillDropDownList(ddlPayhead, "INFORMATION_SCHEMA.COLUMNS A", "ROW_NUMBER()over(order by ORDINAL_POSITION)as ORDINAL_POSITION", "COLUMN_NAME", "TABLE_NAME in ('PAYROLL_EMPMAS','PAYROLL_PAYMAS') AND DATA_TYPE in  ('NVARCHAR','DATETIME','INT','NCHAR')  AND COLUMN_NAME NOT IN('TITLE','COLLEGE_CODE','RESADD1','TOWNADD1','IDMARK1','IDMARK2','HEIGHT','EMAILID','ACTIVE','DOB','DOJ','RDT','STDATE','IDNO' ,'SUBDEPTNO','BANKNO','DESIG_COLLNO','DESIG_UNIVNO','SUPPLINO','SUBDESIGNO','SEQ_NO','DESIGNATURENO','ODOI','DOJ','PAY','OBASIC' ,'SBDATE','DPCAL','COLLEGE_CODE','CASTENO','CATEGORYNO','RELIGIONNO','NATIONALITYNO''SUBDEPTNO','DESIG_COLLNO','DESIG_UNIVNO','SUBDESIGNO','STNO','STATUSNO','NATIONALITYNO','BANKCITYNO' ,'CLNO','QTRNO','ACATNO','NUDESIGNO','SHIFTNO') and ORDINAL_POSITION not in(79,80,81)", "A.COLUMN_NAME");
           // objCommon.FillDropDownList(ddlPayhead, "INFORMATION_SCHEMA.COLUMNS A", "ROW_NUMBER()over(order by ORDINAL_POSITION)as ORDINAL_POSITION", "COLUMN_NAME", "TABLE_NAME in ('PAYROLL_EMPMAS','PAYROLL_PAYMAS') AND DATA_TYPE in  ('NVARCHAR','DATETIME','INT','NCHAR')  AND COLUMN_NAME NOT IN('TITLE','COLLEGE_CODE','RESADD1','TOWNADD1','IDMARK1','IDMARK2','HEIGHT','EMAILID','ACTIVE','DOB','RDT','STDATE','IDNO' ,'BANKNO','DESIG_COLLNO','DESIG_UNIVNO','SUPPLINO','SEQ_NO','ODOI','PAY','OBASIC' ,'SBDATE','DPCAL','COLLEGE_CODE','CASTENO','CATEGORYNO','RELIGIONNO','NATIONALITYNO','DESIG_COLLNO','DESIG_UNIVNO','STATUSNO','NATIONALITYNO','BANKCITYNO' ,'CLNO','QTRNO','ACATNO','NUDESIGNO','SHIFTNO','PGSUBDEPTNO','LOGIN_STATUS','COLLEGE_NO','RELIEVING_DATE','LOG','saldeposite_obdate','STATUS','PFNO','SUBCASTE','UA_TYPE','Expire_dateof_Extention','ANFN') and ORDINAL_POSITION not in(79,80,81)", "A.COLUMN_NAME");
        //'EMPTYPENO',
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

        }
        catch (Exception ex)
        { 
        }
    }
    protected void ddlPayhead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvMonthlyChanges.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
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
            BindListViewList(ddlPayhead.SelectedValue, Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
            txtEmpoyeeCount.Text = Convert.ToString(lvMonthlyChanges.Items.Count);
        }
        catch (Exception ex)
        {
        }
    }

    //22-09-2022 ddlpayhead drowndown 

    protected void GETddlPayHead()
    {
        try
        {
            PayHeadPrivilegesController objPayHead = new PayHeadPrivilegesController();
            DataSet ds = null;
            ds = objPayHead.GetPayHeadsforbulkupdate();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPayhead.DataSource = ds;
                ddlPayhead.DataValueField = ds.Tables[0].Columns[1].ToString();
                //ddlPayhead.DataTextField = ds.Tables[0].Columns[2].ToString();
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
}
