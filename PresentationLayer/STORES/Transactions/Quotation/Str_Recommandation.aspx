<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_Recommandation.aspx.cs" Inherits="Stores_Transactions_Str_Recommandation"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../../Scripts/jquery.js" type="text/javascript"></script>

    <script src="../../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

    <link href="../../Scripts/impromptu.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>

    <script language="javascript" type="text/javascript">
        function confirmDeleteResult(v, m, f) {
            if (v) //user clicked OK 
                $('#' + f.hidID).click();
        }


        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.name.endsWith('chkItem')) {
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true)
                            e.checked = true;
                        else
                            e.checked = false;
                    }
                }
            }
        }


    </script>

    <%-- <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">RECOMMENDATION FORM</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Quotation List</label>
                                </div>
                                <asp:DropDownList ID="lstQtNo" runat="server" AutoPostBack="true" CssClass="form-control" AppendDataBoundItems="true" 
                                    OnSelectedIndexChanged="lstQtNo_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                </asp:DropDownList>
                                <%--  <asp:RequiredFieldValidator ID="rfvQtno" runat="server" ControlToValidate="lstQtNo" ValidationGroup="Submit" 
                                    Display="None" InitialValue="" ErrorMessage="Please Select Quotation."></asp:RequiredFieldValidator>--%>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Vendor List</label>
                                </div>
                                <asp:DropDownList ID="lstVendor" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" 
                                    OnSelectedIndexChanged="lstVendor_SelectedIndexChanged">
                                     <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Vendor Specification</h5>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="Panel2" runat="server">
                            <div class="col-12">
                                <asp:ListView ID="lvItemDetails" runat="server" OnItemDataBound="lvItemSpec_ItemDataBound">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Item Details</h5>
                                        </div>
                                        <div class="table-responsive">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Item Name
                                                        </th>
                                                        <th>Quantity
                                                        </th>
                                                        <th>Rate
                                                        </th>
                                                        <th>Discount Amt
                                                        </th>
                                                        <th>Bill Amount
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlDetails" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/expand_blue.jpg" ToolTip="Click To Show Field List" /><%--~/images/action_down.gif--%>
                                                </asp:Panel>
                                                <asp:Label ID="lblItemNo" runat="server" Visible="false" Text='<%# Eval("ITEM_NO") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <%# Eval("ITEM_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("QTY") %>
                                            </td>
                                            <td>
                                                <%# Eval("PRICE") %>
                                            </td>
                                            <td>
                                                <%# Eval("DISCOUNT_AMOUNT")%>
                                            </td>
                                            <td>
                                                <%# Eval("TOTAMOUNT") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:Panel ID="pnlShowCDetails" runat="server" CssClass="collapsePanel" ScrollBars="Auto">
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                        <tr>
                                                            <td>Field List
                                                                <asp:ListView ID="lvDetails" runat="server">
                                                                    <LayoutTemplate>
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Tax Name
                                                                                    </th>
                                                                                    <th>Tax Type
                                                                                    </th>
                                                                                    <th>%
                                                                                    </th>
                                                                                    <th>Amount
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; font-family: Arial; font-size: medium">
                                                                            No Record Found
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("TAX_NAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("IS_PER") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("TAX_PER") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("TAX_AMOUNT")%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <ajaxToolKit:CollapsiblePanelExtender ID="cpeCourt2" runat="server" TargetControlID="pnlShowCDetails"
                                            ExpandControlID="pnlDetails" CollapseControlID="pnlDetails" CollapsedImage="~/images/action_down.gif"
                                            ExpandedImage="~/images/action_up.gif" ImageControlID="imgExp" Collapsed="true">
                                        </ajaxToolKit:CollapsiblePanelExtender>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Total Tax Amount :</b>
                                                <a class="sub-label">
                                                    <asp:Label runat="server" ID="lblTaxAmt"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Total Gross Amount :</b>
                                                <a class="sub-label">
                                                    <asp:Label runat="server" ID="lblGrossAmount"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Total Bill Amount :</b>
                                                <a class="sub-label">
                                                    <asp:Label runat="server" ID="lblNetAmt"></asp:Label></span> </a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                    </div>

                    <div class="col-12 mt-3">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Select Item For Recommend To Selected Vendor</h5>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="Panel3" runat="server">
                            <div class="col-12 table-responsive">

                                <asp:GridView ID="grdItemsList" CssClass="table table-bordered table-hover" runat="server" HeaderStyle-CssClass="bg-light-blue"
                                    AutoGenerateColumns="false" DataKeyNames="QINO" EmptyDataText="There is No Items For Recommendation"
                                    EmptyDataRowStyle-BackColor="WhiteSmoke">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="bg-light-blue">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeadItem" runat="server" onclick="totAllSubjects(this)" />
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:CheckBox ID="chkItem" runat="server" ToolTip='<%#Eval("ITEM_NO") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Item Name" DataField="ITEM_NAME" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="bg-light-blue" />
                                        <asp:BoundField HeaderText="Req. Quantity" DataField="QTY" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="bg-light-blue" />
                                        <asp:TemplateField HeaderText="Final Req. Quantity" HeaderStyle-CssClass="bg-light-blue">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQty" runat="server" Text='<%#Eval("QTY") %>' CssClass="form-control" MaxLength="6" Enabled="true" />
                                                <asp:HiddenField ID="hdnQty" runat="server" Value='<%#Eval("QTY") %>' />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeQty" runat="server"
                                                    FilterType="Numbers" TargetControlID="txtQty" ValidChars="">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlRemark" runat="server">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Justification</label>
                                    </div>
                                    <asp:TextBox ID="txtJustification" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Items Already Selected For Selected Vendor</h5>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="Panel4" runat="server">
                            <div class="col-12 table-responsive">
                                <asp:GridView ID="grdSelItemList" CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" runat="server" AutoGenerateColumns="false"
                                    EmptyDataText="There is No Items Recommended" EmptyDataRowStyle-BackColor="WhiteSmoke"
                                    DataKeyNames="QINO" OnRowDeleting="grdSelItemList_RowDeleting">
                                    <EmptyDataRowStyle BackColor="WhiteSmoke" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="bg-light-blue">
                                            <ItemTemplate>
                                                <!-- The link which will display the delete confirmation -->
                                                <%--  <a id="linkDelete" runat="server" title="Confirm delete">
                                                    <img id="Img1" src="../../../images/delete.gif " runat="server" alt="Delete" /></a>--%>
                                                <!-- Hidden command button that actually issues the delete -->
                                                <%-- <asp:Button runat="server" CommandName="Delete" ID="Button1" Style="display: none;" />--%>
                                                <asp:ImageButton runat="server" ID="btnDelete" OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" CommandArgument='<%#Eval("QINO") %>' ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Item Name" DataField="ITEM_NAME" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="bg-light-blue" />
                                        <asp:BoundField HeaderText="Req. Quantity" DataField="RECOMMAND_ITEM_QTY" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="bg-light-blue" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>

                        <div class="col-12 btn-footer">
                            <asp:Button runat="server" ID="btnSave" Text="Save Recommendation" OnClientClick="return Validate(this)"
                                OnClick="btnSave_Click" CssClass="btn btn-primary" ToolTip="Click To Save" />
                            <asp:Button runat="server" ID="btnRpt" CssClass="btn btn-warning" Text="Report" Visible="false" ToolTip="Click To Cancel" OnClick="btnRpt_Click" />
                            <%--  <asp:ValidationSummary  ID="valsum" runat="server" ValidationGroup="Submit"  ShowMessageBox="true" ShowSummary="false"/>--%>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager;
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

        function Validate(crl) {
            if (document.getElementById('<%=lstQtNo.ClientID%>').value == '') {
                alert('Please Select Quotation From Quotation List');
                return false;
            }
            else if (document.getElementById('<%=lstVendor.ClientID%>').value == '') {
                alert('Please Select Vendor From Vendor List');
                return false;
            }
            var checkCount = 0;
            var count = Number($("#<%=grdItemsList.ClientID %> tr").length);
            debugger;
            if (count > 1) {
                for (var i = 2; i < count + 1; i++) {                   
                    var chk = document.getElementById('ctl00_ContentPlaceHolder1_grdItemsList_ctl0' + i + '_chkItem');
                    if (chk.checked)
                        checkCount++;
                }
            }           
            if (checkCount == 0) {
                alert('Please Select At Least One Item.');
                return false;
            }
            else {
                return confirm('Are You Sure You Want To Save This Vendor Recommendation?');
            }

        }



    </script>

</asp:Content>
