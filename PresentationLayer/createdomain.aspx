<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="createdomain.aspx.cs" Inherits="domain" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdShow"
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
    <script type="text/javascript" charset="utf-8">
        function IsNumeric(txt) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;
            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Error! Only Numeric Values Are Allowed.")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }
    </script>
    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="UpdShow" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Create Domain</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Domain Name</label>
                                            </div>
                                            <asp:TextBox ID="txtDomainName" runat="server" MaxLength="20" CssClass="form-control" Wrap="False" TabIndex="1" />
                                            <asp:RequiredFieldValidator ID="rfvDomainName" runat="server" ControlToValidate="txtDomainName"
                                                Display="None" ErrorMessage="Domain Name Required" ValidationGroup="domain">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteDomainNamee" runat="server"
                                                TargetControlID="txtDomainName" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="1234567890~!@#$%^&*()+=|\{}[]:;<>,.?/'" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Sequence No</label>
                                            </div>
                                            <asp:TextBox ID="txtSqnNo" runat="server" MaxLength="20" CssClass="form-control" TabIndex="2" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="f1" runat="server" TargetControlID="txtSqnNo"
                                                ValidChars=".0123456789">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblactive" runat="server" Font-Bold="true">Status</asp:Label>
                                            </div>
                                            <div class="switch form-inline">
                                                <%--input type="checkbox" id="rdActive" name="switch" checked />
                                                <label data-on="Active" data-off="Inactive" class="newAddNew Tab" for="rdActive"></label>--%>
                                                <input type="checkbox" id="rdActive" name="switch" checked />
                                                <label data-on="Active" tabindex="7" class="newAddNew Tab" data-off="Inactive" for="rdActive"></label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="3"
                                        ValidationGroup="domain" CssClass="btn btn-primary" OnClientClick="return validate();" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="4"
                                        CausesValidation="False" CssClass="btn btn-warning" />
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlList" runat="server">
                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" CssClass="btn btn-primary">Add New</asp:LinkButton>
                                </div>
                                <div class="col-12">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <asp:Repeater ID="lvDomain" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action</th>
                                                        <th>Domain Name</th>
                                                        <th>Sequence No</th>
                                                        <th>Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%--<tr id="itemPlaceholder" runat="server" />--%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("as_no") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("As_Title") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSeqNo" runat="server" Text='<%# Eval("AS_SRNO") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstatus" runat="server" CssClass='<%# Eval("STATUS")%>' Text='<%# Eval("STATUS")%>' ForeColor='<%# Eval("STATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </asp:Panel>
                            <div id="divMsg" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
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

    <script>
        function SetStat(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {

            $('#hfdStat').val($('#rdActive').prop('checked'));

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
    </script>
</asp:Content>
