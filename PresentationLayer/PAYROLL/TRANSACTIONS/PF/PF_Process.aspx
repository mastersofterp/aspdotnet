<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PF_Process.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_PF_PF_Process" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PF PROCESS</h3>
                            <%--<div class="box-tools pull-right">
                                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                    AlternateText="Page Help" ToolTip="Page Help" />
                            </div>--%>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <asp:Panel ID="pnl" runat="server">
                                        <div class="panel panel-info">

                                            <div class="panel panel-heading">Select Employee/Staff to Process PF</div>
                                            <div class="panel panel-body">
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-8">
                                                        <asp:RadioButton ID="radEmployee" Text="Selected Employee" runat="server" GroupName="pfprocess" TabIndex="1"
                                                            AutoPostBack="true" OnCheckedChanged="radEmployee_CheckedChanged" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                             <asp:RadioButton ID="radStaff" Text="Selected Staff" runat="server" GroupName="pfprocess" TabIndex="2"
                                                                 AutoPostBack="true" OnCheckedChanged="radStaff_CheckedChanged" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-12" id="tblEmployee" runat="server">
                                                   <div class="form-group col-md-4">
                                                    <label>College :<span style="color: #FF0000">*</span></label>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="2" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollege"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select College" ValidationGroup="PF" InitialValue="0"
                                                               ></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4" runat="server" id="trEmployee">
                                                        <label>Employee :<span style="color: #FF0000">*</span></label>
                                                        <asp:DropDownList runat="server" ID="ddlemployee" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="3"
                                                            CssClass="form-control" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged" ToolTip="Select Employee">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlemployee"
                                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee" ValidationGroup="PF"
                                                            ></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4" runat="server" id="trEligibleFor">
                                                        <label>Eligible For :</label>
                                                        <%--<asp:Label ID="lbleligibleFor" Font-Bold="true" runat="server" TabIndex="4"
                                                            CssClass="form-control" ToolTip="Employee Eligible for" Enabled="false"></asp:Label>--%>
                                                          <asp:TextBox id="txteligiblefor" Font-Bold="true" runat="server" TabIndex="2"
                                                               CssClass="form-control" Enabled="false" ToolTip="Employee Eligible For"></asp:TextBox>
                                           
                                                    </div>
                                                    <div class="form-group col-md-4" runat="server" id="trStaff">
                                                        <label>Staff :</label>
                                                        <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="5"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged" ToolTip="Select Staff Type">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                            Display="None" ErrorMessage="Select Staff" ValidationGroup="payroll" InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Month Year :<span style="color: #FF0000">*</span></label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtMonYearProcess" runat="server" CssClass="form-control" TabIndex="6"
                                                                ToolTip="Enter Month Year for PF Process" Style="z-index: 0"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="cetxtMonYearProcess" runat="server" Format="MM/yyyy"
                                                                TargetControlID="txtMonYearProcess" PopupButtonID="Image4" Enabled="true" EnableViewState="true"
                                                                PopupPosition="BottomLeft">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="rfvtxtMonYearProcess" runat="server" ControlToValidate="txtMonYearProcess"
                                                                Display="None" ErrorMessage="Month Year in (MM/yyyy Format)" ValidationGroup="PF"
                                                                SetFocusOnError="True">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-4">

                                                            <asp:CheckBox ID="chkTransfer" runat="server" Text=" Is Tranfer PF" Checked="false"  OnCheckedChanged="chkTransfer_CheckedChanged" AutoPostBack="true" />

                                                        </div>
                                                        <div id="divtranfer" runat="server" visible="false">
                                                            <div class="form-group col-md-4">
                                                                <label>Transfer Amount :</label>
                                                                <asp:TextBox ID="txtTransferAmount" runat="server" CssClass="form-control" TabIndex="7"
                                                                    ToolTip="Enter Transfer Amount"></asp:TextBox>
                                                                  <ajaxtoolkit:FilteredTextBoxExtender ID="ftbPayFine" runat="server"
                                                                TargetControlID="txtTransferAmount"
                                                                FilterType="Custom,Numbers"
                                                                FilterMode="ValidChars"
                                                                ValidChars=".">
                                                            </ajaxtoolkit:FilteredTextBoxExtender>

                                                            </div>
                                                            <div class="form-group col-md-4">
                                                                <label>Transfer Date :</label>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                    </div>
                                                                    <asp:TextBox ID="txtTransferDate" runat="server" CssClass="form-control" TabIndex="6"
                                                                        ToolTip="Enter transfer Date for PF Process" Style="z-index: 0"></asp:TextBox>
                                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtTransferDate" PopupButtonID="Image1" Enabled="true" EnableViewState="true"
                                                                        PopupPosition="BottomLeft">
                                                                    </ajaxToolKit:CalendarExtender>

                                                                </div>
                                                            </div>
                                                        </div>
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
                                    <asp:Button ID="butSubmit" Text="Process" runat="server" CssClass="btn btn-primary" ValidationGroup="PF" TabIndex="7"
                                        OnClick="butSubmit_Click" ToolTip="Click here to Save" />
                                    <asp:Button ID="Button2" Text="Cancel" runat="server" CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="butCancel_Click"
                                        TabIndex="8" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PF"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </p>
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlProcessPF" runat="server" ScrollBars="Auto">
                                    <table class="table table-bordered table-hover table-responsive">
                                        <tr>
                                            <td>
                                                <asp:ListView ID="lvProcessPF" runat="server">
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <p class="text-center text-bold">
                                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                        </p>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="demo-grid" class="vista-grid">
                                                            <h4 class="box-title">Process Result</h4>
                                                            <table class="table table-bordered table-hover table-responsive">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblMonYear" Text="Month Year"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblPf" Text="PF"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblPfAdd" Text="PF Add"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblPfLoan" Text="PF Loan"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblstatus" Text="Status"></asp:Label>
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
                                                                <%# Eval("Monyear")%>
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
                                                            <td>
                                                                <%# Eval("status")%>
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
                    <%-- <td class="vista_page_title_bar" valign="top" style="height: 30px">PF PROCESS&nbsp;                       
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
                    <td style="padding-left: 10px">
                        <br />
                        <div style="text-align: left; width: 95%;">
                            <%--<fieldset class="fieldsetPay">
                                <legend class="legendPay">Select Employee/Staff to Process PF</legend>
                                <br>
                                <table cellpadding="0" cellspacing="0" style="padding-left: 200px">
                                    <tr>
                                        <td class="form_left_text">
                                            <asp:RadioButton ID="radEmployee" Text="Selected Employee" runat="server" GroupName="pfprocess"
                                                AutoPostBack="true" OnCheckedChanged="radEmployee_CheckedChanged" />
                                        </td>
                                        <td class="form_left_text">
                                            <asp:RadioButton ID="radStaff" Text="Selected Staff" runat="server" GroupName="pfprocess"
                                                AutoPostBack="true" OnCheckedChanged="radStaff_CheckedChanged" />
                                        </td>
                                    </tr>
                                </table>
                                <table cellpadding="0" cellspacing="0" id="tblEmployee" runat="server">
                                    <tr runat="server" id="trEmployee">
                                        <td class="form_left_label">Employee :
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
                                    <tr runat="server" id="trEligibleFor">
                                        <td class="form_left_label">Eligible For :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lbleligibleFor" Font-Bold="true" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trStaff">
                                        <td class="form_left_label">Staff :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" Width="300px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                Display="None" ErrorMessage="Select Staff" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Month Year :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtMonYearProcess" runat="server" Width="80px"></asp:TextBox>
                                            &nbsp;<asp:Image ID="Image4" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            <ajaxToolKit:CalendarExtender ID="cetxtMonYearProcess" runat="server" Format="MM/yyyy"
                                                TargetControlID="txtMonYearProcess" PopupButtonID="Image4" Enabled="true" EnableViewState="true"
                                                PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvtxtMonYearProcess" runat="server" ControlToValidate="txtMonYearProcess"
                                                Display="None" ErrorMessage="Month Year in (MM/yyyy Format)" ValidationGroup="PF"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center">
                                            <br />
                                            <asp:Button ID="butSubmit" Text="Process" runat="server" Width="70px" ValidationGroup="PF"
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
                        <br />
                        <table width="100%">
                            <tr>
                                <td>
                                    <%--<asp:Panel ID="pnlProcessPF" runat="server">
                                        <table cellpadding="0" cellspacing="0" style="width: 96%;">
                                            <tr>
                                                <td>
                                                    <asp:ListView ID="lvProcessPF" runat="server">
                                                        <EmptyDataTemplate>
                                                            <br />
                                                            <center>
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </center>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div id="demo-grid" class="vista-grid">
                                                                <div class="titlebar">
                                                                    Process Result
                                                                </div>
                                                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr class="header">
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblMonYear" Text="Month Year"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblPf" Text="PF"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblPfAdd" Text="PF Add"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblPfLoan" Text="PF Loan"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblstatus" Text="Status"></asp:Label>
                                                                        </th>
                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                <td>
                                                                    <%# Eval("Monyear")%>
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
                                                                <td>
                                                                    <%# Eval("status")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                <td>
                                                                    <%# Eval("Monyear")%>
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
                                                                <td>
                                                                    <%# Eval("status")%>
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
