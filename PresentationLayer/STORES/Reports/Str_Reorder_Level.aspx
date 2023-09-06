<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_Reorder_Level.aspx.cs" Inherits="STORES_Reports_Str_Reorder_Level" Title="" %>


<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updpanel">
        <ProgressTemplate>
            <div id="IMGDIV" class="outerDivs" align="center" valign="middle" runat="server"
                style="position: absolute; left: 0%; top: 0%; visibility: visible; border-style: none; background-color: Transparent Black; z-index: 40; height: 100%; width: 100%;">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td valign="middle" align="center">
                            <br />
                            <%--Progressing Request....  --%>
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Recorder Level</h3>

                        </div>

                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                                    <asp:Panel ID="pnl" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Select Group</div>
                                            <div class="panel-body">


                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-6">
                                                        <div class="form-group col-md-10">
                                                            <label>Select Group :<span style="color: #FF0000">*</span></label>

                                                            <asp:RadioButtonList ID="rblGroup" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="rblGroup_SelectedIndexChanged"
                                                                RepeatDirection="Horizontal" TabIndex="1">
                                                                <asp:ListItem Enabled="true" Selected="True" Text="Item Main Group" Value="1"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Item Sub Group" Value="2"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Item" Value="3"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <%-- <label></label>--%>
                                                            <asp:RadioButtonList ID="rblSelectAllItem" runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="rblSelectAllItem_SelectedIndexChanged"
                                                                RepeatDirection="Horizontal" TabIndex="2">
                                                                <asp:ListItem Enabled="true" Text="Particular" Value="1"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Selected="True" Text="All" Value="0"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <div class="form-group col-md-10" id="drpoDowntr" runat="server">
                                                            <label>Item: <span style="color: #FF0000">*</span></label>

                                                            <asp:DropDownList ID="ddlItem" runat="server" AppendDataBoundItems="true "
                                                                TabIndex="3" CssClass="form-control" ToolTip="Please Select Group">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvCItem" runat="server"
                                                                ControlToValidate="ddlItem" Display="None"
                                                                ErrorMessage="Please Select Item" InitialValue="0" SetFocusOnError="True"
                                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            <%--<ajaxToolKit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2"
                                                                runat="server" Enabled="True" TargetControlID="rfvCItem">
                                                            </ajaxToolKit:ValidatorCalloutExtender>--%>
                                                        </div>
                                                        <div class="form-group col-md-10">

                                                            <asp:CheckBox ID="chkReorderLevel" runat="server" AutoPostBack="true"
                                                                Checked="true" Enabled="true" Font-Bold="true" TabIndex="4"
                                                                OnCheckedChanged="chkReorderLevel_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                    </div>
                                                </div>

                                                <div class="col-md-12 text-center">
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                                        ValidationGroup="submit" />

                                                    <asp:Button ID="btnreport" runat="server" OnClick="btnreport_Click"
                                                        TabIndex="5" Text="Report" ToolTip="Click To Report" ValidationGroup="submit"
                                                        CssClass="btn btn-info" />
                                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                                        TabIndex="6" Text="Cancel" ToolTip="Click To Cancel" CssClass="btn btn-warning" />
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







            <table width="100%" cellpadding="1" cellspacing="1" class="table-formdata">
                <%-- <tr>
                    <td style="background: #79c9ec url(images/ui-bg_glass_75_79c9ec_1x400.png) 50% 50% repeat-x; border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;"
                        colspan="4">
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                        <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                        </div>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="4">
                        <!-- "Wire frame" div used to transition from the button to the info panel -->
                        <!-- Info panel to be displayed as a flyout when the button is clicked -->
                        <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                            <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
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
                    <%--<td style="width: 5%"></td>
                    <td style="width: 1%"></td>
                    <td colspan="2">Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                    </td>--%>
                </tr>
                <tr>
                    <td style="width: 5%"></td>
                    <td style="width: 1%"></td>
                    <td style="padding-left: 10px">
                        <asp:Panel ID="pnlStrConfig" runat="server"
                            Style="text-align: left; width: 100%;">
                            <%--<fieldset style="padding-left: 30px; padding-right: 30px">
                                <legend class="legend"></legend>--%>
                            <br />
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="width: 20%"><%--Select Group <span style="color: #FF0000">*</span>
                                    </td>
                                    <td style="width: 2%">
                                        <b>:</b>
                                    </td>
                                    <td style="width: 100%">
                                        <asp:RadioButtonList ID="rblGroup" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="rblGroup_SelectedIndexChanged"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Enabled="true" Selected="True" Text="Item Main Group" Value="1"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Item Sub Group" Value="2"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Item" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%"></td>
                                    <td style="width: 2%">
                                        <b></b>
                                    </td>
                                    <td style="width: 50%">
                                        <%--<asp:RadioButtonList ID="rblSelectAllItem" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="rblSelectAllItem_SelectedIndexChanged"
                                            RepeatDirection="Horizontal" TabIndex="3">
                                            <asp:ListItem Enabled="true" Text="Particular" Value="1"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Selected="True" Text="All" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>--%>
                                    </td>
                                </tr>
                              
                                <tr>
                                    <td style="width: 20%"></td>
                                    <td style="width: 2%"></td>
                                    <td style="width: 35%"></td>
                                </tr>
                                <tr>
                                    <td style="width: 20%"></td>
                                    <td style="width: 2%">
                                        <b></b>
                                    </td>
                                    <td style="width: 50%">
                                        <%--<asp:CheckBox ID="chkReorderLevel" runat="server" AutoPostBack="true"
                                            Checked="true" Enabled="true" Font-Bold="true"
                                            OnCheckedChanged="chkReorderLevel_CheckedChanged" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%"></td>
                                    <td style="width: 2%"></td>
                                    <td style="width: 35%"></td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                            DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                            ValidationGroup="submit" />
                                    </td>
                                    <td style="width: 2%"></td>
                                    <td style="width: 50%">
                                        <asp:Button ID="btnreport" runat="server" OnClick="btnreport_Click"
                                            Style="font-family: Verdana; font-size: 11px; width: 52; padding: 2px"
                                            TabIndex="8" Text="Report" ToolTip="Click To Cancel" ValidationGroup="submit"
                                            Width="70px" />
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                            Style="font-family: Verdana; font-size: 11px; width: 52; padding: 2px"
                                            TabIndex="7" Text="Cancel" ToolTip="Click To Cancel" Width="70px" />--%>
                                    </td>
                                </tr>
                            </table>
                            </fieldset>
                        </asp:Panel>
                    </td>
                    <td style="width: 35%"></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

