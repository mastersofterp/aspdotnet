// =============================================
// MODIFY BY   :  MRUNAL SINGH
// MODIFY DATE :  26-AUG-2015
// Description : 
// ==================================================
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
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;


public partial class ESTATE_Master_quatertype_master : System.Web.UI.Page
{
    Common objCommon = new Common();
    QuaterType_Master objquaterType = new QuaterType_Master();
    QuaterTypeMasterControlle objqutercontroller = new QuaterTypeMasterControlle();
   
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["userno"] == null || Session["username"] == null ||
                      Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    CheckPageAuthorization();
                    ViewState["action"] = "add";
                    BindQuarterMaster();
                }
                objCommon.FillDropDownList(ddlEStaff, "EST_ENTITLE_STAFF", "STAFFID", "STAFF_TYPE", "", "STAFFID");
                divMsg.InnerHtml = string.Empty;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

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

    #region Quater

    protected Boolean funDuplicate()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("EST_QRT_TYPE", "*"," ", "QUARTER_TYPE='"+txtquartertype.Text+"'","");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtquartertype.Text))
            {
                objquaterType.QrtType = txtquartertype.Text.Trim();
                if (txtRent.Text != string.Empty)
                {
                    objquaterType.Rent = Convert.ToInt16(txtRent.Text.Trim());                    
                }
                else
                {
                    objquaterType.Rent = 0;
                }

                if (txtqArea.Text != string.Empty)
                {
                    objquaterType.QArea = txtqArea.Text.Trim();
                }
                else
                {
                    objquaterType.QArea = "0";
                }
                objquaterType.eStaff = Convert.ToInt32(ddlEStaff.SelectedValue);
                objquaterType.fixedCharge = Convert.ToDouble(txtfixedcharge.Text);

                    if (ViewState["action"].Equals("add"))
                {
                    if (funDuplicate() == true)
                    {
                        funClear();
                        objCommon.DisplayMessage(updqtrtypemaster, "Record Already Exist.", this.Page);
                        return;                        
                    }
                    
                    objquaterType.QrtTypeNo = 0;
                    CustomStatus cs = (CustomStatus)objqutercontroller.AddQuarterType(objquaterType);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindQuarterMaster();
                        objCommon.DisplayMessage(updqtrtypemaster, "Record Save Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updqtrtypemaster, "Sorry! Try Again.", this.Page);
                    }
                }
                if (ViewState["action"].Equals("edit"))
                {
                    if (funDuplicate() == true)
                    {
                        DataSet ds = new DataSet();
                        ds = objCommon.FillDropDown("EST_QRT_TYPE", "*", " ", "QNO='" + ViewState["QNO"].ToString() + "'", "");
                        if (txtquartertype.Text != ds.Tables[0].Rows[0]["QUARTER_TYPE"].ToString().Trim())
                        {
                            objCommon.DisplayMessage(updqtrtypemaster, "Record Already Exist.", this.Page);
                            txtquartertype.Focus();
                            funClear();
                            return;
                        }
                    }
                    objquaterType.QrtTypeNo = Convert.ToInt16(ViewState["QNO"]);

                    CustomStatus cs = (CustomStatus)objqutercontroller .AddQuarterType(objquaterType);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindQuarterMaster();
                        objCommon.DisplayMessage(updqtrtypemaster, "Record Updated Successfully.", this.Page);
                        
                    }
                    else
                    {
                        objCommon.DisplayMessage(updqtrtypemaster, "Sorry! Try Again.", this.Page);

                    }

                }

            }
            else
            {
                objCommon.DisplayMessage(updqtrtypemaster, "Please Enter Quarter Type.", this.Page);
            }
            funClear();
        }
            
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void BindQuarterMaster()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("EST_QRT_TYPE QT INNER JOIN EST_ENTITLE_STAFF ES ON (QT.STAFFID = ES.STAFFID)", "QT.QNO", "QT.QUARTER_TYPE, QT.RENT,QAREA, ES.STAFF_TYPE, QT.FIXED_CHARGE", "QT.QNO>0", "QT.QNO");
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                fldmetertype.Visible = true;
                Repeater_metertype.DataSource = ds;
                Repeater_metertype.DataBind();
            }
            else
            {
                fldmetertype.Visible = false;
                Repeater_metertype.DataSource = null;
                Repeater_metertype.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void funClear()
    {
        ViewState["action"] = "add";
        txtqArea.Text = string.Empty;
        txtquartertype.Text = string.Empty;
        txtRent.Text = string.Empty;
        ddlEStaff.SelectedIndex = 0;
        txtfixedcharge.Text = string.Empty;
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        funClear();
    }

    protected void Repeater_metertype_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            int QNO = Convert.ToInt32(e.CommandArgument);
            ViewState["QNO"] = QNO;
            ViewState["action"] = "edit";
            DataSet ds = objCommon.FillDropDown("EST_QRT_TYPE", "QNO", "QUARTER_TYPE, RENT, QAREA, STAFFID, FIXED_CHARGE", "QNO=" + QNO, "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtquartertype.Text = ds.Tables[0].Rows[0]["QUARTER_TYPE"].ToString();
                txtRent.Text = ds.Tables[0].Rows[0]["RENT"].ToString();
                txtqArea.Text = ds.Tables[0].Rows[0]["QAREA"].ToString();
                ddlEStaff.SelectedValue = ds.Tables[0].Rows[0]["STAFFID"].ToString();
                txtfixedcharge.Text = ds.Tables[0].Rows[0]["FIXED_CHARGE"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    #endregion

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTATE," + rptFileName;
            url += "&param=@p_college_code=" + Session["colcode"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updqtrtypemaster, this.updqtrtypemaster.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        ShowReport("Quarter Type Master", "rptQuarterTypeMaster.rpt");
    }

}
