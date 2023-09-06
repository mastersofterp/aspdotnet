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
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

public partial class ESTATE_Master_ConsumerTypeMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    ConsumerTypeMaster objconsumer = new ConsumerTypeMaster();
    ConsumerTypeMasterCont objconsumercont = new ConsumerTypeMasterCont();

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
                    BindMaterialMaster();
                }
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

    protected void BindMaterialMaster()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("EST_CONSUMERTYPE_MASTER", "IDNO", "CONSUMERTYPE_NAME", "IDNO>0", "IDNO");
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                Repeater_consumertype.DataSource = ds;
                Repeater_consumertype.DataBind();
            }
            else
            {
                Repeater_consumertype.DataSource = null;
                Repeater_consumertype.DataBind();
            }
  
        }
        catch(Exception ex)       
        {
            Console.WriteLine(ex.ToString());
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
    
    protected Boolean funDuplicate()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("EST_CONSUMERTYPE_MASTER", "*", " ", "CONSUMERTYPE_NAME='" + txtconsumertype.Text + "'", "");

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
            if (!string.IsNullOrEmpty(txtconsumertype.Text))
            {
                objconsumer.ConsumerType = txtconsumertype.Text;

                if (ViewState["action"].Equals("add"))
                {
                    if (funDuplicate() == true)
                    {
                        objCommon.DisplayMessage(updConsumerTypeMaster, "Record Already Exist.", this.Page);
                        funclear();
                        return;
                    }
                    objconsumer.ConsumerTypeNo =0;
                    CustomStatus cs = (CustomStatus)objconsumercont.AddConsumerType(objconsumer);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindMaterialMaster();
                        objCommon.DisplayMessage(updConsumerTypeMaster,"Record Save Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updConsumerTypeMaster,"Sorry!Try Again.", this.Page);

                    }
                }
                if (ViewState["action"].Equals("edit"))
                {
                    //if (funDuplicate() == true)
                    //{
                    //    objCommon.DisplayMessage(updConsumerTypeMaster, "Record Already Exist.", this.Page);
                    //    funclear();
                    //    return;
                    //}
                    objconsumer.ConsumerTypeNo = Convert.ToInt16(ViewState["MNO"]);

                    CustomStatus cs = (CustomStatus)objconsumercont.AddConsumerType(objconsumer);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindMaterialMaster();
                        objCommon.DisplayMessage(updConsumerTypeMaster, "Record Updated Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updConsumerTypeMaster, "Sorry! Try Again.", this.Page);
                    }
                }

            }
            else
            {
                objCommon.DisplayMessage(updConsumerTypeMaster, "Please Enter Consumer Type.", this.Page);
            }
            funclear();
        }
        catch (Exception ex)
        {

        }
    }
    
    protected void Repeater_consumertype_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            int MNO = Convert.ToInt32(e.CommandArgument);
            ViewState["MNO"] = MNO;
            ViewState["action"] = "edit";
            DataSet ds = objCommon.FillDropDown("EST_CONSUMERTYPE_MASTER", "IDNO", "CONSUMERTYPE_NAME", "IDNO=" +MNO, "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtconsumertype.Text = ds.Tables[0].Rows[0]["CONSUMERTYPE_NAME"].ToString();
            }

        }
        catch(Exception  ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
    
    protected void funclear()
    {
        ViewState["action"] = "add";
        txtconsumertype.Text = string.Empty;
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        funclear();
    }

    //private void ShowReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,ESTATE," + rptFileName;
    //        url += "&param=@p_college_code=" + Session["colcode"].ToString();

    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";

    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        ScriptManager.RegisterClientScriptBlock(this.updConsumerTypeMaster, this.updConsumerTypeMaster.GetType(), "controlJSScript", sb.ToString(), true);

    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //    }
    //}

    //protected void btnreport_Click(object sender, EventArgs e)
    //{
    //    ShowReport("Consumer Type Master", "rptConsumerTypeMaster.rpt");
    //}
}
