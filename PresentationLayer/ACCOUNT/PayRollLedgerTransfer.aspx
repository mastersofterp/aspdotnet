<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PayRollLedgerTransfer.aspx.cs" Inherits="PayRollLedgerTransfer" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>

    <script language="javascript" type="text/javascript">
    </script>

    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>--%>

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
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDLedger"
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
    <asp:UpdatePanel ID="UPDLedger" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PAY ROLL LEDGER TRANSFER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" align="center" style="font-size: x-large; text-align: center" runat="server">
                                </div>
                            </div>
                            <asp:Panel ID="pnlShow" runat="server">
                                <div class="col-12">
                                   <%-- <div class="sub-heading">
                                        <h5>Transfer Payroll</h5>
                                    </div>--%>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-12" id="Div3" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-7 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>College Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                           <asp:ListItem Value="0" Text="Please Select Staff" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Please Select College" runat="server" ControlToValidate="ddlCollege" InitialValue="0" ValidationGroup="valdept" Display="None"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-5 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Staff</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AppendDataBoundItems="true">
                                                          <asp:ListItem Value="0" Text="Please Select" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Please Select Staff" runat="server" ControlToValidate="ddlDegree" InitialValue="0" ValidationGroup="valdept" Display="None"></asp:RequiredFieldValidator>

                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-12" id="Div7" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Department</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">                                                     
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddldept" ErrorMessage="Please Select Department" runat="server" ControlToValidate="ddldept" InitialValue="0" ValidationGroup="valdept" Display="None"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="Image1">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"
                                                            AutoPostBack="false" meta:resourcekey="txtDateResource1" />
                                                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            PopupButtonID="Image1" TargetControlID="txtDate">
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
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-12" id="Div5" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Month</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlMon" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlPARTY_SelectedIndexChanged" AppendDataBoundItems="true">
                                                         <asp:ListItem Value="0" Text="Please Select" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Please Select Month" runat="server" ControlToValidate="ddlMon" InitialValue="0" ValidationGroup="valdept" Display="None"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Bank</label>
                                                    </div>
                                                    <asp:Label ID="lblBank" CssClass="form-control" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hidfBankNo" runat="server"/>
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 btn-footer" id="Div6" runat="server">
                                    <asp:Button ID="Button1" runat="server" Text="Show " OnClick="btnShowData_Click" ValidationGroup="valdept"
                                        CssClass="btn btn-primary" />
                                    <asp:ValidationSummary ID="vmdept" runat="server" ValidationGroup="valdept" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                                <div class="form-group row" id="trDetail" runat="server" visible="false">
                                    <div class="col-12">
                                        <input id="hdnAgParty" runat="server" type="hidden" />
                                        <input id="hdnOparty" runat="server" type="hidden" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="panOne" runat="server">
                                            <asp:Panel ID="ScrollPanel" runat="server">
                                                <div class="table table-responsive">
                                                    <asp:GridView ID="GridData" runat="server" GridLines="Vertical"
                                                        AutoGenerateColumns="False" CssClass="table table-striped table-bordered nowrap"
                                                        BorderStyle="None" BorderWidth="1px" OnRowCreated="GridData_RowCreated" AllowSorting="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="PayHead No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPayHeadNo" runat="server" Text='<%#Bind("PAY_HEAD_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PayHead Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPayHeads" runat="server" Text='<%#Bind("PAY_HEAD_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ledger Head" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLEDGER" runat="server" Text='<%#Bind("PARTY_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
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
                                                        <HeaderStyle CssClass="bg-light-blue" Font-Bold="True" ForeColor="#000" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                            <div class="col-12 mt-2" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Total Amount:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblTamt" runat="server"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>


                                        </asp:Panel>
                                    </div>
                                </div>

                                <div id="Tr7" runat="server" visible="false" class=" ">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Employer Share </h5>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="pnlEmpShare" runat="server">
                                            <asp:Panel ID="pnlEmpShare1" runat="server">
                                                <div class="table table-responsive">
                                                    <asp:GridView ID="gvEmpShare" runat="server" CellPadding="4" GridLines="Vertical"
                                                        AutoGenerateColumns="False" Width="100%" CssClass="table table-striped table-bordered nowrap"
                                                        AllowSorting="True">
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
                                                        <HeaderStyle CssClass="bg-light-blue" Font-Bold="True" ForeColor="#000" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                            <div class="col-12 mt-2">
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Total Amount:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblEmpShareAmt" runat="server"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
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

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
