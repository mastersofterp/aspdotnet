<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SuplimentrySalaryTransfer.aspx.cs" Inherits="PAYROLL_Tally_SuplimentrySalaryTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <img src="../images/ajax-loader.gif" alt="Loading" />
                Loading..
            </ProgressTemplate>
        </asp:UpdateProgress>
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

    <div class="heading">
        Selection Criteria
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>

            <%--<table cellpadding="0" cellspacing="0" border="0" width="100%" id="tblDetails" runat="server">--%>
            <div class="panel-group">
                <div class="panel panel-default with-collapse">
                    <div class="panel-heading">
                        <h4>Selection Criteria<a data-toggle="collapse" href="#collapse1"></a> </h4>
                    </div>
                    <div class="panel-body">
                        <div id="collapse1" class="panel-collapse collapse in">
                            <div class="clr-box">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="text-left">
                                            <span style="color: #FF0000; font-size: small">* Marked Is Mandatory !</span>
                                        </div>
                                        <div class="col-sm-12" id="tblDetails">
                                            <div class="row">
                                                <div class="col-sm-4 form-group">
                                                    <label><span style="color: Red;">*</span>College : </label>
                                                    <asp:DropDownList ID="ddlCollegeName" runat="server" AppendDataBoundItems="True" ValidationGroup="Submit"
                                                        ToolTip="Please Select College" TabIndex="1" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlCollegeName" runat="server" ControlToValidate="ddlCollegeName"
                                                        Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </div>


                                                <div class="col-sm-4 form-group">
                                                    <label><span style="color: #FF0000; font-weight: bold">*</span></label>
                                                    <asp:Label ID="lblCashBook" CssClass="control-label" runat="server" Text="Staff Type"></asp:Label>
                                                    <asp:DropDownList ID="ddlstafftype" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCashbook" runat="server" ControlToValidate="ddlstafftype"
                                                        Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Submit"
                                                        SetFocusOnError="True" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                                </div>


                                                <div class="col-sm-4 form-group">
                                                    <span style="color: #FF0000; font-weight: bold">*</span>
                                                    <asp:Label ID="lblPayMonth" CssClass="control-label" runat="server" Text="Pay Month"></asp:Label>
                                                    <asp:DropDownList ID="ddlPayMonth" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPayMonth"
                                                        Display="None" ErrorMessage="Please Select Pay Month" ValidationGroup="Submit"
                                                        SetFocusOnError="True" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 form-group">
                                                    <span style="color: #FF0000; font-weight: bold">*</span>
                                                    <asp:Label ID="lblServerName" CssClass="control-label" runat="server" Text="Pay Date"></asp:Label>
                                                    <div class='input-group date' id="myDatepicker1">
                                                        <asp:TextBox ID="txtPayDate" runat="server" TabIndex="7" ToolTip="Please Enter Pay Date" CssClass="form-control datepickerinput" />
                                                        <div class="input-group-addon" id="imgCalDateOfBirth" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <%--<asp:Image ID="imgCalDateOfBirth" runat="server" src="../../images/calendar.png" Style="cursor: pointer"
                                                        TabIndex="8" Height="16px" />--%>
                                                        <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="txtPayDate"
                                                            Display="None" ErrorMessage="Please Enter Pay Date" SetFocusOnError="True"
                                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
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


                                          


                                            <div id="Td1" runat="server" visible="false">
                                                <br />
                                                <h5><b>Amount Description</b></h5>

                                                <div class="col-sm-4 form-group">
                                                    <label>CASH</label>
                                                    <asp:Label ID="lblCashAmount" runat="server" Text="--" Font-Bold="true">
                                                    </asp:Label>
                                                </div>


                                                <div class="col-sm-4 form-group">
                                                    <label>DEMAND DRAFT</label>
                                                    <asp:Label ID="lblDDAmount" runat="server" Text="--" Font-Bold="true">
                                                    </asp:Label>
                                                </div>

                                                <div style="display: none">
                                                    <div class="col-sm-4 form-group">
                                                        <label>CHEQUE</label>
                                                        <asp:Label ID="lblChequeAmount" runat="server" Text="--" Font-Bold="true">
                                                        </asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4 form-group">
                                                    <label>TOTAL AMOUNT</label>
                                                    <asp:Label ID="lblTotalAmount" runat="server" Text="--" Font-Bold="true">
                                                    </asp:Label>
                                                </div>


                                                
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
          
                                             <div class="text-center">
                                                <asp:Button ID="btnShow" runat="server" TabIndex="5" Text="Show" ToolTip="Click to Save" ValidationGroup="Submit" CssClass="btn btn-primary progress-button" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" OnClick="btnShow_Click" Font-Bold="true" />
                                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn btn-danger" TabIndex="7" Text="Cancel" ToolTip="Click to Cancel" OnClientClick="Cancel()" OnClick="btnCancel_Click" Font-Bold="true" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" Font-Bold="true" />
                                            </div>
              <br/>
            <%--<table cellpadding="0" cellspacing="0" border="0" width="100%" id="Table1" runat="server">--%>

            <div class="col-sm-12">
            <asp:Panel ID="DivReceipt" runat="server" Visible="false">
               <%-- <table cellpadding="1" cellspacing="1" width="100%">--%>
                     <asp:GridView ID="GridData" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE"
                                BorderStyle="None" BorderWidth="1px" AllowSorting="True">
                                <RowStyle BackColor="#F7F7DE" />
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
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        <br/>
                        <div class="text-center">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary progress-button" Text="Save" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" OnClick="btnSubmit_Click" Font-Bold="true" Height="25px" Width="100px" />
                            <%--<asp:Button ID="btnTransfer" runat="server" CssClass="btn btn-danger" Text="Transfer" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" OnClick="btnTransfer_Click" Font-Bold="true" Height="25px" Width="100px" />--%>
                            <asp:Button ID="btnTransfer" runat="server" CssClass="btn btn-danger" Text="Transfer" UseSubmitBehavior="false" OnClick="btnTransfer_Click" Font-Bold="true" Height="25px" Width="100px" />

                        </div>
           
                 </asp:Panel>
            </div>
                                                              
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>


