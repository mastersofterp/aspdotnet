<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AccountPassingAuthorityPath.aspx.cs" Inherits="ACCOUNT_AccountPassingAuthorityPath" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>--%>

    <%-- <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };
    </script>--%>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)


        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>


    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BILL PASSING PATH AUTHORITY </h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <%--<div class="box-tools pull-left">
                                            <b>*Note &nbsp;:&nbsp;&nbsp; Selection of 'Passing Authority 01' is mandatory.</b>
                                        </div>--%>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Institution Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select College Name"
                                                AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select Institute Name" ValidationGroup="PAPath" SetFocusOnError="true"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Department </label>
                                            </div>
                                            <asp:DropDownList ID="ddlDept" runat="server" TabIndex="2"
                                                ToolTip="Select Department" AppendDataBoundItems="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                                Display="None" ErrorMessage="Please Select Department" SetFocusOnError="true"
                                                ValidationGroup="PAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trEmp" runat="server" style="color: #FF0000;" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Employee </label>
                                            </div>
                                            <asp:Label ID="lblEmpName" runat="server"  Font-Bold="true" TabIndex="3"></asp:Label>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Sectional Head 01 </label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA01" runat="server" TabIndex="4" ToolTip="Select Sectional Head 01" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlPA01_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPA01" runat="server" ControlToValidate="ddlPA01"
                                                Display="None" ErrorMessage="Please select Passing Authority 01" SetFocusOnError="true"
                                                ValidationGroup="PAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Sectional Head 02</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA02" runat="server" AppendDataBoundItems="true" TabIndex="5" ToolTip="Select Sectional Head 02" CssClass="form-control" data-select2-enable="true"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA02_SelectedIndexChanged1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Sectional Head 03 </label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA03" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="6" ToolTip="Select Sectional Head 03"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA03_SelectedIndexChanged1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Sectional Head 04 </label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA04" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="7" ToolTip="Select Sectional Head 04"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA04_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Sectional Head 05 </label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA05" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="6" ToolTip="Select Sectional Head 05"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA05_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Path </label>
                                            </div>
                                            <asp:TextBox ID="txtPAPath" runat="server" CssClass="form-control" ReadOnly="true" TextMode="MultiLine"
                                                TabIndex="7" ToolTip="Path"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12">
                                <asp:Panel ID="pnlEmpList" runat="server">
                                    <div class="sub-heading">
                                        <h5>Employees</h5>
                                    </div>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Employee Name </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlemp" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Employee Name"
                                                        AppendDataBoundItems="true" TabIndex="1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvemp" runat="server" ControlToValidate="ddlemp"
                                                        Display="None" ErrorMessage="Please Select Employee Name" ValidationGroup="PAPath" SetFocusOnError="true"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlEmp" runat="server" Visible="false">
                                        <asp:ListView ID="lvEmployees" runat="server">
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl"
                                                    Text="Employee Not Found!" />
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div id="listViewGrid">
                                                    <div class="sub-heading">
                                                        <h5>List of Employees</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Sr.No
                                                                </th>
                                                                <th>
                                                                    <asp:CheckBox ID="cbAl" runat="server" onclick="totAllSubjects(this)" TabIndex="8" />
                                                                </th>
                                                                <th>Employee Name
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
                                                        <%#Container.DataItemIndex+1 %>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkIdNo" runat="server" ToolTip='<%# Eval("IDNO") %>' TabIndex="9" />

                                                        <asp:HiddenField ID="hidEmployeeNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" ToolTip="Click Add New To Enter LEAVE Passing Authority/Sectional Heads Path" Text="Add New" TabIndex="10" CssClass="btn btn-primary"></asp:LinkButton>
                                    <asp:Button ID="btnShowReport" TabIndex="11" runat="server" Text="Show Report" CssClass="btn btn-info"
                                        OnClick="btnShowReport_Click" ToolTip="Click here to Show the report" Style="display: none" />
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Panel ID="pnlbtn" runat="server">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" TabIndex="12" ToolTip="Click here to Submit" ValidationGroup="PAPath"
                                        OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnBack" runat="server" TabIndex="14" Text="Back" ToolTip="Click here to go back to previous" CausesValidation="false" OnClick="btnBack_Click"
                                        CssClass="btn btn-primary" />
                                    <asp:Button ID="btnCancel" runat="server" TabIndex="13" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PAPath"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </asp:Panel>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlPAPaList" runat="server">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table2">
                                        <asp:Repeater ID="lvPAPath" runat="server">
                                            <HeaderTemplate>
                                              <%--  <div class="sub-heading">
                                                    <h5>Bill Passing Path Authority</h5>
                                                </div>--%>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action</th>
                                                        <th>Department
                                                        </th>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Sectional Head 01
                                                        </th>
                                                        <th>Sectional Head 02
                                                        </th>
                                                        <th>Sectional Head 03
                                                        </th>
                                                        <th>Sectional Head 04
                                                        </th>
                                                        <th>Sectional Head 05
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PAPNO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="15" />
                                                        <asp:ImageButton ID="btnDelete" Visible="false" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("PAPNO") %>'
                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                            OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUBDEPT")%>                                                                 
                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PANAME1")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PANAME2")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PANAME3")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PANAME4")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PANAME5")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
        </Triggers>
    </asp:UpdatePanel>

    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
