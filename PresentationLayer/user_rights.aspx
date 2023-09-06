<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="user_rights.aspx.cs" Inherits="user_rights" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(function () {
            $("[id*=trvLinks] input[type=checkbox]").bind("click", function () {
                var table = $(this).closest("table");
                if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                    //Is Parent CheckBox
                    var childDiv = table.next();
                    var isChecked = $(this).is(":checked");
                    $("input[type=checkbox]", childDiv).each(function () {
                        if (isChecked) {

                            $(this).attr("checked", "checked");
                        } else {

                            $(this).removeAttr("checked");
                        }
                    });
                } else {
                    //Is Child CheckBox
                    var parentDIV = $(this).closest("DIV");
                    if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {



                        $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                    } else {

                        ////                   
                        if (($("input[type=checkbox]:checked", parentDIV).length == 0)) {

                            $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                        }
                        else {
                            $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                        }
                    }
                }
            });
        });
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div3" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">PRE-DEFINED USER RIGHTS MANAGEMENT</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>User Description</label>
                                    </div>
                                    <asp:TextBox ID="txtUserDesc" runat="server" CssClass="form-control" MaxLength="50" TabIndex="1" />
                                    <asp:RequiredFieldValidator ID="rfvUserDescription" runat="server" ControlToValidate="txtUserDesc"
                                        Display="None" ErrorMessage="User Description Required" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Pre-Defned User Rights</label>
                                    </div>
                                    <asp:Panel ID="pnlTree" runat="server" Height="250px" ScrollBars="Both" BorderStyle="Solid"
                                        BorderWidth="1px" BorderColor="Gray" TabIndex="2">
                                        <asp:TreeView ID="trvLinks" runat="server" ExpandDepth="0">
                                        </asp:TreeView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                ValidationGroup="submit" CssClass="btn btn-primary" TabIndex="3" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False"
                                OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="4" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                        <div class="col-12">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlList" runat="server">
                        <div class="col-12 text-center">
                            <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" CssClass="btn btn-primary">Add New</asp:LinkButton>
                        </div>
                        <div class="col-12">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:ListView ID="lvUserTypes" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Pre-Defned User Rights</h5>
                                                </div>
                                                <table class="table table-hover table-striped table-bordered display" id="divpredefuserlist" style="width: 100%;">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="width: 20%; text-align: center;">Action
                                                            </th>
                                                            <th>User Type
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
                                                <td style="width: 20%; text-align: center;">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("USERTYPEID") %>'
                                                        AlternateText='<%# Eval("USERDESC") %>' ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                </td>
                                                <td>
                                                    <%# Eval("USERDESC")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <%--<div class="vista-grid_datapager">
                                            <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvUserTypes" PageSize="20"
                                                OnPreRender="dpPager_PreRender">
                                                <Fields>
                                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                                </Fields>
                                            </asp:DataPager>
                                        </div>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
                    <div id="div2" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#divpredefuserlist').DataTable({

            });
        }
    </script>--%>
</asp:Content>
