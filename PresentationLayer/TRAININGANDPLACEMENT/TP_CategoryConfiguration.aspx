<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TP_CategoryConfiguration.aspx.cs" Inherits="TRAININGANDPLACEMENT_TP_CategoryConfiguration" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfCategory" runat="server" ClientIDMode="Static" />
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">TP ACTIVITY CONFIGURATION</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">

                            <%-- <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divColg" >
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                            SetFocusOnError="true" InitialValue="0" Display="None" ErrorMessage="Plese Select College"
                                            ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>--%>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Degree</label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                             
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Activity Start From Semester</label>
                                </div>
                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                </asp:DropDownList>
                              

                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Status</label>
                                </div>
                                <div class="switch form-inline">
                                    <%-- <asp:CheckBox ID="chkStatusCurrency" runat="server" Checked="true" />--%>
                                    <input type="checkbox" id="chkStatusCategory" name="switch" class="switch" checked />
                                    <label data-on="Active" data-off="Inactive" for="chkStatusCategory"></label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <%--   <asp:Button ID="Show" runat="server" Text="Show"  class="btn btn-primary" CausesValidation="false" />--%>

                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary" CausesValidation="false" OnClick="Submit_Click" OnClientClick="return validate();" />

                        <asp:Button ID="Cancel" runat="server" Text="Cancel" class="btn btn-warning" CausesValidation="false" OnClick="Cancel_Click" />
                      
                    </div>


                    <div class="col-12" id="pnllist" runat="server">
                        <div class="sub-heading">
                            <h5>List of Activity</h5>
                        </div>
                        <asp:ListView ID="lvCategoryconf" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <asp:Label ID="lblErr" runat="server" Text="No More Students to approve">
                                </asp:Label>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="vista-grid">

                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Action
                                                </th>
                                                <th>Degree Name
                                                </th>
                                                <th>Activity Start From Semester
                                                </th>
                                                <th class="text-center">Active Status
                                                </th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </div>

                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>

                                        <asp:ImageButton ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview" ImageUrl="~/Images/edit.png" ToolTip='<%# Eval("C_CONF") %>'
                                            CommandArgument='<%# Eval("C_CONF") %>'></asp:ImageButton>
                                    </td>
                                    <td>
                                        <%# Eval("DEGREENAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("SEMFULLNAME")%>
                                    </td>
                                    <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                        <asp:Label ID="lblstatus" CssClass="badge" runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                    </td>

                                </tr>
                            </ItemTemplate>

                        </asp:ListView>
                    </div>


                </div>
            </div>
        </div>

    </div>

    <script>
        function SetStat(val) {
            $('#chkStatusCategory').prop('checked', val);
        }

        function validate() {

            $('#hfCategory').val($('#chkStatusCategory').prop('checked'));

            //var txtCurrency = $("[id$=txtCurrency]").attr("id");
            //var txtCurrency = document.getElementById(txtCurrency);

            //if (txtCurrency.value.length == 0) {
            //    alert('Please Enter Currency ', 'Warning!');
            //    //   $(txtCurrency).css('border-color', 'red');
            //    $(txtCurrency).focus();
            //    return false;
            //}
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

