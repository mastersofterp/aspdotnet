<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AlumniDataEntry.aspx.cs" Inherits="TRAININGANDPLACEMENT_MASTERS_AlumniDataEntry"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                ALUMNI DATA MASTER&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Enable the button so it can be played again --%>
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
            <td>
                <fieldset class="fieldset">
                    <legend class="legend2">Add/Edit Block</legend>
                    <table cellpadding="2" cellspacing="2" border="0">
                        <tr>
                            <td>
                                &nbsp;&nbsp;<b style="color: Red">*</b>Company Name:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlComp" runat="server" Width="300px" TabIndex="1" AppendDataBoundItems="true" />
                                <asp:RequiredFieldValidator ID="rfvComp" runat="server" ControlToValidate="ddlComp"
                                    Display="None" ErrorMessage="Please select Company." ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;<b style="color: Red">*</b>Degree Name:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDegree" runat="server" Width="300px" TabIndex="2" AppendDataBoundItems="true"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" />
                                <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please select Degree." ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;<b style="color: Red">*</b>Branch Name:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBranch" runat="server" Width="300px" TabIndex="3" AppendDataBoundItems="true" />
                                <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please select Branch." ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;<b style="color: Red">*</b>Student Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtStuName" runat="server" MaxLength="50" Width="300px" />
                                <asp:RequiredFieldValidator ID="rfvStuName" runat="server" ControlToValidate="txtStuName"
                                    Display="None" ErrorMessage="Please Enter Student Name." ValidationGroup="submit"
                                    SetFocusOnError="True" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtStuName" runat="server" FilterType="LowercaseLetters, UppercaseLetters"
                                    TargetControlID="txtStuName" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;<b style="color: Red">*</b>Enrollment No:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEnrollNo" runat="server" Width="300px" MaxLength="10" />
                                <asp:RequiredFieldValidator ID="rfvEnrollNo" runat="server" ControlToValidate="txtEnrollNo"
                                    Display="None" ErrorMessage="Please Enter Enrollment No." ValidationGroup="submit"
                                    SetFocusOnError="True" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtEnrollNo" runat="server" FilterType="Numbers"
                                    TargetControlID="txtEnrollNo">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;Contact No:
                            </td>
                            <td>
                                <asp:TextBox ID="txtContact" runat="server" MaxLength="15" Width="300px" />
                                <%--<asp:RequiredFieldValidator ID="rfvContact" runat="server" ControlToValidate="txtContact"
                                    Display="None" ErrorMessage="Please Enter Contact No." ValidationGroup="submit"
                                    SetFocusOnError="True" />--%>
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtContact" runat="server" FilterType="Numbers"
                                    TargetControlID="txtContact">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;Company contact No:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCompCont" runat="server" MaxLength="15" Width="300px" />
                                <%--<asp:RequiredFieldValidator ID="rfvtxtCompCont" runat="server" ControlToValidate="txtCompCont"
                                    Display="None" ErrorMessage="Please Enter Company Contact No." ValidationGroup="submit"
                                    SetFocusOnError="True" />--%>
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtCompCont" runat="server" FilterType="Numbers"
                                    TargetControlID="txtCompCont">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;Email Id:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="300px" />
                                <%--<asp:RequiredFieldValidator ID="rfvMail" runat="server" ControlToValidate="txtEmail"
                                    Display="None" ErrorMessage="Please Enter Email Id." ValidationGroup="submit"
                                    SetFocusOnError="True" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;Designation
                            </td>
                            <td>
                                <asp:TextBox ID="txtDesig" runat="server" MaxLength="50" Width="300px" />
                                <%--<asp:RequiredFieldValidator ID="reftxtDesig" runat="server" ControlToValidate="txtDesig"
                                    Display="None" ErrorMessage="Please Enter Designation." ValidationGroup="submit"
                                    SetFocusOnError="True" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;<b style="color: Red">*</b>Passout Year
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassYear" runat="server" MaxLength="4" Width="300px" />
                                <asp:RequiredFieldValidator ID="rfvtxtPassYear" runat="server" ControlToValidate="txtPassYear"
                                    Display="None" ErrorMessage="Please Enter Passout Year." ValidationGroup="submit"
                                    SetFocusOnError="True" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtPassYear" runat="server" FilterType="Numbers"
                                    TargetControlID="txtPassYear">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;Address Details:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdd" runat="server" MaxLength="50" Width="300px" TextMode="MultiLine" />
                                <%--<asp:RequiredFieldValidator ID="rfvAdd" runat="server" ControlToValidate="txtAdd"
                                    Display="None" ErrorMessage="Please Enter Address Details." ValidationGroup="submit"
                                    SetFocusOnError="True" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                    OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:ListView ID="lvAlumni" runat="server">
                    <LayoutTemplate>
                        <div id="demo-grid" class="vista-grid">
                            <div class="titlebar">
                                List of Alumni Students
                            </div>
                            <table class="datatable" cellpadding="0" cellspacing="0">
                                <tr class="header">
                                    <th>
                                        Edit
                                    </th>
                                    <th>
                                        Student Name
                                    </th>
                                    <th>
                                        Comp. Name
                                    </th>
                                    <th>
                                        Enrollment No
                                    </th>
                                    <th>
                                        Designation
                                    </th>
                                    <th>
                                        Contact
                                    </th>
                                    <th>
                                        CompContact
                                    </th>
                                    <th>
                                        EmailId
                                    </th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server" />
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="item">
                            <td>
                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                    CommandArgument='<%# Eval("aluno") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                    OnClick="btnEdit_Click" TabIndex="10" />
                            </td>
                            <td>
                                <%# Eval("STUDNAME") %>
                            </td>
                            <td>
                                <%# Eval("COMPNAME") %>
                            </td>
                            <td>
                                <%# Eval("ENROLLMENTNO") %>
                            </td>
                            <td>
                                <%# Eval("DESIGNATION") %>
                            </td>
                            <td>
                                <%# Eval("CONTACTNO") %>
                            </td>
                            <td>
                                <%# Eval("COMP_CONTACTNO") %>
                            </td>
                            <td>
                                <%# Eval("EMAILID") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                <div class="vista-grid_datapager">
                    <asp:DataPager ID="dpBlock" runat="server" PagedControlID="lvAlumni" PageSize="10"
                        OnPreRender="dpBlock_PreRender">
                        <Fields>
                            <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                ShowLastPageButton="false" ShowNextPageButton="false" />
                            <asp:NumericPagerField ButtonType="Link" ButtonCount="6" CurrentPageLabelCssClass="current" />
                            <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                ShowLastPageButton="true" ShowNextPageButton="true" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
