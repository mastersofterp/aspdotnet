<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Acc_BudgetHead_new.aspx.cs" Inherits="BudgetHead_new" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPBudget"
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

    <asp:UpdatePanel ID="UPBudget" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BUDGET HEAD CREATION</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                  <%--  <div class="sub-heading">
                                        <h5>Budget Head New</h5>
                                    </div>--%>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Budget Head</label>
                                            </div>
                                            <asp:TextBox ID="txtBudgetHead" Style="text-transform: uppercase" TabIndex="2" runat="server" CssClass="form-control" ToolTip="Enter Budget Head" placeholder="Enter Budget Head">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvBudgetHead" runat="server" Display="None" ControlToValidate="txtBudgetHead"
                                                ErrorMessage="Please Enter Budget Head" ValidationGroup="bhn"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Parent Budget</label>
                                            </div>
                                            <asp:DropDownList ID="ddlParentBudget" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="3" ToolTip="Select Parent Budget" OnSelectedIndexChanged="ddlParentBudget_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0" Text="Select Parent Budget"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Budget Code </label>
                                            </div>
                                            <asp:TextBox ID="txtBudgetShortName" Style="text-transform: uppercase" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Enter Budget Short Name" placeholder="Enter Budget Short Name" MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvbudgetshoertname" runat="server" ControlToValidate="txtbudgetshortname"
                                                Display="None" ErrorMessage="Please Enter BudgetShortName" ValidationGroup="bhn">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Serial No.</label>
                                            </div>
                                            <asp:TextBox ID="txtSerialno" Style="text-transform: uppercase" Enabled="false" runat="server" TabIndex="4" ToolTip="Enter Serial No" placeholder="Enter Serial Number" CssClass="form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvSerialnum" runat="server" ValidationGroup="bhn" ControlToValidate="txtSerialno" ErrorMessage="Please Enter Serial Number" Display="None"></asp:RequiredFieldValidator>--%>
                                            <asp:HiddenField ID="HdnSerial" runat="server" Value="0" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Budget Proposal</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdbBudgetProposal" runat="server" AppendDataBoundItems="true" TabIndex="5" ToolTip="Select Budget Proposal" RepeatDirection="Horizontal" CellSpacing="10" CellPadding="10">
                                                <asp:ListItem Value="1" Text="Income"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Expenditure"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfvBudgetProposal" runat="server" ControlToValidate="rdbBudgetProposal" Display="None"
                                                ValidationGroup="bhn" ErrorMessage="Please select Budget Proposal"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Definition</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdoRecruit" runat="server" AppendDataBoundItems="true" TabIndex="6" ToolTip="Select Recurring / Non Recurring." RepeatDirection="Horizontal" CellSpacing="10" CellPadding="10">
                                                <asp:ListItem Value="R" Text="Recurring"></asp:ListItem>
                                                <asp:ListItem Value="NR" Text="Non Recurring"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Account Ledger </label>
                                            </div>
                                            <asp:TextBox ID="txtAcc" runat="server" CssClass="form-control" TabIndex="7" ToolTip="Please Enter Account Ledger." AutoPostBack="true" OnTextChanged="txtAcc_TextChanged"></asp:TextBox>
                                            <ajaxToolKit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtAcc" MinimumPrefixLength="1" EnableCaching="true"
                                                CompletionSetCount="1" CompletionInterval="1" ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                            </ajaxToolKit:AutoCompleteExtender>

                                            <asp:HiddenField ID="hdnparty" runat="server" Value="0" />

                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary" TabIndex="8" Text="Submit" OnClick="btnsubmit_Click" ValidationGroup="bhn" ToolTip="Add BudgetHead" />
                                    <asp:Button ID="btncancel" runat="server" CssClass="btn btn-warning" TabIndex="9" Text="Cancel" OnClick="btncancel_Click" ToolTip="Clear the Form" />
                                    <asp:ValidationSummary ID="bhn" runat="server" ValidationGroup="bhn" ShowMessageBox="true" ShowSummary="false" />

                                </div>
                                <div class="col-12 mt-2">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Search Criteria</label>
                                            </div>
                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" Style="text-transform: uppercase" TabInde="10" onkeyup="onCheck(this)" placeholder="Search"
                                                Text="" ToolTip="Please Enter Group Name" CssClass="form-control"></asp:TextBox>
                                             <div class="mt-2">
                                             <asp:Panel ID="pnlRepeator" runat="server">
                                                <asp:ListBox ID="lvbudgethead" runat="server" CssClass="form-control" Rows="10" AutoPostBack="true" TabIndex="11" OnSelectedIndexChanged="lvbudgethead_SelectedIndexChanged"
                                                    ToolTip="BudgetHead" style="height:300px!important;"></asp:ListBox>
                                            </asp:Panel>
                                                 </div>
                                        </div>
                                        
                                         <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Budget Head Hierarchy</label>
                                            </div>
                                            <asp:Panel ID="panel2" runat="server" BorderStyle="Outset">
                                                <asp:TreeView ID="tvHierarchy" runat="server" ImageSet="Arrows">
                                                </asp:TreeView>
                                            </asp:Panel>
                                        </div>

                                    </div>



                                </div>

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
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

    </script>
    <%--<style>
        .Space label {
            margin-left: 20px;
        }
    </style>--%>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
    <script type="text/javascript">

        function onCheck(sender, args) {
            lst = document.getElementById('<%= lvbudgethead.ClientID %>');
            lst.selectedIndex = -1;
            var search_value = sender.value.toUpperCase();
            for (var i = 0; i < lst.options.length; i++) {
                if (lst.options[i].text.toUpperCase() == search_value || lst.options[i].text.toUpperCase() == search_value) {
                    lst.options[i].selected = true;
                    break;
                }
            }
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

    </script>

</asp:Content>

