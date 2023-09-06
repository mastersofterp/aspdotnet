<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ViewQuestionBank.aspx.cs" Inherits="Itle_ViewQuestionBank" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <script src="../includes/modalbox.js" type="text/javascript"></script>--%>
    <link href="../CSS/master.css" rel="stylesheet" />
    <link href="../CSS/jquery-ui.css" rel="stylesheet" />

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STUDENT QUESTION BANK</h3>
                </div>
                <div>
                    <form role="form">
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlQuestionBankMaster" runat="server">
                                    <div class="form-group col-md-12">
                                        <asp:Panel ID="pnlAdd" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">View Student Question Bank</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <div class="col-md-3">
                                                                <label>Select the Type of Question :</label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rbnObjectiveDescriptive" Font-Bold="true" AutoPostBack="true"
                                                                    runat="server" RepeatDirection="Horizontal" TabIndex="1" ToolTip="Select Question Type" Width="216px" OnSelectedIndexChanged="rbnObjectiveDescriptive_SelectedIndexChanged">
                                                                    <asp:ListItem Text="Objective" Value="O" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Descriptive" Value="D"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-12">
                                                            <div class="col-md-3">
                                                                <label><span style="color: Red">*</span>Select Topic :</label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true"
                                                                    TabIndex="1"
                                                                    AutoPostBack="true" CssClass="form-control" ToolTip="Select Session" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <div class="col-sm-12" id="grid">
                                            <div class="row" style="border: solid 1px #CCCCCC">
                                                <div style="font-weight: bold; background-color: #72A9D3; color: white" class="panel-heading">Questions List</div>
                                                <div class="table-responsive">
                                                    <table class="customers">
                                                        <%--       <tr style="font-weight: bold; background-color: #808080; color: white">
                                                            <th style="width: 2%; padding-left: 8px; text-align: left">SrNo</th>
                                                            <th style="width: 34%; text-align: left">Question Text</th>
                                                            <th style="width: 8%; text-align: left">Topic</th>
                                                            <th style="width: 8%; text-align: left">Option1</th>
                                                            <th style="width: 8%; text-align: left">Option2</th>
                                                            <th style="width: 8%; text-align: left">Option3</th>
                                                            <th style="width: 8%; text-align: left">Option4</th>
                                                            <th style="width: 8%; text-align: left">Option5</th>
                                                            <th style="width: 8%; text-align: left">Option6</th>
                                                        </tr>--%>
                                                    </table>
                                                </div>
                                                <div>
                                                    <asp:Panel ID="pnllvView" runat="server" ScrollBars="Vertical" Height="400px" BackColor="#FFFFFF">
                                                        <asp:ListView ID="lvQuestions" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="demo-grid">
                                                                    <table class="table table-bordered table-hover dt-responsive nowrap">
                                                                        <thead>
                                                                            <tr style="font-weight: bold; background-color: #808080; color: white">
                                                                                <th style="width: 2%; padding-left: 8px; text-align: left">SrNo</th>
                                                                                <th style="width: 34%; text-align: left">Question Text</th>
                                                                                <th style="width: 8%; text-align: left">Topic</th>
                                                                                <th style="width: 8%; text-align: left">Option1</th>
                                                                                <th style="width: 8%; text-align: left">Option2</th>
                                                                                <th style="width: 8%; text-align: left">Option3</th>
                                                                                <th style="width: 8%; text-align: left">Option4</th>
                                                                                <th style="width: 8%; text-align: left">Option5</th>
                                                                                <th style="width: 8%; text-align: left">Option6</th>
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
                                                                    <td style="width: 2%;">
                                                                        <%# Container.DataItemIndex + 1%>&nbsp;&nbsp;
                                                                    </td>
                                                                    <td style="width: 44%; text-align: left">
                                                                        <%--Parliamentary Government"is also known as....--%>
                                                                        <%# Eval("QUESTIONTEXT")%>
                                                                    </td>
                                                                    <td style="width: 8%; text-align: left">
                                                                        <%# Eval("TOPIC") %>
                                                                    </td>

                                                                    <td style="width: 8%; text-align: left">
                                                                        <%# Eval("ANS1TEXT")%>
                                                                    </td>
                                                                    <td style="width: 8%; text-align: left">
                                                                        <%# Eval("ANS2TEXT")%>
                                                                    </td>
                                                                    <td style="width: 8%; text-align: left">
                                                                        <%# Eval("ANS3TEXT")%>
                                                                    </td>
                                                                    <td style="width: 8%; text-align: left">
                                                                        <%# Eval("ANS4TEXT")%>
                                                                    </td>
                                                                    <td style="width: 8%; text-align: left">
                                                                        <%# Eval("ANS5TEXT")%>
                                                                    </td>
                                                                    <td style="width: 8%; text-align: left">
                                                                        <%# Eval("ANS6TEXT")%>
                                                                    </td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

