using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class DSRTax
    {
        private int _DSRTNO;
        private int _DSRTRNO;
        private string _TaxName;
        private Double _Qty;
        private Char _Type;
        private int _Calon;
        private double _Percentage;
        private double _Amount;

        public int DSRTNO
        {
            get 
            {
                return _DSRTNO;
            }
            set 
            {
                if (_DSRTNO != value)
                {
                     _DSRTNO = value;
                }
            }
        }
        public int DSRTRNO
        {
            get 
            {
                return _DSRTRNO;
            }
            set 
            {
                if (_DSRTRNO != value)
                {
                    _DSRTRNO = value; 
                }
            }
        }
        public string TaxName
        {
            get 
            {
                return _TaxName;
            }
            set 
            {
                if (_TaxName != value)
                {
                    _TaxName = value;
                }
            }
        }
        public double Qty
        {
            get 
            {
                return _Qty;
            }
            set 
            {
                if (_Qty != value)
                {
                    _Qty = value;
                }
            }
        }
        public char Type
        {
            get 
            {
                return _Type;
            }
            set 
            {
                if (_Type != value)
                {
                    _Type = value;
                }
            }
        }
        public int Calon
        {
            get 
            {
                return _Calon;
            }
            set 
            {
                if (_Calon != value)
                {
                    _Calon = value;
                }
            }
        }
        public double Percentage
        {
            get 
            {
                return _Percentage;
            }
            set 
            {
                if (_Percentage != value)
                {
                    _Percentage = value;
                }
            }
        }
        public double Amount 
        {
            get 
            {
                return _Amount;
            }
            set 
            {
                if (_Amount != value)
                {
                    _Amount = value;
                }
            }
        } 


    }
}
