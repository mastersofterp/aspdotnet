<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Degree_Completion_CriteriaConfig.aspx.cs" Inherits="Degree_Completion_CriteriaConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">Degree Completion Criteria Configuration</h3>--%>
                     <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div>
                        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDegreeConfig"
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
                    <asp:UpdatePanel ID="updDegreeConfig" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Scheme</label>--%>
                                            <asp:Label ID="lblDYddlScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" TabIndex="0" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfcscheme" runat="server" ControlToValidate="ddlScheme" SetFocusOnError="true" Display="None" ErrorMessage="Please Select Scheme." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%--<label>Min Credit Required</label>--%>
                                            <asp:Label ID="lblDYtxtMinCreditRequired" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtminCredit" runat="server" CssClass="form-control" TabIndex="0"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfcmincredit" runat="server" ControlToValidate="txtminCredit" SetFocusOnError="true" Display="None" ErrorMessage="Please Enter Minimum Credit." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%--<label>Core Courses</label>--%>
                                            <asp:Label ID="lblDYddlpreSubjects" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="ddlpreSubjects" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="0"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="rfcprereq" runat="server" ControlToValidate="ddlpreSubjects" SetFocusOnError="true" Display="None" ErrorMessage="Please Select Prerequisites Subjects." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Advance Configure</label>
                                        </div>
                                        <div class="switch form-inline" onclick="clickRdActive();">
                                            <input type="checkbox" id="rdConfig" runat="server" name="rdConfig" />
                                            <label data-on="Yes" data-off="No" for="rdConfig"></label>
                                            <asp:HiddenField ID="hfdadconfig" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12 d-none" id="divgrade" runat="server">
                                        <div class="label-dynamic">
                                            <label>Min. Grade Points Required</label>
                                        </div>
                                        <asp:TextBox ID="txtMinGradePoints" runat="server" CssClass="form-control" TabIndex="0"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12 d-none" id="divexempedsub" runat="server">
                                        <div class="label-dynamic">
                                            <label>Exempted Subjects</label>
                                        </div>
                                        <asp:ListBox ID="ddlExemptedSubject" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="0"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <div class=" col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="submit" Text="Submit" CssClass="btn btn-primary" TabIndex="0" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="btn btn-warning" TabIndex="0" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                            </div>

                           <%-- Set Course Catogory Wise Mix Max in ListView--%>
                            <div id="Div2" class="col-12 mt-3" runat="server" visible="false">
                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvGrade" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Scheme wise Defined Course Criteria</h5>
                                                </div>
                                                <table class="table table-striped " style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>SR.NO           
                                                            </th>
                                                            <th>COURSE CATEGORY           
                                                            </th>
                                                            <th>Min. Credit Points
                                                            </th>
                                                            <th>Max. Credit Points
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.DataItemIndex+1 %></td>
                                                    <td>
                                                        <asp:Label ID="lblcoursecateno" runat="server" Text='<%# (Eval("CATEGORYNAME").ToString() != string.Empty) ? (Eval("CATEGORYNAME")):0 %>' ToolTip='<%# (Eval("CATEGORYNO").ToString() != string.Empty) ? (Eval("CATEGORYNO")):0 %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblmingarde1" CssClass="form-control" runat="server" Text='<%# (Eval("MIN_GRADEPOINT").ToString() != string.Empty) ? (Eval("MIN_GRADEPOINT")):0 %>'></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblmaxgarde" CssClass="form-control" runat="server" Text='<%# (Eval("MAX_GRADEPOINT").ToString() != string.Empty) ? (Eval("MAX_GRADEPOINT")):0 %>'></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div class="col-12 mt-3" runat="server">
                                <asp:Panel ID="Panel" runat="server">
                                    <asp:ListView ID="lvdegreeconfig" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Degree Completion Criteria List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit           
                                                        </th>
                                                        <th>Scheme           
                                                        </th>
                                                        <th>Min. Credit Required
                                                        </th>
                                                        <th>Min. Grade Points
                                                        </th>
                                                        <th>Prerequisites Subjects </th>
                                                        <th>Exempted Subjects </th>
                                                    </tr>
                                                </thead>

                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DCCC_NO")%>' AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="0" OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblscheme" runat="server" Text='<%# Eval("SCHEMENAME")%>' ToolTip='<%# Eval("SCHEMENO")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblmincredit" runat="server" Text='<%# Eval("MIN_CREDIT")%>' ToolTip='<%# Eval("ADVANCE_CONFIG")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblmingarde" runat="server" Text='<%# Eval("MIN_GRADEPOINT")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblpreque" runat="server" Text='<%# Eval("PREREQ_SUBJECT_NAME")%>' ToolTip='<%# Eval("PREREQ_COURSENO")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblexmpted" runat="server" Text='<%# Eval("EXEMPT_SUBJECT_NAME")%>' ToolTip='<%# Eval("EXEMPT_COURSENO")%>'></asp:Label></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <script>
        function SetStatMandat(val) {
            $('#ctl00_ContentPlaceHolder1_rdConfig').prop('checked', val);
            ShowDropDown();
        }
    </script>

    <%--Script for Multiselect and search box in listview dropdown--%>
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
        function clickRdActive() {
            if ($('#ctl00_ContentPlaceHolder1_rdConfig').is(':checked')) {
                $('#ctl00_ContentPlaceHolder1_rdConfig').prop('checked', false);

                $('#ctl00_ContentPlaceHolder1_divexempedsub').addClass('d-none');
                $('#ctl00_ContentPlaceHolder1_divgrade').addClass('d-none');

                $("#ctl00_ContentPlaceHolder1_hfdadconfig option").attr("value", "false");
                $('#<%= txtMinGradePoints.ClientID %>').val('');

            }
            else {
                $('#ctl00_ContentPlaceHolder1_rdConfig').prop('checked', true);

                $('#ctl00_ContentPlaceHolder1_divexempedsub').removeClass('d-none');
                $('#ctl00_ContentPlaceHolder1_divgrade').removeClass('d-none');
                $("#ctl00_ContentPlaceHolder1_hfdadconfig").attr("value", "true");
            }
        }
    </script>

    <script>
        function ShowDropDown() {
            if ($('#ctl00_ContentPlaceHolder1_rdConfig').is(':checked')) {
                $('#ctl00_ContentPlaceHolder1_divexempedsub').removeClass('d-none');
                $('#ctl00_ContentPlaceHolder1_divgrade').removeClass('d-none');
            }
            else {

                $('#ctl00_ContentPlaceHolder1_divexempedsub').addClass('d-none');
                $('#ctl00_ContentPlaceHolder1_divgrade').addClass('d-none');
            }
        }
    </script>


</asp:Content>

