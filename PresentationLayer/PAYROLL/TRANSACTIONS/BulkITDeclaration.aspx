<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BulkITDeclaration.aspx.cs" Inherits="PayRoll_BulkITDeclaration" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">BULK IT DECLARATION</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Select Staff and college</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlSelect" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>From</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgFdate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control textbox" TabIndex="1"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="calFromDate" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="imgFdate" TargetControlID="txtFromDate" />
                                                    <cc1:MaskedEditExtender ID="meeFromDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                        TargetControlID="txtFromDate">
                                                    </cc1:MaskedEditExtender>
                                                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                        Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="true" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>To</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgTdate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" CssClass="form-control textbox"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="calToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                        PopupButtonID="imgTdate" Enabled="true" EnableViewState="true" />
                                                    <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                        MaskType="Date" Mask="99/99/9999">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                        Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>College</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                                    Display="None" ErrorMessage="Please Select College" ValidationGroup="payroll" InitialValue="0"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                   <%-- <label>Staff</label>--%>
                                                    <label>Scheme/Staff</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                    Display="None" ErrorMessage="Please Select Scheme/Staff" ValidationGroup="payroll" InitialValue="0"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <%--  <asp:Button ID="btnShow" runat="server" Text="Show"  Width="80px"
                                                OnClick="btnShow_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="payroll"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />--%>
                                            </div>
                                              <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                   <%-- <label>Staff</label>--%>
                                                    <label>IT Scheme/Rule</label>
                                                </div>
                                                <asp:DropDownList ID="ddlITRule" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlITRule"
                                                    Display="None" ErrorMessage="Please Select IT Rule" ValidationGroup="payroll" InitialValue="0"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <%--  <asp:Button ID="btnShow" runat="server" Text="Show"  Width="80px"
                                                OnClick="btnShow_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="payroll"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" CssClass="btn btn-primary" TabIndex="5"
                                            OnClick="btnSub_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="6"
                                                OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                </asp:Panel>
                                <div id="Div1" class="col-12" runat="server" visible="false">
                                    <asp:Panel ID="pnlITDeclaration" runat="server">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Bulk IT Declaration</h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:ListView ID="lvITDeclaration" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                        <th>Idno 
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                             <EmptyDataTemplate>
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Challan Entry Exists" />
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr class="item">
                                                    <td>
                                                        <%#Eval("IDNO")%>
                                                    </td>
                                                    <td>
                                                        <%#Eval("NAME")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="col-12">
                                    <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
       
    </asp:UpdatePanel>
      <div id="divMsg" runat="server">
        </div>
</asp:Content>
