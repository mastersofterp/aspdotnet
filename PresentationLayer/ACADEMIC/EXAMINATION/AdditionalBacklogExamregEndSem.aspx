<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdditionalBacklogExamregEndSem.aspx.cs" Inherits="ACADEMIC_EXAMINATION_AdditionalBacklogExamregEndSem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 40%; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
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
                                <h3 class="box-title">ADDITIONAL BACKLOG EXAM REGISTRATION </h3>
                            </div>

                            <div class="box-body">
                                <div id="divNote" runat="server" visible="true" style="border: 2px solid #C0C0C0; background-color: #FFFFCC; padding: 20px; color: #990000;">
                                    <b>Note : </b>Steps To Follow For Backlog Exam Registration.
                                        <div style="padding-left: 20px; padding-right: 20px">
                                            <p style="padding-top: 5px; padding-bottom: 5px;">
                                                1. Please click Proceed to Exam Registration button.
                                            </p>
                                            <p style="padding-top: 5px; padding-bottom: 5px;display:none">
                                                2. Please select the semester From Backlog Semester List.
                                            </p>
                                            <p style="padding-top: 5px; padding-bottom: 5px;display:none">
                                                3.After Selecting Backlog Semester ,the  Backlog  Courses will be display on the below for selected semester.
                                            </p>

                                            <p style="padding-top: 5px; padding-bottom: 5px;display:none">
                                                4. Then click on Continue To Pay button and select Pay Through Chalan option To Pay.
                                            </p>
                                            <p style="padding-top: 5px; padding-bottom: 5px;display:none">
                                                5. Finally click on CLick TO Pay button.
                                            </p>
                                            <p style="padding-top: 5px; padding-bottom: 5px;display:none">
                                                6. You will get your Payment Receipt After Successfully Submittion of Payment.
                                            </p>
                                            <p style="padding-top: 5px; padding-bottom: 5px;display:none">
                                                7. You are only able to pay only <u>FIRST TIME</u> from here.
                                            </p>
                                            <p style="padding-top: 5px; padding-bottom: 5px; text-align: center;">
                                                <asp:Button ID="btnProceed" runat="server" Text="Proceed to Exam Registration" OnClick="btnProceed_Click" CssClass="btn btn-primary" />
                                            </p>
                                        </div>
                                </div>

                                <!-- Start Search Panel-->
                                <br />
                                <asp:Panel ID="pnlSearch" runat="server">
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="col-md-12">
                                                <div class="form-group col-md-2"></div>

                                                <div class="form-group col-md-4" id="div_enrollno" runat="server">
                                                    <span style="color: red">*</span><label>Session :</label>
                                                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="true" 
                                                        Font-Bold="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>&nbsp;&nbsp;
                                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="search">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <label>USN No :</label>
                                                    <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-md-2">
                                                </div>

                                            </div>
                                            <div class="row text-center" id="div_btn" runat="server">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnProceed_Click" ValidationGroup="search"
                                                    Text="Show" />
                                                &nbsp;<asp:Button ID="btnCancel" runat="server"
                                                    OnClick="btnCancel_Click" Text="Clear" CssClass="btn btn-danger" CausesValidation="false" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                    ValidationGroup="search" ShowSummary="false" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <!-- End Search Panel-->
                                <div class="form-group col-md-12" runat="server" id="divRegMsg" visible="false">
                                    <marquee direction="left">
                                     <span style="color:red"> 
                                         <p>Additional Backlog Exam Registration is done for Selected Session</p>
                                     </span> 
                                    </marquee>
                                </div>
                                <br />
                                <hr />
                                <div class="row" id="divCourses" runat="server" visible="false">
                                    <div class="col-md-12" id="tblInfo" runat="server">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group col-md-4">
                                                    Student Name :
                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" /><br />
                                                    <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" />
                                                </div>

                                                <div class="form-group col-md-4">
                                                    College :
                                                    <asp:Label ID="lblCollege" runat="server" Font-Bold="True"></asp:Label>
                                                    <asp:Label ID="lblprintfor" runat="server" Visible="false" />
                                                    <asp:HiddenField ID="hdnCollege" Value="" runat="server" />
                                                </div>

                                                <div class="form-group col-md-4">
                                                    Enrollment No. :
                                                    <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    Session :
                                                    <asp:Label ID="lblsession" runat="server" Font-Bold="True"></asp:Label>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    Admission Batch :<asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    Current Semester :
                                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    Degree / Branch :
                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    Scheme :<asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label>
                                                </div>

                                                <div class="form-group col-md-4" style="display:none">
                                                    Backlog Semester :
                                                    <asp:DropDownList ID="ddlBackLogSem" runat="server" AutoPostBack="True" CssClass="form-control" >
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlBackLogSem" runat="server" ControlToValidate="ddlBackLogSem"
                                                        Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="backsem"></asp:RequiredFieldValidator>

                                                    <asp:HiddenField ID="hdfCategory" runat="server" />

                                                    <asp:HiddenField ID="hdfDegreeno" runat="server" />
                                                    <asp:HiddenField ID="hdfcurrcredits" runat="server" />
                                                </div>

                                                <div class="form-group col-md-4" style="display: none">
                                                    Total Amount To Pay :
                                                    <asp:TextBox ID="totamtpay" runat="server" Enabled="false" Width="40%" CssClass="form-control"></asp:TextBox>
                                                    <asp:Label ID="lblOrderID" runat="server" CssClass="data_label" Visible="false">0</asp:Label>
                                                </div>

                                                <div class="col-md-4" style="display: none">
                                                    <fieldset class="fieldset" style="padding: 8%; color: Green">
                                                        <legend class="legend">Note :</legend>
                                                        <span style="font-weight: bold; text-align: center; color: red;"><u>Backlog Registration Fees per Course</u>:</span>
                                                        <asp:Label ID="lblstuendth" Visible="false" runat="server" Style="font-weight: bold; text-align: center; color: black;"></asp:Label>

                                                        <asp:Label ID="Label3" runat="server" Font-Bold="true" />
                                                    </fieldset>
                                                </div>

                                                <div class="col-md-8">
                                                    <div class="container-fluid">
                                                        <span style="font-weight: bold; text-align: left; color: green;"><u>Additional Backlog Total Credit's Limits </u>:</span><asp:Label ID="lblTotalCredits" Text="12.00" runat="server" Style="font-weight: bold; text-align: center; color: black;"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <div class="container-fluid">
                                                        <span style="font-weight: bold; text-align: left; color: green;"><u>Additional Backlog Registrer Credit's </u>:</span><asp:Label ID="lblcred" runat="server" Style="font-weight: bold; text-align: center; color: black;"></asp:Label>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-md-3" style="margin-top: 20px; display: none">
                                                <asp:Image ID="imgPhoto" runat="server" Width="40%" Height="70%" Visible="false" />
                                            </div>

                                            

                                        </div>

                                        <div class="row text-center">
                                            <asp:Button ID="btnReport1" runat="server"
                                                OnClick="btnReport_Click" Text="Print Reciept" ValidationGroup="backsem" CssClass="btn btn-warning" Font-Bold="true" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="backsem"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>

                                        <div class="row" runat="server" id="credits">
                                            <div class="col-md-12">
                                            
                                                
                                            
                                               

                                            </div>
                                        </div>

                                        <div class="row" id="trFailList" runat="server">
                                            <div class="col-md-12">
                                                <div class="container-fluid">
                                                    <asp:ListView ID="lvFailCourse" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    <h4>Fail Course List</h4>
                                                                </div>
                                                                <table id="tblFailCourse" class="table table-hover table-bordered table-responsive">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>
                                                                                Action
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
                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </thead>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr id="trCurRow" class="item">
                                                                <td>
                                                                    <asp:CheckBox ID="chkAccept" runat="server" onclick="exefunction(this,'hdncurcredits')" Checked='<%# (Eval("ACCEPTED").ToString())=="1"? true:false %>'  /><%-- onclick="exefunction(this,'hdncurcredits')"--%>
                                                                    <asp:HiddenField ID="hdncurcredits" runat="server" Value='<%# (Eval("CREDITS").ToString())==""?"0":Eval("CREDITS") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SEMESTER") %>
                                                                </td>

                                                                <td style="font-weight: bold" align="center">
                                                                   <%# Eval("SUBNAME") %>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblcredits" runat="server" Text='<%# (Eval("CREDITS").ToString())==""?"0":Eval("CREDITS") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" id="trNote" runat="server" visible="false">
                                            <div class="col-md-12">
                                                <div class="container-fluid">
                                                    <span style="font-weight: bold; color: green;">Note:-&nbsp 1.Backlog Exam Registration will proceed after checking the chekbox for the particular course.<br />
                                                    </span>
                                                    <span style="font-weight: bold; padding-left: 4%; color: green;">&nbsp 2.The Subjects taken shall not exceed more than 2 including the current semester.
                                                        <br />
                                                    </span>
                                                   <%-- <span style="font-weight: bold; padding-left: 4%; color: green;">3.In Payment Through Chalan please Reconcile Your Chalan From Account Section.<br />
                                                    </span>
                                                    <span style="font-weight: bold; padding-left: 4%; color: green;">4.You Will Get The Exam Registration Receipt After Successfully Reconcilation of Payment.<br />
                                                    </span>
                                                    <span style="font-weight: bold; padding-left: 4%; color: green;">5.You are only able to pay only <u>FIRST TIME</u> from here. </span>--%>
                                                </div>
                                            </div>
                                        </div>

                                        &nbsp;
                                        <div class="row text-center">
                                            <asp:Button ID="btnSubmit" runat="server"  OnClientClick="return showConfirm()" OnClick="btnSubmit_Click" Text="Submit" Font-Bold="true" Visible="false"
                                                CssClass="btn btn-primary" />
                                            &nbsp;<asp:Button ID="btnReport" runat="server"
                                                OnClick="btnReport_Click" Text="Print Reciept" ValidationGroup="backsem" CssClass="btn btn-warning" Font-Bold="true" Visible="false" />
                                            &nbsp;<asp:Button ID="btnRemoveList" runat="server" OnClick="btnRemoveList_Click" Text="Clear List" Font-Bold="true" Visible="false"
                                                CssClass="btn btn-danger" />
                                        </div>

                                        <br />
                                        <div class="row text-center" style="display: none">
                                            <asp:Button ID="btnPrcdToPay" runat="server" Text="Proceed To Pay" Font-Bold="true"
                                                CssClass="btn btn-primary" />

                                        </div>
                                        <br />
                                        <div style="padding-left: 32%;display:none">
                                            <asp:RadioButtonList ID="radiolist" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" Visible="false" OnSelectedIndexChanged="radiolist_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Enabled="false">Online Pay (ICICI Payment Gateway)&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2" Selected="True">Pay Through Chalan</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="row text-center" style="display:none">
                                            <asp:Button ID="BtnPrntChalan" runat="server" Visible="false" Text="Print Chalan" Font-Bold="true"
                                                CssClass="btn btn-primary" OnClick="BtnPrntChalan_Click" />
                                            <asp:Button ID="BtnOnlinePay" runat="server" Visible="false" Text="Click To Pay" Font-Bold="true"
                                                CssClass="btn btn-primary" OnClick="BtnOnlinePay_Click" />

                                        </div>

                                    </div>

                                </div>
                            </div> <!--End Box Body--> 

                        </div>

                    </div>
                    <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                    <div id="divMsg" runat="server">
                    </div>

                    <script>
                        function exefunction(chk, lbl) {
                            debugger;
                            list = 'lvFailCourse';
                            var dataRows = document.getElementsByTagName('tr');
                            if (dataRows != null) {
                                for (i = 0; i < dataRows.length - 1; i++) {
                                    var str = chk.id;
                                    var credist = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + str.charAt(43) + '_' + lbl;


                                    if (chk.checked) {

                                        document.getElementById('<%= hdfcurrcredits.ClientID %>').value = Number(document.getElementById('<%= hdfcurrcredits.ClientID %>').value) + Number(document.getElementById(credist).value);
                                        document.getElementById('ctl00_ContentPlaceHolder1_lblcred').innerText = Number(document.getElementById('<%= hdfcurrcredits.ClientID %>').value).toFixed(2);
                                       
                                        if (document.getElementById('<%= hdfcurrcredits.ClientID %>').value > 12) {
                                            alert("You can not register More than 12.00 in current semester !");
                                            document.getElementById('<%= hdfcurrcredits.ClientID %>').value = Number(document.getElementById('<%= hdfcurrcredits.ClientID %>').value) - Number(document.getElementById(credist).value);
                                            document.getElementById('ctl00_ContentPlaceHolder1_lblcred').innerText = Number(document.getElementById('<%= hdfcurrcredits.ClientID %>').value).toFixed(2);
                                            chk.checked = false;
                                   }
                                break;
                            }
                            else {
                                        document.getElementById('<%= hdfcurrcredits.ClientID %>').value = Number(document.getElementById('<%= hdfcurrcredits.ClientID %>').value) - Number(document.getElementById(credist).value);
                                        document.getElementById('ctl00_ContentPlaceHolder1_lblcred').innerText =Number(document.getElementById('<%= hdfcurrcredits.ClientID %>').value).toFixed(2);
                                        break;
                                    }
                                }
                            }
                        }


                        function showConfirm() {
                            var ret = confirm('Do you really want to submit this courses for Course Registration?');
                            if (ret == true)
                                return true;
                            else
                                return false;
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
</asp:Content>


