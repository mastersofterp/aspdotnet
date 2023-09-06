<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EnquiryGeneration.aspx.cs" Inherits="LEADMANAGEMENT_Transactions_EnquiryGeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <%--  <script src="../../INCLUDES/prototype.js"></script>
    <script src="../../INCLUDES/scriptaculous.js"></script>
    <script src="../../INCLUDES/transliteration.l.js"></script>--%>

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
                            <h3 class="box-title">ENQUIRY EXCEL IMPORT</h3>
                            <%--<span style="padding-left: 10px; color: Red; font-weight: bold;">Note : * marked fields are Mandatory</span>--%>
                        </div>
                        <span style="padding-left: 0px; color: Red; font-weight: bold;">
                            <div class="col-md-1">
                                Note :
                            </div>
                            <div class="col-md-11">
                            </div>
                        </span>
                        <div class="box-body">
                            <span style="padding-left: 0px; color: Red; font-weight: bold;">
                                <div class="col-md-12">
                                    <div class="col-sm-12">
                                        1) * Marked fields are Mandatory
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-sm-12">
                                        2) Before import excel, kinldy ensure that State, City, District,Source available in ERP master. If not available then do the Master entry in ERP for State, City, District & Source then upload excel.
                                    </div>
                                </div>
                            </span>
                            <div class="col-md-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Enquiry Excel Import<%--<span style="padding-left: 800px; color: Red; font-weight: bold;">Note : * marked fields are Mandatory</span> --%></div>
                                        <div class="panel-body">

                                            <div align="center">
                                                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                            </div>
                                            <%--<div style="color: Red; font-weight: bold">
                                                &nbsp;Note : * marked fields are Mandatory
                                            </div>--%>
                                            <br />
                                            <div class="form-group col-md-12">
                                                <%-- <div class="form-group col-md-6">--%>

                                                <div class="form-group col-md-4">
                                                    <label><span style="color: red">*</span>Admission Batch:</label>
                                                    <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                                        OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" TabIndex="1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                                        Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-md-8">
                                                    <label><span style="color: red">*</span>Attach Excel File:</label>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to upload" TabIndex="2" />
                                                </div>
                                            </div>
                                            <div class="form-group col-md-12 text-center">
                                                <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click1" CssClass="btn btn-success margin" TabIndex="3"
                                                    Text="Download Blank Excel Sheet" ToolTip="Click to download blank excel format file" Enabled="true"><i class="fa fa-file-excel-o"></i> Download Blank Excel Sheet</asp:LinkButton>
                                                <asp:LinkButton ID="btnUpload" runat="server" ValidationGroup="report" OnClick="btnUpload_Click2" CssClass="btn btn-primary margin" TabIndex="4"
                                                    Text="Upload Excel Sheet" ToolTip="Click to Upload" Enabled="true"><i class="fa fa-upload"> Upload Excel</i></asp:LinkButton>
                                                <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" CssClass="btn btn-info" TabIndex="5" />

                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="report" Style="text-align: center" />

                                            </div>
                                            <div >
                                                <asp:ListView ID="lvEnquiry" runat="server">
                                                    <LayoutTemplate>
                                                        <div style="overflow:auto;width:100%;height:auto">
                                                            <div class="titlebar">
                                                                <h3>Lead Generation Info</h3>
                                                            </div>
                                                            
                                                                <table class="table table-hover table-bordered table-fixed small" >
                                                                   <thead> <tr class="bg-light-blue">
                                                                        <th>Student Name
                                                                        </th>
                                                                        <th>Student Mobile Number
                                                                        </th>
                                                                        <th>Email
                                                                        </th>
                                                                        <th>Gender
                                                                        </th>
                                                                         <th>DOB
                                                                        </th>
                                                                        <th>Parents Mobile Number
                                                                        </th>
                                                                        <th>School Name
                                                                        </th>
                                                                        <th>Address
                                                                        </th>
                                                                        <th>City
                                                                        </th>
                                                                         <th>District
                                                                        </th>
                                                                         <th>State
                                                                        </th>
                                                                        <th>Pin Code
                                                                        </th>
                                                                        <th>Lead Collected by
                                                                        </th>
                                                                        <th>Lead Generated Through/Source
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
                                                           <%-- <td style="width: 15%">
                                                                <%# Eval("ENQGENNO")%>
                                                            
                                                            </td>--%>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblStuname" Text='<%# Eval("STUD_FIRSTNAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblStuMobile" Text='<%# Eval("STUD_MOBILE")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblMail" Text='<%# Eval("STUD_EMAILID")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblGender" Text='<%# Eval("GENDER")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblDOB" Text='<%# Eval("DOB")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblParentmobile" Text='<%# Eval("PARENT_MOBILE")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblsclool_name" Text='<%# Eval("SCHOOL_NAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblAddress" Text='<%# Eval("ADDRESS")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblCity" Text='<%# Eval("CITY")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="Label1" Text='<%# Eval("DISTRICT")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="Label2" Text='<%# Eval("STATE")%>'></asp:Label>
                                                            </td>
                                                             <td>
                                                                <asp:Label runat="server" ID="Label3" Text='<%# Eval("PIN")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblLeadCollected" Text='<%# Eval("LEAD_COLLECTED_BY")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lbllead_source" Text='<%# Eval("LEAD_SOURCE")%>'></asp:Label>
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
                    <%--<asp:PostBackTrigger ControlID="btnDownload" />--%>
                    <asp:PostBackTrigger ControlID="btnUpload" />
                    <asp:PostBackTrigger ControlID="btnExport" />
                    <asp:PostBackTrigger ControlID="btnReport" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
     <div id="divMsg" runat="server">
    </div>
</asp:Content>

