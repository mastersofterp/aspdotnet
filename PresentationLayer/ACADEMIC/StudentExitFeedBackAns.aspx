<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentExitFeedBackAns.aspx.cs"
    Inherits="ACADEMIC_StudentExitFeedBackAns" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .table-striped tbody tr td, .table-striped tbody tr td label {
            font-weight: bold;
        }
    </style>
    <script>
        $(document).ready(function () {
            $('.table-bordered > tbody > tr:nth-of-type(odd)').addClass("df");

            var myColors = [
                '#c7d8ea', '#b4da72', '#f7e76e', '#f2b78d', '#abd3bc', '#f5a5a3', '#96e8e1', '#e2dfa2', '#d9d8da', '#ccccb3', '#e6b3b3', '#b3e6cc'
            ];
            var i = 0;
            $('.df').each(function () {
                $(this).css('background-color', myColors[i]);
                i = (i + 1) % myColors.length;
            });

            $('.table-bordered > tbody > tr:nth-of-type(even)').addClass("gf");

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
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">College Exit Survey</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Reg.No.</label>
                                    </div>
                                    <asp:TextBox ID="txtSearch" runat="server" MaxLength="10" ToolTip="Please Enter Student Registration No. " Font-Bold="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtSearch" ErrorMessage="Please Enter User Id" SetFocusOnError="True" ValidationGroup="Search" Display="None"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search"
                                        ValidationGroup="Search" OnClick="btnSearch_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                        ValidationGroup="Search" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlStudInfo" runat="server" Visible="false">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-10">
                                    <div class="row">
                                        <div class="col-lg-5 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblName" runat="server" Font-Bold="true" Style="color: green"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Academic Year :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSession" runat="server" Font-Bold="true" Style="color: green"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Semester :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="true" Style="color: green"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-7 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Scheme :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblScheme" runat="server" Font-Bold="true" Style="color: green"></asp:Label>
                                                    </a>
                                                </li>

                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-2">
                                    <asp:Image ID="imgPhoto" runat="server" Width="96 px" Height="110px" />
                                </div>
                            </div>


                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click"
                                Text="Report" ValidationGroup="Report" Visible="false" CssClass="btn btn-outline-primary" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Cancel" CssClass="btn btn-outline-danger"
                                Visible="false" />
                            <asp:ValidationSummary ID="ValidationSummary3" runat="server"
                                DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="Report" />
                        </div>

                        <div class="text-center col-12">
                            <asp:Label ID="lblMsg" runat="server" Visible="false"> <span ID="spMsg" 
                                    style="color:Red;"></span></asp:Label>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlFeedback" runat="server" Visible="false">
                        <%--  <div class="form-group col-md-12" hidden="hidden">
                        <b>Ratings :</b>&nbsp;&nbsp;&nbsp;5 = Excellent &nbsp;&nbsp; 4 = Great &nbsp;&nbsp; 3 = Good &nbsp;&nbsp; 2 = Fair &nbsp;&nbsp; 1 = Poor
                    </div>--%>
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>
                                            <asp:Label ID="lblExitFeedback" runat="server" Visible="false"></asp:Label></h5>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:ListView ID="lvExitFeedback" runat="server">
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
                                                <td>Q&nbsp;<%# Container.DataItemIndex + 1%><asp:Label ID="lblExitFeedbackQuestions" runat="server" Text='  <%# Eval("QUESTIONID")%>' Visible="false"></asp:Label>.</td>
                                                <td><%# Eval("QUESTIONNAME")%></td>
                                            </tr>
                                            <tr>
                                                <td>Ans:&nbsp; </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rblExitFeedback" runat="server" Class="spaced" CssClass="RadioButtonWidth" Style="margin-left: -10px" RepeatDirection="Horizontal" ToolTip='<%# Eval("QUESTIONID") %>'>
                                                    </asp:RadioButtonList>
                                                    <asp:HiddenField ID="hdnExitFeedback" runat="server" Value='<%# Eval("QUESTIONID") %>' />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                        <%-- <div class="form-group col-md-12">
                        <u><b>
                            <asp:Label ID="lblteacher" runat="server" Visible="false"></asp:Label></b></u>
                        <asp:ListView ID="lvTeacher" runat="server">
                            <LayoutTemplate>
                                <div id="demo-grid" class="vista-grid">
                                    <div class="titlebar">
                                    </div>
                                    <table class="table table-bordered table-hover table-fixed">
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
                                    <td style="width: 40px">Ans:&nbsp; </td>
                                    <td>
                                        <asp:RadioButtonList ID="rblTeacher" runat="server" RepeatDirection="Horizontal" CssClass="RadioButtonWidth" ToolTip='<%# Eval("QUESTIONID") %>' Style="margin-left: -10px;">
                                        </asp:RadioButtonList>
                                        <asp:HiddenField ID="hdnTeacher" runat="server" Value='<%# Eval("QUESTIONID") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>--%>
                        <div class="form-group col-lg-12 col-md-12 col-12" style="display: block">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>
                                    <asp:Label ID="lblLikeToConvey" runat="server" Style="font-weight: bold;" Text="What feedback would you like to convey through us to the university on curriculum?"></asp:Label></label>
                            </div>
                            <asp:TextBox ID="txtLikeToConvey" runat="server" TextMode="MultiLine"
                                placeholder="Please enter other changes (Max. 200 char) ." CssClass="form-control"
                                MaxLength="100" oncopy="return false;" onpaste="return false;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLikeToConvey" ErrorMessage="Please Enter Like To Convey" SetFocusOnError="True" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>

                        </div>
                        <div class="form-group col-lg-12 col-md-12 col-12" style="display: block">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>
                                    <asp:Label ID="lblAnyComments" runat="server" Style="font-weight: bold;" Text="Any comments for improving the qualityof education offered by college?"></asp:Label></label>
                            </div>
                            <asp:TextBox ID="txtAnyComments" runat="server" TextMode="MultiLine" placeholder="Please enter comments (Max. 200 char) ."
                                CssClass="form-control" MaxLength="100" oncopy="return false;" onpaste="return false;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAnyComments" ErrorMessage="Please Enter Additional Comments" SetFocusOnError="True" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>

                        </div>
                        <div class="form-group col-lg-12 col-md-12 col-12" style="display: block">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>
                                    <asp:Label ID="lblNameTheBestTeachers" runat="server" Style="font-weight: bold;" Text="Name the best teacher you have met in these 4 years:(Among assistant professor only)"></asp:Label>
                                </label>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-6 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>a) From your Department</label>
                                    </div>
                                    <asp:TextBox ID="txtFromYourDepartment" runat="server" placeholder="Please enter From your Department." CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromYourDepartment" ErrorMessage="Please Enter From Your Department" SetFocusOnError="True" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-6 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>b) From AM / AP / AC / HS Depts.  </label>
                                    </div>
                                    <asp:TextBox ID="txtFromOtherDepartment" runat="server" placeholder="Please enter From Other Department." CssClass="form-control" Width="100 %" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFromOtherDepartment" ErrorMessage="Please Enter From Other Department" SetFocusOnError="True" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
                        <div class="form-group col-md-12" style="display: none;">
                            <asp:Label ID="Label1" runat="server" Text="Any additional Remarks (write briefly)?"></asp:Label>
                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" placeholder="Please enter comments (Max. 100 char) ." Width="100 %" Height="70 px" MaxLength="100"></asp:TextBox>
                        </div>

                        <div class=" col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-outline-info"
                                ValidationGroup="Submit" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                        <span style="font-size: large; color: Red;">
                            <b>Teacher Not Allot!! You Cann't Give FeedBack!<br />
                                Please Contact Administrator! </b>
                        </span>

                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <style>
        .RadioButtonWidth input {
            margin-left: 10px;
        }
    </style>

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
        $(<%=txtLikeToConvey.ClientID%>).on('keypress', function () {
            if ($(this).val().length > 200) {
                alert("Allowed Only Max(200) Char. ");
                return false;
            }

        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(<%=txtLikeToConvey.ClientID%>).on('keypress', function () {
                    if ($(this).val().length > 200) {
                        alert("Allowed Only Max(200) Char. ");
                        return false;
                    }

                });
            });


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

    </script>
</asp:Content>

