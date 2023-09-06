<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SeatingPlanNew.aspx.cs" Inherits="ACADEMIC_SEATINGARRANGEMENT_SeatingPlanNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function CheckAll(txtmain, headid) {
            AsignValue(txtmain, headid);
        }
        function chkIndividualRoom(txtchild, headid) {
            AsignValue(txtchild, headid);
        }
        function AsignValue(txt, headid) {
            try {
                var txtSelectedRoomCap = document.getElementById('<%= txtSelctedRommCap.ClientID %>');
                var hdfSelectedRommCap = document.getElementById('<%= hfroomcapacity.ClientID %>');
                var rmcap = 0;

                tbl = document.getElementById('tblroomcapacity');
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        debugger;
                        var chkid = 'ctl00_ContentPlaceHolder1_lvRoomDetails_ctrl' + i + '_chckroom';
                        var hfActualCapacity = 'ctl00_ContentPlaceHolder1_lvRoomDetails_ctrl' + i + '_hfActualCapcity';

                        if (headid == 1) {

                            if (txt.checked) {
                                document.getElementById(chkid).checked = true;
                                rmcap += Number(document.getElementById(hfActualCapacity).value)
                            }
                            else {
                                document.getElementById(chkid).checked = false;
                            }
                        }
                        else if (headid == 2) {

                            if (document.getElementById(chkid).checked == true) {
                                rmcap += Number(document.getElementById(hfActualCapacity).value)
                            }
                        }
                    }
                    txtSelectedRoomCap.value = rmcap;
                    hdfSelectedRommCap.value = rmcap;
                }
            }
            catch (e) {
                alert(e);
            }
        }
        function CheckIfSequenceEntered() {
            try {
                var seqvalues = "";
                var tbl = document.getElementById('tblroomcapacity');
                var dataRows = tbl.getElementsByTagName('tr');
                var lv = 'lvRoomDetails';
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        debugger;
                        var chkroom = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + i + '_chckroom';
                        var txtRoomId = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + i + '_txtRoomSrNo';
                        if (document.getElementById(chkroom).checked == true) {
                            seqvalues = document.getElementById(txtRoomId).value;
                        }
                        else {
                            seqvalues = "";
                        }
                    }
                }
                if (seqvalues.trim().length > 0)
                    return true;
                else
                    return false;
            }
            catch (e) {
                alert(e);
            }
        }
        //Ebnable text box sequence
        function EnableTextBox(txt) {
            try {
                var tbl = document.getElementById('tblroomcapacity');
                var dataRows = tbl.getElementsByTagName('tr');
                var lv = 'lvRoomDetails';
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        debugger;
                        var chkroom = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + i + '_chckroom';
                        var txtRoomId = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + i + '_txtRoomSrNo';
                        if (document.getElementById(chkroom).checked == true) {
                            document.getElementById(txtRoomId).disabled = false;
                        }
                        else {
                            document.getElementById(txtRoomId).disabled = true;
                        }
                    }
                }
            }
            catch (e) {
                alert(e);
            }
        }
        //Check Duplicate Sequence
        function IsNumberExist(txt, headid) {
            try {
                if (headid == 1) {
                    var tbl = document.getElementById('tblExamCourses');
                    var dataRows = tbl.getElementsByTagName('tr');
                    var lv = 'lvExamCoursesOnDate';
                    var txtboxid = 'txtSrNo';
                }
                else if (headid == 2) {
                    var tbl = document.getElementById('tblroomcapacity');
                    var dataRows = tbl.getElementsByTagName('tr');
                    var lv = 'lvRoomDetails';
                    var txtboxid = 'txtRoomSrNo';
                }
                var SeqExist = "";
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        debugger;
                        var SrNo = 'ctl00_ContentPlaceHolder1_' + lv + '_ctrl' + i + '_' + txtboxid;
                        var SrNoValue = document.getElementById(SrNo).value;
                        if (SeqExist == SrNoValue && SeqExist != "") {
                            alert('Sequence ' + SeqExist + ' already exist.')
                            document.getElementById(SrNo).value = '';
                            break;
                        }
                        else {
                            SeqExist = SrNoValue;
                        }
                    }
                }
                SeqExist = "";
            }
            catch (e) {
                alert(e);
            }
        }
        //validate form
        function validateForm() {
            debugger;
            var hdStudCount = document.getElementById('<%= hdStudCount.ClientID %>');
            var RoomStrength = document.getElementById('<%=hfroomcapacity.ClientID %>')
            //if (Number(hdStudCount.value) >= Number(RoomStrength.value)) {
            //    alert('Students Strength is exceeding the room capacity.')
            //    return false;
            //}
            //else {
            counter = 0;
            var StudOnBench = document.getElementsByName("ctl00$ContentPlaceHolder1$rbOnBench")
            var x = document.getElementsByName("ctl00$ContentPlaceHolder1$rbOnBench").length;
            if (x > 0) {
                for (i = 0; i < x ; i++) {
                    var SeatingCount = 'ctl00_ContentPlaceHolder1_rbOnBench_' + i;
                    var ChkIfSelected = document.getElementById(SeatingCount);
                    if (ChkIfSelected.checked) {
                        counter++;
                    }
                }
            }
            if (counter == 0) {
                alert('Please select number of students seats on bench.');
                return false;
            }
            else {
                if (IfSelected) {
                    return true;
                }
                else {
                    alert('Please Select Seating Arrangement Type.');
                }
            }
            //}
        };
        //check if Seating Arrangement Type Selected
        var IfSelected = function CheckSeatingArrType() {
            counter = 0;
            var ArrType = document.getElementsByName("ctl00$ContentPlaceHolder1$rbSeatingType")
            var x = document.getElementsByName("ctl00$ContentPlaceHolder1$rbSeatingType").length;
            if (x > 0) {
                for (i = 0; i < x ; i++) {
                    var SeatingCount = 'ctl00_ContentPlaceHolder1_rbSeatingType_' + i;
                    var ChkIfSelected = document.getElementById(SeatingCount);
                    if (ChkIfSelected.checked) {
                        counter++;
                    }
                }
            }
            if (counter == 0) {
                return false;
            } else {
                return true;
            }
        }
    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updplRoom"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">SEATING ARRANGEMENT </h3>
                </div>
                <div class="box-body">
                    <asp:UpdatePanel ID="updplRoom" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="True" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/College name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true"
                                            TabIndex="2" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College/School" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College/School" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtExamDate" runat="server" TabIndex="3" ValidationGroup="submit" OnTextChanged="txtExamDate_TextChanged" AutoPostBack="true" />
                                            <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtExamDate" PopupButtonID="imgExamDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtExamDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Exam Date"
                                                ControlExtender="meExamDate" ControlToValidate="txtExamDate" IsValidEmpty="false"
                                                InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Exam Date"
                                                InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamName" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            TabIndex="7" OnSelectedIndexChanged="ddlExamName_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Mid Semester</asp:ListItem>
                                            <asp:ListItem Value="2">End Semester</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExamName"
                                            ValidationGroup="Report" Display="None" ErrorMessage="Please Select Exam Name"
                                            SetFocusOnError="true" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="rfvExa" runat="server" ControlToValidate="ddlExamName"
                                            ValidationGroup="show" Display="None" ErrorMessage="Please Select Exam Name"
                                            SetFocusOnError="true" InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Slot</label>
                                        </div>
                                        <asp:DropDownList ID="ddlslot" runat="server" TabIndex="1" data-select2-enable="true" AppendDataBoundItems="True"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlslot_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlslot"
                                            Display="None" ErrorMessage="Please Select Slot" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Students On Bench</label>
                                        </div>
                                        <asp:RadioButtonList ID="rbOnBench" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbOnBench_SelectedIndexChanged" CellPadding="12">
                                            <%-- <asp:ListItem Value="1">Single</asp:ListItem>--%>
                                            <asp:ListItem Value="2">Dual</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Arrangement Type</label>
                                        </div>
                                        <asp:RadioButtonList ID="rbSeatingType" runat="server" RepeatDirection="Horizontal" CellPadding="12">
                                            <%-- <asp:ListItem Value="1">Continue Seating</asp:ListItem>
                                            <asp:ListItem Value="2">Alternate Seating</asp:ListItem>--%>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Total Registered Student</label>
                                        </div>
                                        <asp:Label ID="lbltotcount" runat="server" Text=""></asp:Label><br />
                                        <asp:HiddenField ID="hdStudCount" runat="server" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                                 <label>Block Capacity <%--(Except Disabled benches)--%> </label>
                                        </div>
                                        <asp:Label ID="lblroomcapacity" runat="server" Text=""></asp:Label><br />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Selected Room Capcity</label>
                                        </div>
                                        <asp:TextBox ID="txtSelctedRommCap" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="hfroomcapacity" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlExamCourse" runat="server" Visible="false">
                                    <div class="sub-heading">
                                        <h5>Exam Course List on Date</h5>
                                    </div>
                                    <asp:ListView ID="lvExamCoursesOnDate" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblExamCourses">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Branch
                                                        </th>
                                                        <th>Course
                                                        </th>
                                                        <th>Seating Sequence
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td>
                                                    <asp:Label ID="lblbranch" runat="server" Text='<%# Eval("CODE")%>' ToolTip='<%# Eval("BRANCHNO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblExamCourse" runat="server" Text='<%# Eval("COURSES")%>' />
                                                    <asp:Label ID="lblCcode" Visible="false" runat="server" Text='<%# Eval("CCODE")%>' ToolTip='<%# Eval("DEGREENO")%>' />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSrNo" runat="server" Onblur="IsNumberExist(this,1);" MaxLength="2"></asp:TextBox>

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FTEtxtColumns" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtSrNo">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlRoomDetails" runat="server" Visible="false">
                                   
                                     <div class="sub-heading">
                                                    <h5>Block Details</h5>
                                                </div>
                                            <asp:ListView ID="lvRoomDetails" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblroomcapacity">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Select Room
                                                                    <asp:CheckBox ID="chckallroom" runat="server" Checked="false" onclick="CheckAll(this,1),EnableTextBox(this);" />
                                                                </th>
                                                                <th>Block Name
                                                                </th>
                                                                <th>Block Capacity
                                                                </th>
                                                                <th>Actual Capcity
                                                                </th>
                                                                <th>Not in Use Seats
                                                                </th>
                                                                <th>Room Sequence
                                                                </th>
                                                                </tr>  
                                                             </thead>
                                                              <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                              </tbody>
                                                    </table>

                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td class="text-center">
                                                            <asp:CheckBox ID="chckroom" runat="server" onclick="chkIndividualRoom(this,2),EnableTextBox(this);" />
                                                            <asp:HiddenField ID="hfActualCapcity" runat="server" Value='<%# Eval("ACTUALCAPACITY") %>' />
                                                            <asp:HiddenField ID="hfRoom" runat="server" Value='<%# Eval("ROOMNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRoomname" runat="server" Text='<%# Eval("ROOMNAME")%>' />

                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblBlockCapcity" runat="server" Text='<%# Eval("ROOMCAPACITY")%>' />

                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRoomCapcity" runat="server" Text='<%# Eval("ACTUALCAPACITY")%>' />

                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSeatNotUse" runat="server" Text='<%# Eval("DISABLED_IDS")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRoomSrNo" runat="server" Enabled="false" Onblur="IsNumberExist(this,2);" MaxLength="2"></asp:TextBox>

                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FTEtxtColumns" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtRoomSrNo">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                     
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnConfigure" runat="server" Text="Configure" ValidationGroup="configure"
                                    TabIndex="8" CssClass="btn btn-primary" OnClick="btnConfigure_Click" OnClientClick="return validateForm(this);" />
                                <asp:Button ID="btnMasterSeating" runat="server" CssClass="btn btn-primary" Text="Master Seating Plan" ValidationGroup="process"
                                    TabIndex="15" Visible="false" />
                                <asp:Button ID="btnStastical" runat="server" Text="Statistical Report" ValidationGroup="process"
                                    TabIndex="15" Visible="false" CssClass="btn btn-info" />
                                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-warning"
                                    Text="Cancel" OnClick="btnClear_OnClick" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="configure" />
                                <asp:ValidationSummary ID="vsRoomNm" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />

                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnldetails" runat="server" Visible="false">
                                    <asp:ListView ID="lvdetails" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>STUDENTS LIST </h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Sr No. </th>
                                                        <th>Enrollment No </th>
                                                        <th>Student Name </th>
                                                        <th>Branch </th>
                                                        <th>Bench No </th>
                                                        <th>Block Name </th>
                                                        <th>Semester </th>
                                                        <th>Course Code </th>
                                                        <th>Exam Slot </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>

                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <td><%#Container.DataItemIndex+1 %></td>
                                            <td><%# Eval("REGNO")%></td>
                                            <td><%# Eval("STUDNAME")%></td>
                                            <td><%# Eval("LONGNAME")%></td>
                                            <td><%# Eval("SEATNO")%></td>
                                            <td><%# Eval("ROOMNAME")%></td>
                                            <td><%# Eval("SEMESTERNO")%></td>
                                            <td><%# Eval("CCODE")%></td>
                                            <td><%# Eval("SLOTNAME")%></td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="ddlRoom" />
        </Triggers>--%>
                        <Triggers>
                            <%-- <asp:PostBackTrigger ControlID="btnConfigure" />
            <asp:PostBackTrigger ControlID="btnStastical" />
            <asp:PostBackTrigger ControlID="btnMasterSeating" />
           <asp:PostBackTrigger ControlID="btnClear" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

