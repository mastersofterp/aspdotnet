<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EnergyMeterReading.aspx.cs"
    Inherits="ESTATE_Transaction_EnergyMeterReading" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=lvenergymeterreading.ClientID %>').Scrollable({
                ScrollHeight: 400
            });

        });
    </script>

    <style type="text/css">
        .Calendar .ajax__calendar_body {
            width: 200px;
            height: 170px; /* modified */
            position: relative;
            border: solid 0px;
        }

        .Calendar .ajax__calendar_container {
            background-color: #ffffff;
            border: 1px solid #646464;
            color: #000000;
            width: 195px;
            height: 210px;
        }

        .Calendar .ajax__calendar_footer {
            border: solid top 1px;
            cursor: pointer;
            padding-top: 3px;
            height: 6px;
        }

        .Calendar .ajax__calendar_day {
            cursor: pointer;
            height: 17px;
            padding: 0 2px;
            text-align: right;
            width: 18px;
        }

        .Calendar .ajax__calendar_year {
            border: solid 1px #E0E0E0;
            /*font-family: Tahoma, Calibri, sans-serif;*/
            font-family: Verdana;
            font-size: 10px;
            text-align: center;
            font-weight: bold;
            text-shadow: 0px 0px 2px #D3D3D3;
            text-align: center !important;
            vertical-align: middle;
            margin: 1px;
            height: 40px; /* added */
        }

        .Calendar .ajax__calendar_month {
            border: solid 1px #E0E0E0;
            /*font-family: Tahoma, Calibri, sans-serif;*/
            font-family: Verdana;
            font-size: 10px;
            text-align: center;
            font-weight: bold;
            text-shadow: 0px 0px 2px #D3D3D3;
            text-align: center !important;
            vertical-align: middle;
            /* margin: 1px;
        height: 40px; /* added */
        }
    </style>

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ENERGY METER READING</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="panel panel-info">
                                    <div class="panel panel-heading">Add/Edit Energy Meter Reading</div>
                                    <div class="panel panel-body">
                                        <div class="form-group row">
                                            <div class="col-md-4">
                                                <label>Select Reading Month<span style="color: red;">*</span> :</label>
                                                <div class="input-group date">
                                                    <asp:TextBox ID="txtDate" runat="server" TabIndex="1" MaxLength="7" ValidationGroup="submit" CssClass="form-control" />
                                                    <ajaxToolKit:CalendarExtender ID="calext" runat="server" Enabled="true" EnableViewState="true" Format="MM/yyyy"
                                                        PopupButtonID="Image1" TargetControlID="txtDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvdate" runat="server"
                                                        ControlToValidate="txtDate" ErrorMessage="Please Enter Month & Year in (MM/YYYY Format)"
                                                        Display="None" ValidationGroup="submit">
                                                    </asp:RequiredFieldValidator>
                                                    <div class="input-group-addon">
                                                        <%--<asp:Image ID="Image5" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                        <asp:ImageButton runat="Server" ID="Image1" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <label>Block :</label>
                                                <asp:DropDownList ID="ddlBlock" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="2">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-4">
                                                <br />
                                                <asp:Button ID="btnshow" runat="server" Text="Show Reading" TabIndex="3" CssClass="btn btn-primary"
                                                    OnClick="btnshow_Click" ValidationGroup="submit" />
                                                <asp:ValidationSummary ID="validsummary" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="submit" />
                                            </div>
                                        </div>

                                        <div class="row" id="energyReading" runat="server">
                                            <div class="col-md-12">
                                                <div class="panel panel-info">
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:ListView ID="lvenergymeterreading" runat="server">
                                                        <EmptyDataTemplate>
                                                            <p class="text-center text-bold">
                                                                <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                            </p>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <h4 class="box-title">Energy Meter Reading</h4>
                                                                <table id="tblitems" class="table table-bordered table-hover">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>SRNO.
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
                                                                            <th>READING DATE
                                                                            </th>
                                                                            <th id="adjTh" runat="server" visible="false">ADJ.UNIT
                                                                            </th>
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
                                                                    <asp:Label ID="lblsrno" runat="server" Font-Names="Verdana" Text='<%#Eval("SRNO")%>'
                                                                        ToolTip='<%#Eval("idno")%>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnQaId" runat="server" Value='<%#Eval("QA_ID")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblname" runat="server" Font-Names="Verdana"
                                                                        Text='<%#Eval("CONSUMERFULLNAME")%>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnEmpCode" runat="server" Value='<%#Eval("PFILENO")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblqrtNo" runat="server" Font-Names="Verdana" ReadOnly="true"
                                                                        Text='<%#Eval("QUARTER_NO") %>'> </asp:Label>
                                                                    <asp:HiddenField ID="hdnquarterno" runat="server" Value='<%#Eval("QTRIDNO") %>' />

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblmeterNo" runat="server" Font-Names="Verdana" ReadOnly="true"
                                                                        Text='<%#Eval("ENERGYMETERNO") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnenergymeterno" runat="server" Value='<%#Eval("M_ID") %>' />
                                                                </td>

                                                                <td>
                                                                    <asp:TextBox ID="txtoldreading" runat="server" Font-Names="Verdana" Text='<%#Eval("OLD_R") %>'
                                                                        CssClass="form-control" TabIndex="4"></asp:TextBox>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbold" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtoldreading">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtcurrentReading" runat="server" Font-Names="Verdana" Text='<%#Eval("CURR_R")%>'
                                                                        onBlur="CalculateTotItemAmt();" CssClass="form-control" TabIndex="5"></asp:TextBox>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftxNumber" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtcurrentReading">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                    
                                                                </td>
                                                                <td>
                                                                    <div class="input-group date">
                                                                       <%-- <asp:TextBox ID="txtReadingDate" runat="server" Font-Names="Verdana" TabIndex="6"
                                                                            Text='<%#Eval("READING_DATE")%>' CssClass="form-control"></asp:TextBox>
                                                                          <ajaxToolKit:CalendarExtender ID="calreading" runat="server" Enabled="true" EnableViewState="true" Format="DD/MM/yyyy"
                                                                           PopupButtonID="imgRD" TargetControlID="txtReadingDate">
                                                                          </ajaxToolKit:CalendarExtender>
                                                                        <ajaxToolKit:MaskedEditExtender ID="msedatebirth" runat="server" AcceptAMPM="True"
                                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                                            ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtReadingDate" />
                                                                         <div class="input-group-addon">
                                                                            <asp:Image ID="imgRD" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                             
                                                                        </div>--%>
                                                                        
                                                            <asp:TextBox ID="txtReadingDate" runat="server" CssClass="form-control" TabIndex="6" Text='<%#Eval("READING_DATE")%>'></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtenderdt"  runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtReadingDate" PopupButtonID="imgorddtd"
                                                                Enabled="True">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtReadingDate"
                                                                Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" />
                                                            <div class="input-group-addon">
                                                               <%-- <asp:Image ID="imgorddtd" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                                <asp:ImageButton runat="Server" ID="imgorddtd" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                            </div>
                                                        </div>

                                                                       
                                                                   
                                                                </td>

                                                                <td id="adj" runat="server" visible="false">
                                                                    <asp:TextBox ID="txtadjUnits" runat="server" Font-Names="Verdana" Text='<%#Eval("ADJ_U")%>'
                                                                        onBlur="CalculateTotItemAmt();" CssClass="form-control" TabIndex="7"></asp:TextBox>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="filtxtadjUnits" runat="server" ValidChars="0123456789-"
                                                                        TargetControlID="txtadjUnits">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </td>
                                                                <%--  <td>
                                                                       <asp:CheckBox ID="chkIDNO"  runat="server"  Visible ="false" /></td>--%>
                                                                <td>
                                                                    <asp:TextBox ID="txttotalReading" runat="server" Font-Names="Verdana" CssClass="form-control"
                                                                        onClick="CalculateTotItemAmt();" TabIndex="8"
                                                                        Text='<%#Eval("TOTAL")%>'></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtstatus" runat="server" Font-Names="Verdana" CssClass="form-control"
                                                                        Text='<%#Eval("CHKSTATUS")%>' TabIndex="9"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row" id="trshowbutton" runat="server">
                                            <div class="col-md-12">
                                                <div class="panel panel-info">
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="text-center">
                                                    <asp:Button ID="btnSubmitData" runat="server" TabIndex="10" Text="Submit"
                                                        CssClass="btn btn-primary" OnClick="btnSubmitData_Click" />
                                                    <asp:Button ID="btnPrint" runat="server" TabIndex="11" Text="Print"
                                                        CssClass="btn btn-info" OnClick="btnPrint_Click" />
                                                    <asp:Button ID="btnreset" runat="server" Text="Reset" TabIndex="12"
                                                        CssClass="btn btn-warning" OnClick="btnreset_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">



        function calculateAmtonCheck() {
            var result = confirm("Want to Update?");
            if (result == true) {

                var table = document.getElementById('tblitems');
                var tot = 0.00;
                for (var r = 0; r < table.rows.length - 1; r++) {

                    var lblold = document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_txtoldreading');
                    var lblcurr = document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_txtcurrentReading');
                    var lbladj = document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_txtadjUnits');
                    var txttot = document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_txttotalReading');
                    var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_chkIDNO')
                    document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_txttotalReading').value = '';
                    if (chk.checked == true) {
                        if (lblcurr.value != '') {
                            if (lbladj.value != '') {
                                tot = (parseInt(lblcurr.value) - parseInt(lblold.value)) + (parseInt(lbladj.value));
                                document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_txttotalReading').value = tot;
                            }
                            else {
                                tot = (parseInt(lblcurr.value) - parseInt(lblold.value)) + (parseInt(0));
                                document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_txttotalReading').value = tot;
                            }
                        }
                    }
                }
            }
            else {

                var table = document.getElementById('tblitems');
                var tot = 0.00;
                for (var r = 0; r < table.rows.length - 1; r++) {
                    var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_chkIDNO')
                    chk.checked = false;
                }
            }
        }
    </script>

    <script type="text/javascript">
        function CalculateTotItemAmt() {            
            var table = document.getElementById('tblitems');
            var tot = 0.00;
            for (var r = 0; r < table.rows.length - 1; r++) {

                var lblold = document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_txtoldreading');
                var lblcurr = document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_txtcurrentReading');
                // var lbladj = document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_txtadjUnits');
                var txttot = document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_txttotalReading');
                document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_txttotalReading').value = '';

               

                if (lblcurr.value != '' && txttot.value == '')
                {
                    //if (lbladj.value != '') {
                    tot = (parseInt(lblcurr.value) - parseInt(lblold.value)); // + (parseInt(lbladj.value));
                    document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_txttotalReading').value = tot;
                }
                else {
                    tot = (parseInt(lblcurr.value) - parseInt(lblold.value));// + (parseInt(0));
                    document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r.toString() + '_txttotalReading').value = tot;
                }

            }
        }


    </script>


    <script type="text/javascript">
        ; debugger
        function CheckCurrentReading() {
            var table = document.getElementById('tblitems');
            var i;
            for (var r = 0; r < table.rows.length - 1; r++) {
                var lblold = document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r + '_txtoldreading');

                var lblcurr = document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r + '_txtcurrentReading');

                // if (lblcurr.value != '0') {
                if (parseInt(lblold.value) > parseInt(lblcurr.value)) {
                    //i = 1;
                    //if (i == 1) {
                    document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r + '_txtcurrentReading').value = '';
                    alert('Current Reading Should Be Greater Than Old Reading.');
                    break;
                }
                //document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r + '_txtcurrentReading').value = '';
                //break;
                //}
                //else {
                //    //document.getElementById('ctl00_ContentPlaceHolder1_lvenergymeterreading_ctrl' + r + '_txtcurrentReading').value = '';
                //    return;
                //  }
                // }
            }
        }

    </script>

    
</asp:Content>

