<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Emp_Bulk_Update.aspx.cs"
    Inherits="PAYROLL_TRANSACTIONS_Emp_Bulk_Update" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
     <style>
        .table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">BULK EMPLOYEE UPDATE</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Select Field/Staff</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                    <asp:Panel ID="pnlSelect" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Employee Field</label>
                                    </div>
                                    <asp:DropDownList ID="ddlPayhead" AppendDataBoundItems="true" runat="server" TabIndex="1" ToolTip="Select Employee Field" CssClass="form-control" data-select2-enable="true"
                                        OnSelectedIndexChanged="ddlPayhead_SelectedIndexChanged" AutoPostBack-="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvPayhead" runat="server" ControlToValidate="ddlPayhead"
                                        Display="None" ErrorMessage="Please Select PayHead" ValidationGroup="payroll"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>College</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true"
                                        TabIndex="2" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--    <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege" ValidationGroup="payroll"
                                                        ErrorMessage="Please select College" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <%--<label>Staff</label>--%>
                                        <label>Scheme/Staff</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                        ToolTip="Select Staff" TabIndex="3" CssClass="form-control" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged" data-select2-enable="true">
                                        <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                        <asp:ListItem Value="0">All Staff</asp:ListItem>
                                    </asp:DropDownList>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStaff" ValidationGroup="payroll"
                                                        ErrorMessage="Please select Staff" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="4" ValidationGroup="payroll" CssClass="btn btn-primary"
                            OnClick="btnShow_Click" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="payroll"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                    <asp:Panel ID="pnlMonthlyChanges" runat="server">
                        <div class="form-group col-12">
                            <div class="label-dynamic">
                                <label>Total Employees</label>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <asp:TextBox ID="txtEmpoyeeCount" BackColor="#006595" ForeColor="White" runat="server"
                                        CssClass="form-control" BorderStyle="None" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <asp:TextBox ID="txtPayheadName" BackColor="#006595" ForeColor="White" runat="server"
                                        BorderStyle="None" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <asp:TextBox ID="txtAmount" BackColor="#006595" ForeColor="White" runat="server"
                                        CssClass="form-control" BorderStyle="None" Enabled="false"></asp:TextBox>
                                </div>
                                <asp:HiddenField ID="hidPayhead" runat="server" />
                            </div>
                        </div>
                        <div class="col-12">
                   <asp:ListView ID="lvMonthlyChanges" runat="server" OnItemDataBound="lvMonthlyChanges_ItemDataBound">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" CssClass="text-center mt-3" />
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Bulk Employee Update</h5>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                            <%-- class="table table-striped table-bordered nowrap display"--%>
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Idno
                                                    </th>
                                                    <th>Employee Code
                                                    </th>
                                                    <th>Name
                                                    </th>
                                                    <th>Designation
                                                    </th>
                                                    <th>Basic
                                                    </th>
                                                    <th>Field                                                       
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
                                    <tr>
                                        <td>
                                            <%#Eval("IDNO")%>
                                        </td>
                                        <td>
                                            <%#Eval("PFILENO")%>
                                        </td>
                                        <td>
                                            <%#Eval("NAME")%>
                                        </td>
                                        <td>
                                            <%#Eval("SUBDESIG")%>
                                        </td>

                                        <td>
                                            <%#Eval("Basic")%>
                                        </td>

                                        <td>
                                            <asp:TextBox ID="txtEditField" runat="server" MaxLength="100" Text='<%#Eval("COLUMN_NAME")%>'
                                                ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" onkeyup="return Increment(this);" />
                                            <asp:DropDownList ID="ddleditfield" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"></asp:DropDownList>
                                            <asp:DropDownList ID="ddlScale" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"></asp:DropDownList>
                                            <asp:DropDownList ID="ddlAppointment" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>


                                            <%--  <asp:DropDownList ID="ddlBloodGroup" runat="server" AppendDataBoundItems="true" Width="70px">
                                                    </asp:DropDownList>                                                 

                                                    <asp:DropDownList ID="ddlStaffNo" runat="server" AppendDataBoundItems="true" Width="70px">
                                                    </asp:DropDownList>

                                                    <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" Width="200px">
                                                    </asp:DropDownList>

                                                    <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true" Width="180px">
                                                    </asp:DropDownList>

                                                      <asp:DropDownList ID="ddlDesignature" runat="server" AppendDataBoundItems="true" Width="200px">
                                                    </asp:DropDownList>--%>
                                            <%-- <asp:DropDownList ID="ddlmaindept" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                    </asp:DropDownList>--%>

                                            <asp:TextBox ID="txtEditFieldDT" runat="server" MaxLength="100" Text='<%#Eval("COLUMN_NAME","{0:dd/MM/yyyy}")%>'
                                                ToolTip='<%#Eval("IDNO")%>' CssClass="form-control"> </asp:TextBox>

                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("COLUMN_NAME")%>' />
                                            <ajaxToolKit:MaskedEditExtender ID="meeAllotmentDate" runat="server" TargetControlID="txtEditFieldDT"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                OnInvalidCssClass="errordate" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeAllotmentDate"
                                                ControlToValidate="txtEditFieldDT" IsValidEmpty="False" EmptyValueMessage="Increment Date is required"
                                                InvalidValueMessage="Increment date is invalid" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                Display="Dynamic" />

                                            <asp:HiddenField ID="hdneditfield" runat="server" Value='<%#Eval("COLUMN_NAME")%>' />

                                            <asp:HiddenField ID="hdnidno" runat="server" Value='<%#Eval("SCALENO")%>' />
                                            <asp:HiddenField ID="hdnpayrule" runat="server" Value='<%#Eval("PAYRULE")%>' />

                                            <asp:HiddenField ID="hdnAppointment" runat="server" Value='<%#Eval("APPOINTNO")%>' />
                                            <asp:HiddenField ID="hdnStafftype" runat="server" Value='<%#Eval("STNO")%>' />
                                            <asp:HiddenField ID="hdnBloodGrp" runat="server" Value='<%#Eval("BLOODGRPNO")%>' />
                                            <asp:HiddenField ID="hdnStaffno" runat="server" Value='<%#Eval("STAFFNO")%>' />
                                            <asp:HiddenField ID="hdnDepartment" runat="server" Value='<%#Eval("SUBDEPTNO")%>' />
                                            <asp:HiddenField ID="hdnDesignation" runat="server" Value='<%#Eval("SUBDESIGNO")%>' />
                                            <asp:HiddenField ID="hdnDesignature" runat="server" Value='<%#Eval("DESIGNATURENO")%>' />
                                            <asp:HiddenField ID="hdnmaindept" runat="server" Value='<%#Eval("MAINDEPTNO")%>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll"
                                OnClick="btnSub_Click" OnClientClick="return ConfirmMessage();" CssClass="btn btn-primary" TabIndex="5" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="6" />
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
    <%-- </ContentTemplate>
    </asp:UpdatePanel>
    --%>

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
            //              var payHead= document.getElementById("ctl00_ContentPlaceHolder1_ddlPayhead").value;
            var payHead = document.getElementById("ctl00_ContentPlaceHolder1_ddlPayhead");
            var hidPayhead = document.getElementById("ctl00_ContentPlaceHolder1_hidPayhead").value;

            // var e = document.getElementById("ddlViewBy");
            var HeadName = payHead.options[payHead.selectedIndex].text;



            if (confirm("Do you want to save changes in " + hidPayhead + " " + HeadName)) {
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


        function Increment(me, vall) {
            var payHead = document.getElementById("ctl00_ContentPlaceHolder1_ddlPayhead");
            var HeadName = payHead.options[payHead.selectedIndex].text;
            if (HeadName == "NPA") {
                if (me.value == "Y" || me.value == "N") {

                }

                else {
                    alert("Please Enter  (Y or N) only");
                    me.value = null;
                    me.value = "";
                    document.getElementById("" + me.id + "").focus();
                    return false;
                }
            }

        }


    </script>

</asp:Content>


