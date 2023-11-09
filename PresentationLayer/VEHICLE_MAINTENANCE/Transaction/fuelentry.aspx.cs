//===========================================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// MODIFIED BY   : MRUNAL SINGH
// MODIFIED DATE : 06-OCT-2015
// DESCRIPTION   : USE TO ENTER THE ITEMS(FUEL/INDENTS) WHICH ARE ISSUED TO VEHICLES/DRIVERS.
//============================================================================================
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class VEHICLE_MAINTENANCE_Transaction_fuelentry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VM objVM = new VM();
    VMController objVMC = new VMController();
        BlobController objBlob = new BlobController();
        Panel panelfordropdown;
        string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();
        decimal File_size;
    string routeCode = string.Empty;

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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    //  objCommon.FillDropDownList(ddlVehicle, "VEHICLE_MASTER", "VIDNO", "REGNO +':'+NAME", "VIDNO>0", "VIDNO");
                    FillDropDownVehicle();
                    FillDropDown();
                    FillDropDownDriver();
                    //BindList(Convert.ToInt32(rdblistVehicleTypes.SelectedValue.ToString()));
                   // divFuelDate.Visible = false;
                    BlobDetails();
                }


            }
            btnSubmit.Enabled = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_fuelentry.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This Event is used to display data page wise.



    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    BindList(Convert.ToInt32(rdblistVehicleTypes.SelectedValue));
    //}
    // This method is used to display the list of items issued.
    private void BindList(int cat, int issue)
    {
        try
        {
            lvFuel.DataSource = null;
            DataSet ds = objVMC.GetFuelEntryAll(cat, issue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvFuel.DataSource = ds;
                lvFuel.DataBind();
                foreach (ListViewItem item in lvFuel.Items)
                {
                    ImageButton imgBtn = item.FindControl("btnEdit") as ImageButton;
                    HiddenField hdItemId = item.FindControl("hdItemId") as HiddenField;
                    HiddenField hdFEID = item.FindControl("hdFEID") as HiddenField;
                    int FEID = Convert.ToInt32(objCommon.LookUp("VEHICLE_FUELENTRY", "Max(FEID)", "ITEM_ID='" +Convert.ToInt32(hdItemId.Value)+ "'"));
                    if (Convert.ToInt32(hdFEID.Value) !=Convert.ToInt32(FEID))
                    {
                        imgBtn.Enabled = false;

                    }

                }

                lvFuel.Visible = true;
                pnlList.Visible = true;
            }
            else
            {
                lvFuel.DataSource = null;
                lvFuel.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_fuelentry.BindList -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to check the page authority.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }
    // This method is used to fill the drop down list.

    private void FillDropDownVehicle()
    {
        objCommon.FillDropDownList(ddlVehicle, "VEHICLE_MASTER", "VIDNO", "REGNO +':'+NAME", "VIDNO>0 AND ACTIVE_STATUS=1", "VIDNO");
    }
    private void FillDropDownHireVehicle()
    {
        objCommon.FillDropDownList(ddlVehicle, "VEHICLE_HIRE_MASTER", "VEHICLE_ID", "VEHICLE_NAME", "VEHICLE_ID>0 AND ACTIVE_STATUS=1", "VEHICLE_ID");
    }


    private void FillDropDown()
    {

        objCommon.FillDropDownList(ddlItem, "VEHICLE_ITEMMASTER", "ITEM_ID", "ITEM_NAME", "ITEM_TYPE=" + rdblistVehicleType.SelectedValue, "ITEM_ID");
         
    }

    private void FillDropDownHire()
    {

        objCommon.FillDropDownList(ddlItem, "VEHICLE_ITEMMASTER", "ITEM_ID", "ITEM_NAME", "ITEM_TYPE=" + rdblistVehicleType.SelectedValue, "ITEM_ID");
    }


    private void FillDropDownDriver()
    {

        objCommon.FillDropDownList(ddlDriver, "VEHICLE_DRIVERMASTER", "DNO", "DNAME", "", "DNAME");

    }
    protected void rdblistVehicleTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblistVehicleTypes.SelectedItem.Text.Equals("College Vehicles"))
        {
            //   BindExpiryList();
            FillDropDownVehicle();
            BindList(Convert.ToInt32(rdblistVehicleTypes.SelectedValue.ToString()),0);
            clear();
            ddlIssueType.SelectedValue = "0";
            // ViewState["action"] = "add";
        }
        else
        {
            //  BindExpiryList();
            FillDropDownHireVehicle();
            BindList(Convert.ToInt32(rdblistVehicleTypes.SelectedValue.ToString()),0);
            clear();
            ddlIssueType.SelectedValue = "0";
            //  ViewState["action"] = "add";
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
        ddlIssueType.SelectedValue = "0";
        //lvFuel.DataSource = null;
        //lvFuel.DataBind();
        lvFuel.Visible = false;
     //   updDocument.Visible = false;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btnDelete = sender as ImageButton;
        int no = int.Parse(btnDelete.CommandArgument);
        objVM.FID = Convert.ToInt32(no);
        CustomStatus CS = (CustomStatus)objVMC.DeleteFitnessByFID(objVM);
        if (CS.Equals(CustomStatus.RecordDeleted))
        {
            objCommon.DisplayMessage("Record Deleted Sucessfully.", this.Page);
            //  BindList(Convert.ToInt32(rdblistVehicleTypes.SelectedValue));
        }
    }

    private void clear()
    {
        txtRate.Text = string.Empty;
        ddlVehicle.SelectedValue = "0";
        ddlDriver.SelectedValue = "0";
        txtFuelDate.Text = string.Empty;
        txtBillNo.Text = string.Empty;
        txtBillDate.Text = string.Empty;
        ddlItem.SelectedValue = "0";
        txtRemark.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtLogNo.Text = string.Empty;
        ViewState["action"] = "add";
        ViewState["FEID"] = null;
        lblUnit.Text = string.Empty;
        ddlDriver_SelectedIndexChanged(null, null);
        txtMeterReading.Text = string.Empty;

        txtEndReading.Text = string.Empty;
        txtNoOfKms.Text = string.Empty;
        txtMilege.Text = string.Empty;
        txtMilegeAmount.Text = string.Empty;
        hdnRate.Value = string.Empty;
       // ddlIssueType.SelectedValue = "0";
        txtAvlQty.Text = string.Empty;
        txtCoupNo.Text = string.Empty;
        txtTotalAmount.Text = string.Empty;
        ddlDepartment.SelectedValue = "0";
        txtDateOfWithdrawal.Text = string.Empty;
        ddlDieselReq.SelectedValue = "0";
        ddlApprover.SelectedValue = "0";
        txtPurposeofwithdrawal.Text = string.Empty;
       
    }


    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        int no = int.Parse(imgBtn.CommandArgument);
        ViewState["FEID"] = int.Parse(imgBtn.CommandArgument);
        ViewState["action"] = "edit";
        ShowDetails(no);
    }
    ////  // This method is used to show the details of the selected record.
    private void ShowDetails(int NO)
    {
        try
        {
            objVM.FID = Convert.ToInt32(NO);
            DataSet ds = objVMC.GetFuelEntryById(NO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                  
                    ddlVehicle.SelectedValue = dr["VIDNO"].ToString();
                    ddlVehicle_SelectedIndexChanged(null, null);
                    objCommon.FillDropDownList(ddlDriver, "VEHICLE_DRIVERMASTER", "DNO", "DNAME", "", "DNAME");
                    ddlDriver.SelectedValue = dr["DNO"].ToString();
                    if(ds.Tables[0].Rows[0]["FUELDATE"].ToString() != "")
                    {
                    txtFuelDate.Text = Convert.ToDateTime(dr["FUELDATE"]).ToString("dd/MM/yyyy");  //04-04-2023
                    }
                    txtQty.Text = dr["QTY"].ToString();
                    txtRemark.Text = dr["REMARK"].ToString();
                    txtLogNo.Text = dr["LOGNO"].ToString();
                    //txtCurMeterReading.Text = Convert.ToDouble(dr["CMREADING"]).ToString("0.00");
                    //txtLastMReading.Text = Convert.ToDouble(dr["LMREADING"]).ToString("0.00");
                    // LOGBOOKID
                    //  txtBillNo.Text = dr["BILLNO"].ToString();
                    //  txtBillDate.Text = Convert.ToDateTime(dr["BILLDATE"]).ToString("dd/MM/yyyy");
                    // txtAmount.Text = Convert.ToDouble(dr["AMOUNT"]).ToString("0.00");                   

                    if (ds.Tables[0].Rows[0]["VEHICLE_CATEGORY"].ToString() == Convert.ToString('F'))
                    {
                        rdblistVehicleType.SelectedValue = Convert.ToString(1);
                    }
                    else
                    {
                        rdblistVehicleType.SelectedValue = Convert.ToString(2);
                    }

                    if (ds.Tables[0].Rows[0]["VEHICLE_TYPE"].ToString() == Convert.ToString('C'))
                    {
                        rdblistVehicleTypes.SelectedValue = Convert.ToString(1);
                    }
                    else
                    {
                        rdblistVehicleTypes.SelectedValue = Convert.ToString(2);
                    }

                    FillDropDown();
                    ddlItem.SelectedValue = dr["ITEM_ID"].ToString();
                    txtMeterReading.Text = dr["METER_READING"].ToString();
                   // ddlItem_SelectedIndexChanged(null, null);

                    txtEndReading.Text = Convert.ToDouble(dr["LMREADING"]).ToString("0.00");
                    txtNoOfKms.Text = Convert.ToDouble(dr["NO_OF_KMS"]).ToString("0.00");
                    txtMilege.Text = Convert.ToDouble(dr["MILEGE"]).ToString("0.00");
                    txtMilegeAmount.Text = Convert.ToDouble(dr["AMOUNT"]).ToString("0.00");
                    txtRate.Text = Convert.ToDouble(dr["RATE"]).ToString("0.00");
                    hdnRate.Value = Convert.ToDouble(dr["RATE"]).ToString("0.00");
                    txtAvlQty.Text = Convert.ToDouble(dr["AVELABLE_QTY"]).ToString("0.00");
                    if (ds.Tables[0].Rows[0]["ISSUE_TYPE"].ToString()=="1")
                    {
                        ddlIssueType.SelectedValue = dr["ISSUE_TYPE"].ToString();
                        txtCoupNo.Text = dr["COUPON_NO"].ToString();
                        txtTotalAmount.Text = dr["TOTAL_AMOUNT"].ToString();
                       
                    }
                    else if (ds.Tables[0].Rows[0]["ISSUE_TYPE"].ToString() == "2")
                    {
                        ddlIssueType.SelectedValue = dr["ISSUE_TYPE"].ToString();
                            ddlDepartment.SelectedValue=dr["DEPARTMENT"].ToString(); 
                              txtDateOfWithdrawal.Text =Convert.ToDateTime(dr["DATE_OF_WITHDRAWAL"]).ToString("dd/MM/yyyy");
                               ddlDieselReq.SelectedValue=dr["DIESEL_REQUESTER"].ToString();
                                  ddlApprover.SelectedValue=dr["APPROVER"].ToString();
                                  txtPurposeofwithdrawal.Text = dr["PURPOSE_OF_WITHDRAWAL"].ToString(); 
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_FuelEntry.ShowDetails() -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BlobDetails()
    {
        try
        {
            string Commandtype = "ContainerNamevehicledoctest";
            DataSet ds = objBlob.GetBlobInfo(2, Commandtype);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsConnection = objBlob.GetConnectionString(2, Commandtype);
                string blob_ConStr = dsConnection.Tables[0].Rows[0]["BlobConnectionString"].ToString();
                string blob_ContainerName = ds.Tables[0].Rows[0]["CONTAINERVALUE"].ToString();
                // Session["blob_ConStr"] = blob_ConStr;
                // Session["blob_ContainerName"] = blob_ContainerName;
                hdnBlobCon.Value = blob_ConStr;
                hdnBlobContainer.Value = blob_ContainerName;
                lblBlobConnectiontring.Text = Convert.ToString(hdnBlobCon.Value);
                lblBlobContainer.Text = Convert.ToString(hdnBlobContainer.Value);
            }
            else
            {
                hdnBlobCon.Value = string.Empty;
                hdnBlobContainer.Value = string.Empty;
                lblBlobConnectiontring.Text = string.Empty;
                lblBlobContainer.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

      public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".jpeg", ".JPEG",".png",".PNG"};
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }
    // This event is used to save the issue entry.
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdblistVehicleType.SelectedValue=="2")
            {
                if (ddlVehicle.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this.updActivity, "Please Select Vehicle.", this.Page);
                    return;
                }
                if (ddlDriver.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this.updActivity, "Please Select Driver.", this.Page);
                    return;
                }
                if (txtFuelDate.Text == string.Empty)
                {
                    objCommon.DisplayMessage(this.updActivity, "Please Enter Issue Date.", this.Page);
                    return;
                }
            }
            else if (rdblistVehicleType.SelectedValue == "1")
            {
                if (ddlIssueType.SelectedValue == "1")
                {
                    if (ddlVehicle.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.updActivity, "Please Select Vehicle.", this.Page);
                        return;
                    }

                   DateTime currenttime =Convert.ToDateTime(DateTime.Today.ToString("dd-MM-yyyy"));
                    if (Convert.ToDateTime(txtFuelDate.Text) > currenttime)
                    {
                        objCommon.DisplayMessage(updActivity, "Future Date Is Not Allowed.", this.Page);
                        txtEndReading.Text = string.Empty;
                        return;

                    }
                    if (ViewState["FEID"] == null)
                    {
                        string Coupon_No = objCommon.LookUp("VEHICLE_FUELENTRY", "(ISNULL(COUPON_NO,'')) AS COUPON_NO", "COUPON_NO='" + txtCoupNo.Text.ToString() + "'");
                        if (Coupon_No == txtCoupNo.Text)
                        {
                            objCommon.DisplayMessage(updActivity, "Coupon No. is Already Exist.", this.Page);
                            txtEndReading.Text = string.Empty;
                            return;
                        }
                    }
                    else
                    {
                        string Coupon_No = objCommon.LookUp("VEHICLE_FUELENTRY", "(ISNULL(COUPON_NO,'')) AS COUPON_NO", "COUPON_NO='" + txtCoupNo.Text.ToString() + "' and FEID!='"+ Convert.ToInt32(ViewState["FEID"])+"'");
                        if (Coupon_No == txtCoupNo.Text)
                        {
                            objCommon.DisplayMessage(updActivity, "Coupon No. is Already Exist.", this.Page);
                            txtEndReading.Text = string.Empty;
                            return;
                        }
                    }
                }
            }
            objVM.FVIDNO = Convert.ToInt32(ddlVehicle.SelectedValue);
            objVM.FDNO = Convert.ToInt32(ddlDriver.SelectedValue);
            if (txtFuelDate.Text != string.Empty)     //Shaikh Juned (29-03-2023)
            {
                objVM.FFDATE = Convert.ToDateTime(txtFuelDate.Text);
            }
            else
            {
                objVM.FFDATE = DateTime.MinValue;
            }
           // objVM.FFDATE = Convert.ToDateTime(txtFuelDate.Text);
            objVM.ITEM_ID = Convert.ToInt32(ddlItem.SelectedValue);
            objVM.FREMARK = txtRemark.Text;
            // objVM.FBILLNO = txtBillNo.Text;
            // objVM.FBILLDATE = Convert.ToDateTime(txtBillDate.Text);
            //  objVM.FFUELTYPE = ddlItem.SelectedValue;               
            // objVM.FAMOUNT = Convert.ToDouble( txtAmount.Text);     
            objVM.FQTY = Convert.ToDouble(txtQty.Text);

            objVM.FLOGNO = txtLogNo.Text;
            objVM.FCOLLEGECODE = Convert.ToInt32(Session["colcode"].ToString());

            if (rdblistVehicleType.SelectedItem.Text.Equals("Fuels"))
            {
                if (ddlIssueType.SelectedValue=="1")
                {
                objVM.VEHICLECAT = Convert.ToString('F');
                objVM.END_METER_READING = Convert.ToDouble(txtEndReading.Text);
                objVM.NO_OF_KMS = Convert.ToDouble(txtEndReading.Text) - Convert.ToDouble(txtMeterReading.Text);  //Convert.ToDouble(txtNoOfKms.Text);
                objVM.MILEGE = (Convert.ToDouble(txtEndReading.Text) - Convert.ToDouble(txtMeterReading.Text)) / Convert.ToDouble(txtQty.Text);   //Convert.ToDouble(txtMilege.Text);
                objVM.MILEGE_AMOUNT = Convert.ToDouble(txtMilegeAmount.Text); //Convert.ToDouble(txtMilegeAmount.Text);
                objVM.RATE = Convert.ToDecimal(txtRate.Text);
                objVM.ISSUE_TYPE = Convert.ToInt32(ddlIssueType.SelectedValue); //----29-03-2023
                objVM.AVELABLE_QTY = Convert.ToDecimal(txtAvlQty.Text);             //----29-03-2023
                objVM.COUPON_NO = txtCoupNo.Text;                                 //----29-03-2023
                objVM.TOTAL_AMOUNT1 = Convert.ToDecimal(txtTotalAmount.Text);        //----29-03-2023
                objVM.ORIGINAL_RATE = Convert.ToDecimal(hdnRate.Value);
                objVM.PURPOSE_OF_WITHDRAWAL = string.Empty;
                }
                else if (ddlIssueType.SelectedValue == "2")
                {
                    //-------01-04-2023---start
                    bool result1 = false;
                     string DOCFOLDER = file_path + "VEHICLE_MAINTENANCE\\UploadFiles";
                         string filename = string.Empty;
                         if (lblBlobConnectiontring.Text != "")
                         {
                             objVM.IsBlob = 1;
                         }
                         else
                         {
                             objVM.IsBlob = 0;
                         }
                        
                        
                         string FilePath = string.Empty;

                         if (UploadrequestLetter.HasFile == true)
                         {
                             //if (FileTypeValid(System.IO.Path.GetExtension(UploadrequestLetter.FileName)))
                             //{

                             //}
                             //else
                             //{

                             //    objCommon.DisplayMessage(updActivity, "Please Upload Valid Image[.jpg,.JPG,.jpeg,.JPEG,.png,.PNG]", this.Page);
                             //    UploadrequestLetter.Focus();
                             //    return;
                             //}

                             string fileName = System.Guid.NewGuid().ToString() + UploadrequestLetter.FileName.Substring(UploadrequestLetter.FileName.IndexOf('.'));
                             string fileExtention = System.IO.Path.GetExtension(fileName);
                             string ext = System.IO.Path.GetExtension(UploadrequestLetter.PostedFile.FileName);

                             int sub_no = Convert.ToInt32(objCommon.LookUp("VEHICLE_FUELENTRY", "(ISNULL(MAX(FEID),0))+1 AS FEID", ""));

                             filename = sub_no + "_RequestLetter_" + sub_no;

                             objVM.UploadBlobName = sub_no + "_RequestLetter_" + sub_no + ext;

                             string UploadRequestletter = objVM.UploadBlobName;

                             if (UploadrequestLetter.HasFile.ToString() != "")
                             {

                                 objVM.UploadRequestLetter = UploadrequestLetter.FileName;

                                 string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                 string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                 result1 = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                 if (result1 == true)
                                 {

                                     int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, filename, UploadrequestLetter);
                                     if (retval == 0)
                                     {
                                         ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                         return;
                                     }

                                     objVM.UploadRequestLetter = UploadRequestletter;


                                 }
                                 else
                                 {
                                     objVM.UploadRequestLetter = UploadrequestLetter.FileName;

                                 }
                             }
                         }

                    //-------01-04-2023-----end

                    objVM.VEHICLECAT = Convert.ToString('F');
                    objVM.DEPARTMENT = Convert.ToInt32(ddlDepartment.SelectedValue);
                    objVM.DATE_OF_WITHDRAWAL = Convert.ToDateTime(txtDateOfWithdrawal.Text);
                    objVM.DIESEL_REQUESTER = Convert.ToInt32(ddlDieselReq.SelectedValue);
                    objVM.APPROVER = Convert.ToInt32(ddlApprover.SelectedValue);
                    objVM.PURPOSE_OF_WITHDRAWAL = txtPurposeofwithdrawal.Text;
                    objVM.ISSUE_TYPE = Convert.ToInt32(ddlIssueType.SelectedValue); //----29-03-2023
                    objVM.AVELABLE_QTY = Convert.ToDecimal(txtAvlQty.Text); 

                }

            }
            else
            {

                if (ViewState["FEID"] == null)
                {
                    int DuplicateIndent = Convert.ToInt32(objCommon.LookUp("VEHICLE_FUELENTRY", "(Count(ISNULL(FEID,0))) AS FEID", "VIDNO='" + ddlVehicle.SelectedValue + "' and ITEM_ID='" + ddlItem.SelectedValue + "' and FUELDATE='" + Convert.ToDateTime(txtFuelDate.Text).ToString("yyyy-MM-dd") + "'"));
                    if (DuplicateIndent > 0)
                    {
                        objCommon.DisplayMessage(updActivity, "Indent is Already Exist.", this.Page);
                       // txtEndReading.Text = string.Empty;
                        return;
                    }
                }
                else
                {
                    int DuplicateIndent = Convert.ToInt32(objCommon.LookUp("VEHICLE_FUELENTRY", "(Count(ISNULL(FEID,0))) AS FEID", "VIDNO='" + ddlVehicle.SelectedValue + "' and ITEM_ID='" + ddlItem.SelectedValue + "' and FUELDATE='" + Convert.ToDateTime(txtFuelDate.Text).ToString("yyyy-MM-dd") + "' and FEID !='" + Convert.ToInt32(ViewState["FEID"]) + "'"));
                    if (DuplicateIndent>0)
                    {
                        objCommon.DisplayMessage(updActivity, "Indent is Already Exist.", this.Page);
                      //  txtEndReading.Text = string.Empty;
                        return;
                    }
                }

                objVM.VEHICLECAT = Convert.ToString('I');
                objVM.PURPOSE_OF_WITHDRAWAL = string.Empty;
            }

            if (txtMeterReading.Text!=string.Empty)
            {
            objVM.METER_READING = Convert.ToDouble(txtMeterReading.Text);
            }

            if (rdblistVehicleTypes.SelectedItem.Text.Equals("College Vehicles"))
            {
                objVM.VEHICLETYPES = Convert.ToString('C'); // C for college vehicle and H for hired vehicle
            }
            else
            {
                objVM.VEHICLETYPES = Convert.ToString('H');
            }


          
            objVM.USERNO = Convert.ToInt32(Session["userno"]); 


            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //objVM.LLOGBOOKID = 0;
                     //DataSet ds = objCommon.FillDropDown("VEHICLE_DRIVERMASTER", "PHONE", "DNAME", "PHONE='" + txtDrvrCntNo.Text.ToString() + "'", "");
                     //if (ds.Tables[0].Rows.Count > 0)
                     //{
                     //    objCommon.DisplayMessage(this.updActivity, "Record Is Already Exist.", this.Page);
                     //    return;
                     //}
                    CustomStatus cs = (CustomStatus)objVMC.FuelEntryInsertUpdate(objVM);
                    //if (cs.Equals(CustomStatus.RecordSaved))
                    //{
                       
                        ViewState["action"] = "add";
                        BindList(Convert.ToInt32(rdblistVehicleTypes.SelectedValue),Convert.ToInt32( ddlIssueType.SelectedValue));
                        // objCommon.DisplayMessage("Record Save Successfully.", this.Page);
                        clear();
                        ddlIssueType.SelectedValue = "0";
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.updActivity, "Record Save Successfully.", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            objCommon.DisplayMessage(this.updActivity, "Coupon No. is Already Exist.", this.Page);
                        }
                        return;
                   // }
                }
                else
                {
                    if (ViewState["FEID"] != null)
                    {
                        objVM.FFEID = Convert.ToInt32(ViewState["FEID"].ToString());
                        CustomStatus cs = (CustomStatus)objVMC.FuelEntryInsertUpdate(objVM);
                        //if (cs.Equals(CustomStatus.RecordSaved))  //06-10-2023
                        //{

                            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue), Convert.ToInt32(ddlIssueType.SelectedValue));
                            clear();
                            ddlIssueType.SelectedValue = "0";
                            ViewState["action"] = "add";
                            // objCommon.DisplayMessage("Record Updated Successfully.", this.Page);
                             if (cs.Equals(CustomStatus.RecordSaved))
                            {
                            objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                            }
                            else if (cs.Equals(CustomStatus.RecordExist))
                            {
                                objCommon.DisplayMessage(this.updActivity, "Coupon No. is Already Exist.", this.Page);
                            }
                            return;
                       // }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_fuelentry.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    //This event is used to fill the list of Drivers.
    protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string vehCat = string.Empty;
            Decimal EndMreading;
            if (rdblistVehicleType.SelectedValue == "1")
            {
                vehCat = "F";
            }
            else
            {
                vehCat = "I";
            }

            if (ddlVehicle.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlDriver, "VEHICLE_ROUTEALLOTMENT RA INNER JOIN VEHICLE_DRIVERMASTER D ON (RA.DRIVER_ID = D.DNO)", "DISTINCT RA.DRIVER_ID", "D.DNAME", "RA.STATUS = 0 AND RA.VIDNO=" + Convert.ToInt32(ddlVehicle.SelectedValue), "D.DNAME");
                // objCommon.FillDropDownList(ddlDriver, "VEHICLE_DRIVERMASTER D", "DNO", "D.DNAME", "RA.VIDNO=" + Convert.ToInt32(ddlVehicle.SelectedValue), "D.DNAME");

            }
            DataSet ds = objCommon.FillDropDown("VEHICLE_FUELENTRY VF INNER JOIN VEHICLE_ITEMMASTER IM ON (VF.ITEM_ID = IM.ITEM_ID) INNER JOIN VEHICLE_UNIT U ON (U.UNIT_ID = IM.UNIT)", " TOP 10 IM.ITEM_NAME, CAST(VF.QTY AS NVARCHAR(255)) +' '+ U.UNIT_NAME AS QTY", "VF.FUELDATE", "VF.VIDNO=" + ddlVehicle.SelectedValue + " AND VEHICLE_CATEGORY ='" + vehCat + "'", "VF.FUELDATE DESC");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //lvLastIssue.DataSource = ds;
                //lvLastIssue.DataBind();
                //lvLastIssue.Visible = true;
            }
            else
            {
                //lvLastIssue.DataSource = null;
                //lvLastIssue.DataBind();
                //lvLastIssue.Visible = false;
            }
            // Changes is done acording to crescent ticket 49880 Start Meter Reading need to change Zero because of change vehicle meter 
            int VehFix=0;
            DateTime meterchangedate = DateTime.MinValue;
            DataSet dsConf = objCommon.FillDropDown("VEHICLE_METER_CONFIGURATION ", "IsNull( VEHICLE_ID,0) as VEHICLE_ID", "cast (READING_DATE as date) as READING_DATE", "VEHICLE_ID='" + ddlVehicle.SelectedValue + "'", "");
            if (dsConf.Tables[0].Rows.Count != 0)
            {
                VehFix = Convert.ToInt32(dsConf.Tables[0].Rows[0]["VEHICLE_ID"].ToString());
                //meterchangedate = Convert.ToDateTime(dsConf.Tables[0].Rows[0]["READING_DATE"]).ToString("yyyy-MM-dd");
              //  meterchangedate = Convert.ToDateTime(dsConf["READING_DATE"]).ToString("dd/MM/yyyy");
                if (dsConf.Tables[0].Rows[0]["READING_DATE"].ToString() != "")
                {
                    meterchangedate = Convert.ToDateTime(dsConf.Tables[0].Rows[0]["READING_DATE"].ToString());  //04-04-2023
                }
            }
            else
            {
                 VehFix = 0;
                meterchangedate=Convert.ToDateTime("2023-10-17");
            }
            
            //DataSet ds1 = objCommon.FillDropDown("VEHICLE_FUELENTRY ", "IsNull( Max(LMREADING),0) as LMREADING", "VIDNO", "VIDNO=" + ddlVehicle.SelectedValue + " AND VEHICLE_CATEGORY ='" + vehCat + "' group by VIDNO", "");
            DataSet ds1 = objCommon.FillDropDown("VEHICLE_FUELENTRY ", "LMREADING as LMREADING", "VIDNO", "VIDNO=" + ddlVehicle.SelectedValue + " AND LMREADING=(case when VIDNO=" + VehFix + " then ((SELECT ISNULL(MAX(LMREADING),0) AS LMREADING FROM VEHICLE_FUELENTRY WHERE VEHICLE_CATEGORY ='F' and VIDNO=" + VehFix + " and cast (CREATED_DATE as date) BETWEEN ( ( CONVERT(DATE,'" + Convert.ToDateTime(meterchangedate).ToString("yyyy-MM-dd") + "'))) AND CAST(GETDATE() AS DATE))) else (SELECT ISNULL(MAX(LMREADING),0) AS LMREADING FROM VEHICLE_FUELENTRY WHERE VEHICLE_CATEGORY ='F' and VIDNO=" + ddlVehicle.SelectedValue + ") end)", "");
            if (ds1.Tables[0].Rows.Count > 0)
          {
            EndMreading  =Convert.ToDecimal( ds1.Tables[0].Rows[0]["LMREADING"].ToString());
          }
          else
          {
              EndMreading=0;
          }
           txtMeterReading.Text =(EndMreading).ToString();  //Remove + 1 in End Meter Reading according to Gudham Sir 10-04-2023
           txtEndReading.Text = string.Empty;
           txtNoOfKms.Text = string.Empty;
           txtMilege.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_fuelentry.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // this event is used to fill the list of items.
    protected void rdblistVehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblistVehicleType.SelectedItem.Text.Equals("Fuels"))
        {
            divIssuetype.Visible = true;
            divItemName.Visible = true;
            divAvailableQty.Visible = true;
            divCouponNo.Visible = true;
            //divIssueDate.Visible = true;
            divFuelDate.Visible = true;

            divRate.Visible = true;
            div1.Visible = true;
            divstrmetread.Visible = true;
            divEndMetRead.Visible = true;
            divkms.Visible = true;
            divmilege.Visible = true;
            divAmount.Visible = false;
            divRemark.Visible = true;


            trAmt.Visible = false;
          //  divMeterRead.Visible = true;
            divRemark.Visible = true;
            FillDropDown();
            //BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue));
            clear();
            ddlIssueType.SelectedValue = "0";
            btnDirectReport.Visible = false;
           // divRate.Visible = true;
            lvFuel.DataSource = null;
            lvFuel.DataBind();
        }
        else
        {
            divIssuetype.Visible = false;
            divItemName.Visible = false;
            divAvailableQty.Visible = false;
            divCouponNo.Visible = false;
            //divIssueDate.Visible = false;
            divFuelDate.Visible = true;
            divItemName.Visible = true;
            divQty.Visible = true;
            divRate.Visible = false;
            div1.Visible = false;
            divstrmetread.Visible = false;
            divEndMetRead.Visible = false;
            divkms.Visible = false;
            divmilege.Visible = false;
            divAmount.Visible = false;
            divRemark.Visible = false;

            trAmt.Visible = false;
          //  divRate.Visible = false;
         //   divMeterRead.Visible = false;
            divRemark.Visible = false;

            divVehicle.Visible = true;
            divDriver.Visible = true;
            divDepartment.Visible = false;
            divDateofwithdrawal.Visible = false;
            divDieselRequester.Visible = false;
            divApprover.Visible = false;
            divPurposeofwithdrawal.Visible = false;

            FillDropDownHire();
            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue),0);
            clear();
            ddlIssueType.SelectedValue = "0";
            btnDirectReport.Visible = false;
            //divRemark.Visible = true;
            UplReqLetter.Visible = false;
        }
    }
    // this event is used to get the unit of the selected items.
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblUnit.Text = string.Empty;
            decimal PurlQty = 0;
            decimal IssueQty = 0;
            decimal AvlQty = 0;
            string vehicletype=string.Empty;
            if (ddlItem.SelectedIndex > 0)
            {
                DataSet ds1 = objCommon.FillDropDown("VEHICLE_ITEM_PURCHASE", "SUM(QUANTITY) as QUANTITY", "ITEM_ID", "ITEM_ID ='" + ddlItem.SelectedValue + "'  GROUP BY ITEM_ID", "ITEM_ID");
                //"SUM(QUANTITY)", "ITEM_ID", "ITEM_ID = '" + ddlItem.SelectedValue + "', ITEM_ID");
                 if (ds1.Tables[0].Rows.Count > 0)
                 {
                     PurlQty = Convert.ToDecimal(ds1.Tables[0].Rows[0]["QUANTITY"]);
                     if(rdblistVehicleType.SelectedValue=="1")
                     {
                         vehicletype = "F";
                     }
                     else if(rdblistVehicleType.SelectedValue=="2")
                     {
                         vehicletype = "I";
                     }
                     DataSet ds2 = objCommon.FillDropDown("VEHICLE_FUELENTRY", "SUM(QTY) as QTY", "ITEM_ID", "ITEM_ID ='" + ddlItem.SelectedValue + "' and VEHICLE_CATEGORY='" + vehicletype + "' GROUP BY ITEM_ID", "ITEM_ID");
                     if (ds2.Tables[0].Rows.Count > 0)
                     {
                         IssueQty = Convert.ToDecimal(ds2.Tables[0].Rows[0]["QTY"]);
                         AvlQty = PurlQty - IssueQty;
                     }
                     else
                     {
                         AvlQty = PurlQty;
                     }
                     txtAvlQty.Text = AvlQty.ToString();

                 }
                 else if (ds1.Tables[0].Rows.Count == 0)
                 {
                     txtRate.Text = string.Empty;
                     txtAvlQty.Text = string.Empty;
                     objCommon.DisplayMessage(this.Page, "Purchase Quantities Are Not Available For This Item.", this.Page);
                     //txtAvlQty.Enabled = false;
                     return;
                 }

                DataSet ds = objCommon.FillDropDown("VEHICLE_ITEMMASTER IM INNER JOIN VEHICLE_UNIT U ON(IM.UNIT = U.UNIT_ID)", "U.UNIT_NAME", "IM.RATE", "IM.ITEM_ID =" + ddlItem.SelectedValue, "");
            if(ds.Tables[0].Rows.Count >0)
              {
                lblUnit.Text = ds.Tables[0].Rows[0]["UNIT_NAME"].ToString();
                txtRate .Text =  ds.Tables[0].Rows[0]["RATE"].ToString();
                hdnRate.Value = ds.Tables[0].Rows[0]["RATE"].ToString();
                }
            }
            else
            {
                txtRate.Text = string.Empty;
                txtEndReading.Text = string.Empty;
                txtAvlQty.Text = string.Empty;
                objCommon.DisplayMessage("Please Select Item.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_fuelentry.ddlItem_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_FUELENTRY VF INNER JOIN VEHICLE_ITEMMASTER IM ON (VF.ITEM_ID = IM.ITEM_ID) INNER JOIN VEHICLE_UNIT U ON (U.UNIT_ID = IM.UNIT)", " TOP 10 IM.ITEM_NAME, CAST(VF.QTY AS NVARCHAR(255)) +' '+ U.UNIT_NAME AS QTY", "VF.FUELDATE", "VF.VIDNO=" + ddlVehicle.SelectedValue, "VF.FUELDATE");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //lvLastIssue.DataSource = ds;
                //lvLastIssue.DataBind();
                //lvLastIssue.Visible = false;
            }
            else
            {
                //lvLastIssue.DataSource = null;
                //lvLastIssue.DataBind();
                //lvLastIssue.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_fuelentry.ddlDriver_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlVehicle.SelectedIndex > 0)
        {
            ShowItemIssueDetails("Item Issue Details", "rptItemIssueDetails.rpt");
        }
        else
        {
            objCommon.DisplayMessage("Please Select Vehicle.", this.Page);
            return;
        }
        // ShowItemIssueDetails("Item Issue Details", "rptItemIssueDetails.rpt");
    }
    private void ShowItemIssueDetails(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=FuelConsumptionReport.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_VIDNO=" + ddlVehicle.SelectedValue;

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_fuelentry.ShowItemIssueDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnBalReport_Click(object sender, EventArgs e)
    {
        ShowItemStockBalance("Item Stock Balance", "rptItemBalanceStock.rpt");
    }

    private void ShowItemStockBalance(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=ItemBalanceStockReport.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_ITEM_TYPE=" + ddlItem.SelectedValue;

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_fuelentry.ShowItemStockBalance() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnDirectReport_Click(object sender, EventArgs e)
    {
        pnlFuel.Visible = false;
        divButton.Visible = false;
        pnlList.Visible = false;
        pnlReport.Visible = true;


    }

    private void ShowDirectFuelReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=DirectFuelReport.pdf";
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            url += "&param=@P_FEID=0";

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_fuelentry.ShowItemStockBalance() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShowDReport_Click(object sender, EventArgs e)
    {
        ShowDirectFuelReport("Direct Fuel Entry Report", "rptDirectFuelReport.rpt");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlFuel.Visible = true;
        divButton.Visible = true;
        //  pnlList.Visible = true;
        pnlReport.Visible = false;
    }
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        if (txtRate.Text != string.Empty && txtQty.Text != string.Empty)
        {
            decimal Amount = Convert.ToDecimal(txtRate.Text) * Convert.ToDecimal(txtQty.Text);
            txtMilegeAmount.Text = Convert.ToString(Amount);
            txtAmount.Text = Convert.ToString(Amount);
            //Double milage = (Convert.ToDouble(txtEndReading.Text) - Convert.ToDouble(txtMeterReading.Text)) / Convert.ToDouble(txtQty.Text);
            //txtMilege.Text = Convert.ToString(milage);
        }
    }
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        if (txtRate.Text != string.Empty && txtQty.Text != string.Empty)
        {
            decimal AvlQty=Convert.ToDecimal(txtAvlQty.Text);
            decimal Qty = Convert.ToDecimal(txtQty.Text);
            if (rdblistVehicleType.SelectedValue=="1")
            {
            if (AvlQty < Qty)
            {
                objCommon.DisplayMessage(this.updActivity, "Quantity Should Not Be Greater Than Available Quantity.", this.Page);
                txtQty.Text = string.Empty;
                return;
            }
            }
            decimal Amount = Convert.ToDecimal(txtRate.Text) * Convert.ToDecimal(txtQty.Text);
            txtMilegeAmount.Text = Amount.ToString("0.00");
            txtAmount.Text = Amount.ToString("0.00");
            txtTotalAmount.Text = Amount.ToString("0.00");
            txtEndReading.Text = string.Empty;
            txtNoOfKms.Text = string.Empty;
            txtMilege.Text = string.Empty;
        }

    }
    protected void ddlIssueType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIssueType.SelectedValue == "1")
        {
            clear();
            updDocument.Visible = true;
            divIssuetype.Visible = true;
            divItemName.Visible = true;
            divAvailableQty.Visible = true;
            divCouponNo.Visible = true;
            //divIssueDate.Visible = true;
            divFuelDate.Visible = true;

            divRate.Visible = true;
            div1.Visible = true;
            divstrmetread.Visible = true;
            divEndMetRead.Visible = true;
            divkms.Visible = true;
            divmilege.Visible = true;
            divAmount.Visible = false;
            divRemark.Visible = true;
            divVehicle.Visible = true;
            divDriver.Visible = true;

            divDepartment.Visible = false;
            divDateofwithdrawal.Visible = false;
            divDieselRequester.Visible = false;
            divApprover.Visible = false;
            divPurposeofwithdrawal.Visible = false;
            updDocument.Visible = true;
            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue), Convert.ToInt32(ddlIssueType.SelectedValue));
            pnlList.Visible = true;
            lvFuel.Visible = true;
            UplReqLetter.Visible = false;

        }
        else if (ddlIssueType.SelectedValue == "2")
        {
            clear();
            divIssuetype.Visible = true;
            divItemName.Visible = true;
            divDepartment.Visible = true;
           divDateofwithdrawal.Visible = true;
            divDieselRequester.Visible = true;
            divApprover.Visible = true;
            divAmount.Visible = false;
            divAvailableQty.Visible = false;
            divVehicle.Visible = false;
            divDriver.Visible = false;
            divFuelDate.Visible = false;
            divCouponNo.Visible = false;
            divRate.Visible = false;
            div1.Visible = false;
            divstrmetread.Visible = false;
            divEndMetRead.Visible = false;
            divkms.Visible = false;
            divmilege.Visible = false;
            divPurposeofwithdrawal.Visible = true;
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPTNO");
            objCommon.FillDropDownList(ddlDieselReq, "PAYROLL_EMPMAS", "IDNO", "FNAME+' '+LNAME", "", "IDNO");
            objCommon.FillDropDownList(ddlApprover, "PAYROLL_EMPMAS", "IDNO", "FNAME+' '+LNAME", "", "IDNO");
            pnlList.Visible = true;
            lvFuel.Visible = true;
            updDocument.Visible = true;
            BindList(Convert.ToInt32(rdblistVehicleType.SelectedValue) ,Convert.ToInt32(ddlIssueType.SelectedValue));
            UplReqLetter.Visible = true;
            divAvailableQty.Visible = true;
        }
        else
        {
            clear();
            divIssuetype.Visible = true;
            divItemName.Visible = true;
            divAvailableQty.Visible = true;
            divCouponNo.Visible = true;
            //divIssueDate.Visible = true;
            divFuelDate.Visible = true;

            divRate.Visible = true;
            div1.Visible = true;
            divstrmetread.Visible = true;
            divEndMetRead.Visible = true;
            divkms.Visible = true;
            divmilege.Visible = true;
            divAmount.Visible = false;
            divRemark.Visible = true;
            divVehicle.Visible = true;
            divDriver.Visible = true;

            divDepartment.Visible = false;
            divDateofwithdrawal.Visible = false;
            divDieselRequester.Visible = false;
            divApprover.Visible = false;
            divPurposeofwithdrawal.Visible = false;
            lvFuel.Visible = false;
            lvFuel.DataSource = null;
            lvFuel.DataBind();
           // updDocument.Visible = false;
          //  pnlList.Visible = false;
            UplReqLetter.Visible = false;
            //divAvailableQty.Visible = false;

        }
    }
    protected void lvFuel_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
       
        HtmlControl tdvehicle = (HtmlControl)e.Item.FindControl("tdvehicle");
        HtmlControl tddriver = (HtmlControl)e.Item.FindControl("tddriver");
        HtmlControl tdissuedate = (HtmlControl)e.Item.FindControl("tdissuedate");
        HtmlControl tduploadletter = (HtmlControl)e.Item.FindControl("tduploadletter");

        HtmlControl tddepartment = (HtmlControl)e.Item.FindControl("tddepartment");
        HtmlControl tddateofwithdrawal = (HtmlControl)e.Item.FindControl("tddateofwithdrawal");

       
        

        if (ddlIssueType.SelectedValue=="2")
        {
            tdvehicle.Visible = false;
            tddriver.Visible = false;
            tdissuedate.Visible = false;
            tduploadletter.Visible = true;
            tddateofwithdrawal.Visible = true;
            tddepartment.Visible = true;
            //foreach (ListViewItem item in lvFuel.Items)
            //{
            //    if (item.DataItem == null)
            //    {

            lvFuel.FindControl("thvehicle").Visible = false;
            lvFuel.FindControl("thdriver").Visible = false;
            lvFuel.FindControl("thissuedate").Visible = false;
            lvFuel.FindControl("thdepartment").Visible = true;
            lvFuel.FindControl("thdateofwithdrawal").Visible = true;

            lvFuel.FindControl("thuploadletter").Visible = true;
            //    }
            //}
            //thvehicle.Visible = false;
            //thdriver.Visible = false;
            //thissuedate.Visible = false;
      
        }
        if (ddlIssueType.SelectedValue == "1")
        {
           
            tduploadletter.Visible = false;
            //foreach (ListViewItem item in lvFuel.Items)
            //{
            //    if (item.DataItem == null)
            //    {
                        lvFuel.FindControl("thvehicle").Visible = true;
                        lvFuel.FindControl("thdriver").Visible = true;
                        lvFuel.FindControl("thissuedate").Visible = true;
                        lvFuel.FindControl("thuploadletter").Visible = false;
                        lvFuel.FindControl("thdepartment").Visible = false;
                        lvFuel.FindControl("thdateofwithdrawal").Visible = false;


                        
            //    }
            //}
        }

        if (rdblistVehicleType.SelectedValue=="2")
        {
            tduploadletter.Visible = false;
            tdvehicle.Visible = true;
            tddriver.Visible = true;
            tdissuedate.Visible = true;

            lvFuel.FindControl("thvehicle").Visible = true;
            lvFuel.FindControl("thdriver").Visible = true;
            lvFuel.FindControl("thissuedate").Visible = true;
            lvFuel.FindControl("thdepartment").Visible = false;
            lvFuel.FindControl("thdateofwithdrawal").Visible = false;
            
            lvFuel.FindControl("thuploadletter").Visible = false;
        }
    }
    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        string path1 = string.Empty;
        string ImageName1 = string.Empty;
        try
        {
            //string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            //string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEmployee"].ToString();
            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/VEHICLE_MAINTENANCE\\UploadFiles" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            // string img = Convert.ToString(objCommon.LookUp("VEHICLE_BUS_STRUCTURE_IMAGE_DATA", "FILE_PATH", "ROUTEID='" + routeid + "' and BUSSTR_ID='" + seating + "'"));
            var ImageName = img;
            if (img == null || img == "")
            {
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a target = \"_blank\"  href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";

            }
            else
            {
                if (img != "")
                {
                    DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                    var blob = blobContainer.GetBlockBlobReference(ImageName);


                    string filePath = directoryPath + "\\" + ImageName;

                    foreach (DataRow row in dtBlobPic.Rows)
                    {
                         path1 = row["Uri"].ToString();
                         ImageName1 = row["Name"].ToString();
                    }

                    //blobUrl + "/" + storageContainer + "/" + fileName;
                   // string bloburl="https://msgcloudstorage.blob.core.windows.net";
                   // string url =bloburl + "/" + blob_ContainerName + "/" + ImageName1;
                    string url = path1;
                    string Script = string.Empty;
                    Script += " window.open('" + url + "','" + ImageName1 + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Report", Script, true);

                    //window.open('http://www.stackoverflow.com');
                     //https://msgcloudstorage.blob.core.windows.net/storageContainer/fileName
                
                   // FileStream sourceFile = new FileStream((path1), FileMode.OpenOrCreate); //+ "\\" + ImageName1
                    //FileStream sourceFile = new FileStream(path1, FileMode.Open);
                    //long fileSize = sourceFile.Length;
                    //byte[] getContent = new byte[(int)fileSize];
                    //sourceFile.Read(getContent, 0, (int)sourceFile.Length);
                    //sourceFile.Close();

                    //Response.Clear();
                    //Response.BinaryWrite(getContent);
                    //Response.ContentType = GetResponseType(filePath.Substring(filePath.IndexOf('.')));

                    //Response.AddHeader("Content-Disposition", "attachment; filename=\"" + ImageName1 + "\"");
                    //HttpContext.Current.Response.Flush();
                    //HttpContext.Current.Response.SuppressContent = true;
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();


                      
                }

            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case ".jpg":
                return "application/{0}";
                break;
            case ".jpeg":
                return "application/{0}";
                break;
            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }
}
