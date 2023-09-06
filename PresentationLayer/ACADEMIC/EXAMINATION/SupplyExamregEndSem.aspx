<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SupplyExamregEndSem.aspx.cs"
    Inherits="ACADEMIC_EXAMINATION_SupplyExamregEndSem" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDetails"
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


    <asp:UpdatePanel ID="updDetails" runat="server">
        <ContentTemplate>

            <asp:Panel ID="pnlStart" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">SUPPLEMENTARY EXAM REGISTRATION </h3>
                            </div>

                            <div class="box-body">
                                <div id="divNote" runat="server" visible="true" style="border: 2px solid #C0C0C0; background-color: #FFFFCC; padding: 20px; color: #990000;">
                                    <b>Note : </b>Steps To Follow For Supplementary Exam Registration.
                                        <div style="padding-left: 20px; padding-right: 20px">
                                            <p style="padding-top: 5px; padding-bottom: 5px;">
                                                1. Please click Proceed to Exam Registration button.
                                            </p>
                                            <p style="padding-top: 5px; padding-bottom: 5px;">
                                                2. Supplementary Exam Registration will proceed after selecting the checkbox for the particular courses.
                                            </p>
                                            <p style="padding-top: 5px; padding-bottom: 5px;">
                                                3. The Number of courses should not exceed 3. 
                                            </p>

                                            <p style="padding-top: 5px; padding-bottom: 5px;">
                                                4. The Cumulative credits for the selected courses should not exceed 12.
                                            </p>
                                            <p style="padding-top: 5px; padding-bottom: 5px;">
                                                5. Then click on Continue to Proceed button and finally click on Click To Pay button.
                                            </p>
                                            <p style="padding-top: 5px; padding-bottom: 5px;">
                                                6. You Will Get the Exam Registration Receipt After Successful Online Payment.
                                            </p>
                                            <p style="padding-top: 5px; padding-bottom: 5px;">
                                                7.You are permitted to apply <u>one time</u> only. 
                                            </p>
                                            <p style="padding-top: 5px; padding-bottom: 5px; text-align: center;">
                                                <asp:Button ID="btnProceed" runat="server" Text="Proceed to Exam Registration" OnClick="btnProceed_Click" CssClass="btn btn-primary" />
                                            </p>
                                        </div>
                                </div>

                                <asp:Panel ID="pnlSearch" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="div_college" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>College</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AppendDataBoundItems="true" Font-Bold="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                    Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="search"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div_enrollno" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Session</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" Font-Bold="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="search"></asp:RequiredFieldValidator>

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div_regno" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Univ. Reg. No. </label>
                                                </div>
                                                <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEnrollno"
                                                    Display="None" ErrorMessage="Please Enter Registration No." ValidationGroup="search"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-12 btn-footer" id="div_btn" runat="server">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnProceed_Click" ValidationGroup="search"
                                                    Text="Show" />
                                                <asp:Button ID="btnCancel" runat="server"
                                                    OnClick="btnCancel_Click" Text="Clear" CssClass="btn btn-warning" CausesValidation="false" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                    ValidationGroup="search" ShowSummary="false" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div id="divCourses" runat="server" visible="false">
                                    <div id="tblInfo" runat="server">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Student Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblName" runat="server" Font-Bold="True" /><br />
                                                                <%--<asp:Label ID="lblFatherName" runat="server" Font-Bold="True" /><br />
                                                                 <asp:Label ID="lblMotherName" runat="server" Font-Bold="True" />--%>
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item"><b>Father Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" />
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item"><b>Mother Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblMotherName" runat="server" Font-Bold="True" />
                                                            </a>
                                                        </li>
                                                        <%-- <li class="list-group-item"><b></b>
                                                            <a class="sub-label">
                                                                
                                                                <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" />
                                                            </a>
                                                        </li>--%>
                                                        <li class="list-group-item"><b>College :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblCollege" runat="server" Font-Bold="True"></asp:Label>
                                                                <asp:Label ID="lblprintfor" runat="server" Visible="false" Text="aayushi" />
                                                                <asp:HiddenField ID="hdnCollege" Value="" runat="server" />
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item"><b>Session :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblsession" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>

                                                    </ul>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Enrollment No. :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label>
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item"><b>Admission Batch :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Current Semester :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item"><b>Degree / Branch :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item" runat="server" id="lsScheme"><b>Scheme :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label>
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item d-none"><b>Photo :</b>
                                                            <a class="sub-label">
                                                                <asp:Image ID="imgPhoto" runat="server" Width="40%" Height="70%" Visible="false" /></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divArrearSem" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Arrear Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBackLogSem" runat="server" AutoPostBack="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBackLogSem_SelectedIndexChanged"
                                                        AppendDataBoundItems="True" ValidationGroup="backsem">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlBackLogSem" runat="server" ControlToValidate="ddlBackLogSem"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="backsem"></asp:RequiredFieldValidator>
                                                    <asp:HiddenField ID="hdfCategory" runat="server" />
                                                    <asp:HiddenField ID="hdfDegreeno" runat="server" />
                                                    <asp:HiddenField ID="hdfcurrcredits" runat="server" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Total Amount To Pay</label>
                                                    </div>
                                                    <asp:TextBox ID="totamtpay" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    <asp:Label ID="lblOrderID" runat="server" CssClass="data_label" Visible="false">0</asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <%--   <div class="orm-group col-lg-3 col-md-6 col-12">
                                            <fieldset class="fieldset" style="padding: 8%; color: Green">
                                                <legend class="legend">Note :</legend>
                                                <span style="font-weight: bold; text-align: center; color: red;"><u>Supplymentary Registration Fees per Course</u>:</span><asp:Label ID="lblstuendth" Visible="false" runat="server" Style="font-weight: bold; text-align: center; color: black;"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Font-Bold="true" />
                                            </fieldset>
                                        </div>--%>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnReport1" runat="server"
                                                OnClick="btnReport_Click" Text="Print Reciept" ValidationGroup="backsem" Visible="false" CssClass="btn btn-warning" Font-Bold="true" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="backsem"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>

                                        <%--                                        <div class="row" runat="server" id="credits" visible="false">
                                            <div class="col-md-12">
                                                <div class="container-fluid">
                                                    <span style="font-weight: bold; text-align: center; color: green;"><u>Credit's Registered</u>:</span><asp:Label ID="lblcred" Text="0" runat="server" Style="font-weight: bold; text-align: center; color: black;"></asp:Label>
                                                    <span style="font-weight: bold; text-align: center; color: green;" visible="false"><u>Fees</u>:</span><asp:Label ID="lblFee" Text="0" runat="server" Style="font-weight: bold; text-align: center; color: black; display: none"></asp:Label>
                                                </div>
                                            </div>
                                        </div>--%>

                                        <div id="trFailList" runat="server">
                                            <div class="col-12">
                                                <asp:ListView ID="lvFailCourse" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Fail Course List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblFailCourse">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>
                                                                        <asp:CheckBox ID="cbHead" runat="server" Visible="false" ToolTip="Select/Select all" onclick="SelectAll(this,'chkAccept','lblcredits')" />
                                                                    </th>
                                                                    <th>Course Code
                                                                    </th>
                                                                    <th>Course Name
                                                                    </th>
                                                                    <th>Semester
                                                                    </th>
                                                                    <th>Subject Type
                                                                    </th>
                                                                    <th>Credits
                                                                    </th>
                                                                    <th>Fees
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr id="trCurRow">
                                                            <td>
                                                                <asp:CheckBox ID="chkAccept" runat="server" onclick="exefunction(this,'hdncurcredits')" AutoPostBack="true" CausesValidation="false" ToolTip='<%# Eval("COURSENO")%>' />
                                                                <%--<asp:CheckBox ID="chkAccept" runat="server" onclick="exefunction(this,'hdncurcredits')" ToolTip='<%# Eval("COURSENO")%>' OnCheckedChanged="chkAccept_CheckedChanged1" />--%>

                                                                <asp:HiddenField ID="hdncurcredits" runat="server" Value='<%# (Eval("CREDITS").ToString())==""?"0":Eval("CREDITS") %>' />
                                                                <asp:HiddenField ID="hdfee" runat="server" Value='<%# (Eval("FEE").ToString())==""?"0":Eval("FEE") %>' />

                                                                <input type="hidden" value='<%# (Eval("CUMMULATIVE_CREDITS").ToString())==""?"0":Eval("CUMMULATIVE_CREDITS") %>' id='hdnCm' class='h_v'>
                                                                <input type="hidden" value='<%# (Eval("IDTYPE").ToString())==""?"0":Eval("IDTYPE") %>' id='hdnIdType' class='h_v'>
                                                                <input type="hidden" value='<%# (Eval("CREDITS").ToString())==""?"0":Eval("CREDITS") %>' id="hdnCourseCredits" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("SUBID").ToString()%>' />
                                                            </td>
                                                            <td>
                                                                <%--<%# Eval("SEMESTER") %>--%>
                                                                <asp:Label ID="lblSemesterno" runat="server" Text='<%# Eval("SEMESTERNO") %>' ToolTip='<%# Eval("SEMESTERNO")%>' Visible="false" />
                                                                <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                                            </td>

                                                            <td style="font-weight: bold" align="center">
                                                                <%# (Eval("SUBID").ToString()) == "1" ? "Theory" : ((Eval("SUBID").ToString()) == "3" ? "Theory cum Practical" : "Practical")%>                                                              
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblcredit" runat="server" Text='<%# Eval("CREDITS") %>' ToolTip='<%# Eval("CREDITS")%>' Visible="false" />
                                                                <asp:Label ID="lblcredits" runat="server" Text='<%# (Eval("CREDITS").ToString())==""?"0":Eval("CREDITS") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblfee" runat="server" Text='<%# Eval("FEE") %>' ToolTip='<%# Eval("FEE")%>' Visible="false" />
                                                                <asp:Label ID="lblAmt" runat="server" Text='<%# (Eval("FEE").ToString())==""?"0":Eval("FEE") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>

                                                </asp:ListView>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="trNote" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <div class="container-fluid">
                                            <span style="font-weight: bold; color: green;">Note:- 1.Supplementary Exam Registration will proceed after selecting the checkbox for the particular courses.<br />
                                            </span>
                                            <span style="font-weight: bold; padding-left: 4%; color: green;">2.The Number of courses should not exceed 3. 
                                                        <br />
                                            </span>
                                            <span style="font-weight: bold; padding-left: 4%; color: green;">3.The Cumulative credits for the selected courses should not exceed 12.<br />
                                            </span>
                                            <span style="font-weight: bold; padding-left: 4%; color: green;">4.You Will Get the Exam Registration Receipt After Successful Online Payment.<br />
                                            </span>
                                            <span style="font-weight: bold; padding-left: 4%; color: green;">5.You are permitted to apply <u>one time</u> only.</span>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Continue To Proceed" Font-Bold="true" Visible="false"
                                        CssClass="btn btn-primary" />

                                    <asp:Button ID="BtnOnlinePay" runat="server" Visible="false" Text="Click To Pay" Font-Bold="true"
                                        CssClass="btn btn-primary" OnClick="BtnOnlinePay_Click" />

                                    <asp:Button ID="BtnPrntChalan" runat="server" Visible="false" Text="Print Chalan" Font-Bold="true"
                                        CssClass="btn btn-primary" OnClick="BtnPrntChalan_Click" />

                                    <asp:Button ID="btnReport" runat="server"
                                        OnClick="btnReport_Click" Text="Print Reciept" ValidationGroup="backsem" CssClass="btn btn-info" Font-Bold="true" Visible="false" />
                                    <asp:Button ID="btnRemoveList" runat="server" OnClick="btnRemoveList_Click" Text="Clear List" Font-Bold="true" Visible="false"
                                        CssClass="btn btn-warning" />

                                    <div style="display: none;">
                                        <asp:Button ID="btnPrcdToPay" runat="server" Text="Proceed To Pay" Visible="false" Font-Bold="true"
                                            CssClass="btn btn-primary" />
                                    </div>
                                </div>
                                <asp:RadioButtonList ID="radiolist" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" Visible="false" OnSelectedIndexChanged="radiolist_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Enabled="false">Online Pay (ICICI Payment Gateway)&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">Pay Through Chalan</asp:ListItem>
                                </asp:RadioButtonList>

                                <div class="col-12 btn-footer">
                                    <%-- <asp:Button ID="BtnPrntChalan" runat="server" Visible="false" Text="Print Chalan" Font-Bold="true"
                                        CssClass="btn btn-primary" OnClick="BtnPrntChalan_Click" />--%>
                                    <%--  <asp:Button ID="BtnOnlinePay" runat="server" Visible="false" Text="Click To Pay" Font-Bold="true"
                                        CssClass="btn btn-primary" OnClick="BtnOnlinePay_Click" />--%>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                <div id="divMsg" runat="server">
                </div>

                <script type="text/javascript" language="javascript">
                    function SelectAll(headerid, chk, lbl) {
                        debugger;
                        var tbl = '';
                        var list = '';
                        var hdfcred = 0;

                        tbl = document.getElementById('tblFailCourse');
                        list = 'lvFailCourse';
                        if (headerid.checked) {

                            document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = 0;
                            hdfcred = document.getElementById('<%= hdfcurrcredits.ClientID %>').value;
                        }
                        else {
                            hdfcred = document.getElementById('<%= hdfcurrcredits.ClientID %>').value;
                        }
                        try {
                            var dataRows = tbl.getElementsByTagName('tr');
                            if (dataRows != null) {
                                for (i = 0; i < dataRows.length - 1; i++) {
                                    var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk;
                                    var credist = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + lbl;
                                    //ctl00_ContentPlaceHolder1_lvFailCourse_ctrl0_lblAmt
                                    if (headerid.checked) {
                                        document.getElementById(chkid).checked = true;
                                        document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lblstuendth").innerHTML);
                                        hdfcred = Number(hdfcred) + Number(document.getElementById(credist).innerHTML);
                                        alert(hdfcred);

                                    }
                                    else {
                                        document.getElementById(chkid).checked = false;
                                        document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) - Number(document.getElementById("ctl00_ContentPlaceHolder1_lblstuendth").innerHTML);
                                        hdfcred = Number(hdfcred) - Number(document.getElementById(credist).innerHTML);
                                        alert(hdfcred);
                                    }

                                }
                            }
                        }
                        catch (e) {
                            alert(e);
                        }
                    }

                    function CheckSelectionCount(chk) {
                        debugger;
                        var count = -1;
                        var frm = document.forms[0]
                        for (i = 0; i < document.forms[0].elements.length; i++) {
                            console.log(count)
                            var e = frm.elements[i];
                            if (count > 3) {
                                // console.log(e,'sdfghjkl');
                                //this.checked = false;
                                //this.checked == false;
                                alert("You have reached maximum limit!");
                                //chk.checked = false;
                                e.checked = false;
                                //e.disabled = true;
                                //chk.disabled = true;
                                return false;
                            }
                            else if (count <= 3) {

                                if (e.checked == true) {
                                    count += 1;
                                }
                            }
                            else {
                                return;
                            }
                        }
                    }


                    function ValidateCourse(chk) {
                        debugger;
                        alert('course validation')
                        var count = 0;
                        // var frm = document.forms[0]
                        var dataRows = document.getElementsByTagName('tr');

                        if (dataRows != null) {

                            for (i = 0; i < dataRows.length - 1; i++) {

                                var cbrow = document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_chkAccept");
                                if (document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_chkAccept").checked == true && !cbrow.disabled) {

                                    if (count == 3) {
                                        alert("You have reached maximum limit!");
                                        cbrow.checked = false;
                                        return;
                                    }
                                    else if (count < 4) {

                                        if (cbrow.checked == true) {
                                            count += 1;
                                        }
                                    }
                                    //else {
                                    //    return;
                                    //}
                                }
                            }
                        }
                    }
                </script>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnOnlinePay" />
        </Triggers>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>


    <script>

        function exefunction(chkAccept, lbl) {
            //var CheckBoxValue = $(this).is(":checked");
            //alert(CheckBoxValue)
            //var total = document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value
            //if (CheckBoxValue == true)
            //{
            //    var Apllicable = $("[id*=hdfee]", td).val();
            //    alert(Apllicable)
            //}

            debugger;

            var totcredtis = 0;
            var fee = 0;
            var feesamt = 0;
            var credit = 0;
            var credits = 0;
            var tempamt = 0
            var count = 0;
            list = 'lvFailCourse';


            var dataRows = document.getElementsByTagName('tr');

            var totCredit = 0;
            var CntCumuCred = $('#hdnCm').val();
            var idtype = $('#hdnIdType').val();
            var currentCred = $('#hdnCourseCredits').val();

            var degreeno = document.getElementById('ctl00_ContentPlaceHolder1_lblBranch').title;

            if (dataRows != null) {
                debugger;


                for (i = 0; i < dataRows.length - 1; i++) {

                    if (document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value == 'NaN' || document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value == '') {

                        fee = 0;
                    }
                    else {
                        document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = fee;
                    }

                    var cbrow = document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_chkAccept");


                    if (document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_chkAccept").checked == true && !cbrow.disabled) {

                        count++;
                        fee += parseInt(document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_hdfee").value);

                        document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = fee;
                        if (count == 4) {
                            alert("You have reached maximum limit!");
                            document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_chkAccept").checked = false;
                            return;
                        }

                        totCredit = parseFloat(CntCumuCred) + parseFloat(currentCred);


                        //if (degreeno == 1) {
                        //    if ((parseFloat(totCredit) >= 60 && idtype == 1) || (parseFloat(totCredit) >= 40 && idtype == 2)) {
                        //        alert("You have reached maximum Credits limit!");
                        //        document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_chkAccept").checked = false;
                        //        return;
                        //    }
                        //}



                        //document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lblstuendth").innerHTML);
                        //document.getElementById('<%= hdfcurrcredits.ClientID %>').value = Number(document.getElementById('<%= hdfcurrcredits.ClientID %>').value) + Number(document.getElementById(credist).value);
                        //document.getElementById('ctl00_ContentPlaceHolder1_lblcred').innerText = document.getElementById('<%= hdfcurrcredits.ClientID %>').value;
                        //if (document.getElementById('<%= hdfcurrcredits.ClientID %>').value > 16) {
                        //alert("Cannot exceed 16 Credit's !");
                        //chks1Thoery.checked = false;
                        //document.getElementById('<%= hdfcurrcredits.ClientID %>').value = Number(document.getElementById('<%= hdfcurrcredits.ClientID %>').value) - Number(document.getElementById(credist).value);
                        //document.getElementById('<%= hdfcurrcredits.ClientID %>').value = Number(document.getElementById('<%= hdfcurrcredits.ClientID %>').value) - Number(document.getElementById(credist).value);
                        // document.getElementById('ctl00_ContentPlaceHolder1_lblcred').innerText = document.getElementById('<%= hdfcurrcredits.ClientID %>').value;

                    }
                    //else {

                    //tempamt = document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value;

                    //fee = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_hdfee").value);

                    //tempamt = tempamt - fee;

                    ////feesamt = feesamt - fee;

                    //document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = tempamt;

                    //document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) - Number(document.getElementById("ctl00_ContentPlaceHolder1_lblstuendth").innerHTML);
                    //document.getElementById('<%= hdfcurrcredits.ClientID %>').value = Number(document.getElementById('<%= hdfcurrcredits.ClientID %>').value) - Number(document.getElementById(credist).value);
                    // document.getElementById('ctl00_ContentPlaceHolder1_lblcred').innerText = document.getElementById('<%= hdfcurrcredits.ClientID %>').value;
                    // break;
                    //}
                }
            }
            //alert('hii')
            //__doPostBack("chkAccept_CheckedChanged1");
            // ValidateCourse(this);
            //ValidateCourse(this)
        }
        function exefunction2(chks6practical) {
            debugger;
            var dataRows = document.getElementsByTagName('tr');
            if (dataRows != null) {
                for (i = 0; i < dataRows.length - 1; i++) {

                    if (chks6practical.checked) {
                        document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lblstuendpr").innerHTML);
                        break;
                    }

                    else {
                        document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) - Number(document.getElementById("ctl00_ContentPlaceHolder1_lblstuendpr").innerHTML);
                        break;
                    }
                }
            }
        }
    </script>

</asp:Content>


