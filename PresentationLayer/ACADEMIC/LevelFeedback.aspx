<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LevelFeedback.aspx.cs" Inherits="LevelFeedback" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .myTable tbody tr td, .table-striped tbody tr td label {
            font-weight: bold;
        }
    </style>
    <script>
        $(document).ready(function () {
            $('.myTable.table-bordered > tbody > tr:nth-of-type(odd)').addClass("df");

            var myColors = [
                '#c7d8ea', '#b4da72', '#f7e76e', '#f2b78d', '#abd3bc', '#f5a5a3', '#96e8e1', '#e2dfa2', '#d9d8da', '#ccccb3', '#e6b3b3', '#b3e6cc'
            ];
            var i = 0;
            $('.df').each(function () {
                $(this).css('background-color', myColors[i]);
                i = (i + 1) % myColors.length;
            });

            $('.myTable.table-bordered > tbody > tr:nth-of-type(even)').addClass("gf");

            var myColors = [
                  '#c7d8ea', '#b4da72', '#f7e76e', '#f2b78d', '#abd3bc', '#f5a5a3', '#96e8e1', '#e2dfa2', '#d9d8da', '#ccccb3', '#e6b3b3', '#b3e6cc'
            ];
            var i = 0;
            $('.gf').each(function () {
                $(this).css('background-color', myColors[i]);
                i = (i + 1) % myColors.length;
            });
        });
    </script>

    <style>
        .RadioButtonWidth tbody tr:nth-of-type(odd) {
            border-bottom: 0px solid transparent;
            background-color: transparent !important;
        }

        .RadioButtonWidth .table-bordered td {
            border: 0px solid #dee2e6;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-5 col-md-12 col-12" id="divnotemsg" runat="server">
                                <div class=" note-div">
                                    <h5 class="heading">Note </h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>MT - Main Teacher || ADT - Additional Teacher</span> </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlStudInfo" runat="server" Visible="false">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-lg-5 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Student Name  :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblName" runat="server" Font-Bold="true" Style="color: green"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>
                                            <asp:Label ID="lblDYtxtSession" runat="server" Font-Bold="true"></asp:Label>
                                            :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSession" runat="server" Font-Bold="true" Style="color: green"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-7 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>
                                            <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                            :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblScheme" runat="server" Font-Bold="true" Style="color: green"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSemester" runat="server" Font-Bold="true" Style="color: green"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Session : </label>
                                    </div>
                                    <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" ToolTip="Please Select Session" runat="server" TabIndex="1" data-select2-enable="true"
                                        CssClass="form-control" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="valSession" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please select session." SetFocusOnError="true" ValidationGroup="submit"
                                        InitialValue="0" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divfeedbacktype" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Feedback Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlFeedbackTyp" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlFeedbackTyp_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 mt-3 mb-3" id="examrow" runat="server" visible="false">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12 mt-2">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Exam</label>
                                    </div>
                                    <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="true"
                                        Visible="false" CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Exam Name">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 mt-4">
                            <asp:Panel ID="pnlSubject" runat="server">
                                <asp:ListView ID="lvSelected" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>SrNo
                                                    </th>
                                                    <th>Subject Code - Subject Name - Faculty Name - Faculty Type
                                                    </th>
                                                    <th>Semester
                                                    </th>
                                                    <th>Feedback Status
                                                    </th>
                                                    <th>Final Submit Status
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
                                            <td>
                                                <%# Container.DataItemIndex + 1%>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkbtnCourse" runat="server" CommandArgument='<%# Eval("COURSENO")%>' CommandName='<%# Eval("SEMESTERNO")%>'
                                                    OnClick="lnkbtnCourse_Click" Text='<%#(String.IsNullOrEmpty(Eval("UA_FULLNAME").ToString()))?GetCourseName(Eval("COURSENAME"),"No FACULTY","No FACULTY TYPE"):GetCourseName(Eval("COURSENAME"),Eval("UA_FULLNAME"),Eval("TEACHER")) %>'
                                                    ToolTip='<%# Eval("ua_no")%>' />
                                                <%--ToolTip='<%# (Convert.ToInt32(Eval("SUBID"))==1 ||Convert.ToInt32(Eval("SUBID"))==3 || 
                                                          Convert.ToInt32(Eval("SUBID"))==13)?Eval("ad_teacher_th"):Eval("ad_teacher_pr")%>'--%>
                                                <asp:HiddenField ID="hdnSubId" runat="server" Value='<%# Eval("SUBID")%>' />
                                                <asp:HiddenField ID="hdnserialno" runat="server" Value='<%# Container.DataItemIndex + 1%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblsem" Text='<%# Eval("SEMESTERNO")%>' runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblComplete" Text='<%# Eval("Status")%>' runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFinalSubmit" Text='<%# Eval("Status")%>' runat="server" Visible="false" ToolTip='<%# Eval("Final_Submit_Status")%>' CommandArgument='<%# Eval("is_Submit")%>'></asp:Label>
                                                <asp:Label ID="lblFinalSubmittext" Text='<%# Eval("Final_Submit_Text")%>' runat="server" ToolTip='<%# Eval("Final_Submit_Text")%>' CommandArgument='<%# Eval("is_Submit")%>'></asp:Label>
                                                <asp:HiddenField ID="hdnFinalSubmitted" Value='<%# Eval("Final_Submit_Status")%>' runat="server" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                        <div class="col-12">
                            <asp:Button ID="btnReport" runat="server"
                                Text="Report" CssClass="btn btn-outline-primary" ValidationGroup="Report" Visible="false" />
                            <asp:Button ID="btnClear" runat="server" Text="Cancel"
                                Visible="false" CssClass="btn btn-outline-danger" />
                            <asp:ValidationSummary ID="ValidationSummary3" runat="server"
                                DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="Report" />
                        </div>
                        <div class="col-12">
                            <asp:Label ID="lblMsg" runat="server" Visible="false"> <span ID="spMsg" 
                                    style="color:Red;"></span></asp:Label>
                        </div>
                    </asp:Panel>


                    <asp:Panel ID="pnlFeedback" runat="server" Visible="false">
                        <div class="form-group col-lg-8 col-md-12 col-12" hidden="hidden">
                            <div class=" note-div">
                                <h5 class="heading">Ratings </h5>
                                <p>
                                    <span>5 = <span style="color: green; font-weight: bold">Excellent, </span></span>&nbsp;&nbsp;
                                      <span>4 = <span style="color: green; font-weight: bold">Great, </span></span>&nbsp;&nbsp;
                                      <span>3 = <span style="color: green; font-weight: bold">Good, </span></span>&nbsp;&nbsp;
                                      <span>2 = <span style="color: green; font-weight: bold">Fair, </span></span>&nbsp;&nbsp;
                                      <span>1 = <span style="color: green; font-weight: bold">Poor </span></span>
                                </p>
                            </div>
                        </div>

                        <div class="col-12 mt-4">
                            <div class="sub-heading">
                                <h5>
                                    <asp:Label ID="lblcrse" runat="server" Visible="false"></asp:Label></h5>
                            </div>
                            <asp:ListView ID="lvCourse" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                            <tr id="itemPlaceholder" runat="server" />
                                        </table>
                                    </div>
                                    <%-- </div>--%>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><asp:Label ID="lblmandatory" runat="server" Text='*' style ="color:red;" Visible='<%#(Convert.ToString(Eval("IS_MANDATORY"))=="1" ? true : false)%>'></asp:Label> Q&nbsp;<%# Container.DataItemIndex + 1%><asp:Label ID="lblCourse" runat="server" Text='  <%# Eval("QUESTIONID")%>' Visible="false"></asp:Label>.</td>
                                        <td><%# Eval("QUESTIONNAME")%></td>
                                    </tr>
                                    <tr>
                                        <td>Ans:&nbsp; </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblCourse" runat="server" Class="spaced" Visible='<%#(Convert.ToString(Eval("OPTION_TYPE"))=="R" ? true : false)%>'
                                                CssClass="RadioButtonWidth" Style="margin-left: -10px"
                                                RepeatDirection="Horizontal" ToolTip='<%# Eval("QUESTIONID") %>'>
                                            </asp:RadioButtonList>
                                            <asp:TextBox ID="txtcourse" runat="server" Visible='<%#(Convert.ToString(Eval("OPTION_TYPE"))=="T" ? true : false)%>'
                                                ToolTip='<%# Eval("QUESTIONID") %>' MaxLength="200"></asp:TextBox>
                                            <asp:HiddenField ID="hdnCourse" runat="server" Value='<%# Eval("QUESTIONID") %>' />
                                            <asp:HiddenField ID="hfOPTION_TYPE" runat="server" Value='<%# Eval("OPTION_TYPE") %>' />
                                            <asp:HiddenField ID="hdnIsMandatory" runat="server" Value='<%# Eval("IS_MANDATORY") %>' />

                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <div class="col-12" id="divcomment" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Comments If Any (max 1000 chars) : </label>
                                        </div>
                                        <asp:TextBox ID="txtComments" runat="server" BorderStyle="Ridge" TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="col-12">
                            <u><b>
                                <asp:Label ID="lblteacher" runat="server" Visible="false"></asp:Label></b></u>
                            <asp:ListView ID="lvTeacher" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                            <tr id="itemPlaceholder" runat="server" />
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>Q&nbsp;<%# Container.DataItemIndex + 1%><asp:Label ID="lblTeacher" runat="server" Text='<%# Eval("QUESTIONID") %>' Visible="false"></asp:Label>.</td>
                                        <td><%# Eval("QUESTIONNAME") %></td>
                                    </tr>
                                    <tr>
                                        <td>Ans:&nbsp; </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblTeacher" runat="server" RepeatDirection="Horizontal" CssClass="RadioButtonWidth" ToolTip='<%# Eval("QUESTIONID") %>' Style="margin-left: -10px;">
                                            </asp:RadioButtonList>
                                            <asp:HiddenField ID="hdnTeacher" runat="server" Value='<%# Eval("QUESTIONID") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>



                        <div class="form-group col-lg-12 col-md-12 col-12" style="display: none">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <asp:Label ID="lblWhatOtherChanges" Style="font-weight: bold;" runat="server" Text="Any other suggestions:"></asp:Label>
                            </div>
                            <asp:TextBox ID="txtWhatOtherChanges" runat="server" TextMode="MultiLine"
                                placeholder="Please enter Any other suggestions (Max. 200 char) ."
                                oncopy="return false;" onpaste="return false;" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWhatOtherChanges"
                                ErrorMessage="Please Enter Any other suggestions" SetFocusOnError="True" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="form-group col-lg-12 col-md-12 col-12" style="display: block" id="divfeedback" runat="server" visible="false">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>
                                    <asp:Label ID="lblAnyComments" runat="server" Style="font-weight: bold;" Text="Mention the topics to be included in the syllabus because they are prerequisite / relevant / contemporary / required for employment / correlating with course outcomes etc."></asp:Label></label>
                            </div>
                            <asp:TextBox ID="txtAnyComments" runat="server" TextMode="MultiLine"
                                placeholder="Please enter comments (Max. 200 char) ." CssClass="form-control" MaxLength="100"
                                oncopy="return false;" onpaste="return false;"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAnyComments"
                                ErrorMessage="Mention the topics to be included in the syllabus because they are prerequisite / relevant / contemporary / required for employment / correlating with course outcomes etc." 
                                SetFocusOnError="True" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group col-lg-12 col-md-12 col-12 d-none">
                            <div class="label-dynamic">
                                <sup></sup>
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Any additional Remarks (write briefly)?"></asp:Label></label>
                            </div>
                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" placeholder="Please enter comments (Max. 100 char) ." CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlFinalSumbit" runat="server" Visible="false">
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Save & Next" OnClick="btnSubmit_Click" CssClass="btn btn-info"
                                ValidationGroup="Submit" />
                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="btnPrevious_Click" CssClass="btn btn-info"
                                ValidationGroup="Submit" />
                            <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" CssClass="btn btn-primary"
                                ValidationGroup="Submit" Visible="false" />
                            <asp:Button ID="btnFinalSubmit" runat="server" Text="Final Submit" OnClick="btnFinalSubmit_Click" CssClass="btn btn-info"
                                ValidationGroup="Submit" Visible="false" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="Submit" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlSubmit" runat="server" Visible="false">
                        <div class="col-12 btn-footer">
                             <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" CssClass="btn btn-info"
                                ValidationGroup="Submit" />
                            <asp:Button ID="btnHide" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                        </div>
                    </asp:Panel>
                    <div class="col-12 text-center">
                        <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                            <span style="font-size: large; color: Red;">
                                <b>Teacher Not Allot!! You Cann't Give FeedBack!<br />
                                    Please Contact Administrator! </b>
                            </span>
                        </asp:Panel>
                    </div>
                    <div class="col-12 text-center">
                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                            <span style="font-size: large; color: Red;">
                                <b>No Course registration found for activity started session.</b>
                            </span>
                        </asp:Panel>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <%--<style>
                .RadioButtonWidth input {
                    margin-left: 10px;
                }
            </style>--%>

    <div id="divMsg" runat="server"></div>



    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>

    <script>
        $(<%=txtAnyComments.ClientID%>).on('keypress', function () {
            if ($(this).val().length > 200) {
                alert("Allowed Only Max(200) Char. ");
                return false;
            }

        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(<%=txtAnyComments.ClientID%>).on('keypress', function () {
                if ($(this).val().length > 200) {
                    alert("Allowed Only Max(200) Char. ");
                    return false;
                }

            });
        });



        $(<%=txtWhatOtherChanges.ClientID%>).on('keypress', function () {
            if ($(this).val().length > 200) {
                alert("Allowed Only Max(200) Char. ");
                return false;
            }

        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(<%=txtWhatOtherChanges.ClientID%>).on('keypress', function () {
                if ($(this).val().length > 200) {
                    alert("Allowed Only Max(200) Char. ");
                    return false;
                }

            });
        });

    </script>
</asp:Content>
