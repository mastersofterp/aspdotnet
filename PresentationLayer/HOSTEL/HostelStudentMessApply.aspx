<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HostelStudentMessApply.aspx.cs" 
Inherits="HostelStudentMessApply" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="~/includes/prototype.js" type="text/javascript"></script>

    <script src="~/includes/scriptaculous.js" type="text/javascript"></script>

    <script src="~/includes/modalbox.js" type="text/javascript"></script>
  <script  type="text/javascript">
      function RunThisAfterEachAsyncPostback() {


          Autocomplete();
      }
    </script>
    <script  type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
<asp:Panel ID="pnDisplay" runat="server">
  <table width="100%" cellpadding="0" cellspacing="0" border="0">
     <tr>
            <td>
                <table class="vista_page_title_bar" width="100%">
                    <tr>
                        <td style="height: 30px">
                           Apply Hostel Mess &nbsp;&nbsp
                            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                AlternateText="Page Help" ToolTip="Page Help" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    
     <tr>
            <td>
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                    border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; font-size: 12px; border: solid 1px #CCCCCC;
                    background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right;">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                            font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record
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

                <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp"
                    Enabled="True">
                    <Animations>
                        <OnClick>
                            <Sequence>
                                <%-- Disable the button so it can't be clicked again --%>
                                <EnableAction Enabled="false" />
                                
                                <%-- Position the wire frame on top of the button and show it --%>
                                
                                
                                
                                <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                                <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                
                                <%-- Move the wire frame from the button's bounds to the info panel's bounds --%>
                                
                                
                                
                                <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
                                    <Move Horizontal="150" Vertical="-50" />
                                    <Resize Width="260" Height="280" />
                                    <Color PropertyKey="backgroundColor" StartValue="#AAAAAA" EndValue="#FFFFFF" />
                                </Parallel>
                                
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
                <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose"
                    Enabled="True">
                    <Animations>
                        <OnClick>
                            <Sequence AnimationTarget="info">
                                <%--  Shrink the info panel out of view --%>
                                <StyleAction Attribute="overflow" Value="hidden"/>
                                <Parallel Duration=".3" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                </Parallel>
                                
                                <%--  Reset the sample so it can be played again --%>
                                
                                
                                
                                <StyleAction Attribute="display" Value="none"/>
                                <StyleAction Attribute="width" Value="250px"/>
                                <StyleAction Attribute="height" Value=""/>
                                <StyleAction Attribute="fontSize" Value="12px"/>
                                <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                                
                                <%--  Enable the button so it can be played again --%>
                                
                                
                                
                                <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                            </Sequence>
                        </OnClick>
                        <OnMouseOver>
                            <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                        </OnMouseOver>
                        <OnMouseOut>
                            <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                        </OnMouseOut>
                    </Animations>
                </ajaxToolKit:AnimationExtender>
                <div style="color: Red; font-weight: bold">
                </div>
            </td>
        </tr>
    
   
        <tr>
            <td class="form_left_label">
                    <span style="font-size: 9pt"><b>Note : * denotes mandatory fields. <br /></span>
            </td>
        </tr>
    
     <tr>
            <td align="center">
                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
            </td>
                                <td rowspan="10" >
                            <fieldset class="fieldset" style="width: 60%">
                                                         <asp:ListView ID="lvMess"   runat="server">
                                                            <LayoutTemplate>
                                                                <div class="GridViewStyle">
                                                                    <table class="datatable" cellpadding="2" cellspacing="2" width="100%">
                                                                        <tr class="header">
                                                                            <th style="text-align: justify">
                                                                                <b style="color: Black;">&nbsp;Mess</b> 
                                                                            </th>
                                                                            <th style="text-align: justify">
                                                                              <b style="color: Black;">Vacant</b> 
                                                                            </th>
                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                    <td>
                                                                       <b style="color: Black;"><%# Eval("MESS_NAME")%></b> 
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                         <b style="color: Black;"><%# Eval("VACANT")%></b> 
                                                                    </td>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        </fieldset> 
                                                    
   
                                </td>
                                
        </tr>
     <tr id ="trpdetail"  runat="server">
            <td>
                <fieldset class="fieldset" style="width: 80%">
                  <legend class="legend">Student Information</legend>   
                    <table cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td class="form_left_label" style="width: 10%">
                               Admission No :
                            </td>
                            <td class="form_left_text" style="width: 10%">
                             <asp:Label ID="lblregno" runat="server" ></asp:Label>
                            
                            <a href="#"  title="Search Student for Modification" onclick="Modalbox.show($('divdemo2'), {title: this.title, width: 600,overlayClose:false});return false;">
                                    <asp:Image ID="imgSearch" Visible="false"  runat="server" ImageUrl="~/images/search.png" TabIndex="1"  
                                        AlternateText="Search Student by IDNo, Name, Reg. No, Branch, Semester" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" />
                                </a>
                                </td> 
                                  <td style="width: 20%"rowspan="10"  >
                                      
                                      <asp:Image ID="imgPhoto" runat="server" Width="128px" Height="128px" /><br />
                                      <br /><br />
                                       <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label" style="width: 10%">
                               Student Name :
                            </td>
                            <td class="form_left_text" style="width: 10%">
                             <asp:Label ID="lblName" runat="server" ></asp:Label>
                            </td>
                                           
                                              
                        </tr>
                        <tr>
                            <td class="form_left_label" style="width: 10%">
                                Branch :
                            </td>
                            <td class="form_left_text" style="width: 10%">
                                 <asp:Label ID="lblBranch" runat="server" ></asp:Label>
                                </td>
                            
                        </tr>
                        <tr>
                            <td class="form_left_label" style="width: 10%">
                                Semester :
                            </td>
                            <td class="form_left_text" style="width: 10%">
                                 <asp:Label ID="lblSemester" runat="server" ></asp:Label>
                                </td>
                        </tr>
                        <tr>
                                <td width="25%">
                                Hostel & Room Name:
                                </td>
                                <td width="30%">
                              <b>  <asp:Label ID="lblhostelname"  runat="server"></asp:Label></b>
                                </td>
                        </tr>
                          <tr runat="server" id="Tr4" visible="false" >
                                <td width="25%">
                                Alloted Mess Name:
                                </td>
                                <td width="30%">
                              <b>  <asp:Label ID="lblallotedmessbname"  runat="server"></asp:Label></b>
                                </td>
                            </tr>
                        <tr runat="server" id="Tr1">
                                <td width="25%">
                                Mess Name:
                                </td>
                                <td width="30%">
                              <b>  <asp:Label ID="lblmessname"  runat="server"></asp:Label></b>
                                </td>
                            </tr>
                            
                        <tr runat="server" id="Tr3">
                                <td width="25%">
                               Hostel Session:
                                </td>
                                <td width="30%">
                              <b>  
                              <asp:Label ID="lblsession"   runat="server"></asp:Label></b>
                               <asp:Label ID="lblschmonth" Visible="false"  runat="server"></asp:Label></b
                                </td>
                            </tr>
                            
                    </table> 
                     </fieldset>  
                        
                   
                </td>
      </tr>
      <tr id ="tr2" visible="false" runat="server">
            <td>
             <fieldset class="fieldset" style="width: 80%">
                  <legend class="legend">Apply Mess</legend>   
                    <table id="tblapplymess" runat="server"  cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                               <td class="form_left_label" style="width: 10%">
                                <span class="validstar">*</span>Mess :
                              </td>
                               <td>
                                <asp:RadioButtonList ID="rblMess" runat="server" RepeatDirection="Vertical" 
                                  Width="300%" ToolTip="Click to select this answer"> </asp:RadioButtonList>
                               </td>
                               </tr>
                      <tr>
                                <td class="form_left_label" align="center"  >
                                   
                                </td>
                                <td visible="false" class="form_left_text">
                                    <asp:DropDownList ID="ddlMess"     runat="server" AppendDataBoundItems="true" ValidationGroup="teacherassign"
                                        Width="300px" runat="server" onselectedindexchanged="ddlMess_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlMess"
                                        Display="None" ErrorMessage="Please Select Batch" InitialValue="0" ValidationGroup="teacherassign"></asp:RequiredFieldValidator>
                                </td>
                               </tr>
                               <tr>
                                <td class="form_left_label"  style="width: 10%">
                              <span class="validstar">*</span>Reason :
                              </td>
                              <td class="form_left_text" style="width: 10%">
                                <asp:TextBox ID="txtpurpose"   runat="server" Width="300px" TabIndex="0"  />
                               </td>
                               </tr>
                             
                               
                            
                </table> 
                     </fieldset>   
            
            </td>
            
      </tr>
   
     <tr>
            <td>
                <table cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td align="center" class="form_button">
                         <asp:Button ID="btnSubmit" Visible="false"  runat="server" Text="Submit" TabIndex="10" OnClick="btnSubmit_Click"
                                Width="80px"  />&nbsp;
                            <asp:Button ID="btnPrint" Visible="false"  runat="server" Text="Print" 
                                TabIndex="10" Width="80px" onclick="btnPrint_Click"  />&nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Visible="false"  Text="Cancel" 
                                Width="80px" TabIndex="11"
                                CausesValidation="false" onclick="btnCancel_Click1" />&nbsp;
                            <asp:ValidationSummary ID="vsEmp" runat="server" ValidationGroup="emp" ShowMessageBox="true"
                                ShowSummary="false" DisplayMode="List" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
 </table> 
  
  <div id="divdemo2" style="display: none; height: 550px">
        <asp:UpdatePanel ID="updEdit" runat="server">
            <ContentTemplate>
                <table cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td>
                            Search Criteria:
                        </td>
                        <td>
                            <asp:RadioButton ID="rbName" runat="server" Text="Name" GroupName="edit" />
                            <asp:RadioButton ID="rbIdNo" runat="server" Text="IdNo" GroupName="edit" />
                            <asp:RadioButton ID="rbBranch" runat="server" Text="Branch" GroupName="edit" />
                            <asp:RadioButton ID="rbSemester" runat="server" Text="Semester" GroupName="edit" />
                            <asp:RadioButton ID="rbEnrollmentNo" runat="server" Text="Enrollmentno" GroupName="edit"
                                Checked="True" />
                            <asp:RadioButton ID="rbRegNo" runat="server" Text="Rollno" GroupName="edit"
                                Checked="True" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Search String :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return submitPopup(this.name);" />
                            <asp:Button ID="btnCancelModal" runat="server" Text="Cancel" OnClientClick="return ClearSearchBox(this.name)" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updEdit">
                                <ProgressTemplate>
                                    <asp:Image ID="imgProg" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                    Loading.. Please Wait!</ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" width="100%">
                            <asp:ListView ID="lvStudent" runat="server">
                                <LayoutTemplate>
                                    <div class="vista-grid">
                                        <div class="titlebar">
                                            Login Details</div>
                                        <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <thead>
                                                <tr class="header">
                                                    <th style="width: 20%">
                                                        Name
                                                    </th>
                                                    <th style="width: 15">
                                                        IdNo
                                                    </th>
                                                    <th style="width: 20%">
                                                        Roll No.
                                                    </th>
                                                    <th style="width: 30%">
                                                        Branch
                                                    </th>
                                                    <th style="width: 15%">
                                                        Semester
                                                    </th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                    <div class="listview-container">
                                        <div id="demo-grid" class="vista-grid">
                                            <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item" >
                                        <td style="width: 20%">
                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                OnClick="lnkId_Click"></asp:LinkButton>
                                        </td>
                                        <td style="width: 15%">
                                            <%# Eval("idno")%>
                                        </td>
                                        <td style="width: 20%">
                                            <%# Eval("EnrollmentNo")%>
                                        </td>
                                        <td style="width: 30%">
                                            <%# Eval("longname")%>
                                        </td>
                                        <td style="width: 15%">
                                            <%# Eval("semesterno")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   
   <script type="text/javascript" language="javascript">
       
        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "~/images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "~/images/collapse_blue.jpg";
            }
        }




        function submitPopup(btnsearch) {
            var rbText;
            var searchtxt;
            if (document.getElementById('<%=rbName.ClientID %>').checked == true)
                rbText = "name";
            else if (document.getElementById('<%=rbIdNo.ClientID %>').checked == true)
                rbText = "idno";
            else if (document.getElementById('<%=rbBranch.ClientID %>').checked == true)
                rbText = "branch";
            else if (document.getElementById('<%=rbSemester.ClientID %>').checked == true)
                rbText = "sem";
            else if (document.getElementById('<%=rbEnrollmentNo.ClientID %>').checked == true)
                rbText = "enrollmentno";
            else if (document.getElementById('<%=rbRegNo.ClientID %>').checked == true)
                rbText = "regno";

            searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;

            __doPostBack(btnsearch, rbText + ',' + searchtxt);

            return true;
        }

        function ClearSearchBox(btncancelmodal) {
            document.getElementById('<%=txtSearch.ClientID %>').value = '';
            __doPostBack(btncancelmodal, '');
            return true;
        }



        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.focus();
                alert('Only Alphabets Allowed');
                return false;
            }
            else
                return true;

        }
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
        function validateAlphaNumeric(txt) {
            var expAN = /[$\\@\\\#%\^\&\*\(\)\[\]\+\_popup\{\}|`\~\=\|]/;
            var strPass = txt.value;
            var strLength = strPass.length;
            var lchar = txt.value.charAt((strLength) - 1);

            if (lchar.search(expAN) != -1) {
                txt.value(txt.value.substring(0, (strLength) - 1));
                txt.focus();
                alert('Only Alpha-Numeric Characters Allowed!');
            }
            return true;
        }
        function LoadImage() {
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").src = document.getElementById("ctl00_ContentPlaceHolder1_fuPhotoUpload").value;
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").height = '96px';
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").width = '96px';
        }

        //Round to two digits
        fixedTo = function(number, n) {
            var k = Math.pow(10, n);
            return (Math.round(number * k) / k);
        }

        
    </script>

   
</asp:Panel> 
<div id="divMsg" runat="server">
    </div>
</asp:Content> 
