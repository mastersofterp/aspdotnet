<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PF_Ledger_Report.aspx.cs" Inherits="PAYROLL_REPORTS_PF_Ledger_Report"
    Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updPayBillReport" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div id="div2" runat="server" style="display:none">
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

                            </div>



                        <div class="box-header with-border">
                            <h3 class="box-title">PF PERSONAL LEDGER REPORT</h3>
                                        <%-- <div class="box-tools pull-right">
                                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                        AlternateText="Page Help" ToolTip="Page Help" />
                                </div>--%>
                        </div>
                        <div>
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnl" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Select Employee or Staff for PF Personal Ledger Report</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-4">
                                                            <asp:RadioButton ID="radEmployee" Font-Bold="true" Text="Selected Employee" runat="server" TabIndex="1"
                                                                GroupName="pfprocess" AutoPostBack="true" OnCheckedChanged="radEmployee_CheckedChanged" />

                                                            <asp:RadioButton ID="radStaff" Font-Bold="true" Text="Selected Staff" runat="server" TabIndex="2"
                                                                GroupName="pfprocess" AutoPostBack="true" OnCheckedChanged="radStaff_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-12" id="tblEmployee" runat="server">
                                                        <div class="form-group col-md-4" runat="server" id="Div3">
                                                             <label>College :</label>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="2" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee" ValidationGroup="PF"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                       
                                                    </div>
                                                        <div class="form-group col-md-4" runat="server" id="trEmployee">
                                                            <label>Employee :</label>
                                                            <asp:DropDownList runat="server" ID="ddlemployee" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="3"
                                                                CssClass="form-control" ToolTip="Select Employee" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee" ValidationGroup="PF"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-4" runat="server" id="trStaff">
                                                            <label>Staff :</label>
                                                            <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" ToolTip="Select Staff"
                                                                AutoPostBack="true" TabIndex="4">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                                Display="None" ErrorMessage="Select Staff" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Fin.Year Start Date :</label>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" ToolTip="Enter Fin.Year Start Date"
                                                                    OnTextChanged="txtFromDate_TextChanged" AutoPostBack="true" TabIndex="5"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                                    PopupPosition="BottomLeft">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                                    Display="None" ErrorMessage="Please Select Fin.Year Start Date in (dd/MM/yyyy Format)"
                                                                    ValidationGroup="PF" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                                    ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter Fin.Year Start Date"
                                                                    InvalidValueMessage="Fin.Year Start Date is Invalid (Enter mm/dd/yyyy Format)"
                                                                    Display="None" TooltipMessage="Please Enter Fin.Year Start Date" EmptyValueBlurredText="Empty"
                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="PF" SetFocusOnError="True" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Fin.Year End Date :</label>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" ToolTip="Fin.Year End Date"
                                                                    Enabled="false" TabIndex="6"></asp:TextBox>
                                                                <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-4" runat="server" id="trEligibleFor">
                                                            <label>Eligible For :</label>
                                                            <asp:Label ID="lbleligibleFor" Font-Bold="true" runat="server" CssClass="form-control" Enabled="false"
                                                                ToolTip="Employee Eligible For" TabIndex="7"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="butReport" Text="Show Report" runat="server" CssClass="btn btn-info" ValidationGroup="PF"
                                    OnClick="butReport_Click" TabIndex="8" ToolTip="Click here to Show Report" />

                                <asp:Button ID="btnReportFormat2" Text="PF Ledger Report Format2 " runat="server" CssClass="btn btn-info" ValidationGroup="PF"
                                    OnClick="btnReportFormat2_Click" />
                                <asp:Button ID="butCancel" Text="Cancel" runat="server" CssClass="btn btn-warning" TabIndex="9"
                                    OnClick="butCancel_Click" ToolTip="Click here to Reset" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PF"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="butReport" />
             <asp:PostBackTrigger ControlID="btnReportFormat2" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>