<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Acc_BudgetDetails_Entry.aspx.cs" Inherits="ACCOUNT_Acc_BudgetDetails_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPBudget"
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
    <asp:UpdatePanel ID="UPBudget" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="divMsg" runat="server">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BUDGET ALLOTMENT ENTRY </h3>
                        </div>
                       
                        <div class="box-body">
                            <div class="col-12 mb-3">
                                 <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                            </div>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <%--<div class="panel-heading">BUDGET ENTRY </div>--%>
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
                                                ValidationGroup="bhn" Display="None" InitialValue="0" ErrorMessage="Please Select Department">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>From Date</label>
                                            </div>
                                           <div class="input-group date">
                                        <div class="input-group-addon" id="Image3">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                                <span id="lblSpan" runat="server" style="text-align: left; display: none"></span>
                                                <asp:TextBox ID="txtFromDate" runat="server" Style="text-align: left"
                                                    CssClass="form-control" />
                                                <cc1:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    PopupButtonID="Image3" TargetControlID="txtFromDate">
                                                </cc1:CalendarExtender>
                                                <cc1:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtFromDate" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True">
                                                </cc1:MaskedEditExtender>
                                                <asp:RequiredFieldValidator ID="rfvfdate" runat="server" ControlToValidate="txtFromDate" Display="None" ErrorMessage="Please Enter From_Date" ValidationGroup="bhn">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>To Date</label>
                                            </div>
                                          <div class="input-group date">
                                        <div class="input-group-addon" id="Image1">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                                <span id="Span1" runat="server" style="text-align: left; display: none"></span>
                                                <asp:TextBox ID="txttodate" runat="server" Style="text-align: left"
                                                    CssClass="form-control" OnTextChanged="txttodate_TextChanged" AutoPostBack="true" />
                                                <cc1:CalendarExtender ID="cetodate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    PopupButtonID="Image1" TargetControlID="txttodate">
                                                </cc1:CalendarExtender>
                                                <cc1:MaskedEditExtender ID="metodate" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                    OnInvalidCssClass="errordate" TargetControlID="txttodate" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True">
                                                </cc1:MaskedEditExtender>
                                                <asp:RequiredFieldValidator ID="rfvtdate" runat="server" ControlToValidate="txttodate" Display="None" ErrorMessage="Please Enter To_Date" ValidationGroup="bhn">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Parent Budget Head</label>
                                            </div>
                                            <asp:DropDownList ID="ddlbudgetparent" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlbudgetparent_SelectedIndexChanged">
                                                <asp:ListItem Text="Select Budget" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-5 col-md-6 col-12" runat="server" id="checkbudget">
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
                                        <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Text="Show" OnClientClick="chkdate()" OnClick="btnShow_Click" />
                                        <%--<asp:Button ID="btnadd" runat="server" CssClass="btn btn-info" Text="Add" OnClick="btnadd_Click" ValidationGroup="bhn" />--%>
                                        <asp:Button ID="btnclear" runat="server" CssClass="btn btn-warning" Text="Clear" OnClick="btnclear_Click" />
                                        <%-- <asp:Button ID="btnView" runat="server" CssClass="btn btn-btn-warning" Text="View" OnClick="btnView_Click" />--%>
                                        <asp:ValidationSummary ID="bhn" runat="server" ValidationGroup="bhn" ShowMessageBox="true" ShowSummary="false" />
                                    </div>
                         
                                <div class="col-12 mt-3" id="bud">
                                        <asp:Panel ID="pnlBudget" runat="server" Visible="false">

                                            <asp:ListView ID="lvbudgethead" runat="server" OnItemDataBound="lvbudgethead_ItemDataBound1">
                                                <LayoutTemplate>
                                                    <div class="lgv1">
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Budget Code
                                                                    </th>
                                                                    <th>Budget Head
                                                                    </th>
                                                                    <th>
                                                                    Budget Amount
                                                                    </th>
                                                                    <th>
                                                                    <asp:Label ID="lblRev" runat="server" Text="Revised Amount" ></asp:Label>
                                                                    </th>
                                                                    <th>
                                                                    <asp:Label ID="lblProp" runat="server" Text="Proposed Amount" ></asp:Label>
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
                                                        <td style="width:15%")%>
                                                             <%# Eval("BUDGET_CODE")%>
                                                            <asp:HiddenField ID="hdnbudgetno" runat="server" Value='<%# Eval("BUDGET_NO")%>' />
                                                        </td>
                                                        <td style="width: 25%">
                                                            <%# Eval("BUDGET_HEAD")%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtbudgetallocation" runat="server" MaxLength="15" Enabled="true" Text='<%# Eval("DEPT_AMOUNT")%>' TabIndex="1" CssClass="form-control" Style="text-align: right"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftbbudget" runat="server" TargetControlID="txtbudgetallocation" FilterType="Numbers, Custom" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                            <asp:Label ID="lnkEdit" runat="server" Text='<%#Eval ("APPROVED_SATUS")%>' Visible="false"></asp:Label>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRevised" runat="server" MaxLength="10" Text='<%# Eval("REV_DEPT_AMOUNT")%>' TabIndex="2" CssClass="form-control" Style="text-align: right" Visible="false"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRevised" FilterType="Numbers, Custom" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                            <asp:Label ID="lblRevStatus" runat="server" Text='<%#Eval ("REV_APPROVED_SATUS")%>' Visible="false"></asp:Label>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProposed" runat="server" MaxLength="10" Text='<%# Eval("NXTYR_DEPT_AMOUNT")%>' TabIndex="3" CssClass="form-control" Style="text-align: right" Visible="false"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtProposed" FilterType="Numbers, Custom" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                            <asp:Label ID="lblPropStatus" runat="server" Text='<%#Eval ("NXTYR_APPROVED_SATUS")%>' Visible="false"></asp:Label>

                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                        </asp:Panel>

                                    </div>
                                  
                                
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary" Text="Submit" TabIndex="4" Enabled="true" OnClick="btnsubmit_Click" ValidationGroup="bhn" Visible="false" />

                        </div>

                  
                    </asp:Panel>
                </div>
            </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
  

    <script type="text/javascript">
        function chkdate() {
            var frmdate = document.getElementById('<%=ddldept.ClientID%>');

            if (frmdate.selectedIndex == 0) {
                alert('Please select Department');
                return false;
            }
        }
    </script>

</asp:Content>

