<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CanvasIntegration.aspx.cs" Inherits="Canvas_CanvasIntegration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/Community/css/iziToast.min.css")%>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/Community/css/tippy.css")%>" rel="stylesheet" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.19/css/intlTelInput.css" integrity="sha512-gxWow8Mo6q6pLa1XH/CcH8JyiSDEtiwJV78E+D+QP0EVasFs8wKXq16G8CLD4CJ2SnonHr4Lm/yY2fSI2+cbmw==" crossorigin="anonymous" referrerpolicy="no-referrer" defer />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/limonte-sweetalert2/11.6.15/sweetalert2.min.css" integrity="sha512-NvuRGlPf6cHpxQqBGnPe7fPoACpyrjhlSNeXVUY7BZAj1nNhuNpRBq3osC4yr2vswUEuHq2HtCsY2vfLNCndYA==" crossorigin="anonymous" referrerpolicy="no-referrer" defer />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.18.3/extensions/filter-control/bootstrap-table-filter-control.min.css" integrity="sha512-IK+plgQ3Qomdj9Mzq2WbM5LF8JY0PPXHGGfiBqBkwhXUi1tVAIvIaLbt330r3t0Y4EUxqG0YGfEhRrQS0gKeYQ==" crossorigin="anonymous" referrerpolicy="no-referrer" defer />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.18.3/bootstrap-table.min.css" integrity="sha512-5RNDl2gYvm6wpoVAU4J2+cMGZQeE2o4/AksK/bi355p/C31aRibC93EYxXczXq3ja2PJj60uifzcocu2Ca2FBg==" crossorigin="anonymous" referrerpolicy="no-referrer" defer />

    <script src="<%=Page.ResolveClientUrl("~/plugins/Community/js/iziToast.min.js")%>"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.18.3/bootstrap-table.min.js" integrity="sha512-Wm00XTqNHcGqQgiDlZVpK4QIhO2MmMJfzNJfh8wwbBC9BR0FtdJwPqDhEYy8jCfKEhWWZe/LDB6FwY7YE9QhMg==" crossorigin="anonymous" referrerpolicy="no-referrer" defer></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.18.3/extensions/auto-refresh/bootstrap-table-auto-refresh.min.js" integrity="sha512-cjm1J28H+LM+85W7s6/fFHr3BsBomtRV/nOGFDAEmloV0E8jr382pUWezLsde1lQr+Ek+x6j1R1li07T3cY5pg==" crossorigin="anonymous" referrerpolicy="no-referrer" defer></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.18.3/extensions/export/bootstrap-table-export.min.js" integrity="sha512-cAMZL39BuY4jWHUkLWRS+TlHzd/riowdz6RNNVI6CdKRQw1p1rDn8n34lu6pricfL0i8YXeWQIDF5Xa/HBVLRg==" crossorigin="anonymous" referrerpolicy="no-referrer" defer></script>

    <script src="<%=Page.ResolveClientUrl("~/Canvas/Plugins/bootstrap-table/dist/extensions/export/libs/FileSaver/FileSaver.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/Canvas/Plugins/bootstrap-table/dist/extensions/export/libs/es6-promise/es6-promise.auto.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/Canvas/Plugins/bootstrap-table/dist/extensions/export/libs/js-xlsx/xlsx.core.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/Canvas/Plugins/bootstrap-table/dist/extensions/export/tableExport.min.js")%>"></script>

    <script src="<%=Page.ResolveClientUrl("~/Canvas/main.js")%>"></script>

    <style>
        .page-list {
            display: none !important;
        }

        .bootstrap-table .fixed-table-pagination > .pagination, .bootstrap-table .fixed-table-pagination > .pagination-detail {
            margin-top: 0px;
            margin-bottom: 0px;
        }

        .fixed-table-toolbar .caret {
            display: none;
        }

        .alert.alert-has-icon {
            background: #fff;
            display: flex;
            border: 1px solid #cbd1d6;
            padding: 0;
            color: #2d335f;
            border-radius: 0;
            font-size: 15px;
        }

            .alert.alert-has-icon .alert-icon {
                margin-top: 0px;
                width: auto;
                background: #6777ef !important;
                color: #fff;
                padding: 10px;
                display: flex;
                align-items: center;
            }

            .alert.alert-has-icon .alert-body {
                padding: 0 7px;
            }

        .subCard {
            border: 1px solid #6777EF;
            border-radius: 4px;
        }

            .subCard .subCard-header {
                background: #F0F3FF 0% 0% no-repeat padding-box;
                border-radius: 3px 3px 0px 0px;
                padding: 7px 16px;
                color: #6777EF;
            }

            .subCard .subCard-body {
                padding: 13px;
            }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                    <h3 class="box-title">Canvas Integration</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs" id="myTab" role="tablist">
                                <li class="nav-item" id="tourDepartmentExport">
                                    <a class="nav-link active" id="college-tab" data-toggle="tab" href="#tabCollegeExport" role="tab"
                                        aria-controls="college" aria-selected="true">College</a>
                                </li>
                                <li class="nav-item" id="Li1">
                                    <a class="nav-link " id="home-tab" data-toggle="tab" href="#tabDepartmentExport" role="tab"
                                        aria-controls="home" aria-selected="true">Session</a>
                                </li>
                                <li class="nav-item" id="tourCourseTemplateExport">
                                    <a class="nav-link" id="curriculum-tab" data-toggle="tab" href="#tabCourseTemplateExport" role="tab"
                                        aria-controls="curriculum" aria-selected="false">Curriculum</a>
                                </li>
                                <li class="nav-item" id="tourCourseOffered">
                                    <a class="nav-link" id="course-tab" data-toggle="tab" href="#tabCourseOffered" role="tab"
                                        aria-controls="course" aria-selected="false">Course/Section Allotment</a>
                                </li>
                                <li class="nav-item" id="tourUserAllocation">
                                    <a class="nav-link" id="user-tab" data-toggle="tab" href="#tabUserAllocation" role="tab"
                                        aria-controls="user" aria-selected="false">User Allocation</a>
                                </li>
                            </ul>
                            <div class="tab-content" id="myTabContent">
                                <div class="tab-pane active" id="tabCollegeExport" role="tabpanel" aria-labelledby="college-tab">
                                    <div class="row mt-3">
                                        <!-- content here -->
                                        <div class="col-lg-4 col-md-4 col-12" id="">
                                            <div class="form-group">
                                                <label><sup>* </sup>Canvas Instance Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlLMSNameTab0" title="" tabindex="1" name="ddlLMSNameTab0">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div1">
                                            <div class="form-group">
                                                <label id="lblOrg"><sup>* </sup>College Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlRFCollegeNameTab0" data-placeholder="Please Select" title="" tabindex="1" name="ddlRFCollegeNameTab0" multiple>
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                       <%-- <div class="col-lg-4 col-md-4 col-12 hide" id="Div2">
                                            <div class="form-group">
                                                <label id="Label1"><sup>* </sup>Canvas Type<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlType">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>--%>

                                        <!-- content end -->
                                    </div>
                                    <!-- buttons here -->
                                    <div class='text-center' id="tourSubmitButton1">

                                        <button type="button" tabindex="1" class="btn btn-primary common-submit-btn" id="btnSubmitColleges">Submit</button>
                                        <button type="button" class="btn btn-outline-danger common-cancel-btn" tabindex="1" id="btnClearColleges">Cancel</button>

                                    </div>
                                    <!-- table here -->
                                    <div class="table-responsive">
                                        <div id="toolbar" class='tableToolbarLeft'>
                                        </div>
                                        <table id="tblTab0" class="table table-striped table-bordered">
                                            <thead>
                                                <tr>
                                                    <th data-field="canvas_instance_creation_id" data-formatter="canvasCreationFormatter" data-filter-control="input" data-sortable="true">Canvas Instance Name</th>
                                                    <th data-field="College_Name" data-filter-control="input" data-sortable="true">College Name</th>
                                                    <th data-field="ModifiedDate" data-filter-control="input" data-sortable="true" data-formatter="quickFormTimeFormatter">Created Date</th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                    <!-- table end -->
                                    <!-- tab end -->

                                </div>

                                <div class="tab-pane fade" id="tabDepartmentExport" role="tabpanel" aria-labelledby="home-tab">
                                    <div class="row mt-3">
                                        <!-- content here -->
                                        <div class="col-lg-4 col-md-4 col-12" id="Div3">
                                            <div class="form-group">
                                                <label><sup>* </sup>Canvas College Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlCanvasCollegeNameTab1" title="" tabindex="1" name="ddlCanvasCollegeNameTab1">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div4">
                                            <div class="form-group">
                                                <label id="Label2"><sup>* </sup>College Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlRFCollegeNameTab1" data-placeholder="Please Select" title="" tabindex="1" name="ddlRFCollegeNameTab1">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div5">
                                            <div class="form-group">
                                                <label id="Label3"><sup>* </sup>Session Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlRFSessionNameTab1" data-placeholder="Please Select" title="" tabindex="1" name="ddlRFSessionNameTab1" multiple>
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <%--<div class="col-lg-4 col-md-4 col-12 hide" id="Div6">
                                            <div class="form-group">
                                                <label id="Label4"><sup>* </sup>Canvas Type<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="Select1">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>--%>

                                        <!-- content end -->
                                    </div>
                                    <!-- buttons here -->
                                    <div class='text-center' id="Div7">

                                        <button type="button" tabindex="1" class="btn btn-primary common-submit-btn" id="btnSubmitSession">Submit</button>
                                        <button type="button" class="btn btn-outline-danger common-cancel-btn" tabindex="1" id="btnClearSession">Cancel</button>

                                    </div>
                                    <!-- table here -->
                                    <div class="table-responsive">
                                        <div id="Div8" class='tableToolbarLeft'>
                                        </div>
                                        <table id="tblTab1" class="table table-striped table-bordered">
                                            <thead>
                                                <tr>
                                                    <th data-field="canvas_instance_creation_id" data-formatter="canvasCreationFormatter" data-filter-control="input" data-sortable="true">Canvas Instance Name</th>
                                                    <th data-field="College_Name" data-filter-control="input" data-sortable="true">College Name</th>
                                                    <th data-field="Session_Name" data-filter-control="input" data-sortable="true">Session Name</th>
                                                    <th data-field="ModifiedDate" data-filter-control="input" data-sortable="true" data-formatter="quickFormTimeFormatter">Created Date</th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                    <!-- table end -->
                                    <!-- tab end -->

                                </div>

                                <div class="tab-pane fade" id="tabCourseTemplateExport" role="tabpanel" aria-labelledby="profile-tab">
                                    <div class="row mt-3">
                                        <!-- content here -->
                                        <div class="col-lg-4 col-md-4 col-12" id="Div9">
                                            <div class="form-group">
                                                <label><sup>* </sup>Canvas College Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlCanvasCollegeNameTab2" title="" tabindex="1" name="ddlCanvasCollegeNameTab2">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div10">
                                            <div class="form-group">
                                                <label id="Label5"><sup>* </sup>Canvas Session Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlCanvasSessionNameTab2">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div11">
                                            <div class="form-group">
                                                <label id="Label6"><sup>* </sup>College Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlRFCollegeNameTab2" data-placeholder="Please Select" title="" tabindex="1" name="ddlRFCollegeNameTab2">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div12">
                                            <div class="form-group">
                                                <label id="Label7"><sup>* </sup>Curriculum Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlRFCurriculumTab2" multiple>
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <!-- content end -->
                                    </div>
                                    <!-- buttons here -->
                                    <div class="text-center" id="uploadButton2">
                                        <button type="button" tabindex="1" class="btn btn-primary common-submit-btn" id="btnSubmitCurriculum">Submit</button>
                                        <button type="button" class="btn btn-outline-danger common-cancel-btn" tabindex="1" id="btnClearCurriculum">Cancel</button>
                                    </div>
                                    <!-- table here -->
                                    <div id="divEmployeeGrid">

                                        <div class="table-responsive header-fixed">
                                            <table id="tblTab2" class="table table-striped table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th data-field="canvas_instance_creation_id" data-formatter="canvasCreationFormatter" data-filter-control="input" data-sortable="true">Canvas Instance Name</th>
                                                        <th data-field="College_Name" data-filter-control="input" data-sortable="true">College Name</th>
                                                        <th data-field="Scheme_Name" data-filter-control="input" data-sortable="true">Curriculum</th>
                                                        <th data-field="ModifiedDate" data-filter-control="input" data-sortable="true" data-formatter="quickFormTimeFormatter">Created Date</th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </div>



                                    </div>

                                    <!-- table end -->
                                    <!-- tab end -->

                                </div>

                                <div class="tab-pane fade" id="tabCourseOffered" role="tabpanel" aria-labelledby="profile-tab">
                                    <div class="row mt-3">
                                        <!-- content here -->
                                        <div class="col-lg-4 col-md-4 col-12" id="Div13">
                                            <div class="form-group">
                                                <label><sup>* </sup>Canvas College Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlCanvasCollegeNameTab3" title="" tabindex="1" name="ddlCanvasCollegeNameTab3">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div14">
                                            <div class="form-group">
                                                <label id="Label8"><sup>* </sup>Canvas Session Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlCanvasSessionTab3">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div15">
                                            <div class="form-group">
                                                <label id="Label9"><sup>* </sup>Canvas Curriculum Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlCanvasCurriculumTab3">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div16">
                                            <div class="form-group">
                                                <label id="Label10"><sup>* </sup>College Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlRFCollegeNameTab3" data-placeholder="Please Select" title="" tabindex="1" name="ddlRFCollegeNameTab3">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div17">
                                            <div class="form-group">
                                                <label id="Label11"><sup>* </sup>Session Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlRFSessionTab3" data-placeholder="Please Select" title="" tabindex="1" name="ddlRFSessionTab3">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div18">
                                            <div class="form-group">
                                                <label id="Label12"><sup>* </sup>Curriculum Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlRFCurriculumTab3" data-placeholder="Please Select" title="" tabindex="1" name="ddlRFCurriculumTab3">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div19">
                                            <div class="form-group">
                                                <label id="Label13"><sup>* </sup>Course Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlRFCourseNameTab3" multiple>
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <!-- check if section is also present -->
                                        <!-- content end -->
                                    </div>
                                    <!-- buttons here -->

                                    <div class="text-center" id="sendButton3">
                                        <button type="button" tabindex="1" class="btn btn-primary common-submit-btn" id="btnSubmitCourse">Submit</button>
                                        <button type="button" class="btn btn-outline-danger common-cancel-btn" tabindex="1" id="btnClearCourse">Cancel</button>

                                    </div>
                                    <!-- table here -->

                                    <!-- table end -->
                                    <div class="table-responsive all-data">
                                        <table id="tblTab3" class="table table-striped table-bordered">
                                            <thead>
                                                <tr>
                                                    <th data-field="canvas_instance_creation_id" data-formatter="canvasCreationFormatter" data-filter-control="input" data-sortable="true">Canvas Instance Name</th>
                                                    <th data-field="College_Name" data-filter-control="input" data-sortable="true">College Name</th>
                                                    <th data-field="Session_Name" data-filter-control="input" data-sortable="true">Session Name</th>
                                                    <th data-field="Scheme_Name" data-filter-control="input" data-sortable="true">Scheme Name</th>
                                                    <th data-field="Course_Name" data-filter-control="input" data-sortable="true">Course Name</th>
                                                    <th data-field="ModifiedDate" data-filter-control="input" data-sortable="true" data-formatter="quickFormTimeFormatter">Created Date</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tbody"></tbody>
                                        </table>
                                    </div>
                                    <!-- tab end -->
                                </div>

                                <!-- user allocation tab -->
                                <div class="tab-pane fade" id="tabUserAllocation" role="tabpanel" aria-labelledby="profile-tab">
                                    <div class="row mt-3">
                                        <!-- content here -->
                                        <div class="col-lg-4 col-md-4 col-12" id="Div20">
                                            <div class="form-group">
                                                <label><sup>* </sup>Canvas College Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlCanvasCollegeNameTab4" title="" tabindex="1" name="ddlCanvasCollegeNameTab4">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div21">
                                            <div class="form-group">
                                                <label id="Label14"><sup>* </sup>Canvas Session Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlCanvasSessionTab4">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div22">
                                            <div class="form-group">
                                                <label id="Label15"><sup>* </sup>Canvas Curriculum Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlCanvasCurriculumTab4">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div23">
                                            <div class="form-group">
                                                <label id="Label16"><sup>* </sup>Canvas Course Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlCanvasCourseTab4">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div24">
                                            <div class="form-group">
                                                <label id="Label17"><sup>* </sup>College Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlRFCollegeNameTab4">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div25">
                                            <div class="form-group">
                                                <label id="Label18"><sup>* </sup>Session Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlRFSessionTab4">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div26">
                                            <div class="form-group">
                                                <label id="Label19"><sup>* </sup>Curriculum Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlRFCurriculumTab4">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div27">
                                            <div class="form-group">
                                                <label id="Label20"><sup>* </sup>Course Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlRFCourseNameTab4">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div28">
                                            <div class="form-group">
                                                <label id="Label21"><sup>* </sup>Section Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlRfSectionTab4">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div29">
                                            <div class="form-group">
                                                <label id="Label22"><sup>* </sup>Canvas Section Name<code></code></label>
                                                <select class="form-control select2 form-select required-select2-validation" id="ddlCanvasSectionTab4">
                                                    <option value="0">Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div30">
                                            <div class="subCard">
                                                <div class="subCard-header">
                                                    Teachers
                                                </div>
                                                <div class="subCard-body">
                                                    <!-- subcard content -->
                                                    <ul class="mb-0" id="teacherList">
                                                        <div class="text-center">Please Select A Course</div>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-12" id="Div31">
                                            <div class="subCard">
                                                <div class="subCard-header">
                                                    Students
                                                </div>
                                                <div class="subCard-body">
                                                    <!-- subcard content -->
                                                    <ul class="mb-0" id="studentList">
                                                        <div class="text-center">Please Select A Course</div>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- content end -->
                                    </div>
                                    <!-- buttons here -->

                                    <div class="text-center mt-4" id="Div32">
                                        <button type="button" tabindex="1" class="btn btn-primary common-submit-btn" id="btnSubmitUser">Submit</button>
                                        <button type="button" class="btn btn-outline-danger common-cancel-btn" tabindex="1" id="btnClearUser">Cancel</button>
                                    </div>
                                    <!-- table here -->

                                    <!-- table end -->
                                    <div class="table-responsive all-data">
                                        <table id="tblTab4" class="table table-striped table-bordered">
                                            <thead>
                                                <tr>
                                                    <th data-field="canvas_instance_creation_id" data-formatter="canvasCreationFormatter" data-filter-control="input" data-sortable="true">Canvas Instance Name</th>
                                                    <th data-field="College_Name" data-filter-control="input" data-sortable="true">College Name</th>
                                                    <th data-field="Session_Name" data-filter-control="input" data-sortable="true">Session Name</th>
                                                    <th data-field="Scheme_Name" data-filter-control="input" data-sortable="true">Scheme Name</th>
                                                    <th data-field="Course_Name" data-filter-control="input" data-sortable="true">Course Role</th>
                                                    <th data-field="rf_user_type" data-filter-control="input" data-sortable="true">User Type</th>
                                                    <th data-field="ModifiedDate" data-filter-control="input" data-sortable="true" data-formatter="quickFormTimeFormatter">Created Date</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tbody1"></tbody>
                                        </table>
                                    </div>

                                    <!-- tab end -->
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12 col-sm-12 col-12 mt-3">
                        <div class="alert alert-secondary alert-has-icon">
                            <div class="alert-icon"><i class="far fa-bell"></i></div>
                            <div class="alert-body">
                                <div class="alert-title mt-1"></div>
                                Once the data has been uploaded, It will be <b>removed</b> from the dropdown.
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>


