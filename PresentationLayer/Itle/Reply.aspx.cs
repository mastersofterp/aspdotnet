
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

using IITMS.NITPRM;
using IITMS;
using IITMS. UAIMS;
using IITMS. UAIMS. BusinessLayer. BusinessEntities;
using IITMS. UAIMS. BusinessLayer. BusinessLogic;
using IITMS. SQLServer. SQLDAL;
namespace JumpyForum
{
	/// <summary>
	/// Summary description for NewMessage.
	/// </summary>
	public partial class Reply : System.Web.UI.Page
	{
        public static string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        Common objCom = new Common();
		private int articleid = 1;
		private int myindent =0;
		private int commentid  =1;
        decimal File_size;
        string PageId;

        string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"];

       

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			LoadComment();
			if (Request.QueryString["Test"] != null)  
			{
				if (String.Compare(Request.QueryString["Test"].ToLower(),"true")== 0)  
				{
					int mParentId =commentid;
					int mArticleId =articleid;
					string mTitle = "Test Reply";
					string mUserName = "quartz";
					string mUserEmail = "quartz@msn.com";
					string mDescription = "Test Reply Description";
					int mIndent=myindent;

					try
					{
                        SqlConnection myC = new SqlConnection(_nitprm_constr);
						//myC.ConnectionString=ConfigurationSettings.AppSettings["ConnectionString"];
						string sqlQuery="INSERT into " + ConfigurationSettings.AppSettings["CommentTable"] + "(ParentId,ArticleId,Title,UserName,UserEmail,Description,Indent,UserProfile) VALUES ('" +mParentId + "','" + mArticleId +  "','" + mTitle +  "','" + mUserName +  "','" + mUserEmail +  "','" + mDescription + "','" + mIndent + "','" + "http://www.codeproject.com/script/profile/whos_who.asp?id=81898" + "')"; 
						myC.Open();
						SqlCommand myCommand=new SqlCommand();
						myCommand.CommandText=sqlQuery;
						myCommand.Connection=myC;
						int i=myCommand.ExecuteNonQuery();
						myC.Close();
						lblStatus.ForeColor = Color.Green ;
						lblStatus.Text ="Status: Success";
						Response.Redirect("Itle_NewDiscussionForum.aspx?id=" + articleid );

					}
					catch(Exception)
					{
				
						lblStatus.ForeColor = Color.Red;
						lblStatus.Text ="Status: Error";
					}	

				}
			}
            // Used to get maximum size of file attachment
            GetAttachmentSize();
			
			
		}
		private void LoadComment()
		{
			

			if (Request.QueryString["id"] != null)  
				articleid = Convert.ToInt32(Request.QueryString["id"]); 
 
			if (Request.QueryString["CId"] != null)  
				commentid = Convert.ToInt32(Request.QueryString["CId"]);

            SqlConnection myC = new SqlConnection(_nitprm_constr);
			//myC.ConnectionString=ConfigurationSettings.AppSettings["ConnectionString"];//"workstation id=RAJ;packet size=4096;integrated security=SSPI;data source=RAJ;persist security info=False;initial catalog=iLakshmi";
			string myQuery ="";

			    myQuery = "SELECT * FROM " + ConfigurationSettings.AppSettings["CommentTable"] + " WHERE ID=" + commentid + " and ArticleID=" + articleid ; 
			
			myC.Open();
			SqlCommand myCommand = new SqlCommand(myQuery,myC);
			
			SqlDataReader myReader = myCommand.ExecuteReader();
			if(myReader.HasRows)
			{
				while(myReader.Read())
				{
					
					lblname.Text =   myReader["Username"].ToString(); 
					lblemail.Text =  myReader["UserEmail"].ToString(); 
					lbltitle.Text =  myReader["Title"].ToString(); 
					txtsubject.Text = "Re: " + lbltitle.Text ;
					lblComment.Text =  myReader["Description"].ToString();
					lblDate.Text =  myReader["DateAdded"].ToString() + " Indent:" + myReader["Indent"].ToString();
					myindent = Convert.ToInt32(myReader["Indent"].ToString()) + 1;
                   // myindent = Convert. ToInt32(myReader["Indent"]. ToString());
				}
			}
			myReader.Close(); 
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

        protected void btnAddReply_Click(object sender, System.EventArgs e)
		{
			int mParentId =commentid;
			int mArticleId =articleid;
			
			string mTitle = "Test";
			string mUserName = "quartz";
			string mUserEmail = "quartz@msn.com";
			string mDescription = "Test Description";
			int mIndent=myindent;
			string mProfile ="";
            string Attachment = "";
            string filePath = "";
            decimal fileSize=0.0M;
            int courseNo = Convert.ToInt32(Session["ICourseNo"]);
            int sessionNo = Convert.ToInt32(Session["SessionNo"]);
            
			try
			{
               
                string usename = objCom.LookUp("user_acc", "ua_fullname", "ua_no=" + Convert.ToInt16(Session["userno"].ToString()));
				mTitle = txtsubject.Text;
                mUserName = usename;
                mUserEmail = "A@GMAIL.COM"; ;
				mDescription = txtcomment.Text;

                //if (fuForum.FileName.EndsWith("txt") || fuForum.FileName.EndsWith(".doc") || fuForum.FileName.EndsWith("pdf") || fuForum.FileName == "" || fuForum.FileName.EndsWith("ppt"))
                //{
                if (fuForum.HasFile ==true)
                {
                    string DOCFOLDER = file_path + "ITLE\\upload_files\\DiscussionForum";

                    if (!System.IO.Directory.Exists(DOCFOLDER))
                    {
                        System.IO.Directory.CreateDirectory(DOCFOLDER);

                    }
                    Attachment = fuForum.FileName.Replace(" ", "_");
                    filePath = file_path + "ITLE\\upload_files\\DiscussionForum\\" + Attachment;
                    fileSize = fuForum.PostedFile.ContentLength;
                    //string uploadPath = HttpContext.Current.Server.MapPath("~/ITLE/UPLOAD_FILES/announcement/"); //
                    //Upload the File
                    //string fileName = System.Guid.NewGuid().ToString() + fuForum.FileName.Substring(fuForum.FileName.IndexOf('.'));

                    string fileExtention = System.IO.Path.GetExtension(Attachment);

                    int count = Convert.ToInt32(objCom.LookUp("ACD_IATTACHMENT_FILE_EXTENTIONS", "COUNT(EXTENTION)", "EXTENTION='" + fileExtention.ToString() + "'"));

                    if (count != 0)
                    {

                        if (!fuForum.FileName.Equals(string.Empty))
                        {

                            if (fuForum.PostedFile.ContentLength < 10485760)
                            {
                                fuForum.SaveAs(filePath);

                            }
                            else
                            {
                                objCom.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);
                                return;
                            }

                            //string uploadFile = System.IO.Path.GetExtension(fuForum.PostedFile.FileName);
                            //fuForum.PostedFile.SaveAs(filePath + Attachment);
                            //flag = true;
                            //}
                        }
                    }
                    else
                    {
                        objCom.DisplayMessage("File Format not supprted.", this);
                        return;
                    }
                }
                //}
                //else
                //{
                //    objCom.DisplayUserMessage(Page,"Plz Enter text, doc or pdf file", this);
                //    return;
                //}


                mProfile = "http://WWW.IITMS.CO.IN"; ;

				int mCommentType = 1;

				if (MsgType_2.Checked) 
					mCommentType = 2;
				if (MsgType_3.Checked) 
					mCommentType = 3;
				if (MsgType_4.Checked) 
					mCommentType = 4;
				if (MsgType_5.Checked) 
					mCommentType = 5;

				if(IsValid)
				{
                    SqlConnection myC = new SqlConnection(_nitprm_constr);
					//myC.ConnectionString=ConfigurationSettings.AppSettings["ConnectionString"];
                    string sqlQuery = "INSERT into " + ConfigurationSettings.AppSettings["CommentTable"] + "(ParentId,ArticleId,Title,UserName,UserEmail,Description,Indent,CommentType,ATTACHMENT,COURSENO,SESSIONNO,FILE_PATH,FILE_SIZE) VALUES ('" + mParentId + "','" + mArticleId + "','" + mTitle + "','" + mUserName + "','" + mUserEmail + "','" + mDescription + "','" + mIndent + "','" + mCommentType + "','" + Attachment + "','" + courseNo + "','" + sessionNo + "','" + filePath + "','" + fileSize + "')"; 
					myC.Open();
					SqlCommand myCommand=new SqlCommand();
					myCommand.CommandText=sqlQuery;
					myCommand.Connection=myC;
					int i=myCommand.ExecuteNonQuery();
					myC.Close();
					lblStatus.ForeColor = Color.Green ;
					lblStatus.Text ="Status: Success";
                    Response.Redirect("Itle_NewDiscussionForum.aspx?id=" + articleid);

                    //objCom.DisplayUserMessage(Page, "Message deleted successfully.", Page);
				}
			}
			catch(Exception)
			{
				
				lblStatus.ForeColor = Color.Red;
				lblStatus.Text ="Status: Error";
			}	
		}

