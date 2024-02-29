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

public partial class HOSTEL_StudSearchInfo : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RoomAllotmentController objRM = new RoomAllotmentController();
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
        //Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"..\includes\prototype.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"..\includes\scriptaculous.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"..\includes\modalbox.js"));

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
                //Page Authorization
                CheckPageAuthorization();
                //Session["apply"] = null;
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
            }
            else
            {

                if (Page.Request.Params["__EVENTTARGET"] != null)
                {
                    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                    {
                        string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                        bindlist(arg[0], arg[1]);
                    }

                    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                    {
                        txtSearch.Text = string.Empty;
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                        lblNoRecords.Text = string.Empty;
                    }
                }
            }
            if (Request.QueryString["id"] != null)
            {
                txtRegno.Text = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + Convert.ToInt32(Request.QueryString["id"].ToString()));
                //if (Convert.ToInt32(Request.QueryString["id"].ToString()) == 0)
                //FillInformation();
                this.DisplayInfo(Convert.ToInt32(Request.QueryString["id"].ToString()));
            }
            if (Session["usertype"].ToString() != "2")
                trRegno.Visible = true;
            else
            {
                trRegno.Visible = false;
                string idno = objCommon.LookUp("ACD_STUDENT S INNER JOIN USER_ACC U ON(U.UA_IDNO=S.IDNO)", "DISTINCT IDNO", "CAN=0 AND ADMCAN=0 AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
                if (idno == "")
                    objCommon.DisplayMessage("Record Not Found", this.Page);
                else
                {
                    // FillInformation();
                    DisplayInfo(Convert.ToInt32(idno));
                }

            }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_StudSearchInfo.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudSearchInfo.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudSearchInfo.aspx");
        }
    }

    private void bindlist(string category, string searchtext)
    {
        DataSet ds = objRM.RetrieveHostelStudDetails(searchtext.Trim(), category.Trim());  //Trim Condition Added By himanshu Tamrakar 27-02-2024

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lvStudent.DataSource = null; // Code Added By Himanshu Tamrakar 27-02-2024
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : 0 ; Student have not alloted room yet.";  //show message added by Saurabh L on 04/10/2022
        }
         //lblNoRecords.Text = "Total Records : 0 ; Student have not alloted room yet.";  //show message added by Saurabh L on 04/10/2022
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        //txtRegno.Text = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + Convert.ToInt32(lnk.CommandArgument));

        Response.Redirect(url + "&id=" + lnk.CommandArgument);

    }

    private void DisplayInfo(int idno)
    {
        DataSet ds = null;
        try
        {
            ds = objRM.GetHostelStudInfoByIdno(idno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                /// show student information

                lblStudName.Text = dr["STUDNAME"].ToString();
                lblSex.Text = dr["GENDER"].ToString();
                lblRegNo.Text = dr["REGNO"].ToString();
                lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());

                lblDegree.Text = dr["DEGREENAME"].ToString();
                lblBranch.Text = dr["BRANCH_NAME"].ToString();
                lblSemester.Text = dr["SEMESTERNAME"].ToString();
                lblBatch.Text = dr["BATCHNAME"].ToString();


                ////Hostel Detail
                lblHostel.Text = dr["HOSTEL_NAME"].ToString();
                lblHostel.ToolTip = dr["HOSTEL_NO"].ToString();
                lblFloor.Text = dr["FLOOR_NAME"].ToString();
                lblBlock.Text = dr["BLOCK_NAME"].ToString();
                lblBlock.ToolTip = dr["BLOCK_NO"].ToString();
                lblRoom.Text = dr["ROOM_NAME"].ToString();
                lblRoom.ToolTip = dr["ROOM_NO"].ToString();
                lblMess.Text = dr["MESS_NAME"].ToString();
                lblSession.Text = dr["SESSION_NAME"].ToString();
                lblSession.ToolTip = dr["HOSTEL_SESSION_NO"].ToString();

                //Personal Details
                lblFatherName.Text = dr["FATHERNAME"].ToString();
                lblFatherMobile.Text = dr["FATHERMOBILE"].ToString();
                lblMotherName.Text = dr["MOTHERNAME"].ToString();
                lblMotherMobile.Text = dr["MOTHERMOBILE"].ToString();
                lblEmail.Text = dr["EMAILID"].ToString();            //Added by Saurabh L on 03/10/2022 TicketId: 34604
                lblmobile.Text = dr["STUDENTMOBILE"].ToString();     //Added by Saurabh L on 03/10/2022 TicketId: 34604
                //Address
                lblAddress.Text = dr["PADDRESS"].ToString();
                lblCity.Text = dr["CITY"].ToString();
                lblState.Text = dr["STATENAME"].ToString();
                //Bank Details
                lblBank.Text = dr["BANK"].ToString();
                lblAccno.Text = dr["ACC_NO"].ToString();
                lblBBranch.Text = dr["BANK_BRANCH"].ToString();

                //Vehical Details
                if (dr["VEHICLE_TYPE"].ToString() == "Please Select") //modify by Sonali Borekar
                {
                    lblVehType.Text = "-";
                }
                else
                {
                    lblVehType.Text = dr["VEHICLE_TYPE"].ToString();
                }

                lblVehName.Text = dr["VEHICLE_NAME"].ToString();
                lblVehNo.Text = dr["VEHICLE_NO"].ToString();


                //divStudInfo.Visible = true;
                ViewState["Idno"] = idno;
                if (dr["PHOTO"].ToString() != "")
                    imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";

                //Asset Details
                AssetDetails(idno);

                //Attendance Details
                AttendanceDetails(idno);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_StudSearchInfo.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void AssetDetails(int idno)
    {
        DataSet ds = objCommon.FillDropDown("ACD_HOSTEL_ASSET_ALLOTMENT HAA INNER JOIN ACD_HOSTEL_ASSET HA ON(HA.ASSET_NO=HAA.ASSET_NO) INNER JOIN ACD_HOSTEL_ROOM_ALLOTMENT HRA ON (HRA.ROOM_NO=HAA.ROOM_NO)INNER JOIN ACD_HOSTEL_ROOM HR ON (HR.ROOM_NO = HRA.ROOM_NO)", "ASSET_ALLOTMENT_NO", "HAA.ASSET_NO,ASSET_NAME,QUANTITY,ALLOTMENT_CODE,HRA.ROOM_NO", "HRA.CAN=0 AND HRA.HOSTEL_SESSION_NO=" + Convert.ToInt32(lblSession.ToolTip) + " AND HRA.HOSTEL_NO=" + Convert.ToInt32(lblHostel.ToolTip) + " AND HAA.ROOM_NO=" + Convert.ToInt32(lblRoom.ToolTip) + " AND RESIDENT_NO=" + idno, "ASSET_ALLOTMENT_NO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvAsset.DataSource = ds;
            lvAsset.DataBind();
        }
        else
        {
            lvAsset.DataSource = null;
            lvAsset.DataBind();
        }
    }
    private void AttendanceDetails(int idno)
    {
        DataSet ds = objRM.MonthlyAttandance(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(lblHostel.ToolTip), 0, 0, idno);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvAttendance.DataSource = ds;
            lvAttendance.DataBind();
        }
        else
        {
            lvAttendance.DataSource = null;
            lvAttendance.DataBind();
        }

    }
}
