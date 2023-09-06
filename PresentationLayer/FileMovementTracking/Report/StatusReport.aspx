<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StatusReport.aspx.cs" Inherits="FileMovementTracking_Report_StatusReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STATUS REPORTS</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="sub-heading">
                                        <h5>Select File For Status Report</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select File</label>
                                            </div>
                                            <asp:DropDownList ID="ddlFile" CssClass="form-control" runat="server" TabIndex="1" data-select2-enable="true"
                                                AppendDataBoundItems="true" ToolTip="Select File">
                                            </asp:DropDownList>
                                           
                                           <asp:RequiredFieldValidator ID="rfvddlfile" runat="server" ErrorMessage="Please Select File."
                                                            ControlToValidate="ddlFile" InitialValue="0"
                                                            Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                           
                                    </div>
                                            
                                        </div>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Report" TabIndex="5" ValidationGroup="Submit" OnClick="btnShow_Click"
                                    CssClass="btn btn-info" CausesValidation="true" ToolTip="Click here to Show Report" />
                                  <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                            ValidationGroup="Submit" />
                                 </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

