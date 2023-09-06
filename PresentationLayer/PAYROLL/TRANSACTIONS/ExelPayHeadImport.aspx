<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ExelPayHeadImport.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_ExelPayHeadImport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">EXCEL PAY HEAD IMPORT</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Excel Pay Head Import</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Pay Head</label>
                                </div>
                                <asp:DropDownList ID="ddlPayhead" runat="server" ValidationGroup="payroll" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="1"
                                    CssClass="form-control"> 
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPayhead"
                                    Display="None" ErrorMessage="Please Select PayHead" ValidationGroup="payroll"
                                    InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Upload Excel Sheet</label>
                                </div>
                                <asp:FileUpload ID="flUplaodPayheadExcel" runat="server" CssClass="btn btn-default" />
                            </div>
                        </div>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnUploadExcel" runat="server" Text="Upload" ValidationGroup="payroll"
                            OnClick="btnUploadExcel_Click" CssClass="btn btn-primary" TabIndex="2"/>
                        <asp:Button ID="btnDownlaod" runat="server" Text="Download" CssClass="btn btn-primary" OnClick="btnDownlaod_Click" TabIndex="3"/>
                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="payroll"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
