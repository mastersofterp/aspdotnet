<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="~/STORES/Reports/DSR_Report.aspx.cs" Inherits="Stores_Reports_DSR_Report" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>
    <link href="../Scripts/impromptu.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function confirmDeleteResult(v, m, f) {
            if (v) //user clicked OK 
                $('#' + f.hidID).click();
        }

    </script>

    <%--    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div3" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">DSR REPORT</h3>

                </div>

                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                            <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                <div class="panel panel-info">
                                    <div class="panel-heading">DSR INFO.</div>
                                    <div class="panel-body">


                                        <div class="form-group col-md-4">
                                            <label>Department:</label>
                                            <asp:DropDownList CssClass="form-control" ToolTip="Select Department" runat="server" ID="ddlDept" AutoPostBack="true" AppendDataBoundItems="true"
                                                TabIndex="1" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                <asp:ListItem Text="Please Select" Value="0">                                                                            
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>DSR Name:</label>
                                            <asp:DropDownList CssClass="form-control" ToolTip="Select DSR Name" runat="server" ID="ddlDSR" AutoPostBack="true" AppendDataBoundItems="true"
                                                TabIndex="2" OnSelectedIndexChanged="ddlDSR_SelectedIndexChanged">
                                                <asp:ListItem Text="Please Select" Value="0">
                                                                            
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Financial Year:</label>
                                            <asp:DropDownList ID="ddlSession" CssClass="form-control" ToolTip="Select Financial Year" runat="server" AutoPostBack="true"
                                                AppendDataBoundItems="true" TabIndex="3" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                <asp:ListItem Text="Please Select" Value="0" Selected="True" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-12">
                                            <h4>Print DSR .</h4>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>From Date: <span style="color: #FF0000">*</span></label>

                                            <div class="input-group">
                                                
                                                <div class="input-group-addon">
                                                    <asp:ImageButton ID="imgDate" runat="server" ImageUrl="~/IMAGES/calendar.png"  />
                                                </div>
                                                <asp:TextBox ID="txtFrom" runat="server"  Style="z-index: 0" CssClass="form-control" TabIndex="4" Text="" ToolTip="Enter From Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="caleDate" runat="server" Enabled="true"
                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgDate"
                                                        TargetControlID="txtFrom">
                                                    </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meDate" runat="server" DisplayMoney="Left" Enabled="true"
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtFrom">
                                                </ajaxToolKit:MaskedEditExtender>
                                                  <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meDate"
                                                                    ControlToValidate="txtFrom" EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                    Display="None" TooltipMessage="Enter From Date" EmptyValueBlurredText="Empty"
                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Print" SetFocusOnError="True" />

                                                 <asp:RequiredFieldValidator ID="rfvFromDt" runat="server" ControlToValidate="txtFrom" Display="None" ErrorMessage="Please Select From Date" ValidationGroup="Print"  SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>To Date: <span style="color: #FF0000">*</span></label>
                                            <div class="input-group">
                                              
                                                <div class="input-group-addon">
                                                    <asp:ImageButton ID="imgdate2" runat="server" ImageUrl="~/IMAGES/calendar.png"  />
                                                </div>
                                                  <asp:TextBox ID="txtTo" runat="server"  Style="z-index: 0" CssClass="form-control" TabIndex="5" Text="" ToolTip="Enter To Date"></asp:TextBox>
                                                  <ajaxToolKit:CalendarExtender ID="caleDate2" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgdate2"
                                                     TargetControlID="txtTo">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                                    Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtTo">
                                                </ajaxToolKit:MaskedEditExtender>
                                                 <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                                                    ControlToValidate="txtTo" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                    Display="None" TooltipMessage="Enter To Date" EmptyValueBlurredText="Empty"
                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Print" SetFocusOnError="True" />
                                              
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtTo" Display="None" ErrorMessage="Please Select To Date" ValidationGroup="Print"
                                                                    SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtFrom"
                                                                ControlToValidate="txtTo" Display="None" ErrorMessage="To Date should be greater than equal to From Date"
                                                                Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="StoreRepSearch"></asp:CompareValidator>

                                        </div>
                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="btnPrint" runat="server" Text="Print DSR" CssClass="btn btn-info" ToolTip="Click To Print" TabIndex="6" OnClick="btnPrint_Click"  ValidationGroup="Print"   />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click To Reset" TabIndex="7" />
                                             <asp:ValidationSummary ID="validationsummary" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Print" />
                                        </div>
                                    </div>
                                </div>


                            </asp:Panel>
                        </div>

                    </div>
                </form>
            </div>

        </div>

    </div>






    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <%-- <td style="background: #79c9ec url(images/ui-bg_glass_75_79c9ec_1x400.png) 50% 50% repeat-x; border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;"
                colspan="6">.
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                <div id="Div2" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
            </td>--%>
            <%--<td class="vista_page_title_bar" valign="top" style="height: 30px;">
                        DEAD STOCK REGISTER REPORT.
                        <!-- Button used to launch the help (animation) -->
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
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record
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
        <tr>
            <td></td>
        </tr>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <br />
                    <div id="div1" style="display: block;">
                        <table cellpadding="0" cellspacing="0" width="99%">
                            <tr>
                                <td>
                                    <%--<asp:Panel >--%>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <caption>
                                            <tr>
                                                <td colspan="2" style="padding-left: 10px;">
                                                    <table cellpadding="0" cellspacing="0" width="99%">
                                                        <tr>
                                                            <td colspan="2" style="padding-left: 10px">
                                                                <fieldset class="fieldset1" style="padding-left: 10px; padding-right: 10px;">
                                                                    <legend class="legend2 "></legend>
                                                                    <br />
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td class="gv_header" style="padding-left: 10px; width: 20%"></td>
                                                                            <td class="gv_header" style="padding-left: 10px; width: 15%"></td>
                                                                            <td class="gv_header" style="padding-left: 10px; width: 15%"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="gv_header" style="padding-left: 10px; width: 30%"></td>
                                                                            <td class="gv_header" style="padding-left: 10px; width: 22%"></td>
                                                                            <td class="gv_header" style="padding-left: 10px; width: 22%"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </caption>
                                        <tr>
                                            <td colspan="2" style="padding-left: 10px;">
                                                <table cellpadding="0" cellspacing="0" width="99%">
                                                    <tr>

                                                        <br />
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td class="gv_header"></td>
                                                                            <td class="gv_header"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr class="gv_header" align="center">
                                                                <td align="center"></td>
                                                            </tr>
                                                        </table>
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr class="gv_header" align="center">
                                                                <td align="center"></td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        </div>
                </td>
            </tr>
    </table>
    </table>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
