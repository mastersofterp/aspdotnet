using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Operator
    {
        #region Private Fields

        int operatorNo;        
        string operatorName;
        int userNo;       
        int sessionNo;
        DateTime activationDate = DateTime.MinValue;
        bool isActive;
        string tableName;
        string menuLevel1_Perms = string.Empty;
        string menuLevel2_Perms = string.Empty;
        string menuLevel3_Perms = string.Empty;
        string collegeCode;
        string examName = string.Empty;

        
        #endregion

        #region Public Properties

        public int OperatorNo
        {
            get { return operatorNo; }
            set { operatorNo = value; }
        }

        public string OperatorName
        {
            get { return operatorName; }
            set { operatorName = value; }
        }

        public int UserNo
        {
            get { return userNo; }
            set { userNo = value; }
        }

        public int SessionNo
        {
            get { return sessionNo; }
            set { sessionNo = value; }
        }

        public DateTime ActivationDate
        {
            get { return activationDate; }
            set { activationDate = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        public string MenuLevel1_Perms
        {
            get { return menuLevel1_Perms; }
            set { menuLevel1_Perms = value; }
        }

        public string MenuLevel2_Perms
        {
            get { return menuLevel2_Perms; }
            set { menuLevel2_Perms = value; }
        }

        public string MenuLevel3_Perms
        {
            get { return menuLevel3_Perms; }
            set { menuLevel3_Perms = value; }
        }

        public string CollegeCode
        {
            get { return collegeCode; }
            set { collegeCode = value; }
        }

        public string ExamName
        {
            get { return examName; }
            set { examName = value; }
        }

        #endregion
    }
}