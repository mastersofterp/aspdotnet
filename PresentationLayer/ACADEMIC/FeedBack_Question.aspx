<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FeedBack_Question.aspx.cs" Inherits="ACADEMIC_FeedBack_Question" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ques_test .dataTables_scrollHeadInner {
            width: max-content !important;
        }
        /*#exit_feedback .dataTables_scrollHeadInner {
            width: max-content !important;
        }*/
    </style>
    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#exit_feedback').DataTable({
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
                                return $('#exit_feedback').DataTable().column(idx).visible();
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
                                            return $('#exit_feedback').DataTable().column(idx).visible();
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
                                            return $('#exit_feedback').DataTable().column(idx).visible();
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
                var table = $('#exit_feedback').DataTable({
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
                                    return $('#exit_feedback').DataTable().column(idx).visible();
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
                                               return $('#exit_feedback').DataTable().column(idx).visible();
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
                                               return $('#exit_feedback').DataTable().column(idx).visible();
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

    <script>
        function getVal() {
            var array = []
            var semNo;
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (semNo == undefined) {
                    semNo = checkboxes[i].value + ',';
                }
                else {
                    semNo += checkboxes[i].value + ',';
                }
            }
            //alert(sectionNo);
            $('#<%= hdnSemester.ClientID %>').val(semNo);
            //document.getElementById(inpHide).value = "degreeNo";
        }
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

                            <div>
                                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updQuestion"
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

                            <asp:UpdatePanel ID="updQuestion" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-12">
                                            <div>
                                                <div id="div1" runat="server"></div>

                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Feedback Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCT" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                    OnSelectedIndexChanged="ddlCT_SelectedIndexChanged"
                                                                    ToolTip="Please Select Feedback Type">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>

                                                                <asp:RequiredFieldValidator ID="rfvCTid" runat="server"
                                                                    ControlToValidate="ddlCT" Display="None"
                                                                    ErrorMessage="Please Select Feedback Type" InitialValue="0"
                                                                    SetFocusOnError="True" ValidationGroup="FeedBack">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divSubjectType" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Subject Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSubType" runat="server" AppendDataBoundItems="true"
                                                                    AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                    OnSelectedIndexChanged="ddlSubType_SelectedIndexChanged" ToolTip="Please Select Subject">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                    ControlToValidate="ddlSubType" Display="None"
                                                                    ErrorMessage="Please Select Subject Type" InitialValue="0"
                                                                    SetFocusOnError="True" ValidationGroup="FeedBack">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Semester</label>
                                                                </div>
                                                                <asp:ListBox ID="ddlSemester" runat="server" AppendDataBoundItems="true"
                                                                    CssClass="form-control  multi-select-demo" SelectionMode="multiple"
                                                                    OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:ListBox>
                                                                <asp:HiddenField ID="hdnSemester" runat="server" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                    ControlToValidate="ddlSemester" Display="None"
                                                                    ErrorMessage="Please Select Semester" SetFocusOnError="True"
                                                                    ValidationGroup="FeedBack">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Feedback Question</label>
                                                                </div>
                                                                <asp:TextBox ID="txtQuestion" runat="server" TextMode="MultiLine" CssClass="form-control"
                                                                    placeholder="Please Enter Feedback Question (Max. 125 char) ."
                                                                    ToolTip="Please Enter Feedback Question">
                                                                </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvQuestion" runat="server"
                                                                    ControlToValidate="txtQuestion" Display="None"
                                                                    ErrorMessage="Please Enter Feedback Question" SetFocusOnError="True"
                                                                    ValidationGroup="FeedBack">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%--<sup>* </sup>--%>
                                                                    <label>Is Consider for Calculation?</label>
                                                                </div>
                                                                <asp:CheckBox ID="chkCalculation" runat="server" Checked="true" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Ans. Option Type :</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAnsoption" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                    ToolTip="Please Select Option" AutoPostBack="true" OnSelectedIndexChanged="ddlAnsoption_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Radio Button List</asp:ListItem>
                                                                    <asp:ListItem Value="2">Text Box</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddland" runat="server" InitialValue="0" ControlToValidate="ddlAnsoption"
                                                                    SetFocusOnError="true" ValidationGroup="FeedBack" ErrorMessage="Please Select Ans. Option Type"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Sequence No. :</label>
                                                                </div>
                                                                <asp:TextBox ID="txtseqno" runat="server" CssClass="form-control" MaxLength="4" ToolTip="Please Enter Sequence No"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvseqno" runat="server" ControlToValidate="txtseqno"
                                                                    Display="None" ErrorMessage="Please Enter Sequence No." SetFocusOnError="True"
                                                                    ValidationGroup="FeedBack">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftdatetime" runat="server" TargetControlID="txtseqno"
                                                                    ValidChars="0123456789" FilterMode="validChars" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divHeaderque" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Header Question</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlHeaderQue" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                    ToolTip="Please Select Header Question">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                    ControlToValidate="ddlHeaderQue" Display="None"
                                                                    ErrorMessage="Please Select Header Question" InitialValue="0"
                                                                    SetFocusOnError="True" ValidationGroup="FeedBack">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%--<sup>* </sup>--%>
                                                                    <label>Course Type : </label>
                                                                </div>

                                                                <asp:RadioButton ID="rdoTheory" runat="server" Text="Theory" GroupName="act_status" />&nbsp;&nbsp;
                                                                <asp:RadioButton ID="rdoPractical" runat="server" Text="Practical" GroupName="act_status" />
                                                                <asp:RadioButton ID="rdoNone1" runat="server" Text="None" GroupName="act_status" checked="true"/>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%--<sup>* </sup>--%>
                                                                    <label>Choice For : </label>
                                                                </div>

                                                                <asp:RadioButton ID="rdoStudent" runat="server" Text="Student" GroupName="act_faculty" />&nbsp;&nbsp;
                                                                <asp:RadioButton ID="rdoFaculty" runat="server" Text="Faculty" GroupName="act_faculty" />
                                                                <asp:RadioButton ID="rdoNone2" runat="server" Text="None" GroupName="act_faculty" checked="true"/>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12" id="divansoption" runat="server" visible="false">
                                                        <div class="sub-heading">
                                                            <h5>Answer Options</h5>
                                                        </div>
                                                        <div class="table-responsive" style="overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <asp:GridView ID="gvAnswers" runat="server" AutoGenerateColumns="false"
                                                                OnRowCreated="gvAnswers_RowCreated" ShowFooter="true" CssClass="table table-hovered table-bordered ">
                                                                <Columns>
                                                                    <asp:BoundField DataField="RowNumber" HeaderText="Sr.No." ItemStyle-Width="200px" />
                                                                    <asp:TemplateField HeaderText="Answers">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtAnsOption" runat="server" MaxLength="150" class="form-control"
                                                                                ondrop="return false;" onpaste="return;" onkeypress="return RestrictCommaSemicolon(event);" onkeyup="ConvertEachFirstLetterToUpper(this.id)">
                                                                            </asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                        </EditItemTemplate>
                                                                        <ControlStyle Width="300px"></ControlStyle>
                                                                        <ItemStyle Width="300px"></ItemStyle>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Value">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtValue" runat="server" MaxLength="5" class="form-control">
                                                                            </asp:TextBox>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteValue" runat="server"
                                                                                FilterType="Custom" FilterMode="ValidChars" ValidChars="1234567890" TargetControlID="txtValue">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>



                                                                        </ItemTemplate>

                                                                        <EditItemTemplate>
                                                                        </EditItemTemplate>
                                                                        <ControlStyle Width="300px"></ControlStyle>
                                                                        <ItemStyle Width="300px"></ItemStyle>

                                                                        <FooterStyle />
                                                                        <FooterTemplate>
                                                                            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                                                                                Text="Add New Option" CssClass="btn btn-primary" />
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <%--<asp:LinkButton ID="lnkRemove" runat="server" OnClick="lnkRemove_Click">
                                                        Remove
                                                     </asp:LinkButton>--%>
                                                                            <asp:ImageButton ID="lnkRemove" runat="server" OnClick="lnkRemove_Click"
                                                                                ImageUrl="~/IMAGES/delete.png" AlternateText="Remove Row"
                                                                                OnClientClick="return UserDeleteConfirmation();"></asp:ImageButton>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                        </EditItemTemplate>
                                                                        <ControlStyle Width="20px"></ControlStyle>
                                                                        <ItemStyle Width="50px"></ItemStyle>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <HeaderStyle CssClass="bg-light-blue main" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Active Status</label>
                                                                </div>
                                                                <asp:CheckBox ID="chkActiveStatus" runat="server" TextAlign="Left" Checked="true" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Check for Mandatory</label>
                                                                </div>
                                                                <asp:CheckBox ID="chkMandatory" runat="server" TextAlign="Left" Checked="true" />
                                                            </div>
                                                            <div class="form-group col-lg-4 col-md-12 col-12">
                                                                <div class=" note-div">
                                                                    <h5 class="heading">Note </h5>
                                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Checked:Status - <span style="color: green; font-weight: bold">Active </span></span></p>
                                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>UnChecked:Status - <span style="color: red; font-weight: bold">DeActive </span></span></p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="FeedBack" CssClass="btn btn-primary" />
                                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="FeedBack" />
                                                    </div>

                                                    <div class="col-12" id="ques_test">
                                                        <asp:ListView ID="lvQuestion" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Questions</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <%-- <th>Sr.No.
                                                    </th>--%>
                                                                            <th>Edit
                                                                            </th>
                                                                            <th>Course/Teacher
                                                                            </th>
                                                                            <th>Subject Type
                                                                            </th>
                                                                            <th>Semester
                                                                            </th>
                                                                            <th>Question
                                                                            </th>
                                                                            <th>Options
                                                                            </th>
                                                                            <th>Active Status
                                                                            </th>
                                                                            <th>Seq. No
                                                                            </th>
                                                                            <th>Course Type</th>
                                                                            <th>Choice For</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <%--   <td>
                                                <%# Container.DataItemIndex + 1%>
                                            </td>--%>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record"
                                                                            CommandArgument='<%# Eval("QUESTIONID") %>' ImageUrl="~/images/Edit.png"
                                                                            OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("CTNAME") %>
                                                                        <asp:HiddenField ID="hfdCtid" runat="server" Value='<%# Eval("CTID") %>' />
                                                                    </td>

                                                                    <td><%# Eval("SUBJECTTYPE")%> </td>
                                                                    <td>
                                                                        <%# Eval("SEMESTERNAME")%>
                                                                        <asp:HiddenField ID="hfdSemesterNo" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                                    </td>

                                                                    <td>
                                                                        <%# Eval("QUESTIONNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rblOptions" Visible='<%#(Convert.ToString(Eval("OPTION_TYPE"))=="R" ? true : false)%>' runat="server" CssClass="radionButtonList"
                                                                            RepeatDirection="Horizontal" RepeatLayout="Flow" ToolTip="Options to be select">
                                                                        </asp:RadioButtonList>
                                                                        <asp:TextBox ID="txtoptions" runat="server" Visible='<%#(Convert.ToString(Eval("OPTION_TYPE"))=="T" ? true : false)%>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnOptions" runat="server"
                                                                            Value='<%# Eval("QUESTIONID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DETAILACTIVE_STATUS")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SEQ_NO")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("COURSE_TYPE")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("CHOISE_FOR")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <%-- <AlternatingItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record"
                                                        CommandArgument='<%# Eval("QUESTIONID") %>' ImageUrl="~/images/Edit.gif"
                                                        OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td>
                                                    <%# Eval("CTNAME") %>
                                                </td>

                                                  <td>  <%# Eval("SUBJECTTYPE")%> </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>

                                                <td>
                                                    <%# Eval("QUESTIONNAME")%>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rblOptions" runat="server" CssClass="radionButtonList"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" ToolTip="Options to be select">
                                                    </asp:RadioButtonList>
                                                    <asp:HiddenField ID="hdnOptions" runat="server"
                                                        Value='<%# Eval("QUESTIONID") %>' />
                                                </td>



                                                <td>
                                                    <%# Eval("DETAILACTIVE_STATUS")%>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>--%>
                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="ddlCT" />
                                    <asp:PostBackTrigger ControlID="ddlSubType" />
                                    <asp:PostBackTrigger ControlID="ddlSemester" />
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                </Triggers>
                            </asp:UpdatePanel>

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

    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>--%>
    <script>
        $(document).ready(function () {
            $(<%=txtQuestion.ClientID%>).on('input propertychange', function () {
                CharLimit(this, 125);
            });
        });

        function CharLimit(input, maxChar) {
            var len = $(input).val().length;
            if (len > maxChar) {
                $(input).val($(input).val().substring(0, maxChar));
                alert("Allowed Only Max(125) Char. ");
            }
        }
    </script>

    <%-- <script>
       $(<%=txtQuestion.ClientID%>).on('keypress', function () {
           if ($(this).val().length > 500) {
               alert("Allowed Only Max(500) Char. ");
               return false;
           }

       });
       var prm = Sys.WebForms.PageRequestManager.getInstance();
       prm.add_endRequest(function () {
           $(<%=txtQuestion.ClientID%>).on('keypress', function () {
                 if ($(this).val().length > 500) {
                     alert("Allowed Only Max(500) Char. ");
                     return false;
                 }

             });
       });
       </script>--%>

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
