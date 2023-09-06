<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PF_SCANCTION_LOAN.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_PF_PF_SCANCTION_LOAN"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PF LOAN SANCTION</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <%--Pane for Employee Selection--%>
                                    <asp:Panel ID="pnlSelection" runat="server">
                                        <div class="panel panel-info" style="width: auto">
                                            <div class="panel-heading">Select Criteria</div>
                                            <div class="panel-body">
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-4">
                                                        <label>Order By&nbsp; :</label>
                                                        <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged" ToolTip="Select Order By" TabIndex="1">
                                                            <asp:ListItem Value="1" Selected="True">IdNo</asp:ListItem>
                                                            <asp:ListItem Value="2">Name</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                     <div class="form-group col-md-4">
                                                    <label>College :</label>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="2"  OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Employee Name&nbsp; :</label>
                                                        <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="2"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" ToolTip="Select Employee Name">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee Name"
                                                            ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <%--Panel For Empoyee Details--%>
                                    <asp:Panel ID="pnlEmpDetails" runat="server" Style="width: auto">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Employee Details</div>
                                            <div class="panel-body">
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-4">
                                                        <h5>IdNo & Name :
                                                            <asp:Label ID="lblIdnoName" runat="server" Font-Bold="true" Text="101-Sanjay"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <h5>Basic :
                                                            <asp:Label ID="lblBasic" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <h5>Date Of Appointment :                                                               
                                                            <asp:Label ID="lblDOA" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <h5>Designation :                                                           
                                                            <asp:Label ID="lblDesignation" runat="server" Font-Bold="true" Text="SoftWare Developer"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <h5>Date Of Birth :                                                                
                                                             <asp:Label ID="lblDOB" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <h5>Department :                                                           
                                                            <asp:Label ID="lblDepartment" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <h5>PF.No. :                                                               
                                                            <asp:Label ID="lblPfNo" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <h5>Eligible PF Type :                                                           
                                                            <asp:Label ID="lblEligiblePFType" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                                        </h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <%-- Panel for Loan Entry--%>
                                    <asp:Panel ID="pnlPFLoanEntry" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Sanction Details</div>
                                            <div class="panel-body">
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-4">
                                                        <label>Fin.Year Start Date :</label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" OnTextChanged="txtFromDate_TextChanged"
                                                                AutoPostBack="true" TabIndex="4" ToolTip="Enter Financial Year Start Date"></asp:TextBox>
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
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Fin.Year End Date :</label>
                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Enabled="false" ToolTip="Financial Year End Date"
                                                            TabIndex="5"></asp:TextBox>
                                                        <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Sanction Number :</label>
                                                        <asp:TextBox ID="txtScanctionNumber" runat="server" CssClass="form-control" TabIndex="6"
                                                            ToolTip="Enter Loan Sanction Number"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rvftxtScanctionNumber" runat="server" ControlToValidate="txtScanctionNumber"
                                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Scanction Number"
                                                            ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Scanction Date :</label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="ImgAdvDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtScanctionDate" runat="server" CssClass="form-control" TabIndex="7"
                                                                ToolTip="Enter Loan Sanction Date"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="cetxtScanctionDate" runat="server" Enabled="true"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImgAdvDate" TargetControlID="txtScanctionDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="rfvtxtScanctionDate" runat="server" ControlToValidate="txtScanctionDate"
                                                                Display="None" ErrorMessage="Please Select Scanction Date in (dd/MM/yyyy Format)"
                                                                SetFocusOnError="True" ValidationGroup="payroll">
                                                            </asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="metxtScanctionDate" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" TargetControlID="txtScanctionDate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:MaskedEditValidator ID="mevtxtScanctionDate" runat="server" ControlExtender="metxtScanctionDate"
                                                                ControlToValidate="txtScanctionDate" Display="None" EmptyValueBlurredText="Empty"
                                                                EmptyValueMessage="Please Enter Scanction Date" InvalidValueBlurredMessage="Invalid Date"
                                                                InvalidValueMessage=" Scanction Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="True"
                                                                TooltipMessage="Please Enter Scanction Date" ValidationGroup="payroll" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Advance Amount :</label>
                                                        <asp:TextBox ID="txtAdvanceAmount" runat="server" MaxLength="9" TabIndex="8" CssClass="form-control"
                                                            Enabled="false" onkeyup="return ValidateNumeric(this);" ToolTip="Loan Advance Amount"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAdvanceAmount" runat="server" ControlToValidate="txtAdvanceAmount"
                                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Advance Amount"
                                                            ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Sanction Amount :</label>
                                                        <asp:TextBox ID="txtScanctionAmount" runat="server" MaxLength="9" TabIndex="9" Enabled="false" CssClass="form-control"
                                                            onkeyup="return ValidateNumeric(this);" ToolTip="Loan Sanction Amount"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rvtxtScanctionAmount" runat="server" ControlToValidate="txtScanctionAmount"
                                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Scanction Amount"
                                                            ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Sanction :</label>
                                                        <asp:CheckBox ID="chkScanction" runat="server" Checked="true" TabIndex="10" ToolTip="Check If Loan is Sanctioned" />
                                                          <asp:HiddenField ID="HDNCOLLEGENO" runat="server" />
                                                    </div>
                                                    <br />
                                                    <div class="form-group col-md-12">
                                                        <p class="text-center">
                                                            <asp:Button ID="butSubmit" runat="server" OnClick="btnSave_Click" TabIndex="11" Text="Submit" ValidationGroup="payroll"
                                                                CssClass="btn btn-primary" ToolTip="Click here to Save" />
                                                            <asp:Button ID="butCancel" runat="server" OnClick="btnCancel_Click" TabIndex="12" Text="Cancel"
                                                                CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                                            <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="payroll" />
                                                        </p>
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
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <table class="table table-bordered table-hover table-responsive">
                                        <tr>
                                            <td>
                                                <asp:ListView ID="lvPFLoanEntry" runat="server">
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <p class="text-center text-bold">
                                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No records found to scanction loan" />
                                                        </p>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="demo-grid" class="vista-grid">
                                                            <h4 class="box-title">PF LOAN DETAILS</h4>
                                                            <table class="table table-bordered table-hover table-responsive">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Adv.Amt
                                                                        </th>
                                                                        <th>Adv.dt
                                                                        </th>
                                                                        <th>Per
                                                                        </th>
                                                                        <th>Taken As
                                                                        </th>
                                                                        <th>Fin.Sdate
                                                                        </th>
                                                                        <th>Fin.Edate
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
                                                                <asp:Button ID="butScanction" runat="server" Text="Sanction" ToolTip=" Click here to Scanction Loan"
                                                                    CommandArgument='<%# Eval("PFLTNO") %>' OnClick="btnScanction_Click" CssClass="btn btn-info" TabIndex="3" />
                                                            </td>
                                                            <td>
                                                                <%# Eval("ADVAMT")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ADVDT","{0:dd/MM/yyyy}")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("PER")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SHORTNAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("FDATE","{0:dd/MM/yyyy}")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("TDATE","{0:dd/MM/yyyy}")%>
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
                    <td class="vista_page_title_bar" valign="top" style="height: 30px"><%--PF LOAN SCANCTION&nbsp;                        <
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
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 20px">
                        <%--<asp:Panel ID="pnlSelection" runat="server" Style="text-align: left; width: 90%; padding-left: 15px;">
                            <fieldset class="fieldsetPay">
                                <legend class="legendPay">Select Criteria</legend>
                                <br>
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td class="form_left_text" style="padding-left: 15px">Order By:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server" Width="100px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">IdNo</asp:ListItem>
                                                <asp:ListItem Value="2">Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px" class="form_left_label">Employee Name :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" Width="300px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee Name"
                                                ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset>
                        </asp:Panel>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<asp:Panel ID="pnlEmpDetails" runat="server" Style="text-align: left; width: 95%; padding-left: 15px;">
                            <fieldset class="fieldsetPay">
                                <legend class="legendPay">Employee Details</legend>
                                <br>
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td class="form_left_label" style="padding-left: 15px">IdNo& Name:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblIdnoName" runat="server" Font-Bold="true" Text="101-Sanjay"></asp:Label>
                                        </td>
                                        <td class="form_left_label" style="padding-left: 15px">Basic :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblBasic" runat="server" Font-Bold="true" Text=".NET"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="padding-left: 15px">Date Of Appointment :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblDOA" runat="server" Font-Bold="true" Text=".NET"></asp:Label>
                                        </td>
                                        <td class="form_left_label" style="padding-left: 15px">Designation :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblDesignation" runat="server" Font-Bold="true" Text="SoftWare Developer"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="padding-left: 15px">Date Of Birth :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblDOB" runat="server" Font-Bold="true" Text=".NET"></asp:Label>
                                        </td>
                                        <td class="form_left_label" style="padding-left: 15px">Department :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblDepartment" runat="server" Font-Bold="true" Text=".NET"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="padding-left: 15px">PF.No. :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblPfNo" runat="server" Font-Bold="true" Text=".NET"></asp:Label>
                                        </td>
                                        <td class="form_left_label" style="padding-left: 15px">Eligible PF Type:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblEligiblePFType" runat="server" Font-Bold="true" Text="No Details"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        &nbsp
                                            <td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>--%>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<asp:Panel ID="pnlPFLoanEntry" runat="server" Style="text-align: left; width: 95%; padding-left: 15px;">
                            <fieldset class="fieldsetPay">
                                <legend class="legendPay">Sanction Details</legend>
                                <br>
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
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
                                        <td class="form_left_label">Scanction Number:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtScanctionNumber" runat="server" Width="120px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvftxtScanctionNumber" runat="server" ControlToValidate="txtScanctionNumber"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Scanction Number"
                                                ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="form_left_label">Scanction Date:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtScanctionDate" runat="server" Width="80px"></asp:TextBox>
                                            &nbsp;<asp:Image ID="ImgAdvDate" runat="server" ImageUrl="~/images/calendar.png"
                                                Style="cursor: pointer" />
                                            <ajaxToolKit:CalendarExtender ID="cetxtScanctionDate" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImgAdvDate" TargetControlID="txtScanctionDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvtxtScanctionDate" runat="server" ControlToValidate="txtScanctionDate"
                                                Display="None" ErrorMessage="Please Select Scanction Date in (dd/MM/yyyy Format)"
                                                SetFocusOnError="True" ValidationGroup="payroll">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:MaskedEditExtender ID="metxtScanctionDate" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" TargetControlID="txtScanctionDate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevtxtScanctionDate" runat="server" ControlExtender="metxtScanctionDate"
                                                ControlToValidate="txtScanctionDate" Display="None" EmptyValueBlurredText="Empty"
                                                EmptyValueMessage="Please Enter Scanction Date" InvalidValueBlurredMessage="Invalid Date"
                                                InvalidValueMessage=" Scanction Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="True"
                                                TooltipMessage="Please Enter Scanction Date" ValidationGroup="payroll" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Advance Amount :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtAdvanceAmount" runat="server" MaxLength="9" Width="80px" Enabled="false"
                                                onkeyup="return ValidateNumeric(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAdvanceAmount" runat="server" ControlToValidate="txtAdvanceAmount"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Advance Amount"
                                                ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="form_left_label">Scanction Amount :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtScanctionAmount" runat="server" MaxLength="9" Width="80px" Enabled="false"
                                                onkeyup="return ValidateNumeric(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvtxtScanctionAmount" runat="server" ControlToValidate="txtScanctionAmount"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Scanction Amount"
                                                ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Scanction :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:CheckBox ID="chkScanction" runat="server" Checked="true" />
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
                                    <tr>
                                        <td>&nbsp;
                                            <td></td>
                                        </td>
                                    </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>--%>
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="padding-left: 35px">
                        <%--<asp:Panel ID="pnlList" runat="server" Width="95%">
                            <table cellpadding="0" cellspacing="0" style="width: 97%; text-align: center">
                                <tr>
                                    <td align="center">
                                        <asp:ListView ID="lvPFLoanEntry" runat="server">
                                            <EmptyDataTemplate>
                                                <br />
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No records found to scanction loan" />
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div id="demo-grid" class="vista-grid">
                                                    <div class="titlebar">
                                                        PF LOAN DETAILS
                                                    </div>
                                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr class="header">
                                                            <th>Action
                                                            </th>
                                                            <th>Adv.Amt
                                                            </th>
                                                            <th>Adv.dt
                                                            </th>
                                                            <th>Per
                                                            </th>
                                                            <th>Taken As
                                                            </th>
                                                            <th>Fin.Sdate
                                                            </th>
                                                            <th>Fin.Edate
                                                            </th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td>
                                                        <asp:Button ID="butScanction" runat="server" Text="Scanction" ToolTip="Scanction"
                                                            CommandArgument='<%# Eval("PFLTNO") %>' OnClick="btnScanction_Click" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("ADVAMT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ADVDT","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PER")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SHORTNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FDATE","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TDATE","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>--%>
                    </td>
                </tr>
            </table>

            <script type="text/javascript">

                function ValidateNumeric(txt) {
                    if (isNaN(txt.value)) {
                        txt.value = txt.value.substring(0, (txt.value.length) - 1);
                        txt.value = "";
                        txt.focus();
                        alert("Only Numeric Characters alloewd");
                        return false;
                    }
                    else {
                        return true;
                    }

                }



            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
