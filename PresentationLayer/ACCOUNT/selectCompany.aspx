<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="selectCompany.aspx.cs" Inherits="Account_selectCompany" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register TagPrefix="Custom" Namespace="ASB" Assembly="AutoSuggestBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function ShowAlert() {
            alert('Msg');
        }
    </script>

    <style type="text/css">
        .CenterPB {
            position: fixed;
            left: 50%;
            top: 50%;
            margin-top: -20px; /* make this half your image/element height */
            margin-left: -20px; /* make this half your image/element width */
            width: auto;
            height: auto;
        }
    </style>
    <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>
   <%-- <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSElCompany"
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

    <asp:UpdatePanel ID="updSElCompany" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SELECT COMPANY</h3>

                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlSelComp" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Company Selection</label>
                                            </div>
                                            <asp:ListBox ID="lstCompany" runat="server" Rows="20" CssClass="form-control " SelectionMode="Single" style="height:300px!important;"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnProceed" runat="server" Text="Proceed " CssClass="btn btn-primary"
                                        OnClick="btnProceed_Click" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div> 
          
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
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
