<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="IO_OutwardDispatch.aspx.cs" Inherits="Dispatch_Transactions_IO_OutwardDispatch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js"></script>
    <div>
        <script type="text/javascript">                                               //Saahil Trivedi 14-01-2021
            function CheckDateEalier(sender, args) {
                if (sender._selectedDate > new Date()) {
                    alert("Future Dates Are Not Allowed");
                    sender._selectedDate = new Date();
                    sender._textbox.set_Value("");
                }
            }
    </script>
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
                            <h3 class="box-title">DISPATCH OUTWARD ENTRY</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12" id="divPanel" runat="server" visible="false">
                                <asp:Panel ID="Panel1" runat="server">

                                    <div class="sub-heading">
                                        <h5>Add/Edit Dispatch Outward</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="2" ToolTip="Select Department">
                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Selected="False" Value="0" Enabled="false">Dispatch</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Letter Category</label>
                                            </div>
                                            <asp:TextBox ID="txtRefNo" runat="server" MaxLength="25" CssClass="form-control" Visible="false" ToolTip="Select Reference No."></asp:TextBox>
                                            <asp:DropDownList ID="ddlLCat" runat="server" AppendDataBoundItems="true" data-select2-enable="true" CssClass="form-control" TabIndex="3" Enabled="true" ToolTip="Select Letter Category">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Official</asp:ListItem>
                                                <asp:ListItem Value="2">Personal</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvLCat" runat="server" ControlToValidate="ddlLCat"
                                                                Display="None" ErrorMessage="Please select letter category." InitialValue="0" SetFocusOnError="true"  ValidationGroup="Submit" />--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Subject</label>
                                            </div>
                                            <asp:TextBox ID="txtSubject" runat="server" TabIndex="4" MaxLength="100" CssClass="form-control" ToolTip="Enter Subject" onkeypress="return CheckAlphaNumeric(event,this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject"
                                                Display="None" ErrorMessage="Please enter the Subject" SetFocusOnError="true" ValidationGroup="Submit" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Post Type </label>
                                            </div>
                                            <asp:DropDownList ID="ddlPostType" runat="server" AppendDataBoundItems="true" TabIndex="5" CssClass="form-control" data-select2-enable="true" ToolTip="Select Post Type"
                                                OnSelectedIndexChanged="ddlPostType_SelectedIndexChanged" Enabled="true">
                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                          <%--   <asp:RequiredFieldValidator ID="rfvddlPostType" runat="server" ControlToValidate="ddlPostType"
                                                Display="None" ErrorMessage="Please Select Post Type." SetFocusOnError="true" ValidationGroup="Submit" />--%>
                                             <asp:RequiredFieldValidator ID="rfvddlPostType" runat="server" ControlToValidate="ddlPostType"
                                                                Display="None" ErrorMessage="Please Select Post Type." InitialValue="0" SetFocusOnError="true"  ValidationGroup="Submit" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <%--<asp:Image ID="imgReceivedDT" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False" Style="cursor: pointer" />--%>
                                                    <i id="imgReceivedDT" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDT" runat="server" MaxLength="10" CssClass="form-control" TabIndex="6" ToolTip="Select Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDT" runat="server" ControlToValidate="txtDT" Display="None"
                                                    ErrorMessage="Please enter valid Dept. Sent Date." SetFocusOnError="true" ValidationGroup="Submit" />
                                                <ajaxToolKit:CalendarExtender ID="CeReceivedDT" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgReceivedDT" TargetControlID="txtDT" OnClientDateSelectionChanged="CheckDateEalier">

                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtDT" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] " ControlToValidate="txtDT"
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*" ValidationGroup="Submit">
                                                </ajaxToolKit:MaskedEditValidator>
                                                 
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Carrier</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCarrier" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="7" ToolTip="Select Carrier">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                           <%-- <asp:RequiredFieldValidator ID="rfvCarrier" runat="server" ControlToValidate="ddlCarrier" Display="None"
                                                ErrorMessage="Please Select Carrier." SetFocusOnError="true" ValidationGroup="Submit" InitialValue="0" />--%>

                                            <%--<asp:RequiredFieldValidator ID="ddlCarrier1" runat="server" ControlToValidate="ddlCarrier"
                                                                Display="None" ErrorMessage="Please Select Carrier." InitialValue="0" SetFocusOnError="true"  ValidationGroup="Add" />--%>

                                          
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Disp.Tracking No.</label>
                                            </div>
                                            <asp:TextBox ID="txtTrackNo"   CssClass="form-control" runat="server" TabIndex="8" onkeypress="return CheckAlphaNumeric(event,this);" MaxLength="25" ToolTip="Enter Disp. Tracking No." />
                                            <asp:RequiredFieldValidator ID="rfvTrackNo" runat="server" ControlToValidate="txtTrackNo" Display="None"
                                                ErrorMessage="Please Enter Tracking Number." SetFocusOnError="true" ValidationGroup="Submit" />
                                        </div>

                                    </div>

                                </asp:Panel>
                            </div>
                            <div class="col-12" id="divpnlTo" runat="server" visible="false">
                                <asp:Panel ID="Panel3" runat="server">

                                    <div class="sub-heading">
                                        <h5>Receiver Details</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Receiver Name</label>
                                            </div>
                                            <asp:TextBox ID="txtTo" runat="server" MaxLength="100" CssClass="form-control" TabIndex="9" ToolTip="Enter To"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTo" runat="server" ControlToValidate="txtTo" Display="None"
                                                ErrorMessage="Please Enter Receiver Name" SetFocusOnError="true" ValidationGroup="Add" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Address Line 1</label>
                                            </div>
                                            <asp:TextBox ID="txtMulAddress" runat="server" MaxLength="100" CssClass="form-control" TabIndex="10"
                                                ToolTip="Enter Address Line 1" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAdd" runat="server" ControlToValidate="txtMulAddress"

                                                Display="None" ErrorMessage="Please Enter Address Line 1." SetFocusOnError="true"

                                                ValidationGroup="Add" />
                                            <%--<ajaxToolKit:FilteredTextBoxExtender ID="ftbeMulAdd" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom, Numbers"
                                                            ValidChars=".': " TargetControlID="txtMulAddress">
                                                        </ajaxToolKit:FilteredTextBoxExtender>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Address Line 2</label>
                                            </div>
                                            <asp:TextBox ID="txtAddLine" runat="server" MaxLength="100" CssClass="form-control" TabIndex="11" ToolTip="Enter Address Line 2" />
                                             <asp:RequiredFieldValidator ID="rfvAdd1" runat="server" ControlToValidate="txtAddLine"
                                                Display="None" ErrorMessage="Please enter Address Line 2." SetFocusOnError="true"
                                                ValidationGroup="Add" />
                                            <%--<ajaxToolKit:FilteredTextBoxExtender ID="ftbeAddLine" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom, Numbers"
                                                            ValidChars=". " TargetControlID="txtAddLine">
                                                        </ajaxToolKit:FilteredTextBoxExtender>--%>
                                          <%--  <asp:RequiredFieldValidator ID="rfvAdd2" runat="server" ControlToValidate="txtAddLine"
                                                Display="None" ErrorMessage="Please Enter Address Line2." SetFocusOnError="true"
                                                ValidationGroup="Add" />--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>City</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="Add" TabIndex="12" ToolTip="Select City">
                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                       
                                             <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                                                Display="None" ErrorMessage="Please select City." InitialValue="0" SetFocusOnError="true"  ValidationGroup="Add" />
                                            &nbsp;<asp:RequiredFieldValidator ID="rfvddlCity" runat="server" ControlToValidate="ddlCity"
                                                Display="None" ErrorMessage="Please Select City." SetFocusOnError="true"
                                                ValidationGroup="Add" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>State/Region </label>
                                            </div>
                                            <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" TabIndex="13" ToolTip="Select State">
                                            </asp:DropDownList>
                                            &nbsp;<asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState"
                                                                Display="None" ErrorMessage="Please select State/Region." InitialValue="0" SetFocusOnError="true"  ValidationGroup="Add" />
                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="ddlState"
                                                Display="None" ErrorMessage="Please Select State." SetFocusOnError="true"
                                                ValidationGroup="Add" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemarks" runat="server" MaxLength="150" CssClass="form-control"
                                                TextMode="MultiLine" TabIndex="14" ToolTip="Enter Remark"></asp:TextBox>
                                            &nbsp;
                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRemarks"
                                                Display="None" ErrorMessage="Please Enter Remark." SetFocusOnError="true"
                                                ValidationGroup="Add" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Pin No.</label>
                                            </div>
                                            <asp:TextBox ID="txtPIN" runat="server" ToolTip="Please Enter PIN" CssClass="form-control"
                                                MaxLength="6" TabIndex="15"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789"
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtPIN">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Country</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="16" ToolTip="Select Country">
                                            </asp:DropDownList>
                                            &nbsp;<asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry"
                                                                Display="None" ErrorMessage="Please Select Country." InitialValue="0" SetFocusOnError="true"  ValidationGroup="Add" />

                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="ddlCountry"
                                                Display="None" ErrorMessage="Please Select Country." SetFocusOnError="true"
                                                ValidationGroup="Add" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Contact No. </label>               
                                            </div>
                                            <asp:TextBox ID="txtContNo" runat="server" MaxLength="12" TabIndex="17" ValidationGroup="Add" CssClass="form-control"
                                                ToolTip="Enter Contact No."></asp:TextBox>

                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtContNo"
                                                Display="None" ErrorMessage="Please Enter Contact No." SetFocusOnError="true"
                                                ValidationGroup="Add" />
                                              &nbsp<%--;<asp:RequiredFieldValidator ID="rfvContact" runat="server" ControlToValidate="txtContNo" Display="None"
                                                    ErrorMessage="Please enter Contact No." SetFocusOnError="true" ValidationGroup="Add" />--%>&nbsp;
                                             <ajaxToolKit:FilteredTextBoxExtender ID="ftbtxtContNo" runat="server" ValidChars="0123456789"
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtContNo">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                      <%--       onkeypress="return CheckNumeric(event,this);"--%>
                                        </div>


                                    </div>
                                </asp:Panel>

                            </div>

                            <div class=" col-12 btn-footer" id="divAddTo" runat="server" visible="false">
                                <asp:Button ID="btnAddTo" runat="server" Text="Add" OnClick="btnAddTo_Click" ValidationGroup="Add"
                                    CssClass="btn btn-primary" ToolTip="Click here to Add" TabIndex="18" />
                                <asp:ValidationSummary ID="ValSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />

                            </div>

                            <div class="col-12" id="divList" runat="server" visible="false">
                                <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvTo" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>RECEIVER COPY</h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
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
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                        ImageUrl="~/images/edit.png" OnClick="btnEditRec_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                        ImageUrl="~/images/delete.png" OnClick="btnDelete_Click" ToolTip="Delete Record" />
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

                            <div class="col-12" id="divDD" runat="server" visible="false">
                                <asp:Panel ID="Panel5" runat="server">
                                     <div class="sub-heading">
                                            <h5>DD/Cheque Details</h5>
                                        </div>
                                            <div class="row">
                                                  
                                                   
                                                    
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdbCheque" runat="server" RepeatDirection="Horizontal" TabIndex="19" AutoPostBack="true" OnSelectedIndexChanged="rdbCheque_SelectedIndexChanged" ToolTip="Select Cheque/DD">
                                                        <asp:ListItem Value="1" Selected="True">Cheque</asp:ListItem>
                                                        <asp:ListItem Value="2">Demand Draft</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <asp:Label ID="lblCheque" runat="server" Text="Chq.No  :" Font-Bold="true"><span style="color: red;">*</span></asp:Label>

                                                    </div>
                                                    <asp:TextBox ID="txtDDNo" runat="server" MaxLength="10" CssClass="form-control" TabIndex="20" ToolTip="Enter Cheque/DD" />
                                                    <asp:RequiredFieldValidator ID="rfvDDNo" runat="server" ControlToValidate="txtDDNo" Display="None" ErrorMessage="Please enter Cheque/DD No." SetFocusOnError="true" ValidationGroup="AddCheq" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Amount</label>
                                                    </div>
                                                    <asp:TextBox ID="txtDDAmt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="21" onkeypress="return CheckNumeric(event,this);"
                                                        ToolTip="Enter Amount" />
                                                    <asp:RequiredFieldValidator ID="rfvAmt" runat="server" ControlToValidate="txtDDAmt" Display="None" ErrorMessage="Please enter Amount." SetFocusOnError="true" ValidationGroup="AddCheq" />

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Bank</label>
                                                    </div>
                                                    <asp:TextBox ID="txtbank" runat="server" MaxLength="100" CssClass="form-control" TabIndex="22"
                                                        onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Bank">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvBank" runat="server" ControlToValidate="txtbank" Display="None" ErrorMessage="Please Enter Bank." SetFocusOnError="true" ValidationGroup="AddCheq" />

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="imgDDdate" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                       
                                                 
                                                    <asp:TextBox ID="txtDDdate" runat="server" MaxLength="10" CssClass="form-control" TabIndex="23" ToolTip="Select DD Date"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CeDDdate" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="imgDDdate" TargetControlID="txtDDdate">
                                                    </ajaxToolKit:CalendarExtender>
                                                          <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtDDdate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] " ControlToValidate="txtDDdate"
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*" ValidationGroup="Submit">
                                                </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDDdate" Display="None" ErrorMessage="Please Enter Cheque Date." SetFocusOnError="true" ValidationGroup="AddCheq" />

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:Button ID="btnAddCheque" runat="server" Text="Add Cheque Details" ValidationGroup="AddCheq" TabIndex="24" OnClick="btnAddCheque_Click" CssClass="btn btn-primary" CausesValidation="true" />
                                                <asp:ValidationSummary ID="vsCheuqeAdd" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddCheq" />

                                            </div>

                                         
                                                <div class="col-12">
                                                    <asp:Panel ID="pnlChqList" runat="server" ScrollBars="Auto">
                                                        <asp:ListView ID="lvChequeDetails" runat="server" Visible="false">
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                   <div class="sub-heading">
                                                                    CHEQUE DETAILS ENTRY LIST
                                                                </div>
                                                              </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
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
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnEditChequeRec" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                                            ImageUrl="~/Images/edit.png" OnClick="btnEditChequeRec_Click" ToolTip="Edit Record" />
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
                                            
                                        </div>
                                
                                </asp:Panel>
                            </div>


                            <div class="col-12" id="divDispDetails" runat="server" visible="false">
                                <asp:Panel ID="Panel6" runat="server">

                                    <div class=" sub-heading">
                                        <h5>Dispatch Details</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Send Date</label>
                                            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgSentDT" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtSentDT" runat="server" CssClass="form-control" TabIndex="25" ToolTip="Select Send Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgSentDT" TargetControlID="txtSentDT">
                                                </ajaxToolKit:CalendarExtender>
                                                 <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtSentDT" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] " ControlToValidate="txtSentDT"
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*" ValidationGroup="Submit">
                                                </ajaxToolKit:MaskedEditValidator>
                                                <asp:RequiredFieldValidator ID="rfvSentDT" runat="server" ErrorMessage="Please enter the Send date"
                                                    ControlToValidate="txtSentDT" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Reference no</label>
                                            </div>
                                            <asp:TextBox ID="txtcentralrefno" ReadOnly="true" CssClass="form-control" runat="server" TabIndex="26" ToolTip="Enter Central Ref. No." />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Weight</label>
                                            </div>
                                            <asp:TextBox ID="txtWeight" runat="server" MaxLength="10" CssClass="form-control" AutoPostBack="true" TabIndex="27"
                                                OnTextChanged="txtWeight_TextChanged"  ToolTip="Enter Weight"/> <%--onkeyup="validateNumeric(this);"--%>
                                             <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" ValidChars="0123456789"
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtWeight">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter  weight"
                                                    ControlToValidate="txtWeight" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>


                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Unit</label>
                                            </div>
                                            <asp:DropDownList ID="ddlUnit" AutoPostBack="true" runat="server" AppendDataBoundItems="false"
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" TabIndex="28" ToolTip="Select Unit">
                                                <asp:ListItem Value="0">GM</asp:ListItem>
                                                <asp:ListItem Value="1">KG</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Stamp Cost</label>
                                            </div>
                                            <asp:TextBox ID="txtScost" runat="server" MaxLength="10" CssClass="form-control" ReadOnly="true" TabIndex="29" ToolTip="Enter Stamp Cost" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Number of Person</label>
                                            </div>
                                            <asp:TextBox ID="txtnperson" AutoPostBack="true" OnTextChanged="txtnperson_TextChanged"
                                                runat="server"  MaxLength="10" CssClass="form-control" TabIndex="30" ToolTip="Enter No. of Person" /> <%-- onkeyup="validateNumeric(this);"--%>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" ValidChars="0123456789"
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtnperson">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter Number of Person"
                                                    ControlToValidate="txtnperson" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Extra Cost</label>
                                            </div>
                                            <asp:TextBox ID="txtExtraCost" AutoPostBack="true" OnTextChanged="txtExtraCost_TextChanged"
                                                runat="server"  MaxLength="10" CssClass="form-control" TabIndex="31" ToolTip="Enter Extra Cost" />  <%--onkeyup="validateNumeric(this);"--%>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" ValidChars="0123456789"
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtExtraCost">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter Extra Cost"
                                                    ControlToValidate="txtExtraCost" Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Cost</label>
                                            </div>
                                            <asp:TextBox ID="txtCost" runat="server" MaxLength="10" CssClass="form-control" ReadOnly="true" TabIndex="32" ToolTip="Enter Cost" />

                                        </div>


                                    </div>

                                </asp:Panel>
                            </div>

                            <div class=" col-12 btn-footer" id="divSubmit" runat="server" visible="false">

                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="33" />
                                <asp:Button ID="btnback" runat="server" OnClick="btnback_Click" Text="Back" TabIndex="35" CssClass="btn btn-primary" ToolTip="Click here to Back" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" TabIndex="34" />

                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />

                            </div>

                            <div id="divAddNew" runat="server" class=" btn-footer col-12" visible="true">
                                <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click" SkinID="LinkAddNew" Text="Add New" TabIndex="1" CssClass="btn btn-primary" ToolTip="Click here to Add New"></asp:LinkButton>
                            </div>

                            <div class="col-12" id="divListview" runat="server">
                                <asp:Panel ID="Panel7" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="IvOutwardDispatch" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>DISPATCH OUTWARD ENTRY LIST</h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <%--<th>Delete
                                                            </th>--%>
                                                            <th>Edit
                                                            </th>
                                                            <th>RFID Number
                                                            </th>
                                                            <th>Sender Name 
                                                                 Department
                                                            </th>
                                                            <th>Receiver Name
                                                            </th>
                                                            <th>Subject
                                                            </th>
                                                            <th>Post Type
                                                            </th>
                                                            <th>Letter Category
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
                                            <tr>
                                                <%-- <td style="width: 10%;">
                                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("IOTRANNO")%>'
                                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDeleteRecord_Click"
                                                                OnClientClick="showConfirmDel(this); return false;" />
                                                        </td>--%>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                        CommandArgument='<%# Eval("IOTRANNO") %>' ImageUrl="~/images/edit.png" OnClick="btnEdit_Click"
                                                        ToolTip="Edit Record" />
                                                </td>
                                                <td>
                                                    <%# Eval("RFID_NUMBER")%>
                                                </td>
                                                <td>
                                                    <%# Eval("UA_FULLNAME")%><br />
                                                    <%# Eval("SUBDEPT")%>
                                                </td>
                                                <td>
                                                    <%-- <%# Eval("SUBDEPT")%>--%>
                                                    <%# Eval("RECEVER_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SUBJECT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("POSTTYPENAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("LETTERCAT")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                    <div>
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


    <script type="text/javascript" >


        //function CheckAlphabet(event, obj) {

        //    var k = (window.event) ? event.keyCode : event.which;
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

