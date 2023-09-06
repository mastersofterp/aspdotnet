<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ConfigurationSetting.aspx.cs" Inherits="ConfigurationSetting" Title="Configuration Setting Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript">
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


        function ShowVoucherPrint() {
            debugger;
            var a = document.getElementById('ctl00_ContentPlaceHolder1_GridData_ctl07_chkCheck').checked;

            if (document.getElementById('ctl00_ContentPlaceHolder1_GridData_ctl07_chkCheck').checked) {

                var popUrl = 'ConfigurationForVoucherPrint.aspx';
                //var popUrl = 'CFVoucherPrint.aspx';
                var name = 'popUp';
                var appearence = 'dependent=yes,menubar=no,resizable=no,' +
                                'status=no,toolbar=no,titlebar=no,' +
                                'left=50,top=35,width=950px,height=650px';
                debugger;
                var openWindow = window.open(popUrl, name, appearence);
                debugger;
                openWindow.focus();

                var popUrl = 'ledgerhead.aspx?obj=' + 'AccountingVouchers&pageno=332';
                var name = 'popUp';
                var appearence = 'dependent=yes,menubar=no,resizable=yes,scrollbar=yes' +
             'status=no,toolbar=no,titlebar=no,' +
             'left=50,top=35,width=900px,height=650px';
                var openWindow = window.open(popUrl, name, appearence);
                openWindow.focus();




            }

            return true;
        }
    </script>

    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UPDMainGroup"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UPDMainGroup" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CONFIGURATION SETTING</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12 mb-3">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large;">
                                </div>
                            </div>
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Add/Modify - Configuration</h5>
                                    </div>
                                </div>
                                <div class="text-center">
                                    <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                </div>
                                <div class="col-12">
                                    <div class="table table-responsive">
                                        <asp:Panel ID="gridScroll" runat="server">
                                            <asp:GridView ID="GridData" runat="server" Width="100%" AutoGenerateColumns="False"
                                                CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                CssClass="table table-striped table-bordered nowrap"
                                                OnSelectedIndexChanged="GridData_SelectedIndexChanged">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:BoundField HeaderText="&nbsp;Configuration Description" DataField="CONFIGDESC" />
                                                    <asp:TemplateField ItemStyle-Width="130px" FooterStyle-Width="130px" ControlStyle-Width="130px" HeaderStyle-Width="130px">
                                                        <HeaderTemplate>Check-Y/Uncheck-N</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkCheck" runat="server" AutoPostBack="False" />
                                                            <asp:HiddenField ID="hdnid" runat="server" Value='<%# Eval("CONFIGID") %>' />
                                                            <asp:HiddenField ID="hdndesc" runat="server" Value='<%# Eval("CONFIGDESC") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="22px" />
                                                        <ItemStyle Width="22px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle Font-Bold="True" ForeColor="black" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                                        Text="Submit" ValidationGroup="submit" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnBack" runat="server" CausesValidation="false"
                                        OnClick="btnBack_Click" Text="Back" CssClass="btn btn-primary" Visible="false" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false"
                                        OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" />

                                </div>


                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
