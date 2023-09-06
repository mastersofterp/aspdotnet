<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_Tender_Entry_Form.aspx.cs" Inherits="Stores_Transactions_Quotation_Str_Tender_Entry_Form" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>--%>

    <link href="<%#Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%#Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div4" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">TENDER ENTRY FORM</h3>
                </div>
                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#IndentDiv">Indent</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#ItemsDiv">Items</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#FieldsDiv" runat="server" visible="false">Fields</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#NewsPaperDiv">News Paper</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#TenderDiv">Tender</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#TenderReportDiv">Tender Report</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="IndentDiv">
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
                                        <div class="box-body">
                                            <asp:Panel ID="pnlReqIndent" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Indent</h5>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 table-responsive">
                                                            <asp:GridView CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" runat="server" ID="grdIndList" HeaderStyle-BackColor="#cccccc"
                                                                DataKeyNames="INDNO" AutoGenerateColumns="False"
                                                                EmptyDataRowStyle-BackColor="WhiteSmoke" EmptyDataText="There Is No Indent For Tender">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Check Indent " HeaderStyle-CssClass="bg-light-blue" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:RadioButton runat="server" TabIndex="1" AutoPostBack="true" OnCheckedChanged="ChkIndentDetails_CheckChanged"
                                                                                ID="CheckIndent" Text=' <%# Eval("INDNO")%> ' ToolTip='<%# Eval("INDNO")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Department" HeaderStyle-CssClass="bg-light-blue" DataField="MDNAME" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left" />
                                                                    <asp:BoundField HeaderText="Date" HeaderStyle-CssClass="bg-light-blue" DataField="INDSLIP_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="butIndent" CssClass="btn btn-primary" TabIndex="2" ToolTip="Click To Show Items" runat="server" Text="Show Items" OnClick="butIndent_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="ItemsDiv">
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
                                            <asp:Panel ID="pnlitems" runat="server">
                                                <div class="col-12">
                                                    <div class="row">


                                                        <div class="col-12">
                                                            <asp:ListView ID="lvitems" runat="server">
                                                                <EmptyDataTemplate>
                                                                    <center>
                                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                    </center>
                                                                </EmptyDataTemplate>
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Item Details</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Item Name
                                                                                </th>
                                                                                <th>Quantity
                                                                                </th>
                                                                                <th>Value
                                                                                </th>
                                                                            </tr>
                                                                            <thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="ChkIndentDetails" TabIndex="1" runat="server" Visible="false" AlternateText="Check All Items"
                                                                                ToolTip='<%# Eval("INDENTNO")%>' /><%# Eval("ITEM_NAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REQ_QTY")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("RATE")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>

                                                        <div class="col-md-1 col-2" id="divprev1" runat="server" visible="false">
                                                            <asp:ImageButton ID="imgItemPrevious" runat="server" ToolTip="Previous" ImageUrl="~/images/prev.jpeg"
                                                                TabIndex="2" />
                                                        </div>
                                                        <div class="col-md-1 col-2" id="divnext1" runat="server" visible="false">
                                                            <asp:ImageButton ID="imgItemNext" runat="server" ToolTip="Next" ImageUrl="~/images/next.jpeg"
                                                                TabIndex="3" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="FieldsDiv">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updatePanel3"
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
                                        <div class="box-body">
                                            <asp:Panel ID="pnlFields" runat="server">

                                                <asp:Panel ID="pnl" runat="server">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Indian</h5>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-12">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Calculative</h5>
                                                                    </div>
                                                                </div>
                                                                <asp:Panel ID="Panel3" runat="server">
                                                                    <div class="col-12 table-responsive">
                                                                        <asp:Panel ID="pnlIndiaCalculative" runat="server">
                                                                            <asp:ListView ID="lvIndiaCalculative" runat="server">
                                                                                <EmptyDataTemplate>
                                                                                    <center>
                                                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                                    </center>
                                                                                </EmptyDataTemplate>
                                                                                <LayoutTemplate>
                                                                                    <div class="sub-heading">
                                                                                        <h5>Calculative Details</h5>
                                                                                    </div>
                                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                                        <thead class="bg-light-blue">
                                                                                            <tr>
                                                                                                <th>
                                                                                                    <asp:CheckBox ID="chkAllItems" TabIndex="1" runat="server" onclick="CheckAllIndents(this)" />Field Name
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
                                                                                            <asp:CheckBox ID="ChkIndentDetails" runat="server" AlternateText="Check All Items" TabIndex="2"
                                                                                                ToolTip='<%# Eval("FNO")%>' /><%# Eval("FNAME")%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>

                                                                            </asp:ListView>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="col-lg-6 col-12">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Informative</h5>
                                                                    </div>
                                                                </div>
                                                                <asp:Panel ID="Panel4" runat="server">
                                                                    <div class="col-12 table-responsive">
                                                                        <asp:Panel ID="pnlIndiaInformative" runat="server">
                                                                            <asp:ListView ID="lvIndiaInformative" runat="server">
                                                                                <EmptyDataTemplate>
                                                                                    <center>
                                                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                                    </center>
                                                                                </EmptyDataTemplate>
                                                                                <LayoutTemplate>
                                                                                    <div class="sub-heading">
                                                                                        <h5>Informative Details</h5>
                                                                                    </div>
                                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                                        <thead class="bg-light-blue">
                                                                                            <tr>
                                                                                                <th>
                                                                                                    <asp:CheckBox ID="chkAllItems" TabIndex="3" runat="server" onclick="CheckAllIndents(this)" />Field Name
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
                                                                                            <asp:CheckBox ID="ChkIndentDetails" runat="server" AlternateText="Check All Items" TabIndex="4"
                                                                                                ToolTip='<%# Eval("FNO")%>' /><%# Eval("FNAME")%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="Panel1" runat="server">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Foreign</h5>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-12">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Calculative</h5>
                                                                    </div>
                                                                </div>
                                                                <asp:Panel ID="Panel5" runat="server">
                                                                    <div class="col-12">
                                                                        <asp:Panel ID="pnlForeignCalculative" runat="server">
                                                                            <asp:ListView ID="lvForeignCalculative" runat="server">
                                                                                <EmptyDataTemplate>
                                                                                    <center>
                                                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                                    </center>
                                                                                </EmptyDataTemplate>
                                                                                <LayoutTemplate>
                                                                                    <div class="sub-heading">
                                                                                        <h5>Informative Details</h5>
                                                                                    </div>
                                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                                        <thead class="bg-light-blue">
                                                                                            <tr>
                                                                                                <th>
                                                                                                    <asp:CheckBox ID="chkAllItems" runat="server" onclick="CheckAllIndents(this)" />Field Name
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
                                                                                            <asp:CheckBox ID="ChkIndentDetails" runat="server" AlternateText="Check All Items" TabIndex="5"
                                                                                                ToolTip='<%# Eval("FNO")%>' /><%# Eval("FNAME")%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <AlternatingItemTemplate>
                                                                                    <tr>
                                                                                        <tds>
                                                                                            <asp:CheckBox ID="ChkIndentDetails" runat="server" AlternateText="Check All Items" TabIndex="6"
                                                                                                ToolTip='<%# Eval("FNO")%>' /><%# Eval("FNAME")%>
                                                                                        </tds>
                                                                                    </tr>
                                                                                </AlternatingItemTemplate>
                                                                            </asp:ListView>
                                                                        </asp:Panel>
                                                                    </div>

                                                                </asp:Panel>
                                                            </div>

                                                            <div class="col-lg-6 col-12">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Informative</h5>
                                                                    </div>
                                                                </div>
                                                                <asp:Panel ID="Panel6" runat="server">
                                                                    <div class="col-12">
                                                                        <asp:Panel ID="pnlForeignInformative" runat="server">

                                                                            <asp:ListView ID="lvForeignInformative" runat="server">
                                                                                <EmptyDataTemplate>
                                                                                    <center>
                                                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                                    </center>
                                                                                </EmptyDataTemplate>
                                                                                <LayoutTemplate>
                                                                                    <div class="sub-heading">
                                                                                        <h5>Informative Details</h5>
                                                                                    </div>
                                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                                        <thead class="bg-light-blue">
                                                                                            <tr>
                                                                                                <th>
                                                                                                    <asp:CheckBox ID="chkAllItems" TabIndex="7" runat="server" onclick="CheckAllIndents(this)" />Field Name
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
                                                                                            <asp:CheckBox ID="ChkIndentDetails" TabIndex="8" runat="server" AlternateText="Check All Items"
                                                                                                ToolTip='<%# Eval("FNO")%>' /><%# Eval("FNAME")%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>

                                                                        </asp:Panel>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnAddFields" runat="server" CssClass="btn btn-primary" TabIndex="9" Text="Add Fields" OnClick="btnAddFields_Click" />
                                                </div>

                                                <asp:Panel ID="Panel7" runat="server">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Added Field List</h5>
                                                        </div>
                                                    </div>
                                                    <div class="col-12 table-responsive">
                                                        <asp:GridView CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" ID="grdField" runat="server" AutoGenerateColumns="False"
                                                            Visible="False" OnRowCommand="grdField_RowCommand" DataKeyNames="FNO" OnRowDeleting="grdField_RowDeleting">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton CommandName="Delete" ID="btndel" runat="server" ToolTip='<%#Eval("FNO") %>'
                                                                            CommandArgument='<%#Eval("FNO") %>' Text="Delete" OnClientClick="return Cinfirm(this)"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="FNO" HeaderText="Field NO" HeaderStyle-CssClass="bg-light-blue" ReadOnly="True" SortExpression="FNO"
                                                                    HeaderStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="FNAME" HeaderText="Field Name" HeaderStyle-CssClass="bg-light-blue" SortExpression="FNAME"
                                                                    HeaderStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="FTYPE" HeaderText="Field TYPE" HeaderStyle-CssClass="bg-light-blue" SortExpression="FTYPE"
                                                                    HeaderStyle-HorizontalAlign="Left" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </asp:Panel>

                                            </asp:Panel>
                                            <div class="col-md-1 col-2" id="divprev2" runat="server" visible="false">
                                                <asp:ImageButton ID="imgFieldPrevious" runat="server" ToolTip="Previous" ImageUrl="~/images/prev.jpeg" />
                                            </div>
                                            <div class="col-md-1 col-2" id="divnext2" runat="server" visible="false">
                                                <asp:ImageButton ID="imgFieldNext" runat="server" ToolTip="Next" ImageUrl="~/images/next.jpeg" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="NewsPaperDiv">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updatePanel4"
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
                                <asp:UpdatePanel ID="updatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <asp:Panel ID="pnlNewsPaper" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>News Paper</h5>
                                                            </div>
                                                        </div>
                                                        <div class="col-12">
                                                            <asp:ListView ID="lvNewspaper" runat="server">
                                                                <EmptyDataTemplate>
                                                                    <center>
                                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                                    </center>
                                                                </EmptyDataTemplate>
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>News Paper Details</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:CheckBox ID="chkAllItems" runat="server" onclick="CheckAllIndents(this)" />News
                                                                                    Paper
                                                                                </th>
                                                                                <th>City
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
                                                                            <asp:CheckBox ID="ChkNewspaper" runat="server" TabIndex="1" AlternateText="Check All Items" ToolTip='<%# Eval("NPNO")%>' /><%# Eval("NPNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("CITY")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnaddNews" runat="server" CssClass="btn btn-primary" TabIndex="2" Text="Add NewsPaper" OnClick="btnaddNews_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Added Newspaper List</h5>
                                                            </div>
                                                        </div>
                                                        <div class="col-12 table-responsive">
                                                            <asp:GridView CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" ID="grdNews" runat="server" AutoGenerateColumns="False"
                                                                Visible="true" OnRowCommand="grdNews_RowCommand" DataKeyNames="NPNO" HorizontalAlign="Left"
                                                                OnRowDeleting="grdNews_RowDeleting">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-CssClass="bg-light-blue">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton CommandName="Delete" ID="btndel" runat="server" ToolTip='<%#Eval("NPNO") %>'
                                                                                CommandArgument='<%#Eval("NPNO") %>' OnClientClick="return Cinfirm(this)" Text="Delete"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="NPNAME" HeaderText="Name" ReadOnly="True" HeaderStyle-CssClass="bg-light-blue">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="CITY" HeaderText="CITY" SortExpression="FNAME" HeaderStyle-CssClass="bg-light-blue">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="col-md-1 col-2" id="divprev3" runat="server" visible="false">
                                                <asp:ImageButton ID="imgNewsPaperPrevious" runat="server" ToolTip="Previous" ImageUrl="~/images/prev.jpeg" />
                                            </div>
                                            <div class="col-md-1 col-2" id="divnext3" runat="server" visible="false">
                                                <asp:ImageButton ID="imgNewsPaperNext" runat="server" ToolTip="Next" ImageUrl="~/images/next.jpeg" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="TenderDiv">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updatePanel6"
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

                                <asp:UpdatePanel ID="updatePanel6" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <asp:Panel ID="Panel8" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Enter Tender Details</h5>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Tender Number </label>
                                                            </div>
                                                            <asp:TextBox ID="txtTenderNumber" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Enter Tender No" MaxLength="50" Enabled="False"></asp:TextBox>
                                                            <asp:DropDownList ID="ddltender" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddltender_SelectedIndexChanged" Width="240px">
                                                                <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvtxtTenderNumber" runat="server" ControlToValidate="txtTenderNumber"
                                                                ErrorMessage="Please Enter Tender Number" Display="None" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Reference Number </label>
                                                            </div>
                                                            <asp:TextBox ID="txtReferenceNumber" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Enter Reference Number" MaxLength="50"
                                                                Enabled="False"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtReferenceNumber" runat="server" ControlToValidate="txtReferenceNumber"
                                                                ErrorMessage="Please Enter Tender Reference Number" Display="None" ValidationGroup="Store"></asp:RequiredFieldValidator>
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
                                                                <asp:TextBox ID="txtSendingDate" runat="server" CssClass="form-control" ToolTip="Enter Sending Date" TabIndex="3"></asp:TextBox>
                                                                <%--<div class="input-group-addon">
                                                                    <asp:Image ID="imgSendingDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>--%>
                                                                <asp:RequiredFieldValidator ID="rfvtxtSendingDate" runat="server" ControlToValidate="txtSendingDate"
                                                                    ErrorMessage="Please Enter Sending Date" Display="None" ValidationGroup="Store"></asp:RequiredFieldValidator>

                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtSendingDate"
                                                                    Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True">
                                                                </ajaxToolKit:MaskedEditExtender>

                                                                <ajaxToolKit:CalendarExtender
                                                                    ID="ceSendingDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgSendingDate"
                                                                    TargetControlID="txtSendingDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Subject </label>
                                                            </div>
                                                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" TabIndex="17" ToolTip="Enter Subject" MaxLength="135"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtSubject" runat="server" ControlToValidate="txtSubject"
                                                                ErrorMessage="Please Enter Subject" Display="None" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtLastDateForSubmission"
                                                                ControlToCompare="txtSendingDate" Type="Date" Operator="GreaterThan" runat="server"
                                                                ErrorMessage="Last Date For Submission should be grater than Tendor Sending Date"
                                                                Display="None" ValidationGroup="Store"></asp:CompareValidator>
                                                            <asp:CompareValidator ID="CompareValidator2" ControlToValidate="txtLasteDateForSaleTime"
                                                                ControlToCompare="txtLastDateForSubmission" Type="Date" Operator="GreaterThan"
                                                                runat="server" ErrorMessage="Technical Bid Opening Date should be grater than Last Date For Submission"
                                                                Display="None" ValidationGroup="Store"></asp:CompareValidator>
                                                            <asp:CompareValidator ID="CompareValidator3" ControlToValidate="txtTendorOpeningDate"
                                                                ControlToCompare="txtLasteDateForSaleTime" Type="Date" Operator="GreaterThan"
                                                                runat="server" ErrorMessage="Financial Bid Opening Date  should be grater than Technical Bid Opening Date"
                                                                Display="None" ValidationGroup="Store"></asp:CompareValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Last Date For Submission </label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i id="imgLastDateForSubmission" class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtLastDateForSubmission" runat="server" TabIndex="4" CssClass="form-control" ToolTip="Enter Last Date For Submission & Time"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvtxtLastDateForSubmission" runat="server" ControlToValidate="txtLastDateForSubmission"
                                                                    ErrorMessage="Please Enter Last Date For Submission" Display="None" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                                                <ajaxToolKit:CalendarExtender ID="ceLastDateForSubmission"
                                                                    runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgLastDateForSubmission"
                                                                    TargetControlID="txtLastDateForSubmission">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtLastDateForSubmission"
                                                                    Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Submission Time :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtLastDateForSubmissionTime" runat="server" TabIndex="5" CssClass="form-control" ToolTip="Tip: Type 'A' or 'P' to switch AM/PM"
                                                                ValidationGroup="Store" />
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtLastDateForSubmissionTime"
                                                                Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" AutoComplete="False" />

                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender3" ControlToValidate="txtLastDateForSubmissionTime"
                                                                IsValidEmpty="false" ErrorMessage="Submission Time Is Invalid [Enter HH:MM (AM/PM) Format]" EmptyValueMessage="Please Enter Submission Time"
                                                                InvalidValueMessage="Submission Time Is Invalid [Enter HH:MM (AM/PM) Format]" Display="None" SetFocusOnError="true"
                                                                Text="*" ValidationGroup="Store" ViewStateMode="Enabled"></ajaxToolKit:MaskedEditValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastDateForSubmissionTime"
                                                                ErrorMessage="Please Enter Submission Time" Display="None" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Technical Bid Opening Date </label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i id="ImaCalStartDate" class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtLasteDateForSaleTime" runat="server" TabIndex="6" ToolTip="Enter Technical Bid Opening Date" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                    ControlToValidate="txtLasteDateForSaleTime" ErrorMessage="Please Enter Technical Bid Opening Date"
                                                                    Display="None" ValidationGroup="Store"></asp:RequiredFieldValidator>

                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtLasteDateForSaleTime"
                                                                    Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True">
                                                                </ajaxToolKit:MaskedEditExtender>


                                                                <%--<div class="input-group-addon">
                                                                    <asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>--%>

                                                                <ajaxToolKit:CalendarExtender ID="ceLasteDateForSaleTime" runat="server" Enabled="True"
                                                                    Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtLasteDateForSaleTime">
                                                                </ajaxToolKit:CalendarExtender>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">

                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Technical Bid Opening Time :</label>
                                                            </div>

                                                            <asp:TextBox ID="txtLasteDateTime" runat="server" CssClass="form-control" ToolTip="Tip: Type 'A' or 'P' to switch AM/PM" TabIndex="7"
                                                                ValidationGroup="Store" />
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtLasteDateTime"
                                                                Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" AutoComplete="False" />

                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtLasteDateTime"
                                                                IsValidEmpty="false" ErrorMessage="Technical Bid Opening Time Is Invalid [Enter HH:MM (AM/PM) Format]" EmptyValueMessage="Please Enter Technical Bid Opening Time"
                                                                InvalidValueMessage="Technical Bid Opening Time Is Invalid [Enter HH:MM (AM/PM) Format]" Display="None" SetFocusOnError="true"
                                                                Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvtxtLasteDateTime" runat="server" ControlToValidate="txtLasteDateTime"
                                                                ErrorMessage="Please Enter Technical Bid Opening Time" Display="None" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divbudgethead" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Budget Head </label>
                                                            </div>
                                                            <asp:DropDownList runat="server" ID="ddlBudgetHead" TabIndex="10" ToolTip="Select Budget Head" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Performance Security  </label>
                                                            </div>
                                                            <%-- <div class="input-group">--%>
                                                            <asp:TextBox ID="txtPerSecurity" runat="server" Text='0' CssClass="form-control" TabIndex="12" ToolTip="Enter Performance Security"
                                                                MaxLength="5" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                            <%--<div class="input-group-addon"><b>%</b></div>--%>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvtxtPSofRs" runat="server" ControlToValidate="txtPerSecurity"
                                                                    ErrorMessage="Please Enter Performance Security Amount" Display="None" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtPerSecurity"
                                                                Type="Integer" MinimumValue="0" MaximumValue="999999999" ErrorMessage="Please Enter amount in the range 0-999999999"
                                                                Display="None" ValidationGroup="store" SetFocusOnError="True"></asp:RangeValidator>
                                                            <%-- </div>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>

                                                            </div>
                                                            <asp:TextBox ID="txtTotalAmt" runat="server" Text="0" CssClass="form-control" TabIndex="14" MaxLength="10"
                                                                onKeyUp="return validateNumeric(this)" Visible="False"></asp:TextBox>
                                                        </div>


                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Financial Bid Opening Date </label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i id="imgTendorOpeningDate" class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtTendorOpeningDate" runat="server" TabIndex="8" ToolTip="Enter Financial Bid Opening Date" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvtxtTendorOpeningDate" runat="server" ControlToValidate="txtTendorOpeningDate"
                                                                    ErrorMessage="Please Enter Financial Bid Opening Date" Display="None" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                                                <%--<div class="input-group-addon">
                                                                    <asp:Image ID="imgTendorOpeningDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                        Style="cursor: pointer" />
                                                                </div>--%>

                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="txtTendorOpeningDate"
                                                                    Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True">
                                                                </ajaxToolKit:MaskedEditExtender>

                                                                <ajaxToolKit:CalendarExtender ID="ceTendorOpeningDate"
                                                                    runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgTendorOpeningDate"
                                                                    TargetControlID="txtTendorOpeningDate">
                                                                </ajaxToolKit:CalendarExtender>


                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Financial Bid Opening Time :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtTendorOpeningDateTime" runat="server" TabIndex="9" CssClass="form-control" ToolTip="Tip: Type 'A' or 'P' to switch AM/PM" ValidationGroup="Store" />
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtTendorOpeningDateTime"
                                                                Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" AutoComplete="False" />

                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txtTendorOpeningDateTime"
                                                                IsValidEmpty="false" ErrorMessage="Financial Bid Opening Time Is Invalid [Enter HH:MM (AM/PM) Format]" EmptyValueMessage="Please Enter Financial Bid Opening Time"
                                                                InvalidValueMessage="Financial Bid Opening Time Is Invalid [Enter HH:MM (AM/PM) Format]" Display="None" SetFocusOnError="true"
                                                                Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>EMD </label>
                                                            </div>
                                                            <%-- <div class="input-group">--%>
                                                            <asp:TextBox ID="txtEMDofRs" runat="server" TabIndex="11" CssClass="form-control"
                                                                MaxLength="5" onKeyUp="return validateNumeric(this)" Text='0'></asp:TextBox>
                                                            <%-- <div class="input-group-addon">%</div>--%>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvtxtEMDofRs" runat="server" ControlToValidate="txtEMDofRs"
                                                                    ErrorMessage="Please Enter Emd" Display="None" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtEMDofRs"
                                                                Type="Integer" MinimumValue="0" MaximumValue="999999999" ErrorMessage="Please Enter amount in the range 0-999999999"
                                                                Display="None" ValidationGroup="store" SetFocusOnError="True"></asp:RangeValidator>
                                                            <%-- </div>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Sales Tax </label>
                                                            </div>
                                                            <%--  <div class="input-group">--%>
                                                            <asp:TextBox ID="txtSalesTax" runat="server" CssClass="form-control" TabIndex="13" ToolTip="Enter Sales Tax"
                                                                MaxLength="5" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                            <%--  <div class="input-group-addon">%</div>--%>
                                                            <%--  <asp:RequiredFieldValidator ID="rfvtxtSalesTax" runat="server" ControlToValidate="txtSalesTax"
                                                                    ErrorMessage="Please Enter Sale Tex" Display="None" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                                            <%-- </div>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Tender Fee</label>
                                                            </div>
                                                            <asp:TextBox ID="txtTenderAmt" runat="server" CssClass="form-control" TabIndex="15"
                                                                MaxLength="10" onKeyUp="return validateNumeric(this)"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfvTenderfee" runat="server" ControlToValidate="txtTenderAmt"
                                                                ErrorMessage="Please Enter Tender Fee" Display="None" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Technical Specification </label>
                                                            </div>
                                                            <asp:TextBox ID="txtSpecification" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                            <div class="label-dynamic">
                                                                <label>Upload Specification Files </label>
                                                            </div>
                                                            <asp:FileUpload ID="FileUpload3" runat="server" TabIndex="16" ToolTip="Specification" />
                                                        </div>


                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="col-12 btn-footer">
                                                <asp:ValidationSummary ID="validationsummary1" runat="server" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="Store" />
                                                <asp:Button ID="butSaveTender" runat="server" Text="Save Tender" OnClick="butSaveTender_Click"
                                                    ValidationGroup="Store" TabIndex="18" ToolTip="Click To Save" CssClass="btn btn-primary" OnClientClick="return Validate();" />
                                                <%--OnClientClick="return Validate();"--%>
                                                <asp:Button runat="server" ID="btnModify" Text="Modify" OnClick="btnModify_Click" ToolTip="Click To Modify" TabIndex="19" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" ToolTip="Click To Cancel" TabIndex="20" CssClass="btn btn-warning" />
                                                <asp:HiddenField ID="hdnAction" runat="server" Value="0" />
                                            </div>
                                            <div class="pull-left d-none">
                                                <asp:ImageButton ID="imgPreviousTender" runat="server" ToolTip="Previous" ImageUrl="~/images/prev.jpeg"
                                                    Style="width: 32px" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="TenderReportDiv">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="updatePanel5"
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
                                <asp:UpdatePanel ID="updatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <asp:Panel ID="Panel9" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Tender Ref No.</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Select Tender To Display The Report</label>
                                                            </div>
                                                            <asp:ListBox ID="lstTender" runat="server" AutoPostBack="True" CssClass="form-control multi-select-demo"
                                                                OnSelectedIndexChanged="lstTender_SelectedIndexChanged"></asp:ListBox>

                                                            <asp:RequiredFieldValidator ID="rfvTenderReport" ControlToValidate="lstTender" InitialValue=""
                                                                runat="server" ErrorMessage="Please select Tender Reference Number" ValidationGroup="TenderReport"
                                                                Display="None"></asp:RequiredFieldValidator>
                                                            <asp:ValidationSummary ID="vsreport" runat="server" ValidationGroup="TenderReport"
                                                                ShowMessageBox="True" ShowSummary="False" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnRpt" runat="server" ValidationGroup="TenderReport" Text="Show Report"
                                                    OnClick="btnRpt_Click" CssClass="btn btn-info" ToolTip="Click To Show Report" />
                                                <asp:Button ID="btnLtdTender" runat="server" ValidationGroup="TenderReport" Visible="false"
                                                    Text="Limited Tender Notice" OnClick="btnLtdTender_Click" CssClass="btn btn-info" ToolTip="Click To Show Limited Tender Notice" />

                                                <asp:Button ID="btnTendeNews" runat="server" Visible="false" ValidationGroup="TenderReport" Text=" Single Bid Report"
                                                    OnClick="btnTendeNews_Click" CssClass="btn btn-info" ToolTip="Click To Show Single Bid Report" />

                                                <asp:Button ID="btnTenderNotice" runat="server" ValidationGroup="TenderReport" Text=" Tender Notice"
                                                    Visible="false" OnClick="btnTenderNotice_Click" CssClass="btn btn-info" ToolTip="Click To Show Tender Notice" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>

    </div>


    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script type="text/javascript" language="javascript">

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '0';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;

            }
            //if (txt.value > 100) {


            //    txt.value = '0';
            //    txt.focus = true;
            //    alert("Enter Value between 0-100");
            //    return false;

            //}
            //else
            //    return true;
        }

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



        function CheckAllIndents(chkIndents) {
            var frm = document.forms[0];
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkIndents.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }



        function Cinfirm(txt) {


            var result = confirm("Do you want to delete Record?");
            if (result == true) {
                return true;
            }
            else {
                return false;
            }
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

        //Added by Gopal Anthati on 03/06/2022 to avoid post back issues while modifying tender
        function Validate() {
            debugger;
            var Action = document.getElementById('<%=hdnAction.ClientID%>').value;

            if (Action == 1) {
                var TenderNo = document.getElementById('<%=ddltender.ClientID%>').value;
                if (TenderNo == 0) {
                    alert('Please Select Tender Number');
                    return false;
                }
            }
            else {
                if (document.getElementById('<%=txtTenderNumber.ClientID%>').value == '') {
                    alert('Please Enter Tender Number');
                    return false;
                }
            }

            if (document.getElementById('<%=txtSendingDate.ClientID%>').value == '') {
                alert('Please Enter Sending Date');
                return false;
            }
            else {
                if (ValidateDate(document.getElementById('<%=txtSendingDate.ClientID%>').value, "Sending Date") == false) return false;
            }

            if (document.getElementById('<%=txtSubject.ClientID%>').value == '') {
                alert('Please Enter Subject');
                return false;
            }
            if (document.getElementById('<%=txtLastDateForSubmission.ClientID%>').value == '') {
                alert('Please Enter Last Date For Submission');
                return false;
            }
            else {
                if (ValidateDate(document.getElementById('<%=txtLastDateForSubmission.ClientID%>').value, "Last Date For Submission") == false) return false;
            }
            if (document.getElementById('<%=txtLastDateForSubmissionTime.ClientID%>').value == '') {
                alert('Please Enter Submission Time');
                return false;
            }
            else {
                if (ValidateTime(document.getElementById('<%=txtLastDateForSubmissionTime.ClientID%>').value, "Submission Time") == false) return false;
            }
            if (document.getElementById('<%=txtLasteDateForSaleTime.ClientID%>').value == '') {
                alert('Please Enter Technical Bid Opening Date');
                return false;
            }
            else {
                if (ValidateDate(document.getElementById('<%=txtLasteDateForSaleTime.ClientID%>').value, "Technical Bid Opening Date") == false) return false;
            }
            if (document.getElementById('<%=txtLasteDateTime.ClientID%>').value == '') {
                alert('Please Enter Technical Bid Opening Time');
                return false;
            }
            else {
                if (ValidateTime(document.getElementById('<%=txtLasteDateTime.ClientID%>').value, "Technical Bid Opening Time") == false) return false;
            }
            if (document.getElementById('<%=txtTendorOpeningDate.ClientID%>').value == '') {
                alert('Please Enter Financial Bid Opening Date');
                return false;
            }
            else {
                if (ValidateDate(document.getElementById('<%=txtTendorOpeningDate.ClientID%>').value, "Financial Bid Opening Date") == false) return false;
            }
            if (document.getElementById('<%=txtTendorOpeningDateTime.ClientID%>').value == '') {
                alert('Please Enter Financial Bid Opening Time');
                return false;
            }
            else {
                if (ValidateTime(document.getElementById('<%=txtTendorOpeningDateTime.ClientID%>').value, "Financial Bid Opening Time") == false) return false;
            }
            if (document.getElementById('<%=txtTenderAmt.ClientID%>').value == '') {
                alert('Please Enter Tender Fee');
                return false;
            }

            if (document.getElementById('<%=txtLastDateForSubmission.ClientID%>').value < document.getElementById('<%=txtSendingDate.ClientID%>').value) {
                alert('Last Date For Submission Should Be Greater Than Or Equal To Sending Date.');
                return false;
            }
            if (document.getElementById('<%=txtLastDateForSubmission.ClientID%>').value > document.getElementById('<%=txtLasteDateForSaleTime.ClientID%>').value) {
                alert('Technical Bid Opening Date Should Be Greater Than Or Equal To Last Date For Submission.');
                return false;
            }
            if (document.getElementById('<%=txtLasteDateForSaleTime.ClientID%>').value > document.getElementById('<%=txtTendorOpeningDate.ClientID%>').value) {
                alert('Financial Bid Opening Date Should Be Greater Than Or Equal To Technical Bid Opening Date.');
                return false;
            }

        }

        function ValidateTime(InputTime, InputText) {
            var Hour = InputTime.split(":");
            var Minute = Hour[1].split(" ");

            if ((Hour[0] <= 12 && Hour[0] >= 0) && (Minute[0] <= 59 && Minute[0] >= 0)) {

            } else {
                alert(InputText + ' Is Invalid [Enter HH:MM (AM/PM) Format]');
                return false;
            }
        }

        function ValidateDate(InputDate, InputText) {
            var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
            if (!(date_regex.test(InputDate))) {
                alert(InputText + " Is Invalid (Enter In [dd/MM/yyyy] Format).");
                return false;
            }
        }

        //End
    </script>


    <div id="divMsg" runat="server">
    </div>
</asp:Content>
