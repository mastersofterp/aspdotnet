<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Abstract_Salary.aspx.cs" Inherits="PayRoll_Abstract_Salary" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title"></h3>
                    <p class="text-center">
                    </p>
                    <div class="box-tools pull-right">
                    </div>
                </div>

                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">Employees Abstract Salary Report</div>
                                <div class="panel-body">
                                    <asp:Panel ID="pnl" runat="server">

                                        <div class="form-group col-md-12">
                                            <label>View Salary as Staff Wise...</label>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>Month:</label>

                                            <asp:DropDownList ID="ddlMonthYear" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Select Month" AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                                ControlToValidate="ddlMonthYear" Display="None" ErrorMessage="Please Select Month/Year"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <label>Staff Type:</label>

                                            <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Select Staff Type" AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStaffNo" runat="server" SetFocusOnError="true"
                                                ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Staff No."
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-6">

                                            <asp:CheckBox ID="chkAbstarct" TabIndex="3" runat="server" Text="Cummulative Abstract" />

                                            <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                                DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />

                                        </div>
                                        <div class="form-group col-md-6">
                                        </div>
                                        <div class="form-group col-md-6">
                                        </div>

                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnRegisterWithAbstract" runat="server" Text="Salary Register With Abstract"
                                    CssClass="btn btn-info" OnClick="btnRegisterWithAbstract_Click" ValidationGroup="Payroll" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-danger"
                                    Width="25%" />
                            </p>
                            <div class="col-md-12">
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
                        <td style="height: 30px">ABSTRACT SALARY REPORT&nbsp;&nbsp
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
                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <%--<fieldset class="fieldsetPay">
                    <legend class="legendPay">Abstract Salary Report</legend>
                    <table width="100%" cellpadding="2" cellspacing="2" border="0">
                        <tr>--%>
                <%-- <td colspan="2">
                                <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    View Salary as Staff Wise..........</b>
                            </td>--%>
        </tr>
        <tr>
            <%--Month:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMonthYear" runat="server" Width="40%" AppendDataBoundItems="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                    ControlToValidate="ddlMonthYear" Display="None" ErrorMessage="Please Select Month/Year"
                                    ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                            </td>
        </tr>
        <%--<tr>
            <td width="28%">Staff Type:
            </td>
            <td>
                <asp:DropDownList ID="ddlStaffNo" runat="server" Width="40%" AppendDataBoundItems="True">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvStaffNo" runat="server" SetFocusOnError="true"
                    ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Staff No."
                    ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
            </td>
        </tr>--%>
        <%--<tr>
            <td width="28%">
                <asp:CheckBox ID="chkAbstarct" runat="server" Text="Cummulative Abstract" />
            </td>
            <td>
                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                    DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
            </td>
        </tr>--%>
        <tr>
            <td width="28%">
                <%-- <asp:Button ID="btnRegisterWithAbstract" runat="server" Text="Salary Register With Abstract"
                    Width="100%" OnClick="btnRegisterWithAbstract_Click" ValidationGroup="Payroll" />
            </td>
            <td>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                    Width="25%" />--%>
            </td>
        </tr>
    </table>
    </fieldset>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
