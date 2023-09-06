<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="HostelSession.aspx.cs" Inherits="Hostel_Masters_HostelSession" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>--%>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };

        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }
        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">DEFINE HOSTEL SESSION</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Session Name</label>
                                </div>
                                <asp:TextBox ID="txtSessionName" runat="server" TabIndex="1" MaxLength="50" />
                                <asp:RequiredFieldValidator ID="rfvSessionName" runat="server" ControlToValidate="txtSessionName"
                                    Display="None" ErrorMessage="Please enter session name." ValidationGroup="submit"
                                    SetFocusOnError="True" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Session Start</label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="imgSessionStart" runat="server" class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtSessionStart" runat="server" TabIndex="2" CssClass="form-control" AutoPostBack="true" ValidationGroup="submit" OnTextChanged="txtSessionStart_TextChanged" />
                                    <ajaxToolKit:CalendarExtender ID="ceSessionStart" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtSessionStart" PopupButtonID="imgSessionStart" />
                                    <ajaxToolKit:MaskedEditExtender ID="meSessionStart" runat="server" TargetControlID="txtSessionStart"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                        MaskType="Date" />
                                    <ajaxToolKit:MaskedEditValidator ID="mvSessionStart" runat="server" EmptyValueMessage="Please enter session start"
                                        ControlExtender="meSessionStart" ControlToValidate="txtSessionStart" IsValidEmpty="false"
                                        InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Select Date"
                                        InvalidValueBlurredMessage="*" ValidationGroup="submit" SetFocusOnError="true" />
                                    <asp:CompareValidator ID="cvStartDate" runat="server" ControlToValidate="txtSessionStart"
                                        Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter a valid start datemm/dd/yyyy)."
                                        EnableClientScript="False" ValidationGroup="submit">
                                    </asp:CompareValidator>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Session End</label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="imgSessionEnd" runat="server" class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtSessionEnd" runat="server" TabIndex="3" CssClass="form-control" AutoPostBack="true" ValidationGroup="submit" OnTextChanged="txtSessionEnd_TextChanged"/>
                                    <ajaxToolKit:CalendarExtender ID="ceSessionEnd" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtSessionEnd" PopupButtonID="imgSessionEnd" Enabled="true"
                                        EnableViewState="true" />
                                    <ajaxToolKit:MaskedEditExtender ID="meSessionEnd" runat="server" TargetControlID="txtSessionEnd"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                        MaskType="Date" ErrorTooltipEnabled="True" />
                                    <ajaxToolKit:MaskedEditValidator ID="mvSessionEnd" runat="server" EmptyValueMessage="Please enter session end"
                                        ControlExtender="meSessionEnd" ControlToValidate="txtSessionEnd" IsValidEmpty="false"
                                        Display="None" ErrorMessage="Please select date" ValidationGroup="submit" SetFocusOnError="true" />
                                    <asp:CompareValidator ID="cvEndDate" runat="server" ControlToCompare="txtSessionStart"
                                        ControlToValidate="txtSessionEnd" Display="None" ErrorMessage="Start date must be less than end date"
                                        Operator="LessThanEqual" ValidationGroup="subnmit" Type="Date" />
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Active </label>
                                </div>
                                <asp:RadioButtonList ID="rdoActive" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True" Tabindex="4">Yes</asp:ListItem>
                                    <asp:ListItem Value="0" Tabindex="5">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit" TabIndex="6"
                            OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false" TabIndex="7"
                            OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                    </div>

                    <div class="col-12">
                        <asp:Repeater ID="lvSession" runat="server">
                            <HeaderTemplate>
                                <div class="sub-heading">
                                    <h5>List of Session</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Edit
                                            </th>
                                            <th>Session Name
                                            </th>
                                            <th>Start Session
                                            </th>
                                            <th>End Session
                                            </th>
                                            <th>Active
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("HOSTEL_SESSION_NO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="7" />&nbsp;
                                             <%--   <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("HNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                    </td>
                                    <td>
                                        <%# Eval("SESSION_NAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("START_DATE") %>
                                    </td>
                                    <td>
                                        <%# Eval("END_DATE") %>
                                    </td>
                                    <td>
                                        <%# Eval("ACTIVE") %>
                                    </td>

                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
