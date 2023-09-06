using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using IITMS;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class BlockMaster
    {
        #region Private Member
        private int _BLOCKNO = 0;
        private string _BLOCK_NAME = string.Empty;

        #endregion

        #region Public Members

        public int BLOCKNO
        {
            get { return _BLOCKNO; }
            set { _BLOCKNO = value; }
        }

        public string BLOCK_NAME
        {
            get { return _BLOCK_NAME; }
            set { _BLOCK_NAME = value; }
        }

        #endregion 
    }
}
