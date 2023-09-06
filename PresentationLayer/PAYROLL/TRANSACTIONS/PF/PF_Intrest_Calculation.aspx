<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PF_Intrest_Calculation.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_PF_PF_Intrest_Calculation"
    Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PF INTREST CALCULATION</h3>                            
                          <%--  <div class="box-tools pull-right">
                                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                    AlternateText="Page Help" ToolTip="Page Help" />
                            </div>--%>
                        </div>                       
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnl" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">Select Employee/Staff to Calculate Interest of PF</div>
                                                <div class="panel-body">
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-8">
                                                            <asp:RadioButton ID="radEmployee" Font-Bold="true" Text="Selected Employee" runat="server" TabIndex="1"
                                                                GroupName="pfprocess" AutoPostBack="true" OnCheckedChanged="radEmployee_CheckedChanged" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:RadioButton ID="radStaff" Font-Bold="true" Text="Selected Staff" runat="server" TabIndex="2"
                                                                GroupName="pfprocess" AutoPostBack="true" OnCheckedChanged="radStaff_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                  <div class="form-group col-md-12" id="tblEmployee" runat="server">
                                                     <div class="form-group col-md-4">
                                                      <label>College :</label>
                                                       <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="2"  OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                       </asp:DropDownList>
                                                      </div>
                                                        <div class="form-group col-md-4" runat="server" id="trEmployee">
                                                            <label>Employee :</label>
                                                            <asp:DropDownList runat="server" ID="ddlemployee" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="3"
                                                                CssClass="form-control" ToolTip="Select Employee" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee" ValidationGroup="PF"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-4" runat="server" id="trEligibleFor">
                                                            <label>Eligible For :</label>
                                                            <asp:Label ID="lbleligibleFor" Font-Bold="true" runat="server" CssClass="form-control" ToolTip="Eligible For"
                                                                TabIndex="4"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-md-4" runat="server" id="trStaff">
                                                            <label>Staff :</label>
                                                            <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" TabIndex="5"
                                                                AutoPostBack="true" CssClass="form-control" ToolTip="Select Staff Type">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                                Display="None" ErrorMessage="Select Staff" ValidationGroup="payroll" InitialValue="0">
                                                            </asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Yearly Percentage :</label>
                                                            <asp:TextBox ID="txtYearlyPercentage" runat="server" CssClass="form-control"
                                                                ToolTip="Enter Yearly Percentage" TabIndex="6" Enabled="false" Text="0"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>Fin.Year Start Date :</label>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" OnTextChanged="txtFromDate_TextChanged"
                                                                    AutoPostBack="true" ToolTip="Enter Financial Year Start Date" TabIndex="7"></asp:TextBox>
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
                                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" ToolTip="Financial Year End Date"
                                                                Enabled="false" TabIndex="8"></asp:TextBox>
                                                            <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <br />
                                                            <div class="text-bold">
                                                                <asp:CheckBox ID="chkCarrayOB" runat="server" Font-Bold="true" TabIndex="9"
                                                                    Text="Calculate and Transfer OB for next Financial year" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12" id="tblIntrest" runat="server">
                                                    <asp:ListView ID="lvIntrest" runat="server" >
                                                        <LayoutTemplate>
                                                            <div id="lgv12">
                                                                <h4 class="box-title">Quarter Intrest Calculation</h4>
                                                            </div>
                                                            <div id="Div2">
                                                                <table class="table table-bordered table-hover table-responsive" tabindex="3">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>Quarter
                                                                            </th>
                                                                            <th>From Date
                                                                            </th>
                                                                            <th>To Date
                                                                            </th>
                                                                            <th>%
                                                                            </th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                            </div>

                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%# Eval("Qtr")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("FDate")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ToDate")%>
                                                                    <asp:HiddenField runat="server" ID="hdnToDate"  Value='<%#Eval("ToDate")%>'/>
                                                                    <asp:HiddenField runat="server" ID="hdnFDate"  Value='<%#Eval("FDate")%>'/>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPer" runat="server" MaxLength="10" Text='<%#Eval("PER")%>'
                                                             Width="200px" />

                                                                    <ajaxtoolkit:FilteredTextBoxExtender ID="ftbPayFine" runat="server"
                                                                TargetControlID="txtPer"
                                                                FilterType="Custom,Numbers"
                                                                FilterMode="ValidChars"
                                                                ValidChars=".">
                                                            </ajaxtoolkit:FilteredTextBoxExtender>
                                                                   
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>

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
                                    <asp:Button ID="butSubmit" Text="Calculate Interest" runat="server" CssClass="btn btn-primary"
                                        ValidationGroup="PF" OnClick="butSubmit_Click" />
                                    <asp:Button ID="butCancel" Text="Cancel" runat="server" CssClass="btn btn-warning" OnClick="butCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PF"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px"><%--PF INTREST CALCULATION&nbsp;
                        <!-- Button used to launch the help (animation) -->
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

                       <%-- <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
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
                            <%-- <fieldset class="fieldsetPay">
                                <legend class="legendPay">Select Employee/Staff to Calculate Intrest of PF</legend>
                                <br>
                                <table cellpadding="0" cellspacing="0" style="padding-left: 200px">
                                    <tr>
                                        <td class="form_left_text">
                                            <asp:RadioButton ID="radEmployee" Font-Bold="true" Text="Selected Employee" runat="server"
                                                GroupName="pfprocess" AutoPostBack="true" OnCheckedChanged="radEmployee_CheckedChanged" />
                                        </td>
                                        <td class="form_left_text">
                                            <asp:RadioButton ID="radStaff" Font-Bold="true" Text="Selected Staff" runat="server"
                                                GroupName="pfprocess" AutoPostBack="true" OnCheckedChanged="radStaff_CheckedChanged" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
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
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                Display="None" ErrorMessage="Select Staff" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">Yearly Percentage :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtYearlyPercentage" runat="server" Width="80px"></asp:TextBox>
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
                                            Fin.Year End Date :
                                            <asp:TextBox ID="txtToDate" runat="server" Width="80px" Enabled="false"></asp:TextBox>--%>
                                            &nbsp;<%--Already Committed<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                            <%--<ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="form_left_label" colspan="2">
                                            <asp:CheckBox ID="chkCarrayOB" runat="server" Font-Bold="true" Text="Calculate and Transfer OB for next Financial year" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="4" align="center">
                                            <br />
                                            <asp:Button ID="butSubmit" Text="Calculate Intrest" runat="server" Width="120px"
                                                ValidationGroup="PF" OnClick="butSubmit_Click" />
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
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>