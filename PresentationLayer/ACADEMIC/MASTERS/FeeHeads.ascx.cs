//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : FEE HEAD DEFINITION                                                  
// CREATION DATE : 14-MAY-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class Academic_Masters_FeeHeads : System.Web.UI.UserControl
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                //Session["colcode"] = "1";
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                }
                pnlfees.Visible = false;
            }

            PopulateDropDownList();
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN Receipt Type
            objCommon.FillDropDownList(ddlRecType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", string.Empty, "RECIEPT_TITLE");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeesHeads.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=FeesHeadEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeesHeadEntry.aspx");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;
        lvFeesHead.DataSource = null;
        lvFeesHead.DataBind();
        ddlRecType.SelectedIndex = 0;
        this.btnReport.Enabled = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            FeesHeadController objFHC = new FeesHeadController();
            int count = 0;

            foreach (ListViewDataItem lvHead in lvFeesHead.Items)
            {
                Label txtHead = lvHead.FindControl("txtHead") as Label;
                TextBox txtLName = lvHead.FindControl("txtLName") as TextBox;
                TextBox txtSName = lvHead.FindControl("txtSName") as TextBox;
                //HiddenField hdnvalue = lvHead.FindControl("hdn_fld") as HiddenField;
                DropDownList ddlRptShow = lvHead.FindControl("ddlReceiptShow") as DropDownList;
                CheckBox chkIsScholarship = lvHead.FindControl("chkIsScholarship") as CheckBox;
                CheckBox ChkIsCautionMoney = lvHead.FindControl("ChkIsCautionMoney") as CheckBox;
                CheckBox chkIsReactivateStudent = lvHead.FindControl("chkIsReactivateStudent") as CheckBox;

                if (txtLName.Text == string.Empty && txtSName.Text != string.Empty)
                {
                    objCommon.DisplayMessage("Please Enter Long Name as well.", this.Page);
                    txtLName.Focus();
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objFHC.UpdateFeeHead(Convert.ToInt32((txtHead.ToolTip)), (txtSName.Text), (txtLName.Text), Convert.ToInt32((ddlRptShow.SelectedIndex > 0) ? Int32.Parse(ddlRptShow.SelectedValue) : 0), Convert.ToInt32(chkIsScholarship.Checked), Convert.ToInt32(chkIsReactivateStudent.Checked), Convert.ToInt32(ChkIsCautionMoney.Checked));
                    //CustomStatus cs = (CustomStatus)objFHC.UpdateFeeHead(objFeesHead);

                    if (cs.Equals(CustomStatus.RecordUpdated))
                        count++;
                }
            }

            if (count == lvFeesHead.Items.Count)
                //lblmsg.Text = "Record Saved Successfully";
                objCommon.DisplayMessage("Fee Heads Defined Successfully...!!!", this.Page);

            else
                //lblerror.Text = "Error...";
                objCommon.DisplayMessage("Error in Fee Heads Definition...!!!", this.Page);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeHeads.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlRecType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.btnReport.Enabled = true;
        BindListViewFeesHead();
        foreach (ListViewDataItem lvHead in lvFeesHead.Items)
        {
            DropDownList ddlRptShw = lvHead.FindControl("ddlReceiptShow") as DropDownList;
            HiddenField hdn_fld = lvHead.FindControl("hdn_fld") as HiddenField;
            objCommon.FillDropDownList(ddlRptShw, "ACD_RECIEPT_HEAD_MASTER", "RECIEPTHEADNO", "RHNAME", "RECIEPTHEADNO>0", "RECIEPTHEADNO");
            ddlRptShw.SelectedValue = hdn_fld.Value;
        }
    }


    private void BindListViewFeesHead()
    {
        try
        {
            FeesHeadController objFHC = new FeesHeadController();
            DataSet ds = objFHC.GetFeesHeads(ddlRecType.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblmsg.Text = null;
                pnlfees.Visible = true;
                lvFeesHead.DataSource = ds;
                lvFeesHead.DataBind();

                //Hide ISSchlorship column except 'TF'-----START
                Control ctrHeader = lvFeesHead.FindControl("thScholarship");
                if (ddlRecType.SelectedValue == "TF")
                {
                    ctrHeader.Visible = true;

                }
                else
                {
                    ctrHeader.Visible = false;
                }

                Control thReactivateStudent = lvFeesHead.FindControl("thReactivateStudent");
                thReactivateStudent.Visible = ddlRecType.SelectedValue == "TF" ? true : false;

                Control thIsCautionMoney = lvFeesHead.FindControl("thIsCautionMoney");
                thIsCautionMoney.Visible = ddlRecType.SelectedValue == "TF" ? true : false;

                foreach (ListViewItem lvRow in lvFeesHead.Items)
                {
                    Control BlockStat = (Control)lvRow.FindControl("tdScholarship");
                    if (ddlRecType.SelectedValue == "TF")
                    {
                        BlockStat.Visible = true;
                    }
                    else
                    {
                        BlockStat.Visible = false;
                    }

                    Control tdIsReactivateStudent = (Control)lvRow.FindControl("tdIsReactivateStudent");
                    tdIsReactivateStudent.Visible = ddlRecType.SelectedValue == "TF" ? true : false;

                    Control tdIsCautionMoney = (Control)lvRow.FindControl("tdIsCautionMoney");
                    tdIsCautionMoney.Visible = ddlRecType.SelectedValue == "TF" ? true : false;   
                     
                }
                   

                //Hide ISSchlorship column except 'TF'---END 

                 
               
                //string checkCurrency = string.Empty;
                foreach (ListViewDataItem itm in lvFeesHead.Items)
                {
                    Label txtHead = itm.FindControl("txtHead") as Label;
                    TextBox txtLName = itm.FindControl("txtLName") as TextBox;
                    TextBox txtSName = itm.FindControl("txtSName") as TextBox;
                    CheckBox chk = itm.FindControl("chkIsScholarship") as CheckBox;
                    CheckBox chkIsReactivateStudent = itm.FindControl("chkIsReactivateStudent") as CheckBox;
                    CheckBox ChkIsCautionMoney = itm.FindControl("ChkIsCautionMoney") as CheckBox;                 
                    DropDownList ddlRptShw = itm.FindControl("ddlReceiptShow") as DropDownList;

                    
                    if (ddlRecType.SelectedValue == "TF")
                    {
                        if (chk.ToolTip.Equals("1"))
                        {
                            chk.Checked = true;
                            if (chk.Checked == true)
                            {
                                txtLName.Enabled = false;
                                txtSName.Enabled = false;
                                ddlRptShw.Enabled = false; 
                            }
                        }

                        if (chkIsReactivateStudent.ToolTip.Equals("1"))
                        {
                            chkIsReactivateStudent.Checked = true;
                            if (chkIsReactivateStudent.Checked == true)
                            {
                                txtLName.Enabled = false;
                                txtSName.Enabled = false;
                                ddlRptShw.Enabled = false;
                            }
                        }

                        if (ChkIsCautionMoney.ToolTip.Equals("1"))
                        {
                            ChkIsCautionMoney.Checked = true;

                            if (ChkIsCautionMoney.Checked == true)
                            {
                                txtLName.Enabled = false;
                                txtSName.Enabled = false;
                                ddlRptShw.Enabled = false;
                            }
                        }

                    }

                    string count = objCommon.LookUp("ACD_CURRENCY_TITLE", "COUNT(1)", "RECIEPT_CODE='" + ddlRecType.SelectedValue + "' AND FEE_HEAD='" + txtHead.Text.ToString().Trim() + "'AND FEE_LONGNAME='" + txtLName.Text.ToString().Trim() + "' AND FEE_SHORTNAME='" + txtSName.Text.ToString().Trim() + "'");


                    //checkCurrency = objCommon.LookUp("ACD_CURRENCY_TITLE", "FEE_SHORTNAME", "RECIEPT_CODE='" + ddlRecType.SelectedValue + "' AND FEE_HEAD='" + txtHead.Text.ToString().Trim() + "'");
                    //if (checkCurrency.ToString().Equals(string.Empty) || checkCurrency.ToString().Equals(null))
                    //{
                    //    txtLName.Enabled = true;
                    //    txtSName.Enabled = true;
                    //    ddlRptShw.Enabled = true;
                    //}
                    //else
                    //{
                    //    txtLName.Enabled = false;
                    //    txtSName.Enabled = false;
                    //    ddlRptShw.Enabled = false;
                    //}


                    if (String.IsNullOrEmpty(txtLName.Text) && String.IsNullOrEmpty(txtSName.Text))
                    {
                        txtLName.Enabled = true;
                        txtSName.Enabled = true;
                        ddlRptShw.Enabled = true;
                    }
                    else if (count != "0")
                    {
                        txtLName.Enabled = false;
                        txtSName.Enabled = false;
                        ddlRptShw.Enabled = false;
                    }                      
                }

            }
            else
            {
                pnlfees.Visible = false;
                lblmsg.Text = "Sorry No Record Found..";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeHeads.BindListViewFeesHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("FeesHead", "rptFeesHeadReport.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString() + ",@P_RECIEPTCODE=" + ddlRecType.SelectedValue;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeesHead.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}