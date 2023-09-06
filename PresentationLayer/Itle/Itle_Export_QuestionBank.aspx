<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Itle_Export_QuestionBank.aspx.cs" Inherits="Itle_Itle_Export_QuestionBank"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

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
                            <h3 class="box-title">EXPORT QUESTIONS FROM QUESTION BANK</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlExport" runat="server">
                                <asp:Panel ID="pnlExportQuestion" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-lg-5 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Session :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSession" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>

                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Course Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblCorseName" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>

                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-2">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Select Option For</label>
                                                </div>
                                                <asp:RadioButtonList ID="rbnQuestionOption" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="true" OnSelectedIndexChanged="rbnQuestionOption_SelectedIndexChanged">
                                                    <asp:ListItem Value="A" Text="All Questions" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="T" Text="Topic Wise"></asp:ListItem>
                                                </asp:RadioButtonList>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Select Course</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                    ValidationGroup="Select sessionno" AutoPostBack="true" ToolTip="Please Select Course name"
                                                    TabIndex="1" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged1">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trTopicName" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <label>Select Topic</label>
                                                </div>
                                                <asp:DropDownList ID="ddlTopic" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                    ValidationGroup="Select Schemeno" ToolTip="Please Select Schemeno" TabIndex="2"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShowGrid" runat="server" Text="Show Questions To Export" CssClass="btn btn-primary"
                                            OnClick="btnShowGrid_Click" ToolTip="Click here to Show Questions To Export" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            ValidationGroup="Cancel Button" ToolTip="Click here to Cancel Field Under Selected Criteria."
                                            CssClass="btn btn-warning" TabIndex="6" />
                                    </div>

                                    <div id="trExcelButton" runat="server">
                                        <div class="col-12 text-center" id="tdimgbtn">
                                            <asp:ImageButton ID="imgbutExporttoexcel" runat="server" ToolTip="Export to excel"
                                                ImageUrl="~/images/excel.jpeg" Height="45px" Width="45px" OnClick="imgbutExporttoexcel_Click" />
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlReport" runat="server">
                                    <div class="col-12">
                                        <asp:GridView ID="grdExportQuestions" CssClass="vista-grid" 
                                            runat="server" Height="10px" OnRowDataBound="grdExportQuestions_RowDataBound"> <%--HeaderStyle-BackColor="ActiveBorder"--%>
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgbutExporttoexcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
