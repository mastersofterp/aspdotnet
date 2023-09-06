<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Lwp_Entry.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_Lwp_Entry"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px">
                        LWP ENTRY&nbsp;
                        <!-- Button used to launch the help (animation) -->
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                    </td>
                </tr>
                <%--PAGE HELP--%>
                <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
               <%-- </tr>--%>
                <tr>
                    <td>
                         <p class="page_help_text">
                                <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div style="text-align: left; width: 80%; padding-left: 10px;">
                                <fieldset class="fieldsetPay">
                                    <legend class="legendPay">ADD/EDIT LWP ENTRY</legend>
                                    <br>
                                    <table>
                                        <tr>
                                            <td class="form_left_label">
                                                Select Month For Lwp Entry :<span style="color: Red">*</span>
                                            </td>
                                            <td class="form_left_text">
                                                <asp:TextBox ID="txtMonthYear" runat="server" Width="60px" onblur="return checkdate(this);"></asp:TextBox>
                                                &nbsp;<asp:Image ID="ImaMonYear" runat="server" ImageUrl="~/images/calendar.png"
                                                    Style="cursor: pointer" />
                                                <ajaxToolKit:CalendarExtender ID="ceMonthYear" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="MM/yyyy" PopupButtonID="ImaMonYear" TargetControlID="txtMonthYear">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtMonthYear"
                                                    Display="None" ErrorMessage="Please Select Month For Salary Donation in (MM/YYYY Format)"
                                                    SetFocusOnError="True" ValidationGroup="payroll">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label">
                                                College :<span style="color: Red">*</span>
                                            </td>
                                            <td class="form_left_text">
                                                <asp:DropDownList ID="ddlCollege" runat="server" Width="400px" AppendDataBoundItems="true"
                                                  AutoPostBack="true"  TabIndex="19" onselectedindexchanged="ddlCollege_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                    ValidationGroup="payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                                    InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label">
                                                Employee Name :<span style="color: Red">*</span>
                                            </td>
                                            <td class="form_left_text">
                                                <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" Width="400px"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee Name"
                                                    ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                                <td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label">
                                                From Date :<span style="color: Red">*</span>
                                            </td>
                                            <td class="form_left_text">
                                                <asp:TextBox ID="txtFromDate" runat="server" Width="80px"></asp:TextBox>
                                                &nbsp;<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                    PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                    Display="None" ErrorMessage="Please Select From Date in (dd/MM/yyyy Format)"
                                                    ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                    ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter mm/dd/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="payroll" SetFocusOnError="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label">
                                                To Date :<span style="color: Red">*</span>
                                            </td>
                                            <td class="form_left_text">
                                                <asp:TextBox ID="txtToDate" runat="server" Width="80px"></asp:TextBox>
                                                &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                    PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                    Display="None" ErrorMessage="Please Select To Date in (dd/MM/yyyy Format)" ValidationGroup="payroll"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                    ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                <asp:CompareValidator ID="cvToDate" runat="server" Display="None" ErrorMessage="To Date Should be Greater than  or equal to Leave from Date"
                                                    ValidationGroup="payroll" SetFocusOnError="True" ControlToCompare="txtFromDate"
                                                    ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                &nbsp
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" OnClick="btnSave_Click"
                                                    Width="80px" />&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                    OnClick="btnCancel_Click" Width="80px" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                &nbsp
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 10px">
                        <asp:Panel ID="pnlList" runat="server">
                            <table cellpadding="0" cellspacing="0" style="width: 81%;">
                                <tr>
                                    <td align="center">
                                        <asp:ListView ID="lvLwpDetails" runat="server">
                                            <EmptyDataTemplate>
                                                <br />
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div id="demo-grid" class="vista-grid">
                                                    <div class="titlebar">
                                                        LWP Details</div>
                                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr class="header">
                                                            <th>
                                                                Action
                                                            </th>
                                                            <th>
                                                                LWP DATE
                                                            </th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("LWPNO")%>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("LWPNO") %>'
                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                            OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("LWPDATE", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("LWPNO")%>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("LWPNO") %>'
                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                            OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("LWPDATE", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
            <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                BackgroundCssClass="modalBackground" />
            <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                <div style="text-align: center">
                    <table>
                        <tr>
                            <td align="center">
                                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                            </td>
                            <td>
                                &nbsp;&nbsp;Are you sure you want to delete this record?
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                                <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>

            <script type="text/javascript">
    //  keeps track of the delete button for the row
    //  that is going to be removed
    var _source;
    // keep track of the popup div
    var _popup;
    
    function showConfirmDel(source){
        this._source = source;
        this._popup = $find('mdlPopupDel');
        
        //  find the confirm ModalPopup and show it    
        this._popup.show();
    }
    
    function okDelClick(){
        //  find the confirm ModalPopup and hide it    
        this._popup.hide();
        //  use the cached button as the postback source
        __doPostBack(this._source.name, '');
    }
    
    function cancelDelClick(){
        //  find the confirm ModalPopup and hide it 
        this._popup.hide();
        //  clear the event source
        this._source = null;
        this._popup = null;
    }
    
    
    
    
    function checkdate(input)
            {  
                var validformat=/^\d{2}\/\d{4}$/ //Basic check for format validity
                var returnval=false
                if (!validformat.test(input.value))
                {
                   alert("Invalid Date Format. Please Enter in MM/YYYY Formate")
                   document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value="";
                   document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
                }
                else
                {
                    var monthfield=input.value.split("/")[0]

                    if(monthfield > 12 || monthfield <=0) 
                    {
                      alert("Month Should be greate than 0 and less than 13");
                      document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value="";
                      document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();                        
                    }
                }     
            }
            
    
    
    
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
