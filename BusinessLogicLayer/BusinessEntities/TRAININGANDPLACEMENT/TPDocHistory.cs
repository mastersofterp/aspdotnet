using System;
// PROJECT NAME  : U-AIMS(TRAINING AND PLACEMENT)                                                          
// MODULE NAME   : UPLOAD FILE HISTORY  
// CREATION DATE : 20-08-2015                                                     
// CREATED BY    : VAIBHAV ASHTIKAR                         
// MODIFIED BY   : 
// MODIFICATIONS : 

// MODIFIED DATE :                    
//=================================================================================

using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class TPDocHistory
            {
                #region Private Members
                //Table Name = ACD_FILE_DETAILS
                private int _COMPID;
                private string _COMPNAME;
                private int _SCHEDULENO;
                private int _FILENO;
                private string _FILENAME;
                private string _FILEPATH;
                private string _COLLEGECODE;
                #endregion

                #region Public Members
                //Company Master [Table=ACD_FILE_DETAILS]
                public int COMPID
                {
                    get
                    {
                        return this._COMPID;
                    }
                    set
                    {
                        if ((this._COMPID != value))
                        {
                            this._COMPID = value;
                        }
                    }
                }
                public string COMPNAME
                {
                    get
                    {
                        return this._COMPNAME;
                    }
                    set
                    {
                        if ((this._COMPNAME != value))
                        {
                            this._COMPNAME = value;
                        }
                    }
                }
                public int SCHEDULE
                {
                    get
                    {
                        return this._SCHEDULENO;
                    }
                    set
                    {
                        if ((this._SCHEDULENO != value))
                        {
                            this._SCHEDULENO = value;
                        }
                    }
                }
                public int FILENO
                {
                    get
                    {
                        return this._FILENO;
                    }
                    set
                    {
                        if ((this._FILENO != value))
                        {
                            this._FILENO = value;
                        }
                    }
                }
                public string FILENAME
                {
                    get
                    {
                        return this._FILENAME;
                    }
                    set
                    {
                        if ((this._FILENAME != value))
                        {
                            this._FILENAME = value;
                        }

                    }
                }
                public string FILEPATH
                {
                    get
                    {
                        return this._FILEPATH;
                    }
                    set
                    {
                        if ((this._FILEPATH != value))
                        {
                            this._FILEPATH = value;
                        }
                    }
                }
                public string COLLEGECODE
                {
                    get
                    {
                        return this._COLLEGECODE;
                    }
                    set
                    {
                        if ((this._COLLEGECODE != value))
                        {
                            this._COLLEGECODE = value;
                        }
                    }
                }
                #endregion

            }//end class TrainingPlacement
        }//end namespace  BusinessLogicLayer.BusinessEntities
    }//end namespace NITPRM
}//end namespace IITMS

