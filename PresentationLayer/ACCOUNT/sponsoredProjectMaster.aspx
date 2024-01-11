<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="sponsoredProjectMaster.aspx.cs" Inherits="ACCOUNT_sponsoredProjectMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 250px;
        }
    </style>

    <%-- <script src="../jquery/jquery-1.10.2.js"></script>--%>

    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Project</h3>
                </div>
                <div class="box-body">
                    <div id="divCompName" runat="server" style="text-align: center; font-size:x-large;"></div>
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Project Sub Head</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Expense Head Defination</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="2">Project Defination</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <%-- <div class="col-12">
                       
                        <div id="div13">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs">
                                    <li class="active"><a href="#Div1" data-toggle="tab" aria-expanded="true">Project Sub Head</a></li>
                                    <li class=""><a href="#Div2" data-toggle="tab" aria-expanded="true">Expense Head Defination</a></li>
                                    <li class=""><a href="#Div3" data-toggle="tab" aria-expanded="true">Project Defination</a></li>
                                </ul>
                            </div>

                            <div class="tab-content">--%>

                                <asp:UpdatePanel ID="updBank" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12 mt-3">
                                                <div class="sub-heading">
                                                    <h5>Add/Edit Project Sub Head</h5>
                                                </div>
                                                <asp:Panel ID="pnl" runat="server">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="span5" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Project Title</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvProjName" runat="server" InitialValue="0" ValidationGroup="ProjAllocation"
                                                                    Display="None" ErrorMessage="Please Select Project Title" ControlToValidate="ddlProjectName"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div12" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Project Sub Head Name</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlProjectSubHead" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectSubHead_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvProjSub" runat="server" InitialValue="0" ValidationGroup="ProjAllocation" Display="None" ErrorMessage="Please Select Project Sub Head Name" ControlToValidate="ddlProjectSubHead"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div4" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Budgeted Amount</label>
                                                                </div>
                                                                <asp:TextBox ID="txtReceived" runat="server" CssClass="form-control"></asp:TextBox><%--onblur="CalculateRemainAmt();"--%>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbereceived" runat="server" TargetControlID="txtReceived" FilterType="Custom,Numbers" FilterMode="ValidChars" ValidChars=".-"></ajaxToolKit:FilteredTextBoxExtender>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div5" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Total Amount Spent in current year</label>
                                                                </div>
                                                                <asp:TextBox ID="txttotSpent" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox><%--onblur="CalculateRemainAmt();"--%>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxttotSpent" runat="server" TargetControlID="txttotSpent" FilterType="Custom,Numbers" FilterMode="ValidChars" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div6" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Remaining Amount</label>
                                                                </div>
                                                                <asp:TextBox ID="txtReceivedAmt" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtReceivedAmt" runat="server" TargetControlID="txtReceivedAmt" FilterType="Custom,Numbers" FilterMode="ValidChars" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>

                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" ValidationGroup="ProjAllocation" OnClick="btnSubmit_Click" />

                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                                        <asp:ValidationSummary ID="vsProjAllocation" runat="server" ShowMessageBox="True" ShowSummary="False" DisplayMode="List" ValidationGroup="ProjAllocation" />
                                                    </div>

                                                </asp:Panel>

                                            </div>
                                            <asp:Panel ID="pnlItemMaster" runat="server">
                                                <div class="col-md-12 table-responsive">
                                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="">
                                                        <asp:Repeater ID="rptsubhead" runat="server" OnItemDataBound="rptsubhead_ItemDataBound">
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr class="bg-light-blue">
                                                                    <td style="font-weight: bold; text-align: left">Project Title : 
                                                                                <%#Eval("ProjectName")%>
                                                                        <asp:HiddenField ID="hdnProjectId" runat="server" Value='<%#Eval("ProjectId")%>' />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5"><%--ScrollBars="Auto"--%>
                                                                        <asp:Panel ID="pnl1" runat="server">
                                                                            <asp:ListView ID="lvSubProjDetail" runat="server" OnItemCommand="lvSubProjDetail_ItemCommand">
                                                                                <LayoutTemplate>

                                                                                    <div id="Div7">
                                                                                        <%--<div class="titlebar">
                                                                                            <h4>Sub Head</h4>
                                                                                        </div>--%>
                                                                                        <table class="table table-striped table-bordered nowrap ">
                                                                                            <tr class="bg-light-blue">
                                                                                                <th>Action</th>
                                                                                                <th>Sub Head
                                                                                                </th>
                                                                                                <th>Budgeted Amount
                                                                                                </th>
                                                                                                <th>Total Amount Spent(Current Yr)
                                                                                                </th>
                                                                                                <th>Remaining
                                                                                                </th>
                                                                                                <tbody>
                                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                                </tbody>
                                                                                                <%-- <th>Approval
                                                                                            </th>--%>
                                                                                            </tr>
                                                                                            <tbody>
                                                                                                <tr id="Tr1" runat="server" />
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </div>

                                                                                    <%--<div id="Div1" class="vista-grid">
                                                                                        <table class="datatable">
                                                                                        </table>
                                                                                        </div>--%>
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td><%--style="width: 2%"--%>
                                                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("ProjectSubHeadAllocationId")%>'
                                                                                                ImageUrl="~/Images/edit.png" ToolTip="Edit record" />
                                                                                        </td>
                                                                                        <td><%--style="width: 38%; font-weight: bold"--%>
                                                                                            <%# Eval("ProjectSubHeadName")%>
                                                                                        </td>
                                                                                        <td style="text-align: right"><%--style="width: 20%; font-weight: bold"--%>
                                                                                            <%# string.Format("{0:#,0.00}", Eval("TotAmtReceived"))%>
                                                                                        </td>
                                                                                        <td style="text-align: right"><%--style="width: 20%; font-weight: bold; text-align: left;"--%>
                                                                                            <%# string.Format("{0:#,0.00}", Eval("TotAmtSpent"))%>
                                                                                        </td>
                                                                                        <td style="text-align: right"><%--style="width: 10%; font-weight: bold; text-align: left;"--%>
                                                                                            <%# string.Format("{0:#,0.00}", Eval("TotAmtRemain"))%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <%--<AlternatingItemTemplate>
                                                                                    <tr class="altitem" onmouseout="this.style.backgroundColor='#FFFFD2'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                                                        <td style="width: 2%">
                                                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("ProjectSubHeadAllocationId")%>'
                                                                                                ImageUrl="~/images/edit.gif" ToolTip="Edit record" />

                                                                                        </td>
                                                                                        <td style="width: 38%; font-weight: bold">
                                                                                            <%# Eval("ProjectSubHeadName")%>
                                                                                        </td>
                                                                                        <td style="width: 20%; font-weight: bold">
                                                                                            <%# string.Format("{0:#,0.00}", Eval("TotAmtReceived"))%>
                                                                                        </td>
                                                                                        <td style="width: 20%; font-weight: bold; text-align: left;">
                                                                                            <%# string.Format("{0:#,0.00}", Eval("TotAmtSpent"))%>
                                                                                        </td>
                                                                                        <td style="width: 20%; font-weight: bold; text-align: left;">
                                                                                            <%# string.Format("{0:#,0.00}", Eval("TotAmtRemain"))%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </AlternatingItemTemplate>--%>
                                                                            </asp:ListView>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </div>
                                            </asp:Panel>


                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="tab_2">
                                <asp:UpdatePanel ID="updatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12 mt-3">
                                                <div class="sub-heading">
                                                    <h5>Add/Edit Item Sub Head Defination</h5>
                                                </div>
                                            </div>

                                            <asp:Panel ID="Panel4" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div21" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Project Title</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="1" ToolTip="Please Select" AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlProject"
                                                                Display="None" ErrorMessage="Please SelectProject Title" SetFocusOnError="True"
                                                                InitialValue="0" ValidationGroup="ProjSubHead"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div19" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Expense Head Type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlResType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="2" ToolTip="Please Select" AutoPostBack="true" OnSelectedIndexChanged="ddlResType_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlResType"
                                                                Display="None" ErrorMessage="Please Select Expense Head Type" SetFocusOnError="True"
                                                                InitialValue="0" ValidationGroup="ProjSubHead"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div9" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Expense Head Short Code </label>
                                                            </div>
                                                            <asp:TextBox ID="txtProjShortName" runat="server" ValidationGroup="submit" ReadOnly="true" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtProjShortName" runat="server" ValidationGroup="ProjSubHead" Display="None" ErrorMessage="Please Enter Expense Head Short Code" ControlToValidate="txtProjShortName"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div8" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Expense Head </label>
                                                            </div>
                                                            <asp:TextBox ID="txtSponsorProj" runat="server" ValidationGroup="submit" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtSponsorProj" runat="server" ValidationGroup="ProjSubHead" Display="None" ErrorMessage="Please Enter Expense Head" ControlToValidate="txtSponsorProj"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Ledger Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtAcc" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Please Enter Ledger Name." AutoPostBack="true" OnTextChanged="txtAcc_TextChanged"></asp:TextBox>
                                                            <ajaxToolKit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtAcc" MinimumPrefixLength="1" EnableCaching="true"
                                                                CompletionSetCount="1" CompletionInterval="1" ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                            </ajaxToolKit:AutoCompleteExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAcc" Display="None"
                                                                ErrorMessage="Please Select Ledger Name" SetFocusOnError="true" ValidationGroup="ProjSubHead">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:HiddenField ID="hdnOpartyManual" runat="server" Value="0" />

                                                        </div>


                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmitSubProj" CssClass="btn btn-primary" runat="server" TabIndex="6" Text="Submit" ValidationGroup="ProjSubHead" OnClick="btnSubmitSubProj_Click" />

                                                    <asp:Button ID="btnCancelSubProj" runat="server" CssClass="btn btn-warning" TabIndex="7" Text="Cancel" OnClick="btnCancelSubProj_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" DisplayMode="List" ValidationGroup="ProjSubHead" />
                                                </div>

                                            </asp:Panel>


                                            <asp:Panel ID="Panel1" runat="server">
                                                <div class="col-12">
                                                    <asp:ListView ID="lstSubProj" runat="server" OnItemCommand="lstSubProj_ItemCommand">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblHead">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>

                                                                            <th>Action</th>
                                                                            <th>Expense Head Short Code
                                                                            </th>
                                                                            <th>Expense Head
                                                                            </th>
                                                                            <th>Expense HeadType
                                                                            </th>
                                                                            <th>Project Title
                                                                            </th>
                                                                            <th>Ledger
                                                                            </th>
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
                                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("ProjectSubId")%>' ImageUrl="~/Images/edit1.gif" ToolTip="Edit record" />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ProjectSubHeadShort")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ProjectSubHeadName")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("PROJECTHEADTYPE")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ProjectName")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("PARTY_NAME")%>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </asp:Panel>

                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <div class="tab-pane" id="tab_3">
                                <asp:UpdatePanel ID="updatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12 mt-3">
                                                <div class="sub-heading">
                                                    <h5>Add/Edit Project Defination</h5>
                                                </div>

                                                <asp:Panel ID="Panel2" runat="server">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic" id="Div11" runat="server">
                                                                <sup>*</sup>
                                                                <label>Project Title</label>
                                                            </div>
                                                            <asp:TextBox ID="txtProjName" runat="server" ValidationGroup="submit" MaxLength="1000"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtProjName" runat="server" ValidationGroup="ProjHead" Display="None" ErrorMessage="Please Enter Project Title" ControlToValidate="txtProjName"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic" id="Div10" runat="server">
                                                                <sup>*</sup>
                                                                <label>Project Number</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSponShortName" runat="server" ValidationGroup="submit"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtSponShortName" runat="server" ValidationGroup="ProjHead" Display="None" ErrorMessage="Please Enter Project Number" ControlToValidate="txtSponShortName"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic" id="Div31" runat="server">
                                                                <%--<sup>*</sup>--%>
                                                                <label>Funding Agency</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlFundingAgency" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ValidationGroup="ProjHead" Display="None"
                                                                ErrorMessage="Please Select Funding Agency" ControlToValidate="ddlFundingAgency" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic" id="Div16" runat="server">
                                                                <%--<sup>*</sup>--%>
                                                                <label>Scheme</label>
                                                            </div>
                                                            <asp:TextBox ID="txtScheme" runat="server" ValidationGroup="submit" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'500');" onkeyup="textCounter(this, this.form.remLen, 250);"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="ProjHead" Display="None"
                                                                ErrorMessage="Please Enter Scheme" ControlToValidate="txtScheme"></asp:RequiredFieldValidator>--%>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic" id="Div17" runat="server">
                                                                <sup>*</sup>
                                                                <label>Principle Investigator</label>
                                                            </div>
                                                            <asp:TextBox ID="txtCoordinator" runat="server" MaxLength="100" ValidationGroup="submit"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="ProjHead" Display="None"
                                                                ErrorMessage="Please Enter Coordinator" ControlToValidate="txtCoordinator"></asp:RequiredFieldValidator>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic" id="Div14" runat="server">
                                                                <sup>*</sup>
                                                                <label>Department</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="ProjHead" Display="None"
                                                                ErrorMessage="Please Select Department" ControlToValidate="ddlDepartment" InitialValue="0"></asp:RequiredFieldValidator>

                                                        </div>

                                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic" id="Div18" runat="server">
                                                                <sup>*</sup>
                                                                <label>Total Project Cost</label>
                                                            </div>
                                                            <asp:TextBox ID="txtValue" runat="server" MaxLength="9" ValidationGroup="submit" onkeyup="validateNumeric(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="ProjHead"
                                                                Display="None" ErrorMessage="Please Enter Value" ControlToValidate="txtValue"></asp:RequiredFieldValidator>

                                                        </div>

                                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic" id="Div32" runat="server">
                                                                 <sup>*</sup> 
                                                                <label>Project Duration</label>
                                                            </div>
                                                            <asp:TextBox ID="txtProjectDuration" runat="server" ValidationGroup="submit"></asp:TextBox>
                                                               <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtProjectDuration"
                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="ProjHead" Display="None"
                                                                ErrorMessage="Please Enter Project Duration" ControlToValidate="txtProjectDuration"></asp:RequiredFieldValidator>

                                                        </div>

                                                         <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Project Start Date</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon" id="Div3">
                                                                    <i class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="submit" CssClass="form-control" OnTextChanged="txtStartDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ValidationGroup="ProjHead"
                                                                Display="None" ErrorMessage="Please Enter Start date" ControlToValidate="txtStartDate"></asp:RequiredFieldValidator>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="ProjHead"
                                                                    Display="None" ErrorMessage="Please Select Date" ControlToValidate="txtStartDate"></asp:RequiredFieldValidator>--%>

                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                                    Format="dd/MM/yyyy" PopupButtonID="Div3" TargetControlID="txtStartDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                            </div>
                                                        </div>

                                                         <div class="form-group col-lg-3 col-md-6 col-12" id="Div13" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Project End Date</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon" id="Div22">
                                                                    <i class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="submit" CssClass="form-control"></asp:TextBox>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="ProjHead"
                                                                    Display="None" ErrorMessage="Please Select End Date" ControlToValidate="txtEndDate"></asp:RequiredFieldValidator>

                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                                                    Format="dd/MM/yyyy" PopupButtonID="Div22" TargetControlID="txtEndDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                            </div>
                                                        </div>

                                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic" id="Div34" runat="server">
                                                                <%--<sup>*</sup>--%>
                                                                <label>Amount Received Recurring</label>
                                                            </div>
                                                            <asp:TextBox ID="txtAmtRecurring" runat="server" ValidationGroup="submit" MaxLength="11" ></asp:TextBox>
                                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="ProjHead" Display="None"
                                                                ErrorMessage="Please Enter Amount Received Recurring" ControlToValidate="txtAmtRecurring"></asp:RequiredFieldValidator>--%>

                                                        </div>

                                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic" id="Div35" runat="server">
                                                                <%--<sup>*</sup>--%>
                                                                <label>Amount Received Non-Recurring</label>
                                                            </div>
                                                            <asp:TextBox ID="txtAmtNonRecurring" runat="server" ValidationGroup="submit"  MaxLength="11" ></asp:TextBox>
                                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="ProjHead" Display="None"
                                                                ErrorMessage="Please Enter Amount Received Non-Recurring" ControlToValidate="txtAmtNonRecurring"></asp:RequiredFieldValidator>--%>

                                                        </div>

                                                       
                                                       
                                                        <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic" id="Div17" runat="server">
                                                                <sup>*</sup>
                                                                <label>Principle Investigator</label>
                                                            </div>
                                                            <asp:TextBox ID="txtCoordinator" runat="server" MaxLength="100" ValidationGroup="submit"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="ProjHead" Display="None"
                                                                ErrorMessage="Please Enter Coordinator" ControlToValidate="txtCoordinator"></asp:RequiredFieldValidator>

                                                        </div>--%>
                                                       <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic" id="Div18" runat="server">
                                                                <sup>*</sup>
                                                                <label>Total Project Cost</label>
                                                            </div>
                                                            <asp:TextBox ID="txtValue" runat="server" MaxLength="9" ValidationGroup="submit" onkeyup="validateNumeric(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="ProjHead"
                                                                Display="None" ErrorMessage="Please Enter Value" ControlToValidate="txtValue"></asp:RequiredFieldValidator>

                                                        </div>--%>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div20" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Sanction Date</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon" id="ImaSanctionDate">
                                                                    <i class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtSanctionDate" runat="server" ValidationGroup="submit" CssClass="form-control"></asp:TextBox>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ValidationGroup="ProjHead"
                                                                    Display="None" ErrorMessage="Please Select Sanction Date" ControlToValidate="txtSanctionDate"></asp:RequiredFieldValidator>

                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True"
                                                                    Format="dd/MM/yyyy" PopupButtonID="ImaSanctionDate" TargetControlID="txtSanctionDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic" id="Div30" runat="server">
                                                                <sup>*</sup>
                                                                <label>Sanction Number </label>
                                                            </div>
                                                            <asp:TextBox ID="txtSanctionLetter" runat="server" ValidationGroup="submit"></asp:TextBox>
                                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Red" ErrorMessage="*Please Enter Alphanumeric Value" ControlToValidate="txtSanctionLetter" ValidationExpression="[a-zA-Z0-9]*$" ValidationGroup="ProjHead"></asp:RegularExpressionValidator>--%>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="ProjHead"
                                                                Display="None" ErrorMessage="Please Enter Sanction Number" ControlToValidate="txtSanctionLetter"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div36" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Date</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon" id="ImaCalStartDate">
                                                                    <i class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtDate" runat="server" ValidationGroup="submit" CssClass="form-control"></asp:TextBox>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="ProjHead"
                                                                    Display="None" ErrorMessage="Please Select Date" ControlToValidate="txtDate"></asp:RequiredFieldValidator>

                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True"
                                                                    Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                            </div>
                                                        </div>

                                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic" id="Div15" runat="server">
                                                                <%--<sup>*</sup>--%>
                                                                <label>Sanction By</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSanctionBy" runat="server" ValidationGroup="submit" MaxLength="100"></asp:TextBox>
                                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="ProjHead" Display="None"
                                                                ErrorMessage="Please Enter Sanction By" ControlToValidate="txtSanctionBy"></asp:RequiredFieldValidator>--%>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Bank Ledger </label>
                                                            </div>
                                                            <asp:HiddenField ID="HdnAcc" runat="server" Value="0" />

                                                            <asp:TextBox ID="txtAgainstAcc" runat="server" AutoPostBack="true" CssClass="form-control" ToolTip="Please Enter Ledger Name" OnTextChanged="txtAgainstAcc_TextChanged"></asp:TextBox>
                                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtAgainstAcc" MinimumPrefixLength="1"
                                                                EnableCaching="true" CompletionSetCount="1" CompletionInterval="1" ServiceMethod="GetAgainstAcc" OnClientShowing="clientShowing">
                                                            </ajaxToolKit:AutoCompleteExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                                ControlToValidate="txtAgainstAcc" Display="None" ErrorMessage="Please Select Account Ledger" SetFocusOnError="true" ValidationGroup="ProjHead">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnSubmitProj" runat="server" CssClass="btn btn-primary" Text="Submit" ValidationGroup="ProjHead" OnClick="btnSubmitProj_Click" />

                                                            <asp:Button ID="btnCancelProj" runat="server" Text="Cancel" OnClick="btnCancelProj_Click" CssClass="btn btn-warning" />
                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False" DisplayMode="List" ValidationGroup="ProjHead" />
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                            </div>
                                            <asp:Panel ID="pnlProject" runat="server">
                                                <div class="col-12">
                                                    <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListView1_ItemCommand">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblHead">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Action

                                                                            </th>
                                                                            <th>Short Code
                                                                            </th>
                                                                            <th>Project Title
                                                                            </th>
                                                                            <th>Department
                                                                            </th>
                                                                            <th>Sanction By
                                                                            </th>
                                                                            <th>
                                                                            Total Project Cost
                                                                            </th>
                                                                        <th>Sanction Number
                                                                        </th>
                                                                            <th>Bank Ledger
                                                                            </th>
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
                                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("ProjectId")%>'
                                                                        ImageUrl="~/Images/edit.png" ToolTip="Edit record" />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ProjectShortName")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ProjectName")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("subdept")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("Sanctionby")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("Value")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SanctionLetter")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("PARTY_NAME")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>

                                                </div>

                                            </asp:Panel>

                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>


    <script language="javascript" type="text/javascript">

        function CalculateRemainAmt() {


            var a = $('#<%=txtReceived.ClientID%>').val();
                    var b = $('#<%=txttotSpent.ClientID%>').val();
                    if (parseInt(a) < parseInt(b)) {
                        alert("Budgeted Amount Should Greater Than Total Amount Spent in current year");
                        $('#<%=txtReceivedAmt.ClientID%>').prop('disabled', true);
                            return false;
                        }
                        else {
                            $('#<%=txtReceivedAmt.ClientID%>').val(parseInt(a) - parseInt(b));
                            $('#<%=txtReceivedAmt.ClientID%>').prop('disabled', true);
                        }
                    }

    </script>

    <script type="text/javascript">
        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event) {//IE
                        e.returnValue = false;
                    }
                    else {//Firefox
                        e.preventDefault();
                    }
                }
            }
        }


        function textCounter(field, countfield, maxlimit) {
            if (field.value.length > maxlimit)
                field.value = field.value.substring(0, maxlimit);
            else
                countfield.value = maxlimit - field.value.length;
        }

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }

    </script>
</asp:Content>

