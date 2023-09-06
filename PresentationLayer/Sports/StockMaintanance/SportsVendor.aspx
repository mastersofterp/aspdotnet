<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SportsVendor.aspx.cs" Inherits="Sports_StockMaintanance_SportsVendor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">VENDOR MASTER</h3>
                    <p class="text-center">
                    </p>
                    <div class="box-tools pull-right">
                    </div>
                </div>
                <!-- Custom Tabs -->
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                        <li class="nav-item"><a class="nav-link active" href="#Div1" data-toggle="tab" aria-expanded="true">Vendor</a></li>
                        <li class="nav-item"><a class="nav-link" href="#Div3" data-toggle="tab" aria-expanded="false">Vendor Category</a></li>

                    </ul>


                    <div class="tab-content">
                        <div class="tab-pane active" id="Div1">
                            <asp:UpdatePanel ID="updatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="col-12">
                                            <%-- <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>--%>
                                            <div class="panel panel-info">

                                                <%-- <div class="panel-heading">Add/Edit Vendor Master</div>--%>
                                                <div class="panel-body">
                                                    <asp:Panel ID="pnl" runat="server">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div4" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Category  : </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" AppendDataBoundItems="true"
                                                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" TabIndex="1" CssClass="form-control" data-select2-enable="true" ToolTip="Enter Category">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlCategory" runat="server" ControlToValidate="ddlCategory"
                                                                    Display="None" ErrorMessage="Please Select Category" InitialValue="0" SetFocusOnError="True" ValidationGroup="store"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Vendor Code :</label>
                                                                </div>
                                                                <asp:TextBox ID="txtVendorCode" runat="server" Enabled="false" MaxLength="20" TabIndex="2" CssClass="form-control" ToolTip="Enter Vendor Code"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvtxtVendorCode" runat="server" ControlToValidate="txtVendorCode" Display="None"
                                                                    ErrorMessage="Please Enter Vendor Code" ValidationGroup="store" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div6" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Vendor Name :</label>
                                                                </div>
                                                                <asp:TextBox ID="txtVendorName" runat="server" MaxLength="100" onkeyup="LowercaseToUppercase(this);" TabIndex="3" CssClass="form-control" ToolTip="Enter Vendor Name"></asp:TextBox><asp:RequiredFieldValidator
                                                                    ID="rfvtxtVendorName" runat="server" ControlToValidate="txtVendorName" Display="None"
                                                                    ErrorMessage="Please Enter Vendor Name" ValidationGroup="store" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="txtNameFilter" runat="server" FilterType="LowercaseLetters, UppercaseLetters,custom"
                                                                    FilterMode="ValidChars" ValidChars="&  " TargetControlID="txtVendorName">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div9" runat="server">
                                                                <div class="label-dynamic">
                                                                    <label>Address:</label>
                                                                </div>
                                                                <asp:TextBox ID="txtAddress" runat="server" MaxLength="200" onKeyup="LowercaseToUppercase(this);" TabIndex="4" CssClass="form-control" ToolTip="Enter Address"></asp:TextBox>
                                                            </div>
                                                        </div>


                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div10" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>City : </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCity" AppendDataBoundItems="true" runat="server" TabIndex="5" CssClass="form-control" data-select2-enable="true" ToolTip="Select City">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlCity" runat="server" ControlToValidate="ddlCity"
                                                                    Display="None" ErrorMessage="Please Select City" ValidationGroup="store" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div11" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>State:</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlState" AppendDataBoundItems="true" runat="server" TabIndex="6" CssClass="form-control" data-select2-enable="true" ToolTip="Select State">
                                                                </asp:DropDownList>
                                                            </div>


                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div5" runat="server">
                                                                <div class="label-dynamic">


                                                                    <label>Phone No.:</label>
                                                                </div>
                                                                <asp:TextBox ID="txtPhone" runat="server" MaxLength="10" TabIndex="8" CssClass="form-control" ToolTip="Enter Phone No."></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="txtPhonenoFilte" runat="server" ValidChars="0123456789"
                                                                    FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtPhone">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                <%--  Shaikh Juned (07-04-2022) --%>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                                    ControlToValidate="txtPhone" Display="None" SetFocusOnError="True" ValidationGroup="store" ErrorMessage="Please enter 10 digits Phone Number"
                                                                    ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div12" runat="server">
                                                                <div class="label-dynamic">
                                                                    <label>Email:</label>
                                                                </div>
                                                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" TabIndex="9" CssClass="form-control" ToolTip="Enter EmailId"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="regfv" runat="server" ControlToValidate="txtEmail"
                                                                    SetFocusOnError="True" Display="None" ValidationGroup="store" ErrorMessage="Email Id is not valid"
                                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div13" runat="server">
                                                                <div class="label-dynamic">
                                                                    <label>C.S.T:</label>
                                                                </div>
                                                                <asp:TextBox ID="txtCST" runat="server" MaxLength="10" TabIndex="10" CssClass="form-control" ToolTip="Enter C.S.T."></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCST" runat="server" ValidChars="/\-"
                                                                    FilterType="Custom,LowercaseLetters, UppercaseLetters, Numbers" FilterMode="ValidChars" TargetControlID="txtCST">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div14" runat="server">
                                                                <div class="label-dynamic">
                                                                    <label>B.S.T:</label>
                                                                </div>
                                                                <asp:TextBox ID="txtBST" runat="server" MaxLength="10" TabIndex="11" CssClass="form-control" ToolTip="Enter B.S.T."></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="/\-"
                                                                    FilterType="Custom,LowercaseLetters, UppercaseLetters, Numbers" FilterMode="ValidChars" TargetControlID="txtBST">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div15" runat="server">
                                                                <div class="label-dynamic">
                                                                    <label>G.S.T%:</label>
                                                                </div>
                                                                <asp:TextBox ID="txtGST" runat="server" MaxLength="10" TabIndex="12" CssClass="form-control" ToolTip="Enter  G.S.T."></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" ValidChars="/\-"
                                                                    FilterType="Custom,LowercaseLetters, UppercaseLetters, Numbers" FilterMode="ValidChars" TargetControlID="txtBST">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div16" runat="server">
                                                                <div class="label-dynamic">
                                                                    <label>PAN No:</label>
                                                                </div>
                                                                <asp:TextBox ID="txtPanNumber" runat="server" TabIndex="13" MaxLength="10" Style="text-transform: uppercase;" CssClass="form-control" ToolTip="Enter PAN No."></asp:TextBox></td>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtPanNumber" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers"
                                                                                ValidChars=" ">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>

                                                                <%--  Shaikh Juned (07-04-2022) --%>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                                    ControlToValidate="txtPanNumber" Display="None" SetFocusOnError="True" ValidationGroup="store" ErrorMessage="Invalid PAN Number/Pleas Enter Valid PAN Number"
                                                                    ValidationExpression="^([A-Za-z]){5}([0-9]){4}([A-Za-z]){1}$"></asp:RegularExpressionValidator>


                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div17" runat="server">
                                                                <div class="label-dynamic">
                                                                    <label>Remark:</label>
                                                                </div>
                                                                <asp:TextBox ID="txtRemark" runat="server" MaxLength="250" TabIndex="7" CssClass="form-control" ToolTip="Enter Remark"></asp:TextBox>

                                                            </div>
                                                        </div>


                                                    </asp:Panel>
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="btnParty" runat="server" OnClick="btnParty_Click" Text="Submit" ValidationGroup="store" TabIndex="14" CssClass="btn btn-primary" ToolTip="Click To Submit" />
                                                        <asp:Button ID="btnPartyCancel" runat="server" OnClick="btnPartyCancel_Click" Text="Cancel" TabIndex="15" ToolTip="Click To Reset" CssClass="btn btn-warning" />
                                                        <asp:Button ID="btnshowrpt" runat="server" Text="Report" CssClass="btn btn-info" ToolTip="Click To Show Report" OnClick="btnshowrpt_Click" TabIndex="16" />

                                                        <%--  Shaikh Juned (07-04-2022) --%>
                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="store" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlItemMaster" runat="server">
                                                <div class="col-12">
                                                    <asp:ListView ID="lvParty" runat="server">
                                                        <EmptyDataTemplate>
                                                            <div class="text-center">
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="" /><%--No Records Found--%>
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>VENDOR ENTRY LIST</h5>
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

                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="btnEditParty" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PNO") %>'
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditParty_Click" TabIndex="17" />
                                                                    &nbsp;&nbsp;
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
                                                    <div class="vista-grid_datapager text-center d-none">
                                                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvParty" PageSize="10"
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
                                    </div>

                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>

                        <div class="tab-pane" id="Div3">
                            <asp:UpdatePanel ID="updatePanel3" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="form-group col-md-12">
                                            <%--  <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>--%>
                                            <div class="panel panel-info">

                                                <%-- <div class="panel-heading">Add/Edit Vendor Category</div>--%>
                                                <div class="panel-body">
                                                    <div class="col-12">

                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div7" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Category Name :</label>
                                                                </div>
                                                                <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" ToolTip="Enter Category Name" MaxLength="100" onkeyup="LowercaseToUppercase(this);" TabIndex="18"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCategoryName" runat="server" TargetControlID="txtCategoryName"
                                                                    ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" FilterMode="ValidChars">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="rfvtxtCategoryName" runat="server" ControlToValidate="txtCategoryName"
                                                                    Display="None" ErrorMessage="Please Enter Category Name" ValidationGroup="store1"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div8" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Category Short Name : </label>
                                                                </div>
                                                                <asp:TextBox ID="txtCategoryShortName" runat="server" CssClass="form-control" ToolTip="Enter Short Name" MaxLength="100" TabIndex="19" onKeyup="LowercaseToUppercase(this);"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCatagoryShort" runat="server" TargetControlID="txtCategoryShortName"
                                                                    ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789" FilterMode="ValidChars">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCategoryShortName"
                                                                    Display="None" ErrorMessage="Please Enter Category Short Name" ValidationGroup="store1" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>

                                                        </div>
                                                        <div class="form-group col-md-6">
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="btnCategoryName" Text="Submit" ValidationGroup="store1" runat="server"
                                                            CssClass="btn btn-primary" ToolTip="Click To Submit" OnClick="btnCategoryName_Click" TabIndex="20" />&nbsp;
                                                                <asp:Button ID="btnPartyCategoryCancel" Text="Cancel" runat="server" CssClass="btn btn-warning" ToolTip="Click To Reset" OnClick="btnPartyCategoryCancel_Click" TabIndex="21" />
                                                        &nbsp;<asp:Button ID="btnCatReport" Text="Report" runat="server" CssClass="btn btn-info" ToolTip="Click To Show Report" OnClick="btnCatReport_Click" TabIndex="22" />
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="store1" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </div>
                                                </div>
                                            </div>
                                            <%--  Modified by Saahil Trivedi 24-02-2022--%>
                                            <asp:Panel ID="pnlCategoryName" runat="server">
                                                <div class="col-12">
                                                    <asp:ListView ID="lvCategoryName" runat="server">
                                                        <EmptyDataTemplate>

                                                            <div class="text-center">
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="" /><%--No Records Found--%>
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>

                                                            <div class="sub-heading">
                                                                <h5>VENDOR CATEGORY ENTRY LIST</h5>
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

                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="btnEditPartyCategory" runat="server" ImageUrl="~/Images/edit.png"
                                                                        CommandArgument='<%#Eval("PCNO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                        OnClick="btnEditPartyCategory_Click" TabIndex="23" />&nbsp;
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
                                                    <div class="vista-grid_datapager text-center">
                                                        <asp:DataPager ID="DataPager2" runat="server" PagedControlID="lvCategoryName"
                                                            PageSize="10" OnPreRender="dpPagerCategoryName_PreRender">
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
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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

