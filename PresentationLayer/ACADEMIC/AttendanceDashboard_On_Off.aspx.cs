/*
==============================================================================
PROJECT NAME : RF-Common Code
MODULE NAME : ACADEMIC ()
PAGE NAME : Attendance Dashboard ON/OFF
CREATION DATE : 28-02-2024
CREATED BY : Vipul Tichakule
MODIFIED DATE : 
MODIFIED DESC : 
==============================================================================
*/



using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.Academic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_AttendanceDashboard_On_Off : System.Web.UI.Page
{
    ModuleConfigController objMConfig = new ModuleConfigController();
    Common objCommon = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAttDropDown();
            BindListBox();
        }
    }



    private void BindAttDropDown()
    {
        string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";
       

        DataSet ds = new DataSet();
        SP_Name = "PKG_ACD_GET_SESSION_COLLEGE_FOR_MC";
        SP_Parameters = "@P_MODE";
        Call_Values = "1";
        ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        {
            lstSession.DataSource = ds.Tables[0];
            lstSession.DataTextField = "SESSION_NAME";
            lstSession.DataValueField = "SESSIONID";
            lstSession.DataBind();

            lstCollege.DataSource = ds.Tables[1];
            lstCollege.DataTextField = "COLLEGE_NAME";
            lstCollege.DataValueField = "COLLEGE_ID";
            lstCollege.DataBind();
        }
    }


    protected void BindListBox()
    {
        DataSet ds = objCommon.FillDropDown("ACD_ATTENDANCE_DASH_ONOFF", "SESSIONID", "COLLEGEID", "", "");

        string att_SessionIds = ds.Tables[0].Rows[0]["SESSIONID"].ToString();
        string[] Session_Ids = att_SessionIds.Split(',');

        for (int j = 0; j < Session_Ids.Length; j++)
        {
            for (int i = 0; i < lstSession.Items.Count; i++)
            {
                if (Session_Ids[j] == lstSession.Items[i].Value)
                {
                    lstSession.Items[i].Selected = true;
                }
            }
        }


        string att_CollegeIds = ds.Tables[0].Rows[0]["COLLEGEID"].ToString();
        string[] CollegeIds = att_CollegeIds.Split(',');

        if (att_CollegeIds != "")
        {
            for (int j = 0; j < CollegeIds.Length; j++)
            {
                for (int i = 0; i < lstCollege.Items.Count; i++)
                {
                    if (CollegeIds[j] == lstCollege.Items[i].Value)
                    {
                        lstCollege.Items[i].Selected = true;
                    }
                }
            }
        }
    
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        int count = 0;
        int scount = 0;
       
                string sessionids = ""; string college_ids = "";
                foreach (ListItem items in lstSession.Items)
                {
                    if (items.Selected == true)
                    {
                        sessionids += items.Value + ',';
                        scount++;

                        foreach (ListItem items1 in lstCollege.Items)
                        {
                            if (items1.Selected == true)
                            {
                                count++;
                            }                          
                        }

                    }
                   
                }
                if (sessionids != "")
                {
                    sessionids = sessionids.Remove(sessionids.Length - 1);
                }

                if (scount > 0)
                {
                    if (count == 0)
                    {
                        objCommon.DisplayMessage(this.upAttendance, "Please select college", this.Page);
                        return;
                    }
                }



                foreach (ListItem items in lstCollege.Items)
                {
                    if (items.Selected == true)
                    {
                        college_ids += items.Value + ',';
                    }
                }
                if (college_ids != "")
                {
                    college_ids = college_ids.Remove(college_ids.Length - 1);
                }

                   CustomStatus cs = (CustomStatus)objMConfig.InsertAttendanceDahsonoff(sessionids,college_ids);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                    
                      objCommon.DisplayMessage(this.upAttendance, "Record inserted successfully", this.Page);
                      Clear();
                      BindListBox(); ;
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                       objCommon.DisplayMessage(this.upAttendance, "Record update successfully", this.Page);
                       Clear();
                       BindListBox();
                    }
          
            }


    protected void btnCancell_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void Clear()
    {
        lstSession.ClearSelection();
        lstCollege.ClearSelection();
    }
}