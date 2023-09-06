<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Advance_Process_Approval.aspx.cs" Inherits="PAYROLL_MASTERS_Pay_Advance_Process_Approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ADVANCE APPROVAL</h3>
                </div>
                <div>
                    <form role="form">
                        <div class="box-body">
                            <div class="col-md-12">
                                 Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                <asp:Panel ID="pnllist" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel panel-heading"> Advance Apply  List</div>
                                        <div class="panel panel-body">
                                            <asp:Panel ID="pnlPending" runat="server" ScrollBars="Auto">
                                                <asp:ListView ID="lvPendingList" runat="server">
                                                    <EmptyDataTemplate>
                                                        <p class="text-center text-bold">
                                                            <asp:Label ID="lblErr" runat="server" Text=" No more Pending List of Advance Apply for Approval">
                                                            </asp:Label>
                                                        </p>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="lgv1">
                                                            <h4 class="box-title">Pending List 
                                                            </h4>
                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th style="width:50px">Action
                                                                        </th>
                                                                        <th>Sr.No.
                                                                        </th>
                                                                        <th>Employee Name
                                                                        </th>
                                                                        <th>Reason
                                                                        </th>
                                                                        <th style="width:110px"> Amount
                                                                        </th>                                                                      
                                                                        <th style="width:100px"> Date
                                                                        </th>
                                                                         <th style="width:120px">Approve/Reject
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
                                                                    <asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" TabIndex="1" />
                                                                </asp:Panel>
                                                            </td>
                                                            <td style="width:25px">
                                                                <%# Eval("sno")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("NAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("REASON")%>
                                                            </td>
                                                            
                                                            <td>
                                                                <%# Eval("ADVANCEAMOUNT") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ADVCANEDATE") %>
                                                            </td>
                                                            <td style="text-align:center">
                                                                <asp:Button ID="btnApproval" runat="server" Text="Select" CommandArgument='<%# Eval("ANO")%>' TabIndex="2"
                                                                    ToolTip="Select to Approve/Reject" CssClass="btn btn-primary" OnClick="btnApproval_Click" />
                                                                <%--OnClick="btnApproval_Click"--%> 
                                                            </td>
                                                           
                                                        </tr>
                                                        <tr>
                                                            <td colspan="9" style="text-align: left; padding-left: 10px; padding-right: 10px">
                                                                <asp:Panel ID="pnlQues" runat="server" CssClass="collapsePanel">
                                                                    <table class="table table-bordered table-hover">
                                                                        <tr class="bg-light-blue">
                                                                            <th>Reason
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("REASON") %>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <ajaxToolKit:CollapsiblePanelExtender ID="cpeQA" runat="server" ExpandDirection="Vertical"
                                                            TargetControlID="pnlQues" ExpandControlID="pnlShowQA" CollapseControlID="pnlShowQA"
                                                            ExpandedText="Hide Reason" CollapsedText="Show Reason" CollapsedImage="~/Images/action_down.png"
                                                            ExpandedImage="~/images/action_up.gif" ImageControlID="imgExp" Collapsed="true">
                                                        </ajaxToolKit:CollapsiblePanelExtender>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                            <div class="vista-grid_datapager">
                                                <div class="text-center">
                                                    <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvPendingList" PageSize="10"
                                                        >
                                                        <%--OnPreRender="dpPager_PreRender"--%>
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
                                </asp:Panel>

                                <div class="form-group col-md-12">
                                    <asp:LinkButton ID="lnkbut" runat="server" Text=" Approval Status" CssClass="btn btn-primary"
                                        ToolTip="Click here for Advance Approval Status" TabIndex="4" OnClick="lnkbut_Click"></asp:LinkButton>
                                     <%--OnClick="lnkbut_Click"--%>
                                </div>
                                
                                <asp:Panel ID="pnlODStatus" runat="server">
                                    <%--<div class="panel panel-info">
                                        <div class="panel panel-heading">Approval Status</div>
                                        <div class="panel panel-body">--%>
                                    <div id="trfrmto" runat="server">
                                        <div class="form-group col-md-4">
                                            <label>From Date :</label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <asp:Image ID="imgCalFromdt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                </div>
                                                <asp:TextBox ID="txtFromdt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Form Date"
                                                    Style="z-index: 0;" TabIndex="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromdt"
                                                    Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalFromdt" TargetControlID="txtFromdt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" TargetControlID="txtFromdt" />
                                            </div>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>To Date :</label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <asp:Image ID="imgCalTodt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                </div>
                                                <asp:TextBox ID="txtTodt" runat="server" AutoPostBack="true" MaxLength="10" CssClass="form-control" ToolTip="Enter To Date"
                                                    Style="z-index: 0;" TabIndex="6"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                                    Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtTodt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" TargetControlID="txtTodt" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-4" id="trbutshow" runat="server">
                                        <br />
                                        <asp:Button ID="btnShow" runat="server" Text="Show"  CssClass="btn btn-info"
                                            ToolTip="Click here To Show Status" TabIndex="7" OnClick="btnShow_Click" />
                                        <%--OnClick="btnShow_Click"--%>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <asp:Panel ID="pnlStatusList" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvApprStatus" runat="server">
                                                <EmptyDataTemplate>
                                                    <p class="text-center text-bold">
                                                        <asp:Label ID="ibler" runat="server" Text="No more Advance process aaplication"></asp:Label>
                                                    </p>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <h4 class="box-title"> Apply Status
                                                        </h4>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    
                                                                    <th>Employee Name
                                                                    </th>                                                                    
                                                                    <th>Reason
                                                                    </th>
                                                                    <th>Advance Amount
                                                                    </th>
                                                                     <th>Advance Approval Date
                                                                    </th>
                                                                    
                                                                    <th>Status
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
                                                            <%# Eval("PAusername")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("REASON")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ADVANCEAMOUNT")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ADV_date")%>
                                                        </td>
                                                       
                                                       
                                                        <td>
                                                            <%# Eval("STATUS") %>
                                                        </td>
                                                        <%--<td>
                                                                <asp:Button ID="btnRPT" runat="server" Text="Report" CommandArgument='<%# Eval("ODTRNO")%>'
                                                                 ToolTip='<%# Eval("ODTRNO")%>'  />
                                                            </td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <%-- <div class="vista-grid_datapager">
                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvStatus" PageSize="10"
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
                                </div>--%>
                                        </asp:Panel>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <div class="text-center">
                                            <asp:Button ID="btnHidePanel" runat="server" Text="Back" CssClass="btn btn-primary" ToolTip="Click here to Go Back"
                                              TabIndex="8" OnClick="btnHidePanel_Click" />
                                              <%--OnClick="btnHidePanel_Click"--%> 
                                        </div>
                                    </div>
                                    <%-- </div>
                                    </div>--%>
                                </asp:Panel>

                                 <div class="form-group col-md-8" id="pnlvedit" runat="server">
                               <%-- <asp:Panel ID="pnlvedit" runat="server">--%>
                                    <div id="trType" runat="server">
                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-3">
                                                <label>Advance Type :</label>
                                            </div>
                                            <div class="form-group col-md-9">
                                                <asp:RadioButtonList ID="rblleavetype" runat="server" AutoPostBack="true"
                                                    RepeatDirection="Horizontal" TabIndex="9" ToolTip="Select Full Day or Half Day"
                                                   >
                                                     <%--OnSelectedIndexChanged="rblleavetype_SelectedIndexChanged"--%>
                                                    <asp:ListItem Enabled="true" Selected="True" Text="Full Day" Value="0"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="Half Day" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12" id="divHalfCriteria" runat="server" visible="false">
                                            <div class="form-group col-md-3" id="tdHalfCriteriaLable1" runat="server">
                                                <label>Half Day Criteria :</label>
                                            </div>
                                            <div class="form-group col-md-9" id="tdHalfCriteriaLable2" runat="server" >
                                                <div id="tdHalfCriteriaLable3" runat="server" >
                                                    <asp:DropDownList ID="ddlLeaveFNAN" runat="server" AppendDataBoundItems="true" TabIndex="10"
                                                        CssClass="form-control" ToolTip="Select Half Day Criteria" AutoPostBack="true"
                                                        >
                                                        <%--OnSelectedIndexChanged="ddlLeaveFNAN_SelectedIndexChanged"--%>
                                                        <asp:ListItem Value="0">AN</asp:ListItem>
                                                        <asp:ListItem Value="1">FN</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>     
                                      <div class="form-group col-md-12" id="divml" runat="server" visible="false">
                                        <div class="form-group col-md-3">
                                            <label>Medical Advance :</label>
                                        </div>
                                        <div class="form-group col-md-9">
                                            <asp:RadioButtonList ID="rdbml" runat="server" AutoPostBack="true" 
                                                        RepeatDirection="Horizontal"   > <%--onselectedindexchanged="rdbml_SelectedIndexChanged"--%>
                                                        <asp:ListItem Enabled="true" Selected="True" Text="Full Pay" Value="0"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Half Pay" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                        </div>
                                    </div>
                                                                  
                                    <div class="form-group col-md-12">
                                        <div class="form-group col-md-3">
                                            <label>Advance Name :</label>
                                        </div>
                                        <div class="form-group col-md-9">
                                            <asp:TextBox ID="txtLeavename" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="11"
                                                ToolTip="Advance Name">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <div class="form-group col-md-3">
                                            <label>Bal.Advance :</label>
                                        </div>
                                        <div class="form-group col-md-9">
                                            <asp:TextBox ID="txtLeavebal" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="12"
                                                ToolTip="Balance Advance" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <div class="form-group col-md-3">
                                            <label>From Date :</label>
                                        </div>
                                        <div class="form-group col-md-9">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <asp:Image ID="imgFromdt" runat="server" ImageUrl="~/images/calendar.png"
                                                        Style="cursor: pointer" />
                                                </div>
                                                <asp:TextBox ID="txtfrmdt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Advance From Date"
                                                    Style="z-index: 0;" TabIndex="13"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFromdt" runat="server"
                                                    ControlToValidate="txtfrmdt" Display="None"
                                                    ErrorMessage="Please Enter From Date" SetFocusOnError="true"
                                                    ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="ceFromdt" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgFromdt"
                                                    TargetControlID="txtfrmdt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    TargetControlID="txtfrmdt" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevFromdt" runat="server"
                                                    ControlExtender="meeFromdt" ControlToValidate="txtfrmdt" Display="None"
                                                    EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date"
                                                    InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    SetFocusOnError="true" TooltipMessage="Please Enter From Date"
                                                    ValidationGroup="Leaveapp">
                                            &nbsp;&nbsp;
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="form-group col-md-12">
                                        <div class="form-group col-md-3">
                                            <label>To Date :</label>
                                        </div>
                                        <div class="form-group col-md-9">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <asp:Image ID="imgTodt" runat="server" ImageUrl="~/images/calendar.png"
                                                        Style="cursor: pointer" />
                                                </div>
                                                <asp:TextBox ID="txttodate" runat="server" AutoPostBack="true" MaxLength="10" Style="z-index: 0;" TabIndex="14"
                                                    CssClass="form-control" ToolTip="Enter Advance To Date" />
                                                 <%--OnTextChanged="txttodate_TextChanged"--%>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txttodate" Display="None" ErrorMessage="Please Enter To Date"
                                                    SetFocusOnError="true" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="ceTodate" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgTodt"
                                                    TargetControlID="txttodate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    TargetControlID="txttodate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevTodt" runat="server"
                                                    ControlExtender="meeTodt" ControlToValidate="txttodate" Display="None"
                                                    EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter To Date"
                                                    InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    SetFocusOnError="true" TooltipMessage="Please Enter To Date"
                                                    ValidationGroup="Leaveapp">
                                                &nbsp;&nbsp;
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="form-group col-md-12">
                                        <div class="form-group col-md-3">
                                            <label>No of Days :</label>
                                        </div>
                                        <div class="form-group col-md-9">
                                            <asp:TextBox ID="txtNodays" runat="server" AutoPostBack="true" MaxLength="5" TabIndex="15"
                                                CssClass="form-control" ToolTip="Enter Number Of Days" />
                                        </div>
                                    </div>
                                     <div class="form-group col-md-12">
                                        <div class="form-group col-md-3">
                                            <label>Joining Date :</label>
                                        </div>
                                        <div class="form-group col-md-9">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <asp:Image ID="imgCalJoindt" runat="server" ImageUrl="~/images/calendar.png"
                                                        Style="cursor: pointer" />
                                                </div>
                                                <asp:TextBox ID="txtJoindt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Joining Date"
                                                    Style="z-index: 0;" TabIndex="16" />
                                                <ajaxToolKit:CalendarExtender ID="CeJoindt" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalJoindt"
                                                    TargetControlID="txtJoindt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeJoindt" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    TargetControlID="txtJoindt" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevJoindt" runat="server"
                                                    ControlExtender="meeJoindt" ControlToValidate="txtJoindt" Display="None"
                                                    EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Joining Date"
                                                    InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="Joining Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    SetFocusOnError="true" TooltipMessage="Please Enter Joining Date"
                                                    ValidationGroup="Leaveapp">
                                            &nbsp;&nbsp;
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                    </div>
                                      <div class="form-group col-md-12">
                                               <div class="form-group col-md-3">
                                            <label>Select :</label></div>
                                               <div class="form-group col-md-9">
                                            <asp:DropDownList ID="ddlSelectModify" runat="server" CssClass="form-control" ToolTip="Select Approve/Reject"
                                                AppendDataBoundItems="true" TabIndex="28">
                                                <%--<asp:ListItem Value="A">Approve</asp:ListItem>
                                                <asp:ListItem Value="R">Reject</asp:ListItem>--%>
                                                 
                                                <asp:ListItem Value="A">Approve </asp:ListItem>
                                                 <asp:ListItem Value="R">Reject</asp:ListItem>
                                            </asp:DropDownList>
                                                   </div>
                                        </div>
                              <%--  </asp:Panel>--%>
                                     </div>

                              <%--  <asp:Panel ID="pnlAdd" runat="server">--%>
                               <%-- <div class="form-group col-md-12">--%>
                                 <div class="form-group col-md-8" id="pnlAdd" runat="server">
                                    <div class="form-group col-md-12">
                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-4">
                                            <label>Employee Name :</label>
                                                </div>
                                              <div class="form-group col-md-8" style="text-align:left">
                                            <asp:Label ID="lblEmpName" runat="server" TabIndex="17" ToolTip="Employee Name"></asp:Label>
                                                </div>
                                        </div>
                                   
                                      
                                        <div class="form-group col-md-12">
                                              <div class="form-group col-md-4">
                                            <label>Reason :</label>
                                                 </div>
                                               <div class="form-group col-md-8">
                                           
                                            <asp:Label ID="lblReason" runat="server" TabIndex="24" ToolTip="Reason"></asp:Label>
                                                   </div>
                                        </div>
                                             <div class="form-group col-md-12">
                                            <div class="form-group col-md-4">
                                            <label> Amount :</label>
                                                </div>
                                            <div class="form-group col-md-8">
                                            <asp:TextBox ID="txtAdvanceAmount" runat="server" CssClass="form-control" TabIndex="29"
                                                ToolTip="Enter Amount" />
                                                </div>
                                        </div>
                                     
                                        <div class="form-group col-md-12">
                                              <div class="form-group col-md-4">
                                            <label>Apply Date :</label>
                                                  </div>
                                               <div class="form-group col-md-8">
                                            <asp:Label ID="lblApplyDate" runat="server" TabIndex="26" ToolTip="Apply Date"
                                                Font-Bold="True">0</asp:Label>
                                                   </div>
                                        </div>
                                   
                                        <div class="form-group col-md-12">
                                               <div class="form-group col-md-4">
                                            <label>Select :</label></div>
                                               <div class="form-group col-md-8">
                                            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control" ToolTip="Select Approve/Reject"
                                                AppendDataBoundItems="true" TabIndex="28">
                                                                                         
                                                <asp:ListItem Value="A">Approve</asp:ListItem>
                                                 <asp:ListItem Value="R">Reject</asp:ListItem>
                                            </asp:DropDownList>
                                                   </div>
                                        </div>
                                        <div class="form-group col-md-12">
                                           <div class="form-group col-md-4">
                                            <label>Remarks :</label>
                                            </div>
                                            <div class="form-group col-md-8">
                                            <asp:TextBox ID="txtRemarks" runat="server"  MaxLength="100" TextMode="MultiLine" CssClass="form-control" TabIndex="29"
                                                ToolTip="Enter Remarks" />
                                            </div>
                                        </div>

                                     
                                    </div>
                                  </div> 

                                 <div class="form-group col-md-4" id="divAuthorityList" runat="server" visible="false">
                                        <asp:Panel ID="pnlSelectList" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvStatus" runat="server">
                                                <EmptyDataTemplate>
                                                    <br />
                                                    <asp:Label ID="ibler" runat="server" Text=""></asp:Label>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <h4 class="box-title">Approval Status
                                                        </h4>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Sr.No.
                                                                    </th>
                                                                    <th>Authority
                                                                    </th>
                                                                    <th>Authority Name
                                                                    </th>
                                                                    <th>Status
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
                                                            <%# Eval("sno")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Passing_Authority_Name")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PAusername")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("STATUS")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                              <%--  </div>--%>
                             <%--   </asp:Panel>--%>

                             <asp:Panel ID="pnlButton" runat="server" Visible="false">
                                     <div class="form-group col-md-12">
                                        <p class="text-center">
                                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" TabIndex="30"
                                                CssClass="btn btn-success" ToolTip="Click here To Submit" OnClick="btnSave_Click" />
                                            <%--OnClick="btnSave_Click"--%> 
                                            &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="31"
                                            CssClass="btn btn-danger" ToolTip="Click here to Reset" OnClick="btnCancel_Click"  />
                                            <%--OnClick="btnCancel_Click"--%>
                                            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-info"
                                                ToolTip="Click here to Go Back" TabIndex="32" OnClick="btnBack_Click" />
                                             <%--OnClick="btnBack_Click"--%>
                                        </p>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <%--<td class="vista_page_title_bar" valign="top" style="height: 30px">Advance Approval &nbsp;               
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>--%>
        </tr>
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
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
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgSReason" runat="server" ImageUrl="~/Images/action_down.png" AlternateText="Show Reason" />
                            Show Reason
                            <asp:Image ID="imgHReason" runat="server" ImageUrl="~/images/action_up.gif" AlternateText="Hide Reason" />
                            Hide Reason
                        </p>
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
            </td>
        </tr>
    </table>
</asp:Content>

