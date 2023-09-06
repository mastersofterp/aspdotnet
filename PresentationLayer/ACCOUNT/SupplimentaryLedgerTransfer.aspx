<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SupplimentaryLedgerTransfer.aspx.cs" Inherits="ACCOUNT_SupplimentaryLedgerTransfer" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>

    <script language="javascript" type="text/javascript">
    </script>

    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>


    <script type="text/javascript" lang="javascript">

        $(document).on('change', 'input[id="chkAll"]', function () {

            if (this.checked) {
                $("[id$='chkSelect']").prop('checked', this.checked).change();
            }
            else {
                $("[id$='chkSelect']").prop('checked', false).change();
            }
        });
    </script>

    <script language="javascript" type="text/javascript">
        function CheckNumeric(obj) {
            var k = (window.event) ? event.keyCode : event.which;
            if (k == 68 || k == 67 || k == 8 || k == 9 || k == 36 || k == 35 || k == 16 || k == 37 || k == 38 || k == 39 || k == 40 || k == 46 || k == 13 || k == 110) {
                if (obj.value == '') {
                    alert('Field Cannot Be Blank');
                    obj.focus();
                    return false;
                }
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58 || k > 95 && k < 106) {
                obj.style.backgroundColor = "White";
                return true;
            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }

        function updateValues(popupValues) {
            document.getElementById('ctl00_ContentPlaceHolder1_hdnPartyNo').value = popupValues[0];
            document.forms(0).submit();
        }
    </script>
    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UPDLedger"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UPDLedger" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SUPPLIMENTRY LEDGER TRANSFER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div id="divCompName" align="center" style="font-size: x-large; text-align: center" runat="server">
                                </div>
                                <asp:Panel ID="pnlShow" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Transfer Supplimentry Bill</div>
                                        <div class="panel-body">
                                            <div class="form-group row" id="Div3" runat="server">
                                                <div class="col-md-2">
                                                    <label>College Name:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                           
                                            <div class="form-group row" id="Div4" runat="server">
                                                <div class="col-md-2">
                                                    <label>Staff :</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                              <div class="form-group row" id="Div11" runat="server">
                                                <div class="col-md-2">
                                                    <label>Bank:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Label ID="lblBank" CssClass="form-control" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hidfBankNo" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group row" id="Div2" runat="server" style="display: none">
                                                <div class="col-md-2">
                                                    <label>Date :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"
                                                            Style="text-align: right" AutoPostBack="false" meta:resourcekey="txtDateResource1" />
                                                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            PopupButtonID="imgCal" TargetControlID="txtDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                            OnInvalidCssClass="errordate" TargetControlID="txtDate" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>From Date<span style="color: red">*</span> : </label>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtFromDate" Style="text-align: right" runat="server" CssClass="form-control" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFromDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFromDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label>To Date<span style="color: red">*</span> : </label>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtTodate" Style="text-align: right" runat="server" CssClass="form-control" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" PopupPosition="BottomLeft"
                                                            TargetControlID="txtTodate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtTodate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row" id="Div5" runat="server" style="display: none">
                                                <div class="col-md-2">
                                                    <label>Month:</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlMon" runat="server" CssClass="form-control"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlPARTY_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <br />
                                           
                                            <div class="form-group row" id="Div7" runat="server" visible="false">
                                                <div class="col-md-2">
                                                    <label>Transfer For</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:RadioButtonList runat="server" ID="rdosuppli" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Selected="True"> GENERAL &nbsp;&nbsp;  </asp:ListItem>
                                                       <%-- <asp:ListItem Value="1"> CONVEYANCE &nbsp;&nbsp; </asp:ListItem>
                                                        <asp:ListItem Value="2"> GUEST </asp:ListItem>--%>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                            <div class="form-group row" id="Div6" runat="server">
                                                <div class="col-md-2"></div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="Button1" runat="server" Text="Show " OnClick="btnShowData_Click"
                                                        CssClass="btn btn-success" />
                                                </div>
                                            </div>
                                            <div class="form-group row" id="trDetail" runat="server" visible="false">
                                                <div class="col-md-2">
                                                    <input id="hdnAgParty" runat="server" type="hidden" />
                                                    <input id="hdnOparty" runat="server" type="hidden" />
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Panel ID="panOne" runat="server">
                                                        <asp:Panel ID="ScrollPanel" Height="400px" runat="server" ScrollBars="Both">
                                                            <asp:GridView ID="GridData" runat="server" GridLines="Vertical"
                                                                AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                                                BorderStyle="None" BorderWidth="1px" OnRowCreated="GridData_RowCreated" AllowSorting="True">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkAll" runat="server" ClientIDMode="Static" onkeydown="return (event.keyCode!=13);" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" ToolTip='<%#Bind("SUPLTRXID") %>' runat="server" onkeydown="return (event.keyCode!=13);" ClientIDMode="Static" TabIndex="6" />

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Employee Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPayHeads" runat="server" Text='<%#Bind("NAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Month" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPayHeadNo" runat="server" Text='<%#Bind("MONYEAR") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Gross Amount" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmt1" runat="server" Text='<%#Bind("Gross") %>'> </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tax Amount" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmt2" runat="server" Text='<%#Bind("Tax") %>'> </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Conveyance Amount" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmt3" runat="server" Text='<%#Bind("Conveyance") %>'> </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Net Amount" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmt4" runat="server" Text='<%#Bind("NET_PAY") %>'> </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField />
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCC99" />
                                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#3C8DBC" Font-Bold="True" ForeColor="White" />
                                                                <AlternatingRowStyle BackColor="White" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                        <div style="display: none">
                                                            Total Amount&nbsp;&nbsp;
                                                            <asp:Label ID="lblTamt" Text="0" runat="server"></asp:Label>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;After Tax Deducted Amount&nbsp;&nbsp;
                                                         <asp:Label ID="lblFinAmt" Text="0" runat="server"></asp:Label>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <br />
                                            <div id="Tr7" runat="server" visible="false" class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Employer Share :</label>
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Panel ID="pnlEmpShare" runat="server">
                                                        <asp:Panel ID="pnlEmpShare1" Height="200px" runat="server" ScrollBars="Both">
                                                            <asp:GridView ID="gvEmpShare" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                                AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE"
                                                                BorderStyle="None" BorderWidth="1px" AllowSorting="True">
                                                                <RowStyle BackColor="#F7F7DE" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="PayHead No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPayHeadNo" runat="server" Text='<%#Bind("PAY_HEAD_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PayHead Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPayHeads" runat="server" Text='<%#Bind("PAY_HEAD_NAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Credit Ledger Head">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCrLEDGER" runat="server" Text='<%#Bind("PARTY_NAME_CR") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PayHead Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDrLedgerHead" runat="server" Text='<%#Bind("PARTY_NAME_DR") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmt" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField />
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCC99" />
                                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                                <AlternatingRowStyle BackColor="White" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                        Total Amount&nbsp;&nbsp;
                                                            <asp:Label ID="lblEmpShareAmt" runat="server"></asp:Label>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div class="form-group row" id="Tr8" runat="server" visible="false">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnSave" runat="server" Text="Transfer" CssClass="btn btn-primary" ValidationGroup="Validation"
                                                        OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Cancel" CssClass="btn btn-warning" />
                                                    <input id="Hidden1" runat="server" type="hidden" />
                                                    <input id="Hidden2" runat="server" type="hidden" />
                                                    <input id="hdnAskSave" runat="server" type="hidden" />
                                                    <input id="hdnVchId" runat="server" type="hidden" />
                                                    <input id="hdnbal2" runat="server" type="hidden" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
