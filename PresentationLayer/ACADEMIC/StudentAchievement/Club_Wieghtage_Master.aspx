<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Club_Wieghtage_Master.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_StudentAchievement_Club_Wieghtage_Master"  %>

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


    <asp:HiddenField ID="hfdWeightage" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdCampusDetails" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfvCount" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfvActivity" runat="server" ClientIDMode="Static" />

   
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
                                                    <button id="btnTab1" runat="server" class="nav-link active" data-toggle="tab" href="#tab_1"> Hour Weightage</button>
                                                </li>
                                                 <li class="nav-item">
                                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_2" >Event Nature</a>--%>
                                                    <%--onserverclick="btnTab2_Click"--%>
                                                    <button id="btnTab2" runat="server" class="nav-link" data-toggle="tab" href="#tab_2">Campus Details</button>
                                                </li>

                                                 <li class="nav-item">
                                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_2" >Event Nature</a>--%>
                                                    <%--onserverclick="btnTab2_Click"--%>
                                                    <button id="btnTab3" runat="server" class="nav-link" data-toggle="tab" href="#tab_3">Hours Count </button>
                                                </li>

                                                  <li class="nav-item">
                                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_2" >Event Nature</a>--%>
                                                    <%--onserverclick="btnTab2_Click"--%>
                                                    <button id="btnTab4" runat="server" class="nav-link" data-toggle="tab" href="#tab_4">Points Mapping</button>
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
                                                                            <label> Hour Weightage</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtWeitage" runat="server" AutoPostBack="true" MaxLength="128"  CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="rfvtxtwight" runat="server" ControlToValidate="txtWeitage"
                                                                            Display="None" ErrorMessage="Please Enter Hour Weightage" SetFocusOnError="True"
                                                                            ValidationGroup="PvalId"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True" TargetControlID="txtWeitage" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="(),- ">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdWeightage" name="switch" checked />
                                                                            <label data-on="Active" data-off="InActive" for="rdWeightage"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:LinkButton ID="btnSubmitWeight" runat="server"  OnClientClick="return validate();" CssClass="btn btn-primary" ValidationGroup="PvalId" OnClick="btnSubmitWeight_Click" >Submit</asp:LinkButton>
                                                                <asp:LinkButton ID="btnCancelweight" runat="server" CssClass="btn btn-warning" OnClick="btnCancelweight_Click" >Cancel</asp:LinkButton>
                                                            </div>

                                                            <asp:ValidationSummary ID="PraticipationValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="PvalId" />

                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Club PHD HR Weightage</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <asp:Panel ID="pnlSession" runat="server" Visible="true">
                                                                        <asp:ListView ID="lvphdweightage" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvphdweightage_ItemEditing" >
                                                                            <LayoutTemplate>
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Edit</th>
                                                                                            <th>Weightage</th>
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
                                                                                                <asp:LinkButton ID="btnWeightageLink" runat="server" CssClass="fas fa-edit" CommandArgument='<%#Eval("WEIGHTAGE_NO") %>' CommandName="Edit" OnClick="btnWeightageLink_Click"></asp:LinkButton>
                                                                                            </td>

                                                                                            <td>
                                                                                                <%# Eval("WEIGHTAGE_NAME") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%-- <span class="badge badge-success"> <%# Eval("ACTIVE_STATUS") %></span>--%>
                                                                                                <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                   <asp:PostBackTrigger ControlID="btnWeightageLink" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSubmitWeight" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnCancelweight" />
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
                                                                            <label>Campus Details</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtCampus" runat="server" MaxLength="128" AutoComplete="off" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="rfvtxtCampus" runat="server" ControlToValidate="txtCampus"
                                                                            Display="None" ErrorMessage="Please Enter Campus Details" SetFocusOnError="True"
                                                                            ValidationGroup="EvalId"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtCampus" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="(),- ">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdCampusStatus" name="switch" checked />
                                                                            <label data-on="Active" data-off="InActive" for="rdCampusStatus"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:LinkButton ID="btnSubmitCampus" runat="server" CssClass="btn btn-primary" OnClientClick="return validateEvent();" ValidationGroup="EvalId" OnClick="btnSubmitCampus_Click">Submit</asp:LinkButton>
                                                                <asp:LinkButton ID="btnCancelCampus" runat="server" CssClass="btn btn-warning" OnClick="btnCancelCampus_Click">Cancel</asp:LinkButton>
                                                            </div>

                                                            <asp:ValidationSummary ID="EvventValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="EvalId" />

                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Campus Details</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <asp:Panel ID="Panel2" runat="server" Visible="true">
                                                                        <asp:ListView ID="lvCampus" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvCampus_ItemEditing">
                                                                            <LayoutTemplate>
                                                                                <table class="table table-striped table-bordered nowrap" style="table-layout: fixed">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Edit</th>
                                                                                            <th>Campus</th>
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
                                                                                                <asp:LinkButton ID="btnCampusEdit" runat="server" CssClass="fas fa-edit" CommandArgument='<%#Eval("CAMPUS_NO") %>' CommandName="Edit" OnClick="btnCampusEdit_Click"></asp:LinkButton>
                                                                                            </td>

                                                                                            <td>
                                                                                                <%# Eval("CAMPUS_NAME") %>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblEStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                                                <%-- <span class="badge badge-success"><%# Eval("ACTIVE_STATUS") %></span>--%>
                                                                                            </td>

                                                                                        </tr>
                                                                                    </ContentTemplate>
                                                                                   <%-- <Triggers>


                                                                    <asp:PostBackTrigger ControlID="btnCampusEdit" />
                                                                </Triggers>--%>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>

                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSubmitCampus" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnCancelCampus" />

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
                                                                            <label>Hours Count</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtCount" runat="server" AutoComplete="off" MaxLength="128" CssClass="form-control" />
                                                                        <asp:RequiredFieldValidator ID="rfvtxtEventCategory" runat="server" ControlToValidate="txtCount"
                                                                            Display="None" ErrorMessage="Please Enter Hours Count" SetFocusOnError="True"
                                                                            ValidationGroup="CategoryValidationId"></asp:RequiredFieldValidator>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="txtCount" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="(),- ">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>

                                                                    </div>
                                                                 
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdCount" name="switch" checked />
                                                                            <label data-on="Active" data-off="InActive" for="rdCount"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:LinkButton ID="btnCountSubmit" runat="server" CssClass="btn btn-primary" ValidationGroup="CategoryValidationId" OnClientClick="return validateCategory();" Onclick="btnCountSubmit_Click">Submit</asp:LinkButton>
                                                                <asp:LinkButton ID="btnCountCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCountCancel_Click">Cancel</asp:LinkButton>

                                                            </div>
                                                            <asp:ValidationSummary ID="CategoryValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="CategoryValidationId" />

                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Hours Count & Points</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <asp:Panel ID="PanelCategory" runat="server" Visible="true">
                                                                        <asp:ListView ID="lvHourCount" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvCategory_ItemEditing">
                                                                            <LayoutTemplate>
                                                                                <table class="table table-striped table-bordered nowrap" style="table-layout: fixed">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Edit</th>
                                                                                            <th>Count Hours  & Point</th>
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
                                                                                                <asp:LinkButton ID="btneditCountHour" runat="server" CssClass="fas fa-edit" CommandArgument='<%# Eval("HC_NO") %>' CommandName="Edit" OnClick="btneditCountHour_Click"></asp:LinkButton>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("COUNT_NAME") %></td>
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
                                                            <asp:AsyncPostBackTrigger ControlID="btnCountSubmit" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnCountCancel" />
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
                                                                            <label>Hours Weightage</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlWeightage" runat="server" AppendDataBoundItems="false" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <asp:RequiredFieldValidator ID="rfvddlEventCatergory" runat="server" ControlToValidate="ddlWeightage"
                                                                            Display="None" ErrorMessage="Please Select Hours Weightage" SetFocusOnError="True"
                                                                            ValidationGroup="ActivityValidationId" InitialValue="-1"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Campus</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlCampus" runat="server" AppendDataBoundItems="false" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCampus"
                                                                            Display="None" ErrorMessage="Please Select Campus" SetFocusOnError="True"
                                                                            ValidationGroup="ActivityValidationId" InitialValue="-1"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Hours Counts</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlCount" runat="server" AppendDataBoundItems="false" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCount"
                                                                            Display="None" ErrorMessage="Please Select Hours Count" SetFocusOnError="True"
                                                                            ValidationGroup="ActivityValidationId" InitialValue="-1"></asp:RequiredFieldValidator>
                                                                    </div>


                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Points</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtPoints" runat="server" AutoComplete="off" MaxLength="256" CssClass="form-control" TextMode="Number" />
                                                                        <asp:RequiredFieldValidator ID="rfctxtActivityCategory" runat="server" ControlToValidate="txtPoints"
                                                                            Display="None" ErrorMessage="Please Enter Points" SetFocusOnError="True"
                                                                            ValidationGroup="ActivityValidationId"></asp:RequiredFieldValidator>
                                                                       
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
                                                                <asp:LinkButton ID="btnSubmitPoints" runat="server" CssClass="btn btn-primary"  OnClientClick="return  validateActivityCategory();" OnClick="btnSubmitPoints_Click">Submit</asp:LinkButton>

                                                                <asp:LinkButton ID="btnCancelPoints" runat="server" CssClass="btn btn-warning" OnClick="btnCancelPoints_Click" >Cancel</asp:LinkButton>

                                                            </div>
                                                            <asp:ValidationSummary ID="ActivityValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="ActivityValidationId" />

                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Activity Category List</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <asp:Panel ID="PanelActivity" runat="server" Visible="true">
                                                                        <asp:ListView ID="lvActivity" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvActivity_ItemEditing">
                                                                            <LayoutTemplate>
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>Edit</th>
                                                                                            <th>Hr Weightage</th>
                                                                                            <th>Campus</th>
                                                                                            <th>Hours Count</th>
                                                                                            <th>Points</th>
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
                                                                                                <asp:LinkButton ID="btnEditActivity" runat="server" CssClass="fas fa-edit" OnClick="btnEditActivity_Click" CommandArgument='<%# Eval("WCC_POINTNO") %>' CommandName="Edit"></asp:LinkButton>
                                                                                            </td>

                                                                                            <td><%# Eval("WEIGHTAGE_NAME") %></td>
                                                                                            <td><%# Eval("CAMPUS_NAME") %></td>
                                                                                            <td><%# Eval("COUNT_NAME") %></td>
                                                                                            <td><%# Eval("POINTS") %></td>

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
                                                            <asp:AsyncPostBackTrigger ControlID="btnSubmitPoints" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnCancelPoints" />
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

            $('#rdWeightage').prop('checked', val);

        }
        function validate() {


            $('#hfdWeightage').val($('#rdWeightage').prop('checked'));

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

            $('#rdCampusStatus').prop('checked', val);
        }
        function validateEvent() {

            $('#hfdCampusDetails').val($('#rdCampusStatus').prop('checked'));

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnCampusDetails').click(function () {
                    validate();
                });
            });
        });

    </script>

    <script>
        function SetCategory(val) {

            $('#rdCount').prop('checked', val);
        }

        function validateCategory() {

            $('#hfvCount').val($('#rdCount').prop('checked'));

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



