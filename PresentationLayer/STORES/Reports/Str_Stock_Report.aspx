<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_Stock_Report.aspx.cs" Inherits="STORES_Reports_Stock_Report" %>

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

    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DAILY STOCK</h3>

                        </div>


                        <div class="box-body">
                            <div class="col-md-12">
                                <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                                <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Stock Report</div>
                                        <div class="panel-body">
                                            <div class="form-group col-md-12">
                                                <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                                    Visible="false">
                                                    <table class="table table-bordered table-hover table-responsive">
                                                        <tr>
                                                            <td style="width: 3%; vertical-align: top"></td>
                                                            <td>
                                                                <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label>Select Group:<span style="color: #FF0000">*</span></label>
                                                <asp:RadioButtonList ID="rblGroup" runat="server" TabIndex="1" RepeatDirection="Horizontal" AutoPostBack="true"
                                                    OnSelectedIndexChanged="rblGroup_SelectedIndexChanged">
                                                    <asp:ListItem Enabled="true" Selected="True" Text="Item Main Group &nbsp;" Value="1"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="Item Sub Group &nbsp;" Value="2" ></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="Item &nbsp;" Value="3" ></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                            <div class="form-group col-md-12">                                              
                                                <div class="col-md-4">
                                                    <label>Department :<span style="color: #FF0000"></span></label>

                                                    <asp:DropDownList ID="ddlDepartment" Enabled="false" runat="server" CssClass="form-control" ToolTip="Please Select Department"
                                                        AppendDataBoundItems="true " TabIndex="6" >
                                                    </asp:DropDownList>

                                                </div>
                                                 <div class="col-md-4">
                                                    <label>Item :<span style="color: #FF0000"></span></label>

                                                    <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control" ToolTip="Please Select Group"
                                                        AppendDataBoundItems="true " TabIndex="6" >
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="rfvCItem" runat="server" ControlToValidate="ddlItem"
                                                        Display="None" ErrorMessage="Please Select Group name" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <div class="form-group col-md-4">
                                                    <label>From Date:</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" ToolTip="Enter From Date" TabIndex="1" Text=""></asp:TextBox>
                                                        <div class="input-group-addon">
                                                            <asp:ImageButton ID="imgFromDate" runat="server" ImageUrl="~/IMAGES/calendar.png" />
                                                        </div>

                                                        <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" DisplayMoney="Left"
                                                            Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                            PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                            Display="None" ValidationGroup="Store" SetFocusOnError="true" ErrorMessage="Please Select From Date">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                          
                                         
                                                <div class="form-group col-md-4">
                                                    <label>To Date:</label>

                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" Text="" CssClass="form-control" ToolTip="Enter To Date"></asp:TextBox>
                                                        <div class="input-group-addon">
                                                            <asp:ImageButton ID="imgToDate" runat="server" ImageUrl="~/IMAGES/calendar.png" TabIndex="7" />
                                                        </div>
                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" DisplayMoney="Left"
                                                            Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                                        </ajaxToolKit:MaskedEditExtender>

                                                        <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgToDate"
                                                            TargetControlID="txtToDate">
                                                        </ajaxToolKit:CalendarExtender>

                                                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select End Date" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="cmpvDate" runat="server" ErrorMessage="End Date Should be greater than or equal to  From Date"
                                                            ControlToCompare="txtFromDate" ControlToValidate="txtToDate" Display="None" ValueToCompare="Date"
                                                            Type="Date" Operator="GreaterThanEqual" ValidationGroup="Store"></asp:CompareValidator>
                                                    </div>
                                                </div>
                                          
                                              </div>
                                            <div class="col-md-12 text-center">
                                                <asp:Button ID="btnRpt" runat="server" Text="Show Report" OnClick="btnRpt_Click"
                                                    CssClass="btn btn-info" TabIndex="3" ToolTip="Click To Show Report" ValidationGroup="Store" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                    CssClass="btn btn-warning" TabIndex="4" ToolTip="Click To Reset" />
                                            </div>
                                        </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <table cellpadding="0" cellspacing="0" width="100%">
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
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <br />
                            <div id="div1" style="display: block;">
                                <%--<asp:Panel >--%>
                                <%--<center>
                                        <table cellpadding="0" cellspacing="0" width="50%">
                                            <tr>
                                                <td colspan="2" style="padding-left: 10px">
                                                    <fieldset class="fieldset" style="padding-left: 10px; padding-right: 10px;">
                                                        <legend class="legend"></legend>--%>
                                <br />
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td><%--From Date:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="texbox" TabIndex="0" Text="" Width="100px"></asp:TextBox>
                                                            <asp:ImageButton ID="imgFromDate" runat="server" ImageUrl="~/IMAGES/calendar.png"
                                                                TabIndex="7" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" DisplayMoney="Left"
                                                                Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                                PopupButtonID="imgFromDate" PopupPosition="Right" TargetControlID="txtFromDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                                Display="None" ValidationGroup="Store" SetFocusOnError="true" ErrorMessage="Please Select From Date">
                                                            </asp:RequiredFieldValidator>--%>
                                                    </td>
                                                    <td><%--To Date:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="texbox" TabIndex="1" Text="" Width="100px"></asp:TextBox>

                                                            <asp:ImageButton ID="imgToDate" runat="server" ImageUrl="~/IMAGES/calendar.png" TabIndex="7" />

                                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" DisplayMoney="Left"
                                                                Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                                            </ajaxToolKit:MaskedEditExtender>

                                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgToDate"
                                                                PopupPosition="Right" TargetControlID="txtToDate">
                                                            </ajaxToolKit:CalendarExtender>

                                                            <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select End Date" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="cmpvDate" runat="server" ErrorMessage="End Date Should be greater than or equal to  From Date"
                                                                ControlToCompare="txtFromDate" ControlToValidate="txtToDate" Display="None" ValueToCompare="Date"
                                                                Type="Date" Operator="GreaterThanEqual" ValidationGroup="Store"></asp:CompareValidator>
                                                        </td>--%>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td align="center">
                                            <%--<asp:Button ID="btnRpt" runat="server" Text="Show Report" OnClick="btnRpt_Click"
                                                    Width="90px" ValidationGroup="Store" />
                                                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                    Width="90px" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                </center>
                                </asp:Panel>
                            </div>
            </td>
        </tr>
    </table>
    </td>
        </tr>
    </table>
    <div>
        <asp:ValidationSummary runat="server" ID="vdReqField" DisplayMode="List" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="Store" />
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
