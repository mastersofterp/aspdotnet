<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ModerationResult.aspx.cs" Inherits="ModerationResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <script src="plugins/Chart.js/dist/Chart.min.js"></script>
    <script src="plugins/chartjs-plugin-datalabels/dist/chartjs-plugin-datalabels.min.js"></script>--%>
    <script src="../../plugins/Chart.js/dist/Chart.min.js"></script>

    <script src="../../plugins/chartjs-plugin-datalabels/dist/chartjs-plugin-datalabels.min.js"></script>

    <script src="<%=Page.ResolveClientUrl("~/plugins/Chart.js/dist/Chart.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/chartjs-plugin-datalabels/dist/chartjs-plugin-datalabels.min.js")%>"></script>

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <style>
        #tblDetails.table > tbody > tr > td {
            padding: 3px 5px;
        }

        .list-group-unbordered > .list-group-item {
            padding: 0.2rem 0.4rem;
        }

        .tbldet {
            border-radius: 8px;
            box-shadow: 0px 2px 10px rgb(0 0 0 / 20%);
            padding: 10px 15px;
        }

        tbody td {
            font-size: 13px;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="Upd"
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
    <asp:UpdatePanel ID="Upd" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Moderation Result</h3>
                        </div>

                        <div class="box-body">

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>College/Scheme </label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School & Scheme" ValidationGroup="Show"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYtxtSession" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Session</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" ValidationGroup="Show"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>

                                            <%--<label>Semester</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="Show"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="LstCourse" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblacademic" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Academic Batch</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlAcdBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAcdBatch_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAcdBatch"
                                            Display="None" ErrorMessage="Please Select Academic Batch" ValidationGroup="Show"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYtxtStudType" runat="server" Font-Bold="true"></asp:Label>

                                            <%--<label>Student Type</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlStudentType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4" AppendDataBoundItems="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular Student</asp:ListItem>
                                            <asp:ListItem Value="1">Backlog Student</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlStudentType"
                                            Display="None" ErrorMessage="Please Select Student Type" ValidationGroup="Show"
                                            SetFocusOnError="True" InitialValue="-1"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Min. Marks</label>
                                        </div>
                                        <asp:TextBox ID="txtMinMarks" runat="server" CssClass="form-control" MaxLength="2" onkeyup="IsNumeric(this);" ></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Max. Marks</label>
                                        </div>
                                        <asp:TextBox ID="txtMaxMarks" runat="server" MaxLength="2" CssClass="form-control" onkeyup="IsNumeric(this);" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" ValidationGroup="Show" />
                                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="BtnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                            </div>
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="col-12 col-md-12 col-lg-8" id="table1" runat="server" visible="false">
                                        <div class="sub-heading">
                                            <h5>Moderation Analysis</h5>
                                        </div>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12 col-md-6 col-lg-5 mt-2">
                                                    <div class="tbldet">
                                                        <table class="table nowrap mb-0" style="width: 100%;" id="tblDetails">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="border-top-0">Total Student</td>
                                                                    <td class="border-top-0">
                                                                        <asp:Label ID="lblTotalStudent" runat="server" Text="0"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Pass</td>
                                                                    <td>
                                                                        <asp:Label ID="lblPass" runat="server" Text="0"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Failed</td>
                                                                    <td>
                                                                        <asp:Label ID="lblFailed" runat="server" Text="0"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Current Pass %</td>
                                                                    <td>
                                                                        <asp:Label ID="lblCurrentPassPer" runat="server" Text="0"></asp:Label></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="col-12 col-md-6 col-lg-7 mt-md-5">
                                                    <div class="d-flex justify-content-center align-item-center">
                                                        <div>
                                                            <i class="fas fa-square" style="color: #c7e0fc;"></i>
                                                            <label>Percentage</label>
                                                        </div>
                                                        <div class="ml-4">
                                                            <i class="fas fa-circle" style="color: #003b7b;"></i>
                                                            <label>Pass Count</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <canvas id="myChart"></canvas>
                                    </div>

                                    <%--<div class="col-12 col-md-6 col-lg-5">
                                <div class="sub-heading">
                                    <h5>Moderation Result</h5>
                                </div>
                                <div class="table-responsive" style="border-top: 1px solid #e5e5e5;">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:CheckBox ID="chkAll" runat="server" />
                                                </th>
                                                <th>Marks
                                                </th>
                                                <th>Fail Count
                                                </th>
                                                <th>Pass Count
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" /></td>
                                                <td>1</td>
                                                <td>40</td>
                                                <td>50</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" /></td>
                                                <td>2</td>
                                                <td>10</td>
                                                <td>20</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox3" runat="server" /></td>
                                                <td>3</td>
                                                <td>50</td>
                                                <td>50</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox4" runat="server" /></td>
                                                <td>4</td>
                                                <td>10</td>
                                                <td>10</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox5" runat="server" /></td>
                                                <td>5</td>
                                                <td>20</td>
                                                <td>40</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox6" runat="server" /></td>
                                                <td>6</td>
                                                <td>40</td>
                                                <td>50</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox7" runat="server" /></td>
                                                <td>7</td>
                                                <td>25</td>
                                                <td>25</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox8" runat="server" /></td>
                                                <td>8</td>
                                                <td>40</td>
                                                <td>50</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox9" runat="server" /></td>
                                                <td>9</td>
                                                <td>10</td>
                                                <td>05</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox10" runat="server" /></td>
                                                <td>10</td>
                                                <td>40</td>
                                                <td>50</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                            </div>--%>
                                    <%--<asp:Panel ID="PanelList" runat="server" Visible="false">--%>
                                    <div class="col-12 col-md-6 col-lg-4" id="PanelList" runat="server" visible="false">
                                        <div class="row">
                                            <asp:ListView ID="LvModerationResult" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Moderation Result</h5>
                                                    </div>
                                                    <div class="table-responsive" style="border-top: 1px solid #e5e5e5;">
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                            <thead>
                                                                <tr>
                                                                    <%-- <th>
                                                                <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />
                                                            </th>--%>
                                                                    <th>Marks
                                                                    </th>
                                                                    <th>Fail Count
                                                                    </th>
                                                                    <th>Pass Count
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <%-- <td>
                                                    <asp:CheckBox ID="chktransfer" runat="server" />
                                                </td>--%>
                                                        <td>
                                                            <asp:Label ID="lblmarks" runat="server" Text='<%# Eval("RANGE_OF_MARKS") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblbackstu" runat="server" Text='<%# Eval("FAIL_COUNT") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblpassstu" runat="server" Text='<%# Eval("PASS_COUNT") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <%--</asp:Panel>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnShow" />
        </Triggers>
    </asp:UpdatePanel>

    <%--<script>
        var ctx1 = document.getElementById('myChart');
        var myChart1 = new Chart(ctx1, {
            plugins: [ChartDataLabels],
           
            data: {
                labels: ['Bar 1', 'Bar 2', 'Bar 3', 'Bar 4', 'Bar 5','Bar 6', 'Bar 7','Bar 8', 'Bar 9', 'Bar 10', 'Bar 11', 'Bar 12','Bar 13', 'Bar 14','Bar 15'],
                datasets: [

                            {
                                type: 'line',
                                label: "Continuous Assessment-1",
                                data: [0, 2, 4, 5, 6, 7, 8, 10, 2, 4, 5, 6, 7, 8, 10],
                                backgroundColor: [
                                    '#003b7b',

                                ],
                                borderWidth: 1,
                                borderColor: '#003b7b',
                            },

                            {
                                //label: "Continuous Assessment-1",
                                type: 'bar',
                                data: [2, 5, 5, 8, 7, 12, 14, 15, 5, 5, 8, 7, 12, 14, 15],
                                backgroundColor: [
                                    '#d1e7ff',

                                ],
                                borderWidth: 1,
                                borderColor: '#ffff',
                            },

                ]
            },
            options: {
                layout: {
                    padding: 10
                },
                plugins: {
                    legend: {
                        display: false

                    },
                    title:{
                        display:true,
                        text:"First Year MBA (2021 -23) Degree Examination - Dec 2021",
                    },
                    datalabels: {
                        anchor: 'end', // remove this line to get label in middle of the bar
                        align: 'end',
                        display: 'auto',
                        formatter: (val) => (`${val}`),
            labels: {
            value: {

                        font: { size: '15px' },
                        },

                    }
        }
        },


        scales: {

        }

        }
        });

    </script>--%>
    <script type="text/javascript" language="javascript">
        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvModerationResult$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvModerationResult$ctrl';
                var g = b + s[1];
                if (e.name == g) {
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
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });

    </script>

    <script>
        function ShowGraph(data) {
            console.log(data)
            var PAssCount = [];
            var x = data.split('#')[0];
            var PAssPer = [];
            var y = data.split('#')[1];
            var mARKS = [];
            var Z = data.split('#')[2];
            var nAME = [];
            var A = data.split('#')[3];
            PAssCount = x.split(',');
            PAssPer = y.split(',');
            mARKS = Z.split(',');
            nAME = A;
            //alert(PAssCount)
            //alert(PAssPer)
            //alert(nAME)
            var ctx1 = document.getElementById('myChart');
            var myChart1 = new Chart(ctx1, {
                plugins: [ChartDataLabels],
           
                data: {
                    //labels: ['Bar 1', 'Bar 2', 'Bar 3', 'Bar 4', 'Bar 5','Bar 6', 'Bar 7','Bar 8', 'Bar 9', 'Bar 10', 'Bar 11', 'Bar 12','Bar 13', 'Bar 14','Bar 15'],
                    labels: mARKS,

                    datasets: [
                                {
                                    type: 'line',
                                    label: "Continuous Assessment-1",
                                    data: PAssCount,
                                    backgroundColor: [
                                        '#003b7b',
                                    ],
                                    borderWidth: 1,
                                    borderColor: '#003b7b',
                                },
                                {
                                    //label: "Continuous Assessment-1",
                                    type: 'bar',
                                    data: PAssPer,
                                    backgroundColor: [
                                        '#d1e7ff',
                                    ],
                                    borderWidth: 1,
                                    borderColor: '#ffff',
                                },
                    ]
                },
                options: {
                    layout: {
                        padding: 15
                    },
                    plugins: {
                        legend: {
                            display: false

                        },
                        title:{
                            display:true,
                            text:nAME,
                            //text:"First Year MBA (2021 -23) Degree Examination - Dec 2021",
                        },
                        datalabels: {
                            anchor: 'end', // remove this line to get label in middle of the bar
                            align: 'end',
                            display: 'auto',
                            formatter: (val) => (`${val}`),
                labels: {
                value: {

                            font: { size: '11px' },
                        },

                    }
        }
        },


        scales: {

        }

        }
        });
        }
    </script>
    <script>
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    //document.getElementById(textbox.id).value = 0;
                    alert("Enter Only Numeric Value")
                    document.getElementById(textbox.id).value = '';
                }
            }
        }
    </script>
</asp:Content>

