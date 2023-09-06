<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Itle_Student_Roll_List.aspx.cs" Inherits="Itle_Student_Roll_List"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STUDENT ROLL LIST REPORT</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <asp:Panel ID="pnlStudentRoll" runat="server">
                            <div class="row">
                                <%-- <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select Criteria For Student Roll List Report</h5>
                                    </div>
                                </div>--%>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Session</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSessionNo" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSessionNo_SelectedIndexChanged1" AutoPostBack="true"
                                        ValidationGroup="submit" ToolTip="Select Session" TabIndex="1" >
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSessionNo" InitialValue="0"
                                        ErrorMessage="Select session." ValidationGroup="submit"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Branch</label>
                                    </div>
                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                        ValidationGroup="Select branchno" AutoPostBack="true" ToolTip="Select Branch"
                                        TabIndex="2" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBranch" InitialValue="0"
                                        ErrorMessage="Select branch." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Regulation</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSchemeNo" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                        ValidationGroup="submit" ToolTip="Please Select Schemeno" AutoPostBack="true"
                                        TabIndex="3" OnSelectedIndexChanged="ddlSchemeNo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSchemeNo" InitialValue="0"
                                        ErrorMessage="Select regulation." ValidationGroup="submit"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Semester</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                        AutoPostBack="True" TabIndex="4" ToolTip="Select Semester">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSem" InitialValue="0"
                                        ErrorMessage="Select semester." ValidationGroup="submit"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <%--<asp:UpdatePanel ID="upnlRollList" runat="server">
                                    <ContentTemplate>--%>
                                        <asp:Button ID="btnReport" runat="server" Text="Roll List Report" OnClick="btnReport_Click"
                                            ValidationGroup="submit" ToolTip="Show RollList under Selected Criteria."
                                            CssClass="btn btn-primary" TabIndex="5" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            ValidationGroup="Cancel Button" ToolTip="Cancel Field under Selected criteria."
                                            CssClass="btn btn-warning" TabIndex="6" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                   <%-- </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnReport" />
                                        <asp:PostBackTrigger ControlID="btnCancel" />
                                    </Triggers>
                                </asp:UpdatePanel>--%>
                            </div>
                            <div class="col-12">
                                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
