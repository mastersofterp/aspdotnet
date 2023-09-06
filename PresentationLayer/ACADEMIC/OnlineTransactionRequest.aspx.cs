using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;

using System.Net;
using System.Text; 

public partial class ACADEMIC_OnlineTransactionRequest : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help

                    //Page Authorization
                    //this.CheckPageAuthorization();

                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
            }
            divMsg.InnerHtml = string.Empty;
            div_Studentdetail.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_OnlineTransactionRequest.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");

        }    
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PaymentTypeModification.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PaymentTypeModification.aspx");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PaymentTypeModification.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objCommon.GetFailedTransactionDataOfStudent(txtAppID.Text.Trim());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                div_Studentdetail.Visible = true;
                lvstudList.DataSource = ds.Tables[0];
                lvstudList.DataBind();
                lvstudList.Visible = true;
                lblStudName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                lblStudEnrollNo.Text = ds.Tables[0].Rows[0]["ENROLLNMENTNO"].ToString();
                lblStudClg.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                lblStudDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                lblStudBranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();


                foreach (ListViewDataItem lvItem in lvstudList.Items)
                {
                    Button btnreq = (Button)lvItem.FindControl("btnRequest");
                    HiddenField flag = (HiddenField)lvItem.FindControl("hdfFlag");
                    if (flag.Value.ToString() == "0")
                    {
                        btnreq.Enabled = true;
                    }
                    else
                    {
                        btnreq.Enabled = false;
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage("Record Not Found", this.Page);
                lvstudList.DataSource = null;
                lvstudList.DataBind();
                lvstudList.Visible = false;
                div_Studentdetail.Visible = false;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PaymentTypeModification.BindListView() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        
    }

    //protected void btnRequest_Click(object sender, EventArgs e)
    //{

    //    foreach (ListViewDataItem lvItem in lvstudList.Items)
    //    {
    //        HiddenField tempDcrNo = (HiddenField)lvItem.FindControl("hdfTempDcrNo");
    //        Button reqbtn = (Button)lvItem.FindControl("btnRequest");
    //       string reval=  reqbtn.CommandArgument;
    //       if (reval == tempDcrNo.Value)
    //       {
    //           Label merchent = (Label)lvItem.FindControl("lblORDERID");
    //           HiddenField transactioId = (HiddenField)lvItem.FindControl("hdfORDERID");
    //           HiddenField apTrnaId = (HiddenField)lvItem.FindControl("hdfApTranId");
    //           Label amount = (Label)lvItem.FindControl("lblAmount");
    //           Label receiptCode = (Label)lvItem.FindControl("lblMsg");

    //           string responsemsg;
    //           var request = (HttpWebRequest)WebRequest.Create("https://eazypay.icicibank.com/EazyPGVerify?ezpaytranid=&amount=&paymentmode=&merchantid=" + merchent.ToolTip + "&trandate=&pgreferenceno=" + transactioId.Value);
    //           var response = (HttpWebResponse)request.GetResponse();
    //           Stream dataStream = response.GetResponseStream();
    //           StreamReader reader = new StreamReader(dataStream);
    //           string strResponse = reader.ReadToEnd();
    //           Dictionary<string, string> keyValuePairs = strResponse.Split('&')
    //             .Select(value => value.Split('='))
    //             .ToDictionary(pair => pair[0], pair => pair[1]);

    //           responsemsg = keyValuePairs["status"];
    //           FeeCollectionController objFeesCnt = new FeeCollectionController();
    //           if (responsemsg != "" || responsemsg != null)
    //           {
    //               int retval = objFeesCnt.OnlinePayment_updatePAyment(apTrnaId.Value.ToString(), transactioId.Value.ToString(), amount.ToolTip, responsemsg, receiptCode.ToolTip, "");
    //               if (retval == -99)
    //               {
    //                   objCommon.DisplayMessage(updLists, "Error ocured", this.Page);
    //               }
    //               else
    //               {
    //                   objCommon.DisplayMessage(updLists, "Request success", this.Page);
    //               }
    //           }
    //       }
    //    }
        
    //}
    
    protected void lvstudList_ItemCommand(object sender, ListViewCommandEventArgs e)
    {

        if (e.CommandName == "getdata")
        {
            if (e.CommandSource is Button)
            {
                ListViewDataItem item = (e.CommandSource as Button).NamingContainer as ListViewDataItem;
                string reval = e.CommandArgument.ToString();
                HiddenField tempDcrNo = (HiddenField)item.FindControl("hdfTempDcrNo");
                if (reval == tempDcrNo.Value)
                {
                    Label merchent = (Label)item.FindControl("lblORDERID");
                    HiddenField transactioId = (HiddenField)item.FindControl("hdfORDERID");
                    HiddenField apTrnaId = (HiddenField)item.FindControl("hdfApTranId");
                    Label amount = (Label)item.FindControl("lblAmount");
                    Label receiptCode = (Label)item.FindControl("lblMsg");

                    string responsemsg;
                    var request = (HttpWebRequest)WebRequest.Create("https://eazypay.icicibank.com/EazyPGVerify?ezpaytranid=&amount=&paymentmode=&merchantid=" + merchent.ToolTip + "&trandate=&pgreferenceno=" + transactioId.Value);
                    var response = (HttpWebResponse)request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string strResponse = reader.ReadToEnd();
                    Dictionary<string, string> keyValuePairs = strResponse.Split('&')
                      .Select(value => value.Split('='))
                      .ToDictionary(pair => pair[0], pair => pair[1]);

                    responsemsg = keyValuePairs["status"];
                    FeeCollectionController objFeesCnt = new FeeCollectionController();
                    if (responsemsg != "" || responsemsg != null)
                    {
                        int retval = objFeesCnt.OnlinePayment_updatePAyment(apTrnaId.Value.ToString(), transactioId.Value.ToString(), amount.ToolTip, responsemsg, receiptCode.ToolTip, "");
                        if (retval == -99)
                        {
                            objCommon.DisplayMessage(updLists, "Error occured", this.Page);
                        }
                        else
                        {
                            BindListView();
                            //objCommon.DisplayMessage(updLists, "Request success", this.Page);
                        }
                    }
                }
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}