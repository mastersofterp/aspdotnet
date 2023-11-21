using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {        
        namespace BusinessLayer.BusinessEntities
        {
            public class Access_Link
            {


                #region Private Members
                private int _al_no = 0;
                private string _al_link = string.Empty;
                private string _al_url = string.Empty;
                private int _al_asno = 0;
                private int _summer = 0;
                private decimal _url_idno = 0.0M;
                private decimal _mastno = 0.0M;
                private decimal _srno = 0.0M;
                private int _levelno = 0;

                //Modified By Rishabh on 30/10/2021
                private int _chklinkstatus = 0;
                //Added by Pranita on 26/10/2021 for shortcut key of accesslink   
                private string _Shortcut_key = string.Empty;
                //Added by Nikhil l. on 24-04-2023 for maintaining flag whether it is transaction page or not.
                private int _transaction = 0;
                private string _pdffile = string.Empty;  // Added By Anurag B. on 31-10-2023
                #endregion

                #region Public Properties
                public int Al_AsNo
                {
                    get { return _al_asno; }
                    set { _al_asno = value; }
                }
                public string Al_Link
                {
                    get { return _al_link; }
                    set { _al_link = value; }
                }
                public int Al_No
                {
                    get { return _al_no; }
                    set { _al_no = value; }
                }
                public string Al_Url
                {
                    get { return _al_url; }
                    set { _al_url = value; }
                }
                public int Summer
                {
                    get { return _summer; }
                    set { _summer = value; }
                }

                public decimal Url_IdNo
                {
                    get { return _url_idno; }
                    set { _url_idno = value; }
                }
                public decimal MastNo
                {
                    get { return _mastno; }
                    set { _mastno = value; }
                }

                public decimal SrNo
                {
                    get { return _srno; }
                    set { _srno = value; }
                }

                public int levelno
                {
                    get { return _levelno; }
                    set { _levelno = value; }
                }

                public int chklinkstatus
                {
                    get
                    {
                        return _chklinkstatus;
                    }
                    set
                    {
                        _chklinkstatus = value;
                    }
                }
                //Added by Pranita on 26/10/2021 for shortcut key of accesslink   
                public string Shortcut_key
                {
                    get { return _Shortcut_key; }
                    set { _Shortcut_key = value; }
                }

                //Added by Nikhil l. on 24-04-2023 for maintaining flag whether it is transaction page or not.
                public int chkTrans
                {
                    get
                    {
                        return _transaction;
                    }
                    set
                    {
                        _transaction = value;
                    }
                }

                public string Al_PdfName              // Added By Anurag B. on 31-10-2023
                {
                    get { return _pdffile; }
                    set { _pdffile = value; }
                }

                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS