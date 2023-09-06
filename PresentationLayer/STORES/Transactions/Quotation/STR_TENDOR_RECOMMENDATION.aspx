<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="STR_TENDOR_RECOMMENDATION.aspx.cs" Inherits="STORES_Transactions_Quotation_STR_TENDOR_RECOMMENDATION"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>

    <script language="javascript" type="text/javascript">
        function confirmDeleteResult(v, m, f) {
            if (v) //user clicked OK 
                $('#' + f.hidID).click();
        }

    </script>

    <%--<asp:UpdatePanel ID="updpnlMain" runat="server"   >
   <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">TENDER RECOMMENDATION FORM</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-4 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Tender List</label>
                                </div>
                                <asp:Panel ID="pnl" runat="server">
                                    <asp:ListBox ID="lstTender" runat="server" AutoPostBack="true" CssClass="form-control multi-select-demo"
                                        OnSelectedIndexChanged="lstTender_SelectedIndexChanged"></asp:ListBox>
                                </asp:Panel>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" id="divVendor" runat="server" visible="false">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Vendor List </label>
                                </div>
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:ListBox ID="lstVendor" runat="server" AutoPostBack="true" CssClass="form-control multi-select-demo"
                                        OnSelectedIndexChanged="lstVendor_SelectedIndexChanged"></asp:ListBox>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                   

                    <div class="col-12" id="divVendorList" runat="server" visible="false">
                        <div class="sub-heading">
                            <h5>Vendor List For Recommendation</h5>
                        </div>

                        <asp:Panel ID="Panel4" runat="server">
                            <div class="col-12 table-responsive">
                                <asp:GridView ID="grdvendorList" CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" runat="server"
                                    AutoGenerateColumns="false" DataKeyNames="TVNO" EmptyDataText="There is No Vendor For Recommandation"
                                    EmptyDataRowStyle-BackColor="WhiteSmoke">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <label>Select</label></HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdbItem" runat="server" GroupName="Item" ToolTip='<%#Eval("TVNO") %>' AutoPostBack="true" OnCheckedChanged="rdbItem_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Vendor Name" HeaderStyle-CssClass="bg-light-blue" DataField="VENDORNAME" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Vendor Address" HeaderStyle-CssClass="bg-light-blue" DataField="ADDRESS" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Contact Number" HeaderStyle-CssClass="bg-light-blue" DataField="VENDOR_CONTACT" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Email" HeaderStyle-CssClass="bg-light-blue" DataField="EMAIL" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>

                     <div class="col-12" id="divItemDetails" runat="server" visible="false">
                      <%--  <div class="sub-heading">
                            <h5>Vendor Specification</h5>
                        </div>--%>
                        <asp:Panel ID="Panel3" runat="server">
                            <asp:ListView ID="lvItemDetails" runat="server" OnItemDataBound="lvItemSpec_ItemDataBound">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Item Details</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
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
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlDetails" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" ToolTip="Click To Show Field List" /><%--~/images/action_down.gif--%>
                                            </asp:Panel>
                                            &nbsp;                                                                    
                                        <asp:Label ID="lblItemNo" runat="server" Visible="false" Text='<%# Eval("ITEM_NO") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <%# Eval("ITEM_NAME")%>
                                            <%-- <asp:HiddenField id="hdnTvno" runat="server" Value='<%# Eval("TVNO") %>'/>--%>
                                            <asp:HiddenField ID="hdnTino" runat="server" Value='<%# Eval("TINO") %>' />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hdnQty" runat="server" Value='<%# Eval("QTY") %>' />
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
                                                <table class="table table-bordered table-hover">
                                                    <tr>
                                                        <td>Field List
                                                        <asp:ListView ID="lvDetails" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="demo-grid">
                                                                    <table class="table table-bordered table-hover" style="width: 100%">
                                                                        <tr class="bg-light-blue">
                                                                            <th style="width: 35%">Tax Name
                                                                            </th>
                                                                            <th style="width: 15%">Tax Type
                                                                            </th>
                                                                            <th style="width: 10%">%
                                                                            </th>
                                                                            <th style="width: 10%">Amount
                                                                            </th>
                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <EmptyDataTemplate>
                                                                <div style="text-align: center; font-family: Arial; font-size: medium">
                                                                    No Record Found
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="width: 35%">
                                                                        <%# Eval("TAX_NAME")%>
                                                                    </td>
                                                                    <td style="width: 15%">
                                                                        <%# Eval("IS_PER") %>
                                                                    </td>
                                                                    <td style="width: 10%">
                                                                        <%# Eval("TAX_PER") %>
                                                                    </td>
                                                                    <td style="width: 10%">
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

                            <div class="col-12 mt-3 mb-3">
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
                                                    <asp:Label runat="server" ID="lblNetAmt"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                        </asp:Panel>
                    </div>

                    <div class="col-12" id="divSelItemList" runat="server" visible="false">
                        <div class="sub-heading">
                            <h5>Already Recommended Vendor</h5>
                        </div>
                        <asp:Panel ID="Panel5" runat="server">
                            <div class="col-12 table-responsive">
                                <asp:GridView ID="grdSelItemList" CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" runat="server" AutoGenerateColumns="false"
                                    EmptyDataText="There Is No Vendor Recommanded" EmptyDataRowStyle-BackColor="WhiteSmoke"
                                    DataKeyNames="TVNO">
                                    <%--OnRowDataBound="grdSelItemList_RowDataBound" OnRowDeleting="grdSelItemList_RowDeleting"--%>
                                    <EmptyDataRowStyle BackColor="WhiteSmoke" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <label>Action</label></HeaderTemplate>
                                            <ItemTemplate>
                                                <!-- The link which will display the delete confirmation -->
                                                <%-- <a id="linkDelete" runat="server" title="Confirm delete">
                                                    <img id="Img1" src="../../../images/delete.png " runat="server" alt="Delete" /></a>--%>
                                                <!-- Hidden command button that actually issues the delete -->

                                                <asp:ImageButton runat="server" ID="btnDelete" OnClientClick="javascript:return confirm('Are You Sure You Want To Delete This Record?')" CommandArgument='<%#Eval("TENDERNO") %>' ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Vendor Name" HeaderStyle-CssClass="bg-light-blue" DataField="VENDORNAME" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Tender Number" HeaderStyle-CssClass="bg-light-blue" DataField="TENDERNO" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button runat="server" ID="btnSave" Text="Save Recommendation" OnClientClick="return Validate(this)"
                            OnClick="btnSave_Click" CssClass="btn btn-primary" TabIndex="4" ToolTip="Click To Save Recommendation" />
                        <asp:Button ID="btnRept1" runat="server" Text="Report" Visible="false"
                            OnClick="btnRept1_Click" CssClass="btn btn-info" TabIndex="5" ToolTip="Click To Report" />
                    </div>
                </div>
            </div>
        </div>
    </div>


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
            if (document.getElementById('<%=lstTender.ClientID%>').value == '') {
                alert('Please Select Tender From Tender List');
                return false;
            }
            // else if (document.getElementById('<%=lstVendor.ClientID%>').value == '') {
            //    alert('Please Select Vendor From Vendor List');
            //    return false;
            //}
            var checkCount = 0;
            var count = Number($("#<%=grdvendorList.ClientID %> tr").length);
            debugger;
            if (count > 1) {
                for (var i = 2; i < count + 1; i++) {
                    var chk = document.getElementById('ctl00_ContentPlaceHolder1_grdvendorList_ctl0' + i + '_rdbItem');
                    if (chk.checked)
                        checkCount++;
                }
            }
            if (checkCount == 0) {
                alert('Please Select At Least One Vendor.');
                return false;
            }
            else {
                return confirm('Are You Sure You Want To Save This Recommendation?');
            }

        }

    </script>

</asp:Content>
