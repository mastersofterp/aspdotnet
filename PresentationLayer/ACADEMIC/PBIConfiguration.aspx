<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PBIConfiguration.aspx.cs" Inherits="PBIConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
        .nav-tabs-custom .nav-link {
            padding: 0.5rem 0.3rem;
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
        $(function () {

            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "tab_1";

            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {

                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
                //$("[id*=TabName]").val();
            });
        });

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
        $('.nav-tabs a').on('shown.bs.tab', function () {
            $($.fn.dataTable.tables(true)).DataTable()
                   .columns.adjust();
        });


    </script>


    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfStatusubworkspace" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfStatuspbi" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="TabName" runat="server" />
    <asp:HiddenField ID="hidTAB" runat="server" Value="tab_1" />

    <asp:UpdatePanel ID="updPbiConfiguration" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnLogo" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">PBI Configuration</h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1">Workspace Master</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2">Sub Workspace Master</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_3">PBI Link Configuration</a>
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
                                                                <label>Workspace Name </label>
                                                            </div>
                                                            <asp:TextBox ID="txtWorkspaceName" runat="server" MaxLength="256" AutoComplete="off" CssClass="form-control" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True" TargetControlID="txtWorkspaceName" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="(),- ">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvtxtWorkspaceName" runat="server" ControlToValidate="txtWorkspaceName"
                                                                Display="None" ErrorMessage="Please Enter Workspace Name" SetFocusOnError="True"
                                                                ValidationGroup="WorkspaceValidation"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Status</label>
                                                            </div>
                                                            <div class="switch form-inline">
                                                                <input type="checkbox" id="rdActiveWorkspace" name="switch" />
                                                                <label data-on="Active" data-off="Inactive" for="rdActiveWorkspace"></label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:LinkButton ID="btnSubmitWorkspace" runat="server" CssClass="btn btn-primary" OnClick="btnSubmitWorkspace_Click" ValidationGroup="WorkspaceValidation" OnClientClick="return validate();">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancelWorkspace" runat="server" CssClass="btn btn-warning" OnClick="btnCancelWorkspace_Click">Cancel</asp:LinkButton>
                                                </div>

                                                <asp:ValidationSummary ID="WorkspaceValidationId" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="WorkspaceValidation" />

                                                <div class="col-12 mt-3">

                                                    <div class="sub-heading">
                                                        <h5>Workspace Master List</h5>
                                                    </div>
                                                    <div class="table-responsive">

                                                        <asp:Panel ID="PanelWorkapce" runat="server" Visible="false">
                                                            <asp:ListView ID="lvWorkapce" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvConfiguration_ItemEditing">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit
                                                                                </th>
                                                                                <th>WORKSPACE_NAME
                                                                                </th>
                                                                                <th>Status
                                                                                </th>
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
                                                                                    <asp:LinkButton ID="btnEditWorkspace" runat="server" CssClass="fas fa-edit" OnClick="btnEditWorkspace_Click" CommandArgument='<%#Eval("WORKSPACE_ID") %>' CommandName="Edit"></asp:LinkButton>

                                                                                </td>

                                                                                <td>
                                                                                    <%# Eval("WORKSPACE_NAME")%>
                                                                                </td>
                                                                                <td>

                                                                                    <%-- <span class="badge badge-success"> <%# Eval("ACTIVE_STATUS") %></span>--%>
                                                                                    <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("STATUS") %>'></asp:Label>
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
                                                <asp:AsyncPostBackTrigger ControlID="btnSubmitWorkspace" />
                                                <asp:AsyncPostBackTrigger ControlID="btnCancelWorkspace" />
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
                                                                <label>Workspace Name </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlWorkspaceName" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfcddlWorkspaceName" runat="server" ControlToValidate="ddlWorkspaceName"
                                                                Display="None" ErrorMessage="Please Select Workspace Name" SetFocusOnError="True"
                                                                ValidationGroup="SubWorkspaceValidation" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Sub Workspace </label>
                                                            </div>
                                                            <asp:TextBox ID="txtSubWorkspace" runat="server" MaxLength="256" AutoComplete="off" CssClass="form-control" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtSubWorkspace" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="(),- ">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfSubWorkspace" runat="server" ControlToValidate="txtSubWorkspace"
                                                                Display="None" ErrorMessage="Please Enter Sub Workspace" SetFocusOnError="True"
                                                                ValidationGroup="SubWorkspaceValidation"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Status</label>
                                                            </div>
                                                            <div class="switch form-inline">
                                                                <input type="checkbox" id="rdSubworkspace" name="switch" />
                                                                <label data-on="Active" data-off="Inactive" for="rdSubworkspace"></label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:LinkButton ID="btnSubmiteSubWorkspace" runat="server" CssClass="btn btn-primary" ValidationGroup="SubWorkspaceValidation" OnClientClick="return validateSubworkspace();" OnClick="btnSubmiteSubWorkspace_Click">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancelSubWorkspace" runat="server" CssClass="btn btn-warning" OnClick="btnCancelSubWorkspace_Click">Cancel</asp:LinkButton>
                                                </div>

                                                <asp:ValidationSummary ID="SubWorkspaceValidationId" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="SubWorkspaceValidation" />

                                                <div class="col-12 mt-3">
                                                    <div class="sub-heading">
                                                        <h5>Sub Workspace List</h5>
                                                    </div>
                                                    <div class="table-responsive">
                                                        <asp:Panel ID="panelSubWorkspace" runat="server" Visible="false">
                                                            <asp:ListView ID="lvSubWorkspace" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvEvent_ItemEditing">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit
                                                                                </th>
                                                                                <th>Workspace Name
                                                                                </th>
                                                                                <th>Sub Worksapce Name
                                                                                </th>
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
                                                                                    <asp:LinkButton ID="btnEditSubworkspace" runat="server" CssClass="fas fa-edit" OnClick="btnEditSubworkspace_Click" CommandArgument='<%#Eval("SUB_WORKSPACE_ID") %>' CommandName="Edit"></asp:LinkButton>
                                                                                </td>

                                                                                <td>
                                                                                    <%# Eval("WORKSPACE_NAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("SUB_WORKSPACE_NAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblWStatus" runat="server" CssClass="badge" Text='<%# Eval("STATUS") %>'></asp:Label>

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
                                                <asp:AsyncPostBackTrigger ControlID="btnSubmiteSubWorkspace" />
                                                <asp:AsyncPostBackTrigger ControlID="btnCancelSubWorkspace" />

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
                                                                <label>Workspace </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlworkspace" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlworkspace_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:RequiredFieldValidator ID="rfddlworkspace" runat="server" ControlToValidate="ddlworkspace"
                                                                Display="None" ErrorMessage="Please Select Workspace" SetFocusOnError="True"
                                                                ValidationGroup="ValidationPbilink" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Sub Workspace </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSubWorkspace" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:RequiredFieldValidator ID="rfddlSubWorkspace" runat="server" ControlToValidate="ddlSubWorkspace"
                                                                Display="None" ErrorMessage="Please Select Sub Workspace" SetFocusOnError="True"
                                                                ValidationGroup="ValidationPbilink" InitialValue="0"></asp:RequiredFieldValidator>


                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Status</label>
                                                            </div>
                                                            <div class="switch form-inline">
                                                                <input type="checkbox" id="rdPbiActive" name="switch" />
                                                                <label data-on="Active" data-off="Inactive" for="rdPbiActive"></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>PBI Link Name </label>

                                                                <asp:TextBox ID="txtPbiLink" runat="server" AutoComplete="off" CssClass="form-control" />
                                                                <asp:RequiredFieldValidator ID="rftxtPbiLink" runat="server" ControlToValidate="txtPbiLink"
                                                                    Display="None" ErrorMessage="Please Enter PBI Link Name" SetFocusOnError="True"
                                                                    ValidationGroup="ValidationPbilink"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:LinkButton ID="btnSubmitPbilink" runat="server" CssClass="btn btn-primary" ValidationGroup="ValidationPbilink" OnClientClick="return validatepbiLink();" OnClick="btnSubmitPbilink_Click">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancelpbiLink" runat="server" CssClass="btn btn-warning" OnClick="btnCancelpbiLink_Click">Cancel</asp:LinkButton>

                                                    <asp:ValidationSummary ID="ValidationPbilinkId" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="ValidationPbilink" />
                                                </div>

                                                <div class="col-12 mt-3">
                                                    <div class="sub-heading">
                                                        <h5>PBI Link Configuration List</h5>
                                                    </div>
                                                    <div class="table-responsive">
                                                        <asp:Panel ID="PanelPbiLink" runat="server" Visible="false">
                                                            <asp:ListView ID="lvPbiLink" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvCategory_ItemEditing">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit
                                                                                </th>
                                                                                <th style="width: 10%;">Workspace Name
                                                                                </th>
                                                                                <th style="width: 20%;">Sub Worksapce Name
                                                                                </th>
                                                                                <th style="width: 60%;">PBI Link Name
                                                                                </th>
                                                                                <th style="width: 10%;">Status
                                                                                </th>
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

                                                                                    <asp:LinkButton ID="btnEditPbi" runat="server" CssClass="fas fa-edit" OnClick="btnEditPbi_Click" CommandArgument='<%#Eval("PBI_LINK_CONFIGRATION_ID") %>' CommandName="Edit"></asp:LinkButton>

                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("WORKSPACE_NAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("SUB_WORKSPACE_NAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("PBI_LINK_NAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblPStatus" runat="server" CssClass="badge" Text='<%# Eval("STATUS") %>'></asp:Label>
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
                                                <asp:AsyncPostBackTrigger ControlID="btnSubmitPbilink" />
                                                <asp:AsyncPostBackTrigger ControlID="btnCancelpbiLink" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function SetWorkspace(val) {

            $('#rdActiveWorkspace').prop('checked', val);

        }
        function validate() {

            $('#hfdActive').val($('#rdActiveWorkspace').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitWorkspace').click(function () {
                    validate();
                });
            });
        });
    </script>


    <script>
        function SetSubWorkspace(val) {

            $('#rdSubworkspace').prop('checked', val);

        }
        function validateSubworkspace() {

            $('#hfStatusubworkspace').val($('#rdSubworkspace').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmiteSubWorkspace').click(function () {
                    validate();
                });
            });
        });
    </script>



    <script>
        function SetPbi(val) {

            $('#rdPbiActive').prop('checked', val);

        }
        function validatepbiLink() {

            $('#hfStatuspbi').val($('#rdPbiActive').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitPbilink').click(function () {
                    validate();
                });
            });
        });
    </script>




</asp:Content>

