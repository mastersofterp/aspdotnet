<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="LabelConfiguration.aspx.cs" Inherits="ADMINISTRATION_LabelConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpnl"
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

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">LABEL CONFIGURATION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div id="div1" runat="server"></div>
                        <div class="box-body">
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Label ID</label>
                                            </div>
                                            <asp:TextBox ID="txtlblId" AutoComplete="off" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvlblId" runat="server" ControlToValidate="txtlblId" Display="None" ErrorMessage="Please Enter Label ID"
                                                SetFocusOnError="true" ValidationGroup="label" />
                                        </div>

                                        <div class="form-group col-md-4">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Label Name</label>
                                            </div>
                                            <asp:TextBox ID="txtlblName" AutoComplete="off" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtlblName" Display="None"
                                                ErrorMessage="Please Enter Label Name" SetFocusOnError="true" ValidationGroup="label" />
                                        </div>

                                        <div class="form-group col-md-5" runat="server" id="divCollege" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="3" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="label" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                        ValidationGroup="label" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlLabel" runat="server">
                                        <asp:ListView ID="lvLabelConfig" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Label Configuration List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="text-align: center">Edit</th>
                                                            <th style="text-align: center">Sr. No.</th>
                                                            <th>Label ID</th>
                                                            <th>Label Name</th>
                                                            <th runat="server" id="lvCol" visible="false">College</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CausesValidation="false" OnClick="btnEdit_Click"
                                                            CommandArgument='<%# Eval("RECNO") %>' AlternateText="Edit Record" TabIndex="5" />
                                                    </td>
                                                    <td style="text-align: center"><%#: Container.DataItemIndex + 1 %></td>
                                                    <td><%# Eval("LABELID")%></td>
                                                    <td><%# Eval("LABELNAME")%></td>
                                                    <td runat="server" id="lvColRow" visible="false"><%# Eval("COLLEGE_NAME")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

