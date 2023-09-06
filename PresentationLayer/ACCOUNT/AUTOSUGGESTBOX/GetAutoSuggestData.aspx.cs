using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using System.Transactions;




namespace ASB
{
	partial  class GetAutoSuggestData : System.Web.UI.Page
	{
		//Generate menu and return it
		private void Page_Load(object sender, System.EventArgs e)
		 {
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            
            string sKeyword		=Request.QueryString["Keyword"];
			string sTextBoxID	=Request.QueryString["TextBoxID"];
			string sDivID		=Request.QueryString["DivID"];
			string sDataType	=Request.QueryString["DataType"];

			//Get menu item labels and values
            if (Session["comp_code"]!= null)
            {
                ArrayList aMenuItems = LoadMenuItems(sDataType, sKeyword);
                ASBMenuItem oMenuItem;
                string sHtml;

                //Output menu items to the web page
                for (int nCount = 0; nCount < aMenuItems.Count; nCount++)
                {
                    oMenuItem = (ASBMenuItem)aMenuItems[nCount];
                    sHtml = oMenuItem.GenHtml(nCount + 1, sTextBoxID, sDivID);

                    Response.Write(sHtml + "\n\r");
                }
            
            }

			
						
			
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support ¯ do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

        		/// <summary>Get all cities that contain specified keyword</summary>
		/// <param name="sKeyword">Keyword to use in a query</param>
		/// <returns></returns>
        private ArrayList LoadClient(string sKeyword,string TextType){
			ArrayList aOut=new ArrayList();
            //string sConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath  ("Registration2.mdb");
            string sConnString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

            //if (Session["UserName"].ToString().Trim() != null && Session["Password"].ToString().Trim() != null && Session["DataBase"].ToString().Trim() != null)
            if (sConnString != null)
            {
                //string sConnString = "Password=" + Session["Password"].ToString().Trim() + ";User ID=" + Session["UserName"].ToString().Trim() + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + Session["DataBase"].ToString().Trim() + ";";

                SqlConnection oCn = new SqlConnection(sConnString);
                string sSql = string.Empty;
                //sSql = " select clientid as client, clientid from clientmaster where  clientid like '" + sKeyword.Replace("'", "''") + "%' and  clientid <>'' order by clientid ";
                if (TextType != "CashBank")
                {
                    if (Session["WithoutCashBank"] != null)
                    {

                        if (Session["WithoutCashBank"].ToString().Trim() == "Y")
                        {
                            sSql = "select ACC_CODE,UPPER(PARTY_NAME) AS PARTY_NAME,BALANCE,STATUS from ACC_" + Session["comp_code"].ToString() + "_PARTY  where  ((PARTY_NAME like '%" + sKeyword.Replace("'", "''") + "%' and  PARTY_NO <>'') or ACC_CODE='%" + sKeyword + "%')  and PAYMENT_TYPE_NO  NOT IN ('1','2') order by ACC_CODE ";
                        }
                       else if (Session["WithoutCashBank"].ToString().Trim() == "N")
                        {
                            sSql = "select ACC_CODE,UPPER(PARTY_NAME) AS PARTY_NAME,BALANCE,STATUS from ACC_" + Session["comp_code"].ToString() + "_PARTY  where  ((PARTY_NAME like '%" + sKeyword.Replace("'", "''") + "%' and  PARTY_NO <>'') or ACC_CODE='%" + sKeyword + "%')   order by ACC_CODE ";
                        }
                        else if (Session["WithoutCashBank"].ToString().Trim() == "YN")
                        {
                            sSql = "select ACC_CODE,UPPER(PARTY_NAME) AS PARTY_NAME,BALANCE,STATUS from ACC_" + Session["comp_code"].ToString() + "_PARTY where  ((PARTY_NAME like '%" + sKeyword.Replace("'", "''") + "%' and  PARTY_NO <>'') or ACC_CODE='%" + sKeyword + "%')  and PAYMENT_TYPE_NO  IN ('1','2') order by ACC_CODE ";
                        }
                        else
                        {
                            if (TextType == "CashBank")
                                sSql = "select ACC_CODE,UPPER(PARTY_NAME) AS PARTY_NAME,BALANCE,STATUS from ACC_" + Session["comp_code"].ToString() + "_PARTY where  ((PARTY_NAME like '%" + sKeyword.Replace("'", "''") + "%' and  PARTY_NO <>'') or ACC_CODE='%" + sKeyword + "%')  and PAYMENT_TYPE_NO IN ('1','2') order by ACC_CODE ";
                            else
                                sSql = "select ACC_CODE,UPPER(PARTY_NAME) AS PARTY_NAME,BALANCE,STATUS from ACC_" + Session["comp_code"].ToString() + "_PARTY where  ((PARTY_NAME like '%" + sKeyword.Replace("'", "''") + "%' and  PARTY_NO <>'') or ACC_CODE='%" + sKeyword + "%')  and  PARTY_NO <>''  order by ACC_CODE ";
                        }
                    }
                }
                else
                {
                    sSql = "select ACC_CODE,UPPER(PARTY_NAME) AS PARTY_NAME,BALANCE,STATUS from ACC_" + Session["comp_code"].ToString() + "_PARTY where  ((PARTY_NAME like '%" + sKeyword.Replace("'", "''") + "%' and  PARTY_NO <>'') or ACC_CODE like '%" + sKeyword + "%')  and PAYMENT_TYPE_NO IN ('1','2') order by ACC_CODE ";
                }

                if (TextType == "Bank")
                {
                    sSql = "select ACC_CODE,UPPER(PARTY_NAME) AS PARTY_NAME,BALANCE,STATUS from ACC_" + Session["comp_code"].ToString() + "_PARTY where  ((PARTY_NAME like '%" + sKeyword.Replace("'", "''") + "%' and  PARTY_NO <>'') or ACC_CODE='%" + sKeyword + "%')  and PAYMENT_TYPE_NO IN ('2') order by ACC_CODE ";
                }

                if (TextType == "BankCheque")
                {
                    sSql = "select bankno,bankname from ACC_BANK_DETAIL where bankname like '%" + sKeyword.Replace("'", "''") + "%'";
                }

                if (TextType == "BankAccount")
                {
                    sSql = "select a.accno,a.accname,b.bankname from ACC_" + Session["comp_code"].ToString() + "_BANKAC a,acc_bank_detail b where  a.bno=b.bankno and  a.accname like '%" + sKeyword.Replace("'", "''") + "%'";
                }

                if (TextType == "Payee")
                {
                    sSql = "select idno,partyname,address,accno from ACC_" + Session["comp_code"].ToString() + "_PAYEE  where  PARTYNAME like '%" + sKeyword.Replace("'", "''") + "%'  order by partyname";
                }

                if (TextType == "LedgerMerge")
                {
                    sSql = "select ACC_CODE,UPPER(PARTY_NAME) AS PARTY_NAME,BALANCE,STATUS from ACC_" + Session["comp_code"].ToString() + "_PARTY  where  ((PARTY_NAME like '%" + sKeyword.Replace("'", "''") + "%' and  PARTY_NO <>'') or ACC_CODE='%" + sKeyword + "%') and  PARTY_NO <>''  order by ACC_CODE ";
                }

                if (TextType == "CostCategory")
                {
                    sSql = "select Cat_ID,Category from Acc_" + Session["comp_code"].ToString() + "_CostCategory  where  Category like '" + sKeyword.Replace("'", "''") + "%' and  Cat_ID <>0  order by Cat_ID ";
                }
                if (TextType == "AccPayee")
                {
                    sSql = "select IDNO,PARTYNAME from Acc_" + Session["comp_code"].ToString() + "_PAYEE  where  PARTYNAME like '" + sKeyword.Replace("'", "''") + "%' and  IDNO <>0  order by IDNO ";
                }
                if (TextType == "AccountName")
                {
                    sSql = "select TRNO, ACCNAME from Acc_" + Session["comp_code"].ToString() + "_BANKAC  where  ACCNAME like '" + sKeyword.Replace("'", "''") + "%' and  TRNO <>0  order by TRNO ";
                }


                //if (TextType == "All")
                //{
                //    sSql = "select ACC_CODE,UPPER(PARTY_NAME) AS PARTY_NAME,BALANCE,STATUS from ACC_" + Session["comp_code"].ToString() + "_PARTY  where  (PAYMENT_TYPE_NO not in (1,2) or ACC_CODE='%" + sKeyword + "%') and  PARTY_NO <>''  order by ACC_CODE ";
                //}
                SqlCommand oCmd = new SqlCommand(sSql, oCn);
                oCn.Open();
                SqlDataReader oReader = oCmd.ExecuteReader();

                string Id;
                string Name;
                string BALANCE;

                ASBMenuItem oMenuItem;
                DataTable dt;

                if (TextType == "BankAccount")
                {
                    string accno = "";
                    string bankname = "";
                    string accname = "";
                    while (oReader.Read())
                    {
                        accno = oReader[0].ToString().Trim();
                        accname = oReader[1].ToString().Trim();
                        bankname = oReader[2].ToString().Trim();
                        oMenuItem = new ASBMenuItem();
                        
                        // Commented on 14/02/2020 to avoid the unnecessary symbols on LedgerHead page.
                        //oMenuItem.Value = accno + "*" + accname.ToString().Trim().PadRight(50, '¯') + "Bank:" + oReader[2].ToString().Trim();
                        //oMenuItem.Label = accno + "*" + accname.ToString().Trim().PadRight(50, '¯') + "Bank:" + oReader[2].ToString().Trim();

                        oMenuItem.Value = accno + "*" + accname.ToString().Trim() + "Bank:" + oReader[2].ToString().Trim();
                        oMenuItem.Label = accno + "*" + accname.ToString().Trim() + "Bank:" + oReader[2].ToString().Trim();

                        aOut.Add(oMenuItem);

                    }
                    oReader.Close();
                    oCn.Close();
                    return aOut;
                }
                else if (TextType == "BankCheque")
                {
                    string bankno = "";
                    string bankname = "";
                    while (oReader.Read())
                    {
                        bankno = oReader[0].ToString().Trim();
                        bankname = oReader[1].ToString().Trim();
                        oMenuItem = new ASBMenuItem();
                        oMenuItem.Value = bankno + "*" + bankname.ToString().Trim();
                        oMenuItem.Label = bankno + "*" + bankname.ToString().Trim();
                        aOut.Add(oMenuItem);
                    }
                    oReader.Close();
                    oCn.Close();
                    return aOut;
                }
                else if (TextType == "Payee")
                {
                    string pid = "";
                    string pname = "";
                    string padd = "";
                    string pacc = "";
                    while (oReader.Read())
                    {
                        pid = oReader[0].ToString().Trim();
                        pname = oReader[1].ToString().Trim();
                        padd = oReader[2].ToString().Trim();
                        pacc = oReader[3].ToString().Trim();

                        oMenuItem = new ASBMenuItem();
                        oMenuItem.Value = pid + "*" + pname.ToString().Trim().PadRight(50, '¯') + "A/cNo:" + pacc.ToString().Trim() + "Add:" + padd;
                        oMenuItem.Label = pid + "*" + pname.ToString().Trim().PadRight(50, '¯') + "A/cNo:" + pacc.ToString().Trim() + "Add:" + padd;
                        aOut.Add(oMenuItem);
                    }
                    oReader.Close();
                    oCn.Close();
                    return aOut;
                }
                else if (TextType == "CostCategory")
                {
                    string Cat_ID = string.Empty;
                    string Category = string.Empty; 
                    
                    while (oReader.Read())
                    {
                        Cat_ID = oReader[0].ToString().Trim();
                        Category = oReader[1].ToString().Trim();
                        
                        oMenuItem = new ASBMenuItem();
                        oMenuItem.Value = Cat_ID + "*" + Category.ToString().Trim().PadRight(50, '¯');
                        oMenuItem.Label = Cat_ID + "*" + Category.ToString().Trim().PadRight(50, '¯');
                        aOut.Add(oMenuItem);
                    }
                    oReader.Close();
                    oCn.Close();
                    return aOut;
                }
                else if (TextType == "AccPayee")
                {
                    string IDNO = string.Empty;
                    string PARTYNAME = string.Empty;

                    while (oReader.Read())
                    {
                        IDNO = oReader[0].ToString().Trim();
                        PARTYNAME = oReader[1].ToString().Trim();

                        oMenuItem = new ASBMenuItem();
                        oMenuItem.Value = IDNO + "*" + PARTYNAME.ToString().Trim().PadRight(50, '¯');
                        oMenuItem.Label = IDNO + "*" + PARTYNAME.ToString().Trim().PadRight(50, '¯');
                        aOut.Add(oMenuItem);
                    }
                    oReader.Close();
                    oCn.Close();
                    return aOut;
                }
                else if (TextType == "AccountName")
                {
                    string TRNO = string.Empty;
                    string ACCNAME = string.Empty;

                    while (oReader.Read())
                    {
                        TRNO = oReader[0].ToString().Trim();
                        ACCNAME = oReader[1].ToString().Trim();

                        oMenuItem = new ASBMenuItem();
                        oMenuItem.Value = TRNO + "*" + ACCNAME.ToString().Trim().PadRight(50, '¯');
                        oMenuItem.Label = TRNO + "*" + ACCNAME.ToString().Trim().PadRight(50, '¯');
                        aOut.Add(oMenuItem);
                    }
                    oReader.Close();
                    oCn.Close();
                    return aOut;
                }
                else
                {

                    while (oReader.Read())
                    {
                        //Build label using City, Country & State
                        Id = oReader[0].ToString().Trim();
                        Name = oReader[1].ToString().Trim();
                        
                        BALANCE = oReader[2].ToString().Trim();
                        
                        //BALANCE =Convert.ToInt32( oReader[2].ToString().Trim())<0?(-(Convert.ToInt32( oReader[2].ToString().Trim()))).ToString():oReader[2].ToString().Trim();

                        //if (oReader[3].ToString().Trim().ToLower()=="d")
                        //{
                        //    if (Convert.ToDouble(BALANCE) < 0)
                        //        BALANCE = Convert.ToString( -(Convert .ToDouble( BALANCE)));
                        //}
                        
                        //sLabel=sCity;
                        string GotString = string.Empty;
                        if (Session["Datatable"] != null)
                        {
                            dt = Session["Datatable"] as DataTable;
                            int i = 0;
                            if (dt.Rows.Count != 0)
                            {

                                for (i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    if (Id + "*" + Name.ToString().Trim() == dt.Rows[i]["Particulars"].ToString().Trim())
                                    {
                                        GotString = "Y";
                                        if (aOut != null)
                                        {
                                            if (aOut.Count != 0)
                                            {
                                                int j;
                                                string gotvalue = string.Empty;
                                                for (j = 0; j <= aOut.Count - 1; j++)
                                                {
                                                    oMenuItem = new ASBMenuItem();
                                                    oMenuItem = aOut[j] as ASBMenuItem;
                                                    if (oMenuItem.Value.ToString().Trim() == Id + "*" + Name.ToString().Trim())
                                                    {
                                                        gotvalue = "Y";
                                                        //if (Convert.ToDouble(dt.Rows[i]["Balance"]) > 0)
                                                        //{
                                                        //    oMenuItem.Label = Id + "*" + Name.PadRight(50, '¯') + "Bal:" + dt.Rows[i]["Balance"].ToString().Trim() + ":" + "Cr"; //oReader[3].ToString().Trim() + "r"; ;
                                                        //}
                                                        //else
                                                        //{
                                                        //    oMenuItem.Label = Id + "*" + Name.PadRight(50, '¯') + "Bal:" + dt.Rows[i]["Balance"].ToString().Trim() + ":" + "Dr";
                                                        //}

                                                        if (Convert.ToDouble(BALANCE) > 0)
                                                        {
                                                            oMenuItem.Label = Name + "*" + Id.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Dr";
                                                        }
                                                        else
                                                        {
                                                            oMenuItem.Label = Name + "*" + Id.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Cr"; //oReader[3].ToString().Trim() + "r"; ;
                                                        }
                                                    }


                                                }
                                                if (gotvalue == "")
                                                {
                                                    oMenuItem = new ASBMenuItem();
                                                    //if (Convert.ToDouble(dt.Rows[i]["Balance"]) > 0)
                                                    //{
                                                    //    oMenuItem.Label = Id + "*" + Name.PadRight(50, '¯') + "Bal:" + dt.Rows[i]["Balance"].ToString().Trim() + ":" + "Cr"; //oReader[3].ToString().Trim() + "r"; ;
                                                    //}
                                                    //else
                                                    //{
                                                    //    oMenuItem.Label = Id + "*" + Name.PadRight(50, '¯') + "Bal:" + dt.Rows[i]["Balance"].ToString().Trim() + ":" + "Dr";
                                                    //}

                                                    if (Convert.ToDouble(BALANCE) > 0)
                                                    {
                                                        oMenuItem.Label = Name + "*" + Id.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Dr";
                                                    }
                                                    else
                                                    {
                                                        oMenuItem.Label = Name + "*" + Id.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Cr"; //oReader[3].ToString().Trim() + "r"; ;
                                                    }
                                                    oMenuItem.Value = Id + "*" + Name.ToString().Trim();
                                                    aOut.Add(oMenuItem);

                                                }

                                            }
                                            else
                                            {
                                                oMenuItem = new ASBMenuItem();
                                                if (Convert.ToDouble(dt.Rows[i]["Balance"]) > 0)
                                                {
                                                    oMenuItem.Label = Name + "*" + Id.PadRight(50, '¯') + "Bal:" + dt.Rows[i]["Balance"].ToString().Trim() + ":" + "Cr"; //oReader[3].ToString().Trim() + "r"; ;

                                                }
                                                else
                                                {
                                                    oMenuItem.Label = Name + "*" + Id.PadRight(50, '¯') + "Bal:" + dt.Rows[i]["Balance"].ToString().Trim() + ":" + "Dr";
                                                }
                                                oMenuItem.Value = Id + "*" + Name.ToString().Trim();
                                                aOut.Add(oMenuItem);
                                            }

                                        }
                                        else
                                        {
                                            oMenuItem = new ASBMenuItem();

                                            if (Convert.ToDouble(dt.Rows[i]["Balance"]) > 0)
                                            {
                                                oMenuItem.Label = Name + "*" + Id.PadRight(50, '¯') + "Bal:" + dt.Rows[i]["Balance"].ToString().Trim() + ":" + "Cr"; //oReader[3].ToString().Trim() + "r"; ;

                                            }
                                            else
                                            {
                                                oMenuItem.Label = Name + "*" + Id.PadRight(50, '¯') + "Bal:" + dt.Rows[i]["Balance"].ToString().Trim() + ":" + "Dr";
                                            }
                                            oMenuItem.Value = Name + "*" + Id.ToString().Trim();
                                            aOut.Add(oMenuItem);
                                        }

                                    }

                                }
                                if (GotString == "")
                                {
                                    oMenuItem = new ASBMenuItem();
                                    //oMenuItem.Label = Id + "." + Name.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + oReader[3].ToString().Trim() + "r";

                                    //if (Convert.ToDouble(BALANCE) > 0)
                                    //{
                                    //    oMenuItem.Label = Id + "*" + Name.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Cr"; //oReader[3].ToString().Trim() + "r"; ;

                                    //}
                                    //else
                                    //{
                                    //    oMenuItem.Label = Id + "*" + Name.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Dr";
                                    //}

                                    if (Convert.ToDouble(BALANCE) > 0)
                                    {
                                        oMenuItem.Label = Name + "*" + Id.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Dr";
                                    }
                                    else
                                    {
                                        oMenuItem.Label = Name + "*" + Id.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Cr"; //oReader[3].ToString().Trim() + "r"; ;
                                    }

                                    oMenuItem.Value = BALANCE.ToString().Trim();
                                    aOut.Add(oMenuItem);
                                }

                            }
                            else
                            {
                                oMenuItem = new ASBMenuItem();
                                //oMenuItem.Label = Id + "." + Name.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + oReader[3].ToString().Trim() + "r"; ;
                                if (Convert.ToDouble(BALANCE) > 0)
                                {
                                    //oMenuItem.Label = Id + "*" + Name.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Cr"; //oReader[3].ToString().Trim() + "r"; ;
                                    oMenuItem.Label = Name + "*" + Id.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Dr";
                                }
                                else
                                {
                                   // BALANCE = Convert.ToString(-Convert.ToDouble(BALANCE));//oldk
                                    oMenuItem.Label = Name + "*" + Id.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Cr"; //oReader[3].ToString().Trim() + "r"; ;
                                    //oMenuItem.Label = Id + "*" + Name.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Dr";
                                }
                                oMenuItem.Value = BALANCE.ToString().Trim();
                                aOut.Add(oMenuItem);
                            }

                        }
                        else
                        {
                            oMenuItem = new ASBMenuItem();
                            //oMenuItem.Label = Id + "." + Name.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + oReader[3].ToString().Trim() + "r"; ;
                            if (Convert.ToDouble(BALANCE) > 0)
                            {
                                //oMenuItem.Label = Id + "*" + Name.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Cr"; //oReader[3].ToString().Trim() + "r"; ;
                                oMenuItem.Label = Name + "*" + Id.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Dr"; //oReader[3].ToString().Trim() + "r"; ;

                            }
                            else
                            {
                                //oMenuItem.Label = Id + "*" + Name.PadRight(50, '¯') + "Bal:" + BALANCE.ToString().Trim() + ":" + "Dr";
                                oMenuItem.Label = Name + "*" + Id.PadRight(50, '¯') + "Bal:" + (-(Convert.ToDouble(BALANCE))).ToString().Trim() + ":" + "Cr";
                            }
                            oMenuItem.Value = BALANCE.ToString().Trim();
                            aOut.Add(oMenuItem);

                        }
                    }
                    oReader.Close();
                    oCn.Close();
                    
                }
            }
            return aOut;
		}



		/// <summary>Load array of ASBMenuItems for a specific data type</summary>
		/// <param name="sDataType"></param>
		/// <param name="sKeyword"></param>
		/// <returns></returns>
		private ArrayList LoadMenuItems(string sDataType, string sKeyword) 
		{
			ArrayList aMenuItems;

			switch(sDataType)
			{
				case "CashBank":
					aMenuItems=LoadClient(sKeyword,"CashBank");
					break;
                case "All":
                    aMenuItems = LoadClient(sKeyword, "All");
                    break;
                case "Bank":
                    aMenuItems = LoadClient(sKeyword, "Bank");
                    break;
                case "BankCheque":
                    aMenuItems = LoadClient(sKeyword, "BankCheque");
                    break;
                case "BankAccount":
                    aMenuItems = LoadClient(sKeyword, "BankAccount");
                    break;
                case "Payee":
                    aMenuItems = LoadClient(sKeyword, "Payee");
                    break;
                case "LedgerMerge":
                    aMenuItems = LoadClient(sKeyword, "LedgerMerge");
                    break;
                case "CostCategory":
                    aMenuItems = LoadClient(sKeyword, "CostCategory");
                    break;
                case "AccPayee":
                    aMenuItems = LoadClient(sKeyword, "AccPayee");
                    break;
                case "AccountName":
                    aMenuItems = LoadClient(sKeyword, "AccountName");
                    break;
                    
				default:
					throw new Exception("GetAutoSuggestData  Type '" + sDataType + "' is not supported.");
			}


			return aMenuItems;
		}	

	}
}
