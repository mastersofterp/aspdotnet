using System;
using System.Data;
using System.Web;

using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;


using System.Data.SqlClient;
using System.Net.Mail;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class MailSender
            {

                public void sendingmail(string ToID, string userMsg)
                {
                    try
                    {
                        MailMessage msg = new MailMessage();
                        SmtpClient c = new SmtpClient();

                        ////msg.From = new MailAddress("tpogec@rediffmail.com","Chandrapur");
                        msg. From = new MailAddress("mis.goa@iitms.co.in", "NIT GOA");
                        msg. To. Add(new MailAddress(ToID));
                        msg. Subject = "Approval";
                        msg. Body = userMsg;
                        //msg.Priority = MailPriority.High;
                        msg. IsBodyHtml = true;
                        //c.Host = "smtp.rediffmail.com";
                        //c.Port = 587;
                        //c.UseDefaultCredentials = false;
                        //c.Credentials = new System.Net.NetworkCredential("tpogec@rediffmail.com", "gec1996");
                        //c.EnableSsl = false;
                        //c.DeliveryMethod = SmtpDeliveryMethod.Network;
                        //c.Send(msg);
                        //msg = null;
                        msg. Priority = MailPriority. High;
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                        smtp. UseDefaultCredentials = false;
                        smtp. EnableSsl = true;
                        smtp. UseDefaultCredentials = false;
                        ////smtp.Credentials = new System.Net.NetworkCredential("tpogec@rediffmail.com", "gec1996");
                        smtp.Credentials = new System.Net.NetworkCredential("mis.goa@iitms.co.in", "tspl@1ne");
                        smtp. DeliveryMethod = SmtpDeliveryMethod. Network;
                        smtp. Send(msg);
                        msg = null;

                    }
                    catch (SmtpException ex)
                    {

                    }
                }
               
       


            }
        }
    }
}

