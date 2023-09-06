<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="WaterMeterReading.aspx.cs"
    Inherits="ESTATE_Transaction_WaterMeterReading" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=lvwatermeterreading.ClientID %>').Scrollable({
                ScrollHeight: 300
            });

        });
    </script>

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">WATER METER READING</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Water Meter Reading
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Reading Date<span style="color: #FF0000">*</span>:</label>
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtDate" runat="server" TabIndex="1" CssClass="form-control"
                                                            ValidationGroup="submit" />
                                                        <ajaxToolKit:CalendarExtender ID="calext" runat="server"
                                                            Enabled="true" EnableViewState="true" Format="dd/MM/yyyy"
                                                            PopupButtonID="Image5" TargetControlID="txtDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeEndDate1" runat="server"
                                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True"
                                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                            TargetControlID="txtDate" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevendDate1" runat="server"
                                                            ControlExtender="meeEndDate1" ControlToValidate="txtDate" Display="None"
                                                            EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter End Date"
                                                            InvalidValueBlurredMessage="Invalid Date"
                                                            InvalidValueMessage="End Date is Invalid (Enter dd/MM/yyyy Format)"
                                                            SetFocusOnError="True" TooltipMessage="Please Enter End Date"
                                                            ValidationGroup="submit" />
                                                        <asp:RequiredFieldValidator ID="rfvdate" runat="server"
                                                            ControlToValidate="txtDate" ErrorMessage="Please Select Date" Display="None"
                                                            ValidationGroup="submit">
                                                        </asp:RequiredFieldValidator>
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <br />
                                                    <asp:Button ID="btnshow" runat="server" Text="Show Reading" CssClass="btn btn-primary"
                                                        TabIndex="2" OnClick="btnshow_Click" ValidationGroup="submit" />
                                                    <asp:Button ID="btnreset" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                                        TabIndex="3" OnClick="btnreset_Click" />
                                                    <asp:ValidationSummary ID="validsummary" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="submit" />
                                                </div>
                                            </div>

                                            <div class="form-group row" id="waterReading" runat="server">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:Panel ID="pnlwaterreading" runat="server" ScrollBars="Vertical">
                                                            <asp:ListView ID="lvwatermeterreading" runat="server">
                                                                <EmptyDataTemplate>
                                                                    <p class="text-center text-bold">
                                                                        <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                                    </p>
                                                                </EmptyDataTemplate>
                                                                <LayoutTemplate>
                                                                    <div class="vista-grid">
                                                                        <h4 class="box-title">Water Meter Reading List</h4>
                                                                        <table id="tblitems" class="table table-bordered table-hover">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th>SRNO
                                                                                    </th>
                                                                                    <th>NAME
                                                                                    </th>
                                                                                    <th>QTRNO
                                                                                    </th>
                                                                                    <th>METER NO
                                                                                    </th>
                                                                                    <th>OLD READING
                                                                                    </th>
                                                                                    <th>CURRENT READING
                                                                                    </th>
                                                                                    <th>ADJ UNIT
                                                                                    </th>
                                                                                    <%--<td>
                                                                                        </td>--%>
                                                                                    <th>TOTAL
                                                                                    </th>
                                                                                    <th>STATUS 
                                                                                    </th>
                                                                                </tr>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                                <thead>
                                                                                </thead>
                                                                            </thead>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblsrno" runat="server" Font-Names="Verdana"
                                                                                Text='<%#Eval("SRNO")%>' ToolTip='<%#Eval("idno")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblname" runat="server" Font-Names="Verdana"
                                                                                Text='<%#Eval("CONSUMERFULLNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblqrtNo" runat="server" Font-Names="Verdana" ReadOnly="true"
                                                                                Text='<%#Eval("QUARTER_NO") %>'> </asp:Label>
                                                                            <asp:HiddenField ID="hdnquarterno" runat="server" Value='<%#Eval("QTRIDNO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblmeterNo" runat="server" Font-Names="Verdana" ReadOnly="true"
                                                                                Text='<%#Eval("WATERMETERNO") %>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnwatermeterno" runat="server" Value='<%#Eval("M_ID") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtoldreading" runat="server" Font-Names="Verdana" Text='<%#Eval("OLD_R") %>'
                                                                                CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbold" runat="server" FilterType="Numbers" TargetControlID="txtoldreading">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>


                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtcurrentReading" runat="server" Font-Names="Verdana" CssClass="form-control" TabIndex="3"
                                                                                Text='<%#Eval("CURR_R")%>'></asp:TextBox>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftxNumber" runat="server" FilterType="Numbers" TargetControlID="txtcurrentReading">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtadjUnits" runat="server" Font-Names="Verdana" Text='<%#Eval("ADJ_U")%>' CssClass="form-control"
                                                                                onblur="CalculateTotItemAmt();" TabIndex="4"></asp:TextBox>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="filtxtadjUnits" runat="server" ValidChars="0123456789-" TargetControlID="txtadjUnits">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>

                                                                        </td>
                                                                        <%-- <td>
                                                                            <asp:CheckBox ID="chkIDNO" runat="server"  Visible ="false" />
                                                                           </td>--%>
                                                                        <td>
                                                                            <asp:TextBox ID="txttotalReading" runat="server" Font-Names="Verdana" CssClass="form-control"
                                                                                onClick="CalculateTotItemAmt();" TabIndex="5"
                                                                                Text='<%#Eval("TOTAL")%>'></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtstatus" runat="server" Font-Names="Verdana" CssClass="form-control"
                                                                                Text='<%#Eval("CHKSTATUS")%>' TabIndex="6"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row" id="pnlbutton" runat="server">
                                                <div class="col-md-12">
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSubmitData" runat="server" TabIndex="7" Text="Submit" ValidationGroup="submit"
                                                            CssClass="btn btn-primary" OnClick="btnSubmitData_Click" />
                                                        <asp:Button ID="btnPrint" runat="server" Text="Print" TabIndex="8" OnClick="btnPrint_Click"
                                                            CssClass="btn btn-info" />
                                                        <asp:Button ID="btnCancel" runat="server" TabIndex="9" Text="Cancel" CssClass="btn btn-warning"
                                                            OnClick="btnCancel_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>

    <script type="text/javascript">



        function calculateAmtonCheck() {
            var result = confirm("Want to Update?");
            if (result == true) {

                var table = document.getElementById('tblitems');

                var tot = 0.00;
                var lbladj = 0.00;
                for (var r = 0; r < table.rows.length - 1; r++) {

                    var lblold = document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txtoldreading');
                    var lblcurr = document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txtcurrentReading');
                    lbladj = document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txtadjUnits');
                    var txttot = document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txttotalReading');
                    var txttot = document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txttotalReading');
                    var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_chkIDNO')

                    document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txttotalReading').value = '';
                    if (chk.checked == true) {
                        if (lblcurr.value != '') {
                            if (lbladj.value != '') {

                                tot = (parseInt(lblcurr.value) - parseInt(lblold.value)) + (parseInt(lbladj.value));
                                document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txttotalReading').value = tot;
                            }
                            else {

                                tot = (parseInt(lblcurr.value) - parseInt(lblold.value)) - (parseInt(0));
                                document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txttotalReading').value = tot;

                            }

                        }
                    }

                }
            }
            else {

                var table = document.getElementById('tblitems');

                var tot = 0.00;
                for (var r = 0; r < table.rows.length - 1; r++) {

                    var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_chkIDNO')
                    chk.checked = false;

                }

            }



        }


        function CalculateTotItemAmt() {
            var table = document.getElementById('tblitems');
            var tot = 0.00;
            var lbladj = 0.00;
            for (var r = 0; r < table.rows.length - 1; r++) {

                var lblold = document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txtoldreading');
                var lblcurr = document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txtcurrentReading');

                lbladj = document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txtadjUnits');
                var txttot = document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txttotalReading');
                //  var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_chkIDNO')

                document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txttotalReading').value = '';
                if (lblcurr.value != '' && txttot.value == '') {

                    if (lbladj.value != '') {

                        tot = (parseInt(lblcurr.value) - parseInt(lblold.value)) + (parseInt(lbladj.value));
                        document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txttotalReading').value = tot;
                        // chk.checked = true;
                        // document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_chkIDNO').disabled = true;
                    }
                    else {

                        tot = (parseInt(lblcurr.value) - parseInt(lblold.value)) - (parseInt(0));
                        document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_txttotalReading').value = tot;
                        // chk.checked = true;
                        // document.getElementById('ctl00_ContentPlaceHolder1_lvwatermeterreading_ctrl' + r.toString() + '_chkIDNO').disabled = true;
                    }

                }
            }

        }


    </script>

</asp:Content>

