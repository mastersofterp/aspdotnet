<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BankEntry.aspx.cs" Inherits="BankEntry" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 250px;
        }
    </style>
    <script src="../JAVASCRIPTS/overlib.js"></script>--%>
    <script language="javascript" type="text/javascript">
    </script>
    <%--   <script src="../JAVASCRIPTS/IITMSTextBox.js"></script>--%>

    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
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
                            <h3 class="box-title">BANK ENTRY</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" runat="server" style="font-size: x-large; text-align: center">
                                </div>
                            </div>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <%--<div class="sub-heading">
                                        <h5>Add/Modify Bank Entry</h5>
                                    </div>--%>
                                    <div class="row">
                                        <div id="row18" class="col-lg-4 col-md-6 col-12" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-10 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Bank Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtBank" runat="server" CssClass="form-control" ToolTip="Please Enter Bank Name" AutoPostBack="True"
                                                        Width="100%"></asp:TextBox>
                                                    <ajaxtoolkit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtBank"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetBankName" OnClientShowing="clientShowing">
                                                    </ajaxtoolkit:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        ControlToValidate="txtBank" Display="None"
                                                        ErrorMessage="Please Enter Bank Name" SetFocusOnError="true"
                                                        ValidationGroup="AccMoney">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-2 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:Button ID="btnSave0" runat="server"
                                                        Style="margin-left: 0px" Text="Go" ValidationGroup="Validation"
                                                        CssClass="btn btn-primary" OnClick="btnSave0_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="row4" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Cheque Height</label>
                                            </div>
                                            <asp:TextBox ID="txtChqHeight" runat="server" ToolTip="Please Enter Cheque Height"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="txtChqHeight" Display="None"
                                                ErrorMessage="Please Enter Cheque Height" SetFocusOnError="true"
                                                ValidationGroup="AccMoney">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Tr1" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Cheque Width</label>
                                            </div>
                                            <asp:TextBox ID="txtChqWidth" runat="server" ToolTip="Please Enter Cheque Width"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="txtChqWidth" Display="None"
                                                ErrorMessage="Please Enter Cheque Width" SetFocusOnError="true"
                                                ValidationGroup="AccMoney">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-4 col-md-12 col-12">
                                            <div class=" note-div">
                                                <h5 class="heading">Note </h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>That All The Dimension Are In Inches ! </span></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-12" id="rowgrid" runat="server">
                                    <div class="">
                                        <input id="hdnAgParty" runat="server" type="hidden" />
                                        <input id="hdnOparty" runat="server" type="hidden" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="ScrollPanel" runat="server">
                                            <div class="table table-responsive">
                                                <asp:GridView ID="GridData" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                    AutoGenerateColumns="False" Width="100%" CssClass="table table-striped table-bordered nowrap" >
                                                    <Columns>
                                                        <asp:BoundField DataField="Particulars" HeaderText="Particulars">
                                                            <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                                            <ItemStyle Wrap="False" Width="50%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Top" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txttop" Style="text-align: right" runat="server" Text='<%# Eval("Top") %>' CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Left" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtleft" Style="text-align: right" runat="server" Text='<%# Eval("Left") %>' CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Width" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtwidth" CssClass="form-control" Style="text-align: right" runat="server" Text='<%# Eval("Width") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="#000" />
                                                    <HeaderStyle  Font-Bold="True" ForeColor="#000" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer" id="Row20" runat="server">
                                    <div>
                                        <input id="hdnAskSave" runat="server" type="hidden" />
                                        <input id="hdnVchId" runat="server" type="hidden" />
                                    </div>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"
                                        ValidationGroup="Validation" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnReset" runat="server"
                                        Text="Cancel" CssClass="btn btn-warning" OnClick="btnReset_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="AccMoney" />
                                </div>

                            </asp:Panel>
                            <input id="hdnbal2" runat="server" type="hidden" />
                            <div id="divMsg" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
