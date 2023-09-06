<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RecheckingandReassesment.aspx.cs" Inherits="ACADEMIC_RecheckingandReassesment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div style="z-index: 1; position: absolute; top: 40%; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
               <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
                 
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function exefunction(chkrechecking) {
            var dataRows = document.getElementsByTagName('tr');
            if (dataRows != null) {
                for (i = 0; i < dataRows.length - 1; i++) {
                    if (chkrechecking.checked) {
                        document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lblstureath").innerHTML);
                        var row = chkrechecking.parentNode.parentNode;
                        var rowIndex = row.rowIndex - 1;
                        if (row.cells[4].getElementsByTagName("input")[0].checked) {
                            row.cells[4].getElementsByTagName("input")[0].checked = false;
                            document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) - Number(document.getElementById("ctl00_ContentPlaceHolder1_lblstureath").innerHTML);
                        }
                        break;
                    }
                    else {
                        document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) - Number(document.getElementById("ctl00_ContentPlaceHolder1_lblstureath").innerHTML);

                        break;
                    }
                }
            }
        }

        function exefunction2(chkreassesment) {
            var dataRows = document.getElementsByTagName('tr');
            if (dataRows != null) {
                for (i = 0; i < dataRows.length - 1; i++) {
                    if (chkreassesment.checked) {
                        document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lblsturecth").innerHTML);
                        var row = chkreassesment.parentNode.parentNode;
                        var rowIndex = row.rowIndex - 1;
                        if (row.cells[3].getElementsByTagName("input")[0].checked) {
                            row.cells[3].getElementsByTagName("input")[0].checked = false;
                            document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) - Number(document.getElementById("ctl00_ContentPlaceHolder1_lblsturecth").innerHTML);
                        }
                        break;
                    }
                    else {
                        document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) - Number(document.getElementById("ctl00_ContentPlaceHolder1_lblsturecth").innerHTML);
                        break;
                    }
                }
            }
        }


    </script>
     <asp:UpdatePanel ID="updDetails" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlstart" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"> RECHECKING REVALUATION REGISTRATION</h3>
                    <div class="box-tools pull-right">
                    </div>
                </div>
                 <div style="color: Red; font-weight: bold;float:right">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
               
                <div class="box-body">
                    <div id="divNote" runat="server" visible="false" style="border: 2px solid #C0C0C0; background-color: #FFFFCC; padding: 20px; color: #990000;">
            <b>Note : </b>Steps To Follow For  Rechecking & Reassesment Registration.
            <div style="padding-left: 20px; padding-right: 20px">
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    1. Please click Proceed to  Registration button.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    2. Please select the one semester .
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    3.After Selecting Semester ,Click on Show Button then the theory Courses will be display on the below for selected semester.
                </p>
               
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    4. Then click on Continue To Proceed button and select one option To Pay.(If you want to register for more subjects after pressing Continue To Proceed Botton please press <u>Back</u> button).<br />
                    (1) Online Pay (ICICI Payment Gateway).<br />
                    (2) Pay Through Chalan.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    5. Finally click on Click To Pay button.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    5. Your  Registration is Considered  after payment only.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    6. You will get your Payment Receipt After Completion of your Payment .
                </p>
                 <p style="padding-top: 5px; padding-bottom: 5px;">
                    7. You are only able to pay once for the parcticular semester .
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px; text-align: center;">
                    <asp:Button ID="btnProceed" runat="server" Text="Proceed to Registration" OnClick="btnProceed_Click" CssClass="btn btn-info" TabIndex="1"/>
                </p>

            </div>
        </div>
                
                    <asp:Panel ID="pnlSearch" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-12">
                        <div class="col-md-12">
                         <%--<div class="form-group col-md-2"></div>--%>
                    <div class="form-group col-md-6" id="div_enrollno" runat="server">
                        <span style="color:red">*</span><label>Session :</label>
                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="true" Font-Bold="true" >
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;
                             <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                 Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="search"></asp:RequiredFieldValidator>
                       
                    </div>
                    <div class="form-group col-md-6" >
                        <span style="color:red">*</span><label> Enrollment No :</label>
                        <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEnrollno"
                                 Display="None" ErrorMessage="Please Enter Enrollnment No." ValidationGroup="search"></asp:RequiredFieldValidator>
                    </div>
                    <%--<div class="form-group col-md-2"></div>--%>
                       </div>
                        <div class="row text-center" id="div_btn" runat="server">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="search"
                            Text="Show" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server"
                            OnClick="btnCancel_Click" Text="Clear" CssClass="btn btn-danger" CausesValidation="false" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                            ValidationGroup="search" ShowSummary="false" />
                        </div>
                    </div>
                    <div class="col-md-1">

                    </div>                 
                   
                </div>
                 <div class="col-md-3" style="margin-top:20px;display:none">
                    <asp:Image ID="imgPhoto" runat="server" Width="40%" Height="70%" Visible="false"/>
                </div>
            </div>
        </asp:Panel>
                    <br />
                    <div class="row" id="divCourses" runat="server" visible="false">
                         <div class="col-md-12" id="tblInfo" runat="server">

                                  <div class="col-md-12">
               
                 <%-- <h4>Backlog Exam Registration</h4>
                <hr />--%>
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
                     <asp:Label ID="lblCollege" runat="server" Font-Bold="True" ></asp:Label>
                      <asp:HiddenField ID="hdnCollege" Value="" runat="server" />
                </div>
                 <div class="form-group col-md-4">
                     Enrollment No. :
                      <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label>
                </div>
                 <div class="form-group col-md-4">
                     Session : <asp:Label ID="lblsession" runat="server" Font-Bold="True"></asp:Label>
                </div>
                 <div class="form-group col-md-4">
                     Admission Batch :<asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label>
                </div>
                 <div class="form-group col-md-4">
                     Current Semester : <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                </div>
                 <div class="form-group col-md-4">
                     Degree / Branch : <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                </div>
                 <div class="form-group col-md-4">
                     Scheme :<asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label>
                </div>
                 <div class="form-group col-md-4">
                     <label><span style="color: red;">*</span> Applying For Semester :</label>
                      <asp:DropDownList ID="ddlRevalRegSemester" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlRevalRegSemester_SelectedIndexChanged" 
                                AppendDataBoundItems="True"
                                ValidationGroup="backsem" TabIndex="2">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlBackLogSem" runat="server" ControlToValidate="ddlRevalRegSemester"
                                Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="backsem"></asp:RequiredFieldValidator>

                            <asp:HiddenField ID="hdfCategory" runat="server" />

                            <asp:HiddenField ID="hdfDegreeno" runat="server" />
                </div>
                 <div class="form-group col-md-4">
                     <%--Total Amount To Pay :--%>
                         <asp:TextBox ID="totamtpay" runat="server" Enabled="false" Width="40%" CssClass="form-control" Visible="false"></asp:TextBox>
                       <asp:Label ID="lblOrderID" runat="server" CssClass="data_label" Visible="false">0</asp:Label>
                </div>
                  <div class="form-group col-md-4" runat="server" visible="false">                                   
                         <fieldset class="fieldset" style="padding: 8%; color: Green">
                            <legend class="legend">Note :</legend>
                            <span style="font-weight: bold;text-align:center;  color: red;"><u>REASSESMENT/RECHECKING FEES</u><br /></span>
                               <span style="font-weight: bold; color: green;">Amount for REASSESMENT -  <div class="fa fa-inr text-brown"></div><asp:Label ID="lblsturecth" Visible="false" runat="server" style="font-weight: bold;text-align:center;  color: black;"></asp:Label> 
                                   <br /></span>
                              <span style="font-weight: bold; color: green;">Amount for RECHECKING - <div class="fa fa-inr text-brown"></div><asp:Label ID="lblstureath" Visible="false" runat="server" style="font-weight: bold;text-align:center;  color:black;"></asp:Label>                                  
                                  <br /></span>                                                      
                        </fieldset>
                    </div>

                              <div class=" col-md-12 text-center">
                                    <asp:Button ID="btnPrintReport" runat="server" OnClick="btnPrintReport_Click" Text="Print Reciept" CssClass="btn btn-warning" Font-Bold="true" Visible="false" TabIndex="10"  />
                                  <%--<asp:Button ID="btnshow" runat="server"  Text="Show" CssClass="btn btn btn-primary" OnClick="btnshow_Click" Visible="false"  />
                                  <asp:Button ID="btncancel1" runat="server" Text="Cancel" CssClass="btn btn btn-info" OnClick="btncancel1_Click" Visible="false" />--%>
                                   <asp:Label ID="lblStatus" runat="server"  style="font-size:15px" Visible="false" CssClass="data_label"></asp:Label><p></p>
                                    <asp:Label ID="lblstatusnew" style="font-size:15px" runat="server" Visible="false" CssClass="data_label"></asp:Label><p></p>
                                   <asp:Button ID="btnreprint" runat="server" Text="Re-print Challan" CssClass="btn btn btn-info" OnClick="reprint_Click" Visible="false" TabIndex="11" />
                                   <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn btn btn-info" OnClick="btnback_Click" Visible="false" TabIndex="11" />

                              </div>


                                      <div class="col-md-12">
                                          <div class="container-fluid">
                                              <asp:ListView ID="lvCurrentSubjects" runat="server"  Visible="false">
                                                  <LayoutTemplate>
                                                      <div class="vista-grid">
                                                          <div class="titlebar">
                                                              <h4>Course List for Rechecking Registration</h4>
                                                          </div>
                                                          <table id="tblCurrentSubjects" class="table table-hover table-bordered table-responsive">
                                                              <thead>
                                                                  <tr class="bg-light-blue">
                                                                      <th>SR.NO
                                                                      </th>
                                                                      <th>Subject Code
                                                                      </th>
                                                                      <th>Subject Name
                                                                      </th>
                                                                      <th>Rechecking
                                                                      </th>
                                                                     <%-- <th>Reassesment
                                                                      </th>--%>

                                                                  </tr>
                                                                  <tr id="itemPlaceholder" runat="server" />
                                                              </thead>
                                                          </table>
                                                      </div>
                                                  </LayoutTemplate>
                                                  <ItemTemplate>
                                                      <tr id="trCurRow" class="item">

                                                          <td><%# Container.DataItemIndex + 1%></td>
                                                          <td>
                                                              <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                          </td>
                                                          <td>
                                                              <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>'  />
                                                               <asp:HiddenField ID="hdnextmark" runat="server" Value='<%# Eval("EXTERMARK") %>' />
                                                              <asp:HiddenField ID="hdnschemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                              
                                                          </td>
                                                         <td>
                                                           <asp:CheckBox ID="chkrechecking" runat="server" TabIndex="3" Checked='<%#Eval("REV_APPROVE_STAT").ToString() == "1" ? true : false%>'/>   <%--onclick="exefunction(this)" --%>
                                                    </td>
                                                   
                                                   <%-- <td>
                                                        <asp:CheckBox ID="chkreassesment" runat="server" onclick="exefunction2(this)" TabIndex="4"  />
                                                    </td>--%>

                                                      </tr>
                                                  </ItemTemplate>
                                              </asp:ListView>

                                          </div>
                                      </div>
                 

                                      <div class="row" id="trNote" runat="server" visible="false">
                <div class="col-md-12" runat="server" visible="false">
                    <div class="container-fluid">
                     <span style="font-weight: bold; color: green;">Note:- 1.Rechecking/Reassesment Registration will proceed after checking the chekbox for the particular course component.<br />  </span>
                      <span style="font-weight: bold;padding-left:4% ;color: green;">2.You can only apply for one course Rechecking or Reassesment but not Both <br /> </span>
                        <span style="font-weight: bold;padding-left:4% ;color: green;">6.If you want to register for more subjects after pressing <u>Continue To Proceed</u> Botton please press <u>Back</u> button<br />
                            </span>
                        <span style="font-weight: bold;padding-left:4% ;color: green;">4.If Reassesment CheckBox is Disabled You have more than 3 BacKlogs in a one Semester You are not Eligible To apply for Reassesment<br /> </span>
                         <span style="font-weight: bold;padding-left:4% ;color: green;">5.In Payment Through Chalan please Reconcile Your Chalan From Account Section.<br />  </span>
                        <span style="font-weight: bold;padding-left:4% ;color: green;">6.You Will Get Your Payment Receipt After Successful Reconcile Of Payment.<br />  </span>
                        <span style="font-weight: bold;padding-left:4% ;color: green;">7.You are only able to pay<u> ONCE</u> for the parcticular semester .</span>
                        </div>
                </div>
            </div>          
                                      <p></p>                                              
            <div class="row text-center">
                  <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Continue To Proceed" Font-Bold="true" Visible="false" TabIndex="4"
                                CssClass="btn btn-info" />               
                            &nbsp;<asp:Button ID="btnRemoveList" runat="server" OnClick="btnRemoveList_Click" Text="Clear List" Font-Bold="true" Visible="false" TabIndex="5"
                                CssClass="btn btn-danger" /> 
                    

            </div>
                <br />
                 <div class="row text-center">
                     <%--<asp:Button ID="btnPrcdToPay" runat="server"  Text="Proceed To Pay" Font-Bold="true" TabIndex="6"
                                CssClass="btn btn-primary" />--%>

                     </div>
                 <br />
               <%-- <div  style="padding-left:32%">
                <asp:radiobuttonlist id="radiolist" runat="server"  RepeatDirection="Horizontal" AutoPostBack="True" Visible="false" OnSelectedIndexChanged="radiolist_SelectedIndexChanged" TabIndex="7" >
                      <asp:listitem value=1 Enabled="false">Online Pay &nbsp;&nbsp;&nbsp;</asp:listitem> 
                      <asp:listitem value=2>Pay Through Chalan</asp:listitem>
                       </asp:radiobuttonlist> 
                    </div>--%>
                 <div class="row text-center">
                     <asp:Button ID="BtnPrntChalan" runat="server" Visible="false" Text="Print Chalan" Font-Bold="true" TabIndex="8"
                                CssClass="btn btn-primary" OnClick="BtnPrntChalan_Click" />
                      <asp:Button ID="BtnOnlinePay" runat="server"  Visible="false" Text="Click To Pay" Font-Bold="true" OnClientClick="return confirm('Do you Really want to Confirm/Submit this Courses for Rechecking and Reassesment Registration?')"
                                CssClass="btn btn-primary"  OnClick="BtnOnlinePay_Click" TabIndex="9"/>

                     </div>
                      
                
                </div>

                              </div>
                        </div>                
                </div>
            </div>
        </div>
    </div>
                </asp:Panel>
            </ContentTemplate>
         </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

