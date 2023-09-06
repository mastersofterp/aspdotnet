<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="GetRecordsForTallyTransfer.aspx.cs" Inherits="Tally_Transactions_GetRecordsForTallyTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  
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
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PAYROLL TALLY TRANSFER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Selection Criteria</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollegeName" runat="server" AppendDataBoundItems="True"
                                            ValidationGroup="Submit" ToolTip="Please Select College" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCollegeName" runat="server" ControlToValidate="ddlCollegeName"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Staff Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlstafftype" AutoPostBack="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" OnSelectedIndexChanged="ddlstafftype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCashbook" runat="server" ControlToValidate="ddlstafftype"
                                            Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>
                                                <asp:Label ID="lblPayMonth" CssClass="control-label" runat="server" Text="Pay Month"></asp:Label></label>
                                        </div>
                                        <asp:DropDownList ID="ddlPayMonth" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPayMonth"
                                            Display="None" ErrorMessage="Please Select Pay Month" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>
                                                <asp:Label ID="lblServerName" CssClass="control-label" runat="server" Text="Pay Date"></asp:Label></label>
                                        </div>
                                        <div class='input-group date' id="myDatepicker1">
                                            <asp:TextBox ID="txtPayDate" runat="server" TabIndex="7" ToolTip="Please Enter Pay Date" CssClass="form-control datepickerinput" />
                                            <div class="input-group-addon" id="imgCalDateOfBirth" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <%--<asp:Image ID="imgCalDateOfBirth" runat="server" src="../../images/calendar.png"
                                                        Style="cursor: pointer" TabIndex="8" Height="16px" />--%>
                                            <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="txtPayDate"
                                                Display="None" ErrorMessage="Please Enter Pay Date" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtPayDate" PopupButtonID="imgCalDateOfBirth" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server" TargetControlID="txtPayDate"
                                                Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-2" id="Div2" runat="server" visible="false">
                                <div class="sub-heading">
                                    <h5>Amount Description </h5>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>CASH :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCashAmount" runat="server" Text="--" Font-Bold="true">
                                                    </asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item d-none"><b>CHEQUE :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblChequeAmount" runat="server" Text="--" Font-Bold="true"> </asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>DEMAND DRAFT :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDDAmount" runat="server" Text="--" Font-Bold="true">
                                                    </asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>TOTAL AMOUNT:</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblTotalAmount" runat="server" Text="--" Font-Bold="true">
                                                    </asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>



                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" TabIndex="5" Text="Show" ToolTip="Click to Save"
                                    ValidationGroup="Submit" class="btn btn-primary" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';"
                                    UseSubmitBehavior="false" OnClick="btnShow_Click" Font-Bold="true" CssClass="btn btn-primary" />

                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" class="btn btn-warning"
                                    TabIndex="7" Text="Cancel" ToolTip="Click to Cancel" OnClientClick="Cancel()"
                                    OnClick="btnCancel_Click" Font-Bold="true" CssClass="btn btn-danger" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" Font-Bold="true" />

                            </div>

                            <div class="col-12">
                                <asp:Panel ID="DivReceipt" runat="server" Visible="false">
                                    <div class="table table-responsive mb-3">
                                    <asp:GridView ID="GridData" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                        AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-hover" 
                                        BorderStyle="None" BorderWidth="1px" AllowSorting="True">
                                        <RowStyle CssClass="bg-light-blue" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="PayHead No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPayHeadNo" runat="server" Text='<%#Bind("PAYHEAD") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PayHead Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPayHeads" runat="server" Text='<%#Bind("FULLNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ledger Head">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLEDGER" runat="server" Text='<%#Bind("CashLedgerName") %>'></asp:Label>
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
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="#000" />
                                        <HeaderStyle CssClass="bg-light-blue" Width="100%" ForeColor="#000" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                   </div>

                                    <div class="text-center">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary progress-button" Text="Save"
                                            OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';"
                                            UseSubmitBehavior="false" OnClick="btnSubmit_Click" />

                                        <asp:Button ID="btnTransfer" runat="server" CssClass="btn btn-info" Text="Transfer"
                                            OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';"
                                            UseSubmitBehavior="false" OnClick="btnTransfer_Click" Font-Bold="true" />
                                        <br />
                                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-warning" TabIndex="8" Text="Delete Already transfered Salary"
                                            ToolTip="Click to Delete Already transfer Salary" OnClick="btnDelete_Click" Visible="false" Font-Bold="true" />

                                        <ajaxToolKit:ConfirmButtonExtender ID="CnfDrop" runat="server" ConfirmText="Are you Sure, Want to delete Already transfer Salary.?"
                                            TargetControlID="btnDelete">
                                        </ajaxToolKit:ConfirmButtonExtender>
                                    </div>

                                </asp:Panel>
                            </div>
                            <%--</table>--%>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
