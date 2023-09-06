<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StrSecGPOutward.aspx.cs" Inherits="STORES_Transactions_StockEntry_StrSecGPOutward" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SECURITY GATE PASS OUTWARD ENTRY</h3>

                        </div>


                        <div class="box-body">
                            <asp:Panel ID="pnlAddNew" runat="server">
                                <%--  <div class="sub-heading">
                                        <h5>Add/Edit Item Repair Entry</h5>
                                    </div>--%>

                                <div class="col-12" id="divRadioList" runat="server" visible="true">
                                    <asp:RadioButtonList ID="rdlList" runat="server" RepeatDirection="Horizontal" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="rdlList_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Selected="True">Outward Item Entry &nbsp;</asp:ListItem>
                                        <asp:ListItem Value="2">Outward-Returnable Item Entry</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <br />
                                <div class="col-12 btn-footer " id="divAddNew" runat="server" visible="true">
                                    <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-primary" CausesValidation="true" Text="Add New" OnClick="btnAddNew_Click" Visible="true" />
                                </div>
                                <div class="col-md-12" id="divEmptyLabel" runat="server" visible="false">
                                    <p class="text-center text-bold">No Active Records Found</p>
                                </div>

                                <div id="divListOutwardGP" runat="server" visible="false">
                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th id="thEditAction" runat="server">Action</th>
                                                <th>Outward Reg.Slip Number</th>
                                                <th>Gate Pass Number</th>
                                                <th>Out Date</th>
                                                <th>Vehicle No.</th>
                                                <th id="thSelAction" runat="server" visible="false">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="lvOutwardGP" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <div class="sub-heading">
                                                            <h5>Security Gate Pass Outward Entry List</h5>
                                                        </div>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>

                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td id="tdEditAction" runat="server" visible='<%#rdlList.SelectedValue == "1" ? true : false %>'>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                CommandArgument='<%#Eval("SP_OW_ID")%>' AlternateText="Edit Record" OnClick="btnEdit_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("REG_SLIP_NO")%>                                                       
                                                        </td>
                                                        <td>
                                                            <%# Eval("GP_NUMBER")%>                                                       
                                                        </td>
                                                        <td>
                                                            <%# Eval("OUT_DATE","{0:dd-MM-yyyy}")%>                                      
                                                        </td>
                                                        <td>
                                                            <%# Eval("VEHICLE_NO")%>
                                                        </td>
                                                        <td id="tdSelAction" runat="server" visible='<%#rdlList.SelectedValue == "1" ? false : true %>'>
                                                            <asp:Button ID="btnSelect" runat="server" CausesValidation="false" Text="Select"
                                                                CommandArgument='<%#Eval("SP_OW_ID")%>' OnClick="btnSelect_Click" CssClass="btn btn-primary" />
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>

                                        </tbody>
                                    </table>


                                </div>



                            </asp:Panel>

                            <asp:Panel ID="pnlItemRepair" runat="server">
                                <div class="col-12" id="divOutwardGP" runat="server" visible="false">
                                    <div class="sub-heading">
                                        <h5>Add/Edit Security Gate Pass Outward Entry</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divTranSlipNo" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Outward Reg.Slip Number</label>
                                            </div>
                                            <asp:TextBox ID="txtRegSlipNo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Gate Pass Number</label>
                                            </div>
                                            <asp:DropDownList ID="ddlGatePass" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                TabIndex="2" ToolTip="Select Requisition" AutoPostBack="true"  OnSelectedIndexChanged="ddlGatePass_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlGatePass"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Gate Pass Number"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divEmp" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Carried By (Employee)</label>
                                            </div>
                                            <asp:TextBox ID="txtCarriedEmp" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                        </div>

                                    </div>

                                    <div class="mb-4" id="divlvDsr" runat="server" visible="false">
                                        <div>
                                            <asp:ListView ID="lvDsrItem" runat="server" Enabled="false">
                                                <LayoutTemplate>
                                                    <div class="lgv1">
                                                        <div class="sub-heading">
                                                            <h5>Items List</h5>
                                                        </div>

                                                        <table class="table table-striped table-bordered nowrap " style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Select</th>
                                                                    <th>Item Serial Number</th>
                                                                    <th>Nature Of Complaint</th>

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
                                                            <asp:CheckBox ID="chkDsrselect" runat="server" ToolTip='<%# Eval("IR_TRANID")%>' Checked='<%#rdlList.SelectedValue == "2" ? false : true %>' />

                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDsrNumber" runat="server" Text='<%# Eval("DSR_NUMBER")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txtNatureOfComplaint" runat="server" Text='<%# Eval("COMPLAINT_NATURE")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Vehicle Number</label>
                                                    <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtVehicleNo" Display="None"
                                                        ErrorMessage="Please Enter Vehicle Number" ValidationGroup="Submit" />
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Out Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="ImaCalStartDate">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtOutDate" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                    <%--   <div class="input-group-addon">
                                                                <asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Style="cursor: pointer" />
                                                            </div>--%>

                                                    <ajaxToolKit:CalendarExtender ID="ceOutDate" runat="server" Enabled="true"
                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtOutDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meIssueDate" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                        MessageValidatorTip="true" TargetControlID="txtOutDate">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meIssueDate" ControlToValidate="txtOutDate"
                                                        IsValidEmpty="false" ErrorMessage="Please Enter Valid GRN Date In [dd/MM/yyyy] format" EmptyValueMessage="Please Enter Out Date"
                                                        InvalidValueMessage="Out Date Is Invalid  [Enter In dd/MM/yyyy Format]" Display="None" SetFocusOnError="true"
                                                        Text="*" ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>


                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Out Time</label>
                                                </div>
                                                <asp:TextBox ID="txtOutTime" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtOutTime"
                                                    Mask="99:99" MaskType="Time"
                                                    AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                                    DisplayMoney="Left" OnInvalidCssClass="errordate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevEnterTimeTo" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtOutTime" Display="None" EmptyValueBlurredText="Empty"
                                                    InvalidValueMessage="Out Time is Invalid (Enter hh:mm Format)" EmptyValueMessage="Please Enter Out Time"
                                                    SetFocusOnError="true" IsValidEmpty="false" ValidationGroup="Submit" />

                                            </div>



                                        </div>
                                    </div>
                                    <div class="col-12" id="divReceiveFields" runat="server" visible="false">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Received Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="Image1">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtReceiveDate" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                    <%--<div class="input-group-addon">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png"
                                                            Style="cursor: pointer" />
                                                    </div>--%>

                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtReceiveDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                        MessageValidatorTip="true" TargetControlID="txtReceiveDate" OnInvalidCssClass="errordate" ClearMaskOnLostFocus="true">
                                                    </ajaxToolKit:MaskedEditExtender>

                                                    <ajaxToolKit:MaskedEditValidator ID="mevIndDate" runat="server" ControlExtender="MaskedEditExtender2"
                                                        ControlToValidate="txtReceiveDate" Display="None" EmptyValueBlurredText="Empty" IsValidEmpty="false"
                                                        InvalidValueBlurredMessage="Received Date is  Invalid " ValidationGroup="Submit" Text="*" EmptyValueMessage="Please Enter Received Date"
                                                        InvalidValueMessage="Received Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="True"></ajaxToolKit:MaskedEditValidator>


                                                </div>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="DVRCVTIME">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Received Time</label>
                                                </div>
                                                <asp:TextBox ID="txtReceiveTime" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtReceiveTime"
                                                    Mask="99:99" MaskType="Time"
                                                    AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                                    DisplayMoney="Left" OnInvalidCssClass="errordate" />

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender3"
                                                    ControlToValidate="txtReceiveTime" Display="None" EmptyValueBlurredText="Empty"
                                                    InvalidValueMessage="Received Time is Invalid (Enter hh:mm Format)" EmptyValueMessage="Please Enter Received Time"
                                                    SetFocusOnError="true" IsValidEmpty="false" ValidationGroup="Submit" />

                                            </div>
                                        </div>

                                    </div>



                                </div>

                            </asp:Panel>

                            <div class="col-12 btn-footer mt-3" id="divButtons" runat="server" visible="false">
                                <asp:Button ID="btnAddNew2" runat="server" CausesValidation="true" OnClick="btnAddNew_Click" 
                                    Text="AddNew" CssClass="btn btn-primary" TabIndex="7" ToolTip="Click To Add" />
                                <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" OnClick="btnSubmit_Click"
                                    Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" TabIndex="7" ToolTip="Click To Submit" />
                                <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" CausesValidation="true" Text="Back" OnClick="btnBack_Click" />
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click"
                                    Text="Cancel" CssClass="btn btn-warning" TabIndex="10" ToolTip="Click To Reset" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                        </div>
                    </div>

                </div>

            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) { var oControl = args.get_postBackElement(); oControl.disabled = true; }

    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>