using System;
using System.Web.UI;
using System.Data;
using System.Text;
using System.Collections;
using System.IO;
using AjaxControlToolkit;
namespace IITMS
{
    
    /// <summary>
    /// Summary description for Common Methods
    /// </summary>
    public class UAIMS_Common
    {
        /// <summary>
        /// ConnectionStrings
        /// </summary>
        string _constr = string.Empty;

        
        /// <summary>
        /// This method shows error in a ModalBox
        /// </summary>
        /// <param name="pg">Parameter pg is used to get page</param>
        /// <param name="errormessage">This string parameter is used to display error message</param>
        public void ShowError(Page pg, string errormessage)
        {
            //Get Reference for ajaxToolkit:ModalPopupExtender
            //UpdatePanel upError = pg.Master.FindControl("upError") as UpdatePanel;
            //ModalPopupExtender modal = upError.FindControl("programmaticModalPopup") as ModalPopupExtender;
            //ModalPopupExtender modal = pg.Master.FindControl("programmaticModalPopup") as ModalPopupExtender;
            //System.Web.UI.WebControls.Label lblError = pg.Master.FindControl("lblError") as System.Web.UI.WebControls.Label;
         // lblError.Text = errormessage;
            //modal.Show();
            
            
        }
        public void DisplayMessage(Control UpdatePanelId, string Message, Page pg)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanelId, UpdatePanelId.GetType(), "Message", " alert('" + Message + "');", true);
        }
    }

}//END namespace IITMS