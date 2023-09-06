<%--<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ACC_APPROVED_AMOUNT_NEW.aspx.cs" Inherits="ACC_APPROVED_AMOUNT_NEW" %>--%>

<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ACC_APPROVED_AMOUNT_NEW.aspx.cs" Inherits="ACC_APPROVED_AMOUNT_NEW" Title="" %>

<%--<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPBudgetApprove"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <%--<div style="width: 100%">--%>
    <asp:UpdatePanel ID="UPBudgetApprove" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="divMsg" runat="server">
                            <div id="div1" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">BUDGET APPROVAL</h3>
                            </div>
                            <div class="box-body">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Department</label>
                                                </div>
                                                <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true"
                                                    OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">--Please Select--</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvdept" runat="server" ControlToValidate="ddldept"
                                                    ValidationGroup="budget" Display="None" InitialValue="0" ErrorMessage="Please Select Department">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>From Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="Image3">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <span id="lblSpan" runat="server" style="text-align: left; display: none"></span>
                                                    <asp:TextBox ID="txtFromDate" Text="" runat="server" Style="text-align: left" Enabled="true"
                                                        CssClass="form-control" />
                                                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        PopupButtonID="Image3" TargetControlID="txtFromDate">
                                                    </ajaxToolKit:CalendarExtender>

                                                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                        OnInvalidCssClass="errordate" TargetControlID="txtFromDate" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate" ErrorMessage="Please Enter From Date" Display="None" ValidationGroup="budget"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>To Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="Image1">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <span id="Span1" runat="server" style="text-align: left; display: none"></span>
                                                    <asp:TextBox ID="txttodate" runat="server" Style="text-align: left" Enabled="true"
                                                        CssClass="form-control" Text="" OnTextChanged="txttodate_TextChanged" AutoPostBack="true" />
                                                    <ajaxToolKit:CalendarExtender ID="cetodate" runat="server" Enabled="true" Format="dd/MM/yyyy"
                                                        PopupButtonID="Image1" TargetControlID="txttodate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="metodate" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                        OnInvalidCssClass="errordate" TargetControlID="txttodate" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <asp:RequiredFieldValidator ID="rfvtodate" runat="server" ControlToValidate="txttodate" ErrorMessage="Please Enter ToDate" Display="None" ValidationGroup="budget"></asp:RequiredFieldValidator>
                                                    <%--  <asp:RequiredFieldValidator ID="rfvtdate" runat="server" ControlToValidate="txttodate" Display="None" ErrorMessage="Please Enter To_Date" ValidationGroup="bhn">
                                                        </asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Parent Budget Head </label>
                                                </div>
                                                <asp:DropDownList ID="ddlbudgetparent" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlbudgetparent_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select Budget" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:CheckBox ID="chkRevised" runat="server" Text="Revised Budget" OnCheckedChanged="chkRevised_CheckedChanged" AutoPostBack="true" />
                                                <asp:CheckBox ID="chkProposed" runat="server" Text="Proposed Budget" OnCheckedChanged="chkRevised_CheckedChanged" AutoPostBack="true" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" ValidationGroup="budget" Text="Show" OnClick="btnShow_Click" />
                                        <asp:Button ID="btnclear" runat="server" CssClass="btn btn-warning" Text="Clear" OnClick="btnclear_Click" />

                                        <asp:ValidationSummary ID="bhn" runat="server" ValidationGroup="bhn" ShowMessageBox="true" ShowSummary="false" />
                                        <asp:ValidationSummary ID="vsbudget" runat="server" ValidationGroup="budget" ShowMessageBox="true" ShowSummary="false" />
                                    </div>
                                    <div class="col-12 mt-3">
                                        <div class="row" >
                                            <div class="col-lg-4 col-md-6 col-12" id="divstatus" runat="server" visible="false">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b></b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="txtstatus" runat="server" Text="Budget is approved for this department"></asp:Label>
                                                        </a>
                                                    </li>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 mt-3">
                                        <asp:Panel ID="pnlBudgetApprove" runat="server" Visible="false">
                                            <asp:ListView ID="Rptbudgethead" runat="server">
                                                <LayoutTemplate>
                                                    <div class="lgv1">
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>
                                                                        <asp:CheckBox ID="cbHead" class="chkApproval" runat="server" onclick="totAllSubjects(this)" />
                                                                    </th>
                                                                    <th>Budget Code </th>
                                                                    <th>Budget Head </th>
                                                                    <th>Budget Amount </th>
                                                                    <th>Approved Status </th>
                                                                    <th>
                                                                        <asp:Label ID="lblRev" runat="server" Text="Revised Amount"></asp:Label>
                                                                    </th>
                                                                    <th>Revised Status </th>
                                                                    <th>
                                                                        <asp:Label ID="lblProp" runat="server" Text="Proposed Amount"></asp:Label>
                                                                    </th>
                                                                    <th>Proposed Status </th>
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
                                                            <asp:CheckBox ID="chkSelect" runat="server" class="chkApproval" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("BUDGET_CODE")%>
                                                            <asp:HiddenField ID="hdnbudgetno" runat="server" Value='<%# Eval("BUDGET_NO")%>' />
                                                        </td>
                                                        <td>
                                                            <%# Eval("BUDGET_HEAD")%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtbudgetallocation" runat="server" Text='<%# Eval("DEPT_AMOUNT")%>' CssClass="form-control" Style="text-align: right" MaxLength="15"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbbudget" runat="server" TargetControlID="txtbudgetallocation" FilterType="Numbers, Custom" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("APPROVED_AMOUNT").ToString().Equals("0")?"PENDING": "APPROVED"%>' ForeColor='<%# Eval("APPROVED_AMOUNT").ToString().Equals("0.00")?System.Drawing.ColorTranslator.FromHtml("#3090C7"): System.Drawing.ColorTranslator.FromHtml("#008000")%>' Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRevised" runat="server" MaxLength="10" Text='<%# Eval("REV_DEPT_AMOUNT")%>' TabIndex="2" CssClass="form-control" Style="text-align: right" Visible="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FTREV" runat="server" TargetControlID="txtRevised" FilterType="Numbers, Custom" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>

                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRevStatus" runat="server" Visible="false" Text='<%# Eval("REV_APPROVED_AMOUNT").ToString().Equals("0")?"PENDING": "APPROVED"%>' ForeColor='<%# Eval("REV_APPROVED_AMOUNT").ToString().Equals("0")?System.Drawing.ColorTranslator.FromHtml("#3090C7"): System.Drawing.ColorTranslator.FromHtml("#008000")%>' Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProposed" runat="server" MaxLength="10" Text='<%# Eval("NXTYR_DEPT_AMOUNT")%>' TabIndex="3" CssClass="form-control" Style="text-align: right" Visible="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FTPOR" runat="server" TargetControlID="txtProposed" FilterType="Numbers, Custom" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPropStatus" runat="server" Visible="false" Text='<%# Eval("NXTYR_APPROVED_AMOUNT").ToString().Equals("0")?"PENDING": "APPROVED"%>' ForeColor='<%# Eval("NXTYR_APPROVED_AMOUNT").ToString().Equals("0")?System.Drawing.ColorTranslator.FromHtml("#3090C7"): System.Drawing.ColorTranslator.FromHtml("#008000")%>' Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <%--<div class="col-md-1"></div>--%>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary" Text="Budget Amount Approve" OnClick="btnsubmit_Click" ValidationGroup="bhn" Visible="false" />
                                        <asp:Button ID="btnRevied" runat="server" CssClass="btn btn-primary" Text="Revised Amount Approve" OnClick="btnRevied_Click" ValidationGroup="bhn" Visible="false" />
                                        <asp:Button ID="btnProposed" runat="server" CssClass="btn btn-primary" Text="Proposed Amount Approve" OnClick="btnProposed_Click" ValidationGroup="bhn" Visible="false" />
                                    </div>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--</div>--%>
    <script type="text/javascript">
        function totAllSubjects(headchk) {

            //var frm = document.forms[0]
            var value = document.getElementsByClassName("chkApproval");

            //for (i = 0; i < document.forms[0].elements.length; i++) {

            for (i = 0; i < value.length; i++) {
                //value[i].children[0].checked = true;
                //var e = frm.elements[i];
                //if (e.type == 'checkbox') {
                if (headchk.checked == true) {
                    value[i].children[0].checked = true;
                }
                else {
                    value[i].children[0].checked = false;
                }
            }
        }

    </script>
</asp:Content>

