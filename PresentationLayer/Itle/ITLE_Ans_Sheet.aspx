<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ITLE_Ans_Sheet.aspx.cs" Inherits="Itle_ITLE_Ans_Sheet" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel1"
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
    <asp:UpdatePanel ID="updPanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT ANSWER SHEET REPORT</h3>
                        </div>

                        <div class="box-body">

                            <div class="col-12">
                                <asp:Panel ID="pnlAnswerSheet" runat="server">
                                    <div class="row">
                                        <div class="col-lg-5 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Session  :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSession" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-7 col-md-12 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Course Name  :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblCorseName" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>

                                    <div class="row mt-3">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Test Type</label>
                                            </div>
                                            <asp:RadioButtonList ID="rbtTestType" runat="server" AutoPostBack="true"
                                                RepeatDirection="Horizontal" Font-Bold="true"
                                                OnSelectedIndexChanged="rbtTestType_SelectedIndexChanged">
                                                <asp:ListItem Value="O" Text="Objective" Selected="True">&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="D" Text="Descriptive"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Test</label>
                                            </div>
                                            <asp:DropDownList ID="ddlTest" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="Select sessionno" AutoPostBack="true" ToolTip="Select Test"
                                                TabIndex="1" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Student Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStudent" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="Select Student" ToolTip="Please Select Schemeno" TabIndex="2"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Show" OnClick="btnSubmit_Click"
                                            ValidationGroup="Select Roll List Report Button" CssClass="btn btn-primary"
                                            ToolTip="Click here Show Answersheet Copy Of Selected Student" TabIndex="4" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            ValidationGroup="Cancel Button" ToolTip="Click here to Reset"
                                            CssClass="btn btn-warning" TabIndex="6" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
