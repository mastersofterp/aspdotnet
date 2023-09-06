<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FeeAccountTransfer.aspx.cs" Inherits="FeeAccountTransfer" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 220px;
        }
    </style>

    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>

    <script language="javascript" type="text/javascript">
    </script>

    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>

    <script language="javascript" type="text/javascript">
        function ShowConfirm() {
            var chk = confirm("Record Allread Present Are U sure want to Replace records?");
            if (chk == true) {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnconfirmValue').value = chk;
                return true;
            }

            return false;

        }


        function NotShowConfirm() {
            return true;


        }

        function AskSave() {
            if (confirm('Some Fee Head Is Not Mapped And Having Amount Do You Want To Procceed ? ') == true) {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = 1;
                return true;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = 0;
                return false;
            }
        }
    </script>

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

        function askConfirm() {
            if (confirm('Some Fee Heads Having Fees in it is Not Mapped and will Not get Transfered?')) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <style type="text/css">
        .highlightRow {
            background-color: Red;
            color: #696969;
        }
    </style>
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
                        <div id="div1" runat="server">
                            <div id="div2" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">FEES ACCOUNT TRANSFER</h3>
                            </div>
                            <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                            <div class="box-body">
                                <asp:Panel ID="pnl" runat="server">
                                    <%-- <div class="col-md-8">--%>
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Fee Account transfer</div>
                                        <div class="panel-body">
                                            <div class="col-md-12">
                                                Note<span style="font-size: small">:</span><span style="font-weight: bold; font-size: x-small; color: red">* Marked is mandatory !</span><br />
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-3">
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:RadioButton ID="rdoGenralFees" runat="server" Text="General Fees" Visible="false" GroupName="FeeType"
                                                            AutoPostBack="true" OnCheckedChanged="rdoGenralFees_CheckedChanged"/>
                                                        <asp:RadioButton ID="rdoMiscFees" runat="server" Text="Miscellaneous Fees" GroupName="FeeType"
                                                            AutoPostBack="true" OnCheckedChanged="rdoMiscFees_CheckedChanged" Checked="true"/>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label>From Date<span style="color: red">*</span> : </label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtFromDate" Style="text-align: right" runat="server" CssClass="form-control" />
                                                            <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                                Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFromDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFromDate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label>To Date<span style="color: red">*</span> : </label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtTodate" Style="text-align: right" runat="server" CssClass="form-control" />
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" PopupPosition="BottomLeft"
                                                                TargetControlID="txtTodate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtTodate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                        </div>
                                                    </div>
                                                </div>


                                                <br />
                                               
                                                  <div class="row">
                                                    <div class="col-md-3">
                                                        <label>Pay Type<span style="color: red"></span> : </label>
                                                    </div>
                                                       <div class="col-md-3">
                                                        <asp:RadioButton ID="rdbReceipt" runat="server" Text="Receipt "  GroupName="PayType" />
                                                      
                                                        <asp:RadioButton ID="rdbPayment" runat="server" Text="Payment " GroupName="PayType" />
                                                     
                                                       </div>
                                                      </div>

                                                <br />
                                                <div id="row18" class="row" runat="server" visible="false">
                                                    <div class="col-md-3">
                                                        <label>Degree : </label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <br />
                                                <div id="Div3" class="row" runat="server" visible="false">
                                                    <div class="col-md-3" id="LABLEDEPT" runat="server" visible="false" >
                                                        <label >Department : </label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" AutoPostBack="True" AppendDataBoundItems="true"
                                                            OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" Visible="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <br />

                                                <div id="row4" class="row" runat="server" visible="false">
                                                    <div class="col-md-3">
                                                        <label>Receipt Type : </label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlRecept" runat="server" CssClass="form-control" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlRecept_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <br />
                                                <div id="TrPaytype" runat="server" visible="false" class="row">
                                                    <div class="col-md-3">
                                                        <label>Group Payment Type : </label>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlGrpPayment" runat="server" CssClass="form-control" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-3">
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:HiddenField ID="hdnconfirmValue" runat="server" />
                                                        <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                                        <asp:Button ID="btnShowData" runat="server" Text="Show" OnClick="btnShowData_Click" CssClass="btn btn-primary" />
                                                    </div>
                                                </div>
                                                <div id="rowgrid" runat="server">
                                                    <div class="col-md-3">
                                                    </div>
                                                    <div class="col-md-5">
                                                        <input id="hdnAgParty" runat="server" type="hidden" />
                                                        <input id="hdnOparty" runat="server" type="hidden" />
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-3">
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:Panel ID="ScrollPanel" Height="300px" runat="server" ScrollBars="Both">
                                                            <asp:GridView ID="GridData" runat="server" GridLines="Vertical"
                                                                AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                                                BorderStyle="None" BorderWidth="1px" OnRowCreated="GridData_RowCreated">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Fee Heads" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFeeHeadsNo" runat="server" Text='<%#Bind("FEE_HEAD_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fee Heads Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFeeHeadsName" runat="server" Text='<%#Bind("FEE_HEAD_Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fee Heads" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFeeHeadsStatus" runat="server" Text='<%#Bind("FeeHeadsStatus") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ledger Head Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLedgerheadName" runat="server" Text='<%#Bind("PARTY_NAME")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cash Amount" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCashAmt" runat="server" Text="0.0" Style="text-align: right"></asp:Label>
                                                                               <asp:HiddenField ID="hdnMISCHEADSRNO" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bank Amount" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBankAmt" runat="server" Text="0.0" Style="text-align: right"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCC99" />
                                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#3C8DBC" Font-Bold="True" ForeColor="White" />
                                                                <AlternatingRowStyle BackColor="White" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                                <br />

                                                <div id="Div6" class="row" runat="server">
                                                    <div class="col-md-3">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Total Bank Amount </label>
                                                        <br />
                                                        <asp:Label ID="lblBankTotal" runat="server" ForeColor="#FF3300"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Total Cash Amount </label>
                                                        <br />
                                                        <asp:Label ID="lblCashTotal" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                 
                                                </div>
                                                <br />
                                                <div class="form-group col-md-12">
                                                    <div class="col-md-3">
                                                    </div>
                                                    <div class="col-md-9">
                                                        <input id="hdnAskSave" runat="server" type="hidden" />
                                                        <input id="hdnVchId" runat="server" type="hidden" />
                                                        <input id="hdnbal2" runat="server" type="hidden" />
                                                        <asp:Button ID="btnTrans" runat="server" Enabled="false" OnClick="btnTrans_Click"
                                                            CssClass="btn btn-primary" Text="Bank Transfer" ValidationGroup="Validation" />
                                                        <asp:Button ID="btnCash" runat="server" Enabled="false" OnClick="btnCash_Click" CssClass="btn btn-primary"
                                                            Text="Cash Transfer" ValidationGroup="Validation" />
                                                        <asp:Button ID="btnReset" runat="server" CausesValidation="False" OnClick="btnReset_Click"
                                                            Text="Cancel" CssClass="btn btn-warning" />
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type='text/javascript' language='javascript'>
        function askConfirm() {
            if (confirm('Some Fee Heads Having Fees in it is Not Mapped and will Not get Transfered?')) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
