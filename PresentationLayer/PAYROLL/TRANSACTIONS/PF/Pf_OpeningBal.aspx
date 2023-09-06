<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pf_OpeningBal.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_PF_Pf_OpeningBal"
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
                            <h3 class="box-title">PF OPENING BALANCE</h3>
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
                                                <div class="panel-heading">Enter Opening Balance and Loan Opening Balance</div>
                                                <div class="panel-body">
                                                    <div class="form-group col-md-12">
                                                     <div class="form-group col-md-4">
                                                    <label>College :<span style="color: #FF0000">*</span></label>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="2" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollege"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select College" ValidationGroup="PF"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Employee :<span style="color: #FF0000">*</span></label>
                                                            <asp:DropDownList runat="server" ID="ddlemployee" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1"
                                                                CssClass="form-control" ToolTip="Select Employee" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlemployee"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee" ValidationGroup="PF"
                                                                ></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Eligible For :</label>
                                                          <%--  <asp:Label ID="lbleligibleFor" Font-Bold="true" runat="server" TabIndex="2" CssClass="form-control" Enabled="false"
                                                                ToolTip="Employee Eligible For"></asp:Label>--%>
                                                            <asp:TextBox id="txteligiblefor" Font-Bold="true" runat="server" TabIndex="2" CssClass="form-control" Enabled="false" ToolTip="Employee Eligible For"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Fin.Year Start Date :<span style="color: #FF0000">*</span></label>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" OnTextChanged="txtFromDate_TextChanged"
                                                                    AutoPostBack="true" TabIndex="3" ToolTip="Enter Financial Year Start Date"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                                    PopupPosition="BottomLeft">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                                    Display="None" ErrorMessage="Please Select Fin.Year Start Date in (dd/MM/yyyy Format)"
                                                                    ValidationGroup="PF" SetFocusOnError="True" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                                    ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter Fin.Year Start Date"
                                                                    InvalidValueMessage="Fin.Year Start Date is Invalid (Enter mm/dd/yyyy Format)"
                                                                    Display="None" TooltipMessage="Please Enter Fin.Year Start Date" EmptyValueBlurredText="Empty"
                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="PF" SetFocusOnError="True"  />
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Fin.Year End Date :<span style="color: #FF0000">*</span></label>
                                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Enabled="false" TabIndex="4"
                                                                ToolTip="Financial Year End Date"></asp:TextBox>
                                                            <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDate"
                                                                    Display="None" ErrorMessage="Please Select Fin.Year Start Date in (dd/MM/yyyy Format)"
                                                                    ValidationGroup="PF" SetFocusOnError="True" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Opening Balance :<span style="color: #FF0000">*</span></label>
                                                            <asp:TextBox runat="server" ID="txtOB" CssClass="form-control" TabIndex="5"
                                                                ToolTip="Enter PF Opening Balance" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtOB" runat="server" ControlToValidate="txtOB"
                                                                Display="None" ErrorMessage="Please Enter Opening Balance" ValidationGroup="PF"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="cvtxtOB" runat="server" Display="None" ErrorMessage="Opening Balance Should be Numeric"
                                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtOB" Type="Double"></asp:CompareValidator>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Loan Opening Balance :<span style="color: #FF0000">*</span></label>
                                                            <asp:TextBox runat="server" ID="txtLoanOB" CssClass="form-control" TabIndex="6"
                                                                ToolTip="Enter Loan Opening Balance" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvLoanOB" runat="server" ControlToValidate="txtLoanOB"
                                                                Display="None" ErrorMessage="Please Enter Loan OB" ValidationGroup="PF" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="CvLoanOB" runat="server" Display="None" ErrorMessage="Loan OB Should be Numeric"
                                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtLoanOB" Type="Double"></asp:CompareValidator>
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
                                    <asp:Button ID="butSubmit" Text="Submit" runat="server" CssClass="btn btn-primary" ValidationGroup="PF" TabIndex="7"
                                        OnClick="butSubmit_Click" ToolTip="Click here to Save" />
                                    <asp:Button ID="butCancel" Text="Cancel" runat="server" CssClass="btn btn-warning" ToolTip="Click here to Reset"
                                        OnClick="butCancel_Click" TabIndex="8" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PF"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </p>
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlOpeningBalance" runat="server" ScrollBars="Auto">
                                    <table class="table table-bordered table-hover table-responsive">
                                        <tr>
                                            <td>
                                                <asp:ListView ID="lvOpeningBalance" runat="server">
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <p class="text-center text-bold">
                                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found"   ForeColor="Red"/>
                                                        </p>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="demo-grid" class="vista-grid">
                                                            <h4 class="box-title">Opening Balance</h4>
                                                            <table class="table table-bordered table-hover table-responsive">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Month Year
                                                                        </th>
                                                                        <th>Fin.Year Start Date
                                                                        </th>
                                                                        <th>Fin.Year End Date
                                                                        </th>
                                                                        <th>Opening Balance
                                                                        </th>
                                                                        <th>Loan Bal
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
                                                                <asp:ImageButton ID="btnEditOB" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                    AlternateText="Edit Record" OnClick="btnEdit_Click" ToolTip="Edit Record" />&nbsp;
                                                            </td>
                                                            <td>
                                                                <%# Eval("MONYEAR")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("FSDATE","{0:dd/MM/yyyy}")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("FEDATE","{0:dd/MM/yyyy}")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("OB")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("LOANBAL")%>
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
                    <td class="vista_page_title_bar" valign="top" style="height: 30px"><%--PF OPENING BALANCE&nbsp;                        
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
                                <legend class="legendPay">Enter Opening Balance and Loan Opening Balance</legend>
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
                                        <td class="form_left_label">Fin.Year Start Date :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="80px" OnTextChanged="txtFromDate_TextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                            &nbsp;<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Select Fin.Year Start Date in (dd/MM/yyyy Format)"
                                                ValidationGroup="PF" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter Fin.Year Start Date"
                                                InvalidValueMessage="Fin.Year Start Date is Invalid (Enter mm/dd/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter Fin.Year Start Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="PF" SetFocusOnError="True" />
                                        </td>
                                        <td class="form_left_label">Fin.Year End Date :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtToDate" runat="server" Width="80px" Enabled="false"></asp:TextBox>--%>
                                            &nbsp;<%--Already Committed<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                            <%--<ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Opening Balance :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox runat="server" ID="txtOB" Width="80px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtOB" runat="server" ControlToValidate="txtOB"
                                                Display="None" ErrorMessage="Please Enter Opening Balance" ValidationGroup="PF"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvtxtOB" runat="server" Display="None" ErrorMessage="Opening Balance Should be Numeric"
                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtOB" Type="Double"></asp:CompareValidator>
                                        </td>
                                        <td class="form_left_label">Loan Opening Balance:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox runat="server" ID="txtLoanOB" Width="80px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLoanOB" runat="server" ControlToValidate="txtOB"
                                                Display="None" ErrorMessage="Please Enter Loan OB" ValidationGroup="PF" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CvLoanOB" runat="server" Display="None" ErrorMessage="Loan OB Should be Numeric"
                                                ValidationGroup="PF" SetFocusOnError="True" ControlToValidate="txtLoanOB" Type="Double"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center">
                                            <br />
                                            <asp:Button ID="butSubmit" Text="Submit" runat="server" Width="70px" ValidationGroup="PF"
                                                OnClick="butSubmit_Click" />
                                            <asp:Button ID="butCancel" Text="Cancel" runat="server" Width="70px" OnClick="butCancel_Click" />
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
                                    <%--<asp:Panel ID="pnlOpeningBalance" runat="server">
                                        <table cellpadding="0" cellspacing="0" style="width: 96%;">
                                            <tr>
                                                <td>
                                                    <asp:ListView ID="lvOpeningBalance" runat="server">
                                                        <EmptyDataTemplate>
                                                            <br />
                                                            <center>
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </center>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div id="demo-grid" class="vista-grid">
                                                                <div class="titlebar">
                                                                    Opening Balance
                                                                </div>
                                                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr class="header">
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Month Year
                                                                        </th>
                                                                        <th>Fin.Year Start Date
                                                                        </th>
                                                                        <th>Fin.Year End Date
                                                                        </th>
                                                                        <th>Opening Balance
                                                                        </th>
                                                                        <th>Loan Bal
                                                                        </th>
                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                <td>
                                                                    <asp:ImageButton ID="btnEditOB" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PFTRXNO") %>'
                                                                        AlternateText="Edit Record" OnClick="btnEdit_Click" ToolTip="Edit Record" />&nbsp;
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MONYEAR")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("FSDATE","{0:dd/MM/yyyy}")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("FEDATE","{0:dd/MM/yyyy}")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("OB")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("LOANBAL")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                <td>
                                                                    <asp:ImageButton ID="btnEditItemMaster" runat="server" ImageUrl="~/images/edit.gif"
                                                                        CommandArgument='<%# Eval("PFTRXNO") %>' OnClick="btnEdit_Click" AlternateText="Edit Record"
                                                                        ToolTip="Edit Record" />&nbsp;
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MONYEAR")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("FSDATE", "{0:dd/MM/yyyy}")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("FEDATE", "{0:dd/MM/yyyy}")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("OB")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("LOANBAL")%>
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
     <script type="text/javascript" language="javascript">
         function validateNumeric(txt) {
             if (isNaN(txt.value)) {
                 txt.value = txt.value.substring(0, (txt.value.length) - 1);
                 txt.value = '';
                 txt.focus = true;
                 alert("Only Numeric Characters allowed !");
                 return false;
             }
             else
                 return true;
         }
    </script>
</asp:Content>

