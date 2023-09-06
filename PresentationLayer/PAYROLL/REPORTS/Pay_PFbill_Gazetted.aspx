<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_PFbill_Gazetted.aspx.cs" Inherits="PAYROLL_REPORTS_Pay_PFbill_Gazetted"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">NPSS PF BILL</h3>
                    <p class="text-center">
                    </p>
                    <div class="box-tools pull-right">
                    </div>
                </div>

                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                            </h5>
                            <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                            <div class="panel panel-info">
                                <div class="panel-heading"></div>
                                <div class="panel-body">
                                    <asp:Panel ID="divPaySlip" runat="server">
                                        <fieldset class="fieldsetPay">
                                            <legend class="legendPay"></legend>
                                            <div class="form-group col-md-12">
                                                <div class="form-group col-md-6">
                                                    <div class="form-group col-md-10">
                                                        <label>Month / Year:<span style="color: Red">*</span></label>

                                                        <asp:DropDownList ID="ddlMonthYear" runat="server" ToolTip="Select Month/Year" CssClass="form-control" AppendDataBoundItems="True"
                                                            TabIndex="1">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                                            ControlToValidate="ddlMonthYear" Display="None" ErrorMessage="Please Select Month/Year"
                                                            ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>

                                                </div>
                                                <div class="form-group col-md-6">
                                                </div>
                                            </div>
                                        </fieldset>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                    </div>
                </form>
            </div>

        </div>

        <div class="col-md-12">
            <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />

            <asp:Button ID="btnShow" runat="server" Text="PFbill" OnClick="btnShow_Click" CssClass="btn btn-info"
                TabIndex="2" ValidationGroup="Payroll" />
            <asp:Button ID="GFR8" runat="server" Text="GFR-8" Width="79px"
                TabIndex="3" ValidationGroup="Payroll" ToolTip="GFR-8" CssClass="btn btn-info" OnClick="GFR8_Click" />

        </div>
    </div>





    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <%--<tr>
            <td>
                <table class="vista_page_title_bar" width="100%">
                    <tr>
                        <td style="height: 30px">NPSS PF BILL&nbsp;&nbsp
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
            <td align="center">
                <%-- <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>--%>
            </td>
        </tr>
        <tr>
            <td>
                <%--<div id="divPaySlip" style="padding-left: 15px; width: 90%" runat="server">--%>
                <%--<fieldset class="fieldsetPay">
                    <legend class="legendPay">NPSS PF BILL</legend>
                    <br />--%>
                    <table width="100%" cellpadding="2" cellspacing="2" border="0">
                        <tr>
                            <td class="form_left_label"><%--Month / Year:
                                </td>
                                <td class="form_left_text">
                                    <asp:DropDownList ID="ddlMonthYear" runat="server" Width="200px" AppendDataBoundItems="True"
                                        TabIndex="1">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                        ControlToValidate="ddlMonthYear" Display="None" ErrorMessage="Please Select Month/Year"
                                        ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                <%-- <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                                </td>
                                <td class="form_left_label">
                                    <br />
                                    <asp:Button ID="btnShow" runat="server" Text="PFbill" OnClick="btnShow_Click" Width="79px"
                                        TabIndex="7" ValidationGroup="Payroll" />
                                    <asp:Button ID="GFR8" runat="server" Text="GFR-8" Width="79px"
                                        TabIndex="7" ValidationGroup="Payroll" OnClick="GFR8_Click" />--%>
                            </td>
                        </tr>
                    </table>
                    <br />
                </fieldset>
                </div>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function DisableDropDownList(disable) {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = disable;
        }
    </script>

</asp:Content>
