<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Emp_Bulk_Paylevel_Update.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Emp_Bulk_Paylevel_Update" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">BULK EMPLOYEE UPDATE</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlSelect" runat="server">
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Select Field/Staff</h5>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Employee Field</label>
                                    </div>
                                    <asp:DropDownList ID="ddlPayhead" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlPayhead_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvPayhead" runat="server" ControlToValidate="ddlPayhead"
                                        Display="None" ErrorMessage="Please Select Field" ValidationGroup="payroll"
                                        InitialValue="0"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>College</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                        AutoPostBack="true" TabIndex="19" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                        ValidationGroup="payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                        InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Staff</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                        <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                        <asp:ListItem Value="0">All Staff</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStaff"
                                        ValidationGroup="payroll" ErrorMessage="Please select Staff" SetFocusOnError="true"
                                        InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer mt-3">
                            <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="payroll" CssClass="btn btn-primary"
                                OnClick="btnShow_Click" />
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="payroll"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                        </div>

                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                    </div>
                    <asp:Panel ID="pnlMonthlyChanges" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Total Employees</label>
                                    </div>
                                    <asp:TextBox ID="txtEmpoyeeCount" runat="server" BorderStyle="None"></asp:TextBox>
                                    <asp:TextBox ID="txtPayheadName" runat="server" BorderStyle="None"></asp:TextBox>
                                    <asp:TextBox ID="txtAmount" runat="server" BorderStyle="None"></asp:TextBox>
                                    <asp:HiddenField ID="hidPayhead" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="col-12 mt-3">
                            <asp:ListView ID="lvMonthlyChanges" runat="server" OnItemDataBound="lvMonthlyChanges_ItemDataBound">
                                <EmptyDataTemplate>
                                    <div class="text-center">
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                    </div>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="vista-grid">
                                        <div class="sub-heading">
                                            <h5>Bulk Employee Update</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Idno </th>
                                                    <th>Name </th>
                                                    <th>Basic </th>
                                       <%--             <th>Field</th>--%>
                                                    <th>Pay level </th>
                                                    <th>Cell No.</th>
                                                </tr>
                                                <thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
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
                                            <%#Eval("Basic")%>
                                        </td>
                                        <td style="display:none">
                                            <asp:TextBox ID="txtEditField" runat="server" MaxLength="100" Text='<%#Eval("COLUMN_NAME")%>'
                                                ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" onkeyup="return Increment(this);" />
                                            <asp:DropDownList ID="ddleditfield" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlScale" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlStaffType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:CheckBox ID="chkShiftManagment" runat="server" Checked='<%#(Convert.ToInt32(Eval("IS_SHIFT_MANAGMENT"))==1 ? true : false)%>' />
                                            <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" Visible="false">
                                            </asp:DropDownList>
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
                                            <asp:HiddenField ID="hdnStaffType" runat="server" Value='<%#Eval("STNO")%>' />
                                            <asp:HiddenField ID="hdnDesigNo" runat="server" Value='<%#Eval("SUBDESIGNO")%>' />
                                            <asp:HiddenField ID="hdnPaylevel" runat="server" Value='<%#Eval("PAYLEVELNO")%>' />
                                            <asp:HiddenField ID="hdnCellNo" runat="server" Value='<%#Eval("Id")%>' />
                                        </td>
                                        <td>
                                          <%--  <asp:Label ID="lblpaylevel" runat="server" Visible="false"></asp:Label>--%>
                                            <asp:DropDownList ID="ddlpayLevel" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                           <%-- <asp:Label ID="lblCellNo" runat="server"  Visible="false"></asp:Label>--%>
                                            <asp:DropDownList ID="ddlCellNo" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <%--   <AlternatingItemTemplate>
                                    <tr class="altitem" >
                                        <td>
                                            <%#Eval("IDNO")%>
                                        </td>
                                        <td>
                                            <%#Eval("NAME")%>
                                        </td>
                                        <td>
                                            <%#Eval("Basic")%>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEditField" runat="server" MaxLength="100" Text='<%#Eval("COLUMN_NAME")%>'
                                                ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" onkeyup="return Increment(this);" />
                                            <asp:DropDownList ID="ddleditfield" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlScale" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlStaffType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:CheckBox ID="chkShiftManagment" runat="server" Width="10px" Checked='<%#(Convert.ToInt32(Eval("IS_SHIFT_MANAGMENT"))==1 ? true : false)%>' />
                                            <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" Visible="false">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtEditFieldDT" runat="server" MaxLength="100" Text='<%#Eval("COLUMN_NAME","{0:dd/MM/yyyy}")%>'
                                                ToolTip='<%#Eval("IDNO")%>' CssClass="form-control"> </asp:TextBox>
                                            <asp:HiddenField ID="hdneditfield" runat="server" Value='<%#Eval("COLUMN_NAME")%>' />
                                            <ajaxToolKit:MaskedEditExtender ID="meeAllotmentDate" runat="server" TargetControlID="txtEditFieldDT"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                OnInvalidCssClass="errordate" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeAllotmentDate"
                                                ControlToValidate="txtEditFieldDT" IsValidEmpty="False" EmptyValueMessage="Increment Date is required"
                                                InvalidValueMessage="Increment date is invalid" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                Display="Dynamic" />
                                            <asp:HiddenField ID="hdnidno" runat="server" Value='<%#Eval("SCALENO")%>' />
                                            <asp:HiddenField ID="hdnpayrule" runat="server" Value='<%#Eval("PAYRULE")%>' />
                                            <asp:HiddenField ID="hdnStaffType" runat="server" Value='<%#Eval("STNO")%>' />
                                            <asp:HiddenField ID="hdnDesigNo" runat="server" Value='<%#Eval("SUBDESIGNO")%>' />
                                            <asp:HiddenField ID="hdnPaylevel" runat="server" Value='<%#Eval("PAYLEVELNO")%>' />
                                            <asp:HiddenField ID="hdnCellNo" runat="server" Value='<%#Eval("Id")%>' />
                                            <td>
                                                <td>
                                                   
                                                    <asp:DropDownList ID="ddlpayLevel" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    
                                                    <asp:DropDownList ID="ddlCellNo" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                </td>
                                            </td>
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>--%>
                            </asp:ListView>
                        </div>
                        <div class="col-12 btn-footer mt-4">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" CssClass="btn btn-outline-primary"
                                OnClick="btnSub_Click" OnClientClick="return ConfirmMessage();" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                OnClick="btnCancel_Click" CssClass="btn btn-warning" />
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

