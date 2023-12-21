<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PA_Path.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_PA_Path" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

    <%-- <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };
    </script>

    <script type="text/javascript">
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
                if (e.name.endsWith('chkIdNo')) {
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true)
                            e.checked = true;
                        else
                            e.checked = false;
                    }
                }
            }
        }

    </script>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">LEAVE PASSING AUTHORITY PATH</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Leave Passing Authority/ Path</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-8 col-md-10 col-12">
                                            <div class=" note-div">
                                                <h5 class="heading">Note</h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Selection of 'Sectional Head 01' is mandatory.</span></p>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Please Tick Mark Required Copy same path to OD Application if Authority Path is Same for OD & Leave.</span></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College Name" data-select2-enable="true"
                                                AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College Name" ValidationGroup="PAPath" SetFocusOnError="true"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDept" runat="server" TabIndex="2" data-select2-enable="true"
                                                ToolTip="Select Department" AppendDataBoundItems="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"
                                                CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                                Display="None" ErrorMessage="Please Select Department" SetFocusOnError="true"
                                                ValidationGroup="PAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trEmp" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Employee</label>
                                            </div>
                                            <%--<asp:Label ID="lblEmpName" runat="server" class="form-control" Font-Bold="true" TabIndex="3"></asp:Label>--%>
                                            <asp:TextBox ID="lblEmpName" runat="server" class="form-control" Font-Bold="true" TabIndex="3" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Sectional Head 01</label>
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

                                        <%--<div class="text-center">
                                                            <asp:Image ID="darrow1" runat="server" ImageUrl="~/Images/action_down.png" Height="20px" Width="20px" />
                                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Sectional Head 02</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA02" runat="server" AppendDataBoundItems="true" TabIndex="5" ToolTip="Select Sectional Head 02" CssClass="form-control" data-select2-enable="true"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA02_SelectedIndexChanged1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <%--<div class="text-center">
                                                            <asp:Image ID="darrow2" runat="server" ImageUrl="~/Images/action_down.png" Height="20px" Width="20px" />
                                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Sectional Head 03</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA03" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="6" ToolTip="Select Sectional Head 03" data-select2-enable="true"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA03_SelectedIndexChanged1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <%--<div class="text-center">
                                                            <asp:Image ID="daarow3" runat="server" ImageUrl="~/Images/action_down.png" Height="20px" Width="20px" />
                                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Sectional Head 04</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA04" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="7" ToolTip="Select Sectional Head 04" data-select2-enable="true"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA04_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <%--<div class="text-center">
                                                            <asp:Image ID="darrow4" runat="server" ImageUrl="~/Images/action_down.png" Height="20px" Width="20px" />
                                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Sectional Head 05</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPA05" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="8" ToolTip="Select Sectional Head 05" data-select2-enable="true"
                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA05_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Path</label>
                                            </div>
                                            <asp:TextBox ID="txtPAPath" runat="server" CssClass="form-control" ReadOnly="true" TextMode="MultiLine"
                                                Height="40px" TabIndex="9" ToolTip="Path"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divcopypath" runat="server">
                                            <div class="label-dynamic">
                                                <label>Copy Same Path For OD Application</label>
                                            </div>
                                            <asp:CheckBox ID="chkcopypath" runat="server" CssClass="form-control" Checked="false" />
                                        </div>

                                        <asp:Panel ID="pnlLeaveWisePath" runat="server" Visible="false">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Leave Wise Path</h5>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-md-12">
                                                        <div class="label_dynamic">
                                                            <label>Leave Name :<span style="color: #FF0000">*</span> </label>
                                                        </div>
                                                        <asp:CheckBoxList ID="chkleave" runat="server" AppendDataBoundItems="true"
                                                            ToolTip="Please Select" RepeatDirection="Horizontal" RepeatColumns="4">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>


                                    </div>

                                    <%--<div class="form-group col-md-6">
                                                    </div>--%>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlEmpList" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Employees</h5>
                                    </div>
                                    <asp:Panel ID="pnlEmp" runat="server">
                                        <asp:ListView ID="lvEmployees" runat="server">
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl"
                                                    Text="Employee Not Found!" CssClass="d-block text-center mt-3" />
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div id="listViewGrid">
                                                    <div class="sub-heading">
                                                        <h5>List of Employees</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                        <%--class="datatable"--%>
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
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item">
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
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" ToolTip="Click Add New To Enter LEAVE Passing Authority/Sectional Heads Path" Text="Add New" TabIndex="10" CssClass="btn btn-primary"></asp:LinkButton>
                                    <asp:Button ID="btnShowReport" TabIndex="11" runat="server" Text="Show Report" CssClass="btn btn-info"
                                        OnClick="btnShowReport_Click" ToolTip="Click here to Show the report" />
                                </asp:Panel>
                            </div>
                            <asp:Panel ID="pnlbtn" runat="server">
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" TabIndex="12" ToolTip="Click here to Submit" ValidationGroup="PAPath"
                                        OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnBack" runat="server" TabIndex="13" Text="Back" ToolTip="Click here to go back to previous" CausesValidation="false" OnClick="btnBack_Click"
                                        CssClass="btn btn-primary" />
                                    <asp:Button ID="btnCancel" runat="server" TabIndex="14" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PAPath"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <div class="col-md-12">
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12">
                                <asp:Panel ID="pnlPAPaList" runat="server">
                                    <asp:ListView ID="lvPAPath" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter Leaves" CssClass="d-block text-center mt-3" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Leave Passing Authority/ Sectional Head Path</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
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
                                                        <th id="divLvName" runat="server" visible="false">Leave Name
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
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
                                                <td id="tdLName" runat="server" visible="false">
                                                    <%# Eval("Leave_Name")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnBack" />
            <asp:PostBackTrigger ControlID="ddlCollege" />
            <asp:PostBackTrigger ControlID="ddlDept" />
            <asp:PostBackTrigger ControlID="ddlPA01" />
            <asp:PostBackTrigger ControlID="ddlPA02" />
            <asp:PostBackTrigger ControlID="ddlPA03" />
            <asp:PostBackTrigger ControlID="ddlPA04" />
        </Triggers>
    </asp:UpdatePanel>

    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
