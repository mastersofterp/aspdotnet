<%@ Control Language="C#" AutoEventWireup="true" CodeFile="masters.ascx.cs" Inherits="Masters_masters" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolKit" %>

<link href="../Css/master.css" rel="stylesheet" type="text/css" />
<link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />


<div class="row">
    <div class="col-md-12">
         <asp:Panel ID="Panel1" runat="server">
              <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3><asp:Label id="lblPageTitle" runat="server" /></h3>
                    <div class="box-tools pull-right">
                    </div>
                </div>
                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <asp:Label ID="lblmsg" runat="server" SkinID="Errorlbl" ></asp:Label>
                        </div>
                        <div class="col-md-12">
                             <asp:PlaceHolder ID="phAdd" runat="server" />  
                        </div>
                    </div>
                    </form>
                  <div class="box-footer">
                      <p class="text-center">
                          <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="submit" ToolTip="Submit" CssClass="btn btn-success" />
                          <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" ToolTip="Cancel" CssClass="btn btn-danger" />
                          <asp:Button ID="btnShowReport" runat="server" Text="Report" CausesValidation="False" ToolTip="Show Report" CssClass="btn btn-info" />
                          <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                          <div class="col-md-12">
                              <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                          </div>
                          <div class="col-md-12">
                              <asp:Panel ID="pnlList" runat="server">

                                 <%-- CHANGES BY SUMIT 10102019--%>
                                 <div id="gvTitle" runat="server" style="font-size:16px;">
                                 </div>
                                      
                             
                                     <div id="demo-grid" class="vista-grid">
                                         <asp:Panel ID="pnlGrid" runat="server" Style="overflow: auto;">
                                             <asp:PlaceHolder ID="phList" runat="server" />
                                         </asp:Panel>
                                     </div>
                                   
                                     
                              </asp:Panel>
                          </div>
                          <div id="divMsg" runat="server">
                          </div>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                          <p>
                          </p>
                      </p>
                  </div>
                  </div>             
         </asp:Panel>
    </div>
</div>

<table cellpadding="0" cellspacing="0" style="width:100%; margin:auto;">
    <%--<tr>
        <td class="vista_page_title_bar" style="height:30px" colspan="2"> 
           <asp:Label id="lblPageTitle" runat="server" />
           <!-- Button used to launch the help (animation) -->
            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;" AlternateText="Page Help" ToolTip="Page Help" />
        </td>
    </tr>--%>

    <%--PAGE HELP--%>
    <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
    <%--<tr>
         <td colspan="2">
            <!-- "Wire frame" div used to transition from the button to the info panel -->
            <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;"></div>
        
            <!-- Info panel to be displayed as a flyout when the button is clicked -->
            <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                    <asp:LinkButton id="btnClose" runat="server" OnClientClick="return false;" Text="X" ToolTip="Close"
                        Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                </div>
                <div>
                    <p class="page_help_head">
                       <span style="font-weight:bold;text-decoration: underline;">Page Help</span><br />
                       <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" /> Edit Record&nbsp;&nbsp;
                       <br />The above button is used for selecting a record to modify.<br />                       
                    </p>
                    <p class="page_help_text"><asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
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
            
            <ajaxToolkit:AnimationExtender id="OpenAnimation" runat="server" TargetControlID="btnHelp">
                <Animations>
                    <OnClick>
                        <Sequence>
                            <EnableAction Enabled="false" />
                            
                            <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                            
                            <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                            <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                            <FadeIn AnimationTarget="info" Duration=".2"/>
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                            
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
            </ajaxToolkit:AnimationExtender>
            <ajaxToolkit:AnimationExtender id="CloseAnimation" runat="server" TargetControlID="btnClose">
                <Animations>
                    <OnClick>
                        <Sequence AnimationTarget="info">
                            <StyleAction Attribute="overflow" Value="hidden"/>
                            <Parallel Duration=".3" Fps="15">
                                <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                <FadeOut />
                            </Parallel>
                            
                            <StyleAction Attribute="display" Value="none"/>
                            <StyleAction Attribute="width" Value="250px"/>
                            <StyleAction Attribute="height" Value=""/>
                            <StyleAction Attribute="fontSize" Value="12px"/>
                            <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                            
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
            </ajaxToolkit:AnimationExtender>
            
         </td>
    </tr>--%>
    
    <%--ADD/UPDATE MASTERS --%>
   <asp:Panel ID="pnlAdd" runat="server">   
   <tr>
    <td colspan="2" align="center">
        <%--<asp:Label ID="lblmsg" runat="server" SkinID="Errorlbl" ></asp:Label>--%>
    </td>
   </tr>
   
   <tr>
    <td colspan="2">
       <%-- <asp:PlaceHolder ID="phAdd" runat="server" />--%>
    </td>
   </tr>
    
    <tr>
        <td class="form_left_label">&nbsp;</td>
        <td class="form_left_text">
           <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="submit" ToolTip="Submit" Width="80px" />&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" ToolTip="Cancel" Width="80px"/>&nbsp;
            <asp:Button ID="btnShowReport" runat="server" Text="Report" CausesValidation="False" ToolTip="Show Report" Width="80px"/>&nbsp;
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List"/>--%>
        </td>
          
    </tr>
    <tr>
        <td colspan="2" class="form_button">
           <%-- <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />--%>
        </td>
    </tr>    
    </asp:Panel>

    <%--MASTERS LIST--%>
  <%--  <asp:Panel ID="pnlList" runat="server">           --%>                   
        <tr>
             <td style="padding-left:10px">
                <%--GRIDVIEW--%>
                <%--<div id="demo-grid" class="vista-grid">
                    <div id="gvTitle" runat="server" class="titlebar"></div>
                    <asp:Panel ID="pnlGrid" runat="server" style="overflow:auto;" Height="400px">
                        <asp:PlaceHolder ID="phList" runat="server" />--%>
                        <%--<asp:GridView ID="gv" runat="server"></asp:GridView>--%>
         <%--           </asp:Panel>
                </div>
            </td>
        </tr>
</asp:Panel>--%>
 
</table>


