<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_Quotation_Entry_Form.aspx.cs" Inherits="Stores_Transactions_Quotation_Str_Quotation_Entry_Form" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
    <%-- <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <%--<script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            DataTable();
        }
        function DataTable() {
            $(document).ready(function () {
                $(".DataGrid").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers",
                    "bSort": false
                });
            });

            $(document).ready(function () {
                $("th.icon span").remove();
                $("th.icon").css({ cursor: 'arrow' });
                $("th.icon").unbind('click');
            });
        }
    </script>--%>
    <%--    <link href="<%#Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%#Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></scrip--%>
    <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>

    <script type="text/javascript">
        function SelectAllTrainer(headchk) {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chkSelect')) {
                        if (headchk.checked == true) {
                            e.checked = true;
                            hdfTot.value = Number(hdfTot.value) + 1;
                        }
                        else

                            e.checked = false;
                    }
                }
            }
            if (headchk.checked == false)
                hdfTot.value = "0";
        }

        function validateAssign() {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>').value;

            if (hdfTot == 0) {
                alert('Please Select Atleast One Item From The List');
                return false;
            }
            else
                return true;
        }
    </script>
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

    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updatePanel1"
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

    <asp:UpdatePanel ID="updatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div7" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">QUOTATION ENTRY</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12" id="divIndentList" runat="server" visible="true">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Indent List For Quotation</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-12" id="divReqIndent">
                                        <asp:Panel ID="pnlReqIndent" runat="server">
                                            <div class="table-responsive">
                                                <asp:GridView CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" runat="server" ID="grdIndList" DataKeyNames="INDNO"
                                                    AutoGenerateColumns="False" EmptyDataText="There Is No Indent For Quotation">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Check Indent " HeaderStyle-CssClass="bg-light-blue">
                                                            <ItemTemplate>
                                                                <asp:RadioButton runat="server" AutoPostBack="true" OnCheckedChanged="ChkIndentDetails_CheckChanged"
                                                                    ID="CheckIndent" Text=' <%# Eval("INDNO")%> ' ToolTip='<%# Eval("INDNO")%>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Department" DataField="MDNAME" HeaderStyle-CssClass="bg-light-blue">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Date" HeaderStyle-CssClass="bg-light-blue" DataField="INDSLIP_DATE" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                    </Columns>

                                                    <EmptyDataRowStyle BackColor="WhiteSmoke" />
                                                    <HeaderStyle BackColor="#CCCCCC" />
                                                </asp:GridView>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="butIndent" CssClass="btn btn-primary" runat="server" Text="Show Items" OnClick="butIndent_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReqListNext" runat="server" CssClass="btn btn-primary" Text="Next" OnClick="btnReqListNext_Click" />
                            </div>

                            <div class="col-12" id="divItemDetails" runat="server" visible="false">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="row">
                                        <div class="col-12">
                                            <asp:Panel ID="pnlitems" runat="server">
                                                <asp:ListView ID="lvitems" runat="server" DataKeyNames="ITEM_NO">
                                                    <EmptyDataTemplate>
                                                        <center>
                                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found"></asp:Label>
                                                        </center>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Item Details</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th id="thSelect" runat="server" visible="false">
                                                                        <asp:CheckBox ID="chkSelectAll" runat="server" Checked="true" onclick="SelectAllTrainer(this)" />
                                                                        Select All
                                                                    </th>
                                                                    <th>Item Name
                                                                    </th>
                                                                    <th>Quantity
                                                                    </th>
                                                                    <th>Value
                                                                    </th>
                                                                    <th>Item Specification
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
                                                            <td id="tdSelect" runat="server" visible="false">
                                                                <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("ITEM_NO")%>' Checked="true" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblItem" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtQuantity" runat="server" Style="text-align: left;" Enabled="true"
                                                                    Text='<%# Eval("REQ_QTY")%>' CssClass="form-control" MaxLength="6"></asp:TextBox>   <%-- 21/11/2022--%>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeQty" runat="server"
                                                                    FilterType="Custom,Numbers" TargetControlID="txtQuantity" ValidChars=".">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblAmt" runat="server" Text=' <%# Eval("RATE")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblItemSpeci" runat="server" Text=' <%# Eval("ITEM_SPECIFICATION")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:HiddenField ID="hdfTot" runat="server" />
                                        <asp:Button ID="btnDivTwoBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnDivTwoBack_Click" />
                                        <asp:Button ID="btnDivTwoNext" runat="server" CssClass="btn btn-primary" Text="Next" OnClick="btnDivTwoNext_Click" />
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-12" id="divFields" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Indian</h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Calculative</h5>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlIndiaCalculative" runat="server">
                                                <div class="col-12">
                                                    <asp:ListView ID="lvIndiaCalculative" runat="server">
                                                        <EmptyDataTemplate>
                                                            <center>
                                                                <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                            </center>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Calculative Details</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>
                                                                            <asp:CheckBox ID="chkAllIndiaCalculative" Checked="false" runat="server" onclick="checkAll(this); " />Field Name                                                            
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
                                                                    <asp:CheckBox ID="chkIndiaCalculative" runat="server" AlternateText="Check All Items"
                                                                        ToolTip='<%# Eval("FNO")%>' /><%# Eval("FNAME")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                    <div class="col-12 col-md-6">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Informative</h5>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlIndiaInformative" runat="server">
                                                <div class="col-12">
                                                    <asp:ListView ID="lvIndiaInformative" runat="server">
                                                        <EmptyDataTemplate>
                                                            <center>
                                                                <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                            </center>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Informative Details</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>
                                                                            <asp:CheckBox ID="chkAllIndiaInformative" Checked="false" runat="server" onClick="SelectAll(this);" />Field Name
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
                                                                    <asp:CheckBox ID="chkIndiaInformative" runat="server" AlternateText="Check All Items"
                                                                        ToolTip='<%# Eval("FNO")%>' /><%# Eval("FNAME")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="divFieldDetails" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Foreign</h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Calculative</h5>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlForeignCalculative" runat="server">
                                                <div class="col-12">
                                                    <asp:ListView ID="lvForeignCalculative" runat="server">
                                                        <EmptyDataTemplate>
                                                            <center>
                                                                <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                            </center>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Calculative Details</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>
                                                                            <asp:CheckBox ID="chkAllForeignCalculativeDetails" runat="server" />Field Name
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
                                                                    <asp:CheckBox ID="chkForeignCalculativeDetails" runat="server" AlternateText="Check All Items"
                                                                        ToolTip='<%# Eval("FNO")%>' /><%# Eval("FNAME")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                    <div class="col-12 col-md-6">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Informative</h5>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlForeignInformative" runat="server">
                                                <div class="col-12">
                                                    <asp:ListView ID="lvForeignInformative" runat="server">
                                                        <EmptyDataTemplate>
                                                            <center>
                                                                <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                            </center>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Informative Details</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>
                                                                            <asp:CheckBox ID="chkAllForeignInformativeDetails" runat="server" />Field Name
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
                                                                    <asp:CheckBox ID="ChkForeignInformativeDetails" runat="server" AlternateText="Check All Items"
                                                                        ToolTip='<%# Eval("FNO")%>' /><%# Eval("FNAME")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnFieldsBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnFieldsBack_Click" />
                                        <asp:Button ID="btnFieldsNext" runat="server" CssClass="btn btn-primary" Text="Next" OnClick="btnFieldsNext_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="divVendorDetails" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12 col-md-6">

                                        <%--  <div class="col-12">
                                                <div class="sub-heading"><h5>Vendors Category</h5></div>
                                            </div>--%>
                                        <asp:Panel ID="pnlCategory" runat="server" ScrollBars="Auto">
                                            <div class="col-12">
                                                <asp:ListView ID="lvCategory" runat="server">
                                                    <EmptyDataTemplate>
                                                        <center>
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </center>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Vendors Category</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap " style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <%--onclick="checkAll(this);"--%>
                                                                <tr>
                                                                    <th>
                                                                        <%--<asp:CheckBox ID="chkAllCategory" Checked="false" runat="server" onclick="toggle(this);" />--%>  <%--12/05/2022--%>
                                                                        Select
                                                                    </th>
                                                                    <th>Category
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
                                                                <asp:CheckBox ID="ChkCategory" runat="server" AlternateText="Check  Category" ToolTip='<%# Eval("PCNO")%>' />
                                                            </td>
                                                            <td>
                                                                <%# Eval("PCNAME")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </asp:Panel>
                                        <div class="col-12 btn-footer">
                                            <asp:Button runat="server" ID="btnGetVender" Text="Get Vendors" CssClass="btn btn-primary" OnClick="btnGetVender_Click" />
                                            <%--<asp:Button ID="btnvenderGetNew" runat="server" OnClick="btnvenderGetNew_Click" Text="get vender new"/>--%>
                                        </div>

                                    </div>

                                    <div class="col-12 col-md-6" id="pnlVendors" runat="server">
                                        <asp:Panel ID="pnlVendor" runat="server">
                                            <div class="col-12">
                                                <asp:ListView ID="lvVendors" runat="server">
                                                    <EmptyDataTemplate>
                                                        <center>
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </center>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Vendors Details</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap " style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>
                                                                        <%--<asp:CheckBox ID="chkAllVendorDetails" Checked="false" runat="server" />--%> <%--12/05/2022--%>
                                                                        Select
                                                                    </th>
                                                                    <th>Vendor Name
                                                                    </th>
                                                                    <%-- <th style="width: 10%">Category
                                                                        </th>--%>
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
                                                                <asp:CheckBox ID="ChkVendorDetails" runat="server" AlternateText="Check Vendor" ToolTip='<%# Eval("PNO")%>' />
                                                            </td>
                                                            <td>
                                                                <%# Eval("PNAME")%>
                                                            </td>
                                                            <%-- <td style="width: 10%;">
                                                                    <%# Eval("PCNAME")%>
                                                                </td>--%>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </asp:Panel>

                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnVendorBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnVendorBack_Click" />
                                        <asp:Button ID="btnVendorNext" runat="server" CssClass="btn btn-primary" Text="Next" OnClick="btnVendorNext_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="divQuotationDetails" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Quotation Details</h5>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="Panel5" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Quotation Number </label>
                                            </div>
                                            <asp:TextBox ID="txtQuotationNumber" runat="server" CssClass="form-control" TabIndex="1" MaxLength="50"
                                                Enabled="False"></asp:TextBox>
                                            <asp:DropDownList Visible="False" ID="ddlQuotno" runat="server"
                                                AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlQuotno_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvtxtQuotationNumber" runat="server" ControlToValidate="ddlQuotno"
                                                Display="None" InitialValue="0" ValidationGroup="StoreItem" ErrorMessage="Please select Quotation No."></asp:RequiredFieldValidator>

                                            <asp:RequiredFieldValidator ID="rfvQN" runat="server" ControlToValidate="txtQuotationNumber"
                                                Display="None" ValidationGroup="StoreItem" ErrorMessage="Please Enter Quotation No."></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Reference Number </label>
                                            </div>
                                            <asp:TextBox ID="txtReferenceNumber" runat="server" CssClass="form-control" TabIndex="2" MaxLength="25" Enabled="False">
                                            </asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtReferenceNumber"
                                                runat="server" ControlToValidate="txtReferenceNumber" Display="None" ValidationGroup="StoreItem"
                                                ErrorMessage="Please Enter Refrence No."></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Rate Valid Up To </label>
                                            </div>
                                            <asp:TextBox ID="txtRateValidupto" runat="server" CssClass="form-control" TabIndex="3" MaxLength="4" onKeyUp="return validateNumeric(this)"></asp:TextBox><asp:RequiredFieldValidator
                                                ID="rfvtxtRateValidupto" runat="server" ControlToValidate="txtRateValidupto"
                                                Display="None" ValidationGroup="StoreItem" ErrorMessage="Please Enter Rate Valid Up To"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cmptxtRateValid" runat="server" Display="None" ErrorMessage="Enter Only Numeric Value for Quantity"
                                                ControlToValidate="txtRateValidupto" Type="Double" Operator="DataTypeCheck" ValidationGroup="StoreItem"></asp:CompareValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Rate Validity </label>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlRateValidupto" CssClass="form-control" data-select2-enable="true" TabIndex="4">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Day(s)</asp:ListItem>
                                                <asp:ListItem Value="2">Month</asp:ListItem>
                                                <asp:ListItem Value="3">Week</asp:ListItem>
                                                <asp:ListItem Value="4">Year</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlRateValidupto" runat="server" ControlToValidate="ddlRateValidupto"
                                                Display="None" ValidationGroup="StoreItem" ErrorMessage="Please Select Rate Validity"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Sending Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgSendingDate" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtSendingDate" runat="server" TabIndex="6" CssClass="form-control"></asp:TextBox>

                                                <%--<asp:Image ID="imgSendingDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                <ajaxToolKit:MaskedEditExtender
                                                    ID="MaskedEditExtender4" runat="server" TargetControlID="txtSendingDate" Mask="99/99/9999"
                                                    MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender4"
                                                    ControlToValidate="txtSendingDate" EmptyValueMessage="Please Select Sending Date."
                                                    InvalidValueMessage="Sending Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Select Sending date." EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Sending date "
                                                    ValidationGroup="StoreItem" SetFocusOnError="True" ErrorMessage="MaskedEditValidator4" />

                                                <ajaxToolKit:CalendarExtender
                                                    ID="ceSendingDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgSendingDate"
                                                    TargetControlID="txtSendingDate">
                                                </ajaxToolKit:CalendarExtender>

                                                <asp:RequiredFieldValidator ID="rfvtxtSendingDate" runat="server" ControlToValidate="txtSendingDate"
                                                    Display="None" ValidationGroup="StoreItem" ErrorMessage="Please Select Sending Date."></asp:RequiredFieldValidator>
                                            </div>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Last Date of Receipt </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImaCalStartDate" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtLasteDateofReciptTime" runat="server" TabIndex="7" CssClass="form-control"></asp:TextBox>
                                                <%--<asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                <ajaxToolKit:MaskedEditExtender ID="meReqdate" runat="server" TargetControlID="txtLasteDateofReciptTime"
                                                    Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevIndDate" runat="server" ControlExtender="meReqdate"
                                                    ControlToValidate="txtLasteDateofReciptTime" EmptyValueMessage="Please enter Last date of Receipt"
                                                    InvalidValueMessage="Last Date of Receipt is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter Last Date of Receipt" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid last Date of Receipt " ValidationGroup="StoreItem"
                                                    SetFocusOnError="True" ErrorMessage="mevIndDate" />
                                                <ajaxToolKit:CalendarExtender
                                                    ID="ceLasteDateofReciptTime" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    PopupButtonID="ImaCalStartDate" TargetControlID="txtLasteDateofReciptTime">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLasteDateofReciptTime"
                                                    Display="None" ValidationGroup="StoreItem" ErrorMessage="Please Select Last Date of Receipt."></asp:RequiredFieldValidator>
                                                <%--   <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="meReqdate" ControlToValidate="txtLasteDateofReciptTime"
                                                                        IsValidEmpty="false" ErrorMessage="Please Enter Valid Last Date Of Receipt In [dd/MM/yyyy] format"
                                                                        InvalidValueMessage="Please Enter Valid Last Date Of Receipt In [dd/MM/yyyy] format" Display="None" SetFocusOnError="true"
                                                                        Text="*" ValidationGroup="StoreItem"></ajaxToolKit:MaskedEditValidator>
                                                <asp:RequiredFieldValidator ID="rfvtxtLasteDateofReciptTime" runat="server" ControlToValidate="txtLasteDateofReciptTime"
                                                    Display="None" ValidationGroup="StoreItem" ErrorMessage="Please Enter Last Date Of Reciept"></asp:RequiredFieldValidator>--%>

                                                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtQuotationOpeningDate"
                                                    ControlToCompare="txtLasteDateofReciptTime" Type="Date" Operator="GreaterThan"
                                                    runat="server" ErrorMessage="Quotation Opening Date should be greater than Last Date of Receipt"
                                                    Display="None" ValidationGroup="StoreItem"></asp:CompareValidator>
                                                <asp:CompareValidator ID="CompareValidator2" ControlToValidate="txtSendingDate" ControlToCompare="txtQuotationOpeningDate"
                                                    Type="Date" Operator="LessThan" runat="server" ErrorMessage="Sending date should be less than Quotation Opening Date"
                                                    Display="None" ValidationGroup="StoreItem"></asp:CompareValidator>
                                                <asp:CompareValidator ID="CompareValidator3" ControlToValidate="txtSendingDate" ControlToCompare="txtLasteDateofReciptTime"
                                                    Type="Date" Operator="LessThan" runat="server" ErrorMessage="Sending date should be less than last date of receipt"
                                                    Display="None" ValidationGroup="StoreItem"></asp:CompareValidator>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divLastTime" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:TextBox ID="txtLasteDateTime" runat="server" TabIndex="8" ToolTip=" Tip:Type 'A' or 'P' to switch AM/PM"
                                                CssClass="form-control" ValidationGroup="StoreItem" />
                                            <%--<asp:RequiredFieldValidator ID="rfvtxtLasteDateTime" runat="server" ControlToValidate="txtLasteDateTime"
                                                Display="None" ValidationGroup="StoreItem" ErrorMessage="Please Enter Time For Last Date Of Receipt"></asp:RequiredFieldValidator>--%>

                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtLasteDateTime"
                                                Mask="99:99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" AutoComplete="False" />
                                            <%--<ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender3"
                                                ControlToValidate="txtLasteDateTime" IsValidEmpty="False" EmptyValueMessage="Time for Last Date of Quotation is required"
                                                InvalidValueMessage="Time for Last Date of Recipt is invalid" Display="None"
                                                TooltipMessage="Input a time" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                ValidationGroup="StoreItem" ErrorMessage="MaskedEditValidator1" />--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Quotation Opening Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgQuotationOpeningDate" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtQuotationOpeningDate" runat="server" TabIndex="9" CssClass="form-control"></asp:TextBox>

                                                <%--<asp:Image ID="imgQuotationOpeningDate" runat="server" ImageUrl="~/images/calendar.png"
                                                    Style="cursor: pointer" />--%>
                                                <ajaxToolKit:CalendarExtender ID="ceQuotationOpeningDate"
                                                    runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgQuotationOpeningDate"
                                                    TargetControlID="txtQuotationOpeningDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtQuotationOpeningDate" runat="server" ControlToValidate="txtQuotationOpeningDate"
                                                    Display="None" ValidationGroup="StoreItem" ErrorMessage="Please Select Quotation Opening Date"></asp:RequiredFieldValidator><ajaxToolKit:MaskedEditExtender
                                                        ID="MaskedEditExtender2" runat="server" TargetControlID="txtQuotationOpeningDate"
                                                        Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                    ControlToValidate="txtQuotationOpeningDate" EmptyValueMessage="Please Select Quotation Opening Date"
                                                    InvalidValueMessage="Quotation opening date is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Select Quotation opening date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Quotation opening date " ValidationGroup="StoreItem"
                                                    SetFocusOnError="True" ErrorMessage="MaskedEditValidator2" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBudget" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Budget Head </label>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlBudgetHead" AppendDataBoundItems="True" TabIndex="11" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlBudgetHead" runat="server" ControlToValidate="ddlBudgetHead"
                                                Display="None" ValidationGroup="StoreItem" ErrorMessage="Please Select Budget Head"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Terms and Conditions </label>
                                            </div>
                                            <%-- <asp:FileUpload ID="FileUpload2" runat="server" onChange="return ValidateTermsrFile()" TabIndex="13" />--%>
                                            <asp:TextBox ID="txtTermsCondition" runat="server" TextMode="MultiLine"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Top Matter/Schedule </label>
                                            </div>
                                            <%--<asp:FileUpload ID="FileUpload1" onChange="return ValidateMatterFile()" TabIndex="12" runat="server" />--%>
                                            <asp:TextBox ID="txtTopMatter" runat="server" TextMode="MultiLine"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Subject </label>
                                            </div>
                                            <asp:TextBox ID="txtSubject" runat="server" TabIndex="15" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Any Specification </label>
                                            </div>
                                            <asp:TextBox ID="txtSpecification" TextMode="MultiLine" runat="server" CssClass="form-control" TabIndex="14"></asp:TextBox><asp:FileUpload
                                                ID="FileUpload3" runat="server" Visible="false" TabIndex="14" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Authority </label>
                                            </div>
                                            <asp:RadioButtonList ID="rblAuthority" TabIndex="16" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0" Selected="True">Principal</asp:ListItem>
                                                <asp:ListItem Value="1">Vice Chancellor</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divQuotOpenTime" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:TextBox ID="txtQuotationOpeningDateTime" runat="server" ToolTip="Tip: Type 'A' or 'P' to switch AM/PM" TabIndex="10" CssClass="form-control"
                                                ValidationGroup="StoreItem" />
                                            <%--<asp:RequiredFieldValidator ID="rfvtxtQuotationOpeningDateTime" runat="server" ControlToValidate="txtQuotationOpeningDateTime"
                                                Display="None" ValidationGroup="StoreItem" ErrorMessage="Please Enter Time For Quotation Opening Date"></asp:RequiredFieldValidator>--%>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtQuotationOpeningDateTime"
                                                Mask="99:99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" AutoComplete="False" />

                                            <%--<ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender3"
                                                ControlToValidate="txtQuotationOpeningDateTime" IsValidEmpty="False" EmptyValueMessage="Quotation Opening Time is required"
                                                InvalidValueMessage="Quotation Opening Time is invalid" Display="Dynamic" TooltipMessage="Input a time"
                                                EmptyValueBlurredText="*" InvalidValueBlurredMessage="*" ValidationGroup="StoreItem"
                                                ErrorMessage="MaskedEditValidator3" />--%>
                                        </div>

                                    </div>
                                </asp:Panel>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnQuoDetailsBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnQuoDetailsBack_Click" />
                                    <asp:Button ID="butSaveQuotation" runat="server" Text="Save Quotation" CssClass="btn btn-primary"
                                        OnClick="butSaveQuotation_Click" ValidationGroup="StoreItem" TabIndex="17" />
                                    <asp:Button ID="btnModify"
                                        runat="server" Text="Modify" UseSubmitBehavior="false" OnClick="btnModify_Click" CssClass="btn btn-primary" TabIndex="18" />
                                    <asp:Button ID="btnQuoDetailsNext" runat="server" CssClass="btn btn-primary" Text="Next" OnClick="btnQuoDetailsNext_Click" />
                                    <asp:Button ID="btncancel"
                                        runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="19" OnClick="btncancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1"
                                        runat="server" ValidationGroup="StoreItem" ShowMessageBox="True" ShowSummary="False" />

                                </div>
                            </div>

                            <div class="col-12" id="divSearch" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Select Opening Date</h5>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="Panel6" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Start Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImaCalFromDate" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" ToolTip="Select Start Date" CssClass="form-control"
                                                    TabIndex="1" ValidationGroup="StoreRep"></asp:TextBox>

                                                <%--<asp:Image ID="ImaCalFromDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>

                                                <ajaxToolKit:MaskedEditExtender
                                                    ID="MaskedEditExtender5" runat="server" TargetControlID="txtFromDate" Mask="99/99/9999"
                                                    MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImaCalFromDate" TargetControlID="txtFromDate">
                                                </ajaxToolKit:CalendarExtender>


                                                <ajaxToolKit:MaskedEditValidator ID="mevFrom" runat="server"
                                                    ControlExtender="MaskedEditExtender5" ControlToValidate="txtFromDate" Display="None"
                                                    EmptyValueBlurredText="Empty" EmptyValueMessage="Please Select Start Date"
                                                    InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="Start Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    SetFocusOnError="true" TooltipMessage="Please Select From Date" IsValidEmpty="false"
                                                    ValidationGroup="StoreRep">
                                                </ajaxToolKit:MaskedEditValidator>

                                                <%-- <asp:RequiredFieldValidator ID="rfvtxtFromDate" runat="server" ControlToValidate="txtFromDate" SetFocusOnError="true"
                                                    Display="None" ValidationGroup="StoreRep" ErrorMessage="Please Enter Start  Date "></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>End Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImaCalToDate" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" ToolTip="Select End Date" CssClass="form-control" TabIndex="2"
                                                    ValidationGroup="StoreRep"></asp:TextBox>

                                                <%--<asp:Image ID="ImaCalToDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>

                                                <ajaxToolKit:MaskedEditExtender
                                                    ID="MaskedEditExtender6" runat="server" TargetControlID="txtToDate" Mask="99/99/9999"
                                                    MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImaCalToDate" TargetControlID="txtToDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <%-- <asp:RequiredFieldValidator ID="rfvtxtToDate" runat="server" ControlToValidate="txtToDate"
                                                        Display="None" ValidationGroup="StoreRep" ErrorMessage="Please Enter End  Date "></asp:RequiredFieldValidator>--%>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                    ControlExtender="MaskedEditExtender6" ControlToValidate="txtToDate" Display="None"
                                                    EmptyValueBlurredText="Empty" EmptyValueMessage="Please Select End Date"
                                                    InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="End Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    SetFocusOnError="true" TooltipMessage="Please Select To Date" IsValidEmpty="false"
                                                    ValidationGroup="StoreRep">
                                                </ajaxToolKit:MaskedEditValidator>
                                                <asp:CompareValidator ID="cmpValidator" runat="server" ControlToCompare="txtFromDate"
                                                    ControlToValidate="txtToDate" Display="None" ErrorMessage="End Date should be greater than or equal to Start Date"
                                                    Operator="GreaterThanEqual" Type="Date" ValidationGroup="StoreRep"></asp:CompareValidator>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSearchRep" runat="server" ValidationGroup="StoreRep" Text="Search" TabIndex="3"
                                                CssClass="btn btn-primary" ToolTip="Click To Search" OnClick="btnSearchRep_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary2"
                                                runat="server" ValidationGroup="StoreRep" ShowMessageBox="True" ShowSummary="False" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-12" id="divReport" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Quotation Ref No.</h5>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="Panel3" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Quotation To Display The Report </label>
                                            </div>
                                            <%--  <asp:ListBox ID="lstquot" CssClass="form-control multi-select-demo"  AutoPostBack="true" TabIndex="4" runat="server"></asp:ListBox>--%>
                                            <asp:DropDownList ID="lstquot" CssClass="form-control" runat="server" AppendDataBoundItems="true" TabIndex="1" ToolTip="Quotation List">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvQuotReport" ControlToValidate="lstquot" InitialValue=""
                                                runat="server" ErrorMessage="Please Select Quotation Reference Number" ValidationGroup="ShowReport"
                                                Display="None"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnRpt" runat="server" ValidationGroup="ShowReport" Text="Show Report" OnClick="btnRpt_Click"
                                                CssClass="btn btn-info" />
                                            <%-- <asp:Button ID="Button2" runat="server" ValidationGroup="StoreRep" Text="Search" TabIndex="3"
                                                    CssClass="btn btn-primary" ToolTip="Click To Search"   OnClick="btnSearchRep_Click" />--%>
                                            <asp:ValidationSummary ID="ValidationSummary3"
                                                runat="server" ValidationGroup="ShowReport" ShowMessageBox="True" ShowSummary="False" DisplayMode="List" />
                                            <asp:Button ID="btnNoticeRpt" runat="server" Visible="false" Text="Quotation Notice" ValidationGroup="QuotReport"
                                                OnClick="btnNoticeRpt_Click" CssClass="btn btn-info" TabIndex="6" />
                                            <asp:Button ID="btnSampleQuotRpt" runat="server" Visible="false" Text="Sample Quotation" ValidationGroup="QuotReport"
                                                OnClick="btnSampleQuotRpt_Click" CssClass="btn btn-info" TabIndex="7" />
                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning" Text="Back" OnClick="Button1_Click" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearchRep" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }

        function toggleExpansion(imageCtl, divId) {

            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../../images/collapse_blue.jpg";
            }
        }



        // function for numeric textbox

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1); txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }

        function SelectAll(mainChk) {
            // var frm = document.lvDsrIssue[0]
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (mainChk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }

            }
        }

    </script>

    <script type="text/javascript">
        $(document).ready(function () {

            //Uncheck All Checkbox on Indent select
            $("[id$='CheckIndent']").click(function () {

                $("[id$='chkAllCategory']").attr('checked', false);
                $("[id$='chkAllIndiaCalculative']").attr('checked', false);
                $("[id$='chkAllVendorDetails']").attr('checked', false);
                $("[id$='ChkVendorDetails']").attr('checked', false);

            });

            //Uncheck All Checkbox on Indent select
            $("[id$='ddlQuotno']").change(function () {


                $("[id$='chkAllCategory']").attr('checked', false);
                $("[id$='chkAllIndiaCalculative']").attr('checked', false);
                $("[id$='chkAllVendorDetails']").attr('checked', false);
                $("[id$='ChkVendorDetails']").attr('checked', false);
            });

            //Check / Uncheck All Checkbox for Vendor Category
            $("[id$='chkAllCategory']").live('click', function () {
                $("[id$='ChkCategory']").attr('checked', this.checked);

                $("[id$='chkAllVendorDetails']").attr('checked', false);
                $("[id$='ChkVendorDetails']").attr('checked', false);

            });

            //Check / Uncheck All Checkbox for Vendor 
            $("[id$='chkAllVendorDetails']").live('click', function () {
                $("[id$='ChkVendorDetails']").attr('checked', this.checked);

            });

            //Check / Uncheck All Checkbox for India Calculative Field
            $("[id$='chkAllIndiaCalculative']").live('click', function () {
                $("[id$='chkIndiaCalculative']").attr('checked', this.checked);

            });

            //Check / Uncheck All Checkbox for India Informative Field
            $("[id$='chkAllIndiaInformative']").live('click', function () {
                $("[id$='chkIndiaInformative']").attr('checked', this.checked);

            });

            //Check / Uncheck All Checkbox for Foreign Calculative Field
            $("[id$='chkAllForeignCalculativeDetails']").live('click', function () {
                $("[id$='chkForeignCalculativeDetails']").attr('checked', this.checked);

            });


            //Check / Uncheck All Checkbox for Items
            $("[id$='chkSelectAll']").live('click', function () {
                $("[id$='chkSelect']").attr('checked', this.checked);

            });
        });



        // Checking for File validation

        //  var validFilesTypes = ["txt"];

        // function ValidateMatterFile() {

        //   var file = document.getElementById("");
        //  var path = file.value;
        //  var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
        //  var isValidFile = false;

        //  for (var i = 0; i < validFilesTypes.length; i++) {

        //    if (ext == validFilesTypes[i]) {
        //        isValidFile = true;
        //        break;
        //   }
        // }

        //  if (!isValidFile) {
        //    file.value = "";
        //   alert("Upload Only text file");
        //   return false;
        // }
        //  return isValidFile;
        //  }




        function ValidateTermsrFile() {

            // var file = document.getElementById("");
            var path = file.value;
            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
            var isValidFile = false;

            for (var i = 0; i < validFilesTypes.length; i++) {

                if (ext == validFilesTypes[i]) {
                    isValidFile = true;
                    break;
                }
            }

            if (!isValidFile) {

                file.value = "";
                alert("Upload Only text file");
                return false;
            }
            return isValidFile;
        }
    </script>

    <%-- <script type="text/javascript">
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

    </script>--%>

    <script>            //19-02-2022
        function toggle(source) {
            var checkboxes = document.querySelectorAll('input[type="checkbox"]');
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i] != source)
                    checkboxes[i].checked = source.checked;
            }
        }
    </script>

    <%--  Reset the sample so it can be played again --%>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

