<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LeadAllotmentReport.aspx.cs" Inherits="LEADMANAGEMENT_Report_LeadAllotmentReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();
        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="upLeadReport" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="upLeadReport" runat="server">
        <ContentTemplate>
            <div class="box box-primary">
                <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                           <div class="box-body">
                            <div class="col-12">
                <%--<div class="box-header with-border">
                    <span class="glyphicon glyphicon-detail text-blue"></span>
                    <h3 class="box-title"><b>LEAD ALLOTMENT REPORT</b></h3>
                    <asp:Label ID="Label2" runat="server"><span  style="padding-right: 650px; color: Red; font-weight: bold;">Note : * marked fields are Mandatory</span></asp:Label>
                </div>--%>

                 <div class="row">
                                                    <div class="col-md-12">
                                                       <%-- <div class="form-group">--%>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Admission Batch"></asp:Label>
                                                                        </div>
                                                                <asp:DropDownList ID="ddlAdmissionBatch" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Admission Batch" runat="server"></asp:DropDownList>

                                                                         <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="ddlAdmissionBatch"
                                                                    Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="submit"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                             
                                       </div>    
                                 <div class="box-footer">
                                    <div class="col-md-12">
                                        <p class="text-center">
                                             <asp:Button ID="btnShowReport" runat="server" Text="Excel Report" ToolTip="Submit" ValidationGroup="submit"
                                                CssClass="btn btn-info" OnClick="btnShowReport_Click" TabIndex="3" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                CssClass="btn btn-danger" OnClick="btnCancel_Click" TabIndex="4" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="submit"
                                                ShowMessageBox="true"  ShowSummary="false" DisplayMode="List" />
                                        </p>
                                    </div>
                                </div>
                         <%--      
                            </div>
                        </div>
                    </div>--%>
               <%-- </div>--%>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

