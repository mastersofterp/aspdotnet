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
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class STORES_Transactions_Quotation_str_comparative_statements : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MemoryStream ms = new MemoryStream();
        Document doc = new Document(PageSize.A2, 5f, 5f, 7f, 0f);
        doc.SetMargins(0f, 0f, 20f, 20f);
        //PdfWriter.GetInstance(doc, Response.OutputStream);
        PdfWriter.GetInstance(doc, ms);

        doc.Open();
        iTextSharp.text.Table grdTable = Session["grdTable"] as iTextSharp.text.Table;
        doc.Add(grdTable);
        doc.Close();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content_disposition", "inline;filename=test1.pdf");
        Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
        Response.End();
    }
}
