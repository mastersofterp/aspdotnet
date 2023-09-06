<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QualDegreeMap.aspx.cs" Inherits="ACADEMIC_QualDegreeMap" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <style>
#ctl00_ContentPlaceHolder1_pnlListView .dataTables_scrollHeadInner {
width: max-content !important;
}
</style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updQual"
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
    <asp:UpdatePanel ID="updQual" runat="server">
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
                                    <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Please Select Degree." data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select Degree."></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblQualDegree" runat="server" Font-Bold="true" Text="Qualifying Degree"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlQualDegree" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Please Select Qualifying Degree." data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlQualDegree"
                                            Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select Qualifying Degree."></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lblStatus" runat="server" Font-Bold="true" Text="Status"></asp:Label>
                                        </div>
                                        <div style="padding-top: 5px">
                                            <asp:CheckBox ID="chkStatus" runat="server" AppendDataBoundItems="true" TabIndex="1" ToolTip="Check To Active." Text="&nbsp;Active"></asp:CheckBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="1" ToolTip="Click To Submit." ValidationGroup="Submit" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="1" ToolTip="Click To Cancel." CausesValidation="false" />
                                <asp:ValidationSummary ID="vsSubmit" runat="server" ValidationGroup="Submit" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlListView" runat="server">
                                    <asp:ListView ID="lvQualDegree" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Qualifying Degree Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divQualDetails">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align:center">
                                                            Edit
                                                        </th>
                                                        <th>
                                                            Degree
                                                        </th>
                                                        <th>
                                                            Qualifying Degree
                                                        </th>
                                                        <th>
                                                            Status
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td style="text-align: center;">
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit1.gif" OnClick="btnEdit_Click"
                                                CommandArgument='<%# Eval("QUAL_DEGREE")%>'  AlternateText="Edit Record" ToolTip="Edit Record"/>
                                                </td>
                                                <td>
                                                    <%#Eval("DEGREENAME") %>
                                                </td>
                                                 <td>
                                                     <%#Eval("QUAL_DEGREENAME") %>
                                                </td>
                                                 <td>
                                                     <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' ForeColor='<%#  Convert.ToInt32(Eval("ACTIVE_STATUS"))==1 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>                                                     
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
