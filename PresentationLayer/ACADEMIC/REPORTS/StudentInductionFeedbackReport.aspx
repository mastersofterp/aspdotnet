<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentInductionFeedbackReport.aspx.cs" Inherits="ACADEMIC_REPORTS_StudentInductionFeedbackReport" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- jQuery library -->
    <%--<script src="../jquery/jquery-3.2.1.min.js"></script>   --%>
    <script src="<%=Page.ResolveClientUrl("../../plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <link href="<%=Page.ResolveClientUrl("../../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            // if you use jQuery, you can load them when dom is read.          
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();

            function InitializeRequest(sender, args) {
            }

            function EndRequest(sender, args) {
                // after update occur on UpdatePanel re-init the Autocomplete
                //   InitAutoCompl();
            }
            //function InitAutoCompl() {
            //    $('select[multiple]').val('').multiselect({
            //        columns: 3,     // how many columns should be use to show options            
            //        search: true, // include option search box
            //        searchOptions: {
            //            delay: 250,                         // time (in ms) between keystrokes until search happens
            //            clearSelection: true,
            //            showOptGroups: false,                // show option group titles if no options remaining

            //            searchText: true,                 // search within the text

            //            searchValue: true,                // search within the value

            //            onSearch: function (element) { } // fires on keyup before search on options happens
            //        },

            //        // plugin texts

            //        texts: {

            //            placeholder: 'Please Select', // text to use in dummy input

            //            search: 'Search',         // search input placeholder text

            //            selectedOptions: ' selected',      // selected suffix text

            //            selectAll: 'Select all',     // select all text

            //            unselectAll: 'Unselect all',   // unselect all text

            //            //noneSelected: 'None Selected'   // None selected text

            //        },

            //        // general options

            //        selectAll: true, // add select all option

            //        selectGroup: false, // select entire optgroup

            //        minHeight: 200,   // minimum height of option overlay

            //        maxHeight: null,  // maximum height of option overlay

            //        maxWidth: null,  // maximum width of option overlay (or selector)

            //        maxPlaceholderWidth: null, // maximum width of placeholder button

            //        maxPlaceholderOpts: 10, // maximum number of placeholder options to show until "# selected" shown instead

            //        showCheckbox: true,  // display the checkbox to the user

            //        optionAttributes: [],

            //    });
            //}
        });

    </script>

    <%--<style>
        div.dd_chk_select {
            height: 35px;
            font-size: 14px !important;
            padding-left: 12px !important;
            line-height: 2.2 !important;
            width: 100%;
        }

            div.dd_chk_select div#caption {
                height: 35px;
            }
    </style>--%>

    <%--   <style type="text/css">
        #load {
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 9999;
            /*background: url("/images/loading_icon.gif") no-repeat center center rgba(0,0,0,0.25);*/
        }
    </style>--%>

    <script type="text/javascript">
        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('load').style.visibility = "hidden";
                    document.getElementById('contents').style.visibility = "visible";
                }, 1000);
            }
        }
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                            OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherassign">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree </label>
                                        </div>
                                        <asp:ListBox ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control multi-select-demo" SelectionMode="multiple" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>

                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" ValidationGroup="teacherassign">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch </label>
                                        </div>
                                        <asp:ListBox ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="3"
                                            CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>

                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Feedback Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlFeedbackType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="4" OnSelectedIndexChanged="ddlFeedbackType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem>Induction Feedback</asp:ListItem>
                                            <asp:ListItem>Exit Feedback</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFeedbackType"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Feedback Type" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlFeedbackType"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Feedback Type" ValidationGroup="teacherassign">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnFilter" runat="server" Text="Show" ValidationGroup="teacherallot" Visible="false"
                                    OnClick="btnFilter_Click" CssClass="btn btn-primary" />

                                <asp:Button ID="btnStudentExitFeedbackReport" runat="server" Text="Exit Feedback Count" ValidationGroup="teacherallot" TabIndex="5" CssClass="btn btn-primary" OnClick="btnStudentExitFeedbackReport_Click" />

                                <asp:Button ID="btnExitFeedbackConsolidated" runat="server" Text="Exit Consolidated"
                                    ValidationGroup="teacherallot" TabIndex="6"
                                    OnClick="btnExitFeedbackConsolidated_Click" CssClass="btn btn-primary" />


                                <asp:Button ID="btnStudentFeedbackReport" runat="server" Text="Feedback Count" ValidationGroup="teacherassign"
                                    TabIndex="7" CssClass="btn btn-primary" OnClick="btnStudentFeedbackReport_Click" />

                                <asp:Button ID="btnExitFeedbackReport" runat="server" Text="Exit FeedBack Report" TabIndex="8" CssClass="btn btn-primary"
                                    ValidationGroup="teacherassign" OnClick="btnExitFeedbackReport_Click"  style="display:none;"/>


                                <%--  <asp:Button ID="btnInductionFeedbackReport" runat="server" Text="Induction FeedBack Report" TabIndex="9" CssClass="btn btn-primary" 
                                      ValidationGroup="teacherassign" OnClick="btnInductionFeedbackReport_Click" />--%>


                                <asp:Button ID="btnExitFeedbackComment" runat="server" Text="Exit FeedBack Comments Report" TabIndex="9" CssClass="btn btn-primary"
                                    ValidationGroup="teacherassign" OnClick="btnExitFeedbackComment_Click"  style="display:none;"/>


                                <asp:Button ID="btnExitClear" runat="server" Text="Clear" OnClick="btnExitClear_Click" CssClass="btn btn-warning"
                                    Visible="false" TabIndex="10" />

                                <asp:Button ID="btnStudentFeedbackGraphReport" runat="server" Text="Induction Graph"
                                    ValidationGroup="teacherallot" style="display:none;"
                                    TabIndex="11" CssClass="btn btn-primary" OnClick="btnStudentFeedbackGraphReport_Click" />
                                <asp:Button ID="btnBridgeCourseGraphReport" runat="server" Text="Bridge Course" ValidationGroup="teacherallot"
                                    TabIndex="12" CssClass="btn btn-primary" OnClick="btnBridgeCourseGraphReport_Click" Visible="false"  style="display:none;"/>



                                <asp:Button ID="btnConductionOfInductionGraphReport" runat="server" Text="Conduction Report" Visible="false" ValidationGroup="teacherallot" TabIndex="13"  style="display:none;" CssClass="btn btn-primary" OnClick="btnConductionOfInductionGraphReport_Click" />


                                <asp:Button ID="btnEminentSpeakersGraphReport" runat="server" Text="Eminent Speakers" ValidationGroup="teacherallot"
                                    TabIndex="14" CssClass="btn btn-primary" OnClick="btnEminentSpeakersGraphReport_Click" Visible="false"  style="display:none;"/>

                                <asp:Button ID="btnExtraCurriGraphReport" runat="server" Text="Extra Curricular" ValidationGroup="teacherallot"
                                    TabIndex="15" CssClass="btn btn-primary" OnClick="btnExtraCurriGraphReport_Click" Visible="false"  style="display:none;"/>


                                <asp:Button ID="btnCalibrationEventsGraphReport" runat="server" Text="Calibration Events" ValidationGroup="teacherallot"
                                    TabIndex="16" CssClass="btn btn-primary" OnClick="btnCalibrationEventsGraphReport_Click" Visible="false"  style="display:none;"/>


                                <asp:Button ID="btnYogaTrainingGraphReport" runat="server" Text="Yoga Training" ValidationGroup="teacherallot"
                                    TabIndex="17" CssClass="btn btn-primary" OnClick="btnYogaTrainingGraphReport_Click" Visible="false"  style="display:none;"/>

                                <asp:Button ID="btnConductionOf3WeeksGraphReport" runat="server" Text="3 Weeks Report" ValidationGroup="teacherallot"
                                    TabIndex="18" CssClass="btn btn-primary" OnClick="btnConductionOf3WeeksGraphReport_Click" Visible="false"  style="display:none;"/>

                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-warning"
                                    TabIndex="19" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="teacherassign"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <div id="divMsg" runat="server">
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
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
                });
            });
        });
    </script>
</asp:Content>
