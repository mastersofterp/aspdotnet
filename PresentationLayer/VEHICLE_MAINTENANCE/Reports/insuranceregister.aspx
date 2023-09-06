<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="insuranceregister.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Reports_insuranceregister"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">INSURANCE REGISTER REPORT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Vehicle Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlVehicle" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Select Vehicle Name" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Insurance Company</label>
                                        </div>
                                        <asp:DropDownList ID="ddlInsurance" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Insurance Company" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Agent Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAgentName" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Agent Name" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Maturity From Date </label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="ImgBntCalc">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>

                                            <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control" ToolTip="Enter Maturity From Date"
                                                TabIndex="4" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd/MM/yyyy" PopupButtonID="ImgBntCalc" TargetControlID="txtFDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                OnInvalidCssClass="errordate" TargetControlID="txtFDate" ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt"
                                                ControlToValidate="txtFDate" IsValidEmpty="true" Display="None" Text="*"
                                                ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="Submit"
                                                InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format">
                                            </ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Maturity To Date </label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="Image1789">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>

                                            <asp:TextBox ID="txtTDate" runat="server" CssClass="form-control" ToolTip="Enter Maturity To Date"
                                                TabIndex="5"/>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtTDate" PopupButtonID="Image1789">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtTDate"
                                                ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtTDate" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                IsValidEmpty="true" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                Display="None" Text="*" ValidationGroup="Submit">
                                            </ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Report In </label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoReportType" runat="server" ToolTip="Select Report In Type"
                                            RepeatDirection="Horizontal" TabIndex="6">
                                            <%-- <asp:ListItem Selected="True" Value="No Export">Normal Report</asp:ListItem>--%>
                                            <asp:ListItem Selected="True" Value="pdf">Adobe Reader&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="xls">MS-Excel&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="doc">MS-Word&nbsp;&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Report" ValidationGroup="Submit" TabIndex="7"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-info" ToolTip="Click here to Show Report" CausesValidation="true" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="8"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" HeaderText="Following Field(s) are mandatory" />


                            </div>
                        </div>
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
        <tr>
            <%--<td class="vista_page_title_bar" style="height: 30px">Insurance Register Report              
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>--%>
        </tr>
        <%-- Flash the text/border red and fade in the "close" button --%>        <%--  Shrink the info panel out of view --%>
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
                            Edit Record&nbsp;&nbsp;
                            <%--  Reset the sample so it can be played again --%>
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
    <br />


    <div id="divMsg" runat="server">
    </div>
</asp:Content>
