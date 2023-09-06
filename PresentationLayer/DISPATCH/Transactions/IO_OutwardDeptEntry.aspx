<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="IO_OutwardDeptEntry.aspx.cs" Inherits="Dispatch_Transactions_IO_OutwardDeptEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DEPARTMENT OUTWARD ENTRY</h3>
                        </div>

                        <div class="box-body">
                            <div id="divpnlRpt" runat="server">
                                <asp:Panel ID="pnlRpt" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading"><h5>Search Criteria</h5></div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>From Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <%-- <asp:Image ID="imgFrmDt" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False" Style="cursor: pointer" />--%>
                                                        <i id="imgFrmDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtFrmDate" runat="server" MaxLength="100" CssClass="form-control" TabIndex="1" ToolTip="Select From Date"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvFrmDt" runat="server" ControlToValidate="txtFrmDate"
                                                        Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="true" ValidationGroup="Date"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="ceFrmDt" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="imgFrmDt" TargetControlID="txtFrmDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                       <%--  Modified by Saahil Trivedi 17-02-2022--%>
                                                    <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtFrmDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server"  ControlExtender="medt" ControlToValidate="txtFrmDate"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] "
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*"
                                                    ValidationGroup="Date"></ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>To Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <%--<asp:Image ID="imgTodt" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False"
                                                            Style="cursor: pointer" />--%>
                                                        <i id="imgTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtToDate" runat="server" MaxLength="18" CssClass="form-control" TabIndex="2" ToolTip="Select To Date"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtToDate"
                                                        Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="Date"></asp:RequiredFieldValidator>

                                                    <ajaxToolKit:CalendarExtender ID="ceTodt" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="imgTodt" TargetControlID="txtToDate">
                                                    </ajaxToolKit:CalendarExtender>
                                       <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtToDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"  ControlExtender="medt" ControlToValidate="txtToDate"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] "
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*"
                                                    ValidationGroup="Date"></ajaxToolKit:MaskedEditValidator>
                                                     <asp:CompareValidator ID="cmpvDatee" runat="server" ErrorMessage="To Date Should be greater than or equal to  From Date"
                                                            ControlToCompare="txtFrmDate" ControlToValidate="txtToDate" Display="None" ValueToCompare="Date"
                                                            Type="Date" Operator="GreaterThanEqual" ValidationGroup="Date"></asp:CompareValidator>

                                                     <asp:CompareValidator ID="cmpvDate" runat="server" ErrorMessage="To Date Should be greater than or equal to  From Date"
                                                            ControlToCompare="txtFrmDate" ControlToValidate="txtToDate" Display="None" ValueToCompare="Date"
                                                            Type="Date" Operator="GreaterThanEqual" ValidationGroup="Date"></asp:CompareValidator>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" ValidationGroup="Date" CssClass="btn btn-primary" ToolTip="Click here to Show" TabIndex="3" />
                                        <asp:Button ID="btnreport" Visible="false" runat="server" Text="Report" ValidationGroup="Date" CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnreport_Click" TabIndex="4" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Date" />
                                        <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtToDate" CultureInvariantValues="true" Display="Static" Operator="GreaterThanEqual" SetFocusOnError="True"
                                            Type="Date" ValidationGroup="Date" ControlToCompare="txtFrmDate" />
                                    </div>
                                </asp:Panel>
                            </div>

                            <div id="divPanel" runat="server" visible="false">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading"><h5>Add/Edit Outward Entry</h5></div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Subject </label>
                                                </div>
                                                <asp:TextBox ID="txtSubject" runat="server" MaxLength="100" CssClass="form-control" TabIndex="6" ToolTip="Enter Subject"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject" Display="None" ErrorMessage="Please enter the Subject" SetFocusOnError="true"
                                                    ValidationGroup="Submit" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Central Ref No. </label>
                                                </div>
                                                <asp:TextBox ID="txtRefNo" runat="server" MaxLength="15" CssClass="form-control" Visible="false" ToolTip="Enter Reference No."></asp:TextBox>
                                                <asp:TextBox ID="txtcentralrefno" ReadOnly="true" CssClass="form-control" ToolTip="Enter Central Reference No." runat="server" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Date </label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <%-- <asp:Image ID="imgReceivedDT" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False" Style="cursor: pointer" />--%>
                                                        <i id="imgReceivedDT" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtSentDT" runat="server" MaxLength="10" CssClass="form-control" TabIndex="8" ToolTip="Select Date"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvSentDT" runat="server" ControlToValidate="txtSentDT"
                                                        Display="None" ErrorMessage="Please enter valid Sent Date." SetFocusOnError="true" ValidationGroup="Submit" />
                                                    <ajaxToolKit:CalendarExtender ID="CeReceivedDT" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="imgReceivedDT" TargetControlID="txtSentDT">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <asp:HiddenField ID="hdnDate" runat="server" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" visible="false" runat="server">
                                                <div class="label-dynamic">
                                                    <label>Carrier </label>
                                                </div>
                                                <asp:DropDownList ID="ddlCarrier" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" Enabled="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>PostType </label>
                                                </div>
                                                <asp:DropDownList ID="ddlPostType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="9" ToolTip="Select Post Type">
                                                    <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvPostType" runat="server" ControlToValidate="ddlPostType"
                                                    Display="None" ErrorMessage="Please select Post Type" InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit" />

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Letter Category </label>
                                                </div>
                                                <asp:DropDownList ID="ddlLCat" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="10" ToolTip="Select Letter Category">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Official</asp:ListItem>
                                                    <asp:ListItem Value="2">Personal</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvLCat" runat="server" ControlToValidate="ddlLCat"
                                                    Display="None" ErrorMessage="Please select letter category." InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Disp.Tracking No. </label>
                                                </div>
                                                <asp:TextBox ID="txtTrackNo" Enabled="true" CssClass="form-control" runat="server" ToolTip="Enter Dispatch Tracking No." />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div id="divpnlTo" runat="server" visible="false">
                                <asp:Panel ID="pnlTo" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading"><h5>Receiver Details</h5></div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Receiver Name </label>
                                                </div>
                                                <asp:TextBox ID="txtTo" runat="server" MaxLength="100" CssClass="form-control" TabIndex="11" ToolTip="Enter To"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvTo" runat="server" ControlToValidate="txtTo" Display="None"
                                                    ErrorMessage="Please Enter Receiver Name" SetFocusOnError="true" ValidationGroup="Add" />
                                                <%--<ajaxToolKit:FilteredTextBoxExtender ID="ftbeTo" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom, Numbers"
                                                    ValidChars=" " TargetControlID="txtTo">
                                                </ajaxToolKit:FilteredTextBoxExtender>--%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Address Line 1 </label>
                                                </div>
                                                <asp:TextBox ID="txtMulAddress" runat="server" MaxLength="100" CssClass="form-control" TabIndex="12"
                                                    ToolTip="Enter Address Line 1" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAdd" runat="server" ControlToValidate="txtMulAddress"
                                                    Display="None" ErrorMessage="Please Enter Address Line 1." SetFocusOnError="true"
                                                    ValidationGroup="Add" />
                                                <%--<ajaxToolKit:FilteredTextBoxExtender ID="ftbeMulAdd" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom, Numbers"
                                                    ValidChars="-/,': " TargetControlID="txtMulAddress">
                                                </ajaxToolKit:FilteredTextBoxExtender>--%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Address Line 2 </label>
                                                </div>
                                                <asp:TextBox ID="txtAddLine" runat="server" MaxLength="100" CssClass="form-control" TabIndex="13" ToolTip="Enter Address Line 2" />
                                                <%-- <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAddLine" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom, Numbers"
                                                    ValidChars="-/,': " TargetControlID="txtAddLine">
                                                </ajaxToolKit:FilteredTextBoxExtender>--%>

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>City </label>
                                                </div>
                                                <asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ValidationGroup="Add" TabIndex="14" ToolTip="Select City">
                                                    <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                                    Display="None" ErrorMessage="Please Select City." SetFocusOnError="true" ValidationGroup="Add" InitialValue="0" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>State/Region </label>
                                                </div>
                                                <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="15" ToolTip="Select State/Region">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState"
                                                    InitialValue="0" Display="None" ErrorMessage="Please Select State" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Remark </label>
                                                </div>
                                                <asp:TextBox ID="txtRemarks" runat="server" MaxLength="150" CssClass="form-control" TextMode="MultiLine" TabIndex="16" ToolTip="Enter Remarks"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Pin No. </label>
                                                </div>
                                                <asp:TextBox ID="txtPIN" runat="server" CssClass="form-control" onkeypress="return CheckNumeric(event,this);" MaxLength="6" TabIndex="17" ToolTip="Enter Pin No."></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Country </label>
                                                </div>
                                                <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="18" ToolTip="Select Country">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry"
                                                    InitialValue="0" Display="None" ErrorMessage="Please Select Country" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Contact No. </label>
                                                </div>
                                                <asp:TextBox ID="txtContNo" runat="server" MaxLength="10" ValidationGroup="Add" CssClass="form-control" onkeypress="return CheckNumeric(event,this);" TabIndex="19" ToolTip="Contact No."></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvContNo" runat="server" ControlToValidate="txtContNo" Display="None" ErrorMessage="Please enter contact number" SetFocusOnError="true" ValidationGroup="Add" />--%>
                                                  <ajaxToolKit:FilteredTextBoxExtender ID="ftbtxtContNo" runat="server" ValidChars="0123456789+"
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtContNo">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer" id="divAddTo" runat="server" visible="false">
                                <asp:Button ID="btnAddTo" runat="server" Text="Add" OnClick="btnAddTo_Click" ValidationGroup="Add" CssClass="btn btn-primary" TabIndex="20" ToolTip="Click here to Add" />
                                <asp:ValidationSummary ID="ValSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />
                            </div>

                            <div class="col-12" id="divList" runat="server" visible="false">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvTo" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading"><h5>Receiver Copy</h5></div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit
                                                        </th>
                                                        <th>Delete
                                                        </th>
                                                        <th>Receiver Name
                                                        </th>
                                                        <th>Address Line1
                                                        </th>
                                                        <th>Address Line2
                                                        </th>
                                                        <th>City
                                                        </th>
                                                        <th>State
                                                        </th>
                                                        <th>Pin No.
                                                        </th>
                                                        <th>Country
                                                        </th>
                                                        <th>Remarks
                                                        </th>
                                                        <th>Contact No.
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                        ImageUrl="~/Images/edit.png" OnClick="btnEditRec_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                        ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" ToolTip="Delete Record" />
                                                </td>
                                                <td id="IOTRANNO" runat="server">
                                                    <%# Eval("IOTO") %>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMulAddress" runat="server" Text='<%# Eval("MULTIPLE_ADDRESS") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAddLine" runat="server" Text='<%# Eval("ADDLINE") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CITYNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblState" runat="server" Text='<%# Eval("STATE") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPinNo" runat="server" Text='<%# Eval("PINNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRY") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARK") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblContNo" runat="server" Text='<%# Eval("CONTACTNO") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div id="divFrom" runat="server" visible="false">
                                <asp:Panel ID="pnlFrom" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading"><h5>From Details</h5></div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Personal File No. :</b>
                                                        <a class="sub-label"><asp:Label ID="lblRFID" runat="server" Text="" ToolTip="Personal File No." Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Employee Name :</b>
                                                        <a class="sub-label"><asp:Label ID="lblEmpName" runat="server" Text="" ToolTip="Employee Name" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Designation :</b>
                                                        <a class="sub-label"><asp:Label ID="lblDesig" runat="server" Text="" ToolTip="Employee Designation" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Department :</b>
                                                        <a class="sub-label"><asp:Label ID="lblDept" runat="server" Text="" ToolTip="Employee department" Font-Bold="true"></asp:Label></a>
                                                    </li>        
                                                </ul>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>College Address :</b>
                                                        <a class="sub-label"><asp:Label ID="lblAddress" runat="server" Text="" ToolTip="College Address" Font-Bold="true"></asp:Label> </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Mobile No. :</b>
                                                        <a class="sub-label"><asp:Label ID="lblMobile" runat="server" Text="" ToolTip="Employee mobile no." Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>RFID Number :</b>
                                                        <a class="sub-label"><asp:Label ID="Label4" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                    </li>       
                                                </ul>
                                            </div>

                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div id="divDD" runat="server" visible="false">
                                <asp:Panel ID="pnlDD" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading"><h5>DD/Cheque Details</h5></div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label> </label>
                                                </div>
                                                <asp:RadioButtonList ID="rdbCheque" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" TabIndex="21" OnSelectedIndexChanged="rdbCheque_SelectedIndexChanged" ToolTip="Select Cheque/DD">
                                                    <asp:ListItem Value="1">Cheque</asp:ListItem>
                                                    <asp:ListItem Value="2">Demand Draft</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label><asp:Label ID="lblCheque" runat="server" Text="Chq.No"></asp:Label> </label>
                                                </div>
                                                <asp:TextBox ID="txtDDNo" runat="server" onkeypress="return CheckNumeric(event,this);"
                                                    MaxLength="6" CssClass="form-control" TabIndex="22" ToolTip="Enter Cheque/DD No." />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Amount </label>
                                                </div>
                                                <asp:TextBox ID="txtDDAmt" runat="server" MaxLength="10" CssClass="form-control" onkeypress="return CheckNumeric(event,this);" TabIndex="23" ToolTip="Enter Amount" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Bank </label>
                                                </div>
                                                <asp:TextBox ID="txtbank" runat="server" MaxLength="100" CssClass="form-control" onkeypress="return CheckAlphabet(event,this);" TabIndex="24" ToolTip="Enter Bank"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Date </label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <%--<asp:Image ID="imgDDdate" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False" Style="cursor: pointer" />--%>
                                                        <i id="imgDDdate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDDdate" runat="server" MaxLength="10" CssClass="form-control" TabIndex="25" ToolTip="Enter Cheque/DD Date"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CeDDdate" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="imgDDdate" TargetControlID="txtDDdate">
                                                    </ajaxToolKit:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div id="divDispDetails" runat="server" visible="false">
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading"><h5>Dispatch Details</h5></div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Send Date </label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgDetails" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtSendDetails" runat="server" CssClass="form-control" ToolTip="Select Send Date"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgDetails" TargetControlID="txtSendDetails">
                                                    </ajaxToolKit:CalendarExtender>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Reference No. </label>
                                                </div>
                                                <asp:TextBox ID="txtRef" ReadOnly="true" CssClass="form-control" runat="server" ToolTip="Enter Reference No." />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Weight </label>
                                                </div>
                                                <asp:TextBox ID="txtWeight" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Weight" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Unit </label>
                                                </div>
                                                <asp:DropDownList ID="ddlUnit" runat="server" AppendDataBoundItems="false" CssClass="form-control" ToolTip="Select Unit">
                                                    <asp:ListItem Value="0">GM</asp:ListItem>
                                                    <asp:ListItem Value="1">KG</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Stamp Cost </label>
                                                </div>
                                                <asp:TextBox ID="txtScost" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Stamp Cost" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Number of person</label>
                                                </div>
                                                <asp:TextBox ID="txtnperson" AutoPostBack="true" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter No.of person" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Extra Cost </label>
                                                </div>
                                                <asp:TextBox ID="txtExtraCost" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Extra Cost" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Cost </label>
                                                </div>
                                                <asp:TextBox ID="txtCost" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Cost" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer" id="divSubmit" runat="server" visible="false">
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Submit" TabIndex="26" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" TabIndex="27" CssClass="btn btn-warning" ToolTip="Click here to Cancel" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>

                            <div class="form-group col-lg-9 col-md-12 col-12" id="divNote" runat="server" visible="false">
                                <div class=" note-div"> 
                                    <h5 class="heading">Note </h5> 
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Please submit the hard copy at front office on the same day(between 9:00 am to 5:00 pm).
                                If not submitted letter will be discarded.</span> </p>
                                </div>
                            </div>
                             
                            <div class="col-12 btn-footer" id="divAddNew" runat="server">
                                <%--<asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click" TabIndex="5"  CssClass="btn btn-primary" ToolTip="Click to Add New Outward">Add New</asp:LinkButton>--%>
                                <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" TabIndex="5" CssClass="btn btn-primary" ToolTip="Click to Add New Outward" />
                            </div>

                            <div class="col-12" id="divListview" runat="server">
                                <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="IvOutward" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading"><h5>Department Outward Entry</h5></div>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Delete
                                                        </th>
                                                        <th>Edit
                                                        </th>
                                                        <th>Letter Category                                                                
                                                        </th>
                                                        <th>Post Type
                                                        </th>
                                                            <th>Receiver Name
                                                        </th>
                                                        <th>To Address
                                                        </th>
                                                        <th>Date
                                                        </th>
                                                        <th>Subject
                                                        </th>
                                                        <%-- <th>Reference No
                                                        </th>--%>

                                                        <th>Carrier Name
                                                        </th>
                                                        <th>Postage Cost
                                                        </th>
                                                        <th>Remark
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("IOTRANNO")%>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDeleteRecord_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                        CommandArgument='<%# Eval("IOTRANNO") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click"
                                                        ToolTip="Edit Record" />
                                                </td>
                                                <td>
                                                    <%# Eval("LETTERCAT")%>                                                             
                                                </td>
                                                <td>
                                                    <%# Eval("POSTTYPENAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("IOTO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TO_ADDRESS")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DEPTRECSENTDT","{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SUBJECT")%>
                                                </td>
                                                <%--<td>
                                                    <%# Eval("CENTRALREFERENCENO")%>
                                                </td>--%>
                                                        
                                                <td>
                                                    <%# Eval("CARRIERNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TOTAL_COST")%>
                                                </td>
                                                <td>
                                                    <%# Eval("REMARK")%>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
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

    <%--END MODAL POPUP EXTENDER FOR DELETE CONFIRMATION --%>

    <script type="text/javascript">

        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = 0;
                }
            }
        }

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
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

