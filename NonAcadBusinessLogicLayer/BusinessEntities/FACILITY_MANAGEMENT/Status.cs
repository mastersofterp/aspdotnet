using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Status
    {
        private string _Id;
        private bool _isErrorInService;
        private string _message;

        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        
        public bool IsErrorInService
        {
            get { return _isErrorInService; }
            set { _isErrorInService = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
