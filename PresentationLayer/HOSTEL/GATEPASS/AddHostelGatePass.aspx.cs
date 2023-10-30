using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class HOSTEL_GATEPASS_AddHostelGatePass : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AddHostelGatePass objGatePass = new AddHostelGatePass();
    AddHostelGatePassController objHGP = new AddHostelGatePassController();

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            imgPhoto.ImageUrl = "~/images/nophoto.jpg";
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Master_GatePassMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion Page Events

    #region Action
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        objGatePass.PassNo = Convert.ToInt32(txtPass.Text.Trim());

        string gatepassno = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "HOSTEL_GATE_PASS_NO", "HOSTEL_GATE_PASS_NO=" + txtPass.Text.ToString() + " AND IDNO="+  Session["idno"].ToString());
        if (gatepassno == "")
        {
            objCommon.DisplayMessage("Record Not Found", this.Page);
            pnlinfo.Visible = false;
        }
        else
        {
            Session["GatePassno"] = Convert.ToInt32(txtPass.Text.Trim());
            objGatePass.PassNo = Convert.ToInt32(Session["GatePassno"]);
            // FillInformation();
            DisplayInfo(Convert.ToInt32(gatepassno));
        }
    }

    private void DisplayInfo(int gatepassno)
    {
        DataSet ds = null;
        try
        {
            ds = objHGP.GetHostelGatePassInfo(gatepassno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                /// show student information

                lblName.Text = dr["STUDNAME"].ToString();
                lbladno.Text = dr["REGNO"].ToString();
                lblhostel.Text = dr["HOSTEL_NAME"].ToString();
                lblRoom.Text = dr["ROOM_NO"].ToString();
                lblvalFrom.Text = dr["OUTDATE"].ToString();
                lblvalTo.Text = dr["INDATE"].ToString();

                //if (dr["PHOTO"].ToString() != "")
                    imgPhoto.ImageUrl = "~/showimage.aspx?id=" + Session["idno"].ToString() + "&type=STUDENT";

                pnlinfo.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_AddHostelGatePass.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }

    }
    #endregion Action
}