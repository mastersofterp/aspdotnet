<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Transfer_Attendance.aspx.cs" Inherits="Transfer_Attendance" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">TRANSFER ATTENDANCE/LWP RECORD</h3>
                </div>

                <div class="box-body">
                    <asp:UpdatePanel ID="updTdt" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlSelection" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Transfer Attendance/LWP</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr1" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College Name" data-select2-enable="true"
                                                AppendDataBoundItems="true" TabIndex="1">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please select College" SetFocusOnError="true"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr2" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlstafftype" runat="server" CssClass="form-control" ToolTip="Select Staff Type" data-select2-enable="true"
                                                AppendDataBoundItems="true" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlstafftype"
                                                Display="None" ErrorMessage="Please Select Staff" SetFocusOnError="true"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalFromDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="3" CssClass="form-control"
                                                    ToolTip="Enter From Date" Style="z-index: 0;" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                                    Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="true"
                                                    ValidationGroup="Payroll" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalToDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" TabIndex="4" CssClass="form-control"
                                                    ToolTip="Enter To Date" Style="z-index: 0;" AutoPostBack="True" OnTextChanged="txtToDate_TextChanged" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                                    Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true"
                                                    ValidationGroup="Payroll" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary"
                                        ValidationGroup="Payroll" TabIndex="5" OnClick="btnShow_Click" ToolTip="CLick here to Show" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                        TabIndex="6" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                    <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="false" Width="123px" />
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnShow" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <asp:Panel ID="Panel1" runat="server" Visible="true">
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" ToolTip="Click here to Edit" Visible="false"
                                ValidationGroup="Payroll" TabIndex="6" OnClick="btnEdit_Click" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Transfer/Update" OnClientClick="showConfirmUpdate(this); return false;"
                                CssClass="btn btn-primary" ToolTip="Click here to update/transfer" Visible="false" OnClick="btnUpdate_Click" />
                        </div>
                    </asp:Panel>
                    <div class="col-12">
                        <asp:Panel ID="pnlView" runat="server" Visible="false">
                            <asp:ListView ID="lvEmpList" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Employee Not Found" />
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Absent Days Record</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <%-- <th width="1%">
                                                                    <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Select IDNO" Enabled="true"
                                                                        runat="server" onclick="checkAllEmployees(this)" />
                                                                </th>  --%>
                                                <th>IDNO
                                                                    
                                                </th>
                                                <th>NAME
                                                </th>
                                                <%--  <th width="2%">
                                                                    DEPARTMENT
                                                                </th>--%>
                                                <th>LWP DAYS
                                                </th>
                                                <th>UPDATED LWP DAYS
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <%--  <td width="1%">
                                                        <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' 
                                                            ToolTip='<%#Eval("IDNO")%>'/>
                                                    </td>--%>
                                        <td>
                                            <%--  <%# Eval("IDNO")%>--%>

                                            <asp:Label ID="lblidno" runat="server" Text='<%# Eval("IDNO")%>' Enabled="false"></asp:Label>

                                        </td>
                                        <td>
                                            <%# Eval("NAME")%>
                                        </td>
                                        <%--<td width="20%">
                                                      <%# Eval("SUBDEPT")%>
                                                    </td>--%>
                                        <td>
                                            <asp:TextBox ID="txtleave" runat="server" Text='<%# Eval("LEAVES")%>' Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtleaveUpdated" runat="server" Text='<%# Eval("LEAVE_UPDATED")%>' Enabled="false" MaxLength="3"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbecl" runat="server" TargetControlID="txtleaveUpdated" FilterType="Numbers,Custom" ValidChars=".1234567890"></ajaxToolKit:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%-- Flash the text/border red and fade in the "close" button --%>
        <%-- <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll" 
                    DisplayMode="List" ShowMessageBox="false" ShowSummary="false" />--%>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/Images/edit.png" AlternateText="Edit Record" />
                            Edit Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                    // Move an element directly on top of another element (and optionally
                    // make it the same size)
                    function Cover(bottom, top, ignoreSize) {
                        var location = Sys.UI.DomElement.getLocation(bottom);
                        top.style.position = 'absolute';
                        top.style.top = location.y + 'px';
                        top.style.left = location.x + 'px';
                        if (!ignoreSize) {
                            top.style.height = bottom.offsetHeight + 'px';
                            top.style.width = bottom.offsetWidth + 'px';
                        }
                    }
                </script>
            </td>
        </tr>
    </table>
    <br />
    <%--<asp:UpdatePanel ID="updTdt" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlSelection" runat="server" Width="70%">
                <fieldset class="fieldsetPay">
                    <legend class="legendPay">Transfer Attendance/LWP</legend>
                    <br />
                    <table width="100%" cellpadding="1" cellspacing="1">
                        <tr id="tr1" runat="server" visible="true">
                            <td class="form_left_label">College Name <span style="color: Red">*</span>
                            </td>
                            <td><b>:</b></td>

                            <td class="form_left_text" colspan="4">
                                <asp:DropDownList ID="ddlCollege" runat="server" Width="80%" AppendDataBoundItems="true" TabIndex="1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                    Display="None" ErrorMessage="Please select College" SetFocusOnError="true" ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>

                        </tr>

                        <tr>
                            <td class="form_left_label">From Date 
                            </td>
                            <td><b>:</b></td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="2" Width="80px" />
                                &nbsp;<asp:Image ID="imgCalFromDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: hand" />
                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                </ajaxToolKit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                    Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="true"
                                    ValidationGroup="Payroll" />
                                <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" />
                            </td>
                            <td class="form_left_label">To Date
                            </td>
                            <td><b>:</b></td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtToDate" runat="server" TabIndex="3" Width="80px" AutoPostBack="True" />
                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                                </ajaxToolKit:CalendarExtender>
                                <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                <asp:Image ID="imgCalToDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: hand" />
                                <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                    Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true"
                                    ValidationGroup="Payroll" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <asp:Button ID="btnShow" runat="server" Text="Show" Width="10%"
                                    ValidationGroup="Payroll" TabIndex="4" OnClick="btnShow_Click" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    TabIndex="5" Width="10%" />

                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                    DisplayMode="List" ShowMessageBox="true" ShowSummary="false" Width="123px" />

                            </td>
                        </tr>
                    </table>
                    <br />
                </fieldset>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>
    </asp:UpdatePanel>--%>
    <div>
        <%-- <asp:Panel ID="pnlView" runat="server" Width="70%" Visible="false">
        <fieldset id="Fieldset1" class="fieldsetPay" runat="server">
             <legend class="legendPay">Transfer Attendance Record</legend>
            <table width="100%" cellpadding="1" cellspacing="1">
                <tr>
                    <td>
                        <asp:ListView ID="lvEmpList" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <center>
                       <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Employee Not Found" /></center>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                        Absent Days Record
                                    </div>
                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                        <thead>
                                            <tr class="header">--%>
        <%--Already Committed<th width="1%">
                                                                    <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Select IDNO" Enabled="true"
                                                                        runat="server" onclick="checkAllEmployees(this)" />
                                                                </th>  --%>
        <%-- <th width="1%">IDNO
                                                                    
                                                </th>
                                                <th width="2%">NAME
                                                </th>--%>
        <%--Already Committed <th width="2%">
                                                                    DEPARTMENT
                                                                </th>--%>
        <%--     <th width="2%">LWP DAYS
                                                </th>
                                                <th width="2%">UPDATED LWP DAYS
                                                </th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                                <div class="listview-container">
                                    <div id="demo-grid" class="vista-grid">
                                        <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">--%>
        <%--Already Committed <td width="1%">
                                                        <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' 
                                                            ToolTip='<%#Eval("IDNO")%>'/>
                                                    </td>--%>
        <%--   <td width="5%">--%>
        <%--  <%# Eval("IDNO")%>--%>

        <%--    <asp:Label ID="lblidno" runat="server" Text='<%# Eval("IDNO")%>' Enabled="false"></asp:Label>

                                    </td>
                                    <td width="10%">
                                        <%# Eval("NAME")%>
                                    </td>--%>
        <%--Already Committed<td width="20%">
                                                      <%# Eval("SUBDEPT")%>
                                                    </td>--%>
        <%--     <td width="10%">
                                        <asp:TextBox ID="txtleave" runat="server" Text='<%# Eval("LEAVES")%>' Enabled="false"></asp:TextBox>
                                    </td>
                                    <td width="10%">
                                        <asp:TextBox ID="txtleaveUpdated" runat="server" Text='<%# Eval("LEAVE_UPDATED")%>' Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">--%>
        <%--Already Committed<td width="1%">
                                                        <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' 
                                                            ToolTip='<%#Eval("IDNO")%>'/>
                                                    </td>--%>
        <%-- <td width="5%">--%>
        <%--Already Committed <%# Eval("IDNO")%>--%>
        <%--  <asp:Label ID="lblidno" runat="server" Text='<%# Eval("IDNO")%>' Enabled="false"></asp:Label>

                                    </td>
                                    <td width="10%">
                                        <%# Eval("NAME")%>
                                    </td>--%>
        <%--Already Committed <td width="20%">
                                                      <%# Eval("SUBDEPT")%>
                                                    </td>--%>
        <%--   <td width="10%">
                                        <asp:TextBox ID="txtleave" runat="server" Text='<%# Eval("LEAVES")%>' Enabled="false"></asp:TextBox>
                                    </td>
                                    <td width="10%">
                                        <asp:TextBox ID="txtleaveUpdated" runat="server" Text='<%# Eval("LEAVE_UPDATED")%>' Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
            </table>

        </fieldset>

    </asp:Panel>--%>
    </div>

    <td>
        <%-- <asp:Panel ID="Panel1" runat="server" Width="70%" Visible="true">
        <table width="100%" cellpadding="1" cellspacing="1">
            <tr>
                <td align="center">
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="10%" Visible="false"
                        ValidationGroup="Payroll" TabIndex="6" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="showConfirmUpdate(this); return false;"
                        Width="10%" Enabled="false" Visible="false" OnClick="btnUpdate_Click" />--%>
        <%--Already Committed<asp:Button ID="btnReport" runat="server" Text="Report" 
           Width="10%" Visible="false" onclick="btnReport_Click" />--%>

        <%--   </td>
            </tr>
        </table>
    </asp:Panel>--%>
    </td>

    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div class="text-center">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                    <td>&nbsp;&nbsp;Are you sure Transfer this record..?</td>
                    <div class="text-center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-primary" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmUpdate(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }
        ; debugger
        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            //         this._popup.hide();
            //         //  use the cached button as the postback source
            //         //         __doPostBack(this._source.name, '');
            //         var st = vall.id.split("lvEmpList_ctrl");
            //         var i = st[1].split("_txtleave");
            //         var index = i[0];
            //         document.getElementById('ctl00_ContentPlaceHolder1_lvEmpList_ctrl' + index + '_txtleave').disabled = false;
            //         document.getElementById('ctl00_ContentPlaceHolder1_btnUpdate').value = "Update";



            //         var frm = document.forms[0];
            //         for (i = 0; i < document.forms[0].elements.length; i++) {
            //             var e = frm.elements[i];
            //             if (e.type == 'textbox') {

            //                 e.disabled = false;
            //             }
            //         }
            //         document.getElementById('ctl00_ContentPlaceHolder1_btnUpdate').text = "Update";

            this._popup.hide();
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }


        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }

        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }
    </script>
    <div id="divMsg" runat="server">
    </div>


</asp:Content>

