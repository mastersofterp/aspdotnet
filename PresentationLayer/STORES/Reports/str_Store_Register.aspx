<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="str_Store_Register.aspx.cs" Inherits="STORES_Reports_str_Store_Register" Title="Untitled Page" %>

<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
    <%-- Flash the text/border red and fade in the "close" button --%>

    <%--    <script src="../Scripts/jquery.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>
    <link href="../Scripts/impromptu.css" rel="stylesheet" type="text/css" />--%>
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STORE REGISTER</h3>

                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlStrConfig" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                      <%--  <div class="sub-heading"><h5>STORE REGISTER</h5></div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgCal">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFrmDt" runat="server" CssClass="form-control" ToolTip="Enter From Date" TabIndex="1" />
                                                <%-- <div class="input-group-addon">
                                                        <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>--%>

                                                <ajaxToolKit:CalendarExtender ID="ceQuotDt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrmDt"
                                                    PopupButtonID="imgCal" Enabled="true" EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>

                                                <ajaxToolKit:MaskedEditExtender ID="meQuotDate" runat="server" TargetControlID="txtFrmDt"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevtodate" runat="server" ControlExtender="meQuotDate" ControlToValidate="txtFrmDt"
                                                    EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date" SetFocusOnError="True"
                                                    ValidationGroup="submit" IsValidEmpty="false"> </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="Image1">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtTodt" runat="server" TabIndex="2" Text="" CssClass="form-control" ToolTip="Enter To Date" />
                                                <%--   <div class="input-group-addon">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>--%>

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtTodt" PopupButtonID="Image1" Enabled="true" EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtTodt"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtTodt"
                                                    EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date" SetFocusOnError="True"
                                                    ValidationGroup="submit" IsValidEmpty="false"> </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Vendor</label>
                                            </div>
                                            <asp:RadioButtonList ID="rblSelectAllVendor" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="True" TabIndex="3" OnSelectedIndexChanged="rblSelectAllVendor_SelectedIndexChanged">
                                                <asp:ListItem Enabled="true" Text="Particular" Value="1"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="All" Value="0" Selected="True"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                      
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="drpoDowntr" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Particular Vendor Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlVendor" runat="server" data-select2-enable="true" CssClass="form-control" ToolTip="Please Select Vendor name"
                                                AppendDataBoundItems="true " TabIndex="4">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCddlVendor" runat="server" ControlToValidate="ddlVendor"
                                                Display="None" ErrorMessage="Please Select Vendor name" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnGRR" runat="server" Text="Goods Receipt Register" TabIndex="5"
                                            ToolTip="Click To Genearate Goods Receipt Register" CssClass="btn btn-primary"
                                            ValidationGroup="submit" OnClick="btnGRR_Click" Visible="false" />
                                        <asp:Button ID="btnStrReg" runat="server" Text="Stores Register " TabIndex="6" ToolTip="Click To Stores Register"
                                            ValidationGroup="submit" OnClick="btnStrReg_Click" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnInwardreport" runat="server" Text="Inward Report"
                                            ValidationGroup="submit" TabIndex="7" CssClass="btn btn-info"
                                            OnClick="btnInwardreport_Click" Visible="false" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="8" ToolTip="Click To Cancel" CssClass="btn btn-warning"
                                            OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                </div>
            </div>





            <table width="100%" cellpadding="1" cellspacing="1" class="table-formdata">
                <tr>
                    <%--<td class="vista_page_title_bar" valign="top" style="height: 30px">STORE REGISTER&nbsp;
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
                    <td colspan="3" style="padding-left: 10px">
                        <asp:Panel Style="text-align: left; width: 90%;">
                            <%-- <fieldset class="fieldset" style="padding-left: 30px; padding-right: 30px">
                                <legend class="legend"></legend>--%>
                            <br />
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 5%"></td>
                                    <td style="width: 1%"></td>
                                    <td style="width: 20%">
                                        <%--<ajaxToolKit:MaskedEditValidator ID="mevQuotDate" runat="server" ControlExtender="meQuotDate"
                                                        ControlToValidate="txtFrmDt" EmptyValueMessage="Please Enter From Date" InvalidValueMessage=" From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="True" />--%>
                                    </td>
                                    <td style="width: 35%"></td>
                                </tr>
                                <tr>
                                    <td style="width: 5%"></td>
                                    <td style="width: 1%"></td>
                                    <td style="width: 20%">
                                        <%-- <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meQuotDate"
                                                        ControlToValidate="txtQuotDate" EmptyValueMessage="Please To Date" InvalidValueMessage=" To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="True" />--%>
                                    </td>
                                    <td style="width: 35%"></td>
                                </tr>
                                <tr>
                                    <td style="width: 5%"></td>
                                    <td style="width: 1%"></td>
                                    <td style="width: 20%"></td>
                                    <td style="width: 2%">
                                        <b></b>
                                    </td>
                                    <td style="width: 50%"></td>
                                    <td style="width: 35%"></td>
                                </tr>
                                <tr>
                                    <td style="width: 5%"></td>
                                    <td style="width: 1%"></td>
                                    <td style="width: 20%">
                                        <%--   <ajaxToolKit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                Enabled="True" TargetControlID="rfvCddlVendor">
                                            </ajaxToolKit:ValidatorCalloutExtender>--%>
                                    </td>
                                    <td style="width: 35%"></td>
                                </tr>
                                <tr>
                                    <td style="width: 5%"></td>
                                    <td style="width: 1%"></td>
                                    <td style="width: 20%"></td>
                                    <td style="width: 2%"></td>
                                    <td style="width: 35%"></td>
                                    <td style="width: 35%"></td>
                                </tr>
                                <tr>
                                    <td style="width: 5%"></td>
                                    <td style="width: 1%"></td>
                                    <td style="width: 20%">
                                        <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="submit" runat="server" />
                                    </td>
                                    <td style="width: 2%"></td>
                                    <td style="width: 80%"></td>
                                    <td style="width: 35%"></td>
                                </tr>
                            </table>
                            </fieldset>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

