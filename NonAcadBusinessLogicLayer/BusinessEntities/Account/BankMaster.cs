using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class BankMaster
            {
                #region Private Members
                private int _BANKNO = 0;
                private string _BANKNAME = string.Empty;
                private double _CHECKHEIGHT = 0;
                private double _CHECKWIDTH = 0;
                private double _PARTYTOP = 0;
                private double _PARTYLEFT = 0;
                private double _AMTTOP = 0;
                private double _AMTWIDTH = 0;
                private double _AMTLEFT = 0;
                private double _WAMTTOP = 0;
                private double _WAMTWIDTH = 0;
                private double _WAMTLEFT = 0;
                private double _CHECKTOP = 0;
                private double _CHECKLEFT = 0;
                private double _CHECKRIGHT = 0;
                private double _CKDTTOP = 0;
                private double _CKDTLEFT = 0;
                private double _CKDTWIDTH = 0;
                private double _PARTYWIDTH = 0;
                private double _STAMP1TOP = 0;
                private double _STAMP1LEFT = 0;
                private double _STAMP1WIDTH = 0;
                private double _STAMP2TOP = 0;
                private double _STAMP2LEFT = 0;
                private double _STAMP2WIDTH = 0;
             
              #endregion


                #region Public
                public int BANKNO
                {
                    get { return _BANKNO; }
                    set { _BANKNO = value; }
                }
                public string BANKNAME
                {
                    get { return _BANKNAME; }
                    set { _BANKNAME = value; }
                }
                public double CHECKHEIGHT
                {
                    get { return _CHECKHEIGHT; }
                    set { _CHECKHEIGHT = value; }
                }
                public double CHECKWIDTH
                {
                    get { return _CHECKWIDTH; }
                    set { _CHECKWIDTH = value; }
                }
                public double PARTYTOP
                {
                    get { return _PARTYTOP; }
                    set { _PARTYTOP = value; }
                }
                public double PARTYLEFT
                {
                    get { return _PARTYLEFT; }
                    set { _PARTYLEFT = value; }
                }
                public double AMTTOP
                {
                    get { return _AMTTOP; }
                    set { _AMTTOP = value; }
                }
                public double AMTWIDTH
                {
                    get { return _AMTWIDTH; }
                    set { _AMTWIDTH = value; }
                }
                public double AMTLEFT
                {
                    get { return _AMTLEFT; }
                    set { _AMTLEFT = value; }
                }
                public double WAMTTOP
                {
                    get { return _WAMTTOP; }
                    set { _WAMTTOP = value; }
                }
                public double WAMTWIDTH
                {
                    get { return _WAMTWIDTH; }
                    set { _WAMTWIDTH = value; }
                }
                public double WAMTLEFT
                {
                    get { return _WAMTLEFT; }
                    set { _WAMTLEFT = value; }
                }
                public double CHECKTOP
                {
                    get { return _CHECKTOP; }
                    set { _CHECKTOP = value; }
                }
                public double CHECKLEFT
                {
                    get { return _CHECKLEFT; }
                    set { _CHECKLEFT = value; }
                }
                public double CHECKRIGHT
                {
                    get { return _CHECKRIGHT; }
                    set { _CHECKRIGHT = value; }
                }
                public double CKDTTOP
                {
                    get { return _CKDTTOP; }
                    set { _CKDTTOP = value; }
                }
                public double CKDTLEFT
                {
                    get { return _CKDTLEFT; }
                    set { _CKDTLEFT = value; }
                }
                public double CKDTWIDTH
                {
                    get { return _CKDTWIDTH; }
                    set { _CKDTWIDTH = value; }
                }
                public double PARTYWIDTH
                {
                    get { return _PARTYWIDTH; }
                    set { _PARTYWIDTH = value; }
                }
                public double STAMP1TOP
                {
                    get { return _STAMP1TOP; }
                    set { _STAMP1TOP = value; }
                }
                public double STAMP1LEFT
                {
                    get { return _STAMP1LEFT; }
                    set { _STAMP1LEFT = value; }
                }
                public double STAMP1WIDTH
                {
                    get { return _STAMP1WIDTH; }
                    set { _STAMP1WIDTH = value; }
                }
                public double STAMP2TOP
                {
                    get { return _STAMP2TOP; }
                    set { _STAMP2TOP = value; }
                }
                public double STAMP2LEFT
                {
                    get { return _STAMP2LEFT; }
                    set { _STAMP2LEFT = value; }
                }
                public double STAMP2WIDTH
                {
                    get { return _STAMP2WIDTH; }
                    set { _STAMP2WIDTH = value; }
                }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS