<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TPWorkArea.aspx.cs" Inherits="TRAININGANDPLACEMENT_Masters_TPWorkArea" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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

    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">WORK AREA MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Domain</label>
                                        </div>
                                        <asp:TextBox ID="txtWorkarea" runat="server" MaxLength="100" CssClass="form-control" placeholder="Enter Domain Name" AutoComplete="off" />
                                        <asp:RequiredFieldValidator ID="rfvWorkarea" runat="server" ControlToValidate="txtWorkarea"
                                            Display="None" ErrorMessage="Please Enter Domain" SetFocusOnError="true"
                                            ValidationGroup="Submit" />

                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />

                            </div>

                            <div class="col-12 table-responsive">
                                <asp:ListView ID="lvWorkarea" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <div class="sub-heading">
                                                <h5>Domoian List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit
                                                        </th>
                                                        <th>Domain No.
                                                        </th>
                                                        <th>Domain
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
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                    CommandArgument='<%# Eval("WORKAREANO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("WORKAREANO")%>
                                            </td>
                                            <td>
                                                <%# Eval("WORKAREANAME")%>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                    CommandArgument='<%# Eval("WORKAREANO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("WORKAREANO")%>
                                            </td>
                                            <td>
                                                <%# Eval("WORKAREANAME")%>
                                            </td>

                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>

                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

