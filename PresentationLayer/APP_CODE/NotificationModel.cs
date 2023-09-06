using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NotificationModel
/// </summary>
public class NotificationModel
{
    //public NotificationModel()
    //{
    public string MessageHeading { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public int uano { get; set; }
        public int ActivityNo { get; set; }
        public bool IsRead { get; set; }
	//}
}