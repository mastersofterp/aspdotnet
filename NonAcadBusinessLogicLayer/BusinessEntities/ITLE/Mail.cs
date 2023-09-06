using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Mail
    {
        #region Private Fields
        int _mailId = 0;
        int _parentId = 0;
        int _senderId = 0;
        DateTime _sentDate = DateTime.MinValue;
        string _subject = string.Empty;
        string _body = string.Empty;
        List<MailReceiver> _receivers = new List<MailReceiver>();
        List<MailAttachment> _attachments = new List<MailAttachment>();
        #endregion

        #region Public Properties
        public int MailId
        {
            get { return _mailId; }
            set { _mailId = value; }
        }

        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        public int SenderId
        {
            get { return _senderId; }
            set { _senderId = value; }
        }

        public DateTime SentDate
        {
            get { return _sentDate; }
            set { _sentDate = value; }
        }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        public List<MailReceiver> Receivers
        {
            get { return _receivers; }
            set { _receivers = value; }
        }

        public List<MailAttachment> Attachments
        {
            get { return _attachments; }
            set { _attachments = value; }
        }

        #endregion

        public Mail()
        {

        }
    }

    public class MailReceiver
    {
        int _recordId = 0;
        int _mailId = 0;
        int _receiverId = 0;
        char _status;
        string _userName = "";


        public int RecordId
        {
            get { return _recordId; }
            set { _recordId = value; }
        }

        public int MailId
        {
            get { return _mailId; }
            set { _mailId = value; }
        }

        public int ReceiverId
        {
            get { return _receiverId; }
            set { _receiverId = value; }
        }

        public char Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
    }

    public class MailAttachment
    {
        int _attachmentId = 0;
        int _mailId = 0;
        string _fileName = "";
        string _filePath = "";
        int _size = 0;

        public int AttachmentId
        {
            get { return _attachmentId; }
            set { _attachmentId = value; }
        }

        public int MailId
        {
            get { return _mailId; }
            set { _mailId = value; }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }
    }
}