        protected void btnBack_Click(object sender, System.EventArgs e)
		{
            Response.Redirect("Itle_NewDiscussionForum.aspx?id=" + articleid);
		}

		protected void txtProfile_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void MsgType_5_ServerChange(object sender, System.EventArgs e)
		{
		
		}

		protected void MsgType_2_ServerChange(object sender, System.EventArgs e)
		{
		
		}

		protected void MsgType_3_ServerChange(object sender, System.EventArgs e)
		{
		
		}


        //Get Page Id for getting File size from configuration

        private void GetAttachmentSize()
        {


            try
            {

                PageId = Request.QueryString["pageno"];

                if (Convert.ToInt32(Session["usertype"]) == 1)
                {

                    File_size = Convert.ToDecimal(objCom.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_ADMIN", "PAGE_ID=" + PageId));
                    lblFileSize.Text = objCom.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_ADMIN,'Bytes')AS FILE_SIZE_ADMIN", "PAGE_ID=" + PageId);
                }
                else

                    if (Convert.ToInt32(Session["usertype"]) == 2)
                    {
                        File_size = Convert.ToDecimal(objCom.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_STUDENT", "PAGE_ID=" + PageId));

                    }

                    else if (Convert.ToInt32(Session["usertype"]) == 3)
                    {
                        File_size = Convert.ToDecimal(objCom.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_FACULTY", "PAGE_ID=" + PageId));
                        lblFileSize.Text = objCom.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_FACULTY,'Bytes')AS FILE_SIZE_FACULTY", "PAGE_ID=" + PageId);
                    }



            }
            catch (Exception ex)
            {

            }

        }
	}
}
