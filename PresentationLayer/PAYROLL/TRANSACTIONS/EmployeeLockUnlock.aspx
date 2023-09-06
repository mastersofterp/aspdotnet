<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeLockUnlock.aspx.cs"
    Inherits="PAYROLL_TRANSACTIONS_EmployeeLockUnlock" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">EMPLOYEE LOCK/UNLOCK</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlSelect" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select College/Staff</h5>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>College</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                        ValidationGroup="payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                        InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Staff</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                        <%-- <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                        <asp:ListItem Value="0">All Staff</asp:ListItem>--%>
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                        Display="None" ErrorMessage="Please Select Staff" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                    <%-- <asp:CheckBox ID="chkIdno" runat="server" Text="Order By Idno" AutoPostBack="true"
                                                OnCheckedChanged="chkIdno_CheckedChanged" />--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddlDeptmon" AppendDataBoundItems="true" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDeptmon_SelectedIndexChanged" TabIndex="3" CssClass="form-control" data-select2-enable="true">
                                        <%--<asp:ListItem Value="-1">Please Select</asp:ListItem>
                                        <asp:ListItem Value="0">All Staff</asp:ListItem>--%>
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDeptmon"
                                        Display="None" ErrorMessage="Please Select Department" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    <%-- <asp:CheckBox ID="chkIdno" runat="server" Text="Order By Idno" AutoPostBack="true"
                                                OnCheckedChanged="chkIdno_CheckedChanged" />--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Order By</label>
                                    </div>
                                    <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server"
                                        AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="4">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">IDNO</asp:ListItem>
                                        <asp:ListItem Value="2">SEQUENCE NO</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlorderby"
                                        Display="None" ErrorMessage="Select Order" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="payroll" CssClass="btn btn-primary"
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
                            <div class="form-group col-lg-8 col-md-12 col-12">
                            <div class="label-dynamic">
                                <label>Total Employees</label>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <asp:TextBox ID="txtEmpoyeeCount" runat="server" CssClass="form-control" TabIndex="5"></asp:TextBox>
                                </div>

                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <asp:TextBox ID="txtPayheadName" runat="server" CssClass="form-control" TabIndex="6"  Visible="false"></asp:TextBox>
                                </div>

                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" TabIndex="7" Visible="false"></asp:TextBox>
                                </div>
                                <asp:HiddenField ID="hidPayhead" runat="server" />
                            </div>     
                        </div>                                
                            <div class="form-group col-lg-4 col-md-12 col-12">
                            <div class=" note-div">
                                <h5 class="heading">Note</h5>
                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Uncheck Lock/Unlock column to unlock employees for updation</span></p>
                            </div>
                        </div>
                        </div>
                        </div>
                        <div class="col-12">
                            <asp:ListView ID="lvMonthlyChanges" runat="server">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Employee Lock/Unlock</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Idno
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
                                                <th>Lock/Unlock
                                                </th>
                                            </tr>
                                            <thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td>
                                            <%#Eval("IDNO")%>
                                        </td>
                                        <td>
                                            <%#Eval("NAME")%>
                                        </td>
                                        <td>
                                            <%#Eval("SUBDESIG")%>
                                        </td>
                                        <td>
                                            <%#Eval("SUBDEPT")%>
                                        </td>
                                        <td>
                                            <%#Eval("BASIC")%>
                                        </td>
                                        <td>
                                            <%#Eval("EMPLOYEE_LOCK")%>
                                        </td>
                                        <td>
                                            <%--<asp:TextBox ID="txtDays" runat="server" MaxLength="6" Text='<%#Eval("AMOUNT")%>'
                                                    CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="60px" onkeyup="return check(this);"
                                                    onfocus="Check_Click(this)" />--%>
                                            <asp:CheckBox ID="chkEmployeeLockUnlock" runat="server" ToolTip='<%#Eval("IDNO")%>' />


                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="altitem">
                                        <td>
                                            <%#Eval("IDNO")%>
                                        </td>
                                        <td>
                                            <%#Eval("NAME")%>
                                        </td>
                                        <td>
                                            <%#Eval("SUBDESIG")%>
                                        </td>
                                        <td>
                                            <%#Eval("SUBDEPT")%>
                                        </td>
                                        <td>
                                            <%#Eval("BASIC")%>
                                        </td>
                                        <td>
                                            <%#Eval("EMPLOYEE_LOCK")%>
                                        </td>
                                        <td>
                                            <%-- <asp:TextBox ID="txtDays" runat="server" MaxLength="6" Text='<%#Eval("AMOUNT")%>'
                                                   CssClass="writeAll" ToolTip='<%#Eval("IDNO")%>' Width="60px" onkeyup="return check(this);" 
                                                   onfocus="Check_Click(this)" />--%>
                                            <asp:CheckBox ID="chkEmployeeLockUnlock" runat="server" ToolTip='<%#Eval("IDNO")%>' />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:ListView>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll"
                                OnClick="btnSub_Click" OnClientClick="return ConfirmMessage();" TabIndex="8" CssClass="btn btn-primary"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-warning"
                                    OnClick="btnCancel_Click" TabIndex="9"/>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                    </asp:Panel>
                    <div id="divMsg" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>

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


        function ValidateNumeric(txt) {


            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = "";
                txt.focus();
                alert("Only Numeric Characters alloewd");
                return false;
            }
            else {
                return true;
            }
        }

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

    <script lang="javascript" type="text/javascript">
        function ApplyAll(txt) {
            //         if($.isNumeric(txt.val) == true)
            {
                $(".writeAll").val(txt);
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

</asp:Content>

