<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_Technical_Bid.aspx.cs" Inherits="STORES_Transactions_Quotation_Str_Technical_Bid"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
<script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>--%>

<%--    <link href="../../../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multi-select/bootstrap-multiselect.js"></script>--%>
    <script language="javascript" type="text/javascript">
        function confirmDeleteResult(v, m, f) {
            if (v) //user clicked OK 
                $('#' + f.hidID).click();
        }

    </script>

    <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
    <br />
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div3" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">TECHNICAL BID</h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#Div1">Tender Selection</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#Div2">Vendor Technical Specification</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#Div3">Recommendation for Commercial Bid</a>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active" id="Div1">
                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updatePanel3"
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
                                <asp:UpdatePanel ID="updatePanel3" runat="server">
                                    <ContentTemplate>
                                        <%--   <form role="form">--%>
                                        <div class="box-body">

                                            <asp:Panel ID="pnl" runat="server">
                                                <div class="col-12">
                                                    <div class="row">

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Tender List</label>
                                                            </div>
                                                            <asp:DropDownList ID="lstTendor" runat="server" CssClass="form-control" AutoPostBack="True" TabIndex="1" ToolTip="Tender List"
                                                                OnSelectedIndexChanged="lstTendor_SelectedIndexChanged" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvtender" runat="server" ControlToValidate="lstTendor" ValidationGroup="Submit"
                                                                ErrorMessage="Please Select Tender From List" Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel4" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Tender Details</h5>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Tender Number :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblTREFNO" runat="server"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Financial Bid Opening Date  :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblTOpen" runat="server"></asp:Label></a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-4 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>EMD :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblEMD" runat="server"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Sales Tax :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblSalesTax" runat="server"></asp:Label></a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-4 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Tender Amount :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblTAMT" runat="server"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Total Amount  :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblTotal" runat="server"></asp:Label></a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>



                                            <asp:ImageButton ID="imgFieldNext" runat="server" ImageUrl="~/images/next.jpeg" ToolTip="Next"
                                                OnClick="imgFieldNext_Click" CssClass="pull-right" Visible="false" />
                                        </div>

                                        <%--</form>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <div class="tab-pane" id="Div2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="updatePanel1"
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
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Vendor Information</h5>
                                                </div>
                                                <asp:Panel ID="pnlVendor" runat="server">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Vendor Name</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="True"
                                                                AppendDataBoundItems="True" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" ToolTip="Select The Vendor Name">
                                                                <asp:ListItem Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlVendor" ValidationGroup="Submit"
                                                                ErrorMessage="Please Select Vendor Name" Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Vendor Code</label>
                                                            </div>
                                                            <asp:TextBox ID="txtVCode" runat="server" CssClass="form-control"
                                                                Enabled="False" TabIndex="2" ToolTip="Enter Vendor Code"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Vendor Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtvnd" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Enter Vendor Name"
                                                                Enabled="False"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Contact Number</label>
                                                            </div>
                                                            <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Enter Contact Number" Enabled="False"></asp:TextBox>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Vendor Address</label>
                                                            </div>
                                                            <asp:TextBox ID="txtvndadd" runat="server" TextMode="MultiLine"
                                                                CssClass="form-control" TabIndex="8" ToolTip="Enter Vendor Address" Enabled="False"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>CST </label>
                                                            </div>

                                                            <asp:TextBox ID="txtcst" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Enter CST"
                                                                Enabled="False"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>BST </label>
                                                            </div>
                                                            <asp:TextBox ID="txtbst" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Enter BST" Enabled="False"></asp:TextBox>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Email Address </label>
                                                            </div>
                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TabIndex="7" ToolTip="Enter Email Address" Enabled="False"></asp:TextBox>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Vendor Remark </label>
                                                            </div>
                                                            <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine"
                                                                CssClass="form-control" TabIndex="9" ToolTip="Enter Vendor Remark" Enabled="False"></asp:TextBox>

                                                        </div>





                                                    </div>

                                                </asp:Panel>
                                            </div>
                                            <div class="col-12 mt-3" style="display: none">
                                                <asp:Panel ID="pnlItem" runat="server">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Technical Specification</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Technical Specification Upload</label>
                                                            </div>
                                                            <asp:FileUpload ID="fldtechspech" runat="server" TabIndex="10" />
                                                            <asp:TextBox runat="server" ID="txtSpec" TabIndex="11" CssClass="form-control" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                            </div>

                                            <div class="col-12  mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Item List</h5>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12 table-responsive">
                                                        <asp:GridView CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" ID="grdItemList" runat="server" AutoGenerateColumns="False"
                                                            DataKeyNames="ITEM_NO">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Item Name" HeaderStyle-CssClass="bg-light-blue" DataField="ITEM_NAME" HeaderStyle-HorizontalAlign="Center">
                                                                    <ControlStyle Width="120px" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Unit" HeaderStyle-CssClass="bg-light-blue" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnit" Text='<%#Eval("ITEM_UNIT") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tech. Specification" HeaderStyle-CssClass="bg-light-blue" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTechspec" Text='<%#Eval("TECHSPEC") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vendor Tech. Specification" HeaderStyle-CssClass="bg-light-blue" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtPerticular" runat="server" CssClass="form-control" TabIndex="12" TextMode="MultiLine">
                                                                        </asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="bg-light-blue" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control" TabIndex="13">
                                                                            <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                            <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                        </asp:DropDownList>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="altitem" />
                                                            <EmptyDataRowStyle CssClass="altitem" />
                                                            <HeaderStyle CssClass="gv_header" HorizontalAlign="Left" />
                                                            <RowStyle CssClass="item" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Submit" CssClass="btn btn-primary" TabIndex="14" ToolTip="Click To Save" ValidationGroup="Submit" />
                                                <asp:Button ID="btnMod" runat="server" OnClick="btnMod_Click" Text="Modify" CssClass="btn btn-primary" TabIndex="15" ToolTip="Click To Modify" Visible="false" />
                                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" TabIndex="16" ToolTip="Click To Reset" />
                                                <asp:ValidationSummary ID="valsum" runat="server" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="Div3">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatePanel2"
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
                                <asp:UpdatePanel ID="updatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Tender List</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Tender List</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlTender" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" ToolTip="Select Tender" AutoPostBack="True"
                                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlTender_SelectedIndexChanged">
                                                            <asp:ListItem Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvtender2" runat="server" Display="None" ControlToValidate="ddlTender" ValidationGroup="SaveVendor" ErrorMessage="Please Select Tender From List" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Vendor List</h5>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-12 table-responsive">
                                                        <asp:GridView runat="server" CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" ID="grdVendor" AutoGenerateColumns="False"
                                                            DataKeyNames="TVNO" EmptyDataText="There Is No Vendor For Recommendation" AllowSorting="True"
                                                            AllowPaging="True" PageSize="20" OnSelectedIndexChanging="grdVendor_SelectedIndexChanging"
                                                            OnPageIndexChanging="grdVendor_PageIndexChanging">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="bg-light-blue">
                                                                    <HeaderTemplate>Select</HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" ID="chkVendor" ToolTip='<%# Eval("TVNO")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vendor Details" HeaderStyle-CssClass="bg-light-blue">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblVName" Text='<%# Eval("VENDORNAME")%> '> </asp:Label>
                                                                        <br />
                                                                        <asp:Label runat="server" ID="lblVAddress" Text='<%# Eval("ADDRESS")%> '> </asp:Label>
                                                                        <br />
                                                                        <asp:Label runat="server" ID="lblVContact" Text='<%# Eval("VENDOR_CONTACT")%>'> </asp:Label>
                                                                        <br />
                                                                        <asp:Label runat="server" ID="lblVEMAIL" Text='<%# Eval("EMAIL")%> '> </asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Vendor Tech. Specification" HeaderStyle-CssClass="bg-light-blue" DataField="TECHSPECI">
                                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                </asp:BoundField>
                                                                <asp:CommandField ShowSelectButton="True" Visible="false" HeaderText="Download Specification Document" SelectText="Show Specification Document" HeaderStyle-CssClass="bg-light-blue">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:CommandField>
                                                            </Columns>
                                                            <EmptyDataRowStyle BackColor="WhiteSmoke" />
                                                            <HeaderStyle CssClass="gv_header" />
                                                            <PagerSettings Mode="NextPrevious" NextPageText="Next" PreviousPageText="Prev" />
                                                            <PagerStyle CssClass="PagerStyle" />
                                                            <RowStyle BackColor="AntiqueWhite" />
                                                            <SelectedRowStyle CssClass="grid_bg " />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:GridView ID="GvCmpAllItem" runat="server" CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue">
                                            </asp:GridView>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnRSave" runat="server" OnClick="btnRSave_Click" Text="Save Vendor" ToolTip="Save Vendor" TabIndex="2" CssClass="btn btn-primary" ValidationGroup="SaveVendor" />
                                                <asp:Button ID="btnReport1" runat="server" Text="Report" OnClick="btnReport1_Click" ToolTip="Click To Reprot" TabIndex="4" CssClass="btn btn-info" />
                                                <asp:Button ID="btnCmpRpt" runat="server" Text="Comparative Statement Excel" OnClick="btnCmpRpt_Click" ToolTip="Click To Comparative Report" TabIndex="5" CssClass="btn btn-info" />
                                                <asp:Button ID="btnRCancel" runat="server" OnClick="btnRCancel_Click" Text="Cancel" ToolTip="Click To Cancel" TabIndex="3" CssClass="btn btn-warning" />
                                                <asp:ValidationSummary ID="ValSumma" runat="server" ValidationGroup="SaveVendor" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                            </div>
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnCmpRpt"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <ajaxToolKit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="1" Width="990px"
        CssClass="ajax__tab_yuitabview-theme">
        <ajaxToolKit:TabPanel runat="server" ID="TabPanel1"
            TabIndex="0">
            <%-- <HeaderTemplate>
                                            Tender Selection
                                        </HeaderTemplate>--%>
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <br />
                            <div id="div1" style="display: block;">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="padding-left: 10px">
                                            <asp:Panel ID="Panel1" runat="server" Width="100%">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="padding-left: 10px; width: 300px; padding-right: 10px" align="left">
                                                            <%-- <fieldset class="fieldset">
                                                                                            <legend class="legend">Tender List</legend>--%>
                                                            <br />
                                                            <%--<asp:ListBox ID="lstTendor" runat="server" AutoPostBack="True" Width="300px" Height="200px"
                                                                                                OnSelectedIndexChanged="lstTendor_SelectedIndexChanged"></asp:ListBox>--%>
                                                            <br />
                                                            <br />
                                                            </fieldset>
                                                        </td>
                                                        <td>
                                                            <%-- <fieldset class="fieldset">
                                                                                            <legend class="legend">Tender Details</legend>--%>
                                                            <br />
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td align="left" style="padding-left: 10px; width: 60%">
                                                                        <%--<b>Tender Number : </b>
                                                                                                        <asp:Label ID="lblTREFNO" runat="server"></asp:Label>--%>
                                                                    </td>
                                                                    <td align="left" style="padding-left: 10px;">
                                                                        <%-- <b>Tender Opening Date : </b>
                                                                                                    <asp:Label ID="lblTOpen" runat="server"></asp:Label>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="padding-left: 10px; width: 60%">
                                                                        <%--<b>EMD : </b>
                                                                                                    <asp:Label ID="lblEMD" runat="server"></asp:Label>--%>
                                                                    </td>
                                                                    <td align="left" style="padding-left: 10px;">
                                                                        <%--<b>Sales Tax : </b>
                                                                                                    <asp:Label ID="lblSalesTax" runat="server"></asp:Label>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="padding-left: 10px; width: 60%">
                                                                        <%--<b>Tender Amount : </b>
                                                                                                    <asp:Label ID="lblTAMT" runat="server"></asp:Label>--%>
                                                                    </td>
                                                                    <td align="left" style="padding-left: 10px;">
                                                                        <%--<b>Total Amount : </b>
                                                                                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 10px; padding-right: 10px" align="center">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <table>
                    <tr>
                        <td align="right" style="padding-left: 940px">
                            <%--<asp:ImageButton ID="imgFieldNext" runat="server" ImageUrl="~/images/next.jpeg" ToolTip="Next"
                                                            OnClick="imgFieldNext_Click" />--%>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
        <ajaxToolKit:TabPanel runat="server" ID="TabPanel2"
            TabIndex="1">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <br />
                            <div id="divItems" style="display: block;">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="padding-left: 10px; padding-right: 10px">
                                            <asp:Panel ID="pnlitems" runat="server" Width="100%">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <%--<asp:Panel ID="pnlVendor" runat="server" Width="100%">--%>
                                                            <%-- <fieldset class="fieldset">
                                                                                                <legend class="legend">Vendor Information</legend>--%>
                                                            <%--  <asp:Panel ID="pnlmodify" runat="server">
                                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                                    <br />
                                                                                                   <%-- <tr align="left">
                                                                                                        <td align="left">
                                                                                                            Select The Vendor Name
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                            <b>:</b>
                                                                                                        </td>
                                                                                                        <td align="left" style="padding-left: 10px;">
                                                                                                            <asp:DropDownList ID="ddlVendor" runat="server" CssClass="dropdownlist" AutoPostBack="True"
                                                                                                                AppendDataBoundItems="True" Width="300px" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged">
                                                                                                                <asp:ListItem Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>--%>
                                                            <br />
                                                            <table cellpadding="2" cellspacing="2" width="100%">
                                                                <tr style="padding-left: 15px;">
                                                                    <td align="left"><%--Select The Vendor Name
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                            <b>:</b>
                                                                                                        </td>
                                                                                                        <td align="left">
                                                                                                            <asp:DropDownList ID="ddlVendor" runat="server" CssClass="dropdownlist" AutoPostBack="True"
                                                                                                                AppendDataBoundItems="True" Width="255px" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged">
                                                                                                                <asp:ListItem Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                                                                                            </asp:DropDownList>--%>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>

                                                                <tr style="padding-left: 15px;">

                                                                    <td style="padding-left: 10px;"><%--Vendor Code
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                            <b>:</b>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtVCode" runat="server" CssClass="texbox"
                                                                                                                Width="250px" Enabled="False"></asp:TextBox>--%>
                                                                    </td>
                                                                    <td><%--CST
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                            <b>:</b>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtcst" runat="server" CssClass="texbox"
                                                                                                                Enabled="False"></asp:TextBox>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr style="padding-left: 15px;">
                                                                    <td style="padding-left: 10px;"><%--Vendor Name
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                            <b>:</b>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtvnd" runat="server" CssClass="texbox"
                                                                                                                Width="250px" Enabled="False"></asp:TextBox>--%>
                                                                    </td>
                                                                    <td><%--BST
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                            <b>:</b>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtbst" runat="server" CssClass="texbox" Enabled="False"></asp:TextBox>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr style="padding-left: 15px;">
                                                                    <td style="padding-left: 10px;"><%--Contact Number
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                            <b>:</b>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtContact" runat="server" CssClass="texbox"
                                                                                                                Width="250px" Enabled="False"></asp:TextBox>--%>
                                                                    </td>
                                                                    <td><%--Email Address
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                            <b>:</b>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="texbox" Enabled="False"></asp:TextBox>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr style="padding-left: 15px;">
                                                                    <td style="padding-left: 10px;"><%--Vendor Address
                                                                                                    </td>
                                                                                                    <td style="width: 2%">
                                                                                                        <b>:</b>
                                                                                                    </td>
                                                                                                    <td colspan="4">
                                                                                                        <asp:TextBox ID="txtvndadd" runat="server" CssClass="texbox" TextMode="MultiLine"
                                                                                                            Height="40px" Width="580px" Enabled="False"></asp:TextBox>--%>
                                                                    </td>
                                                                </tr>

                                                                <tr style="padding-left: 15px;">
                                                                    <td style="padding-left: 10px;"><%--Vendor Remark
                                                                                                    </td>
                                                                                                    <td style="width: 2%">
                                                                                                        <b>:</b>
                                                                                                    </td>
                                                                                                    <td colspan="4">
                                                                                                        <asp:TextBox ID="txtremark" runat="server" CssClass="texbox" TextMode="MultiLine"
                                                                                                            Height="40px" Width="580px" Enabled="False"></asp:TextBox>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table cellpadding="2" cellspacing="2" width="100%">
                                                                <%--<tr style="padding-left: 15px;">
                                                                                                    <td style="padding-left: 10px;">
                                                                                                        Vendor Address
                                                                                                    </td>
                                                                                                    <td style="width: 2%">
                                                                                                        <b>:</b>
                                                                                                    </td>
                                                                                                    <td style="padding-left: 10px;">
                                                                                                        <asp:TextBox ID="txtvndadd" runat="server" CssClass="texbox" TextMode="MultiLine"
                                                                                                            Height="40px" Width="580px" Enabled="False"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>--%>
                                                                <%--  <tr style="padding-left: 15px;">
                                                                                                    <td style="padding-left: 10px;">
                                                                                                        Vendor Remark
                                                                                                    </td>
                                                                                                    <td style="width: 2%">
                                                                                                        <b>:</b>
                                                                                                    </td>
                                                                                                    <td style="padding-left: 10px;">
                                                                                                        <asp:TextBox ID="txtremark" runat="server" CssClass="texbox" TextMode="MultiLine"
                                                                                                            Height="40px" Width="580px" Enabled="False"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>--%>
                                                            </table>
                                                            <br />
                                                            </fieldset>
                                            </asp:Panel>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%--<asp:Panel ID="pnlItem" runat="server" Width="100%">--%>
                                            <%--<fieldset class="fieldset">
                                                                                <legend class="legend">Technical Specification</legend>--%>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <%-- <br />
                                                                                        <td style="padding-left: 20px;">Technical Specification Upload
                                                                                        </td>
                                                                                        <td style="width: 2%">
                                                                                            <b>:</b>
                                                                                        </td>
                                                                                        <td>--%>
                                                    <%--<asp:FileUpload Width="250px" ID="fldtechspech" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 20px;">
                                            <%-- Technical Specification Details :--%>
                                        </td>
                                        <td>
                                            <%--<asp:TextBox runat="server" ID="txtSpec" CssClass="texbox" TextMode="MultiLine" Width="500px"
                                                                            Height="100px" Visible="false"></asp:TextBox>--%>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset>
                                                                        </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; padding-right: 10px">
                            <asp:Panel ID="Panel3" runat="server" Width="100%">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                            <%-- <fieldset class="fieldset" style="padding-left: 10px; padding-right: 10px">
                                                                            <legend class="legend">Item List.</legend>--%>
                                            <br />
                                            <%--<asp:GridView CssClass="vista-grid" ID="grdItemList" runat="server" AutoGenerateColumns="False"
                                                                                DataKeyNames="ITEM_NO">
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Item Name" DataField="ITEM_NAME" HeaderStyle-HorizontalAlign="Center">
                                                                                        <ControlStyle Width="120px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:TemplateField HeaderText="Unit" HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblUnit" Text='<%#Eval("ITEM_UNIT") %>' runat="server"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Tech. Specification" HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblTechspec" Text='<%#Eval("TECHSPEC") %>' runat="server"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Vendor Tech. Specification" HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtPerticular" runat="server" CssClass="texbox" Width="250px" TextMode="MultiLine">
                                                                                            </asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="dropdownlist" Width="80px">
                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                           
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <AlternatingRowStyle CssClass="altitem" />
                                                                                <EmptyDataRowStyle CssClass="altitem" />
                                                                                <HeaderStyle CssClass="gv_header" HorizontalAlign="Left" />
                                                                                <RowStyle CssClass="item" />
                                                                            </asp:GridView>--%>
                                            <br />
                                            <br />
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                                ValidationGroup="Store" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ValidationGroup="Store" />
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <caption>
                                                                <br />
                                                                <tr align="center" style="padding-left: 10px;">
                                                                    <td align="center">
                                                                        <%--<asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Submit" />
                                                                        <asp:Button ID="btnMod" runat="server" OnClick="btnMod_Click" Text="Modify" />
                                                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />--%>
                                                                    </td>
                                                                </tr>
                                                            </caption>
                                                        </table>
                                                    </td>
                                                </tr>
                </table>
                                            </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
        <ajaxToolKit:TabPanel runat="server" ID="TabPanel3"
            TabIndex="2">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <br />
                            <div id="div2" style="display: block;">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="padding-left: 10px">
                                            <asp:Panel ID="Panel2" runat="server" Width="100%">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="padding-left: 10px; padding-right: 10px" align="center">
                                                            <%-- <fieldset class="fieldset">
                                                                                            <legend class="legend">Tender List</legend>--%>
                                                            <br />
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <b><%--Select Tender :</b>
                                                                                                    </td>
                                                                                                    <td align="left">
                                                                                                        <asp:DropDownList ID="ddlTender" runat="server" CssClass="dropdownlist" AutoPostBack="True"
                                                                                                            AppendDataBoundItems="True" Width="250px" OnSelectedIndexChanged="ddlTender_SelectedIndexChanged">
                                                                                                            <asp:ListItem Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                                                                                        </asp:DropDownList>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 10px; padding-right: 10px" align="center">
                                                            <br />
                                                            <%--<fieldset class="fieldset">
                                                                                            <legend class="legend">Vendor List</legend>--%>
                                                            <br />
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <%--<asp:GridView runat="server" ID="grdVendor" CssClass="vista-grid " AutoGenerateColumns="False"
                                                                                                            DataKeyNames="TVNO" EmptyDataText="There Is No Vendor For Reccomondation" AllowSorting="True"
                                                                                                            AllowPaging="True" PageSize="20" OnSelectedIndexChanging="grdVendor_SelectedIndexChanging"
                                                                                                            OnPageIndexChanging="grdVendor_PageIndexChanging">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox runat="server" ID="chkVendor" ToolTip='<%# Eval("TVNO")%>' />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Vendor Details">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label runat="server" ID="lblVName" Text='<%# Eval("VENDORNAME")%> '> </asp:Label>
                                                                                                                        <br />
                                                                                                                        <asp:Label runat="server" ID="lblVAddress" Text='<%# Eval("ADDRESS")%> '> </asp:Label>
                                                                                                                        <br />
                                                                                                                        <asp:Label runat="server" ID="lblVContact" Text='<%# Eval("VENDOR_CONTACT")%>'> </asp:Label>
                                                                                                                        <br />
                                                                                                                        <asp:Label runat="server" ID="lblVEMAIL" Text='<%# Eval("EMAIL")%> '> </asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:BoundField HeaderText="Technical Specification" DataField="OTHERTECH" Visible="false">
                                                                                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                                                                                </asp:BoundField>
                                                                                                                <asp:CommandField ShowSelectButton="True" SelectText="Show Specification Document">
                                                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                                                </asp:CommandField>
                                                                                                            </Columns>
                                                                                                            <EmptyDataRowStyle BackColor="WhiteSmoke" />
                                                                                                            <HeaderStyle CssClass="gv_header" />
                                                                                                            <PagerSettings Mode="NextPrevious" NextPageText="Next" PreviousPageText="Prev" />
                                                                                                            <PagerStyle CssClass="PagerStyle" />
                                                                                                            <RowStyle BackColor="AntiqueWhite" />
                                                                                                            <SelectedRowStyle CssClass="grid_bg " />
                                                                                                        </asp:GridView>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                    <caption>
                                                        <br />
                                                        <tr>
                                                            <td>
                                                                <table cellpadding="2" cellspacing="2" width="100%">
                                                                    <tr align="center">
                                                                        <td align="center">
                                                                            <br />
                                                                            <%--<asp:GridView ID="GvCmpAllItem" runat="server">
                                                                                                        </asp:GridView>
                                                                                                        <asp:Button ID="btnRSave" runat="server" OnClick="btnRSave_Click" Text="Save Vendor" />
                                                                                                        <asp:Button ID="btnRCancel" runat="server" OnClick="btnRCancel_Click" Text="Cancel" />
                                                                                                        <asp:Button ID="btnReport1" runat="server" Text="Report" OnClick="btnReport1_Click" />
                                                                                                        <asp:Button ID="btnCmpRpt" runat="server" Text="Comparative Report" OnClick="btnCmpRpt_Click" />--%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </caption>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
            </ContentTemplate>
        </ajaxToolKit:TabPanel>
    </ajaxToolKit:TabContainer>


    <!-- MultiSelect Script -->
<%--    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
    </script>--%>
</asp:Content>
