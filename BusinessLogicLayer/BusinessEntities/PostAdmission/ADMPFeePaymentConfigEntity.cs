using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
---------------------------------------------------------------------------------------------------------------------------                                                                      
Created By  :                                                                 
Created On  :                                                    
Purpose     :                                      
Version     :                                                             
---------------------------------------------------------------------------------------------------------------------------                                                                        
Version   Modified On   Modified By        Purpose                                                                        
---------------------------------------------------------------------------------------------------------------------------                                                                        
1.0.1     14-03-2024    Isha               Added branchno,officevisitstartdate,officevisitenddate,provisionaladmissiondate                         
------------------------------------------- -------------------------------------------------------------------------------                                                                                                                     
 */
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ADMPFeePaymentConfigEntity
            {
                #region Declared Variable;


                /*CONFIGID   INT,  
                 ACTIVITYFOR  INT,   
                 ADMBATCH   INT,  
                 PROGRAMTYPE  INT,  
                 DEGREENO   INT,  
                 PAYMENTCATEGORY INT,  
                 FEEPAYMENT  NUMERIC(8,2),  
                 STARTDATE  DATETIME,  
                 ENDDATE   DATETIME,  
                 ACTIVITYSTATUS BIT */

                private int configid;
                private int activityfor;
                private int admbatch;
                private int programtype;
                private int degreeno;
                //<1.0.1>
                private string branchno;
                //</1.0.1>
                private int paymentcategory;
                private double feepayment;
                private DateTime startdate;
                private DateTime enddate;
                //<1.0.1>
                private DateTime officevisitstartdate;
                private DateTime officevisitenddate;
                private DateTime provisionaladmissiondate;
                //</1.0.1>
                private bool activitystatus;

                #endregion Declared Variable;

                #region Assign Value
                public int ConfigID
                {
                    get { return configid; }
                    set { configid = value; }
                }

                public int Activityfor
                {
                    get { return activityfor; }
                    set { activityfor = value; }
                }

                public int Admbatch
                {
                    get { return admbatch; }
                    set { admbatch = value; }
                }

                public int Programtype
                {
                    get { return programtype; }
                    set { programtype = value; }
                }

                public int Degreeno
                {
                    get { return degreeno; }
                    set { degreeno = value; }
                }
                //<1.0.1>
                public string Branchno
                {
                    get { return branchno; }
                    set { branchno = value; }
                }
                //</1.0.1>
                public int Paymentcategory
                {
                    get { return paymentcategory; }
                    set { paymentcategory = value; }
                }
                public double Feepayment
                {
                    get { return feepayment; }
                    set { feepayment = value; }
                }
                public DateTime Startdate
                {
                    get { return startdate; }
                    set { startdate = value; }
                }
                public DateTime Enddate
                {
                    get { return enddate; }
                    set { enddate = value; }
                }
                //<1.0.1>
                public DateTime OfficeVisitStartDate
                {
                    get { return officevisitstartdate; }
                    set { officevisitstartdate = value; }
                }
                public DateTime OfficeVisitEndDate
                { 
                    get { return officevisitenddate; }
                    set { officevisitenddate = value; }
                }

                public DateTime ProvisionalAdmissionDate
                {
                    get { return provisionaladmissiondate; }
                    set { provisionaladmissiondate = value; }
                }
                //</1.0.1>
                public bool Activitystatus
                {
                    get { return activitystatus; }
                    set { activitystatus = value; }
                }

            
                #endregion Assign Value
            }
        }
    }
}
