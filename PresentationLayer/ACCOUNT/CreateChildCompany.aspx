<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CreateChildCompany.aspx.cs" Inherits="Account_CreateChildCompany" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updpan">

        <ProgressTemplate>

            <div id="IMGDIV" class="outerDivs" align="center" valign="middle" runat="server"
                style="position: absolute; left: 0%; top: 0%; visibility: visible; border-style: none; background-color: Transparent Black; z-index: 40; height: 100%; width: 100%;">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td valign="middle" align="center">
                            <img src="images/BarRotation.gif" style="height: 30px" /><br />
                            <%--Progressing Request....  --%>
                        </td>
                    </tr>
                </table>


            </div>

        </ProgressTemplate>

    </asp:UpdateProgress>

    <script language="javascript" type="text/javascript">
        function AskConfirmation() {

            if (document.getElementById('ctl00_ctp_txtCode').value == '') {
                alert('Select a company/Cash Book For Dropping.');
                document.getElementById('ctl00_ctp_txtCode').focus();
                return false;

            }

            document.getElementById('ctl00_ctp_hdnConfirm').value = confirm('Your Are About To Delete The Selected Company/Cash Book, Do You Want To Continue ?');
            document.getElementById('ctl00_ctp_btndelete').focus();
            return true;

        }

        function AskToDelete() {
            if (confirm('Do You Want To Drop Cash Book ? ') == true) {
                document.getElementById('ContentPlaceHolder1_hdnConfirm').value = 1;
                return true;
            }
            else {
                document.getElementById('ContentPlaceHolder1_hdnConfirm').value = 0;
                return false;
            }
        }

        function filterDigits(txt) {
            var a = txt.value;
            var c = a.slice(-1)
            if (c == ',' || c == '!' || c == '#' || c == '$' || c == '%' || c == '^' || c == '&' || c == '*' || c == '@') {
                a = a.substring(0, a.length - 1);
                txt.value = a;
                alert('Special Charcters are Not Allowed');
            }
        }

    </script>


    <asp:UpdatePanel ID="updpan" runat="server">
        <ContentTemplate>
            <div class="form-group col-md-12">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">CREATE COMPANY/CASH BOOK
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                                <%--<div id="Div1" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                        </div>--%>
                            </h3>
                        </div>

                        <div>
                            <div class="form-group-row">
                                <asp:Panel ID="Panel_Error" runat="server" CssClass="Panel_Error" EnableViewState="false"
                                    Visible="false">
                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width: 3%; vertical-align: top">
                                                <img src="../images/error.gif" align="absmiddle" alt="Error" />&nbsp;</td>
                                            <td style="width: 97%"><font style="font-family: Verdana; font-family: 11px; font-weight: bold; color: #CD0A0A"> </font>
                                                <asp:Label ID="Label_ErrorMessage" runat="server" Style="font-family: Verdana; font-size: 11px; color: #CD0A0A"></asp:Label></td>


                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                    Visible="false">

                                    <div>
                                        <td style="width: 3%; vertical-align: top">
                                            <img src="../images/confirm.gif" align="absmiddle" alt="confirm" />&nbsp;</td>
                                        <td style="width: 97%">
                                            <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label></td>
                                    </div>

                                </asp:Panel>
                            </div>

                        </div>
                        
                        <div class="box-body">
                            <div class="col-md-12">

                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">

                                        <div class="panel-heading">New Child Company</div>
                                        <div class="panel-body">

                                            <div class="col-md-12">

                                                <div class="form-group row">
                                                    <div class="form_left_label" colspan="2">
                                                        &nbsp;
                                                        <label>Note </label>
                                                        <span style="font-size: small">:</span>
                                                        <span style="font-weight: bold; font-size: x-small; color: red">* Marked is mandatory !</span>
                                                    </div>

                                                </div>

                                                <div class="col-md-6">

                                                    <div class="form-group row">
                                                        <div class="col-md-4">
                                                            
                                                            <label>Code : </label>
                                                            <span style="color: #FF0000">*</span>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtCode" ToolTip="Please Enter Code" CssClass="form-control"
                                                                Style="text-transform: uppercase" runat="server" MaxLength="4"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="Please Enter Code"
                                                                ControlToValidate="txtCode" Display="None" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                        </div>

                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-md-4" style="height: 9px">
                                                            
                                                            <label>Name : </label>
                                                            <span style="color: #FF0000">*</span>
                                                            <input id="hdnConfirm" runat="server" type="hidden" />
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtName" ToolTip="Please Enter Name" CssClass="form-control"
                                                                Style="text-transform: uppercase" Width="95%" runat="server"
                                                                TextMode="MultiLine" onkeyup="filterDigits(this)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Please Enter Name"
                                                                ControlToValidate="txtName" Display="None" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-md-4">
                                                            
                                                            <label>Fin Year From : </label>
                                                            <span style="color: #FF0000">*</span>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="input-group date">

                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtFromDate" ToolTip="Please Enter From Date" CssClass="form-control"
                                                                    Style="text-align: right" runat="server"> </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                                    Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="True"
                                                                    ValidationGroup="submit" />
                                                                <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true"
                                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal"
                                                                    PopupPosition="BottomLeft" TargetControlID="txtFromDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server"
                                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                                    OnInvalidCssClass="errordate" TargetControlID="txtFromDate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-md-4">
                                                            
                                                            <label>Fin Year To : </label>
                                                            <span style="color: #FF0000">*</span>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtToDate" ToolTip="Please Enter To Date" Style="text-align: right" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                                    Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="True"
                                                                    ValidationGroup="submit" />
                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1"
                                                                    PopupPosition="BottomLeft" TargetControlID="txtToDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                                    OnInvalidCssClass="errordate" TargetControlID="txtToDate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <input id="hdnCompNo" runat="server" type="hidden" />
                                                                <input id="hdnOldFinYr" runat="server" type="hidden" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-md-4">
                                                            
                                                            <label>Book Writing Dt : </label>
                                                            <span style="color: #FF0000">*</span>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtBWDate" CssClass="form-control" ToolTip="Please Enter Book Writing Date" Style="text-align: right" runat="server"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvBWDate" runat="server" ControlToValidate="txtBWDate"
                                                                    Display="None" ErrorMessage="Please Enter Book Writing Date" SetFocusOnError="True"
                                                                    ValidationGroup="submit" />
                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2"
                                                                    PopupPosition="BottomLeft" TargetControlID="txtBWDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                                    OnInvalidCssClass="errordate" TargetControlID="txtBWDate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-md-2"></div>
                                                        <div class="col-md-3">
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success form-control"
                                                                ValidationGroup="submit" OnClick="btnSubmit_Click" />
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning form-control"
                                                                CausesValidation="false" OnClick="btnCancel_Click" />
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:Button ID="btndelete" runat="server" Text="Drop Company" CssClass="btn btn-danger form-control"
                                                                CausesValidation="false" OnClick="btndelete_Click" OnClientClick="return AskToDelete()" />
                                                        </div>
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                                    </div>

                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-group row">
                                                        <asp:ListBox ID="lstCompany" runat="server" Rows="18" Style="overflow: scroll"
                                                            AutoPostBack="True" CssClass="form-control"
                                                            OnSelectedIndexChanged="lstCompany_SelectedIndexChanged" TabIndex="-1"></asp:ListBox>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            </table>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
</asp:Content>
