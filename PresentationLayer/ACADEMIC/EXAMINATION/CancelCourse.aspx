<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CancelCourse.aspx.cs" Inherits="ACADEMIC_CancelCourse" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                   COURSE CANCELLATION
                    <!-- Button used to launch the help (animation) -->
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                </td>
            </tr>
        </table>
           <br />
            <asp:Panel ID="pnlSearch" runat="server">
          
                    <table width="100%" cellpadding="0" cellspacing="0" style="border:dashed 1px black; height:1px">
                      
                        <tr>
                            <td class="form_left_label">
                                Session:
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" Width="210px" 
                                    Font-Bold="False" AutoPostBack="True" onselectedindexchanged="ddlSession_SelectedIndexChanged"
                                   >
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="form_left_label">
                                Reg. No:
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtStudent" runat="server" onkeypress="return getRegNo(event)" ToolTip="REG.NO." Width="160px"
                                    TabIndex="2" />&nbsp;<asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click"
                                    TabIndex="3" ValidationGroup="rpt" OnClientClick="return checkRegNo();" /></td>
                        </tr>
                               <tr>
                            <td class="form_left_label">
                                <asp:Label ID="lblsem" runat="server" Text="Semester:" Visible="false"></asp:Label>
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlsemester" runat="server" AppendDataBoundItems="True" 
                                    Visible="false" Width="160px" 
                                    Font-Bold="False" AutoPostBack="True" 
                                    onselectedindexchanged="ddlsemester_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsemester"
                                    Display="None" ErrorMessage="Please Select semester" ValidationGroup="report"
                                    InitialValue="0"></asp:RequiredFieldValidator>
        
                                     
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                &nbsp;</td>
                            <td class="form_left_text">
                                    <asp:Label ID="lblMsg" runat="server" SkinID="lblmsg"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                &nbsp;
                            </td>
                            <td class="form_left_text">
                                <asp:ValidationSummary ID="vsCopyCase" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="rpt" />
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                   
                   <div id="Details" runat="server" visible="false">
                   <fieldset class="fieldset">

                   <legend class="legend">Student Details</legend>
             
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="form_left_label">
                                    Student Name :
                                </td>
                                <td class="form_left_text">
                                    <asp:Label ID="lblStudent" runat="server" Font-Bold="True" TabIndex="8"></asp:Label>
                                </td>
                                 <td class="form_left_label">
                                    Branch :
                                </td>
                                <td class="form_left_text">
                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True" TabIndex="5"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                   Roll No. :
                                </td>
                                <td class="form_left_text">
                                    <asp:Label ID="lblroll" runat="server" Font-Bold="True" TabIndex="6"></asp:Label>
                                </td>
                                <td class="form_left_label">
                                    Degree Name :
                                </td>
                                <td class="form_left_text">
                                    <asp:Label ID="lblDegreeName" runat="server" Font-Bold="True" ToolTip="DEGREENO"
                                        TabIndex="4"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                    Scheme :
                                </td>
                                <td class="form_left_text">
                                    <asp:Label ID="lblScheme" runat="server" Font-Bold="True" ToolTip="SCHEMENO" TabIndex="10"></asp:Label>
                                </td>
                                <td class="form_left_label">
                                    Semester :
                                </td>
                                <td class="form_left_text">
                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="True" ToolTip="SEMESTERNO"
                                        TabIndex="9"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="form_left_label">
                                    Remark :
                                </td>
                                <td class="form_left_text" align="left" colspan="1">
                                    <asp:TextBox runat="server" ID="txtReason" TextMode="MultiLine" Width="79%" Height="70px"
                                        TabIndex="13" MaxLength="30"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvReason" runat="server" Display="None" ErrorMessage="Please Enter Reason"
                                        ValidationGroup="report" ControlToValidate="txtReason"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_label">
                                </td>
                                <td class="form_left_text" colspan="1">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                        ValidationGroup="report" TabIndex="14" />&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                         OnClick="btnCancel_Click" TabIndex="15" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:ValidationSummary ID="VSCALCEL" runat="server" ShowMessageBox="True" ShowSummary="False"
                                        ValidationGroup="report" />
                                </td>
                            </tr>
                             </table>
    
          </fieldset>
                   </div>
     
              
  
        <tr>
            <td colspan="4" style="padding: 10px; text-align: center;">
             <table width="100%">
             <tr>
               <td valign="top">
                <asp:ListView ID="lvinfo" runat="server">
                    <LayoutTemplate>
                        <div class="vista-grid">
                            <div class="titlebar">
                                Course Registered</div>
                            <table id="table1" cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                <thead>
                                    <tr class="header">
                                    <th>
                                    <asp:CheckBox ID="chkselectall" runat="server" ToolTip="Select/Select all"  onclick="checkAll(this);" />
                                    Select 
                                    
                                    </th>
                                          <th>
                                            Course Code
                                        </th>
                                        
                                        <th align="center">
                                            Course
                                        </th>
                                         <th>
                                           Theory/Prac
                                    </th>
                                    <th>
                                           STATUS
                                    </th>
                                    <th>
                                    Credits
                                    </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server" />
                                </thead>
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                           <td>
                              <asp:CheckBox ID="chkAccept" runat="server"/>
                            </td>
                                 <td align="center">
                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                 </td>
                                <td align="center">
                                  <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                             </td>
                               <td style="font-weight:bold" align="center">
                                   <%# (Eval("SUBID").ToString()) == "1" ? "Theory" : "Practical"%>
                               </td>
                               <td style="font-weight:bold" align="center">
                                   '<%# Eval("PRE_STATUS")%>'
                               </td>
                                    <td align="center">
                              <asp:Label ID="lblcredits" runat="server" Text='<%# Eval("CREDITS")%>' />   
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                </td>
                
          
        </tr>
    </table>
    </td>
    </tr>

   
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">
        function getRegNo(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode == 13) {
                if (checkRegNo() == true) {
                    var btn = document.getElementById('<%= btnShow.ClientID %>');
                    //__doPostBack('ctl00$ContentPlaceHolder1$Button1','');
                    __doPostBack(btn.name, '');
                }
            }

            document.getElementById('ctl00_ContentPlaceHolder1_lblMsg').innerHTML = '';
        }

        
        function checkAll(chkid) {

            var tbl = document.getElementById('table1');
            
            var selectedItem = 0;
            if (tbl != null && tbl.rows && tbl.rows.length > 0) {

                for (i = 0; i < tbl.rows.length; i++) 
                {
                    var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvinfo_ctrl' + i + '_chkAccept');
                    if (chkid.checked == true) 
                    {
                        chk.checked = true;
                    }
                    else
                        chk.checked = false;
                }
            }
        }

        function checkRegNo() 
        {
            var txtReg = document.getElementById('<%= txtStudent.ClientID %>');
            if(document.getElementById('<%=ddlSession.ClientID %>').selectedIndex == 0)
                 {
                        alert('Please Select Session.');
                        return false;
                 }
           else if (txtReg.value == '' | txtReg.value == null) 
           {
                txtReg.focus();
                alert('Please Enter Reg.No.');
                return false;
            }

            else 
            {
                var ret = confirm('Confirm Reg.No. : ' + txtReg.value);
                if (ret == true)
                    return true;
                else
                {
                    txtReg.focus();
                    return false;
                }
            }
        }
    </script>

</asp:Content>
