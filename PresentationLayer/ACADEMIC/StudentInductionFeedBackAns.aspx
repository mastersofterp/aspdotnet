<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentInductionFeedBackAns.aspx.cs"
    Inherits="ACADEMIC_StudentInductionFeedBackAns" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .RadioButtonWidth tbody tr:nth-of-type(odd) {
            border-bottom: 0px solid transparent;
            background-color: transparent !important;
        }
        .RadioButtonWidth .table-bordered td {
            border: 0px solid #dee2e6;
        }
    </style>

    <%--<script>
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
    </script>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                    </h3>
                </div>

                <div class="box-body" id="divbody" runat="server" visible="true">
                    <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Reg.No.</label>
                                    </div>
                                    <asp:TextBox ID="txtSearch" runat="server" MaxLength="10" ToolTip="Please Enter Student Registration No." CssClass="form-control" Font-Bold="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtSearch" ErrorMessage="Please Enter User Id" SetFocusOnError="True" ValidationGroup="Search" Display="None"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label></label>
                                    </div>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" />

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
                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblName" runat="server" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Academic Year :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSession" runat="server" Font-Bold="true"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Semester :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSemester" runat="server" Font-Bold="true"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-5 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Scheme :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblScheme" runat="server" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-2 col-md-6 col-12">
                                    <asp:Image ID="imgPhoto" runat="server" Width="96 px" Height="110px" />
                                </div>
                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="btn btn-info"
                                Text="Report" ValidationGroup="Report" Visible="false" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Cancel" CssClass="btn btn-warning"
                                Visible="false" />
                            <asp:ValidationSummary ID="ValidationSummary3" runat="server"
                                DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="Report" />
                        </div>

                        <div class="form-group col-md-12">
                            <asp:Label ID="lblMsg" runat="server" Visible="false"> 
                                <span ID="spMsg" style="color:Red;"></span>
                            </asp:Label>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlFeedback" runat="server" Visible="false">
                        <div class="form-group col-lg-8 col-md-12 col-12">
                            <div class=" note-div">
                                <h5 class="heading">Ratings </h5>
                                <p>
                                    <span>5 = <span style="color: green; font-weight: bold">Excellent, </span></span>&nbsp;&nbsp;
                                    <span>4 = <span style="color: green; font-weight: bold">Very Good,</span></span> &nbsp;&nbsp;
                                    <span>3 = <span style="color: green; font-weight: bold">Good, </span></span>&nbsp;&nbsp;
                                    <span>2 = <span style="color: green; font-weight: bold">Satisfactory, </span></span>&nbsp;&nbsp;
                                    <span>1 = <span style="color: green; font-weight: bold">Needs Improvement </span></span>
                                </p>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>
                                            <asp:Label ID="lblInductionProgram" runat="server" Visible="false"></asp:Label></h5>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <asp:ListView ID="lvInductionProgram" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid" class="vista-grid">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>Q&nbsp;<%# Container.DataItemIndex + 1%><asp:Label ID="lblInductionProgramQuestions" runat="server" Text='  <%# Eval("QUESTIONID")%>' Visible="false"></asp:Label>.</td>
                                                <td><%# Eval("QUESTIONNAME")%></td>
                                            </tr>
                                            <tr>
                                                <td>Ans:&nbsp; </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rblInductionProgram" runat="server" Class="spaced" CssClass="RadioButtonWidth" Style="margin-left: -10px" RepeatDirection="Horizontal" ToolTip='<%# Eval("QUESTIONID") %>'>
                                                    </asp:RadioButtonList>
                                                    <asp:HiddenField ID="hdnInductionProgram" runat="server" Value='<%# Eval("QUESTIONID") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>

                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <div class="label-dynamic">
                                        <asp:Label ID="lblAnySuggestions" Style="font-weight: bold;" runat="server" Text="Any Suggestions?"></asp:Label>
                                    </div>
                                    <asp:TextBox ID="txtAnySuggestions" runat="server" TextMode="MultiLine" placeholder="Please enter Suggestions (Max. 200 char)" CssClass="form-control"
                                        oncopy="return false;" onpaste="return false;" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAnySuggestions" ErrorMessage="Please Enter Suggestions" SetFocusOnError="True" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-12 col-md-6 col-12 d-none">
                                    <div class="label-dynamic">
                                        <asp:Label ID="Label1" runat="server" Text="Any additional Remarks (write briefly)?"></asp:Label>
                                    </div>
                                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" placeholder="Please enter comments (Max. 100 char)" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary"
                                ValidationGroup="Submit" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                        </div>

                    </asp:Panel>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnInductionFeedbackCertificate" runat="server" Text="Induction Program Certificate" OnClick="btnInductionFeedbackCertificate_Click" CssClass="btn btn-primary" Visible="false" />
                    </div>

                    <div class="col-12 text-center">
                        <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                            <span style="font-size: large; color: Red;">
                                <b>Teacher Not Allot!! You Cann't Give FeedBack!<br />
                                    Please Contact Administrator! </b>
                            </span>
                        </asp:Panel>
                    </div>
                    
                    <div class="col-12 text-center">
                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                    </div>

                    <div id="divMsg" runat="server"></div>

                </div>
                <div class="col-12 text-center">
                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                            <span style="font-size: large; color: Red;">
                                <b>No Feedback Questions are found for this student.</b>
                            </span>
                        </asp:Panel>
                    </div>
            </div>
        </div>
    </div>
 
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
            $(<%=txtAnySuggestions.ClientID%>).on('keypress', function () {
                if ($(this).val().length > 200) {
                    alert("Allowed Only Max(200) Char. ");
                    return false;
                }

            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(<%=txtAnySuggestions.ClientID%>).on('keypress', function () {
                    if ($(this).val().length > 200) {
                        alert("Allowed Only Max(200) Char. ");
                        return false;
                    }

                });
            });

        </script>
</asp:Content>

