<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_AuthorityTypeMas.aspx.cs" Inherits="PAYROLL_MASTERS_Pay_AuthorityTypeMas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <%-- <link href="../Css/master.css" rel="stylesheet" type="text/css" />
<link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div id="div1" runat="server"></div>
            <div class="box-header with-border">
                <h3 class="box-title">NODUES AUTHORITY TYPE</h3>
                <p class="text-center text-bold">
                    <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                </p>
                <%--<div class="box-tools pull-right">
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                </div>--%>
            </div>
            <div>
                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <asp:Panel ID="pnldeducationentry" runat="server">
                                <div class="panel panel-info">
                                    <div class="panel panel-heading">Add/Edit Authority Type</div>
                                    <div class="panel panel-body">
                                       <%-- <h4 class="box-title">Add/Edit Quarter</h4>--%>
                                        <div class="form-group col-md-12">
                                            
                                            <div class="form-group col-md-8">
                                                <label>Authority Type :</label>
                                                <asp:TextBox ID="txtAuthoryName" runat="server" MaxLength="200" TabIndex="2" CssClass="form-control"
                                                    ToolTip="Enter Authority Name (only alphabets)" />
                                                <asp:RequiredFieldValidator ID="rfvQuarter" runat="server" ControlToValidate="txtAuthoryName"
                                                    Display="None" ErrorMessage="Please Enter Authority Name " ValidationGroup="quarter"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </form>
            </div>

            <div class="box-footer">
                <div class="col-md-12">
                    <p class="text-center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                            ValidationGroup="quarter" ToolTip="Click here to Submit" TabIndex="3" CssClass="btn btn-primary" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                            CausesValidation="False" ToolTip="Click here to Reset" TabIndex="4" CssClass="btn btn-warning" />
                        <asp:Button ID="btnShowReport" runat="server" Text="Report" CausesValidation="False" Visible="false"
                            ToolTip="Click here to Show Report" TabIndex="4" CssClass="btn btn-info" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="quarter"
                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" />
                    </p>
                </div>
                <div class="col-md-12">
                    <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                        <asp:ListView ID="lvAutho" runat="server">
                            <LayoutTemplate>
                                <div id="lgv1">
                                    <h4 class="box-title">Authority Type List</h4>
                                    <table class="table table-bordered table-hover">
                                        <thead>
                                            <tr class="bg-light-blue">
                                                <th>Action</th>
                                                <th>Authority Type</th>
                                               
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
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("AUTHO_TYP_ID") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                       
                                    </td>
                                    <td>
                                        <asp:Label ID="lblQtrName" runat="server" Text='<%# Eval("AUTHORITY_TYP_NAME") %>' />
                                    </td>
                                    
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
                </div>
                <div class="vista-grid_datapager">
                    <div class="text-center">
                        <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvAutho"
                            PageSize="10">
                            <Fields>
                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

   
   
    
   
   
 </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
