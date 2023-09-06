<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="IncomeExpenditureConfiguration.aspx.cs" Inherits="IncomeExpenditureConfiguration"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../jquery/jquery-1.3.2.min.js" type="text/javascript"></script>--%>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>

    <script language="javascript" type="text/javascript">

        function add(ctl, id1) {

            var value1 = ctl.value;

            var rptData = document.getElementById('ctl00_ContentPlaceHolder1_rptSchDef').rows.length;
            for (var i = 2; i <= rptData; i++) {
                var Position = document.getElementById('ctl00_ContentPlaceHolder1_rptSchDef_ctl0' + i + '_txt_position').value;
                if (Position == value1) {
                    //document.getElementById('ctl00_ContentPlaceHolder1_rptSchDef_ctl0' + i + '_txt_position').value = "";

                    if (ctl.id != 'ctl00_ContentPlaceHolder1_rptSchDef_ctl0' + i + '_txt_position') {
                        ctl.value = "";

                        //                     setTimeout(function() { ctl.focus(); }, 1);
                        alert('Position Must Be Different');
                        return false;
                    }

                }
            }
        }

        function Position(ctl, id1) {

            var value1 = ctl.value;

            var rptData = document.getElementById('ctl00_ContentPlaceHolder1_rptSchDef1').rows.length;
            for (var i = 2; i <= rptData; i++) {
                var Position = document.getElementById('ctl00_ContentPlaceHolder1_rptSchDef1_ctl0' + i + '_txt_position').value;
                if (Position == value1) {
                    //document.getElementById('ctl00_ContentPlaceHolder1_rptSchDef_ctl0' + i + '_txt_position').value = "";
                    if (ctl.id != 'ctl00_ContentPlaceHolder1_rptSchDef1_ctl0' + i + '_txt_position') {
                        ctl.value = "";
                        alert('Position Must Be Different');
                        return false;
                    }

                }
            }
        }
     
    </script>

    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    INCOME EXPENDITURE REPORT CONFIGURATION
                    <!-- Button used to launch the help (animation) -->
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- "Wire frame" div used to transition from the button to the info panel -->
                    <!-- Info panel to be displayed as a flyout when the button is clicked -->
                    <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                        font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                        <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                            <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                                font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                        </div>
                        <div>
                            <p class="page_help_head">
                                <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                <%--  Enable the button so it can be played again --%>
                            </p>
                            <p class="page_help_text">
                                <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
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

                    <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp">
                        <Animations>
                    <OnClick>
                        <Sequence>
                            <%-- Disable the button so it can't be clicked again --%>
                            <EnableAction Enabled="false" />
                            
                            <%-- Position the wire frame on top of the button and show it --%>
                            <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>

                            <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                            <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                            <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                            <FadeIn AnimationTarget="info" Duration=".2"/>
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                            
                            <%-- Flash the text/border red and fade in the "close" button --%>
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
                <td style="padding: 10px">
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="padding: 10px">
        <asp:UpdatePanel ID="upd" runat="server">
            <ContentTemplate>
                <fieldset class="vista-grid">
                                <legend class="titlebar">Income Expenditure Report Configuration</legend>
                    <table cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td>
                                <b>Expenditure</b><asp:Panel ID="pnlgrd" Height="400px" ScrollBars="Vertical" runat="server">
                                    <asp:GridView ID="rptSchDef" runat="server" AutoGenerateColumns="False" BorderColor="White"
                                        BorderStyle="Ridge" BorderWidth="2px" Width="500px" CellPadding="3" Height="180px"
                                        BackColor="White" CellSpacing="1" GridLines="None" OnRowCommand="rptSchDef_RowCommand1">
                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                        <RowStyle Wrap="True" BackColor="#DEDFDE" ForeColor="Black" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Schedule Name" HeaderStyle-BackColor="#3399FF" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-ForeColor="White">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblparty" BackColor="Gainsboro" BorderStyle="None" runat="server"
                                                        Width="250px" Enabled="True" ReadOnly="True"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#3399FF" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="cl_balance" HeaderText="Amount">
                                                <ControlStyle BorderStyle="None" Width="50px" />
                                                <HeaderStyle BackColor="#3399FF" BorderStyle="None" />
                                                <ItemStyle BorderStyle="None" ForeColor="Blue" Width="50px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Position">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_position" runat="server" Width="50px" onblur="return add(this,'<%= txt_position.ClientID%>');"
                                                        OnTextChanged="txt_position_TextChanged"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" ID="ftbePosition" TargetControlID="txt_position"
                                                        ValidChars="0123456789" FilterMode="ValidChars">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnPrint" Text="Print" runat="server" CommandArgument='<%# Eval("Sch_ID") %>'
                                                        CommandName="Print" />
                                                    <asp:HiddenField ID="hdnSchIdEx" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnAmount" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                        <AlternatingRowStyle Wrap="True" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                            <td>
                                <b>Income</b>
                                <asp:Panel ID="pnlgrd1" Height="100%" ScrollBars="Vertical" runat="server">
                                    <asp:GridView ID="rptSchDef1" runat="server" AutoGenerateColumns="False" BorderColor="White"
                                        BorderStyle="Ridge" BorderWidth="2px" Width="90%" CellPadding="3" Height="180px"
                                        OnPageIndexChanging="rptSchDef1_PageIndexChanging" BackColor="White" CellSpacing="1"
                                        GridLines="None" OnRowCommand="rptSchDef1_RowCommand">
                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                        <RowStyle Wrap="True" BackColor="#DEDFDE" ForeColor="Black" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Schedule Name" HeaderStyle-BackColor="#3399FF" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-ForeColor="White">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblparty" BackColor="Gainsboro" BorderStyle="None" runat="server"
                                                        Width="250px" Enabled="True" ReadOnly="True"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#3399FF" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="cl_balance" HeaderText="Amount">
                                                <ControlStyle BorderStyle="None" Width="50px" />
                                                <HeaderStyle BackColor="#3399FF" BorderStyle="None" />
                                                <ItemStyle BorderStyle="None" ForeColor="Blue" Width="50px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Position">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_position" runat="server" Width="50px" AutoPostBack="False" onblur="return Position(this,'<%= txt_position.ClientID%>');"
                                                        CommandName="CheckPosition"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" ID="ftbePosition" TargetControlID="txt_position"
                                                        ValidChars="0123456789" FilterMode="ValidChars">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnPrint" Text="Print" runat="server" CommandArgument='<%# Eval("Sch_ID") %>'
                                                        CommandName="Print" />
                                                    <asp:HiddenField ID="hdnSchIdIncome" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnAmount" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                        <AlternatingRowStyle Wrap="True" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSet" runat="server" Text="Set Report" OnClick="btnSet_Click" />
                                &nbsp;<asp:Button ID="btnshow" runat="server" Text="Show" OnClick="btnshow_Click"
                                    Enabled="False" />
                                &nbsp;
                                <asp:Button ID="btnshowWithLedger" runat="server" Text="Show with Ledger" Enabled="False"
                                    OnClick="btnshowWithLedger_Click" />
                                &nbsp;
                                <asp:Button ID="btnExport" runat="server" Text="Export In word" OnClick="btnExport_Click"
                                    Enabled="False" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
