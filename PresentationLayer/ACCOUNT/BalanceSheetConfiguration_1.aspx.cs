//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : BALANCE SHEET CONFIGURATION
// CREATION DATE : 08-MARCH-2010                                                  
// CREATED BY    : JITENDRA CHILATE
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Drawing;
using IITMS.NITPRM;

public partial class BalanceSheetConfiguration : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
   static string fromdate = string.Empty;
   static string todate = string.Empty;
   static string isNetSurplus = string.Empty;
   public bool IsShowMsg = true; 

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        fromdate = Request.QueryString["obj"].Split(',')[0].ToString().Trim();
        todate = Request.QueryString["obj"].Split(',')[1].ToString().Trim();

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
                if (Session["comp_code"] == null || Session["fin_yr"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                }
                else { Session["comp_set"] = ""; }
                   
                //Page Authorization
                //CheckPageAuthorization();

                divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                Page.Title = Session["coll_name"].ToString();
                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}

                BindScheduleDefination();
                CreateBalanceSheetFormat();                            
            }
          
        }
        //divMsg.InnerHtml = string.Empty;
    }
    protected void CreateBalanceSheetFormat()
    {
        TrialBalanceReportController obj = new TrialBalanceReportController();
        obj.CallBalanceSheetFormatProc(isNetSurplus,"Y");
    
    }
    protected void BindScheduleDefination()
    {
        string code_year = Session["comp_code"].ToString();// +"_" + Session["fin_yr"].ToString();
        rptSchDef.DataSource = null;
        rptSchDef.DataBind();
        rptSchDef1.DataSource = null;
        rptSchDef1.DataBind();
        BindLiabilitySide();
        BindAssetSide();
    }

    protected DataSet SetProfitLossNetDeficit(DataSet dsOp)
    {
        TrialBalanceReportController obj = new TrialBalanceReportController();
        DataSet dsPLOC = obj.GetProfitLossOpeningClosingBalance(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));
       
        if (dsOp != null)
        {

            if (dsOp.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToDouble(dsPLOC.Tables[0].Rows[0][1]) < 0)
                {
                    isNetSurplus = "N";

                    DataRow row;
                    row = dsOp.Tables[0].NewRow();
                    row["partyname"] = "NET DEFICIT";
                    row["mgrp_no"] = "17654";
                    row["prno"] = "0";
                    row["party_no"] = "0";

                    if (Convert.ToString(dsPLOC.Tables[0].Rows[0]["Opening"]).Trim() == "")
                    {
                        row["op_balance"] = "0";
                    }
                    else
                    {
                        row["op_balance"] = Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Opening"]);
                    }
                    if (Convert.ToString(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]).Trim() == "")
                    {
                        row["Cl_balance"] = "0";
                    }
                    else
                    {
                       // row["Cl_balance"] = (Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Cl"])) / 2;
                        row["Cl_balance"] = (Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]));
                    }
                    row["is_party"] = "0";


                    DataSet dsPLDC = obj.GetProfitLossDrCr(Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy").ToString(), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy").ToString(), Session["comp_code"].ToString().Trim());

                    if (dsPLDC != null)
                    {
                        if (dsPLDC.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Debit"]).Trim() == "")
                            {
                                row["Debit"] = 0;
                            }
                            else
                            {
                                row["Debit"] = (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Debit"]));
                            }

                            if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Credit"]).Trim() == "")
                            {
                                row["Credit"] = 0;
                            }
                            else
                            {
                                row["Credit"] = (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Credit"]));
                            }
                            row["fano"] = "2";
                            row["CounterId"] = "100000";
                            row["lindex"] = "0";
                            //row["position"] = "";
                            //row["Schedule"] = "";
                            //row["CCode"] = Session["comp_code"].ToString().Trim();
                        }
                    }

                    dsOp.Tables[0].Rows.Add(row);
                }

            }
        }

        return dsOp;
        
    }
    protected DataSet SetProfitLossNetSurplus(DataSet dsOp)
    {
        TrialBalanceReportController obj = new TrialBalanceReportController();
        DataSet dsPLOC = obj.GetProfitLossOpeningClosingBalance(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));

        if (dsOp != null)
        {

            if (dsOp.Tables[0].Rows.Count > 0)
            {

                if (Convert.ToDouble(dsPLOC.Tables[0].Rows[0][1]) > 0)
                {
                    isNetSurplus = "Y";

                    DataRow row;
                    row = dsOp.Tables[0].NewRow();
                    row["partyname"] = "NET SURPLUS";
                    row["mgrp_no"] = "17655";
                    row["prno"] = "0";
                    row["party_no"] = "0";

                    if (Convert.ToString(dsPLOC.Tables[0].Rows[0]["Opening"]).Trim() == "")
                    {
                        row["op_balance"] = "0";
                    }
                    else
                    {
                        row["op_balance"] = Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Opening"]);
                    }
                    if (Convert.ToString(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]).Trim() == "")
                    {
                        row["Cl_balance"] = "0";
                    }
                    else
                    {
                        //row["Cl_balance"] = (Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["Cl"])) / 2;
                        row["Cl_balance"] = (Convert.ToDouble(dsPLOC.Tables[0].Rows[0]["ClosingTransaction"]));
                    }
                    row["is_party"] = "0";


                    DataSet dsPLDC = obj.GetProfitLossDrCr(Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy").ToString(), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy").ToString(), Session["comp_code"].ToString().Trim());

                    if (dsPLDC != null)
                    {
                        if (dsPLDC.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Debit"]).Trim() == "")
                            {
                                row["Debit"] = 0;
                            }
                            else
                            {

                                row["Debit"] = (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Debit"]));
                            }

                            if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Credit"]).Trim() == "")
                            {
                                row["Credit"] = 0;
                            }
                            else
                            {
                                row["Credit"] = (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Credit"]));
                            }
                            row["fano"] = "1";
                            row["CounterId"] = "100000";
                            row["lindex"] = "0";
                            //row["position"] = "";
                            //row["Schedule"] = "";
                            //row["CCode"] = Session["comp_code"].ToString().Trim();
                        }
                    }

                    dsOp.Tables[0].Rows.Add(row);

                }
            }
        }

        return dsOp;

    }

    protected void BindLiabilitySide()
    {

        //for liability side============
        DataSet dsOp;
        string present = string.Empty;

        dsOp = objCommon.FillDropDown("TEMP_SHEDULE_LIABILITY", "*", "", " IS_PARTY=0 AND fano=1 and  CCode= '" + Session["comp_code"].ToString().Trim() + "'", "counterid");
        if (dsOp != null)
        {
            if (dsOp.Tables[0].Rows.Count > 0)
            {
                present = "Y";
            }
            else
            {
                dsOp = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "*", "", " IS_PARTY=0 AND fano=1", "counterid");
                dsOp = SetProfitLossNetSurplus(dsOp);
            }
        }
        else
        {
            dsOp = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "*", "", " IS_PARTY=0 AND fano=1", "counterid");
            dsOp = SetProfitLossNetSurplus(dsOp);
        
        }

       
         if (dsOp != null)
        {
            if (dsOp.Tables[0].Rows.Count > 0)
            {
                rptSchDef.DataSource = dsOp.Tables[0];
                rptSchDef.DataBind();
                if (rptSchDef.Rows.Count > 0)
                {
                    int i = 0;
                    for (i = 0; i < rptSchDef.Rows.Count; i++)
                    {
                        HiddenField hidmgrpno = rptSchDef.Rows[i].FindControl("hid_mgrpno") as HiddenField;
                        hidmgrpno.Value = dsOp.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim();

                        HiddenField hidprno = rptSchDef.Rows[i].FindControl("hid_prno") as HiddenField;
                        hidprno.Value = dsOp.Tables[0].Rows[i]["prno"].ToString().Trim();

                        HiddenField hidpartyno = rptSchDef.Rows[i].FindControl("hid_partyno") as HiddenField;
                        hidpartyno.Value = dsOp.Tables[0].Rows[i]["party_NO"].ToString().Trim();

                        TextBox lblprt = rptSchDef.Rows[i].FindControl("lblparty") as TextBox;
                        lblprt.Text = dsOp.Tables[0].Rows[i]["partyname"].ToString();

                        if (present == "Y")
                        {
                            TextBox txtp = rptSchDef.Rows[i].FindControl("txt_position") as TextBox;
                            txtp.Text = dsOp.Tables[0].Rows[i]["position"].ToString();

                            TextBox txtS = rptSchDef.Rows[i].FindControl("txt_sch") as TextBox;
                            txtS.Text = dsOp.Tables[0].Rows[i]["Schedule"].ToString();

                            HiddenField headno = rptSchDef.Rows[i].FindControl("hid_headno") as HiddenField;
                            headno.Value = dsOp.Tables[0].Rows[i]["HEADNO"].ToString().Trim();

                        }
                                               


                    }


                }




            }




        }
    
    
    }
    protected void BindAssetSide()
    {

        //for asset side============

        DataSet dsOp;
        string present = string.Empty;

        dsOp = objCommon.FillDropDown("TEMP_SHEDULE_ASSET", "*", "", " IS_PARTY=0 AND fano=2 and CCode= '" + Session["comp_code"].ToString().Trim() + "'", "counterid");
        if (dsOp != null)
        {
            if (dsOp.Tables[0].Rows.Count > 0)
            {
                present = "Y";
            }
            else
            {
                dsOp = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "*", "", " IS_PARTY=0 AND fano=2", "counterid");
                dsOp = SetProfitLossNetDeficit(dsOp);
            }
        }
        else
        {
            dsOp = objCommon.FillDropDown("TEMP_TRIAL_BALANCE_REPORT_FORMAT", "*", "", " IS_PARTY=0 AND fano=2", "counterid");
            dsOp = SetProfitLossNetDeficit(dsOp);
        }

              
        if (dsOp != null)
        {
            if (dsOp.Tables[0].Rows.Count > 0)
            {
                rptSchDef1.DataSource = dsOp.Tables[0];
                rptSchDef1.DataBind();
                if (rptSchDef.Rows.Count > 0)
                {
                    int i = 0;
                    for (i = 0; i < rptSchDef1.Rows.Count; i++)
                    {
                        HiddenField hidmgrpno = rptSchDef1.Rows[i].FindControl("hid_mgrpno") as HiddenField;
                        hidmgrpno.Value = dsOp.Tables[0].Rows[i]["MGRP_NO"].ToString().Trim();

                        HiddenField hidprno = rptSchDef1.Rows[i].FindControl("hid_prno") as HiddenField;
                        hidprno.Value = dsOp.Tables[0].Rows[i]["prno"].ToString().Trim();

                        HiddenField hidpartyno = rptSchDef1.Rows[i].FindControl("hid_partyno") as HiddenField;
                        hidpartyno.Value = dsOp.Tables[0].Rows[i]["party_NO"].ToString().Trim();

                        TextBox lblprt = rptSchDef1.Rows[i].FindControl("lblparty") as TextBox;
                        lblprt.Text = dsOp.Tables[0].Rows[i]["partyname"].ToString();


                        if (present == "Y")
                        {
                            TextBox txtp = rptSchDef1.Rows[i].FindControl("txt_position") as TextBox;
                            txtp.Text = dsOp.Tables[0].Rows[i]["position"].ToString();

                            TextBox txtS = rptSchDef1.Rows[i].FindControl("txt_sch") as TextBox;
                            txtS.Text = dsOp.Tables[0].Rows[i]["Schedule"].ToString();

                            HiddenField headno = rptSchDef1.Rows[i].FindControl("hid_headno") as HiddenField;
                            headno.Value = dsOp.Tables[0].Rows[i]["HEADNO"].ToString().Trim();

                        }

                       

                    }
                }


            }




        }


    }
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(Request.Url.ToString());
    //}
    protected void rptSchDef_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        rptSchDef.PageIndex = e.NewPageIndex;
        rptSchDef.DataBind();
    }
    protected void rptSchDef1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        rptSchDef1.PageIndex = e.NewPageIndex;
        rptSchDef1.DataBind();
    }
    
    #region User Defined Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SchedularDefining.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SchedularDefining.aspx");
        }
    }
    #endregion

    //protected void rptSchDef_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    int i = 0;
    //    for (i = 0; i < rptSchDef.Rows.Count; i++)
    //    {
    //      TextBox txtpos=  rptSchDef.Rows[i].FindControl("txt_position") as TextBox;

    //      HiddenField hidmgrp = rptSchDef.Rows[i].FindControl("hid_mgrpno") as HiddenField;
    //      HiddenField hidpr = rptSchDef.Rows[i].FindControl("hid_prno") as HiddenField;
    //      txtpos.Attributes.Add("onblur", "LoadPosition('" + hidmgrp.Value.ToString() + "','" + hidpr.Value.ToString() + "');");
         
    //    }

    // }
    protected void SetPosition(string value3,int i)
    {
        string setpos = string.Empty;
        int k = 0;
        for (k = 0; k < rptSchDef.Rows.Count; k++)
        {
            HiddenField hid4 = rptSchDef.Rows[Convert.ToInt16(k)].FindControl("hid_prno") as HiddenField;

            if (value3.ToString().Trim() == hid4.Value.ToString().Trim())
            {

                TextBox textValue2 = rptSchDef.Rows[Convert.ToInt16(k)].FindControl("txt_position") as TextBox;
                TextBox textValue3 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_position") as TextBox;
                textValue2.Text = textValue3.Text;
                HiddenField hid5 = rptSchDef.Rows[Convert.ToInt16(k)].FindControl("hid_mgrpno") as HiddenField;
                SetPosition(hid5.Value.ToString().Trim(), k);
                setpos = "Y";
            }

        }
        if (k == rptSchDef.Rows.Count)
        {
            if (setpos != "Y")
            {
                TextBox textValue4 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_position") as TextBox;
                textValue4.Enabled = false;
            
            }
        
        }
    
    }
    protected void SetPositionAsset(string value3, int i)
    {
        string setpos = string.Empty;
        int k = 0;
        for (k = 0; k < rptSchDef1.Rows.Count; k++)
        {
            HiddenField hid4 = rptSchDef1.Rows[Convert.ToInt16(k)].FindControl("hid_prno") as HiddenField;

            if (value3.ToString().Trim() == hid4.Value.ToString().Trim())
            {

                TextBox textValue2 = rptSchDef1.Rows[Convert.ToInt16(k)].FindControl("txt_position") as TextBox;
                TextBox textValue3 = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("txt_position") as TextBox;
                textValue2.Text = textValue3.Text;
                HiddenField hid5 = rptSchDef1.Rows[Convert.ToInt16(k)].FindControl("hid_mgrpno") as HiddenField;
                SetPositionAsset(hid5.Value.ToString().Trim(), k);
                setpos = "Y";
            }

        }
        if (k == rptSchDef1.Rows.Count)
        {
            if (setpos != "Y")
            {
                TextBox textValue4 = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("txt_position") as TextBox;
                textValue4.Enabled = false;

            }

        }

    }
    protected void SetSchedule(string value3, int i)
    {
        string setpos = string.Empty;
        int k = 0;
        for (k = 0; k < rptSchDef.Rows.Count; k++)
        {
            HiddenField hid4 = rptSchDef.Rows[Convert.ToInt16(k)].FindControl("hid_prno") as HiddenField;

            if (value3.ToString().Trim() == hid4.Value.ToString().Trim())
            {

                TextBox textValue2 = rptSchDef.Rows[Convert.ToInt16(k)].FindControl("txt_sch") as TextBox;
                TextBox textValue3 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_sch") as TextBox;
                textValue2.Text = textValue3.Text;
                HiddenField hid5 = rptSchDef.Rows[Convert.ToInt16(k)].FindControl("hid_mgrpno") as HiddenField;
                SetPosition(hid5.Value.ToString().Trim(), k);
                setpos = "Y";
            }

        }
        //if (k == rptSchDef.Rows.Count)
        //{
        //    if (setpos != "Y")
        //    {
        //        TextBox textValue4 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_sch") as TextBox;
        //        textValue4.Enabled = false;

        //    }

        //}

    }
    protected void SetScheduleAsset(string value3, int i)
    {
        string setpos = string.Empty;
        int k = 0;
        for (k = 0; k < rptSchDef1.Rows.Count; k++)
        {
            HiddenField hid4 = rptSchDef1.Rows[Convert.ToInt16(k)].FindControl("hid_prno") as HiddenField;

            if (value3.ToString().Trim() == hid4.Value.ToString().Trim())
            {

                TextBox textValue2 = rptSchDef1.Rows[Convert.ToInt16(k)].FindControl("txt_sch") as TextBox;
                TextBox textValue3 = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("txt_sch") as TextBox;
                textValue2.Text = textValue3.Text;
                HiddenField hid5 = rptSchDef1.Rows[Convert.ToInt16(k)].FindControl("hid_mgrpno") as HiddenField;
                SetScheduleAsset(hid5.Value.ToString().Trim(), k);
                setpos = "Y";
            }

        }
        //if (k == rptSchDef.Rows.Count)
        //{
        //    if (setpos != "Y")
        //    {
        //        TextBox textValue4 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_sch") as TextBox;
        //        textValue4.Enabled = false;
        //    }
        //}

    }
    protected void txt_position_TextChanged(object sender, EventArgs e)
    {
    TextBox t = sender as TextBox;
    string  id= t.ClientID;
        
      string[] ids =  id.Split('_');
      if (ids.Length > 0)
      {
          string idstr = ids[3].Remove(0, 3).ToString();
          HiddenField hid1 = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_mgrpno") as HiddenField;
          TextBox textValueChk = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_position") as TextBox;
          HiddenField headno = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_headno") as HiddenField;
          headno.Value = "10";
           int l = 0;
           for (l = 0; l < rptSchDef.Rows.Count; l++)
           {

               if (Convert.ToInt16(idstr) - 2 != l)
               {

                   TextBox textValueCur = rptSchDef.Rows[Convert.ToInt16(l)].FindControl("txt_position") as TextBox;

                   if (CheckPosition(textValueChk.Text.ToString().Trim(), textValueCur.Text.ToString().Trim()) == false)
                   {
                       objCommon.DisplayMessage(upd, "Position No. Allready Available, Try Another. ", this);
                       textValueChk.Focus();
                       return;

                   }
               }

           }



         int i = 0;
         for (i = 0; i < rptSchDef.Rows.Count; i++)
         {
             HiddenField hid2 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("hid_prno") as HiddenField;
             if (hid1.Value.ToString().Trim() == hid2.Value.ToString().Trim())
             {
                 TextBox textValue = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_position") as TextBox;
                 TextBox textValue1 = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_position") as TextBox;
                 textValue.Text = textValue1.Text;
                
                 HiddenField hid3 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("hid_mgrpno") as HiddenField;
                SetPosition(hid3.Value.ToString().Trim(), i);
             }
         }
      }

    }
    private Boolean CheckPosition(string EnteredPosition,string PresentPosition )
    {
        if (EnteredPosition == PresentPosition)
        {
            return false;

        }
        else
        {
            return true;
        }
    
    }
    protected void txt_sch_TextChanged(object sender, EventArgs e)
    {

        TextBox t = sender as TextBox;
        string id = t.ClientID;

        string[] ids = id.Split('_');
        if (ids.Length > 0)
        {
            string idstr = ids[3].Remove(0, 3).ToString();
            HiddenField hid1 = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_mgrpno") as HiddenField;

            TextBox textValueChk = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_Sch") as TextBox;
            int l = 0;
            for (l = 0; l < rptSchDef.Rows.Count; l++)
            {

                if (Convert.ToInt16(idstr) - 2 != l)
                {

                    TextBox textValueCur = rptSchDef.Rows[Convert.ToInt16(l)].FindControl("txt_Sch") as TextBox;

                    if (CheckPosition(textValueChk.Text.ToString().Trim(), textValueCur.Text.ToString().Trim()) == false)
                    {
                        objCommon.DisplayMessage(upd, "Shedule No. Allready Available, Try Another. ", this);
                        textValueChk.Focus();
                        return;

                    }
                }

            }

            int x = 0;
            for (x = 0; x < rptSchDef1.Rows.Count; x++)
            {

                if (Convert.ToInt16(idstr) - 2 != x)
                {

                    TextBox textValueCur = rptSchDef1.Rows[Convert.ToInt16(x)].FindControl("txt_Sch") as TextBox;

                    if (CheckPosition(textValueChk.Text.ToString().Trim(), textValueCur.Text.ToString().Trim()) == false)
                    {
                        objCommon.DisplayMessage(upd, "Shedule No. Allready Available, Try Another. ", this);
                        textValueChk.Focus();
                        return;

                    }
                }

            }
            int i = 0;
            for (i = 0; i < rptSchDef.Rows.Count; i++)
            {
                HiddenField hid2 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("hid_prno") as HiddenField;
                if (hid1.Value.ToString().Trim() == hid2.Value.ToString().Trim())
                {
                    TextBox textValue = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("txt_sch") as TextBox;
                    TextBox textValue1 = rptSchDef.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_sch") as TextBox;
                    textValue.Text = textValue1.Text;

                    HiddenField hid3 = rptSchDef.Rows[Convert.ToInt16(i)].FindControl("hid_mgrpno") as HiddenField;
                    SetSchedule(hid3.Value.ToString().Trim(), i);
                }
            }
        }
    }

    protected void txt_position_TextChanged1(object sender, EventArgs e)
    {
        TextBox t = sender as TextBox;
        string id = t.ClientID;

        string[] ids = id.Split('_');
        if (ids.Length > 0)
        {
            string idstr = ids[3].Remove(0, 3).ToString();
            HiddenField hid1 = rptSchDef1.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_mgrpno") as HiddenField;
            TextBox textValueChk = rptSchDef1.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_position") as TextBox;
            HiddenField headno = rptSchDef1.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_headno") as HiddenField;
            headno.Value = "10";
            int l = 0;
            for (l = 0; l < rptSchDef1.Rows.Count; l++)
            {
                if (Convert.ToInt16(idstr) - 2 != l)
                {
                    TextBox textValueCur = rptSchDef1.Rows[Convert.ToInt16(l)].FindControl("txt_position") as TextBox;

                    if (CheckPosition(textValueChk.Text.ToString().Trim(), textValueCur.Text.ToString().Trim()) == false)
                    {
                        objCommon.DisplayMessage(upd, "Position No. Allready Available, Try Another. ", this);
                        textValueChk.Focus();
                        return;
                    }
                }
            }

            int i = 0;
            for (i = 0; i < rptSchDef1.Rows.Count; i++)
            {
                HiddenField hid2 = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("hid_prno") as HiddenField;
                if (hid1.Value.ToString().Trim() == hid2.Value.ToString().Trim())
                {
                    TextBox textValue = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("txt_position") as TextBox;
                    TextBox textValue1 = rptSchDef1.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_position") as TextBox;
                    textValue.Text = textValue1.Text;

                    HiddenField hid3 = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("hid_mgrpno") as HiddenField;
                    SetPositionAsset(hid3.Value.ToString().Trim(), i);
                }
            }
        }

    }
    protected void txt_sch_TextChanged1(object sender, EventArgs e)
    {
        TextBox t = sender as TextBox;
        string id = t.ClientID;

        string[] ids = id.Split('_');
        if (ids.Length > 0)
        {
            string idstr = ids[3].Remove(0, 3).ToString();
            HiddenField hid1 = rptSchDef1.Rows[Convert.ToInt16(idstr) - 2].FindControl("hid_mgrpno") as HiddenField;

            TextBox textValueChk = rptSchDef1.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_Sch") as TextBox;
            int l = 0;
            for (l = 0; l < rptSchDef1.Rows.Count; l++)
            {

                if (Convert.ToInt16(idstr) - 2 != l)
                {
                    TextBox textValueCur = rptSchDef1.Rows[Convert.ToInt16(l)].FindControl("txt_Sch") as TextBox;

                    if (CheckPosition(textValueChk.Text.ToString().Trim(), textValueCur.Text.ToString().Trim()) == false)
                    {
                        objCommon.DisplayMessage(upd, "Shedule No. Allready Available, Try Another. ", this);
                        textValueChk.Focus();
                        return;

                    }
                }

            }

           int x = 0;
            for (x = 0; x < rptSchDef.Rows.Count; x++)
            {
                if (Convert.ToInt16(idstr) - 2 != x)
                {
                    TextBox textValueCur = rptSchDef.Rows[Convert.ToInt16(x)].FindControl("txt_Sch") as TextBox;

                    if (CheckPosition(textValueChk.Text.ToString().Trim(), textValueCur.Text.ToString().Trim()) == false)
                    {
                        objCommon.DisplayMessage(upd, "Shedule No. Allready Available, Try Another. ", this);
                        textValueChk.Focus();
                        return;
                    }
                }

            }

            int i = 0;
            for (i = 0; i < rptSchDef1.Rows.Count; i++)
            {
                HiddenField hid2 = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("hid_prno") as HiddenField;
                if (hid1.Value.ToString().Trim() == hid2.Value.ToString().Trim())
                {
                    TextBox textValue = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("txt_sch") as TextBox;
                    TextBox textValue1 = rptSchDef1.Rows[Convert.ToInt16(idstr) - 2].FindControl("txt_sch") as TextBox;
                    textValue.Text = textValue1.Text;

                    HiddenField hid3 = rptSchDef1.Rows[Convert.ToInt16(i)].FindControl("hid_mgrpno") as HiddenField;
                    SetScheduleAsset(hid3.Value.ToString().Trim(), i);
                }
            }
        }

    }
    protected void btnSet_Click(object sender, EventArgs e)
    {
        if (rptSchDef.Rows.Count > 0 && rptSchDef1.Rows.Count > 0)
        { 
         //Set Liability Side
            int i = 0;
            for (i = 0; i < rptSchDef.Rows.Count; i++)
            {
                TextBox txtpos = rptSchDef.Rows[i].FindControl("txt_position") as TextBox;
                TextBox txtsch = rptSchDef.Rows[i].FindControl("txt_sch") as TextBox;
                HiddenField hdnmgrp = rptSchDef.Rows[i].FindControl("hid_mgrpno") as HiddenField;
                HiddenField hdnprno = rptSchDef.Rows[i].FindControl("hid_prno") as HiddenField;
                HiddenField hdHeadNo = rptSchDef.Rows[i].FindControl("hid_headno") as HiddenField;
                if (Convert.ToString(txtpos.Text).Trim() == "")
                {
                    txtpos.Text = "0";
                }
                if (Convert.ToString(hdHeadNo.Value).Trim() == "")
                {
                    hdHeadNo.Value = "0";
                }
                if (Convert.ToString(txtsch.Text).Trim() == "")
                {
                    txtsch.Text = "0";
                }

                //obj1.SetBalanceSheetFormat(Convert.ToInt16(hdnmgrp.Value), Convert.ToInt16(hdnprno.Value), Convert.ToInt16(txtpos.Text), Convert.ToInt16(txtsch.Text), Convert.ToInt16(hdHeadNo.Value));
                TrialBalanceReportController obj1 = new TrialBalanceReportController();
                if (hdnmgrp.Value.ToString().Trim() == "17655")
                {
                    //pl account
                    DataSet dstemp = new DataSet();
                    TrialBalanceReportController obj = new TrialBalanceReportController();
                    DataSet dsplac = obj.GetProfitLossOpeningClosingBalance(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));
                    if (dsplac != null)
                    {
                        if (dsplac.Tables[0].Rows.Count > 0)
                        {
                            double op = 0;
                            double cl = 0;
                            double cr = 0;
                            double dr = 0;
                            if (Convert.ToString(dsplac.Tables[0].Rows[0]["Opening"]).Trim() == "")
                            {
                                op = 0;
                            }
                            else
                            {
                                op = (Convert.ToDouble(dsplac.Tables[0].Rows[0]["Opening"]));
                            }
                            if (Convert.ToString(dsplac.Tables[0].Rows[0]["ClosingTransaction"]).Trim() == "")
                            {
                                cl = 0;
                            }
                            else
                            {
                                //cl = (Convert.ToDouble(dsplac.Tables[0].Rows[0]["Cl"])) / 2;
                                cl = (Convert.ToDouble(dsplac.Tables[0].Rows[0]["ClosingTransaction"]));
                            }

                            DataSet dsPLDC = obj.GetProfitLossDrCr(Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy").ToString(), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy").ToString(), Session["comp_code"].ToString().Trim());

                            if (dsPLDC != null)
                            {
                                if (dsPLDC.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Debit"]).Trim() == "")
                                    {
                                        dr = 0;
                                    }
                                    else
                                    {
                                        dr = (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Debit"]));
                                    }
                                    if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Credit"]).Trim() == "")
                                    {
                                        cr = 0;
                                    }
                                    else
                                    {
                                        cr = (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Credit"]));
                                    }
                                }
                            }
                            obj1.SetBalanceSheetFormatForPLAccount(Convert.ToInt16(hdnmgrp.Value), Convert.ToInt16(hdnprno.Value), Convert.ToInt16(txtpos.Text), Convert.ToInt16(txtsch.Text), "Y", Convert.ToDouble(op), Convert.ToDouble(cl), Convert.ToDouble(dr), Convert.ToDouble(cr));
                        }

                    }
                }
                else
                {
                    obj1.SetBalanceSheetFormat(Convert.ToInt16(hdnmgrp.Value), Convert.ToInt16(hdnprno.Value), Convert.ToInt16(txtpos.Text), Convert.ToInt16(txtsch.Text), Convert.ToInt16(hdHeadNo.Value));
                }
            }

            //Set Asset Side
            i = 0;
            for (i = 0; i < rptSchDef1.Rows.Count; i++)
            {
                TextBox txtpos = rptSchDef1.Rows[i].FindControl("txt_position") as TextBox;
                TextBox txtsch = rptSchDef1.Rows[i].FindControl("txt_sch") as TextBox;
                HiddenField hdnmgrp = rptSchDef1.Rows[i].FindControl("hid_mgrpno") as HiddenField;
                HiddenField hdnprno = rptSchDef1.Rows[i].FindControl("hid_prno") as HiddenField;
                HiddenField hdHeadNo = rptSchDef1.Rows[i].FindControl("hid_headno") as HiddenField;
                if (Convert.ToString(txtpos.Text).Trim() == "")
                {
                    txtpos.Text = "0";

                }
                if (Convert.ToString(hdHeadNo.Value).Trim() == "")
                {
                    hdHeadNo.Value = "0";

                }
                if (Convert.ToString(txtsch.Text).Trim() == "")
                {
                    txtsch.Text = "0";

                }
                TrialBalanceReportController obj1 = new TrialBalanceReportController();
                if (hdnmgrp.Value.ToString().Trim() == "17654")
                {
                    //pl account
                    DataSet dstemp=new DataSet();
                    TrialBalanceReportController obj = new TrialBalanceReportController();
                    DataSet dsplac = obj.GetProfitLossOpeningClosingBalance(Session["comp_code"].ToString().Trim(), Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy"), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy"));
                    if (dsplac != null)
                    {
                        if (dsplac.Tables[0].Rows.Count > 0)
                        {
                            double op = 0;
                            double cl = 0;
                            double cr = 0;
                            double dr = 0;
                            if (Convert.ToString(dsplac.Tables[0].Rows[0]["Opening"]).Trim() == "")
                            {
                                op = 0;
                            }
                            else
                            {
                                op = (Convert.ToDouble(dsplac.Tables[0].Rows[0]["Opening"]));
                            }
                            if (Convert.ToString(dsplac.Tables[0].Rows[0]["ClosingTransaction"]).Trim() == "")
                            {
                                cl = 0;
                            }
                            else
                            {
                                //cl = (Convert.ToDouble(dsplac.Tables[0].Rows[0]["Cl"]) )/2;
                                cl = (Convert.ToDouble(dsplac.Tables[0].Rows[0]["ClosingTransaction"]));
                            }

                             DataSet dsPLDC = obj.GetProfitLossDrCr(Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy").ToString(), Convert.ToDateTime(todate).ToString("dd-MMM-yyyy").ToString(), Session["comp_code"].ToString().Trim());

                             if (dsPLDC != null)
                             {
                                 if (dsPLDC.Tables[0].Rows.Count > 0)
                                 {
                                     if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Debit"]).Trim() == "")
                                     {
                                        dr= 0;
                                     }
                                     else
                                     {

                                        dr= (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Debit"]) );
                                     }

                                     if (Convert.ToString(dsPLDC.Tables[0].Rows[0]["Credit"]).Trim() == "")
                                     {
                                         cr = 0;
                                     }
                                     else
                                     {
                                         cr = (Convert.ToDouble(dsPLDC.Tables[0].Rows[0]["Credit"]) );
                                     }
                                 }
                             }

                            obj1.SetBalanceSheetFormatForPLAccount(Convert.ToInt16(hdnmgrp.Value), Convert.ToInt16(hdnprno.Value), Convert.ToInt16(txtpos.Text), Convert.ToInt16(txtsch.Text), "Y", Convert.ToDouble(op), Convert.ToDouble(cl), Convert.ToDouble(dr), Convert.ToDouble(cr));
                        
                        }
                    
                    }

                }
                else
                {
                    obj1.SetBalanceSheetFormat(Convert.ToInt16(hdnmgrp.Value), Convert.ToInt16(hdnprno.Value), Convert.ToInt16(txtpos.Text), Convert.ToInt16(txtsch.Text), Convert.ToInt16(hdHeadNo.Value));
                }
                
            }
            TrialBalanceReportController obj2 = new TrialBalanceReportController();
            obj2.ArrangeBalanceSheet(Session["comp_code"].ToString().Trim());
            ArrangeExactBalanceSheet();
            if(IsShowMsg!=false)
                objCommon.DisplayMessage(upd, "Balance Sheet Format Set Successfully, To View Report Please Click On Show Button.", this);
            
                    
        }


    }

    protected void ArrangeExactBalanceSheet()
    { 
        //for Liability side
        DataSet dsOp = objCommon.FillDropDown("TEMP_BALANACESHEET_LIABILITY", "*", "",string.Empty, "counterid");
        DeleteExtraShedules(dsOp);
        //for Asset side
        DataSet dsOp1 = objCommon.FillDropDown("TEMP_BALANACESHEET_ASSET", "*", "", string.Empty, "counterid");
        DeleteExtraShedules(dsOp1);
    }

    protected void DeleteExtraShedules(DataSet dsext)
    {
        if (dsext != null)
        {
            if (dsext.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                for(i=0;i<dsext.Tables[0].Rows.Count;i++)
                {
                if (Convert.ToString(dsext.Tables[0].Rows[i]["Schedule"]).Trim() != "0")
                {
                    int j = 0;
                    int x = 0;
                    if (Convert.ToString(dsext.Tables[0].Rows[i]["Schedule"]).Trim() != "")
                    {
                        x = Convert.ToInt16(Convert.ToString(dsext.Tables[0].Rows[i]["Schedule"]).Trim());
                    }
                    
                    for (j = 0; j < dsext.Tables[0].Rows.Count; j++)
                    {
                        int y = 0;
                        if (Convert.ToString(dsext.Tables[0].Rows[j]["Schedule"]).Trim() != "")
                        {
                            y = Convert.ToInt16(Convert.ToString(dsext.Tables[0].Rows[j]["Schedule"]).Trim());
                        }

                        if (Convert.ToInt16(x) == Convert.ToInt16(y))
                        {
                            int k = 0;
                            for (k = 0; k < dsext.Tables[0].Rows.Count; k++)
                            {
                                int z = 0;
                                if (Convert.ToString(dsext.Tables[0].Rows[k]["Schedule"]).Trim() != "")
                                {
                                    z =Convert.ToInt16(Convert.ToString(dsext.Tables[0].Rows[k]["Schedule"]).Trim());
                                }

                                if (Convert.ToInt16(x) == Convert.ToInt16(z))
                                {
                                    if (Convert.ToInt16(dsext.Tables[0].Rows[i]["MGRP_NO"]) == Convert.ToInt16(dsext.Tables[0].Rows[k]["PRNO"]))
                                    {
                                        //delete query
                                        TrialBalanceReportController o = new TrialBalanceReportController();
                                        o.DeleteSchedules(Convert.ToInt16(dsext.Tables[0].Rows[i]["MGRP_NO"]), Convert.ToInt16(dsext.Tables[0].Rows[k]["Schedule"]));
                                     }
                                 }
                            }
                        
                        }
                    
                    
                    }
                
                }
            
                }
            }

        }
    
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ACCOUNT/AccountingVouchersModifications.aspx?pageno=349");
       
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        //ShowBalanceSheetRpt("BALANCESHEET", "MainBalanceSheet.rpt");

        IsShowMsg = false;
        
        //To set balancesheet
        btnSet_Click(sender, e);
        ShowBalanceSheetRpt1("BALANCESHEET", "MainBalanceSheet.rpt");
    }

    private void ShowBalanceSheetRpt1(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@@CollegeName=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@@FinYear=" + Session["fin_yr"].ToString().Trim() + "," + "@@AsOnDate=" + todate.ToString().Trim() + "," + "@FromDate=" + fromdate.ToString().Trim().ToUpper() + "," + "@@DuringDate=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@UptoDate=" + todate.ToString().Trim();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowBalanceSheet -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //notinuse
    private void ShowBalanceSheetRpt(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string CollegeName ="M.T.E. SOCITEY'S WALCHAND COLLEGE OF ENGINEERING SANGLI";
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,ACCOUNT," + rptFileName;
            //url += "&param=@@CollegeName=" + CollegeName.ToString() + "," + "@@FinYear=" + Session["fin_yr"].ToString().Trim() + "," + "@@AsOnDate=" + todate.ToString().Trim() + "," + "@FromDate=" + fromdate.ToString().Trim().ToUpper() + "," + "@@DuringDate=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@UptoDate="+ todate.ToString().Trim();

            //Script1 += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            //ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script1, true);

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@@CollegeName=" + CollegeName.ToString() + "," + "@@FinYear=" + Session["fin_yr"].ToString().Trim() + "," + "@@AsOnDate=" + todate.ToString().Trim() + "," + "@FromDate=" + fromdate.ToString().Trim().ToUpper() + "," + "@@DuringDate=" + fromdate.ToString().Trim().ToUpper() + " to " + todate.ToString().Trim() + "," + "@UptoDate=" + todate.ToString().Trim();

           // Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            //string newWin = "window.open('" + url + "');";
            //ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

            //ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>openNewWin('" + url + "')</script>");

            //string fullURL = "window.open('" + url + "', '_blank', 'height=500,width=800,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true); 


            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

            //ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Report", Script, true);
            //ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Report", Script1, true);
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Report", "Script1", true);

            Response.Redirect(url);

            //string newWin = "window.open('" + url + "');";
            //ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowBalanceSheet -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSchedule_Click(object sender, EventArgs e)
    {
        IsShowMsg = false;
        //To set balancesheet
        btnSet_Click(sender, e);
        ShowBalanceSheetScheduleRpt("ScheduleReport","ScheduleReport.rpt");
    }

    private void ShowBalanceSheetScheduleRpt(string reportTitle, string rptFileName)
    {
        try
        {
            string sch = string.Empty;

            string Script1 = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            if (txtschedule.Text.ToString().Trim() == "")
            {
                sch = "0";
            }
            else
            {
                sch = txtschedule.Text.ToString().Trim();
            }
           
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@V_SCHNO=" + sch.ToString();

            Script1 += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.upd, upd.GetType(), "Reports", Script1, true);
            //Response.Redirect(url);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountingVouchers.ShowBalanceSheetScheduleRpt -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        TrialBalanceReportController oref = new TrialBalanceReportController();
        oref.RefreshBalanceSheet(Session["comp_code"].ToString().Trim());
        BindScheduleDefination();
        CreateBalanceSheetFormat();    
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    