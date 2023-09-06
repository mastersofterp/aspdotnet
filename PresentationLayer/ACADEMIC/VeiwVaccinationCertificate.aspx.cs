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
using System.IO;

public partial class VACCINATION_VeiwVaccinationCertificate : System.Web.UI.Page
{
    IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string idno = Request.QueryString["idno"];
            string dose = Request.QueryString["dose"];
            string pathName = string.Empty;
            string fileName = string.Empty;
            if (dose == "1")
            {
                pathName = objCommon.LookUp("ACD_COVID_VACCINATION_DETAIL", "FIRSTDOSE_FILE_PATH", "IDNO=" + idno);
                fileName = objCommon.LookUp("ACD_COVID_VACCINATION_DETAIL", "FIRSTDOSE_FILE_NAME", "IDNO=" + idno);
            }
            else if(dose == "2")
            {
                pathName = objCommon.LookUp("ACD_COVID_VACCINATION_DETAIL", "SECONDDOSE_FILE_PATH", "IDNO=" + idno);
                fileName = objCommon.LookUp("ACD_COVID_VACCINATION_DETAIL", "SECONDDOSE_FILE_NAME", "IDNO=" + idno);
            }
            string filePath = Path.Combine(pathName, fileName);
            this.Response.ContentType = "application/pdf";
            this.Response.AppendHeader("Content-Disposition;", "attachment;filename=" + fileName);
            this.Response.WriteFile(filePath);
            this.Response.End();
        }
    }
}
