using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class News
            {
                #region Private Members
                private int _newsid = 0;
                private string _category = string.Empty;
                private DateTime _expiryDate = DateTime.Now;
                private string _filename = string.Empty;
                private string _oldfilename = string.Empty;
                private string _link = string.Empty;
                private string _newsDesc = string.Empty;
                private string _newsTitle = string.Empty;
                private int _status = 0;
                private int _ua_Type = 0;
                private DateTime _uploadedDate = DateTime.Now;
                #endregion

                #region Public Property Fields
                public int NewsID
                {
                    get { return _newsid; }
                    set { _newsid = value; }
                }

                public string NewsTitle
                {
                    get { return _newsTitle; }
                    set { _newsTitle = value; }
                }

                public string NewsDesc
                {
                    get { return _newsDesc; }
                    set { _newsDesc = value; }
                }

                public int Status
                {
                    get { return _status; }
                    set { _status = value; }
                }

                public string Link
                {
                    get { return _link; }
                    set { _link = value; }
                }

                public int UA_Type
                {
                    get { return _ua_Type; }
                    set { _ua_Type = value; }
                }

                public string Category
                {
                    get { return _category; }
                    set { _category = value; }
                }

                public string Filename
                {
                    get { return _filename; }
                    set { _filename = value; }
                }

                public string OldFilename
                {
                    get { return _oldfilename; }
                    set { _oldfilename = value; }
                }

                public DateTime UploadedDate
                {
                    get { return _uploadedDate; }
                    set { _uploadedDate = value; }
                }

                public DateTime ExpiryDate
                {
                    get { return _expiryDate; }
                    set { _expiryDate = value; }
                }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS