<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ITLE_TEST_Preview.aspx.cs" Inherits="ITLE_ITLE_TEST_Preview" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <style>
        .loader {
            border: 5px solid white;
            border-radius: 50%;
            border-top: 5px solid blue;
            border-right: 5px solid green;
            border-bottom: 5px solid red;
            border-left: 5px solid orange;
            width: 40px;
            height: 40px;
            -webkit-animation: spin 2s linear infinite;
            animation: spin 2s linear infinite;
        }

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
    </style>--%>
    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TEST PREVIEW</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12" id="dvSelectTest" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Select Test</label>
                                            <asp:DropDownList ID="ddlTest" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="Select Schemeno" ToolTip="Select Test" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStaff" runat="server" ControlToValidate="ddlTest"
                                                Display="None" ErrorMessage="Please Select Test" SetFocusOnError="True" ValidationGroup="Show"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnShow" runat="server" Text="" CssClass="btn btn-primary" ValidationGroup="Show" OnClick="btnShow_Click"
                                        TabIndex="5" ToolTip="Click here to Show Students Test Status"><i class="fa fa-eye"></i> See Test Status</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text=""
                                        ValidationGroup="Cancel Button" ToolTip="Click here to Reset"
                                        CssClass="btn btn-warning" TabIndex="6"><i class="fa fa-refresh"></i> Cancel</asp:LinkButton>
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="Show" DisplayMode="List" />
                                </div>

                            </div>
                            <div class="col-md-12" id="dvPreview" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnBack" runat="server" Text="" CssClass="btn btn-primary" OnClick="btnBack_Click1"
                                                TabIndex="5" ToolTip="Click To Go Back To Test Selection Page"><i class="fa fa-backward"></i> Back</asp:LinkButton>
                                            <asp:LinkButton ID="btnPrev" runat="server" Text="" CssClass="btn btn-primary" OnClick="btnPrev_Click"
                                                TabIndex="5" ToolTip="Click To Go Preview The Latest"><i class="fa fa-eye"></i> Preview</asp:LinkButton>
                                            <%--<button id="btnStartPreview" onclick="myStartFunction()" class="btn btn-success"><i class="fa fa-repeat"></i> Start AutoPreview</button>
            <button id="btnStopPreview" runat="server" onclick="myStopFunction()" class="btn btn-warning"><i class="fa fa-times"></i> Stop AutoPreview</button>--%>
                                            <asp:LinkButton ID="btnStartPreview" runat="server" Text="" CssClass="btn btn-info" OnClick="btnStartPreview_Click"
                                                OnClientClick="myStartFunction()" TabIndex="5" ToolTip="Click To Start AutoPreview"><i class="fa fa-repeat"></i> Start AutoPreview</asp:LinkButton>
                                            <asp:LinkButton ID="btnStopPreview" runat="server" Text="" CssClass="btn btn-warning" OnClick="btnStopPreview_Click"
                                                OnClientClick="myStopFunction()" TabIndex="5" ToolTip="Click To Stop AutoPreview"><i class="fa fa-times"></i> Stop AutoPreview</asp:LinkButton>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:DropDownList ID="ddlTestPrevType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlTestPrevType_SelectedIndexChanged" ToolTip="Select Test Preview Type">
                                                <asp:ListItem Value="0" Selected="True">Detail View</asp:ListItem>
                                                <asp:ListItem Value="1">Chart View</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4" id="lblAutoPreview" runat="server">

                                            <div class="col-md-9">
                                                <label style="color: black;">Auto Preview In Progress....</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="dvDetailsPrev" runat="server" class="row">
                                    <div class="col-md-9 col-12">
                                        <asp:ListView ID="lvTest" runat="server" OnItemDataBound="lvTest_ItemDataBound">
                                            <LayoutTemplate>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <div class="col-12 mb-2" style="padding: 15px; box-shadow: 0 0 8px #ccc; box-shadow: rgb(0 0 0 / 20%) 0px 0px 5px; border-radius: 10px;">
                                                            <div class="row">
                                                                <%--<h3 class="box-title" style="font-size: large; font-family: 'Times New Roman'">Preview</h3>--%>
                                                                <div class="col-sm-1 col-12 text-center">
                                                                    <b class="label label-danger btn " style="box-shadow: 0 0 3px #ccc; border-radius: 50%; height: 35px; width: 35px; font-size: small; text-align: center;">
                                                                        <%# Container.DataItemIndex + 1%>
                                                                    </b>
                                                                </div>
                                                                <div class="col-sm-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Student Name :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblStudName" runat="server" ToolTip='<%# Eval("TESTNO") %>' Text='<%# Eval("STUDNAME") %>' Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Roll No. :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="Label1" runat="server" ToolTip='<%# Eval("IDNO") %>' Text='<%# Eval("REGNO") %>' Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                                <div class="col-sm-5 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Status :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text='<%# Eval("TEST_STATUS") %>'></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Last Action :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="Label3" runat="server" Font-Bold="true" Text='<%# Eval("ANSSUBMITTIME") %>'></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>

                                                            </div>

                                                            <div class="col-sm-11 offset-sm-1 mt-3">
                                                                <asp:ListView ID="lvPreview" runat="server">
                                                                    <LayoutTemplate>
                                                                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <table style="display: inline-block;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:LinkButton ID="btnQuesStatus" runat="server" Style="border-radius: 50%; height: 35px; width: 35px; font-size: small; text-align: center; cursor: default;" CssClass='<%# Eval("ANS_STAT") %>' CommandName='<%# Eval("SRNO") %>' CommandArgument='<%# Eval("QUESTIONNO") %>'><%# Eval("SRNO") %></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </div>

                                                            <hr />

                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <span style="height: 16px; width: 17px; background-color: green; border-radius: 50%; display: inline-block;"></span>
                                                                    Answer :
                                                                    <asp:Label ID="lblAnsCount" runat="server" Style="font-weight: 600; font-size: large" Text='<%# Eval("ANSWER") %>'></asp:Label>
                                                                </div>

                                                                <div class="col-sm-3">
                                                                    <span style="height: 16px; width: 17px; background-color: #3399ff; border-radius: 50%; display: inline-block;"></span>
                                                                    Review :
                                                                    <asp:Label ID="lblRevCount" runat="server" Style="font-weight: 600; font-size: large" Text='<%# Eval("REVIEW") %>'></asp:Label>
                                                                </div>

                                                                <div class="col-sm-3">
                                                                    <span style="height: 16px; width: 17px; background-color: orange; border-radius: 50%; display: inline-block;"></span>
                                                                    Skip :
                                                                    <asp:Label ID="lblSkipCount" runat="server" Style="font-weight: 600; font-size: large;" Text='<%# Eval("SKIP") %>'></asp:Label>
                                                                </div>

                                                                <div class="col-sm-3">
                                                                    <span style="height: 16px; width: 17px; background-color: red; border-radius: 50%; display: inline-block;"></span>
                                                                    Not Answer :
                                                                    <asp:Label ID="lblNotAnsCount" runat="server" Style="font-weight: 600; font-size: large" Text='<%# Eval("NOT_ANSWER") %>'></asp:Label>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    <div class="col-md-3 col-12">
                                        <div class="col-12" style="padding: 15px; box-shadow: 0 0 8px #ccc; box-shadow: rgb(0 0 0 / 20%) 0px 0px 5px; border-radius: 10px;">

                                            <div class="col-lg-12 col-md-12 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Absent Count :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblAbsentCount" runat="server" Style="font-weight: 600; font-size: medium"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div class="col-12 text-center mt-3" style="max-height: 300px; overflow-y: scroll;">
                                                <asp:ListView ID="lvAbsent" runat="server">
                                                    <LayoutTemplate>
                                                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <table style="display: inline-block;">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="btnAbsentStud" runat="server" ToolTip="Click To View Student Information" Style="border-radius: 10px; height: 35px; font-size: small; text-align: center; cursor: pointer; margin-top: 2px;" OnClick="btnAbsentStud_Click" CssClass="btn btn-outline-danger" CommandArgument='<%# Eval("IDNO") %>'><%# Eval("REGNO") %></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <div id="dvChart" runat="server">
                                    <div class="col-md-12 text-center">

                                        <asp:Chart ID="Chart1" CanResize="true"  runat="server" Height="450px" Width="450px">
                                            <Titles>
                                                <asp:Title ShadowOffset="3" Name="Items" />
                                            </Titles>
                                            <Legends>
                                                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                                    LegendStyle="Row" />
                                            </Legends>
                                            <Series>
                                                <asp:Series Name="Default" />
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                                            </ChartAreas>
                                        </asp:Chart>
                                    </div>
                                </div>
                            </div>

                            <div id="dvIntermediate" visible="false" runat="server" style="position: fixed; z-index: 50; width: 100%; min-height: 100%; top: 0%; left: 0%; background: rgba(0, 128, 128, 0.8);"></div>

                            <!-- The Modal -->
                            <div id="dvShowStudent" visible="false" runat="server">
                                <div class="modal d-block">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">

                                            <!-- Modal Header -->
                                            <div class="modal-header">
                                                <h4 class="modal-title">Student Information</h4>
                                                <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                                            </div>

                                            <!-- Modal body -->
                                            <div class="modal-body">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-lg-9 col-md-9 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Name :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblPopStudName" runat="server" Font-Bold="true" Text="Devbratta"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Roll No :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblPopRegno" runat="server" Font-Bold="true" Text="19201001"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Semester :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblPopSem" runat="server" Font-Bold="true" Text="III"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Phone :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblPopPhone" runat="server" Font-Bold="true" Text="9856742120"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Email :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblPopMail" runat="server" Font-Bold="true" Text="abc@gmail.com"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Degree :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblPopDegree" runat="server" Font-Bold="true" Text="M.Tech"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Branch :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblPopBranch" runat="server" Font-Bold="true" Text="Computer Science & Engineering"></asp:Label>
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-2 col-md-3 col-6 offset-lg-1">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b></b>
                                                                    <a class="sub-label">
                                                                        <asp:Image ID="imgPhoto" runat="server" Style="width: 100%; height: 100%;" />
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <!-- Modal footer -->
                                            <div class="modal-footer">
                                                <asp:LinkButton ID="btnCloseModal" runat="server" CssClass="btn btn-outline-danger" Style="border-radius: 10px;" OnClick="btnCloseModal_Click"><i class="fa fa-close"></i> Close</asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        //$(window).load(function () {
        //    document.getElementById("btnStopPreview").style.visibility = "hidden";
        //    alert("Load");
        //});

        var myVar;

        function myStartFunction() {
            //debugger;
            //document.getElementById("btnStopPreview").style.visibility = "visible";
            //document.getElementById("btnStartPreview").style.visibility = "hidden";

            //alert("Hi");
            myVar = setTimeout("autoPreview()", 15000);
        }

        function autoPreview() {
            document.getElementById("<%=btnPrev.ClientID %>").click();
            myVar = setTimeout("autoPreview()", 15000);
        }

        function myStopFunction() {
            //document.getElementById("btnStopPreview").style.visibility = "hidden";
            clearTimeout(myVar);
        }


        function totAllQUESTIONS(headchk) {
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



        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        function IsNumeric(txt) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0 ; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }

    </script>
    <!-- iCheck -->
    <script type="text/javascript">
        window.history.forward();
        function noBack() {
            window.history.forward();
        }
    </script>

</asp:Content>

