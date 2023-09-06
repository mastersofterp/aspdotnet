<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="Pay_ChangeInMasterFile.aspx.cs" Inherits="PayRoll_Pay_ChangeInMasterFile"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../../JAVASCRIPTS/jquery-1.5.1.js"></script>--%>
    <%-- <script src="../../JAVASCRIPTS/jquery-1.6.4.min.js" type="text/javascript"></script>--%>
    <%--<link href="../../Css/jquery-ui.css" rel="stylesheet" />--%>

    <%--<script src="../../JAVASCRIPTS/jquery-1.6.4.min.js" type="text/javascript"></script>--%>

    <%--<style>
        .DocumentList {
            overflow-x: scroll;
            overflow-y: scroll;
            
        }
    </style>--%>
    <%-- Flash the text/border red and fade in the "close" button --%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">MONTHLY CHANGES IN MASTERFILE</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlSelect" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select PayHead/Staff</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Pay Head</label>
                                    </div>
                                    <asp:DropDownList ID="ddlPayhead" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Select Pay Head" data-select2-enable="true"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlPayhead_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvPayhead" runat="server" ControlToValidate="ddlPayhead"
                                        Display="None" ErrorMessage="Please Select PayHead" ValidationGroup="payroll"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                            <%--     <sup>* </sup>--%>
                                        <label>Sub Pay Head</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSubPayHead" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                        TabIndex="2" ToolTip="Select Sub Pay Head" AutoPostBack="True" OnSelectedIndexChanged="ddlSubPayHead_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>


                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                       <%-- <sup>* </sup>--%>
                                        <label>College</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="3" ToolTip="Select College" data-select2-enable="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--  <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                        ValidationGroup="payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                                        InitialValue="0" Display="None"></asp:RequiredFieldValidator>--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                      <%-- <sup>* </sup>--%>
                                        <%--<label>Scheme</label>--%>
                                        <label>Scheme/Staff</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="4" data-select2-enable="true"
                                        ToolTip="Select Scheme"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">

                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--  <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                        Display="None" ErrorMessage="Please Select Scheme" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                    <%--   <sup>* </sup>--%>
                                        <%--<label>Staff</label>--%>
                                        <label>Employee Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control" data-select2-enable="true"
                                        AppendDataBoundItems="True" TabIndex="5" ToolTip="Select Staff">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddlDeptmon" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="6" data-select2-enable="true"
                                        ToolTip="Select Department"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDeptmon_SelectedIndexChanged">

                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Order By</label>
                                    </div>
                                    <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="7" data-select2-enable="true"
                                        ToolTip="Select Order By"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">IDNO</asp:ListItem>
                                        <asp:ListItem Value="2">SEQUENCE NO</asp:ListItem>
                                        <asp:ListItem Value="3">Employee Code</asp:ListItem>
                                        <asp:ListItem Value="4">Name</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlorderby"
                                        Display="None" ErrorMessage="Select Order" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <label>Select Rule</label>
                                    </div>
                                    <asp:DropDownList ID="ddlpayruleselect" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="8" data-select2-enable="true"
                                        ToolTip="Select Rule"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlpayruleselect_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlpayruleselect"
                                                        Display="None" ErrorMessage="Select Rule" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="payroll" CssClass="btn btn-primary" TabIndex="9" ToolTip="Click To Show"
                                OnClick="btnShow_Click" />
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="payroll"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                    </asp:Panel>
                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>

                    <asp:Panel ID="pnlMonthlyChanges" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Total Employees =</label>
                                    </div>
                                    <asp:TextBox ID="txtEmpoyeeCount" BackColor="#006595" ForeColor="White" runat="server"
                                        BorderStyle="None" Enabled="false"></asp:TextBox>
                                    <asp:TextBox ID="txtPayheadName" BackColor="#006595" ForeColor="White" runat="server"
                                        BorderStyle="None"></asp:TextBox>
                                    <asp:TextBox ID="txtAmount" BackColor="#006595" ForeColor="White" runat="server"
                                        BorderStyle="None" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="hidPayhead" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <asp:ListView ID="lvMonthlyChanges" runat="server">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" CssClass="text-center mt-3"/>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Monthly Changes</h5>
                                    </div>
                                    <table  style="width: 100%"> <%--class="table table-striped table-bordered nowrap display"--%>
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Srno
                                                </th>

                                                <th>Emp ID
                                                </th>
                                                <th>Staff ID
                                                </th>
                                                <th>Name
                                                </th>

                                                <th>Designation
                                                </th>
                                                <th>Department
                                                </th>
                                                <th>Basic
                                                </th>
                                                <th>Group
                                                </th>
                                                <th>Amount                                                           
                                                                <asp:TextBox ID="txtAllAmt" Width="120px" runat="server" class="input" onkeyup="SetNo(this.value)">
                                                                </asp:TextBox>

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                                        Enabled="True"
                                                        ValidChars="0123456789."
                                                        FilterMode="ValidChars"
                                                        FilterType="Custom"
                                                        TargetControlID="txtAllAmt">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </th>
                                            </tr>
                                            <thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="text-center">
                                            <%# Container.DataItemIndex + 1%>
                                        </td>
                                        <td>
                                            <%#Eval("EmployeeId")%>
                                        </td>
                                        <td>
                                            <%#Eval("STAFFNO")%>
                                        </td>
                                        <td>
                                            <%#Eval("NAME")%>
                                        </td>

                                        <td>
                                            <%#Eval("subdesig")%>
                                        </td>
                                        <td>
                                            <%#Eval("subdept")%>
                                        </td>
                                        <td>
                                            <%#Eval("Basic")%>
                                        </td>
                                        <td>
                                            <%#Eval("CLNAME")%>
                                        </td>
                                        <td>

                                            <asp:TextBox ID="txtDays" runat="server" MaxLength="10" Text='<%#Eval("AMOUNT")%>' TabIndex="8"
                                                CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="120px" onkeyup="return check(this);" onfocus="Check_Click(this)" />
                                            <%----%>

                                            <asp:RequiredFieldValidator ID="rfvDays" runat="server" ControlToValidate="txtDays"
                                                Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" CssClass="btn btn-primary" TabIndex="10" ToolTip="Click To Save"
                                OnClick="btnSub_Click" OnClientClick="return ConfirmMessage();" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="11" ToolTip="Click To Reset" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
    <%-- <asp:CheckBox ID="chkIdno" runat="server" Text="Order By Idno" AutoPostBack="true"
                                                OnCheckedChanged="chkIdno_CheckedChanged" />--%>

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


        function ConfirmMessage() {
            var payHead = document.getElementById("ctl00_ContentPlaceHolder1_ddlPayhead").value;
            var hidPayhead = document.getElementById("ctl00_ContentPlaceHolder1_hidPayhead").value;

            if (confirm("Do you want to save changes in " + hidPayhead + "->" + payHead)) {
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

        function Check_Click(objRef) {

            //Get the Row based on checkbox
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
