<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Appointment.aspx.cs" Inherits="PayRoll_Pay_Appointment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                                        <div class="panel-heading">AppointMent</div>
                                        <div class="panel-body">
                                            <asp:Panel ID="pnlAdd" runat="server">

                                                <div class="form-group col-md-5">

                                                    <label>AppointMent :<span style="color: Red">*</span></label>

                                                    <asp:TextBox ID="txtAppointMent" runat="server" MaxLength="30" TabIndex="1" ToolTip="Enter Appointment" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="rfvAppointMent" runat="server" ControlToValidate="txtAppointMent"
                                                        Display="None" ErrorMessage="Please Enter AppointMent" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="reAppoint" runat="server" ControlToValidate="txtAppointMent"
                                                        ValidationExpression="^[A-za-z ]+$" Display="None" ErrorMessage="Please Enter charaters only"
                                                        ValidationGroup="payroll"></asp:RegularExpressionValidator>



                                                </div>

                                            </asp:Panel>
                                            <div class="col-md-12 table-responsive">
                                                <asp:Panel ID="pnlPayhead" runat="server">
                                                    <asp:ListView ID="lvPayhead" runat="server">
                                                        <EmptyDataTemplate>
                                                            <br />
                                                            <br />
                                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Data Found" />
                                                        </EmptyDataTemplate>

                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    <h4>Pay Head</h4>
                                                                </div>

                                                                <table class="table table-bordered table-hover table-responsive">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th style="width: 10%">
                                                                                <asp:CheckBox ID="chkAll" runat="server" TabIndex="2" onclick="totalAppointment(this)" />Action
                                                                            </th>
                                                                            <th style="width: 20%">Pay Short
                                                                            </th>
                                                                        </tr>
                                                                        <thead>
                                                                </table>
                                                            </div>

                                                            <div class="listview-container">
                                                                <div id="Div1" class="vista-grid">
                                                                    <table class="table table-bordered table-hover table-responsive">
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </LayoutTemplate>

                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    <asp:CheckBox ID="ChkAppointment" runat="server" TabIndex="2" AlternateText="Check to allocate Payhead"
                                                                        ToolTip='<%# Eval("PayHead")%>' />
                                                                    <%# Eval("PayHead")%>
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <%# Eval("PayShort")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" TabIndex="3" CssClass="btn btn-success" ToolTip="Click to Show Report"
                                        OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" TabIndex="4" CssClass="btn btn-danger" ToolTip="Click to Reset" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                <br />
                                <div class="col-md-12">
                                    <asp:Panel ID="PnlAppoint" runat="server">
                                        <asp:ListView ID="lvAppoint" runat="server">
                                            <LayoutTemplate>
                                                <div class="vista-grid">
                                                    <div class="titlebar">
                                                        <h4>AppointMent</h4>
                                                    </div>
                                                    <table class="table table-bordered table-hover table-responsive">
                                                        <tr class="bg-light-blue">
                                                            <th style="width: 10%">Action
                                                            </th>
                                                            <th style="width: 20%">Appoint
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="listview-container">
                                                    <div id="Div2" class="vista-grid">
                                                        <table class="table table-bordered table-hover table-responsive">
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width: 10%">
                                                        <asp:ImageButton ID="btnEdit" TabIndex="5" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("APPOINTNO")%>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                    </td>
                                                    <td style="width: 20%">
                                                        <%# Eval("APPOINT")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>

                                        </asp:ListView>
                                    </asp:Panel>
                                </div> 
                            </div>
                        </form>
                    </div>

                </div>


            </div>
















            <table cellpadding="0" cellspacing="0" width="100%">
                <%--<tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px">Appointment&nbsp;
                        <!-- Button used to launch the help (animation) -->
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                    </td>
                </tr>--%>
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


                        <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
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
                    <td>
                        <%--<asp:Panel ID="pnlAdd" runat="server">
                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td class="form_left_label">AppointMent :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtAppointMent" runat="server" MaxLength="30" Width="200px" />
                                        <asp:RequiredFieldValidator ID="rfvAppointMent" runat="server" ControlToValidate="txtAppointMent"
                                            Display="None" ErrorMessage="Please Enter AppointMent" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="reAppoint" runat="server" ControlToValidate="txtAppointMent"
                                            ValidationExpression="^[A-za-z ]+$" Display="None" ErrorMessage="Please Enter charaters only"
                                            ValidationGroup="payroll"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>--%>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="30%" style="padding-left: 10px">
                                    <%-- <asp:Panel ID="pnlPayhead" runat="server" style="width:90%;overflow:auto;" Height="400px">--%>
                                    
                                </td>
                                <td width="2%">&nbsp
                                </td>
                                <td width="30%" style="padding-right: 10px">
                                    <%-- <asp:Panel ID="PnlAppoint" runat="server" style="width:90%;overflow:auto;" Height="400px">--%>
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">&nbsp
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <%--<asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" Width="80px"
                            OnClick="btnSave_Click" />&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                            OnClick="btnCancel_Click" Width="80px" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />--%>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">&nbsp
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function totalAppointment(chkcomplaint) {
            var frm = document.forms[0];
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkcomplaint.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

</asp:Content>
