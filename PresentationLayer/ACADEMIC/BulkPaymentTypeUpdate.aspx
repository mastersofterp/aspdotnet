<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BulkPaymentTypeUpdate.aspx.cs" Inherits="HOSTEL_BulkPaymentTypeUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">BULK PAYMENT TYPE UPDATE</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--<label>Adm. Batch</label>--%>
                                    <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddladmbatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvadmbatch" runat="server" ControlToValidate="ddladmbatch"
                                    Display="None" ErrorMessage="Please Select Admission Batch." InitialValue="0"
                                    SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--<label>Degree</label>--%>
                                    <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                    TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--<label>Programme/Branch</label>--%>
                                    <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Submit" CssClass="btn btn-primary" OnClick="btnShow_Click" TabIndex="4" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn btn-primary" TabIndex="5" Enabled="false" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="6" CssClass="btn btn-warning" CausesValidation="false" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                    </div>

                    <div class="col-12">
                        <asp:Panel runat="server" ID="pnlStudentList">
                            <asp:ListView ID="lvPaymenttype" runat="server" OnItemDataBound="lvPaymenttype_ItemDataBound">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Student List</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="mytable">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr.No
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                </th>
                                                <th>Student Name
                                                </th>
                                                <th>Category
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDYtxtYear" runat="server" Font-Bold="true"></asp:Label>
                                                </th>
                                                <th>Payment Type
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                            <%--<tr id="itemPlaceholder" runat="server" />--%>
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td>
                                            <%# Container.DataItemIndex+1%>
                                        </td>
                                        <td>
                                            <%# Eval("REGNO")%>
                                            <asp:HiddenField runat="server" ID="hdfIdno" Value='<%# Eval("IDNO")%>' />
                                        </td>
                                        <td>
                                            <%# Eval("STUDNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("CATEGORY")%>
                                        </td>
                                        <td>
                                            <%# Eval("BRANCH")%>
                                        </td>
                                        <td>
                                            <%# Eval("YEAR")%>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlPaymentType" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip='<%# Eval("PTYPE")%>'>
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
