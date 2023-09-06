<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LeaveShiftManagement.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_LeaveShiftManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <script src="../../../includes/prototype.js" type="text/javascript"></script>
    <script src="../../../includes/scriptaculous.js" type="text/javascript"></script>
    <script src="../../../includes/modalbox.js" type="text/javascript"></script>
    <script src="../../../jquery/jquery-1.10.2.min.js" type="text/javascript"></script>--%>

    <script type="text/javascript" language="javascript">

        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }
        ; debugger
        function DAYOFFColorChange1(vall) {
            var st = vall.id.split("lvIncharge_ctrl");
            var i = st[1].split("_ddlDayOff1");
            var index = i[0];
            var DayOffVal = document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '__ddlDayOff1').value;
            if (DayOffVal == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '__ddlDayOff1').style.color = "Black";
            }
            else {

                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '__ddlDayOff1').style.background = "Blue";
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '__ddlDayOff1').style.color = "White";
            }

        }
        function ColorChange1(vall) {


            var st = vall.id.split("lvIncharge_ctrl");
            var i = st[1].split("_ddlDay1");
            var index = i[0];
            var DayOffVal = document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay1').value;
            if (DayOffVal == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay1').style.color = "Red";
                //             document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay1').style.disabled = false;
            }
            else
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay1').style.color = "Black";
        }
        function ColorChange2(vall) {


            var st = vall.id.split("lvIncharge_ctrl");
            var i = st[1].split("_ddlDay2");
            var index = i[0];
            var DayOffVal = document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay2').value;
            if (DayOffVal == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay2').style.color = "Red";
            }
            else
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay2').style.color = "Black";
        }

        function ColorChange3(vall) {
            var st = vall.id.split("lvIncharge_ctrl");
            var i = st[1].split("_ddlDay3");
            var index = i[0];
            var DayOffVal = document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay3').value;
            if (DayOffVal == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay3').style.color = "Red";
            }
            else
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay3').style.color = "Black";
        }
        function ColorChange4(vall) {
            var st = vall.id.split("lvIncharge_ctrl");
            var i = st[1].split("_ddlDay4");
            var index = i[0];
            var DayOffVal = document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay4').value;
            if (DayOffVal == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay4').style.color = "Red";
            }
            else
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay4').style.color = "Black";
        }
        function ColorChange5(vall) {
            var st = vall.id.split("lvIncharge_ctrl");
            var i = st[1].split("_ddlDay5");
            var index = i[0];
            var DayOffVal = document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay5').value;
            if (DayOffVal == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay5').style.color = "Red";
            }
            else
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay5').style.color = "Black";
        }
        function ColorChange6(vall) {
            var st = vall.id.split("lvIncharge_ctrl");
            var i = st[1].split("_ddlDay6");
            var index = i[0];
            var DayOffVal = document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay6').value;
            if (DayOffVal == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay6').style.color = "Red";
            }
            else
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay6').style.color = "Black";
        }
        function ColorChange7(vall) {
            var st = vall.id.split("lvIncharge_ctrl");
            var i = st[1].split("_ddlDay7");
            var index = i[0];
            var DayOffVal = document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay7').value;
            if (DayOffVal == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay7').style.color = "Red";
            }
            else
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncharge_ctrl' + index + '_ddlDay7').style.color = "Black";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <style>
        .drop-new, td{
            display:flex;
        }
    </style>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">LEAVE SHIFT MANAGEMENT </h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlSelect" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgCalFromDate">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Enabled="true"
                                            TabIndex="1" AutoPostBack="true" OnTextChanged="txtFromDate_TextChanged" />

                                        <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meeFromDate"
                                            ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter From Date"
                                            InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Incharge" SetFocusOnError="True" />
                                        <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDate"
                                            Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Incharge"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>To Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgCalToDate">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Enabled="false"
                                            TabIndex="1" />
                                        <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="false"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server"
                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtToDate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server"
                                            ControlExtender="meeTodt" ControlToValidate="txtToDate" Display="None"
                                            EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter To Date"
                                            InvalidValueBlurredMessage="Invalid Date"
                                            InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter To Date"
                                            ValidationGroup="Leaveapp">
                                                          &#160;&#160;
                                        </ajaxToolKit:MaskedEditValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDate"
                                            Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Incharge"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtToDate"
                                            CultureInvariantValues="true" Display="None" ErrorMessage="To Date Must Be Equal Or Greater Than From Date"
                                            Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"
                                            ValidationGroup="Incharge" ControlToCompare="txtFromDate" />
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>College</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                        TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" ErrorMessage="Please Select College" ValidationGroup="Incharge"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Incharge Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlIncharge" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                        TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlIncharge_SelectedIndexChanged">
                                        <%--OnSelectedIndexChanged="ddlIncharge_SelectedIndexChanged"--%>
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="rfvIncharge" runat="server" ControlToValidate="ddlIncharge"
                                        Display="None" ErrorMessage="Please Select Incharge Name" ValidationGroup="Incharge"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>

                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Incharge"
                                CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            <asp:Button ID="btnReport" runat="server" Text="Report-Format1"
                                CssClass="btn btn-info" OnClick="btnReport_Click" ValidationGroup="Incharge" />
                            <asp:Button ID="btnReportFormat2" runat="server" Text="Report-Format2"
                                CssClass="btn btn-info" OnClick="btnReportFormat2_Click" ValidationGroup="Incharge" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Incharge"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="pnlNote" runat="server" Width="100%">
                        <div class="form-group col-lg-4 col-md-12 col-12" id="tdAbsentDays" runat="server" visible="false">
                            <div class=" note-div" id="trNote" runat="server">
                                <h5 class="heading">Note </h5>
                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Red Mark shows, Shift Not yet alloted </span></p>
                            </div>
                        </div>


                    </asp:Panel>

                    <div class="table-responsive">
                        <asp:Panel ID="pnlIncharge" runat="server">
                            <asp:ListView ID="lvIncharge" runat="server" OnItemDataBound="lvIncharge_ItemDataBound">
                                <LayoutTemplate>
                                    <div class="vista-grid">
                                        <div class="sub-heading">
                                            <h5>Employee Attendance Entry</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <div class="form-group col-lg-10 col-md-12 col-12">
                                                        <div class=" note-div">
                                                            <h5 class="heading">Note </h5>
                                                            <p id="btnMSG" class="" enabled="false"><i class="fa fa-star" aria-hidden="true"></i><span>(Day-Off):-To Make Eligible For Comp-Off Against Day-Off, Select D/O from beside Dropdown With Working Shift"  </span></p>
                                                            <p>
                                                                <i class="fa fa-star" aria-hidden="true"></i><span>(Holiday):-To Make Eligible For Comp-Off Against Holiday, Select Working Shift from Dropdown"
                                                            </p>
                                                        </div>
                                                </tr>
                                                <tr>
                                                    <th>Employee Name                                             
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lbldate1" runat="server" Text=""></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lbldate2" runat="server" Text=""></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lbldate3" runat="server" Text=""></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lbldate4" runat="server" Text=""></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lbldate5" runat="server" Text=""></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lbldate6" runat="server" Text=""></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lbldate7" runat="server" Text=""></asp:Label>
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
                                    <tr class="">
                                        <%--<td style="width:15% ;border:solid 1px #347D9F;text-align:center">--%>
                                        <td>
                                            <asp:Label ID="lblEmp" runat="server" Text='<%# Eval("NAME")%>' ToolTip='<%# Eval("EMPLOYEEIDNO")%>'></asp:Label>
                                            <asp:HiddenField ID="hdnLock" runat="server" Value='<%#Eval("LOCK")%>' />
                                            <%--Checked='<%# Eval("WorkShiftId").ToString() == "0" ? false : true %>' --%>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDay1" runat="server"  AppendDataBoundItems="True" onchange="return ColorChange1(this);">
                                                <%--onblur  onselect onchange--%>
                                                <%--<asp:ListItem  Value="0">Select</asp:ListItem>--%> <%--Enabled='<%# Eval("LOCK").ToString() == "N" ? false : true %>' --%>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnday1" runat="server" Value='<%#Eval("SHIFT1")%>' />
                                            <asp:DropDownList ID="ddlDayOff1" runat="server"  AppendDataBoundItems="True" onchange="return DAYOFFColorChange1(this);">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">D/O</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdndayOff1" runat="server" Value='<%#Eval("IsDayOff_SHIFT1")%>' />
                                            <%-- <asp:CheckBox id="chkDayOff1" runat="server" Width="5%" Text="Y"/>--%>
                                                                 
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDay2" runat="server" AppendDataBoundItems="True" onchange="return ColorChange2(this);">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnday2" runat="server" Value='<%#Eval("SHIFT2")%>' />
                                            <asp:DropDownList ID="ddlDayOff2" runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">D/O</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdndayOff2" runat="server" Value='<%#Eval("IsDayOff_SHIFT2")%>' />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDay3" runat="server" AppendDataBoundItems="True" onchange="return ColorChange3(this);">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnday3" runat="server" Value='<%#Eval("SHIFT3")%>' />
                                            <asp:DropDownList ID="ddlDayOff3" runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">D/O</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdndayOff3" runat="server" Value='<%#Eval("IsDayOff_SHIFT3")%>' />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDay4" runat="server" AppendDataBoundItems="True" onchange="return ColorChange4(this);">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnday4" runat="server" Value='<%#Eval("SHIFT4")%>' />
                                            <asp:DropDownList ID="ddlDayOff4" runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">D/O</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdndayOff4" runat="server" Value='<%#Eval("IsDayOff_SHIFT4")%>' />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDay5" runat="server" AppendDataBoundItems="True" onchange="return ColorChange5(this);">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnday5" runat="server" Value='<%#Eval("SHIFT5")%>' />
                                            <asp:DropDownList ID="ddlDayOff5" runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">D/O</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdndayOff5" runat="server" Value='<%#Eval("IsDayOff_SHIFT5")%>' />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDay6" runat="server" AppendDataBoundItems="True" onchange="return ColorChange6(this);">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnday6" runat="server" Value='<%#Eval("SHIFT6")%>' />
                                            <asp:DropDownList ID="ddlDayOff6" runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">D/O</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdndayOff6" runat="server" Value='<%#Eval("IsDayOff_SHIFT6")%>' />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDay7" runat="server" AppendDataBoundItems="True" onchange="return ColorChange7(this);">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnday7" runat="server" Value='<%#Eval("SHIFT7")%>' />
                                            <asp:DropDownList ID="ddlDayOff7" runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">D/O</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdndayOff7" runat="server" Value='<%#Eval("IsDayOff_SHIFT7")%>' />
                                        </td>

                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <%-- Select Shift for Employees --%>
                                    <asp:Label ID="lblEmpty" runat="server" Style="color: Red; text-align: center" Text="Record Not Found !!"></asp:Label>

                                </EmptyDataTemplate>
                            </asp:ListView>

                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
