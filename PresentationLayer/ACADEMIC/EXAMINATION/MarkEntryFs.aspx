<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MarkEntryFs.aspx.cs" Inherits="ACADEMIC_MarkEntryFs" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                MARK ENTRY SESSIONNAL (B-Arch)
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                    border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                    font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                            font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                    </div>
                </div>
                <script type="text/javascript" language="javascript">
                    // Move an element directly on top of another element (and optionally
                    // make it the same size)
                    function Cover(bottom, top, ignoreSize) {
                        var location = Sys.UI.DomElement.getLocation(bottom);
                        top.style.position = 'absolute';
                        top.style.top = location.y + 'px';
                        top.style.left = location.x + 'px';
                        if (!ignoreSize) {
                            top.style.height = bottom.offsetHeight + 'px';
                            top.style.width = bottom.offsetWidth + 'px';
                        }
                    }
                </script>

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
                    <Animations>
                    <OnClick>
                        <Sequence>
                            <%-- Disable the button so it can't be clicked again --%>
                            <EnableAction Enabled="false" />
                            
                            <%-- Position the wire frame on top of the button and show it --%>
                            <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                            
                            <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                            <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                            <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                            <FadeIn AnimationTarget="info" Duration=".2"/>
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                            
                            <%-- Flash the text/border red and fade in the "close" button --%>
                            <Parallel AnimationTarget="info" Duration=".5">
                                <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                            </Parallel>
                            <Parallel AnimationTarget="info" Duration=".5">
                                <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                            </Parallel>
                        </Sequence>
                    </OnClick>
                    </Animations>
                </ajaxToolKit:AnimationExtender>
            </td>
        </tr>
    </table>
 <asp:Panel ID="pnlMarkEntry" runat="server">
        <fieldset class="fieldset">
            <legend class="legend">Marks Entry</legend>
            <table style="width: 100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td style="padding: 5px; border: solid 1px gray;">
                        <table style="width: 100%" cellpadding="2" cellspacing="2">
                            <tr>
                                <td style="width: 15%">
                                    Session :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" 
                                        Font-Bold="true" onselectedindexchanged="ddlSession_SelectedIndexChanged" 
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Select the Session" 
                                    ControlToValidate="ddlSession" Display="None" SetFocusOnError="true" ValidationGroup="submit" />
                                </td>
                                <td rowspan="2" style="width: 40%; vertical-align: top">
                                    <fieldset class="fieldset" style="padding: 5px; color: Green">
                                        <legend class="legend">Note</legend>Please Enter :<br />
                                        <b>401</b> for Absent<br />
                                        <b>403</b> for Copy Case (UFM)<br />
                                        <span style="font-weight: bold; color: Red;">Please Save and Lock for Final Submission
                                            of Marks</span>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: top;">
                                    Course Name :
                                </td>
                                <td style="vertical-align: top">
                                    <asp:DropDownList ID="ddlCourse" runat="server" Width="50%" AppendDataBoundItems="true" Font-Bold="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ErrorMessage="Select the Course" 
                                    ControlToValidate="ddlCourse" Display="None"  SetFocusOnError="true" ValidationGroup="submit" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; vertical-align: top">
                                 <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                    
                                </td>
                                <td style="vertical-align: top">
                                    <asp:Button ID="btnShow" runat="server" Font-Bold="true" OnClick="btnShow_Click"
                                        Text="Show" Width="60px"  OnClientClick="return validate();" ValidationGroup="submit"/>
                                    &nbsp;&nbsp;<asp:Button ID="btnSave" runat="server"  Font-Bold="true"
                                        OnClick="btnSave_Click" Text="Save" Width="60px" Enabled="false"/>
                                    &nbsp;&nbsp;<asp:Button ID="btnLock" runat="server"  Font-Bold="true"
                                        OnClick="btnLock_Click" OnClientClick="return showLockConfirm();" Text="Lock"
                                        Width="60px" Enabled="false" />
                                    &nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Font-Bold="true" OnClick="btnCancel_Click"
                                        Text="Cancel" Width="60px" />
                                    &nbsp;
                                    <asp:Button ID="btnReport" runat="server" Font-Bold="true" Enabled="false"
                                        OnClick="btnReport_Click" Text="Report" 
                                        ValidationGroup="submit" Width="60px" />
                                    </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px; text-align: center">
                        <asp:Panel ID="pnlStudGrid" runat="server">
                            <div id="demo-grid" class="vista-grid">
                                <div class="titlebar">
                                    Enter Marks for following Students</div>
                                <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CssClass="datatable" onrowdatabound="gvStudent_RowDataBound" >
                                    <HeaderStyle CssClass="gv_header" />
                                    <AlternatingRowStyle BackColor="ControlLight"/>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CLASS ROLL NO." ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="35%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="MidnightBlue">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("ROLL_NO") %>' ToolTip='<%# Bind("IDNO") %>'
 />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="STUDNAME" HeaderText="STUDENT NAME" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="45%" HeaderStyle-ForeColor="MidnightBlue">
                                            <ItemStyle HorizontalAlign="Center" Width="35%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SEMESTER" HeaderText="SEMESTER" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%" HeaderStyle-ForeColor="MidnightBlue">
                                            <ItemStyle HorizontalAlign="Center"  />
                                        </asp:BoundField>
                                        <%--EXAM MARK ENTRY--%>
                                        <asp:TemplateField HeaderText="TH" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="MidnightBlue">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFSMarks" runat="server" Text='<%# Bind("S1MARK","{0:###,##0}") %>' Width="50px"
                                                    Font-Bold="true" Style="text-align: center"  />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtFSMarks" runat="server" FilterType="Custom"
                                                    ValidChars="0123456789." TargetControlID="txtFSMarks">
                                                </ajaxToolKit:FilteredTextBoxExtender>  
                                                <asp:Label ID="lblFSMarks" runat="server"    Text='<%# Bind("S1MAX") %>'  Visible="false" />
                                                <asp:Label ID="lblFSMinMarks" runat="server" Text='<%# Bind("S1MIN") %>'  Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">

        function validate() 
        { 
            if (document.getElementById('<%=ddlSession.ClientID%>').selectedIndex == 0) {
                alert("Please Enter Session");
                return false;
            }
            if (document.getElementById('<%=ddlCourse.ClientID%>').selectedIndex == 0) {
                alert("Please Enter Course");
                return false;
            }
        }
     
        function validateMark(metxt, maxmrk, minmark) {

            //TA MARK  
            if (metxt != null) {
                if (metxt.value != '' & (Number(metxt.value) > maxmrk)) {
                    if (Number(metxt.value) == 401 || Number(metxt.value) == 403)
                    { }
                    else {
                        alert("Please Enter Marks in the Range of 0 to " + maxmrk.toString() + ' and Note : 401 for Absent and 403 for Copy case');
                        metxt.value = '';
                    }
                    IsNumericTest(metxt);
                }
            }
        }
          
       function IsNumericTest(txt) {
            var ValidChars = "0123456789.";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);
                if (mChar == '.') cnt++;
                if (cnt > 1) { alert("Please Check the value."); txt.value = ""; return }
                else
                    if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }
        
        function IsNumeric(txt) {
            var ValidChars = "0123456789";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);

                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }

        function showLockConfirm() {
            var ret = confirm('Do you really want to lock marks for selected exam?');
            if (ret == true)
                return true;
            else
                return false;
        }
        
    </script>
</asp:Content>

