<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Emp_No_Dues.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_Emp_Nodues" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="pnDisplay" runat="server">

        <div class="row">
            <div class="col-md-12">
                <!-- general form elements -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">No Dues Entry</h3>
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
                                                        <asp:ListItem Value="IDNO" Selected="True"> IDNO</asp:ListItem>
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
                                                        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                            CssClass="btn btn-info" TabIndex="6" ToolTip="Click here to Search Employee" />
                                                        <asp:Button ID="btnCanceModal" runat="server" Text="Cancel" OnClick="BtnCancelSearch_Click"
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
                                                    <div class="form-group col-md-6">

                                                        <label>ID No.</label>
                                                        <asp:Label ID="lblIDNo" runat="server"></asp:Label>

                                                    </div>

                                                    <div class="form-group col-md-6">
                                                        <label>Employee Id.</label>
                                                        <asp:Label ID="lblEmpcode" runat="server"></asp:Label>
                                                    </div>
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

                                                <div class="form-group col-md-4">
                                                    <label>Dues: </label>
                                                    <span style="color: #FF0000">*</span>
                                                    <asp:DropDownList ID="ddlNoDues" runat="server"
                                                        ValidationGroup="search" AppendDataBoundItems="True" class="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlNoDues"
                                                        InitialValue="0" Display="None" ErrorMessage="Please Select No Dues" SetFocusOnError="true" ValidationGroup="search" />
                                                </div>

                                                <div class="form-group col-md-8">
                                                    <label>Remark: </label>
                                                    <span style="color: #FF0000">*</span>
                                                    <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" class="form-control"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtRemark"
                                                        Display="None" ErrorMessage="Please Enter Remark" SetFocusOnError="true" ValidationGroup="search" />
                                                </div>

                                                <div class="form-group col-md-6" style="display:none">
                                                    <label>Authority Type: </label>
                                                    <span style="color: #FF0000">*</span>
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>


                                                            <asp:DropDownList ID="ddlAuthorityType" runat="server"
                                                                ValidationGroup="search" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlAuthoName_SelectedIndexChanged" class="form-control">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                            </asp:DropDownList>

                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAuthorityType"
                                                                InitialValue="0" Display="None" ErrorMessage="Please Select Authority type" SetFocusOnError="true" ValidationGroup="search" />--%>
                                                        </ContentTemplate>
                                                     <%--   <Triggers >
                                                            <asp:PostBackTrigger ControlID="ddlAuthorityType" />
                                                        </Triggers>--%>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label>Authority Name: </label>
                                                    <span style="color: #FF0000">*</span>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlAuthorityName" runat="server"
                                                                ValidationGroup="search" AppendDataBoundItems="True" class="form-control">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                            </asp:DropDownList>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAuthorityName"
                                                                InitialValue="0" Display="None" ErrorMessage="Please Select Authority Name" SetFocusOnError="true" ValidationGroup="search" />
                                                        </ContentTemplate>
                                                      <%--  <Triggers >
                                                            <asp:PostBackTrigger ControlID="ddlAuthorityName" />
                                                        </Triggers>--%>
                                                    </asp:UpdatePanel>
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


                                            <div class="box-footer">
                                                <div class="form-group col-md-12">
                                                    <asp:ListView ID="lvEmp" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    <h4>
                                                                        <label class="label label-default">No Dues Details</label>
                                                                    </h4>
                                                                </div>
                                                                <table id="id1" class="table table-hover table-bordered">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th style="text-align: center;">Action
                                                                            </th>
                                                                            <th style="text-align: center;">ID No.
                                                                            </th>
                                                                            <th>Name
                                                                            </th>
                                                                            <th style="text-align: center;">Department
                                                                            </th>
                                                                            <th style="text-align: center;">No Dues
                                                                            </th>
                                                                            <th>Remark
                                                                            </th>
                                                                            <th>Authority Type
                                                                            </th>
                                                                            <th>Authority Name
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
                                                                    <asp:ImageButton ID="btnEditNoDues" runat="server" OnClick="btnEditNoDues_Click"
                                                                        CommandArgument='<%# Eval("NODUES_NO")%>' ImageUrl="~/images/edit.gif" ToolTip="Edit Record" />
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
                                                                <td style="text-align: center;">
                                                                    <%# Eval("SUBDEPT")%>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <%# Eval("NODUES_STATUS_DETAILS")%>
                                                                    <asp:Label ID="lblNODUES_STATUS" runat="server" Text='<%# Eval("NODUES_STATUS")%>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("REMARK")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("AUTHORITY_TYP_NAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("AUTHORITY_NAME")%>
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
        </div>

        
    </asp:Panel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

