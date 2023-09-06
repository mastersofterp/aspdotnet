<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PartyCategoryMaster.aspx.cs" Inherits="Health_StockMaintenance_PartyCategoryMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>--%>


    <div id="temp" runat="server" visible="true">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">VENDOR MASTER</h3>
                    </div>
                    <div class="box-body">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">VENDOR</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">VENDOR CATEGORY</a>
                                </li>
                            </ul>

                            <div class="tab-content" id="my-tab-content">

                                <div class="tab-pane active" role="tabpanel" id="tab_1">
                                    <div class="col-12">
                                       <%-- <div>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatePanel1"
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
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Category</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" TabIndex="1"
                                                                AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Category"
                                                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlCategory" runat="server" ControlToValidate="ddlCategory"
                                                                Display="None" ErrorMessage="Please Select Category" InitialValue="0" SetFocusOnError="True"
                                                                ValidationGroup="store"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Vendor Code</label>
                                                            </div>
                                                            <asp:TextBox ID="txtVendorCode" runat="server" Enabled="false" CssClass="form-control"
                                                                MaxLength="20" ToolTip="Vendor Code" TabIndex="2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtVendorCode" runat="server" ControlToValidate="txtVendorCode"
                                                                Display="None" ErrorMessage="Please Enter Vendor Code" ValidationGroup="store"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Vendor Name </label>
                                                            </div>
                                                            <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control" MaxLength="100"
                                                                onkeyup="LowercaseToUppercase(this);" ToolTip="Enter Vendor Name" TabIndex="3">
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtVendorName" runat="server" ControlToValidate="txtVendorName"
                                                                Display="None" ErrorMessage="Please Enter Vendor Name" ValidationGroup="store"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtNameFilter" runat="server"
                                                                FilterType="LowercaseLetters, UppercaseLetters,custom"
                                                                FilterMode="ValidChars" ValidChars=" " TargetControlID="txtVendorName">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Phone</label>
                                                            </div>
                                                            <asp:TextBox ID="txtPhone" runat="server" MaxLength="50" CssClass="form-control"
                                                                ToolTip="Enter Phone Number" TabIndex="4"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtPhonenoFilte" runat="server" ValidChars="0123456789-+"
                                                                FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtPhone">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Address</label>
                                                            </div>
                                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" ToolTip="Enter Address"
                                                                MaxLength="200" onKeyup="LowercaseToUppercase(this);" TabIndex="5"></asp:TextBox>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>City</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCity" AppendDataBoundItems="true" runat="server"
                                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select City" TabIndex="7">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlCity" runat="server" ControlToValidate="ddlCity"
                                                                Display="None" ErrorMessage="Please Select City" ValidationGroup="store" InitialValue="0"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divEmail" runat="server" visible="true">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Email</label>
                                                            </div>
                                                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" CssClass="form-control"
                                                                ToolTip="Enter Email" TabIndex="6"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="regfv" runat="server" ControlToValidate="txtEmail"
                                                                SetFocusOnError="True" Display="None" ValidationGroup="store" ErrorMessage="Email Id is not valid"
                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divCity" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>C.S.T</label>
                                                            </div>
                                                            <asp:TextBox ID="txtCST" runat="server" MaxLength="10" CssClass="form-control"
                                                                ToolTip="Enter C.S.T" TabIndex="8"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCST" runat="server" ValidChars="/\-"
                                                                FilterType="Custom,LowercaseLetters, UppercaseLetters, Numbers" FilterMode="ValidChars"
                                                                TargetControlID="txtCST">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>State</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlState" AppendDataBoundItems="true" runat="server"
                                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select State" TabIndex="9">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBST" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>B.S.T</label>
                                                            </div>
                                                            <asp:TextBox ID="txtBST" runat="server" MaxLength="10" CssClass="form-control"
                                                                ToolTip="Enter B.S.T" TabIndex="10"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="/\-"
                                                                FilterType="Custom,LowercaseLetters, UppercaseLetters, Numbers"
                                                                FilterMode="ValidChars" TargetControlID="txtBST">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divRem" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Remark</label>
                                                            </div>
                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control"
                                                                MaxLength="200" ToolTip="Enter Remark If Any" TabIndex="11"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnParty" runat="server" OnClick="btnParty_Click" Text="Submit" ValidationGroup="store"
                                                        CssClass="btn btn-outline-primary" ToolTip="Click here to Submit" TabIndex="12" />
                                                    <asp:Button ID="btnPartyCancel" runat="server" OnClick="btnPartyCancel_Click" Text="Cancel"
                                                        CssClass="btn btn-outline-danger" ToolTip="CLick here to Reset" TabIndex="13" />
                                                    <asp:Button ID="btnshowrpt" runat="server" Text="Report" CssClass="btn btn-outline-info"
                                                        ToolTip="Click here to Show Report" TabIndex="14" OnClick="btnshowrpt_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="store" />

                                                </div>

                                                <div class="col-12 mt-3">
                                                    <asp:Panel ID="pnlItemMaster" runat="server">
                                                        <asp:ListView ID="lvParty" runat="server">
                                                            <EmptyDataTemplate>
                                                                <p class="text-center text-bold">
                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                </p>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <div class="sub-heading">
                                                                        <h5>Vendor</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Action
                                                                                </th>
                                                                                <th>Code
                                                                                </th>
                                                                                <th>Vendor Name
                                                                                </th>
                                                                                <th>Category
                                                                                </th>
                                                                                <th>Phone
                                                                                </th>
                                                                                <th>Email
                                                                                </th>
                                                                                <th>Address
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
                                                                        <asp:ImageButton ID="btnEditParty" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("PNO") %>'
                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditParty_Click" />

                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("PCODE")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("PNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("CATEGORY")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Phone")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Email")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Address")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <div class="vista-grid_datapage d-none">
                                                            <div class="text-center">
                                                                <asp:DataPager ID="dpPagerItemMaster" runat="server" PagedControlID="lvParty" PageSize="10"
                                                                    OnPreRender="dpPagerParty_PreRender">
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


                                <div class="tab-pane" role="tabpanel" id="tab_2">
                                    <div class="col-12">
                                       <%-- <div>
                                            <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updatePanel3"
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
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Category Name </label>
                                                            </div>
                                                            <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" MaxLength="100"
                                                                onkeyup="LowercaseToUppercase(this);" ToolTip="Enter Category Name" TabIndex="15"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCategoryName" runat="server" TargetControlID="txtCategoryName"
                                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " FilterMode="ValidChars">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvtxtCategoryName" runat="server" ControlToValidate="txtCategoryName"
                                                                Display="None" ErrorMessage="Please Enter Category Name" ValidationGroup="store1"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Category Short Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtCategoryShortName" runat="server" CssClass="form-control" MaxLength="100"
                                                                onKeyup="LowercaseToUppercase(this);" ToolTip="Enter Category Short Name" TabIndex="16"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCatagoryShort" runat="server"
                                                                TargetControlID="txtCategoryShortName"
                                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 " FilterMode="ValidChars">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCategoryName"
                                                                Display="None" ErrorMessage="Please Enter Category Short Name" ValidationGroup="store1"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnCategoryName" Text="Submit" ValidationGroup="store1" runat="server" TabIndex="17"
                                                        CssClass="btn btn-outline-primary" ToolTip="Clcik here to Submit" OnClick="btnCategoryName_Click" />
                                                    <asp:Button ID="btnshowrpt1" Text="Report" runat="server" CssClass="btn btn-outline-info"
                                                         OnClick="btnshowrpt1_Click" ToolTip="Click here to Show Report" TabIndex="19" />
                                                    <asp:Button ID="btnPartyCategoryCancel" Text="Cancel" runat="server" CssClass="btn btn-outline-danger"
                                                        OnClick="btnPartyCategoryCancel_Click" ToolTip="Click here to Reset" TabIndex="18" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="store1"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                                </div>

                                                <div class="col-12 mt-3">
                                                    <asp:Panel ID="pnlCategoryName" runat="server">
                                                        <asp:ListView ID="lvCategoryName" runat="server">
                                                            <EmptyDataTemplate>
                                                                <p class="text-center text-bold">
                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                </p>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <div class="sub-heading">
                                                                        <h5>Vendor Category</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Action
                                                                                </th>
                                                                                <th>Category Name
                                                                                </th>
                                                                                <th>Category Short Name
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
                                                                        <asp:ImageButton ID="btnEditPartyCategory" runat="server" ImageUrl="~/images/edit.png"
                                                                            CommandArgument='<%#Eval("PCNO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                            OnClick="btnEditPartyCategory_Click" />&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("PCNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("PCSHORTNAME") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <div class="vista-grid_datapage d-none">
                                                            <div class="text-center">
                                                                <asp:DataPager ID="dpVendorCategory" runat="server" PagedControlID="lvCategoryName" PageSize="10"
                                                                      OnPreRender="dpVendorCategory_PreRender">
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
    </div>
    <script type="text/javascript">
        function LowercaseToUppercase(txt) {
            var textValue = txt.value;
            txt.value = textValue.toUpperCase();

        }
    </script>
        <div id="divMsg" runat="server">
    </div>
</asp:Content>

