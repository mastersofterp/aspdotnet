<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_QuotORTnderLock.aspx.cs" Inherits="Stores_Transactions_Quotation_Str_QuotORTnderLock"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    &nbsp;<%--    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">TENDER LOCK </h3>
                </div>
                <div id="tabs" role="tabpanel">

                    <ul class="nav nav-tabs">
                       <%-- <li class="active"><a href="#Div1" data-toggle="tab" aria-expanded="true">Quotation List</a></li>--%>
                      <%--  <li class=""><a href="#Div3" data-toggle="tab" aria-expanded="TRUE"  runat="server" >Tender List</a></li>--%>

                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="Div1">
                             <asp:UpdatePanel ID="updatepanel1" runat="server">
                                <ContentTemplate>

                            <%--<div class="box-body">
                                <div class="col-md-12">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Quotation List</div>
                                        <div class="panel-body">
                                            <asp:Panel ID="pnl" runat="server">
                                                <div class="form-group col-md-12" id="QuotList">
                                                    <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlLockUnlock" runat="server" CssClass="form-control" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlLockUnlock_SelectedIndexChanged" TabIndex="1" Visible="false">
                                                            <asp:ListItem>All</asp:ListItem>
                                                            <asp:ListItem>Locked</asp:ListItem>
                                                            <asp:ListItem>UnLocked</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:Panel ID="pnlReqIndent" runat="server">
                                                            <div class="table-responsive">
                                                                <asp:GridView CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" ID="grdQuotList" runat="server" AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-CssClass="bg-light-blue">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" ID="chkqno" ToolTip='<%#Eval("QNO") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Quotation Number" HeaderStyle-CssClass="bg-light-blue" DataField="QUOTNO">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Reference Number" HeaderStyle-CssClass="bg-light-blue" DataField="REFNO">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Indent Number" HeaderStyle-CssClass="bg-light-blue" DataField="INDNO">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Open Date" HeaderStyle-CssClass="bg-light-blue" DataField="ODATE"
                                                                            DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Last Date" HeaderStyle-CssClass="bg-light-blue" DataField="LDATE"
                                                                            DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Is Lock?" HeaderStyle-CssClass="bg-light-blue" DataField="ISLOCK">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <RowStyle />
                                                                </asp:GridView>
                                                            </div>
                                                            <br />   
                                                        </asp:Panel>
                                                    </div>
                                                    <div class="col-md-12 text-center">

                                                                <asp:Button ID="btnLock" runat="server" CssClass="btn btn-info" Text="Lock" OnClick="btnLock_Click" />
                                                                <asp:Button ID="btnUnLock" runat="server" CssClass="btn btn-success" Text="UnLock" OnClick="btnUnLock_Click"  Visible="false"/>
                                                            </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>

                            </div>--%>

                             </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <%--<div class="tab-pane" id="Div3">--%>
                            <asp:UpdatePanel ID="updatepanel2" runat="server">
                                <ContentTemplate>

                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Tender List</div>
                                        <div class="panel-body">
                                            <asp:Panel ID="Panel1" runat="server" Visible="true">
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-12" id="TenderList">
                                                        <div class="col-md-5">
                                                            <asp:DropDownList ID="ddlLockUnlockTender" AppendDataBoundItems="True"
                                                                runat="server" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddlLockUnlockTender_SelectedIndexChanged"
                                                                AutoPostBack="True" Visible="False">
                                                                <asp:ListItem>All</asp:ListItem>
                                                                <asp:ListItem>Locked</asp:ListItem>
                                                                <asp:ListItem>UnLocked</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-md-12">
                                                        <asp:Panel ID="pnlitems" runat="server">

                                                            <div class="table-responsive">
                                                                <asp:GridView CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" ID="grdTenderList" runat="server"
                                                                    AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-CssClass="bg-light-blue">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" ID="chkqno" ToolTip='<%#Eval("TNO") %>' ItemStyle-HorizontalAlign="Left" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Tender Number" HeaderStyle-CssClass="bg-light-blue" DataField="TENDERNO">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Reference Number" HeaderStyle-CssClass="bg-light-blue" DataField="TDRNO">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Indent Number" HeaderStyle-CssClass="bg-light-blue" DataField="INDNO">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Financial Bid Opening Date" HeaderStyle-CssClass="bg-light-blue" DataField="TDODATE"
                                                                            DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Technical Bid Opening Date" HeaderStyle-CssClass="bg-light-blue" DataField="LDATESALE"
                                                                            DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Last Date For Submission" HeaderStyle-CssClass="bg-light-blue" DataField="SUBMITDATE"
                                                                            DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Is Lock?" DataField="ISLOCK" HeaderStyle-CssClass="bg-light-blue">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <RowStyle />
                                                                </asp:GridView>
                                                            </div>


                                                        </asp:Panel>
                                                        <br />
                                                        <div class="col-md-12 text-center">
                                                            <asp:Button ID="btnTLock" runat="server" CssClass="btn btn-info" Text="Lock" OnClick="btnTLock_Click" />
                                                            <asp:Button ID="btnTUnLock" runat="server" CssClass="btn btn-success" Text="UnLock" OnClick="btnTUnLock_Click" Visible="false" />
                                                            <asp:HiddenField ID="hidLastTab" Value="0" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>

                            </div>

                             </ContentTemplate>
                               
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
            </div>

            <div class="col-md-12">
            </div>
        </div>
    </div>


    


    <%--<script type="text/javascript">
       
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#tabs').tabs({
                        activate: function () {
                            var newIdx = $('#tabs').tabs('option', 'active');
                            $('#<%=hidLastTab.ClientID%>').val(newIdx);
                        }, heightStyle: "auto",
                        active: $('#<%=hidLastTab.ClientID%>').val(),
                        show: { effect: "fadeIn", duration: 1000 }
                    });
                }
            });
        };


    </script>--%>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $('#tabs').tabs({
                activate: function () {
                    var newIdx = $('#tabs').tabs('option', 'active');
                    $('#<%=hidLastTab.ClientID%>').val(newIdx);
                }, heightStyle: "auto",
                active: $('#<%=hidLastTab.ClientID%>').val(),
                show: { effect: "fadeIn", duration: 1000 }
            });
        });
    </script>--%>
   <%-- <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
</asp:Content>
