<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LeaveAndODReport.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Reports_LeaveAndODSummay" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">


        function parseDate(str) {
            var date = str.split('/');
            return new Date(date[2], date[1], date[0] - 1);
        }

        function GetDaysBetweenDates(date1, date2) {
            return (date2 - date1) / (1000 * 60 * 60 * 24)
        }


        function caldiff() {

            if ((document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value != null) && (document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value != null)) {

                var d = GetDaysBetweenDates(parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value), parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value));
                {
                    document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = (parseInt(d) + 1);
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value == "NaN") {
                        document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                    }
                }

            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value <= 0) {
                alert("No. of Days can not be 0 or less than 0 ");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            if (parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value) > parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtLeavebal').value)) {

                alert("No. of Days not more than Balance Days");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            return false;
        }

    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">OD AND LEAVE SUMMARY REPORT</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="updAdd" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Select Criteria for Leave & OD Summary Report</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>From Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalFromdt" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtFromdt" runat="server" MaxLength="10" CssClass="form-control"
                                                        ToolTip="Enter From Date" TabIndex="1" Style="z-index: 0;" />
                                                    <asp:RequiredFieldValidator ID="rfvFromdt" runat="server"
                                                        ControlToValidate="txtFromdt" Display="None"
                                                        ErrorMessage="Please Enter From Date" SetFocusOnError="true"
                                                        ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="ceFromdt" runat="server" Enabled="true"
                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalFromdt"
                                                        TargetControlID="txtFromdt">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server"
                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                        Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                        TargetControlID="txtFromdt" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevFromdt" runat="server"
                                                        ControlExtender="meeFromdt" ControlToValidate="txtFromdt" Display="None"
                                                        EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date"
                                                        InvalidValueBlurredMessage="Invalid Date"
                                                        InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                        SetFocusOnError="true" TooltipMessage="Please Enter From Date"
                                                        ValidationGroup="Leaveapp" InitialValue="__/__/____"> <%--&#160;&#160;--%>                                               
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>To Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtTodt" runat="server" MaxLength="10" TabIndex="2" CssClass="form-control" AutoPostBack="true"
                                                        ToolTip="Enter To Date" Style="z-index: 0;" OnTextChanged="txtTodt_TextChanged" />
                                                    <asp:RequiredFieldValidator ID="rfvTodt" runat="server"
                                                        ControlToValidate="txtTodt" Display="None" ErrorMessage="Please Enter To Date"
                                                        SetFocusOnError="true" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true"
                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalTodt"
                                                        TargetControlID="txtTodt">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server"
                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                        Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                        TargetControlID="txtTodt" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevTodt" runat="server"
                                                        ControlExtender="meeTodt" ControlToValidate="txtTodt" Display="None"
                                                        EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter To Date"
                                                        InvalidValueBlurredMessage="Invalid Date"
                                                        InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                        SetFocusOnError="true" TooltipMessage="Please Enter To Date"
                                                        ValidationGroup="Leaveapp">   <%--&#160;&#160;--%>                                                 
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="tr1" runat="server" visible="true">
                                                <div class="label-dynamic">
                                                    <sup> </sup>
                                                    <label>College Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="3" data-select2-enable="true"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Select College Name">
                                                </asp:DropDownList>
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                                    Display="None" ErrorMessage="Please select College" SetFocusOnError="true" ValidationGroup="Leaveapp"
                                                    InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Staff Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlstafftype" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="4" data-select2-enable="true"
                                                    ToolTip="Please Select Staff Type" OnSelectedIndexChanged="ddlstafftype_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvstafftype" runat="server" ControlToValidate="ddlstafftype"
                                                    Display="None" ErrorMessage="Please Select Staff Type" SetFocusOnError="true" ValidationGroup="Leaveapp"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trddldept" runat="server" visible="true">
                                                <div class="label-dynamic">
                                                    <sup> </sup>
                                                    <label>Department Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddldept" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                    AutoPostBack="true" CssClass="form-control" TabIndex="5"
                                                    OnSelectedIndexChanged="ddldept_SelectedIndexChanged" ToolTip="Select Department Name">
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddldept"
                                                    Display="None" ErrorMessage="Please select Department" SetFocusOnError="true" ValidationGroup="Leaveapp"
                                                    InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:RadioButtonList ID="rblAllParticular" runat="server"
                                                    RepeatDirection="Horizontal" TabIndex="5" CssClass="radio-button-list-style"
                                                    OnSelectedIndexChanged="rblAllParticular_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Enabled="true" Selected="True" Text="All Employees" Value="0"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="Particular Employee" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="tremp" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Select Employee</label>
                                                </div>
                                                <asp:DropDownList ID="ddlEmp" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                    TabIndex="6" ToolTip="Select Employee">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmp"
                                                    Display="None" ErrorMessage="Please select Employee" SetFocusOnError="true" ValidationGroup="Leaveapp"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnReport" />
                                <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-12 btn-footer">
                         <asp:Button ID="btnDetail" runat="server" Text="Detail" ValidationGroup="Leaveapp" TabIndex="8"
                                CssClass="btn btn-primary" ToolTip="Click here to See Details" OnClick="btnDetail_Click" Visible="false" />
                        <asp:Button ID="btnReport" runat="server" Text="Summary" ValidationGroup="Leaveapp" TabIndex="7"
                            CssClass="btn btn-info" ToolTip="Click here to Show Report" OnClick="btnReport_Click" />                           
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="9"
                            CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                   <%-- <div class="col-12">
                        <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                            <div class="text-center">
                                <div class="modal-content">
                                    <div class="modal-body">
                                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                                        <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                                        <div class="text-center">
                                            <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                                            <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>

</asp:Content>

