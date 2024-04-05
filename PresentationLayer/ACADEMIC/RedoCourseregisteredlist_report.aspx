<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" ViewStateEncryptionMode="Always" EnableViewStateMac="true" CodeFile="RedoCourseregisteredlist_report.aspx.cs" Inherits="ACADEMIC_RedoCourseregisteredlist_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl_details"
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
    <asp:UpdatePanel ID="updpnl_details" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <%--    <label>REDO COURSE REGISTRATION LIST REPORT</label>--%>
                                <label>REGISTRATION LIST REPORT</label>
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="col-12">
                                    <div class="row">

                                        <%--                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College/Session</label>
                                            </div>
                                            <asp:ListBox ID="ddlCollege" runat="server" AppendDataBoundItems="true" ValidationGroup="Excel" TabIndex="1"
                                                CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="false"></asp:ListBox>

                                            <%--  <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                                data-select2-enable="true" ValidationGroup="Excel">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session </label>
                                            </div>
                                            <asp:DropDownList ID="ddlSessionCollege" runat="server" TabIndex="1" CssClass="form-control" AppendDataBoundItems="true"
                                                ToolTip="Please Select Session." data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSessionCollege_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvSCol" runat="server" ValidationGroup="show" ControlToValidate="ddlSessionCollege" ErrorMessage="Please Select Session" Display="None" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                        </div>


                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>College</label>--%>
                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <%--<asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ToolTip="Please Select School/Institute." AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" ValidationGroup="submit" TabIndex="1">
                                                    </asp:DropDownList>--%>
                                            <%--Added for Multiple Selection on 2022 Aug 30--%>

                                            <asp:ListBox ID="ddlCollege" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"
                                                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" TabIndex="1"></asp:ListBox>
                                            <%--OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged1" --%>

                                            <%--Added for Multiple Selection on 2022 Aug 30 End--%>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please Select School/Institute." ValidationGroup="show"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Activity Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlActivityName" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                ToolTip="Please Select Activity." AutoPostBack="true" OnSelectedIndexChanged="ddlActivityName_SelectedIndexChanged">
                                                <%-- OnSelectedIndexChanged="ddlActivityName_SelectedIndexChanged" --%>
                                                <%--AutoPostBack="true" OnSelectedIndexChanged=""--%>
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <%--  <asp:ListItem Value="1">Course Registration</asp:ListItem>
                                                        <asp:ListItem Value="2">Redo Course Registration</asp:ListItem>--%>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvActivityName" runat="server" ValidationGroup="show" Display="None" ErrorMessage="Please Select Activity." InitialValue="0" SetFocusOnError="true" ControlToValidate="ddlActivityName"></asp:RequiredFieldValidator>


                                        </div>

                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnExcel" runat="Server" ToolTip="Excel" Text="Excel Report" TabIndex="1" ValidationGroup="show" OnClick="btnExcel_Click"
                                            CssClass="btn btn-info" />
                                        <asp:Button ID="btnCancel" runat="server" ToolTip="Cancel" Text="Cancel" TabIndex="1" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="show" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript">
        function Validate() {
            var ddlSessionCollege = document.getElementById("<%=ddlSessionCollege.ClientID %>");
            if (ddlSessionCollege.value == "" || ddlSessionCollege.value == 0) {
                //If the "Please Select" option is selected display error.
                alert("Please Select Session!");
                return false;
            }

            var ddlCollege = document.getElementById("<%=ddlCollege.ClientID%>");
            if (ddlCollege.value == "" || ddlCollege.value == 0) {
                alert("Please Select School/Institute.");
                ddlCollege.focus();
                return false;
            }

            var ddlActivityName = document.getElementById("<%=ddlActivityName.ClientID%>")
            if (ddlActivityName.value == "" || ddlActivityName.value == 0) {
                alert("Please Select Activity.");
                return false;
            }


            return true;
        }
    </script>

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

