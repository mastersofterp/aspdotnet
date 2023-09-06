using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                private int paymentcategory;
                private double feepayment;
                private DateTime startdate;
                private DateTime enddate;
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
