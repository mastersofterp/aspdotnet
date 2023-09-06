<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Achievement_Basic_Details.aspx.cs" Inherits="ACADEMIC_StudentAchievement_Achievement_Basic_Details" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .nav-tabs-custom .nav-link {
            padding: 0.5rem 0.22rem;
            background: #fff;
            color: #3c8dbc;
        }

        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 90px;
        }
    </style>

    <script type="text/javascript">
        /*
        $(function () {

            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "tab_1";

            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {

                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
                //$("[id*=TabName]").val();
            });
        });
        */
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
            document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
            document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });
    </script>

    <script>
        function Setdate(date) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).ready(function () {

                    var startDate = moment(date.split('-')[0], "MMM DD, YYYY");
                    var endtDate = moment(date.split('-')[1], "MMM DD, YYYY");

                    $('#date').html(startDate.format("MMM DD, YYYY") + ' - ' + endtDate.format("MMM DD, YYYY"));
                    document.getElementById('<%=hdnDate.ClientID%>').value = date;
                    //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                    $('#picker').daterangepicker({
                        startDate: startDate.format("MM/DD/YYYY"),
                        endDate: endtDate.format("MM/DD/YYYY"),
                        ranges: {
                        },
                    },
            function (start, end) {
                debugger
                $('#date').html(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
            });

                });
            });
};
    </script>

    <script>

        $("#ctl00_ContentPlaceHolder1_Panel2").click(function () {
            var tab2 = $("[id*=TabName]").val("tab_2");//document.getElementById('<%= TabName.ClientID%>').value;
            //$('#Tabs a[href="' + tab1 + '"]').tab('show');
            //alert(tab2.val());

        });
        $("#ctl00_ContentPlaceHolder1_PanelCategory").click(function () {
            var tab3 = $("[id*=TabName]").val("tab_3");//document.getElementById('<%= TabName.ClientID%>').value;
            //alert(tab3.val());
            //$('#Tabs a[href="' + tab2 + '"]').tab('show');

        });
        $("#ctl00_ContentPlaceHolder1_PanelActivity").click(function () {
            var tab4 = $("[id*=TabName]").val("tab_4");//document.getElementById('<%= TabName.ClientID%>').value;
            //alert(tab4.val());
            //$('#Tabs a[href="' + tab3 + '"]').tab('show');

        });

        $('.nav-tabs a').on('shown.bs.tab', function () {
            $($.fn.dataTable.tables(true)).DataTable()
                   .columns.adjust();
        });
    </script>


    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdEvent" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfvCategory" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfvActivity" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfvYear" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfvLevel" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfvTechnical" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfvParticipation" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfvmooc" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfvDuration" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfvStatus" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidTAB" runat="server" Value="tab_1" />

    <asp:UpdatePanel ID="updBasicDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnLogo" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            </div>

                            <div class="box-body">

                                <div id="Tabs" role="tabpanel">
                                    <div id="divbasicconfig">
                                        <div class="nav-tabs-custom">
                                            <ul class="nav nav-tabs" role="tablist">
                                                <li class="nav-item">
                                                    <%--<a class="nav-link active" data-toggle="tab" href="#tab_1">Participation Level Name</a>--%>
                                                    <%--onserverclick="btnTab1_Click"--%>
                                                    <button id="btnTab1" runat="server" class="nav-link active" data-toggle="tab" href="#tab_1">Participation Level</button>
                                                </li>
                                                <li class="nav-item">
                                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_2" >Event Nature</a>--%>
                                                    <%--onserverclick="btnTab2_Click"--%>
                                                    <button id="btnTab2" runat="server" class="nav-link" data-toggle="tab" href="#tab_2">Event Nature</button>
                                                </li>
                                                <li class="nav-item">
                                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_3">Event Category</a>--%>
                                                    <button id="btnTab3" runat="server" onserverclick="btnTab3_Click" class="nav-link" data-toggle="tab" href="#tab_3">Event Category</button>
                                                    <%--<asp:Button ID="btnTab3" runat="server" OnClick="btnTab3_Click" class="nav-link" data-toggle="tab" href="#tab_3" Text="Event Category"></asp:Button>--%>
                                                    <%--<asp:LinkButton ID="btnTab3" runat="server" CssClass="nav-link" data-toggle="tab" data-target="#tab_3" OnClick="btnTab3_Click" ClientIDMode="Static">Event Category</asp:LinkButton>--%>
                                                </li>
                                                <li class="nav-item">
                                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_4">Activity Category</a>--%>
                                                    <button id="btnTab4" runat="server" onserverclick="btnTab4_Click" class="nav-link" data-toggle="tab" href="#tab_4">Activity Category</button>
                                                    <%--<asp:Button ID="btnTab4" runat="server" OnClick="btnTab4_Click" class="nav-link" data-toggle="tab" href="#tab_4" Text="Activity Category"></asp:Button>--%>
                                                </li>
                                                <li class="nav-item">
                                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_5">Academic Year</a>--%>
                                                    <button id="btnTab5" runat="server" class="nav-link" data-toggle="tab" href="#tab_5">Academic Year</button>
                                                </li>
                                                <li class="nav-item">
                                                    <%-- <a class="nav-link" data-toggle="tab" href="#tab_6">Event Level</a>--%>
                                                    <button id="btnTab6" runat="server" class="nav-link" data-toggle="tab" href="#tab_6">Event Level</button>
                                                </li>
                                                <li class="nav-item">
                                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_7">Technical Activity Type</a>--%>
                                                    <button id="btnTab7" runat="server" class="nav-link" data-toggle="tab" href="#tab_7">Technical Activity Type</button>

                                                </li>
                                                <li class="nav-item">
                                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_8">Participation Type</a>--%>
                                                    <button id="btnTab8" runat="server" class="nav-link" data-toggle="tab" href="#tab_8">Participation Type</button>

                                                </li>
                                                <li class="nav-item">
                                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_9">Mooc's Platform</a>--%>
                                                    <button id="btnTab9" runat="server" class="nav-link" data-toggle="tab" href="#tab_9">Mooc's Platform</button>

                                                </li>
                                                <li class="nav-item">
                                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_10">Duration</a>--%>
                                                    <button id="btnTab10" runat="server" class="nav-link" data-toggle="tab" href="#tab_10">Duration</button>
                                                </li>
                                            </ul>

                                            <div class="tab-content" id="my-tab-content">
                                                <div class="tab-pane active" id="tab_1">
                                                    <asp:UpdatePanel runat="server" ID="updp">
                                                        <ContentTemplate>
                                                            <div class="col-12 mt-3">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Participation Level Name</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtParticipationLevelName" runat="server" MaxLength="256" AutoComplete="off" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="rfvtxtParticipationLevelName" runat="server" ControlToValidate="txtParticipationLevelName"
                                                                            Display="None" ErrorMessage="Please Enter Participation Level Name" SetFocusOnError="True"
                                                                            ValidationGroup="PvalId"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True" TargetControlID="txtParticipationLevelName" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="(),- ">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdActiveParticipationLevel" name="switch" checked />
                                                                            <label data-on="Active" data-off="InActive" for="rdActiveParticipationLevel"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:LinkButton ID="btnSubmitParticipationLevel" runat="server" OnClientClick="return validate();" CssClass="btn btn-primary" ValidationGroup="PvalId" OnClick="btnSubmitParticipationLevel_Click">Submit</asp:LinkButton>
                                                                <asp:LinkButton ID="btnCancelParticipationLevel" runat="server" CssClass="btn btn-warning" OnClick="btnCancelParticipationLevel_Click">Cancel</asp:LinkButton>
                                                            </div>

                                                            <asp:ValidationSummary ID="PraticipationValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="PvalId" />

                                                            <div class="col-12 mt-3">

                                                                <div class="sub-heading">
                                                                    <h5>Participation Level List</h5>
                                                                </div>
                                                                <div class="table-responsive">

                                                                    <asp:Panel ID="pnlSession" runat="server" Visible="false">
                                                                        <asp:ListView ID="lvBascicDetails" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvBascicDetails_ItemEditing">
                                                                            <LayoutTemplate>
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Edit</th>
                                                                                            <th>Participation Level Name</th>
                                                                                            <th>Status</th>
                                                                                        </tr>
                                                                                    </thead>

                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel runat="server" ID="updParticipation">
                                                                                    <ContentTemplate>
                                                                                        <tr>

                                                                                            <td>
                                                                                                <asp:LinkButton ID="btnParticipationLevel" runat="server" CssClass="fas fa-edit" OnClick="btnParticipationLevel_Click" CommandArgument='<%#Eval("PARTICIPATION_LEVEL_ID") %>' CommandName="Edit"></asp:LinkButton>

                                                                                            </td>

                                                                                            <td>
                                                                                                <%# Eval("PARTICIPATION_LEVEL_NAME") %>
                                                                                            </td>
                                                                                            <td>

                                                                                                <%-- <span class="badge badge-success"> <%# Eval("ACTIVE_STATUS") %></span>--%>
                                                                                                <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                                            </td>

                                                                                        </tr>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>

                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSubmitParticipationLevel" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnCancelParticipationLevel" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="tab-pane fade" id="tab_2">
                                                    <asp:UpdatePanel runat="server" ID="upde">
                                                        <ContentTemplate>
                                                            <div class="col-12 mt-3">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Event Nature</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtEventNature" runat="server" MaxLength="64" AutoComplete="off" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="rfvtxtEventNature" runat="server" ControlToValidate="txtEventNature"
                                                                            Display="None" ErrorMessage="Please Enter Event Nature" SetFocusOnError="True"
                                                                            ValidationGroup="EvalId"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtEventNature" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="(),- ">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdEventNature" name="switch" checked />
                                                                            <label data-on="Active" data-off="InActive" for="rdEventNature"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:LinkButton ID="btnSubmitEventNature" runat="server" CssClass="btn btn-primary" OnClientClick="return validateEvent();" ValidationGroup="EvalId" OnClick="btnSubmitEventNature_Click">Submit</asp:LinkButton>
                                                                <asp:LinkButton ID="btnCancelEventNature" runat="server" CssClass="btn btn-warning" OnClick="btnCancelEventNature_Click">Cancel</asp:LinkButton>
                                                            </div>

                                                            <asp:ValidationSummary ID="EvventValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="EvalId" />

                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Event Nature List</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                                        <asp:ListView ID="lvEvent" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvEvent_ItemEditing1">
                                                                            <LayoutTemplate>
                                                                                <table class="table table-striped table-bordered nowrap" style="table-layout: fixed">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Edit</th>
                                                                                            <th>Event Nature</th>
                                                                                            <th>Status</th>
                                                                                        </tr>
                                                                                    </thead>

                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </LayoutTemplate>

                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel runat="server" ID="updEventNature">
                                                                                    <ContentTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:LinkButton ID="btnEventEdit" runat="server" CssClass="fas fa-edit" OnClick="btnEventEdit_Click" CommandArgument='<%#Eval("EVENT_NATURE_ID") %>' CommandName="Edit"></asp:LinkButton>
                                                                                            </td>

                                                                                            <td>
                                                                                                <%# Eval("EVENT_NATURE") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblEStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                                                <%-- <span class="badge badge-success"><%# Eval("ACTIVE_STATUS") %></span>--%>
                                                                                            </td>

                                                                                        </tr>
                                                                                    </ContentTemplate>
                                                                                    <%--<Triggers>


                                                                    <asp:PostBackTrigger ControlID="btnEventEdit" />
                                                                </Triggers>--%>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>

                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSubmitEventNature" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnCancelEventNature" />

                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="tab-pane fade" id="tab_3">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                        <ContentTemplate>
                                                            <div class="col-12 mt-3">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Event Category Name</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtEventCategory" runat="server" AutoComplete="off" MaxLength="256" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="rfvtxtEventCategory" runat="server" ControlToValidate="txtEventCategory"
                                                                            Display="None" ErrorMessage="Please Enter Event Category Name" SetFocusOnError="True"
                                                                            ValidationGroup="CategoryValidationId"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="txtEventCategory" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="(),- ">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>

                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Event Nature</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlEventNature" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <asp:RequiredFieldValidator ID="rfcddlEventNature" runat="server" ControlToValidate="ddlEventNature"
                                                                            Display="None" ErrorMessage="Please Select Event Nature" SetFocusOnError="True"
                                                                            ValidationGroup="CategoryValidationId" InitialValue="0"></asp:RequiredFieldValidator>


                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdEventCategory" name="switch" checked />
                                                                            <label data-on="Active" data-off="InActive" for="rdEventCategory"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:LinkButton ID="btnSubmitEventCategory" runat="server" CssClass="btn btn-primary" ValidationGroup="CategoryValidationId" OnClientClick="return validateCategory();" OnClick="btnSubmitEventCategory_Click">Submit</asp:LinkButton>
                                                                <asp:LinkButton ID="btnCancelEventCategory" runat="server" CssClass="btn btn-warning" OnClick="btnCancelEventCategory_Click">Cancel</asp:LinkButton>

                                                            </div>
                                                            <asp:ValidationSummary ID="CategoryValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="CategoryValidationId" />

                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Event Category List</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <asp:Panel ID="PanelCategory" runat="server" Visible="false">
                                                                        <asp:ListView ID="lvCategory" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="ListView1_ItemEditing">
                                                                            <LayoutTemplate>
                                                                                <table class="table table-striped table-bordered nowrap" style="table-layout: fixed">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Edit</th>
                                                                                            <th>Event Category Name</th>
                                                                                            <th>Event Nature</th>
                                                                                            <th>Status</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel runat="server" ID="updEventCategory">
                                                                                    <ContentTemplate>
                                                                                        <tr>
                                                                                            <td>

                                                                                                <asp:LinkButton ID="btneditEventCategory" runat="server" CssClass="fas fa-edit" OnClick="btneditEventCategory_Click" CommandArgument='<%# Eval("EVENT_CATEGORY_ID") %>' CommandName="Edit"></asp:LinkButton>

                                                                                            </td>
                                                                                            <td><%# Eval("EVENT_CATEGORY_NAME") %></td>
                                                                                            <td><%# Eval("EVENT_NATURE") %></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblCStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                                            </td>
                                                                                            <%--  <td><%# Eval("ACTIVE_STATUS") %></span></td>--%>
                                                                                        </tr>

                                                                                    </ContentTemplate>
                                                                                    <%--<Triggers>
                                                                 <asp:PostBackTrigger ControlID ="btneditEventCategory"/>
                                                                
                                                             </Triggers>--%>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSubmitEventCategory" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnCancelEventCategory" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="tab-pane fade" id="tab_4">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                                        <ContentTemplate>
                                                            <div class="col-12 mt-3">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Activity Category</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlEventCatergory" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <asp:RequiredFieldValidator ID="rfvddlEventCatergory" runat="server" ControlToValidate="ddlEventCatergory"
                                                                            Display="None" ErrorMessage="Please Select Activity Catergory" SetFocusOnError="True"
                                                                            ValidationGroup="ActivityValidationId" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Activity Category Name</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtActivityCategory" runat="server" AutoComplete="off" MaxLength="256" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="rfctxtActivityCategory" runat="server" ControlToValidate="txtActivityCategory"
                                                                            Display="None" ErrorMessage="Please Enter Activity Category Name" SetFocusOnError="True"
                                                                            ValidationGroup="ActivityValidationId"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" TargetControlID="txtActivityCategory" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="(),- ">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdActivity" name="switch" checked />
                                                                            <label data-on="Active" data-off="InActive" for="rdActivity"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:LinkButton ID="btnSubmitActivityCategory" runat="server" CssClass="btn btn-primary" OnClick="btnSubmitActivityCategory_Click1" OnClientClick="return validateActivityCategory();" ValidationGroup="ActivityValidationId">Submit</asp:LinkButton>

                                                                <asp:LinkButton ID="btnCancelActivityCategory" runat="server" CssClass="btn btn-warning" OnClick="btnCancelActivityCategory_Click">Cancel</asp:LinkButton>

                                                            </div>
                                                            <asp:ValidationSummary ID="ActivityValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="ActivityValidationId" />

                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Activity Category List</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <asp:Panel ID="PanelActivity" runat="server" Visible="false">
                                                                        <asp:ListView ID="lvActivity" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="ListView1_ItemEditing">
                                                                            <LayoutTemplate>
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Edit</th>
                                                                                            <th>Event Category</th>
                                                                                            <th>Activity Category Name</th>
                                                                                            <th>Status</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel runat="server" ID="updEventCategory">
                                                                                    <ContentTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:LinkButton ID="btnEditActivity" runat="server" OnClick="btnEditActivity_Click" CssClass="fas fa-edit" CommandArgument='<%# Eval("ACTIVITY_CATEGORY_ID") %>' CommandName="Edit"></asp:LinkButton>
                                                                                            </td>

                                                                                            <td><%# Eval("EVENT_CATEGORY_NAME") %></td>
                                                                                            <td><%# Eval("ACTIVITY_CATEGORY_NAME") %></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblAStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                                            </td>


                                                                                    </ContentTemplate>
                                                                                    <%-- <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnEditActivity" />

                                                                </Triggers>--%>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>

                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSubmitActivityCategory" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnCancelActivityCategory" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="tab-pane fade" id="tab_5">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                                        <ContentTemplate>
                                                            <div class="col-12 mt-3">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Academic Year Name</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtAcademicYear" MaxLength="256" AutoComplete="off" runat="server" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="rfvtxtAcademicYear" runat="server" ControlToValidate="txtAcademicYear"
                                                                            Display="None" ErrorMessage="Please Enter Academic Year Name" SetFocusOnError="True"
                                                                            ValidationGroup="AcademicValidationId"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                                            TargetControlID="txtAcademicYear" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                                            FilterMode="ValidChars" ValidChars="0123456789(),- ">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Start End Date</label>

                                                                        </div>
                                                                        <asp:HiddenField ID="hdnDate" runat="server" />
                                                                        <div id="picker" class="form-control">
                                                                            <i class="fa fa-calendar"></i>&nbsp;
                                                                            <span id="date"></span>
                                                                            <%--<label id="date" runat="server" />--%>
                                                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                                        </div>


                                                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label>Session Start Date</label>
                                                                            </div>
                                                                            <div class="input-group date">
                                                                                <div class="input-group-addon">
                                                                                    <i id="dvcal1" runat="server" class="fa fa-calendar text-blue"></i>
                                                                                </div>
                                                                                <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="submit" onpaste="return false;"
                                                                                    TabIndex="3" ToolTip="Please Enter Session Start Date" CssClass="form-control" Style="z-index: 0;" />
                                                                                <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                                                    TargetControlID="txtStartDate" PopupButtonID="dvcal1" />
                                                                                <%-- <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                Display="None" ErrorMessage="Please Enter Session Start Date" SetFocusOnError="True"
                                                ValidationGroup="submit" />--%>
                                                                                <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                                                    TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                                <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                                                    ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter Start Date"
                                                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                                    TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                                    ValidationGroup="submit" SetFocusOnError="True" />
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label>Session End Date</label>
                                                                            </div>
                                                                            <div class="input-group date">
                                                                                <div class="input-group-addon">
                                                                                    <i id="dvcal2" runat="server" class="fa fa-calendar text-blue"></i>
                                                                                </div>
                                                                                <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="AcademicValidationId" TabIndex="4"
                                                                                    ToolTip="Please Enter Session End Date" CssClass="form-control" Style="z-index: 0;" />
                                                                                <%-- <asp:Image ID="imgEDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                    AlternateText="Select Date" Style="cursor: pointer" />--%>
                                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                                    TargetControlID="txtEndDate" PopupButtonID="dvcal2" />
                                                                                <%-- <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                                ErrorMessage="Please Enter Session End Date" ControlToValidate="txtEndDate" Display="None"
                                                ValidationGroup="submit" />--%>
                                                                                <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                                                                    TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                                <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                                                                    ControlToValidate="txtEndDate" EmptyValueMessage="Please Enter Session End Date"
                                                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                                    TooltipMessage="Please Enter Session End Date" EmptyValueBlurredText="Empty"
                                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="AcademicValidationId" SetFocusOnError="True" />
                                                                            </div>
                                                                        </div>


                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label></label>
                                                                        </div>
                                                                        <asp:CheckBox ID="chkDefault" runat="server" Visible="false" Text="Default" Checked />
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdAcademicYear" name="switch" checked />
                                                                            <label data-on="Active" data-off="InActive" for="rdAcademicYear"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:LinkButton ID="btnSubmitAcademicYear" runat="server" CssClass="btn btn-primary" OnClick="btnSubmitAcademicYear_Click" OnClientClick="return valiAcademic();" ValidationGroup="AcademicValidationId">Submit</asp:LinkButton>
                                                                <asp:LinkButton ID="btnCancelAcademicYear" runat="server" CssClass="btn btn-warning" OnClick="btnCancelAcademicYear_Click">Cancel</asp:LinkButton>
                                                            </div>
                                                            <asp:ValidationSummary ID="AcademicValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="AcademicValidationId" />
                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Academic Year List</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <asp:Panel ID="PanelAcademic" runat="server" Visible="false">
                                                                        <asp:ListView ID="lvAcademic" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="ListView1_ItemEditing">
                                                                            <LayoutTemplate>
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Edit</th>
                                                                                            <th>Academic Year Name</th>
                                                                                            <th>Start Date</th>
                                                                                            <th>End Date</th>
                                                                                            <th>Status</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel runat="server" ID="updEventCategory">
                                                                                    <ContentTemplate>

                                                                                        <tr>

                                                                                            <td>
                                                                                                <asp:LinkButton ID="btnEditAcademicYear" runat="server" OnClick="btnEditAcademicYear_Click" CssClass="fas fa-edit" CommandArgument='<%# Eval("ACADMIC_YEAR_ID") %>' CommandName="Edit"></asp:LinkButton>
                                                                                            </td>
                                                                                            <td><%# Eval("ACADMIC_YEAR_NAME") %></td>
                                                                                            <td><%# Convert.ToDateTime (Eval("STDATE")).ToString("d") %></td>
                                                                                            <td><%# Convert.ToDateTime (Eval("ENDDATE")).ToString("d") %></td>

                                                                                            <td>
                                                                                                <asp:Label ID="lblAcademicStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                                            </td>

                                                                                    </ContentTemplate>
                                                                                    <%-- <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnEditAcademicYear" />

                                                                </Triggers>--%>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>

                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSubmitAcademicYear" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnCancelAcademicYear" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="tab-pane fade" id="tab_6">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                                        <ContentTemplate>
                                                            <div class="col-12 mt-3">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Event Level</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtEventLevel" runat="server" AutoComplete="off" MaxLength="256" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="rfctxtEventLevel" runat="server" ControlToValidate="txtEventLevel"
                                                                            Display="None" ErrorMessage="Please Enter Event Level" SetFocusOnError="True"
                                                                            ValidationGroup="EventLevelValidationId"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" TargetControlID="txtEventLevel" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="(),- ">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdEventLevel" name="switch" checked />
                                                                            <label data-on="Active" data-off="InActive" for="rdEventLevel"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:LinkButton ID="btnSubmitEventLevel" runat="server" CssClass="btn btn-primary" OnClientClick="return validateEventLevel();" ValidationGroup="EventLevelValidationId" OnClick="btnSubmitEventLevel_Click">Submit</asp:LinkButton>
                                                                <asp:LinkButton ID="btnCancelEventLevel" runat="server" CssClass="btn btn-warning" OnClick="btnCancelEventLevel_Click">Cancel</asp:LinkButton>
                                                            </div>
                                                            <asp:ValidationSummary ID="EventLevelValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="EventLevelValidationId" />
                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Event Level List</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <asp:Panel ID="panelEvent" runat="server" Visible="false">
                                                                        <asp:ListView ID="lvEventLevel" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="ListView1_ItemEditing">
                                                                            <LayoutTemplate>
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Edit</th>
                                                                                            <th>Event Level</th>
                                                                                            <th>Status</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel runat="server" ID="updEventLevel">
                                                                                    <ContentTemplate>

                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:LinkButton ID="btnEditEventLevel" runat="server" CssClass="fas fa-edit" OnClick="btnEditEventLevel_Click" CommandArgument='<%#Eval("EVENT_LEVEL_ID") %>' CommandName="Edit"></asp:LinkButton>
                                                                                            </td>
                                                                                            <td><%# Eval("EVENT_LEVEL") %></td>



                                                                                            <td>
                                                                                                <asp:Label ID="lblEventStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ContentTemplate>
                                                                                    <%-- <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnEditEventLevel" />

                                                                </Triggers>--%>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>


                                                                        </asp:ListView>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>

                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSubmitEventLevel" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnCancelEventLevel" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="tab-pane fade" id="tab_7">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                                        <ContentTemplate>
                                                            <div class="col-12 mt-3">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Technical Activity Type</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtTechnicalActivity" runat="server" AutoComplete="off" MaxLength="256" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="rfctxtTechnicalActivity" runat="server" ControlToValidate="txtTechnicalActivity"
                                                                            Display="None" ErrorMessage="Please Enter Technical Activity Type" SetFocusOnError="True"
                                                                            ValidationGroup="TechnicalValidationId"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True" TargetControlID="txtTechnicalActivity" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="(),- ">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdTechnicalActivity" name="switch" checked />
                                                                            <label data-on="Active" data-off="InActive" for="rdTechnicalActivity"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:LinkButton ID="btnSubmitTechnicalActivity" runat="server" CssClass="btn btn-primary" ValidationGroup="TechnicalValidationId" OnClientClick="return validateTecnical();" OnClick="btnSubmitTechnicalActivity_Click">Submit</asp:LinkButton>
                                                                <asp:LinkButton ID="btnCancelTechnicalActivity" runat="server" CssClass="btn btn-warning" OnClick="btnCancelTechnicalActivity_Click">Cancel</asp:LinkButton>
                                                            </div>
                                                            <asp:ValidationSummary ID="TechnicalValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="TechnicalValidationId" />
                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Technical Activity List</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <asp:Panel ID="panelTec" runat="server" Visible="false">
                                                                        <asp:ListView ID="lvTechnical" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="ListView1_ItemEditing">
                                                                            <LayoutTemplate>
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Edit</th>
                                                                                            <th>Technical Activity Type</th>
                                                                                            <th>Status</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel runat="server" ID="updTechnical">
                                                                                    <ContentTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:LinkButton ID="btnEditTechnical" runat="server" CssClass="fas fa-edit" OnClick="btnEditTechnical_Click" CommandArgument='<%#Eval("TECHNICALACTIVITY_TYPE_ID") %>' CommandName="Edit"></asp:LinkButton>
                                                                                            </td>
                                                                                            <td><%# Eval("TECHNICALACTIVITY_TYPE") %></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblTStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ContentTemplate>
                                                                                    <%--<Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnEditTechnical" />

                                                                </Triggers>--%>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>


                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSubmitTechnicalActivity" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnCancelTechnicalActivity" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="tab-pane fade" id="tab_8">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                                        <ContentTemplate>
                                                            <div class="col-12 mt-3">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Participation Type</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtParticipation" runat="server" AutoComplete="off" MaxLength="256" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="rfvtxtParticipation" runat="server" ControlToValidate="txtParticipation"
                                                                            Display="None" ErrorMessage="Please Enter Participation Type" SetFocusOnError="True"
                                                                            ValidationGroup="ParticipationValidationId"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True" TargetControlID="txtParticipation" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="(),- ">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdParticipation" name="switch" checked />
                                                                            <label data-on="Active" data-off="InActive" for="rdParticipation"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:LinkButton ID="btnSubmitParticipation" runat="server" CssClass="btn btn-primary" ValidationGroup="ParticipationValidationId" OnClientClick="return valiParticipation();" OnClick="btnSubmitParticipation_Click">Submit</asp:LinkButton>
                                                                <asp:LinkButton ID="btnCancelParticipation" runat="server" CssClass="btn btn-warning" OnClick="btnCancelParticipation_Click">Cancel</asp:LinkButton>
                                                            </div>
                                                            <asp:ValidationSummary ID="ParticipationValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="ParticipationValidationId" />
                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Participation List</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <asp:Panel ID="panelParticipation" runat="server" Visible="false">
                                                                        <asp:ListView ID="lvParticipation" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="ListView1_ItemEditing">
                                                                            <LayoutTemplate>
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Edit</th>
                                                                                            <th>Participation Type</th>
                                                                                            <th>Status</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel runat="server" ID="updParticipation">
                                                                                    <ContentTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:LinkButton ID="btnEditParticipation" runat="server" CssClass="fas fa-edit" OnClick="btnEditParticipation_Click" CommandArgument='<%#Eval("PARTICIPATION_TYPE_ID") %>' CommandName="Edit"></asp:LinkButton>
                                                                                            </td>
                                                                                            <td><%# Eval("PARTICIPATION_TYPE") %></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblParticipationStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ContentTemplate>
                                                                                    <%--<Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnEditParticipation" />

                                                                </Triggers>--%>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSubmitParticipation" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnCancelParticipation" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="tab-pane fade" id="tab_9">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel7">
                                                        <ContentTemplate>
                                                            <div class="col-12 mt-3">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Mooc's Platform</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtMoocsPlatform" runat="server" AutoComplete="off" MaxLength="256" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="rfvtxtMoocsPlatform" runat="server" ControlToValidate="txtMoocsPlatform"
                                                                            Display="None" ErrorMessage="Please Enter Moocs Platform" SetFocusOnError="True"
                                                                            ValidationGroup="MoocsValidationId"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True" TargetControlID="txtMoocsPlatform" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="(),- ">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdMoocsPlatform" name="switch" checked />
                                                                            <label data-on="Active" data-off="InActive" for="rdMoocsPlatform"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:LinkButton ID="btnSubmitMoocsPlatform" runat="server" CssClass="btn btn-primary" ValidationGroup="MoocsValidationId" OnClientClick="return valiMoocs();" OnClick="btnSubmitMoocsPlatform_Click">Submit</asp:LinkButton>
                                                                <asp:LinkButton ID="btnCancelMoocsPlatform" runat="server" CssClass="btn btn-warning" OnClick="btnCancelMoocsPlatform_Click">Cancel</asp:LinkButton>
                                                            </div>
                                                            <asp:ValidationSummary ID="MoocsValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="MoocsValidationId" />
                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Mooc's Platform List</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <asp:Panel ID="panelMoocs" runat="server" Visible="false">
                                                                        <asp:ListView ID="lvMoocs" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="ListView1_ItemEditing">
                                                                            <LayoutTemplate>
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Edit</th>
                                                                                            <th>Mooc's Platform</th>
                                                                                            <th>Status</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel runat="server" ID="updMoocs">
                                                                                    <ContentTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:LinkButton ID="btnEditMoocs" runat="server" CssClass="fas fa-edit" OnClick="btnEditMoocs_Click" CommandArgument='<%#Eval("MOOCS_PLATFORM_ID") %>' CommandName="Edit"></asp:LinkButton>
                                                                                            </td>
                                                                                            <td><%# Eval("MOOCS_PLATFORM") %></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblMoocsStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                        </tr>
                                                                                    </ContentTemplate>
                                                                                    <%-- <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnEditMoocs" />

                                                                </Triggers>--%>
                                                                                </asp:UpdatePanel>

                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>


                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSubmitMoocsPlatform" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnCancelMoocsPlatform" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="tab-pane fade" id="tab_10">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                                                        <ContentTemplate>
                                                            <div class="col-12 mt-3">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Duration</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtDuration" runat="server" AutoComplete="off" MaxLength="256" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="rfvtxtDuration" runat="server" ControlToValidate="txtDuration"
                                                                            Display="None" ErrorMessage="Please Enter Duration" SetFocusOnError="True"
                                                                            ValidationGroup="DurationValidationId"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True" TargetControlID="txtDuration" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="0123456789(),- ">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdDuration" name="switch" checked />
                                                                            <label data-on="Active" data-off="InActive" for="rdDuration"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:LinkButton ID="btnSubmitDuration" runat="server" CssClass="btn btn-primary" ValidationGroup="DurationValidationId" OnClientClick="return valiDuration();" OnClick="btnSubmitDuration_Click">Submit</asp:LinkButton>
                                                                <asp:LinkButton ID="btnCancelDuration" runat="server" CssClass="btn btn-warning" OnClick="btnCancelDuration_Click">Cancel</asp:LinkButton>
                                                            </div>
                                                            <asp:ValidationSummary ID="DurationValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="DurationValidationId" />
                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Duration List</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <asp:Panel ID="panelDuration" runat="server" Visible="false">
                                                                        <asp:ListView ID="lvDuration" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="ListView1_ItemEditing">
                                                                            <LayoutTemplate>
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Edit</th>
                                                                                            <th>Duration</th>
                                                                                            <th>Status</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel runat="server" ID="updDuration">
                                                                                    <ContentTemplate>
                                                                                        <tr>
                                                                                            <td>

                                                                                                <asp:LinkButton ID="btnEditDuration" runat="server" OnClick="btnEditDuration_Click" CssClass="fas fa-edit" CommandArgument='<%#Eval("DURATION_ID") %>' CommandName="Edit"></asp:LinkButton>
                                                                                            </td>

                                                                                            <td><%# Eval("DURATION")%></td>

                                                                                            <td>
                                                                                                <asp:Label ID="lblDurationStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>


                                                                                    </ContentTemplate>
                                                                                    <%-- <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnEditDuration" />

                                                                </Triggers>--%>
                                                                                </asp:UpdatePanel>

                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>


                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSubmitDuration" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnCancelDuration" />


                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="TabName" runat="server" />
        </ContentTemplate>

        <Triggers>
            <%--<asp:PostBackTrigger ControlID="ddlEventCatergory"/>--%>
        </Triggers>
    </asp:UpdatePanel>

    <!-- Start End Date Script -->
    <script>

        function TabShow(tabName) {
            //alert('hii' + tabName);
            //var tabName = "tab_2";
            $('#Tabs button[href="#' + tabName + '"]').tab('show');
            $("#Tabs button").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
        });
    </script>

    <script>
        function SetParticipation(val) {

            $('#rdActiveParticipationLevel').prop('checked', val);

        }
        function validate() {


            $('#hfdActive').val($('#rdActiveParticipationLevel').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitParticipationLevel').click(function () {
                    validate();
                });
            });
        });
    </script>

    <script>
        function SetEventNature(val) {

            $('#rdEventNature').prop('checked', val);
        }
        function validateEvent() {

            $('#hfdEvent').val($('#rdEventNature').prop('checked'));

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitEventNature').click(function () {
                    validate();
                });
            });
        });

    </script>

    <script>
        function SetCategory(val) {

            $('#rdEventCategory').prop('checked', val);
        }

        function validateCategory() {

            $('#hfvCategory').val($('#rdEventCategory').prop('checked'));

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitEventCategory').click(function () {
                    validate();
                });
            });
        });
    </script>

    <script>
        function SetActivity(val) {

            $('#rdActivity').prop('checked', val);
        }

        function validateActivityCategory() {

            $('#hfvActivity').val($('#rdActivity').prop('checked'));

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitActivityCategory').click(function () {
                    validate();
                });
            });
        });
    </script>

    <script>
        function SetEventLevel(val) {

            $('#rdEventLevel').prop('checked', val);
        }

        function validateEventLevel() {

            $('#hfvLevel').val($('#rdEventLevel').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitEventLevel').click(function () {
                    validate();
                });
            });
        });
    </script>

    <script>
        function SetTechnical(val) {

            $('#rdTechnicalActivity').prop('checked', val);
        }
        function validateTecnical() {

            $('#hfvTechnical').val($('#rdTechnicalActivity').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitTechnicalActivity').click(function () {
                    validate();
                });
            });
        });
    </script>

    <script>
        function setParticipation(val) {

            $('#rdParticipation').prop('checked', val);
        }

        function valiParticipation() {

            $('#hfvParticipation').val($('#rdParticipation').prop('checked'));




        }



        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitParticipation').click(function () {
                    validate();
                });
            });
        });
    </script>

    <script>
        function setMoocs(val) {

            $('#rdMoocsPlatform').prop('checked', val);
        }

        function valiMoocs() {

            $('#hfvmooc').val($('#rdMoocsPlatform').prop('checked'));





        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitMoocsPlatform').click(function () {
                    validate();
                });
            });
        });
    </script>

    <script>
        function setDuration(val) {

            $('#rdDuration').prop('checked', val);
        }

        function valiDuration() {

            $('#hfvDuration').val($('#rdDuration').prop('checked'));

        }






        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitDuration').click(function () {
                    validate();
                });
            });
        });
    </script>

    <script>
        function setAcademic(val) {

            $('#rdAcademicYear').prop('checked', val);
        }

        function valiAcademic() {

            $('#hfvYear').val($('#rdAcademicYear').prop('checked'));


        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitAcademicYear').click(function () {
                    valiAcademic();
                });
            });
        });
    </script>

</asp:Content>

