using BusinessLogicLayer.BusinessLogic.PostAdmission;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_POSTADMISSION_ADMP_StudentTransfer : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //ADMPStudentTransferController objTrans = new ADMPStudentTransferController();

     string _UAIMS_constr=System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
     

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
         
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["colcode"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();
                objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
                ddlAdmissionBatch.SelectedIndex = 0;
                //btnAttendance.Visible = false;
        
            }
        }
    }
    #endregion Page Load


    protected void ddlProgramType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedIndex > 0)
            {
               objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND UGPGOT=" + ddlProgramType.SelectedValue, "D.DEGREENO");
            }
            ddlDegree.Items.Insert(0, new ListItem("Please Select Degree", "0"));
            ddlDegree.SelectedIndex = 0;
            ddlDegree.Focus();
            lstProgram.Items.Clear();
            //pnlCount.Visible = false;
            pnlCount.Visible = false;            
            pnllvSh.Visible = false;
            lvSchedule.DataSource = null;
            lvSchedule.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Exam_Hall_Ticket.ddlProgramType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstProgram.Items.Clear();
        //pnlCount.Visible = false;
        pnlCount.Visible = false;        
        pnllvSh.Visible = false;
        lvSchedule.DataSource = null;
        lvSchedule.DataBind();
        int Degree = Convert.ToInt16(ddlDegree.SelectedValue);
        MultipleCollegeBind(Degree);
    }

    private void MultipleCollegeBind(int Degree)
    {
        try
        {
            DataSet ds = null;
            ds = GetBranch(Degree);

            lstProgram.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstProgram.DataSource = ds;
                lstProgram.DataValueField = ds.Tables[0].Columns[0].ToString();
                lstProgram.DataTextField = ds.Tables[0].Columns[1].ToString();
                lstProgram.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void lstProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    pnlCount.Visible = false;
        //    pnllvSh.Visible = false;
        //    DataSet ds = null;
        //    int DegreeId = Convert.ToInt32(ddlDegree.SelectedValue);
        //    int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
        //    string branchno = string.Empty;

        //    foreach (ListItem items in lstProgram.Items)
        //    {
        //        if (items.Selected == true)
        //        {
        //            branchno += items.Value + ',';
        //        }
        //    }
        //    branchno = branchno.TrimEnd(',').Trim();
        //    string Branch = ddlProgramType.SelectedValue;
        //    ds = objAttendance.GetSChedule(DegreeId, branchno, ADMBATCH);

        //    ddlExamSlot.Items.Clear();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        ddlExamSlot.DataSource = ds;
        //        ddlExamSlot.DataValueField = ds.Tables[0].Columns[0].ToString();
        //        ddlExamSlot.DataTextField = ds.Tables[0].Columns[1].ToString();
        //        ddlExamSlot.DataBind();
        //        ddlExamSlot.Items.Insert(0, new ListItem("Select Schedule", "0"));
        //        ddlExamSlot.SelectedIndex = 0;
        //    }
        //}
        //catch
        //{
        //    throw;
        //}
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Branch/Program.", this.Page);
                return;
            }            
            int ADMBATCH = Convert.ToInt32(ddlAdmissionBatch.SelectedValue);
            int ProgramType = Convert.ToInt32(ddlProgramType.SelectedValue);
            int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
           
            string branchno = string.Empty;

            //btnAttendance.Visible = true;

            foreach (ListItem items in lstProgram.Items)
            {
                if (items.Selected == true)
                {
                    branchno += items.Value + ',';
                    //activitynames += items.Text + ',';
                }
            }

            //branchno.TrimEnd(',').TrimEnd();
            branchno = branchno.TrimEnd(',').Trim();

            DataSet ds = null;
            ds = GetStudentList(ADMBATCH, ProgramType, DegreeNo, branchno);

            lvSchedule.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnllvSh.Visible = true;
                lvSchedule.Visible = true;
                lvSchedule.DataSource = ds;
                lvSchedule.DataBind();
                pnlCount.Visible = true;
                txtTotalCount.Text = ds.Tables[0].Rows.Count.ToString("0000");
                int listviewcount = lvSchedule.Items.Count;


            }
            else
            {
                //   lvSchedule.Items.Clear();
                lvSchedule.DataSource = null;
                lvSchedule.DataBind();
                pnlCount.Visible = false;
                objCommon.DisplayMessage(upAttendance, "Record Not Found,Please Upload The All Mondatory Document And Paid The Final Admission Fee.", this.Page);
                //btnAttendance.Visible = false;
            }
        }
        catch
        {
            throw;
        }
    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) GROUP BY ADMBATCH,BATCHNAME", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "", "ADMBATCH DESC");

        objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMISSION_CONFIG A INNER JOIN ACD_ADMBATCH B ON(A.ADMBATCH=B.BATCHNO) ", "DISTINCT MAX(ADMBATCH) ADMBATCH", "BATCHNAME", "IsNull(B.ACTIVESTATUS,0)=1 GROUP BY ADMBATCH,BATCHNAME", "ADMBATCH DESC");
        ddlProgramType.SelectedIndex = 0;
        ddlDegree.Items.Clear();
        lstProgram.Items.Clear();
        //pnlCount.Visible = false;
        pnlCount.Visible = false;        
        pnllvSh.Visible = false;
        lvSchedule.DataSource = null;
        lvSchedule.DataBind();
       // btnAttendance.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProgramType.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Program Type.", this.Page);
                return;
            }
            else if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Degree.", this.Page);
                return;
            }
            else if (lstProgram.SelectedValue == "0")
            {
                objCommon.DisplayMessage(upAttendance, "Please Select Branch/Program.", this.Page);
                return;
            }           
            string ipaddress = string.Empty;

            string rollno = string.Empty;
            int chkCount = 0;
            int updCount = 0;

            ipaddress = Request.ServerVariables["REMOTE_HOST"];

            int ua_no = Convert.ToInt32(Session["userno"].ToString());
            int UserNo = 0;
            int PType = 1;
            int DegreeNo = 0;           
            int BranchNo = 0;     
            foreach (ListViewDataItem lvItem in lvSchedule.Items)
            {

                Label lblstatus = lvItem.FindControl("lblStatus") as Label;
                CheckBox chkBox = lvItem.FindControl("chkRecon") as CheckBox;
                HiddenField hdnUserNo = lvItem.FindControl("hdnUserNo") as HiddenField;
                HiddenField hdnDegreeNo  = lvItem.FindControl("hdnDegreeNo") as HiddenField;
                HiddenField hdnBranchNo = lvItem.FindControl("hdnBranchNo") as HiddenField;
                

                if (!hdnUserNo.Value.Equals(string.Empty))
                {
                    UserNo = Convert.ToInt32(hdnUserNo.Value);
                }
                if (!hdnDegreeNo.Value.Equals(string.Empty))
                {
                    DegreeNo = Convert.ToInt32(hdnDegreeNo.Value);
                }
                if (!hdnBranchNo.Value.Equals(string.Empty))
                {
                    BranchNo = Convert.ToInt32(hdnBranchNo.Value);
                }


                if (chkBox.Checked)
                {

                    CustomStatus cs = (CustomStatus)INSERT_StudentTransfer(UserNo, PType, DegreeNo,BranchNo);
                    // CustomStatus cs = (CustomStatus)1;
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        updCount++;

                    }
                }


            }

            if (updCount > 0)
            {

                objCommon.DisplayMessage(upAttendance, "Student Transfered Successfully.", this.Page);
                btnShow_Click(sender, e);

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_AdmitCard.btnGenerate_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    public DataSet GetBranch(int Degree)
    {
        DataSet ds = null;
        SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

        try
        {
            SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@Degree",Degree),
                        };

            ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_ACAD_GET_GETDEGREENAME]", objParams);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return ds;
    }


    public DataSet GetStudentList(int ADMBATCH, int UGPG, int Degree, string Branch)
    {
        DataSet ds = null;
        SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

        try
        {
            SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_ADMBATCH",ADMBATCH),
                           new SqlParameter("@P_PROGRAMME_TYPE",UGPG),
                           new SqlParameter("@P_DEGREENO",Degree),
                           new SqlParameter("@P_BRANCHNO",Branch)
                        };

            ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_GET_LIST_FOR_STUDENT_TRANSFER]", objParams);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return ds;
    }

    public int INSERT_StudentTransfer(int UserNo, int PType, int DegreeNo, int BranchNo)
    {
        int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[7];
            {
                objParams[0] = new SqlParameter("@P_USERNO", UserNo);
                objParams[1] = new SqlParameter("@P_PTYPE", PType);
                objParams[2] = new SqlParameter("@P_DEGREENO", DegreeNo);
                objParams[3] = new SqlParameter("@P_BRANCHNO", BranchNo);
                objParams[4] = new SqlParameter("@P_REMARK", "Post Admisssion Transfere");
                objParams[5] = new SqlParameter("@P_ADMDATE", DateTime.Now);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;
            };
            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMP_TRANSFER_STUD_DATA", objParams, false);

            if (Convert.ToInt32(ret) == -99)
                retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
            else
                retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ADMPStudentTransferController.INSERT_StudentTransfer()-->" + ex.Message + " " + ex.StackTrace);
        }
        return retStatus;

    }
}