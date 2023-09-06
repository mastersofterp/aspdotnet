<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="IO_InwardDispatch.aspx.cs" Inherits="Dispatch_Transactions_IO_InwardDispatch" %>

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
                            <h3 class="box-title">DISPATCH INWARD ENTRY</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12" id="divAdd" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading"><h5>Add/Edit Inward Entry</h5></div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Post Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlPostType" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Post Type">
                                            <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="rfvPostType" runat="server" ControlToValidate="ddlPostType"
                                                Display="None" InitialValue="0" ErrorMessage="Please select Post Type" SetFocusOnError="true"
                                                ValidationGroup="Submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Reference No.</label>
                                        </div>
                                        <asp:TextBox ID="txtReferenceNo" runat="server" MaxLength="50" CssClass="form-control" TabIndex="2" ToolTip="Enter Reference No." />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeReferenceNo" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            ValidChars="/\-" TargetControlID="txtReferenceNo">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Received Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <%--<asp:Image ID="imgReceivedDT" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False" Style="cursor: pointer" />--%>
                                                    <i id="imgReceivedDT" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtReceivedDT" runat="server" TabIndex="3" MaxLength="10" CssClass="form-control" ToolTip="Select Received Date"></asp:TextBox>

                                            <ajaxToolKit:CalendarExtender ID="CeReceivedDT" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="imgReceivedDT" TargetControlID="txtReceivedDT"  OnClientDateSelectionChanged="CheckDateEalier">
                                            </ajaxToolKit:CalendarExtender>
                                               <%--  Modified by Saahil Trivedi 17-02-2022--%>
                                               <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtReceivedDT" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server"  ControlExtender="medt" ControlToValidate="txtReceivedDT"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] "
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*"
                                                    ValidationGroup="Date"></ajaxToolKit:MaskedEditValidator>
                                            <asp:RequiredFieldValidator ID="rfvReceivedDT" runat="server" ControlToValidate="txtReceivedDT" Display="None" ErrorMessage="Please enter valid Received Date." SetFocusOnError="true" ValidationGroup="Submit" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>If this is a reply to our letter, select our letter</label>
                                        </div>
                                        <asp:DropDownList ID="ddlOutRefNo" runat="server" TabIndex="4" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select our letter">
                                            <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Dispatch Reference No.</label>
                                        </div>
                                        <asp:TextBox ID="txtDispRefno" ReadOnly="true" runat="server" MaxLength="100" CssClass="form-control" ToolTip="Dispatch Reference No."></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                                  
                            <div class="col-12" id="divUser" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading"><h5>Sender Details</h5></div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Sender Name</label>
                                        </div>
                                        <asp:TextBox ID="txtFrom" runat="server" MaxLength="100" TabIndex="5" CssClass="form-control" ToolTip="Enter From Address" />
                                        <asp:RequiredFieldValidator ID="rfvFrom" runat="server" ControlToValidate="txtFrom"
                                            Display="None" ErrorMessage="Please Enter Sender Name" SetFocusOnError="true" ValidationGroup="Submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Subject</label>
                                        </div>
                                        <asp:TextBox ID="txtSubject" runat="server" MaxLength="100" TabIndex="6" CssClass="form-control" ToolTip="Enter Subject"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Address Line 1</label>
                                        </div>
                                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" TabIndex="7" CssClass="form-control"
                                                ToolTip="Enter Address" TextMode="MultiLine" />
                                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                                            Display="None" ErrorMessage="Please Enter Address Line 1." SetFocusOnError="true" ValidationGroup="Submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Address Line 2</label>
                                        </div>
                                        <asp:TextBox ID="txtAddLine" runat="server" MaxLength="100" TabIndex="8" CssClass="form-control" ToolTip="Enter Address" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>City</label>
                                        </div>
                                        <asp:DropDownList runat="server" ID="ddlCity" CssClass="form-control" data-select2-enable="true" TabIndex="9" AppendDataBoundItems="true" ToolTip="Select City">
                                            <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                            Display="None" ErrorMessage="Please Select City." InitialValue="0" SetFocusOnError="true" ValidationGroup="Submit" />--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>State/Province/Region</label>
                                        </div>
                                        <asp:DropDownList ID="ddlState" runat="server" TabIndex="10" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select State">
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState"
                                            InitialValue="0" Display="None" ErrorMessage="Please Select State" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic"> 
                                            <label>Pin No.</label>
                                        </div>
                                        <asp:TextBox ID="txtPIN" onkeypress="return CheckNumeric(event,this);" runat="server" TabIndex="11"
                                            CssClass="form-control" ToolTip="Enter PIN Code" MaxLength="6"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbtxtContNo" runat="server" ValidChars="0123456789"
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtPIN">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                      <%--  <asp:RequiredFieldValidator ID="rfvPinNo" runat="server" ControlToValidate="txtPIN"
                                            Display="None" ErrorMessage="Please Enter Pin No." SetFocusOnError="true" ValidationGroup="Submit" />--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Country</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" TabIndex="12"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Country">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry"
                                            InitialValue="0" Display="None" ErrorMessage="Please Select Country" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:RadioButtonList ID="rdbUL" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbUL_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Selected="True">Principal&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="1">Secretary&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">Chairman&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="3">Staff&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="4">HOD&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="5">RC</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Attendant Name</label>
                                        </div>
                                        <asp:TextBox ID="txtPeon" runat="server" MaxLength="25" TabIndex="17" CssClass="form-control" ToolTip="Enter Peon Name" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row" id="divUserDetails" runat="server" visible="false">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>User Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlUType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="13"
                                            ToolTip="Select User Type" AutoPostBack="true" OnSelectedIndexChanged="ddlUType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Department</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChange"
                                            AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="13" ToolTip="Select Department" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Receiver Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="true" TabIndex="14" OnSelectedIndexChanged="ddlUser_SelectedIndexChange" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select To User" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Designation</label>     <%--gayatri rode 13-01-2021 Enabled="false" --%>
                                        </div>
                                        <asp:DropDownList ID="ddlDesig" runat="server" AppendDataBoundItems="true" TabIndex="15" CssClass="form-control" data-select2-enable="true" Enabled="false" ToolTip="Select Designation" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Employee Code</label>   <%--gayatri rode 13-01-2021 Enabled="false" --%>
                                        </div>
                                        <asp:TextBox ID="txtRFID" runat="server" MaxLength="25" TabIndex="16" CssClass="form-control" onkeypress="return CheckAlphaNumeric(event,this);"  Enabled="false" ToolTip="Enter RFID No. "></asp:TextBox>
                                    </div>

                                </div>

                                <div class="row" id="divHOD" runat="server" visible="false">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Department</label>
                                        </div>
                                        <asp:DropDownList ID="ddlHODdept" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="13" 
                                            ToolTip="Select Department" />
                                    </div>
                                </div>

                                <div id="Divtr1" runat="server" visible="false" class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Receiver Name</label>
                                    </div>
                                    <asp:TextBox ID="txtToUser" CssClass="form-control" ToolTip="Enter To User" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>

                            </div>

                            <div id="divDD" runat="server" visible="false" class="col-12">
                                <%-- <asp:Panel ID="pnlDD" runat="server">--%>
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading"><h5>DD/Cheque Details</h5></div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:RadioButtonList ID="rdbCheque" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" TabIndex="18" OnSelectedIndexChanged="rdbCheque_SelectedIndexChanged">
                                            <asp:ListItem Value="1" Selected="True">Cheque</asp:ListItem>
                                            <asp:ListItem Value="2">Demand Draft</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblCheque" runat="server" Text="Chq.No  :" Font-Bold="true"></asp:Label>
                                        </div>
                                        
                                        <asp:TextBox ID="txtDDNo" runat="server" MaxLength="6" TabIndex="19" CssClass="form-control" ToolTip="Enter Cheque/DD No." onkeypress="return CheckNumeric(event,this);" />
                                        <asp:RequiredFieldValidator ID="rfvDDNo" runat="server" ControlToValidate="txtDDNo" Display="None" ErrorMessage="Please enter Cheque/DD No." SetFocusOnError="true" ValidationGroup="AddCheq" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtDDAmt" onkeypress="return CheckNumeric(event,this);" TabIndex="20" ToolTip="Select Amount" runat="server" MaxLength="10" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvAmt" runat="server" ControlToValidate="txtDDAmt" Display="None" ErrorMessage="Please enter Amount." SetFocusOnError="true" ValidationGroup="AddCheq" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgDDdate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtDDdate" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Select Cheque/DD Date" TabIndex="21"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="CeDDdate" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="imgDDdate" TargetControlID="txtDDdate">
                                            </ajaxToolKit:CalendarExtender>
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDDdate" Display="None" ErrorMessage="Please Enter Cheque Date." SetFocusOnError="true" ValidationGroup="AddCheq" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Bank</label>
                                        </div>
                                        <asp:TextBox ID="txtbank" runat="server" MaxLength="100" onkeypress="return CheckAlphabet(event,this);"
                                            CssClass="form-control" TabIndex="22" ToolTip="Enter Bank Name">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvBank" runat="server" ControlToValidate="txtbank" Display="None" ErrorMessage="Please Enter Bank." SetFocusOnError="true" ValidationGroup="AddCheq" />
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnAddCheque" runat="server" Text="Add Cheque Details" ValidationGroup="AddCheq" TabIndex="23" OnClick="btnAddCheque_Click" CssClass="btn btn-primary" CausesValidation="true" />
                                        <asp:ValidationSummary ID="vsAdd" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddCheq" />
                                    </div>
                                </div>
                           
                                <div class="row">
                                    <asp:Panel ID="pnlChqList" runat="server">
                                        <asp:ListView ID="lvChequeDetails" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading"><h5>CHEQUE DETAILS ENTRY LIST</h5></div>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit
                                                            </th>
                                                            <th>Cheq. No
                                                            </th>
                                                            <th>Amount
                                                            </th>
                                                            <th>Date
                                                            </th>
                                                            <th>Bank
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
                                                            ImageUrl="~/images/edit.png" OnClick="btnEditRec_Click" ToolTip="Edit Record" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblChequeNumber" runat="server" Text='<%# Eval("CHEQUE_NO") %>' />
                                                        <asp:HiddenField ID="hdnChequeType" runat="server" Value='<%# Eval("CHEQUE_TYPE") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCheqAmt" runat="server" Text='<%# Eval("CHEQUE_AMOUNT") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCheqDate" runat="server" Text='<%# Eval("CHEQUE_DATE","{0:dd-MMM-yyyy}") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCheqBank" runat="server" Text='<%# Eval("CHEQUE_BANK") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <%-- </asp:Panel>--%>
                            </div>

                            <div class="col-12 btn-footer" id="divBtnPanel" runat="server" visible="false">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" TabIndex="24" CssClass="btn btn-primary" ToolTip="CLick here to Submit" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" TabIndex="26" CssClass="btn btn-primary" ToolTip="CLick here to Back" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" TabIndex="25" />
                                <asp:HyperLink ID="HyperGoToMovement" SkinID="LinkAddNew" Target="_blank" Text="Do Movement....." runat="server" NavigateUrl="~/DISPATCH/Transactions/Movement.aspx" Visible="false"></asp:HyperLink>
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>

                            <div class="col-12 btn-footer" id="divAddNew" runat="server">
                                <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" CssClass="btn btn-primary" ToolTip="Click to Add New" OnClick="btnAdd_Click">Add New</asp:LinkButton>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvInward" OnItemDataBound="lvInward_Bound" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading"><h5>Dispatch Inward Entry List</h5></div>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>EDIT
                                                        </th>
                                                        <th>
                                                            POST TYPE
                                                            
                                                        
                                                         <th>
                                                        RECEIVED DT.
                                                             </th>
                                                        <th>SENDER NAME
                                                        </th>
                                                        <th>RECEIVER NAME
                                                        </th>
                                                        <th>REFERENCE NO.
                                                            STATUS
                                                        </th>
                                                        <th>SUBJECT
                                                        </th>
                                                        <th>ATTENDANT
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%# Eval("IOTRANNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click1" />
                                                </td>
                                                <td>
                                                    <%# Eval("POSTTYPENAME")%>
                                                   
                                                </td>
                                                 <td>
                                                   
                                                    <%# Eval("CENTRALRECSENTDT","{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("IOFROM")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TOUSER")%>
                                                </td>
                                                <td>
                                                    <%# Eval("OUTREFERENCENO")%>
                                                    <asp:Label ID="lblStatus" Text='<%# Eval("STATUS") %>' runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("SUBJECT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PEON")%>
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

     <script type="text/javascript">                                               //gayatri rode 14-01-2021
         function CheckDateEalier(sender, args) {
             if (sender._selectedDate > new Date()) {
                 alert("Future Date Not Accepted for Received Date");
                 sender._selectedDate = new Date();
                 sender._textbox.set_Value("");
             }
         }
    </script>
    <script type="text/javascript" >
        //function CheckAlphabet(event, obj) {

        //    ////var k = (window.event) ? event.keyCode : event.which;
        //    if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
        //        obj.style.backgroundColor = "White";
        //        return true;

        //    }
        //    if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
        //        obj.style.backgroundColor = "White";
        //        return true;

        //    }
        //    else {
        //        alert('Please Enter Alphabets Only!');
        //        obj.focus();
        //    }
        //    return false;
        //}


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
</asp:Content>

