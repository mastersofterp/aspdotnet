﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Acc_Funding_Agency.aspx.cs" Inherits="ACCOUNT_Acc_Funding_Agency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript">
         //On Page Load
         $(document).ready(function () {
             $('#table2').DataTable();
         });
    </script>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };

        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
    </script>
    <link href="../../Css/transliteration.css" rel="stylesheet" />
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Funding Agency</h3>
                        </div>

                        <div class="box-body">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            <asp:Panel ID="pnlPfMaster" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <%--<h5>Add/Edit Funding Agency</h5>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Funding Agency</label>
                                            </div>
                                            <asp:TextBox ID="txtfunding" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True" onkeypress="return CheckAlphabet(event,this);"
                                                TabIndex="1" ToolTip="Please Enter Funding Agency Name" MaxLength="200" style="width: 300px" >
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="txtfunding" runat="server" ErrorMessage="Enter Funding Agency Name" ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                        </div>
                                  
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" TabIndex="4" Text="Submit" ValidationGroup="submit"
                                    ToolTip="Submit" CssClass="btn btn-primary" Onclick="btnsubmit_Click" />

                                <asp:Button ID="btncancel" runat="server" TabIndex="6" Text="Cancel" CausesValidation="False"
                                     ToolTip="Cancel" CssClass="btn btn-warning" OnClick="btncancel_Click" />

                               <%-- <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                  CssClass="btn btn-warning"    ToolTip="Click here to Return to Previous Menu" TabIndex="8" />--%>

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>
                            <div class="col-12">

                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvFundingAgency" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In University"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Funding Agency</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Action
                                                            </th>
                                                            <th>Funding Agency
                                                            </th>
                                                            
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("AGENCY_ID") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />

                                                </td>
                                                <td>
                                                    <%# Eval("FUNDING_AGENCY")%>
                                                </td>
                                               
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
            <div id="divMsg" runat="server"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

