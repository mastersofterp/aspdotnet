<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Grade_Allotment.aspx.cs" Inherits="ACADEMIC_EXAMINATION_Grade_Allotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
        .statistics .tile-box {
            background-color: rgb(255, 255, 255);
            box-shadow: rgba(0, 0, 0, 0.24) 0px 3px 8px;
            /*box-shadow: rgba(0, 0, 0, 0.1) 2px 2px 4px;*/
            display: flex;
            justify-content: center;
            padding: 15px 10px;
            overflow: hidden;
            border-radius: 8px;
        }

            .statistics .tile-box > i.icon-sm {
                width: 35px;
                height: 35px;
                line-height: 35px;
            }

            .statistics .tile-box > i {
                float: left;
                color: rgb(255, 255, 255);
                width: 60px;
                height: 60px;
                line-height: 60px;
                font-size: 22px;
                border-radius: 50%;
            }

            .statistics .tile-box .info {
                float: left;
                width: auto;
                margin-left: 10px;
            }

                .statistics .tile-box .info h3 {
                    margin: 5px 0 5px;
                    display: inline-block;
                    color: #444;
                    font-size: 20px;
                }



        .grade-range .table > tbody > tr > td {
            padding: 3px 8px;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlExam"
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

    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">GRADE ALLOTMENT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12 col-md-12 col-lg-8">
                                        <div class="row">

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <%-- <label>Session</label>--%>
                                                    <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged">
                                                    <%--OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"--%>
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="ddlSession"
                                                    Display="None" ErrorMessage="Please Select Sesssion" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="search"></asp:RequiredFieldValidator>


                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <%-- <sup>*</sup>--%>
                                                    <%-- <label>College/Scheme</label>--%>
                                                    <%--<label>College & Scheme</label>--%>
                                                    <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlcolgname" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddlcolgname_SelectedIndexChanged">
                                                    <%--OnSelectedIndexChanged="ddlcolgname_SelectedIndexChanged"--%>
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:HiddenField runat="server" ID="hdfnscheme" />
                                                <%--                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcolgname"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="search"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <%--  <label>Semester</label>--%>
                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlSemester" AppendDataBoundItems="True" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlsem_OnselectedIndexChanged">

                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSemester"
                                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="search"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Course</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCourse" AppendDataBoundItems="True" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlCourse_OnselectedIndexChanged">

                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCourse"
                                                    Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="search"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12" id="divgradetype" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Grade Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlgradetype" AppendDataBoundItems="True" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" Visible="false">

                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">New Grade</asp:ListItem>
                                                    <asp:ListItem Value="2">Old Grade</asp:ListItem>
                                                    <asp:ListItem Value="3">Fixed New Grade</asp:ListItem>
                                                    <asp:ListItem Value="4">Fixed Old Grade</asp:ListItem>
                                                    <asp:ListItem Value="5">AHS TILL 2021</asp:ListItem>
                                                    <asp:ListItem Value="6">AHS 2022 ONWARDS</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlgradetype"
                                                    Display="None" ErrorMessage="Please Select Grade Type" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="search"></asp:RequiredFieldValidator>
                                            </div>



                                            <div class="col-12 mt-3 mb-4">
                                                <section class="statistics">
                                                    <div class="gutters-sm">
                                                        <div class="mybox-main row">
                                                            <div class="col-lg-4 col-md-6 col-6 pad-box">
                                                                <div class="tile-box in-down a3">
                                                                    <i class=" fa fa-book fa-fw warning icon-sm" style="background-color: #c23531"></i>
                                                                    <div class="info">
                                                                        <h3>
                                                                            <label id="lblTotalStudent" runat="server"></label>
                                                                        </h3>
                                                                        <span>
                                                                            <b>Total Subject</b>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-4 col-md-6 col-6 pad-box">
                                                                <div class="tile-box in-down a2">
                                                                    <i class="fa fa-graduation-cap fa-fw danger icon-sm" style="background-color: #255282"></i>
                                                                    <div class="info">
                                                                        <h3>
                                                                            <label id="lblFemaleCount" runat="server"></label>
                                                                        </h3>
                                                                        <span>
                                                                            <b>Grade Allot</b>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-4 col-md-6 col-6 pad-box">
                                                                <div class="tile-box in-down a1">
                                                                    <i class="fa fa-users fa-fw icon-sm" style="background-color: #2f4554"></i>
                                                                    <div class="info">
                                                                        <h3>
                                                                            <label id="lblMaleCount" runat="server"></label>
                                                                        </h3>
                                                                        <span>
                                                                            <b>Grade Pending </b>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>

                                                </section>
                                            </div>

                                            <div class="col-12 mt-3">
                                                <asp:ListView ID="lvGradeAllotment" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Course Details</h5>
                                                        </div>
                                                        <div style="width: 100%; overflow: auto">
                                                            <table id="tblBacklogSubjects" class="table table-striped table-bordered nowrap">
                                                                <thead>


                                                                    <tr>
                                                                        <th>Action</th>
                                                                        <th>Subject</th>
                                                                        <th>Register Count</th>
                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </thead>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr id="trCurRow" class="item">
                                                            <td>
                                                                <asp:CheckBox ID="chkAccept" runat="server" />

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("CCODE")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRegister" runat="server" Text='<%# Eval("IS_REGISTERED") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>

                                                </asp:ListView>
                                            </div>

                                            <%--                                            <div class="col-12 text-center mt-3" id="divscale" runat="server" visible="false">
                                                <label style="font-weight: 700;font-size: 16px;">Common Factor:</label>
                                                <asp:Label CssClass="text-success" style="font-weight: 700;font-size: 16px;" ID="lblfactor" runat="server" Text='<%# Eval("POWERFACTOR").ToString() %>'></asp:Label>
                                                
                                            </div>--%>



                                            <%--added by parfull on dt-21062023--%>
                                            <div class="col-12 mt-3">
                                                <div runat="server" id="divpower" visible="false">

                                                    <asp:ListView ID="lvpowerfactor" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Range Details</h5>
                                                            </div>
                                                            <div style="width: 100%; overflow: auto">
                                                                <table id="tblBacklogSubjects" class="table table-striped table-bordered nowrap">
                                                                    <thead>


                                                                        <tr>
                                                                            <th>Action</th>
                                                                            <th>Upper Range</th>
                                                                            <th>Lower Range</th>
                                                                            <th>Power Factor</th>
                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </thead>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr id="trCurRow" class="item">
                                                                <td>
                                                                    <asp:CheckBox ID="chkAccept" runat="server" OnCheckedChanged="chkAccept_CheckedChanged" AutoPostBack="true" />

                                                                </td>
                                                                <td>
                                                                    <%-- <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("CCODE")%>' />--%>
                                                                    <asp:TextBox ID="txtupper" runat="server" MaxLength="3" Enabled="false" Text='<%# Eval("MAX_RANGE_CPU").ToString() %>'></asp:TextBox>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtupper"
                                                                        ValidChars="1234567890." FilterMode="ValidChars" />
                                                                </td>
                                                                <td>
                                                                    <%--                    <asp:Label ID="lblRegister" runat="server" Text='<%# Eval("IS_REGISTERED") %>' />--%>
                                                                    <asp:TextBox ID="txtlower" runat="server" MaxLength="3" Enabled="false" Text='<%# Eval("MIN_RANGE_CPU").ToString() %>'></asp:TextBox>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtlower"
                                                                        ValidChars="1234567890." FilterMode="ValidChars" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblpwerfactor" runat="server" Text='<%# Eval("POWER_FACTOR_CPU") %>' />
                                                                    <%-- <asp:TextBox ID="TextBox1" runat="server" Enabled="false" Text='<%# Eval("MIN_RANGE_CPU").ToString() %>' AutoPostBack="true"></asp:TextBox>--%>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                </div>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-lg-4 col-md-6 col-12 grade-range">
                                        <asp:ListView ID="lvGradeRange" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Grade Range Details</h5>
                                                </div>
                                                <div style="width: 100%; overflow: auto">
                                                    <table class="table table-striped table-bordered nowrap" id="lvGradeRange">
                                                        <thead>
                                                            <tr>
                                                                <th>Min.Marks</th>
                                                                <th>Max.Marks</th>
                                                                <th>Grade</th>
                                                                <th>GDP</th>
                                                                <th id="studcount">Student Count</th>
                                                                <%-- <th>Percent</th>--%>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                            <asp:PlaceHolder runat="server" ID="gpholder"></asp:PlaceHolder>
                                                            <asp:PlaceHolder runat="server" ID="itemplacehold"></asp:PlaceHolder>
                                                        </thead>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>

                                                <tr id="trCurRow" class="item">
                                                    <td>
                                                        <asp:TextBox ID="txtmin" runat="server" placeholder="" MaxLength="5" Text='<%# Eval("MINMARK").ToString() %>' Enabled='<%# ViewState["RangeChange"].ToString()=="1"? true:false %>' CssClass="form-control" onkeyUP="changeMinMarksRange(this);"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fltminmark" runat="server" FilterType="Numbers,Custom" ValidChars="., " TargetControlID="txtmin" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtmax" runat="server" placeholder="" MaxLength="5" Text='<%# Eval("MAXMARK").ToString() %>' Enabled="false" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fltmax" runat="server" FilterType="Numbers,Custom" ValidChars="., " TargetControlID="txtmax" />

                                                     <%--   <asp:TextBox ID="txtmax" runat="server" placeholder="" MaxLength="5" Text='<%# Eval("MAXMARK").ToString() %>' Enabled='<%#ViewState["RangeChange"].ToString()=="1"? true:false %>' CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers,Custom" ValidChars="., " TargetControlID="txtmax" />--%>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("GRADE_NAME") %>' ToolTip='<%# Eval("GRADE_NAME") %>' />

                                                    <td>
                                                        <asp:Label ID="lblGDP" runat="server" Text='<%# Eval("POINT") %>' />
                                                    </td>
                                                    <td id="stdcounts">

                                                        <asp:Label ID="lblCount" runat="server" Text='<%# Eval("TOTAL_STU") %>' />
                                                    </td>
                                                    <%--  <td>
                                               
                                                <asp:Label ID="lblPer" runat="server" Text='<%# Eval("Percentage") %>' />
                                            </td>--%>
                                                </tr>

                                            </ItemTemplate>

                                        </asp:ListView>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12 btn-footer mt-4 mb-4">
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="search" OnClick="OnClick_ShowCourse" OnClientClick="showhidediv();" />
                                <asp:Button ID="btnRangeLock" runat="server" Text="Grade Range Generate" CssClass="btn btn-primary" ValidationGroup="search" OnClick="OnClick_Grade_RangeLock" />
                                <asp:Button ID="btnmodifypowerfactor" runat="server" Text="Modify Power factor" CssClass="btn btn-primary" ValidationGroup="search" Visible="false" OnClick="btnmodifypowerfactor_Click" />

                                <asp:Button ID="btnReRange" runat="server" Text="Modify Grade Range " CssClass="btn btn-primary" ValidationGroup="search" OnClick="btnReRange_Click" Visible="false" />
                                <asp:Button ID="btnRangrlock" runat="server" Text="Lock Grade Range " CssClass="btn btn-primary" ValidationGroup="search" OnClick="btnRangrlock_Click" Visible="false" />
                                <asp:Button ID="btnRangrUnlock" runat="server" Text="UnLock Grade Range " CssClass="btn btn-primary" ValidationGroup="search" OnClick="btnRangrUnlock_Click" Visible="false" />
                                <asp:Button ID="btnGradeAllotment" runat="server" Text="Grade Allotment" ValidationGroup="search" CssClass="btn btn-primary" Visible="false" OnClick="OnClick_Grade_Allotment" OnClientClick="showhidedivgraph();" /><%--OnClick="OnClick_Grade_Allotment"--%>

                                <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="search" CssClass="btn btn-primary" Visible="false" OnClick="btnReport_Click" /><%--OnClick="OnClick_Grade_Allotment"--%>

                                <span style="cursor: pointer;" runat="server" id="test" visible="false" onclick="getallotment(); getGradeAolltement();showhidedivgraph()" class="btn btn-primary">Graph</span>
                                <asp:Button ID="btnCancle" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="OnClick_Cancle" /><%--OnClick="OnClick_Cancle"--%>

                                <%-- <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="search" DisplayMode="List"
                                        ShowSummary="false" ShowMessageBox="true" />--%>
                            </div>
                            <div class="col-12" runat="server" id="divgraph" style="display: none;">
                                <div class="row">
                                    <div class="col-md-6 col-12">
                                        <canvas id="scaledownPercentage"></canvas>
                                    </div>
                                    <div class="col-md-6 col-12">
                                        <canvas id="scaledownGrade"></canvas>
                                    </div>
                                </div>
                            </div>

                            <%--   <div class="col-12 mt-6">

                                <asp:Label ID ="lblnote" runat="server" Visible="false" ForeColor="Red" Font-Size="Medium"></asp:Label>
                            </div>--%>

                            <div class="col-12 mt-3" id="divgradedetails" runat="server">
                                <asp:ListView ID="lvStudentDetails" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Student Details</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudent">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <%--<th>Select All</th>--%>
                                                    <th>SRNO</th>
                                                    <th>REGNO</th>
                                                    <th>STUDENT NAME</th>
                                                    <th>INTERNAL MARK</th>
                                                    <th>EXTERNAL MARK</th>
                                                    <th>MARKTOT</th>
                                                    <th>SCALEUP_PERCENT
                                                        <%--<asp:Label ID="lblscale" runat="server" Text='<%# Session["OrgId"].ToString()=="5"? "SCALEDN_PERCENT":"SCALEDN_PERCENT" %>'--%>
                                                    </th>
                                                    <th>GENERATED GRADE</th>
                                                    <th>FINAL GRADE</th>
                                                    <th>REMARK</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow">
                                            <%--<td>
                                        <asp:CheckBox ID="chkAccept" runat="server" />

                                    </td>--%>
                                            <td>
                                                <asp:Label ID="lblSrno" runat="server" Text='<%# Eval("SRNO") %>' />

                                            </td>
                                            <td>
                                                <asp:Label ID="lblRegno" runat="server" Text='<%# Eval("REGNO") %>' />

                                            </td>
                                            <td>
                                                <asp:Label ID="lblstudname" runat="server" Text='<%# Eval("STUDNAME") %>' />
                                            </td>

                                            <td>
                                                <asp:Label ID="lblintermark" runat="server" Text='<%# Eval("INTERMARK") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblextermark" runat="server" Text='<%# Eval("EXTERMARK") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMARKTOT" runat="server" Text='<%# Eval("MARKTOT") %>' />

                                            </td>
                                            <td>
                                                <asp:Label ID="lblSPERCENT" runat="server" Text='<%# Eval("SCALEDN_PERCENT") %>' />

                                            </td>
                                            <td>
                                                <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GRADE_NAME") %>' />

                                            </td>
                                            <td>
                                                <asp:Label ID="lblfinalGrade" runat="server" Text='<%# Eval("GRADE") %>' />
                                                <%-- Text='<%# Session["OrgId"].ToString()=="8"? DataBinder.Eval(Container.DataItem,"DECODENO"):DataBinder.Eval(Container.DataItem,"REGNO") %>'--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblremark" runat="server" Text='<%# Eval("REMARK") %>' />
                                            </td>


                                        </tr>
                                    </ItemTemplate>

                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
        ShowSummary="false" ValidationGroup="search" />

    <script src="../../plugins/Chart.js/dist/Chart.min.js"></script>

    <script src="../../plugins/chartjs-plugin-datalabels/dist/chartjs-plugin-datalabels.min.js"></script>



    <script>

        function changeMinMarksRange(txt) {

            debugger

            X1 = Number(txt.value);
            var dataRowsmark = null;
            var percnt;
            if (document.getElementById('lvGradeRange') != null)
            {
                dataRowsmark = document.getElementById('lvGradeRange').getElementsByTagName('tr');
                for (j = 0; j < dataRowsmark.length - 1; j++) 
                {
                    debugger
                    //var MaxMark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax');
                    //var MinMark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin');

                    var MaxMark1 = 0;
                    var MinMark1 = 0;
                    var AssGrade;
                    var GDpoint;
                    var AssValu = 0;


                    MaxMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGradeRange_ctrl' + j + '_txtmax').value.trim());
                    MinMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGradeRange_ctrl' + j + '_txtmin').value.trim());
                   
                    //if(MaxMark < MinMark)
                    //{
                    //    alert('Please check Max Marks Should Not Be Greater Than Min Marks..!!');
                    //    return false;
                    //}

                    if (X1 == 0) {
                    }
                    else if (parseFloat(MinMark) == X1) {
                        AssValu = Number(X1 - 0.01);
                        document.getElementById('ctl00_ContentPlaceHolder1_lvGradeRange_ctrl' + (j + 1) + '_txtmax').value = parseFloat(AssValu);
                    }
                }


            }
        }

        function showhidediv(){
            // alert('h1')
           
            $("#ctl00_ContentPlaceHolder1_divgraph").hide();
            $('#ctl00_ContentPlaceHolder1_divgradedetails').show();
            $("#divgradedetails").show();
            $(".dataTables_scroll").show(); 
            $("#scaledownPercentage").hide();
            //$("#scaledownGrade").attr("Visibility",false);
            //$("#scaledownPercentage").attr("Visibility",false);

        }
        function showhidedivgraph(){
         
            $("#divgradedetails").hide();
            $("#ctl00_ContentPlaceHolder1_divgraph").show();
            $('#ctl00_ContentPlaceHolder1_divgradedetails').hide();
            $(".dataTables_scroll").hide();
            $("#scaledownPercentage").show();
            $("#scaledownGrade").show();

         
        } 
        function getallotment() {
            $("#divgraph").attr("visibility", "visible");

            // alert('Hi');
            debugger;
            $(function () {
                var obj = {};

                // alert('new');
                //console.log($('#<%=ddlSession.ClientID%>').val(),'not data')
                obj.session = $('#<%=ddlSession.ClientID%>').val();//$('#ddlSession').val();
                obj.sem = $('#<%=ddlSemester.ClientID%>').val();//$('#ddlSemester').val();
                obj.course = $('#<%=ddlCourse.ClientID%>').val();//$('#ddlCourse').val();
                obj.scheme = 0;   //$('#<%=hdfnscheme.ClientID%>').val();

                //obj.session = 203;
                //obj.sem = 1;
                //obj.course = 1084;
                //obj.scheme = 56;
                var scaledownMarks = [];
                var sclaedownStudents = [];
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Grade_Allotment.aspx/GetAllotment",
                    data: JSON.stringify(obj),
                    dataType: "json",

                    success: function (data) {

                        var r = JSON.stringify(data.d);
                        var x = JSON.parse(r);
                        for (var i = 0; i < x.length ; i++) {
                            scaledownMarks.push(x[i]["MARKS"]);
                            sclaedownStudents.push(x[i]["STUD_COUNT"]);
                            //console.log(x[i], "chart-1", scaledownMarks)
                        }

                       
  

                        var  ctx1 = document.getElementById('scaledownPercentage');
                        var  myChart1 = new Chart(ctx1, {
                            plugins: [ChartDataLabels],//, legendMargin],
                            type: 'bar',
                            data: {
                                labels: scaledownMarks,
                                datasets: [{
                                    // label: "Full Time",
                                    data: sclaedownStudents,
                                    backgroundColor: [
                                        'rgba(54, 162, 235, 0.5)',
                                    ],
                                    borderWidth: 1,
                                    borderColor: '#ffff',
                                }

                                ]
                            },
                            options: {
                                layout: {
                                    padding: {
                                        top:18
                                    }
                                },
                                plugins: {
                                    legend: {
                                        display: false

                                    },
                                    datalabels: {
                                        anchor: 'end', // remove this line to get label in middle of the bar
                                        align: 'end',
                                        display: 'auto',
                                        formatter: (val) => (`${val}`),
                            labels: {
                            value: {

                                        font: { size: '12px' },
                                        },

                                        }
                    }
                },


                    scales: {}

            }
            });



        },
        error: function (data) {

        }

        }); 

        });
        };
            


        
        function getGradeAolltement() {

            //alert('hi');
            // debugger;
            $(function () {
                var obj = {};
                //obj.session = $('#ddlSession').val();
                //obj.sem = $('#ddlSemester').val();
                //obj.course = $('#ddlCourse').val();
                //obj.scheme = $('#hdfnscheme').val();
                obj.session = $('#<%=ddlSession.ClientID%>').val();//$('#ddlSession').val();
                obj.sem = $('#<%=ddlSemester.ClientID%>').val();//$('#ddlSemester').val();
                obj.course = $('#<%=ddlCourse.ClientID%>').val();//$('#ddlCourse').val();
                obj.scheme = 0;//$('#<%=hdfnscheme.ClientID%>').val();
                //obj.session = 203;
                //obj.sem = 1;
                //obj.course = 1084;
                //obj.scheme = 56;
                var gradeLabel =[];
                var gradeValue =[];
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Grade_Allotment.aspx/GetGradeAllotment",
                    data: JSON.stringify(obj),
                    dataType: "json",

                    success: function (data) {

                        var r = JSON.stringify(data.d);
                        var x = JSON.parse(r);
                        for (var i = 0; i < x.length ; i++) {
                            gradeLabel.push(x[i]["GRADE_NAME"]);
                            gradeValue.push(x[i]["STUDENT_COUNT"]);
                            //console.log(x[i], "chart-1", scaledownMarks)
                        }


                        var  ctx2 = document.getElementById('scaledownGrade');
                        var  myChart2 = new Chart(ctx2, {
                            plugins: [ChartDataLabels],//, legendMargin],
                            type: 'line',
                            data: {
                                labels: gradeLabel,
                                datasets: [{
                                    //label: "Full Time",
                                    data: gradeValue,
                                    backgroundColor: [
                                        '#fc544b',
                                    ],
                                    borderWidth: 2,
                                    borderColor: '#fc544b',
                                }

                                ]
                            },
                            options: {
                                layout: {
                                    padding: {
                                        top:18
                                    }
                                },
                                plugins: {
                                    legend: {
                                        display: false

                                    },
                                    datalabels: {
                                        anchor: 'end', // remove this line to get label in middle of the bar
                                        align: 'end',
                                        display: 'auto',
                                        formatter: (val) => (`${val}`),
                            labels: {
                            value: {

                                        font: { size: '12px' },
                                        },

                                        }
                    }
                },


                    scales: {}

            }
            });




        },
        error: function (data) {

        }

        });
        });
        }

    
    </script>
</asp:Content>

