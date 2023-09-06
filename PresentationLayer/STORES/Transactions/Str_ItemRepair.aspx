<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_ItemRepair.aspx.cs" Inherits="STORES_Transactions_Str_ItemRepair" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>
    <link href="../Scripts/impromptu.css" rel="stylesheet" type="text/css" />--%>


    <%--  <script src="../Scripts/jquery.js" type="text/javascript"></script>--%>

    <%--    <script src="../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

    <link href="../Scripts/impromptu.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript">
        ; debugger
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 9999999999;
        }


        function ValidateTextBox() {
            if (document.getElementById("txtStudName").value == "") {
                alert("Please enter Name!");
                return false;
            }

        }

    </script>
    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) { var oControl = args.get_postBackElement(); oControl.disabled = true; }

    </script>

    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ITEM REPAIR ENTRY</h3>

                        </div>


                        <div class="box-body">
                            <asp:Panel ID="pnlAddNew" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Add/Edit Item Repair Entry</h5>
                                    </div>

                                    <div class="row">
                                        <div class="form-group col-lg-8 col-md-6 col-12" id="divRadioList" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:RadioButtonList ID="rdlList" runat="server" RepeatDirection="Horizontal" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="rdlList_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">Outward Item Entry &nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">Outward-Returnable Item Entry</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-12 btn-footer" id="divAddNew" runat="server" visible="true">
                                            <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-primary" CausesValidation="true" Text="Add New" OnClick="btnAddNew_Click" />
                                            <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
                                        </div>

                                        <div class="col-12" id="divListItemReapir" runat="server" visible="false">
                                            <asp:Label ID="lblhead" runat="server" Font-Size="Large" Text="Item Repair Entry List"></asp:Label>
                                            <%--<div class="sub-heading">
                                                <h5>Item Repair Entry List</h5>
                                            </div>--%>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th id="thEditAction" runat="server">Action</th>
                                                        <th>Gate Pass Number</th>
                                                        <th>Item In</th>
                                                        <%-- <th>Item Name</th>--%>
                                                        <th>Transaction Date</th>
                                                        <th id="thSelAction" runat="server" visible="false">Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>

                                                    <asp:ListView ID="lvItemRepairEntry" runat="server">
                                                        <%--OnItemDataBound="lvItemRepairEntry_ItemDataBound"--%>
                                                        <LayoutTemplate>
                                                            <div>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td id="tdEditAction" runat="server" visible='<%#rdlList.SelectedValue == "1" ? true : false %>'>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                        CommandArgument='<%#Eval("IR_ID")%>' AlternateText="Edit Record" OnClick="btnEdit_Click"
                                                                        ToolTip='<%#Eval("SEC_OUT_DONE_ID")%>' />
                                                                    <%--    Visible='<%#Eval("SEC_OUT_DONE_ID").ToString() == "0" ? true : false %>' />--%>
                                                                      
                                                                </td>

                                                                <td>
                                                                    <%# Eval("GP_NUMBER")%>                                                       
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ITEM_IN")%>                                                       
                                                                </td>
                                                                <%--  <td>
                                                                    <%# Eval("ITEM_NAME")%>                                                    
                                                                </td>--%>
                                                                <td>
                                                                    <%# Eval("TRAN_DATE","{0:dd-MM-yyyy}")%>
                                                                </td>
                                                                <td id="tdSelAction" runat="server" visible='<%#rdlList.SelectedValue == "1" ? false : true %>'>
                                                                    <asp:Button ID="btnSelect" runat="server" CausesValidation="false" Text="Select"
                                                                        CommandArgument='<%#Eval("IR_ID")%>' OnClick="btnSelect_Click" CssClass="btn btn-primary"
                                                                        Enabled='<%#Eval("FLAG").ToString() == "0" ? true : false %>' />

                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                </tbody>
                                            </table>


                                        </div>

                                        <%------------------------21/04/2022------------------%>
                                        <div class="form-group col-md-12" runat="server" id="divEmptyLabel" visible="false">
                                            <p class="text-center text-bold">
                                                No Active Records Found
                                            </p>
                                        </div>
                                        <%------------------------21/04/2022------------------%>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12" id="divItemRepair" runat="server" visible="false">
                                <asp:Panel ID="pnlItemRepair" runat="server">
                                    <div class="panel panel-info">
                                        <div class="sub-heading">
                                            <%-- <h5>Add/Edit Item Repair Entry</h5>--%>
                                        </div>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divGatePassNum" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Gate Pass Number</label>
                                                    </div>
                                                    <asp:TextBox ID="txtGatePassNum" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="div2" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Item In</label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdlItemIn" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdlItemIn_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Text="MainStore" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="College"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divCollege" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>College Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        TabIndex="2" ToolTip="Select Requisition" AutoPostBack="false" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlCollege"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select College"
                                                        ValidationGroup="StoreReq"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divDept" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Department</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        TabIndex="2" ToolTip="Select Requisition" AutoPostBack="false" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDept"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Department"
                                                        ValidationGroup="StoreReq"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Item Sub Category</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubCategory" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="True" TabIndex="3" ToolTip="Select Sub Category" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlSubCategory"
                                                        Display="None" ErrorMessage="Please Select Sub Category" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divItem" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Item Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4"
                                                        AppendDataBoundItems="true" AutoPostBack="false" ToolTip="Select Item" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlItem"
                                                        Display="None" ErrorMessage="Please Select Item" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                    <asp:HiddenField ID="hdnDsrRowCount" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hdnAddedDsrRowCount" runat="server" Value="0" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer" id="divShowBack" runat="server" visible="true">
                                            <asp:Button ID="btnShow" runat="server" CausesValidation="true" OnClick="btnShow_Click"
                                                Text="Show" ValidationGroup="Show" CssClass="btn btn-primary" TabIndex="7" ToolTip="Click To Show Item" />
                                            <asp:Button ID="btnBackShow" runat="server" CausesValidation="true" OnClick="btnBack_Click"
                                                Text="Back" CssClass="btn btn-info" TabIndex="7" ToolTip="Click To Back" />
                                            <asp:ValidationSummary ID="vsdsr" runat="server" ValidationGroup="Show" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                        </div>



                                        <div class="col-12" id="divRepairItemDet" runat="server" visible="false">
                                            <%--<div class="panel panel-info">
                                                        <div class="panel-heading">Item Details</div>
                                                        <div class="panel-body">--%>
                                            <div class="row">


                                                <div class="form-group col-md-12" id="divCarryEmpDetails" runat="server" visible="false">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div4" runat="server">

                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Carried Employee</label>
                                                            </div>
                                                            <asp:RadioButtonList ID="rdlCarriedEmp" runat="server" RepeatDirection="Horizontal" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="rdlCarriedEmp_SelectedIndexChanged">
                                                                <asp:ListItem Value="1" Selected="True">College &nbsp;</asp:ListItem>
                                                                <asp:ListItem Value="2">Outside-College</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divToDept" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Carry Employee Department</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlToDept" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlToDept_SelectedIndexChanged"
                                                                TabIndex="2" ToolTip="Select Requisition">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divToEmp" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Carry Employee Name</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlToEmployee" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                                TabIndex="2" ToolTip="Select Requisition">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divCarriedEmpName" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Employee Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtCarriedEmpName" runat="server" CssClass="form-control" TabIndex="3" MaxLength="60"></asp:TextBox>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divMobileNo" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Mobile No.</label>
                                                            </div>
                                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" TabIndex="3" MaxLength="12"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                FilterType="Custom, Numbers" ValidChars="+- " TargetControlID="txtMobileNo">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <%--<asp:RequiredFieldValidator ID="rfvContact" runat="server" ErrorMessage="Please Enter Mobile No." ControlToValidate="txtMobileNo" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>


                                                <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>To Whom Sent</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSentTo" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>

                                                </div>--%>
                                                <%-----------------------------21/04/2022----------------start------------------------------------------------------------------------%>
                                                <div class="form-group col-md-12" id="divOutFieldsConfig" runat="server" visible="false">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divToWhomSent" runat="server" visible="true">
                                                            <label>To Whom Sent:<span id="span5" style="color: #FF0000">*</span> </label>
                                                            <asp:TextBox ID="txtSentTo" runat="server" CssClass="form-control" TabIndex="3" MaxLength="60"></asp:TextBox>
                                                            <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtSentTo" Display="None"
                                                                ErrorMessage="Please Enter To Whom Sent" ValidationGroup="Submit" />--%>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCatagoryShort" runat="server" TargetControlID="txtSentTo"
                                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxy z" FilterMode="ValidChars">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-md-3" id="divVehicleNum" runat="server" visible="true">
                                                            <label>Vehicle Number<span style="color: #FF0000">*</span>:</label>
                                                            <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="form-control" MaxLength="64"></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtVehicleNo" Display="None"
                                                                ErrorMessage="Please Enter Vehicle Number" ValidationGroup="Submit" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender222" runat="server" TargetControlID="txtVehicleNo"
                                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxy z0123456789" FilterMode="ValidChars">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-md-3" id="divOutDate" runat="server" visible="true">
                                                            <label>Out Date<span style="color: #FF0000">*</span>:</label>

                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtOutDate" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png"
                                                                        Style="cursor: pointer" />
                                                                </div>

                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtOutDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                    MessageValidatorTip="true" TargetControlID="txtOutDate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meIssueDate" ControlToValidate="txtOutDate"
                                                                    IsValidEmpty="false" ErrorMessage="Please Enter Valid GRN Date In [dd/MM/yyyy] format" EmptyValueMessage="Please Enter Out Date"
                                                                    InvalidValueMessage="Out Date Is Invalid  [Enter In dd/MM/yyyy Format]" Display="None" SetFocusOnError="true"
                                                                    Text="*" ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>


                                                            </div>
                                                        </div>
                                                        <div class="col-md-2" id="divOutTime" runat="server" visible="true">
                                                            <label><span style="color: red">*</span>Out Time</label>
                                                            <asp:TextBox ID="txtOutTime" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtOutTime"
                                                                Mask="99:99" MaskType="Time"
                                                                AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                                                DisplayMoney="Left" OnInvalidCssClass="errordate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevEnterTimeTo" runat="server" ControlExtender="MaskedEditExtender2"
                                                                ControlToValidate="txtOutTime" Display="None" EmptyValueBlurredText="Empty"
                                                                InvalidValueMessage="Out Time is Invalid (Enter hh:mm Format)" EmptyValueMessage="Please Enter Out Time"
                                                                SetFocusOnError="true" IsValidEmpty="false" ValidationGroup="Submit" />

                                                        </div>
                                                    </div>

                                                </div>
                                                <%-----------------------------21/04/2022----------------end------------------------------------------------------------------------%>


                                                <div class="form-group col-md-12" id="divReturnDate" runat="server" visible="false">
                                                    <%--21/04/2022--%>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Return Date</label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon" id="ImaCalStartDate">
                                                                <i class="fa fa-calendar text-blue"></i>
                                                            </div>

                                                            <asp:TextBox ID="txtReturnDate" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Item Issue Date"></asp:TextBox>
                                                            <%-- <div class="input-group-addon">
                                                            <asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                Style="cursor: pointer" />
                                                        </div>--%>

                                                            <ajaxToolKit:CalendarExtender ID="cetxtIndentSlipDate" runat="server" Enabled="true"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtReturnDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meIssueDate" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" TargetControlID="txtReturnDate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meIssueDate" ControlToValidate="txtReturnDate"
                                                                IsValidEmpty="true" ErrorMessage="Please Enter Valid Return Date In [dd/MM/yyyy] format"
                                                                InvalidValueMessage="Return Date Is Invalid  [Enter In dd/MM/yyyy Format]" Display="None" SetFocusOnError="true"
                                                                Text="*" ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>


                                                        </div>
                                                    </div>


                                                </div>

                                                <%-- ------------------21/04/2022-----------------------------%>
                                                <div class="form-group col-md-12" id="divReceiveFieldsConfig" runat="server" visible="false">

                                                    <div class="form-group col-md-4">
                                                        <label>Received Date<span style="color: #FF0000">*</span>:</label>

                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtReceiveDate" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Style="cursor: pointer" />
                                                            </div>

                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtReceiveDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" TargetControlID="txtReceiveDate" OnInvalidCssClass="errordate" ClearMaskOnLostFocus="true">
                                                            </ajaxToolKit:MaskedEditExtender>

                                                            <ajaxToolKit:MaskedEditValidator ID="mevIndDate" runat="server" ControlExtender="MaskedEditExtender2"
                                                                ControlToValidate="txtReceiveDate" Display="None" EmptyValueBlurredText="Empty" IsValidEmpty="false"
                                                                InvalidValueBlurredMessage="Received Date is  Invalid " ValidationGroup="Submit" Text="*" EmptyValueMessage="Please Enter Received Date"
                                                                InvalidValueMessage="Received Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="True"></ajaxToolKit:MaskedEditValidator>


                                                        </div>

                                                        <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtOutDate" Display="None"
                                                                ErrorMessage="Please Enter Received Date " ValidationGroup="Submit" />--%>
                                                    </div>

                                                    <div class="col-md-4" id="DVRCVTIME">
                                                        <label><span style="color: red">*</span>Received Time</label>
                                                        <asp:TextBox ID="txtReceiveTime" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtReceiveTime"
                                                            Mask="99:99" MaskType="Time"
                                                            AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                                            DisplayMoney="Left" OnInvalidCssClass="errordate" />

                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender3"
                                                            ControlToValidate="txtReceiveTime" Display="None" EmptyValueBlurredText="Empty"
                                                            InvalidValueMessage="Received Time is Invalid (Enter hh:mm Format)" EmptyValueMessage="Please Enter Received Time"
                                                            SetFocusOnError="true" IsValidEmpty="false" ValidationGroup="Submit" />


                                                    </div>

                                                </div>

                                                <%-- ------------------21/04/2022---end--------------------------%>
                                            </div>
                                            <div id="divOWEntryList" runat="server" visible="false">

                                                <div class="form-group " id="divDsr" runat="server" visible="false">
                                                    <asp:ListView ID="lvDsrItem" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="lgv1">
                                                                <div class="sub-heading">
                                                                    <h5>Item List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap " style="width: 100%;margin-bottom:0px" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="width:5%">Select</th>
                                                                            <th style="width:27%">Item Serial Number</th>
                                                                            <th style="width:50%">Nature Of Complaint</th>
                                                                        </tr>
                                                                    </thead>

                                                                </table>
                                                            </div>
                                                            <div class="listview-container" style="overflow-y: scroll; overflow-x: hidden; height: 150px">
                                                                <div id="demo-grid" class="vista-griid">
                                                                    <table class="table table-bordered table-hover table-responsive">
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </div>

                                                            </div>

                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="width:10%">
                                                                    <asp:CheckBox ID="chkDsrselect" runat="server" ToolTip='<%# Eval("ITEM_NO")%>' />
                                                                </td>
                                                                <td style="width:25%">
                                                                    <asp:Label ID="lblDsrNumber" runat="server" Text='<%# Eval("DSR_NUMBER")%>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnDSRListId" runat="server" Value='<%#Eval("INVDINO")%>' />
                                                                </td>
                                                                <td style="width:50%">
                                                                    <asp:TextBox ID="txtNatureOfComplaint" runat="server" CssClass="form-control" MaxLength="524"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>

                                                </div>

                                                <div class="col-1 btn-footer" id="divAddItem" runat="server" visible="false">

                                                    <asp:Button ID="btnAdditem" runat="server" CausesValidation="true" OnClientClick="return ValDupItem()" OnClick="btnAdditem_Click" Width="70px"
                                                        Text="Add" CssClass="btn btn-primary" TabIndex="7" ToolTip="Click To Add Item" />

                                                </div>

                                                <div id="divAddDsrList" runat="server" visible="false">

                                                    <asp:ListView ID="lvAddDsr" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="lgv1">
                                                                <div class="sub-heading">
                                                                    <h5>Item Entry List</h5>
                                                                </div>

                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>

                                                                            <th></th>
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
                                                                    <asp:ImageButton ID="btnDeleteItem" runat="server" CausesValidation="false" ImageUrl="~/Images/delete.png"
                                                                        CommandArgument='<%#Eval("DSR_NUMBER")%>' AlternateText="Delete Record" OnClick="btnDeleteItem_Click" />

                                                                    <asp:HiddenField ID="hdnDSRId" runat="server" Value='<%#Eval("INVDINO")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAddDsrNumber" runat="server" Text='<%# Eval("DSR_NUMBER")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="txtAddNatureOfComplaint" runat="server" Text='<%# Eval("COMPLAINT_NATURE")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>


                                                </div>

                                            </div>
                                            <div id="divOWReturnbleList" runat="server" visible="false">
                                                <asp:ListView ID="lvOWReturnble" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="lgv1">
                                                            <div class="sub-heading">
                                                                <h5>Item List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;margin-bottom:0px" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Select</th>
                                                                        <th>Item Serial Number</th>
                                                                        <th>Nature Of Complaint</th>
                                                                        <th>Repair Cost</th>
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
                                                                <asp:CheckBox ID="chkOWRDsrselect" runat="server" ToolTip='<%# Eval("ITEM_NO")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblOWRDsrNumber" runat="server" Text='<%# Eval("DSR_NUMBER")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdnOWDSRId" runat="server" Value='<%#Eval("INVDINO")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtOWRNatureOfComplaint" runat="server" Text='<%# Eval("COMPLAINT_NATURE")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRepairCost" runat="server" CssClass="form-control" MaxLength="7"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars"
                                                                    TargetControlID="txtRepairCost" ValidChars=".">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>

                                                </asp:ListView>


                                            </div>
                                        </div>
                                </asp:Panel>

                                <div class="col-12 btn-footer mt-4" id="divButtons" runat="server" visible="false">
                                    <asp:Button ID="btnAddNew2" runat="server" CausesValidation="true" OnClick="btnAddNew_Click"
                                        Text="AddNew" CssClass="btn btn-primary" TabIndex="7" ToolTip="Click To Add" />
                                    <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" OnClick="btnSubmit_Click" OnClientClick="return ValidateFields()"
                                        Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" TabIndex="7" ToolTip="Click To Submit" />
                                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" CausesValidation="true" Text="Back" OnClick="btnBack_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click"
                                        Text="Cancel" CssClass="btn btn-warning" TabIndex="10" ToolTip="Click To Reset" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <asp:HiddenField ID="hdnSelectList" runat="server" Value="1" />
                                </div>

                            </div>



                        </div>

                    </div>

                </div>


            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" lang="javascript">
        function OnOffGridFields() {
            //var ListValue = $('#<%=rdlList.ClientID %> input[type=radio]:checked').val();

            if ($('#<%=rdlList.ClientID %> input[type=radio]:checked').val() == 2) {
                var RowCount = Number(document.getElementById('<%=hdnRowCount.ClientID%>').value);
                for (var i = 0; i < RowCount; i++) {
                    document.getElementById('ctl00_ContentPlaceHolder1_lvItemRepairEntry_ctrl' + i + '_thEditAction').style.display = 'none';
                    document.getElementById('ctl00_ContentPlaceHolder1_lvItemRepairEntry_ctrl' + i + '_tdEditAction').style.display = 'none';
                }
            }
        }


        function ValidateFields() {

            if (document.getElementById('<%=hdnSelectList.ClientID%>').value == 1) {
                if ($('#<%=rdlCarriedEmp.ClientID %> input[type=radio]:checked').val() == 1) {
                    if (document.getElementById('<%=ddlToDept.ClientID%>').value == 0) {
                        alert('Please Select Carry Employee Department.');
                        return false;
                    }
                    if (document.getElementById('<%=ddlToEmployee.ClientID%>').value == 0) {
                        alert('Please Select Carry Employee Name.');
                        return false;
                    }

                }
                else {
                    if (document.getElementById('<%=txtCarriedEmpName.ClientID%>').value == '') {
                        alert('Please Enter Employee Name.');
                        return false;
                    }
                    if (document.getElementById('<%=txtMobileNo.ClientID%>').value == '') {
                        alert('Please Enter Mobile No.');
                        return false;
                    }
                }
                if (document.getElementById('<%=txtSentTo.ClientID%>').value == '') {
                    alert('Please Enter To Whom Sent.');
                    return false;
                }
                if (document.getElementById('<%=txtVehicleNo.ClientID%>').value == '') {
                    alert('Please Enter Vehicle Number.');
                    return false;
                }
                if (document.getElementById('<%=txtOutDate.ClientID%>').value == '') {
                    alert('Please Enter Out Date.');
                    return false;
                }
                else {
                    var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
                    if (!(date_regex.test(document.getElementById('<%= txtOutDate.ClientID %>').value))) {
                        alert("Out Date Is Invalid (Enter In [dd/MM/yyyy] Format).");
                        return false;
                    }
                }
                if (document.getElementById('<%=txtOutTime.ClientID%>').value == '')                 //Shaikh Juned (29-04-2022)---Start
                {
                    alert('Please Enter Out Time.');
                    return false;
                }
                else {
                    if (ValidateTime(document.getElementById('<%=txtOutTime.ClientID%>').value, "Out Time") == false) return false;
                }
            }
            else {

                if (document.getElementById('<%=txtReceiveDate.ClientID %>').value == '') {
                    alert("Please Enter Received Date.");
                    return false;
                }
                else {

                    var date_regexx = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
                    if (!(date_regexx.test(document.getElementById('<%= txtReceiveDate.ClientID %>').value))) {
                        alert("Received Date Is Invalid (Enter In [dd/MM/yyyy] Format).");
                        return false;
                    }
                }

                if (document.getElementById('<%=txtReceiveTime.ClientID%>').value == '')      //Shaikh Juned (29-04-2022)---Start
                {
                    alert('Please Enter Received Time.');
                    return false;
                }
                else {
                    if (ValidateTime(document.getElementById('<%=txtReceiveTime.ClientID%>').value, "Received Time") == false) return false;
                }

            }

        }
        function ValidateTime(InputTime, InputText) {
            var Hour = InputTime.split(":");
            var Minute = Hour[1].split(" ");

            if ((Hour[0] <= 12 && Hour[0] >= 0) && (Minute[0] <= 59 && Minute[0] >= 0)) {

            } else {
                alert(InputText + ' Is Invalid [Enter HH:MM (AM/PM) Format]');
                return false;
            }
        }

        function ValDupItem() {
            //  try {              

            var DsrRowCount = Number(document.getElementById('<%=hdnDsrRowCount.ClientID%>').value);
            var ChkCount = 0;
            for (var i = 0; i < DsrRowCount; i++) {
                var DsrCheck = document.getElementById('ctl00_ContentPlaceHolder1_lvDsrItem_ctrl' + i + '_chkDsrselect');

                if (DsrCheck.checked) {
                    ChkCount++;
                    var DsrNumber1 = document.getElementById('ctl00_ContentPlaceHolder1_lvDsrItem_ctrl' + i + '_lblDsrNumber').inn;
                    var DsrAddedRowCount = Number(document.getElementById('<%=hdnAddedDsrRowCount.ClientID%>').value);
                    for (var j = 0; j < DsrAddedRowCount; j++) {

                        var DsrNumber2 = document.getElementById('ctl00_ContentPlaceHolder1_lvAddDsr_ctrl' + i + '_lblAddDsrNumber').innerText;
                        if (DsrNumber1 == DsrNumber2) {
                            alert(DsrNumber1 + ' is Already Exist.');
                            return false;
                        }
                    }
                }
            }
            if (ChkCount == 0) {
                alert('Please Select At Least One Item.');
                return false;
            }

            // }
            //catch (e) {
            //    alert("Error: " + e.description);
            //}

        }


    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

