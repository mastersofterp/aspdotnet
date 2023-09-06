//======================================================================================
// PROJECT NAME  : CCMS                                                               
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PAY_ITAcknowledge.aspx.cs                                             
// CREATION DATE :  Mrunal Bansod                                                     
// CREATED BY    :  1/05/2011      
// Modified By   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Pay_ITAcknowledge : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITMasController objITMas = new ITMasController();

  
    string UsrStatus = string.Empty;

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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                GetAcknowledge();
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_ITAcknowledge.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ITAcknowledge.aspx");
        }
    }
    private void GetAcknowledge()
    {
        DataTableReader dtr = objITMas.GetITAcknowledge();

        if (dtr != null)
        {
            if (dtr.Read())
            {
                txtQuarter1.Text = dtr["Q1"].ToString().Trim();
                txtQuarter2.Text = dtr["Q2"].ToString().Trim();
                txtQuarter3.Text = dtr["Q3"].ToString().Trim();
                txtQuarter4.Text = dtr["Q4"].ToString().Trim();
                txtQuarter5.Text = dtr["Q5"].ToString().Trim();

                txtAck1.Text = dtr["ACK1"].ToString().Trim();
                txtAck2.Text = dtr["ACK2"].ToString().Trim();
                txtAck3.Text = dtr["ACK3"].ToString().Trim();
                txtAck4.Text = dtr["ACK4"].ToString().Trim();
                txtAck5.Text = dtr["ACK5"].ToString().Trim();
                
                


            }
            dtr.Close();
        }
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ITConfiguration objIT = new ITConfiguration();

            objIT.Q1 = txtQuarter1.Text.Trim();
            objIT.Q2 = txtQuarter2.Text.Trim();
            objIT.Q3 = txtQuarter3.Text.Trim();
            objIT.Q4 = txtQuarter4.Text.Trim();
            objIT.Q5 = txtQuarter5.Text.Trim();

            objIT.ACK1 = txtAck1.Text.Trim();
            objIT.ACK2 = txtAck2.Text.Trim();
            objIT.ACK3 = txtAck3.Text.Trim();
            objIT.ACK4 = txtAck4.Text.Trim();
            objIT.ACK5 = txtAck5.Text.Trim();


            CustomStatus cs = (CustomStatus)objITMas.UpdateITAcknowledge(objIT);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                Label_ConfirmMessage.Text = "Record Saved Successfully";
                objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);
                //ViewState["action"] = "edit";
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }

    }


    //private string GetUserRight()
    //{
    //    objucc = new UserAuthorizationController(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
    //    DataTable dtrights = objucc.GetMenuStatusUserwise(Convert.ToInt32(Session["idno"]),"PAYROLL/IncomeTax/Master/"+objCommon.GetCurrentPageName());
    //    if (dtrights != null)
    //    {
    //        if (dtrights.Rows.Count > 0)
    //        {
    //            UsrStatus = dtrights.Rows[0]["STATUS"].ToString().Trim();
    //            return UsrStatus;
    //        }
    //        else
    //        {
    //            objCommon.DisplayUserMessage(updpanel, "Invalid Page Request!", this);
    //            return "INVALID";
    //        }
    //    }
    //    else
    //    {
    //        objCommon.DisplayUserMessage(updpanel, "Invalid Page Request!", this);
    //        return "INVALID";
    //    }
    //}

    protected void btncancel_Click(object sender, EventArgs e)
    {
        GetAcknowledge();
        //Clear();
    }

    private void Clear()
    {
        txtQuarter1.Text = string.Empty;
        txtQuarter2.Text = string.Empty;
        txtQuarter3.Text = string.Empty;
        txtQuarter4.Text = string.Empty;
        txtQuarter5.Text = string.Empty;

        txtAck1.Text = string.Empty;
        txtAck2.Text = string.Empty;
        txtAck3.Text = string.Empty;
        txtAck4.Text = string.Empty;
        txtAck5.Text = string.Empty;


    }
}
