//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : Hostel Room Charge                                                        
// CREATION DATE : 18-Jan-2023                                                         
// CREATED BY    : SONALI BHOR
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBusinessLogicLayer.BusinessEntities.Hostel
{
    public class HostelRoomCharge
    {
    
      #region private 

     private int _CHARGE_NO=0;
     private int _SESSIONNO=0;
     private int _HOSTEL_NO=0;
     private int _ROOM_TYPE=0;
     private int _RESIDENT_TYPE=0;
     private Decimal _CHARGES;
     private int _COLLEGE_CODE;
     private int _ORGANIZATIONID;
     private int _USERNO;
     private string _IPADDRESS;
     private DateTime _ENTRY_DATE;
     private DateTime _UPDATE_DATE;
     private int _UPDATED_BY;

      #endregion

      #region Public Property Fields

     public int CHARGE_NO
     {
         get { return _CHARGE_NO; }
         set { _CHARGE_NO = value; }
     }
     public int SESSIONNO
     {
         get { return _SESSIONNO; }
         set { _SESSIONNO = value; }
     }
     public int HOSTEL_NO
     {
         get { return _HOSTEL_NO; }
         set { _HOSTEL_NO = value; }
     }
     public int ROOM_TYPE
     {
         get { return _ROOM_TYPE; }
         set { _ROOM_TYPE = value; }
     }
     public int RESIDENT_TYPE
     {
         get { return _RESIDENT_TYPE; }
         set { _RESIDENT_TYPE = value; }
     }
     public Decimal CHARGES
     {
         get { return _CHARGES; }
         set { _CHARGES = value; }
     }
     public int COLLEGE_CODE
     {
         get { return _COLLEGE_CODE; }
         set { _COLLEGE_CODE = value; }
     }
     public int ORGANIZATIONID
     {
         get { return _ORGANIZATIONID; }
         set { _ORGANIZATIONID = value; }
     }
     public int USERNO
     {
         get { return _USERNO; }
         set { _USERNO = value; }
     }
     public string IPADDRESS
     {
         get { return _IPADDRESS; }
         set { _IPADDRESS = value; }
     }
     public DateTime ENTRY_DATE
     {
         get { return _ENTRY_DATE; }
         set { _ENTRY_DATE = value; }
     }
     public DateTime UPDATE_DATE
     {
         get { return _UPDATE_DATE; }
         set { _UPDATE_DATE = value; }
     }
     public int UPDATED_BY
     {
         get { return _UPDATED_BY; }
         set { _UPDATED_BY = value; }
     }
     #endregion

    }
}
