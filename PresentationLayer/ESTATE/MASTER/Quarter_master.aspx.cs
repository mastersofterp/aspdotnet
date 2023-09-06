using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;



public partial class ESTATE_Master_Quarter_master : System.Web.UI.Page
{

    Common objCommon                     = new Common();
    Quater_Master objquater              = new Quater_Master();
    QuaterMasterController objquaterCntr = new QuaterMasterController();

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
                    //objCommon.FillDropDownList(ddlcustomernumber, "EST_METERTYPE_MST", "MTYPE_NO", "METER_TYPE", "ID=" + Convert.ToInt16(1), "MTYPE_NO");
                     //o objCommon.FillDropDownList(ddlmeternumber, "EST_METERNO_MST", "M_ID", "METER_NO", "M_ID>=0", "M_ID");
                    // bjCommon.FillDropDownList(ddlmeternumber, "EST_METERNO_MST", "M_ID", "METER_NO", "M_ID>=0", "M_ID");

                    binddroplist();
                    objCommon.FillDropDownList(ddlquaterType, "EST_QRT_TYPE", "QNO", "QUARTER_TYPE", "QNO>0", "QNO");
                    objCommon.FillDropDownList(ddlBlock, "EST_BLOCK_MASTER", "BLOCKID", "BLOCKNAME", "BLOCKID>0", "BLOCKID");
                    BindQuarterMst();
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

    protected void binddropdownlistedit()
    {
        try
        {

            //objCommon.FillDropDownList(ddlmeternumber, "EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=1)", "A.M_ID", "A.METER_NO +'-'+B.METER_TYPE", "A.M_ID>0", "A.M_ID");
            //objCommon.FillDropDownList(ddlwatermeter, "EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=2 )", "A.M_ID", "A.METER_NO +'-'+B.METER_TYPE", "A.M_ID>0", "A.M_ID");
            objCommon.FillDropDownList(ddlmeternumber, "EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=1 )", "A.M_ID", "A.METER_NO +'-'+B.METER_TYPE", "A.M_ID>0", "A.M_ID");
            objCommon.FillDropDownList(ddlwatermeter,  "EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=2 )", "A.M_ID", "A.METER_NO +'-'+B.METER_TYPE", "A.M_ID>0", "A.M_ID");

        }
        catch
        {
        }
    }

