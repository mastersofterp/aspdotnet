<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BatchMaster.aspx.cs" Inherits="Academic_Masters_BatchMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBatch"
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

    <asp:UpdatePanel ID="updBatch" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">BATCH MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Batch Name</label>
                                        </div>
                                        <asp:TextBox ID="txtBatchName" runat="server" TabIndex="1"
                                            MaxLength="50" ToolTip="Please Enter Batch Name" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubjectType" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" ToolTip="Please Select Subject Type">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                            <label data-on="Active" tabindex="7" class="newAddNew Tab" data-off="Inactive" for="rdActive"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit" OnClientClick="return validate();"
                                    CssClass="btn btn-primary" OnClick="btnSave_Click" TabIndex="3" />
                                <asp:Button ID="btnShowReport" runat="server" CausesValidation="False" OnClick="btnShowReport_Click" Visible="false"
                                    TabIndex="4" Text="Report" ToolTip="Show Report" CssClass="btn btn-info" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="5" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-md-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <asp:Repeater ID="lvBatchName" runat="server">
                                            <HeaderTemplate>
                                                <div class="sub-heading">
                                                    <h5>Batch Name List</h5>
                                                </div>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Batch Name
                                                        </th>
                                                        <th>Section
                                                        </th>
                                                        <th>Status
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%--<tr id="itemPlaceholder" runat="server" />--%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("BATCHNO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="6" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("BATCHNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SECTIONNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ACTIVESTATUS")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:Panel>
                            </div>

                        </div>
                        <div id="divMsg" runat="server">
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function SetStat(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {

            $('#hfdStat').val($('#rdActive').prop('checked'));

            var txtBatchName = $("[id$=txtBatchName]").attr("id");
            var txtBatchName = document.getElementById(txtBatchName);
            // alert(txtOwnershipStatusName.value.length)
            if (txtBatchName.value.length == 0) {
                alert('Please Enter Batch Name', 'Warning!');
                $(txtBatchName).focus();
                return false;
            }

            var ddlSubjectType = $("[id$=ddlSubjectType]").attr("id");
            var ddlSubjectType = document.getElementById(ddlSubjectType);
            // alert(txtOwnershipStatusName.value.length)
            if (ddlSubjectType.value == 0) {
                alert('Please Select Section', 'Warning!');
                $(ddlSubjectType).focus();
                return false;
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    validate();
                });
            });
        });
    </script>
</asp:Content>
