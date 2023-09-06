<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HostelFineReport.aspx.cs" Inherits="HOSTEL_REPORT_HostelFineReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript" language="javascript" src="../Javascripts/FeeCollection.js"></script>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Hostel Fine Report</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlSelect" runat="server">
                        <div class="col-12" id="divStudentSearch" runat="server">
                            <div class="row" id="divStudentFine" runat="server">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Session </label>
                                    </div>
                                    <asp:DropDownList ID="ddlHostelSessionNo" runat="server" AppendDataBoundItems="True"
                                        TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvHostelSessionNo" runat="server" ErrorMessage="Please Select Hostel Session"
                                        ControlToValidate="ddlHostelSessionNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Hostel </label>
                                    </div>
                                    <asp:DropDownList ID="ddlHostelNo" runat="server"
                                        AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlHostelNo_SelectedIndexChanged" TabIndex="2"
                                        CssClass="form-control" data-select2-enable="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvHostelNo" runat="server" ErrorMessage="Please Select Hostel Name"
                                        ControlToValidate="ddlHostelNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                                </div>
                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnShow" runat="server" Text="Show"
                                ToolTip="Show Fine Student" CssClass="btn btn-primary"
                                TabIndex="3" ValidationGroup="Submit" OnClick="btnShow_Click" />

                             <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />

                            <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                ToolTip="Submit Fine Student" CssClass="btn btn-primary"
                                TabIndex="4" ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnReport" runat="server" Text="Report"
                                ToolTip="Report Fine Student" CssClass="btn btn-info"
                                TabIndex="5" ValidationGroup="Submit" OnClick="btnReport_Click" />
                            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                TabIndex="6" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="PnlLv" runat="server">
                        <div class="col-12">
                            <asp:ListView ID="lvDetails" runat="server"
                                OnItemDataBound="lvDetails_ItemDataBound">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <div class="titlebar">
                                            <b>Student List</b>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Edit</th>
                                                    <th>Roll No
                                                    </th>
                                                    <th>Student Name
                                                    </th>
                                                    <th>Degree
                                                    </th>
                                                    <th>Branch
                                                    </th>
                                                    <th>Semester
                                                    </th>
                                                    <th>Room 
                                                    </th>
                                                    <th>Fine
                                                    </th>
                                                    <th>Remark
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
                                            <asp:ImageButton ID="btnEditFine" runat="server" OnClick="btnEditFine_Click"
                                                CommandArgument='<%# Eval("IDNO") %>' ImageUrl="~/Images/edit.png" ToolTip='<%# Eval("IDNO") %>' />
                                        </td>
                                        <td>
                                            <%# Eval("IDNO") %>
                                            <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                        </td>
                                        <td>
                                            <%# Eval("STUDNAME") %>
                                        </td>
                                        <td>
                                            <%# Eval("DEGREE") %>
                                        </td>
                                        <td>
                                            <%# Eval("BRANCH") %>
                                        </td>
                                        <td>
                                            <%# Eval("SEMESTERNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("ROOM_NAME") %>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFine" runat="server" MaxLength="8" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender
                                                ID="ftbd" runat="server" TargetControlID="txtFine" ValidChars="1234567890.">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

