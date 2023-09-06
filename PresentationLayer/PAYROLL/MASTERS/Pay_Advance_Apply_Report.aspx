<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Advance_Apply_Report.aspx.cs" Inherits="PAYROLL_MASTERS_Pay_Advance_Apply_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ADVANCE APPLY REPORT</h3>
                </div>
                <div>
                    <form role="form">
                        <div class="box-body">
                            <div class="col-md-12">
                                Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>

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
                                                <asp:TextBox ID="txtTodt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter To Date"
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
                                        <div class="form-group col-md-4">
                                            <label>Status :</label>
                                            <asp:DropDownList ID="ddlstatus" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                ToolTip="Select Status Type" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select </asp:ListItem>
                                                <asp:ListItem Value="1">Approved</asp:ListItem>
                                                <asp:ListItem Value="2">Pending</asp:ListItem>

                                            </asp:DropDownList>
                                           <%--  <asp:RequiredFieldValidator ID="rfvddlSelect" runat="server" ControlToValidate="ddlstatus"
                                             Display="None" ErrorMessage="Please Select Status"  SetFocusOnError="True" InitialValue="0">
                                                        </asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>

                                    <div class="form-group col-md-12">
                                        <div class="form-group col-md-4"></div>
                                        <div class="form-group col-md-4" id="trbutshow" runat="server">
                                            <br />
                                            <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-info"
                                                ToolTip="Click here To Show Status" TabIndex="7" OnClick="btnShow_Click" />
                                            <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="btn btn-info" 
                                                ToolTip="Click here To Print Status" TabIndex="8" OnClick="btnprint_Click" />
                                            <%--OnClick="btnShow_Click"--%>
                                        </div>
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
                                                        <h4 class="box-title">Advance Apply Status
                                                        </h4>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th style="width:25px">Sr.No
                                                                    </th>
                                                                    <th style="width:85px">Emp Code
                                                                    </th>
                                                                    <th>Employee Name
                                                                    </th>
                                                                    <th style="width:180px">Department
                                                                    </th>
                                                                    <th style="width:120px"> Amount
                                                                    </th>
                                                                    <th style="width:120px">Approved/Rejected
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
                                                            <%# Eval("PFILENO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUBDEPT")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ADVANCEAMOUNT") %>
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
                                             <div class="vista-grid_datapager">
                                        <div class="text-center">
                                            <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvApprStatus" PageSize="10">
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
                                        </asp:Panel>

                                    </div>


                                </asp:Panel>

                                <asp:Panel ID="pnlButton" runat="server" Visible="false">
                                    <div class="form-group col-md-12">
                                        <p class="text-center">
                                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" TabIndex="30"
                                                CssClass="btn btn-success" ToolTip="Click here To Submit" />
                                            <%--OnClick="btnSave_Click"--%> 
                                            &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="31"
                                            CssClass="btn btn-danger" ToolTip="Click here to Reset" />
                                            <%--OnClick="btnCancel_Click"--%>
                                            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary"
                                                ToolTip="Click here to Go Back" TabIndex="32" />
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
     <div id="divMsg" runat="server">
    </div>
</asp:Content>

