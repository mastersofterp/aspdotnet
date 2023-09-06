<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ItemMaster.aspx.cs" Inherits="Health_StockMaintenance_ItemMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="updpnlMain" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">ITEM MASTER</h3>
                    </div>
                    <div class="box-body">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Item Master</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">Item Sub Group Master</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="1">Item Group Master</a>
                                </li>
                            </ul>

                            <div class="tab-content" id="my-tab-content">
                                <div class="tab-pane active" id="tab_1">
                                    <div class="box-body">
                                        <%--<div>
                                            <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updatePanel1"
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
                                        </div>--%>
                                        <asp:UpdatePanel ID="updatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Item Sub Group</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlItemSubGroup" AppendDataBoundItems="true" runat="server" data-select2-enable="true"
                                                                CssClass="form-control" ToolTip="Select Item Sub Group" TabIndex="1">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlItemSubGroup" runat="server" ControlToValidate="ddlItemSubGroup"
                                                                Display="None" ErrorMessage="Please Select Item Sub Group" ValidationGroup="store"
                                                                InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trunit" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Unit</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                                ToolTip="Select Unit" TabIndex="3" data-select2-enable="true">
                                                                <asp:ListItem Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvUnit" runat="server" ControlToValidate="ddlUnit"
                                                                Display="None" ErrorMessage="Select Unit " InitialValue="0" ValidationGroup="store">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Item Code</label>
                                                            </div>
                                                            <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control" MaxLength="30" TabIndex="4"
                                                                onKeyUp="LowercaseToUppercase(this)" ToolTip="Enter Item Code"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeItemCode" runat="server" TargetControlID="txtItemCode"
                                                                FilterType="Custom, Numbers, LowercaseLetters, UppercaseLetters" FilterMode="ValidChars" ValidChars="/">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvtxtItemCode" runat="server" ControlToValidate="txtItemCode"
                                                                Display="None" ErrorMessage="Please Enter Item Code" ValidationGroup="store"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Min Quantity</label>
                                                            </div>
                                                            <asp:TextBox ID="txtMinQuantity" runat="server" CssClass="form-control" MaxLength="10" TabIndex="7"
                                                                Text="0" ToolTip="Enter Minimum Quantity"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtMinQuantityFilter" runat="server" FilterType="Numbers,custom"
                                                                FilterMode="ValidChars" TargetControlID="txtMinQuantity" ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Ope.Bal Quantity</label>
                                                            </div>
                                                            <asp:TextBox ID="txtOpeningBalanceQuantity" runat="server" CssClass="form-control" Text="0"
                                                                MaxLength="10" TabIndex="10" ToolTip="Enter Opening Balance Quantity"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtOpBalQtyFilter" runat="server" FilterType="Numbers,custom"
                                                                FilterMode="ValidChars" TargetControlID="txtOpeningBalanceQuantity" ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Item Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control"
                                                                MaxLength="100" TabIndex="2" ToolTip="Enter Item Name"
                                                                onKeyUp="LowercaseToUppercase(this)"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtName" runat="server" TargetControlID="txtItemName"
                                                                FilterType="Custom, Numbers, LowercaseLetters, UppercaseLetters" FilterMode="ValidChars" ValidChars="- ">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvtxtItemName" runat="server" ControlToValidate="txtItemName"
                                                                Display="None" ErrorMessage="Please Enter Item Name" ValidationGroup="store"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Re-Order Level</label>
                                                            </div>
                                                            <asp:TextBox ID="txtReorderLevel" runat="server" CssClass="form-control" MaxLength="5" TabIndex="6"
                                                                Text="0" ToolTip="Enter Re-Order Level"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtReorderLevelFilter" runat="server" FilterType="Numbers,custom"
                                                                FilterMode="ValidChars" TargetControlID="txtReorderLevel" ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Max Quantity</label>
                                                            </div>
                                                            <asp:TextBox ID="txtMaxQuantity" runat="server" CssClass="form-control" MaxLength="10" TabIndex="5"
                                                                Text="0" ToolTip="Enter Maximum Quantity"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtMaxQuantityFilter" runat="server" FilterType="Numbers,custom"
                                                                FilterMode="ValidChars" TargetControlID="txtMaxQuantity" ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Remark</label>
                                                            </div>
                                                            <asp:TextBox ID="txtCommonDescriptionOfItem" runat="server" CssClass="form-control" MaxLength="2000"
                                                                TabIndex="8" TextMode="MultiLine" ToolTip="Enter Remark If any"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlItmType" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                TabIndex="9" Visible="false" ToolTip="Select Item Type" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select
                                                                </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="butItemMasterSubmit" ValidationGroup="store" Text="Submit" runat="server" TabIndex="11"
                                                        CssClass="btn btn-outline-primary" ToolTip="Click here to Submit" OnClick="butItemMasterSubmit_Click" />

                                                    <asp:Button ID="btnshowrptItems" Text="Report" runat="server" CssClass="btn btn-outline-info"
                                                        TabIndex="13" ToolTip="Click here to Show Report" OnClick="btnshowrptItems_Click" />
                                                    <asp:Button ID="btnRport" runat="server" Text="Item Wise Summary Report" TabIndex="14"
                                                        CssClass="btn btn-outline-info" OnClick="btnRport_Click" ToolTip="Click here to Show Item Wise Summary Report" />
                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="store"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    <asp:Button ID="butItemMasterCancel" Text="Cancel" runat="server" CssClass="btn btn-outline-danger"
                                                        OnClick="butItemMasterCancel_Click" TabIndex="12" ToolTip="CLick here to Reset" />
                                                </div>

                                                <div class="col-12 mt-3">
                                                    <asp:Panel ID="pnlItemMaster" runat="server">
                                                        <asp:ListView ID="lvItemMaster" runat="server">
                                                            <EmptyDataTemplate>
                                                                <p class="text-center text-bold">
                                                                    <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                                </p>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <div class="sub-heading">
                                                                        <h5>Item Master</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Action
                                                                                </th>
                                                                                <th>Group Name
                                                                                </th>
                                                                                <th>Sub Group Name
                                                                                </th>
                                                                                <th>Item Name
                                                                                </th>
                                                                                <th id="thunit" runat="server" visible="false">Unit
                                                                                </th>
                                                                                <th>MinQty
                                                                                </th>
                                                                                <th>MaxQty
                                                                                </th>
                                                                                <th>OpBalQty
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
                                                                        <asp:ImageButton ID="btnEditItemMaster" runat="server" AlternateText="Edit Record"
                                                                            CommandArgument='<%# Eval("ITEM_NO") %>' ImageUrl="~/images/edit.png" OnClick="btnEditItemMaster_Click"
                                                                            ToolTip="Edit Record" TabIndex="15" />
                                                                        &#160;
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("MIGNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("MISGNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ITEM_NAME")%>
                                                                    </td>
                                                                    <td id="tdunit" runat="server" visible="false">
                                                                        <%# Eval("ITEM_UNIT")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ITEM_MIN_QTY")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ITEM_MAX_QTY")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ITEM_OB_QTY")%>
                                                                    </td>
                                                                    <%-- <td>
                                                                            <%# Eval("ITEM_OB_VALUE")%>
                                                                         </td>--%>
                                                                    <%--<td>
                                                                             <%# Eval("APPROVAL")%>
                                                                         </td>--%>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                    <%-- <div class="vista-grid_datapager">
                                                        <div class="text-center">
                                                            <asp:DataPager ID="dpPagerItemMaster" runat="server" OnPreRender="dpPagerItemMaster_PreRender"
                                                                PagedControlID="lvItemMaster" PageSize="10">
                                                                <Fields>
                                                                    <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="&lt;&lt;" PreviousPageText="&lt;"
                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowLastPageButton="false"
                                                                        ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                                                    <asp:NumericPagerField ButtonCount="7" ButtonType="Link" CurrentPageLabelCssClass="current" />
                                                                    <asp:NextPreviousPagerField ButtonType="Link" LastPageText="&gt;&gt;" NextPageText="&gt;"
                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowLastPageButton="true"
                                                                        ShowNextPageButton="true" ShowPreviousPageButton="false" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>

                                <div class="tab-pane" id="tab_2">
                                    <div class="box-body">
                                        <%--<div>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatePanel2"
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
                                        </div>--%>
                                        <asp:UpdatePanel ID="updatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Item Sub Group</h5>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Item Group Name</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlItemGroupName" AppendDataBoundItems="true" runat="server"
                                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select Item Group Name" TabIndex="16">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlItemGroupName" runat="server" ControlToValidate="ddlItemGroupName"
                                                                Display="None" ErrorMessage="Please Select Item Group Name" ValidationGroup="storesub"
                                                                InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Item Sub Group Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtItemSubGroupName" MaxLength="100" runat="server" CssClass="form-control"
                                                                onKeyUp="LowercaseToUppercase(this);" TabIndex="17"
                                                                ToolTip="Enter Item Sub Group Name"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtItemSubGroupName" runat="server" ControlToValidate="txtItemSubGroupName"
                                                                Display="None" ErrorMessage="Please Enter Item Sub Group Name" ValidationGroup="storesub"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator runat="server" ID="regtxtItemSubGroupName" Display="None"
                                                                ValidationExpression="^[a-z|A-Z|]+[a-z|A-Z|0-9|\s]*" ControlToValidate="txtItemSubGroupName"
                                                                ErrorMessage="Enterd Only Alphanumeric Characters For Sub Group Name " ValidationGroup="storesub">
                                                            </asp:RegularExpressionValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Short Name/Code</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSubShortname" runat="server" CssClass="form-control" MaxLength="100"
                                                                onKeyUp="LowercaseToUppercase(this);" TabIndex="18"
                                                                ToolTip="Enter Short Name/Code"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtSubShortname" runat="server" ControlToValidate="txtSubShortname"
                                                                Display="None" ErrorMessage="Please Enter Item Sub Group Short Name" ValidationGroup="storesub"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" Display="None"
                                                                ValidationGroup="storesub" ValidationExpression="^[a-z|A-Z|]+[a-z|A-Z|0-9|\s]*"
                                                                ControlToValidate="txtSubShortname" ErrorMessage="Enterd Only Alphanumeric Characters Short sub Group Name ">
                                                            </asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="butSubItemSubmit" runat="server" OnClick="butSubItemSubmit_Click" TabIndex="19"
                                                        Text="Submit" ValidationGroup="storesub" CssClass="btn btn-outline-primary" ToolTip="Click here to Submit" />
                                                    <asp:Button ID="btnshowsubgrprpt" Text="Report" runat="server" CssClass="btn btn-outline-info"
                                                        ToolTip="Click here to Show Report" TabIndex="21" OnClick="btnshowsubgrprpt_Click" />
                                                    <asp:Button ID="butSubItemcancel" Text="Cancel" runat="server" TabIndex="20"
                                                        OnClick="butSubItemcancel_Click" CssClass="btn btn-outline-danger" ToolTip="CLick here to Reset" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="storesub" />
                                                </div>


                                                <div class="col-12 mt-3">
                                                    <asp:Panel ID="pnlItemSubGroupMaster" runat="server">
                                                        <asp:ListView ID="lvItemSubGroupMaster" runat="server">
                                                            <EmptyDataTemplate>
                                                                <p class="text-center text-bold">
                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                </p>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <div class="sub-heading">
                                                                        <h5>Item Sub Group Master</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Action
                                                                                </th>
                                                                                <th>Group Name
                                                                                </th>
                                                                                <th>Sub Group Name
                                                                                </th>
                                                                                <th>Short Name
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
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png"
                                                                            CommandArgument='<%# Eval("MISGNO") %>' OnClick="btnEditSubGroup_Click"
                                                                            AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="22" />&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("MIGNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("MISGNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SNAME")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                    <%-- <div class="vista-grid_datapager">
                                                        <div class="text-center">
                                                            <asp:DataPager ID="dpPagerSubGroupMaster" runat="server" PagedControlID="lvItemSubGroupMaster"
                                                                PageSize="10" OnPreRender="dpPagerSubGroupMaster_PreRender">
                                                                <Fields>
                                                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="tab-pane" id="tab_3">
                                    <div class="box-body">
                                        <%--<div>
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updatePanel3"
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
                                        </div>--%>
                                        <asp:UpdatePanel ID="updatePanel3" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12">
                                                    <div class="sub-heading"><h5>Item Main Group</h5></div>
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Item Group Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtItemGroupName" runat="server" CssClass="form-control" MaxLength="100"
                                                                onKeyUp="LowercaseToUppercase(this);" TabIndex="23"
                                                                ToolTip="Enter Item Group Name"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtItemGroupName" runat="server" ControlToValidate="txtItemGroupName"
                                                                Display="None" ErrorMessage="Please Enter Item Group Name" ValidationGroup="storegrp"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator runat="server" ID="regtxtItemGroupName" Display="None"
                                                                ValidationGroup="storegrp" ValidationExpression="^[a-z|A-Z|]+[a-z|A-Z|0-9|\s]*"
                                                                ControlToValidate="txtItemGroupName"
                                                                ErrorMessage="Enterd Only Alphanumeric Characters For  Group Name ">
                                                            </asp:RegularExpressionValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Short Name/Code</label>
                                                            </div>
                                                            <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control" TabIndex="24"
                                                                MaxLength="25" onKeyUp="LowercaseToUppercase(this);" ToolTip="Enter Short Name/Code">
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtShortName" runat="server" ControlToValidate="txtShortName"
                                                                Display="None" ErrorMessage="Please Enter Item Group Short Name" ValidationGroup="storegrp"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="butGroupSubmit" ValidationGroup="storegrp" Text="Submit" runat="server" TabIndex="25"
                                                        CssClass=" btn btn-outline-primary" OnClick="butGroupSubmit_Click" ToolTip="Click here to Submit" />
                                                    <asp:Button ID="butGroupCancel" Text="Cancel" runat="server" CssClass="btn btn-outline-danger"
                                                        OnClick="butGroupCancel_Click" ToolTip="Click here to Reset" TabIndex="26" />

                                                    <asp:Button ID="btnshorptitemgrp" Text="Report" runat="server" CssClass="btn btn-outline-info"
                                                        ToolTip="Click here to Show Report" TabIndex="27" OnClick="btnshorptitemgrp_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="storegrp"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                                </div>

                                                <div class="col-12 mt-3">
                                                    <asp:Panel ID="pnlItemGroupMaster" runat="server">
                                                        <asp:ListView ID="lvItemGroupMaster" runat="server">
                                                            <EmptyDataTemplate>
                                                                <p class="text-center text-bold">
                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                </p>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <div class="sub-heading">
                                                                        <h5>Item Group Master</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Action
                                                                                </th>
                                                                                <th>Group Name
                                                                                </th>
                                                                                <th>Short Name
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
                                                                        <asp:ImageButton ID="btnEditGroup" runat="server" ImageUrl="~/images/edit.png"
                                                                            CommandArgument='<%# Eval("MIGNO") %>' OnClick="btnEditGroup_Click"
                                                                            AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="28" />&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("MIGNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SNAME")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                    <div class="vista-grid_datapager d-none">
                                                        <div class="text-center">
                                                            <asp:DataPager ID="dpPagerGroupMaster" runat="server" PagedControlID="lvItemGroupMaster"
                                                                PageSize="10" OnPreRender="dpPagerGroupMaster_PreRender">
                                                                <Fields>
                                                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function LowercaseToUppercase(txt) {
            var textValue = txt.value;
            txt.value = textValue.toUpperCase();
        }
    </script>
</asp:Content>

