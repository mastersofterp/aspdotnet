<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LockEntryNotDone.aspx.cs" Inherits="ACADEMIC_REPORTS_LockEntryNotDone" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div id="divMsg" runat="server">
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>LOCK ENTRY NOT DONE</b></h3>
                            <div class="pull-right">
                                <div style="color: Red; font-weight: bold;">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="box-body">
                            <asp:Panel ID="pnlMarkEntry" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3 form-group">
                                            <label><span style="color: red;">*</span>Session :</label>
                                            <asp:DropDownList ID="ddlSession" runat="server" class="form-control"
                                                AppendDataBoundItems="true" TabIndex="1" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="report"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-md-3 form-group">
                                            <label><span style="color: red;">*</span>Degree :</label>
                                            <asp:DropDownList ID="ddlDegree" runat="server" class="form-control"
                                                AppendDataBoundItems="true" TabIndex="1" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="report"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-3 form-group">
                                            <label><span style="color: red;">*</span>Branch :</label>
                                            <asp:DropDownList ID="ddlBranch" runat="server" class="form-control"
                                                AppendDataBoundItems="true" TabIndex="1" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                              <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3 form-group">
                                            <label><span style="color: red;">*</span>Scheme :</label>
                                            <asp:DropDownList ID="ddlScheme" runat="server" class="form-control" AutoPostBack="True"
                                                AppendDataBoundItems="true" TabIndex="1" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3 form-group">
                                            <label><span style="color: red;">*</span>Semester :</label>
                                            <asp:DropDownList ID="ddlSemester" runat="server" class="form-control"
                                                AppendDataBoundItems="true" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3 form-group">
                                            <label><span style="color: red;">*</span>Exam :</label>
                                            <asp:DropDownList ID="ddlTest" runat="server" class="form-control"
                                                AppendDataBoundItems="true" TabIndex="2"
                                                AutoPostBack="True">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTest" runat="server" ControlToValidate="ddlTest"
                                                Display="None" ErrorMessage="Please Select Exam" InitialValue="-1" SetFocusOnError="True"
                                                ValidationGroup="report"></asp:RequiredFieldValidator>
                                        </div>
                                        <div id="trStud" class="col-md-3 form-group" style="display: none;">
                                            <label><span style="color: red;">*</span>Exam :</label>
                                            <asp:RadioButtonList ID="rblStud" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0" Selected="True">Regular Student</asp:ListItem>
                                                <asp:ListItem Value="1">BackLog Student</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4 form-group">
                                                </div>
                                                <div class="col-md-4 form-group">
                                                    <asp:Button ID="btnReport" runat="server" Text="Show Report" CssClass="btn btn-info"
                                                        ValidationGroup="report" OnClick="btnReport_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Font-Bold="true" OnClick="btnCancel_Click"
                                                    Text="Cancel" CssClass="btn btn-danger" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                                                </div>
                                                <div class="col-md-4 form-group">
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

