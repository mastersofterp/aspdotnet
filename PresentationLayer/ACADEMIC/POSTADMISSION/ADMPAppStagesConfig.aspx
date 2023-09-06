<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPAppStagesConfig.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMPAppStagesConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function allowAlphaNumericSpace(e) {
            var code = ('charCode' in e) ? e.charCode : e.keyCode;
            if (!(code == 32) && // space
              !(code == 45) && // Dash
              !(code > 47 && code < 58) && // numeric (0-9)
              !(code > 64 && code < 91) && // upper alpha (A-Z)
              !(code > 96 && code < 123)) { // lower alpha (a-z)
                e.preventDefault();
            }
        }
        function functionx(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                alert("Enter Only Numeric Value ");
                return false;
            }
        }
    </script>
    <style>
        .nav-tabs-custom .nav-link {
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

    <style>
        .comp-tbl .table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5;
        }

        .form-inline {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-flow: row;
            flex-flow: row;
        }
    </style>

    <asp:HiddenField ID="hfdPhase" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStage" runat="server" ClientIDMode="Static" />

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <%--<h3 class="box-title">Application Stages Configuration</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                    </h3>
                </div>
                <div id="Tabs" role="tabpanel">
                    <div class="box-body">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <%--<a class="nav-link active" data-toggle="tab" href="#tab_1">Phases</a>--%>
                                    <button id="btnTab1" runat="server" class="nav-link active" data-toggle="tab" href="#tab_1">Phases</button>
                                </li>
                                <li class="nav-item">
                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_2">Stages</a>--%>
                                    <button id="btnTab2" runat="server" onserverclick="btnTab2_Click" class="nav-link" data-toggle="tab" href="#tab_2">Stages</button>
                                </li>
                                <li class="nav-item">
                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_3">Stages Degree Mapping</a>--%>
                                    <button id="btnTab3" runat="server" onserverclick="btnTab3_Click" class="nav-link" data-toggle="tab" href="#tab_3">Stages Degree Mapping</button>
                                </li>
                                <li class="nav-item">
                                    <%--<a class="nav-link" data-toggle="tab" href="#tab_4">Stages Dependencies</a>--%>
                                    <button id="btnTab4" runat="server" onserverclick="btnTab4_Click" class="nav-link" data-toggle="tab" href="#tab_4">Stages Dependencies</button>
                                </li>
                            </ul>

                            <div class="tab-content" id="my-tab-content">

                                <%--TAB-1 Application Phase--%>
                                <div class="tab-pane active" id="tab_1">
                                    <asp:UpdatePanel runat="server" ID="updp">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Application Phase</label>
                                                        </div>
                                                        <asp:TextBox ID="txtPhases" runat="server" CssClass="form-control" MaxLength="200" onkeypress="allowAlphaNumericSpace(event)" placeholder="Enter Application Phase" />
                                                        <asp:RequiredFieldValidator ID="rfvtxtPhases" runat="server" ControlToValidate="txtPhases" Display="None" ErrorMessage="Please Enter Application Phase Name" ValidationGroup="phase" SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdActivePhase" name="switch" checked />
                                                            <label data-on="Active" data-off="InActive" for="rdActivePhase"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnSubmitPhase" runat="server" CssClass="btn btn-primary" OnClientClick="return validatePhase();" ValidationGroup="phase" OnClick="btnSubmitPhase_Click">Submit</asp:LinkButton>
                                                <%--OnClick="btnSubmitPhase_Click"--%>
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="phase"
                                                    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                <asp:LinkButton ID="btnCancelPhase" runat="server" CssClass="btn btn-warning" OnClick="btnCancelPhase_Click">Cancel</asp:LinkButton>
                                                <%--OnClick="btnCancelPhase_Click"--%>
                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="pnlPhases" runat="server" Visible="false">
                                                    <asp:ListView ID="lvPhases" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="divPhasesList">
                                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                        <tr>
                                                                            <th>Edit</th>
                                                                            <th>Application Phase</th>
                                                                            <th>Status</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel runat="server" ID="updPhase">
                                                                <ContentTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditPhase" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                                CommandArgument='<%# Eval("PHASEID")%>' AlternateText="Edit Record" OnClick="btnEditPhase_Click" ToolTip="Edit Record" TabIndex="7" />
                                                                            <%--OnClick="btnEditPhase_Click"--%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("PHASE")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblStatusPhage" runat="server" CssClass="badge" Text='<%# Eval("ACTIVESTATUS") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                            <%--<div class="col-12 mt-4">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr id="tr1">
                                                <th>Edit</th>
                                                <th>Application Phase</th>
                                                <th>Status</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton2" class="newAddNew Tab" runat="server" ImageUrl="~/Images/edit.png" AlternateText="Edit Record" ToolTip="Edit Record" /></td>
                                                <td>Applied</td>
                                                <td>yes </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton3" class="newAddNew Tab" runat="server" ImageUrl="~/Images/edit.png" AlternateText="Edit Record" ToolTip="Edit Record" /></td>
                                                <td>Pending</td>
                                                <td>yes </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>--%>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmitPhase" />
                                            <asp:PostBackTrigger ControlID="btnCancelPhase" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                                <%--TAB-2 Application Stages--%>
                                <div class="tab-pane fade" id="tab_2">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Application Phase</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAppliPhase" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlAppliPhase" runat="server" ControlToValidate="ddlAppliPhase"
                                                            ErrorMessage="Please Select Application Phase" Display="None" ValidationGroup="stage" InitialValue="Please Select"></asp:RequiredFieldValidator>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Application Stage</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAppliStage" runat="server" CssClass="form-control" onkeypress="allowAlphaNumericSpace(event)" placeholder="Enter Application Stage Name" />
                                                        <asp:RequiredFieldValidator ID="rfvtxtAppliStage" runat="server" ControlToValidate="txtAppliStage" Display="None" ErrorMessage="Please Enter Application Stage Name" ValidationGroup="stage" SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Description</label>
                                                        </div>
                                                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdActiveStage" name="switch" checked />
                                                            <label data-on="Active" data-off="InActive" for="rdActiveStage"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">     
                                                             <sup> * </sup>                                         
                                                            <label>Select Process</label>
                                                        </div>
                                              
                                                        <asp:RadioButton ID="rboP" runat="server" Text="Portal" GroupName="Process"/>&nbsp;&nbsp;
                                                        <asp:RadioButton ID="rboV" runat="server" Text="Verification" GroupName="Process"/>&nbsp;&nbsp;
                                                        <asp:RadioButton ID="rboS" runat="server" Text="Scheduled" GroupName="Process"/>&nbsp;&nbsp;
                                                        <asp:RadioButton ID="rboN" runat="server" Text="None" GroupName="Process" Checked="true"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer mt-3 mb-2">
                                                <asp:LinkButton ID="btnSubmitStage" runat="server" CssClass="btn btn-primary" OnClientClick="return validate();" ValidationGroup="stage" OnClick="btnSubmitStage_Click">Submit</asp:LinkButton>
                                                <%--OnClick="btnSubmitStage_Click"--%>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="stage"
                                                    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                <asp:LinkButton ID="btnCancelStage" runat="server" CssClass="btn btn-warning" OnClick="btnCancelStage_Click">Cancel</asp:LinkButton>
                                                <%--OnClick="btnCancelStage_Click"--%>
                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="PnlStage" runat="server" Visible="false">
                                                    <asp:ListView ID="lvStage" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="divStageList">
                                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                        <tr>
                                                                            <th>Edit</th>
                                                                            <th>Application Phase</th>
                                                                            <th>Application Stage</th>
                                                                            <th>Status</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel runat="server" ID="updStage">
                                                                <ContentTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditStage" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                                CommandArgument='<%# Eval("STAGEID")%>' AlternateText="Edit Record" OnClick="btnEditStage_Click" ToolTip="Edit Record" TabIndex="7" />
                                                                            <%--OnClick="btnEditStage_Click"--%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("PHASE")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("STAGENAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblStatusStage" runat="server" CssClass="badge" Text='<%# Eval("ACTIVESTATUS") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                            <%--<div class="col-12 mt-4">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="example">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Edit</th>
                                                <th>Application Phase</th>
                                                <th>Application Stage</th>
                                                <th>Status</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>Pending</td>
                                                <td>Eligible and Need Verification</td>
                                                <td>yes </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton1" class="newAddNew Tab" runat="server" ImageUrl="~/Images/edit.png" AlternateText="Edit Record" ToolTip="Edit Record" /></td>
                                            </tr>
                                            <tr>
                                                <td>Pending</td>
                                                <td>Eligible Candidate</td>
                                                <td>yes </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton4" class="newAddNew Tab" runat="server" ImageUrl="~/Images/edit.png" AlternateText="Edit Record" ToolTip="Edit Record" /></td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </div>--%>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmitStage" />
                                            <asp:PostBackTrigger ControlID="btnCancelStage" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                                <%--TAB-3 Stages Degree Mapping--%>
                                <div class="tab-pane fade" id="tab_3">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-md-4 col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Admission Batch</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAdmBatch" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                                                    ErrorMessage="Please Select Admission Batch" Display="None" ValidationGroup="stage-degree-map" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Degree</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDegremapping" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegremapping_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlDegremapping" runat="server" ControlToValidate="ddlDegremapping"
                                                                    ErrorMessage="Please Select Degree" Display="None" ValidationGroup="stage-degree-map" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-8 col-md-8 col-12">
                                                        <asp:Panel ID="pnlStageDegree" runat="server" Visible="true">
                                                            <asp:ListView ID="lvStageDegree" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="divStageDegree">
                                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                <tr>
                                                                                    <th></th>
                                                                                    <th>Stages</th>
                                                                                    <th>Sequence No.</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel runat="server" ID="UpdateLV">
                                                                        <ContentTemplate>
                                                                            <tr>
                                                                                <td style="text-align: center">
                                                                                    <asp:HiddenField ID="hfdStageId" runat="server" Value='<%# Eval("STAGEID") %>' />
                                                                                    <asp:CheckBox ID="chkIsActive" runat="server" Enabled="false" CssClass="chkbox_addsubject" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("STAGENAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtSequance" runat="server" CssClass="form-control" Enabled="false" MaxLength="3" min="0" Text='<%# Eval("SEQUANCENO") %>' onkeypress="return functionx(event)" ToolTip="Please Enter Sequence Number" type="number"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>

                                                        <%--<table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="width: 15%;">
                                                            <asp:CheckBox ID="CheckBox1" runat="server" />Select All</th>
                                                        <th>Stages</th>
                                                        <th>Sequence No. </th>


                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="CheckBox2" runat="server" /></td>
                                                        <td>Pending for payment</td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Width="100px" placeholder="Sequence No." />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="CheckBox3" runat="server" /></td>
                                                        <td>Pending for Document Verification</td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" Width="100px" placeholder="Sequence No." />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="CheckBox8" runat="server" /></td>
                                                        <td>Pending for payment</td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Width="100px" placeholder="Sequence No." />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnSubmitStageMapping" runat="server" OnClick="btnSubmitStageMapping_Click" CssClass="btn btn-primary" ValidationGroup="stage-degree-map">Submit</asp:LinkButton>
                                                <%--OnClick="btnSubmitStageMapping_Click"--%>
                                                <asp:LinkButton ID="bntCancelStageMapping" runat="server" CssClass="btn btn-warning" OnClick="bntCancelStageMapping_Click">Cancel</asp:LinkButton>
                                                <%--OnClick="bntCancelStageMapping_Click"--%>
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="false" ValidationGroup="stage-degree-map" />
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmitStageMapping" />
                                            <asp:PostBackTrigger ControlID="bntCancelStageMapping" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlAdmBatch" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlDegremapping" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                                <%--TAB-4 Stages Dependencies--%>
                                <div class="tab-pane fade" id="tab_4">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-md-4 col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Admission Batch</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDepAdmBatch" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlDepAdmBatch_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDepAdmBatch"
                                                                    ErrorMessage="Please Select Admission Batch" Display="None" ValidationGroup="StageDepend" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Degree</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDepDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDepDegree_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDepDegree"
                                                                    ErrorMessage="Please Select Degree" Display="None" ValidationGroup="StageDepend" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Current Stage</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCurrentStage" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrentStage_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlCurrentStage" runat="server" ControlToValidate="ddlCurrentStage"
                                                                    ErrorMessage="Please Select Current Stage" Display="None" ValidationGroup="StageDepend" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-8 col-md-6 col-12">
                                                        <div class="sub-heading">
                                                            <h5>Next Stage</h5>
                                                        </div>
                                                        <asp:Panel ID="pnldependancies" runat="server" Visible="false">
                                                            <asp:ListView ID="lvdependancies" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="divdependancies">
                                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                <tr>
                                                                                    <th></th>
                                                                                    <th>Stages</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel runat="server" ID="Update10">
                                                                        <ContentTemplate>
                                                                            <tr>
                                                                                <td style="text-align: center">
                                                                                    <asp:HiddenField ID="hfdCurrentStage" runat="server" Value='<%# Eval("STAGEID") %>' />
                                                                                    <asp:HiddenField ID="hfdActiveStatus" runat="server" Value='<%# Eval("ACTIVESTATUS") %>' />
                                                                                    <asp:CheckBox ID="chkCurrentStage" runat="server" Enabled="false" CssClass="chkbox_addsubject" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("STAGENAME")%>
                                                                                </td>
                                                                            </tr>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                        <%--<table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr id="tr2">
                                                        <th style="width: 13%;">
                                                            <asp:CheckBox ID="CheckBox5" runat="server" />Select All</th>
                                                        <th>Stages</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="CheckBox4" runat="server" /></th>
                                                        <td>Pending for payment</td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="CheckBox6" runat="server" /></td>
                                                        <td>Pending for Document Verification</td>

                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="CheckBox7" runat="server" /></th>
                                                        <td>Pending for payment</td>

                                                    </tr>

                                                </tbody>
                                            </table>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnsubDependancy" runat="server" ValidationGroup="StageDepend" CssClass="btn btn-primary" OnClick="btnsubDependancy_Click">Submit</asp:LinkButton>
                                                <%--OnClick="btncancelDependancy_Click"--%>
                                                <asp:LinkButton ID="btncancelDependancy" runat="server" CssClass="btn btn-warning" OnClick="btncancelDependancy_Click">Cancel</asp:LinkButton>
                                                <%--OnClick="btncancelDependancy_Click"--%>
                                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="false" ValidationGroup="StageDepend" />
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnsubDependancy" />
                                            <asp:PostBackTrigger ControlID="btncancelDependancy" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlCurrentStage" EventName="SelectedIndexChanged" />
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
    <script>
        function SetPhases(val) {

            $('#rdActivePhase').prop('checked', val);

        }
        function validatePhase() {

            $('#hfdPhase').val($('#rdActivePhase').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitPhase').click(function () {
                    validatePhase();
                });
            });
        });
    </script>
    <script>
        function SetStages(val) {

            $('#rdActiveStage').prop('checked', val);

        }
        function validate() {


            $('#hfdStage').val($('#rdActiveStage').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitStage').click(function () {
                    validate();
                });
            });
        });
    </script>

    <%--===== Data Table Script added by gaurav =====--%>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('#example').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#example').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                            {
                                extend: 'copyHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#example').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },
                            {
                                extend: 'excelHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#example').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },

                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#example').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#example').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                               {
                                   extend: 'copyHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#example').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },
                               {
                                   extend: 'excelHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#example').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },

                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>
</asp:Content>
