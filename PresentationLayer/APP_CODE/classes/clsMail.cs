/* *************************************************
 * Author: Rajesh Lal(connectrajesh@hotmail.com)
 * Date: 12/14/06
 * Company Info: www.irajesh.com
 * See EULA.txt and Copyright.txt for additional information
 * **************************************************/
using System;
using System.Web.Mail; 
namespace JumpyForum
{
	/// <summary>
	/// Summary description for clsMail.
	/// </summary>
	public class clsMail
	{
		public clsMail()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public bool SendMail(string ToM, string FromM, string CcM, string MSubject, string MBody ) // Opens database connection with Granth in SQL SERVER
		{
			try
			{
				MailMessage objMM = new MailMessage();
				//'Set the properties
				objMM.To = ToM;//"razesh@hotmail.com";
				objMM.From = FromM;//"connectrajesh@hotmail.com";

				//'If you want to CC this email to someone else...
				objMM.Cc = CcM;//"flytorajesh@someaddress.com";

				//'If you want to BCC this email to someone else...
				//objMM.Bcc = "studyrajesh@hotmail.com";

				//'Send the email in text format
				objMM.BodyFormat = MailFormat.Html ;

				//'(to send HTML format, change MailFormat.Text to MailFormat.Html)

				//'Set the priority - options are High, Low, and Normal

				objMM.Priority = MailPriority.Normal;

				//'Set the subject
				objMM.Subject = MSubject;//"Hello there testing!";

				//'Set the body - use VbCrLf to insert a carriage return
				objMM.Body = MBody;//"Hi! How are you doing?";
				SmtpMail.SmtpServer = "nitiny2k@gmail.com"; 
				SmtpMail.Send(objMM);

				return true;
			}
			catch
			{
				return false;
			}
			finally
			{
				
			}

		}
	}
}
