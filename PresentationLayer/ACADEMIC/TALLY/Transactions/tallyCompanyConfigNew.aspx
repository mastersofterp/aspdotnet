<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="tallyCompanyConfigNew.aspx.cs" Inherits="Tally_Transactions_tallyCompanyConfigNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProga" runat="server" AssociatedUpdatePanelID="upDetails"
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
    <asp:UpdatePanel ID="upDetails" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TALLY COMPANY CONFIGURATION </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>
                                                <asp:Label ID="lblReceiptBook" runat="server" Text="Select Reciept Type"></asp:Label>
                                            </label>
                                        </div>
                                        <asp:DropDownList ID="ddlReceiptBook" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlReceiptBook" Display="None" ErrorMessage="Please Select Receipt Book" ValidationGroup="Submit"
                                            SetFocusOnError="True" Text="*" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>
                                                <asp:Label ID="lblServerName" runat="server" Text="Select Server"></asp:Label>
                                            </label>
                                        </div>
                                        <asp:DropDownList ID="ddlServer" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvServer" runat="server" ControlToValidate="ddlServer" Display="None" ErrorMessage="Please Select Server IP" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblCompanyName" runat="server" Text="Tally Company Name"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtTallyCompany" onkeydown="return (event.keyCode!=13);" runat="server" placeholder="Please Enter Tally Company Name" CssClass="form-control" MaxLength="256" TabIndex="3">
                                        </asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTallyCompany" Display="None" ErrorMessage="Please Get Company Name" ValidationGroup="Submit"
                                            SetFocusOnError="True" Text="*"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnSearch" TabIndex="4" runat="server" CssClass="btn btn-primary" Text="Get Current Company" OnClick="btnSearch_Click" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>
                                                <asp:Label ID="lblIsActive" CssClass="control-label" runat="server" Text="Active"></asp:Label>
                                            </label>
                                        </div>
                                        <asp:CheckBox ID="chkIsActive" onkeydown="return (event.keyCode!=13);" runat="server" TabIndex="5" Checked="true" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="6" Text="Submit" ToolTip="Click to Save" ValidationGroup="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" />

                                <asp:Button ID="btnReport" Visible="false" runat="server" CssClass="btn btn-info" OnClick="btnReport_Click" TabIndex="6" Text="Report" ToolTip="Click to Report" />

                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn btn-warning" OnClick="btnCancel_Click" OnClientClick="Cancel()" TabIndex="7" Text="Cancel" ToolTip="Click to Cancel" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>

                            <div class="col-12" id="Table1" runat="server">
                                <asp:Panel ID="DivCompany" runat="server" Visible="false">
                                    <asp:ListView ID="Rep_Company" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid">
                                                <div class="sub-heading">
                                                    <h5>Company Configuration</h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>ACTION
                                                            </th>
                                                            <th>SR.NO.
                                                            </th>

                                                            <th>RECEIPT BOOK
                                                            </th>

                                                            <th>TALLY COMPANY  
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" TabIndex="8" Text="EDIT"
                                                        CommandArgument='<%# Eval("TallyCompanyConfigId") %>' ToolTip="Edit Record"
                                                        ImageUrl="~/Images/edit.png" OnClick="btnEdit_click" />
                                                </td>

                                                <td>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </td>

                                                <td>
                                                    <%# Eval("CashBookName")%>
                                                                        
                                                </td>

                                                <td>
                                                    <strong><%# Eval("TallyCompanyName")%></strong>

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
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

