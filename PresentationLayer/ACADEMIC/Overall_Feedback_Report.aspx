<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Overall_Feedback_Report.aspx.cs" Inherits="Overall_Feedback_Report"
    ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFeed"
            DynamicLayout="true" DisplayAfter="0">
            <progresstemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </progresstemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updFeed" runat="server">
        <contenttemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>

                        <div class="col-lg-12 col-md-12 col-12" id="dvFaculttyFeedback" runat="server">
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                  <%--<label>College & Scheme</label>--%>
                                                <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                                 OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                                Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="Report">
                                            </asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Session</label>--%>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                               OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" ToolTip="Please Select Session" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession1" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Semester" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                               <%-- <label>Semester</label>--%>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                 OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="3" ToolTip="Please Select Semester" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        </div>
                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Feedback Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlFeedbackTyp" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                TabIndex="4" ToolTip="Please Select Feedback Type"  AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvFeedback" runat="server" ControlToValidate="ddlFeedbackTyp"
                                                Display="None" ErrorMessage="Please Select Feedback Type" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnReport" runat="server" Text="Report(Excel)" TabIndex="6" 
                                        ValidationGroup="Report" CssClass="btn btn-primary" OnClick="btnReport_Click" />
                                    <asp:Button ID="btnCancelReport" runat="server" Text="Cancel" TabIndex="7" CssClass="btn btn-warning" OnClick="btnCancelReport_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Report" />

                                    <div id="divMsg" runat="server">
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                        </contenttemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnCancelReport" />
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


