<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_SubPayHead_Reort.aspx.cs" Inherits="PAYROLL_REPORTS_Pay_SubPayHead_Reort"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">SUB-PAYHEAD REPORT</h3>
                    <p class="text-center">
                    </p>
                    <div class="box-tools pull-right">
                    </div>
                </div>

                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                            </h5>
                            <div class="panel panel-info">
                                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                <div class="panel-heading">Sub Pay Head Report</div>
                                <div class="panel-body">
                                    <asp:Panel ID="divPaySlip" runat="server">

                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-6">
                                                <div class="form-group col-md-10">
                                                    <label>Month / Year :<span style="color: Red">*</span></label>

                                                    <asp:DropDownList ID="ddlMonthYear" runat="server" ToolTip="Select Month/Year" CssClass="form-control" AppendDataBoundItems="True"
                                                        TabIndex="1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                                        ControlToValidate="ddlMonthYear" Display="None" ErrorMessage="Please Select Month/Year"
                                                        ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>College Name :<span style="color: Red">*</span></label>

                                                    <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" TabIndex="2" ToolTip="Select College Name" CssClass="form-control"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select College Name"
                                                        ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Staff :<span style="color: Red">*</span></label>
                                                    <asp:ListBox ID="lstStaffName" runat="server" SelectionMode="Multiple" BackColor="#E0EAF1"
                                                        Height="140px" CssClass="form-control" ToolTip="Select Staff" TabIndex="3" AppendDataBoundItems="True"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="rfvLstParticularColumn" runat="server" ControlToValidate="lstStaffName"
                                                        Display="None" ErrorMessage="Please Select Atleast one Staff." SetFocusOnError="true" InitialValue="0"
                                                        ValidationGroup="Payroll" />
                                                    <asp:HiddenField ID="hidIdNo" runat="server" />
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Pay Head :<span style="color: Red">*</span></label>

                                                    <asp:DropDownList ID="ddlPayhead" CssClass="form-control" ToolTip="Select Pay Head" TabIndex="4" AppendDataBoundItems="true" runat="server" Width="200px"
                                                        OnSelectedIndexChanged="ddlPayhead_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlPayhead" runat="server" ControlToValidate="ddlPayhead"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Payhead" ValidationGroup="payroll"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Sub-Pay Head :<span style="color: Red">*</span></label>

                                                    <asp:DropDownList ID="ddlSubpayhead" CssClass="form-control" ToolTip="Select Sub-Pay Head" TabIndex="5" AppendDataBoundItems="true" runat="server">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlSubpayhead" runat="server" ControlToValidate="ddlSubpayhead"
                                                        Display="None" ErrorMessage="Please Select Sub Payhead" ValidationGroup="Payroll"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Cheque No. :</label>

                                                    <asp:TextBox ID="txtAccNo" runat="server" MaxLength="6" CssClass="form-control" ToolTip="Enter Cheque No" TabIndex="6" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Cheque Submission Date :<span style="color: Red">*</span></label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtFromDate" runat="server" ToolTip="Enter Cheque Submission Date" TabIndex="7" CssClass="form-control" />
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCalFromDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>

                                                        <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                                                            Display="None" ErrorMessage="Please enter Submission Date" ValidationGroup="Payroll"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate" MaskType="Date" Mask="99/99/9999"></ajaxToolKit:MaskedEditExtender>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="form-group col-md-6">
                                            </div>
                                        </div>

                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnShowReport" runat="server" ToolTip="Click To Show Report" Text="Show" CssClass="btn btn-info" OnClick="btnShowReport_Click"
                                    TabIndex="8" ValidationGroup="Payroll" />

                                <asp:Button ID="btnCancel" runat="server" ToolTip="Click To Reset" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" TabIndex="9" />
                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                    DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                            </div>
                        </div>

                    </div>
                </form>
            </div>

        </div>


    </div>



    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <%--<tr>
            <td>
                <table class="vista_page_title_bar" width="100%">
                    <tr>
                        <td style="height: 30px">SUB-PAYHEAD REPORT&nbsp;&nbsp
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
            <td align="center"></td>
        </tr>
        <tr>
            <td>
                <center>
                    </br>
                    <%--<div id="divPaySlip" style="padding-left: 15px; width: 60%" runat="server">--%>
                        <%--<fieldset class="fieldset">
                            <legend class="legend"><b>Sub Pay Head Report</b></legend>
                            <br />--%>
                            <table width="100%" cellpadding="2" cellspacing="2" border="0">
                                <tr>
                                    <td class="form_left_label">
                                    <%--   <span style="color:Red">*</span>--%> <%--Month / Year :
                                    </td>
                                   
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlMonthYear" runat="server" Width="250px" AppendDataBoundItems="True"
                                            TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                            ControlToValidate="ddlMonthYear" Display="None" ErrorMessage="Please Select Month/Year"
                                            ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                 <tr>
                                     <td class="form_left_label">
                                      <%--College Name :
                                     </td>
                                     <td class="form_left_text">
                                     <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" Width="300px"
                                          AutoPostBack="true">
                                           <asp:ListItem Value="0">Please Select</asp:ListItem>
                                      </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                         Display="None" SetFocusOnError="true" ErrorMessage="Please Select College Name"
                                         ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        </td>
                                </tr>
                    
                                <tr>
                                    <td class="form_left_label">
                                      &nbsp;  <%--Staff :
                                    </td>
                                   
                                    <td class="form_left_text">
                                        
                                        <asp:ListBox ID="lstStaffName" runat="server" SelectionMode="Multiple" BackColor="#E0EAF1"
                                      Height="140px" Width="60%" TabIndex="10" AppendDataBoundItems="True">
                                     </asp:ListBox>
                     <asp:RequiredFieldValidator ID="rfvLstParticularColumn" runat="server" ControlToValidate="lstStaffName"
                        Display="None" ErrorMessage="Please Select Atleast one Staff." SetFocusOnError="true" InitialValue="0" 
                        ValidationGroup="Payroll" />
                    <asp:HiddenField ID="hidIdNo" runat="server" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">
                                       &nbsp;  <%--Pay Head :
                                    </td>
                                   
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlPayhead" AppendDataBoundItems="true" runat="server" Width="200px"
                                            OnSelectedIndexChanged="ddlPayhead_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlPayhead" runat="server" ControlToValidate="ddlPayhead"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select Payhead" ValidationGroup="payroll"
                                            InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">
                                       <%--<span style="color:Red">*</span>--%>   <%--Sub-Pay Head :
                                    </td>
                                    
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlSubpayhead" AppendDataBoundItems="true" runat="server" Width="200px">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSubpayhead" runat="server" ControlToValidate="ddlSubpayhead"
                                            Display="None" ErrorMessage="Please Select Sub Payhead" ValidationGroup="Payroll"
                                            InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <%--  Reset the sample so it can be played again --%>
                                <%--  Enable the button so it can be played again --%>
                                <tr>
                                    <td class="form_left_label">
                                       &nbsp;<%-- Cheque No. :
                                    </td>
                                   
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtAccNo" runat="server" MaxLength="6" onKeyUp="return validateNumeric(this)"></asp:TextBox>--%>
                                        <%--  Shrink the info panel out of view --%>
                                    </td>
                                </tr>
                                <tr>
                                <td  class="form_left_label">
                                <%--<span style="color:Red">*</span>  Cheque Submission Date :
                                </td>
                                
                                <td class="form_left_text">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" />
                        <asp:Image ID="imgCalFromDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                        <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                                            Display="None" ErrorMessage="Please enter Submission Date" ValidationGroup="Payroll"></asp:RequiredFieldValidator>
                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate" MaskType="Date" Mask="99/99/9999"></ajaxToolKit:MaskedEditExtender>
                                </td>--%>
                                
                                </tr>
                                <tr id="tr1" runat="server">
                                    <td class="form_left_label">
                                        
                                    </td>
                                    <td class="form_left_label">
                                    </td>
                                    <td class="form_left_text">
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        <div style="width: 100%">
                            <center>
                               <%-- <asp:Button ID="btnShowReport" runat="server" Text="Show" Width="79px" OnClick="btnShowReport_Click"
                                    TabIndex="9" ValidationGroup="Payroll" />
                                &nbsp; &nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="79px" OnClick="btnCancel_Click"
                                    TabIndex="11" />
                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                            DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />--%>
                            </center>
                        </div>
                </div>
                </center>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>


    <script type="text/javascript">

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
