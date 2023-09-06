<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="STR_OpeningBalReport.aspx.cs" Inherits="STORES_Reports_STR_OpeningBalReport" Title="" %>


<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">OPENING STOCK</h3>

                        </div>

                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Select Group</div>
                                        <div class="panel-body">

                                            <asp:Panel ID="Panel_Error" runat="server" CssClass="Panel_Error" EnableViewState="false"
                                                Visible="false">
                                                <table class="table table-bordered table-hover table-responsive">
                                                    <tr>
                                                        <td style="width: 3%; vertical-align: top"></td>
                                                        <td>
                                                            <font style="font-family: Verdana; font-family: 11px; font-weight: bold; color: #CD0A0A">
                                                               </font>
                                                            <asp:Label ID="Label_ErrorMessage" runat="server" Style="font-family: Verdana; font-size: 11px; color: #CD0A0A"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
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
                                            <asp:Panel ID="pnlStrConfig" runat="server">

                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-6">
                                                        <div class="form-group col-md-12">
                                                            <label>Select Group:<span style="color: #FF0000">*</span></label>
                                                            <asp:RadioButtonList ID="rblGroup" runat="server" TabIndex="1" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                OnSelectedIndexChanged="rblGroup_SelectedIndexChanged">
                                                                <asp:ListItem Enabled="true" Selected="True" Text="Item Main Group" Value="1"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Item Sub Group" Value="2" TabIndex="2"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Item" Value="3" TabIndex="3"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <div class="form-group col-md-10">

                                                            <asp:RadioButtonList ID="rblSelectAllItem" runat="server" RepeatDirection="Horizontal"
                                                                AutoPostBack="True" TabIndex="4" OnSelectedIndexChanged="rblSelectAllItem_SelectedIndexChanged">
                                                                <asp:ListItem Enabled="true" Text="Particular" Value="1"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="All" Value="0" TabIndex="5" Selected="True"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <div class="form-group col-md-10" id="drpoDowntr" runat="server">
                                                            <label>Item :<span style="color: #FF0000">*</span></label>

                                                            <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control" ToolTip="Please Select Group"
                                                                AppendDataBoundItems="true " TabIndex="6">
                                                            </asp:DropDownList>

                                                            <asp:RequiredFieldValidator ID="rfvCItem" runat="server" ControlToValidate="ddlItem"
                                                                Display="None" ErrorMessage="Please Select Item" InitialValue="0" SetFocusOnError="True"
                                                                ValidationGroup="submit"></asp:RequiredFieldValidator>

                                                            <%--<ajaxToolKit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                Enabled="True" TargetControlID="rfvCItem">
                                                            </ajaxToolKit:ValidatorCalloutExtender>--%>


                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                            <div class="col-md-12 text-center">
                                                <asp:Button ID="btnreport" runat="server" Text="Report" TabIndex="7" ToolTip="Click To Report"
                                                    CssClass="btn btn-info"
                                                    ValidationGroup="submit" OnClick="btnreport_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="8" ToolTip="Click To Cancel"
                                                    CssClass="btn btn-warning"
                                                    OnClick="btnCancel_Click" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </form>
                    </div>

                </div>



            </div>



            <table width="100%" cellpadding="1" cellspacing="1">
                <%--<tr>
                    <td style="background: #79c9ec url(images/ui-bg_glass_75_79c9ec_1x400.png) 50% 50% repeat-x; border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;"
                        colspan="6">
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                        <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                        </div>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="6">
                        <!-- "Wire frame" div used to transition from the button to the info panel -->
                        <!-- Info panel to be displayed as a flyout when the button is clicked -->
                        <div id="info" style="display: none; width: 250px; z-index: 2; font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                            <div id="btnCloseParent" style="float: right;">
                                <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                    ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                            </div>
                            <div>
                                <p class="page_help_head">
                                    <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                    <%--  Shrink the info panel out of view --%>
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
                    <td style="width: 5%"></td>
                    <td style="width: 1%"></td>
                    <%-- <td colspan="4">Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%"></td>
                    <td style="width: 1%"></td>
                    <td colspan="3">
                        <%--                        <asp:Panel ID="Panel_Error" runat="server" CssClass="Panel_Error" EnableViewState="false"
                            Visible="false">
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 3%; vertical-align: top"></td>
                                    <td style="width: 97%">
                                        <font style="font-family: Verdana; font-family: 11px; font-weight: bold; color: #CD0A0A">
                                        </font>
                                        <asp:Label ID="Label_ErrorMessage" runat="server" Style="font-family: Verdana; font-size: 11px; color: #CD0A0A"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                            Visible="false">
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 3%; vertical-align: top"></td>
                                    <td style="width: 97%">
                                        <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>--%>
                    </td>
                    <td style="width: 35%"></td>
                </tr>
                <tr>
                    <td style="width: 5%"></td>
                    <td style="width: 1%"></td>
                    <td colspan="3" style="padding-left: 10px">
                        <%--<asp:Panel ID="pnlStrConfig" runat="server" Style="text-align: left; width: 100%;">--%>
                        <%--<fieldset style="padding-left: 30px; padding-right: 30px">
                                <legend class="legend">Select Group</legend>--%>
                        <br />
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 5%"></td>
                                <td style="width: 1%"></td>
                                <td style="width: 20%"><%--Select Group <span style="color: #FF0000">*</span>--%>
                                </td>
                                <td style="width: 2%">
                                    <b>:</b>
                                </td>
                                <td style="width: 70%">
                                    <%--<asp:RadioButtonList ID="rblGroup" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                OnSelectedIndexChanged="rblGroup_SelectedIndexChanged">
                                                <asp:ListItem Enabled="true" Selected="True" Text="Item Main Group" Value="1"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Item Sub Group" Value="2"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Item" Value="3"></asp:ListItem>
                                            </asp:RadioButtonList>--%>
                                </td>
                                <td style="width: 20%"></td>
                            </tr>
                            <tr>
                                <td style="width: 5%"></td>
                                <td style="width: 1%"></td>
                                <td style="width: 20%"></td>
                                <td style="width: 2%"></td>
                                <td style="width: 50%">
                                    <%--<asp:RadioButtonList ID="rblSelectAllItem" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="True" TabIndex="3" OnSelectedIndexChanged="rblSelectAllItem_SelectedIndexChanged">
                                            <asp:ListItem Enabled="true" Text="Particular" Value="1"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="All" Value="0" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>--%>
                                </td>
                                <td style="width: 35%"></td>
                            </tr>
                            <%--<tr id="drpoDowntr" runat="server">
                                    <td style="width: 5%"></td>
                                    <td style="width: 1%"></td>
                                    <td style="width: 20%">Item <span style="color: #FF0000">*</span>
                                    </td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td style="width: 50%">
                                        <asp:DropDownList ID="ddlItem" runat="server" Width="250px" ToolTip="Please Select Group"
                                            AppendDataBoundItems="true " TabIndex="4">
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvCItem" runat="server" ControlToValidate="ddlItem"
                                            Display="None" ErrorMessage="Please Select Group name" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        <ajaxToolKit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                            Enabled="True" TargetControlID="rfvCItem">
                                        </ajaxToolKit:ValidatorCalloutExtender>

                                    </td>
                                    <td style="width: 35%"></td>
                                </tr>--%>
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
                                <td style="width: 50%">
                                    <%--<asp:Button ID="btnreport" runat="server" Text="Report" TabIndex="8" ToolTip="Click To Cancel"
                                            Width="70px" Style="font-family: Verdana; font-size: 11px; width: 52; padding: 2px"
                                            ValidationGroup="submit" OnClick="btnreport_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="7" ToolTip="Click To Cancel"
                                            Width="70px" Style="font-family: Verdana; font-size: 11px; width: 52; padding: 2px"
                                            OnClick="btnCancel_Click" />--%>
                                </td>
                                <td style="width: 35%"></td>
                            </tr>
                        </table>
                        </asp:Panel>
                    </td>
                    <td style="width: 35%"></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




