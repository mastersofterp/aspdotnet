<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ItemMaster.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Master_ItemMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
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
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ITEM MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <%-- <div class=" sub-heading">Item Details</div>--%>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Item Name </label>
                                        </div>
                                        <asp:TextBox ID="txtItemName" runat="server" MaxLength="100" CssClass="form-control" ToolTip="Enter Item Name"
                                            onkeypress="return CheckAlphaNumeric(event, this);" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvItemName" runat="server" SetFocusOnError="true" Display="None"
                                            ErrorMessage="Please Enter Item Name."
                                            ValidationGroup="Submit" ControlToValidate="txtItemName"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Unit</label>
                                        </div>
                                        <asp:DropDownList ID="ddlunit" runat="server" TabIndex="2" AppendDataBoundItems="true" ValidationGroup="Submit"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Unit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvUnit" ValidationGroup="Submit" ControlToValidate="ddlunit" Display="None"
                                            ErrorMessage="Please Select Unit." SetFocusOnError="true" runat="server"
                                            InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Rate/Unit </label>
                                        </div>
                                        <asp:TextBox ID="txtRate" runat="server" MaxLength="8" TabIndex="3"
                                            CssClass="form-control" ToolTip="Enter Rate/Unit"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FTBE1" runat="server" FilterType="Custom, Numbers" TargetControlID="txtRate"
                                            ValidChars=".">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvRate" runat="server" SetFocusOnError="true" Display="None"
                                            ErrorMessage="Please Enter Rate Per Unit."
                                            ValidationGroup="Submit" ControlToValidate="txtRate"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revRate" runat="server" ControlToValidate="txtRate" Display="None"
                                            ErrorMessage="Two decimals allowed for Rate/Unit." ValidationExpression="^[+]?[0-9]\d{0,14}(\.\d{1,2})?%?$"
                                            SetFocusOnError="true" ValidationGroup="Submit"></asp:RegularExpressionValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Item Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlItemType" runat="server" TabIndex="5" AppendDataBoundItems="true"
                                            ValidationGroup="Submit" CssClass="form-control" data-select2-enable="true" ToolTip="Select Item Type">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Fuel</asp:ListItem>
                                            <asp:ListItem Value="2">Indent</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvItemType" ValidationGroup="Submit" ControlToValidate="ddlItemType"
                                            Display="None" ErrorMessage="Please Select Item Type." SetFocusOnError="true" runat="server"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="6"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="8" CssClass="btn btn-info"
                                    OnClick="btnReport_Click" ToolTip="Click here to Show Report" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="7"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />

                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />

                            </div>
                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvItem" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Item(Fuel and Indent) Entry List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>EDIT
                                                            </th>
                                                            <th>ITEM NAME
                                                            </th>
                                                            <th>UNIT
                                                            </th>
                                                            <th>RATE PER UNIT
                                                            </th>
                                                            <th>ITEM TYPE
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%# Eval("ITEM_ID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("ITEM_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("UNIT_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("RATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ITEM_TYPE").ToString() == "1" ? "Fuel" : "Indent"%>
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
</asp:Content>
