using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class MeterTarrif
     {
         private int        _mID = 0;
         private int        _metertype = 0;
         private Int64       _meterunitFrom = 0;
         private Int64       _meterunitTO = 0;
         private decimal    _meterRate = 0;


         public int MID
         {
             get { return _mID; }
             set { _mID = value; }
         }
         

         public int Metertype
         {
             get { return _metertype; }
             set { _metertype = value; }
         }


         public Int64 MeterunitFrom
         {
             get { return _meterunitFrom; }
             set { _meterunitFrom = value; }
         }

         public Int64 MeterunitTO
         {
             get { return _meterunitTO; }
             set { _meterunitTO = value; }
         }
         

         public decimal MeterRate
         {
             get { return _meterRate; }
             set { _meterRate = value; }
         }
          

     }
}
