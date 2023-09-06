<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_Location_Master.aspx.aspx.cs" Inherits="STORES_Masters_Str_Location_Master_aspx" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>--%>
<%--<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_NewsPaper_Master.aspx.cs" Inherits="Stores_Masters_Str_NewsPaper_Master"
    Title="" %>--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">LOCATION MASTER</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanNewsPaper" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Location Name</label>
                                            </div>
                                            <asp:TextBox ID="txtLocationName" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Enter Location Name" MaxLength="100"
                                                onKeyUp="LowercaseToUppercase(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtLocationName" runat="server" ControlToValidate="txtLocationName"
                                                Display="None" ErrorMessage="Please Enter Location Name" ValidationGroup="store"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtNewPaerFilteChar" runat="server"
                                                FilterMode="ValidChars" FilterType="LowercaseLetters, UppercaseLetters,custom"
                                                ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.1234567890-_ " TargetControlID="txtLocationName">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div4" runat="server">
                                            <div class="label-dynamic">
                                                <label>Active Status</label>
                                            </div>
                                            <asp:CheckBox ID="chkStatus" runat="server" Checked="true" AutoPostBack="true"  />
                                        </div>
                                       
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="store"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:Button ID="butSubmit" ValidationGroup="store" Text="Submit" runat="server" CssClass="btn btn-primary" TabIndex="3" ToolTip="Click To Submit"
                                    OnClick="butSubmit_Click" />
                                <asp:Button ID="btnshowrpt" Text="Report" runat="server" CssClass="btn btn-info" TabIndex="5" ToolTip="Click To Show Report" Visible="false" />
                                <asp:Button ID="butCancel" Text="Cancel" runat="server" CssClass="btn btn-warning" TabIndex="4" ToolTip="Click To Reset" OnClick="butCancel_Click" />

                            </div>
                                                         
                            <asp:Panel ID="pnlNewsPaper" runat="server" ScrollBars="Auto" Style="overflow-y: scroll; max-height: 550px;">
                                <div class="col-12">
                                    <asp:ListView ID="lvLocationMaster" runat="server">
                                        <EmptyDataTemplate>
                                            <div class="text-center">
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                            </div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="demo-grid" class="vista-grid">
                                                <div class="sub-heading">
                                                    <h5>Location Details</h5>
                                                </div>
                                    
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action
                                                            </th>
                                                            <th>Location Name
                                                            </th>
                                                            <th>Active Status
                                                            </th>
                                                        </tr>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                </table>
                                                   
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("LOCATIONNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                </td>
                                                <td>
                                                    <%# Eval("LOCATION")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ACTIVESTATUS")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
              
                                   <%-- <div class="vista-grid_datapager text-center d-none">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvLocationMaster" PageSize="10"
                                            OnPreRender="dpPager_PreRender">
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
                                    </div>--%>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function LowercaseToUppercase(txt) {
            var textValue = txt.value;
            txt.value = textValue.toUpperCase();

        }

    </script>
</asp:Content>


