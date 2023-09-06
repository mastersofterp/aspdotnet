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
using System.Text;
using IITMS.UAIMS;

public partial class ChatApp_ChatRoom : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
{
    private string _callBackStatus;
    Common objCommon = new Common();
    protected void Page_Init(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["ChatUserID"] == null || Session["ChatUsername"] == null)
            {
                Response.Redirect("Itle_Chat_Close.aspx");
            }

            string roomId = (string)Request["roomId"];
            Session["ChatUserID"] = Session["userno"].ToString();
            Session["ChatUsername"] = Session["userfullname"];
            lblRoomId.Text = roomId;
            //this.InsertNewUsers();
            this.GetRoomInformation();
            this.GetLoggedInUsers();
            //this.InsertMessage(ConfigurationManager.AppSettings["ChatLoggedInText"] + " " + DateTime.Now.ToString());
            this.GetMessages();
            this.FocusThisWindow();
            // create a call back reference so we can log-out user when user closes the browser
            string callBackReference = Page.ClientScript.GetCallbackEventReference(this, "arg", "LogOutUser", "");
            string logOutUserCallBackScript = "function LogOutUserCallBack(arg, context) { " + callBackReference + "; }";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LogOutUserCallBack", logOutUserCallBackScript, true);
            // create a call back reference so that we can refocus to this window when the cursor is placed in the message text box
            string focusWindowCallBackReference = Page.ClientScript.GetCallbackEventReference(this, "arg", "FocusThisWindow", "");
            string focusThisWindowCallBackScript = "function FocusThisWindowCallBack(arg, context) { " + focusWindowCallBackReference + "; }";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "FocusThisWindowCallBack", focusThisWindowCallBackScript, true);
            this.divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> SetScrollPosition();  </script> ";
            //getCollegeName();
        }
       
    }

    //private void getCollegeName()
    //{
   //    string collegeName = objCommon.LookUp("Reff", "CollegeName", "");
    //    string marqeeTag = collegeName;
    //    divHeader.InnerHtml = marqeeTag;
    //}

        //private void InsertNewUsers()
        //{
        //    LinqChatDataContext db = new LinqChatDataContext();

        //    var user = (from u in db.LoggedInUsers
        //                where u.UserID == Convert.ToInt32(Session["ChatUserID"])
        //                && u.RoomID == Convert.ToInt32(lblRoomId.Text)
        //                select u).SingleOrDefault();

        //    // if user does not exist in the LoggedInUser table
        //    // then let's add/insert the user to the table
        //    if (user == null)
        //    {
        //        LoggedInUser loggedInUser = new LoggedInUser();
        //        loggedInUser.UserID = Convert.ToInt32(Session["ChatUserID"]);
        //        loggedInUser.RoomID = Convert.ToInt32(lblRoomId.Text);
        //        db.LoggedInUsers.InsertOnSubmit(loggedInUser);
        //        db.SubmitChanges();
        //    }
        //}

        private void GetRoomInformation()
        {
            // get the room information from the database
            // we're going to set this up so that we can use
            // many rooms if we want to
            LinqChatDataContext db = new LinqChatDataContext();

            //var room = (from r in db.Rooms
            //            where r.RoomID == Convert.ToInt32(lblRoomId.Text)
            //            select r).SingleOrDefault();

            //lblRoomId.Text = room.RoomID.ToString();
            //lblRoomName.Text = room.Name;
            lblRoomId.Text = "1";
            lblRoomName.Text = "Room1";
        }

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text.Length > 0)
            {
                this.InsertMessage(null);
                this.GetMessages();
                txtMessage.Text = String.Empty;
                //this.GetPrivateMessages();
                this.FocusThisWindow();
                
                //ScriptManager1.SetFocus(txtMessage.ClientID);
            }
        }

        protected void Timer1_OnTick(object sender, EventArgs e)
        {
            this.GetLoggedInUsers();
            this.GetMessages();
           // this.GetPrivateMessages();

            if (Session["DefaultWindow"] != null)
            {
                if (Session["DefaultWindow"].ToString() == "MainWindow")
                {
                    this.FocusThisWindow();
                }
            }
            //this.divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> SetScrollPosition();  </script> ";
            this.divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> SetScrollPosition();  </script> ";

        }

        /// <summary>
        /// This will insert the passed text to the message table in the database
        /// </summary>
        private void InsertMessage(string text)
        {
            LinqChatDataContext db = new LinqChatDataContext();

            Message message = new Message();
            message.RoomID = Convert.ToInt32(lblRoomId.Text);
            message.UserID = Convert.ToInt32(Session["ChatUserID"]);

            if (String.IsNullOrEmpty(text))
            {
                message.Text = txtMessage.Text.Replace("<", "");
                //message.Color = ddlColor.SelectedValue;
            }
            else
            {
                message.Text = text;
               // message.Color = "gray";
            }

            message.ToUserID = null;            // in the future, we will use this value for private messages
            message.TimeStamp = DateTime.Now;
            db.Messages.InsertOnSubmit(message);
            db.SubmitChanges();
        }

        private void GetLoggedInUsers()
        {
            LinqChatDataContext db = new LinqChatDataContext();

            // let's check if this authenticated user exist in the
            // LoggedInUser table (means user is logged-in to this room)
            if (Session["ChatUserID"] != null)
            {
                var user = (from u in db.LoggedInUsers
                            where u.UserID == Convert.ToDecimal(Session["ChatUserID"])
                            && u.RoomID == Convert.ToInt32(lblRoomId.Text)
                            select u).SingleOrDefault();
                
                // if user does not exist in the LoggedInUser table
                // then let's add/insert the user to the table
                if (user == null)
                {
                    LoggedInUser loggedInUser = new LoggedInUser();
                    loggedInUser.UserID = Convert.ToInt32(Session["ChatUserID"]);
                    loggedInUser.RoomID = Convert.ToInt32(lblRoomId.Text);
                    loggedInUser.LastActivityTime = System.DateTime.Now;
                    db.LoggedInUsers.InsertOnSubmit(loggedInUser);
                    db.SubmitChanges();

                    UpdateVisitTime();
                    Session["LastActivityTime"] = System.DateTime.Now;
                }
                else
                {
                    
                    UpdateVisitTime();
                    //var userid = (from u in db.LoggedInUsers
                    //            where u.UserID == Convert.ToInt32(Session["ChatUserID"])
                    //            && u.RoomID == Convert.ToInt32(lblRoomId.Text)
                    //            select u).SingleOrDefault();
                    Session["LastActivityTime"] = user.LastActivityTime;
                }


                string userIcon=string.Empty;
                StringBuilder sb = new StringBuilder();
                LinqChatDataContext dbl = new LinqChatDataContext();
                // get all logged in users to this room
                var loggedInUsers = from l in dbl.LoggedInUsers
                                    where l.RoomID == Convert.ToInt32(lblRoomId.Text)
                                    select l;

                // list all logged in chat users in the user list
                foreach (var loggedInUser in loggedInUsers)
                {
                     TimeSpan TimeDifference = DateTime.Now - loggedInUser.LastActivityTime;
                        int dayDiff = TimeDifference.Days;
                        int hourDiff = TimeDifference.Hours;
                        int minuteDiff = TimeDifference.Minutes;

                        if (dayDiff == 0 && hourDiff == 0 && minuteDiff <= 1)
                        {

                            // show user icon based on sex
                            //if (loggedInUser.User.Sex.ToString().ToLower() == "m")
                            if (loggedInUser.User_Acc.UA_TYPE == 2)
                            {
                                int idno = Convert.ToInt32(objCommon.LookUp("User_Acc", "ISNULL(UA_IDNO,0)AS UA_IDNO", "UA_NO=" + loggedInUser.UserID + " AND UA_TYPE IN (2)"));
                                if (idno != 0)
                                {
                                    userIcon = "<img src='../../showimage.aspx?id=" + idno + "&type=STUDENT' style='vertical-align:middle' width='25px' height='25px' alt=''/>  ";
                                }
                                else
                                {
                                    userIcon = "<img src='../../IMAGES/photo.png' style='vertical-align:middle' width='25px' height='25px' alt=''/>  ";
                                }
                            }
                            else if (loggedInUser.User_Acc.UA_TYPE == 3)
                            {
                                int idno = Convert.ToInt32(objCommon.LookUp("User_Acc", "ISNULL(UA_IDNO,0)AS UA_IDNO", "UA_NO=" + loggedInUser.UserID + " AND UA_TYPE IN (3)"));
                                if (idno != 0)
                                {
                                    userIcon = "<img src='../../showimage.aspx?id=" + idno + "&type=EMP' style='vertical-align:middle' width='25px' height='25px' alt=''/>  ";
                                }
                                else
                                {
                                    userIcon = "<img src='../../IMAGES/photo.png' style='vertical-align:middle' width='25px' height='25px' alt=''/>  ";
                                }
                            }

                            // else
                            //    userIcon = "<img src='Images/womanIcon.gif' style='vertical-align:middle' alt=''>  ";
                            string openWndow = "window.open('ChatWindow.aspx?FromUserId=" + Session["ChatUserID"] +
                                        "&ToUserId=" + loggedInUser.User_Acc.UA_NO + "&Username=" + loggedInUser.User_Acc.UA_FULLNAME +
                                        "','" + loggedInUser.User_Acc.UA_FULLNAME + "','width=240,height=333,scrollbars=yes,toolbars=no,titlebar=no,menubar=no,resizable=no,top=400,left=1100');";

                            // open the chat window when the logged-in user clicks another user in the list
                            if (loggedInUser.User_Acc.UA_FULLNAME != (string)Session["ChatUsername"])
                            {

                                sb.Append(userIcon + "<a href=# onclick=\"window.open('ChatWindow.aspx?FromUserId=" + Session["ChatUserID"] +
                                        "&ToUserId=" + loggedInUser.User_Acc.UA_NO + "&Username=" + loggedInUser.User_Acc.UA_FULLNAME +
                                        "','" + loggedInUser.User_Acc.UA_FULLNAME + "','width=240,height=333,scrollbars=yes,toolbars=no,titlebar=no,menubar=no,resizable=no,top=400,left=1100'); isLostFocus = 'true';\">" +
                                        loggedInUser.User_Acc.UA_FULLNAME + "</a> <img id='chatIcon' src='../../Images/onlineS.png' width='8px' height='8px' alt=''/> <br>");

                            }
                            else
                            {
                                sb.Append(userIcon + "<b>" + loggedInUser.User_Acc.UA_FULLNAME + "</b> <img id='chatIcon' src='../../Images/onlineS.png' width='8px' height='8px' alt=''/><br>");
                            }
                        }

                        else
                        {
                            // log out the user by deleting from the LoggedInUser table


                            var DeleteOfflineUser = (from l in dbl.LoggedInUsers
                                                     where l.UserID == Convert.ToInt32(loggedInUser.UserID)
                                                && l.RoomID == Convert.ToInt32(lblRoomId.Text)
                                                select l).SingleOrDefault();

                            dbl.LoggedInUsers.DeleteOnSubmit(DeleteOfflineUser);                           
                            dbl.SubmitChanges();
                        }
                }

                // holds the names of the users shown in the chatroom
                litUsers.Text = sb.ToString();
            }
            else
            {               

                Response.Redirect("Itle_Chat_Close.aspx");
            }
        }

        private void UpdateVisitTime()
        {
            LinqChatDataContext db = new LinqChatDataContext();

            var updateLastTime = (from p in db.LoggedInUsers
                                  where p.UserID == Convert.ToInt32(Session["ChatUserID"])
                                  select p).Single();
            updateLastTime.LastActivityTime = System.DateTime.Now;
            db.SubmitChanges();
        }


        /// <summary>
        /// Get the last 20 messages for this room
        /// </summary>
        private void GetMessages()
        {
            LinqChatDataContext db = new LinqChatDataContext();

            var messages = (from m in db.Messages
                            where m.RoomID == Convert.ToInt32(lblRoomId.Text)
                            orderby m.TimeStamp descending
                            select m).Take(20).OrderBy(m => m.TimeStamp);

            if (messages != null)
            {
                StringBuilder sb = new StringBuilder();
                int ctr = 0;    // toggle counter for alternating color

                foreach (var message in messages)
                {
                    // alternate background color on messages
                    if (ctr == 0)
                    {
                        sb.Append("<div style='padding: 10px;'>");
                        ctr = 1;
                    }
                    else
                    {
                        sb.Append("<div style=' padding: 10px;'>");
                        ctr = 0;
                    }

                    //if (message.User_Acc.Sex.ToString().ToLower() == "m")
                    sb.Append("<img src='Images/manIcon.gif' style='vertical-align:middle' alt=''> <span style='color: black; font-weight: bold;'>" + message.User_Acc.UA_FULLNAME + ":</span>  " + message.Text + "</div>");
                    //else
                    //    sb.Append("<img src='Images/womanIcon.gif' style='vertical-align:middle' alt=''> <span style='color: black; font-weight: bold;'>" + message.User.Username + ":</span>  " + message.Text + "</div>");
                }

                litMessages.Text = sb.ToString();
            }
        }

        protected void BtnLogOut_Click(object sender, EventArgs e)
        {
            // log out the user by deleting from the LoggedInUser table
            LinqChatDataContext db = new LinqChatDataContext();

            var loggedInUser = (from l in db.LoggedInUsers
                                where l.UserID == Convert.ToInt32(Session["ChatUserID"])
                                && l.RoomID == Convert.ToInt32(lblRoomId.Text)
                                select l).SingleOrDefault();

            db.LoggedInUsers.DeleteOnSubmit(loggedInUser);
            //db.PrivateMessages.DeleteOnSubmit(loggedInUser);
            db.SubmitChanges();

            // insert a message that this user has logged out
            this.InsertMessage("Just logged out! " + DateTime.Now.ToString());

            // clean the session
            //Session.RemoveAll();
            //Session.Abandon();

            // redirect the user to the login page
            //Response.Redirect("Itle_Chat_Close.aspx");
        }

        /// <summary>
        /// Check if anyone invited me to chat privately
        /// </summary>
        private void GetPrivateMessages()
        {
            LinqChatDataContext db = new LinqChatDataContext();

            var privateMessage = (from pm in db.PrivateMessages
                                  where pm.ToUserID == Convert.ToInt32(Session["ChatUserID"])
                                  select pm).SingleOrDefault();

            if (privateMessage != null)
            {
                lblChatNowUser.Text = privateMessage.User_Acc.UA_FULLNAME;
                btnChatNow.OnClientClick =
                    "window.open('ChatWindow.aspx?FromUserId=" + Session["ChatUserID"] +
                    "&ToUserId=" + privateMessage.UserID + "&Username=" + privateMessage.User_Acc.UA_FULLNAME +
                    "&IsReply=yes','','width=240,height=333,scrollbars=yes,toolbars=no,titlebar=no,menubar=no,resizable=no,top=400,left=1100'); isLostFocus = 'true';";

                //pnlChatNow.Visible = true;


                //db.PrivateMessages.DeleteOnSubmit(privateMessage);
                //db.SubmitChanges();
            }
        }

        protected void BtnChatNow_Click(object sender, EventArgs e)
        {
            pnlChatNow.Visible = false;
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            pnlChatNow.Visible = false;
        }

        private void FocusThisWindow()
        {
            form1.DefaultButton = "btnSend";
            form1.DefaultFocus = "txtMessage";
            Session["DefaultWindow"] = "MainWindow";
        }

        #region ICallbackEventHandler Members

        string System.Web.UI.ICallbackEventHandler.GetCallbackResult()
        {
            return _callBackStatus;
        }

        /// <summary>
        /// We're doing 2 things here now so we want to validate whether we're trying to "LogOut" or "FocusThisWindow"
        /// based on the eventArgument parameter that is passed via a JavaScript callback method
        /// </summary>
        void System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
        {
            _callBackStatus = "failed";

            if (!String.IsNullOrEmpty(eventArgument))
            {
                // put back the focus on this window
                if (eventArgument == "FocusThisWindow")
                {
                    this.FocusThisWindow();
                }
            }

            if (!String.IsNullOrEmpty(eventArgument))
            {
                if (eventArgument == "LogOut")
                {
                    // log out the user by deleting from the LoggedInUser table
                    LinqChatDataContext db = new LinqChatDataContext();

                    var loggedInUser = (from l in db.LoggedInUsers
                                        where l.UserID == Convert.ToInt32(Session["ChatUserID"])
                                        && l.RoomID == Convert.ToInt32(lblRoomId.Text)
                                        select l).SingleOrDefault();

                    db.LoggedInUsers.DeleteOnSubmit(loggedInUser);
                    db.SubmitChanges();

                    // insert a message that this user has logged out
                    this.InsertMessage("Just logged out! " + DateTime.Now.ToString());
                }
            }

            _callBackStatus = "success";
        }

        #endregion
    }
