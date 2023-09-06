<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_MISReport.aspx.cs" Inherits="PAYROLL_REPORTS_Pay_MISReport" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">PAY MIS REPORT</h3>
                    <p class="text-center">
                    </p>
                    <div class="box-tools pull-right">
                    </div>
                </div>

                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                            </h5>
                            <div class="panel panel-info">
                                <div class="panel-heading">PAY MIS REPORT</div>
                                <div class="panel-body">
                                    <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                    <asp:Panel ID="pnl" runat="server">

                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-6">
                                                <div class="form-group col-md-10">
                                                    <label>College :<span style="color: Red">*</span></label>

                                                    <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="1" ToolTip="Select College" CssClass="form-control" AppendDataBoundItems="True">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                                        ControlToValidate="ddlCollege" Display="None" ErrorMessage="Please Select  College"
                                                        ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Staff Type:<span style="color: Red">*</span></label>

                                                    <asp:DropDownList ID="ddlStaffNo" runat="server" TabIndex="1" ToolTip="Select Staff Type" CssClass="form-control" AppendDataBoundItems="True">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvStaffNo" runat="server" SetFocusOnError="true"
                                                        ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Staff No."
                                                        ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Report Type:<span style="color: Red">*</span></label>

                                                    <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Select Report Type"
                                                        AppendDataBoundItems="True">
                                                        <asp:ListItem Enabled="true" Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Designation" Value="SUBDESIG"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Department" Value="SUBDEPT"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Scale" Value="SCALE"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Category" Value="CATEGORY"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Religion" Value="RELIGION"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Quarter" Value="QTRNAME"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Handicapped" Value="HP"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Pay Rule" Value="RULENAME"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Increment Date Wise" Value="DOI"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Retirement Date Wise" Value="RDT"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Joining Date Wise" Value="DOJ"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Birth Date Wise" Value="DOB"></asp:ListItem>
                                                        
                                                        <asp:ListItem Enabled="true" Text="Staff Wise" Value="Staff"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Appointment Wise" Value="APPOINT"></asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvReportType" runat="server" SetFocusOnError="true"
                                                        ControlToValidate="ddlReportType" Display="None" ErrorMessage="Please Select Report Type"
                                                        ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <asp:CheckBox ID="chkShowWithGraph" runat="server" Checked="false" TabIndex="3" ToolTip="Show With Graph"
                                                        Text="Show With Graph" />
                                                </div>
                                                <div class="form-group col-md-10">
                                                </div>

                                            </div>
                                            <div class="form-group col-md-6">
                                            </div>
                                        </div>

                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:Button ID="btnReport" runat="server" Text="Report"
                                    CssClass="btn btn-info" TabIndex="4" ToolTip="Click to Show the Report" ValidationGroup="Payroll" OnClick="btnReport_Click" />


                                <asp:Button ID="btnCancel" runat="server" ToolTip="Click To Reset" TabIndex="5" Text="Cancel" OnClick="btnCancel_Click"
                                    CssClass="btn btn-danger" />

                            </div>
                        </div>

                    </div>
                </form>
            </div>
        </div>
    </div>   
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

