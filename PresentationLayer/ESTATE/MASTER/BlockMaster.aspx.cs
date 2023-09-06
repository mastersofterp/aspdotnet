//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ESTATE                           
// CREATION DATE : 25-SEP-2017                                                        
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 
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

public partial class ESTATE_Master_BlockMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    BlockMaster objBlock = new BlockMaster();
    NABlockController objBlockController = new NABlockController();

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
                    BindBlock();
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

    protected void BindBlock()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("EST_BLOCK_MASTER", "BLOCKID", "BLOCKNAME", "BLOCKID>0", "BLOCKID");
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                fldmetertype.Visible = true;
                Repeater_block.DataSource = ds;
                Repeater_block.DataBind();
            }
            else
            {
                fldmetertype.Visible = false;
                Repeater_block.DataSource = null;
                Repeater_block.DataBind();
            }
        }
        catch (Exception ex)
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
        ds = objCommon.FillDropDown("EST_BLOCK_MASTER", "*", " ", "BLOCKNAME='" + txtBlock.Text + "'", "");

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
            if (!string.IsNullOrEmpty(txtBlock.Text))
            {
                objBlock.BLOCK_NAME = txtBlock.Text;

                if (ViewState["action"].Equals("add"))
                {
                    if (funDuplicate() == true)
                    {
                        objCommon.DisplayMessage(updBlockmaster, "Record Already Exist.", this.Page);
                        funclear();
                        return;
                    }
                    objBlock.BLOCKNO = 0;
                    CustomStatus cs = (CustomStatus)objBlockController.AddUpdateBlock(objBlock);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindBlock();
                        objCommon.DisplayMessage(updBlockmaster, "Record Save Successfully.", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(updBlockmaster, "Record Already Exist.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updBlockmaster, "Sorry!Try Again.", this.Page);
                    }
                }
                if (ViewState["action"].Equals("edit"))
                {
                    //if (funDuplicate() == true)
                    //{
                    //    objCommon.DisplayMessage(updBlockmaster, "Record Already Exist.", this.Page);
                    //    funclear();
                    //    return;
                    //}
                    objBlock.BLOCKNO = Convert.ToInt16(ViewState["BLOCKNO"]);

                    CustomStatus cs = (CustomStatus)objBlockController.AddUpdateBlock(objBlock);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindBlock();
                        objCommon.DisplayMessage(updBlockmaster, "Record Updated Successfully.", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(updBlockmaster, "Record Already Exist.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updBlockmaster, "Sorry!Try Again.", this.Page);
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updBlockmaster, "Please Enter Block Name.", this.Page);
            }
            funclear();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    protected void Repeater_block_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            int BLOCKNO = Convert.ToInt32(e.CommandArgument);
            ViewState["BLOCKNO"] = BLOCKNO;
            ViewState["action"] = "edit";
            DataSet ds = objCommon.FillDropDown("EST_BLOCK_MASTER", "BLOCKID", "BLOCKNAME", "BLOCKID=" + BLOCKNO, "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtBlock.Text = ds.Tables[0].Rows[0]["BLOCKNAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void funclear()
    {
        ViewState["action"] = "add";
        txtBlock.Text = string.Empty;
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        funclear();
    }


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

            ScriptManager.RegisterClientScriptBlock(this.updBlockmaster, this.updBlockmaster.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    protected void btnreport_Click1(object sender, EventArgs e)
    {
        ShowReport("Block Master", "rptBlockMaster.rpt");
    }
}