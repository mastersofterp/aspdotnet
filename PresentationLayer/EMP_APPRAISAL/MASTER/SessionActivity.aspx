﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SessionActivity.aspx.cs" Inherits="EMP_APPRAISAL_MASTER_SessionActivity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">APPRAISAL SESSION ACTIVITY</h3>
                        </div>
                        <div class="box-body">

                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="panel panel-info">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Appraisal Session Activity</h5>
                                            </div>
                                            <div class="panel-body">
                                                <br />
                                                <div class="col-md-12">
                                                    <asp:Panel ID="uppadd" runat="server">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label><span style="color: #FF0000">*</span>Session:</label>
                                                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true"
                                                                    TabIndex="1" AutoPostBack="true" ToolTip="Select Session" CssClass="form-control datepickerinput" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSession"
                                                                    Display="None" ErrorMessage="Please Select Session" ValidationGroup="submit"
                                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label><span style="color: #FF0000">*</span>College Name :</label>
                                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control datepickerinput" AppendDataBoundItems="true"
                                                                    TabIndex="3" AutoPostBack="true" ToolTip="Select College Name" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                                                    Display="None" ErrorMessage="Please Select College Name" ValidationGroup="submit"
                                                                    SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label><span style="color: #FF0000">*</span>Start Date :</label>

                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                    </div>
                                                                    <asp:TextBox ID="txtStartDate" runat="server" TabIndex="7" CssClass="form-control"
                                                                        ToolTip="Please Enter Start Date"></asp:TextBox>
                                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                                        EnableViewState="true" PopupButtonID="Image2" TargetControlID="txtStartDate">
                                                                    </ajaxToolKit:CalendarExtender>
                                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left"
                                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                                        OnInvalidCssClass="errordate" TargetControlID="txtStartDate" ClearMaskOnLostFocus="true">
                                                                    </ajaxToolKit:MaskedEditExtender>
                                                                    <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txtStartDate"
                                                                        IsValidEmpty="false" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                                        InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None"
                                                                        Text="*" ValidationGroup="submit"></ajaxToolKit:MaskedEditValidator>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvDOfBirth" ControlToValidate="txtStartDate" Display="None"
                                                                        ErrorMessage="Please Enter Start Date." ValidationGroup="submit" />
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtStartDate"
                                                                        ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                                                        ErrorMessage="Invalid date format" ValidationGroup="seminars" Display="None" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label><span style="color: #FF0000">*</span>End Date :</label>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                    </div>
                                                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control datepickerinput" TabIndex="5"
                                                                        AutoPostBack="true" OnTextChanged="txtEndDate_TextChanged" ToolTip="Enter Session Activity End Date"></asp:TextBox>

                                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                        EnableViewState="true" PopupButtonID="Image1" TargetControlID="txtEndDate">
                                                                    </ajaxToolKit:CalendarExtender>
                                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                                        OnInvalidCssClass="errordate" TargetControlID="txtEndDate" ClearMaskOnLostFocus="true">
                                                                    </ajaxToolKit:MaskedEditExtender>
                                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txtEndDate"
                                                                        IsValidEmpty="false" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                                        InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None"
                                                                        Text="*" ValidationGroup="Personal"></ajaxToolKit:MaskedEditValidator>
                                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtEndDate" Display="None"
                                                                        ErrorMessage="Please Enter End Date." ValidationGroup="submit" />
                                                                    <asp:CompareValidator ID="cvToDate" runat="server" Display="None" ErrorMessage="End Date Should Be Larger Than Or Equals To Start Date"
                                                                        ValidationGroup="submit" SetFocusOnError="True" ControlToCompare="txtStartDate"
                                                                        ControlToValidate="txtEndDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEndDate"
                                                                        ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                                                        ErrorMessage="Invalid date format" ValidationGroup="seminars" Display="None" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <label><span style="color: #FF0000">*</span> Activity Status :</label>
                                                                <asp:RadioButton ID="rdoStart" runat="server" Text="Started" GroupName="act_status"
                                                                    TabIndex="5" />&nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rdoStop" runat="server" Text="Stopped" Checked="true" GroupName="act_status"
                                                                        TabIndex="6" />
                                                            </div>
                                                        </div>
                                                        <div class="text-center">
                                                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="submit" OnClick="btnSave_Click"
                                                                TabIndex="4" ToolTip="Click to Save Record" Width="80px" CssClass="btn btn-primary progress-button" />
                                                            &nbsp;
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-outline-danger"
                                                            TabIndex="5" OnClick="btnCancel_Click" Width="80px" ToolTip="Click to Clear Record" />&nbsp;
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </div>
                                                        <div class="col-md-8 form-group" visible="false">
                                                            <label visible="false"><span style="color: #FF0000"></span></label>
                                                            <asp:DropDownList ID="ddlSessionType" AppendDataBoundItems="true" runat="server"
                                                                TabIndex="2" CssClass="form-control datepickerinput" Visible="false">
                                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSessionType"
                                                                Display="None" ErrorMessage="Please select Appraisal Type."
                                                                ValidationGroup="submit" InitialValue="0" />
                                                        </div>
                                                        <br />
                                                        <br />
                                                    </asp:Panel>
                                                </div>
                                                <div class="form-group col-md-12">
                                                </div>
                                                <asp:Panel ID="pnlList" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="table-responsive">
                                                            <asp:ListView ID="lvActivity" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="lgv1">
                                                                         <div class="sub-heading">
                                                        <h5>Session Activity List</h5>
                                                    </div>
                                                                        <table class="table table-striped table-bordered nowrap display">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th>Action
                                                                                    </th>
                                                                                    <th>Session
                                                                                    </th>
                                                                                    <th>Start Date
                                                                                    </th>
                                                                                    <th>End Date
                                                                                    </th>
                                                                                    <th>Activity Status
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item">
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SESSION_ACTIVITY_NO") %>'
                                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                            <asp:ImageButton ID="btnDelete" Visible="true" runat="server" ImageUrl="~/Images/delete.png"
                                                                                CommandArgument='<%# Eval("SESSION_ACTIVITY_NO") %>' AlternateText="Delete Record"
                                                                                ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SESSION_NAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("START_DATE", "{0:dd/MM/yyyy}")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("END_DATE", "{0:dd/MM/yyyy}")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblactinestatus" runat="server" Text='<%# Eval("STARTED")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr class="altitem">
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SESSION_ACTIVITY_NO") %>'
                                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                            <asp:ImageButton ID="btnDelete" Visible="true" runat="server" ImageUrl="~/Images/delete.png"
                                                                                CommandArgument='<%# Eval("SESSION_ACTIVITY_NO") %>' AlternateText="Delete Record"
                                                                                ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SESSION_NAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("START_DATE", "{0:dd/MM/yyyy}")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("END_DATE", "{0:dd/MM/yyyy}")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblactinestatus" runat="server" Text='<%# Eval("STARTED")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
     </div>
        </ContentTemplate>

    </asp:UpdatePanel>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
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

        function OnSetChange() {
            document.getElementById('<%=ddlCollege.ClientID %>').focus();

        }
    </script>

    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

