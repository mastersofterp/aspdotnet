<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Anti_Ragging_Declaration.aspx.cs" Inherits="ACADEMIC_Anti_Ragging_Declaration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


        <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title"><asp:Label runat="server">Anti Ragging Declaration Upload</asp:Label></h3>                    
                </div>

                <asp:HiddenField ID="hfFileName" runat="server" />
                <div class="box-body">                  
                        <div class="form-group col-lg-6 col-md-6 col-12">
                            <div class="row">

                                <div class="form-group col-lg-6 col-md-6 col-12">
                                    <div class="label-dynamic">

                                        <label>Anti Ragging Declaration</label>
                                    </div>
                                    <asp:FileUpload ID="fuAntiRagging" runat="server" TabIndex="3" accept=".pdf" />
                                     <asp:Label ID="lblfile" runat="server" />
                                </div>

                            </div>

                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnAntiRaggingUpload" CssClass="btn btn-primary" runat="server" Text="Upload" TabIndex="1" OnClick="btnAntiRaggingUpload_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-outline-danger" runat="server" Text="Cancel" TabIndex="1" OnClick="btnCancel_Click" />
                        </div>
                </div>

                <div class="col-12">
                    <asp:Panel ID="pnlAntiRagging" runat="server" Visible="true">
                        <asp:ListView ID="lvAntiRagging" runat="server">
                            <LayoutTemplate>
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divAntiRagging">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th style="text-align: center; width: 10%">Edit</th>
                                            <th style="text-align: center; width: 80%">Document Name</th>
                                            <th style="text-align: center; width: 10%">Preview</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <asp:UpdatePanel runat="server" ID="updCountry">
                                    <ContentTemplate>
                                        <tr>

                                            <td style="text-align: center;">
                                                <asp:ImageButton ID="btnEditAntiRagging" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                    CommandArgument='<%# Eval("ARNO")%>' AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEditAntiRagging_Click" />
                                                                             
                                            </td>
                                            <td>
                                                <%# Eval("AR_DOCUMENT_NAME")%>
                                            </td>
                                            
                                            <td>
                                                <asp:ImageButton ID="btnPreview" CommandArgument='<%# Eval("AR_DOCUMENT_NAME")%>' class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/search-svg.png"
                                                    AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnPreview_Click" OnClientClick="return openPdfInNewWindow();" />
                                            </td>
                                        </tr>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
                </div>


            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openPdfInNewWindow() {
            debugger;
            var fileName = '<%= (btnPreview != null && btnPreview.CommandArgument != null) ? btnPreview.CommandArgument.ToString() : string.Empty %>';

        if (!fileName) {
            alert('Invalid file name.');
            return false;
        }

        var pdfPath = '<%= ResolveUrl("~/AntiRagging/") %>' + encodeURIComponent(fileName);
        window.open(pdfPath, '_blank');
        return false;
    }
</script>


</asp:Content>

