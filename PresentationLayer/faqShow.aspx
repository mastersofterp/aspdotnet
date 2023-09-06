<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="faqShow.aspx.cs" Inherits="faqShow" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFAQ"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updFAQ" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">FAQ's</h3>
                        </div>

                        <div class="box-body">
                            <%--   <legend >FAQ's</legend>--%>
                            <asp:Panel ID="pnlQuestion" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Question</label>
                                            </div>
                                            <asp:TextBox ID="txtQuestion" runat="server" TextMode="MultiLine" MaxLength="1000"
                                                CssClass="form-control" Wrap="true" />
                                            <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" ControlToValidate="txtQuestion"
                                                Display="None" ErrorMessage="Please Enter Questions." ValidationGroup="faqShow">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Answer</label>
                                            </div>
                                            <asp:TextBox ID="txtAnswer" runat="server" TextMode="MultiLine" MaxLength="1000"
                                                CssClass="form-control" Wrap="true" />
                                            <asp:RequiredFieldValidator ID="rfvAnswer" runat="server" ControlToValidate="txtAnswer"
                                                Display="None" ErrorMessage="Please Enter Answer" ValidationGroup="faqShow">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                                ValidationGroup="faqShow" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnBack" runat="server" Text="Cancel" OnClick="btnBack_Click" CausesValidation="False"
                                                CssClass="btn btn-warning" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="faqShow"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlList" runat="server">
                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" CssClass="btn btn-primary">Add New Question & Answer</asp:LinkButton>
                                </div>
                                <div class="col-12">
                                    <asp:ListView ID="lvList" runat="server" OnItemDataBound="lvList_ItemDataBound">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>FAQ</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit
                                                            </th>
                                                            <%--<th>Delete
                                                </th>--%>
                                                            <th>Question
                                                            </th>
                                                            <th>Answer
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
                                                    <asp:Panel ID="pnlShowQA" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                        <asp:Image ID="imgExp" runat="server" ImageUrl="~/images/action_down.gif" Visible="false" />
                                                    </asp:Panel>

                                                    <asp:ImageButton ID="btnSelect" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("QID") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit" OnClick="btnSelect_Click" />

                                                </td>
                                                <td style="display: none">
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("QID") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("QUESTION")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ANSWER")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnlQues" runat="server" Visible="false" CssClass="collapsePanel">
                                                        <%--ANSWERS--%>
                                                        <asp:ListView ID="lvAnswers" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="demo-grid">
                                                                    <table class="table table-hover table-bordered">
                                                                        <tr class="bg-light-blue">
                                                                            <th>Ans
                                                                            </th>
                                                                            <th>Full Name
                                                                            </th>
                                                                            <th>Date
                                                                            </th>
                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr onmouseover="this.style.backgroundColor='#D3F5A9'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                    <td>
                                                                        <%# Eval("Title") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Fname")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("FDate","{0:dd-MMM-yyyy}")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <ajaxToolKit:CollapsiblePanelExtender ID="cpeQA" runat="server" ExpandDirection="Vertical"
                                                TargetControlID="pnlQues" ExpandControlID="pnlShowQA" CollapseControlID="pnlShowQA"
                                                ExpandedText="Hide Q&A" CollapsedText="Show Q&A" CollapsedImage="~/images/action_down.gif"
                                                ExpandedImage="~/images/action_up.gif" ImageControlID="imgExp" Collapsed="true">
                                            </ajaxToolKit:CollapsiblePanelExtender>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlShow" runat="server">
                                <div class="col-md-4">
                                    <label>User Name</label>
                                    <div class="label">
                                        <asp:Label ID="lblUserName" runat="server" Wrap="False" Font-Bold="True" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>Question</label>
                                    <div class="label">
                                        <asp:Label ID="lblQuestion" runat="server" Wrap="False" Font-Bold="True" />
                                    </div>
                                </div>

                                <div class="box-footer">
                                    <p class="text-center">
                                        <asp:Button ID="btnSaveAnswer" runat="server" Text="Save" OnClick="btnSaveAnswer_Click"
                                            ValidationGroup="faqShow" CssClass="btn btn-success" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            CausesValidation="False" CssClass="btn btn-danger" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="faqShow"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </p>
                                    <div id="div2" runat="server">
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   
    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: auto;">
        <%--    <tr>
        <td class="vista_page_title_bar" style="height: 30px" colspan="2">FAQ            
            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif"
                OnClientClick="return false;" AlternateText="Page Help" ToolTip="Page Help"
                Height="12px" />
        </td>
    </tr>--%>

        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
        <%--  <tr>
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
                       <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" /> Delete Record
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
                            
                            <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
                                <Move Horizontal="150" Vertical="-50" />
                                <Resize Width="260" Height="280" />
                                <Color PropertyKey="backgroundColor" StartValue="#AAAAAA" EndValue="#FFFFFF" />
                            </Parallel>
                            
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
    </tr>
    
   <asp:Panel ID="pnlQuestion" runat="server">   
     <tr>
        <td class="form_left_label">Question :</td>
        <td class="form_left_text"><asp:TextBox ID="txtQuestion" runat="server" textmode="MultiLine"
              MaxLength="1000" Width="250px"  Wrap="False" />  
             <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" 
                  ControlToValidate="txtQuestion" Display="None" ErrorMessage="Questions Required" 
                  ValidationGroup="faqShow"></asp:RequiredFieldValidator>          
          </td>
     </tr>
                             
     <tr>
       <td class="form_left_label">&nbsp;</td>
        <td class="form_left_text">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="faqShow" />&nbsp;&nbsp;
            <asp:Button ID="btnBack" runat="server" Text="Cancel" OnClick="btnBack_Click" CausesValidation="False" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server"  ValidationGroup="faqShow" ShowMessageBox="true" ShowSummary="false" DisplayMode="List"/>
         </td>
     </tr>  
  </asp:Panel>
    
   <asp:Panel ID="pnlList" runat="server">
        <tr>
            <td style="text-align:left;padding-left:50px;padding-top:10px" colspan="2"> 
                <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click">Ask a Question</asp:LinkButton>
            </td>
        </tr>
      <tr>
         <td align="center" colspan="2">
            <asp:ListView ID="lvList" runat="server" onitemdatabound="lvList_ItemDataBound">
                <LayoutTemplate>
                    <div id="demo-grid" class="vista-grid">
                        <div class="titlebar">FAQ</div>
                        <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                            <tr class="header">
                                <th>Action</th>
                                <th>Title</th>
                                <th>Full Name</th>
                                <th>Date</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server" /> 
                        </table>
                    </div>
                </LayoutTemplate>
                
                <ItemTemplate>
                    <tr class="item" >
                        <td>
                            <asp:Panel ID="pnlShowQA" runat="server" style="cursor: pointer; vertical-align: top;float:left"> 
                                <asp:Image ID="imgExp" runat="server" ImageUrl="~/images/action_down.gif"  />
                            </asp:Panel>&nbsp;
                            <asp:ImageButton ID="btnSelect" runat="server" ImageUrl="~/images/edit.gif" 
                                CommandArgument='<%# Eval("fid") %>' AlternateText="Select Record" ToolTip="Select Record" 
                                OnClick="btnSelect_Click"/>&nbsp;
                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" 
                                CommandArgument='<%# Eval("fid") %>' AlternateText="Delete Record" ToolTip="Delete Record" 
                                OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                        </td>
                        <td><%# Eval("Title") %></td>
                        <td><%# Eval("Fname")%></td>
                        <td><%# Eval("FDate","{0:dd-MMM-yyyy}")%></td>
                    </tr>
                                     
                    <tr>
                        <td colspan="4" style="text-align:left;padding-left:10px;padding-right:10px">
                            <asp:Panel ID="pnlQues" runat="server" CssClass="collapsePanel">
                                
                                 <asp:ListView ID="lvAnswers" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid" class="vista-grid">
                                            <table class="datatable" cellpadding="0" cellspacing="0" width="99%">
                                                <tr class="header">
                                                    <th>Ans</th>
                                                    <th>Full Name</th>
                                                    <th>Date</th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" /> 
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    
                                    <ItemTemplate>
                                        <tr class="item" onmouseover="this.style.backgroundColor='#D3F5A9'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td><%# Eval("Title") %></td>
                                            <td><%# Eval("Fname")%></td>
                                            <td><%# Eval("FDate","{0:dd-MMM-yyyy}")%></td>
                                        </tr>    
                                    </ItemTemplate>
                                 </asp:ListView>
                            </asp:Panel>
                        </td>
                    </tr>
                    
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeQA" runat="server" 
                        ExpandDirection="Vertical"
                        TargetControlID="pnlQues"
                        ExpandControlID="pnlShowQA"
                        CollapseControlID="pnlShowQA"
                        ExpandedText="Hide Q&A"
                        CollapsedText="Show Q&A" 
                        CollapsedImage="~/images/action_down.gif"
                        ExpandedImage="~/images/action_up.gif"
                        ImageControlID="imgExp"
                        Collapsed="true">
                    </ajaxToolkit:CollapsiblePanelExtender>
                </ItemTemplate>
            </asp:ListView>
        </td>
    </tr>
 </asp:Panel>
    
    <asp:Panel ID="pnlShow" runat="server">   
      <tr>
            <td class="form_left_label" style="height: 25px">User Name :</td>
            <td class="form_left_text" style="height: 25px"><asp:Label ID="lblUserName" runat="server" Wrap="False" Font-Bold="True" />
            </td>
         </tr>
         
         <tr>
            <td class="form_left_label" style="height: 25px">Question :</td>
            <td class="form_left_text" style="height: 25px"><asp:Label ID="lblQuestion" runat="server" Wrap="False" Font-Bold="True" />
            </td>
         </tr>
            
         <tr>
            <td class="form_left_label">Answer :</td>
            <td class="form_left_text"><asp:TextBox ID="txtAnswer" runat="server" textmode="MultiLine"
                  MaxLength="1000" Width="250px"  Wrap="False" />  
                 <asp:RequiredFieldValidator ID="rfvAnswer" runat="server" 
                      ControlToValidate="txtAnswer" Display="None" ErrorMessage="Answer Required" 
                      ValidationGroup="faqShow"></asp:RequiredFieldValidator>          
              </td>
         </tr>
                                 
         <tr>
           <td class="form_left_label">&nbsp;</td>
            <td class="form_left_text">
                <asp:Button ID="btnSaveAnswer" runat="server" Text="Save" OnClick="btnSaveAnswer_Click" ValidationGroup="faqShow" />&nbsp;&nbsp;        
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server"  ValidationGroup="faqShow" ShowMessageBox="true" ShowSummary="false" DisplayMode="List"/>
             </td>
         </tr>  
      </asp:Panel>     --%>
    </table>

    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
        TargetControlID="div" PopupControlID="div"
        OkControlID="btnOkDel" OnOkScript="okDelClick();"
        CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />

    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <img align="middle" src="Images/warning.png" alt="" /></td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-danger" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-danger" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>

</asp:Content>

