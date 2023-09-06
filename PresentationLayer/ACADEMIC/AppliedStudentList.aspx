<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AppliedStudentList.aspx.cs" Inherits="ACADEMIC_AppliedStudentList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <div class="row">
        <div class="col-md-12">
            <asp:UpdatePanel ID="updpnl" runat="server">
                <ContentTemplate>
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Online Applied Students</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading" style="text-align:right"><span style="color: Red; font-weight: bold;">Note : * Marked fields are Mandatory</span> </div>
                                        <div class="panel-body">

                                            <%--  <div align="center">
                                                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                            </div>--%>
                                            <%--<div style="color: Red; font-weight: bold">
                                                &nbsp;Note : * marked fields are Mandatory
                                            </div>--%>

                                            <div class="form-group col-md-12">
                                                <div class="form-group col-md-4">
                                                    <label><span style="color: red">*</span>Admission Batch:</label>
                                                    <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                                        TabIndex="1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                                        Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmBatch"
                                                        Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    </div>
                                                <div class="form-group col-md-4 fieldset">
                                                    <fieldset class="" style="padding: 1px; color: Green">
                                                        <legend class="legend">Note</legend>Please Select<br />
                                                        <span style="font-weight: bold; color: Red;">Export All Student Details : </span>
                                                        <br />
                                                        Admission Batch<br />
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <div class="form-group col-md-3">
                                                    <label><span style="color: red">*</span>Select Criteria:</label>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <asp:RadioButtonList ID="rdbshow" runat="server" RepeatDirection="Horizontal" TabIndex="2"
                                                        AutoPostBack="true" RepeatColumns="3" OnSelectedIndexChanged="rdbshow_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">&nbsp;&nbsp;Application Fees Paid&nbsp;&nbsp;</asp:ListItem>
                                                           <asp:ListItem Value="2">&nbsp;&nbsp;Application Fees Not Paid&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="3">&nbsp;&nbsp;Student Details Completed&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="4">&nbsp;&nbsp;Student Details Not Completed&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="5">&nbsp;&nbsp;Provisional Fees Paid</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfvCriteria" runat="server" ControlToValidate="rdbshow" Display="None"
                                                        ErrorMessage="Please Select Criteria" ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>
                                                
                                            </div>
                                            <div class="form-group col-md-12 text-center">
                                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show Student List" ValidationGroup="show" TabIndex="3" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnExport" runat="server" Text="Export In Excel" OnClick="btnExport_Click" TabIndex="4" ValidationGroup="show" CssClass="btn btn-info" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                                <asp:Button ID="btnAllExport" runat="server" Text="Export All Student Details" OnClick="btnAllExport_Click" TabIndex="5" ValidationGroup="report" CssClass="btn btn-info" />


                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="show" Style="text-align: center" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="report" Style="text-align: center" />

                                            </div>
                                            <div>
                                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                                            </div>
                                            <div class=" col-md-12">
                                                <asp:ListView ID="lvStudent" runat="server">
                                                    <LayoutTemplate>
                                                        <div>
                                                            <div class="titlebar">
                                                                <h4>Online Applied Student List</h4>
                                                            </div>

                                                            <table class="table table-hover table-bordered table-fixed">
                                                                <tr class="bg-light-blue">
                                                                    <th>Sr.No
                                                                    </th>
                                                                    <th>Application Id
                                                                    </th>
                                                                    <th>Student Name
                                                                    </th>
                                                                    <th>Mobile No.
                                                                    </th>
                                                                    <th>Course/Program
                                                                    </th>
                                                                    <th>Application Fee
                                                                    </th>
                                                                    <th>Student Details Status
                                                                    </th>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblsrno" Text='<%# Eval("SRNO")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblAppid" Text='<%# Eval("USERNAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblStuname" Text='<%# Eval("FIRSTNAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblStuMobile" Text='<%# Eval("MOBILENO")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblBranch" Text='<%# Eval("COURSE_PREFERENCE")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblAmt" Text='<%# Eval("TOTAL_AMT")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("DETAILS_STATUS")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>

                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAllExport" />
                    <asp:PostBackTrigger ControlID="btnExport" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