    protected void binddroplist()
    {
        try
        {

            objCommon.FillDropDownList(ddlmeternumber, "EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=1 and A.M_ID not in(select  E_meter_no  from EST_QRT_MST) )", "A.M_ID", "A.METER_NO +'-'+B.METER_TYPE", "A.M_ID>0", "A.M_ID");
            objCommon.FillDropDownList(ddlwatermeter, "EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=2  and A.M_ID not in(select  W_METER_NO  from EST_QRT_MST))", "A.M_ID", "A.METER_NO +'-'+B.METER_TYPE", "A.M_ID>0", "A.M_ID");
        }
        catch
        {
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
        ds = objCommon.FillDropDown("EST_QRT_MST", "*"," ", "QUARTER_NO='"+txtquarternumber.Text+"'","");

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
            //if (!string.IsNullOrEmpty(txtquarternumber.Text) && !string.IsNullOrEmpty(txtquarterName.Text))
            //{   
               
                objquater.QrtType        = Convert.ToInt16(ddlquaterType.SelectedItem.Value);
                objquater.QrtNo          = txtquarternumber.Text.Trim();
                objquater.QrtName        = txtquarterName.Text.Trim();
                objquater.MeterNumber    = Convert.ToInt16(ddlmeternumber.SelectedItem.Value);
                objquater.WaterNumber    = Convert.ToInt16(ddlwatermeter.SelectedItem.Value);
                objquater.block          = Convert.ToInt32(ddlBlock.SelectedValue);
                objquater.ConnectedLoad  =  txtConLoad.Text.Trim() == string.Empty ? 0 :  Convert.ToInt32(txtConLoad.Text.Trim());
                
                if (ViewState["action"].Equals("add"))
                {
                    if (funDuplicate() == true)
                    {
                        funclear();
                        objCommon.DisplayMessage(updquater, "Record Already Exist.", this.Page);
                        return;
                        
                    }
                    objquater.QId = 0;
                    CustomStatus cs = (CustomStatus)objquaterCntr.AddQuarter(objquater);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        binddroplist();
                        BindQuarterMst();
                        objCommon.DisplayMessage(updquater, "Record Save Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updquater, "Sorry! Try Again.", this.Page);

                    }
                }
                if (ViewState["action"].Equals("edit"))
                {
                    if (funDuplicate() == true)
                    {
                        //DataSet ds = new DataSet();
                        //ds = objCommon.FillDropDown("EST_QRT_MST", "*", " ", "IDNO='" + ViewState["QId"].ToString() + "'", "");
                        //if (txtquarternumber.Text != ds.Tables[0].Rows[0]["QUARTER_NO"].ToString().Trim())
                        //{
                        //    objCommon.DisplayMessage(updquater, "Records Already Exist!", this.Page);
                        //    txtquarternumber.Focus();
                        //    funclear();
                        //    return;
                        //}
                    }
                    objquater.QId = Convert.ToInt16(ViewState["QId"]);

                    CustomStatus cs = (CustomStatus)objquaterCntr.AddQuarter(objquater);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        binddroplist();
                        BindQuarterMst();
                        objCommon.DisplayMessage(updquater, "Record Updated Successfully.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updquater, "Sorry! Try Again.", this.Page);

                    }
                }
                funclear();
            //}
            //else
            //{
            //    objCommon.DisplayMessage(updquater, "Please Enter material Type.", this.Page);
            //}

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void funclear()
    {
        ddlwatermeter.ClearSelection();
        ddlmeternumber.ClearSelection();
        ddlquaterType.ClearSelection();
        ddlBlock.SelectedIndex = 0;
        //txtquarter.Text       = string.Empty;
        txtquarternumber.Text = string.Empty;
        txtquarterName.Text = string.Empty;
        ViewState["action"] = "add";
        binddroplist();
        txtConLoad.Text = string.Empty;
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        funclear();
    }
    
    protected void BindQuarterMst()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("EST_QRT_MST A INNER JOIN EST_QRT_TYPE B ON(A.QNO=B.QNO) LEFT JOIN EST_METERNO_MST C ON(A.E_METER_NO =C.M_ID) INNER JOIN EST_BLOCK_MASTER BM ON (A.BLOCKID = BM.BLOCKID)", "A.QNO, BM.BLOCKNAME", "A.QUARTER_NO,A.QRTNAME,C.METER_NO AS ENERGY,B.QUARTER_TYPE,A.IDNO, A.CONNECTED_LOAD", "A.QNO>0", "A.QNO");
                               
            //DataSet ds = objCommon.FillDropDown("EST_QRT_MST A INNER JOIN EST_QRT_TYPE B ON(A.QNO=B.QNO)inner join EST_METERNO_MST c on(a.E_METER_NO =c.M_ID)inner join EST_METERNO_MST d on (a.w_METER_NO =d.M_ID )", "A.QNO","A.QUARTER_NO,A.QRTNAME,C.METER_NO as energy,d.METER_NO as water,B.QUARTER_TYPE,A.IDNO", "A.QNO>0", "A.QNO");
            
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

    protected void Repeater_metertype_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataSet ds = null;
        try
        {
             //binddropdownlistedit();

             int QtNo = Convert.ToInt32(e.CommandArgument);
             ViewState["QId"] = QtNo;
             ViewState["action"] = "edit";

             ds = objCommon.FillDropDown("EST_QRT_MST", "IDNO", "QNO, QUARTER_NO, QRTNAME, E_METER_NO, W_METER_NO, BLOCKID, CONNECTED_LOAD", "IDNO=" + QtNo, "IDNO");

            // ds = objCommon.FillDropDown("EST_QRT_MST a inner join EST_METERTYPE_MST b on (a.E_METER_NO =b.MTYPE_NO)inner  join EST_METERTYPE_MST c on (a.W_METER_NO  =c.MTYPE_NO)", " a.IDNO", "a.QNO,a.QUARTER_NO,a.QRTNAME,a.E_METER_NO,a.W_METER_NO, b.METER_TYPE as energy ,c.METER_TYPE as water", "a.idno=" + QtNo, "a.idno");

             if (ds != null && ds.Tables[0].Rows.Count > 0)
             {
                
                 ddlquaterType.SelectedValue          = ds.Tables[0].Rows[0]["QNO"].ToString();
                 txtquarternumber.Text                = ds.Tables[0].Rows[0]["QUARTER_NO"].ToString();
                 txtquarterName.Text                  = ds.Tables[0].Rows[0]["QRTNAME"].ToString();
                 
               
                 objCommon.FillDropDownList(ddlmeternumber, "EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=1)", "A.M_ID", "A.METER_NO +'-'+B.METER_TYPE", "A.M_ID not in(select  E_meter_no  from EST_QRT_MST where E_meter_no not in(" + ds.Tables[0].Rows[0]["E_METER_NO"].ToString() + "))", "A.M_ID");
                 objCommon.FillDropDownList(ddlwatermeter, "EST_METERNO_MST A INNER JOIN EST_METERTYPE_MST B ON (A.MTYPE_NO =B.MTYPE_NO AND B.ID=2)", "A.M_ID", "A.METER_NO +'-'+B.METER_TYPE", "A.M_ID not in(select  W_METER_NO  from EST_QRT_MST where W_METER_NO not in (" + ds.Tables[0].Rows[0]["W_METER_NO"].ToString() + "))", "A.M_ID");

                 ddlmeternumber.SelectedValue = ds.Tables[0].Rows[0]["E_METER_NO"].ToString();
                 ddlwatermeter.SelectedValue = ds.Tables[0].Rows[0]["W_METER_NO"].ToString();

                 ddlBlock.SelectedValue = ds.Tables[0].Rows[0]["BLOCKID"].ToString();
                 txtConLoad.Text = ds.Tables[0].Rows[0]["CONNECTED_LOAD"].ToString();
                        
             }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    #endregion

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
    //        ScriptManager.RegisterClientScriptBlock(this.updquater, this.updquater.GetType(), "controlJSScript", sb.ToString(), true);

    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //    }
    //}

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
            ScriptManager.RegisterClientScriptBlock(this.updquater, this.updquater.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    protected void btnReport_Click1(object sender, EventArgs e)
    {
        ShowReport("Quarter Master", "rptQuarterMaster.rpt");
    }
}
 