<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Monthly_Changes_Master_File_Bulk.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_Monthly_Changes_Master_File_Bulk" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../../JAVASCRIPTS/jquery-1.5.1.js"></script>--%>
    <%-- <script src="../../JAVASCRIPTS/jquery-1.6.4.min.js" type="text/javascript"></script>--%>
    <link href="../../Css/jquery-ui.css" rel="stylesheet" />

    <script src="../../JAVASCRIPTS/jquery-1.6.4.min.js" type="text/javascript"></script>

    <%--<style>
        .DocumentList {
            overflow-x: scroll;
            overflow-y: scroll;
            
        }
    </style>--%>
    <%-- Flash the text/border red and fade in the "close" button --%>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">MONTHLY CHANGES IN MASTERFILE WITH MULTIPLE HEADS</h3>
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
                            <asp:Panel ID="pnlSelect" runat="server">

                                  <div class="panel panel-info">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Select PayHead/Staff</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                               <%-- <div class="panel-heading">Select PayHead/Staff</div>--%>
                                    <div class="panel-body">

                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-md-10" style="display: none">
                                            <label>Pay Head :<span style="color: Red">*</span></label>
                                            <asp:DropDownList ID="ddlPayhead" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Select Pay Head"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlPayhead_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--  <asp:RequiredFieldValidator ID="rfvPayhead" runat="server" ControlToValidate="ddlPayhead"
                                                        Display="None" ErrorMessage="Please Select PayHead" ValidationGroup="payroll"
                                                        InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-md-10" style="display: none">
                                            <label>Sub Pay Head :</label>
                                            <asp:DropDownList ID="ddlSubPayHead" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                                TabIndex="1" ToolTip="Select Sub Pay Head" AutoPostBack="True" OnSelectedIndexChanged="ddlSubPayHead_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>College :<span style="color: Red">*</span></label>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="2" ToolTip="Select College"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                ValidationGroup="payroll" ErrorMessage="Please Select College" SetFocusOnError="true"
                                                InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                        </div>
                                        <div id="Div2" runat="server" visible="true" class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Department :</label>


                                            <asp:DropDownList ID="ddlDeptmon" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="4"
                                                ToolTip="Select Department"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlDeptmon_SelectedIndexChanged">

                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <%-- </div>--%>
                                        <%--<div class="form-group col-md-4">--%>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <%--<label>Scheme :</label>--%>
                                            <label>Scheme/Staff</label>
                                            <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="3"
                                                ToolTip="Select Scheme"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">

                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--  <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                        Display="None" ErrorMessage="Please Select Scheme" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Order By :<span style="color: Red">*</span></label>
                                            <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="5"
                                                ToolTip="Select Order By" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">IDNO</asp:ListItem>
                                                <asp:ListItem Value="2">SEQUENCE NO</asp:ListItem>
                                                <asp:ListItem Value="3">Employee Code</asp:ListItem>
                                                <asp:ListItem Value="4">Name</asp:ListItem>
                                                <asp:ListItem Value="5">EmployeeId</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlorderby"
                                                Display="None" ErrorMessage="Select Order" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div id="Div3" runat="server" visible="false" class="form-group col-md-10">
                                            <label>Select Rule </label>
                                            <asp:DropDownList ID="ddlpayruleselect" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="6"
                                                ToolTip="Select Rule"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlpayruleselect_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlpayruleselect"
                                                        Display="None" ErrorMessage="Select Rule" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <%-- </div>
                                            <div class="form-group col-md-4">--%>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <%--<label>Staff :</label>--%>
                                            <label>Employee Type</label>
                                            <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control"
                                                AppendDataBoundItems="True" TabIndex="4" ToolTip="Select Employee Type">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Pay Head Type :</label>
                                            <asp:DropDownList ID="ddlHeadType" runat="server" CssClass="form-control"
                                                AppendDataBoundItems="True" TabIndex="4" ToolTip="Select Head type" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlHeadType_SelectedIndexChanged">
                                                <asp:ListItem Value="I"> INCOME</asp:ListItem>
                                                <asp:ListItem Value="D"> DEDUCTION</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                        </div>
                    </div>
                    <div class="col-md-12 text-center">

                        <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="payroll" CssClass="btn btn-info" TabIndex="7" ToolTip="Click To Show"
                            OnClick="btnShow_Click" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="payroll"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />



                    </div>

                    </asp:Panel>
                        
                            <div class="text-center">
                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                            </div>

                    <asp:Panel ID="pnlMonthlyChanges" runat="server">

                        <%-- <table class="table table-bordered table-hover table-responsive" >
                            <tr>
                                <td style="font-family: Verdana; color: #fff; background-color: #006595; width:90%; height:20%;">
                                    <div class="col-md-2">
                                        Total Employees =
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtEmpoyeeCount" BackColor="#006595" ForeColor="White" runat="server"
                                            BorderStyle="None"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtPayheadName" BackColor="#006595" ForeColor="White" runat="server"
                                            BorderStyle="None" Visible="false"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtAmount" BackColor="#006595" ForeColor="White" runat="server"
                                            BorderStyle="None"></asp:TextBox>
                                    </div>



                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                </td>
                            </tr>
                        </table>--%>
                       
                            <%-- <div>
                                <div style="font-family: Verdana; color: #fff; background-color: #006595; width:90%; height:20%;">--%>

                              <div class="col-10">
                                <div class="row">
                                    <%-- Total Employees =--%>
                                      <div class="col-md-2">
                                      <label>Total Employees=</label>
                                      </div>
                                    <div class="col-md-3" >

                                        <asp:TextBox ID="txtEmpoyeeCount" runat="server" BorderStyle="None" Visible="true"></asp:TextBox>
                                        
                                    <%--BackColor="#006595" ForeColor="White" runat="server"--%>
                                    </div>
                                   
                                    <div class="col-md-3" >
                                        <asp:TextBox ID="txtPayheadName" runat="server"
                                            BorderStyle="None" Visible="false"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtAmount" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                           </div>
                       

                        <asp:HiddenField ID="hidPayhead" runat="server" />
                        <%-- </div>
                            </div>--%>
                        </div>
                        <div id="divbutton" runat="server" visible="false" class="text-center form-group">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" CssClass="btn btn-primary" TabIndex="9" ToolTip="Click To Save"
                                OnClick="btnSub_Click" OnClientClick="return ConfirmMessage();" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="10" ToolTip="Click To Reset" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                        <div class="table table-responsive ">

                            <div id="divSumofAllHeads" runat="server" style="width: 1500px">
                                <asp:ListView ID="lvSumIncome" runat="server">
                                    <EmptyDataTemplate>
                                        <br />
                                        <center>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></center>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div style="width: 100%">
                                            <table style="margin-bottom: 0px; width: 1500px" class="table table-hover table-bordered">
                                                <thead>
                                                    <tr class="bg-light-blue" style="width: 1500px">

                                                        <th style="width: 410px; text-align: center">
                                                        Sum of Individual Heads instead
                                                                <th style="width: 70px">
                                                                    <asp:Label ID="lblEH1" runat="server"></asp:Label>
                                                                </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH2" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH3" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH4" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH5" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH6" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH7" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH8" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH9" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH10" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH11" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH12" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH13" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH14" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH15" runat="server"></asp:Label>
                                                        </th>

                                                    </tr>
                                                </thead>
                                            </table>
                                        </div>
                                        <div id="demo-grid" style="overflow-x: hidden; overflow-y: hidden;">
                                            <%--class="vista-grid"--%>
                                            <table class="table table-bordered table-hover">
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" style="text-align: left" />
                                                </tbody>
                                            </table>
                                        </div>

                                    </LayoutTemplate>
                                    <ItemTemplate>


                                        <tr>


                                            <td style="width: 410px; text-align: center">
                                                <asp:Label ID="lblIname" runat="server" Text=' <%#Eval("NAME")%>'></asp:Label>

                                            </td>


                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI1" runat="server" MaxLength="10" Text='<%#Eval("I1")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>

                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI2" runat="server" MaxLength="10" Text='<%#Eval("I2")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />


                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI3" runat="server" MaxLength="10" Text='<%#Eval("I3")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />


                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI4" runat="server" MaxLength="10" Text='<%#Eval("I4")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />


                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI5" runat="server" MaxLength="10" Text='<%#Eval("I5")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />


                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI6" runat="server" MaxLength="10" Text='<%#Eval("I6")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />


                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI7" runat="server" MaxLength="10" Text='<%#Eval("I7")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />




                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI8" runat="server" MaxLength="10" Text='<%#Eval("I8")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />


                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI9" runat="server" MaxLength="10" Text='<%#Eval("I9")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI10" runat="server" MaxLength="10" Text='<%#Eval("I10")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />


                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI11" runat="server" MaxLength="10" Text='<%#Eval("I11")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />



                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI12" runat="server" MaxLength="10" Text='<%#Eval("I12")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />


                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI13" runat="server" MaxLength="10" Text='<%#Eval("I13")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />


                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI14" runat="server" MaxLength="10" Text='<%#Eval("I14")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />


                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI15" runat="server" MaxLength="10" Text='<%#Eval("I15")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />



                                            </td>

                                        </tr>


                                    </ItemTemplate>

                                </asp:ListView>

                            </div>
                        </div>
                        <div class="table table-responsive ">
                            <div id="DivIncome" runat="server" style="width: 1500px">
                                <asp:ListView ID="lvMonthlyChanges" runat="server" OnItemDataBound="LvMonthlyChangesIncome_ItemDatabound">
                                    <EmptyDataTemplate>
                                        <br />
                                        <center>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></center>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div style="width: 100%">
                                            <table style="margin-bottom: 0px; width: 1500px" class="table table-hover table-bordered">
                                                <thead>
                                                    <tr class="bg-light-blue" style="width: 1500px">
                                                        <th style="width: 60px">
                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="totalAppointment(this)" />
                                                        </th>
                                                        <th style="width: 50px">EmpID 
                                                        </th>
                                                        <th style="width: 300px">Employee   Name
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH1" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH2" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH3" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH4" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH5" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH6" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH7" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH8" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH9" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH10" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH11" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH12" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH13" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH14" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblEH15" runat="server"></asp:Label>


                                                            <asp:TextBox ID="txtAllAmt" runat="server" Visible="false" ForeColor="Black" class="input" onkeyup="SetNo(this.value)">
                                                            </asp:TextBox>

                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                                                Enabled="True"
                                                                ValidChars="0123456789."
                                                                FilterMode="ValidChars"
                                                                FilterType="Custom"
                                                                TargetControlID="txtAllAmt">
                                                            </ajaxToolKit:FilteredTextBoxExtender>

                                                        </th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </div>
                                        <div id="demo-grid" style="overflow-x: hidden; overflow-y: scroll; height: 600px;">
                                            <%--class="vista-grid"--%>
                                            <table class="table table-bordered table-hover">
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" style="text-align: left" />
                                                </tbody>
                                            </table>
                                        </div>

                                    </LayoutTemplate>
                                    <ItemTemplate>


                                        <tr>

                                            <td style="width: 60px">
                                                <asp:CheckBox ID="chkIno" CssClass="checkbox-primary" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                            </td>

                                            <td style="width: 50px">
                                                <%#Eval("PFILENO")%>
                                            </td>

                                            <td style="width: 300px">
                                                <asp:Label ID="lblIname" runat="server" Text=' <%#Eval("NAME")%>'></asp:Label>

                                            </td>


                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI1" runat="server" MaxLength="10" Text='<%#Eval("I1")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                    TargetControlID="txtI1"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>


                                                <asp:RequiredFieldValidator ID="rfvDays" runat="server" ControlToValidate="txtI1"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>

                                            </td>

                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI2" runat="server" MaxLength="10" Text='<%#Eval("I2")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    TargetControlID="txtI2"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtI2"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>

                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI3" runat="server" MaxLength="10" Text='<%#Eval("I3")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>


                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                    TargetControlID="txtI3"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtI3"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>

                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI4" runat="server" MaxLength="10" Text='<%#Eval("I4")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                    TargetControlID="txtI4"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtI4"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>

                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI5" runat="server" MaxLength="10" Text='<%#Eval("I5")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                    TargetControlID="txtI5"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtI5"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>

                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI6" runat="server" MaxLength="10" Text='<%#Eval("I6")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                    TargetControlID="txtI6"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>


                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtI6"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>

                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI7" runat="server" MaxLength="10" Text='<%#Eval("I7")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server"
                                                    TargetControlID="txtI7"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtI7"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>

                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI8" runat="server" MaxLength="10" Text='<%#Eval("I8")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                    TargetControlID="txtI8"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtI8"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>

                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI9" runat="server" MaxLength="10" Text='<%#Eval("I9")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server"
                                                    TargetControlID="txtI9"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtI9"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>

                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI10" runat="server" MaxLength="10" Text='<%#Eval("I10")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server"
                                                    TargetControlID="txtI10"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtI10"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>

                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI11" runat="server" MaxLength="10" Text='<%#Eval("I11")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                    TargetControlID="txtI11"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtI11"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>

                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI12" runat="server" MaxLength="10" Text='<%#Eval("I12")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                                    TargetControlID="txtI12"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtI12"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>

                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI13" runat="server" MaxLength="10" Text='<%#Eval("I13")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                    TargetControlID="txtI13"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtI13"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>

                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI14" runat="server" MaxLength="10" Text='<%#Eval("I14")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                    TargetControlID="txtI14"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtI14"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>

                                            </td>
                                            <td style="width: 70px">

                                                <asp:TextBox ID="txtI15" runat="server" MaxLength="10" Text='<%#Eval("I15")%>' TabIndex="8"
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHead();" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <%----%>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                                    TargetControlID="txtI15"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtI15"
                                                    Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>


                                                <asp:Label runat="server" ID="lblI1" Visible="false" Text='<%# Eval("API1")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblI2" Visible="false" Text='<%# Eval("API2")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblI3" Visible="false" Text='<%# Eval("API3")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblI4" Visible="false" Text='<%# Eval("API4")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblI5" Visible="false" Text='<%# Eval("API5")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblI6" Visible="false" Text='<%# Eval("API6")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblI7" Visible="false" Text='<%# Eval("API7")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblI8" Visible="false" Text='<%# Eval("API8")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblI9" Visible="false" Text='<%# Eval("API9")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblI10" Visible="false" Text='<%# Eval("API10")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblI11" Visible="false" Text='<%# Eval("API11")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblI12" Visible="false" Text='<%# Eval("API12")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblI13" Visible="false" Text='<%# Eval("API13")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblI14" Visible="false" Text='<%# Eval("API14")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblI15" Visible="false" Text='<%# Eval("API15")%>'> </asp:Label>

                                                <asp:Label runat="server" ID="lblCk1" Visible="false" Text='<%# Eval("CHKI1")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCk2" Visible="false" Text='<%# Eval("CHKI2")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCk3" Visible="false" Text='<%# Eval("CHKI3")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCk4" Visible="false" Text='<%# Eval("CHKI4")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCk5" Visible="false" Text='<%# Eval("CHKI5")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCk6" Visible="false" Text='<%# Eval("CHKI6")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCk7" Visible="false" Text='<%# Eval("CHKI7")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCk8" Visible="false" Text='<%# Eval("CHKI8")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCk9" Visible="false" Text='<%# Eval("CHKI9")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCk10" Visible="false" Text='<%# Eval("CHKI10")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCk11" Visible="false" Text='<%# Eval("CHKI11")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCk12" Visible="false" Text='<%# Eval("CHKI12")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCk13" Visible="false" Text='<%# Eval("CHKI13")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCk14" Visible="false" Text='<%# Eval("CHKI14")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCk15" Visible="false" Text='<%# Eval("CHKI15")%>'> </asp:Label>

                                            </td>

                                        </tr>


                                    </ItemTemplate>

                                </asp:ListView>

                            </div>
                        </div>


                        <div class="table table-responsive ">
                            <div id="Div4" runat="server" style="width: 2500px">

                                <asp:ListView ID="lvSumDiduction" runat="server" Visible="false">
                                    <EmptyDataTemplate>
                                        <br />
                                        <center>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></center>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>

                                        <div style="width: 100%">

                                            <table class="table table-hover table-bordered" style="margin-bottom: 0px; width: 2500px">
                                                <thead>
                                                    <tr class="bg-light-blue" style="width: 2500px">


                                                        <th style="width: 410px">Sum of Individual Heads instead 
                                                        </th>

                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH1" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH2" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH3" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH4" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH5" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH6" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH7" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH8" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH9" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH10" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH11" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH12" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH13" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH14" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH15" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH16" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH17" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH18" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH19" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH20" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH21" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH22" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH23" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH24" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH25" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH26" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH27" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH28" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH29" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH30" runat="server"></asp:Label>
                                                        </th>

                                                        <th></th>
                                                    </tr>
                                                </thead>
                                            </table>

                                        </div>
                                        <div id="demo-grid" style="overflow-x: hidden; overflow-y: hidden;">
                                            <table class="table table-bordered table-hover">
                                                <tbody>

                                                    <tr id="itemPlaceholder" runat="server" style="text-align: left" />
                                                </tbody>
                                            </table>
                                        </div>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>




                                            <td style="width: 410px">
                                                <asp:Label ID="lblDname" runat="server" Text=' <%#Eval("NAME")%>'></asp:Label>
                                            </td>

                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD1" runat="server" MaxLength="10" Text='<%#Eval("D1")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD2" runat="server" MaxLength="10" Text='<%#Eval("D2")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD3" runat="server" MaxLength="10" Text='<%#Eval("D3")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD4" runat="server" MaxLength="10" Text='<%#Eval("D4")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD5" runat="server" MaxLength="10" Text='<%#Eval("D5")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD6" runat="server" MaxLength="10" Text='<%#Eval("D6")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD7" runat="server" MaxLength="10" Text='<%#Eval("D7")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD8" runat="server" MaxLength="10" Text='<%#Eval("D8")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD9" runat="server" MaxLength="10" Text='<%#Eval("D9")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD10" runat="server" MaxLength="10" Text='<%#Eval("D10")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD11" runat="server" MaxLength="10" Text='<%#Eval("D11")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD12" runat="server" MaxLength="10" Text='<%#Eval("D12")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD13" runat="server" MaxLength="10" Text='<%#Eval("D13")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD14" runat="server" MaxLength="10" Text='<%#Eval("D14")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD15" runat="server" MaxLength="10" Text='<%#Eval("D15")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD16" runat="server" MaxLength="10" Text='<%#Eval("D16")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD17" runat="server" MaxLength="10" Text='<%#Eval("D17")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD18" runat="server" MaxLength="10" Text='<%#Eval("D18")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD19" runat="server" MaxLength="10" Text='<%#Eval("D19")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD20" runat="server" MaxLength="10" Text='<%#Eval("D20")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD21" runat="server" MaxLength="10" Text='<%#Eval("D21")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD22" runat="server" MaxLength="10" Text='<%#Eval("D22")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD23" runat="server" MaxLength="10" Text='<%#Eval("D23")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD24" runat="server" MaxLength="10" Text='<%#Eval("D24")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD25" runat="server" MaxLength="10" Text='<%#Eval("D25")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD26" runat="server" MaxLength="10" Text='<%#Eval("D26")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />



                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD27" runat="server" MaxLength="10" Text='<%#Eval("D27")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD28" runat="server" MaxLength="10" Text='<%#Eval("D28")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />


                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD29" runat="server" MaxLength="10" Text='<%#Eval("D29")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />


                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD30" runat="server" MaxLength="10" Text='<%#Eval("D30")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" />

                                            </td>

                                        </tr>
                                    </ItemTemplate>

                                </asp:ListView>
                            </div>
                        </div>

                        <div class="table table-responsive ">
                            <div id="DivDeduction" runat="server" style="width: 2500px">

                                <asp:ListView ID="lvMonthlyChangesDeduction" runat="server" OnItemDataBound="LvMonthlyChangesDeduction_ItemDatabound">
                                    <EmptyDataTemplate>
                                        <br />
                                        <center>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></center>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>

                                        <div style="width: 100%">

                                            <table class="table table-hover table-bordered" style="margin-bottom: 0px; width: 2500px">
                                                <thead>
                                                    <tr class="bg-light-blue" style="width: 2500px">

                                                        <th style="width: 60px">
                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="totalAppointment(this)" />
                                                        </th>
                                                        <th style="width: 50px">EmpID
                                                        </th>

                                                        <th style="width: 300px">Name
                                                        </th>

                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH1" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH2" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH3" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH4" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH5" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH6" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH7" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH8" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH9" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH10" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH11" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH12" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH13" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH14" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH15" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH16" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH17" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH18" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH19" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH20" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH21" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH22" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH23" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH24" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH25" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH26" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH27" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH28" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH29" runat="server"></asp:Label>
                                                        </th>
                                                        <th style="width: 70px">
                                                            <asp:Label ID="lblDH30" runat="server"></asp:Label>
                                                        </th>

                                                        <th></th>
                                                    </tr>
                                                </thead>
                                            </table>

                                        </div>
                                        <div id="demo-grid" style="overflow-x: hidden; overflow-y: scroll; height: 600px;">
                                            <table class="table table-bordered table-hover">
                                                <tbody>

                                                    <tr id="itemPlaceholder" runat="server" style="text-align: left" />
                                                </tbody>
                                            </table>
                                        </div>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>


                                            <td style="width: 70px">
                                                <asp:CheckBox ID="chkIno" CssClass="checkbox-primary" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                            </td>
                                            <td style="width: 50px">
                                                <%#Eval("PFILENO")%>
                                            </td>

                                            <td style="width: 350px">
                                                <asp:Label ID="lblDname" runat="server" Text=' <%#Eval("NAME")%>'></asp:Label>
                                            </td>



                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD1" runat="server" MaxLength="10" Text='<%#Eval("D1")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                                    TargetControlID="txtD1"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="rfvDays" runat="server" ControlToValidate="txtD1" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD2" runat="server" MaxLength="10" Text='<%#Eval("D2")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server"
                                                    TargetControlID="txtD2"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtD2" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD3" runat="server" MaxLength="10" Text='<%#Eval("D3")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server"
                                                    TargetControlID="txtD3"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtD3" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD4" runat="server" MaxLength="10" Text='<%#Eval("D4")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server"
                                                    TargetControlID="txtD4"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtD4" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD5" runat="server" MaxLength="10" Text='<%#Eval("D5")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtD5" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server"
                                                    TargetControlID="txtD5"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD6" runat="server" MaxLength="10" Text='<%#Eval("D6")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtD6" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server"
                                                    TargetControlID="txtD6"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD7" runat="server" MaxLength="10" Text='<%#Eval("D7")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtD7" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server"
                                                    TargetControlID="txtD7"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD8" runat="server" MaxLength="10" Text='<%#Eval("D8")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtD8" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server"
                                                    TargetControlID="txtD8"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD9" runat="server" MaxLength="10" Text='<%#Eval("D9")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtD9" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server"
                                                    TargetControlID="txtD9"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD10" runat="server" MaxLength="10" Text='<%#Eval("D10")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtD10" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server"
                                                    TargetControlID="txtD10"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD11" runat="server" MaxLength="10" Text='<%#Eval("D11")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtD11" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server"
                                                    TargetControlID="txtD11"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD12" runat="server" MaxLength="10" Text='<%#Eval("D12")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtD12" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server"
                                                    TargetControlID="txtD12"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD13" runat="server" MaxLength="10" Text='<%#Eval("D13")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtD13" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server"
                                                    TargetControlID="txtD13"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD14" runat="server" MaxLength="10" Text='<%#Eval("D14")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtD14" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server"
                                                    TargetControlID="txtD14"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD15" runat="server" MaxLength="10" Text='<%#Eval("D15")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtD15" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender30" runat="server"
                                                    TargetControlID="txtD15"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD16" runat="server" MaxLength="10" Text='<%#Eval("D16")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtD16" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender31" runat="server"
                                                    TargetControlID="txtD16"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD17" runat="server" MaxLength="10" Text='<%#Eval("D17")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtD17" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>


                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender32" runat="server"
                                                    TargetControlID="txtD17"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD18" runat="server" MaxLength="10" Text='<%#Eval("D18")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtD20" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender33" runat="server"
                                                    TargetControlID="txtD18"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD19" runat="server" MaxLength="10" Text='<%#Eval("D19")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txtD20" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender34" runat="server"
                                                    TargetControlID="txtD19"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD20" runat="server" MaxLength="10" Text='<%#Eval("D20")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtD20" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender35" runat="server"
                                                    TargetControlID="txtD20"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD21" runat="server" MaxLength="10" Text='<%#Eval("D21")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtD21" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender36" runat="server"
                                                    TargetControlID="txtD21"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD22" runat="server" MaxLength="10" Text='<%#Eval("D22")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txtD22" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender37" runat="server"
                                                    TargetControlID="txtD22"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD23" runat="server" MaxLength="10" Text='<%#Eval("D23")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtD23" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender38" runat="server"
                                                    TargetControlID="txtD23"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD24" runat="server" MaxLength="10" Text='<%#Eval("D24")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtD24" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender39" runat="server"
                                                    TargetControlID="txtD24"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD25" runat="server" MaxLength="10" Text='<%#Eval("D25")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtD25" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender40" runat="server"
                                                    TargetControlID="txtD25"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD26" runat="server" MaxLength="10" Text='<%#Eval("D26")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="txtD26" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server"
                                                    TargetControlID="txtD26"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD27" runat="server" MaxLength="10" Text='<%#Eval("D27")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="txtD27" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender42" runat="server"
                                                    TargetControlID="txtD27"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD28" runat="server" MaxLength="10" Text='<%#Eval("D28")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txtD28" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender43" runat="server"
                                                    TargetControlID="txtD28"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD29" runat="server" MaxLength="10" Text='<%#Eval("D29")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="txtD29" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender44" runat="server"
                                                    TargetControlID="txtD29"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td style="width: 70px">
                                                <asp:TextBox ID="txtD30" runat="server" MaxLength="10" Text='<%#Eval("D30")%>' TabIndex="8" CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="50px" onblur="SumOfHeadDeduct()" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="txtD30" Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender45" runat="server"
                                                    TargetControlID="txtD30"
                                                    FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>


                                                <asp:Label runat="server" ID="lblD1" Visible="false" Text='<%# Eval("APD1")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD2" Visible="false" Text='<%# Eval("APD2")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD3" Visible="false" Text='<%# Eval("APD3")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD4" Visible="false" Text='<%# Eval("APD4")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD5" Visible="false" Text='<%# Eval("APD5")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD6" Visible="false" Text='<%# Eval("APD6")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD7" Visible="false" Text='<%# Eval("APD7")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD8" Visible="false" Text='<%# Eval("APD8")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD9" Visible="false" Text='<%# Eval("APD9")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD10" Visible="false" Text='<%# Eval("APD10")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD11" Visible="false" Text='<%# Eval("APD11")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD12" Visible="false" Text='<%# Eval("APD12")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD13" Visible="false" Text='<%# Eval("APD13")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD14" Visible="false" Text='<%# Eval("APD14")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD15" Visible="false" Text='<%# Eval("APD15")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD16" Visible="false" Text='<%# Eval("APD16")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD17" Visible="false" Text='<%# Eval("APD17")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD18" Visible="false" Text='<%# Eval("APD18")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD19" Visible="false" Text='<%# Eval("APD19")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD20" Visible="false" Text='<%# Eval("APD20")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD21" Visible="false" Text='<%# Eval("APD21")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD22" Visible="false" Text='<%# Eval("APD22")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD23" Visible="false" Text='<%# Eval("APD23")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD24" Visible="false" Text='<%# Eval("APD24")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD25" Visible="false" Text='<%# Eval("APD25")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD26" Visible="false" Text='<%# Eval("APD26")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD27" Visible="false" Text='<%# Eval("APD27")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD28" Visible="false" Text='<%# Eval("APD28")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD29" Visible="false" Text='<%# Eval("APD29")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblD30" Visible="false" Text='<%# Eval("APD30")%>'> </asp:Label>

                                                <asp:Label runat="server" ID="lblCkD1" Visible="false" Text='<%# Eval("CHKD1")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD2" Visible="false" Text='<%# Eval("CHKD2")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD3" Visible="false" Text='<%# Eval("CHKD3")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD4" Visible="false" Text='<%# Eval("CHKD4")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD5" Visible="false" Text='<%# Eval("CHKD5")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD6" Visible="false" Text='<%# Eval("CHKD6")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD7" Visible="false" Text='<%# Eval("CHKD7")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD8" Visible="false" Text='<%# Eval("CHKD8")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD9" Visible="false" Text='<%# Eval("CHKD9")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD10" Visible="false" Text='<%# Eval("CHKD10")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD11" Visible="false" Text='<%# Eval("CHKD11")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD12" Visible="false" Text='<%# Eval("CHKD12")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD13" Visible="false" Text='<%# Eval("CHKD13")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD14" Visible="false" Text='<%# Eval("CHKD14")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD15" Visible="false" Text='<%# Eval("CHKD15")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD16" Visible="false" Text='<%# Eval("CHKD16")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD17" Visible="false" Text='<%# Eval("CHKD17")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD18" Visible="false" Text='<%# Eval("CHKD18")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD19" Visible="false" Text='<%# Eval("CHKD19")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD20" Visible="false" Text='<%# Eval("CHKD20")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD21" Visible="false" Text='<%# Eval("CHKD21")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD22" Visible="false" Text='<%# Eval("CHKD22")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD23" Visible="false" Text='<%# Eval("CHKD23")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD24" Visible="false" Text='<%# Eval("CHKD24")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD25" Visible="false" Text='<%# Eval("CHKD25")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD26" Visible="false" Text='<%# Eval("CHKD26")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD27" Visible="false" Text='<%# Eval("CHKD27")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD28" Visible="false" Text='<%# Eval("CHKD28")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD29" Visible="false" Text='<%# Eval("CHKD29")%>'> </asp:Label>
                                                <asp:Label runat="server" ID="lblCkD30" Visible="false" Text='<%# Eval("CHKD30")%>'> </asp:Label>



                                            </td>

                                        </tr>
                                    </ItemTemplate>

                                </asp:ListView>
                            </div>
                        </div>


                    </asp:Panel>
            </div>


        </div>
        </form>
    </div>

    </div>


    </div>


    <table cellpadding="0" cellspacing="0" width="100%">
        <%--<tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
               &nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>--%>
        <%--  Shrink the info panel out of view --%>
        <%--  Reset the sample so it can be played again --%>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                    // Move an element directly on top of another element (and optionally
                    // make it the same size)
                    function Cover(bottom, top, ignoreSize) {
                        var location = Sys.UI.DomElement.getLocation(bottom);
                        top.style.position = 'absolute';
                        top.style.top = location.y + 'px';
                        top.style.left = location.x + 'px';
                        if (!ignoreSize) {
                            top.style.height = bottom.offsetHeight + 'px';
                            top.style.width = bottom.offsetWidth + 'px';
                        }
                    }
                </script>


                <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                    <Animations>
                            <OnClick>
                                <Sequence AnimationTarget="info">
                                    <%--  Shrink the info panel out of view --%>
                                    <StyleAction Attribute="overflow" Value="hidden"/>
                                    <Parallel Duration=".3" Fps="15">
                                        <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                        <FadeOut />
                                    </Parallel>
                                    <%--  Reset the sample so it can be played again --%>
                                    <StyleAction Attribute="display" Value="none"/>
                                    <StyleAction Attribute="width" Value="250px"/>
                                    <StyleAction Attribute="height" Value=""/>
                                    <StyleAction Attribute="fontSize" Value="12px"/>
                                    <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                                    <%--  Enable the button so it can be played again --%>
                                    <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                                </Sequence>
                            </OnClick>
                            <OnMouseOver>
                                <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                            </OnMouseOver>
                            <OnMouseOut>
                                <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                            </OnMouseOut>
                    </Animations>
                </ajaxToolKit:AnimationExtender>
            </td>
        </tr>
        <tr>
            <td>&nbsp
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px">
                <%--<asp:Panel ID="" runat="server" Style="padding-left: 10px;" Width="95%">--%>
                <%--<fieldset class="fieldsetPay">
                        <legend class="legendPay">Select PayHead/Staff</legend>--%>
                <br />
                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td class="form_left_label" style="padding-left: 10px; width: 15%"><%--PayHead :<span style="color: Red">*</span>
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlPayhead" AppendDataBoundItems="true" runat="server" Width="300px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlPayhead_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPayhead" runat="server" ControlToValidate="ddlPayhead"
                                    Display="None" ErrorMessage="Please Select PayHead" ValidationGroup="payroll"
                                    InitialValue="0"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="padding-left: 10px; width: 15%"><%--College :<span style="color: Red">*</span>
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlCollege" runat="server" Width="300px" AppendDataBoundItems="true"
                                    AutoPostBack="true" TabIndex="19" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                    ValidationGroup="payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                    InitialValue="0" Display="None"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="padding-left: 10px; width: 15%"><%--Staff :<span style="color: Red">*</span>
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" Width="300px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                    
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                    Display="None" ErrorMessage="Please Select Staff" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                            <%-- <asp:CheckBox ID="chkIdno" runat="server" Text="Order By Idno" AutoPostBack="true"
                                                OnCheckedChanged="chkIdno_CheckedChanged" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="padding-left: 10px; width: 15%"><%--Department :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlDeptmon" AppendDataBoundItems="true" runat="server" Width="300px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDeptmon_SelectedIndexChanged">
                                   
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>--%>
                               
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label">&nbsp; <%--Order By :<span style="color: Red">*</span>
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server" Width="200px"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">IDNO</asp:ListItem>
                                    <asp:ListItem Value="2">SEQUENCE NO</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlorderby"
                                    Display="None" ErrorMessage="Select Order" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label">&nbsp; <%--Select Rule :<span style="color:Red">*</span>
                            </td>
                            <td class="form_left_text">
                                <b>
                                    <asp:DropDownList ID="ddlpayruleselect" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="true" Width="200px" OnSelectedIndexChanged="ddlpayruleselect_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </b>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlorderby"
                                    Display="None" ErrorMessage="Select Rule" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2"></td>
                    </tr>
                </table>
                <br />
                </fieldset>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2"></td>
        </tr>
        <tr>
            <td>&nbsp
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
    <%-- <asp:CheckBox ID="chkIdno" runat="server" Text="Order By Idno" AutoPostBack="true"
                                                OnCheckedChanged="chkIdno_CheckedChanged" />--%>
    <script type="text/javascript" language="javascript">
        function totalAppointment(chkcomplaint) {
            var frm = document.forms[0];
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkcomplaint.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function check(me) {

            if (ValidateNumeric(me) == true) {
                var count = 0.00;
                for (i = 0; i <= Number(document.getElementById("ctl00_ContentPlaceHolder1_txtEmpoyeeCount").value) - 1; i++) {

                    count = Number(count) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lvMonthlyChanges_ctrl" + i + "_txtDays").value);

                }

                document.getElementById("ctl00_ContentPlaceHolder1_txtAmount").value = count;
            }

        }
        //Added by vijay andoju for sum of Headd
        function SumOfHead() {

            var Rows = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtEmpoyeeCount").value);

            for (j = 1; j <= 15; j++) {

                var sum = 0;

                for (k = 0; k < Number(document.getElementById("ctl00_ContentPlaceHolder1_txtEmpoyeeCount").value) ; k++) {

                    sum = parseFloat(sum) + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_lvMonthlyChanges_ctrl" + k + "_txtI" + j).value);

                }

                document.getElementById("ctl00_ContentPlaceHolder1_lvSumIncome_ctrl0_txtI" + j).value = parseFloat(sum);
                document.getElementById("ctl00_ContentPlaceHolder1_lvSumIncome_ctrl0_txtI" + j).disabled = true;


            }

            document.getElementById("ctl00_ContentPlaceHolder1_lvSumIncome_ctrl0_lblIname").innerHTML = 'Total';
            document.getElementById("ctl00_ContentPlaceHolder1_lvSumIncome_ctrl0_chkIno").style.display = 'none';
        }

        //Added by vijay andoju for sum of Headd
        function SumOfHeadDeduct() {
            debugger;
            var Rows = Number(document.getElementById("ctl00_ContentPlaceHolder1_txtEmpoyeeCount").value);

            for (j = 1; j <= 30; j++) {

                var sum = 0;

                for (k = 0; k < Number(document.getElementById("ctl00_ContentPlaceHolder1_txtEmpoyeeCount").value) ; k++) {

                    sum = parseFloat(sum) + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_lvMonthlyChangesDeduction_ctrl" + k + "_txtD" + j).value);

                }

                document.getElementById("ctl00_ContentPlaceHolder1_lvSumDiduction_ctrl0_txtD" + j).value = parseFloat(sum);
                document.getElementById("ctl00_ContentPlaceHolder1_lvSumDiduction_ctrl0_txtD" + j).disabled = true;

            }

            document.getElementById("ctl00_ContentPlaceHolder1_lvSumDiduction_ctrl0_lblDname").innerHTML = 'Total';
            document.getElementById("ctl00_ContentPlaceHolder1_lvSumDiduction_ctrl0_chkIno").style.display = 'none';
        }


        function ConfirmMessage() {
            var payHead = document.getElementById("ctl00_ContentPlaceHolder1_ddlPayhead").value;
            var hidPayhead = document.getElementById("ctl00_ContentPlaceHolder1_hidPayhead").value;

            if (confirm("Do you want to save changes ?")) {
                return true;
            }
            else {
                return false;
            }
        }


        //function ValidateNumeric(txt) {


        //    if (isNaN(txt.value)) {
        //        txt.value = txt.value.substring(0, (txt.value.length) - 1);
        //        txt.value = "";
        //        txt.focus();
        //        alert("Only Numeric Characters allowed");
        //        return false;
        //    }
        //    else {
        //        return true;
        //    }
        //}
        //added by vijay andoju sumofhead


        function Check_Click(objRef) {
            //Get the Row based on checkbox
            debugger;
            var row = objRef.parentNode.parentNode;
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");


            if (objRef.focus) {
                for (var i = 0; i < inputList.length; i++) {

                    if (inputList[i].parentNode.parentNode == row) {

                        //If focused change color                  
                        row.style.backgroundColor = "#CCCC88";
                    }
                    else {
                        //inputList[i].parentNode.parentNode.style.backgroundColor = "White";


                        if (inputList[i].parentNode.parentNode.rowIndex % 2 == 0) {

                            //Alternating Row Color

                            inputList[i].parentNode.parentNode.style.backgroundColor = "White";
                        }

                        else {
                            inputList[i].parentNode.parentNode.style.backgroundColor = "#ffffd2";

                        }

                    }
                }
            }
        }



    </script>

    <script type="text/javascript">
        function ApplyAll(txt) {
            {
                $('.writeAll').val(txt);
            }
        }


        $(document).ready(function () {

            //attach keypress to input
            $('.input').keydown(function (event) {
                // Allow special chars + arrows 
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9
                    || event.keyCode == 27 || event.keyCode == 13
                    || (event.keyCode == 65 && event.ctrlKey === true)
                    || (event.keyCode >= 35 && event.keyCode <= 39)) {
                    return;
                } else {
                    // If it's not a number stop the keypress
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();




                    }
                }
            });

        });

    </script>




    <%-- sachin 25 April 2017 --%>
    <script type="text/javascript">
        function SetNo(val) {
            debugger;
            var num = Number(val);
            var i = 0;

            for (i = 0; i < 900000000; i++) {
                var txtbx = document.getElementById('ctl00_ContentPlaceHolder1_lvMonthlyChanges_ctrl' + i + '_txtDays');
                txtbx.value = num;

            }

            return false;
        }



        $(document).ready(function () {

            //attach keypress to input
            $('.input').keydown(function (event) {
                // Allow special chars + arrows 
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9
                    || event.keyCode == 27 || event.keyCode == 13
                    || (event.keyCode == 65 && event.ctrlKey === true)
                    || (event.keyCode >= 35 && event.keyCode <= 39)) {
                    return;
                } else {
                    // If it's not a number stop the keypress
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
                }
            });

        });



    </script>


    <%-- For tab sequence using down arrow key 02 May2017--%>

    <script type="text/javascript">


        $(document).ready(function () {
            debugger;
            var inputs = $(':input').keydown(function (e) {
                if (e.which == 40) {
                    e.preventDefault();
                    var nextInput = inputs.get(inputs.index(this) + 1);
                    if (nextInput) {
                        nextInput.focus();
                    }
                }
            });


            var inputs1 = $(':input').keyup(function (e) {
                if (e.which == 38) {
                    e.preventDefault();
                    var PreInput = inputs1.get(inputs1.index(this) - 1);
                    if (PreInput) {
                        PreInput.focus();
                    }
                }

            });

        });
    </script>

    <%-- End of For tab sequence --%>



    <%-- End of 27Apr2017  --%>
</asp:Content>

