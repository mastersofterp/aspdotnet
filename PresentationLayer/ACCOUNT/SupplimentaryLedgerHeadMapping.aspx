<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SupplimentaryLedgerHeadMapping.aspx.cs" Inherits="ACCOUNT_SupplimentaryLedgerHeadMapping" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>

    <script language="javascript" type="text/javascript">
    </script>

    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>

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
                            <h3 class="box-title">SUPPLIMENTARY LEDGER HEAD MAPPING</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div id="divCompName" align="center" style="font-size: x-large; text-align: center" runat="server">
                                </div>
                                <asp:Panel ID="pnlShow" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">SUPPLIMENTARY LEDGER HEAD MAPPING</div>
                                        <div class="panel-body">
                                            <div class="form-group row" id="Tr4" runat="server">
                                                <div class="col-md-2">
                                                    <label>College Name:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group row" id="row18" runat="server">
                                                <div class="col-md-2">
                                                    <label>Staff :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group row" id="Tr2" runat="server">
                                                <div class="col-md-2">
                                                    <label>Bank:</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlPARTY" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlPARTY_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group row" id="Tr1" runat="server">
                                                <div class="col-md-2"></div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnShowData" runat="server" Text="Show " OnClick="btnShowData_Click"
                                                        CssClass="btn btn-success" />
                                                </div>
                                            </div>
                                            <div class="form-group row" id="rowgrid" runat="server">
                                                <div class="col-md-2">
                                                    <input id="hdnAgParty" runat="server" type="hidden" />
                                                    <input id="hdnOparty" runat="server" type="hidden" />
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Panel ID="ScrollPanel" Height="500px" runat="server" ScrollBars="Both">
                                                        <asp:GridView ID="GridData" runat="server"
                                                            GridLines="Vertical" AutoGenerateColumns="False"
                                                            CssClass="table table-striped table-bordered table-hover"
                                                            BorderStyle="None" BorderWidth="1px" OnRowCreated="GridData_RowCreated">
                                                            <Columns>
                                                                <%--<asp:TemplateField HeaderText="" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkPayHeadNo" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Supplimentary Head No." HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPayHeadsNo" runat="server" Text='<%#Bind("PAYHEAD") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Supplimentary Head Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPayHeads" runat="server" Text='<%#Bind("FULLNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ledger Head" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlleagerHead" runat="server" AppendDataBoundItems="true">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCC99" />
                                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="True" ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div id="Div2" class="form-group row" runat="server" visible="false">
                                                <div class="col-md-2"></div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnEmpShare" runat="server" OnClick="btnEmpShare_Click" CssClass="btn btn-primary" Text="Add To Employer Share" />
                                                </div>
                                            </div>
                                            <div class="form-group row" id="Tr3" runat="server" visible="false">
                                                <div class="col-md-2">
                                                    <label>Employer Share :</label>
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Panel ID="pnlEmpoyer" Height="200px" runat="server" ScrollBars="Both">
                                                        <asp:GridView ID="gvEmployer" CssClass="datatable" runat="server" CellPadding="4"
                                                            ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" Width="100%"
                                                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                                            OnRowCommand="gvEmployer_RowCommand" OnRowDataBound="gvEmployer_RowDataBound"
                                                            OnRowCreated="gvEmployer_RowCreated">
                                                            <RowStyle BackColor="#F7F7DE" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Remove" CommandName="Remove" CommandArgument='<%#Bind("PAYHEAD") %>' />
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
                                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="True" ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div class="form-group row" id="Row20" runat="server">
                                                <div class="col-md-2">
                                                    <input id="hdnAskSave" runat="server" type="hidden" />
                                                    <input id="hdnVchId" runat="server" type="hidden" />
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="Validation"
                                                        OnClick="btnSave_Click" />
                                                    &nbsp;&nbsp;
                                            <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Cancel" CssClass="btn btn-warning" />
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
        <%--<Triggers>
                <asp:PostBackTrigger ControlID="btnEmpShare" />
                <asp:PostBackTrigger ControlID="gvEmployer" />
            </Triggers>--%>
    </asp:UpdatePanel>
    <input id="hdnbal2" runat="server" type="hidden" />
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
