
using System;
using System.ComponentModel;
namespace IITMS
{    
    namespace UAIMS
    {
        /// <summary>
        /// These are Custom Status used to define the status
        /// </summary>
        public enum CustomStatus
        {
            RecordSaved = 1,                            //For Record Saved
            RecordUpdated = 2,                          //For Record Updation
            RecordDeleted = 3,                          //For Record Deletion
            RecordFound = 4,                            //For Record Found
            RecordNotFound = 5,                         //For Record Not Found
            TransactionFailed = -99,                    //For Transaction Failed
            FileExists = 999,                           //For File Exist
            InvalidUserNamePassword = 1000,             //For Invalid Username & Password
            ValidUser = 1001,                           //For Valid User
            Others = 99,                                //For Others
            Error = -999,                               //For Error
            DuplicateRecord = -1001,                     //For Duplicate Record
            FileNotExists = -1,                           //For File not exists
            TextFileCheck = -2,                           //For If not Text File
            TextFileSize = -3,                            //If File Size Less than TextFileSize in web.config file 
            RecordExist = 2627,

            [Description("This Services is Temporary Unavailable")]
            ExportExcel = 2001
        }
    }
}