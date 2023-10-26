<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ClubActivityReport.aspx.cs" Inherits="ACADEMIC_ClubActivityReport"
    ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>--%>
    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFeed"
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

    <asp:UpdatePanel ID="updFeed" runat="server">
        <ContentTemplate>
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
                                                <%-- <label>Session</label>--%>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" ToolTip="Please Select Session" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="rfvSession1" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Report"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group form-group col-lg-3 col-md-6 col-12">
                                            <label><span style="color: red;">*</span>Club</label>

                                            <asp:ListBox ID="lstbxClub" runat="server" AppendDataBoundItems="true" TabIndex="4" ToolTip="Select Club"
                                                CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="true"></asp:ListBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6"  SetFocusOnError="true" runat="server" ControlToValidate="lstbxSemester" ErrorMessage="Please Select Semester" InitialValue="0" ForeColor="Red"
                                          ValidationGroup="submit" Display="none" ></asp:RequiredFieldValidator>--%>
                                        </div>


                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnReport" runat="server" Text="Report(Excel)" TabIndex="6"
                                        ValidationGroup="Report" OnClientClick="return validateForm();" CssClass="btn btn-primary" OnClick="btnReport_Click" />
                                    <asp:Button ID="btnCancelReport" runat="server" Text="Cancel" TabIndex="7"
                                        OnClick="btnCancelReport_Click" CssClass="btn btn-warning" />

                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Report" />

                                    <div id="divMsg" runat="server">
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
        </ContentTemplate>
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


    <script type="text/javascript">
        function validateForm() {
            var ddlSession = document.getElementById('<%= ddlSession.ClientID %>');
            var lstbxClub = document.getElementById('<%= lstbxClub.ClientID %>');
       
            if (ddlSession != null) {
                if (ddlSession.value === "0") {
                    alert('Please select session.');
                    return false;
                }
            }

            if (lstbxClub != null) {
                var selectedItems = 0;
                for (var i = 0; i < lstbxClub.options.length; i++) {
                    if (lstbxClub.options[i].selected) {
                        selectedItems++;
                    }
                }

                if (selectedItems === 0) {
                    alert('Please select at least one Club.');
                    return false;
                }
            }


        }
    </script>

</asp:Content>

