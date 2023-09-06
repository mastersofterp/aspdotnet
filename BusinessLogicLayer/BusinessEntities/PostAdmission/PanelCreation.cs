using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities
{
   public class PanelCreation
   {
       #region Private Member
       private int _panelid = 0;
       private int _Batchno = 0;
       private int _Degreeno = 0;
       private int _Branchno = 0;
       private int _Scheduleno = 0;
       private string _Panelname = string.Empty;
       private int _panelfor = 0;
       private string _staff = string.Empty;
      
       #endregion

       #region public members
       public int panelid
       {
           get
           {
               return _panelid;
           }
           set
           {
               _panelid = value;
           }
       }

       public int Batchno
       {
           get
           {
               return _Batchno;
           }
           set
           {
               _Batchno = value;
           }
       }

       public int Degreeno
       {
           get
           {
               return _Degreeno;
           }
           set
           {
               _Degreeno = value;
           }
       }

       public int Branchno
       {
           get
           {
               return _Branchno;
           }
           set
           {
               _Branchno = value;
           }
       }
       public int Scheduleno
       {
           get
           {
               return _Scheduleno;
           }
           set
           {
               _Scheduleno = value;
           }
       }
       
       public string Panelname
       {
           get
           {
               return _Panelname;
           }
           set
           {
               _Panelname = value;
           }
       }
       public int panelfor
       {
           get
           {
               return _panelfor;
           }
           set
           {
               _panelfor = value;
           }
       }
       public string staff
       {
           get
           {
               return _staff;
           }
           set
           {
               _staff = value;
           }
       }

       #endregion
   }
}
