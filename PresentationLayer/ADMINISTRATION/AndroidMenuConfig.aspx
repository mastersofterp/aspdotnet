<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AndroidMenuConfig.aspx.cs" Inherits="ADMINISTRATION_AndroidMenuConfig" ViewStateEncryptionMode="Always" EnableViewStateMac="true"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>

    <asp:UpdatePanel ID="updpnCheckUpdate" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                        </div>
                        <div>
                            <asp:UpdatePanel ID="updpnlListView" runat="server">
                                <ContentTemplate>
                                    <div class="box-footer" style="text-align: center">
                                        <div class="form-group col-md-12 mt-2" style="margin-top: 10px">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <asp:ListView ID="lvDataDisplay" runat="server" EnableModelValidation="True">
                                                    <LayoutTemplate>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Sr.No</th>
                                                                    <th>Menu List</th>
                                                                    <th>Status</th>
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
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("MENU_NAME")%>'></asp:Label>
                                                                <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("MENU_ID")%>' />
                                                            </td>

                                                            <td>
                                                                <asp:CheckBox ID="chkStatus" runat="server" Checked='<%# Convert.ToBoolean(Eval("IS_ON")) %>' /></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer" style="text-align: center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" TabIndex="1" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" runat="server" TabIndex="2" ToolTip="Cancel" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnCancel" />
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="popup" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="modal" id="myModalPopUp" data-backdrop="static">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-body pl-0 pr-0 pl-lg-2 pr-lg-2">
                                <div class="col-12 mt-3">
                                    <h5 class="heading">Please enter password to access this page.</h5>
                                    <div class="row">
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <%--  <label>PASSWORD</label>--%>
                                            <asp:Label ID="lblPass" runat="server" Text="ybc@123" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtPass" TextMode="Password" runat="server" TabIndex="1" ToolTip="Please Enter Password" AutoComplete="new-password"
                                                MaxLength="50" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="req_password" runat="server" ErrorMessage="Password Required !" ControlToValidate="txtPass"
                                                Display="None" ValidationGroup="password"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                        </div>
                                        <div class="btn form-group col-lg-12 col-md-12 col-12">
                                            <asp:Button ID="btnConnect" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="1" CssClass="btn btn-outline-primary"
                                                runat="server" Text="Submit" OnClick="btnConnect_Click" ValidationGroup="password" />
                                            <asp:Button ID="btnCancel1" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="2" CssClass="btn btn-danger"
                                                runat="server" Text="Cancel" OnClick="btnCancel1_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="false" ValidationGroup="password" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnConnect" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        $(window).on('load', function () {
            $('#myModalPopUp').modal('show');
        });
    </script>
</asp:Content>

