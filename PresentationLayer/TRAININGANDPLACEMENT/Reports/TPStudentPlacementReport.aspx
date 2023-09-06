<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TPStudentPlacementReport.aspx.cs" Inherits="TRAININGANDPLACEMENT_Reports_TPStudentPlacementReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title"><strong>STUDENT PLACEMENT REPORT</strong></h3>

                </div>

                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12" ID="pnlSelect" runat="server">
                            <div class="panel panel-info">
                                 
                                <div class="panel-heading"><sup>*</sup><strong>Selection Criteria</strong></div>
                                <br />
                                <div class="panel-body">

                                   <div class="form-group col-md-4">
                                      
                                       <%--  <label>Session</label>--%>
                                        <asp:DropDownList ID="ddlCriteria" runat="server" AppendDataBoundItems="True" ValidationGroup="show"
                                            class="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                             <asp:ListItem Value="1">NIRF</asp:ListItem>
                                             <asp:ListItem Value="2">IQAC</asp:ListItem>
                                             <asp:ListItem Value="3">BTCRC</asp:ListItem>
                                             <asp:ListItem Value="4">Annual Report</asp:ListItem>
                                             <%--<asp:ListItem Value="5">COE</asp:ListItem>
                                             <asp:ListItem Value="6">CISJAF</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlCriteria"
                                            Display="None" ErrorMessage="Please Select Criteria" InitialValue="0" ValidationGroup="Select"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                                
                                        <div class="box-footer text-center">
                                            <div class="col-md-12">

                                            <asp:Button ID="btnExcel" runat="server" Text="Report" ValidationGroup="Select" OnClick="btnExcel_Click"
                                                class="btn btn-info" />

                                            <asp:Button ID="btnCan" runat="server" Text="Cancel" OnClick="btnCan_Click"
                                                class="btn btn-warning" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Select"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                    </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
     <div id="divMsg" runat="server">
    </div>
</asp:Content>

