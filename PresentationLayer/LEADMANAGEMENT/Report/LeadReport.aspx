<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LeadReport.aspx.cs" Inherits="LEADMANAGEMENT_Report_LeadReport" %>

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
                    <span class="glyphicon glyphicon-detail text-blue"></span>
                    <h3 class="box-title"><b>LEAD REPORT</b></h3>
                    <asp:Label ID="Label2" runat="server"><span  style="padding-left: 650px; color: Red; font-weight: bold;">Note : * marked fields are Mandatory</span></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <form role="form">
                                    <div id="divMsg" runat="server">
                                    </div>
                                    <div class="box-body">
                                        <fieldset>
                                            <legend>Lead Report
                                            </legend>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-3">
                                                            </div>
                                                            <div class="form-group col-md-2">
                                                                <label><span style="color: red">*</span>Admission Batch</label>
                                                            </div>
                                                            <div class="form-group col-md-4">
                                                                <asp:DropDownList ID="ddlAdmissionBatch" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Admission Batch" runat="server"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="ddlAdmissionBatch"
                                                                    Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="submit"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-md-3">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>

                                        <fieldset>
                                            <legend>Report Type
                                            </legend>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-12">
                                                                <asp:RadioButton ID="rdoConsolitedLeadReport" GroupName="Report" runat="server" />
                                                                <b>Consolited Report</b>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-12">
                                                                <asp:RadioButton ID="rdoAllotedNotAllotedEnquiryList" GroupName="Report" runat="server" />
                                                                <b>Enquiry Alloted & Not Alloted Report</b>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-12">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-12">
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </fieldset>
                                    </div>
                                </form>
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
                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

