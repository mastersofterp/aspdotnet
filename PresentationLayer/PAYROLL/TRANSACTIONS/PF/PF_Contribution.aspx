<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PF_Contribution.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_PF_PF_Contribution"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PF CONTRIBUTION</h3>
                           <%-- <div class="box-tools pull-right">
                                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                    AlternateText="Page Help" ToolTip="Page Help" />
                            </div>--%>
                        </div>                       
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnl" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Selection Criteria</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group col-md-12">
                                                     <div class="form-group col-md-4">
                                                    <label>College :</label>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="2" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Employee :</label>
                                                            <asp:DropDownList runat="server" ID="ddlemployee" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1"
                                                                CssClass="form-control" ToolTip="Select Employee Name" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee" ValidationGroup="PF"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Eligible For :</label>
                                                            <asp:Label ID="lbleligibleFor" Font-Bold="true" runat="server" TabIndex="2" CssClass="form-control"
                                                                ToolTip="Employee Eligible for" Enabled="false"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Month Year :</label>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtMonthYearContributionAmount" runat="server" CssClass="form-control"
                                                                    TabIndex="3" ToolTip="Enter Mont Year for PF Contribution" style="z-index:0"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="ceContributionAmount" runat="server" Format="MM/yyyy"
                                                                    TargetControlID="txtMonthYearContributionAmount" PopupButtonID="Image3" Enabled="true"
                                                                    EnableViewState="true" PopupPosition="BottomLeft">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvtxtMonthYearContributionAmount" runat="server"
                                                                    ControlToValidate="txtMonthYearContributionAmount" Display="None" ErrorMessage="Month Year in (MM/yyyy Format)"
                                                                    ValidationGroup="PF" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Subscription :</label>
                                                            <asp:TextBox runat="server" ID="txtDeductionAmount1" Text="0.00" TabIndex="4" CssClass="form-control"
                                                                ToolTip="Enter Subscription Amount"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount1" runat="server" ControlToValidate="txtDeductionAmount1"
                                                                Display="None" ErrorMessage="Please Enter Deduction Amount1" ValidationGroup="PF"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="cvtxtDeductionAmount1" runat="server" Display="None" ErrorMessage="Deduction Amount1 Should be Numeric"
                                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount1"
                                                                Type="Double"></asp:CompareValidator>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Add Subscription :</label>
                                                            <asp:TextBox runat="server" ID="txtDeductionAmount2" Text="0.00" TabIndex="5" CssClass="form-control"
                                                                ToolTip="Enter Add Subscription Amount"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount2" runat="server" ControlToValidate="txtDeductionAmount2"
                                                                Display="None" ErrorMessage="Please Enter Deduction Amount2" ValidationGroup="PF"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="cvtxtDeductionAmount2" runat="server" Display="None" ErrorMessage="Deduction Amount2 Should be Numeric"
                                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount2"
                                                                Type="Double"></asp:CompareValidator>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Repayment :</label>
                                                            <asp:TextBox runat="server" ID="txtDeductionAmount3" Text="0.00" TabIndex="6" CssClass="form-control"
                                                                ToolTip="Enter Repayment Amount"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount3" runat="server" ControlToValidate="txtDeductionAmount3"
                                                                Display="None" ErrorMessage="Please Enter Deduction Amount3" ValidationGroup="PF"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="cvtxtDeductionAmount3" runat="server" Display="None" ErrorMessage="Deduction Amount3 Should be Numeric"
                                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount3"
                                                                Type="Double"></asp:CompareValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>                       
                        <div class="box-footer">
                            <div class="col-md-12">
                                <p class="text-center">
                                    <asp:Button ID="butSubmit" Text="Submit" runat="server" CssClass="btn btn-primary" ToolTip="Click here to Save" ValidationGroup="PF"
                                        OnClick="butSubmit_Click" TabIndex="7" />
                                    <asp:Button ID="Button2" Text="Cancel" runat="server" CssClass="btn btn-warning" TabIndex="8"
                                        OnClick="butCancel_Click" ToolTip="Click here to Reset" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PF"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </p>
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="PnlGpfCpfContibutionEntry" runat="server" ScrollBars="Auto">
                                    <table class="table table-bordered table-hover table-responsive">
                                        <tr>
                                            <td>
                                                <asp:ListView ID="lvItemGpfCpfContibutionEntry" runat="server">
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <p class="text-center text-bold">
                                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                        </p>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="demo-grid" class="vista-grid">
                                                            <h4 class="box-title">PF Contribution</h4>
                                                            <table class="table table-bordered table-hover table-responsive">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Month Year
                                                                        </th>
                                                                        <th>P.F
                                                                        </th>
                                                                        <th>P.F ADD
                                                                        </th>
                                                                        <th>P.F Loan
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btnEditGroup" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                            </td>
                                                            <td>
                                                                <%# Eval("MONYEAR")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("H1")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("H2")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("H3")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px"><%--PF CONTRIBUTION&nbsp;                       
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />--%>
                    </td>
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

                        <%--<ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
                            <Animations>
                                <OnClick>
                                    <Sequence>
                                       
                                        <EnableAction Enabled="false" />
                                        
                                     
                                        <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                                        <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                        
                                       
                                        <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                                        <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                                        <FadeIn AnimationTarget="info" Duration=".2"/>
                                        <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                                        
                                       
                                        <Parallel AnimationTarget="info" Duration=".5">
                                            <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                            <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                                        </Parallel>
                                        <Parallel AnimationTarget="info" Duration=".5">
                                            <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                            <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                            <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                                        </Parallel>
                                    </Sequence>
                                </OnClick>
                            </Animations>
                        </ajaxToolKit:AnimationExtender>
                        <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                            <Animations>
                                <OnClick>
                                    <Sequence AnimationTarget="info">
                                      
                                        <StyleAction Attribute="overflow" Value="hidden"/>
                                        <Parallel Duration=".3" Fps="15">
                                            <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                            <FadeOut />
                                        </Parallel>
                                        
                                      
                                        <StyleAction Attribute="display" Value="none"/>
                                        <StyleAction Attribute="width" Value="250px"/>
                                        <StyleAction Attribute="height" Value=""/>
                                        <StyleAction Attribute="fontSize" Value="12px"/>
                                        <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                                        
                                       
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
                        </ajaxToolKit:AnimationExtender>--%>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 10px">
                        <br />
                        <div style="text-align: left; width: 95%;">
                            <%--<fieldset class="fieldsetPay">
                                <legend class="legendPay">Selection Criteria</legend>
                                <br>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="form_left_label" width="20%">Employee :
                                        </td>
                                        <td class="form_left_text" colspan="2">
                                            <asp:DropDownList runat="server" ID="ddlemployee" AppendDataBoundItems="true" AutoPostBack="true"
                                                Width="300px" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee" ValidationGroup="PF"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Eligible For :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lbleligibleFor" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="20%">Month Year :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtMonthYearContributionAmount" runat="server" Width="80px"></asp:TextBox>
                                            &nbsp;<asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            <ajaxToolKit:CalendarExtender ID="ceContributionAmount" runat="server" Format="MM/yyyy"
                                                TargetControlID="txtMonthYearContributionAmount" PopupButtonID="Image3" Enabled="true"
                                                EnableViewState="true" PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvtxtMonthYearContributionAmount" runat="server"
                                                ControlToValidate="txtMonthYearContributionAmount" Display="None" ErrorMessage="Month Year in (MM/yyyy Format)"
                                                ValidationGroup="PF" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Subscription :
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox runat="server" ID="txtDeductionAmount1" Text="0.00" Width="80px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount1" runat="server" ControlToValidate="txtDeductionAmount1"
                                                Display="None" ErrorMessage="Please Enter Deduction Amount1" ValidationGroup="PF"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvtxtDeductionAmount1" runat="server" Display="None" ErrorMessage="Deduction Amount1 Should be Numeric"
                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount1"
                                                Type="Double"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Add Subscription :
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox runat="server" ID="txtDeductionAmount2" Text="0.00" Width="80px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount2" runat="server" ControlToValidate="txtDeductionAmount2"
                                                Display="None" ErrorMessage="Please Enter Deduction Amount2" ValidationGroup="PF"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvtxtDeductionAmount2" runat="server" Display="None" ErrorMessage="Deduction Amount2 Should be Numeric"
                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount2"
                                                Type="Double"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Repayment :
                                        </td>
                                        <td class="form_left_text" colspan="3">
                                            <asp:TextBox runat="server" ID="txtDeductionAmount3" Text="0.00" Width="80px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDeductionAmount3" runat="server" ControlToValidate="txtDeductionAmount3"
                                                Display="None" ErrorMessage="Please Enter Deduction Amount3" ValidationGroup="PF"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvtxtDeductionAmount3" runat="server" Display="None" ErrorMessage="Deduction Amount3 Should be Numeric"
                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtDeductionAmount3"
                                                Type="Double"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center">
                                            <br />
                                            <asp:Button ID="butSubmit" Text="Submit" runat="server" Width="70px" ValidationGroup="PF"
                                                OnClick="butSubmit_Click" />
                                            <asp:Button ID="Button2" Text="Cancel" runat="server" Width="70px" OnClick="butCancel_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PF"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset>--%>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 10px;">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <br />
                                    <%--<asp:Panel ID="PnlGpfCpfContibutionEntry" runat="server">
                                        <table cellpadding="0" cellspacing="0" style="width: 96%;">
                                            <tr>
                                                <td align="center">
                                                    <asp:ListView ID="lvItemGpfCpfContibutionEntry" runat="server">
                                                        <EmptyDataTemplate>
                                                            <br />
                                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div id="demo-grid" class="vista-grid">
                                                                <div class="titlebar">
                                                                    PF Contribution
                                                                </div>
                                                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr class="header">
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Month Year
                                                                        </th>
                                                                        <th>P.F
                                                                        </th>
                                                                        <th>P.F ADD
                                                                        </th>
                                                                        <th>P.F Loan
                                                                        </th>
                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                <td>
                                                                    <asp:ImageButton ID="btnEditGroup" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MONYEAR")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("H1")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("H2")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("H3")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                <td>
                                                                    <asp:ImageButton ID="btnEditGroup" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MONYEAR")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("H1")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("H2")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("H3")%>
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:ListView>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
