<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="HandicapStudentList.aspx.cs" Inherits="HOSTEL_REPORT_HandicapStudentList"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">PHYSICALLY HANDICAPPED STUDENT LIST REPORT</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Session</label>
                                </div>
                                <asp:DropDownList ID="ddlHostelSessionNo" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                    TabIndex="1">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostelSessionNo" runat="server" ErrorMessage="Please Select Hostel Session"
                                    ControlToValidate="ddlHostelSessionNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Degree </label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                    AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Branch </label>
                                </div>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true"
                                    TabIndex="1">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Semester </label>
                                </div>
                                <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true"
                                    TabIndex="1">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnReport" runat="server" Text="Student Apply List" ValidationGroup="Submit"
                            OnClick="btnReport_Click" CssClass="btn btn-primary" />
                        <asp:Button ID="btnExcelReport" runat="server" Text="Excel Report" ValidationGroup="Submit"
                             CssClass="btn btn-info" OnClick="btnExcelReport_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                        <asp:ValidationSummary ID="vsStudent" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
