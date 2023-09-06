<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Attendance.aspx.cs" Inherits="PayRoll_Pay_Attendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ATTENDANCE ENTRY</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Select Staff</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlSelect" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                                ToolTip="Select College" AppendDataBoundItems="true" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege" ValidationGroup="payroll"
                                                ErrorMessage="Please select College" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <%--<label>Scheme</label>--%>
                                                <label>Scheme/Staff</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged" AutoPostBack="true" ToolTip="Select Scheme/Staff">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                Display="None" ErrorMessage="Select Scheme" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:CheckBox ID="chkIdno" runat="server" Text="Order By Idno" AutoPostBack="true"
                                                OnCheckedChanged="chkIdno_CheckedChanged" TabIndex="3" />
                                            <asp:CheckBox ID="chkAbsent" runat="server" Text="Show Absent Only" AutoPostBack="true"
                                                OnCheckedChanged="chkAbsent_CheckedChanged" TabIndex="4" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                               <%-- <label>Staff</label>--%>
                                                <label>Employee Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmployeeType" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="4" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlEmployeeType_SelectedIndexChanged" AutoPostBack="true" ToolTip="Select Staff">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDepartment" AppendDataBoundItems="true" runat="server" data-select2-enable="true"
                                                CssClass="form-control" TabIndex="5" AutoPostBack="True" ToolTip="Select Department"
                                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Order by</label>
                                            </div>
                                            <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Select Order By" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                                <asp:ListItem Value="1">IDNO</asp:ListItem>
                                                <asp:ListItem Value="2">SEQUENCE NO</asp:ListItem>
                                                <asp:ListItem Value="3">Employee Code</asp:ListItem>
                                                <asp:ListItem Value="4">Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                            </asp:Panel>
                            <asp:Panel ID="pnlNote" runat="server">
                                <div class="form-group col-lg-6 col-md-12 col-12">
                                    <div class=" note-div">
                                        <h5 class="heading">Note</h5>
                                        <div id="tdAbsentDays" runat="server">
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>1) Enter Absent Day's in Day's Column</span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>2) If Present in whole month then enter only 0</span></p>
                                        </div>
                                        <div id="tdPresentDays" runat="server">
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>1) Enter Present Day's in Day's Column</span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>2) If Present in whole month then enter only 0</span></p>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlAttendance" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvAttendance" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" CssClass="text-center mt-3" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Enter Attendance</h5>
                                            </div>
                                          <%--  class="table table-striped table-bordered nowrap display"--%>
                                            <table  style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Srno
                                                        </th>
                                                        <th>Idno
                                                        </th>
                                                        <th>Employee Code
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Designation
                                                        </th>
                                                        <th>Days
                                                        </th>
                                                        <th>Over Time (Hrs.Min)
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
                                                <td>
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <td>
                                                    <%#Eval("IDNO")%>
                                                </td>

                                                <td>
                                                    <%#Eval("PFILENO")%>
                                                </td>

                                                <td>
                                                    <%#Eval("EMPNAME")%>
                                                </td>
                                                <td>
                                                    <%#Eval("SUBDESIG")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDays" runat="server" MaxLength="4" Text='<%#Eval("PAYDAYS")%>'
                                                        ToolTip='<%#Eval("IDNO")%>' CssClass="from-control" onkeyup="return check(this);" TabIndex="7" onfocus="Check_Click(this)" />
                                                    <asp:RequiredFieldValidator ID="rfvDays" runat="server" ControlToValidate="txtDays"
                                                        Display="None" ErrorMessage="Please Enter Days" ValidationGroup="payroll" SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="rvDays" runat="server" ControlToValidate="txtDays" MinimumValue="0"
                                                        MaximumValue="99" Display="None" ValidationGroup="payroll" ErrorMessage="Please Enter Days between 0 to 31"
                                                        Type="Double" SetFocusOnError="true">
                                                    </asp:RangeValidator>
                                                    <asp:CompareValidator ID="cvDays" runat="server" ControlToValidate="txtDays" Display="None"
                                                        ErrorMessage="Please Enter Numeric Value" SetFocusOnError="true" ValidationGroup="payroll"
                                                        Operator="DataTypeCheck" Type="Double">  
                                                    </asp:CompareValidator>
                                                </td>
                                                <td>

                                                    <asp:TextBox ID="txtOverTime" runat="server" CssClass="form-control" Text='<%#Eval("OverTime")%>' ToolTip='<%#Eval("IDNO")%>' TabIndex="8" MaxLength="5" onfocus="Check_Click(this)" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOverTime"
                                                        Display="None" ErrorMessage="Please Enter Grade Pay" ValidationGroup="payroll" SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        TargetControlID="txtOverTime"
                                                        FilterType="Custom,Numbers"
                                                        FilterMode="ValidChars"
                                                        ValidChars=".">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </div>
                                <div class="col-12 btn-footer mt-2">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" CssClass="btn btn-primary"
                                        OnClick="btnSub_Click" ToolTip="Click to Submit" TabIndex="9" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="10" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function check(me) {
            if (ValidateNumeric(me) == true) {
                var myArr = new Array();
                var myArrdays = new Array();
                myString = "" + me.id + "";
                myArr = myString.split("_");
                var index = myArr[3].substring(4, myArr[3].length);
                var Attenddays = document.getElementById("ctl00_ContentPlaceHolder1_lvAttendance_ctrl" + index + "_txtDays");
                var Attend_days = Attenddays.value;
                myArrdays = Attend_days.split(".");

                if (!(Attend_days > 31)) {
                    if (myArrdays[1] > 0) {
                        if (myArrdays[1] > 5 || myArrdays[1] < 5) {
                            alert("Please enter 5 only after decimal");
                            document.getElementById("ctl00_ContentPlaceHolder1_lvAttendance_ctrl" + index + "_txtDays").value = "";
                            document.getElementById("ctl00_ContentPlaceHolder1_lvAttendance_ctrl" + index + "_txtDays").focus();
                        }
                    }
                }
                else {
                    alert("Please enter days less than 32");
                    me.value = "";
                    me.focus();
                }
            }
        }


        function ValidateNumeric(txt) {


            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = "";
                txt.focus();
                alert("Only Numeric Characters allowed");
                return false;
            }
            else {
                return true;
            }
        }

    </script>

</asp:Content>
