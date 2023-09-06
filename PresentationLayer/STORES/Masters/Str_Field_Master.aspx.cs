//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_Field_Master.aspx                                                  
// CREATION DATE : 01-Sept-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Stores_Masters_Str_Field_Master : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objStrMaster = new StoreMasterController();

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
                this.BindListViewField();
                ViewState["action"] = "add";              

            }
            //Set Report Parameters
           // objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Field_Master_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }


    private void BindListViewField()
    {
        try
        {
            DataSet ds = objStrMaster.GetAllFields();
            lvField.DataSource = ds;
            lvField.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Field_Master.BindListViewField-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            StoreMaster objStrMst = new StoreMaster();
            objStrMst.FNAME = txtFieldName.Text;
            objStrMst.FTYPE = Convert.ToChar(ddlType.SelectedValue);
            objStrMst.FSRNO = Convert.ToInt32(txtsrno.Text);
            objStrMst.COLLEGE_CODE = Session["colcode"].ToString();
            if (radIndia.Checked)
                objStrMst.IND_FOR = Convert.ToChar("I");

            if (radForegin.Checked)
                objStrMst.IND_FOR = Convert.ToChar("F");

            if (chkCalOnBasicAmt.Checked)
                objStrMst.ADDED_IN_BASIC = true;
            else
                objStrMst.ADDED_IN_BASIC = false;
            if (chkDeductTax.Checked)
                objStrMst.DEDUCT_IN_BASIC = true;
            else
                objStrMst.DEDUCT_IN_BASIC = false;

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_FIELDMASTER", " count(*)", "fsrno=" + Convert.ToInt32(txtsrno.Text) + "and ftype='" + Convert.ToString(ddlType.SelectedValue) + "'"));

                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objStrMaster.AddField(objStrMst);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            this.Clear();
                            objCommon.DisplayMessage(updpnlMain, "Record Saved Successfully", this);
                            this.BindListViewField();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlMain, "Record Already Exist", this);
                    }
                }

                else
                {
                    if (ViewState["fNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_FIELDMASTER", " count(*)", "fsrno=" + Convert.ToInt32(txtsrno.Text) + "and ftype='" + Convert.ToString(ddlType.SelectedValue) + "' and fno <> " + Convert.ToInt32(ViewState["fNo"].ToString())));

                        if (duplicateCkeck == 0)
                        {

                            objStrMst.FNO = Convert.ToInt32(ViewState["fNo"].ToString());
                            CustomStatus csupd = (CustomStatus)objStrMaster.UpdateFiled(objStrMst);
                            if (csupd.Equals(CustomStatus.RecordUpdated))
                            {
                                ViewState["action"] = "add";
                                this.Clear();
                                objCommon.DisplayMessage(updpnlMain, "Record Updated Successfully", this);
                                this.BindListViewField();
                            }
                            
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlMain, "Record Already Exist", this);
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {
            if (ex.Message.Contains("Unique Key Violation"))
            {
                objCommon.DisplayMessage(updpnlMain,"Record Already Exist", Page);
            }
            else
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Stores_Masters_Str_Field_Master.butSubDepartment_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    
    

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["fNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            radIndia.Checked  = false;
            radForegin.Checked = false;
            ShowEditDetailsField(Convert.ToInt32(ViewState["fNo"].ToString()));
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Field_Master.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowEditDetailsField(int fNo)
    {
        DataSet ds = null;

        try
        {
            //ds = objStrMaster.GetSingleRecordField(fNo);
            ds = objCommon.FillDropDown("STORE_FIELDMASTER", "FNAME,FTYPE,FSRNO,IND_FOR,ADDED_IN_BASIC,TAX_DEDUCTED", "FNO", "FNO=" + fNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlType.SelectedValue = ds.Tables[0].Rows[0]["FTYPE"].ToString();
                txtFieldName.Text = ds.Tables[0].Rows[0]["FNAME"].ToString();
                txtsrno.Text = ds.Tables[0].Rows[0]["FSRNO"].ToString();
                if (ds.Tables[0].Rows[0]["IND_FOR"].ToString() == "I")
                    radIndia.Checked=true;

                if (ds.Tables[0].Rows[0]["IND_FOR"].ToString() == "F")
                    radForegin.Checked = true;

                if (ds.Tables[0].Rows[0]["ADDED_IN_BASIC"].ToString() == "True")
                    chkCalOnBasicAmt.Checked = true;

                if (ds.Tables[0].Rows[0]["TAX_DEDUCTED"].ToString() == "True")
                    chkDeductTax.Checked = true;

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Field_Master.ShowEditDetailsField-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    protected void butCancel_Click(object sender, EventArgs e)
    {
        //Clear();
        Response.Redirect(Request.Url.ToString());
    }

    protected void Clear()
    {
        ddlType.SelectedValue = "0";
        txtFieldName.Text = string.Empty;
        txtsrno.Text = string.Empty;
        
        chkCalOnBasicAmt.Checked = false;
        chkDeductTax.Checked = false;
        radForegin.Checked = false;
        radIndia.Checked = true;
        ViewState["action"] = "add";
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewField();
    }




    protected void btnshowrpt_Click(object sender, EventArgs e)
    {
        ShowReport("Field Master REPORT", "Field_Master_Report.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"] + "," + "@UserName=" + Session["username"];
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


            // objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Grand_Master_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
