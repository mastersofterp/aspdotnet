<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AttendanceDashboard_On_Off.aspx.cs" Inherits="ACADEMIC_AttendanceDashboard_On_Off" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upAttendance"
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

    <asp:UpdatePanel ID="upAttendance" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Attendance Dashboard ON/OFF</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="Label34" runat="server" Font-Bold="true">Session</asp:Label>
                                        </div>
                                        <asp:ListBox ID="lstSession" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true" TabIndex="1"></asp:ListBox>

                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lstSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select session" InitialValue=""  ValidationGroup="Attend" >
                                        </asp:RequiredFieldValidator>--%>

                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="Label36" runat="server" Font-Bold="true">School/College</asp:Label>
                                        </div>
                                        <asp:ListBox ID="lstCollege" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true" TabIndex="2"></asp:ListBox>

                                     <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lstCollege" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select college" InitialValue=""  ValidationGroup="Attend">
                                        </asp:RequiredFieldValidator>--%>


                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" OnClick="btnSubmit_Click"
                                    CssClass="btn btn-primary" TabIndex="3" ValidationGroup="Attend" />

                                <asp:Button ID="btnCancell" runat="server" OnClick="btnCancell_Click" Text="Cancel" ToolTip="Cancel" CssClass="btn btn-warning" TabIndex="4" />

                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Attend"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancell" />
        </Triggers>
    </asp:UpdatePanel>



    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>

</asp:Content>

