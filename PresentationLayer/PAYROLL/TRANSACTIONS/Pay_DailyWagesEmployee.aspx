<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_DailyWagesEmployee.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_DailyWagesEmployee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
         <div class="row">
             <div class="col-md-12 col-sm-12 col-12">
                  <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">DAILY WAGES ENTRY FORM &nbsp;
                            </h3>
                        </div>
                      <div class="box-body">
                          <asp:Panel ID="pnlSelect" runat="server">
                           <div class="col-12">
                               <div class="row">
                                     <div class="col-12">
                                         <div class="sub-heading">
                                               <h5>Select College,Staff and Month</h5>
                                           </div>
                                      </div>
                                </div>
                            </div>
                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                   <div class="col-md-12">
                                       Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                   </div>
                               </div>
                             <div class="col-12">
                                <div class="row">
                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                              <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>College : </label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" >
                                                </asp:DropDownList>
                                     <%--     <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege" InitialValue="0" SetFocusOnError="true"
                                          Display="None" ErrorMessage="Please Select College" ValidationGroup="Payroll"></asp:RequiredFieldValidator>--%>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege" InitialValue="0" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="Payroll"></asp:RequiredFieldValidator>
                                           </div> 
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                              <div class="label-dynamic">
                                                      <sup>* </sup>
                                                    <label>Month :</label>
                                                </div>
                                                   <div class="input-group date">
                                                               <div class="input-group-addon">
                                                                    <asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                        Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtMonthYear" runat="server" AutoPostBack="true" onblur="return checkdate(this);" OnTextChanged="txtMonthYear_TextChanged" 
                                                                    ToolTip="Enter Month and Year" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                                    Format="MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtMonthYear">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtMonthYear"
                                                                    Display="None" ErrorMessage="Please Month &amp; Year in (MM/YYYY Format)" SetFocusOnError="True"
                                                                    ValidationGroup="Payroll">
                                                                </asp:RequiredFieldValidator>
                                                    </div> 
                                            </div> 
                                           <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Staff : </label>
                                                </div>
                                                    <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" > 
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="rfvstaff" runat="server" ControlToValidate="ddlStaff" InitialValue="0"
                                                        Display="None" ErrorMessage="Please Select Staff" SetFocusOnError="true" ValidationGroup="Payroll" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                  <div class="label-dynamic">
                                                    <label> Department : </label>
                                                </div>
                                                    <asp:DropDownList ID="ddldepratment" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"  AutoPostBack="true" OnSelectedIndexChanged="ddldepratment_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                            </div>
                                 </div>
                              </div>
                              <div class="col-12 btn-footer">
                                <asp:Button ID="btnshow" runat="server" Text="Show"  CssClass="btn btn-primary" OnClick="btnshow_Click"  ValidationGroup="Payroll"/>
                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll" DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                             </div>
                             <div class="form-group row">
                                 <div class="col-md-12">
                                     <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                     <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                 </div>
                                 </div>
                             <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlIncrement" runat="server">
                                                        <asp:ListView ID="lvIncrement" runat="server"  OnItemDataBound="lvIncrement_ItemDataBound">
                                                            <EmptyDataTemplate>
                                                                <br />
                                                                <center>
                                                                     <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Employees For Increment" /></center>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div>
                                                                    <div class="box-title">
                                                                        Daily Wages
                                                                    </div>
                                                                        <table class="table table-striped table-bordered nowrap display">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>
                                                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="totalAppointment(this)" />
                                                                                </th>
                                                                                <th>Idno
                                                                                </th>
                                                                                <th>Employee Name
                                                                                </th>
                                                                               <%-- <th>Department</th>--%>
                                                                                <th>Designation
                                                                                </th>                                      
                                                                                <th>Daily Amount
                                                                                </th>
                                                                                <th>Month Days
                                                                                </th>
                                                                                <th>
                                                                                    Total No of days Worked in office
                                                                                </th>
                                                                                <th>
                                                                                    Admissible Weekly Off                                                                                  
                                                                                </th>
                                                                                <th>
                                                                                    Payable Holidays
                                                                                </th>
                                                                                <th>
                                                                                    Total No of Days
                                                                                </th>
                                                                                 <th>
                                                                                  Total Payable Days
                                                                                </th>
                                                                                <th>New Basic
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
                                                                <%--  <tr class="item">
                                                                    <td class="form_left_label" colspan="7" style="background-color: #FFFFAA">
                                                                        Scale :
                                                                        <%#Eval("scale")%>
                                                                    </td>
                                                                </tr>--%>
                                                                <%--<tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">--%>
                                                                <tr class="item">
                                                                    <td>
                                                                        <asp:CheckBox ID="chkID" runat="server" AlternateText="Check to allocate Payhead"
                                                                            ToolTip='<%# Eval("IDNO")%>' />
                                                                    </td>
                                                                    <td>

                                                                        <%#Eval("IDNO")%>
                                                                    </td>
                                                                    <td>
                                                                           <%#Eval("EMPNAME")%>
                                                                         <%-- <asp:TextBox ID="txtempname" runat="server" MaxLength="6" Text='<%#Eval("EMPNAME")%>'
                                                                            ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" onkeyup="return Increment(this);" Enabled="false" />
                                                             --%>      

                                                                    </td>
                                                                    <%--<td><%#Eval("SUBDEPT") %></td>--%>
                                                                    <td>
                                                                           <%#Eval("SUBDESIG")%>
                                                                         <%-- <asp:TextBox ID="txtdesg" runat="server" MaxLength="6" Text='<%#Eval("SUBDESIG")%>'
                                                                            ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" onkeyup="return Increment(this);" Enabled="false"  />
                                             --%>                       </td>
                                                                    <td>

                                                                        <asp:TextBox ID="txtfixamt" runat="server" MaxLength="6" Text='<%#Eval("FIX_AMT")%>'
                                                                            ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" onkeyup="return Increment(this);"   />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtMonth" runat="server" MaxLength="3" Text='<%#Eval("MONTH")%>'
                                                                            CssClass="form-control"   Enabled="false" /> 
                                                           <%--             onkeyup="return IncrementMonth(this);"--%>
                                                                    </td>
                                                                   <td>                            
                                                                         <asp:TextBox ID="txtpayabledays" runat="server" MaxLength="6" Text='<%#Eval("Payable_Days")%>'
                                                                             CssClass="form-control" ToolTip='<%#Eval("IDNO")%>'  
                                                                               onchange="return Increment(this);" 
                                                                             onkeydown="return (event.keyCode!=13);"   Enabled="true" />
                                                                         <%--onchange="validatedays(this);"--%>
                                                                    </td>
                                                                    <td>
                                                                    <asp:TextBox ID="txtadmissibledays" runat="server" MaxLength="6" Text='<%#Eval("Admissible_Weekly_Off")%>'
                                                                     ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" onchange="return IncrementAdmissible(this);" onkeydown="return (event.keyCode!=13);" />
                                                             
                                                                    </td>
                                                                    <td>
                                                                   <asp:TextBox ID="txtpayableholidays" runat="server" MaxLength="6" Text='<%#Eval("Payable_Holiday")%>'
                                                                  ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" onchange="return IncrementPaybleHolidays(this);" onkeydown="return (event.keyCode!=13);" />
                                                             
                                                                    </td>
                                                                    <td>
                                                                  <asp:TextBox ID="txttotalnodays" runat="server" MaxLength="6" Text='<%#Eval("Total_Days")%>'
                                                                  ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" onkeydown="return (event.keyCode!=13);" />
                                                             
                                                                    </td>
                                                                     <td>                            
                                                                         <asp:TextBox ID="txttotalpayabledays" runat="server" MaxLength="6" Text='<%#Eval("Total_Payable_Days")%>'
                                                                             CssClass="form-control" ToolTip='<%#Eval("IDNO")%>'   onkeydown="return (event.keyCode!=13);"   />
                                                                         <%--onchange="validatedays(this);"--%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtBasic" runat="server" Text='<%#Eval("BASIC")%>'
                                                                            ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" Enabled="false" />
                                                                        <asp:HiddenField ID="hdnattendencelock" runat="server" Value='<%#Eval("Attedence_Lock")%>'  />
                                                                        <asp:Label ID="lblattendencelock" runat="server" Text='<%#Eval("Attedence_Lock")%>'   Visible="false"></asp:Label>

                                                                    </td>
                                                                    <%--   <td>
                                                                          <asp:TextBox ID="txtNewBasic" MaxLength="6" runat="server" Text='<%#Eval("BASIC")%>'
                                                ToolTip='<%#Eval("IDNO")%>' Width="70px" Enabled="false" />
                                           
                                                                    </td>--%>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <div class="form-group row">
                                                            <div class="col-md-12">
                                                                <p class="text-center">
                                                                    <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-primary"  OnClientClick="return confirmation();" OnClick="btnSave_Click"  />
                                                                    <asp:Button ID="btnunlock" runat="server" Text="UnLock" CssClass="btn btn-primary"  OnClientClick="return confirmationUnlock();"  OnClick="btnunlock_Click" Visible="false" />
                                                                    <asp:Button ID="btnreport" runat="server" Text="Report" CssClass="btn btn-primary"    OnClick="btnreport_Click" />
                                                                     <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-warning"
                                                                        OnClick="btnCancel_Click" />
                                                                       <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                               </asp:Panel>
                           </div>
                      </div>
                  </div>
              </div>
         </div
     </ContentTemplate>
     <Triggers>
          <asp:PostBackTrigger ControlID="btnreport" />
     </Triggers>
   </asp:UpdatePanel>
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

    <script type="text/javascript">
        function validatedays(vall) {
            debugger
            var st = vall.id.split("lvIncrement_ctrl");
            //  var i = st[1].split("_txtMonth");
            var i = st[1].split("_txtpayabledays");
            var index = i[0];
            var totaldaysinmonth = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + '_txtMonth').value;
            var txtmonth = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + '_txtpayabledays').value; // Updated on 04-03-2022
            if (txtmonth > totaldaysinmonth) {
                alert("Total Payable Days should not greater than Total days in Month !");
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + '_txtpayabledays').value = ""; // Updated on 04-03-2022
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + '_txtBasic').value = "";
                return false;
            }
            return true;

        }

        // NEW CODE
        function Increment(vall) {
            debugger
            validateNumeric(vall);
            var st = vall.id.split("lvIncrement_ctrl");
            var i = st[1].split("txtpayabledays");
            var index = i[0];
            var fixamt = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtfixamt').value;
            var txtmonth = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtMonth').value; // Updated on 04-03-2022
            var Payabledays = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtpayabledays').value; // Updated on 04-03-2022
            var AdmmisibleweeklyOff = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtadmissibledays').value; // Updated on 04-03-2022
            var PayableHoliday = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtpayableholidays').value; // Updated on 04-03-2022
            var DaysInMonths = parseFloat(txtmonth);

            var txtTotaldays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
            var TotalPayableDays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
            var taxbasic = (parseFloat(fixamt) * parseFloat(txtTotaldays));
            var DaysInMonths = parseFloat(txtmonth);


            document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtBasic').value = taxbasic;
            document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalnodays').value = txtTotaldays;
            document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalpayabledays').value = txtTotaldays;

            var TotalDay = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalnodays').value;
            var TotalPayDays = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalpayabledays').value;

            if (TotalDay > DaysInMonths && TotalPayDays > DaysInMonths) {
                alert("Total No of Day and Total Payable Days should not greater than Total days in Month !");
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtpayabledays').value = "0";


                var fixamt = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtfixamt').value;
                var txtmonth = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtMonth').value; // Updated on 04-03-2022
                var Payabledays = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtpayabledays').value; // Updated on 04-03-2022
                var AdmmisibleweeklyOff = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtadmissibledays').value; // Updated on 04-03-2022
                var PayableHoliday = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtpayableholidays').value; // Updated on 04-03-2022
                var txtTotaldays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
                var TotalPayableDays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
                var taxbasic = (parseFloat(fixamt) * parseFloat(txtTotaldays));
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtBasic').value = taxbasic.toFixed(2);
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalnodays').value = txtTotaldays;
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalpayabledays').value = txtTotaldays;
                return false;
            }
            else {

                var txtTotaldays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
                var TotalPayableDays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
                var taxbasic = (parseFloat(fixamt) * parseFloat(txtTotaldays));
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtBasic').value = taxbasic.toFixed(2);
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalnodays').value = txtTotaldays;
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalpayabledays').value = txtTotaldays;
                return;
            }

        }
        // New Code for Admissible Working Days
        function IncrementAdmissible(vall) {
            debugger
            validateNumeric(vall);
            var st = vall.id.split("lvIncrement_ctrl");
            var i = st[1].split("txtadmissibledays");
            var index = i[0];
            var fixamt = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtfixamt').value;
            var txtmonth = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtMonth').value; // Updated on 04-03-2022
            var Payabledays = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtpayabledays').value; // Updated on 04-03-2022
            var AdmmisibleweeklyOff = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtadmissibledays').value; // Updated on 04-03-2022
            var PayableHoliday = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtpayableholidays').value; // Updated on 04-03-2022


            var txtTotaldays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
            var TotalPayableDays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
            var taxbasic = (parseFloat(fixamt) * parseFloat(txtTotaldays));
            var DaysInMonths = parseFloat(txtmonth);


            document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtBasic').value = taxbasic.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalnodays').value = txtTotaldays.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalpayabledays').value = txtTotaldays.toFixed(2);

            var TotalDay = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalnodays').value;
            var TotalPayDays = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalpayabledays').value;

            if (TotalDay > DaysInMonths && TotalPayDays > DaysInMonths) {
                alert("Total No of Day and Total Payable Days should not greater than Total days in Month !");
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtadmissibledays').value = "0";

                var fixamt = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtfixamt').value;
                var txtmonth = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtMonth').value; // Updated on 04-03-2022
                var Payabledays = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtpayabledays').value; // Updated on 04-03-2022
                var AdmmisibleweeklyOff = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtadmissibledays').value; // Updated on 04-03-2022
                var PayableHoliday = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtpayableholidays').value; // Updat

                var txtTotaldays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
                var TotalPayableDays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
                var taxbasic = (parseFloat(fixamt) * parseFloat(txtTotaldays));
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtBasic').value = taxbasic.toFixed(2);
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalnodays').value = txtTotaldays.toFixed(2);
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalpayabledays').value = txtTotaldays.toFixed(2);

                return false;
            }
            else {

                var txtTotaldays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
                var TotalPayableDays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
                var taxbasic = (parseFloat(fixamt) * parseFloat(txtTotaldays));

                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtBasic').value = taxbasic.toFixed(2);
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalnodays').value = txtTotaldays.toFixed(2);
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalpayabledays').value = txtTotaldays.toFixed(2);

                return;
            }
        }
        //New Code for Payable Holidays 
        function IncrementPaybleHolidays(vall) {
            debugger
            validateNumeric(vall);
            var st = vall.id.split("lvIncrement_ctrl");
            var i = st[1].split("txtpayableholidays");
            var index = i[0];
            var fixamt = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtfixamt').value;
            var txtmonth = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtMonth').value; // Updated on 04-03-2022
            var Payabledays = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtpayabledays').value; // Updated on 04-03-2022
            var AdmmisibleweeklyOff = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtadmissibledays').value; // Updated on 04-03-2022
            var PayableHoliday = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtpayableholidays').value; // Updated on 04-03-2022

            var txtTotaldays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
            var TotalPayableDays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
            var taxbasic = (parseFloat(fixamt) * parseFloat(txtTotaldays));
            var DaysInMonths = parseFloat(txtmonth);

            document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtBasic').value = taxbasic.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalnodays').value = txtTotaldays.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalpayabledays').value = txtTotaldays.toFixed(2);

            var txtotalDay = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalnodays').value;
            var txtotalPayDays = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalpayabledays').value;
            var TotalDay = parseFloat(txtotalDay);
            var TotalPayableDay = parseFloat(txtotalPayDays);

            if (TotalDay > DaysInMonths && TotalPayableDay > DaysInMonths) {

                alert("Total No of Day and Total Payable Days should not greater than Total days in Month !");
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtpayableholidays').value = "0";

                var fixamt = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtfixamt').value;
                var txtmonth = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtMonth').value; // Updated on 04-03-2022
                var Payabledays = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtpayabledays').value; // Updated on 04-03-2022
                var AdmmisibleweeklyOff = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtadmissibledays').value; // Updated on 04-03-2022
                var PayableHoliday = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtpayableholidays').value; // Updat

                var txtTotaldays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
                var TotalPayableDays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
                var taxbasic = (parseFloat(fixamt) * parseFloat(txtTotaldays));
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtBasic').value = taxbasic.toFixed(2);
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalnodays').value = txtTotaldays.toFixed(2);
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalpayabledays').value = txtTotaldays.toFixed(2);


                return false;
            }
            else {
                var txtTotaldays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
                var TotalPayableDays = parseFloat(Payabledays) + parseFloat(AdmmisibleweeklyOff) + parseFloat(PayableHoliday);
                var taxbasic = (parseFloat(fixamt) * parseFloat(txtTotaldays));

                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txtBasic').value = taxbasic.toFixed(2);
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalnodays').value = txtTotaldays.toFixed(2);
                document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + 'txttotalpayabledays').value = txtTotaldays.toFixed(2);
                return;
            }
        }
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }

    </script>
         <div id="divMsg" runat="server"></div>
</asp:Content>

