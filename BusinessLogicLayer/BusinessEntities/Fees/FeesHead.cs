//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS ENTITIES FILE [FEES HEAD DEFINATION]                        
// CREATION DATE : 12-MAY-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================


using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {

            public class FeesHead
            {
                #region Private Member 
                private int _feeTitleNo = 0;
                private string _feeHead = string.Empty;
                private string _recieptCode = string.Empty;
                private string _feeShortName = string.Empty;
                private string _feeLongName = string.Empty;
                private int _collegeCode = 0;
                #endregion

                #region Public Property Fields
                    public int FeeTitleNo
                    {
                        get { return _feeTitleNo; }
                        set { _feeTitleNo = value; }
                    }
                    public string FeeHead
                    {
                        get { return _feeHead; }
                        set { _feeHead = value; }
                    }
                    public string RecieptCode
                    {
                        get { return _recieptCode; }
                        set { _recieptCode = value; }
                    }
                    public string FeeShortName
                    {
                        get { return _feeShortName; }
                        set { _feeShortName = value; }
                    }
                    public string FeeLongName
                    {
                        get { return _feeLongName; }
                        set { _feeLongName = value; }
                    }
                    public int CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                } 
                #endregion
            }
        }
    }
}
