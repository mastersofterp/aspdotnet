<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PayRollDeleteTransfer.aspx.cs" Inherits="PayRollDeleteTransfer" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: center;
        }
    </style>

    <%--   <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>

    <script language="javascript" type="text/javascript">
    </script>

    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>--%>

    <script language="javascript" type="text/javascript">

        function CheckNumeric(obj) {


            var k = (window.event) ? event.keyCode : event.which;

            // alert(k);

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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">PAY ROLL LEDGER TRANSFER</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div id="divCompName" runat="server" style="font-size: x-large; text-align: center">
                        </div>
                    </div>
                    <asp:Panel ID="pnl" runat="server">
                        <%--   <div class="sub-heading">Pay Roll Ledger Transfer</div>--%>

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
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>College Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Staff</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AppendDataBoundItems="true">
                                             <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                 </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                 </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddldept" ErrorMessage="Please Select Department" runat="server" ControlToValidate="ddldept" InitialValue="0" ValidationGroup="valdept" Display="None"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Month</label>
                                            </div>
                                            <asp:DropDownList ID="ddlMon" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlPARTY_SelectedIndexChanged" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                      </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Bank</label>
                                            </div>
                                            <asp:Label ID="lblBank" runat="server"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                                <div class="" id="Tr1" runat="server">
                                    <div class="col-12">
                                        <asp:HiddenField ID="hidfBankNo" runat="server" />
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShowData" runat="server" Text="Show" OnClick="btnShowData_Click" ValidationGroup="valdept"
                                            CssClass="btn btn-primary" />
                                        <asp:Button ID="BtnShowEmp" runat="server" CssClass="btn btn-primary" Text="Show By Employee" OnClick="BtnShowEmp_Click"
                                            Visible="False" />

                                        <asp:ValidationSummary ID="vmdept" runat="server" ValidationGroup="valdept" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                </div>

                                <div class="" id="rowgrid" runat="server">
                                    <div class="col-12">
                                        <input id="hdnAgParty" runat="server" type="hidden" />
                                        <input id="hdnOparty" runat="server" type="hidden" />
                                    </div>
                                    <div class="col-12 mt-3">
                                        <div class="table table-responsive">
                                            <asp:GridView ID="GridData" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                AutoGenerateColumns="False" CssClass="table table-striped table-bordered nowrap"
                                                BorderStyle="None" BorderWidth="1px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%#Bind("TRANSACTION_DATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Particulars" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblParticulars" runat="server" Text='<%#Bind("PARTY_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Voucher No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVchNo" runat="server" Text='<%#Bind("VOUCHER_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Debit " HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDebit" Text='<%#Bind("Debit") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Credit " HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCredit" Text='<%#Bind("Credit") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" />
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="True" ForeColor="#000" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="" id="Tr4" runat="server">
                                    <div class="col-12">
                                        <input id="Hidden1" runat="server" type="hidden" />
                                        <input id="Hidden2" runat="server" type="hidden" />
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnDelete" runat="server" Enabled="false" OnClick="btnDelete_Click"
                                            Text="Delete" ValidationGroup="Validation" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                                <div class="form-group row" id="Row20" runat="server">
                                    <input id="hdnAskSave" runat="server" type="hidden" />
                                    <input id="hdnVchId" runat="server" type="hidden" />
                                </div>
                                <div class="form-group row" id="Tr5" runat="server">
                                </div>
                                <div class="form-group row" id="Tr6" runat="server">
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <input id="hdnbal2" runat="server" type="hidden" />
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
