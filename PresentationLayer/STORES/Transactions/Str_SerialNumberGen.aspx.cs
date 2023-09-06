using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using System.Collections;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Collections.Generic;

public partial class STORES_Transactions_Str_SerialNumberGen : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

    Str_SerialNumberGenController strSerial = new Str_SerialNumberGenController();

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
        try
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
                    //CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    if (ViewState["action"] == null)
                        ViewState["action"] = "add";

                    this.FillCategory();

                }
            }
        }
        catch (Exception ex)
        {
        }

    }

    private void FillCategory()
    {
        try
        {
            objCommon.FillDropDownList(ddlCategory, "STORE_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "MIGNO <> 0 and ITEM_TYPE='F'", "MIGNAME");
            objCommon.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=1", "MISGNAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EditAdmBatch .FillDepartment() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkUpdOpeningBalance.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkUpdOpeningBalance.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCategory.SelectedIndex = 0;
        ddlSubCategory.SelectedIndex = 0;
        ddlItems.SelectedIndex = 0;
        radlTransfer.SelectedValue = "0";
        pnlitems.Visible = false;
        divauto.Visible = false;
        chkAutoSerial.Checked = false;

    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSubCategory();
    }

    private void FillSubCategory()
    {
        try
        {
            objCommon.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=1", "MISGNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EditAdmBatch .FillDepartment() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void FillItem()
    {
        try
        {
            if (ddlSubCategory.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlItems, "store_item", "ITEM_NO", "ITEM_NAME", "MIGNO=" + Convert.ToInt32(ddlCategory.SelectedValue) + " AND MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "", "ITEM_NAME");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EditAdmBatch .FillDepartment() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }




    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillItem();
    }
    //protected void btnShow_Click(object sender, EventArgs e)
    //{
    //    int Item_no = 0;
    //    int Type = 0;

    //    Boolean Autoincrement = false;
    //    try
    //    {


    //        if (ddlItems.SelectedIndex > 0)
    //        {
    //            Item_no = Convert.ToInt32(ddlItems.SelectedValue);
    //        }
    //        else
    //        {
    //            MessageBox("Please Select Item");
    //            return;
    //        }

    //        Type = Convert.ToInt32(radlTransfer.SelectedValue);

    //        if (chkAutoSerial.Checked == true)
    //        {
    //            Autoincrement = true;
    //        }
    //        else
    //        {
    //            Autoincrement = false;
    //        }

    //        DataSet ds = strSerial.GetAllItemserial(Item_no, Type, Autoincrement);           
    //        if (ds != null && ds.Tables.Count > 0)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                lvitems.DataSource = ds;
    //                lvitems.DataBind();
    //                lvitems.Visible = true;
    //                pnlitems.Visible = true;
    //            }
    //            else
    //            {
    //                lvitems.DataSource = null;
    //                lvitems.DataBind();
    //                lvitems.Visible = false;
    //                pnlitems.Visible = false;
    //                MessageBox("No Record Found !.");
    //            }
    //        }
    //        else
    //        {

    //            lvitems.DataSource = null;
    //            lvitems.DataBind();
    //            lvitems.Visible = false;
    //            pnlitems.Visible = false;
    //            MessageBox("No Record Found !.");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}


    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            if (ddlCategory.SelectedValue == "0")
            {
                objCommon.DisplayMessage("Please Select Category", this);
                return;


                if (!chkAutoSerial.Checked)
                {
                    objCommon.DisplayMessage("Please Checked Auto Serial Number", this);
                    return;
                }
            }



            //if (radlTransfer.SelectedValue == " 0")
            //{
            foreach (ListViewItem lv in lvitems.Items)
            {
                TextBox txtSerialNo = lv.FindControl("txtSerialNo") as TextBox;
                TextBox txtSpecification = lv.FindControl("txtSpecification") as TextBox;
                TextBox txtQtySpec = lv.FindControl("txtQtySpec") as TextBox;
                TextBox txtFromDt = lv.FindControl("txtFromDt") as TextBox;

                if (txtSerialNo.Text == "")
                {
                    objCommon.DisplayMessage("Please Fill Serial Number Feild", this);
                    return;
                }
                //else if (txtFromDt.Text == "")
                //{
                //    objCommon.DisplayMessage("Please Select Depreciation  Date", this);
                //    return;
                //}



                //}





            }

            int records = 0; int TRANNO = 0;
            Str_SerialNumber objLM = new Str_SerialNumber();
            CustomStatus cs = new CustomStatus();

            //DataTable dtAppRecord = new DataTable();
            //dtAppRecord.Columns.Add("SerialNo");
            //dtAppRecord.Columns.Add("TECH_SPEC");
            //dtAppRecord.Columns.Add("QUALITY_QTY_SPEC");
            //dtAppRecord.Columns.Add("ITEM_REMARK");
            //dtAppRecord.Columns.Add("Des_Date");

            TRANNO = Convert.ToInt32(radlTransfer.SelectedValue);




            foreach (ListViewDataItem dataItem in lvitems.Items)
            {
                records += 1;

                HiddenField INVDINO = dataItem.FindControl("INVDINO") as HiddenField;

                objLM.INVDINO = Convert.ToInt32(INVDINO.Value);


                TextBox txtSerialNo = dataItem.FindControl("txtSerialNo") as TextBox;

                objLM.serialNumber = txtSerialNo.Text;

                TextBox txtSpecification = dataItem.FindControl("txtSpecification") as TextBox;
                objLM.TECH_SPEC = txtSpecification.Text;

                TextBox txtQtySpec = dataItem.FindControl("txtQtySpec") as TextBox;
                objLM.QUALITY_QTY_SPEC = txtQtySpec.Text;

                TextBox txtRemarks = dataItem.FindControl("txtRemarks") as TextBox;
                objLM.ITEM_REMARK = txtRemarks.Text;

                TextBox txtFromDt = dataItem.FindControl("txtFromDt") as TextBox;

                if (txtFromDt.Text != string.Empty)
                    objLM.DES_Date = Convert.ToDateTime(txtFromDt.Text);
                else
                    objLM.DES_Date = null;

                //DataRow dr = dtAppRecord.NewRow();

                //dr["SerialNo"] = objLM.serialNumber;
                //dr["TECH_SPEC"] = objLM.TECH_SPEC;
                //dr["QUALITY_QTY_SPEC"] = objLM.QUALITY_QTY_SPEC;
                //dr["ITEM_REMARK"] = objLM.ITEM_REMARK;
                //dr["Des_Date"] = objLM.DES_Date;

                cs = (CustomStatus)strSerial.Add_Update_SerialNumber(objLM, TRANNO); //Shaikh Juned (29/03/2022)


            }

            if ((Convert.ToInt32(radlTransfer.SelectedValue) == 0))
            {
                TRANNO = Convert.ToInt32(radlTransfer.SelectedValue);

                if (ViewState["action"].ToString().Equals("add"))
                {

                    //cs = (CustomStatus)strSerial.Add_Update_SerialNumber(objLM, dtAppRecord);
                    cs = (CustomStatus)strSerial.Add_Update_SerialNumber(objLM, TRANNO);

                }
            }
            else if (ViewState["action"].ToString().Equals("edit"))
            {
                TRANNO = Convert.ToInt32(radlTransfer.SelectedValue);
                //cs = (CustomStatus)strSerial.Add_Update_SerialNumber(objLM, dtAppRecord);
                cs = (CustomStatus)strSerial.Add_Update_SerialNumber(objLM, TRANNO);

            }

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage("Records Saved Successfully", this);
                clear();
                pnlitems.Visible = false;

            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage("Records Updated Successfully", this);

            }
            else
                objCommon.DisplayMessage("Entry Not submitted", this.Page);

        }
        catch (Exception ex)
        {

        }
    }




    private void clear()
    {
        ddlCategory.SelectedValue = "0";
        ddlSubCategory.SelectedValue = "0";
        ddlItems.SelectedValue = "0";
        radlTransfer.SelectedValue = "0";
        chkAutoSerial.Checked = false;
        divauto.Visible = false;


    }
    protected void chkAutoSerial_CheckedChanged(object sender, EventArgs e)
    {
        int Item_no = 0;
        int Type = 0;

        Boolean Autoincrement = false;
        try
        {


            if (ddlItems.SelectedIndex > 0)
            {
                Item_no = Convert.ToInt32(ddlItems.SelectedValue);
            }
            else
            {
                MessageBox("Please Select Item");
                return;
            }

            Type = Convert.ToInt32(radlTransfer.SelectedValue);

            if (chkAutoSerial.Checked == true)
            {
                Autoincrement = true;
            }
            else
            {
                Autoincrement = false;
            }
            DataSet ds = strSerial.GetAllItemserial(Item_no, Type, Autoincrement);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvitems.DataSource = ds;
                    lvitems.DataBind();
                    lvitems.Visible = true;
                    pnlitems.Visible = true;
                }
                else
                {
                    lvitems.DataSource = null;
                    lvitems.DataBind();
                    lvitems.Visible = false;
                    pnlitems.Visible = false;
                    MessageBox("No Record Found !.");
                }
            }
            else
            {

                lvitems.DataSource = null;
                lvitems.DataBind();
                lvitems.Visible = false;
                pnlitems.Visible = false;
                MessageBox("No Record Found !.");
            }
        }
        catch (Exception ex)
        {

        }


    }


    protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        Boolean Autoincrement = false;
        if (radlTransfer.SelectedValue == "0")
        {
            divauto.Visible = true;
        }
        else
        {
            divauto.Visible = false;
            Autoincrement = false;
        }
        int Item_no = 0;
        int Type = 0;
        try
        {
            if (ddlItems.SelectedIndex > 0)
            {
                Item_no = Convert.ToInt32(ddlItems.SelectedValue);
            }
            else
            {
                MessageBox("Please Select Item");
                return;
            }
            Type = Convert.ToInt32(radlTransfer.SelectedValue);

            if (chkAutoSerial.Checked == true)
            {
                Autoincrement = true;
            }
            else
            {
                Autoincrement = false;
            }
            DataSet ds = strSerial.GetAllItemserial(Item_no, Type, Autoincrement);
            

            //if (Convert.ToInt32(flag) == 1)
            //{
            //    txtSerialNo.enabled = false;

            //}
            //else
            //{
            //    txtSerialNo.enabled = true;
            
            //}
                

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
         


                    lvitems.DataSource = ds;
                    lvitems.DataBind();
                    lvitems.Visible = true;
                    pnlitems.Visible = true;
                }
                else
                {
                    lvitems.DataSource = null;
                    lvitems.DataBind();
                    lvitems.Visible = false;
                    pnlitems.Visible = false;
                    MessageBox("No Record Found / Invoice Entry Not Done Against This Item.");
                    divauto.Visible = false;
                }
            }
            else
            {

                lvitems.DataSource = null;
                lvitems.DataBind();
                lvitems.Visible = false;
                pnlitems.Visible = false;
                MessageBox("No Record Found !.");
            }
        }
        catch (Exception ex)
        {
        }


    }
    protected void radlTransfer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSubCategory.SelectedValue = "0";
            ddlItems.SelectedValue = "0";
            pnlitems.Visible = false;
            chkAutoSerial.Checked = false;
            if (radlTransfer.SelectedValue == "1")
            {
                divauto.Visible = false;
                ViewState["action"] = "Edit";
            }
            else
            {
                ViewState["action"] = "add";
            }

        }
        catch (Exception ex)
        {

        }
    }
}