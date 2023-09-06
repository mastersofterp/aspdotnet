<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdmissionBranch_OA.aspx.cs" Inherits="ACADEMIC_AdmissionBranch_OA" Title="" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updatepanel1"
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
    <script>
        function validate() {
            var branch = document.getElementById('<%=ddlBranch.ClientID%>');
            if (branch.value == "0") {
                alert('Please select branch.');
                return false;
            }
            else {
                return true;
            }
        }
    </script>
    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch Name</label> 
                                            <%--<asp:Label ID="lblDYtxtBranchName	" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" TabIndex="1" CssClass="form-control" ToolTip="Please enter branch." data-select2-enable="true" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-6">
                                        <div class="label-dynamic">
                                            <label>IsActive</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                            <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnUpdate" runat="server" Text="Update Status" ToolTip="Click to Update." TabIndex="1" OnClick="btnUpdate_Click" CssClass="btn btn-primary" OnClientClick="return validate();" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Click to Cancel." TabIndex="1" CssClass="btn btn-warning" OnClientClick="Cancel();" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto" Visible="false">
                                    <asp:ListView ID="lvList" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h6>Admission Branch Status</h6>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tab-le">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Branch Name</th>
                                                            <th>Active Status</th>
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
                                                    <%#Eval("BRANCH")%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblActive" runat="server" Text='<%#Eval("ACTIVE_STATUS")%>' ForeColor='<%#Convert.ToInt32(Eval("Branch_status"))==1?System.Drawing.Color.Green : System.Drawing.Color.Red%>'></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
