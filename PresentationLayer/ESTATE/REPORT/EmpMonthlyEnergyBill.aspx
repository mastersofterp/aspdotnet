<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EmpMonthlyEnergyBill.aspx.cs" Inherits="ESTATE_Report_EmpMonthlyEnergyBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="updReport" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">EMPLOYEE MONTHLY BILLING REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlReport" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            MONTHLY BILLING REPORT 
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Select Bill Month<span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtselectdate" runat="server" MaxLength="7" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="calextenderdatebirth" runat="server" Format="MM/yyyy"
                                                            TargetControlID="txtselectdate" PopupButtonID="imgCal"
                                                            Enabled="True" />
                                                        <%--<ajaxToolKit:MaskedEditExtender ID="msedatebirth" runat="server" TargetControlID="txtselectdate"
                                Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                CultureTimePlaceholder="" Enabled="True" />--%>
                                                        <div class="input-group-addon">
                                                            <%--<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                            <asp:ImageButton runat="Server" ID="imgCal" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnEnergyReport" runat="server" Text="Energy Billing" CssClass="btn btn-primary" OnClick="btnReport_Click" TabIndex="2" />
                                                    <asp:Button ID="btnreset" runat="server" Text="Reset" CssClass="btn btn-warning" OnClick="btnreset_Click" TabIndex="3" />
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
            <%-- <table width="100%" cellspacing="1" cellpadding="1" class="section_header">
                <tr id="trSearch" runat="server" visible="true">

                    <td style="width: 10%;"></td>
                    <td style="width: 15%; font-family: Verdana; font-size: small; font-weight: bold; color: #ffffff;" align="right">Search Resident</td>
                    <td style="width: 5%;">
                        <center><b>:</b></center>
                    </td>


                    <td style="width: 70%;">
                        <asp:TextBox ID="txtSearch" runat="server" Width="300px" class="tbConsumer"></asp:TextBox>
                        <asp:HiddenField ID="hfInvNo" runat="server" />

                        <ajaxToolKit:AutoCompleteExtender ID="txtSearch_AutoCompleteExtender" runat="server"
                            Enabled="True" ServicePath="~/Autocomplete.asmx" TargetControlID="txtSearch"
                            CompletionSetCount="6" ServiceMethod="GetEstateEmployee" MinimumPrefixLength="3" CompletionInterval="0"
                            CompletionListCssClass="autocomplete_completionListElement" OnClientItemSelected="GetEstateEmployee"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem">
                        </ajaxToolKit:AutoCompleteExtender>


                        &nbsp;  <span style="font-family: Verdana; font-size: small; font-weight: bold; color: #ffffff;">Individual Bill Calculation</span>

                    </td>

                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript">
        //function GetEstateEmployee(source, eventArgs) {
        //    var idno = eventArgs.get_value().split("*");
        //    var Name = idno[0].split("-");
        //    document.getElementById('ctl00_ContentPlaceHolder1_txtSearch').value = Name[0];
        //    document.getElementById('ctl00_ContentPlaceHolder1_hfInvNo').value = idno[1];
        //}

        function CheckAlphabet(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }

    </script>
</asp:Content>

