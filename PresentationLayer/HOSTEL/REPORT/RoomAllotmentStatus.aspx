<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="RoomAllotmentStatus.aspx.cs" Inherits="Academic_RoomAllotmentStatus"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">HOSTEL ROOM ALLOTMENT STATUS</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Session </label>
                                </div>
                                <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                    AutoPostBack="true" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                    ErrorMessage="Please Select Session" Display="None" ValidationGroup="academic"
                                    SetFocusOnError="true" InitialValue="0">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel </label>
                                </div>
                                <asp:DropDownList ID="ddlHostelNo" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlHostelNo_SelectedIndexChanged" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostelNo" runat="server" ControlToValidate="ddlHostelNo"
                                    ErrorMessage="Please Select Hostel" Display="None" ValidationGroup="academic"
                                    SetFocusOnError="true" InitialValue="0">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Block No. </label>
                                </div>
                                <asp:DropDownList ID="ddlBlockNo" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlBlockNo_SelectedIndexChanged" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" id="Tr1" runat="server">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Floor </label>
                                </div>
                                <asp:DropDownList ID="ddlFloor" runat="server" CssClass="form-control" TabIndex="3"
                                    AppendDataBoundItems="true" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlFloor_SelectedIndexChanged" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Room No. </label>
                                </div>
                                <asp:DropDownList ID="ddlRoomNo" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Order By</label>
                                </div>
                                <asp:RadioButton ID="rdoRoomno" runat="server" Checked="true" Text="Room No." GroupName="A" />
                                &nbsp;&nbsp;&nbsp;
                                           <asp:RadioButton ID="rdoAdmissionNo" runat="server" Text="Admission No." GroupName="A" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Room Status </label>
                                </div>
                                <asp:RadioButton ID="rdovacant" runat="server" Text="Vacant" GroupName="B" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdonotvacant" runat="server" Checked="false" Text="Used" GroupName="B" />

                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Text="Show" OnClick="btnShow_Click"
                            ValidationGroup="academic" />
                        <asp:Button ID="btnstud" runat="server" CssClass="btn btn-primary" Text="Student List For Hostel"
                            ValidationGroup="academic" OnClick="btnstud_Click" />
                        <asp:Button ID="btnvacantroom" CssClass="btn btn-primary" runat="server" Text="Vacant Report of Room"
                            ValidationGroup="academic" OnClick="btnvacantroom_Click" />
                        <asp:Button ID="btnCancel" CssClass="btn btn-warning" runat="server" Text="Cancel" Width="79px" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="academic" DisplayMode="List" />
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnExcelReport" runat="server" CssClass="btn btn-info" Text="Excel Report" ValidationGroup="academic" OnClick="btnExcelReport_Click" />
                        <asp:Button ID="btnstudExcel" runat="server" CssClass="btn btn-info" Text="Student List For Hostel Excel" OnClick="btnstudExcel_Click"
                            ValidationGroup="academic"  />
                        <asp:Button ID="btnvacantroomExcel" CssClass="btn btn-info" runat="server" Text="Vacant Report of Room Excel" OnClick="btnvacantroomExcel_Click"
                            ValidationGroup="academic"  />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
