using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;

namespace JumpyForum
{
	/// <summary>
	/// Summary description for Delete.
	/// </summary>
	public partial class Delete : System.Web.UI.Page
	{
        public static string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

		private int articleid = 0;
		private int commentid  =0;
       
        Common objcommon = new Common();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (Request.QueryString["id"] != null)  
				articleid = Convert.ToInt32(Request.QueryString["id"]); 
 
			if (Request.QueryString["CId"] != null)  
				commentid = Convert.ToInt32(Request.QueryString["CId"]); 
 
			
			try
			{
                
				clsDataAccess myclass = new clsDataAccess();
				myclass.openConnection();
				Boolean myReturn = myclass.DeleteForumData(articleid,commentid);
				myclass.closeConnection();                
                Response.Redirect("Itle_NewDiscussionForum.aspx?id=" + articleid);
                //objcommon.DisplayUserMessage(UPDFORUM, "Message deleted successfully.",Page);
			}
			catch(Exception)
			{
				Response.Write("<h2> Unexpected error ! Try slamming your head into your computer monitor :)</h2>"); 

			}


		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
