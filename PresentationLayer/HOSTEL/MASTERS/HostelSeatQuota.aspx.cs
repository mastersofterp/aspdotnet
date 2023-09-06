//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : THIS IS A Hostel Seat Quota Master                                     
// CREATION DATE : 19 March 2013
// CREATED BY    : ASHISH MOTGHARE                               
// MODIFIED BY   : ASHISH MOTGHARE
// MODIFIED DATE : 21 March 2013
// MODIFIED BY   : ASHISH MOTGHARE
// MODIFIED DATE : 22 March 2013
// MODIFIED BY   : ASHISH MOTGHARE
// MODIFIED DATE : 23 March 2013
//=================================================================================
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
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
public partial class HOSTEL_MASTERS_HostelSeatQuota : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    SeatQuotaController objSeatQuota = new SeatQuotaController();
    SeatQuota objSeatQuotaEntity = new SeatQuota();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Fill Dropdown lists                
                   
                    this.objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO desc");
                   
                
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_MASTERS_HostelSeatQuota.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HostelSeatQuota.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HostelSeatQuota.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           objSeatQuotaEntity.BatchNo = Convert.ToInt32(ddlAdmbatch.SelectedValue);
           foreach (ListViewDataItem dataItem in lvQuota.Items)
           {
               Label AllIndiaQuotaNo = dataItem.FindControl("lblSeatQuotaAllIndia") as Label;
               Label StateLevelQuotaNo = dataItem.FindControl("lblSeatQuotaStateLevel") as Label;

               TextBox txtSQAllIndia_per = dataItem.FindControl("txtSeatQuota") as TextBox;
               TextBox txtSQStateLevel_per = dataItem.FindControl("txtSeatQuota1") as TextBox;


               TextBox txtAllIndia_GENPer = dataItem.FindControl("txtGeneral") as TextBox;
               TextBox txtStateLevel_GENPer = dataItem.FindControl("txtGeneral1") as TextBox;

               TextBox txtAllIndia_OBCPer = dataItem.FindControl("txtObc") as TextBox;
               TextBox txtStateLevel_OBCPer = dataItem.FindControl("txtObc1") as TextBox;

               TextBox txtAllIndia_SCPer = dataItem.FindControl("txtSc") as TextBox;
               TextBox txtStateLevel_SCPer = dataItem.FindControl("txtSc1") as TextBox;

               TextBox txtAllIndia_STPer = dataItem.FindControl("txtSt") as TextBox;
               TextBox txtStateLevel_STPer = dataItem.FindControl("txtSt1") as TextBox;

               TextBox txtAllIndia_NTPer = dataItem.FindControl("txtNt") as TextBox;
               TextBox txtStateLevel_NTPer = dataItem.FindControl("txtNt1") as TextBox;

               objSeatQuotaEntity.AllIndiaSQNo = Convert.ToInt32(AllIndiaQuotaNo.ToolTip);
               objSeatQuotaEntity.StateLevelSQNo = Convert.ToInt32(StateLevelQuotaNo.ToolTip);


               objSeatQuotaEntity.AllIndiaPer = Convert.ToDecimal(txtSQAllIndia_per.Text);
               objSeatQuotaEntity.StateLevelPer = Convert.ToDecimal(txtSQStateLevel_per.Text);

               if (txtAllIndia_GENPer.Text != string.Empty)
               {
                   objSeatQuotaEntity.AllIndia_GENPer = Convert.ToDecimal(txtAllIndia_GENPer.Text);
               }
               else
               {
                   objSeatQuotaEntity.AllIndia_GENPer = 0.0M;
               }
               if (txtStateLevel_GENPer.Text != string.Empty)
               {
                   objSeatQuotaEntity.StateLevel_GENPer = Convert.ToDecimal(txtStateLevel_GENPer.Text);
               }
               else
               {
                   objSeatQuotaEntity.StateLevel_GENPer = 0.0M;
               }
              
               if (txtAllIndia_OBCPer.Text != string.Empty)
               {
                   objSeatQuotaEntity.AllIndia_OBCPer = Convert.ToDecimal(txtAllIndia_OBCPer.Text);
               }
               else
               {
                   objSeatQuotaEntity.AllIndia_OBCPer = 0.0M;
               }
               if (txtStateLevel_OBCPer.Text != string.Empty)
               {
                   objSeatQuotaEntity.StateLevel_OBCPer = Convert.ToDecimal(txtStateLevel_OBCPer.Text);
               }
               else
               {
                   objSeatQuotaEntity.StateLevel_OBCPer = 0.0M;
               }
               
               if (txtAllIndia_SCPer.Text != string.Empty)
               {
                   objSeatQuotaEntity.AllIndia_SCPer = Convert.ToDecimal(txtAllIndia_SCPer.Text);
               }
               else
               {
                   objSeatQuotaEntity.AllIndia_SCPer = 0.0M;
               }
               if (txtStateLevel_SCPer.Text != string.Empty)
               {
                   objSeatQuotaEntity.StateLevel_SCPer = Convert.ToDecimal(txtStateLevel_SCPer.Text);
               }
               else
               {
                   objSeatQuotaEntity.StateLevel_SCPer = 0.0M;
               }
               
               if (txtAllIndia_STPer.Text != string.Empty)
               {
                   objSeatQuotaEntity.AllIndia_STPer = Convert.ToDecimal(txtAllIndia_STPer.Text);
               }
               else
               {
                   objSeatQuotaEntity.AllIndia_STPer = 0.0M;
               }
               if (txtStateLevel_STPer.Text != string.Empty)
               {
                   objSeatQuotaEntity.StateLevel_STPer = Convert.ToDecimal(txtStateLevel_STPer.Text);
               }
               else
               {
                   objSeatQuotaEntity.StateLevel_STPer = 0.0M;
               }

               
               if (txtAllIndia_NTPer.Text != string.Empty)
               {
                   objSeatQuotaEntity.AllIndia_NTPer = Convert.ToDecimal(txtAllIndia_NTPer.Text);
               }
               else
               {
                   objSeatQuotaEntity.AllIndia_NTPer = 0.0M;
               }
               if (txtStateLevel_NTPer.Text != string.Empty)
               {
                   objSeatQuotaEntity.StateLevel_NTPer = Convert.ToDecimal(txtStateLevel_NTPer.Text);
               }
               else
               {
                   objSeatQuotaEntity.StateLevel_NTPer = 0.0M;
               }
               
           }
          
           objSeatQuotaEntity.CollegeCode = Session["colcode"].ToString();
           CustomStatus cs = (CustomStatus)objSeatQuota.AddUpdateQuota(objSeatQuotaEntity);
           if (cs.Equals(CustomStatus.RecordSaved))
           {
               ShowMessage("Record Saved Successfully!!!");
           }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_MASTERS_HostelSeatQuota.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearControlContents();
    }
 
    private void ClearControlContents()
    {
       
        ddlAdmbatch.SelectedIndex = 0;
        lvQuota.Visible = false;
    }
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private void FillListView()
    {
        try
        {
            DataSet dsGetQuota = objCommon.FillDropDown("acd_hostel_quota", "QUOTA_NO", "BATCH_NO,QUOTA_PER,GEN_PER,OBC_PER,SC_PER,ST_PER,NT_PER", "BATCH_NO=" + Convert.ToInt32(ddlAdmbatch.SelectedValue), "QUOTA_NO");
            if (dsGetQuota != null & dsGetQuota.Tables.Count > 0 && dsGetQuota.Tables[0].Rows.Count > 0)
            {
                foreach (ListViewDataItem dataItem in lvQuota.Items)
                {
                        Label AllIndiaQuotaNo = dataItem.FindControl("lblSeatQuotaAllIndia") as Label;
                        Label StateLevelQuotaNo = dataItem.FindControl("lblSeatQuotaStateLevel") as Label;
                    
                        TextBox txtSQAllIndia_per = dataItem.FindControl("txtSeatQuota") as TextBox;
                        TextBox txtSQStateLevel_per = dataItem.FindControl("txtSeatQuota1") as TextBox;


                        TextBox txtAllIndia_GENPer = dataItem.FindControl("txtGeneral") as TextBox;
                        TextBox txtStateLevel_GENPer = dataItem.FindControl("txtGeneral1") as TextBox;

                        TextBox txtAllIndia_OBCPer = dataItem.FindControl("txtObc") as TextBox;
                        TextBox txtStateLevel_OBCPer = dataItem.FindControl("txtObc1") as TextBox;

                        TextBox txtAllIndia_SCPer = dataItem.FindControl("txtSc") as TextBox;
                        TextBox txtStateLevel_SCPer = dataItem.FindControl("txtSc1") as TextBox;

                        TextBox txtAllIndia_STPer = dataItem.FindControl("txtSt") as TextBox;
                        TextBox txtStateLevel_STPer = dataItem.FindControl("txtSt1") as TextBox;

                        TextBox txtAllIndia_NTPer = dataItem.FindControl("txtNt") as TextBox;
                        TextBox txtStateLevel_NTPer = dataItem.FindControl("txtNt1") as TextBox;
                        for (int i = 0; i < dsGetQuota.Tables[0].Rows.Count; i++)
                        {
                           
                                if (dsGetQuota.Tables[0].Rows[i]["QUOTA_NO"].ToString() == "1")
                                {
                                    txtSQAllIndia_per.Text = dsGetQuota.Tables[0].Rows[i]["QUOTA_PER"].ToString();
                                    txtAllIndia_GENPer.Text = dsGetQuota.Tables[0].Rows[i]["GEN_PER"].ToString();
                                    txtAllIndia_OBCPer.Text = dsGetQuota.Tables[0].Rows[i]["OBC_PER"].ToString();
                                    txtAllIndia_SCPer.Text = dsGetQuota.Tables[0].Rows[i]["SC_PER"].ToString();
                                    txtAllIndia_STPer.Text = dsGetQuota.Tables[0].Rows[i]["ST_PER"].ToString();
                                    txtAllIndia_NTPer.Text = dsGetQuota.Tables[0].Rows[i]["NT_PER"].ToString();

                                }
                                if (dsGetQuota.Tables[0].Rows[i]["QUOTA_NO"].ToString() == "2")
                                {
                                    txtSQStateLevel_per.Text = dsGetQuota.Tables[0].Rows[i]["QUOTA_PER"].ToString();
                                    txtStateLevel_GENPer.Text = dsGetQuota.Tables[0].Rows[i]["GEN_PER"].ToString();
                                    txtStateLevel_OBCPer.Text = dsGetQuota.Tables[0].Rows[i]["OBC_PER"].ToString();
                                    txtStateLevel_SCPer.Text = dsGetQuota.Tables[0].Rows[i]["SC_PER"].ToString();
                                    txtStateLevel_STPer.Text = dsGetQuota.Tables[0].Rows[i]["ST_PER"].ToString();
                                    txtStateLevel_NTPer.Text = dsGetQuota.Tables[0].Rows[i]["NT_PER"].ToString();
                                }
                            
                    }
                          
                    
                }
            }
            else
            {
                objCommon.DisplayMessage("Record Not Found!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_MASTERS_HostelSeatQuota.FillListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataSet dsShow = objCommon.FillDropDown("ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO=" + Convert.ToInt32(ddlAdmbatch.SelectedValue), "BATCHNO desc");
        lvQuota.DataSource = dsShow;
        lvQuota.DataBind();
        lvQuota.Visible = true;
        FillListView();
    }
}
