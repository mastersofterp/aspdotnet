<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_FixedAssets_Report.aspx.cs" Inherits="STORES_Reports_Str_FixedAssets_Report" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script src="../../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../../jquery/jquery.multiselect.js"></script>
    <script src="jquery/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Clear() {
            document.getElementById('<%=ddlAssets.ClientID%>').value="0"
        }
       </script>


    
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Fixed Assets Sr Wise Report</h3>

                        </div>


                        <div class="box-body">
                            <div class="col-md-12">
                                <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                                <asp:Panel ID="pnlStrConfig" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Fixed Assets Report</div>
                                        <div class="panel-body">
                                            <div class="col-sm-12 form-group">
                                                <div class="col-sm-4">
                                                    <label><span style="color:red">*</span>Select Item :</label>
                                                    <asp:DropDownList ID="ddlAssets" runat="server" AppendDataBoundItems="true" CssClass="form-control" ></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvassets" runat="server" Display="None" ErrorMessage="Please Select Item" InitialValue="0" ControlToValidate="ddlAssets" ValidationGroup="Assets" >
                                                        </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                             <div class="col-sm-12 form-group text-center">
                                                 <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="Assets" CssClass="btn btn-info" OnClick="btnReport_Click" />
                                                 <asp:Button ID="btnClear" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClientClick="Clear();" />
                                                 <asp:ValidationSummary ID="vsAssets" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Assets" />
                                                 </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

