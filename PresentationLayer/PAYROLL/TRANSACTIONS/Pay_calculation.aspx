<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_calculation.aspx.cs" Inherits="PayRoll_Pay_calculation" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <style>
        .bootstrap-duallistbox-container .buttons {
            display: flex;
        }

        :focus {
            outline: 0;
        }

        table {
            border: 1px solid #f4f4f4;
        }

        th {
            background-color: #255282 !important;
            color: #fff !important;
            padding: 2px 8px;
            font-weight: 400 !important;
        }

        td {
            color: #333333 !important;
            background: #fff !important;
            padding: 2px 8px;
        }

        .arrow-top {
            margin-top: 75px;
        }
         #lstEmployee, select {
            height:200px !important;
         }

        @media (max-width:576px) {
            .arrow-top {
                margin-top: 10px;
            }
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">SALARY PROCESS</h3>
                        </div>

                        <div class="box-body">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Which Month salary do you want to calculate</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblerror" runat="server" SkinID="Errorlbl" />
                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>1) Please Dont close the browser when salary is processed</span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>2) Please click salary process button only once</span></p>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Month &amp; Year</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="ImaCalStartDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtMonthYear" runat="server" AutoPostBack="true" onblur="return checkdate(this);"
                                                        OnTextChanged="txtMonthYear_TextChanged" ToolTip="Enter Month and Year" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    
                                                    <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtMonthYear">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtMonthYear"
                                                        Display="None" ErrorMessage="Please Month &amp; Year in (MM/YYYY Format)" SetFocusOnError="True"
                                                        ValidationGroup="payroll">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>College</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" ToolTip="Select College" AppendDataBoundItems="true" data-select2-enable="true"
                                                    AutoPostBack="true" TabIndex="2" CssClass="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                    ValidationGroup="payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                                    InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                   <%-- <label>Scheme</label>--%>
                                                     <label>Scheme/Staff</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStaff" runat="server" ToolTip="Select Staff" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true"
                                                    TabIndex="3" CssClass="form-control" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                    Display="None" ErrorMessage="Please Select Scheme/Staff" InitialValue="0" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trspace" runat="server">
                                                <div class="label-dynamic">
                                                    <label></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                           
                            <div class="col-12" id="trselection" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Salary Reprocess For?</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:RadioButton ID="radAllEmployees" TabIndex="4" GroupName="radpay" runat="server" Text="All Employees"
                                                Checked="true" AutoPostBack="true" OnCheckedChanged="radAllEmployees_CheckedChanged" /><br />
                                            <asp:RadioButton ID="radSelectedEmployees" TabIndex="5" GroupName="radpay" runat="server" Text="Selected Employees"
                                                AutoPostBack="true" OnCheckedChanged="radSelectedEmployees_CheckedChanged" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr_OrderBy" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Order by</label>
                                            </div>
                                            <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="5" data-select2-enable="true"
                                                ToolTip="Select Order By" AutoPostBack="true" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                                <%-- <asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                                <asp:ListItem Value="1">IDNO</asp:ListItem>
                                                <asp:ListItem Value="2">SEQUENCE NO</asp:ListItem>
                                                <asp:ListItem Value="3">Employee Code</asp:ListItem>
                                                <asp:ListItem Value="4">Name</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlorderby"
                                                Display="None" ErrorMessage="Select Order" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tremployee" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Employee</label>
                                            </div>
                                            <asp:ListBox ID="lstEmployee" runat="server" TabIndex="6" ToolTip="Select Employee" SelectionMode="Multiple" AppendDataBoundItems="true"
                                                CssClass="form-control" style="height: 250px!important"></asp:ListBox>
                                            <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" ControlToValidate="lstEmployee"
                                                Display="None" ErrorMessage="Please Select Employee" InitialValue="" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                    <ProgressTemplate>
                                        <asp:Image ID="imgmoney" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                        Processing Salary .........................................
                                    </ProgressTemplate>
                                </asp:UpdateProgress>

                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="butSalaryProcess" runat="server" Text="Process Salary" ValidationGroup="payroll"
                                    CssClass="btn btn-primary" OnClick="butSalaryProcess_Click" TabIndex="7"/>
                                <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="payroll" />
                            </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
      
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<center>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" align="center">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <asp:Image ID="imgmoney" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                            Processing Salary .........................................
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
        </table>
    </center>--%>

    <script type="text/javascript" language="javascript">
        function checkdate(input) {
            var validformat = /^\d{2}\/\d{4}$/ //Basic check for format validity
            var returnval = false
            if (!validformat.test(input.value)) {
                alert("Invalid Date Format. Please Enter in MM/YYYY Formate")
                document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
            }
            else {
                var monthfield = input.value.split("/")[0]

                if (monthfield > 12 || monthfield <= 0) {
                    alert("Month Should be greate than 0 and less than 13");
                    document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                    document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
                }
            }
        }
    </script>

</asp:Content>
