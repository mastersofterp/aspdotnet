<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Exit_feedback_header_master.aspx.cs" Inherits="ACADEMIC_Exit_feedback_header_master" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        /*#ques_test .dataTables_scrollHeadInner {
            width: max-content !important;
        }*/

        .dataTables_scrollHeadInner {
            width: max-content!important;
        }
        /*#exit_feedback .dataTables_scrollHeadInner {
            width: max-content !important;
        }*/
    </style>

    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            // alert('ok');
            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });
        function bindDataTable() {
            var myDT = $('#ctl00_ContentPlaceHolder1_ddlSemester').multiselect({
                includeSelectAllOption: true
            });
            $('.btn-group').find('.multiselect').addClass('btn-block');
        }
    </script>
    <%--<style>
        .multiselect-selected-text {
        text-align:left !important
        }
        </style>
     <style type="text/css">
        .multiselect-selected-text {
        text-align:left !important
        }
        .multiselect.dropdown-toggle.btn {
        display: flex !important;
        display:-webkit-flex;
        flex-direction: row !important;
        align-items: center !important;
        border-radius:0;
        }

            .multiselect.dropdown-toggle.btn .caret {
            margin-left:5px
            }
        </style>--%>


    <%--<script>
        $(document).ready(function () {
            $('.btn-group').find('.multiselect').addClass('btn-block');
        });
        </script>

     <script>
         //var MulSel = $.noConflict();
         $(document).ready(function () {
             $('.multi-select-demo').multiselect();
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             prm.add_endRequest(function () {
                 $('.multi-select-demo').multiselect();
             });
         });
    </script>--%>



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
                InitAutoCompl();
            }
        });



    </script>


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

    <script type="text/javascript" language="javascript">
        function ConvertEachFirstLetterToUpper(id) {
            var txt = document.getElementById(id);
            txt.value = txt.value.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
        }
    </script>

    <script type="text/javascript">
        function RestrictCommaSemicolon(e) {
            var theEvent = e || window.event;
            var key = theEvent.keyCode || theEvent.which;
            key = String.fromCharCode(key);
            var regex = /[^,;]+$/;
            if (!regex.test(key)) {
                theEvent.returnValue = false;
                if (theEvent.preventDefault) {
                    theEvent.preventDefault();
                }
            }
        }
    </script>


    <asp:UpdatePanel runat="server" ID="UPDROLE" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <div class="tab-content" id="my-tab-content">

                                <div>
                                    <asp:UpdateProgress ID="UpdateExitheader" runat="server" AssociatedUpdatePanelID="UPDExitFeed"
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
                                <asp:UpdatePanel ID="UPDExitFeed" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-12">
                                                <div>
                                                    <div id="div3" runat="server"></div>

                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <%-- <div class="form-group col-lg-4 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Semester</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlExitsem" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                        OnSelectedIndexChanged="ddlExitsem_SelectedIndexChanged"
                                                                        ToolTip="Please Select Semester">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>

                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                        ControlToValidate="ddlExitsem" Display="None"
                                                                        ErrorMessage="Please Select Semester" InitialValue="0"
                                                                        SetFocusOnError="True" ValidationGroup="ExitFeedBack">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>--%>

                                                                <%--<div class="form-group col-lg-4 col-md-6 col-12" id="DivMultipleSelect" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Exit Feedback Questions</label>
                                                                    </div>

                                                                    <asp:ListBox ID="ddlExitQues" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>

                                                                </div>--%>

                                                                <%-- <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label>Exit Feedback Questions</label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlExitQues" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                                OnSelectedIndexChanged="ddlExitQues_SelectedIndexChanged"
                                                                                ToolTip="Please Select Exit Feedback Questions">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>

                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                                ControlToValidate="ddlExitQues" Display="None"
                                                                                ErrorMessage="Please Select Exit Feedback Questions" InitialValue="0"
                                                                                SetFocusOnError="True" ValidationGroup="ExitFeedBack">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>--%>

                                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Exit Feedback Question Header</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtFeedbackHeader" runat="server" TextMode="MultiLine" CssClass="form-control"
                                                                        placeholder="Please Enter Exit Feedback Question Header (Max. 120 char) ."
                                                                        ToolTip="Please Enter Feedback Question" MaxLength="120"  TabIndex="1">
                                                                    </asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                                        ControlToValidate="txtFeedbackHeader" Display="None"
                                                                        ErrorMessage="Please Enter Exit Feedback Question Header" SetFocusOnError="True"
                                                                        ValidationGroup="ExitFeedBack">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnExitSubmit" runat="server" OnClick="btnExitSubmit_Click" Text="Submit" ValidationGroup="ExitFeedBack"  TabIndex="2" CssClass="btn btn-primary" />
                                                            <asp:Button ID="btnExitCancel" runat="server" OnClick="btnExitCancel_Click" Text="Cancel" CssClass="btn btn-warning"  TabIndex="3"/>
                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="ExitFeedBack" />
                                                        </div>

                                                        <div class="col-12">
                                                            <asp:ListView ID="lvExitFeedback" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Exit Feedback Question Header</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Action
                                                                                </th>
                                                                                <%--<th>Question
                                                                                </th>--%>
                                                                                <th>Question Header
                                                                                </th>
                                                                                <%--<th>Semester
                                                                                </th>--%>
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
                                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("HEADER_ID") %>'
                                                                                AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="4" OnClick="btnEdit_Click" />
                                                                        </td>
                                                                        <%--<td>
                                                                            <asp:Label ID="Srno" runat="server" Text='<%# Eval("HEADER_ID") %>' ToolTip='<%# Eval("HEADER_ID") %>'></asp:Label>
                                                                        </td>--%>
                                                                        <td>
                                                                            <%# Eval("QUESTION_HEADER") %>
                                                                        </td>
                                                                        <%--<td>
                                                                            <%# Eval("QUESTION_HEADER")%> 
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SEMESTERNAME")%> 
                                                                        </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>

                                                            </asp:ListView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>




                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>


    <%--<script>
           $(document).ready(function () {
               $('#example').DataTable();
           });
            </script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            // alert('ok');
            bindDataTable1();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });
        function bindDataTable1() {
            var myDT = $('#example').DataTable();
        }
    </script>

    <%--<script type="text/javascript">
        $(document).ready(function () {

            $('.multiselect.dropdown-toggle').parent('.btn-group').css({ 'width': "100%" });
            $('.multiselect .dropdown-toggle').addClass('btn-block');
        });
    </script>--%>

    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to Remove this Entry?"))
                return true;
            else
                return false;
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

