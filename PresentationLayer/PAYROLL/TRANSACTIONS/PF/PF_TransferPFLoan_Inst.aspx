<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PF_TransferPFLoan_Inst.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_PF_PF_TransferPFLoan_Inst"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TRANSFER PF LOAN TO INSTALLMENT</h3>
                            <div class="box-tools pull-right">
                                <%--<asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                    AlternateText="Page Help" ToolTip="Page Help" />--%>
                            </div>
                        </div>
                       <%-- <div class="box box-info">--%>
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlTransferLoanToInst" runat="server" ScrollBars="Auto">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading"></div>
                                                <div class="panel panel-body">
                                                    <table class="table table-bordered table-hover table-responsive">
                                                        <tr>
                                                            <td style="padding-left: 10px; padding-right: 10px">
                                                                <asp:ListView ID="lvSanctionLoan" runat="server">
                                                                    <EmptyDataTemplate>
                                                                        <br />
                                                                        <br />
                                                                        <p class="text-center text-bold">
                                                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Data Found" />
                                                                        </p>
                                                                    </EmptyDataTemplate>
                                                                    <LayoutTemplate>
                                                                        <div class="vista-grid">
                                                                            <h3 class="box-title">PF LOAN</h3>
                                                                            <table class="table table-bordered table-hover table-responsive">
                                                                                <thead>
                                                                                    <tr class="bg-light-blue">
                                                                                        <th>
                                                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="checkall(this)" />Action
                                                                                        </th>
                                                                                        <th>Employee Name
                                                                                        </th>
                                                                                        <th>Scanction Number
                                                                                        </th>
                                                                                        <th>Scanction Amount
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
                                                                                <asp:CheckBox ID="ChkPFloan" runat="server" AlternateText="Check to allocate Payhead"
                                                                                    ToolTip='<%# Eval("PFLTNO")%>' />
                                                                                <asp:HiddenField ID="hdnloanno" runat="server"  Value='<%# Eval("PFLTNO")%>' />
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("EMPNAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SANNO")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SANAMT")%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                       <%-- </div>--%>
                        <div class="box-footer">
                            <div class="col-md-12">
                                <p class="text-center">
                                    <asp:Button ID="butSubmit" runat="server" OnClick="btnSave_Click" Text="Submit" ValidationGroup="payroll"
                                        CssClass="btn btn-primary" ToolTip="Click here to Save" />
                                    <asp:Button ID="butCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                        CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                    <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="payroll" />
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <%--<td class="vista_page_title_bar" valign="top" style="height: 30px">TRANSFER PF LOAN TO INSTALLMENT&nbsp;                       
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
                    <td colspan="2" style="padding-left: 35px">
                        <br />
                        <%--<asp:Panel ID="pnlTransferLoanToInst" runat="server" Width="100%">
                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td style="padding-left: 10px; padding-right: 10px">
                                        <asp:ListView ID="lvSanctionLoan" runat="server">
                                            <EmptyDataTemplate>
                                                <br />
                                                <br />
                                                <center>
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Data Found" />
                                                </center>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div class="vista-grid">
                                                    <div class="titlebar">
                                                        PF LOAN
                                                    </div>
                                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                        <thead>
                                                            <tr class="header">
                                                                <th style="width: 10%">
                                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="checkall(this)" />Action
                                                                </th>
                                                                <th style="width: 20%">Employee Name
                                                                </th>
                                                                <th style="width: 20%">Scanction Number
                                                                </th>
                                                                <th style="width: 20%">Scanction Amount
                                                                </th>
                                                            </tr>
                                                            <thead>
                                                    </table>
                                                </div>
                                                <div class="listview-container">
                                                    <div id="Div1" class="vista-grid">
                                                        <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td style="width: 10%">
                                                        <asp:CheckBox ID="ChkPFloan" runat="server" AlternateText="Check to allocate Payhead"
                                                            ToolTip='<%# Eval("PFLTNO")%>' />
                                                    </td>
                                                    <td style="width: 20%">
                                                        <%# Eval("EMPNAME")%>
                                                    </td>
                                                    <td style="width: 20%">
                                                        <%# Eval("SANNO")%>
                                                    </td>
                                                    <td style="width: 20%">
                                                        <%# Eval("SANAMT")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                    <td style="width: 10%">
                                                        <asp:CheckBox ID="ChkPFloan" runat="server" AlternateText="Check to allocate Payhead"
                                                            ToolTip='<%# Eval("PFLTNO")%>' />
                                                    </td>
                                                    <td style="width: 20%">
                                                        <%# Eval("EMPNAME")%>
                                                    </td>
                                                    <td style="width: 20%">
                                                        <%# Eval("SANNO")%>
                                                    </td>
                                                    <td style="width: 20%">
                                                        <%# Eval("SANAMT")%>
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <br />
                                        <asp:Button ID="butSubmit" runat="server" OnClick="btnSave_Click" Text="Submit" ValidationGroup="payroll"
                                            Width="80px" />
                                        <asp:Button ID="butCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                            Width="80px" />
                                        <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="payroll" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>--%>
                    </td>
                </tr>
            </table>

            <script type="text/javascript" language="javascript">
                function checkall(checkall) {
                    var frm = document.forms[0];
                    for (i = 0; i < document.forms[0].elements.length; i++) {
                        var e = frm.elements[i];
                        if (e.type == 'checkbox') {
                            if (checkall.checked == true)
                                e.checked = true;
                            else
                                e.checked = false;
                        }
                    }
                }
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
