using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;

public partial class OBE_QuestionPaper_Unlock : System.Web.UI.Page
{
    Common objCommon = new Common();
    ExamQuestionPaperController objStatC = new ExamQuestionPaperController();
    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //TO SET THE MASTERPAGE
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            //CHECK SESSION

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //PAGE AUTHORIZATION
                // this.CheckPageAuthorization();
                this.FillDropdownList();

            }
        }
    }
    #endregion
    protected void btnShow_Click(object sender, EventArgs e)
    {
       this.Show();
    }
    private void FillDropdownList()
    {
       
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER SM ON(S.COLLEGE_ID=SM.COLLEGE_ID)", "S.sessionno", "Concat(s.SESSION_NAME , '-',SM.SHORT_NAME)SESSION_NAME", "FLOCK=1", "S.sessionno");
        pnlPaper.Visible = false;

    }
    private void Show()
    {
        DataSet ds = objCommon.FillDropDown("tblexamquestionpaper teq inner join tblacdschemesubjectmapping tac on (teq.SchemeSubjectId=tac.SchemeSubjectId) inner join tblexampatternmapping M on (M.ExamPatternMappingId=teq.ExamPatternMappingId) inner join tblexamnamemaster tem on (tem.ExamNameId=M.ExamNameId) inner join USER_ACC UA on (UA.UA_NO=teq.CreatedBy)", "tem.ExamName", "QuestionPaperId,case when teq.ISlock=1 then 'Lock' else 'In-Progress' end as [Status],teq.TotalMaxMarks,tem.ExamName,tac.SchemeMappingName,UA.UA_NAME as CreatedBy,teq.CCODE", "teq.sessionid=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SchemeId=" + Convert.ToInt32(ddlscheme.SelectedValue), "tac.SchemeSubjectId,tem.ExamName desc");
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            pnlPaper.Visible = true;
            btnUpdate.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Paper Not Created Yet...!!", this.Page);
            btnUpdate.Visible = false;
        }

    
    
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlscheme, "acd_student_result ASR inner join ACd_SCHEME AC on(AC.schemeno=ASR.schemeno)", "Distinct ASR.SCHEMENO", "ASR.CCODE +'-'+ AC.SCHEMENAME +'-'+ cast(ASR.SEMESTERNO as nvarchar(10))SEMESTERNO", "isnull(cancel,0)=0 and exam_registered=1 and sessionno=" + Convert.ToInt32(ddlSession.SelectedValue), "");

        objCommon.FillDropDownList(ddlscheme, "acd_student_result ASR inner join ACd_SCHEME AC on(AC.schemeno=ASR.schemeno)", "Distinct ASR.SCHEMENO", "AC.SCHEMENAME +'-'+ cast(ASR.SEMESTERNO as nvarchar(10))SEMESTERNO", "isnull(cancel,0)=0 and exam_registered=1 and sessionno=" + Convert.ToInt32(ddlSession.SelectedValue), "");
        pnlPaper.Visible = false;
        btnUpdate.Visible = false;
    }
  
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        pnlPaper.Visible = false;
        btnUpdate.Visible = false;
    }
    private string GetIDNO()
    {
        string retIDNO = string.Empty;
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            HiddenField hiddenfield = item.FindControl("hfqp") as HiddenField;

            if (chk.Checked)
            {
                if (retIDNO.Length == 0) retIDNO = hiddenfield.Value.ToString();
                else
                    retIDNO += "$" + hiddenfield.Value.ToString();
            }
        }
        if (retIDNO.Equals("")) return "0";
        else return retIDNO;
        //return retIDNO;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        int count = 0;
        foreach (ListViewItem item in lvStudent.Items)
        {
            if ((item.FindControl("chkStudent") as CheckBox).Checked == true)
            {
                count++;
            }
        }
        if (count > 0)
        {
            String ids = GetIDNO(); //Seprated by $
            if (txtconformmessageValue.Value == "Yes")
            {
                int Unlock = objStatC.QuestionPaper_Unlock(Convert.ToString(ids));
                if (Unlock == 1)
                {
                    objCommon.DisplayMessage(updEdit, "Paper Unlock Successfully.", this.Page);
                    this.Show();

                }
                else
                {
                    objCommon.DisplayMessage(updEdit, "Error occored while Unlock Question Paper.", this.Page);
                
                }
                
            }

        }
        else
        {
            objCommon.DisplayMessage(this.updEdit, "Please Select at least one Paper !!!!", this.Page);
            return;
        }

    }
}