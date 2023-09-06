<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SendCouncelingDetail_Via_MailSms.aspx.cs" Inherits="ACADEMIC_SendCouncelingDetail_Via_MailSms" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 40%; left: 50%;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updCollege"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>In-Progress</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>
    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }

        // javascript function for showing charector Limit

        function LimtCharacters(txtMsg, CharLength, indicator) {
            
            chars = txtMsg.value.length;
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (chars > CharLength) {
                txtMsg.value = txtMsg.value.substring(0, CharLength);
            }
        }
    </script>
    <script src="../../Content/jquery.js" type="text/javascript"></script>

    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <%-- <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <asp:UpdatePanel ID="updCollege" runat="server">

        <ContentTemplate>
     
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><b>SEND BULK COUNCELING MAIL/SMS</b></h3>
                                <div class="pull-right">
                                    <div style="color: Red; font-weight: bold">
                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                    </div>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="row" id="trSubject" runat="server">
                                        <div class="form-group col-md-2">
                                            <label><span style="color: red;">*</span>  Enter Text Subject </label>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <asp:TextBox ID="txtSubject" runat="server" ToolTip="Please enter the message"
                                                onkeyup="LimtCharacters(this,50,'lblcount');" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-2">
                                            <label><span style="color: red;">*</span> Enter Text Message </label>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" ToolTip="Please enter the message"
                                                Height="90px" onkeyup="LimtCharacters(this,160,'lblcount');" MaxLength="160"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMessage"
                                                ErrorMessage="Please Enter the Massage !" Display="None" SetFocusOnError="true"
                                                ValidationGroup="SendEmail"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Number of Characters Left:</label>
                                            <label id="lblcount" style="background-color: #E2EEF1; color: Red; font-weight: bold;">
                                                160</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-2">
                                            <label><span style="color: red;">*</span> Message Type </label>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <asp:RadioButtonList ID="rdbMessage" runat="server"
                                                RepeatDirection="Horizontal" AutoPostBack="true"
                                                OnSelectedIndexChanged="rdbMessage_SelectedIndexChanged">
                                                <asp:ListItem Value="SMS" Selected="True">SMS </asp:ListItem>
                                                <asp:ListItem Value="Mail">Mail </asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rdbMessage"
                                                ErrorMessage="Please Select Message Type !" Display="None" SetFocusOnError="true"
                                                ValidationGroup="SendEmail"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <p class="text-center">
                                            <asp:Button ID="btnSndSms" runat="server" TabIndex="12" Text="Send Message" ValidationGroup="SendEmail"
                                                ToolTip="Send SMS" CssClass="btn btn-primary" OnClick="btnSndSms_Click" />
                                            &nbsp;
                                                        <asp:Button ID="btnCancel" runat="server" TabIndex="12" Text="Cancel" ToolTip="Clear All"
                                                            CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="SendEmail" />
                                        </p>
                                    </div>
                                
                                </div>
                            </div>  
                                   
                            <div class="box-body">
                                    <div class="box-header with-border">
                                            <h3 class="box-title"><b> Student Information </b></h3>
                                     </div>
                                        <div class="col-md-12">
                                          <%-- <div class="row"  runat="server" visible="false">
                                               <td class="form_left_text">Select User Type :
                                                   <asp:DropDownList ID="ddlUserType" runat="server" TabIndex="2" AppendDataBoundItems="true"
                                                       ValidationGroup="ShowEmployee" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged"
                                                       AutoPostBack="true">
                                                       <asp:ListItem>Please Select</asp:ListItem>
                                                       <asp:ListItem></asp:ListItem>
                                                   </asp:DropDownList>
                                                   <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlUserType"
                                                       ErrorMessage="Select atleast one Department !" Display="None" InitialValue="0"
                                                       ValidationGroup="ShowEmployee"></asp:RequiredFieldValidator>
                                               </td>
                                           </div>--%>
                                           <div class="row" id="trStudent_search" runat="server" style=" padding-top:20px">
                                               <div class="form-group col-md-2">
                                               <label><span style="color: red;">*</span> Admission Batch : </label>
                                               </div>
                                               <div class="form-group col-md-4">
                                                   <asp:DropDownList ID="ddlBatch" runat="server" TabIndex="2" AppendDataBoundItems="true">
                                                       <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                   </asp:DropDownList>
                                               </div>
                                        
                                           </div>
                                               <div class="form-group col-md-10">
                                                    <asp:RadioButtonList ID="rdbshow" runat="server" RepeatDirection="Horizontal" TabIndex="2"
                                                        AutoPostBack="true" RepeatColumns="2" OnSelectedIndexChanged="rdbshow_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">&nbsp;&nbsp;Application Fees Paid&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        
                                                        <asp:ListItem Value="2">&nbsp;&nbsp;Student Details Completed&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="3">&nbsp;&nbsp;Student Details Not Completed&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="4">&nbsp;&nbsp;Provisional Fees Paid</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfvCriteria" runat="server" ControlToValidate="rdbshow" Display="None"
                                                        ErrorMessage="Please Select Criteria" ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>
                                             <div class="col-md-12" >
                                                        <asp:ListView ID="lvStudent" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="listViewGrid" >
                                                                   <h4>
                                                                        Select Student
                                                                    </h4>
                                                                    <table class="table table-bordered">
                                                                        <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>Select
                                                                                <asp:CheckBox ID="chkheader" runat="server" onclick="return totAll(this);" />
                                                                            </th>
                                                                            <th>Application ID
                                                                            </th>
                                                                            <th>Student Name
                                                                            </th>
                                                                            <th>Mobile No.
                                                                            </th>
                                                                            <th>Email ID.
                                                                            </th>
                                                                            <th>Fees Paid
                                                                            </th>
                                                                        </tr>
                                                                        </thead>
                                                                         <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                         </tbody>
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <EmptyDataTemplate>
                                                            </EmptyDataTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                    <td>
                                                                        <%-- <asp:CheckBox ID="chkSelect" runat="server" ToolTip="Select to view tabulation chart" />--%>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("USERNO") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblusername" runat="server" Text='<%# Eval("USERNAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblFname" runat="server" Text='<%# Eval("FIRSTNAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("MOBILENO")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblfee_status" runat="server" Text='<%# (Eval("FEE_STATUS").ToString() == "True") ? "Yes" : "" %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                    <td>
                                                                        <%-- <asp:CheckBox ID="chkSelect" runat="server" ToolTip="Select to view tabulation chart" />--%>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("USERNO") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblusername" runat="server" Text='<%# Eval("USERNAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblFname" runat="server" Text='<%# Eval("FIRSTNAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("MOBILENO")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblfee_status" runat="server" Text='<%# (Eval("FEE_STATUS").ToString() == "True") ? "Yes" : "" %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </AlternatingItemTemplate>
                                                        </asp:ListView>
                                                  
                                                </div>
                                          <%--   <div class="row" id="trEmployee"  runat="server">
                                                    <td align="center" colspan="5">
                                                        <asp:ListView ID="lvEmployee" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="demp_grid" class="vista-grid">
                                                                    <div class="titlebar">
                                                                        Employee List
                                                                    </div>
                                                                    <table class="datatable" cellpadding="0" cellspacing="0">
                                                                        <tr class="header">
                                                                            <th>Select
                                                                                <asp:CheckBox ID="chkheader" runat="server" onclick="return totAll(this);" />
                                                                            </th>
                                                                            <th>User Name
                                                                            </th>
                                                                            <th>Mobile No.
                                                                            </th>
                                                                            <th>Email ID.
                                                                            </th>
                                                                            <th>User Type.
                                                                            </th>
                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <EmptyDataTemplate>
                                                            </EmptyDataTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                    <td>
                                                                 
                                                                        <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("UA_NO") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblFname" runat="server" Text='<%# Eval("UA_FULLNAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("UA_MOBILE")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("UA_EMAIL") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblUserDesc" runat="server" Text=' <%# Eval("USERDESC")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                    <td>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("UA_NO") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblFname" runat="server" Text='<%# Eval("UA_FULLNAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("UA_MOBILE")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("UA_EMAIL") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblUserDesc" runat="server" Text=' <%# Eval("USERDESC")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </AlternatingItemTemplate>
                                                        </asp:ListView>
                                                    </td>
                                             </div>--%>
                                       </div>
                             </div>
                          
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSndSms" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>


    </asp:UpdatePanel>
</asp:Content>

