<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Company_Reg.aspx.cs" Inherits="TRAININGANDPLACEMENT_Masters_Company_Reg" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                COMPANY REGISTRATION &nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
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
                            <%--<asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record--%>
                            <%--<asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record--%>
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                            // Move an element directly on top of another element (and optionally
                            // make it the same size)
                            function Cover(bottom, top, ignoreSize) 
                            {
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
                <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
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
            </td>
        </tr>
        <tr>
            <td><br />
                <asp:Panel ID="pnlAdd" runat="server" Width="813px">
                    <div style="text-align: left; width: 97%; padding-left: 10px;">
                        <fieldset class="fieldsetPay">
                            <legend class="legendPay">Add</legend>
                            
                            <table>
                                <tr>
                                    <td style="width: 713px">
                                        <br />
                                        <table cellpadding="0" cellspacing="0">
                                        
                                            <tr>
                                                <td class="form_left_label" style="width: 25%">
                                                    Company Name :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtCompany" runat="server" MaxLength="100" Width="350px" />
                                                    <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ControlToValidate="txtCompany"
                                                        Display="None" ErrorMessage="Please Enter Company Name" ValidationGroup="Company"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" >
                                                    Short Name :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtShortname" runat="server" MaxLength="20" Width="100px" />
                                                    <asp:RequiredFieldValidator ID="rfvShortname" runat="server" ControlToValidate="txtShortname"
                                                        Display="None" ErrorMessage="Please Enter Short Name" ValidationGroup="Company"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" >
                                                    Category :
                                                </td>
                                                <td class="form_left_text" >
                                                    <asp:DropDownList ID="ddlCategory" runat="server" Width="200px" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvCategory"  runat="server" ControlToValidate="ddlCategory"
                                                        Display="None" ErrorMessage="Please Select Category  " ValidationGroup="Company"
                                                        SetFocusOnError="true" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" >
                                                    Director Name :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtDirector" runat="server" MaxLength="50" Width="250px" />                                                    
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="form_left_label" >
                                                    Company Address :
                                                </td>
                                                <td class="form_left_text">
                                                   <asp:TextBox ID="txtCompAdd" runat="server" TextMode="MultiLine" Height="40px" MaxLength="200" Width="300px"  />  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" >
                                                    City :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:DropDownList ID="ddlCity" runat="server" Width="200px"
                                                    AppendDataBoundItems="true" AutoPostBack="true" 
                                                    OnSelectedIndexChanged="ddlcity_SelectedIndexChanged">
                                                    <%--<asp:ListItem Value="-2">New Item?</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    
                                                   
                                                    <asp:TextBox ID="txtbox" runat="server" width="200px" Visible="false"
                                                     ontextchanged="txtbox_TextChanged" AutoPostBack="true"  ></asp:TextBox>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" >
                                                    Pincode :
                                                </td>
                                                <td class="form_left_text">
                                                  <asp:TextBox ID="txtPincode" runat="server" MaxLength="6"  Width="100px"></asp:TextBox>  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" >
                                                    Phone No :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtPhoneNo" runat="server" MaxLength="50"  Width="150px"></asp:TextBox>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" >
                                                    FAX No :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtFaxNo" runat="server" MaxLength="50" Width="150px"></asp:TextBox>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" >
                                                    Email Id :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtemailid" runat="server" MaxLength="50"  Width="200px"></asp:TextBox>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" >
                                                    Web Site :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtWebSite" runat="server" MaxLength="50"  Width="200px"></asp:TextBox>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" >
                                                    Salary Range :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtSalRange" runat="server" MaxLength="50"  Width="200px"></asp:TextBox>                                                    
                                                </td>
                                            </tr>
                                                  <tr>
                                                <td class="form_left_label">
                                                <b><asp:CheckBox ID="chkInplant" runat="server" Text="Inplant Training" /></b>
                                                </td>
                                                <td class="form_left_text">
                                                 Branch : &nbsp;<asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" Width="350px">
                                                 <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                 </asp:DropDownList>&nbsp;
                                                 <asp:Button ID="btnAddIP" OnClick="btnAddIP_Click" runat="server" Text="Add" />
                                                 </td>                                               
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                <div id="divto" style="padding-left:10px">
                                                    <asp:ListView ID="lvTo" runat="server" style="padding-left:10px;">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    Branch Name
                                                                </div>
                                                                <br />
                                                                <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                                    <tbody>
                                                                        <tr class="header">
                                                                            <th>
                                                                                Delete
                                                                            </th>
                                                                            <th>
                                                                                Branch Name
                                                                            </th>
                                                                         </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                                <td>
                                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("SrNo") %>'
                                                                        ImageUrl="~/images/delete.gif" OnClick="btnDelete_Click" ToolTip="Delete Record" />
                                                                </td>
                                                                <td id="IOTRANNO" runat="server">
                                                                    <%# Eval("BRANCHNAME") %>
                                                                    <asp:HiddenField ID="hfBranchno" Value='<%# Eval("BRANCHNO")%>' runat="server" />
                                                                </td>
                                                                </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <fieldset id="Fieldset1" class="fieldset" runat="server">
                                                        <legend class="legend">Contact Person Details</legend>
                                                        <table>
                                                            <tr>
                                                                <td class="form_left_label">
                                                                    Name :
                                                                </td>
                                                                <td class="form_left_text">
                                                                    <asp:TextBox ID="txtContPerson" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvContPerson" runat="server" ControlToValidate="txtContPerson"
                                                                        SetFocusOnError="true" ErrorMessage="Please Enter Contact Person" Display="None"
                                                                        ValidationGroup="Company" />
                                                                </td>
                                                                <td class="form_left_label">
                                                                    Designation :
                                                                </td>
                                                                <td class="form_left_text">
                                                                    <asp:TextBox ID="txtContDesignation" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="form_left_label">
                                                                    Address :
                                                                </td>
                                                                <td class="form_left_text">
                                                                    <asp:TextBox ID="txtContAddress" runat="server" MaxLength="200" TextMode="MultiLine"
                                                                        Height=" 40px" Width="300px"></asp:TextBox>
                                                                </td>
                                                                <td class="form_left_label">
                                                                    Contact No. :
                                                                </td>
                                                                <td class="form_left_text">
                                                                    <asp:TextBox ID="txtContPhone" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="form_left_label">
                                                                    Email :
                                                                </td>
                                                                <td class="form_left_text">
                                                                    <asp:TextBox ID="txtContMailId" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvContMailid" runat="server" ControlToValidate="txtContMailId"
                                                                        Display="None" ErrorMessage="Enter Contact mail ID" ValidationGroup="Company"
                                                                        SetFocusOnError="true" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                Copy Contact Person Deatils
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgbToCopyLocalAddress" runat="server" ImageUrl="~/images/copy.png"
                                                        OnClientClick="copyDetails(this)"  ToolTip="Copy Contact Person Deatils" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <fieldset id="Fieldset2" class="fieldset" runat="server">
                                                        <legend class="legend">Contact Person Details For Inplant Training</legend>
                                                        <table>
                                                            <tr>
                                                                <td class="form_left_label">
                                                                    Name :
                                                                </td>
                                                                <td class="form_left_text">
                                                                    <asp:TextBox ID="txtIPName" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtContPerson"
                                                                        SetFocusOnError="true" ErrorMessage="Please Enter Inplant Training Conatctame" Display="None"
                                                                        ValidationGroup="Company" />--%>
                                                                </td>
                                                                <td class="form_left_label">
                                                                    Designation :
                                                                </td>
                                                                <td class="form_left_text">
                                                                    <asp:TextBox ID="txtIPDesignation" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="form_left_label">
                                                                    Address :
                                                                </td>
                                                                <td class="form_left_text">
                                                                    <asp:TextBox ID="txtIPAddress" runat="server" MaxLength="200" TextMode="MultiLine" Height=" 40px"
                                                                        Width="300px"></asp:TextBox>
                                                                </td>
                                                                <td class="form_left_label">
                                                                    Contact No. :
                                                                </td>
                                                                <td class="form_left_text">
                                                                    <asp:TextBox ID="txtIPContNo" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="form_left_label">
                                                                    Email :
                                                                </td>
                                                                <td class="form_left_text">
                                                                    <asp:TextBox ID="txtIPEmail" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContMailId"
                                                                        Display="None" ErrorMessage="Enter Contact mail ID" ValidationGroup="Company"
                                                                        SetFocusOnError="true" />
                                                                </td>
                                                                <td class="form_left_label">
                                                                Placement Contact No.:                                                                    
                                                                </td>
                                                                <td>
                                                                <asp:TextBox ID="txtPConactNo" runat="server" Width="100px" MaxLength="15"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            
                                            
                                            <tr>
                                                <td class="form_left_label" >
                                                    Other Information :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtOthInfo" runat="server" MaxLength="300" TextMode="MultiLine" Height= " 50px" Width="300px"></asp:TextBox>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" >
                                                    Remark :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtRemark" runat="server" MaxLength="300" TextMode="MultiLine" Height= " 50px" Width="300px"></asp:TextBox>                                                    
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width: 713px">
                                        &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td  class="form_button" align="center" style="width: 713px">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Company" OnClick="btnSave_Click"
                                            Width="80px" />
                                        &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                            OnClick="btnCancel_Click" Width="80px" />&nbsp;
                                        <%--<asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                            Width="80px" />--%>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Company"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width: 713px">
                                        &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width:713px">
                                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-left: 10px">
                <asp:Panel ID="pnlList" runat="server" Width="813px">
                    <table cellpadding="0" cellspacing="0" style="width: 97%; text-align: center">
                        <tr>
                            <td style="text-align: left; padding-left: 10px; padding-top: 10px;">
                                <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click">Add New</asp:LinkButton>
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" Width="90px" OnClick="btnShowReport_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:RadioButtonList ID="radlStatus" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="radlStatus_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="B" Selected="True">All</asp:ListItem>
                                    <asp:ListItem Value="P">Pending</asp:ListItem>
                                    <asp:ListItem Value="A">Approved</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:ListView ID="lvCompany" runat="server">
                                    <EmptyDataTemplate>
                                        <br />
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To register Company " />
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div id="demo_grid" class="vista-grid">
                                            <div class="titlebar">
                                                Company List</div>
                                            <table class="datatable">
                                                <tr class="header">
                                                    <th>
                                                        Action
                                                    </th>
                                                    <th>
                                                        Company
                                                    </th>
                                                    <th>
                                                        Category
                                                    </th>
                                                    <th>
                                                        City
                                                    </th>
                                                    <th>
                                                        Website
                                                    </th>
                                                    <th>
                                                        Cont.Person
                                                    </th>
                                                    <th>
                                                        Ph. No.
                                                    </th>
                                                    <th>
                                                        Email id
                                                    </th>
                                                    <th>
                                                        Status
                                                    </th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td style="width: 5%">
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("COMPID") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                <%-- <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("COMPID") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                            </td>
                                            <td style="width: 15%">
                                                <%# Eval("COMPNAME")%>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("CATNAME")%>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("CITY")%>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("WEBSITE")%>
                                            </td>
                                            <td style="width: 15%">
                                                <%# Eval("CONTPERSON")%>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("CONTPHONE")%>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("CONTMAILID")%>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("STATUS")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("COMPID") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                <%-- <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("COMPID") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                            </td>
                                            <td>
                                                <%# Eval("COMPNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("CATNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("CITY")%>
                                            </td>
                                            <td>
                                                <%# Eval("WEBSITE")%>
                                            </td>
                                            <td>
                                                <%# Eval("CONTPERSON")%>
                                            </td>
                                            <td>
                                                <%# Eval("CONTPHONE")%>
                                            </td>
                                            <td>
                                                <%# Eval("CONTMAILID")%>
                                            </td>
                                            <td>
                                                <%# Eval("STATUS")%>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                                <div class="vista-grid_datapager">
                                    <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvCompany" PageSize="10"
                                        OnPreRender="dpPager_PreRender">
                                        <Fields>
                                            <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                            <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        </table>
     <div id="divMsg" runat="server">
    </div>
             <script type="text/javascript">
        

         function copyDetails(chk) {
             if (chk.click) {
                 document.getElementById('<%= txtIPName.ClientID %>').value = document.getElementById('<%= txtContPerson.ClientID %>').value;
                 document.getElementById('<%= txtIPDesignation.ClientID %>').value = document.getElementById('<%= txtContDesignation.ClientID %>').value;

                 document.getElementById('<%= txtIPAddress.ClientID %>').value = document.getElementById('<%= txtContAddress.ClientID %>').value;
                 document.getElementById('<%= txtIPContNo.ClientID %>').value = document.getElementById('<%= txtContPhone.ClientID %>').value;
                 document.getElementById('<%= txtIPEmail.ClientID %>').value = document.getElementById('<%= txtContMailId.ClientID %>').value;
             }
             else {
                 document.getElementById('<%= txtIPName.ClientID %>').value = '';
                 document.getElementById('<%= txtIPDesignation.ClientID %>').value = '';
                 document.getElementById('<%= txtIPAddress.ClientID %>').value = '';
                 document.getElementById('<%= txtIPContNo.ClientID %>').value = '';
                 document.getElementById('<%= txtIPEmail.ClientID %>').value = '';

             }
         } 
    </script>
</asp:Content>

