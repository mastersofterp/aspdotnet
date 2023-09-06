<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DeleteTransferedFees.aspx.cs" Inherits="DeleteTransferedFees" Title="DeleteTransferedFees" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 220px;
        }
    </style>--%>
    <%--<script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>

    <script language="javascript" type="text/javascript">
    </script>

    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>--%>

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

        function checkAll(gvExample, colIndex) {

            var GridView = gvExample.parentNode.parentNode.parentNode;

            for (var i = 1; i < GridView.rows.length; i++) {

                var chb = GridView.rows[i].cells[colIndex].getElementsByTagName("input")[0];

                chb.checked = gvExample.checked;

            }

        }



        function checkItem_All(objRef, colIndex) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var selectAll = GridView.rows[0].cells[colIndex].getElementsByTagName("input")[0];

            if (!objRef.checked) {

                selectAll.checked = false;

            }

            else {

                var checked = true;

                for (var i = 1; i < GridView.rows.length; i++) {

                    var chb = GridView.rows[i].cells[colIndex].getElementsByTagName("input")[0];

                    if (!chb.checked) {

                        checked = false;

                        break;

                    }

                }

                selectAll.checked = checked;

            }

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
                            <h3 class="box-title">DELETE FEE TRANSFER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large">
                                </div>
                            </div>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12 mt-3">
                                    <%--  <div class="panel-heading">Delete Fees transfer</div>--%>
                                    <div class="row">
                                        <div class="form-group col-lg-2 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:RadioButton ID="rdoGenralFees" runat="server" Text="General Fees" GroupName="FeeType"
                                                AutoPostBack="true"
                                                OnCheckedChanged="rdoGenralFees_CheckedChanged"
                                                Checked="true" />
                                            <asp:RadioButton ID="rdoMiscFees" runat="server" Text="Miscellaneous Fees" GroupName="FeeType" Visible="false"
                                                AutoPostBack="true" OnCheckedChanged="rdoMiscFees_CheckedChanged" />

                                        </div>
                                        <div class="col-lg-5 col-md-6 col-12" id="row18" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>From Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="imgCal">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" />
                                                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFromDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFromDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>To Date </label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="Image1">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control" />
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
                                        </div>
                                        <!-------->
                                        <div class="col-lg-5 col-md-6 col-12" id="row4" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Degree</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" AppendDataBoundItems="true"
                                                        OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                         <asp:ListItem Value="0" Text="Please Select" Selected="True">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Receipt Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRecept" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" AppendDataBoundItems="true"
                                                        OnSelectedIndexChanged="ddlRecept_SelectedIndexChanged">
                                                         <asp:ListItem Value="0" Text="Please Select" Selected="True">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer" id="Tr1" runat="server">
                                    <asp:HiddenField ID="hdnconfirmValue" runat="server" />
                                    <asp:Button ID="btnShowData" runat="server" Text="Show"
                                        OnClick="btnShowData_Click" CssClass="btn btn-primary" />
                                </div>

                                <div class="" id="rowgrid" runat="server">
                                    <div class="col-12">
                                        <input id="hdnAgParty" runat="server" type="hidden" />
                                        <input id="hdnOparty" runat="server" type="hidden" />

                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="ScrollPanel" runat="server" Visible="false">
                                            <div class="table table-responsive">
                                                <asp:GridView ID="GridData" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                    AutoGenerateColumns="False" CssClass="table table-striped table-bordered nowrap"
                                                    BorderStyle="None" BorderWidth="1px" OnRowCreated="GridData_RowCreated">
                                                    <RowStyle BackColor="#F7F7DE" />
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Fee Heads No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFeeHeadsNo" runat="server" Text='<%#Bind("FEE_HEAD_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Fee Heads Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFeeHeadsName" runat="server" Text='<%#Bind("FEE_HEAD_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Fee Heads">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFeeHeads" runat="server" Text='<%#Bind("RECIEPT_TYPE") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Fee Heads No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFeeHeadsNo" runat="server" Text='<%#Bind("FEE_HEAD_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Ledger Head Name">
                                                            <%--<ItemTemplate>
                                                    <asp:Label ID="lblFeeHeadsNo" runat="server" ></asp:Label>
                                                    </ItemTemplate>--%>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLedgerheadName" runat="server" Text='<%#Bind("PARTY_NAME")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cash Amount">
                                                            <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCashAmt" runat="server" Text="0.0" Style="text-align: right"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bank Amount">
                                                            <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBankAmt" runat="server" Text="0.0" Style="text-align: right"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Fee Heads">
                                                            <ItemTemplate>
                                                            <asp:TextBox ID="txttop" Width="60px" style="text-align:right" runat="server" Text='<%# Eval("Top") %>'></asp:TextBox>
                                                             </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Left">
                                                            <ItemTemplate>
                                                            <asp:TextBox ID="txtleft" Width="60px" style="text-align:right" runat="server" Text='<%# Eval("Left") %>' ></asp:TextBox>
                                                             </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Widht">
                                                            <ItemTemplate>
                                                            <asp:TextBox ID="txtwidth" Width="60px" style="text-align:right" runat="server" Text='<%# Eval("Width") %>'>' ></asp:TextBox>
                                                             </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Ledger_No." Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLedger_No" runat="server" Text='<%#Bind("LEDGERNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>

                                                            <HeaderTemplate>

                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this,6);" />

                                                            </HeaderTemplate>

                                                            <ItemTemplate>

                                                                <asp:CheckBox ID="chkitem" runat="server" onclick="checkItem_All(this,6)" />

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
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="" id="Tr2" runat="server">
                                    <div class="col-12">
                                        <input id="Hidden1" runat="server" type="hidden" />
                                        <input id="Hidden2" runat="server" type="hidden" />
                                    </div>
                                    <div class="col-12 ">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Total Amount</label>
                                                </div>
                                                <asp:Label ID="lblCashTotal" runat="server"
                                                    ForeColor="Red" Font-Bold="true" Font-Size="Larger"></asp:Label>
                                                <asp:Label ID="lblBankTotal" runat="server" ForeColor="#FF3300" Visible="false"></asp:Label>

                                            </div>
                                            <%-- <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>:</b>
                                                        <a class="sub-label">
                                                            </a>
                                                    </li>
                                                </ul>
                                            </div>--%>
                                        </div>
                                    </div>

                                </div>
                                <div class="mt-3" id="Row20" runat="server">
                                    <div class="col-12">
                                        <input id="hdnAskSave" runat="server" type="hidden" />
                                        <input id="hdnVchId" runat="server" type="hidden" />
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnDelete" runat="server" Enabled="false" OnClick="btnDelete_Click"
                                            Text="Delete" ValidationGroup="Validation"
                                            CssClass="btn btn-warning" />
                                        <asp:Button ID="btnReset" runat="server" CausesValidation="False"
                                            OnClick="btnReset_Click" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <input id="hdnbal2" runat="server" type="hidden" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>
