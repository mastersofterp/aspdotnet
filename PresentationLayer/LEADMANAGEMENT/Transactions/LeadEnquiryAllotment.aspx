<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LeadEnquiryAllotment.aspx.cs" Inherits="LEADMANAGEMENT_Transactions_LeadEnquiryAllotment" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <%--  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script src="../../JAVASCRIPTS/ScrollableTablePlugin_1.0_min.js"></script>

    <script type="text/javascript">
        function ScrollRepeater() {
            $('#Table1').Scrollable({
                ScrollHeight: 400
            });
        }
    </script>

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



      <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="upLeadEnquiryAllotment" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="upLeadEnquiryAllotment" runat="server">
        <ContentTemplate>
            <div class="box box-primary">
                <div class="box-header with-border">
                    <span class="glyphicon glyphicon-detail text-blue"></span>
                    <h3 class="box-title"><b>ENQUIRY ALLOTMENT</b></h3>
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
                                            <legend>Enquiry Allotment
                                            </legend>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-4">
                                                                <label><span style="color: red">*</span>Admission Batch</label>

                                                            </div>
                                                            <div class="form-group col-md-8">
                                                                <asp:DropDownList ID="ddlAdmissionBatch" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Admission Batch" runat="server" OnSelectedIndexChanged="ddlAdmissionBatch_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="ddlAdmissionBatch"
                                                                    Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="submit"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-4">
                                                                <label><span style="color: red">*</span>Equiry Allotment</label>
                                                            </div>
                                                            <div class="form-group col-md-8">
                                                                <asp:DropDownList ID="ddlEquiryAllotment" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Enquiry Allotment" runat="server"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEquiryAllotment"
                                                                    Display="None" ErrorMessage="Please Select Enquiry Allotment" InitialValue="0" ValidationGroup="submit"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" runat="server" id="divEnquiryCount" visible="false">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-12">
                                                                <b>Total Enquiry : </b>
                                                                <asp:Label ID="lblTotalCount" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-12">
                                                                <b>Alloted Enquiry : </b>
                                                                <asp:Label ForeColor="Green" ID="lblAllotmentCount" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-12">
                                                                <b>Not Alloted Enquiry : </b>
                                                                <asp:Label ForeColor="Orange" ID="lblPending" runat="server"></asp:Label>
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
                                        <asp:Panel ID="pnlStudentList" runat="server">
                                            <asp:Repeater ID="rptStudentList" runat="server">
                                                <HeaderTemplate>
                                                    <table id="Table1" class="table table-hover table-bordered">
                                                        <tr class="bg-light-blue">
                                                            <th scope="col">Select
                                                            </th>
                                                            <th scope="col">Student Name
                                                            </th>
                                                            <th scope="col">Equiry Alloted
                                                            </th>
                                                            <th scope="col">Enquiry Allotment Status
                                                            </th>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:CheckBox ID="chkSingle" Enabled='<%# Eval("ENQUIRYSTATUS").ToString()=="ALLOTED"? false:true %>' runat="server" />
                                                            <asp:HiddenField ID="hdfEnquiryNo" Value='<%# Eval("ENQUIRYNO") %>' runat="server" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("STUDENTNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ENQUIRY_ALLOTED")%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblEnquiryStatusDone" runat="server" ForeColor="Green" Visible='<%# Eval("ENQUIRYSTATUS").ToString()=="ALLOTED"? true:false %>'><%# Eval("ENQUIRYSTATUS")%></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </asp:Panel>
                                        </table>
                                    </div>
                                    <br />

                                    <div class="col-md-12" id="divButton" visible="false" runat="server" style="padding-top: 18px">
                                        <p class="text-center">
                                            <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                                CssClass="btn btn-success" OnClick="btnSave_Click" TabIndex="3" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                CssClass="btn btn-danger" OnClick="btnCancel_Click" TabIndex="4" />
                                            <asp:Button ID="btnShowReport" runat="server" CausesValidation="False" OnClick="btnShowReport_Click"
                                                TabIndex="5" Text="Excel Report" ToolTip="Excel Report" CssClass="btn btn-info" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="submit"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
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

