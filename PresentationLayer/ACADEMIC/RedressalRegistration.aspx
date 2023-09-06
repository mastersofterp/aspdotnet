<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RedressalRegistration.aspx.cs" Inherits="ACADEMIC_RedressalRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
      
        function exefunction(chkRedressal) {
            debugger;
            if (document.getElementById("ctl00_ContentPlaceHolder1_ddlrevalseeing").value == "1") {
                var dataRows = document.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        if (chkRedressal.checked) {
                            document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lblRedressal").innerHTML);
                            break;
                        }
                        else {
                            document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) - Number(document.getElementById("ctl00_ContentPlaceHolder1_lblRedressal").innerHTML);
                            break;
                        }
                    }
                }
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_ddlrevalseeing").value == "2")
            {
                var dataRows = document.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        if (chkRedressal.checked) {
                            document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lblpaper").innerHTML);
                            break;
                        }
                        else {
                            document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) - Number(document.getElementById("ctl00_ContentPlaceHolder1_lblpaper").innerHTML);
                            break;
                        }
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
                    <h3 class="box-title"> REDRESSAL/PAPER SEEING REGISTRATION</h3>
                    <div class="box-tools pull-right">
                    </div>
                </div>

                <div class="box-body">
                    <div id="divNote" runat="server" visible="true" style="border: 2px solid #C0C0C0; background-color: #FFFFCC; padding: 20px; color: #990000;">
            <b>Note : </b>Steps To Follow For  Redressal/PaperSeeing Registration.
            <div style="padding-left: 20px; padding-right: 20px">
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    1. Please click Proceed to  Registration button.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    2. Please select the one semester .
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    3.After Selecting Semester ,Click on Show Button then the  Courses will be display on the below for selected semester.
                </p>
               
                <p style="padding-top: 5px; padding-bottom: 5px;">
                      4. Then click on Continue To Pay button and select Pay Through Chalan option To Pay.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    5. Finally click on Click TO Pay button.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    5. Your  Registration is Considered  after payment only.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    6. You will get your Payment Receipt After Completion of your Payment .
                </p>
                 <p style="padding-top: 5px; padding-bottom: 5px;">
                    7. You are only able to pay only FIRST TIME from here.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px; text-align: center;">
                    <asp:Button ID="btnProceed" runat="server" Text="Proceed to Registration" OnClick="btnProceed_Click" CssClass="btn btn-info" TabIndex="1"/>
                </p>

            </div>
        </div>
                
                    <asp:Panel ID="pnlSearch" runat="server">
            <div class="row">
                <div class="col-md-12">
                    
                         <div class="form-group col-md-2"></div>
                    <div class="form-group col-md-4" id="div_enrollno" runat="server">
                        <span style="color:red">*</span><label>Session :</label>
                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="true" Font-Bold="true" >
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                            
                        </asp:DropDownList>&nbsp;&nbsp;
                             <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                 Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="search"></asp:RequiredFieldValidator>
                       
                    </div>
                    <div class="form-group col-md-4" >
                        <label> USN No :</label>
                        <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
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
                        
                 <div class="col-md-3" style="margin-top:20px;display:none">
                    <asp:Image ID="imgPhoto" runat="server" Width="40%" Height="70%" Visible="false"/>
                </div>
          
        </asp:Panel>
                   
              </div>
                    <br />
                   <hr />
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
                                      <div class="col-md-3">
                                          <label><span style="color: red;">*</span> Apply For :</label>
                                          <asp:DropDownList ID="ddlrevalseeing" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlrevalseeing_SelectedIndexChanged" AutoPostBack="True"
                                AppendDataBoundItems="True"
                                ValidationGroup="backsem" TabIndex="3">
                                              <asp:ListItem value="0">Please Select</asp:ListItem>
                                               <asp:ListItem value="1">Redressal</asp:ListItem>
                                               <asp:ListItem value="2">Paper Seeing</asp:ListItem>
                            </asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="rfvreval" runat="server" ControlToValidate="ddlrevalseeing"
                                Display="None" InitialValue="0" ErrorMessage="Please Select Redressal/Paper Seeing" ValidationGroup="backsem"></asp:RequiredFieldValidator>
                                      </div>
                 <div class="form-group col-md-3">
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
                 <div class="form-group col-md-3">
                     Total Amount To Pay :
                         <asp:TextBox ID="totamtpay" runat="server" Enabled="false" Width="60%" CssClass="form-control"></asp:TextBox>
                       <asp:Label ID="lblOrderID" runat="server" CssClass="data_label" Visible="false">0</asp:Label>
                </div>
                  <div class="form-group col-md-3" style="margin-left:-30px;width:300px">                                   
                         <fieldset class="fieldset" style="padding: 8%; color: Green;">
                            <legend class="legend">Note :</legend>
                            <span style="font-weight: bold;text-align:center;  color: red;"><u>REDRESSAL/PaperSeeing FEES</u><br /></span>
                               <span style="font-weight: bold; color: green;">Amount for Redressal Registration Per Course -  <div class="fa fa-inr text-brown"></div>
                                   <asp:Label ID="lblRedressal" Visible="false" runat="server" style="font-weight: bold;text-align:center;  color: black;"></asp:Label> 
                                   <br /></span>  
                              <span style="font-weight: bold; color: green;">Amount for PaperSeeing Registration Per Course -  <div class="fa fa-inr text-brown"></div>
                                   <asp:Label ID="lblpaper" Visible="false" runat="server" style="font-weight: bold;text-align:center;  color: black;"></asp:Label> 
                                   <br /></span>                                                                                  
                        </fieldset>
                    </div>

                              <div class=" col-md-12 text-center">
                                    <asp:Button ID="btnPrintReport" runat="server" OnClick="btnPrintReport_Click" Text="Print Reciept" CssClass="btn btn-warning" Font-Bold="true" Visible="false" TabIndex="10"  />
                                  <%--<asp:Button ID="btnshow" runat="server"  Text="Show" CssClass="btn btn btn-primary" OnClick="btnshow_Click" Visible="false"  />
                                  <asp:Button ID="btncancel1" runat="server" Text="Cancel" CssClass="btn btn btn-info" OnClick="btncancel1_Click" Visible="false" />--%>
                                   <asp:Label ID="lblStatus" runat="server" Visible="false" CssClass="data_label"></asp:Label><p></p>
                                   <asp:Button ID="btnreprint" runat="server" Text="Re-print Challan" CssClass="btn btn btn-info" OnClick="reprint_Click" Visible="false" TabIndex="11" />

                              </div>


                                      <div class="col-md-12">
                                          <div class="container-fluid">
                                              <asp:ListView ID="lvCurrentSubjects" runat="server"  Visible="false">
                                                  <LayoutTemplate>
                                                      <div class="vista-grid">
                                                          <div class="titlebar">
                                                              <h4>Course List for Redressal/PaperSeeing</h4>
                                                          </div>
                                                          <table id="tblCurrentSubjects" class="table table-hover table-bordered table-responsive">
                                                              <thead>
                                                                  <tr class="bg-light-blue">
                                                                      <th>Sl.NO
                                                                      </th>
                                                                      <th>Select
                                                                      </th>
                                                                      <th>Subject Code
                                                                      </th>
                                                                      <th>Subject Title
                                                                      </th>
                                                                      <th>
                                                                          Grade Obtained
                                                                      </th>
                                                                      

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
                                                           <asp:CheckBox ID="chkRedressal" runat="server" onclick="exefunction(this)" TabIndex="3"  />
                                                    </td>
                                                          <td>
                                                              <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                          </td>
                                                          <td>
                                                              <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>'  />
                                                               <asp:HiddenField ID="hdnextmark" runat="server" Value='<%# Eval("EXTERMARK") %>' />
                                                              <asp:HiddenField ID="hdnschemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                              
                                                          </td>
                                                          <td>
                                                              <%#Eval("GRADE") %>
                                                          </td>
                                                        
                                                   
                                                   

                                                      </tr>
                                                  </ItemTemplate>
                                              </asp:ListView>

                                          </div>
                                      </div>
                 

                                      <div class="row" id="trNote" runat="server" visible="false">
                <div class="col-md-12">
                    <div class="container-fluid">
                     <span style="font-weight: bold; color: green;">Note:- 1.Appeal Redressal/Paper Seeing Registration will proceed after checking the chekbox for the particular course component.<br />  </span>
                     
                         <span style="font-weight: bold;padding-left:4% ;color: green;">2.In Payment Through Chalan please Reconcile Your Chalan From Account Section.<br />  </span>
                        <span style="font-weight: bold;padding-left:4% ;color: green;">3.You Will Get Your Payment Receipt After Successful Reconcile Of Payment.<br />  </span>
                        <span style="font-weight: bold;padding-left:4% ;color: green;">4.You are only able to pay only <u>FIRST TIME</u> from here. </span>
                        </div>
                </div>
            </div>          
                                      <p></p>                                              
            <div class="row text-center">
                  <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Save & Continue" Font-Bold="true" Visible="false" TabIndex="4"
                                CssClass="btn btn-info" />               
                            &nbsp;<asp:Button ID="btnRemoveList" runat="server" OnClick="btnRemoveList_Click" Text="Clear List" Font-Bold="true" Visible="false" TabIndex="5"
                                CssClass="btn btn-danger" /> 
                    

            </div>
                <br />
                 <div class="row text-center">
                     <asp:Button ID="btnPrcdToPay" runat="server"  Text="Proceed To Pay" Font-Bold="true" TabIndex="6"
                                CssClass="btn btn-primary" />

                     </div>
                 <br />
                <div  style="padding-left:32%">
                <asp:radiobuttonlist id="radiolist" runat="server"  RepeatDirection="Horizontal" AutoPostBack="True" Visible="false" OnSelectedIndexChanged="radiolist_SelectedIndexChanged" TabIndex="7" >
                      <asp:listitem value=1 Enabled="false">Online Pay (ICICI Payment Gateway)&nbsp;&nbsp;&nbsp;</asp:listitem> 
                      <asp:listitem value=2 Selected="true">Pay Through Chalan</asp:listitem>
                       </asp:radiobuttonlist> 
                    </div>
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