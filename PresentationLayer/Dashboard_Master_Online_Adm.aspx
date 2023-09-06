<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master"
    CodeFile="Dashboard_Master_Online_Adm.aspx.cs" Inherits="Dashboard_Master_Online_Adm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .Tab:focus
        {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upd1"
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

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ONLINE ADMISSION DASHBOARD MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>DashBoard Name</label>
                                        </div>
                                        <asp:TextBox ID="txtdashboardname" AutoComplete="off" runat="server" TabIndex="1" placeholder="Enter DashBoard Name" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvdashboard" runat="server" ControlToValidate="txtdashboardname"
                                            Display="None" ErrorMessage="Please Enter DashBoard Name" SetFocusOnError="True"
                                            ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddladmbatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvsession" runat="server" ControlToValidate="ddladmbatch"
                                            Display="None" ErrorMessage="Please select Admission Batch." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="show" />
                                    </div>

                                    <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status</label>
                                        </div>
                                        <asp:CheckBox ID="chkstatus" TabIndex="3" runat="server" />  
                                    </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="row">
                                            <div class="form-group col-6">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Active Status</label>
                                                </div>
                                                <div class="switch form-inline">
                                                    <input type="checkbox" id="rdActive" name="switch" checked />
                                                    <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="3"  OnClientClick="return validate();" OnClick="btnsubmit_Click" ValidationGroup="show" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="4" OnClick="btncancel_Click" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="show" />
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvdashboard" runat="server" OnItemDataBound="lvdashboard_ItemDataBound">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Dash Board List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divdashboardlist">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th style="text-align: center;">Action
                                                    </th>
                                                    <th>Sr No.
                                                    </th>
                                                    <th>Name
                                                    </th>
                                                    <th>Status
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
                                            <td style="text-align: center;">
                                                <%-- circulamdetails,documentlist--%>
                                                <asp:ImageButton ID="btnEdit" runat="server" class="newAddNew Tab" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("ID") %>'
                                                    AlternateText="Edit Record" TabIndex="5" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%# Container.DataItemIndex + 1%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("NAME")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("STATUS")%>' ToolTip='<%# Eval("BATCHNO")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {

            $('#hfdActive').val($('#rdActive').prop('checked'));

        
            var idtxtweb = $("[id$=txtdashboardname]").attr("id");
        var txtweb = document.getElementById(idtxtweb);
        if (txtweb.value.length == 0) {
            alert('Please Enter DashBoard Name', 'Warning!');
            //$(txtweb).css('border-color', 'red');
            $(txtweb).focus();
            return false;
        }

        var ddlState = $("[id$=ddladmbatch]").attr("id");
        var ddlState = document.getElementById(ddlState);
        if (ddlState.value == 0) {
            alert('Please select Admission Batch', 'Warning!');
            $(ddlState).focus();
            return false;
        }

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnsubmit').click(function () {
                    validate();
                });
            });
        });
    </script>
    <%--<script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#divdashboardlist').DataTable({

            });
        }

    </script>--%>
</asp:Content>

