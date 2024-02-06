<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MinorFacility.aspx.cs" Inherits="MinorFacility" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">MINOR FACILITY ENTRY</h3>
                        </div>
                        <%-- <div class="box box-info">--%>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlAdd" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading" style="font-weight: 600; font-size: 13px">Add/Edit Minor Facility Details</div>

                                            <div class="panel-body">
                                                 <div class="form-group col-lg-8 col-md-12 col-12">
                                                        <div class=" note-div">
                                                            <h5 class="heading">Note</h5>
                                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>* Marked Is Mandatory !</span></p>
                                                        </div>
                                                    </div>
                                                    <br />
                                                <div class="col-md-12">
                                                   
                                                    <div class="row">
                                                        <div class="form-group col-md-4">
                                                            <label>Minor Facility Name :<span style="color: #FF0000">*</span></label>
                                                            <asp:TextBox ID="txtFacilityName" runat="server" TabIndex="1" MaxLength="50" ToolTip="Enter Minor facility Name" CssClass="form-control" />
                                                            <%--onkeypress="return CheckAlphabet(event,this);"--%>
                                                            <asp:RequiredFieldValidator ID="rfvHolyType" runat="server" ControlToValidate="txtFacilityName"
                                                                Display="None" ErrorMessage="Please Enter Minor Facility Name" ValidationGroup="Facility"
                                                                SetFocusOnError="True">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                         <div class="form-group col-md-4">
                                                            <label>Facility Details :</label>
                                                            <asp:TextBox ID="txtDetail" runat="server" MaxLength="500" TabIndex="2"
                                                                TextMode="MultiLine" CssClass="textbox form-control" ToolTip="Enter Minor Facility Details" />

                                                        </div>
                                                    </div>
                                                       
                                                </div>
                                            </div>
                                    </asp:Panel>
                                    <div class="col-md-12 text-center">
                                        <asp:Panel ID="pnlList" runat="server">
                                            <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" ToolTip="Click Add New To Enter Minor Facility" Text="Add New" CssClass="btn btn-primary"></asp:LinkButton>
                                            <%-- <asp:Button ID="btnShowReport"  Visible="false" runat="server" Text=" Report" CausesValidation="false" OnClick="btnShowReport_Click"
                                                CssClass="btn btn-info" ToolTip="Click here to show the report" />--%>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer">
                                <asp:Panel ID="pnlbutton" runat="server">
                                    <p class="text-center">

                                        <asp:Button ID="btnSave" runat="server" TabIndex="3" Text="Submit" ValidationGroup="Facility"
                                            CssClass="btn btn-primary" OnClick="btnSave_Click" ToolTip="Click here to Submit" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                            OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="4" ToolTip="Click here to Reset" />&nbsp;
                                        <asp:Button ID="btnBack" runat="server" TabIndex="5" Text="Back" CausesValidation="false" ToolTip="Click here to go back to previous" OnClick="btnBack_Click"
                                            CssClass="btn btn-info" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Facility"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                    </p>
                                </asp:Panel>
                                <div class="col-md-12 table-responsive">
                                    <asp:ListView ID="lvFacility" runat="server">
                                        <LayoutTemplate>
                                            <div id="demp_grid" class="vista-grid">
                                                <div class="sub-heading">
                                                    <h5>Minor Facility List</h5>
                                                </div>
                                                <table class="table table-hover table-bordered table-striped nowrap display">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th style="width: 10px">Action
                                                            </th>
                                                            <th>Facility Name
                                                            </th>
                                                            <th style="width: 150px">Detail
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("MinFacilityNo") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                <asp:ImageButton ID="btnDelete" Visible="false" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("MinFacilityNo") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("MinFacilityName")%>
                                                </td>
                                                <td>
                                                    <%#Eval("MinFacilityDetail")%>
                                                </td>


                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                    <div class="vista-grid_datapager">
                                        <div class="text-center">
                                            <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvFacility" PageSize="10"
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
                                    </div>
                                </div>
                            </div>

                            <%--</div>--%>
                    </div>
                    </fieldset>
                                        
                </div>
            </div>
            </form>
            </div>

            </div>
            </div>
            </div>

































            <table width="100%" cellpadding="1" cellspacing="1" class="table-formdata">
                <div id="divMsg" runat="server"></div>
                <tr>
                    <%--<td class="vista_page_title_bar" style="border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;" colspan="6">LEAVE TYPE ENTRY--%>
                    <%-- <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />--%>
                    <div id="Div1" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                    </div>
                    </td>
                </tr>

                <tr>
                    <td>
                        <!-- "Wire frame" div used to transition from the button to the info panel -->
                        <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                        </div>
                        <!-- Info panel to be displayed as a flyout when the button is clicked -->
                        <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                            <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                                <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                    ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                            </div>
                            <div>
                                <%--  <p class="page_help_head">
                                    <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                    <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                                    Edit Record
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                                    Delete Record
                                </p>--%>
                                <p class="page_help_text">
                                    <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                                </p>
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

                        <%--<ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
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
                        </ajaxToolKit:AnimationExtender>--%>
                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%"></td>
                    <td style="width: 1%"></td>
                    <%--<td colspan="4">Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />--%>
                    </td>
                </tr>
            </table>
            <center>
      
                <%--<asp:Panel ID="pnlAdd" runat="server" Width="90%">--%>
                 
                        <fieldset >
                           <%-- <legend class="legend">Leave Type Entry</legend>   --%>                         
                              <table width="100%" cellpadding="1" cellspacing="1" border="0">
                                <%--<tr align="center" >
                                   <td style="width: 15%; padding-left:20%" align="left" >
                                     Leave Name <span style="color: #FF0000">*</span>
                                   </td>
                                   <td style ="width:1%"><b>:</b></td>
                                    <td style="width: 15%" align="left">
                                         <asp:TextBox ID="txtFacilityName" runat="server" MaxLength="50" Width="300px" onkeypress="return CheckAlphabet(event,this);" CssClass="textbox"/>
                                         <asp:RequiredFieldValidator ID="rfvHolyType" runat="server" ControlToValidate="txtFacilityName"
                                                        Display="None" ErrorMessage="Please Enter Leave Name" ValidationGroup="Facility"
                                                        SetFocusOnError="True">
                                         </asp:RequiredFieldValidator>
                                    </td>
                                </tr>--%>
                                <%-- <tr align="center" >
                                   <td style="width: 15%; padding-left:20%" align="left">
                                      Short Name <span style="color: #FF0000">*</span>
                                   </td>
                                   <td style ="width:1%"><b>:</b></td>
                                   <td style="width: 15%" align="left">
                                         <asp:TextBox ID="txtshortname" runat="server" MaxLength="50" Width="300px" onkeypress="return CheckAlphabet(event,this);" CssClass="textbox"/>
                                                    
                                                    <asp:RequiredFieldValidator ID="rfvshrtname" runat="server" ControlToValidate="txtshortname" 
                                                        Display="None" ErrorMessage="Please Enter Short Name" ValidationGroup="Facility"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>--%>
                                 <%--<tr align="center" >
                                       <td style="width: 15%; padding-left:20%" align="left" >
                                    Max Days<span style="color: #FF0000">*</span>
                                   </td>
                                   <td style ="width:1%"><b>:</b></td>
                                  <td style="width: 15%" align="left">
                                      <asp:TextBox ID="txtmaxday" runat="server" MaxLength="5" Width="150px" onkeypress="return CheckNumeric(event,this);" CssClass="textbox"/>
                                          <asp:RequiredFieldValidator ID="rfvmaxdays" runat="server" ControlToValidate="txtmaxday"
                                                        Display="None" ErrorMessage="Please Enter Max Days" ValidationGroup="Facility"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    
                                                                            
                                    </td>
                                </tr>--%>
                                  
                                 <%--<tr align="center" >
                                      <td style="width: 15%; padding-left:20%" align="left">
                                  Period<span style="color: #FF0000">*</span>
                                   </td>
                                   <td style ="width:1%"><b>:</b></td>
                                   <td style="width: 15%" align="left">
                                     <asp:RadioButtonList ID="rdbYearly" runat="server" RepeatDirection="Horizontal" Autopostback="true">
                                            <asp:ListItem Enabled="true" Text="Yearly" Value="Max_Days"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Half Yearly" Value="Yearly"></asp:ListItem>
                                                 </asp:RadioButtonList>
                                                 
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdbYearly"
                                                        Display="None" ErrorMessage="Please Select Period" ValidationGroup="Facility"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>--%>
                               
                                <tr>
                                    <td align="center" style="width: 499px">
                                        &nbsp
                                    </td>
                                </tr>
                                </table>
                        </fieldset>                  
                </asp:Panel>
              </br>
                               <center>
                                            <%--<asp:Panel ID="pnlbutton" runat="server" Width="90%">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Facility"
                                            Width="80px" onclick="btnSave_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                            OnClick="btnCancel_Click" Width="80px" />&nbsp;
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                            Width="80px" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Facility"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </asp:Panel>--%>
                                   </center>
                               <%-- <tr>
                                    <td align="center" colspan="2" style="width: 599px">
                                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                    </td>
                                </tr>--%>
                            
           
        
     
               <%-- <asp:Panel ID="pnlList" runat="server">--%>
                    <table cellpadding="0" cellspacing="0" style="width: 90%; text-align: center">
                        <%--<tr>
                            <td style="text-align: left; padding-left: 10px; padding-top: 10px;">
                              
                               <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click">Add New</asp:LinkButton>
                                
                                  <asp:Button ID="btnShowReport" runat="server" Text="Report" CausesValidation="false" OnClick="btnShowReport_Click"
                                            Width="80px" />
                               
                                
                              
                                              
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <%--<asp:ListView ID="lvFacility" runat="server">--%>
                                    <EmptyDataTemplate>
                                        <br />
                                        <%--<asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter Leave Name" />--%>
                                    </EmptyDataTemplate>
                                   <%-- <LayoutTemplate>
                                        <div id="demp_grid" class="vista-grid">
                                            <div class="titlebar">
                                               Leave Types</div>
                                            <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                <tr class="header">
                                                    <th>
                                                        Action
                                                    </th>
                                                    <th>
                                                        Leave Name
                                                    </th>
                                                    <th>
                                                       Leave Short Name
                                                    </th>
                                                    <th>
                                                      Max Days
                                                    </th>
                                                    <th>
                                                     Yearly/Half
                                                    </th>
                                                    
                                                    
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>--%>
                                   <%-- <ItemTemplate>
                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("LVNO") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("LVNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />
                                            </td>
                                            <td>
                                                <%# Eval("Leave_Name")%>
                                            </td>
                                            <td>
                                              <%#Eval("Leave_ShortName")%>
                                            </td>
                                            <td>
                                              <%#Eval("Max_Days")%>
                                            </td>
                                            <td>
                                             <%#Eval("Yearly")%>
                                            </td>
                                           
                                            
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("LVNO") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("LVNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />
                                            </td>
                                            <td>
                                                <%# Eval("Leave_Name")%>
                                            </td>
                                            <td>
                                              <%#Eval("Leave_ShortName")%>
                                            </td>
                                            <td>
                                              <%#Eval("Max_Days")%>
                                            </td>
                                            <td>
                                             <%#Eval("Yearly")%>
                                                                                
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>--%>
                                       
                               <%-- <div class="vista-grid_datapager">
                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvFacility" PageSize="10"
                                OnPreRender="dpPager_PreRender">
                                <Fields>
                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<"  ButtonType="Link"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText= ">"  ButtonType="Link"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                
                                </Fields>
                                </asp:DataPager>
                                </div>--%>
                            </td>
                        </tr>
                        </table>
                </asp:Panel>
   
      </center>
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <%--  Enable the button so it can be played again --%>            <%# Eval("Leave_Name")%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />

    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
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
        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }

        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }

    </script>
</asp:Content>

