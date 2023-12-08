using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class SendNotice
    {
        private int _FromUserNo;
        private string _NoticeTitle;
        private string _ImageUrl;
        private string _NoticeDetailMessage;
      //  private List<NoticeReceiver> _NoticeReceivers;

        public int FromUserNo
        {
            get { return _FromUserNo; }
            set { _FromUserNo = value; }
        }

        public string MessageTitle
        {
            get { return _NoticeTitle; }
            set { _NoticeTitle = value; }
        }

        public string ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }

        public string Message
        {
            get { return _NoticeDetailMessage; }
            set { _NoticeDetailMessage = value; }
        }

        //public List<NoticeReceiver> NoticeReceivers
        //{
        //    get { return _NoticeReceivers; }
        //    set { _NoticeReceivers = value; }
        //}
    }
}
