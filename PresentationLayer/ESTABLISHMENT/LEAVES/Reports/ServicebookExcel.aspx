<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServicebookExcel.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ESTABLISHMENT_LEAVES_Reports_ServicebookExcel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Employee consolidate Information</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select Criteria for Employee consolidate Information</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>College</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true"
                                        CssClass="form-control" ToolTip="Select College" AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" ErrorMessage="Please select College" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0">
                                    </asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Staff Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaffType" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                        CssClass="form-control" TabIndex="2" AutoPostBack="True" ToolTip="Select Staff" OnSelectedIndexChanged="ddlStaffType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%-- <asp:RequiredFieldValidator ID="rfvstafftype" runat="server" ControlToValidate="ddlStaffType"
                                        Display="None" ErrorMessage="Please Select Staff Type" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0">
                                    </asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div3" runat="server">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                        CssClass="form-control" TabIndex="3" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" ToolTip="Select Department">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div4" runat="server">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Employee</label>
                                    </div>
                                    <asp:DropDownList ID="ddlEmp" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                        CssClass="form-control" TabIndex="4" AutoPostBack="true" ToolTip="Select Employee">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div5" runat="server">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Service Book category</label>
                                    </div>
                                    <asp:DropDownList ID="ddlservicebook" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                        CssClass="form-control" TabIndex="5" AutoPostBack="true" ToolTip="Please Select Servicebook category" OnSelectedIndexChanged="ddlservicebook_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvservicebook" runat="server" ControlToValidate="ddlservicebook"
                                        Display="None" ErrorMessage="Please Select Servicebook category" ValidationGroup="Leaveapp" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divcheck" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Highest Qualification</label>
                                    </div>
                                    <asp:CheckBox ID="chkhigest" runat="server" ToolTip="Check mark for Highest Qualification" />
                                </div>

                            </div>
                        </div>
                    </asp:Panel>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnExcelReport" runat="server" Text="Excel" ValidationGroup="Leaveapp" TabIndex="6" CssClass="btn btn-info" ToolTip="Click here to Export Report" OnClick="btnExcelReport_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="7" CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>



