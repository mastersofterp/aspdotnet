<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LeaveAllotmentReport.aspx.cs" Inherits="LeaveAllotmentReport" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">LEAVE ALLOTMENT REPORT</h3>
                        </div>

                        <div class="box-body">
                            <div class="panel-body">
                                <asp:Label ID="Label1" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="Label2" runat="server" SkinID="lblmsg"></asp:Label>

                                <asp:UpdatePanel ID="updAdd" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlAdd" runat="server">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>College</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="3" data-select2-enable="true"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Select College Name">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="Submit" SetFocusOnError="true"
                                                            InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Staff Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlStaffType" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="3" data-select2-enable="true" OnSelectedIndexChanged="ddlStaffType_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvstafftype" runat="server" ControlToValidate="ddlStaffType" Display="None"
                                                            ErrorMessage="Please Select Staff Type" ValidationGroup="Submit" SetFocusOnError="true"
                                                            InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Department</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" TabIndex="4" AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Employee</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlEmp" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="4" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Leave Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlLeaveName" runat="server" TabIndex="5" data-select2-enable="true"
                                                            AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row" runat="server" id="divPeriod" visible="false">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Period</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPeriod" runat="server" TabIndex="1" data-select2-enable="true"
                                                            AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divYear" visible="false">
                                                        <div class="label-dynamic">
                                                            <label>Year</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlYear" runat="server" TabIndex="2" data-select2-enable="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnShowReport" runat="server" Text="Report" ValidationGroup="Submit" TabIndex="6"
                                                    OnClick="btnShowReport_Click" CssClass="btn btn-info" />
                                                <asp:Button ID="btnexport" runat="server" Text="Export" ValidationGroup="Submit" TabIndex="7"
                                                    OnClick="btnexport_Click" CssClass="btn btn-info" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="7"
                                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>

                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnShowReport" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>


            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

