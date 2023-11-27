using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessEntities
{
    public class Funding_Agency_Entity
    {
        private string _FAGENCY;

        private int _AGENCYID;



        //Public Member
        public string FAGENCY
        {
            get
            {
                return this._FAGENCY;
            }
            set
            {
                if ((this._FAGENCY != value))
                {
                    this._FAGENCY = value;
                }
            }
        }


        public int AGENCYID
        {
            get
            {
                return this._AGENCYID;
            }
            set
            {
                if ((this._AGENCYID != value))
                {
                    this._AGENCYID = value;
                }
            }
        }
    }

}
