//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : RECIEPT TYPE CLASS
// CREATION DATE : 14-MAY-2009                                                        
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class RecieptType
    {
        #region Private Members

        int _recieptTypeId = -1;
        string _recieptCode = string.Empty;
        string _recieptTitle = string.Empty;
        char _belongsTo;
        string _accountNumber = string.Empty;
        string _companyName = string.Empty;
        bool _isLinked = false;
        string _collegeCode = string.Empty;
        int _isadmission = 0;


        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public int RecieptTypeId
        {
            get { return _recieptTypeId; }
            set { _recieptTypeId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RecieptCode
        {
            get { return _recieptCode; }
            set { _recieptCode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RecieptTitle
        {
            get { return _recieptTitle; }
            set { _recieptTitle = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public char BelongsTo
        {
            get { return _belongsTo; }
            set { _belongsTo = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLinked
        {
            get { return _isLinked; }
            set { _isLinked = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        public int isadmission
        {
            get { return _isadmission; }
            set { _isadmission = value; }
        }
        #endregion
    }
}