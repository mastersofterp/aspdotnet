<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CancelReciept.aspx.cs" Inherits="Academic_CancelReciept" Title="" Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function getCurrentYear() {
            var cDate = new Date();
            return cDate.getFullYear();
        }

        function CheckDate(sender, args) {
            //var txtfrm = document.getElementById('txtFromDate')
            //var txtto = document.getElementById('txtToDate')
            if (sender._selectedDate > new Date()) {
                sender._selectedDate = new Date();
                alert("Do not select Future Date!");
                //sender._textbox.set_Value("");
                document.getElementById("txtFromDate").value = '';
                document.getElementById("txtToDate").value = "";
            }
        }
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">RECEIPT COLLECTION REPORT</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-12 col-md-12 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Report Types</label>
                                </div>
                                <asp:RadioButton ID="rdoCancelReciept" Text="Cancel Receipt Report" CssClass="data_label"
                                    Checked="true" GroupName="Reciept" TabIndex="1" runat="server" />
                            
                                <asp:RadioButton ID="rdoChalanCancel" Visible="false" Text="Deleted Chalan  Report" CssClass="data_label"
                                    GroupName="Reciept" TabIndex="2" runat="server" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Criteria for Report Generation</h5>
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>From Date</label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon" id="imgCalFromDate">
                                        <i class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="3" CssClass="form-control" />
                                    <%--<asp:Image ID="imgCalFromDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckDate"
                                        TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                    </ajaxToolKit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                        Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                                        ValidationGroup="report" />
                                    <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>To Date</label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon" id="imgCalToDate">
                                        <i class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="4" CssClass="form-control" />
                                    <%-- <asp:Image ID="imgCalToDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckDate"
                                        TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                                    </ajaxToolKit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                        Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                                        ValidationGroup="report" />
                                    <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                    <asp:CompareValidator ID="CompareValidator1" ValidationGroup="report" ForeColor="Red" runat="server"
                                        ControlToValidate="txtFromDate" ControlToCompare="txtToDate" Operator="LessThan" Type="Date" Display="None"
                                        ErrorMessage="From date must be less than To date."></asp:CompareValidator>
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Counter No.</label>
                                </div>
                                <asp:DropDownList ID="ddlCounterNo" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                    TabIndex="5" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Receipt Type</label>
                                </div>
                                <asp:DropDownList ID="ddlReceiptType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                    TabIndex="6" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Data Filters</h5>
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Degree</label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                    TabIndex="7" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Year</label>
                                </div>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="9" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Branch</label>
                                </div>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="8" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Semester</label>
                                </div>
                                <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="10" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" TabIndex="11" ValidationGroup="report" CssClass="btn btn-info" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" TabIndex="12" CssClass="btn btn-warning" />
                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />
                    </div>

                </div>
            </div>
        </div>
    </div>

    
    <%-- <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                RECEIPT COLLECTION REPORT&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                    border: solid 1px #D0D0D0;">
                </div>
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
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                // Move an element directly on top of another element (and optionally
                // make it the same size)
                function Cover(bottom, top, ignoreSize) 
                {
                    var location = Sys.UI.DomElement.getLocation(bottom);
                    top.style.position = 'absolute';
                    top.style.top = location.y + 'px';
                    top.style.left = location.x + 'px';
                    if (!ignoreSize) 
                    {
                        top.style.height = bottom.offsetHeight + 'px';
                        top.style.width = bottom.offsetWidth + 'px';
                    }
                }
                </script>

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
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
                </ajaxToolKit:AnimationExtender>
            </td>
        </tr>
    </table>--%>
  
    <%--  <fieldset class="fieldset">
        <legend class="legend">Report Types</legend>
        <table width="100%" cellpadding="2" cellspacing="2" border="0">
            <tr>
                <td rowspan="3" valign="top">
                    <fieldset class="fieldset">
                        <asp:RadioButton ID="rdoCancelReciept" Text="Cancel Receipt Report" CssClass="data_label"
                            Checked="true" GroupName="Reciept" TabIndex="1" runat="server" /><br />
                        <asp:RadioButton ID="rdoChalanCancel" Text="Deleted Chalan  Report" CssClass="data_label"
                            GroupName="Reciept" TabIndex="2" runat="server" />
                    </fieldset>
                </td>
                <td width="60%">
                    &nbsp;</td>
            </tr>
           
        </table>
    </fieldset>
    <br />
    <br />
    <fieldset class="fieldset">
        <legend class="legend">Criteria for Report Generation</legend>
        <table width="100%" cellpadding="2" cellspacing="2" border="0">
            <tr>
                <td class="data_label" width="15%">
                    From Date:
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="3" Width="150px" />
                    <asp:Image ID="imgCalFromDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />
                 
                    <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                        Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                        ValidationGroup="report" />
                    <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                </td>
                <td width="15%">
                    Counter No.:
                </td>
                <td width="35%">
                    <asp:DropDownList ID="ddlCounterNo" runat="server" Width="154px" AppendDataBoundItems="true"
                        TabIndex="5" />
                  
                </td>
            </tr>
            <tr>
                <td class="data_label">
                    To Date:
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="4" Width="150px" />
                    <asp:Image ID="imgCalToDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />
                  
                    <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                        Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                        ValidationGroup="report" />
                    <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                </td>
                <td>
                    Receipt Type:
                </td>
                <td>
                   
                    <asp:DropDownList ID="ddlReceiptType" runat="server" Width="154px" AppendDataBoundItems="true"
                        TabIndex="6" />
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset class="fieldset">
        <legend class="legend">Data Filters</legend>
        <table width="100%" cellpadding="2" cellspacing="2" border="0">
            <tr>
                <td width="15%">
                    Degree:
                </td>
                <td width="35%">
                    <asp:DropDownList ID="ddlDegree" runat="server" Width="150px" AppendDataBoundItems="true"
                        TabIndex="7" 
                        onselectedindexchanged="ddlDegree_SelectedIndexChanged" />
                </td>
                <td width="15%">
                    Year:
                </td>
                <td width="35%">
                    <asp:DropDownList ID="ddlYear" runat="server" Width="150px" AppendDataBoundItems="true"
                        TabIndex="9" />
                </td>
            </tr>
            <tr>
                <td>
                    Branch:
                </td>
                <td>
                    <asp:DropDownList ID="ddlBranch" runat="server" Width="150px" AppendDataBoundItems="true"
                        TabIndex="8" />
                </td>
                <td>
                    Semester:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSemester" runat="server" Width="150px" AppendDataBoundItems="true"
                        TabIndex="10" />
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td align="center">
                <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click"
                    TabIndex="11" ValidationGroup="report" />&nbsp;<asp:Button ID="btnCancel" 
                    runat="server" Text="Cancel" CausesValidation="false"
                    OnClick="btnCancel_Click" TabIndex="12" />
                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                    ShowSummary="false" ValidationGroup="report" />
            </td>
        </tr>
        
       
    </table>
    <br />--%>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
