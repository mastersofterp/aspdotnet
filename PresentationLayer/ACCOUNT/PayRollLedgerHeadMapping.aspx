<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PayRollLedgerHeadMapping.aspx.cs" Inherits="PayRollLedgerHeadMapping"
    Title="" %>

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

        //Addede by vijay andoju for enble and disabling tha gridview controls
        function Calculation(type) {
            alert('test');
            var grid = document.getElementById("<%= GridData.ClientID%>");

            for (var i = 0; i < grid.rows.length - 1; i++) {
                if (type == "0") {
                    alert('1');
                    var txtAmountReceive = $("input[id*=ddlleagerHead]")
                }
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
                        <div id="div4" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PAY ROLL LEDGERHEAD MAPPING</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" align="center" style="font-size: x-large; text-align: center" runat="server">
                                </div>
                            </div>
                            <asp:Panel ID="pnlShow" runat="server">
                                <div class="col-12 mt-3">
                                    <%--<div class="sub-heading">Pay Roll Ledgerhead Mapping</div>--%>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-12" id="Tr4" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>College Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Please Select College" runat="server" ControlToValidate="ddlCollege" InitialValue="0" ValidationGroup="valdept" Display="None"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Staff</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Please Select Staff" runat="server" ControlToValidate="ddlDegree" InitialValue="0" ValidationGroup="valdept" Display="None"></asp:RequiredFieldValidator>

                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-12" id="Div3" runat="server">
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
                                                <div class="form-group col-lg-6 col-md-6 col-12" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Bank</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlPARTY" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPARTY_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer" id="Tr1" runat="server">
                                    <asp:Button ID="btnShowData" runat="server" Text="Show " OnClick="btnShowData_Click" ValidationGroup="valdept"
                                        CssClass="btn btn-primary" />
                                    <asp:ValidationSummary ID="vmdept" runat="server" ValidationGroup="valdept" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                                <div class="" id="rowgrid" runat="server">
                                    <div class="col-12">
                                        <input id="hdnAgParty" runat="server" type="hidden" />
                                        <input id="hdnOparty" runat="server" type="hidden" />
                                    </div>
                                    <div class="col-12 mt-3">
                                        <asp:Panel ID="ScrollPanel" runat="server">
                                            <div class="table table-responsive">
                                                <asp:GridView ID="GridData" runat="server"
                                                    GridLines="Vertical" AutoGenerateColumns="False"
                                                    CssClass="table table-striped table-bordered nowrap"
                                                    BorderStyle="None" BorderWidth="1px" OnRowCreated="GridData_RowCreated">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkPayHeadNo" runat="server" AutoPostBack="true" OnCheckedChanged="chkPayHeadNo_CheckedChanged" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PayHead No" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayHeadsNo" runat="server" Text='<%#Bind("PAYHEAD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PayHead Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayHeads" runat="server" Text='<%#Bind("FULLNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ledger Head" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlleagerHead" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                                </asp:DropDownList>
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
                                <div id="Div2" class="col-12 btn-footer" runat="server" visible="false">

                                    <asp:Button ID="btnEmpShare" runat="server" OnClick="btnEmpShare_Click" CssClass="btn btn-primary" Text="Add To Employer Share" />
                                </div>

                                <div class="form-group row" id="Tr3" runat="server" visible="false">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Employer Share</h5>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="pnlEmpoyer" runat="server">
                                            <div class="table-table-responsive">
                                                <asp:GridView ID="gvEmployer" runat="server" CellPadding="4"
                                                    ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" CssClass="table table-striped table-bordered nowrap" Width="100%"
                                                    OnRowCommand="gvEmployer_RowCommand" OnRowDataBound="gvEmployer_RowDataBound"
                                                    OnRowCreated="gvEmployer_RowCreated">
                                                    <RowStyle BackColor="#F7F7DE" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-warning" Text="Remove" CommandName="Remove" CommandArgument='<%#Bind("PAYHEAD") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PayHead No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayHeadsNo" runat="server" Text='<%#Bind("PAYHEAD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PayHead Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayHeads" runat="server" Text='<%#Bind("FULLNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Credit Head">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlCr" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Debit Head">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlDr" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" />
                                                    <%--<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />--%>
                                                    <%--<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />--%>
                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="True" ForeColor="#000" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                            </div>
                                        </asp:Panel>
                                    </div>

                               
                                </div>
                                     <div id="divsave" runat="server" visible="false">
                                        <div class="col-12">
                                            <input id="hdnAskSave" runat="server" type="hidden" />
                                            <input id="hdnVchId" runat="server" type="hidden" />
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="Validation"
                                                OnClick="btnSave_Click" />
                                            <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Cancel" CssClass="btn btn-warning" />
                                        </div>
                                    </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <%--<Triggers>
                <asp:PostBackTrigger ControlID="btnEmpShare" />
                <asp:PostBackTrigger ControlID="gvEmployer" />
            </Triggers>--%>
    </asp:UpdatePanel>
    <input id="hdnbal2" runat="server" type="hidden" />
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
