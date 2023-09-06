<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ApplicationStages.aspx.cs" Inherits="ACADEMIC_Admission_ACD_ADM_ApplicationStages" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        ul {
            list-style-type: none;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">APPLICATION STAGE</h3>
                </div>

                <div class="box-body">
                      <asp:UpdatePanel ID="updapplicationstage" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label><sup>*</sup>Stage Name</label>
                                </div>
                                <asp:textbox id="txtStageName" runat="server" cssclass="form-control" MaxLength="100" onkeydown="return((event.keyCode >= 64 && event.keyCode <= 91)|| (event.keyCode==32)|| (event.keyCode==8)|| (event.keyCode==9));" />
                                <asp:RequiredFieldValidator ID="rfvtxtStageName" runat="server" ControlToValidate="txtStageName"
                                            Display="None" ErrorMessage="Please Enter Stage Name " SetFocusOnError="True"
                                            ValidationGroup="ApplicationStage"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-6 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label><sup>*</sup>Description</label>
                                </div>
                                <asp:textbox id="txtDescription" runat="server" textmode="MultiLine" cssclass="form-control" rows="1" />
                                <asp:RequiredFieldValidator ID="rfvtxtDescription" runat="server" ControlToValidate="txtDescription"
                                            Display="None" ErrorMessage="Please Enter Description " SetFocusOnError="True"
                                            ValidationGroup="ApplicationStage"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <%--<div class="row">
                            <div class="col-12 col-lg-9 ">
                                <asp:checkboxlist id="chkApp" runat="server" repeatdirection="Horizontal" repeatcolumns="5">
                                    <asp:ListItem Value="0">Form Filled &nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="1">Payment Done &nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="2">Doc Verification &nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="3">Scheduled Interaction &nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="4">Interaction Completed </asp:ListItem>
                                </asp:checkboxlist>
                            </div>
                        </div>--%>
                    </div>
                    <div class="col-12 btn-footer mt-4">
                        <asp:button id="btnsubmit" runat="server" text="Submit" cssclass="btn btn-primary" OnClick="btnsubmit_Click" ValidationGroup="ApplicationStage" />
                        <asp:ValidationSummary ID="vsapplicationstage" runat="server" DisplayMode="List" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="ApplicationStage" />
                        <asp:button id="btnCancel" runat="server" text="Cancel" cssclass="btn btn-warning" OnClick="btnCancel_Click" />
                    </div>
                            <div class="col-12">
                        <asp:Panel ID="pnlAppStages" runat="server" Visible="true">
                            <asp:ListView ID="lvApplicationStages" runat="server" Visible="true">
                                <LayoutTemplate>

                                    <table class="table table-striped table-bordered nowrap display">
                                        <thead>
                                            <tr class="bg-light-blue" >

                                                <th>Edit
                                                </th>
                                                <th>Stage Name
                                                </th>
                                                <th>Description
                                                </th>
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
                                            <asp:ImageButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" CommandArgument='<%# Eval("STAGEID")%>'
                                                ImageUrl="~/Images/edit.png" />
                                        </td>
                                        <td id="participate" runat="server">
                                            <asp:Label runat="server" ID="lblStageName" Text='<%#Eval("STAGENAME") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDescription" Text='<%#Eval("DESCRIPTION") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                          </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
