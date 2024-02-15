//======================================================================================                                                            
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ANTI RAGGING DECLARATION UPLOAD                                                   
// CREATION DATE : 09-FEB-2024                                                         
// CREATED BY    : SHRIKANT WAGHMARE                                                                                 
//======================================================================================
using IITMS.UAIMS;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;

using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_Anti_Ragging_Declaration : System.Web.UI.Page
{
    Common objCommon = new Common();
    StudentController objStudent = new StudentController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindListView();
        }
    }


    protected void btnAntiRaggingUpload_Click(object sender, EventArgs e)
    {

        string fileName = string.Empty;
        int userno = Convert.ToInt32(Session["userno"].ToString());
        string Fileext = System.IO.Path.GetExtension(fuAntiRagging.FileName);

        if (!fuAntiRagging.HasFile)
        {
            objCommon.DisplayMessage(this.Page, "Please Select File To Upload!", this.Page);
            return;
        }        
        else if (Fileext.ToLower()!= ".pdf")
        {
            objCommon.DisplayMessage(this.Page, "Please Upload PDF Files only!", this.Page);
            return;
        }
        else
        {
            fileName = fuAntiRagging.FileName;


            string directoryPath = Server.MapPath("~/AntiRagging");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            fuAntiRagging.SaveAs(Server.MapPath("~") + "//AntiRagging//" + fuAntiRagging.FileName);

            CustomStatus cs = (CustomStatus)objStudent.InsertUpdateAntiRaggingDeclaration(fileName, userno);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage("Record Saved Successfully!", this.Page);
                BindListView();
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage("Record Updated Successfully!", this.Page);
                BindListView();
            }
            else
            {
                objCommon.DisplayMessage("Error Occured!", this.Page);
                return;
            }

        }
    }

    public void BindListView()
    {
        DataSet ds = null;

        ds = objCommon.FillDropDown("ACD_ANTI_RAGGING_DECLARATION", "ARNO", "AR_DOCUMENT_NAME", string.Empty, string.Empty);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            pnlAntiRagging.Visible = true;
            lvAntiRagging.DataSource = ds;
            lvAntiRagging.DataBind();
        }
    }
    protected void btnEditAntiRagging_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEditAntiRagging = sender as ImageButton;
        DataSet ds;

        string arno = btnEditAntiRagging.CommandArgument.ToString();

        ds = objCommon.FillDropDown("ACD_ANTI_RAGGING_DECLARATION", "ARNO", "AR_DOCUMENT_NAME", "ARNO=" + arno, string.Empty);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblfile.Text = ds.Tables[0].Rows[0]["AR_DOCUMENT_NAME"].ToString();
        }
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        ImageButton btnPreview = (ImageButton)sender;

        string fileName = objCommon.LookUp("ACD_ANTI_RAGGING_DECLARATION", "AR_DOCUMENT_NAME", string.Empty);

        if (fileName != null)
        {
            btnPreview.CommandArgument = fileName;
            hfFileName.Value = fileName;
        }
        else
        {
            string script = "alert('No File is Uploaded for Preview!');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        fuAntiRagging.Attributes["value"] = ""; 
    }
}