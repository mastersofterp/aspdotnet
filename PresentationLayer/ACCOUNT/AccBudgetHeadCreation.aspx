<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="AccBudgetHeadCreation.aspx.cs" Inherits="ACCOUNT_AccBudgetHeadCreation" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../Content/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../Content/jquery.js" language="javascript" type="text/javascript"></script>
    <script src="../jquery/jquery-ui-1.7.3.custom.min.js" type="text/javascript"></script>
    <link href="../Content/demo_table.css" rel="stylesheet" />
    <link href="../Content/demo_table_jui.css" rel="stylesheet" />
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: right;
        }
    </style>

    <script type="text/javascript" charset="utf-8">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();
        }
        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UPDMainGroup"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div style="width: 100%">
        <asp:UpdatePanel ID="UPDMainGroup" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div id="divMsg" runat="server">
                                <div id="div1" runat="server"></div>
                                <div class="box-header with-border">
                                    <h3 class="box-title">BUDGET HEAD CREATION</h3>
                                </div>
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                                <div class="box-body">
                                    <asp:Panel ID="pnl" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Create Budget Head</div>
                                            <div class="panel-body">
                                                <div class="col-md-8">
                                                    Note<span style="font-size: small">:</span><span style="font-weight: bold; font-size: x-small; color: red">* Marked is mandatory !</span><br />
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label>Budget Code<span style="color: red">*</span> : </label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="txtBudCode" runat="server" Style="text-transform: uppercase" Text=""
                                                                ToolTip="Please Enter Budget Code" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtBudCode" runat="server" ControlToValidate="txtBudCode"
                                                                Display="None" ErrorMessage="Please Enter Budget Code" SetFocusOnError="True"
                                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label>Budget Name<span style="color: red">*</span> : </label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="txtbudgetName" runat="server" Style="text-transform: uppercase"
                                                                Text="" ToolTip="Please Enter Budget Name" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtbudgetName" runat="server" ControlToValidate="txtbudgetName"
                                                                Display="None" ErrorMessage="Please Enter Budget Name" SetFocusOnError="True"
                                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label>Parent Budget : </label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:DropDownList ID="ddlBudget" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlBudget_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div id="Div3" class="row" runat="server" visible="false">
                                                        <div class="col-md-3">
                                                            <label>Department : </label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDept"
                                                                Display="None" ErrorMessage="Please Select Department" SetFocusOnError="True"
                                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                                                CssClass="btn btn-info" OnClick="btnSubmit_Click" />&nbsp;
                                                             <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                                 CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            Search Criteria....
                                                        </div>
                                                        <div class="col-md-5">
                                                            <div class="input-group date">
                                                                <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"
                                                                    Style="text-transform: uppercase" Text="" ToolTip="Please Enter Group Name" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="form-group col-md-12">
                                                        <asp:Panel ID="pnlRepeator" runat="server" Height="250px">
                                                            <asp:Repeater ID="lvbudgethead" runat="server" OnItemCommand="lvbudgethead_ItemCommand">
                                                                <HeaderTemplate>
                                                                    <h4 id="demo-grid">
                                                                        <h4 class="box-title">Budget Head
                                                                        </h4>
                                                                        <table class="table table-bordered table-hover">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th style="width: 10%">Action</th>
                                                                                    <th style="width: 20%; text-align: left">Budget Code</th>
                                                                                    <th style="width: 70%; text-align: left">Budget Name</th>
                                                                                    <%--<th style="width: 70%; text-align: left">Department</th>--%>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td style="width: 10%; text-align: left">
                                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="../images/edit.gif" CommandName="Edit"
                                                                                CommandArgument='<%# Eval("BUDG_NO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                                TabIndex="6" />
                                                                        </td>
                                                                        <td style="width: 20%; text-align: left">
                                                                            <%# Eval("BUDG_CODE")%>
                                                                        </td>
                                                                        <td style="width: 70%; text-align: left">
                                                                            <%# Eval("BUDG_NAME")%>
                                                                        </td>
                                                                        <%-- <td style="width: 70%; text-align: left">
                                                                            <%# Eval("SUBDEPT")%>
                                                                        </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </tbody></table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="col-md-4">
                                                    <b>Budget Head Hierarchy</b>
                                                    <asp:Panel ID="panel2" runat="server" ScrollBars="Both" Height="428px" Width="350px" BorderStyle="Outset">
                                                        <asp:TreeView ID="tvHierarchy" runat="server" ImageSet="Arrows">
                                                        </asp:TreeView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                        <%--</div>--%>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="tvHierarchy" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
