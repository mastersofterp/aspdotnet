<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Head.aspx.cs" Inherits="PayRoll_Pay_Head" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../INCLUDES/KannadaScript.js"></script>
    <script src="../../INCLUDES/KannadaScript.js"></script>--%>
    <%--  <link href="../../Css/transliteration.css" rel="stylesheet" />--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PAYMENT HEAD</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlSelect" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Edit Payment Head</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Select Head</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPayhead" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlPayhead_SelectedIndexChanged" AutoPostBack="true" ToolTip="Select Payment Head">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1" Selected="True">Income Head</asp:ListItem>
                                                <asp:ListItem Value="2">Deduction Head</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlPayhead" runat="server" ControlToValidate="ddlPayhead"
                                                Display="None" ErrorMessage="Select Head" ValidationGroup="complaint" InitialValue="-1"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlupdate" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Edit Pay Head</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Paycode</label>
                                            </div>
                                            <asp:TextBox ID="txtPaycode" runat="server" TabIndex="2" CssClass="form-control"
                                                disabled="true" ToolTip="Enter Paycode"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Payhead</label>
                                            </div>
                                            <asp:TextBox ID="txtPayhead" runat="server" TabIndex="3" MaxLength="10"
                                                CssClass="form-control" ToolTip="Enter Pay Head"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddltype" AppendDataBoundItems="true" TabIndex="4" runat="server" AutoPostBack="true" data-select2-enable="true"
                                                CssClass="form-control" OnSelectedIndexChanged="ddltype_SelectedIndexChanged" ToolTip="Select Type">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                <asp:ListItem Value="C">Calculative</asp:ListItem>
                                                <asp:ListItem Value="E">Earning</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div id="calrow" runat="server" class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Calculation</label>
                                            </div>
                                            <asp:DropDownList ID="ddlcal" AppendDataBoundItems="true" runat="server" TabIndex="5" ToolTip="Select CalCulation" data-select2-enable="true"
                                                CssClass="form-control" OnSelectedIndexChanged="ddlcal_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                <asp:ListItem Value="BASIC">BASIC</asp:ListItem>
                                                <asp:ListItem Value="SCALE">SCALE</asp:ListItem>
                                                <asp:ListItem Value="GROSS">GROSS</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlcal" runat="server" ControlToValidate="ddlcal"
                                                Display="None" ErrorMessage="Please Select Calculation" ValidationGroup="payroll"
                                                SetFocusOnError="True" InitialValue="-1">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Full Name</label>
                                            </div>
                                            <asp:TextBox ID="txtFullname" runat="server" TabIndex="6" ToolTip="Enter Full Name" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div id="formularow" runat="server" class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Formula</label>
                                            </div>
                                            <asp:TextBox ID="txtFormula" runat="server" TabIndex="7" ToolTip="Enter Formula" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtformula" runat="server" ControlToValidate="txtFormula"
                                                Display="None" ErrorMessage="Please Enter Formula" ValidationGroup="payroll"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <%-- <div class="form-group col-md-10">
                                                                <label>Pay Head Kannada :</label>
                                                                <asp:TextBox ID="txtPayHeadKannada" runat="server" CssClass="form-control" TabIndex="8"
                                                                    onblur="PayHeadKannada()"></asp:TextBox>--%>
                                        <%-- onpaste="return false"--%>
                                        <%-- </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Clear Amount After Permanent Lock</label>
                                            </div>
                                            <asp:CheckBox ID="chkperlock" runat="server" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                 <label>Is LWP Effect</label>
                                            </div>
                                              <asp:CheckBox ID="chkIsLWPEffect" runat="server" />
                                        </div>


                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSub" runat="server" Text="Submit" TabIndex="7" ToolTip="Click here to Save" ValidationGroup="payroll"
                                    CssClass="btn btn-primary" OnClick="btnSub_Click" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" TabIndex="8" ToolTip="Click here to Reset" CausesValidation="false"
                                    CssClass="btn btn-primary" OnClick="btnBack_Click" />
                                <asp:Button ID="btnShowReport" runat="server" TabIndex="9" ToolTip="Click here to Show Report"
                                    Text="Show Report" CssClass="btn btn-info" OnClick="btnShowReport_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvPayhead" runat="server">
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Pay Head Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action</th>
                                                        <th>PayCode</th>
                                                        <th>PayHead</th>
                                                        <th>Type</th>
                                                        <th>Calculation</th>
                                                        <th>Full Name</th>
                                                        <th>Formula</th>
                                                        <th>ISLWPEffect</th>
                                                        <%--<th>Payhead Kannada</th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SRNO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                </td>
                                                <td>
                                                    <%# Eval("PAYHEAD")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PAYSHORT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TYPE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CAL_ON")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PAYFULL")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FORMULA")%>
                                                </td>
                                                <td>
                                                   <%# Eval("IsLWPEffect")%>
                                                </td>
                                                <%--<td>
                                                    <%# Eval("PAYSHORT_KANNADA")%>
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
     <div id="divMsg" runat="server"></div>
</asp:Content>
