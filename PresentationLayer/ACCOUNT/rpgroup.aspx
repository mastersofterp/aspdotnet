<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="rpgroup.aspx.cs" Inherits="Account_rpgroup" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="IITMSTextBox" Namespace="MyCustomControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 320px;
        }
    </style>--%>
   
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDRPGROUP"
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
    <asp:UpdatePanel ID="UPDRPGROUP" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="divMsg" runat="server">
                            <div id="div1" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">RECEIPT/PAYMENT GROUP</h3>
                            </div>
                            <div class="box-body">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large;"></div>
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="col-12">
                                        <%-- <div class="sub-heading">
                                            <h5>Create Receipt/Payment Group</h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Group Name</label>
                                                </div>
                                                <asp:TextBox ID="txtGroupName" runat="server" Style="text-transform: uppercase" Text=""
                                                    ToolTip="Please Enter Group Name" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvGoupName" runat="server" ControlToValidate="txtGroupName"
                                                    Display="None" ErrorMessage="Please Enter Group Name" SetFocusOnError="True"
                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Parent Group</label>
                                                </div>
                                                <asp:DropDownList ID="ddlParentGroup" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Final Account Head </label>
                                                </div>
                                                <asp:DropDownList ID="ddlFAHead" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div3" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Account Code</label>
                                                </div>
                                                <input type="hidden" runat="server" id="hdnid" />
                                                <cc1:CTextBox ID="CTextBox1" runat="server" DataType="DateType" IsRequired="True"
                                                    IsValidate="True" NextControlID="CTextBox2" Text="" Visible="false">
                                                </cc1:CTextBox>
                                                <br />
                                                <cc1:CTextBox ID="CTextBox2" runat="server" DataType="DateType" IsRequired="False"
                                                    IsValidate="True" NextControlID="CTextBox3" PreviousControlID="CTextBox1" Text=""
                                                    Visible="false">
                                                </cc1:CTextBox>
                                                <br />
                                                <cc1:CTextBox ID="CTextBox3" runat="server" DataType="DateType" IsRequired="False"
                                                    IsValidate="True" Text="" Visible="false">
                                                </cc1:CTextBox>
                                                <asp:TextBox ID="txtAccCode" Style="text-transform: uppercase" ToolTip="Please Enter Account Code"
                                                    runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                                CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                        </div>
                                        <div class="col-12 mb-5">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Search Criteria</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSearch" Style="text-align: left;" runat="server" AutoPostBack="true"
                                                        OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                                    <div class="mt-2">
                                                        <asp:ListBox ID="lstGroup" Style="text-align: left; height: 300px!important;" runat="server" Rows="10" CssClass="form-control"
                                                            AutoPostBack="True" OnSelectedIndexChanged="lstGroup_SelectedIndexChanged"></asp:ListBox>
                                                        <ajaxToolKit:ListSearchExtender ID="lstGroup_ListSearchExtender" runat="server" Enabled="True"
                                                            TargetControlID="lstGroup">
                                                        </ajaxToolKit:ListSearchExtender>
                                                    </div>

                                                </div>
                                        </div>
                                    </div>
                                    <%--</div>--%>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
   

</asp:Content>
