using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using IITMS;
using IITMS.UAIMS;
using System.Data.Linq;
using System.Collections.Generic;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using System.Data;

public partial class VEHICLE_MAINTENANCE_Transaction_BusStructureMapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();
    BlobController objBlob = new BlobController();
    string routeCode = string.Empty;
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();
    decimal File_size;

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
                    objCommon.FillDropDownList(ddlRoute, "VEHICLE_ROUTEMASTER", "ROUTEID", "ROUTENAME", "", "");
                    objCommon.FillDropDownList(ddlbusStructure, "VEHICLE_BUS_STRUCTURE_MASTER", "BUSSTR_ID", "SEATING_STRUCTURE", "", "");
                    objCommon.FillDropDownList(ddlStatus, "VEHICLE_MAPPING_STATUS_MASTER", "STATUS_ID", "MAPPING_STATUS", "", "");
                    //BindListView();
                    BindListView();
                    BlobDetails();

                    ViewState["checkcond"] = null;
       
                }
            }
           
            ViewState["checkcond"] = null;
            BlobDetails();


        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Bus_Structure_Mapping.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


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

    public void DataTable(int seating, int routeid)
    {
        //----------------Data Table-----------------//

        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
        dt.Columns.Add(new DataColumn("STATUS_ID", typeof(string)));
        

        DataRow dr1 = null;
        int count = 0;
        if (seating==1)
        {
        for (int i = 1; i < 51;i++ )
        {

            //System.Web.UI.WebControls.Label lblcount = (System.Web.UI.WebControls.Label)FindControl("lblcount");
            string status = Convert.ToString(objCommon.LookUp("[dbo].[VEHICLE_STRUCTURE_MAPPING] A inner join [dbo].[VEHICLE_MAPPING_STATUS_MASTER] B ON (A.STATUS_ID=B.STATUS_ID)", "Isnull(MAPPING_STATUS,0)", "BUSSTR_ID='" + seating + "' and SEATS='" + i + "' and ROUTEID='" + routeid + "'"));

            dr1 = dt.NewRow();
            dr1["SrNo"] = i ;

            if (status == "")
            {
                dr1["STATUS_ID"] = DBNull.Value;
            }
            else
            {
                dr1["STATUS_ID"] = status;
            }
            //dr1["STATUS"] = status;
            dt.Rows.Add(dr1);

        }
        }
        else if (seating == 2)
        {
            for (int i = 1; i < 41; i++)
            {

                //System.Web.UI.WebControls.Label lblcount = (System.Web.UI.WebControls.Label)FindControl("lblcount");
                string status = Convert.ToString(objCommon.LookUp("[dbo].[VEHICLE_STRUCTURE_MAPPING] A inner join [dbo].[VEHICLE_MAPPING_STATUS_MASTER] B ON (A.STATUS_ID=B.STATUS_ID)", "Isnull(MAPPING_STATUS,0)", "BUSSTR_ID='" + seating + "' and SEATS='" + i + "' and ROUTEID='" + routeid + "'"));

                dr1 = dt.NewRow();
                dr1["SrNo"] = i;

                if (status == "")
                {
                    dr1["STATUS_ID"] = DBNull.Value;
                }
                else
                {
                    dr1["STATUS_ID"] = status;
                }

                dt.Rows.Add(dr1);

            }
        }
        else
        {
            lvstructure.DataSource = null;
            lvstructure.DataBind();
        }
        objVM.Structure_Count_TBL = dt;

        lvstructure.DataSource = dt;
        lvstructure.DataBind();
        //-------------------------------------------//
    }
   
    protected void btnShowBusStr_Click(object sender, EventArgs e)
    {
        int seating =Convert.ToInt32(ddlbusStructure.SelectedValue);
        int routeid = Convert.ToInt32(ddlRoute.SelectedValue);
        if (ddlRoute.SelectedValue=="0")
       {
           objCommon.DisplayMessage(this.updActivity, "Please Select Route.", this.Page);
           return;
       }
        if (ddlbusStructure.SelectedValue=="0")
        {
            objCommon.DisplayMessage(this.updActivity, "Please Select Bus Structure.", this.Page);
            return;
        }
        DataTable(seating, routeid);
        divStatus.Visible = true;
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

    protected void btnUploadStructure_Click(object sender, EventArgs e)
    {
        if (ddlRoute.SelectedValue=="0")
        {
            objCommon.DisplayMessage(this.updActivity, "Please Select Route.", this.Page);
            return;
        }
        if (ddlbusStructure.SelectedValue=="0")
        {
            objCommon.DisplayMessage(this.updActivity, "Please Select Bus Structure.", this.Page);
            return;
        }
        if (fuUploadBusStructure.HasFile == false)
        {
            objCommon.DisplayMessage(this.updActivity, "Please Select Upload Bus Structure File.", this.Page);
            return;
        }
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
        objVM.Route =Convert.ToInt32( ddlRoute.SelectedValue);
        objVM.BusStructure = Convert.ToInt32(ddlbusStructure.SelectedValue);


        bool result1;
        string FilePath = string.Empty;

        if (fuUploadBusStructure.HasFile == true)
        {
            if (FileTypeValid(System.IO.Path.GetExtension(fuUploadBusStructure.FileName)))
            {

            }
            else
            {

                objCommon.DisplayMessage(updActivity, "Please Upload Valid Image[.jpg,.JPG,.jpeg,.JPEG,.png,.PNG]", this.Page);
                fuUploadBusStructure.Focus();
                return;
            }

            string fileName = System.Guid.NewGuid().ToString() + fuUploadBusStructure.FileName.Substring(fuUploadBusStructure.FileName.IndexOf('.'));
            string fileExtention = System.IO.Path.GetExtension(fileName);
            string ext = System.IO.Path.GetExtension(fuUploadBusStructure.PostedFile.FileName);

            int sub_no = Convert.ToInt32(objCommon.LookUp("VEHICLE_BUS_STRUCTURE_IMAGE_DATA", "(ISNULL(MAX(STIMG_ID),0))+1 AS STIMG_ID", ""));

            filename = sub_no + "_BusStructure_" + sub_no;

            objVM.UploadBlobName = sub_no + "_BusStructure_" + sub_no + ext;



            if (fuUploadBusStructure.HasFile.ToString() != "")
            {

                objVM.UploadBusStructure = fuUploadBusStructure.FileName;

                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                result1 = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                if (result1 == true)
                {

                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, filename, fuUploadBusStructure);
                    if (retval == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                        return;
                    }

                    objVM.UploadBusStructure = fuUploadBusStructure.FileName;


                }
                else
                {
                    objVM.UploadBusStructure = fuUploadBusStructure.FileName;

                }
                CustomStatus cs = (CustomStatus)objVMC.AddBusStructureImage(objVM);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    // Clear();
                    objCommon.DisplayMessage(this.updActivity, "Record Save Successfully.", this.Page);
                    return;
                }


                //fileUploader.SaveAs(Server.MapPath("") + "\\UPLOAD_FILES\\" + fileName);
            }
            else
            {
                objCommon.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);

                return;

            }


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

    public void DataTable1()
    {
        //----------------Data Table-----------------//

        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ROUTE", typeof(int)));
        dt.Columns.Add(new DataColumn("BUSSTRUCTURE", typeof(int)));
        dt.Columns.Add(new DataColumn("SEATS", typeof(int)));
        dt.Columns.Add(new DataColumn("STATUS", typeof(int)));
        

        DataRow dr1 = null;
        foreach (ListViewItem ii in lvstructure.Items)
        {

            System.Web.UI.WebControls.CheckBox chkSeat = (System.Web.UI.WebControls.CheckBox)ii.FindControl("chkSeat");

            System.Web.UI.WebControls.Label lblcount = (System.Web.UI.WebControls.Label)ii.FindControl("lblcount");
            int count = 0;
            dr1 = dt.NewRow();
            if (chkSeat.Checked==true)
            {
                dr1["SRNO"] = count+1;
                dr1["ROUTE"] = Convert.ToInt32(ddlRoute.SelectedValue);
                dr1["BUSSTRUCTURE"] = Convert.ToInt32(ddlbusStructure.SelectedValue);

                dr1["SEATS"] = lblcount.Text;
                dr1["STATUS"] = Convert.ToInt32(ddlStatus.SelectedValue);
               

            dt.Rows.Add(dr1);
            ViewState["checkcond"] = dt;
            }

        }
        objVM.Bus_Structure_Data_TBL = dt;
       // objLM.DEAD_STOCK_ITEM_TBL = dt;


        //-------------------------------------------//
    }

    public void Clear()
    {
        ddlRoute.SelectedValue = "0";
        ddlbusStructure.SelectedValue = "0";
        ddlStatus.SelectedValue = "0";

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlRoute.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updActivity, "Please Select Route.", this.Page);
                return;
            }
            if (ddlbusStructure.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updActivity, "Please Select Bus Structure.", this.Page);
                return;
            }
            if (ddlStatus.SelectedValue=="0")
            {
                objCommon.DisplayMessage(this.updActivity, "Please Select Status.", this.Page);
                return;
            }
            DataTable1();
            if (ViewState["checkcond"]==null)
            {
                objCommon.DisplayMessage(this.updActivity, "Please Check Any Seat.", this.Page);
                return;
            }
            int Org =0;
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objVMC.AddUpdateBusStructure(objVM, Org);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {

                        // Clear();
                        //lvRoutePath.Visible = true;
                        objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                       // BindlistView();
                        int seating = Convert.ToInt32(ddlbusStructure.SelectedValue);
                        int routeid = Convert.ToInt32(ddlRoute.SelectedValue);
                        DataTable(seating, routeid);
                        BindListView();
                        ViewState["action"] = "add";
                       Clear();
                       lvstructure.DataSource = null;
                       lvstructure.DataBind();
                       divStatus.Visible = false;
                        objCommon.DisplayMessage(this.updActivity, "Record Save Successfully.", this.Page);
                    }
                    if (cs.Equals(CustomStatus.RecordUpdated))
                     {
                         int seating = Convert.ToInt32(ddlbusStructure.SelectedValue);
                         int routeid = Convert.ToInt32(ddlRoute.SelectedValue);
                         DataTable(seating, routeid);
                         BindListView();
                         ViewState["action"] = "add";
                         lvstructure.DataSource = null;
                         lvstructure.DataBind();
                         divStatus.Visible = false;
                         objCommon.DisplayMessage(this.updActivity, "Record Update Successfully.", this.Page);
                         Clear();
                     }
                }
                //else
                //{
                //    if (ViewState["ROUTENO"] != null)
                //    {
                //        objVM.ROUTENO = Convert.ToInt32(ViewState["ROUTENO"].ToString());
                //        CustomStatus cs = (CustomStatus)objVMC.AddUpdateRouteMaster(objVM);
                //        if (cs.Equals(CustomStatus.RecordExist))
                //        {
                //           // lvRoutePath.Visible = true;
                //            objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                //            return;
                //        }
                //        if (cs.Equals(CustomStatus.RecordSaved))
                //        {
                //           // BindlistView();
                //            ViewState["action"] = "add";

                //            objCommon.DisplayMessage(this.updActivity, "Record Update Successfully.", this.Page);
                //           // Clear();
                //        }

                //    }
                //}
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Master_RouteMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void BindListView()
    {

        DataSet ds = objVMC.BindBusStructureMapping();
        lvBusStructure.DataSource = ds;
        lvBusStructure.DataBind();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        lvstructure.DataSource = null;
        lvstructure.DataBind();
        divStatus.Visible = false;
    }
    protected void btnPaymentReport_Click(object sender, EventArgs e)
    {
        DataSet ds = objVMC.GetOnlinePaymentReportForExcel();
        GridView gvStudData = new GridView();
        gvStudData.DataSource = ds;
        gvStudData.DataBind();
        string FinalHead = @"<style>.FinalHead { font-weight:bold;  }</style>";
        string attachment = "attachment; filename=PaymentReport.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.MS-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Response.Write(FinalHead);
        gvStudData.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
}