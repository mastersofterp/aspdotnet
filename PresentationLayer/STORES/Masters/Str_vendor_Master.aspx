<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_vendor_Master.aspx.cs" Inherits="Stores_Masters_Str_vendor_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>--%>

        <style>
        #lvBank .dataTables_scrollHeadInner, #ctl00_ContentPlaceHolder1_pnlItemMaster .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .dynamic-nav-tabs li.active a{
            color: #255282;
            background-color: #fff;
            border-top: 3px solid #255282 !important;
            border-color: #255282 #255282 #fff !important;
        }
    </style>

    <%-- <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>--%>
   
    <div class="row">
        <div class="col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">VENDOR MASTER</h3>
                </div>
                <div class="box-body">
                    <div id="tabs" role="tabpanel">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                                <li class="nav-item active" id="divlkAnnouncement" runat="server">
                                    <%--<a class="nav-link active" data-toggle="tab" href="#abc">Vendor</a>--%>
                                    <asp:LinkButton ID="lkAnnouncement" runat="server" OnClick="lkAnnouncement_Click" CssClass="nav-link" TabIndex="1">Vendor</asp:LinkButton>
                                </li>
                                <li class="nav-item" id="divlkFeed" runat="server">
                                    <%--<a class="nav-link" data-toggle="tab" href="#pqr">Vendor Category</a>--%>
                                    <asp:LinkButton ID="lkFeedback" runat="server" OnClick="lkFeedback_Click" CssClass="nav-link" TabIndex="2">Vendor Category</asp:LinkButton>
                                </li>
                            </ul>

                            <div class="tab-content" id="my-tab-content">
                                <%--<div class="tab-pane active" id="abc">--%>
                                <div class="tab-pane fade show active" id="divAnnounce" role="tabpanel" runat="server">
                                    <div>
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
                                    </div>

                                    <asp:UpdatePanel ID="updatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="box-body">
                                                <asp:Panel ID="pnl" runat="server">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-12 col-md-6 col-12 mt-2">
                                                                <div class="sub-heading">
                                                                    <h5>Add/Edit Vendor Master</h5>
                                                                </div>
                                                            </div>


                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="spanCategory" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Category</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCategory" AutoPostBack="true" data-select2-enable="true" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" CssClass="form-control" ToolTip="Enter Category">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlCategory" runat="server" ControlToValidate="ddlCategory"
                                                                    Display="None" ErrorMessage="Please Select Category" InitialValue="0" SetFocusOnError="True"
                                                                    ValidationGroup="store"></asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Vendor Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control"  TabIndex="2" ToolTip="Enter Vendor Name" MaxLength="80" onkeyup="LowercaseToUppercase(this);"></asp:TextBox><asp:RequiredFieldValidator
                                                                    ID="rfvtxtVendorName" runat="server" ControlToValidate="txtVendorName" Display="None"
                                                                    ErrorMessage="Please Enter Vendor Name" ValidationGroup="store" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Address</label>
                                                                </div>
                                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" MaxLength="190" TabIndex="3" ToolTip="Enter Address" onKeyup="LowercaseToUppercase(this);"></asp:TextBox>

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="spanCity" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>City</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCity" AppendDataBoundItems="true" TabIndex="4" runat="server" data-select2-enable="true" CssClass="form-control" ToolTip="Select City">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlCity" runat="server" ControlToValidate="ddlCity"
                                                                    Display="None" ErrorMessage="Please Select City" ValidationGroup="store" InitialValue="0"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>State</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlState" AppendDataBoundItems="true" data-select2-enable="true" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Select State"
                                                                    OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlState"
                                                                    Display="None" ErrorMessage="Please Select State" ValidationGroup="store" InitialValue="0"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="spanCode" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Vendor Code</label>
                                                                </div>
                                                                <asp:TextBox ID="txtVendorCode" runat="server" Enabled="false" TabIndex="6" CssClass="form-control" ToolTip="Enter Vendor Code" MaxLength="16"></asp:TextBox><asp:RequiredFieldValidator
                                                                    ID="rfvtxtVendorCode" runat="server" ControlToValidate="txtVendorCode" Display="None"
                                                                    ErrorMessage="Please Enter Vendor Code" ValidationGroup="store" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Phone</label>
                                                                </div>
                                                                <asp:TextBox ID="txtPhone" runat="server" MaxLength="10" TabIndex="7" ToolTip="Enter Phone" CssClass="form-control"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="txtPhonenoFilte" runat="server" ValidChars="0123456789-+"
                                                                    FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtPhone">
                                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Email</label>
                                                                </div>
                                                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="90" TabIndex="8" ToolTip="Enter Email" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="regfv" runat="server" ControlToValidate="txtEmail"
                                                                    SetFocusOnError="True" Display="None" ValidationGroup="store" ErrorMessage="Email Id is not valid"
                                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>GSTINO</label>
                                                                </div>
                                                                <asp:TextBox ID="txtCST" runat="server" MaxLength="20" TabIndex="9" ToolTip="Enter C.S.T" CssClass="form-control"></asp:TextBox>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Reg No.</label>
                                                                </div>
                                                                <asp:TextBox ID="txtBST" runat="server" MaxLength="10" TabIndex="10" ToolTip="Enter B.S.T" CssClass="form-control"></asp:TextBox>

                                                            </div>

                                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Remark</label>
                                                                </div>
                                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TabIndex="11" onkeyDown="checkTextAreaMaxLength(this,event,'195');" onkeyup="textCounter(this, this.form.remLen, 195);" ToolTip="Enter Remark" TextMode="MultiLine"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-12 col-md-6 col-12">
                                                            <div class="sub-heading">
                                                                <h5>Add Vendor Bank Details</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Bank Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Enter Bank Name" MaxLength="100" onkeyup="LowercaseToUppercase(this); "></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBankName" Display="None"
                                                                ErrorMessage="Please Enter Bank Name" ValidationGroup="Add" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Branch Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtBranchName"  runat="server" CssClass="form-control" TabIndex="2" ToolTip="Enter Branch Name" MaxLength="65" onkeyup="LowercaseToUppercase(this);"></asp:TextBox>     
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBranchName" Display="None"
                                                                ErrorMessage="Please Enter Branch Name" ValidationGroup="Add" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                             <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtBranchName"
                                                                        ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxy z" FilterMode="ValidChars">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>	

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>IFSC Code</label>
                                                            </div>
                                                            <asp:TextBox ID="txtIfscCode" onkeypress="javascript:return validate(event)" runat="server" ToolTip="Enter IFSC Code Name" MaxLength="100" TabIndex="2" CssClass="form-control"></asp:TextBox>

                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" ValidChars="0123456789 A-Z"
                                                                FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtAccNum">
                                                            </ajaxToolKit:FilteredTextBoxExtender>


                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtIfscCode" Display="None"
                                                                ErrorMessage="Please Enter IFSC Code Name" ValidationGroup="Add" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Bank Account Number</label>
                                                            </div>
                                                            <asp:TextBox ID="txtAccNum" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Enter Bank Account Number" MaxLength="100"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAccNum" Display="None"
                                                                ErrorMessage="Please Enter Bank Account Number" ValidationGroup="Add" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789"
                                                                FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtAccNum">
                                                            </ajaxToolKit:FilteredTextBoxExtender>

                                                        </div>




                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnAddBankInfo" runat="server" Text="Add" OnClick="btnAddBankInfo_Click" ValidationGroup="Add"
                                                                CssClass="btn btn-primary" ToolTip="Click To Submit" TabIndex="12" />
                                                            <asp:Button ID="btnCancelBank" runat="server" Text="Cancel" TabIndex="13" OnClick="btnCancelBank_Click"
                                                                ToolTip="Click To Reset" CssClass="btn btn-warning" />
                                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />
                                                        </div>
                                                    </div>
                                                </div>

                                                        <div class="col-12">
                                                            <asp:ListView ID="lvBank" runat="server" Visible="false">
                                                                <LayoutTemplate>
                                                                    <div class="table-responsive">
                                                                    <%-- <div id="demo-grid" class="vista-grid">--%>
                                                                    <div class="sub-heading">
                                                                        <h5>Bank Entry List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>

                                                                                <th>Action
                                                                                </th>
                                                                                <th>Bank Name
                                                                                </th>
                                                                                <th>Branch Name
                                                                                </th>
                                                                                <th>IFSC Code
                                                                                </th>
                                                                                <th>Bank Account Number
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
                                                                            <asp:ImageButton ID="btnDeleteBank" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("SRNO") %>'
                                                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDeleteBank_Click"  /><%--OnClientClick="showConfirmDel(this); return false;"--%>

                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("BANK_NAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("BRANCH_NAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("IFSC_CODE")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("BANK_ACC_NO")%>
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>

                                                            </asp:ListView>
                                                        </div>

                                                <div class="col-12  btn-footer">
                                                    <asp:Button ID="butParty" runat="server" OnClick="butParty_Click" Text="Submit" ValidationGroup="store"
                                                        CssClass="btn btn-primary" ToolTip="Click To Submit" TabIndex="12" />
                                                    
                                                    <asp:Button ID="btnshowrpt" runat="server" Text="Report" CssClass="btn btn-info" ToolTip="Click To Show Report" TabIndex="14" OnClick="btnshowrpt_Click" />
                                                    <asp:Button ID="butPartyCancel" runat="server" OnClick="butPartyCancel_Click" Text="Cancel" TabIndex="13"
                                                        ToolTip="Click To Reset" CssClass="btn btn-warning" />
                                                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List"
                                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="store" />
                                                </div>
                                                <div class="col-12">
                                                    <asp:Panel ID="pnlItemMaster" runat="server">
                                                    
                                                        <asp:ListView ID="lvParty" runat="server">
                                                            <EmptyDataTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Vendor</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
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

                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <%--<div class="tab-pane fade" id="pqr">--%>
                                <div id="divEmoji" runat="server" visible="false" role="tabpanel">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatePanel3"
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
                                    <asp:UpdatePanel ID="updatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="box-body">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Add/Edit Vendor Category</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spancatName" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Category Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtCategoryName" runat="server" TabIndex="1" ToolTip="Enter Category Name" CssClass="form-control" MaxLength="80" onkeyup="LowercaseToUppercase(this);"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCategoryName" runat="server" TargetControlID="txtCategoryName"
                                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " FilterMode="ValidChars">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvtxtCategoryName" runat="server" ControlToValidate="txtCategoryName"
                                                                Display="None" ErrorMessage="Please Enter Category Name" ValidationGroup="store1"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="span1" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Category Short Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtCategoryShortName" runat="server" TabIndex="2" ToolTip="Enter Category Short Name" CssClass="form-control" MaxLength="32"
                                                                onKeyup="LowercaseToUppercase(this);"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCatagoryShort" runat="server" TargetControlID="txtCategoryShortName"
                                                                ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789" FilterMode="ValidChars">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCategoryName"
                                                                Display="None" ErrorMessage="Please Enter Category Short Name" ValidationGroup="store1"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="butCategoryName" Text="Submit" TabIndex="3" ToolTip="Click To Submit" ValidationGroup="store1" runat="server"
                                                        CssClass="btn btn-primary" OnClick="butCategoryName_Click" />
                                                  
                                                    <asp:Button ID="btnshowrpt1" Text="Report" TabIndex="5" ToolTip="Click To Show Report" runat="server" CssClass="btn btn-info" OnClick="btnshowrpt1_Click" />
                                                      <asp:Button ID="butPartyCategoryCancel" TabIndex="4" ToolTip="Click To Reset"
                                                        Text="Cancel" runat="server" CssClass="btn btn-warning" OnClick="butPartyCategoryCancel_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="store1"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                                </div>

                                                <asp:Panel ID="pnlCategoryName" runat="server">
                                                    <div class="col-12">
                                                        <asp:ListView ID="lvCategoryName" runat="server">
                                                            <EmptyDataTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Vendor Category</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
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



        <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-warning" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>


    <script>
        setTimeout(function () {
            $($.fn.dataTable.tables(true)).DataTable()
                   .columns.adjust();
        }, 200);
    </script>
    <script type="text/javascript">
        function LowercaseToUppercase(txt) {
            var textValue = txt.value;
            txt.value = textValue.toUpperCase();

        }

        function Validate(txt) {
            var isValid = false;
            var regex = /^[a-zA-Z0-9]*$/;
            isValid = regex.test(document.getElementById("<%=txtIfscCode.ClientID %>").value);
            document.getElementById("spnError").style.display = !isValid ? "block" : "none";
            return isValid;
        }

        //function validateAlphabet(txt) {
        //    var expAlphabet = /^[A-Za-z]+$/;
        //    if (txt.value.search(expAlphabet) == -1) {
        //        txt.value = txt.value.substring(0, (txt.value.length) - 1);
        //        txt.value = '';
        //        txt.focus = true;
        //        alert("Only Alphabets allowed!");
        //        return false;
        //    }
        //    else
        //        return true;
        //}

        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }

    </script>
    <script type="text/javascript">
        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event) {//IE
                        e.returnValue = false;
                    }
                    else {//Firefox
                        e.preventDefault();
                    }
                }
            }
        }


        function textCounter(field, countfield, maxlimit) {
            if (field.value.length > maxlimit)
                field.value = field.value.substring(0, maxlimit);
            else
                countfield.value = maxlimit - field.value.length;
        }
    </script>



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


        //function LowercaseToUppercase(txt) {
        //    var textValue = txt.value;
        //    txt.value = textValue.toUpperCase();

        //}

    </script>

</asp:Content>
