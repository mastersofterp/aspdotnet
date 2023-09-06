using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class VoucherPhoto
            {
                #region Private Member
                private int _idno = 0;
                private string _photoPath = string.Empty;
                private int _photoSize = 0;
                //Pending to Declare variable.....
                private byte[] _photo;
                private string _collegeCode = string.Empty;
                private string _VoucherNo = string.Empty;
                #endregion
               
                
                #region Public Property Fields
                public int Idno
                {
                    get { return _idno; }
                    set { _idno = value; }
                }
                public string PhotoPath
                {
                    get { return _photoPath; }
                    set { _photoPath = value; }
                }
                public int PhotoSize
                {
                    get { return _photoSize; }
                    set { _photoSize = value; }
                }
                public byte[] Photo1
                {
                    get { return _photo; }
                    set { _photo = value; }
                }
                public string CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                }
                public string VoucherNo
                {
                    get { return _VoucherNo; }
                    set { _VoucherNo = value; }
                }
                #endregion
            }
        }
    }
}

    