<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MISC_Fine_Master.aspx.cs" Inherits="ACADEMIC_ONLINEFEESCOLLECTION_MISC_Fine_Master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBank"
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

    <asp:UpdatePanel ID="updBank" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">MISC MASTER</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Cash Book</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCashBook" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" ValidationGroup="show">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlCashBook"
                                            Display="None" ErrorMessage="Please Select Cash Book" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" TabIndex="2" OnClick="btnShow_Click"
                                    ValidationGroup="show" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="3" OnClick="btnSubmit_Click"
                                    ValidationGroup="submit" />
                                <asp:Button ID="btnClear" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="4" OnClick="btnClear_Click" />
                                <asp:ValidationSummary ID="valshow" runat="server" ValidationGroup="show" ShowMessageBox="true"
                                    DisplayMode="List" ShowSummary="false" />
                                <asp:ValidationSummary ID="valSubmit" runat="server" ValidationGroup="submit" ShowMessageBox="true"
                                    DisplayMode="List" ShowSummary="false" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlMisclst" runat="server">
                                    <asp:ListView ID="lvmiscCollection" runat="server">
                                        <EmptyDataTemplate>
                                            <div align="center" class="data_label">
                                                -- No Student Record Found --
                                            </div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sl.No.
                                                        </th>
                                                        <th>Select
                                                            <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="totAll(this);"
                                                                ToolTip="Select or Deselect All Records" />
                                                        </th>
                                                        <th>MISC HEAD CODE
                                                        </th>
                                                        <th>MISC HEAD
                                                        </th>
                                                        <th>Amount
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server">
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td>
                                                    <%#Container.DataItemIndex+1 %>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkReport" runat="server" /><asp:HiddenField ID="hidIdNo" runat="server"
                                                        Value='<%# Eval("MISCHEADSRNO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("MISCHEADCODE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("MISCHEAD")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAmount" CssClass="from-control" runat="server" AppendDataBoundItems="true" MaxLength="10" Text='<%# Eval("AMOUNT")%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div id='divMsg' runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>


</asp:Content>
