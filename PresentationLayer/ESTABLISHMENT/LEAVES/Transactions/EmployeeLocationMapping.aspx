<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeLocationMapping.aspx.cs" MasterPageFile="~/SiteMasterPage.master"
    Inherits="ESTABLISHMENT_LEAVES_Transactions_EmployeeLocationMapping" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function ReceiveServerData(arg) {

            if (arg == 0) {

                document.getElementById('ctl00_ContentPlaceHolder1_hdnConfirmvalue').value = confirm('Do yant to Proceed ?');
            }
        }

    </script>

    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)


        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.name.endsWith('chkSelect')) {
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true)
                            e.checked = true;
                        else
                            e.checked = false;
                    }
                }
            }
        }

        function totAllSubjectsLOC(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.name.endsWith('checkLoc')) {
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true)
                            e.checked = true;
                        else
                            e.checked = false;
                    }
                }
            }
        }

        function totAllSubjectsUEMP(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.name.endsWith('chkUN')) {
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true)
                            e.checked = true;
                        else
                            e.checked = false;
                    }
                }
            }
        }

        function totAllSubjectsUnMap(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.name.endsWith('checkUNLoc')) {
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Employee Location Mapping</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Employee Location Mapping</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>College Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                        AutoPostBack="true" ToolTip="Select College Name" > <%-- OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" ErrorMessage="Please Select College" ValidationGroup="Leave1"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control" TabIndex="3" data-select2-enable="true"
                                        AppendDataBoundItems="true" ToolTip="Select Department" AutoPostBack="true" OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <%-- <sup>* </sup>--%>
                                        <label>Staff Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="true" data-select2-enable="true"
                                        AutoPostBack="true" ToolTip="Select Staff Type" OnSelectedIndexChanged="ddlStafftype_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfvStafftype" runat="server" ControlToValidate="ddlStafftype"
                                                        Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Leave"
                                                        SetFocusOnError="true" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStafftype"
                                        Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Leave1"
                                        SetFocusOnError="true" InitialValue="0">
                                    </asp:RequiredFieldValidator>--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>View Mode</label>
                                    </div>
                                    <asp:RadioButtonList ID="radView" runat="server" RepeatDirection="Horizontal" TabIndex="4"
                                        AutoPostBack="true" OnSelectedIndexChanged="radView_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Location Wise&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="1">Employee Wise</asp:ListItem>
                                    </asp:RadioButtonList>
                                     <asp:RequiredFieldValidator ID="rfvView" runat="server" ControlToValidate="radView"
                                        Display="None" ErrorMessage="Please Select View Mode" ValidationGroup="Leave1"
                                        SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="tremp" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Employee</label>
                                    </div>
                                    <asp:DropDownList ID="ddlEmp" runat="server" CssClass="form-control" TabIndex="5" AppendDataBoundItems="true" data-select2-enable="true"
                                        ToolTip="Select Employee" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmp"
                                        Display="None" ErrorMessage="Please Select Employee" ValidationGroup="Leave1"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divLoc" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Location</label>
                                    </div>
                                    <asp:DropDownList ID="ddlLoc" runat="server" CssClass="form-control" TabIndex="6" AppendDataBoundItems="true" data-select2-enable="true"
                                        ToolTip="Select Employee" OnSelectedIndexChanged="ddlLoc_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="rfvLoc" runat="server" ControlToValidate="ddlLoc"
                                        Display="None" ErrorMessage="Please Select Location" ValidationGroup="Leave1"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnMap" runat="server" Text="Show Mapped Details" ValidationGroup="Leave1"
                            CssClass="btn btn-primary" ToolTip="Click here to Show" TabIndex="13" OnClick="btnMap_Click" />
                        <asp:Button ID="btnUnMap" runat="server" Text="Show UnMapped Details" ValidationGroup="Leave1"
                            CssClass="btn btn-primary" ToolTip="Click here to Show" TabIndex="13" OnClick="btnUnMap_Click" />
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" ToolTip="Click here to Transfer"
                            Text="Save for Mapping" ValidationGroup="Leave1" TabIndex="14" OnClick="btnSave_Click" CausesValidation="true" />
                        <asp:Button ID="btnSaveUnMap" runat="server" CssClass="btn btn-primary" ToolTip="Click here to Transfer"
                            Text="Save for UnMapping" ValidationGroup="Leave1" TabIndex="14" OnClick="btnSaveUnMap_Click" />
                        <asp:Button ID="btnCancel" runat="server" CausesValidation="false" TabIndex="15"
                            Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Leave1"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlEmpList" runat="server">
                            <%--<asp:Panel ID="pnlEmpList" runat="server" Height="450px" ScrollBars="Vertical">--%>
                            <asp:ListView ID="lvEmployees" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>List of Employees</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr.No
                                                </th>
                                                <th>
                                                    <asp:CheckBox ID="cbAl" runat="server" onclick="totAllSubjects(this)" />
                                                    Select
                                                </th>
                                                <th>Employee code
                                                </th>
                                                <th>Employee Name
                                                </th>
                                                <th>Designation
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
                                            <%#Container.DataItemIndex+1 %>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                        </td>
                                        <td>
                                            <%# Eval("PFILENO")%>
                                        </td>
                                        <td>
                                            <%# Eval("EMPNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("SUBDESIG")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="pnlLoc" runat="server">
                            <asp:ListView ID="lvLocation" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>List of Location</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr.No
                                                </th>
                                                <th>
                                                    <asp:CheckBox ID="cbLO" runat="server" onclick="totAllSubjectsLOC(this)" />
                                                    Select
                                                </th>
                                                <th>Location Name
                                                </th>
                                                <th>Location Address
                                                </th>
                                                <th>Latitude
                                                </th>
                                                <th>Longitude
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
                                            <%#Container.DataItemIndex+1 %>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="checkLoc" runat="server" ToolTip='<%# Eval("LOCNO") %>' />

                                        </td>
                                        <td>
                                            <%# Eval("LOCATION_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("LOCATION_ADDRESS")%>
                                        </td>
                                        <td>
                                            <%# Eval("LATITUDE")%>
                                        </td>
                                        <td>
                                            <%# Eval("LONGITUDE")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="pnlEmpUnMap" runat="server">
                            <%--<asp:Panel ID="pnlEmpList" runat="server" Height="450px" ScrollBars="Vertical">--%>
                            <asp:ListView ID="lvEmpUnmap" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>List of Employees</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr.No
                                                </th>
                                                <th>
                                                    <asp:CheckBox ID="cbALL" runat="server" onclick="totAllSubjectsUEMP(this)" />
                                                    Select
                                                </th>
                                                <th>Employee code
                                                </th>
                                                <th>Employee Name
                                                </th>
                                                <th>Designation
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
                                            <%#Container.DataItemIndex+1 %>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkUN" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                            <asp:HiddenField ID="hdnEmpMap" runat="server" Value='<%# Eval("MAPID") %>' />
                                        </td>
                                        <td>
                                            <%# Eval("PFILENO")%>
                                        </td>
                                        <td>
                                            <%# Eval("EMPNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("SUBDESIG")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="pnlLocUnMap" runat="server">
                            <asp:ListView ID="lvLocUnMap" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>List of Location</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr.No
                                                </th>
                                                <th>
                                                    <asp:CheckBox ID="chkUNML" runat="server" onclick="totAllSubjectsUnMap(this)" />
                                                    Select
                                                </th>
                                                <th>Location Name
                                                </th>
                                                <th>Location Address
                                                </th>
                                                <th>Latitude
                                                </th>
                                                <th>Longitude
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
                                            <%#Container.DataItemIndex+1 %>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="checkUNLoc" runat="server" ToolTip='<%# Eval("LOCNO") %>' />
                                            <asp:HiddenField ID="hdnLocMap" runat="server" Value='<%# Eval("MAPID") %>' />
                                        </td>
                                        <td>
                                            <%# Eval("LOCATION_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("LOCATION_ADDRESS")%>
                                        </td>
                                        <td>
                                            <%# Eval("LATITUDE")%>
                                        </td>
                                        <td>
                                            <%# Eval("LONGITUDE")%>
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

</asp:Content>





