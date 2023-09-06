<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Comp_Off_Leave_List.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_Comp_Off_Leave_List"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<div class="row">
    <div class="col-md-12 col-sm-12 col-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">COMP-OFF LEAVES TO CREDIT</h3>
            </div>

            <div class="box-body">
                 <asp:Panel ID="pnlView" runat="server" Visible="true">
                     <div class="col-12">
        <asp:ListView ID="lvEmpList" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <center><asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Employee Not Found" /></center>
                            </EmptyDataTemplate>
                            <LayoutTemplate>                                   
                                    <div class="sub-heading">
	                                    <h5>Employee List</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr.No
                                                </th>
                                                <th>
                                                    <asp:CheckBox ID="cbAl" runat="server" onclick="totAllSubjects(this)" />
                                                    Select
                                                </th>
                                                <th>Employee Name
                                                </th>
                                                <th>Working Date
                                                </th>
                                                <th>Hour
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                    </table>                                
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item">
                                    <td>
                                        <%#Container.DataItemIndex+1 %>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("RNO") %>' onclick="EnableCredit(this)" />
                                        <asp:HiddenField ID="hidEmployeeNo" runat="server" Value='<%# Eval("RNO") %>' />
                                    </td>
                                    <td>
                                        <%# Eval("NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("WORKING_DATE","{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td>
                                        <%# Eval("WORKING_HOUR")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="item">
                                    <td>
                                        <%#Container.DataItemIndex+1 %>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("RNO") %>' onclick="EnableCredit(this)" />
                                        <asp:HiddenField ID="hidEmployeeNo" runat="server" Value='<%# Eval("RNO") %>' />
                                    </td>
                                    <td>
                                        <%# Eval("NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("WORKING_DATE","{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td>
                                        <%# Eval("WORKING_HOUR")%>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                         </div>
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server">
        <div class="col-12 btn-footer" id="trShow" runat="server">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click"
                Text="Transfer" ValidationGroup="Leave" Visible="False" CssClass="btn btn-primary"/>
            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click"
                Text="Cancel" CssClass="btn btn-warning"/>
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Leave1"
                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                ValidationGroup="Leave" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
        </div>
    </asp:Panel>
            </div>
        </div>
    </div>
</div>


   


</asp:Content>
