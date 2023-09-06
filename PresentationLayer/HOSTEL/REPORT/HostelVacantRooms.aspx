<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="HostelVacantRooms.aspx.cs" Inherits="Hostel_Report_HostelVacantRooms"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">HOSTEL STUDENT VACANT ROOM</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Session </label>
                                </div>
                                <asp:DropDownList ID="ddlSession" CssClass="form-control" runat="server" AppendDataBoundItems="True" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel No. </label>
                                </div>
                                <asp:DropDownList ID="ddlHostelNo" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlHostelNo_SelectedIndexChanged" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ControlToValidate="ddlHostelNo"
                                    Display="None" ErrorMessage="Please Select Hostel" ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Block No. </label>
                                </div>
                                <asp:DropDownList ID="ddlBlockNo" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlBlockNo_SelectedIndexChanged" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Floor </label>
                                </div>
                                <asp:DropDownList ID="ddlFloor" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="true" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Text="Show" OnClick="btnShow_Click" Width="79px"
                            ValidationGroup="submit" />
                        <asp:Button ID="btnExcelReport" runat="server" CssClass="btn btn-primary" Text="Excel Report" OnClick="btnExcelReport_Click"  Width="102px" ValidationGroup="submit" />
                        &nbsp;<asp:Button ID="btnCancel" CssClass="btn btn-warning" runat="server" Text="Cancel" Width="79px" OnClick="btnCancel_Click"
                            CausesValidation="false" />
                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
