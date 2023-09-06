<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdmissionNotice.aspx.cs" Inherits="ACADEMIC_AdmissionNotice" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAdmissionNotice"
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

    <asp:UpdatePanel ID="updAdmissionNotice" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ADMISSION NOTIFICATION MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Enter Notice</label>
                                        </div>
                                        <asp:TextBox ID="txtNotice" runat="server" MaxLength="500" TextMode="MultiLine" TabIndex="1" CssClass="form-control" ToolTip="Please Enter Notice"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNotice" runat="server" ControlToValidate="txtNotice" ErrorMessage="Please Enter Notice" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-primary" TabIndex="2" Text="Submit" ValidationGroup="Submit" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" TabIndex="3" OnClick="btnCancel_Click" Text="Cancel" />
                                        <asp:ValidationSummary ID="vsNotice" runat="server" DisplayMode="List" ShowMessageBox="true" ValidationGroup="Submit" ShowSummary="false" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlListView" runat="server">
                                   <asp:ListView ID="lvAdmNotice" runat="server">
                                       <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Notice List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblAdmNotice">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            Action
                                                        </th>
                                                        <th>
                                                            Notice
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
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit1.gif"
                                                        CommandArgument='<%# Eval("NOTICEID")%>' OnClick="btnEdit_Click" AlternateText="Edit Record" ToolTip="Edit Record"/>
                                                </td>
                                                <td>
                                                    <%# Eval("NOTICE") %>
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

