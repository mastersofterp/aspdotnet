<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_EmployeeResignationPassingPath.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_EmployeeResignationPassingPath" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnDisplay" runat="server">
        <div class="row">
            <div class="col-md-12">
                <!-- general form elements -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Exit Application for Authority</h3>
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
                                    <div class="box-header with-border">
                                        <h3 class="box-title">General Information</h3>
                                        <div class="box-tools pull-right">
                                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                                <i class="fa fa-minus"></i>
                                            </button>

                                        </div>
                                    </div>
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
                                            <div class="form-group col-md-12" style="display:none">
                                                <div class="form-group col-md-6">
                                                    <asp:RadioButtonList runat="server" ID="rdoSelection" RepeatDirection="Horizontal">
                                                        <%--  <asp:ListItem Value="IDNO" Selected="True"> IDNO</asp:ListItem>--%>
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


                                                <%--<asp:Panel ID="pnlSearch" runat="server" ScrollBars="Auto" Height="300px">--%>
                                                <%--<asp:Panel ID="pnlSearch" runat="server" ScrollBars="Auto" Height="300px" Width="520px">--%>
                                                <asp:ListView ID="ListView1" runat="server">
                                                    <LayoutTemplate>
                                                        <div>

                                                            <%--<h4>Login Details</h4>--%>
                                                            <asp:Panel ID="pnlSearch" runat="server" ScrollBars="Vertical" Height="350px">
                                                                <table class="table table-hover table-bordered">
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

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                            </asp:Panel>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name")%>' CommandArgument='<%# Eval("IDNo")%>' OnClick="lnkId_Click"></asp:LinkButton>
                                                                <%-- --%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SUBDEPT")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SUBDEPT")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SUBDESIG")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
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
                                            <div class="form-group col-md-12">
                                                <div class="form-group col-md-8">
                                                    <br />
                                                    <div class="form-group col-md-6">
                                                        <label>Employee No.</label>
                                                        <asp:Label ID="lblIDNo" runat="server"></asp:Label>
                                                    </div>

                                                  <%--  <div class="form-group col-md-6">
                                                        <label>Employee Code.</label>
                                                        <asp:Label ID="lblEmpcode" runat="server" ></asp:Label>
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

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <div class="form-group col-md-6 text-right">
                                                        <asp:Image ID="imgPhoto" runat="server" Width="234px" Height="250px" Visible="false" Style="margin-left: 2px" />
                                                    </div>
                                                </div>
                                                <%--------- here new code added  by me------%>
                                                <div class="form-group col-md-4">
                                                    <label>Exit Application Date: </label>
                                                    <span style="color: #FF0000">*</span>
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtresigntiondate" Enabled="false" runat="server" onblur="return checkdate(this);"
                                                                    ToolTip="Enter Date" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                              <%--  <div class="input-group-addon">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png"
                                                                        Style="cursor: pointer" />
                                                                </div>--%>
                                                                <%--<ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                                    Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtresigntiondate">
                                                                </ajaxToolKit:CalendarExtender>--%>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtresigntiondate"
                                                                    Display="None" ErrorMessage="Please Month &amp; Year in (dd/MM/yyyy Format)" SetFocusOnError="True"
                                                                    ValidationGroup="payroll">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                        </ContentTemplate>
                                                        <%--   <Triggers >
                                                            <asp:PostBackTrigger ControlID="ddlAuthorityType" />
                                                        </Triggers>--%>
                                                    </asp:UpdatePanel>

                                                    <%--   <div class="form-group col-md-7">
                                                                <label>From Date :</label>
                                                                                            </div>--%>
                                                </div>
                                                <div class="form-group col-md-4">

                                                    <label ><span style="color: red;">*</span> Exit Type :</label>
                                                    <asp:DropDownList ID="ddlExitType" runat="server" CssClass="form-control" Enabled="false"
                                                        AppendDataBoundItems="True" TabIndex="2" ToolTip="Please Select Employee Exit Type">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Resigning</asp:ListItem>
                                                        <asp:ListItem Value="2">Retirement</asp:ListItem>
                                                        <asp:ListItem Value="3">Termination</asp:ListItem>
                                                        <asp:ListItem Value="4">Contract Over</asp:ListItem>
                                                    </asp:DropDownList>
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlExitType"
                                                                InitialValue="0" Display="None" ErrorMessage="Please Select Exit Type" SetFocusOnError="true" ValidationGroup="search" />
                                                </div>
                                                <div class="form-group col-md-4">
                                                    &nbsp;
                                                    <label>Notice Period (In Months) :</label>
                                                   <%-- <asp:Label ID="lblNoticePeriod" runat="server"></asp:Label>--%>
                                                     <asp:TextBox ID="txtNoticePrirod" runat="server" text="0" Enabled="false"
                                                                   CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    
                                                </div>
                                                  <div class="form-group col-md-4" style="display:none">

                                                    <label ><span style="color: red;">*</span> Send to :</label>
                                                    <asp:DropDownList ID="ddlAuthoUser" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="true" ToolTip="Please Select Send to">
                                                       
                                                    </asp:DropDownList>
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAuthoUser"
                                                                InitialValue="0" Display="None" ErrorMessage="Please Select Send to" SetFocusOnError="true" ValidationGroup="search" />
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label>Exit Reason: </label>
                                                    <span style="color: #FF0000">*</span>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtresignationremark" runat="server" ControlToValidate="txtresignationremark" Enabled="false" TextMode="MultiLine" Height="200px" class="form-control"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtresignationremark"
                                                                 Display="None" ErrorMessage="Please Enter Resignation  Remark" SetFocusOnError="true" ValidationGroup="search" />
                                                        </ContentTemplate>
                                                        <%--  <Triggers >
                                                            <asp:PostBackTrigger ControlID="ddlAuthorityName" />
                                                        </Triggers>--%>
                                                    </asp:UpdatePanel>
                                                </div>
                                                 <div class="form-group col-md-12">
                                                    <label>Remark: </label>
                                                    <span style="color: #FF0000">*</span>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtRemark" runat="server"   TextMode="MultiLine" Height="100px" class="form-control"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRemark"
                                                                 Display="None" ErrorMessage="Please Enter Remark" SetFocusOnError="true" ValidationGroup="search" />
                                                        </ContentTemplate>
                                                     
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="form-group col-md-4" >

                                                    <label ><span style="color: red;">*</span> Send to :</label>
                                                    <asp:DropDownList ID="ddlsendto" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="true" ToolTip="Please Select Send to">
                                                       
                                                    </asp:DropDownList>
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlsendto"
                                                                InitialValue="0" Display="None" ErrorMessage="Please Select Send to" SetFocusOnError="true" ValidationGroup="search" />
                                                </div>


                                                <div class="form-group col-md-6">

                                                    <asp:TextBox ID="txtcollegeno" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div class="form-group col-md-12" style="text-align: center">

                                                    <br />
                                                    &nbsp;&nbsp; 
                                                <asp:Button ID="Btnsubmit" runat="server" Text="Submit"
                                                    ValidationGroup="search" CssClass="btn btn-primary" OnClick="Btnsubmit_Click" />&nbsp;
                                                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel"
                                                        CssClass="btn btn-warning" OnClick="BtnCancel_Click" />
                                                    <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="search"
                                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                </div>
                                            </div>
                                            <div class="box-footer" style="display:none">
                                                <div class="form-group col-md-12">
                                                    <asp:ListView ID="lvEmp" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    <h4>
                                                                        <label class="label label-default">Employee Exit Details</label>
                                                                    </h4>
                                                                </div>
                                                                <table id="id1" class="table table-hover table-bordered">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>Action
                                                                            </th>
                                                                            <th>Employee No
                                                                            </th>
                                                                            <th>Name
                                                                            </th>
                                                                            <th>Department
                                                                            </th>
                                                                            <th>Exit Application Date
                                                                            </th>
                                                                            <th>Exit Reason
                                                                            </th>
                                                                            <th>Status
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
                                                                <td>
                                                                    <asp:ImageButton ID="btneditasset" runat="server" OnClick="btneditasset_Click"
                                                                        CommandArgument='<%# Eval("EMPRESIGNATIONID")%>' ImageUrl="~/images/edit.gif" ToolTip="Edit Record" />
                                                                </td>
                                                                <td>
                                                                <%# Eval("EmployeeId")%>
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
                                                                    <asp:HiddenField ID="hdnNotice" runat="server" Value='<%# Eval("REG_NOTICE_PERIOD") %>' />
                                                                    <asp:HiddenField ID="hdnRegDate" runat="server" Value='<%# Eval("RESIGNATIONDATE") %>' />
                                                                    <asp:HiddenField ID="HdnRegRemark" runat="server" Value='<%# Eval("RESIGNATIONREMARK") %>' />
                                                                     <asp:HiddenField ID="hdnexittypeid" runat="server" Value='<%# Eval("ExitTypeId") %>' />
                                                                       <asp:HiddenField ID="hdnREGPASSID" runat="server" Value='<%# Eval("REGPASSID") %>' />
                                                                    
                                                                    
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
</asp:Content>

