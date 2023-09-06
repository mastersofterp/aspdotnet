<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_EmployeeResignationAccept.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_EmployeeResignationAccept" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%-- 
    <script src="../../Datatable/jquery-1.12.0.min.js"></script>
    <script src="../../Datatable/jquery.dataTables.min.js"></script>
    <script src="../../Datatable/dataTables.bootstrap.min.js"></script>
    <script src="../../Datatable/dataTables.responsive.min.js"></script>
    <link href="../../Datatable/responsive.bootstrap.min.css" rel="stylesheet" />
    <link href="../../Datatable/dataTables.bootstrap.min.css" rel="stylesheet" />--%>

    <script src="../Datatable/jquery-1.12.0.min.js"></script>
    <script src="../Datatable/jquery.dataTables.min.js"></script>
    <script src="../Datatable/dataTables.bootstrap.min.js"></script>
    <script src="../Datatable/dataTables.responsive.min.js"></script>
    <link href="../Datatable/responsive.bootstrap.min.css" rel="stylesheet" />
    <link href="../Datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../Datatable/responsive.bootstrap.min.css" rel="stylesheet" />

    <style>
        .DocumentList {
            overflow-x: scroll;
            overflow-y: scroll;
            height: 350px;
            width: 100%;
        }
    </style>

    <script type="text/javascript" charset="utf-8">
        var dt = $.noConflict();
        dt(document).ready(function () {
            dt('#example').DataTable();
        });
    </script>

    <asp:Panel ID="pnDisplay" runat="server">
        <div class="row">
            <div class="col-md-12">
                <!-- general form elements -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Exit Application Approval</h3>
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">

                                <div class="box box-primary ">
                                    <%--<div class="box-header with-border">
                                        <h3 class="box-title">General Information</h3>
                                        <div class="box-tools pull-right">
                                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                                <i class="fa fa-minus"></i>
                                            </button>

                                        </div>
                                    </div>--%>
                                    <!-- /.box-header -->
                                    <!-- form start -->
                                    <%--   <form role="form">--%>

                                    <div class="form-group col-md-12">
                                        <%-- <asp:RadioButtonList runat="server" ID="rdoSelection" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="IDNO" Selected="True" > IDNO</asp:ListItem>
                                                         <asp:ListItem Value="NAME" > NAME</asp:ListItem>
                                                         <asp:ListItem Value="RFID" > RFID</asp:ListItem>
                                                        <asp:ListItem Value="PFILENO" > EMPLOYEE CODE</asp:ListItem>
                                                    </asp:RadioButtonList>--%>
                                        <%--      <label>ID No. </label>--%>


                                        <div class="col-md-12" id="DivSerach" runat="server">
                                            <div class="form-group col-md-12" style="display: none">
                                                <div class="form-group col-md-6">
                                                    <asp:RadioButtonList runat="server" ID="rdoSelection" RepeatDirection="Horizontal">
                                                        <%--<asp:ListItem Value="IDNO" Selected="True"> IDNO</asp:ListItem>--%>
                                                        <asp:ListItem Value="EMPLOYEEID" Selected="True"> EMPLOYEE NO.</asp:ListItem>
                                                        <asp:ListItem Value="NAME"> NAME</asp:ListItem>

                                                        <asp:ListItem Value="PFILENO"> EMPLOYEE CODE</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <asp:TextBox ID="txtSearch" runat="server" TabIndex="5" placeholder="Search for..."
                                                        CssClass="form-control" ToolTip="Enter Search String"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <p class="text-center">
                                                        <asp:Button ID="Search" runat="server" Text="Search" OnClick="Search_Click"
                                                            CssClass="btn btn-info" TabIndex="6" ToolTip="Click here to Search Employee" />
                                                        <asp:Button ID="btnCanceModal" runat="server" Text="Cancel" OnClick="btnCanceModal_Click"
                                                            CssClass="btn btn-warning" TabIndex="7" ToolTip="Click here to Reset" />
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <%--    <div class="DocumentList">--%>

                                                <asp:Repeater ID="rpt_RegEmp" runat="server">
                                                    <HeaderTemplate>
                                                        <table id="example" class="table table-striped table-bordered dt-responsive nowrap" width="100%" cellspacing="0">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Name
                                                                    </th>
                                                                    <th>Employee No.
                                                                    </th>
                                                                    <th>Department
                                                                    </th>
                                                                    <th>Designation
                                                                    </th>
                                                                    <th>Exit Status
                                                                    </th>
                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name")%>' CommandArgument='<%# Eval("EMPRESIGNATIONID")%>' OnClick="lnkId_Click"></asp:LinkButton>
                                                                <%-- --%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("EmployeeId")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SUBDEPT")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SUBDESIG")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("REG_STATUS_DETAILS")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody></table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                <%-- </div>--%>

                                                <%--<asp:Panel ID="pnlSearch" runat="server" ScrollBars="Auto" Height="300px">--%>
                                                <%--<asp:Panel ID="pnlSearch" runat="server" ScrollBars="Auto" Height="300px" Width="520px">--%>

                                                <%--</asp:Panel>--%>
                                            </div>
                                            <%--  <div class="input-group  date">
                                            <div class="input-group-addon">
                                                <asp:TextBox ID="txtIdNo" runat="server" ValidationGroup="submit" class="form-control"></asp:TextBox>
                                                <%--<ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtIdNo"
                                                    ValidChars="0123456789" FilterMode="ValidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>--%>
                                            <%--  Enable the button so it can be played again --

                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click"
                                                    ValidationGroup="submit" />
                                                <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtIdNo"
                                                    Display="None" ErrorMessage="Please Enter ID No." ValidationGroup="submit">
                                                </asp:RequiredFieldValidator>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="submit" />
                                            </div>
                                            <asp:TextBox ID="txt" runat="server" Visible="false"></asp:TextBox>
                                        </div>--%>
                                        </div>



                                        <asp:Panel ID="pnlId" runat="server" Visible="false">
                                            <div class="form-group col-md-12 panel panel-success">

                                                <div class="form-group col-md-12 panel panel-success">
                                                    <div class="form-group col-md-8">
                                                              <br />
                                                        <div class="form-group col-md-6">
                                                            <label>Employee No.</label>
                                                            <asp:Label ID="lblIDNo" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblEmployeeNo" runat="server"></asp:Label>

                                                        </div>

                                                      <%--  <div class="form-group col-md-6">
                                                            <label>Employee Code.</label>
                                                            <asp:Label ID="lblEmpcode" runat="server"></asp:Label>
                                                        </div>--%>
                                                        <div class="form-group col-md-6">
                                                            <label>Employee Name :</label>
                                                            <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                            <asp:Label ID="lblFName" runat="server"></asp:Label>
                                                            <asp:Label ID="lblMname" runat="server"></asp:Label>
                                                            <asp:Label ID="lblLname" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label>Date of Joining :</label>
                                                            <asp:Label ID="lblDOJ" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label>Department : </label>
                                                            <asp:Label ID="lblDepart" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label>Designation :</label>
                                                            <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label>Mobile No :</label>
                                                            <asp:Label ID="lblMob" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label>Email :</label>
                                                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label>Exit Status :</label>
                                                            <asp:Label ID="lblRegStatus" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                             <label>Relieved Status :</label>
                                                            <asp:Label ID="lblrelievedstatus" runat="server"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-md-4">
                                                        <div class="form-group col-md-6 text-right">
                                                            <asp:Image ID="imgPhoto" runat="server" Width="234px" Height="250px" Visible="false" Style="margin-left: 2px" />
                                                        </div>
                                                    </div>
                                                    <%--------- here new code added  by me------%>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-4">
                                                        <label>Exit Application Date: </label>
                                                        <span style="color: #FF0000">*</span>

                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtresigntiondate" runat="server" AutoPostBack="true" onblur="return checkdate(this);"
                                                                ToolTip="Enter Month and Year" CssClass="form-control" TabIndex="1" Enabled="false"></asp:TextBox>
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Style="cursor: pointer" />
                                                            </div>
                                                            <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                                Format="yyyy/MM/dd" PopupButtonID="ImaCalStartDate" TargetControlID="txtresigntiondate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtresigntiondate"
                                                                Display="None" ErrorMessage="Please Month &amp; Year in (YYYY/MM/dd Format)" SetFocusOnError="True"
                                                                ValidationGroup="payroll">
                                                            </asp:RequiredFieldValidator>
                                                        </div>


                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Notice Period in Months: </label>
                                                        <%--<span style="color: #FF0000">*</span>--%>
                                                        <asp:TextBox ID="txtNoticePeriod" runat="server" onblur="CalDate(this);" class="form-control" OnTextChanged="txtNoticePeriod_TextChanged" AutoPostBack="true"></asp:TextBox>

                                                        <%--                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNoticePeriod"
                                                        InitialValue="0" Display="None" ErrorMessage="Please Enter Notice Period" SetFocusOnError="true" ValidationGroup="search" />--%>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>No of Day Absent :</label>
                                                        <%-- <asp:Label ID="lblNoDayAbs" runat="server"></asp:Label>--%>
                                                        <asp:TextBox ID="txtNoDayAbs" runat="server"
                                                            CssClass="form-control" TabIndex="1" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-4">

                                                        <label><span style="color: red;">*</span> Exit Type :</label>
                                                        <asp:DropDownList ID="ddlExitType" runat="server" CssClass="form-control"
                                                            AppendDataBoundItems="True" TabIndex="2" ToolTip="Please Select Employee Exit Type">
                                                                     <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Resigning</asp:ListItem>
                                                        <asp:ListItem Value="2">Retirement</asp:ListItem>
                                                        <asp:ListItem Value="3">Termination</asp:ListItem>
                                                        <asp:ListItem Value="4">Contract Over</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlExitType"
                                                            InitialValue="0" Display="None" ErrorMessage="Please Select Exit Type" SetFocusOnError="true" ValidationGroup="search" />
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Current Relieving Date :</label>
                                                        <%--  <asp:Label ID="lblCurrReleveDate" runat="server"></asp:Label>--%>

                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtCurrRelevingDate" runat="server" AutoPostBack="true" onblur="return checkdate(this);"
                                                                ToolTip="Enter Month and Year" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Style="cursor: pointer" />
                                                            </div>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true"
                                                                Format="yyyy/MM/dd" PopupButtonID="Image3" TargetControlID="txtCurrRelevingDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCurrRelevingDate"
                                                                Display="None" ErrorMessage="Please Month &amp; Year in (YYYY/MM/dd Format)" SetFocusOnError="True"
                                                                ValidationGroup="payroll">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>



                                                    <div class="form-group col-md-4">
                                                        <label>Actual Releving Date: </label>
                                                        <%--<span style="color: #FF0000">*</span>--%>

                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtRelevingDate" runat="server" AutoPostBack="true" onblur="return checkdate(this);"
                                                                ToolTip="Enter Month and Year" CssClass="form-control" TabIndex="1" ></asp:TextBox>
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Style="cursor: pointer" />
                                                            </div>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                                Format="yyyy/MM/dd" PopupButtonID="Image2" TargetControlID="txtRelevingDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRelevingDate"
                                                                Display="None" ErrorMessage="Please Month &amp; Year in (YYYY/MM/dd Format)" SetFocusOnError="True"
                                                                ValidationGroup="payroll">
                                                            </asp:RequiredFieldValidator>
                                                        </div>


                                                    </div>
                                                </div>
                                                 <div class="form-group col-md-12">
                                                      <div class="form-group col-md-4">
                                                        <asp:Label ID="lblfinalamount" runat="server" Text="Final Amount:" Font-Bold="true"  Visible="false"></asp:Label>
                                                          
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtfinalAmount" runat="server"
                                                                ToolTip="Enter Final Amount" CssClass="form-control" TabIndex="1" MaxLength="8" Visible="false"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCurrRelevingDate"
                                                                Display="None" ErrorMessage="Enter Final Amount" SetFocusOnError="True"
                                                                ValidationGroup="payroll">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                     </div>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-12">
                                                        <label>Exit Reason: </label>
                                                        <span style="color: #FF0000">*</span>

                                                        <asp:TextBox ID="txtresignationremark" runat="server" ControlToValidate="txtresignationremark" TextMode="MultiLine" Enabled="false" Height="250px" class="form-control"></asp:TextBox>

                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtresignationremark"
                                                            InitialValue="0" Display="None" ErrorMessage="Please Enter Resignation  Remark" SetFocusOnError="true" ValidationGroup="search" />

                                                    </div>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-12">
                                                        <label>Hr Remark: </label>
                                                        <%--<span style="color: #FF0000">*</span>--%>

                                                        <asp:TextBox ID="txtHrRemark" runat="server" TextMode="MultiLine" Height="150px" class="form-control"></asp:TextBox>



                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-6">
                                                            <asp:HiddenField runat="server" ID="hdnempregid" />
                                                            <asp:TextBox ID="txtcollegeno" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <br />
                                                        <div class="form-group col-md-12" style="text-align: center">

                                                            <br />
                                                            &nbsp;&nbsp; 
                                                    <asp:Button ID="BtnAccept" runat="server" Text="Accept"
                                                        ValidationGroup="search" CssClass="btn btn-success" OnClick="BtnAccept_Click" />&nbsp;
                                                    <asp:Button ID="btnReject" runat="server" Text="Reject"
                                                        ValidationGroup="search" CssClass="btn btn-danger" OnClick="BtnRejected_Click" />&nbsp;
                                                     <asp:Button ID="btnReleive" runat="server" Text="Releving"
                                                         ValidationGroup="search" CssClass="btn btn-primary" OnClick="BtnReleving_Click" />&nbsp;
                                                <asp:Button ID="Btnsubmit" runat="server" Text="Submit"
                                                    ValidationGroup="search" CssClass="btn btn-primary" Visible="false" OnClick="Btnsubmit_Click" />&nbsp;
                                                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel"
                                                        CssClass="btn btn-warning" OnClick="BtnCancel_Click" />
                                                            <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="search"
                                                                DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="box-footer">
                                                    <div class="form-group col-md-12" style="display: none">
                                                        <asp:ListView ID="lvEmp" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="vista-grid">
                                                                    <div class="titlebar">
                                                                        <h4>
                                                                            <label class="label label-default">Employee Resignation Details</label>
                                                                        </h4>
                                                                    </div>
                                                                    <table id="id1" class="table table-hover table-bordered">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>Action
                                                                                </th>
                                                                                <th>ID No.
                                                                                </th>
                                                                                <th>Name
                                                                                </th>
                                                                                <th>Department
                                                                                </th>
                                                                                <th>Resignation Date
                                                                                </th>
                                                                                <th>Resignation Reason
                                                                                </th>
                                                                                <th>Status
                                                                                </th>
                                                                                <th>
                                                                                    Relieved Status
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
                                                                <tr class="item">
                                                                    <td style="text-align: center;">
                                                                        <asp:ImageButton ID="btneditasset" runat="server" OnClick="btneditasset_Click"
                                                                            CommandArgument='<%# Eval("EMPRESIGNATIONID")%>' ImageUrl="~/images/edit.gif" ToolTip="Edit Record" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("IDNO")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("TITLE")%>
                                                                        <%# Eval("FNAME")%>
                                                                        <%# Eval("MNAME")%>
                                                                        <%# Eval("LNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SUBDEPT")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("RESIGNATIONDATE")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("RESIGNATIONREMARK")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REG_STATUS_DETAILS")%>
                                                                        <asp:Label ID="lblresignation_STATUS" runat="server" Text='<%# Eval("REG_STATUS")%>' Visible="false"></asp:Label>
                                                                        <asp:HiddenField ID="hdnisappStatus" runat="server" Value='<%# Eval("REG_STATUS_DETAILS") %>' />
                                                                    </td>
                                                                    <td>
                                                                          <%# Eval("REG_STATUS_RELIEVED")%>
                                                                        <asp:Label ID="lblrevievedstatus" runat="server" Text='<%# Eval("IsReleving")%>' Visible="false"></asp:Label>
                                                                        <asp:HiddenField ID="hdnrelievedstatus" runat="server" Value='<%# Eval("REG_STATUS_RELIEVED") %>' />
                                                               
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>

    <script>
        function CalDate(NoticePeriod) {
            var dt = new Date(document.getElementById("txtresigntiondate").innerText);
            dt.setMonth(dt.getMonth() + NoticePeriod);
            document.getElementById("txtRelevingDate").innerText = dt;
        }
      </script>

</asp:Content>

