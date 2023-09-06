<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DailyAdmissionStatus.aspx.cs" Inherits="ACADEMIC_DailyAdmissionStatus" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_divSelectedStudents .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <style>
        #ctl00_ContentPlaceHolder1_lvStudent_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"


            });

        });
    </script>--%>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblStdList').DataTable({
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
                                return $('#tblStdList').DataTable().column(idx).visible();
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
                                            return $('#tblStdList').DataTable().column(idx).visible();
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
                                            return $('#tblStdList').DataTable().column(idx).visible();
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
                var table = $('#tblStdList').DataTable({
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
                                    return $('#tblStdList').DataTable().column(idx).visible();
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
                                                return $('#tblStdList').DataTable().column(idx).visible();
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
                                                return $('#tblStdList').DataTable().column(idx).visible();
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Daily Admission Status</h3>
                    <%--<h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label><small style="text-transform: capitalize;"></small></h3>--%>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Email Configuration</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Send Admission Status Mail</a>
                            </li>
                        </ul>
                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnladmstatus"
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
                                <asp:UpdatePanel ID="pnladmstatus" runat="server">
                                    <ContentTemplate>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group  col-12">
                                                    <div class="label-dynamic">
                                                        <label><sup>*</sup>To </label>


                                                    </div>
                                                    <asp:TextBox ID="txtToMail" runat="server" CssClass="form-control" MaxLength="512" TabIndex="1" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="rfvTo" runat="server" ControlToValidate="txtToMail" ValidationGroup="SubmitMail"
                                                        ErrorMessage="Please Enter To Mail" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                    <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email Address" ControlToValidate="txtToMail"
                                                ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" SetFocusOnError="True" Display="None" ValidationGroup="SubmitMail" />--%>
                                                </div>

                                                <div class="form-group  col-12">
                                                    <div class="label-dynamic">
                                                        <label>Cc Email</label>
                                                    </div>
                                                    <asp:TextBox ID="txtCC" runat="server" CssClass="form-control" MaxLength="512" TabIndex="2" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>

                                                    <%--                                                      <asp:RequiredFieldValidator ID="rfvCC" runat="server" ControlToValidate="txtCC" ValidationGroup="submit"
                                                        ErrorMessage="Please Enter CC Mail" Display="None" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>

                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Email Address" ControlToValidate="txtCC"
                                                ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" SetFocusOnError="True" Display="None" ValidationGroup="SubmitMail" />--%>
                                                </div>

                                                <div class="form-group   col-12">
                                                    <div class="label-dynamic">
                                                        <%-- <sup>* </sup>--%>
                                                        <%--    <asp:Label ID="lblBCC" runat="server" Font-Bold="true"></asp:Label>--%>
                                                        <label>Bcc Email</label>
                                                        <%-- <label>BCC</label>--%>
                                                    </div>
                                                    <asp:TextBox ID="txtBCC" runat="server" CssClass="form-control" MaxLength="512" TabIndex="3" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>

                                                    <%--                                                      <asp:RequiredFieldValidator ID="rfvBCC" runat="server" ControlToValidate="txtBCC" ValidationGroup="submit"
                                                        ErrorMessage="Please Enter BCC Mail" Display="None" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>

                                                    <%--                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Email Address" ControlToValidate="txtBCC"
                                                ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" SetFocusOnError="True" Display="None" ValidationGroup="SubmitMail" />--%>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmitMail" runat="server" Text="Submit" ValidationGroup="SubmitMail" CssClass="btn btn-primary" TabIndex="4" OnClick="btnSubmitMail_Click" />
                                            <asp:Button ID="bntCancel" TabIndex="5" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="bntCancel_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="false" ValidationGroup="SubmitMail" />
                                        </div>





                                    </ContentTemplate>
                                    <%-- <Triggers>
                                      
                                    </Triggers>--%>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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

                                <asp:UpdatePanel ID="Updsendmail" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">


                                                <div class="form-group col-lg-12 col-md-12 col-12" >
                                                    <asp:RadioButtonList ID="rblSelection" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblSelection_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Selected="True">&nbsp;&nbsp;&nbsp;Daily Admission Status&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                          <asp:ListItem Value="2" >&nbsp;&nbsp;&nbsp;Admission Batchwise Status&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>               
                                                    </asp:RadioButtonList>
                                                    <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="DivadmBatch" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>

                                                    <%--    <asp:DropDownList ID="ddladmbatch" runat="server" AppendDataBoundItems="True">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddladmbatch"
                                                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0"
                                                                            SetFocusOnError="True" ValidationGroup="send"></asp:RequiredFieldValidator>--%>
                                                    <asp:DropDownList runat="server" class="form-control" ID="ddladmbatch" AutoPostBack="true" AppendDataBoundItems="true"
                                                        data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddladmbatch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvadmbatch" runat="server" ControlToValidate="ddladmbatch"
                                                        Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="send"></asp:RequiredFieldValidator>

                                                </div>

                                                <div class="col-12 table-club">
                                                    <asp:Panel ID="pnllvsendemail" runat="server">
                                                        <asp:ListView ID="lvsendemail" runat="server" EnableModelValidation="True">
                                                            <%--<EmptyDataTemplate>
                                                                <div>
                                                                    -- No Student Record Found --
                                                                </div>
                                                            </EmptyDataTemplate>--%>
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Daily Admission Status Count  List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>College</th>
                                                                            <th>Degree </th>
                                                                            <th>Branch</th>
                                                                            <th>Student count</th>
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
                                                                        <%# Eval("COLLEGE") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DEGREE")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("BRANCH")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("STUDENT_COUNT")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>



                                                <%--  <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblactive" runat="server" Font-Bold="true">Status</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkactive" name="chkoutstandingfees" />
                                                            <label data-on="Yes" tabindex="2" data-off="No" for="chkactive"></label>
                                                        </div>
                                                    </div>--%>
                                            </div>
                                        </div>

                                        <div class="btn-footer">
                                            <asp:Button ID="btnsendemail" runat="server" Text="Send Mail"
                                                CssClass="btn btn-primary" ValidationGroup="send" OnClick="btnsendemail_Click" TabIndex="2" Visible="false" />

                                            <asp:Button ID="btnSendmailDailyStatus" runat="server" Text="Send Mail"
                                                CssClass="btn btn-primary" ValidationGroup="send" OnClick="btnSendmailDailyStatus_Click" TabIndex="3" Visible="false"/>

                                            <asp:Button ID="btnsendcancel" TabIndex="4" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnsendcancel_Click"   />

                                            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="send" />

                                        </div>

                                    </ContentTemplate>
                                    <%-- <Triggers>
                                       
                                    </Triggers>--%>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="divMsg" runat="server">
                </div>
            </div>
        </div>
    </div>


</asp:Content>
