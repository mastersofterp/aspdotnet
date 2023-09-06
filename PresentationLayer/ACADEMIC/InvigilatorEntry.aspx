<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="InvigilatorEntry.aspx.cs" Inherits="ACADEMIC_InvigilatorEntry" Title=" " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updInv"
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

    <asp:UpdatePanel ID="updInv" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="divMsg" runat="server">
                        </div>
                        <div class="box-header with-border">
                            <h3 class="box-title">INVIGILATOR ENTRY</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvInvigilator" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Invigilator Master </h5>
                                            </div>
                                            <div class="table-responsive" style="height: 400px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblroom">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th>Select
                                                            </th>
                                                            <th>Invigilator Name
                                                            </th>
                                                            <%-- <th style="width: 10%"> Status</th>--%>
                                                            <th>No. of Duties
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
                                                    <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("UA_NO")%>' Checked='<%#Eval("[STATUS]").ToString() == "" ? false : true %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("UA_FULLNAME")%>
                                                </td>
                                                <%-- <td style="width: 10%">
                                                                        <asp:CheckBox ID="chkStatus" runat="server" ToolTip='<%# Eval("UA_NO")%>' Checked='<%#Eval("[STATUS]").ToString() == "" ? false : true %>' />
                                                                        <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
                                                                    </td>--%>
                                                <td>

                                                    <asp:TextBox ID="txtDuties" runat="server" CssClass="form-control" Text='<%# Eval("NO_OF_DUTIES")%>'></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtReorder" runat="server" FilterType="Numbers" TargetControlID="txtDuties">
                                                    </ajaxToolKit:FilteredTextBoxExtender>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer mt-4">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                    OnClick="btnSubmit_Click" class="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    OnClick="btnCancel_Click" class="btn btn-warning" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
