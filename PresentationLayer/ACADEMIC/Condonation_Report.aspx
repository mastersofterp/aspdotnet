<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Condonation_Report.aspx.cs" Inherits="ACADEMIC_Condonation_Report" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
      <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdReport"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UpdReport" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:ListBox ID="lstSession" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                <asp:Button ID="Excel" runat="server" CssClass=" btn btn-info"
                                    ValidationGroup="Show" Text="Excel Report"  OnClick="Excel_Click" />
                                     <asp:Button ID="BtnPdf" runat="server" CssClass=" btn btn-info"
                                    ValidationGroup="Show" Text="Pdf Report"   OnClick="BtnPdf_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" Text="Cancel"
                                    OnClick="btnCancel_Click" />
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Excel" />
              <asp:PostBackTrigger ControlID="BtnPdf" />
        </Triggers>
        </asp:UpdatePanel>
        <script type="text/javascript">
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
    </script>
</asp:Content>

