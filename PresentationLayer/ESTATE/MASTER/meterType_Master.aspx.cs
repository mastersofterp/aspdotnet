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

public partial class ESTATE_Master_meterType_Master : System.Web.UI.Page
{
    MeterTypeName objMeterTypeName = new MeterTypeName();
    Common objCommon = new Common();

    MeterTypeNamecontroller objmetertypecontroller = new MeterTypeNamecontroller();
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
                    objCommon.FillDropDownList(ddltype, "EST_METER_MST", "ID", "METER_HEAD", "ID>0", "ID");
                    ddltype.SelectedIndex = 1;
                    BindMeterTypeMaster();
                }

            }
            
            divMsg.InnerHtml = string.Empty;
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

    #region Meter

    protected Boolean funDuplicate()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("EST_METERTYPE_MST", "*", " ", "METER_TYPE='" + txtmetertypeName.Text + "' and id="+ddltype.SelectedValue.ToString() , "");

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
            if (!string.IsNullOrEmpty(txtmetertypeName.Text) && ddltype.SelectedIndex != 0)
            {
                objMeterTypeName.MeterTypeName1 = txtmetertypeName.Text.Trim();
                objMeterTypeName.MeterTypeNo = Convert.ToInt16(ddltype.SelectedItem.Value);
                if (ViewState["action"].Equals("add"))
                {
                    if (funDuplicate() == true)
                    {
                        funClear();
                        objCommon.DisplayMessage(updpnlmetertype, "Record Already Exist.", this.Page);
                        return;
                       
                    }
                    objMeterTypeName.Mtype = 0;
                    CustomStatus cs = (CustomStatus)objmetertypecontroller.AddMeterType(objMeterTypeName);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindMeterTypeMaster();
                        objCommon.DisplayMessage(updpnlmetertype, "Record Save Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlmetertype, "Sorry!Try Again.", this.Page);
                    }
                }
                if (ViewState["action"].Equals("edit"))
                {
                    //if (funDuplicate() == true)
                    //{
                    //    DataSet ds = new DataSet();
                    //    ds = objCommon.FillDropDown("EST_METERTYPE_MST", "*", " ", "METER_TYPE='" + txtmetertypeName.Text + "' and Id=" + ddltype.SelectedValue.ToString(), "");
                    //    if (ds != null && ds.Tables[0].Rows.Count >0)
                    //    {
                    //        //if (txtmetertypeName.Text != ds.Tables[0].Rows[0]["METER_TYPE"].ToString().Trim())
                    //        //{
                    //            objCommon.DisplayMessage(updpnlmetertype, "Record Already Exist.", this.Page);
                    //            txtmetertypeName.Focus();
                    //            funClear();
                    //            return;
                    //        //}
                    //    }
                    //}
                    objMeterTypeName.Mtype = Convert.ToInt16(ViewState["Meterno"]);

                    CustomStatus cs = (CustomStatus)objmetertypecontroller.AddMeterType(objMeterTypeName);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindMeterTypeMaster();
                        objCommon.DisplayMessage(updpnlmetertype, "Record Updated Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlmetertype, "Sorry! Try Again.", this.Page);
                    }
                }
                funClear();
            }
            else
            {
                objCommon.DisplayMessage(updpnlmetertype, "Please Enter Meter Type.", this.Page);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void BindMeterTypeMaster()
    {
        try {

            DataSet ds = objCommon.FillDropDown("EST_METERTYPE_MST A INNER JOIN EST_METER_MST B  ON (A.ID =B.ID)", "a.MTYPE_NO", "  A.METER_TYPE,B.METER_HEAD", "A.MTYPE_NO>0", "A.MTYPE_NO");
            if(ds.Tables[0]!=null&&ds.Tables[0].Rows.Count>0)
            {
                fldmetertype.Visible =true;
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

    protected void Repeater_metertype_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            int Meterno = Convert.ToInt32(e.CommandArgument);
            ViewState["Meterno"] = Meterno;
            ViewState["action"] = "edit";
            
            DataSet ds = objCommon.FillDropDown("EST_METERTYPE_MST", "MTYPE_NO", "ID,METER_TYPE", "MTYPE_NO=" + Meterno, "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtmetertypeName.Text    = ds.Tables[0].Rows[0]["METER_TYPE"].ToString();
                ddltype.SelectedValue    = ds.Tables[0].Rows[0]["ID"].ToString();
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
        txtmetertypeName.Text = string.Empty;
        ddltype.ClearSelection();
        ddltype.SelectedIndex = 1;
    }


    protected void btnreset_Click(object sender, EventArgs e)
    {
        funClear();
    }

    #endregion

    protected void btnreport_Click(object sender, EventArgs e)
    {
        ShowReport("Meter Type Master", "rptMeterTypeMaster.rpt");
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTATE," + rptFileName;
            url += "&param=@p_college_code=" + Session["colcode"].ToString() ;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlmetertype, this.updpnlmetertype.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

}
