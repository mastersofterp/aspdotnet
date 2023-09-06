<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentRegistrationNPF.aspx.cs" Inherits="ACADEMIC_StudentRegistration_Jecrc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../plugins/jQuery/jquery_ui_min/jquery-ui.min.css" rel="stylesheet" />
    <script src="../plugins/jQuery/jquery_ui_min/jquery-ui.min.js"></script>
    <script type="text/javascript">
        if (document.layers) {
            //Capture the MouseDown event.
            document.captureEvents(Event.MOUSEDOWN);

            //Disable the OnMouseDown event handler.
            document.onmousedown = function () {
                return false;
            };
        }
        else {
            //Disable the OnMouseUp event handler .
            document.onmouseup = function (e) {
                if (e != null && e.type == "mouseup") {
                    //Check the Mouse Button which is clicked.
                    if (e.which == 2 || e.which == 3) {
                        //If the Button is middle or right then disable.
                        return false;
                    }
                }
            };
        }

        //Disable the Context Menu event.
        document.oncontextmenu = function () {
            return false;
        };
       
    </script>
    <%--<style>
        .box.box-primary {
    padding: 5px;
    border-top: none;
    border-radius: 10px;
    box-shadow: rgb(0 0 0 / 20%) 0px 5px 10px;
    /* margin-top: 30px; */
    margin-top: -55px;
}
    </style>--%>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updEdit"
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
    <div id="myModal2" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Search</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updEdit" runat="server">
                        <ContentTemplate>
                            <div class="form-group col-md-12">
                                <label>Search Criteria</label>
                                <br />
                                <asp:RadioButton ID="rbName" runat="server" Text="Name" GroupName="edit" Checked="true" />

                                <%--<asp:RadioButton ID="rbIdNo" runat="server" Text="IdNo" GroupName="edit" />
                                <asp:RadioButton ID="rbBranch" runat="server" Text="Branch" GroupName="edit" />
                                <asp:RadioButton ID="rbSemester" runat="server" Text="Semester" GroupName="edit" />
                                <asp:RadioButton ID="rbEnrollmentNo" runat="server" Text="Enroll NO" GroupName="edit" Checked="True"/> 
                                <asp:RadioButton ID="rbRegNo" runat="server" Text="Rollno" GroupName="edit" Checked="True" />--%>
                                <%--<asp:RadioButton ID="rbprospectno" runat="server" Text="PROSPECTUSNO" GroupName="edit" />--%>
                                <asp:RadioButton ID="rbprospectno" runat="server" Text="Prospectus No" GroupName="edit" />
                                 <asp:RadioButton ID="rbTempID" runat="server" Text="Temp IDNO" GroupName="edit" />
                                <asp:RadioButton ID="rbAdmonline" runat="server" Text="USERNAME" GroupName="edit" Visible="false" />
                            </div>
                            <div class="form-group col-md-12">
                                <div class="col-md-6">
                                    <label>Search String</label>
                                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <p class="text-center">
                                    <asp:Button ID="btnsearch1" runat="server" Text="Search" OnClientClick="return submitPopup(this.name);" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnCancelModal" runat="server" Text="Cancel" OnClientClick="return ClearSearchBox(this.name)" CssClass="btn btn-warning" />
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </p>
                                <div>
                                    <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                </div>

                                <div class="col-md-12">
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvStudent" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h4>Student Details</h4>
                                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="300px">
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Name
                                                                    </th>
                                                                    <th>Application ID
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                    </asp:Panel>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("NAME") %>' CommandArgument='<%# Eval("APPLICATIONID") %>'
                                                            OnClick="lnkId_Click" OnClientClick="return Dismiss_Modal(this);"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <%# Eval("APPLICATIONID")%>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>


                                        <asp:ListView ID="lvstudentprospectusno" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h4>Student Details</h4>
                                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="300px">
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Name
                                                                    </th>
                                                                    <th>Prospectus No
                                                                    </th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                    </asp:Panel>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkIdpros" runat="server" Text='<%# Eval("STUDENT_NAME") %>' CommandArgument='<%# Eval("PROSNO") %>'
                                                            OnClientClick="return Dismiss_Modal(this);" OnClick="lnkIdpros_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PROSPECTUSNO")%>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>



                                         <asp:ListView ID="LVtempid" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h4>Student Details</h4>
                                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="300px">
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Name
                                                                    </th>
                                                                    <th>Temp IDNO
                                                                    </th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                    </asp:Panel>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkTempID" runat="server" Text='<%# Eval("STUDNAME") %>' CommandArgument='<%# Eval("IDNO") %>'
                                                            OnClientClick="return Dismiss_Modal(this);" OnClick="lnkTempID_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <%# Eval("IDNO")%>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>


                                    </asp:Panel>
                                </div>

                                <div class="col-md-12">
                                    <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="ListView1" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h4>Student Details</h4>
                                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="300px">
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Name
                                                                    </th>
                                                                    <th>Application ID
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                    </asp:Panel>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("NAME") %>' CommandArgument='<%# Eval("APPLICATIONID") %>'
                                                            OnClick="lnkId_Click" OnClientClick="return Dismiss_Modal(this);"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <%# Eval("APPLICATIONID")%>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>


                                        <asp:ListView ID="lvstudinfoOA" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h4>Student Details</h4>
                                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="300px">
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>FIRSTNAME
                                                                    </th>
                                                                    <th>MOBILENO No
                                                                    </th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                    </asp:Panel>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkIdamonline" runat="server" Text='<%# Eval("FIRSTNAME") %>' CommandArgument='<%# Eval("USERNO") %>'
                                                            OnClientClick="return Dismiss_Modal(this);" OnClick="lnkIdamonline_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MOBILENO")%>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                </div>
            </div>

        </div>
    </div>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="dvStudent" runat="server">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div id="div12" runat="server"></div>
                            <div class="box-header with-border">
                                <%--<h3 class="box-title">NEW STUDENT ENTRY</h3>--%>
                                <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            </div>

                            <asp:Panel ID="Panel3" runat="server" Visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search</label>
                                        </div>
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TabIndex="1" Enabled="False" />

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="box-body">
                                <div class="col-12" id="div14">
                                    <div class="row" style="margin-left: 40%">
                                        <%--<div class="form-group col-lg-4 col-md-6 col-12" id="Div15" runat="server" visible="true">--%>
                                        <%--<div class="input-group" >--%>
                                        <%--<div style="text-align:center">--%>
                                        <asp:Button ID="btnSearchStu" runat="server" Text="Search Student" OnClick="btnSearchStu_Click" ToolTip="Search Student Information" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnNewStu" runat="server" Text="New Student" ToolTip="New Student Registration" OnClick="btnNewStu_Click" CssClass="btn btn-primary" />
                                        <%--</div>--%>
                                        <%--</div>--%>
                                        <%--</div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function submitPopup(btnsearch1) {

            var rbText;
            var searchtxt;
            if (document.getElementById('<%=rbName.ClientID %>').checked == true)
                rbText = "Name";
            else if (document.getElementById('<%=rbprospectno.ClientID %>').checked == true)
                rbText = "PROSPECTUSNO";
            else if (document.getElementById('<%=rbTempID.ClientID %>').checked == true)
                rbText = "IDNO";
            else if (document.getElementById('<%=rbAdmonline.ClientID %>').checked == true)
                rbText = "USERNAME";
        searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;

            __doPostBack(btnsearch1, rbText + ',' + searchtxt);

            return true;
        }

        function ClearSearchBox(btncancelmodal) {
            document.getElementById('<%=txtSearch.ClientID %>').value = '';
            __doPostBack(btncancelmodal, '');
            return true;
        }
    </script>

    <%--  <script type="text/javascript">
    function RunThisAfterEachAsyncPostback()
    {
        $(function() { 
				 $("#<%=txtDateOfBirth.ClientID%>").datepicker({
					changeMonth: true,
					changeYear: true,
					dateFormat: 'dd/mm/yy',
					yearRange: '1975:' + getCurrentYear()
				});
			});
			
			$(function() {
				 $("#<%=txtDateOfAdmission.ClientID%>").datepicker({
					changeMonth: true,
					changeYear: true,
					dateFormat: 'dd/mm/yy',
					yearRange: '1975:' + getCurrentYear()
				});
			});
			
	   function getCurrentYear()
       {
        var cDate = new Date(); 
        return  cDate.getFullYear();  
       }
        Autocomplete();
    }
    </script>--%>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>


    <script type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>

    <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>
            <div id="dvMain" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div id="div2" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">New Student</h3>
                                
                            </div>

                            <asp:Panel ID="pnlId" runat="server" Visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search</label>
                                        </div>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtIDNo" runat="server" CssClass="form-control" TabIndex="1" Enabled="False" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="box-body">
                                <div class="col-12" id="divGeneralInfo" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12" id="tempid" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Application ID</label>
                                            </div>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtREGNo" runat="server" TabIndex="1" class="watermarked" CssClass="form-control" Width="50%" />
                                                <span class="input-group-addon" style="height: 50%"><a href="#" title="Search Student for Modification" data-toggle="modal" data-target="#myModal2">
                                                    <asp:Image ID="imgSearch" runat="server" ImageUrl="~/Images/search-svg.png" TabIndex="1"
                                                        AlternateText="Search Student by  Name, Prospectus No" Style="padding-left: -500px" ToolTip="Search Student by Name , Prospectus No" /></a>
                                                    <%--  Enable the button so it can be played again --%>
                                                </span>
                                                <ajaxToolKit:TextBoxWatermarkExtender ID="watREGNo" TargetControlID="txtREGNo" runat="server" WatermarkText="Enter Application Id">
                                                </ajaxToolKit:TextBoxWatermarkExtender>
                                                <asp:RequiredFieldValidator ID="rfvStudentName" runat="server" ControlToValidate="txtREGNo"
                                                    ErrorMessage="Please Enter Application ID" Display="None" ValidationGroup="Show"
                                                    SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                                <div class="form-group col-md-2">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click1"
                                                        CssClass="btn btn-info" ValidationGroup="Show" TabIndex="2" />
                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Show"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                                <div class="form-group col-md-2">
                                                    <asp:Button ID="btnNewStudentS" runat="server" Text="New Student" ToolTip="New Student Registration" OnClick="btnNewStudentS_Click"
                                                        CssClass="btn btn-primary" TabIndex="3" />
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>


                                <div class="col-12">
                                    <div class="row">

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divStudentfullName" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supStudentfullName" runat="server">* </sup>
                                                <label>Name of the Student <span style="color: red; font: 100; font-size: 11px;">(as per 10<sup>th</sup> marksheet)</span></label>
                                            </div>
                                            <asp:TextBox ID="txtStudentfullName" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Please Enter Student Full Name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" placeholder="Please Enter Student Full Name" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteStudent" runat="server" TargetControlID="txtStudentfullName" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                            <asp:RequiredFieldValidator ID="rfvStudent" runat="server" ControlToValidate="txtStudentfullName"
                                                Display="None" ErrorMessage="Please Enter Student Full Name" ValidationGroup="academic"
                                                SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                       <%-- < added by Rohit on 23_08_2022>--%>

                                         <div class="form-group col-lg-3 col-md-6 col-12" id="divFatherName" runat="server" visible="false"> 
                                            <div class="label-dynamic">
                                                 <sup id="supFatherName" runat="server">* </sup>
                                                <label>Father's Name</label>
                                            </div>
                                            <asp:TextBox ID="txtFatherName" runat="server" TabIndex="145" ToolTip="Please Enter Father's Name." onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" placeholder="Please Enter Father's Name." />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                TargetControlID="txtFatherName" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                              <asp:RequiredFieldValidator ID="rfvFatherName" runat="server" ControlToValidate="txtFatherName"
                                                        Display="None" ErrorMessage="Please Enter Father's Name" ValidationGroup="academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                        </div>


                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divStudMobile" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supStudMobile" runat="server">* </sup>
                                                <label>Student Mobile No.</label>
                                            </div>
                                            <asp:TextBox ID="txtStudMobile" runat="server" CssClass="form-control" TabIndex="4" MaxLength="10" onkeyup="validateNumeric(this);"
                                                placeholder="Please Enter Student Mobile No."></asp:TextBox>
                                            <%--Change by Rahul Moraskar 2022-07-26--%>
                                            <%--ID="RequiredFieldValidator7"--%>
                                            <asp:RequiredFieldValidator ID="rfvStudMobile" runat="server" ControlToValidate="txtStudMobile"
                                                Display="None" ErrorMessage="Please Enter Student Mobile No " ValidationGroup="academic"
                                                SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" Visible="false" ErrorMessage="Mobile No. is Invalid" ID="revMobile" ControlToValidate="txtStudMobile"
                                                ValidationExpression="^[\s\S]{8,}$" Display="Dynamic" ValidationGroup="academic"></asp:RegularExpressionValidator>

                                            <asp:RegularExpressionValidator Display="None" ControlToValidate="txtStudMobile" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{10}$" runat="server" ErrorMessage="Minimum 10 characters required For Mobile No.." ValidationGroup="academic"></asp:RegularExpressionValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtStudMobile" FilterType="Custom" FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9" />
                                            <%-- ValidationExpression=".{10}.*"--%>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divStudentMoblieNo2" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supStudentMoblieNo2" runat="server">* </sup>
                                                <label>Student Mobile No. 2</label>
                                            </div>
                                            <asp:TextBox ID="txtStudMobile2" runat="server" CssClass="form-control" TabIndex="5" MaxLength="10" onkeyup="validateNumeric(this);"
                                                placeholder="Please Enter Student Mobile No.2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvStudMobile2" runat="server" ControlToValidate="txtStudMobile2"
                                                Display="None" ErrorMessage="Please Enter Student Mobile No. 2 " ValidationGroup="academic"
                                                SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" Visible="false" ErrorMessage="Mobile No. 2 is Invalid" ID="RegularExpressionValidator1" ControlToValidate="txtStudMobile2"
                                                ValidationExpression="^[\s\S]{8,}$" Display="Dynamic" ValidationGroup="academic"></asp:RegularExpressionValidator>

                                            <asp:RegularExpressionValidator Display="None" ControlToValidate="txtStudMobile2" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{10,}$" runat="server" ErrorMessage="Minimum 10 characters required For Mobile No. 2" ValidationGroup="academic"></asp:RegularExpressionValidator>
                                            <%-- ValidationExpression=".{10}.*"--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtStudMobile2" FilterType="Custom" FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9" />
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divParentMobileNo" runat="server">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <sup id="supParentMobileNo" runat="server">*</sup>
                                                <label>Parent Mobile No.</label>
                                            </div>
                                            <asp:TextBox ID="txtParentmobno" runat="server" CssClass="form-control" TabIndex="5" MaxLength="10" onkeyup="validateNumeric(this);"
                                                placeholder="Please Enter Parent Mobile No."></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvparentmobno" runat="server" ControlToValidate="txtParentmobno"
                                                Display="None" ErrorMessage="Please Enter Parent Mobile No." ValidationGroup="academic"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator runat="server" Visible="false" ErrorMessage="Parent Mobile No. is Invalid" ID="RegularExpressionValidator4" ControlToValidate="txtParentmobno"
                                                ValidationExpression="^[\s\S]{8,}$" Display="Dynamic" ValidationGroup="academic"></asp:RegularExpressionValidator>

                                            <asp:RegularExpressionValidator Display="None" ControlToValidate="txtParentmobno" ID="RegularExpressionValidator5" ValidationExpression="^[\s\S]{10,}$" runat="server" ErrorMessage="Minimum 10 characters required For Parent Mobile No." ValidationGroup="academic"></asp:RegularExpressionValidator>
                                            <%-- ValidationExpression=".{10}.*"--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtParentmobno" FilterType="Custom" FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9" />
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPersonalEmail" runat="server">
                                            <div class="label-dynamic">
                                                <%--<sup>*</sup>--%>
                                                <sup id="supPersonalEmail" runat="server">*</sup>
                                                <label>Personal Email</label>
                                            </div>
                                            <asp:TextBox ID="txtStudEmail" runat="server" ToolTip="Please Enter Personal Email" CssClass="form-control"
                                                TabIndex="6" placeholder="Please Enter Student Personal Email." />
                                            <%--Change by Rahul Moraskar 2022-07-26--%>
                                            <%--ID="rfvStudEmail"--%>

                                            <asp:RegularExpressionValidator ID="revStudEmail" runat="server" ControlToValidate="txtStudEmail"
                                                Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorMessage="Please Enter Valid EmailID" ValidationGroup="academic"></asp:RegularExpressionValidator>

                                            <%--Change by Rahul Moraskar 2022-07-26--%>
                                            <%--ID="RequiredFieldValidator2"--%>

                                            <asp:RequiredFieldValidator ID="rfvStudEmail" runat="server" ControlToValidate="txtStudEmail"
                                                Display="None" ErrorMessage="Please Enter Student Personal Email " ValidationGroup="academic"
                                                SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divstate" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supstate" runat="server">* </sup>
                                                <label>State</label>
                                            </div>
                                            <asp:DropDownList ID="ddlstate" runat="server" TabIndex="7" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Please Select State">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <%--  <asp:ListItem Value="1" Selected="True">Karnataka</asp:ListItem>
                                            <asp:ListItem Value="2">Non-Karnataka</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvstate" runat="server" ControlToValidate="ddlstate"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select State"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divCity" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supCity" runat="server">* </sup>
                                                <label>City</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCity" runat="server" TabIndex="7" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Please Select City">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <%--  <asp:ListItem Value="1" Selected="True">Karnataka</asp:ListItem>
                                            <asp:ListItem Value="2">Non-Karnataka</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select City"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divGender" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supGender" runat="server">* </sup>
                                                <label>Select Gender</label>
                                            </div>
                                            <asp:RadioButton ID="rdoMale" runat="server" GroupName="Sex" TabIndex="8" Text="Male" />

                                            <asp:RadioButton ID="rdoFemale" runat="server" GroupName="Sex" TabIndex="9" Text="Female" />

                                            <asp:RadioButton ID="rdoTransGender" runat="server" GroupName="Sex" TabIndex="10" Text="Others" />

                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSchool" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supSchool" runat="server">* </sup>
                                                <%--<label>School Admitted</label>--%>
                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSchool" runat="server" ValidationGroup="academic" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Please Select School Admitted." TabIndex="11" AppendDataBoundItems="True" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSchool" runat="server" ControlToValidate="ddlSchool"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Institute School"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDegree" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supDegree" runat="server">* </sup>
                                                <%--<label>Degree / Program</label>--%>
                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" ValidationGroup="academic" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Please Select Degree / Program" TabIndex="12" AppendDataBoundItems="True" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Degree / Program"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBranch" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supBranch" runat="server">* </sup>
                                                <%--<label>Branch</label>--%>
                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="13" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="academic" ToolTip="Please Select Branch" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                                ErrorMessage="Please Select Branch" Display="None" ValidationGroup="academic"
                                                SetFocusOnError="true" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvDD_Branch" runat="server" ControlToValidate="ddlBranch"
                                                ErrorMessage="Please Select Branch" Display="None" ValidationGroup="ddinfo" SetFocusOnError="true"
                                                InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divSpecialisation" visible="false">
                                            <div class="label-dynamic">
                                                <sup id="supSpecialisation" runat="server">* </sup>
                                                <label>Specialisation</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSpecialisation" runat="server" TabIndex="13" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="academic" ToolTip="Please Select Specialisation">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSpecialisation" runat="server" ControlToValidate="ddlSpecialisation"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Specialisation"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>

                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divadmthrough" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supadmthrough" runat="server">*</sup>
                                                <label>Admission Through</label>
                                            </div>
                                            <asp:DropDownList ID="ddladmthrough" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Admission Type" TabIndex="14">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvadmthrough" runat="server" ControlToValidate="ddladmthrough"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Through"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divAdmType" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supAdmType" runat="server">* </sup>
                                                <label>Admission Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdmType" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnTextChanged="ddlAdmType_TextChanged"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Admission Type" TabIndex="15">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAdmission" runat="server" ControlToValidate="ddlAdmType"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Type"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divYear" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supYear" runat="server">* </sup>
                                                <label>Admission Year</label>
                                            </div>
                                            <asp:DropDownList ID="ddlYear" runat="server" TabIndex="16" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Admission Year" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Year"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSemester" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supSemester" runat="server">* </sup>
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="17" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Please Select Semester" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged" AutoPostBack="true" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Semester"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBatch" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supBatch" runat="server">* </sup>
                                                <label>Admission Batch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBatch" runat="server" TabIndex="18" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Admission Batch" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged" AutoPostBack="true" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlBatch"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Batch"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvddino_batch" runat="server" ControlToValidate="ddlBatch"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Batch"
                                                ValidationGroup="ddinfo"></asp:RequiredFieldValidator>
                                        </div>

                                        <%--Ask to Sir Start --%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Student First Name</label>
                                            </div>
                                            <asp:TextBox ID="txtStudentName" runat="server" TabIndex="16"
                                                ToolTip="Please Enter Student Name" ValidationGroup="academic" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />
                                            <%--<ajaxToolKit:TextBoxWatermarkExtender ID="tbWaterMarkStudName" TargetControlID="txtStudentName"
                                                    WatermarkText="Type Full Name Here" runat="server">--%>
                                            <%-- </ajaxToolKit:TextBoxWatermarkExtender>--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                TargetControlID="txtStudentName" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="1234567890" />
                                            <%-- <asp:RequiredFieldValidator ID="rfvStudentFirstName" runat="server" ControlToValidate="txtStudentName"
                                                Display="None" ErrorMessage="Please Enter Student First Name" ValidationGroup="academic"
                                                SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>

                                       

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Student Middle Name</label>
                                            </div>
                                            <asp:TextBox ID="txtStudentMiddleName" runat="server" TabIndex="122"
                                                ToolTip="Please Enter Student Middle Name" ValidationGroup="academic" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />
                                            <%--<ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID="txtStudentName"
                                                 WatermarkText="Type Full Name Here" runat="server">--%>
                                            <%-- </ajaxToolKit:TextBoxWatermarkExtender>--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                TargetControlID="txtStudentMiddleName" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="1234567890" />
                                            <%--<asp:RequiredFieldValidator ID="rfvStuMiddleName" runat="server" ControlToValidate="txtStudentMiddleName"
                                                Display="None" ErrorMessage="Please Enter Student MIddle Name" ValidationGroup="academic"
                                                SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Father Middle Name</label>
                                            </div>
                                            <asp:TextBox ID="txtFatherMiddleName" runat="server" TabIndex="3123" ToolTip="Please Enter Father Middle Name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                TargetControlID="txtFatherMiddleName" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="1234567890" />
                                            <%-- <asp:RequiredFieldValidator ID="rfvFatMiddleName" runat="server" ControlToValidate="txtFatherMiddleName"
                                                    Display="None" ErrorMessage="Please Enter Father MIddle Name" ValidationGroup="academic"
                                                    SetFocusOnError="true">--%>
                                            <%--    </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Student Last Name</label>
                                            </div>
                                            <asp:TextBox ID="txtStudentLastName" runat="server" TabIndex="123"
                                                ToolTip="Please Enter Student Last Name" ValidationGroup="academic" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />
                                            <%--<ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtStudentName"
                                                WatermarkText="Type Full Name Here" runat="server">--%>
                                            <%--   </ajaxToolKit:TextBoxWatermarkExtender>--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                TargetControlID="txtStudentLastName" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="1234567890" />
                                            <%-- <asp:RequiredFieldValidator ID="rfvStuLastName" runat="server" ControlToValidate="txtStudentLastName"
                                                Display="None" ErrorMessage="Please Enter Student Last Name" ValidationGroup="academic"
                                                SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Father Last Name</label>
                                            </div>
                                            <asp:TextBox ID="txtFatherLastName" runat="server" TabIndex="21323" ToolTip="Please Enter Father Last Name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                TargetControlID="txtFatherName" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="1234567890" />
                                            <%--<asp:RequiredFieldValidator ID="rfvFatherLastName" runat="server" ControlToValidate="txtFatherLastName"
                                                    Display="None" ErrorMessage="Please Enter Father Last Name" ValidationGroup="academic"
                                                    SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Mother's Name</label>
                                            </div>
                                            <asp:TextBox ID="txtMotherName" runat="server" TabIndex="3132" ToolTip="Please Enter Mother's name." onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                TargetControlID="txtMotherName" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="1234567890" />
                                            <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMotherName"
                                                    Display="None" ErrorMessage="Please Enter Mother's Name" ValidationGroup="academic"
                                                    SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDateofBirth" runat="server" visible="false"> <%--style="display: none"--%>
                                            <div class="label-dynamic">
                                                <sup id="supDateofBirth" runat="server">* </sup>
                                                <label>Date of Birth</label>
                                            </div>
                                            <div class="input-group">
                                                <div class="input-group-addon" id="txtDateOfBirth1">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtDateOfBirth" runat="server" TabIndex="19" ValidationGroup="academic" />
                                                <%-- <asp:Image ID="imgCalDateOfBirth" runat="server" src="../images/calendar.png" Width="16px" />--%>
                                                <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDateOfBirth" PopupButtonID="txtDateOfBirth1" Enabled="true"
                                                    EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
                                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                    MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server" EmptyValueMessage="Please Enter DateOfBirth"
                                                    ControlExtender="meeDateOfBirth" ControlToValidate="txtDateOfBirth" IsValidEmpty="true"
                                                    InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                    ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                    ValidationGroup="academic" SetFocusOnError="true" />
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDateOfBirth"
                                                    Display="None" ErrorMessage="Please Enter Date of Birth" ValidationGroup="academic"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Religion</label>
                                            </div>
                                            <asp:DropDownList ID="ddlReligion" runat="server" TabIndex="1325" AppendDataBoundItems="true"
                                                ToolTip="Please Select Religion" />
                                            <%--<asp:TextBox ID="txtReligion" runat="server" Width="90%" class="tbReligion" ToolTip="Please Enter Religion"
                                                TabIndex="8" /><span class="validstar">+</span>--%>

                                            <%-- <asp:RequiredFieldValidator ID="rfvReligion" runat="server" ControlToValidate="ddlReligion"
                                                Display="None" ErrorMessage="Please Select Religion" InitialValue="0" ValidationGroup="academic"
                                                SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Nationality</label>
                                            </div>
                                            <asp:DropDownList ID="ddlNationality" runat="server" TabIndex="1326" AppendDataBoundItems="true"
                                                ToolTip="Please Select Nationality" />
                                            <%--    <asp:RequiredFieldValidator ID="rfvNationality" runat="server" ControlToValidate="ddlNationality"
                                                    Display="None" ErrorMessage="Please Select Nationality" InitialValue="0" ValidationGroup="academic"
                                                    SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>SR Category</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="4" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Please Select Caste Category">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory"
                                                Display="None" ErrorMessage="Please Select Caste Category" ValidationGroup="academic" InitialValue="0"
                                                SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Claimed Category</label>
                                            </div>
                                            <asp:DropDownList ID="ddlClaimedCat" runat="server" TabIndex="5" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Please Select Claimed Category" />
                                            <%--<asp:RequiredFieldValidator ID="rfvClaimedCategory" runat="server" ControlToValidate="ddlClaimedCat"
                                                    Display="None" ErrorMessage="Please Select Claimed Category" ValidationGroup="academic" InitialValue="0"
                                                    SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Entrance Exam</label>
                                            </div>
                                            <asp:DropDownList ID="ddlExamNo" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlExamNo_SelectedIndexChanged1"
                                                CssClass="form-control" data-select2-enable="true" TabIndex="14">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="rfvddlExamNo" runat="server" ControlToValidate="ddlExamNo"
                                                Display="None" ErrorMessage="Please Select Entrance Exam" ValidationGroup="academic" InitialValue="0"
                                                SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>--%>
                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divotherentrance" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup id="supotherentrance" runat="server">* </sup>
                                                <label>Other Entrance Exam</label>
                                            </div>
                                            <asp:TextBox ID="txtothetentrance" runat="server" TabIndex="8213" MaxLength="15" CssClass="form-control"></asp:TextBox>

                                            <%--Change by Rahul Moraskar 2022-07-26--%>
                                            <%--ID="RequiredFieldValidator3"--%>

                                            <asp:RequiredFieldValidator ID="rfvothetentrance" runat="server" ControlToValidate="txtothetentrance"
                                                Display="None" ErrorMessage="Please Enter Other Entrance Exam" ValidationGroup="academic"
                                                SetFocusOnError="true" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>CET/COMEDK Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtcetcomdate" runat="server" TabIndex="16" onchange="CheckFutureDate(this);" CssClass="form-control" placeholder="dd/MM/yyyy" />
                                                <%--<asp:Image ID="imgCalDateOfAdmission" runat="server" src="../images/calendar.png" />--%>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcetcomdate"
                                                Display="none" ErrorMessage="Please Enter CET/COMEDK Date" SetFocusOnError="true"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtcetcomdate" PopupButtonID="imgCalDateOfAdmission" Enabled="true"
                                                    EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Fees Paid at KEA/COMEDK</label>
                                            </div>
                                            <asp:TextBox ID="txtfeepaid" runat="server" TabIndex="12" ToolTip="Please Enter Fees." onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />
                                            <%--<asp:RequiredFieldValidator ID="rfvpaid" runat="server" ControlToValidate="txtfeepaid"
                                                Display="None" ErrorMessage="Please Enter Fees Paid at KEA/COMEDK" ValidationGroup="academic"
                                                SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10"
                                                runat="server" FilterType="Numbers" TargetControlID="txtfeepaid">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Martial Status</label>
                                            </div>
                                            <asp:RadioButton ID="rdoMarriedYes" runat="server" GroupName="Married"
                                                TabIndex="11" />
                                            Married
                                            <asp:RadioButton ID="rdoMarriedNo" runat="server" GroupName="Married"
                                                TabIndex="12" Checked="true" />
                                            Unmarried
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Student Indus Email</label>
                                            </div>
                                            <asp:TextBox ID="txtIUEmail" runat="server" ToolTip="Please Enter Student Indus Email"
                                                TabIndex="14" />
                                            <%--Change by Rahul Moraskar 2022-07-26--%>
                                            <%--ID="rfvIuEmail"--%>

                                            <asp:RegularExpressionValidator ID="revIuEmail" runat="server" ControlToValidate="txtIUEmail"
                                                Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorMessage="Please Enter Valid EmailID" ValidationGroup="academic"></asp:RegularExpressionValidator>
                                            <%--     <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtStudEmail"
                                            Display="None" ErrorMessage="Please Enter Student Indus Email" ValidationGroup="academic"
                                            SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Blood Group</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBloodGrp" runat="server" TabIndex="20" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Blood Group" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            <%--   <asp:RequiredFieldValidator ID="rfvBloodGrp" runat="server" ControlToValidate="ddlBloodGrp"
                                                    Display="None" ErrorMessage="Please Select Blood Group" ValidationGroup="academic" InitialValue="0"
                                                    SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Physical Handicap</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPhyHandicap" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                                TabIndex="17">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPhyHandicap"
                                                    Display="None" ErrorMessage="Please Select Physical Handicap " ValidationGroup="academic" InitialValue="0"
                                                    SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divAllotedCat" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supAllotedCat" runat="server">* </sup>
                                                <label>Category</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAllotedCat" runat="server" TabIndex="21" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Category" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAllotedCategory" runat="server" ControlToValidate="ddlAllotedCat"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Category"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Date of Admission</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDateOfAdmission" runat="server" TabIndex="19" onchange="CheckFutureDate(this);" CssClass="form-control" placeholder="dd/MM/yyyy" Visible="false" />
                                                <%--<asp:Image ID="imgCalDateOfAdmission" runat="server" src="../images/calendar.png" />--%>
                                                <%--  <asp:RequiredFieldValidator ID="rfvDateOfAdmission" runat="server" ControlToValidate="txtDateOfAdmission"
                                                Display="none" ErrorMessage="Please Enter Date Of Admission" SetFocusOnError="true"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                                <ajaxToolKit:CalendarExtender ID="ceDateOfAdmission" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDateOfAdmission" PopupButtonID="imgCalDateOfAdmission" Enabled="true"
                                                    EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>

                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Entrance Roll No.</label>
                                            </div>
                                            <asp:TextBox ID="txtJeeRollNo" runat="server" TabIndex="1866" MaxLength="15" placeholder="Please Enter Entrance exam Roll No"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvJeeRollNo" runat="server" ControlToValidate="txtJeeRollNo"
                                            Display="None" ErrorMessage="Please Enter Entrance exam Roll No " ValidationGroup="academic"
                                            SetFocusOnError="true" />--%>
                                        </div>

                                        <%--Ask to Sir END --%>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDateOfReporting" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supDateOfReporting" runat="server">* </sup>
                                                <label>Date of Entry</label>
                                            </div>
                                            <asp:TextBox ID="txtDateOfReporting" runat="server" TabIndex="16" CssClass="form-control" placeholder="dd/MM/yyyy" />
                                            <%--<asp:Image ID="imgCalDateOfAdmission" runat="server" src="../images/calendar.png" />--%>
                                            <asp:RequiredFieldValidator ID="rfvDateOfReporting" runat="server" ControlToValidate="txtDateOfReporting"
                                                Display="none" ErrorMessage="Please Enter Date of Entry" SetFocusOnError="true"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:CalendarExtender ID="ceDateOfReporting" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDateOfReporting" PopupButtonID="imgCalDateOfAdmission" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <%--  <asp:TextBox ID="txtDateOfReporting" runat="server" TabIndex="22" CssClass="form-control" placeholder="dd/MM/yyyy" ReadOnly="true" />
                                            <%--<asp:Image ID="imgCalDateOfAdmission" runat="server" src="../images/calendar.png" />--%>
                                            <%--<asp:RequiredFieldValidator ID="rfvDateOfReporting" runat="server" ControlToValidate="txtDateOfReporting"
                                                Display="none" ErrorMessage="Please Enter Date" SetFocusOnError="true"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Rank</label>
                                            </div>
                                            <asp:TextBox ID="txtJeeRankNo" runat="server" TabIndex="1055" MaxLength="10"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvRankRollNo" runat="server" ControlToValidate="txtJeeRankNo"
                                                Display="None" ErrorMessage="Please Enter Entrance exam Rank" ValidationGroup="academic"
                                                SetFocusOnError="true" />--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteRank"
                                                runat="server" FilterType="Numbers" TargetControlID="txtJeeRankNo">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>CET/COMEDK Order No</label>
                                            </div>
                                            <asp:TextBox ID="txtcetcomorederno" runat="server" TabIndex="1355" ToolTip="Please Enter CET/COMEDK Order No." onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />
                                            <%--<asp:RequiredFieldValidator ID="rfvcetcomorederno" runat="server" ControlToValidate="txtcetcomorederno"
                                                Display="None" ErrorMessage="Please Enter CET/COMEDK Order No" ValidationGroup="academic"
                                                SetFocusOnError="true" />--%>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPaymentType" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supPaymentType" runat="server">* </sup>
                                                <label>Payment Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPaymentType" runat="server" TabIndex="23" AppendDataBoundItems="true" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Admission Category for Payment" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPaymentType" runat="server" ControlToValidate="ddlPaymentType"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Payment Type"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Section</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSection" runat="server" TabIndex="2145" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Semester" />
                                            <%--<asp:RequiredFieldValidator ID="rfvsection" runat="server" ControlToValidate="ddlSection"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Section"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Aadhaar No.</label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtAadhaarNo" TabIndex="24" placeholder="Please Enter Aadhaar no." AutoPostBack="true" MaxLength="12" onkeyup="IsNumeric(this);" OnTextChanged="txtAadhaarNo_TextChanged"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAadhaarNo"
                                                Display="None" ErrorMessage="Please Enter Aadhaar No." ValidationGroup="academic"
                                                SetFocusOnError="true" />--%>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divapplicationid" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supapplicationid" runat="server"> </sup>
                                                <label>Application ID</label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtapplicationid" TabIndex="25" placeholder="Please Enter Application ID" MaxLength="12" ReadOnly="true"></asp:TextBox>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divmerirtno" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supmerirtno" runat="server">* </sup>
                                                <label>Merit No.</label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtmerirtno" TabIndex="26" placeholder="Please Enter Merit no." AutoPostBack="true" MaxLength="10" onkeyup="IsNumeric(this);" Enabled="false"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="rfvmerirtno" runat="server" ControlToValidate="txtmerirtno"
                                                Display="None" ErrorMessage="Please Enter Merit No." ValidationGroup="academic"
                                                SetFocusOnError="true" />
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divScore" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supScore" runat="server">* </sup>
                                                <label>Score</label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtscore" TabIndex="27" placeholder="Please Enter Score" MaxLength="12" onkeyup="IsNumeric(this);" Enabled="false"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvscore" runat="server" ControlToValidate="txtscore"
                                                Display="None" ErrorMessage="Please Enter Score" ValidationGroup="academic"
                                                SetFocusOnError="true" />
                                        </div>

                                        <div id="Div3" class="form-group col-lg-3 col-md-6 col-12" hidden="hidden" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College Code</label>
                                            </div>
                                            <asp:DropDownList ID="ddlcolcode" runat="server" TabIndex="28" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Please Select College Code">
                                                <asp:ListItem Value="0">Please select</asp:ListItem>
                                                <asp:ListItem Value="1">E021</asp:ListItem>
                                                <asp:ListItem Value="2">E057</asp:ListItem>
                                                <asp:ListItem Value="3">B292</asp:ListItem>
                                                <asp:ListItem Value="4">B292BC</asp:ListItem>
                                                <asp:ListItem Value="5">B292BR</asp:ListItem>
                                                <asp:ListItem Value="6">E721 </asp:ListItem>
                                                <asp:ListItem Value="7">B292BD</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="rfvcolcode" runat="server" ControlToValidate="ddlcolcode"
                                                Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select College Code"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divAppliedFees" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supAppliedFees" runat="server">* </sup>
                                                <label>Total Applicable Fees</label>
                                            </div>

                                            <div>
                                                <asp:TextBox runat="server" ID="txtAppliedFees" TabIndex="27" placeholder="Applicable Fees" MaxLength="12"
                                                    Enabled="false"></asp:TextBox>

                                            </div>
                                        </div>

                                        <%-- <div class="col-lg-3 col-md-6 col-12">--%>
                                        <div class="form-group col-md-12">

                                            <%--Add div ID Change by Rahul Moraskar 2022-07-26--%>

                                            <div class="sub-heading" id="divScholarshipDetails" runat="server">
                                                <h5>Scholarship Details</h5>
                                            </div>
                                            <div class="row">

                                                <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divScholarship" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supScholarship" runat="server">* </sup>
                                                        <label>Scholarship</label>
                                                    </div>
                                                    <div class="switch form-inline d-none">
                                                        <input type="checkbox" id="Checkbox1" name="switch" runat="server" />
                                                        <label data-on="Yes" data-off="No" for="installment"></label>
                                                    </div>
                                                    <div>
                                                        <asp:RadioButtonList ID="rdoscholarship" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" TabIndex="29" OnSelectedIndexChanged="rdoscholarship_SelectedIndexChanged">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>

                                                <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divschtype" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup id="sup1" runat="server">* </sup>
                                                        <label>Scholarship Type</label>
                                                    </div>
                                                         <asp:DropDownList ID="ddlSchType" runat="server" TabIndex="32" AppendDataBoundItems="true"  CssClass="form-control" data-select2-enable="true" 
                                                             ToolTip="Please Select Scholarship Type" AutoPostBack="true" OnSelectedIndexChanged="ddlSchType_SelectedIndexChanged">
                                                         </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSchType"
                                                            Display="None" ErrorMessage="Please Select Scholarship Type" ValidationGroup="academic" InitialValue="0"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divschmode" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup id="supschmode" runat="server">* </sup>
                                                        <label>Scholarship Mode</label>
                                                    </div>
                                                    <div>
                                                        <asp:DropDownList ID="ddlSchMode" runat="server" TabIndex="32" AppendDataBoundItems="true"
                                                            CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Scholarship Mode" AutoPostBack="true" OnSelectedIndexChanged="ddlSchMode_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                                            <asp:ListItem Value="1">Percentage Wise</asp:ListItem>
                                                            <asp:ListItem Value="2">Amount Wise</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <%--Change by Rahul Moraskar 2022-07-26--%>
                                                        <%--ID="RequiredFieldValidator1"--%>

                                                        <asp:RequiredFieldValidator ID="rfvSchMode" runat="server" ControlToValidate="ddlSchMode"
                                                            Display="None" ErrorMessage="Please Select Scholarship Mode" ValidationGroup="academic" InitialValue="0"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divAmt" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup id="supAmt" runat="server">* </sup>
                                                        <asp:Label runat="server" ID="lblamt"></asp:Label>
                                                    </div>

                                                    <div>
                                                        <asp:TextBox runat="server" ID="txtschAmt" TabIndex="27" onkeyup="validateNumericAndNotZero(this);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvschamt" runat="server" ControlToValidate="txtschAmt"
                                                            Display="None" ErrorMessage="Please Enter" ValidationGroup="academic"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                            </div>

                                            <%--Add div ID Change by Rahul Moraskar 2022-07-26--%>

                                            <div class="sub-heading" id="divFeeInstallmentDetails" runat="server">
                                                <h5>Fee Installment Details</h5>
                                            </div>
                                            <div class="row">

                                                <%--Add div ID and sup ID Change by Rahul Moraskar 2022-07-26--%>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divinstallment" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supinstallment" runat="server">* </sup>
                                                        <label>Fee Installment</label>
                                                    </div>
                                                    <div class="switch form-inline d-none">
                                                        <input type="checkbox" id="installment" name="switch" runat="server" />
                                                        <label data-on="Yes" data-off="No" for="installment"></label>
                                                    </div>
                                                    <div>
                                                        <asp:RadioButtonList ID="rdoInstallment" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoInstallment_SelectedIndexChanged" TabIndex="31">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>



                                                <%--</div>--%>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divinstaltype" visible="False">
                                                    <div class="label-dynamic" id="Div6" runat="server">
                                                        <sup id="supinstaltype" runat="server">* </sup>
                                                        <label>Installment Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlinstallmenttype" runat="server" TabIndex="32" AppendDataBoundItems="true"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Installment Type" AutoPostBack="true" OnSelectedIndexChanged="ddlinstallmenttype_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="rfvinstallmenttype" runat="server" ControlToValidate="ddlinstallmenttype"
                                                            Display="None" ErrorMessage="Please Select Installment Type" ValidationGroup="academic" InitialValue="0"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivDuedate1" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup id="supDuedate1" runat="server">* </sup>
                                                        <label>1st Installment Due Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="Div7">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtduedate1" runat="server" TabIndex="33" ValidationGroup="academic" />
                                                        <%-- <asp:Image ID="imgCalDateOfBirth" runat="server" src="../images/calendar.png" Width="16px" />--%>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtduedate1" PopupButtonID="txtduedate1" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtduedate1"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" EmptyValueMessage="Please Enter 1st Installment Due Date"
                                                            ControlExtender="meeDateOfBirth" ControlToValidate="txtduedate1" IsValidEmpty="true"
                                                            InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                            ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            ValidationGroup="academic" SetFocusOnError="true" />

                                                        <%--Change by Rahul Moraskar 2022-07-26--%>
                                                        <%--ID="RequiredFieldValidator4"--%>


                                                        <asp:RequiredFieldValidator ID="rfvduedate1" runat="server" ControlToValidate="txtduedate1"
                                                            Display="None" ErrorMessage="Please Select 1st installment Due Date" ValidationGroup="academic"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivDuedate2" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup id="supDuedate2" runat="server">* </sup>
                                                        <label>2nd Installment Due Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="div8">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtduedate2" runat="server" TabIndex="34" ValidationGroup="academic" />
                                                        <%-- <asp:Image ID="imgCalDateOfBirth" runat="server" src="../images/calendar.png" Width="16px" />--%>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtduedate2" PopupButtonID="txtduedate2" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtduedate2"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" EmptyValueMessage="Please Enter 2nd installment Due Date"
                                                            ControlExtender="meeDateOfBirth" ControlToValidate="txtduedate2" IsValidEmpty="true"
                                                            InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                            ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            ValidationGroup="academic" SetFocusOnError="true" />

                                                        <%--Change by Rahul Moraskar 2022-07-26--%>
                                                        <%--ID="RequiredFieldValidator5"--%>

                                                        <asp:RequiredFieldValidator ID="rfvduedate2" runat="server" ControlToValidate="txtduedate2"
                                                            Display="None" ErrorMessage="Please Select 2nd installment Due Date" ValidationGroup="academic"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivDuedate3" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup id="supDuedate3" runat="server">* </sup>
                                                        <label>3rd Installment Due Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="Div10">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtduedate3" runat="server" TabIndex="35" ValidationGroup="academic" />
                                                        <%-- <asp:Image ID="imgCalDateOfBirth" runat="server" src="../images/calendar.png" Width="16px" />--%>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtduedate3" PopupButtonID="txtduedate3" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtduedate3"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" EmptyValueMessage="Please Enter 3rd installment Due Date"
                                                            ControlExtender="meeDateOfBirth" ControlToValidate="txtduedate3" IsValidEmpty="true"
                                                            InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                            ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            ValidationGroup="academic" SetFocusOnError="true" />

                                                        <%--Change by Rahul Moraskar 2022-07-26--%>
                                                        <%--ID="RequiredFieldValidator8"--%>

                                                        <asp:RequiredFieldValidator ID="rfvduedate3" runat="server" ControlToValidate="txtduedate3"
                                                            Display="None" ErrorMessage="Please Select 3rd installment Due Date" ValidationGroup="academic"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivDuedate4" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup id="supDuedate4" runat="server">* </sup>
                                                        <label>4th Installment Due Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="Div11">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtduedate4" runat="server" TabIndex="36" ValidationGroup="academic" />
                                                        <%-- <asp:Image ID="imgCalDateOfBirth" runat="server" src="../images/calendar.png" Width="16px" />--%>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtduedate4" PopupButtonID="txtduedate4" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtduedate4"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" EmptyValueMessage="Please Enter 4th installment Due Date"
                                                            ControlExtender="meeDateOfBirth" ControlToValidate="txtduedate4" IsValidEmpty="true"
                                                            InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                            ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            ValidationGroup="academic" SetFocusOnError="true" />

                                                        <%--Change by Rahul Moraskar 2022-07-26--%>
                                                        <%--ID="RequiredFieldValidator9"--%>


                                                        <asp:RequiredFieldValidator ID="rfvduedate4" runat="server" ControlToValidate="txtduedate4"
                                                            Display="None" ErrorMessage="Please Select 4th installment Due Date" ValidationGroup="academic"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivdueDate5" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup id="supDuedate5" runat="server">* </sup>
                                                        <label>5th Installment Due Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="Div13">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtduedate5" runat="server" TabIndex="37" ValidationGroup="academic" />
                                                        <%-- <asp:Image ID="imgCalDateOfBirth" runat="server" src="../images/calendar.png" Width="16px" />--%>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtduedate5" PopupButtonID="txtduedate5" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtduedate5"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" EmptyValueMessage="Please Enter 5th installment Due Date"
                                                            ControlExtender="meeDateOfBirth" ControlToValidate="txtduedate5" IsValidEmpty="true"
                                                            InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                            ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            ValidationGroup="academic" SetFocusOnError="true" />

                                                        <%--Change by Rahul Moraskar 2022-07-26--%>
                                                        <%--ID="RequiredFieldValidator10"--%>

                                                        <asp:RequiredFieldValidator ID="rfvduedate5" runat="server" ControlToValidate="txtduedate5"
                                                            Display="None" ErrorMessage="Please Select 5th installment Due Date" ValidationGroup="academic"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <%--Add div ID Change by Rahul Moraskar 2022-07-26--%>

                                            <div class="sub-heading" id="divHostellerDetails" runat="server">
                                                <h5>Hosteller Details</h5>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divhosteller" runat="server">
                                                    <%--<div id="dvhosteller" runat="server">--%>
                                                    <div class="label-dynamic">
                                                        <label id="lbhosteller" runat="server"><sup id="suphosteller" runat="server">*</sup>Hosteller</label>

                                                    </div>
                                                    <%--  </div>--%>
                                                    <%--    <div class="switch form-inline d-none">
                                                        <input type="checkbox" id="hostel" name="switch" runat="server" />
                                                        <label data-on="Yes" data-off="No" for="switch"></label>
                                                    </div>--%>
                                                    <div>
                                                        <asp:RadioButtonList ID="rdoHosteler" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoHosteler_SelectedIndexChanged" RepeatDirection="Horizontal" TabIndex="28">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>

                                                <%--Add div ID Change by Rahul Moraskar 2022-07-26--%>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divHostel" runat="server" visible="false">
                                                    <%--visible="false"--%>
                                                    <div class="label-dynamic">
                                                        <sup id="supHostel" runat="server">* </sup>
                                                        <label>Hostel Type</label>
                                                        <asp:Label runat="server" ID="lblHostelType"></asp:Label>
                                                    </div>
                                                    <div>

                                                        <asp:DropDownList ID="ddlHostel" runat="server" TabIndex="32" AppendDataBoundItems="true"
                                                            CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Scholarship Mode" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                                            <%--<asp:ListItem Value="1">Hostel AC (Three Seated)</asp:ListItem>
                                                            <asp:ListItem Value="2">Hostel Non-AC (Three Seated)</asp:ListItem>
                                                            <asp:ListItem Value="3">Hostel Non-AC (Four Seated)</asp:ListItem>--%>
                                                        </asp:DropDownList>

                                                        <%--Ask to sir--%>
                                                        <%--Change by Rahul Moraskar 2022-07-26--%>
                                                        <%--ID="RequiredFieldValidator6"  ControlToValidate="ddlSchMode" --%>

                                                        <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ControlToValidate="ddlHostel"
                                                            Display="None" ErrorMessage="Please Select Hostel Type" ValidationGroup="academic" InitialValue="0"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSchMode"
                                                            Display="None" ErrorMessage="Please Select Scholarship Mode" ValidationGroup="academic" InitialValue="0"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <%--Add div ID Change by Rahul Moraskar 2022-07-26--%>

                                            <div class="sub-heading" id="divTransportationDetails" runat="server">
                                                <h5>Transportation Details</h5>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divTransportation" runat="server">
                                                    <%-- <div id="dvtransport" runat="server">--%>
                                                    <div class="label-dynamic">
                                                        <label id="lbtransport" runat="server"><sup id="supTransportation" runat="server">*</sup>Transportation</label>

                                                    </div>
                                                    <%--  </div>--%>
                                                    <%--    <div class="switch form-inline d-none">
                                                        <input type="checkbox" id="transport" name="switch" runat="server" />
                                                        <label data-on="Yes" data-off="No" for="transport"></label>
                                                    </div>--%>
                                                    <div>
                                                        <asp:RadioButtonList ID="rdbTransport" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdbTransport_SelectedIndexChanged" RepeatDirection="Horizontal" TabIndex="30">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                <div class="label-dynamic" id="lblHosteller" runat="server" visible="False">
                                                    <sup>* </sup>
                                                    <label>Select Hosteller</label>
                                                </div>
                                                <asp:DropDownList ID="ddlHosteller" runat="server" TabIndex="27" AppendDataBoundItems="true" Visible="false"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Please Select College Code">
                                                    <asp:ListItem Value="0">Please select</asp:ListItem>
                                                    <asp:ListItem Value="1">AC</asp:ListItem>
                                                    <asp:ListItem Value="2">NON-AC</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                <div class="label-dynamic" id="lblTransport" runat="server" visible="False">
                                                    <sup>* </sup>
                                                    <label>Select Transportation</label>
                                                </div>
                                                <asp:DropDownList ID="ddlTransport" runat="server" TabIndex="27" AppendDataBoundItems="true" Visible="false"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Please Select College Code">
                                                    <asp:ListItem Value="0">Please select</asp:ListItem>
                                                    <asp:ListItem Value="1">Gamharia</asp:ListItem>
                                                    <asp:ListItem Value="2">Other than Gamharia</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>State</label>
                                            </div>
                                            <asp:TextBox ID="txtState" runat="server" class="tbState" ToolTip="Please Enter State" TabIndex="24" />
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtState"
                                            Display="None" ErrorMessage="Please Enter Permantnant State" ValidationGroup="academic"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>

                                    </div>
                                </div>

                                <div class="form-group col-12" id="trphoto_sign" runat="server" style="display: none;">
                                    <div class="label-dynamic" id="trphotosign">
                                        <sup>* </sup>
                                        <label>Photo And Sign Upload</label>
                                    </div>
                                    <div class="col-12" id="divUpload" runat="server">
                                        <div class="row">
                                            <div class="col-lg-3 col-md-6 col-12">
                                                <label></label>
                                                <asp:Image ID="imgPhoto" runat="server" Width="128px" Height="128px" />
                                                <asp:FileUpload ID="fuPhotoUpload" runat="server" TabIndex="19" onChange="LoadImage()" />
                                            </div>
                                            <div class="col-lg-3 col-md-6 col-12">
                                                <label>Sign Upload</label>
                                                <asp:Image ID="ImgSign" runat="server" Width="125px" Height="40px" />
                                                <asp:FileUpload ID="fuSignUpload" runat="server" onChange="LoadImageSign()" TabIndex="20" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group col-lg-6 col-md-12 col-12" style="display: none">
                                    <div class=" note-div">
                                        <h5 class="heading">Note (Please Select)</h5>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Don't forget to print admission slip after submission of New Student entry.</span>  </p>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" TabIndex="38" ToolTip="Submit Student Information"
                                        ValidationGroup="academic" CssClass="btn btn-primary" OnClick="btnSave_Click" OnClientClick = "Confirm()"/>
                                    <asp:Button ID="btnpayment" runat="server" Text="Offline Fees" TabIndex="24" ToolTip="For Offline Fees Collection"
                                        CausesValidation="false" CssClass="btn btn-primary" OnClick="btnpayment_Click"
                                        Visible="false" />
                                    <asp:Button ID="btnReport" runat="server" Text="Admission Slip/Offer Letter" TabIndex="39" ToolTip="Admission Slip/Offer Letter"
                                        CausesValidation="false" CssClass="btn btn-info" ValidationGroup="PayType"
                                        Visible="true" OnClick="btnReport_Click" />
                                    <asp:Button ID="btnChallan" runat="server" Text="Challan" TabIndex="24" ToolTip="Print Challan"
                                        CausesValidation="false" CssClass="btn btn-primary" OnClick="btnChallan_Click"
                                        Visible="false" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Reset" TabIndex="40" ToolTip="Cancel Student Information"
                                        CausesValidation="false" CssClass="btn btn-warning" ValidationGroup="academic"
                                        OnClick="btnCancel_Click" />

                                    <div id="divsendmail" runat="server" visible="false">
                                      <asp:Button ID="btnsendmail" runat="server" Text="Send Mail" TabIndex="40" ToolTip="Send Mail"
                                        CausesValidation="false" CssClass="btn btn-warning" ValidationGroup="academic" OnClick="btnsendmail_Click" />
                                    </div>


                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="academic"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ddinfo" />

                                </div>
                            </div>
                        </div>

                        <div class="col-md-12" style="display: none;">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Address and Contact Details </h3>
                                    <div class="box-tools pull-right">
                                        <img id="img2" style="cursor: pointer;" src="../Images/collapse_blue.jpg" alt=""
                                            onclick="javascript:toggleExpansion(this,'divAddressAndContactDetails')" />
                                    </div>
                                </div>
                                <div class="box-body" id="divAddressAndContactDetails" style="display: block;">
                                    <div class="col-md-6">
                                        <legend>Permenant Address</legend>
                                        <div>

                                            <div class="form-group col-md-12">
                                                <label>Permanent Address</label>
                                                <asp:TextBox ID="txtPermanentAddress" runat="server" TabIndex="21" CssClass="form-control"
                                                    TextMode="MultiLine" ToolTip="Please Enter Permanent Address"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label for="city">City/Village</label>
                                                <asp:TextBox ID="txtCity" runat="server" class="tbCity" ToolTip="Please Enter City"
                                                    TabIndex="14" />
                                                <%--  <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity"
                                    Display="None" ErrorMessage="Please Enter Permantnant City" ValidationGroup="academic"
                                    SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label for="city">ZIP/PIN</label>
                                                <asp:TextBox ID="txtPIN" runat="server" TabIndex="15" MaxLength="6" ToolTip="Please Enter PIN" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteTxtPin" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtPIN">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>Mobile No.</label>
                                                <asp:TextBox ID="txtMobileNo" runat="server" ToolTip="Please Enter Mobile No." TabIndex="15" MaxLength="12" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteMobilenum" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtMobileNo">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:CompareValidator ID="rfvMobileNo" runat="server" ControlToValidate="txtMobileNo"
                                                    ErrorMessage="Please Numeric Number" Operator="DataTypeCheck" SetFocusOnError="True"
                                                    Type="Integer" ValidationGroup="Academic" Display="None" Visible="False">
                                                </asp:CompareValidator>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMobileNo"
                                    Display="None" ErrorMessage="Please Enter Mobile Number" ValidationGroup="academic"
                                    SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>Contact No.</label>
                                                <asp:TextBox ID="txtContactNumber" runat="server" TabIndex="16" ValidationGroup="academic"
                                                    MaxLength="20" ToolTip="Please Enter Contact Number" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteTxtContactNum" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtContactNumber">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <legend>Local Address and Contact Details</legend>
                                        <div id="divGuardianAddress" style="display: block;">
                                            <div class="form-group col-md-12">
                                                <label>
                                                    Detailed Address (Copy Address) 
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/copy.png" OnClientClick="copyPermAddr(this)"
                                                ToolTip="Copy Permanent Address" TabIndex="16" /></label>
                                                <asp:TextBox ID="txtPostalAddress" runat="server" TabIndex="16" TextMode="MultiLine"
                                                    ToolTip="Please Enter Postal Address"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>City</label>
                                                <asp:TextBox ID="txtLocalCity" runat="server" class="tbCity" ToolTip="Please Enter Local City"
                                                    TabIndex="16" />
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>State</label>
                                                <asp:TextBox ID="txtLocalState" runat="server" Width="85%" class="tbState" ToolTip="Please Enter Local State"
                                                    TabIndex="24" />
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>Mobile No.</label>
                                                <asp:TextBox ID="txtGuardianMobile" runat="server" ToolTip="Please Enter Guardian Mobile No."
                                                    TabIndex="16" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                    FilterType="Numbers" TargetControlID="txtGuardianMobile">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:CompareValidator ID="rfvGuardianMobileNo" runat="server" ControlToValidate="txtGuardianMobile"
                                                    ErrorMessage="Please Numeric Number" Operator="DataTypeCheck" SetFocusOnError="True"
                                                    Type="Integer" ValidationGroup="academic" Display="None"></asp:CompareValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>Contact No.</label>
                                                <asp:TextBox ID="txtGuardianPhone" runat="server" ToolTip="Please Enter Guardian Phone No."
                                                    TabIndex="16" />
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>Email Id</label>
                                                <asp:TextBox ID="txtGuardianEmail" runat="server" TabIndex="16" ToolTip="Please Enter Email" />
                                                <asp:RegularExpressionValidator ID="rfvGuardianEmail" runat="server" ControlToValidate="txtGuardianEmail"
                                                    Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ErrorMessage="Please Enter Valid EmailID" ValidationGroup="academic"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12" style="display: none;">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Admission Details </h3>
                                    <div class="box-tools pull-right">
                                        <img id="img3" style="cursor: pointer;" src="../Images/collapse_blue.jpg" alt=""
                                            onclick="javascript:toggleExpansion(this,'divAdmissionDetails')" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div id="divAdmissionDetails" style="display: block;">
                                        <div class=" form-group col-md-3">
                                        </div>

                                        <div class="form-group col-md-3"></div>
                                        <div class="form-group col-md-3"></div>
                                        <div class="form-group col-md-3"></div>
                                        <div class="form-group col-md-3"></div>
                                        <div class="form-group col-md-3"></div>
                                        <div class="form-group col-md-3"></div>
                                        <div class="form-group col-md-3"></div>
                                        <div class="form-group col-md-3"></div>

                                        <div class="form-group col-md-3">
                                            <label>State of Eligibility</label>
                                            <asp:TextBox ID="txtStateOfEligibility" runat="server" class="tbState"
                                                ToolTip="Please Enter State Of Eligibility" TabIndex="24" />
                                        </div>

                                        <div class="form-group col-md-3" style="display: none">
                                            <label>Admission Round</label>
                                            <asp:DropDownList ID="ddlAdmRound" runat="server" TabIndex="26" AppendDataBoundItems="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12" style="display: none;">
                            <div class="box box-primary">
                                <div class="box-header with-border" runat="server" id="tblExam" style="display: none">
                                    <h3 class="box-title">Entrance Exam Scores</h3>
                                    <div class="box-tools pull-right">
                                        <img id="img4" style="cursor: pointer;" src="../images/collapse_blue.jpg" alt=""
                                            onclick="javascript:toggleExpansion(this,'divEntranceExamScores')" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div id="divEntranceExamScores" style="display: block;">
                                        <div class="col-md-6" runat="server" id="trExam" visible="false">
                                            <div class="col-md-6">
                                                <label>Exam name</label>

                                            </div>
                                            <div class="col-md-6">
                                                <label>Year of Exam</label>
                                                <asp:TextBox ID="txtYearOfExam" runat="server" MaxLength="4"
                                                    onkeyup="validateNumeric(this);" TabIndex="27"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6" id="trInfo" style="display: none">

                                            <div class="col-md-6">
                                                <label>Quota</label>
                                                <asp:DropDownList ID="ddlQuota" runat="server" AppendDataBoundItems="True"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <label>Exam Roll No</label>
                                                <asp:TextBox ID="txtQExamRollNo" runat="server"
                                                    ToolTip="Please Enter Qualifying Exam Roll No"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>All India Rank </label>
                                                <asp:TextBox ID="txtAllIndiaRank" runat="server"
                                                    ToolTip="Please Enter All India Rank"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="txtAllIndiaRank_FilteredTextBoxExtender"
                                                    runat="server" FilterType="Numbers" TargetControlID="txtAllIndiaRank">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <div class="col-md-4" id="trPer" runat="server" visible="false">
                                            <div class="col-md-6">
                                                <label>Percentage </label>
                                                <asp:TextBox ID="txtPer" runat="server" onkeyup="validateNumeric(this);"
                                                    ToolTip="Please Enter Percentage"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label>Percentile </label>
                                                <asp:TextBox ID="txtPercentile" runat="server"
                                                    onkeyup="validateNumeric(this);" TabIndex="29"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-2" runat="server" id="trSpotOption" visible="false">
                                            <label>Spot Option </label>
                                            <asp:DropDownList ID="ddlSpotOption" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlSpotOption_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">--Please Select--</asp:ListItem>
                                                <asp:ListItem Value="1">GATE </asp:ListItem>
                                                <asp:ListItem Value="2">Non GATE</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-2" id="trGetScore" runat="server" visible="false">
                                            <label>Gate Score</label>
                                            <asp:TextBox ID="txtStateRank" runat="server"
                                                ToolTip="Please Enter State Rank"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtStateRank_FilteredTextBoxExtender"
                                                runat="server" FilterType="Numbers" TargetControlID="txtStateRank">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-md-2" runat="server" id="trCCMT" visible="false">
                                            <label>Paid by e-challan</label>
                                            <asp:TextBox ID="txtDDAmountPaid" runat="server"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtDDAmountPaid" FilterType="Numbers" />

                                            <%--Change by Rahul Moraskar 2022-07-26--%>
                                            <%--ID="DDpaidAmt"--%>

                                            <asp:RequiredFieldValidator ID="rvfDDAmountPaid" runat="server"
                                                ControlToValidate="txtDDAmountPaid" Display="None"
                                                ErrorMessage="Please enter DD through paid amount." SetFocusOnError="true"
                                                ValidationGroup="academic" />
                                        </div>
                                        <div class="form-group col-md-2" runat="server" id="trMca" visible="false">
                                            <label>Reported at Round 1/2</label>
                                            <asp:DropDownList ID="ddlRound" runat="server" AutoPostBack="True">
                                                <asp:ListItem Selected="True" Value="-1">--Please Select--</asp:ListItem>
                                                <asp:ListItem Value="1">Yes </asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvRound" runat="server"
                                                ControlToValidate="ddlRound" Display="None"
                                                ErrorMessage="Please Select Round" InitialValue="-1" SetFocusOnError="true"
                                                ValidationGroup="academic"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-2" runat="server" id="tr1" visible="false">
                                            <label>Reconcile Process</label>
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="0">Yes</asp:ListItem>
                                                <asp:ListItem Value="1">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-md-3" runat="server" id="trReconcile" visible="false">
                                            <label>Reconcile Process</label>
                                            <asp:RadioButtonList ID="rblReconcile" runat="server"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="0">Yes</asp:ListItem>
                                                <asp:ListItem Value="1">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <asp:UpdatePanel ID="UpdpayType" runat="server">
                                            <ContentTemplate>
                                                <div class="form-group col-md-4" runat="server" id="trPaytype" visible="false">
                                                    <div id="divPaytype" runat="server">
                                                        <label>Pay Type</label>
                                                        <asp:TextBox ID="txtPayType" runat="server"
                                                            onblur="ValidatePayType(this); UpdateCash_DD_Amount();" MaxLength="1"
                                                            ToolTip="Enter C for cash payment OR D for payment by demand draft OR I for I-Collect Payment"></asp:TextBox>

                                                        <%--Change by Rahul Moraskar 2022-07-26--%>
                                                        <%--ID="valPayType"--%>

                                                        <asp:RequiredFieldValidator ID="rfvPayType" runat="server"
                                                            ControlToValidate="txtPayType" Display="None"
                                                            ErrorMessage="Please enter type of payment whether cash(C) or demand draft(D) or I-Collect (I)."
                                                            SetFocusOnError="true" ValidationGroup="academic" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="form-group col-md-12" id="trmsg" runat="server" visible="false">
                                            <label style="font-size: medium; color: Red">Note :&nbsp;&nbsp;</label>
                                            Note : Please enter type of pay Type whether cash(C) or demand draft(D) or&nbsp; SBI - Collect ( I ) and press the tab button.
                  
                                        </div>

                                        <div id="divDDDetails" runat="server" style="display: none">
                                            <fieldset>
                                                <legend>Demand Draft Details/ I - Collect Details</legend>
                                                <div class="form-group col-md-4">
                                                    <label>DD No./Transaction ID</label>
                                                    <asp:TextBox ID="txtDDNo" runat="server" CssClass="data_label" />
                                                    <%--Change by Rahul Moraskar 2022-07-26--%>
                                                    <%--ID="valDDNo"--%>

                                                    <asp:RequiredFieldValidator ID="rfvDDNo" ControlToValidate="txtDDNo" runat="server"
                                                        Display="None" ErrorMessage="Please enter demand draft number." ValidationGroup="dd_info" />
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label>Amount</label>
                                                    <asp:TextBox ID="txtDDAmount" onkeyup="IsNumeric(this);" runat="server"
                                                        CssClass="data_label" />

                                                    <%--Change by Rahul Moraskar 2022-07-26--%>
                                                    <%--ID="valDdAmount"--%>

                                                    <asp:RequiredFieldValidator ID="rvfDDAmount" ControlToValidate="txtDDAmount" runat="server"
                                                        Display="None" ErrorMessage="Please enter amount of demand draft." ValidationGroup="dd_info" />
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label>Date</label>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDDDate" runat="server" TabIndex="38" CssClass="data_label" />
                                                        <%--<asp:Image ID="imgCalDDDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                        <ajaxToolKit:CalendarExtender ID="ceDDDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDDDate"
                                                            PopupButtonID="imgCalDDDate" />
                                                        <ajaxToolKit:MaskedEditExtender ID="meeDDDate" runat="server" TargetControlID="txtDDDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                            OnInvalidCssClass="errordate" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevDDDate" runat="server" ControlExtender="meeDDDate"
                                                            ControlToValidate="txtDDDate" IsValidEmpty="False" EmptyValueMessage="Demand draft date is required"
                                                            InvalidValueMessage="Demand draft date is invalid" EmptyValueBlurredText="*"
                                                            InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="dd_info" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label>City</label>
                                                    <asp:TextBox ID="txtDDCity" runat="server" />
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label>Bank</label>
                                                    <asp:DropDownList ID="ddlBank" AppendDataBoundItems="true" runat="server" />

                                                    <%--Change by Rahul Moraskar 2022-07-26--%>
                                                    <%--ID="valBankName"--%>

                                                    <asp:RequiredFieldValidator ID="rfvBank" runat="server" ControlToValidate="ddlBank"
                                                        Display="None" ErrorMessage="Please select bank name." ValidationGroup="dd_info"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                    <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="dd_info" />
                                                </div>

                                            </fieldset>
                                        </div>

                                        <div id="divCashDate" runat="server" style="display: none" class="form-group col-md-4">
                                            <label>Date</label>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtCashDate" runat="server" TabIndex="8" CssClass="data_label" />
                                                <%--<asp:Image ID="Image2" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtCashDate"
                                                    PopupButtonID="imgCashDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeDate" runat="server" TargetControlID="txtCashDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                    OnInvalidCssClass="errordate" />
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeDate"
                                                    ControlToValidate="txtDDDate" IsValidEmpty="False" EmptyValueMessage="date is required"
                                                    InvalidValueMessage="date is invalid" EmptyValueBlurredText="*"
                                                    InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="dd_info" />

                                            </div>
                                        </div>

                                        <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="ddlExamNo"
                                            ValidationGroup="EntranceExam" Display="None" SetFocusOnError="true" InitialValue="0"
                                            ErrorMessage="Please Select Exam Name"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvYearOfEntranceExam" runat="server" ControlToValidate="txtYearOfExam"
                                            Display="None" SetFocusOnError="true" ValidationGroup="EntranceExam" ErrorMessage="Please Enter Year of Exam">

                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>

                            </div>



                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <%--  <asp:PostBackTrigger ControlID="btnSave" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">

        /* To collapse and expand page sections */
        function toggleExpansion(image, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                image.src = "../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                image.src = "../Images/collapse_blue.jpg";
            }
        }
    </script>

    <script type="text/javascript">
        function chk(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Enter Numbers Only!');
                txt.focus();
                return;
            }
        }
    </script>

    <script type="text/javascript">
        function ValidateBranch() {
            if (document.getElementById('<%= ddlBranch.ClientID %>').selectedIndex == 0) {
                alert("Please Select Branch");
                return false;
            }
            else
                return true;
        }

        function copyPermAddr(chk) {
            if (chk.click) {
                var city = document.getElementById('<%= txtCity.ClientID %>').value;
                var state = document.getElementById('<%= txtState.ClientID %>').value;
                document.getElementById('<%= txtPostalAddress.ClientID %>').value = document.getElementById('<%= txtPermanentAddress.ClientID %>').value
                document.getElementById('<%= txtLocalCity.ClientID %>').value = city;
                document.getElementById('<%= txtLocalState.ClientID %>').value = state;

            }
            else {
                document.getElementById('<%= txtPostalAddress.ClientID %>').value = '';
            }
        }

        function ValidatePayType(txtPayType) {
            try {
                if (txtPayType != null && txtPayType.value != '') {
                    if (txtPayType.value.toUpperCase() == 'D') {

                        txtPayType.value = "D";
                        if (document.getElementById('<%= divDDDetails.ClientID %>') != null) {
                            document.getElementById('<%= divDDDetails.ClientID %>').style.display = "block";
                            document.getElementById('<%= txtDDNo.ClientID%>').focus();
                        }
                        if (document.getElementById('<%= divCashDate.ClientID %>') != null) {
                            document.getElementById('<%= divCashDate.ClientID %>').style.display = "none";
                        }
                    }
                    else {
                        if (txtPayType.value.toUpperCase() == 'I') {
                            txtPayType.value = "I";
                            if (document.getElementById('<%= divDDDetails.ClientID %>') != null) {
                                document.getElementById('<%= divDDDetails.ClientID %>').style.display = "block";
                                document.getElementById('<%= txtDDNo.ClientID%>').focus();
                            }
                            if (document.getElementById('<%= divCashDate.ClientID %>') != null) {
                                document.getElementById('<%= divCashDate.ClientID %>').style.display = "none";
                            }
                        }
                        else {
                            if (txtPayType.value.toUpperCase() == 'C') {
                                txtPayType.value = "C";
                                if (document.getElementById('<%= divDDDetails.ClientID %>') != null) {
                                    document.getElementById('<%= divDDDetails.ClientID %>').style.display = "none";
                                }
                                if (document.getElementById('<%= divCashDate.ClientID %>') != null) {
                                    document.getElementById('<%= divCashDate.ClientID %>').style.display = "block";

                                    //document.getElementById('ctl00_ContentPlaceHolder1_divFeeItems').style.display = "block";
                                }
                            }
                            else {
                                alert("Please enter only 'C' for Cash payment OR 'D' for payment through Demand Drafts OR 'I' for payment through SBI-Collect.");
                                if (document.getElementById('<%= divDDDetails.ClientID %>') != null || document.getElementById('<%= divCashDate.ClientID %>') != null)
                                    document.getElementById('<%= divDDDetails.ClientID %>').style.display = "none";
                                document.getElementById('<%= divCashDate.ClientID %>').style.display = "none";

                                txtPayType.value = "";
                                txtPayType.focus();
                            }
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function UpdateCash_DD_Amount() {
            try {
                var txtPayType = document.getElementById('ctl00_ContentPlaceHolder1_txtPayType');
                var txtPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount');

                if (txtPayType != null && txtPaidAmt != null) {
                    if (txtPayType.value.trim() == "C" && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                        document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = txtPaidAmt.value.trim();
                    }
                    else if (txtPayType.value.trim() == "D" && document.getElementById('tblDD_Details') != null) {
                        var totalDDAmt = 0.00;
                        var dataRows = document.getElementById('tblDD_Details').getElementsByTagName('tr');
                        if (dataRows != null) {
                            for (i = 1; i < dataRows.length; i++) {
                                var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                                var dataCell = dataCellCollection.item(6);
                                if (dataCell != null) {
                                    var txtAmt = dataCell.innerHTML.trim();
                                    totalDDAmt += parseFloat(txtAmt);
                                }
                            }
                            if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount') != null &&
                            document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount').value = totalDDAmt;
                                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = parseFloat(txtPaidAmt.value.trim()) - parseFloat(totalDDAmt);
                            }
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = '';
                }
            }
        }
        function conver_uppercase(text) {
            text.value = text.value.toUpperCase();
        }

        //    function LoadImage() {

        //        document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").src = document.getElementById("ctl00_ContentPlaceHolder1_fuPhotoUpload").value;
        //        document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").height = '96px';
        //        document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").width = '96px';
        //    }

        //    function LoadImageSign() {
        //        document.getElementById("ctl00_ContentPlaceHolder1_ImgSign").src = document.getElementById("ctl00_ContentPlaceHolder1_fuSignUpload").value;
        //        document.getElementById("ctl00_ContentPlaceHolder1_ImgSign").height = '96px';
        //        document.getElementById("ctl00_ContentPlaceHolder1_ImgSign").width = '96px';
        //    }
    </script>

    <script type="text/javascript">
        function Validate() {
            var mobile = document.getElementById("mobile").value;
            var pattern = /^\d{10}$/;
            if (pattern.test(mobile)) {
                alert("Your mobile number : " + mobile);
                return true;
            }
            alert("It is not valid mobile number.input 10 digits number!");
            return false;
        }
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return false;
            }
        }
        function validateNumericAndNotZero(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return false;
            }
            if (txt.value == '0') {
                txt.value = '';
                alert('Please Enter Values Greater Than Zero!');
                txt.focus();
                return false;
            }

        }
        function Check(txt) {
            debugger;
            var a = txt.value;
            var l = a.length;
            if (l < 10) {
                alert('Please enter ');
            }
        }
    </script>

    <script type="text/javascript">
        $(function () {
            var minDate = new Date('1/1/1990');
            var todaysDate = new Date();
            var maxDate = new Date(todaysDate.getFullYear(),
                               todaysDate.getMonth(),
                               todaysDate.getDate() - 1);
            var currentsYear = todaysDate.getFullYear();


            var range = '1900:' + currentsYear
            $('#txtDateOfBirth').datepicker({
                minDate: minDate,
                maxDate: maxDate,
                changeMonth: true,
                changeYear: true,
                yearRange: range
            });
        });
    </script>


    <%--<script>
        $("input:file").change(function () {
            var fileName = $(this).val();

            newText = fileName.replace(/fakepath/g, '');
            var newtext1 = newText.replace(/C:/, '');
            //newtext2 = newtext1.replace('//', ''); 
            var result = newtext1.substring(2, newtext1.length);


            if (result.length > 0) {
                $(this).parent().children('span').html(result);
            }
            else {
                $(this).parent().children('span').html("Choose file");
            }
        });
        //file input preview
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $("input:file").change(function () {
            readURL(this);
        });
    </script>--%>
    <div id="divMsg" runat="server">
    </div>

    <script>

        function Close() {
            //$("#Details_Veiw").Show();
            //alert("Close");
            $("#myModal2").modal('hide');
        }
    </script>


    <script>
        function confirmhostel() {


            if (Page_ClientValidate())
                if (confirm("The Seat capacity in the selected Program is complete, if you still wish to admit the candidate, please confirm.")) {

                    // $('#divhosteltype').css("display", "block");
                    __doPostBack("savedata");

                    //  $('#divhottrsansport').css("display", "block");
                    //window.location.href = "https://jecrc.mastersofterp.in/";
                }
                else {



                }
        }



        

        //function confirmmsg() {
        //    if (confirm("If You Select For Hostel Then Your Payable Fees Will be Modified. If You Wish to Continue for Hostel Click 'Ok' Else Click 'Cancel'")) {

        //        __doPostBack("savedata");
        //       // $('#divhosteltype').css("display", "block");
        //        //window.location.href = "https://jecrc.mastersofterp.in/";
        //    }
        //    else {

                
        //    }
        //}

             //function Confirm() {
             //    var confirm_value = document.createElement("INPUT");
             //    confirm_value.type = "hidden";
             //    confirm_value.name = "confirm_value";
             //    if (confirm("Do you want to save data?")) {
             //        confirm_value.value = "Yes";
             //    } else {
             //        confirm_value.value = "No";
             //    }
             //    document.forms[0].appendChild(confirm_value);
             //}

    </script>

    <%-- <script type="text/javascript">
        function SelectDate(e) {
              alert("a");
            var chkdate = document.getElementById("txtduedate2").value;
            var edate = chkdate.split("/");
            var spdate = new Date();
            var sdd = spdate.getDate();
            var smm = spdate.getMonth();
            var syyyy = spdate.getFullYear();
            var today = new Date(syyyy, smm, sdd).getTime();
            var e_date = new Date(edate[2], edate[1] - 1, edate[0]).getTime();
            if (e_date < today) {
                alert("You Cannot Select Past Date");
                document.getElementById('txtduedate2').value = " ";
                return false;
            }

        }
    </script>
    <script>

        //$(function pastdate() {

        function pastdate() {
            // $(function () {
            var today = new Date();
            var month = ('0' + (today.getMonth() + 1)).slice(-2);
            var day = ('0' + today.getDate()).slice(-2);
            var year = today.getFullYear();
            var date = year + '-' + month + '-' + day;
            $('[id*=txtduedate2]').attr('min', date);
            // });
        }
        </script>--%>
    <%-- <script>
        $(function () {
        var dateToday = new Date();
        $(function () {
            $("#divduedate2").datepicker({
                minDate: dateToday
            });
        });
        });
    </script>--%>
   <%--<script>
       function OpenConfirmDialog() {
           if (confirm('The Seat capacity in the selected Program is complete, if you still wish to admit the candidate, please confirm.')) {
               //True .. do something
               return true;
           }
           else {
               return false;
               //False .. do something
           }
       }

</script>--%>
</asp:Content>
