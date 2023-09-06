<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="demandModify.aspx.cs" Inherits="Academic_demandModify" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">TUITION FEES DEMAND MODIFICATION</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>O/S</label>
                                </div>
                                <asp:Label ID="lblDfno" runat="server" Font-Bold="True"></asp:Label>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>O/S</label>
                                </div>
                                O/S Date :
                                <asp:Label ID="lblDate" runat="server" Font-Bold="True"></asp:Label>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>O/S</label>
                                </div>
                                Select Student :
                            
                                <asp:DropDownList ID="ddlStudent" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="rfvStud" runat="server" ControlToValidate="ddlStudent"
                                    Display="None" ErrorMessage="Please Select Student" InitialValue="0" ValidationGroup="Modification"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>O/S</label>
                                </div>
                                Select Semester :
                           
                                <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Modification"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" CausesValidation="true"
                            ValidationGroup="Modification" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ValidationGroup="Modification" ShowMessageBox="True" ShowSummary="False" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
