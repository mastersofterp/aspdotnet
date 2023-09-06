<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ItemMaster.aspx.cs" Inherits="Sports_StockMaintanance_ItemMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
    <%--<asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div7" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ITEM MASTER</h3>
                </div>

                <div class="box-body">
                    <div id="tabs" role="tabpanel">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs" role="tablist">

                                <li class="nav-item"><a class="nav-link active" href="#ItmMas" data-toggle="tab" role="tab" aria-expanded="true">Item Master</a></li>
                                <li class="nav-item"><a class="nav-link" href="#SubbGrp" data-toggle="tab" role="tab" aria-expanded="true">Item Sub Group Master</a></li>
                                <li class="nav-item"><a class="nav-link" id="A1" href="#MaiGrp" data-toggle="tab" role="tab" aria-expanded="false" runat="server" visible="true">Item Group Master</a></li>


                            </ul>

                            <div class="tab-content">
                                <div class="tab-pane active" role="tabpanel" id="ItmMas">
                                    <asp:UpdatePanel ID="updatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="box-body">
                                                <div class="col-12">
                                                    <asp:Panel ID="pnl" runat="server">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div4" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Item Sub Group</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlItemSubGroup" AppendDataBoundItems="true" runat="server"
                                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Select Item Sub Group" TabIndex="1">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlItemSubGroup" runat="server" ControlToValidate="ddlItemSubGroup"
                                                                    Display="None" ErrorMessage="Please Select Item Sub Group" ValidationGroup="store"
                                                                    InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div5" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Item Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" ToolTip="Enter Item Name" MaxLength="100" TabIndex="2"
                                                                    onKeyUp="LowercaseToUppercase(this)"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtName" runat="server" TargetControlID="txtItemName"
                                                                    FilterType="Custom, Numbers, LowercaseLetters, UppercaseLetters" FilterMode="ValidChars" ValidChars="- ">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="rfvtxtItemName" runat="server" ControlToValidate="txtItemName"
                                                                    Display="None" ErrorMessage="Please Enter Item Name" ValidationGroup="store"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div6" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Item Code</label>
                                                                </div>
                                                                <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control" ToolTip="Enter Item Code" MaxLength="30" TabIndex="3"
                                                                    onKeyUp="LowercaseToUppercase(this)"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeItemCode" runat="server" TargetControlID="txtItemCode"
                                                                    FilterType="Custom, Numbers, LowercaseLetters, UppercaseLetters" FilterMode="ValidChars" ValidChars="/">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="rfvtxtItemCode" runat="server" ControlToValidate="txtItemCode"
                                                                    Display="None" ErrorMessage="Please Enter Item Code" ValidationGroup="store"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">

                                                                    <label>Re-order Level</label>
                                                                </div>
                                                                <asp:TextBox ID="txtReorderLevel" runat="server" CssClass="form-control" ToolTip="Enter Re-Order Level" MaxLength="5" TabIndex="4"
                                                                    Text="0"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="txtReorderLevelFilter" runat="server" FilterType="Numbers,custom"
                                                                    FilterMode="ValidChars" TargetControlID="txtReorderLevel" ValidChars=".">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">

                                                                    <label>Remark</label>
                                                                </div>
                                                                <asp:TextBox ID="txtCommonDescriptionOfItem" runat="server" CssClass="form-control" ToolTip="Enter Remark" MaxLength="2000"
                                                                    TabIndex="5" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">

                                                                    <label>Max Quantity</label>
                                                                </div>
                                                                <asp:TextBox ID="txtMaxQuantity" runat="server" CssClass="form-control" ToolTip="Enter Max Quantity" MaxLength="10" TabIndex="6"
                                                                    Text="0"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="txtMaxQuantityFilter" runat="server" FilterType="Numbers,custom"
                                                                    FilterMode="ValidChars" TargetControlID="txtMaxQuantity" ValidChars=".">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">

                                                                    <label>Min Quantity</label>
                                                                </div>
                                                                <asp:TextBox ID="txtMinQuantity" runat="server" CssClass="form-control" ToolTip="Enter Min Quantity" MaxLength="10" TabIndex="7"
                                                                    Text="0"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="txtMinQuantityFilter" runat="server" FilterType="Numbers,custom"
                                                                    FilterMode="ValidChars" TargetControlID="txtMinQuantity" ValidChars=".">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">

                                                                    <label>Ope.Bal Quantity</label>
                                                                </div>
                                                                <asp:TextBox ID="txtOpeningBalanceQuantity" runat="server" CssClass="form-control" ToolTip="Enter Opening Balance" Text="0"
                                                                    MaxLength="10" TabIndex="8"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="txtOpBalQtyFilter" runat="server" FilterType="Numbers,custom"
                                                                    FilterMode="ValidChars" TargetControlID="txtOpeningBalanceQuantity" ValidChars=".">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>

                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="butItemMasterSubmit" ValidationGroup="store" Text="Submit" runat="server"
                                                        CssClass="btn btn-primary" ToolTip="Click To Submit" OnClick="butItemMasterSubmit_Click" TabIndex="9" />
                                                    <asp:Button ID="btnshowrptItems" Text="Report" runat="server" CssClass="btn btn-info" ToolTip="Click To Show Summary Report" TabIndex="11" OnClick="btnshowrptItems_Click" />
                                                    <asp:Button ID="btnRport" runat="server" Text="Item Wise Summary Report" TabIndex="12" CssClass="btn btn-info" ToolTip="Click To Show Report" OnClick="btnRport_Click" />
                                                    <asp:Button ID="butItemMasterCancel" Text="Cancel" runat="server" ToolTip="Click To Reset" CssClass="btn btn-warning"
                                                        OnClick="butItemMasterCancel_Click" TabIndex="10" />

                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="store" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                                <asp:Panel ID="pnlItemMaster" runat="server">
                                                    <div class="col-12">
                                                        <asp:ListView ID="lvItemMaster" runat="server">
                                                            <EmptyDataTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div class="vista-grid">
                                                                    <div class="sub-heading">
                                                                        <h5>Item Master</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>

                                                                                <th>Action
                                                                                </th>
                                                                                <th>GroupName
                                                                                </th>
                                                                                <th>SubGroupName
                                                                                </th>
                                                                                <th>ItemName
                                                                                </th>
                                                                                <th>MinQty
                                                                                </th>
                                                                                <th>MaxQty
                                                                                </th>
                                                                                <th>OpBalQty
                                                                                </th>
                                                                            </tr>
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
                                                                            ToolTip="Edit Record" TabIndex="13" />
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
                                                                    <td>
                                                                        <%# Eval("ITEM_MIN_QTY")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ITEM_MAX_QTY")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ITEM_OB_QTY")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>

                                                        </asp:ListView>
                                                        <div class="vista-grid_datapager text-center">
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
                                                    </div>
                                                </asp:Panel>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane" role="tabpanel" id="SubbGrp">
                                    <asp:UpdatePanel ID="updatePanel5" runat="server">
                                        <ContentTemplate>
                                            <div class="box-body">
                                                <div class="col-12">
                                                    <asp:Panel ID="Panel4" runat="server">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div8" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Item Group Name</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlItemGroupName" AppendDataBoundItems="true" runat="server"
                                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Select Item Group Name" TabIndex="14">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlItemGroupName" runat="server" ControlToValidate="ddlItemGroupName"
                                                                    Display="None" ErrorMessage="Please Select Item Group Name" ValidationGroup="storesub"
                                                                    InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div9" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Item Sub Group Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtItemSubGroupName" MaxLength="100" runat="server" CssClass="form-control" ToolTip="Enter Item Sub Group Name" onKeyUp="LowercaseToUppercase(this);" TabIndex="15"></asp:TextBox>

                                                                <asp:RequiredFieldValidator ID="rfvtxtItemSubGroupName" runat="server" ControlToValidate="txtItemSubGroupName"
                                                                    Display="None" ErrorMessage="Please Enter Item Sub Group Name" ValidationGroup="storesub" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                                <asp:RegularExpressionValidator runat="server" ID="regtxtItemSubGroupName" Display="None" ValidationExpression="^[a-z|A-Z|]+[a-z|A-Z|0-9|\s]*" ControlToValidate="txtItemSubGroupName"
                                                                    ErrorMessage="Enterd Only Alphanumeric Characters For Sub Group Name " ValidationGroup="storesub"></asp:RegularExpressionValidator>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div10" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Short Name/Code</label>
                                                                </div>
                                                                <%--<asp:TextBox ID="txtSubShortname" runat="server" CssClass="form-control"  ToolTip="Enter Short Name" MaxLength="100" onKeyUp="LowercaseToUppercase(this);" TabIndex="16"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvtxtSubShortname" runat="server" ControlToValidate="txtSubShortname"
                                                                    Display="None" ErrorMessage="Please Enter Item Sub Group Short Name" ValidationGroup="storesub" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" Display="None"
                                                                    ValidationGroup="storesub" ValidationExpression="^[a-z|A-Z|]+[a-z|A-Z|0-9|\s]*"
                                                                    ControlToValidate="txtSubShortname" ErrorMessage="Enterd Only Alphanumeric Characters Short sub Group Name "></asp:RegularExpressionValidator>--%>
                                                                  <asp:TextBox ID="txtSubShortname" runat="server" MaxLength="25" onKeyUp="LowercaseToUppercase(this);" CssClass="form-control" TabIndex="2" ToolTip="Enter Short Name/Code"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubShortname" Display="None" ErrorMessage="Please Enter Item Group Short Name" SetFocusOnError="True" ValidationGroup="storegrp"></asp:RequiredFieldValidator>
                                                    
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubGroupSubmit" ValidationGroup="storesub" Text="Submit" runat="server" CssClass="btn btn-primary" OnClick="btnSubGroupSubmit_Click" ToolTip="Click To Submit" TabIndex="23" />

                                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" ToolTip="Click To Reset" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="24" />

                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="storesub" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </div>
                                                </div>
                                                <asp:Panel ID="pnlItemSubGroupMaster" runat="server">
                                                    <div class="col-12">
                                                        <asp:ListView ID="lvItemSubGroupMaster" runat="server">
                                                            <EmptyDataTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div id="demo-grid" class="vista-grid">
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
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>

                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("MISGNO") %>'
                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditSubGroup_Click" TabIndex="20" />&nbsp;
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
                                                        <div class="vista-grid_datapager text-center">
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
                                                    </div>
                                                </asp:Panel>
                                            </div>


                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane" role="tabpanel" id="MaiGrp">
                                    <asp:UpdatePanel ID="updatePanel6" runat="server">
                                        <ContentTemplate>
                                            <div class="box-body">
                                                <div class="col-12">
                                                    <asp:Panel ID="Panel5" runat="server">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div12" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Item Group Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtItemGroupName" runat="server" CssClass="form-control" ToolTip="Enter Item Group Name" MaxLength="100" onKeyUp="LowercaseToUppercase(this);" TabIndex="21"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvtxtItemGroupName" runat="server" ControlToValidate="txtItemGroupName"
                                                                    Display="None" ErrorMessage="Please Enter Item Group Name" ValidationGroup="storegrp"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator runat="server" ID="regtxtItemGroupName" Display="None"
                                                                    ValidationGroup="storegrp" ValidationExpression="^[a-z|A-Z|]+[a-z|A-Z|0-9|\s]*"
                                                                    ControlToValidate="txtItemGroupName" ErrorMessage="Enterd Only Alphanumeric Characters For  Group Name "></asp:RegularExpressionValidator>

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div13" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Short Name/Code</label>
                                                                </div>
                                                                <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control" ToolTip="Enter Short Name" MaxLength="25" onKeyUp="LowercaseToUppercase(this);" TabIndex="22"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvtxtShortName" runat="server" ControlToValidate="txtShortName"
                                                                    Display="None" ErrorMessage="Please Enter Item Group Short Name" ValidationGroup="storegrp"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="butGroupSubmit" ValidationGroup="storegrp" Text="Submit" runat="server" CssClass="btn btn-primary" ToolTip="Click To Submit" OnClick="butGroupSubmit_Click" TabIndex="23" />
                                                    <asp:Button ID="btnshorptitemgrp" Text="Report" runat="server" CssClass="btn btn-info" ToolTip="Click To Show Report" OnClick="btnshorptitemgrp_Click" TabIndex="25" />
                                                    <asp:Button ID="butGroupCancel" Text="Cancel" runat="server" ToolTip="Click To Reset" CssClass="btn btn-warning" OnClick="butGroupCancel_Click" TabIndex="24" />

                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="storegrp" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>

                                                <asp:Panel ID="pnlItemGroupMaster" runat="server">
                                                    <div class="col-12">
                                                        <asp:ListView ID="lvItemGroupMaster" runat="server">
                                                            <EmptyDataTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div id="demo-grid" class="vista-grid">
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
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>

                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnEditGroup" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("MIGNO") %>'
                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditGroup_Click" TabIndex="26" />&nbsp;
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
                                                        <div class="vista-grid_datapager text-center">
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
                                                </asp:Panel>
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
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <script type="text/javascript">
        function LowercaseToUppercase(txt) {
            var textValue = txt.value;
            txt.value = textValue.toUpperCase();
        }
    </script>
</asp:Content>

