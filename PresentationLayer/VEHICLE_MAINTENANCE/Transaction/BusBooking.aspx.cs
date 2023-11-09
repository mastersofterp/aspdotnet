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
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IITMS.NITPRM;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;




public partial class VEHICLE_MAINTENANCE_Transaction_BusBooking : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();
    BlobController objBlob = new BlobController();
    FeeCollectionController feeController = new FeeCollectionController();
    string routeCode = string.Empty;
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();
    decimal File_size;
    private Stopwatch stopwatch;
    private System.Threading.Timer timer;
    private DateTime startTime;

    //The number of Columns to be generated
    const int colsCount = 5;    //You can changed the value of 8 based on you requirements

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
                    objCommon.FillDropDownList(ddlRoute, "VEHICLE_ROUTEMASTER", "ROUTEID", "ROUTENAME +' ['+ROUTE_NUMBER+']'", "", "");
                    objCommon.FillDropDownList(ddlSession, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "IS_CURRENT_FY=1 and ACTIVE_STATUS=1", "");

                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "", "");
                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER A Inner Join ACD_COLLEGE_MASTER B ON(A.COLLEGE_ID=B.COLLEGE_ID)", "SESSIONNO", "SESSION_PNAME+' '+COLLEGE_NAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND A.OrganizationId='" + Convert.ToInt32(Session["OrgId"]) + "'", "SESSIONNO DESC");
                    
                    objCommon.FillDropDownList(ddlbSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "");
                    objCommon.FillDropDownList(ddlStructure, "VEHICLE_BUS_STRUCTURE_MASTER", "BUSSTR_ID", "SEATING_STRUCTURE", "", "");
                    //BindListView();
                    getDetails();
                     int user_idno =Convert.ToInt32(objCommon.LookUp("user_acc", "Isnull(UA_IDNO, 0)", "UA_NO= '" + Session["userno"] + "'"));
                    checkuser(user_idno);

                }
            }


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

    public void checkuser(int user_no)
    {
        
        //DataSet ds = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS", "IDNO", "BID", "IDNO= '" + Convert.ToInt32(user_no) + "'", "");
        DataSet ds = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS", "SUM(FEES) FEES,IDNO", "ROUTEID,STOPID", "IDNO= '" + Convert.ToInt32(user_no) + "' and SESSION=(select ACADEMIC_YEAR_ID from ACD_ACADEMIC_YEAR where ACTIVE_STATUS=1 and IS_CURRENT_FY=1) group by IDNO,ROUTEID,STOPID ", "");
      if (ds.Tables[0].Rows.Count > 0)
        {
            int Routefees = 0;
            int bookedfees = 0;
            bookedfees = Convert.ToInt32(ds.Tables[0].Rows[0]["FEES"]);
            //getroutefees(Convert.ToInt32(ddlStop.SelectedValue), Convert.ToInt32(ddlRoute.SelectedValue));
           // getroutefees(Convert.ToInt32(ds.Tables[0].Rows[0]["STOPID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[0]["ROUTEID"].ToString()));
            DataSet dsbookedfees = objVMC.GetRouteFees(Convert.ToInt32(ds.Tables[0].Rows[0]["STOPID"].ToString()), Convert.ToInt32(ds.Tables[0].Rows[0]["ROUTEID"].ToString()));
            if (dsbookedfees != null)
          {
              btnReport.Enabled = true;
              devreceiptcode.Visible = true;
              objCommon.FillDropDownList(ddlReceipt, "VEHICLE_BUS_BOOKING_DETAILS", "ORDER_ID", "CONCAT('Slip No.', CAST(ROW_NUMBER() OVER (ORDER BY CAST(BID AS NVARCHAR(MAX))) AS NVARCHAR(MAX)))+ '- '+ ORDER_ID AS SlipNumber", "IDNO=(select UA_IDNO from user_acc where UA_NO ='" + Session["userno"] + "' AND UA_TYPE=2)", "");
             // string OrderNo = (objCommon.LookUp("VEHICLE_BUS_BOOKING_DETAILS", "ORDER_ID", "IDNO=(select UA_IDNO from user_acc where UA_NO ='" + Session["userno"] + "' AND UA_TYPE=2)"));
              if (dsbookedfees.Tables[0].Rows.Count > 0)
              {
                  Routefees = Convert.ToInt32(dsbookedfees.Tables[0].Rows[0]["ROUTE_FEES"].ToString());
              }
          }
            if (bookedfees == Routefees)
          {
            btnSubmit.Enabled = false;
            
            DataSet dsBookingDetails = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS A Inner Join VEHICLE_ROUTEMASTER B ON (A.ROUTEID=B.ROUTEID) Inner Join VEHICLE_STOPMASTER C ON (A.STOPID=C.STOPID)", "B.ROUTENAME +' '+ROUTE_NUMBER as RouteName,C.STOPNAME", "A.SEAT_NO,A.ROUTEID,A.STOPID,A.FEES", "IDNO= '" + Convert.ToInt32(user_no) + "'", "");
            string RouteName = dsBookingDetails.Tables[0].Rows[0]["RouteName"].ToString();
            string StopName = dsBookingDetails.Tables[0].Rows[0]["STOPNAME"].ToString();
            int SeatNo = Convert.ToInt32(dsBookingDetails.Tables[0].Rows[0]["SEAT_NO"]);
           // objCommon.DisplayMessage(this.Page, "Route='" + RouteName + "' Stope='" + StopName + "' Seat='" + SeatNo + "'", this.Page);
           // string BookingStatus = "Route='" + RouteName + "' Stope='" + StopName + "' Seat='" + SeatNo + "'";
          //  objCommon.DisplayMessage(this.updActivity, BookingStatus , this.Page);
            int route = Convert.ToInt32(dsBookingDetails.Tables[0].Rows[0]["ROUTEID"].ToString());
            DateTime StopTime = Convert.ToDateTime(objCommon.LookUp("vehicle_routemaster", "Cast(STARTING_TIME as time) STARTING_TIME", "ROUTEID='" + Convert.ToInt32(route) + "'"));
            // DateTime sttime = Convert.ToDateTime(StopTime.tostring("hh:mm tt"));
            txtStopStarttime.Text = StopTime.ToString("hh:mm tt");

            string BookingStatus = "Route=" + ' ' + " " + RouteName + " ,Stop=" + ' ' + " " + StopName + " ,Seat=" + ' ' + " " + SeatNo;
            objCommon.DisplayMessage(this.Page, BookingStatus, this.Page);
          }
            if (bookedfees < Routefees)
        {

            DataSet dsBookingDetails = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS A Inner Join VEHICLE_ROUTEMASTER B ON (A.ROUTEID=B.ROUTEID) Inner Join VEHICLE_STOPMASTER C ON (A.STOPID=C.STOPID)", "B.ROUTENAME +' '+ROUTE_NUMBER as RouteName,C.STOPNAME", "A.SEAT_NO,A.ROUTEID,A.STOPID,A.FEES", "IDNO= '" + Convert.ToInt32(user_no) + "'", "");
            string RouteName = dsBookingDetails.Tables[0].Rows[0]["RouteName"].ToString();
            string StopName = dsBookingDetails.Tables[0].Rows[0]["STOPNAME"].ToString();
            int SeatNo = Convert.ToInt32(dsBookingDetails.Tables[0].Rows[0]["SEAT_NO"]);
            // objCommon.DisplayMessage(this.Page, "Route='" + RouteName + "' Stope='" + StopName + "' Seat='" + SeatNo + "'", this.Page);
            // string BookingStatus = "Route='" + RouteName + "' Stope='" + StopName + "' Seat='" + SeatNo + "'";
            //  objCommon.DisplayMessage(this.updActivity, BookingStatus , this.Page);
            int balancefees = Routefees - bookedfees;
            ddlRoute.SelectedValue = dsBookingDetails.Tables[0].Rows[0]["ROUTEID"].ToString();
            ddlRoute.Enabled = false;
            ddlRoute_SelectedIndexChanged(null, null);
            ddlStop.SelectedValue = dsBookingDetails.Tables[0].Rows[0]["STOPID"].ToString();
            ddlStop.Enabled = false;
            lblfees.Text = balancefees.ToString();
            div1.Visible = true;
            btnShowStrbtnShowStr.Enabled = false;
            txtBusSeate.Text = dsBookingDetails.Tables[0].Rows[0]["SEAT_NO"].ToString();
            lblPfees.Text = bookedfees.ToString(); //dsBookingDetails.Tables[0].Rows[0]["FEES"].ToString();
            lblTfees.Text = Routefees.ToString();
            divSeats.Visible = true;
            int route =Convert.ToInt32( dsBookingDetails.Tables[0].Rows[0]["ROUTEID"].ToString());
            DateTime StopTime = Convert.ToDateTime(objCommon.LookUp("vehicle_routemaster", "Cast(STARTING_TIME as time) STARTING_TIME", "ROUTEID='" + Convert.ToInt32(route) + "'"));
            // DateTime sttime = Convert.ToDateTime(StopTime.tostring("hh:mm tt"));
            txtStopStarttime.Text = StopTime.ToString("hh:mm tt");
            div5.Visible = true;
            string BookingStatus = "Route=" + ' ' + " " + RouteName + " ,Stop=" + ' ' + " " + StopName + " ,Seat=" + ' ' + " " + SeatNo + ",                     Balance Amount=" + ' ' + " " + balancefees;
            objCommon.DisplayMessage(this.Page, BookingStatus , this.Page);
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

    public void getDetails()
    {
        int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'"));
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "STUDNAME,SEMESTERNO,ACADEMIC_YEAR_ID,SECTIONNO,SCHEMENO", "IDNO", "IDNO = '" + IDNO + "'", "");

        DataSet ds1 = objCommon.FillDropDown("ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "IS_CURRENT_FY=1 and ACTIVE_STATUS=1", "");

        ddlSession.SelectedValue = ds1.Tables[0].Rows[0]["ACADEMIC_YEAR_ID"].ToString();
        ddlbSemester.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
        txtname.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
    }

    protected void btnShowStr_Click(object sender, EventArgs e)
    {
       // divimg.Visible = true;
        lblrout.Text = ddlRoute.SelectedItem.Text;
        int seating = Convert.ToInt32(ddlStructure.SelectedValue);
        int routeid = Convert.ToInt32(ddlRoute.SelectedValue);
        if (ddlRoute.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updActivity, "Please Select Route.", this.Page);
            divimg.Visible = false;
            return;
        }
        //if (ddlStructure.SelectedValue == "0")
        //{
        //    objCommon.DisplayMessage(this.updActivity, "Please Select Bus Structure.", this.Page);
        //    divimg.Visible = false;
        //    return;
        //}
        string Url = string.Empty;
        string directoryPath = string.Empty;
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
            // string img = ((System.Web.UI.WebControls.LinkButton)(sender)).ToolTip.ToString();
            string img = Convert.ToString(objCommon.LookUp("VEHICLE_BUS_STRUCTURE_IMAGE_DATA", "FILE_PATH", "ROUTEID='" + routeid + "'")); //"' and BUSSTR_ID='" + seating +
            var ImageName = img;

            if (img != "")
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                if (dtBlobPic.Rows.Count>0)
                {
                var blob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;

                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                divimg.Visible = true;
                lblImage.ImageUrl = string.Format(ResolveUrl("~//VEHICLE_MAINTENANCE\\UploadFiles/" + ImageName));
                }
            }
            else
            {
                divimg.Visible = false;
                objCommon.DisplayMessage(this.updActivity, "Selected Route Bus Structure Is Not Available.", this.Page);
                return;
            }


            //-----------start--20-04-2023----dynamic grid------

            //ShowBusStructure();

            //-------------------------------------------------------------
          //  lvstructure.Visible = true;

            //---------

            ///  DataSet ds = objCM.BindBorrowerData(ObjEM);
            ///  
            
            getbus();
          
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    //--------start----------20-04-2023---

    //public void ShowBusStructure()
    //{
    //    try
    //    {
    //        DataSet dsBusBooking = null;

    //        int Hostel_session = Convert.ToInt32(Session["hostel_session"]);

    //        //if (Session["usertype"].ToString().Equals("2"))
    //        //{
    //            DataSet ds = new DataSet();
               
    //        dsBusBooking = objVMC.GetBusAvailabilityStatus(Convert.ToInt32(ddlRoute.SelectedValue));


    //            //dsBusBooking = objVMC.GetBusAvailabilityStatus(Convert.ToInt32(ddlHostel.SelectedValue), Hostel_session, DegreeNo, SemesterNo, Convert.ToInt32(ddlBlock.SelectedValue));

    //            ///  OrganizationId=" + Convert.ToInt32(Session["OrgId"])

    //        //}
    //        //else
    //        //{
    //        //    if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
    //        //    {
    //        //        dsBusBooking = raController.GetRoomAvailabilityStatusAdminCpuK(Convert.ToInt32(ddlHostel.SelectedValue), Hostel_session, Convert.ToInt32(ddlDeg.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlBlock.SelectedValue));
    //        //    }
    //        //    else
    //        //    {
    //        //        dsBusBooking = raController.GetRoomAvailabilityStatusAdmin(Convert.ToInt32(ddlHostel.SelectedValue), Hostel_session, Convert.ToInt32(ddlDeg.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlBlock.SelectedValue));
    //        //    }
    //        //}


    //        if (dsBusBooking != null && dsBusBooking.Tables[0].Rows.Count > 0)
    //        {
    //            if (dsBusBooking.Tables[0].Rows.Count > 0)
    //            {
    //                ////The number of Columns to be generated
    //                //const int colsCount = 8;    //You can changed the value of 3 based on you requirements
    //                double rows = Convert.ToDouble(dsBusBooking.Tables[0].Rows.Count) / Convert.ToDouble(colsCount);

    //                //Store the Total Rows Count in ViewState
    //                //Dataset rows divide by no. of columns in dymanic table is Total No of rows in Dynamic table
    //                if (ddlBlock.SelectedIndex > 0)
    //                {
    //                    ViewState["RowsCount"] = Math.Ceiling(rows) + 1;
    //                }
    //                else
    //                {
    //                    ViewState["RowsCount"] = Math.Ceiling(rows) + Convert.ToInt32(ddlBlock.Items.Count - 1);
    //                }

    //                ViewState["TableRoomStatus"] = dsBusBooking.Tables[0];
    //                // COMMENTED BY SONALI ON 30/11/2022 AS ROOM TYPE MASTER IS INTEGRATED IN ALL UAT

    //                //if (Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 2 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 3 || Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) == 4)
    //                //{
    //                GenerateTable_Kota(Convert.ToInt32(ViewState["RowsCount"]), colsCount, dsBusBooking.Tables[0]);
    //                //}
    //                //else
    //                //{
    //                //    GenerateTable(Convert.ToInt32(ViewState["RowsCount"]), colsCount, dsRooms.Tables[0]);
    //                //}

    //                pnlBusBookingTable.Visible = true;
    //            }
    //            dsBusBooking.Dispose();
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage("Rooms Not Found..!!", this.Page);
    //            pnlBusBookingTable.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    //---------end----------20-04-2023


    protected void getbus()
    {
        DataSet ds = objVMC.BindBusStructureDynamicGrid(Convert.ToInt32(ddlRoute.SelectedValue));
        //DataTable dt = new DataTable();
        //ViewState["DataSetValue"] = ds;
        //dt = (DataTable)ViewState["DataSetValue"];
        if (ds.Tables[0].Rows.Count == 1)
        {
        if(ds.Tables[0].Rows[0]["Msg"].ToString()!="")
        {

            objCommon.DisplayMessage(this.Page, ds.Tables[0].Rows[0]["Msg"].ToString(), this.Page);
            return;
        }
        }

        updDocument.Visible = true;
        structurediscription.Visible = true;
        seatestatus.Visible = true;
        divroutepath.Visible = true;
        string RoutePath = (objCommon.LookUp("VEHICLE_ROUTEMASTER", "ROUTEPATH", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'"));
        lblRoutPath.Text = RoutePath;
        //int maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["COL4"]));

        //ViewState["structurerowcount"] = ds.Tables[0].Columns.Count;
      
        //if(ds.Tables[0].Columns.Count==5)
        //{
        int Count = Convert.ToInt32(objCommon.LookUp("VEHICLE_STRUCTURE_MAPPING", "COUNT(*)", "ROUTEID='" +Convert.ToInt32(ddlRoute.SelectedValue) + "'"));
        if (Count == 50)
        {
        lvStructure.DataSource = ds;
        lvStructure.DataBind();
        }
        else if (Count == 40)
        {
            lvStructure.DataSource = ds;
            lvStructure.DataBind();
        }
        else if (Count == 12)
        {
            lvStructure.DataSource = ds;
            lvStructure.DataBind();
        }

        //}
        //else if(ds.Tables[0].Columns.Count==4)
        //{
        //    lvStructure.DataSource = ds;
        //    lvStructure.DataBind();
        //}


        // Find User is mail and femail and show seats acording to gender
        int status = 0;
        int SEAT_NO = 0;
        string idno = Convert.ToString(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'"));
        string Sex = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "SEX", "IDNO='" + idno + "'"));
        if (Sex == "M")
        {
            status = 2;
        }
        else if (Sex == "F")
        {
            status = 3;
        }

        // Get Booked Seats and dicable this seats and change colour
      //  DataSet seats = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS", "SEAT_NO", "BID", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
        // Get Booked Seats and dicable this seats and change colour
        //DataSet bookedseats = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS", "SEAT_NO", "BID", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
        // for (int j = 0; j < bookedseats.Tables[0].Rows.Count; j++)
        // {
        // SEAT_NO = Convert.ToInt32(bookedseats.Tables[0].Rows[j]["SEAT_NO"].ToString());



        for (int i = 0; i < lvStructure.Items.Count; i++)
        {
             
                  int seate1 = Convert.ToInt32(ds.Tables[0].Rows[i]["COL1"].ToString());
                  HtmlControl tdcol1 = lvStructure.Items[i].FindControl("tdcol1") as HtmlControl;
                  ImageButton imgbtncol1 = lvStructure.Items[i].FindControl("byncol1") as ImageButton;



                  DataSet ds1 = objCommon.FillDropDown("VEHICLE_STRUCTURE_MAPPING", "STATUS_ID", "SEATS", "ROUTEID = '" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEATS='" + seate1 + "' ", "");
                  int STATUS_ID = Convert.ToInt32(ds1.Tables[0].Rows[0]["STATUS_ID"].ToString());

                  if (STATUS_ID == status)
                  {
                      imgbtncol1.Enabled = true;
                  }
                  else
                  {
                      imgbtncol1.Enabled = false;
                  }
                  if (STATUS_ID == 1)
                  {
                      tdcol1.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#8b8585");
                  }
                  else if (STATUS_ID == 2)
                  {
                      tdcol1.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#78b5ff");
                  }
                  else if (STATUS_ID == 3)
                  {
                      tdcol1.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#EB9999");
                  }
                  else if (STATUS_ID == 4)
                  {
                      tdcol1.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#ffff00");
                  }

                  // Get Booked Seats and dicable this seats and change colour
                  DataSet bookedseats = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS", "SEAT_NO", "BID", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEAT_NO='" + seate1 + "'", "");

                  //SEAT_NO = Convert.ToInt32(bookedseats.Tables[0].Rows[i]["SEAT_NO"].ToString());
                  //if (seate1 == SEAT_NO)
                  //{
                  if (bookedseats.Tables[0].Rows.Count > 0)
                  {
                      imgbtncol1.Enabled = false;
                      imgbtncol1.ImageUrl = "~/Images/booked_seat_img.png";
                  }

              

        }
        // }
        // DataSet bookedseats1 = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS", "SEAT_NO", "BID", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
         //for (int j = 0; j < bookedseats.Tables[0].Rows.Count; j++)
         //{
         //    SEAT_NO = Convert.ToInt32(bookedseats.Tables[0].Rows[j]["SEAT_NO"].ToString());
        for (int i = 0; i < lvStructure.Items.Count; i++)
        {
              int seate1 = Convert.ToInt32(ds.Tables[0].Rows[i]["COL2"].ToString());
                HtmlControl tdcol2 = lvStructure.Items[i].FindControl("tdcol2") as HtmlControl;
                ImageButton imgbtncol2 = lvStructure.Items[i].FindControl("byncol2") as ImageButton;




                DataSet ds1 = objCommon.FillDropDown("VEHICLE_STRUCTURE_MAPPING", "STATUS_ID", "SEATS", "ROUTEID = '" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEATS='" + seate1 + "' ", "");
                int STATUS_ID = Convert.ToInt32(ds1.Tables[0].Rows[0]["STATUS_ID"].ToString());
                if (STATUS_ID == status)
                {
                    imgbtncol2.Enabled = true;
                }
                else
                {
                    imgbtncol2.Enabled = false;
                }
                if (STATUS_ID == 1)
                {
                    tdcol2.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#8b8585");
                }
                else if (STATUS_ID == 2)
                {
                    tdcol2.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#78b5ff");
                }
                else if (STATUS_ID == 3)
                {
                    tdcol2.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#EB9999");
                }
                else if (STATUS_ID == 4)
                {
                    tdcol2.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#ffff00");
                }
                // Get Booked Seats and dicable this seats and change colour
                DataSet bookedseats = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS", "SEAT_NO", "BID", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEAT_NO='" + seate1 + "'", "");

                //SEAT_NO = Convert.ToInt32(bookedseats.Tables[0].Rows[i]["SEAT_NO"].ToString());
                //if (seate1 == SEAT_NO)
                //{
                if (bookedseats.Tables[0].Rows.Count > 0)
                {
                    imgbtncol2.Enabled = false;
                    imgbtncol2.ImageUrl = "~/Images/booked_seat_img.png";
                }
            
        }
       //  }

         //DataSet bookedseats2 = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS", "SEAT_NO", "BID", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
         //for (int j = 0; j < bookedseats.Tables[0].Rows.Count; j++)
         //{
         //    SEAT_NO = Convert.ToInt32(bookedseats.Tables[0].Rows[j]["SEAT_NO"].ToString());
        for (int i = 0; i < lvStructure.Items.Count; i++)
        {
           
            int seate1 = Convert.ToInt32(ds.Tables[0].Rows[i]["COL3"].ToString());
            ImageButton imgbtncol3 = lvStructure.Items[i].FindControl("byncol3") as ImageButton;

            DataSet ds1 = objCommon.FillDropDown("VEHICLE_STRUCTURE_MAPPING", "STATUS_ID", "SEATS", "ROUTEID = '" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEATS='" + seate1 + "' ", "");
            int STATUS_ID = Convert.ToInt32(ds1.Tables[0].Rows[0]["STATUS_ID"].ToString());
            HtmlControl tdcol3 = lvStructure.Items[i].FindControl("tdcol3") as HtmlControl;
            if (STATUS_ID == status)
            {
                imgbtncol3.Enabled = true;
            }
            else
            {
                imgbtncol3.Enabled = false;
            }
            if (STATUS_ID == 1)
            {
                tdcol3.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#8b8585");
            }
            else if (STATUS_ID == 2)
            {
                tdcol3.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#78b5ff");
            }
            else if (STATUS_ID == 3)
            {
                tdcol3.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#EB9999");
            }
            else if (STATUS_ID == 4)
            {
                tdcol3.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#ffff00");
            }

            // Get Booked Seats and dicable this seats and change colour
            DataSet bookedseats = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS", "SEAT_NO", "BID", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEAT_NO='" + seate1 + "'", "");

            //SEAT_NO = Convert.ToInt32(bookedseats.Tables[0].Rows[i]["SEAT_NO"].ToString());
            //if (seate1 == SEAT_NO)
            //{
            if (bookedseats.Tables[0].Rows.Count > 0)
            {
                imgbtncol3.Enabled = false;
                imgbtncol3.ImageUrl = "~/Images/booked_seat_img.png";
            }

        }
         //}


         //DataSet bookedseats3 = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS", "SEAT_NO", "BID", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
         //for (int j = 0; j < bookedseats.Tables[0].Rows.Count; j++)
         //{
         //    SEAT_NO = Convert.ToInt32(bookedseats.Tables[0].Rows[j]["SEAT_NO"].ToString());
        for (int i = 0; i < lvStructure.Items.Count; i++)
        {
            int seate1 = Convert.ToInt32(ds.Tables[0].Rows[i]["COL4"].ToString());
            HtmlControl tdcol4 = lvStructure.Items[i].FindControl("tdcol4") as HtmlControl;
            ImageButton imgbtncol4 = lvStructure.Items[i].FindControl("byncol4") as ImageButton;

          

            DataSet ds1 = objCommon.FillDropDown("VEHICLE_STRUCTURE_MAPPING", "STATUS_ID", "SEATS", "ROUTEID = '" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEATS='" + seate1 + "' ", "");
            int STATUS_ID = Convert.ToInt32(ds1.Tables[0].Rows[0]["STATUS_ID"].ToString());
          
            if (STATUS_ID == status)
            {
                imgbtncol4.Enabled = true;
            }
            else
            {
                imgbtncol4.Enabled = false;
            }
            if (STATUS_ID == 1)
            {
                tdcol4.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#8b8585");
            }
            else if (STATUS_ID == 2)
            {
                tdcol4.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#78b5ff");
            }
            else if (STATUS_ID == 3)
            {
                tdcol4.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#EB9999");
            }
            else if (STATUS_ID == 4)
            {
                tdcol4.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#ffff00");
            }

            // Get Booked Seats and dicable this seats and change colour
            DataSet bookedseats = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS", "SEAT_NO", "BID", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEAT_NO='" + seate1 + "'", "");

            //SEAT_NO = Convert.ToInt32(bookedseats.Tables[0].Rows[i]["SEAT_NO"].ToString());
            //if (seate1 == SEAT_NO)
            //{
            if (bookedseats.Tables[0].Rows.Count > 0)
            {
                imgbtncol4.Enabled = false;
                imgbtncol4.ImageUrl = "~/Images/booked_seat_img.png";
            }
        }
        // }


        // DataSet bookedseats4 = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS", "SEAT_NO", "BID", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
         //for (int j = 0; j < bookedseats.Tables[0].Rows.Count; j++)
         //{
         //    SEAT_NO = Convert.ToInt32(bookedseats.Tables[0].Rows[j]["SEAT_NO"].ToString());
        for (int i = 0; i < lvStructure.Items.Count; i++)
        {
            int Count1 = Convert.ToInt32(objCommon.LookUp("VEHICLE_STRUCTURE_MAPPING", "COUNT(*)", "ROUTEID='" +Convert.ToInt32(ddlRoute.SelectedValue) + "'"));
              if (Count1 == 50 )
              {
            int seate1 = Convert.ToInt32(ds.Tables[0].Rows[i]["COL5"].ToString());
            HtmlControl tdcol5 = lvStructure.Items[i].FindControl("tdcol5") as HtmlControl;
            ImageButton imgbtncol5 = lvStructure.Items[i].FindControl("byncol5") as ImageButton;

           

            DataSet ds1 = objCommon.FillDropDown("VEHICLE_STRUCTURE_MAPPING", "STATUS_ID", "SEATS", "ROUTEID = '" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEATS='" + seate1 + "' ", "");
            int STATUS_ID = Convert.ToInt32(ds1.Tables[0].Rows[0]["STATUS_ID"].ToString());
         
            if (STATUS_ID == status)
            {
                imgbtncol5.Enabled = true;
            }
            else
            {
                imgbtncol5.Enabled = false;
            }
            if (STATUS_ID == 1)
            {
                tdcol5.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#8b8585");
            }
            else if (STATUS_ID == 2)
            {
                tdcol5.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#78b5ff");
            }
            else if (STATUS_ID == 3)
            {
                tdcol5.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#EB9999");
            }
            else if (STATUS_ID == 4)
            {
                tdcol5.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#ffff00");
            }

            // Get Booked Seats and dicable this seats and change colour
            DataSet bookedseats = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_DETAILS", "SEAT_NO", "BID", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEAT_NO='" + seate1 + "'", "");

            //SEAT_NO = Convert.ToInt32(bookedseats.Tables[0].Rows[i]["SEAT_NO"].ToString());
            //if (seate1 == SEAT_NO)
            //{
            if (bookedseats.Tables[0].Rows.Count > 0)
            {
                imgbtncol5.Enabled = false;
                imgbtncol5.ImageUrl = "~/Images/booked_seat_img.png";
            }
        }
       }

    }
    protected void btnShowBusSeats_Click(object sender, EventArgs e)
    {
        divSeats.Visible = true;
        int status = 0;
        string idno = Convert.ToString(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'"));
        string Sex = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "SEX", "IDNO='" + idno + "'"));
       if (Sex == "M")
       {
            status = 2;
       }
       else
       {
           status = 3;
       }

       //objCommon.FillDropDownList(ddlSeats, "[dbo].[VEHICLE_STRUCTURE_MAPPING]", "STRMAP_ID", "SEATS", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'  and STATUS_ID='" + status + "'AND SEATS NOT IN (SELECT SEAT_NO from [dbo].[VEHICLE_BUS_BOOKING_DETAILS] where ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "' )", "SEATS"); //and BUSSTR_ID='" + Convert.ToInt32(ddlStructure.SelectedValue) + "'
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    public void clear()
    {
       // ddlSession.SelectedValue = "0";
        ddlStop.SelectedValue = "0";
        ddlRoute.SelectedValue = "0";
       // ddlbSemester.SelectedValue = "0";
        ddlStructure.SelectedValue = "0";
        //ddlSeats.SelectedValue = "0";
        txtBusSeate.Text = string.Empty;
        lblImage.ImageUrl = null;
        divimg.Visible = false;
        lblfees.Text = string.Empty;
        div1.Visible = false;
        divSeats.Visible = false;
        lvStructure.DataSource = null;
        lvStructure.DataBind();
        updDocument.Visible = false;
        structurediscription.Visible = false;
        seatestatus.Visible = false;
        ddlReceipt.SelectedValue = "0";
    }

    private void CreateStudentPayOrderId()
    {
        ViewState["OrderId"] = null;
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        //string Orderid = Convert.ToString((Convert.ToInt32(Session["IDNO"].ToString())) + (Convert.ToString(ViewState["Branch"].ToString())) + (Convert.ToString(ViewState["Semester"].ToString())) + ir);
        string Orderid = Convert.ToString((Convert.ToInt32(ViewState["idno"].ToString())) + (Convert.ToString(10)) + (Convert.ToString(2)) + ir);


        ViewState["OrderId"] = Orderid;
        Session["Order_id"] = Orderid;
    }


    #region time count
    private void StartStopwatch()
    {
       // startTime = DateTime.Now;
        DateTime startTime = Convert.ToDateTime(objCommon.LookUp("VEHICLE_BUS_BOOKING_CHECK_PROCESS", "cast(TRAN_DATE as time) as TRAN_DATE", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEAT_NO='" + Convert.ToInt32(txtBusSeate.Text) + "'"));
        lblStartTime.Text = startTime.ToString("hh:mm:ss tt");
        lblElapsedTime.Text = "00:00";
        stopwatch.Reset();
        stopwatch.Start();
      //  timer.Enabled = true;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        TimeSpan elapsedTime = stopwatch.Elapsed;

        if (elapsedTime.TotalMinutes >= 5)
        {
            StopStopwatch();
        }
        else
        {
            lblElapsedTime.Text = string.Format("{0:00}:{1:00}", (int)elapsedTime.TotalMinutes, elapsedTime.Seconds);
        }
    }

    private void StopStopwatch()
    {
        stopwatch.Stop();
      //  timer.Enabled = false;
        lblElapsedTime.Text = "05:00";
        // perform any other desired actions when the stopwatch reaches 5 minutes
    }

    #endregion

    // Define a mutex object
    private static Mutex mutex = new Mutex();

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
      

        try
        {

            if (ddlSession.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updActivity, "Please Select Session.", this.Page);
                return;
            }
            if (ddlStop.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updActivity, "Please Select Stop.", this.Page);
                return;
            }

            if (ddlRoute.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updActivity, "Please Select Route.", this.Page);
                return;
            }
            if (ddlbSemester.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updActivity, "Please Select Semester.", this.Page);
                return;
            }
            //if (ddlStructure.SelectedValue == "0")
            //{
            //    objCommon.DisplayMessage(this.updActivity, "Please Select Bus Structure.", this.Page);
            //    return;
            //}
            if (txtBusSeate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.updActivity, "Please Select Bus Seats.", this.Page);
                return;
            }
            if (lblfees.Text == "0")
            {
                objCommon.DisplayMessage(this.updActivity, "Fees Is Not Set.", this.Page);
                return;
            }

            DataSet ds = objCommon.FillDropDown("VEHIVLE_TERMS_AND_CONDITIONS_MASTER", "Id", "TERMS_AND_CONDITIONS", "", "");
            //list1.InnerText=ds.Tables[0].Rows[0]["TERMS_AND_CONDITIONS"].ToString();
            //list2.InnerText=ds.Tables[0].Rows[1]["TERMS_AND_CONDITIONS"].ToString();
            rptData.DataSource = ds;
            rptData.DataBind();
            checIsConform.Checked = false;
            Panel1_ModalPopupExtender.Show();
            #region
          //  int idno1 = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='"+Session["userno"]+"'"));

         //   DataSet checkseat = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_CHECK_PROCESS", "BCP", "ROUTEID,SEAT_NO", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEAT_NO='" + Convert.ToInt32(txtBusSeate.Text) + "' and IDNO=" + idno1  , "");
            
            //if (checkseat.Tables[0].Rows.Count==0)
            //{

            //     DataSet checkseat1 = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_CHECK_PROCESS", "BCP", "ROUTEID,SEAT_NO", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEAT_NO='" + Convert.ToInt32(txtBusSeate.Text) + "' ", "");

            //     if (checkseat1.Tables[0].Rows.Count > 0)
            //     {
            //         DateTime startTime = Convert.ToDateTime(objCommon.LookUp("VEHICLE_BUS_BOOKING_CHECK_PROCESS", "cast(TRAN_DATE as time) as TRAN_DATE", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEAT_NO='" + Convert.ToInt32(txtBusSeate.Text) + "'"));
            //         DateTime endTime = startTime.AddMinutes(8);
            //         TimeSpan sttime = startTime.TimeOfDay;
            //         TimeSpan edtime = endTime.TimeOfDay;

            //         DateTime now = DateTime.Now;
            //         TimeSpan currenttime = now.TimeOfDay;

            //         TimeSpan duration = edtime - currenttime;

            //         string formattedTime = duration.ToString(@"m\:ss");


            //         objCommon.DisplayMessage(this.updActivity, "Seat Booking Is In Process Please Try After " + formattedTime, this.Page);
            //         return;
            //     }
            //}

            //DataSet demandds1 = objCommon.FillDropDown("ACD_STUDENT A inner join USER_ACC B ON (A.IDNO=B.UA_IDNO)", "SCHEMENO,SECTIONNO,SEMESTERNO,SCHEMENO,B.IPADDRESS,B.UA_NO", "A.COLLEGE_CODE,REGNO,A.IDNO", "IDNO=(select UA_IDNO from user_acc where UA_NO ='" + Session["userno"] + "')", "");
            //int idno = Convert.ToInt32(demandds1.Tables[0].Rows[0]["IDNO"]);
            //int SECTIONNO = Convert.ToInt32(demandds1.Tables[0].Rows[0]["SECTIONNO"]);
            //int SEMESTERNO = Convert.ToInt32(demandds1.Tables[0].Rows[0]["SEMESTERNO"]);
            //int Routid = Convert.ToInt32(ddlRoute.SelectedValue);
            //int Stopeid = Convert.ToInt32(ddlStop.SelectedValue);
            //int seat = Convert.ToInt32(txtBusSeate.Text);
            //decimal fees = Convert.ToDecimal(lblfees.Text);
            //CustomStatus cs = (CustomStatus)objVMC.AddBusCheckSeats(idno, SECTIONNO, SEMESTERNO, Routid, Stopeid, seat, fees);
            //#endregion


            //#region ONLINE PAYMENT GATEWAY by Shaikh Juned 20_02_2023

            //try
            //{
            //    int ifdemandexist = 0;
            //    //ifdemandexist = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(DISTINCT 1) DEMAND_COUNT", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(Session["sessionno_current"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(CAN,0)=0"));
            //    //if (ifdemandexist == 0)
            //    //{
            //    //    objCommon.DisplayMessage("DEMAND is Not Created Properly !", this.Page);
            //    //    return;
            //    //}

            //    //int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(Session["sessionno_current"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0"));
            //    //if (ifPaidAlready > 0)
            //    //{
            //    //    objCommon.DisplayMessage("Exam Fee has been paid already. Can not proceed with the transaction !", this.Page);
            //    //    return;
            //    //}
            //    //    ViewState["Final_Amt"] = lblTotalExamFee.Text.ToString();

            //    // ViewState["Final_Amt"] = "1";


            //    //if (Convert.ToDouble(ViewState["Final_Amt"]) == 0)
            //    //{
            //    //    objCommon.DisplayMessage("You are not eligible for Fee Payment !", this);
            //    //    return;
            //    //}


            //    //if (ViewState["Final_Amt"].ToString() != string.Empty)
            //    //{
            //    //DataSet d = objCommon.FillDropDown("ACD_STUDENT", "IDNO", "REGNO,STUDNAME,STUDENTMOBILE,EMAILID", "IDNO = '" + ViewState["IDNO"] + "'", "");


            //    //-----------CREATE DEMAND ---------             
            //    //ExamController objEC = new ExamController();
            //    //// string Amt = FinalTotal.Text;
            //    //if (ViewState["TotalSubFee"] == string.Empty || ViewState["TotalSubFee"] == null)
            //    //{
            //    //    ViewState["TotalSubFee"] = "0";
            //    //}
            //    //if (ViewState["latefee"] == string.Empty || ViewState["latefee"] == null)
            //    //{
            //    //    ViewState["latefee"] = "0";
            //    //}
            //    //if (FinalTotal.Text == string.Empty || FinalTotal.Text == null)
            //    //{
            //    //    FinalTotal.Text = "0";
            //    //}

            //    DataSet demandds = objCommon.FillDropDown("ACD_STUDENT A inner join USER_ACC B ON (A.IDNO=B.UA_IDNO)", "SCHEMENO,SECTIONNO,SEMESTERNO,SCHEMENO,B.IPADDRESS,B.UA_NO", "A.COLLEGE_CODE,REGNO,A.IDNO", "IDNO=(select UA_IDNO from user_acc where UA_NO ='" + Session["userno"] + "')", "");


            //    int retStatus = 0;
            //    string Amt = lblfees.Text;
            //    objVM.SESSIONN = Convert.ToInt32(ddlSession.SelectedValue);
            //    objVM.SCHEMENO = Convert.ToInt32(demandds.Tables[0].Rows[0]["SCHEMENO"]);
            //    objVM.SEMESTERNOS = Convert.ToInt32(demandds.Tables[0].Rows[0]["SEMESTERNO"]);
            //    // objVM.COURSENOS = Convert.ToInt32(demandds.Tables[0].Rows[0]["SCHEMENO"]);
            //    objVM.COURSENOS = 0;
            //    objVM.IPADDRESS1 = (demandds.Tables[0].Rows[0]["IPADDRESS"].ToString());
            //    objVM.IDNO1 = Convert.ToInt32(demandds.Tables[0].Rows[0]["IDNO"]);
            //    ViewState["idno"] = Convert.ToInt32(demandds.Tables[0].Rows[0]["IDNO"]);
            //    objVM.REGNO1 = (demandds.Tables[0].Rows[0]["REGNO"].ToString());
            //    objVM.UA_NO = Convert.ToInt32(demandds.Tables[0].Rows[0]["UA_NO"]);
            //    if (demandds.Tables[0].Rows[0]["COLLEGE_CODE"] != DBNull.Value)
            //    {
            //        objVM.COLLEGE_CODE1 = Convert.ToInt32(demandds.Tables[0].Rows[0]["COLLEGE_CODE"]);
            //    }
            //    else
            //    {
            //        objVM.COLLEGE_CODE1 = 0;
            //    }
            //    CreateStudentPayOrderId();

            //    retStatus = objVMC.AddStudentExamRegistrationDetails(objVM, Amt, ViewState["OrderId"].ToString());
            //    if (retStatus == -99)
            //    {
            //        objCommon.ShowError(Page, "BusBooking.aspx.btnSubmit_Click() --> ");
            //        return;
            //        // objCommon.DisplayMessage("Something Went Wrong", this.Page);
            //        //return;
            //    }

            //    DataSet ds = objCommon.FillDropDown("VEHICLE_MAPPING_STATUS_MASTER", "STATUS_ID", "MAPPING_STATUS", "", "");

            //    int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'"));

            //    DataSet d = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON B.BRANCHNO=S.BRANCHNO", "IDNO", "ISNULL(REGNO,'')REGNO,ISNULL(ENROLLNO,'')ENROLLNO,ISNULL(STUDNAME,'')STUDNAME,ISNULL(STUDENTMOBILE,'')STUDENTMOBILE,ISNULL(EMAILID,'')EMAILID,ISNULL(B.SHORTNAME,'')SHORTNAME,ISNULL(SEMESTERNO,0)SEMESTERNO", "IDNO = '" + IDNO + "'", "");
            //    ViewState["STUDNAME"] = (d.Tables[0].Rows[0]["STUDNAME"].ToString());
            //    ViewState["IDNO"] = (d.Tables[0].Rows[0]["IDNO"].ToString());
            //    ViewState["EMAILID"] = (d.Tables[0].Rows[0]["EMAILID"].ToString());
            //    ViewState["MOBILENO"] = (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString());
            //    ViewState["REGNO"] = (d.Tables[0].Rows[0]["REGNO"].ToString());
            //    ViewState["SESSIONNO"] = Convert.ToInt32(Session["sessionno_current"]);
            //    ViewState["SEM"] = objCommon.LookUp("ACD_STUDENT_RESULT", "distinct SEMESTERNO", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + " AND sessionno=" + Convert.ToInt32(Session["sessionno_current"]));
            //    ViewState["RECIEPT"] = "BFR";
            //    ViewState["ENROLLNO"] = (d.Tables[0].Rows[0]["ENROLLNO"].ToString());
            //    ViewState["SHORTNAME"] = (d.Tables[0].Rows[0]["SHORTNAME"].ToString());
            //    ViewState["info"] = ViewState["REGNO"] + "," + ViewState["SHORTNAME"] + "," + ViewState["SEM"] + "," + ViewState["MOBILENO"];
            //    ViewState["basicinfo"] = ViewState["ENROLLNO"];
            //    PostOnlinePayment();
            //    string amount = string.Empty;
            //    // amount = Convert.ToString(ViewState["Final_Amt"]);
            //    amount = lblfees.Text;//amount lable
            //    // amount = "1.00";
            //    try
            //    {
            //        Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            //        int OrganizationId = Convert.ToInt32(Session["OrgId"]);
            //        //    DailyCollectionRegister dcr = this.Bind_FeeCollectionData();
            //        // string PaymentMode = "ONLINE EXAM FEES";
            //        string PaymentMode = "BUS CAN";
            //        Session["PaymentMode"] = PaymentMode;
            //        Session["studAmt"] = amount;
            //        ViewState["studAmt"] = amount;//hdnTotalCashAmt.Value;
            //        // dcr.TotalAmount = Convert.ToDouble(amount);//Convert.ToDouble(ViewState["studAmt"].ToString());
            //        Session["studName"] = ViewState["STUDNAME"].ToString(); //lblStudName.Text;
            //        Session["studPhone"] = ViewState["MOBILENO"].ToString(); // lblMobileNo.Text;
            //        Session["studEmail"] = ViewState["EMAILID"].ToString(); // lblMailId.Text;

            //        Session["ReceiptType"] = "BFR";// add recie code// tera Reciept code dalna 
            //        Session["idno"] = Convert.ToInt32(ViewState["IDNO"].ToString()); //hdfIdno.Value;
            //        Session["paysession"] = Convert.ToInt32(ddlSession.SelectedValue); // hdfSessioNo.Value;
            //        Session["paysemester"] = ddlbSemester.SelectedValue;
            //        Session["homelink"] = "RetestExamRegistration_All.aspx";
            //        Session["regno"] = ViewState["REGNO"].ToString(); // lblRegno.Text;
            //        Session["payStudName"] = ViewState["STUDNAME"].ToString(); //lblStudName.Text;
            //        Session["paymobileno"] = ViewState["MOBILENO"].ToString(); // lblMobileNo.Text;
            //        Session["Installmentno"] = "0";  //here we are passing the Sessionno as installment
            //        Session["Branchname"] = ViewState["SHORTNAME"].ToString(); //lblBranchName.Text;
            //        // yaha se apn link uthayenge filhal static dalenge
            //        string RequestUrl = string.Empty;

            //        // Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
            //        Session["paymentId"] = 1;


            //        //int payactivityno = Convert.ToInt32(objCommon.LookUp("ACD_Payment_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME  like'%BUS%'"));

            //        //string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
            //        //  RequestUrl = "http://localhost:58197//PresentationLayer//ACADEMIC//ONLINEFEECOLLECTION//PayUOnlinePaymentRequest.aspx";
            //        //Response.Redirect(RequestUrl, false); 

            //        //  Response.Redirect("http://localhost:58197/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/BilldeskOnlinePaymentRequest.aspx", false);

            //        Session["BusBookingDetails"] = ddlRoute.SelectedValue + "-" + ddlStop.SelectedValue + "-" + txtBusSeate.Text;

            //        int payactivityno = Convert.ToInt32(objCommon.LookUp("ACD_PG_CONFIGURATION A inner join ACD_Payment_ACTIVITY_MASTER B on (B.ACTIVITYNO=A.ACTIVITY_NO)", "DISTINCT(B.ACTIVITYNO)", " B.ACTIVITYNAME  like '%BUS%'"));
            //        Session["payactivityno"] = payactivityno;

            //        DataSet ds1 = feeController.GetOnlinePaymentConfigurationDetails(OrganizationId, 0, Convert.ToInt32(payactivityno));
            //        if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
            //        {
            //            if (ds1.Tables[0].Rows.Count > 1)
            //            {

            //                //// Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
            //                // Session["paymentId"] = 1;
            //                //  RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
            //                //// RequestUrl = "http://localhost:58197//PresentationLayer//ACADEMIC//ONLINEFEECOLLECTION//PayUOnlinePaymentRequest.aspx";
            //                // Response.Redirect(RequestUrl, false);// url dal dena konsi

            //                //// Response.Redirect("http://localhost:58197//PresentationLayer\\ACADEMIC\\ONLINEFEECOLLECTION\\BilldeskOnlinePaymentRequest.aspx", true);




            //            }
            //            else
            //            {
            //                Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
            //                RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
            //                Response.Redirect(RequestUrl, false);

            //                // Response.Redirect("http://localhost:58197//PresentationLayer\\ACADEMIC\\ONLINEFEECOLLECTION\\BilldeskOnlinePaymentRequest.aspx", true);
            //            }
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        Response.Write(ex.Message);
            //    }

            //    //}
            //    //else
            //    //{
            //    //    objCommon.DisplayMessage("Transaction Failed !.", this.Page);
            //    //    return;
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}


            #endregion
        }
        catch (Exception ex)
        {
            throw ex;
        }
        //finally
        //{
        //    // Release the mutex
        //    mutex.ReleaseMutex();
        //}

    }

     #region FOR GET PG_IDS ADDED BY Shaikh Juned 08_12_2022
    protected void PostOnlinePayment()
    {

        int orgId = 2; int payId = 2; string merchId = string.Empty; string checkSumKey = string.Empty; string requestUrl = string.Empty; string responseUrl = string.Empty;
        string pgPageUrl = string.Empty; string accCode = string.Empty;
        DataSet dsGetPayConfig = feeController.GetOnlinePaymentConfigurationDetails(orgId, 0, payId);
        if (dsGetPayConfig.Tables[0].Rows.Count > 0)
        {
            merchId = dsGetPayConfig.Tables[0].Rows[0]["MERCHANT_ID"].ToString();
            checkSumKey = dsGetPayConfig.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
            requestUrl = dsGetPayConfig.Tables[0].Rows[0]["REQUEST_URL"].ToString();
            responseUrl = dsGetPayConfig.Tables[0].Rows[0]["RESPONSE_URL"].ToString();
            pgPageUrl = dsGetPayConfig.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
            accCode = dsGetPayConfig.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
        }
    }
    #endregion

    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        //div1.Visible = true;
        //getroutefees(Convert.ToInt32(ddlStop.SelectedValue), Convert.ToInt32(ddlRoute.SelectedValue));
       // objCommon.FillDropDownList(ddlStop, "VEHICLE_STOPMASTER", "STOPID", "STOPNAME", "", "");
       // clear();
        lblImage.ImageUrl = null;
        divimg.Visible = false;
        structurediscription.Visible = false;
        seatestatus.Visible = false;
        lvStructure.DataSource = null;
        lvStructure.DataBind();
        updDocument.Visible = false;
        div1.Visible = false;
        div5.Visible = false;
        txtStopStarttime.Text = string.Empty;
        lblfees.Text = string.Empty;
        divSeats.Visible = false;
        txtBusSeate.Text = string.Empty;

        int route = Convert.ToInt32(ddlRoute.SelectedValue);
        DataSet ds = null;

        ds = objVMC.GetStope(route);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //lblfees.Text = ds.Tables[0].Rows[0]["ROUTE_FEES"].ToString();
                ddlStop.Items.Clear();

                DataRow dr = ds.Tables[0].NewRow();
                dr["STOPNAME"] = "Please Select";
                dr["ROUTE_CODE"] = "0";
                ds.Tables[0].Rows.InsertAt(dr, 0);

                ddlStop.DataTextField = "STOPNAME";
                ddlStop.DataValueField = "ROUTE_CODE";
                ddlStop.DataSource = ds;
                ddlStop.DataBind();
            }
        }
    }

    public void getroutefees(int stop, int route)
    {
        DataSet ds = objVMC.GetRouteFees(stop, route);
        if (ds != null)
        {
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblfees.Text = ds.Tables[0].Rows[0]["ROUTE_FEES"].ToString();
        }
        else
        {
            objCommon.DisplayMessage(this.updActivity, "Fees Is Not Set For This Stop.", this.Page);
            lblfees.Text = "0";
            return;
        }
        DateTime StopTime =Convert.ToDateTime(objCommon.LookUp("vehicle_routemaster", "Cast(STARTING_TIME as time) STARTING_TIME", "ROUTEID='" + Convert.ToInt32(route) + "'"));
       // DateTime sttime = Convert.ToDateTime(StopTime.tostring("hh:mm tt"));
        txtStopStarttime.Text = StopTime.ToString("hh:mm tt");
        }
       
    }
    protected void ddlStop_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblImage.ImageUrl = null;
        divimg.Visible = false;
        structurediscription.Visible = false;
        seatestatus.Visible = false;
        lvStructure.DataSource = null;
        lvStructure.DataBind();
        updDocument.Visible = false;
        div1.Visible = false;
        lblfees.Text = string.Empty;
        txtStopStarttime.Text = string.Empty;
        divSeats.Visible = false;
        txtBusSeate.Text = string.Empty;
        div1.Visible = true;
        div5.Visible = true;
        getroutefees(Convert.ToInt32(ddlStop.SelectedValue), Convert.ToInt32(ddlRoute.SelectedValue));
        
    }
    protected void lvStructure_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("VEHICLE_STRUCTURE_MAPPING", "STATUS_ID", "SEATS", "ROUTEID = '" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
        int STATUS_ID = 0;
        int SEATS = 0;
        HtmlControl tdcol5 = (HtmlControl)e.Item.FindControl("tdcol5");
       
        ImageButton byncol5 = (ImageButton)e.Item.FindControl("byncol5");
        Label Label7 = (Label)e.Item.FindControl("Label7");


        int Count = Convert.ToInt32(objCommon.LookUp("VEHICLE_STRUCTURE_MAPPING", "COUNT(*)", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'"));
        if (Count == 40)
        {
            byncol5.Visible = false;
            Label7.Visible = false;
            tdcol5.Visible = false;
        }
        else if (Count == 12)
        {
            byncol5.Visible = false;
            Label7.Visible = false;
            tdcol5.Visible = false;
        }
        
       

    }
    protected void byncol1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton byncol1 = sender as ImageButton;
        
        int no = int.Parse(byncol1.CommandArgument);
        DataSet ds1 = objCommon.FillDropDown("[dbo].[VEHICLE_BUS_BOOKING_DETAILS] ", "*", "", "SEAT_NO='" + no + "' and ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            objCommon.DisplayMessage(this.Page, "Seat Number " + no + " Is Already Booked !", this.Page);
            return;
        }

        DataSet ds = objCommon.FillDropDown("[dbo].[VEHICLE_BUS_BOOKING_LOG] A inner join ACD_ONLINE_PAYMENT_LOG B on (A.SRNO=B.SRNO) ", "A.*, Cast( A.TRAN_DATE as date) as trandate", "B.STATUSFLAG", "cast( A.TRAN_DATE as date)=cast(Getdate() as date) and B.STATUSFLAG<>'Success' and ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
       if(ds.Tables[0].Rows.Count>0)
       {
        int route = Convert.ToInt32(ds.Tables[0].Rows[0]["ROUTEID"].ToString());
        int seatno = Convert.ToInt32(ds.Tables[0].Rows[0]["SEAT_NO"].ToString());
        DateTime TranDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["trandate"].ToString());
        DateTime dateTime = DateTime.UtcNow.Date;
        if (route == Convert.ToInt32(ddlRoute.SelectedValue) && seatno == no)
        {
            if (TranDate == dateTime)
            {
                //objCommon.DisplayMessage(this.Page, "Seat Is In Process !", this.Page);
                //return;
            }
        }
       }

       foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol11 = i.FindControl("byncol1") as ImageButton;
           if (int.Parse(byncol11.CommandArgument) == no )
           {
               byncol11.ImageUrl = "~/Images/selected_seat_img.png";
           }
           else 
           {
               if (byncol11.ImageUrl != "~/Images/booked_seat_img.png")
               {
               byncol11.ImageUrl = "~/Images/available_seat_img_New.png";
               }
           }
       }

       foreach (ListViewItem i in lvStructure.Items)
       {
           ImageButton byncol22 = i.FindControl("byncol2") as ImageButton;
           if (int.Parse(byncol22.CommandArgument) == no)
           {
               byncol22.ImageUrl = "~/Images/selected_seat_img.png";
           }
           else
           {
               if (byncol22.ImageUrl != "~/Images/booked_seat_img.png")
               {
                   byncol22.ImageUrl = "~/Images/available_seat_img_New.png";
               }
           }
       }

       foreach (ListViewItem i in lvStructure.Items)
       {
           ImageButton byncol33 = i.FindControl("byncol3") as ImageButton;
           if (int.Parse(byncol33.CommandArgument) == no)
           {
               byncol33.ImageUrl = "~/Images/selected_seat_img.png";
           }
           else
           {
               if (byncol33.ImageUrl != "~/Images/booked_seat_img.png")
               {
                   byncol33.ImageUrl = "~/Images/available_seat_img_New.png";
               }
           }
       }

       foreach (ListViewItem i in lvStructure.Items)
       {
           ImageButton byncol44 = i.FindControl("byncol4") as ImageButton;
           if (int.Parse(byncol44.CommandArgument) == no)
           {
               byncol44.ImageUrl = "~/Images/selected_seat_img.png";
           }
           else
           {
               if (byncol44.ImageUrl != "~/Images/booked_seat_img.png")
               {
                   byncol44.ImageUrl = "~/Images/available_seat_img_New.png";
               }
           }
       }
       foreach (ListViewItem i in lvStructure.Items)
       {
           ImageButton byncol55 = i.FindControl("byncol5") as ImageButton;
           if (int.Parse(byncol55.CommandArgument) == no)
           {
               byncol55.ImageUrl = "~/Images/selected_seat_img.png";
           }
           else
           {
               if (byncol55.ImageUrl != "~/Images/booked_seat_img.png")
               {
                   byncol55.ImageUrl = "~/Images/available_seat_img_New.png";
               }
           }
       }

       // byncol1.ImageUrl = "~/Images/selected_seat_img.png";
        txtBusSeate.Text = no.ToString();
        divSeats.Visible = true;
    }
    protected void byncol2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton byncol2 = sender as ImageButton;
        int no = int.Parse(byncol2.CommandArgument);

        DataSet ds1 = objCommon.FillDropDown("[dbo].[VEHICLE_BUS_BOOKING_DETAILS] ", "*", "", "SEAT_NO='" + no + "' and ROUTEID='"+Convert.ToInt32(ddlRoute.SelectedValue)+"'", "");
        if(ds1.Tables[0].Rows.Count>0)
        {
            objCommon.DisplayMessage(this.Page, "Seat Is Already Booked !", this.Page);
            return;
        }

        DataSet ds = objCommon.FillDropDown("[dbo].[VEHICLE_BUS_BOOKING_LOG] A inner join ACD_ONLINE_PAYMENT_LOG B on (A.SRNO=B.SRNO) ", "A.*, Cast( A.TRAN_DATE as date) as trandate", "B.STATUSFLAG", "cast( A.TRAN_DATE as date)=cast(Getdate() as date) and B.STATUSFLAG<>'Success' and ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            int route = Convert.ToInt32(ds.Tables[0].Rows[0]["ROUTEID"].ToString());
            int seatno = Convert.ToInt32(ds.Tables[0].Rows[0]["SEAT_NO"].ToString());
            DateTime TranDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["trandate"].ToString());
            DateTime dateTime = DateTime.UtcNow.Date;
            if (route == Convert.ToInt32(ddlRoute.SelectedValue) && seatno == no)
            {
                if (TranDate == dateTime)
                {
                    //objCommon.DisplayMessage(this.Page,"Seat Is In Process !", this.Page);
                    //return;
                }
            }
        }
        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol11 = i.FindControl("byncol1") as ImageButton;
            if (int.Parse(byncol11.CommandArgument) == no)
            {
                byncol11.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol11.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol11.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }

        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol22 = i.FindControl("byncol2") as ImageButton;
            if (int.Parse(byncol22.CommandArgument) == no)
            {
                byncol22.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol22.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol22.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }

        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol33 = i.FindControl("byncol3") as ImageButton;
            if (int.Parse(byncol33.CommandArgument) == no)
            {
                byncol33.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol33.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol33.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }

        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol44 = i.FindControl("byncol4") as ImageButton;
            if (int.Parse(byncol44.CommandArgument) == no)
            {
                byncol44.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol44.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol44.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }
        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol55 = i.FindControl("byncol5") as ImageButton;
            if (int.Parse(byncol55.CommandArgument) == no)
            {
                byncol55.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol55.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol55.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }
       // byncol2.ImageUrl = "~/Images/selected_seat_img.png";
        txtBusSeate.Text = no.ToString();
        divSeats.Visible = true;

    }
    protected void byncol3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton byncol3 = sender as ImageButton;
        int no = int.Parse(byncol3.CommandArgument);
        DataSet ds1 = objCommon.FillDropDown("[dbo].[VEHICLE_BUS_BOOKING_DETAILS] ", "*", "", "SEAT_NO='" + no + "' and ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            objCommon.DisplayMessage(this.Page, "Seat Is Already Booked !", this.Page);
            return;
        }
        DataSet ds = objCommon.FillDropDown("[dbo].[VEHICLE_BUS_BOOKING_LOG] A inner join ACD_ONLINE_PAYMENT_LOG B on (A.SRNO=B.SRNO) ", "A.*, Cast( A.TRAN_DATE as date) as trandate", "B.STATUSFLAG", "cast( A.TRAN_DATE as date)=cast(Getdate() as date) and B.STATUSFLAG<>'Success' and ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            int route = Convert.ToInt32(ds.Tables[0].Rows[0]["ROUTEID"].ToString());
            int seatno = Convert.ToInt32(ds.Tables[0].Rows[0]["SEAT_NO"].ToString());
            DateTime TranDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["trandate"].ToString());
            DateTime dateTime = DateTime.UtcNow.Date;
            if (route == Convert.ToInt32(ddlRoute.SelectedValue) && seatno == no)
            {
                if (TranDate == dateTime)
                {
                    //objCommon.DisplayMessage(this.Page, "Seat Is In Process !", this.Page);
                    //return;
                }
            }
        }
        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol11 = i.FindControl("byncol1") as ImageButton;
            if (int.Parse(byncol11.CommandArgument) == no)
            {
                byncol11.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol11.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol11.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }

        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol22 = i.FindControl("byncol2") as ImageButton;
            if (int.Parse(byncol22.CommandArgument) == no)
            {
                byncol22.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol22.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol22.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }

        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol33 = i.FindControl("byncol3") as ImageButton;
            if (int.Parse(byncol33.CommandArgument) == no)
            {
                byncol33.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol33.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol33.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }

        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol44 = i.FindControl("byncol4") as ImageButton;
            if (int.Parse(byncol44.CommandArgument) == no)
            {
                byncol44.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol44.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol44.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }
        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol55 = i.FindControl("byncol5") as ImageButton;
            if (int.Parse(byncol55.CommandArgument) == no)
            {
                byncol55.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol55.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol55.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }
       // byncol3.ImageUrl = "~/Images/selected_seat_img.png";
        txtBusSeate.Text = no.ToString();
        divSeats.Visible = true;
    }
    protected void byncol4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton byncol4 = sender as ImageButton;
        int no = int.Parse(byncol4.CommandArgument);
        DataSet ds1 = objCommon.FillDropDown("[dbo].[VEHICLE_BUS_BOOKING_DETAILS] ", "*", "", "SEAT_NO='" + no + "' and ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            objCommon.DisplayMessage(this.Page, "Seat Is Already Booked !", this.Page);
            return;
        }

        DataSet ds = objCommon.FillDropDown("[dbo].[VEHICLE_BUS_BOOKING_LOG] A inner join ACD_ONLINE_PAYMENT_LOG B on (A.SRNO=B.SRNO) ", "A.*, Cast( A.TRAN_DATE as date) as trandate", "B.STATUSFLAG", "cast( A.TRAN_DATE as date)=cast(Getdate() as date) and B.STATUSFLAG<>'Success' and ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            int route = Convert.ToInt32(ds.Tables[0].Rows[0]["ROUTEID"].ToString());
            int seatno = Convert.ToInt32(ds.Tables[0].Rows[0]["SEAT_NO"].ToString());
            DateTime TranDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["trandate"].ToString());
            DateTime dateTime = DateTime.UtcNow.Date;
            if (route == Convert.ToInt32(ddlRoute.SelectedValue) && seatno == no)
            {
                if (TranDate == dateTime)
                {
                    //objCommon.DisplayMessage(this.Page, "Seat Is In Process !", this.Page);
                    //return;
                }
            }
        }
        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol11 = i.FindControl("byncol1") as ImageButton;
            if (int.Parse(byncol11.CommandArgument) == no)
            {
                byncol11.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol11.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol11.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }

        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol22 = i.FindControl("byncol2") as ImageButton;
            if (int.Parse(byncol22.CommandArgument) == no)
            {
                byncol22.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol22.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol22.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }

        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol33 = i.FindControl("byncol3") as ImageButton;
            if (int.Parse(byncol33.CommandArgument) == no)
            {
                byncol33.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol33.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol33.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }

        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol44 = i.FindControl("byncol4") as ImageButton;
            if (int.Parse(byncol44.CommandArgument) == no)
            {
                byncol44.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol44.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol44.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }
        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol55 = i.FindControl("byncol5") as ImageButton;
            if (int.Parse(byncol55.CommandArgument) == no)
            {
                byncol55.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol55.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol55.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }
       // byncol4.ImageUrl = "~/Images/selected_seat_img.png";
        txtBusSeate.Text = no.ToString();
        divSeats.Visible = true;
    }


    

    protected void byncol5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton byncol5 = sender as ImageButton;
        int no = int.Parse(byncol5.CommandArgument);

        DataSet ds1 = objCommon.FillDropDown("[dbo].[VEHICLE_BUS_BOOKING_DETAILS] ", "*", "", "SEAT_NO='" + no + "' and ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            objCommon.DisplayMessage(this.Page, "Seat Is Already Booked !", this.Page);
            return;
        }
        DataSet ds = objCommon.FillDropDown("[dbo].[VEHICLE_BUS_BOOKING_LOG] A inner join ACD_ONLINE_PAYMENT_LOG B on (A.SRNO=B.SRNO) ", "A.*, Cast( A.TRAN_DATE as date) as trandate", "B.STATUSFLAG", "cast( A.TRAN_DATE as date)=cast(Getdate() as date) and B.STATUSFLAG<>'Success' and ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "'", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            int route = Convert.ToInt32(ds.Tables[0].Rows[0]["ROUTEID"].ToString());
            int seatno = Convert.ToInt32(ds.Tables[0].Rows[0]["SEAT_NO"].ToString());
            DateTime TranDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["trandate"].ToString());
            DateTime dateTime = DateTime.UtcNow.Date;
            if (route == Convert.ToInt32(ddlRoute.SelectedValue) && seatno == no)
            {
                if (TranDate == dateTime)
                {
                    //objCommon.DisplayMessage(this.Page, "Seat Is In Process !", this.Page);
                    //return;
                }
            }
        }
        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol11 = i.FindControl("byncol1") as ImageButton;
            if (int.Parse(byncol11.CommandArgument) == no)
            {
                byncol11.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol11.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol11.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }

        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol22 = i.FindControl("byncol2") as ImageButton;
            if (int.Parse(byncol22.CommandArgument) == no)
            {
                byncol22.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol22.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol22.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }

        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol33 = i.FindControl("byncol3") as ImageButton;
            if (int.Parse(byncol33.CommandArgument) == no)
            {
                byncol33.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol33.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol33.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }

        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol44 = i.FindControl("byncol4") as ImageButton;
            if (int.Parse(byncol44.CommandArgument) == no)
            {
                byncol44.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol44.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol44.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }
        foreach (ListViewItem i in lvStructure.Items)
        {
            ImageButton byncol55 = i.FindControl("byncol5") as ImageButton;
            if (int.Parse(byncol55.CommandArgument) == no)
            {
                byncol55.ImageUrl = "~/Images/selected_seat_img.png";
            }
            else
            {
                if (byncol55.ImageUrl != "~/Images/booked_seat_img.png")
                {
                    byncol55.ImageUrl = "~/Images/available_seat_img_New.png";
                }
            }
        }
        //byncol5.ImageUrl = "~/Images/selected_seat_img.png";
        txtBusSeate.Text = no.ToString();
        divSeats.Visible = true;
    }
    protected void btnokyes_Click(object sender, EventArgs e)
    {
        #region
        if (checIsConform.Checked==true)
        {
            // Acquire the mutex
            mutex.WaitOne();
            try
            {
                int idno1 = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Session["userno"] + "'"));

                DataSet checkseat = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_CHECK_PROCESS", "BCP", "ROUTEID,SEAT_NO", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEAT_NO='" + Convert.ToInt32(txtBusSeate.Text) + "' and IDNO=" + idno1, "");

                if (checkseat.Tables[0].Rows.Count == 0)
                {

                    DataSet checkseat1 = objCommon.FillDropDown("VEHICLE_BUS_BOOKING_CHECK_PROCESS", "BCP", "ROUTEID,SEAT_NO", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEAT_NO='" + Convert.ToInt32(txtBusSeate.Text) + "' ", "");

                    if (checkseat1.Tables[0].Rows.Count > 0)
                    {
                        DateTime startTime = Convert.ToDateTime(objCommon.LookUp("VEHICLE_BUS_BOOKING_CHECK_PROCESS", "cast(TRAN_DATE as time) as TRAN_DATE", "ROUTEID='" + Convert.ToInt32(ddlRoute.SelectedValue) + "' and SEAT_NO='" + Convert.ToInt32(txtBusSeate.Text) + "'"));
                        DateTime endTime = startTime.AddMinutes(8);
                        TimeSpan sttime = startTime.TimeOfDay;
                        TimeSpan edtime = endTime.TimeOfDay;

                        DateTime now = DateTime.Now;
                        TimeSpan currenttime = now.TimeOfDay;

                        TimeSpan duration = edtime - currenttime;

                        string formattedTime = duration.ToString(@"m\:ss");


                        objCommon.DisplayMessage(this.updActivity, "Seat Booking Is In Process Please Try After " + formattedTime, this.Page);
                        return;
                    }
                }

                DataSet demandds1 = objCommon.FillDropDown("ACD_STUDENT A inner join USER_ACC B ON (A.IDNO=B.UA_IDNO)", "SCHEMENO,SECTIONNO,SEMESTERNO,SCHEMENO,B.IPADDRESS,B.UA_NO", "A.COLLEGE_CODE,REGNO,A.IDNO", "IDNO=(select UA_IDNO from user_acc where UA_NO ='" + Session["userno"] + "') AND UA_TYPE=2", "");
                int IsConfirm=0;
                int idno = Convert.ToInt32(demandds1.Tables[0].Rows[0]["IDNO"]);
                int SECTIONNO = Convert.ToInt32(demandds1.Tables[0].Rows[0]["SECTIONNO"]);
                int SEMESTERNO = Convert.ToInt32(demandds1.Tables[0].Rows[0]["SEMESTERNO"]);
                int Routid = Convert.ToInt32(ddlRoute.SelectedValue);
                int Stopeid = Convert.ToInt32(ddlStop.SelectedValue);
                int seat = Convert.ToInt32(txtBusSeate.Text);
                decimal fees = Convert.ToDecimal(lblfees.Text);
                if (checIsConform.Checked == true)
                {
                    IsConfirm = 1;
                }
                CustomStatus cs = (CustomStatus)objVMC.AddBusCheckSeats(idno, SECTIONNO, SEMESTERNO, Routid, Stopeid, seat, fees, IsConfirm);
        #endregion


                #region ONLINE PAYMENT GATEWAY by Shaikh Juned 20_02_2023

                try
                {
                    int ifdemandexist = 0;
                    //ifdemandexist = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(DISTINCT 1) DEMAND_COUNT", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(Session["sessionno_current"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(CAN,0)=0"));
                    //if (ifdemandexist == 0)
                    //{
                    //    objCommon.DisplayMessage("DEMAND is Not Created Properly !", this.Page);
                    //    return;
                    //}

                    //int ifPaidAlready = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(DISTINCT 1) PAY_COUNT", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + " AND SESSIONNO =" + Convert.ToInt32(Session["sessionno_current"]) + " AND RECIEPT_CODE = 'REF' AND ISNULL(RECON,0) = 1 AND ISNULL(CAN,0)=0"));
                    //if (ifPaidAlready > 0)
                    //{
                    //    objCommon.DisplayMessage("Exam Fee has been paid already. Can not proceed with the transaction !", this.Page);
                    //    return;
                    //}
                    //    ViewState["Final_Amt"] = lblTotalExamFee.Text.ToString();

                    // ViewState["Final_Amt"] = "1";


                    //if (Convert.ToDouble(ViewState["Final_Amt"]) == 0)
                    //{
                    //    objCommon.DisplayMessage("You are not eligible for Fee Payment !", this);
                    //    return;
                    //}


                    //if (ViewState["Final_Amt"].ToString() != string.Empty)
                    //{
                    //DataSet d = objCommon.FillDropDown("ACD_STUDENT", "IDNO", "REGNO,STUDNAME,STUDENTMOBILE,EMAILID", "IDNO = '" + ViewState["IDNO"] + "'", "");


                    //-----------CREATE DEMAND ---------             
                    //ExamController objEC = new ExamController();
                    //// string Amt = FinalTotal.Text;
                    //if (ViewState["TotalSubFee"] == string.Empty || ViewState["TotalSubFee"] == null)
                    //{
                    //    ViewState["TotalSubFee"] = "0";
                    //}
                    //if (ViewState["latefee"] == string.Empty || ViewState["latefee"] == null)
                    //{
                    //    ViewState["latefee"] = "0";
                    //}
                    //if (FinalTotal.Text == string.Empty || FinalTotal.Text == null)
                    //{
                    //    FinalTotal.Text = "0";
                    //}

                    DataSet demandds = objCommon.FillDropDown("ACD_STUDENT A INNER JOIN USER_ACC B ON (A.IDNO=B.UA_IDNO)", "SCHEMENO,SECTIONNO,SEMESTERNO,SCHEMENO,B.IPADDRESS,B.UA_NO", "A.COLLEGE_CODE,REGNO,A.IDNO", "IDNO=(SELECT UA_IDNO FROM USER_ACC WHERE UA_NO ='" + Session["userno"] + "') AND UA_TYPE=2", "");


                    int retStatus = 0;
                    string Amt = lblfees.Text;
                    objVM.SESSIONN = Convert.ToInt32(ddlSession.SelectedValue);
                    objVM.SCHEMENO = Convert.ToInt32(demandds.Tables[0].Rows[0]["SCHEMENO"]);
                    objVM.SEMESTERNOS = Convert.ToInt32(demandds.Tables[0].Rows[0]["SEMESTERNO"]);
                    // objVM.COURSENOS = Convert.ToInt32(demandds.Tables[0].Rows[0]["SCHEMENO"]);
                    objVM.COURSENOS = 0;
                    objVM.IPADDRESS1 = (demandds.Tables[0].Rows[0]["IPADDRESS"].ToString());
                    objVM.IDNO1 = Convert.ToInt32(demandds.Tables[0].Rows[0]["IDNO"]);
                    ViewState["idno"] = Convert.ToInt32(demandds.Tables[0].Rows[0]["IDNO"]);
                    objVM.REGNO1 = (demandds.Tables[0].Rows[0]["REGNO"].ToString());
                    objVM.UA_NO = Convert.ToInt32(demandds.Tables[0].Rows[0]["UA_NO"]);
                    if (demandds.Tables[0].Rows[0]["COLLEGE_CODE"] != DBNull.Value)
                    {
                        objVM.COLLEGE_CODE1 = Convert.ToInt32(demandds.Tables[0].Rows[0]["COLLEGE_CODE"]);
                    }
                    else
                    {
                        objVM.COLLEGE_CODE1 = 0;
                    }
                    CreateStudentPayOrderId();

                    retStatus = objVMC.AddStudentExamRegistrationDetails(objVM, Amt, ViewState["OrderId"].ToString());
                    if (retStatus == -99)
                    {
                        objCommon.ShowError(Page, "BusBooking.aspx.btnSubmit_Click() --> ");
                        return;
                        // objCommon.DisplayMessage("Something Went Wrong", this.Page);
                        //return;
                    }

                    DataSet ds = objCommon.FillDropDown("VEHICLE_MAPPING_STATUS_MASTER", "STATUS_ID", "MAPPING_STATUS", "", "");

                    int IDNO = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO='" + Convert.ToInt32(Session["userno"]) + "'"));

                    DataSet d = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON B.BRANCHNO=S.BRANCHNO", "IDNO", "ISNULL(REGNO,'')REGNO,ISNULL(ENROLLNO,'')ENROLLNO,ISNULL(STUDNAME,'')STUDNAME,ISNULL(STUDENTMOBILE,'')STUDENTMOBILE,ISNULL(EMAILID,'')EMAILID,ISNULL(B.SHORTNAME,'')SHORTNAME,ISNULL(SEMESTERNO,0)SEMESTERNO", "IDNO = '" + IDNO + "'", "");
                    ViewState["STUDNAME"] = (d.Tables[0].Rows[0]["STUDNAME"].ToString());
                    ViewState["IDNO"] = (d.Tables[0].Rows[0]["IDNO"].ToString());
                    ViewState["EMAILID"] = (d.Tables[0].Rows[0]["EMAILID"].ToString());
                    ViewState["MOBILENO"] = (d.Tables[0].Rows[0]["STUDENTMOBILE"].ToString());
                    ViewState["REGNO"] = (d.Tables[0].Rows[0]["REGNO"].ToString());
                    ViewState["SESSIONNO"] = Convert.ToInt32(Session["sessionno_current"]);
                    ViewState["SEM"] = objCommon.LookUp("ACD_STUDENT_RESULT", "distinct SEMESTERNO", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + " AND sessionno=" + Convert.ToInt32(Session["sessionno_current"]));
                    ViewState["RECIEPT"] = "BFR";
                    ViewState["ENROLLNO"] = (d.Tables[0].Rows[0]["ENROLLNO"].ToString());
                    ViewState["SHORTNAME"] = (d.Tables[0].Rows[0]["SHORTNAME"].ToString());
                    ViewState["info"] = ViewState["REGNO"] + "," + ViewState["SHORTNAME"] + "," + ViewState["SEM"] + "," + ViewState["MOBILENO"];
                    ViewState["basicinfo"] = ViewState["ENROLLNO"];
                    PostOnlinePayment();
                    string amount = string.Empty;
                    // amount = Convert.ToString(ViewState["Final_Amt"]);
                    amount = lblfees.Text;//amount lable
                    // amount = "1.00";
                    try
                    {
                        Session["ReturnpageUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
                        int OrganizationId = Convert.ToInt32(Session["OrgId"]);
                        //    DailyCollectionRegister dcr = this.Bind_FeeCollectionData();
                        // string PaymentMode = "ONLINE EXAM FEES";
                        string PaymentMode = "BUS CAN";
                        Session["PaymentMode"] = PaymentMode;
                        Session["studAmt"] = amount;
                        ViewState["studAmt"] = amount;//hdnTotalCashAmt.Value;
                        // dcr.TotalAmount = Convert.ToDouble(amount);//Convert.ToDouble(ViewState["studAmt"].ToString());
                        Session["studName"] = ViewState["STUDNAME"].ToString(); //lblStudName.Text;
                        Session["studPhone"] = ViewState["MOBILENO"].ToString(); // lblMobileNo.Text;
                        Session["studEmail"] = ViewState["EMAILID"].ToString(); // lblMailId.Text;

                        Session["ReceiptType"] = "BFR";// add recie code// tera Reciept code dalna 
                        Session["idno"] = Convert.ToInt32(ViewState["IDNO"].ToString()); //hdfIdno.Value;
                        Session["paysession"] = Convert.ToInt32(ddlSession.SelectedValue); // hdfSessioNo.Value;
                        Session["paysemester"] = ddlbSemester.SelectedValue;
                        Session["homelink"] = "RetestExamRegistration_All.aspx";
                        Session["regno"] = ViewState["REGNO"].ToString(); // lblRegno.Text;
                        Session["payStudName"] = ViewState["STUDNAME"].ToString(); //lblStudName.Text;
                        Session["paymobileno"] = ViewState["MOBILENO"].ToString(); // lblMobileNo.Text;
                        Session["Installmentno"] = "0";  //here we are passing the Sessionno as installment
                        Session["Branchname"] = ViewState["SHORTNAME"].ToString(); //lblBranchName.Text;
                        // yaha se apn link uthayenge filhal static dalenge
                        string RequestUrl = string.Empty;

                        // Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                        Session["paymentId"] = 1;


                        //int payactivityno = Convert.ToInt32(objCommon.LookUp("ACD_Payment_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME  like'%BUS%'"));

                        //string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                        //  RequestUrl = "http://localhost:58197//PresentationLayer//ACADEMIC//ONLINEFEECOLLECTION//PayUOnlinePaymentRequest.aspx";
                        //Response.Redirect(RequestUrl, false); 

                        //  Response.Redirect("http://localhost:58197/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/BilldeskOnlinePaymentRequest.aspx", false);

                        Session["BusBookingDetails"] = ddlRoute.SelectedValue + "-" + ddlStop.SelectedValue + "-" + txtBusSeate.Text;

                        int payactivityno = Convert.ToInt32(objCommon.LookUp("ACD_PG_CONFIGURATION A inner join ACD_Payment_ACTIVITY_MASTER B on (B.ACTIVITYNO=A.ACTIVITY_NO)", "DISTINCT(B.ACTIVITYNO)", " B.ACTIVITYNAME  like '%BUS%'"));
                        Session["payactivityno"] = payactivityno;

                        DataSet ds1 = feeController.GetOnlinePaymentConfigurationDetails(OrganizationId, 0, Convert.ToInt32(payactivityno));
                        if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 1)
                            {

                                //// Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                                // Session["paymentId"] = 1;
                                //  RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                                //// RequestUrl = "http://localhost:58197//PresentationLayer//ACADEMIC//ONLINEFEECOLLECTION//PayUOnlinePaymentRequest.aspx";
                                // Response.Redirect(RequestUrl, false);// url dal dena konsi

                                //// Response.Redirect("http://localhost:58197//PresentationLayer\\ACADEMIC\\ONLINEFEECOLLECTION\\BilldeskOnlinePaymentRequest.aspx", true);




                            }
                            else
                            {
                                Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                                RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                                Response.Redirect(RequestUrl, false);

                                // Response.Redirect("http://localhost:58197//PresentationLayer\\ACADEMIC\\ONLINEFEECOLLECTION\\BilldeskOnlinePaymentRequest.aspx", true);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }


                    //}
                    //else
                    //{
                    //    objCommon.DisplayMessage("Transaction Failed !.", this.Page);
                    //    return;
                    //}
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Release the mutex
                mutex.ReleaseMutex();
            }
        }
        else
        {
            DataSet ds = objCommon.FillDropDown("VEHIVLE_TERMS_AND_CONDITIONS_MASTER", "Id", "TERMS_AND_CONDITIONS", "", "");
            //list1.InnerText = ds.Tables[0].Rows[0]["TERMS_AND_CONDITIONS"].ToString();
            //list2.InnerText = ds.Tables[0].Rows[1]["TERMS_AND_CONDITIONS"].ToString();
            rptData.DataSource = ds;
            rptData.DataBind();
            Panel1_ModalPopupExtender.Show();
         }


        #endregion
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlReceipt.SelectedValue=="0")
        {
            objCommon.DisplayMessage(this.Page, "Please Select Receipt Id.", this.Page);
            return;
        }
        ShowReport("OnlineFeePayment", "rptBusOnlineReceipt.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            //string OrderNo = (objCommon.LookUp("VEHICLE_BUS_BOOKING_DETAILS", "ORDER_ID", "IDNO=(select UA_IDNO from user_acc where UA_NO ='" + Session["userno"] + "' AND UA_TYPE=2)"));
            //int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "ORDER_ID='" + Convert.ToString(OrderNo) + "'"));
            //int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID='" + Convert.ToString(OrderNo) + "'"));
            int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "ORDER_ID='" + Convert.ToString(ddlReceipt.SelectedValue) + "'"));
            int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID='" + Convert.ToString(ddlReceipt.SelectedValue) + "'"));
            int collegeCode = Convert.ToInt32(objCommon.LookUp("Reff", "College_code", ""));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("VEHICLE_MAINTENANCE")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + collegeCode + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            string LedgerName = string.Empty;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}