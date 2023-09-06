<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="MultipleMonthsSalaryReport.aspx.cs" Inherits="MultipleMonthsSalaryReport" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">MULTIPLE MONTH SALARY REPORT</h3>
                    <p class="text-center">
                    </p>
                    <div class="box-tools pull-right">
                    </div>
                </div>

                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                            <div class="panel panel-info">
                                <div class="panel-heading">Multiple Month Salary Report</div>
                                <div class="panel-body">
                                    <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                    <asp:Panel ID="pnl" runat="server">

                                        <div class="form-group col-md-12">
                                            <div class="col-md-6">
                                                <div class="col-md-10">
                                                    <label>From Date :<span style="color: Red">*</span></label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" ToolTip="Enter From Date" CssClass="form-control" />
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCalFromDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: hand" />
                                                        </div>

                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                                            Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                                                            ValidationGroup="Payroll" />
                                                        <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    </div>

                                                    <div class="col-md-10">
                                                        <label>To Date :<span style="color: Red">*</span></label>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtToDate" runat="server" ToolTip="Enter To Date" TabIndex="2" CssClass="form-control" />
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="imgCalToDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: hand" />
                                                            </div>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" />

                                                            <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                                                Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                                                                ValidationGroup="Payroll" />
                                                        </div>

                                                    </div>
                                                    <div class="col-md-10">
                                                        <label>Staff Type:</label>

                                                        <asp:DropDownList ID="ddlStaffNo" runat="server" TabIndex="3" ToolTip="Select Staff Type" CssClass="form-control" AutoPostBack="true"
                                                            AppendDataBoundItems="True"
                                                            OnSelectedIndexChanged="ddlStaffNo_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-10">
                                                        <label>Employee :</label>

                                                        <asp:DropDownList ID="ddlEmployeeNo" runat="server" AppendDataBoundItems="True" TabIndex="4" ToolTip="Select Employee" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                  
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>

                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report"
                                    OnClick="btnShowReport_Click" ValidationGroup="Payroll" TabIndex="5" ToolTip="Click To Show Report" CssClass="btn btn-info" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    CssClass="btn btn-danger" TabIndex="6" ToolTip="Click To Reset" />
                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                    DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                            </div>
                        </div>

                    </div>
                </form>
            </div>

        </div>


    </div>





    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <%-- <tr>
            <td>
                <table class="vista_page_title_bar" width="100%">
                    <tr>
                        <td style="height: 30px">MULTIPLE MONTH SALARY REPORT&nbsp;&nbsp
                            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                AlternateText="Page Help" ToolTip="Page Help" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        <tr>
            <td>
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right;">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
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
            </td>
        </tr>
        <tr>
            <td align="center"></td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                <fieldset class="fieldsetPay" style="width: 80%">
                    <legend class="legendPay"></legend>
                    <table width="80%" cellpadding="2" cellspacing="2" border="0">
                        <tr>
                            <td class="form_left_label"><%--From Date :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" Width="80px" />
                                &nbsp;<asp:Image ID="imgCalFromDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: hand" />
                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                </ajaxToolKit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                    Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                                    ValidationGroup="Payroll" />
                                <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label"><%--To Date :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" Width="80px" />
                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                                </ajaxToolKit:CalendarExtender>
                                <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                <asp:Image ID="imgCalToDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: hand" />
                                <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                    Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                                    ValidationGroup="Payroll" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label"><%--Staff Type:
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlStaffNo" runat="server" Width="70%" AutoPostBack="true"
                                    AppendDataBoundItems="True"
                                    OnSelectedIndexChanged="ddlStaffNo_SelectedIndexChanged">
                                </asp:DropDownList>--%>
                            </td>
                        </tr>

                        <tr>
                            <td class="form_left_label"><%--Employee :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlEmployeeNo" runat="server" AppendDataBoundItems="True" Width="300px">
                                </asp:DropDownList>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <%--<asp:Button ID="btnShowReport" runat="server" Text="Show Report"
                                    OnClick="btnShowReport_Click" ValidationGroup="Payroll" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    Width="25%" />--%>
                            </td>
                        </tr>
                    </table>
                    <br />
                </fieldset>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
