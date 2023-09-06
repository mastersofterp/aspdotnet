<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="AlertConfiguration.aspx.cs" Inherits="ACCOUNT_AlertConfiguration" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>


    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>

    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UPDLedger"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <div style="width: 100%">
        <asp:UpdatePanel ID="UPDLedger" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div id="div2" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">ALERT CONFIGURATION</h3>
                            </div>
                            <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                            <div class="box-body">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Alert Configuration</div>
                                        <div class="panel-body">
                                            <div class="col-md-12">
                                                <div class="form-group row">
                                                    <div class="col-md-12">
                                                        Note<span style="font-size: small">:</span><span style="font-weight: bold; font-size: x-small; color: red">* Marked is mandatory !</span>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2">
                                                        <label>Alert For<span style="color: red">*</span> :</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtAlertFor" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAlertFor" Display="None"
                                                            ErrorMessage="Please Enter Alert For" SetFocusOnError="true" ValidationGroup="AccMoney">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Ledger Name<span style="color: red">*</span> :</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtAcc" runat="server" TabIndex="2" CssClass="form-control" ToolTip="Please Enter Ledger Name"
                                                            AutoPostBack="true"></asp:TextBox>
                                                        <ajaxToolKit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtAcc"
                                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                            ServiceMethod="GetMergeLedger" OnClientShowing="clientShowing">
                                                        </ajaxToolKit:AutoCompleteExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                            ControlToValidate="txtAcc" Display="None"
                                                            ErrorMessage="Please Select Ledger" SetFocusOnError="true"
                                                            ValidationGroup="AccMoney">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Alert To
                                        </div>
                                        <div class="panel-body">
                                            <div class="col-md-12">
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>Email<span style="color: red">*</span> :</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Please Enter Email Address"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" Display="None"
                                                            ErrorMessage="Please Enter Email Address" SetFocusOnError="true" ValidationGroup="AccMoney">
                                                        </asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Mobile No.<span style="color: red">*</span> :</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Please Enter Mobile No."
                                                            MaxLength="10"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPhoneNo" Display="None"
                                                            ErrorMessage="Please Enter Mobile Number" SetFocusOnError="true" ValidationGroup="AccMoney">
                                                        </asp:RequiredFieldValidator>--%>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPhoneNo"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" Enabled="True" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-12 text-center">
                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" TabIndex="5" OnClick="btnAdd_Click" />
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:Repeater ID="RptAlertDetails" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="table table-striped table-bordered dt-responsive nowrap">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>SELECT</th>
                                                                            <th>EMAIL</th>
                                                                            <th>MOBILE NUMBER</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnItemEdit" runat="server" TabIndex="15" Text="Select"
                                                                            CommandArgument='<%# Eval("SRNO") %>' ToolTip="Edit Record"
                                                                            ImageUrl="~/IMAGES/edit.png" Width="15" Height="15" OnClick="btnItemEdit_Click" />
                                                                        <%--OnClick="btnItemEdit_Click"--%>
                                                                        <asp:ImageButton ID="btnItemDelete" runat="server" TabIndex="14" Text="Select"
                                                                            CommandArgument='<%# Eval("SRNO") %>' ToolTip="Delete Record"
                                                                            ImageUrl="~/IMAGES/delete.png" Width="15" Height="15" OnClick="btnItemDelete_Click" />
                                                                        <%--OnClick="btnItemDelete_Click"--%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("MOBILENO") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody></table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="form-group row">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnSubmit" Text="Submit" CssClass="btn btn-primary" runat="server" TabIndex="6" OnClick="btnSubmit_Click"
                                            ValidationGroup="AccMoney" />
                                        <asp:Button ID="btnCancel" Text="Cancel" CssClass="btn btn-warning" runat="server" TabIndex="7" OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="AccMoney" DisplayMode="List" />
                                    </div>
                                </div>
                                <asp:Panel ID="pnlList" runat="server" Visible="false">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Alert List
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Repeater ID="rptList" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="table table-striped table-bordered dt-responsive nowrap">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>SELECT</th>
                                                                        <th>ALERT FOR</th>
                                                                        <th>Ledger</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" TabIndex="15" Text="Select"
                                                                        CommandArgument='<%# Eval("ALERTID") %>' ToolTip="Edit Record"
                                                                        ImageUrl="~/IMAGES/edit.png" Width="15" Height="15" OnClick="btnEdit_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAlertFor" runat="server" Text='<%# Eval("ALERTNAME") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblLedger" runat="server" Text='<%# Eval("PARTY_NAME") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnLedger" runat="server" Value='<%# Eval("PARTY_NO") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody></table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>

