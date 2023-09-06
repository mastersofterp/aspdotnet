<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentFileUpload1.aspx.cs" Inherits="ACADEMIC_StudentFileUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <%--<img src="../images/ajax-loader.gif" alt="Loading" />
                Loading..--%>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <table cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                FILE UPLOAD
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
                <div id="divMsg" runat="server">
                </div>
            </td>
        </tr>
        <%--  Shrink the info panel out of view --%>
        <%--  Reset the sample so it can be played again --%>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
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
                            <%--  Enable the button so it can be played again --%>
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

                <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp">
                    <Animations>
                    <OnClick>
                        <Sequence>
                            <%-- Disable the button so it can't be clicked again --%>
                            <EnableAction Enabled="false" />
                            
                            <%-- Position the wire frame on top of the button and show it --%>
                            <ScriptAction Script="Cover($get('ctl00$ctp$btnHelp'), $get('flyout'));" />
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
                <br />
                <div style="color: Red; font-weight: bold">
                    &nbsp;Note : * marked fields are Mandatory</div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <fieldset class="fieldset">
                    <legend class="legend">Student Record Upload</legend>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%" class="datatable">
                               <tr>
                              <td class="form_left_label">
                               Admission Batch
                               </td>
                              <td class="form_left_text">
                               <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true">
                               <asp:ListItem Value="0">Please Select</asp:ListItem>
                               </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                        Display="None" ValidationGroup="academic" InitialValue="0" ErrorMessage="Please Select Admission Batch">
                                    </asp:RequiredFieldValidator>
                               </td>
                               </tr>
                                <tr>
                                    <td class="form_left_label">
                                        Degree
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" Width="25%">
                                            <asp:ListItem>Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                          <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ValidationGroup="academic" InitialValue="0" ErrorMessage="Please Select Degree">
                                    </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%" class="form_left_label">
                                        Excel File<span style="color: #FF0000">*</span>
                                    </td>
                                    <td style="width: 75%" class="form_left_label">
                                        <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="1" ToolTip="Select file to Import" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <asp:Label ID="lblTotalMsg" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 75%">
                                        <asp:Button ID="btnUpload" runat="server" ValidationGroup="academic" OnClick="btnUpload_Click"
                                            TabIndex="2" Text="Upload" ToolTip="Click to Upload" />
                                        <asp:Button ID="btnCancel" runat="server" TabIndex="5" Text="Cancel" ToolTip="Click To Cancel"
                                            OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 75%">
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="academic" />
                                        <tr>
                                            <td class="form_left_label">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="width: 20%">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 2%">
                                                        </td>
                                                        <td style="width: 78%">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpload" />
                        </Triggers>
                    </asp:UpdatePanel>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
