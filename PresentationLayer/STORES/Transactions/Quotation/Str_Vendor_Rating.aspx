<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_Vendor_Rating.aspx.cs" Inherits="STORES_Transactions_Quotation_Str_Vendor_Rating" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VENDOR RATING</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Vendor Rating</div>
                                        <div class="panel-body">

                                            <div class="form-group col-md-2">
                                                <label>Vendor Category</label>
                                                <span style="color: red">*</span>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <asp:DropDownList ID="ddlCategory" runat="server" OnSelectedIndexChanged="ddlRemark_SelectedIndexChanged" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                           <%-- <div class="col-md-2">
                                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-info" />
                                            </div>--%>

                                            <asp:Panel ID="pnlItemMaster" runat="server" Visible="false">
                                                <div class="col-md-12 table-responsive">
                                                    <asp:ListView ID="lvParty" runat="server" OnItemDataBound="lvParty_ItemDataBound">
                                                        <EmptyDataTemplate>
                                                            <br />
                                                            <div class="text-center">
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div id="demo-grid" class="vista-grid">
                                                                <div class="titlebar">
                                                                    <h4>Vendor List</h4>
                                                                </div>
                                                                <table class="table table-bordered table-hover table-responsive">
                                                                    <tr class="bg-light-blue">
                                                                        <th>Action
                                                                        </th>
                                                                        <th style="width: 60%">Vendors
                                                                        </th>
                                                                        <th style="width: 30%">Rating
                                                                        </th>

                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="ckhAll" runat="server" ToolTip='<%# Eval("PNO") %>' />

                                                                </td>
                                                                <td style="width: 60%">
                                                                    <%--<%# Eval("PNAME")%>--%>
                                                                    <asp:Label ID="lblPname" runat="server" Text='<%# Eval("PNAME")%>'></asp:Label>
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:DropDownList ID="ddlRemark" runat="server" CssClass="form-control" >
                                                                        <asp:ListItem Text="0">Please Select</asp:ListItem>
                                                                        <asp:ListItem Text="1">Excellent</asp:ListItem>
                                                                        <asp:ListItem Text="2">Very Good</asp:ListItem>
                                                                        <asp:ListItem Text="3">Good</asp:ListItem>
                                                                        <asp:ListItem Text="4">Poor</asp:ListItem>
                                                                        <asp:ListItem Text="5">Black Listed</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>

                                                </div>
                                                <div class="form-group col-md-12 text-center">
                                                    <br />
                                                    <asp:Button ID="bntSubmit" runat="server" Text="Submit" ValidationGroup="store" OnClick="bntSubmit_Click"
                                                        CssClass="btn btn-primary" ToolTip="Click To Submit" TabIndex="12" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="13"
                                                        ToolTip="Click To Reset" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                                    <asp:Button ID="btnshowrpt" runat="server" OnClick="btnshowrpt_Click" Text="Report" CssClass="btn btn-info" ToolTip="Click To Show Report" TabIndex="14" />
                                                </div>
                                            </asp:Panel>

                                        </div>

                                    </div>
                                </asp:Panel>


                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <table cellpadding="0" cellspacing="0" width="100%">

                <tr>
                    <td>
                        <!-- "Wire frame" div used to transition from the button to the info panel -->
                        <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                        </div>
                        <!-- Info panel to be displayed as a flyout when the button is clicked -->
                        <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                            <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                                <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                    ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                            </div>
                            <div>
                                <p class="page_help_head">
                                    <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                    <asp:Image ID="imgSReason" runat="server" ImageUrl="~/images/action_down.gif" AlternateText="Show Reason" />
                                    Show Reason
                            <asp:Image ID="imgHReason" runat="server" ImageUrl="~/images/action_up.gif" AlternateText="Hide Reason" />
                                    Hide Reason
                                </p>
                                <p class="page_help_text">
                                    <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                                </p>
                            </div>
                        </div>

                        <script type="text/javascript" language="javascript">
                            // Move an element directly on top of another element (and optionally
                            // make it the same size)
                            function Cover(bottom, top, ignoreSize) {
                                var location = Sys.UI.DomElement.getLocation(bottom);
                                top.style.position = 'absolute';
                                top.style.top = location.y + 'px';
                                top.style.left = location.x + 'px';
                                if (!ignoreSize) {
                                    top.style.height = bottom.offsetHeight + 'px';
                                    top.style.width = bottom.offsetWidth + 'px';
                                }
                            }
                        </script>

                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 10px">
                        <br />

                        <table cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                    </td>
                    <td class="form_left_label"></td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
</asp:Content>

