<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OnlineAdmUserCreate.aspx.cs" Inherits="ACADEMIC_OnlineAdmUserCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../jquery/jquery-1.10.2.min.js"></script>
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updtime"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updtime" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>Generate Online Admission Credential</b></h3>
                            <div class="box-tools pull-right">
                                <div style="color: Red; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>
                        <div class="box-body">


                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Student Full Name </label>
                                <asp:TextBox ID="txtStudName" runat="server" TabIndex="1" ToolTip="Duration" onkeypress="return alphaOnly(event);"
                                    onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" CssClass="form-control" placeholder="Please Enter Student Name" />
                                <asp:RequiredFieldValidator ID="rfvDuration" runat="server" ControlToValidate="txtStudName"
                                    Display="None" ErrorMessage="Please Enter Student Name" ValidationGroup="submit"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                       <ajaxtoolkit:filteredtextboxextender id="FilteredTextBoxExtender3" runat="server"
                                           targetcontrolid="txtStudName" filtertype="Custom" filtermode="InvalidChars"
                                           invalidchars="~`!@#$%*()_+=,./:;<>?'{}[]\|-&&;'^" />

                            </div>

                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Student Mobile No. </label>
                                <asp:TextBox ID="txtMobileNo" runat="server" TabIndex="2" MaxLength="10" ToolTip="Duration" onchange="CheckAvailabilityMobile(this);" onkeyup="validateNumeric(this);" CssClass="form-control" placeholder="Please Enter Mobile No" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMobileNo"
                                    Display="None" ErrorMessage="Please Enter Mobile No" ValidationGroup="submit"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                               <%-- <asp:RegularExpressionValidator runat="server" Visible="false" ErrorMessage="Mobile No. is Invalid" ID="revMobile" ControlToValidate="txtMobileNo"
                                                    ValidationExpression="^[\s\S]{8,}$" Display="Dynamic" ValidationGroup="academic"></asp:RegularExpressionValidator>--%>
                                <span id="message1" style="font-size: small;"></span>
                                          
                            </div>

                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Email Id </label>
                                <asp:TextBox ID="txtEmailId" runat="server" TabIndex="3" ToolTip="Duration" CssClass="form-control" onchange="CheckAvailability(this);" placeholder="Please Enter Email Id" />
                                  <asp:RegularExpressionValidator ID="rfvStudentEmail" runat="server" ControlToValidate="txtEmailId"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ErrorMessage="Please Enter Valid Student Email Id" ValidationGroup="Academic">
                                                </asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmailId"
                                    Display="None" ErrorMessage="Please Enter Email Id" ValidationGroup="submit"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                               <span id="message" style="font-size: small;"></span>
                                          
                            </div>

                            <div class="form-group col-md-4">
                                <label><span style="color: red">* </span>State</label>
                                <asp:DropDownList ID="ddlState" runat="server" TabIndex="4" AutoPostBack="true"
                                    AppendDataBoundItems="True" ToolTip="Please Select State" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlState"
                                    Display="None" ErrorMessage="Please Select State" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-md-4">
                                <label><span style="color: red">* </span>City</label>
                                <asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="5"
                                    AutoPostBack="True" ToolTip="Please Select City">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvColg" runat="server" ControlToValidate="ddlCity"
                                    Display="None" ErrorMessage="Please Select City" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label><span style="color: red">* </span>Admission Type</label>
                                <asp:DropDownList ID="ddlAdmissionType" runat="server" AppendDataBoundItems="True" TabIndex="6" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlAdmissionType_SelectedIndexChanged" ToolTip="Please Select Admission Type" AutoPostBack="True">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlAdmissionType"
                                    Display="None" ErrorMessage="Please Select Admission Type" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label><span style="color: red">* </span>Program</label>
                                <asp:DropDownList ID="ddlProgram" runat="server" AppendDataBoundItems="True" TabIndex="7" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" ToolTip="Please Select Program" AutoPostBack="True">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlProgram"
                                    Display="None" ErrorMessage="Please Select Program" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                            </div>




                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="8" CssClass="btn btn-info"
                                    OnClick="btnSubmit_Click" ValidationGroup="submit" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="9"
                                    CssClass="btn btn-danger" />
                            </p>


                            <p>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="submit" DisplayMode="List" />
                            </p>


                             <div class="col-md-12">
                               
                                <asp:Panel ID="pnlStudents" runat="server">
                                    <asp:ListView ID="lvStudent" runat="server">
                                      
                                        <LayoutTemplate>
                                            <div>
                                                <h4>Student List </h4>
                                                <table id="tblHead" class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="bg-light-blue" id="trRow">
                                                         
                                                            <th>Application Id
                                                            </th>
                                                        
                                                            <th>Student Name
                                                            </th>
                                                             <th>Student Email
                                                            </th>
                                                             <th>Student Mobile
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
                                            <tr>
                                             
                                                <td>
                                                    <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("USERNAME") %>' ToolTip='<%# Eval("USERNAME") %>' />
                                                </td>
                                                 <td>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("FIRSTNAME") %>' ToolTip='<%# Eval("FIRSTNAME") %>' />
                                                </td>
                                                 <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("EMAILID") %>' ToolTip='<%# Eval("EMAILID") %>' />
                                                </td>
                                           
                                                <td>
                                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("MOBILENO") %>' ToolTip='<%# Eval("MOBILENO") %>' />
                                                
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    
                                    </asp:ListView>

                                </asp:Panel>
                            <div id="divMsg" runat="server">
                           
                        </div>
                    </div>
                </div>
            </div>
             <script type="text/javascript">

                 function alphaOnly(e) {
                     var code;
                     if (!e) var e = window.event;

                     if (e.keyCode) code = e.keyCode;
                     else if (e.which) code = e.which;

                     if ((code >= 48) && (code <= 57)) {
                         alert('Only Alphabets Allowed!');
                         return false;
                     }
                     return true;
                 }
    </script>
    <script type="text/javascript" lang="javascript">
        function EmailValidate() {
            debugger;
            var numericExpression = /^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$/;
            //  ^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$
            var elem = $("#txtEmailId").val();
            if (elem.match(numericExpression))
                return true;
            else
                alert("Invalid Email Id");
            return false;

        }


        function conver_uppercase(text) {
            text.value = text.value.toUpperCase();
        }

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {

            $('#txtEmailId').change(function () {

                var txtLEmail = $.trim($('#txtEmailId').val());
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                //alert(first_name);
                if (txtLEmail.length == 0) {
                    $('#txtEmailId').addClass('box-color-blank');
                    alert('Email ID is mandatory');
                }
                else {
                    if (reg.test(txtLEmail) == false) {
                        alert('Invalid Email ID');

                    }
                    $('#txtEmailId').removeClass('box-color-blank');
                }
            });
        });
    </script>

     <script type="text/javascript" lang="javascript">
         function CheckAvailability(txt) {
             var username = txt.value;
             PageMethods.CheckEmail(username, function (response) {
                 var message = document.getElementById("message");
                 if (response) {
                     //Username available.
                     //ClearMessage();
                     document.getElementById("message").innerHTML = "";

                 }
                 else {

                     //Username not available.
                     //message.style.color = "red";
                     //message.innerHTML = "Email Id is Already Exits";
                     alert('Email Id is Already Exits');
                     txt.value = "";
                 }
             });
         };

         function ClearMessage() {
             document.getElementById("message").innerHTML = "";
         };
    </script>

    <script type="text/javascript" lang="javascript">
        function CheckAvailabilityMobile(txt) {
            
            var mobile = txt.value;
           
            PageMethods.CheckMobile(mobile, function (response) {
                var message = document.getElementById("message1");
                if (response) {
                    //Username available.
                    // ClearMessage();
                    document.getElementById("message1").innerHTML = "";
                }
                else {

                    //Username not available.
                    //message.style.color = "red";
                    //message.innerHTML = "Mobile No is Already Exits";
                    alert('Mobile No is Already Exits');
                    txt.value = "";
                }
            });
        };

        function ClearMessage() {
            document.getElementById("message1").innerHTML = "";
        };
    </script>
        </ContentTemplate>

    </asp:UpdatePanel>
   


</asp:Content>

