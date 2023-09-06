<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StaffAllotmentToIncharge.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_StaffAllotmentToIncharge" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

 <%--   <script src="../../../includes/prototype.js" type="text/javascript"></script>
    <script src="../../../includes/scriptaculous.js" type="text/javascript"></script>
    <script src="../../../includes/modalbox.js" type="text/javascript"></script>
    <script src="../../../jquery/jquery-1.10.2.min.js" type="text/javascript"></script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STAFF ALLOTMENT TO INCHARGE </h3>
                </div>
                <%--<form role="form">--%>
                <div class="box-body">
                    <div class="col-12">
                        <%--<div class="panel panel-info">--%>
                        <%--<div class="panel-heading">Staff Allotment To Incharge </div>--%>
                        <%--<div class="panel-body">--%>
                        <%--Note : <span style="color: #FF0000">* Marked Is Mandatory!</span>--%>
                        <asp:Panel ID="pnlSelect" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <%-- <div class="col-md-6">--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>College:</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="Incharge"
                                            SetFocusOnError="True" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="InchargeShow"
                                            SetFocusOnError="True" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Incharge Name :</label>
                                        </div>
                                        <asp:DropDownList ID="ddlIncharge" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlIncharge_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvIncharge" runat="server" ControlToValidate="ddlIncharge"
                                            Display="None" ErrorMessage="Please Select Incharge Name" ValidationGroup="InchargeShow"
                                            SetFocusOnError="True" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>


                                    <%--</div>--%>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <%--<p class="text-center">--%>
                                <%-- <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Incharge"
                                                    Width="80px" OnClick="btnSave_Click" />   
                                                  &nbsp; --%>
                                <%-- <asp:Button id="btnTempRemove" runat="server" Text="Temporary Remove"  OnClick="btnTempRemove_Click"/>
                                                       &nbsp; 
                                                    <asp:Button id="btnPerRemove" runat="server" Text="Permanent Remove" OnClick="btnPerRemove_Click"/>
                                                &nbsp; --%>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-warning"
                                    OnClick="btnCancel_Click" />&nbsp;  
                                                <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="Incharge" CssClass="btn btn-info"
                                                    OnClick="btnReport_Click" />&nbsp;    

                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Incharge"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" Width="338px" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="InchargeShow"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <%--</p>--%>
                            </div>
                        </asp:Panel>

                        <div class="table-responsive">
                            <asp:Panel ID="pnlIncharge" runat="server">
                                <asp:ListView ID="lvIncharge" runat="server">
                                    <%--<EmptyDataTemplate>
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Data Found" />
                                        <%--Staff Allotment--%>
                                    <%--</EmptyDataTemplate>--%>
                                    <LayoutTemplate>
                                        <div id="demo-grid" class="vista-grid">
                                            <div class="sub-heading">
                                                <h5>Employee Staff</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="width: 2%">Select
                                                        </th>
                                                        <th style="width: 20%">Employee Name
                                                        </th>
                                                        <th style="width: 20%">Designation
                                                        </th>
                                                        <th style="width: 20%">Temporary Available
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                            <%--</div>--%>
                                            <%--<div class="listview-container">
                                                <div id="Div1" class="vista-grid">
                                                    <table class="table table-bordered table-hover">
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td style="width: 5%">
                                                <asp:CheckBox ID="chkIncharge" runat="server" AlternateText="Check to select incharge"
                                                    ToolTip='<%# Eval("EMPLOYEEIDNO")%>' />
                                            </td>
                                            <td style="width: 20%">
                                                <%# Eval("NAME")%>
                                            </td>
                                            <td style="width: 20%">
                                                <%# Eval("SUBDESIG")%>
                                            </td>
                                            <td style="width: 20%">
                                                <%# Eval("IsTempRemove")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="altitem">
                                            <td style="width: 5%">
                                                <asp:CheckBox ID="chkIncharge" runat="server" AlternateText="Check to select incharge"
                                                    ToolTip='<%# Eval("EMPLOYEEIDNO")%>' />


                                            </td>
                                            <td style="width: 20%">
                                                <%# Eval("NAME")%>
                                            </td>
                                            <td style="width: 20%">
                                                <%# Eval("SUBDESIG")%>
                                            </td>
                                            <td style="width: 20%">
                                                <%# Eval("IsTempRemove")%>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>

                            </asp:Panel>
                        </div>


                        <div class="table-responsive">
                            <asp:Panel ID="pnlView" runat="server">
                                <asp:ListView ID="lvView" runat="server">
                                    <%--<EmptyDataTemplate>
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Data Found" />
                                    </EmptyDataTemplate>--%>
                                    <%--Temporary/Permanent Remove--%>
                                    <LayoutTemplate>
                                        <div id="demo-grid" class="vista-grid">
                                            <div class="sub-heading">
                                                <h5>Temporary/Permanent Remove Employee Staff </h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="width: 2%">Select
                                                        </th>
                                                        <th style="width: 20%">Employee Name
                                                        </th>
                                                        <th style="width: 20%">Designation
                                                        </th>
                                                        <th style="width: 20%">Temporary Remove
                                                        </th>
                                                        <th style="width: 20%">Temporary Available
                                                        </th>
                                                    </tr>
                                                    <thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td style="width: 5%">
                                                <asp:CheckBox ID="chkStaff" runat="server" AlternateText="Check to select Staff"
                                                    ToolTip='<%# Eval("IDNO")%>'
                                                    Font-Bold='<%# Eval("IsTempAvailable").ToString() == "Y" ? true : false %>' />
                                                <%--   Enabled='<%# Eval("IsTempAvailable").ToString() == "Y" ? false : true %>' --%>
                                            </td>
                                            <td style="width: 20%">
                                                <%# Eval("NAME")%>
                                            </td>
                                            <td style="width: 20%">
                                                <%# Eval("SUBDESIG")%>
                                            </td>
                                            <td style="width: 20%">
                                                <%# Eval("IsTempRemove")%>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="lblTempAvailable" runat="server" Text='<%# Eval("IsTempAvailable")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="altitem">
                                            <td style="width: 5%">
                                                <asp:CheckBox ID="chkStaff" runat="server" AlternateText="Check to select Staff"
                                                    ToolTip='<%# Eval("IDNO")%>' />
                                            </td>

                                            <td style="width: 20%">
                                                <%# Eval("NAME")%>
                                            </td>
                                            <td style="width: 20%">
                                                <%# Eval("SUBDESIG")%>
                                            </td>
                                            <td style="width: 20%">
                                                <%# Eval("IsTempRemove")%>
                                            </td>
                                            <td style="width: 20%">

                                                <asp:Label ID="lblTempAvailable" runat="server" Text='<%# Eval("IsTempAvailable")%>'></asp:Label>

                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>

                            </asp:Panel>
                        </div>

                        <div class="col-12 btn-footer">
                            <%--<p class="text-center">--%>
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Incharge" CssClass="btn btn-primary"
                                    OnClick="btnSave_Click" Visible="false" />

                                <asp:Button ID="btnTempRemove" runat="server" Text="Temporary Remove" OnClick="btnTempRemove_Click" Visible="false" CssClass="btn btn-primary" />
                                <asp:Button ID="btnPerRemove" runat="server" Text="Permanent Remove" OnClick="btnPerRemove_Click" Visible="false" CssClass="btn btn-primary" />
                            <%--</p>--%>
                        </div>

                        <%-- </div>--%>
                        <%-- </div>--%>
                    </div>
                </div>
                <%--</form>--%>
            </div>
        </div>
    </div>

</asp:Content>

