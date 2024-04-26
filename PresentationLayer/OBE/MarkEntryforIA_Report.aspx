<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="MarkEntryforIA_Report.aspx.cs" Inherits="Academic_MarkEntryforIA_Report" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpanle1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader"></div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updpanle1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <style>
                .GridHeader {
                    text-align: center !important;
                }
            </style>
            <style style="text/css">
                .bounce {
                    height: 30px;
                    overflow: hidden;
                    position: relative;
                    background: white;
                    color: red;
                    padding: 5px;
                }

                    .bounce p {
                        position: absolute;
                        width: 100%;
                        height: 50%;
                        margin: 0;
                        line-height: 30px;
                        text-align: center;
                        /* Starting position */
                        -moz-transform: translateX(50%);
                        -webkit-transform: translateX(50%);
                        transform: translateX(50%);
                        /* Apply animation to this element */
                        -moz-animation: bouncing-text 10s linear infinite alternate;
                        -webkit-animation: bouncing-text 10s linear infinite alternate;
                        animation: bouncing-text 10s linear infinite alternate;
                    }
                /* Move it (define the animation) */
                @-moz-keyframes bouncing-text {
                    0% {
                        -moz-transform: translateX(50%);
                    }

                    100% {
                        -moz-transform: translateX(-50%);
                    }
                }

                @-webkit-keyframes bouncing-text {
                    0% {
                        -webkit-transform: translateX(50%);
                    }

                    100% {
                        -webkit-transform: translateX(-50%);
                    }
                }

                @keyframes bouncing-text {
                    0% {
                        -moz-transform: translateX(50%); /* Browser bug fix */
                        -webkit-transform: translateX(50%); /* Browser bug fix */
                        transform: translateX(50%);
                    }

                    100% {
                        -moz-transform: translateX(-50%); /* Browser bug fix */
                        -webkit-transform: translateX(-50%); /* Browser bug fix */
                        transform: translateX(-50%);
                    }
                }

                .bounce p:hover {
                    -moz-animation-play-state: paused;
                    -webkit-animation-play-state: paused;
                    animation-play-state: paused;
                }

                .myCss > thead > tr > th, .myCss > tbody > tr > th, .myCss > tfoot > tr > th, .myCss > thead > tr > td, .myCss > tbody > tr > td, .myCss > tfoot > tr > td {
                    border: 1px solid #ddd;
                }

                #myBtnPageScrollUp {
                    display: none;
                    position: fixed;
                    bottom: 30px;
                    right: 20px;
                    z-index: 99;
                    font-size: 18px;
                    border: none;
                    outline: none;
                    background-color: #3c8dbc;
                    color: Black;
                    cursor: pointer;
                    padding: 15px;
                    border-radius: 1em;
                }

                    #myBtnPageScrollUp:hover {
                        background-color: #555;
                    }
            </style>
            <div class="row" id="myDiv">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">CAT Exam Mark Entry Report</h3>
                        </div>

                        <%--<div class="box-header with-border bounce" id="divstatus" runat="server" visible="false">
                            <marquee onmouseover="this.stop();" onmouseout="this.start();><b>This is to inform you that Mark Entry activity(s) For Session :
                                <asp:Label ID="lblsession" style="color:#3c8dbc;" runat="server"></asp:Label>  are going to be stopped for
                                <asp:Label ID="lblstatusmark" style="color:#3c8dbc;" runat="server"></asp:Label> on 
                                <asp:Label ID="lblStopDate" style="color:#3c8dbc;" runat="server"></asp:Label> , so kindly enter the marks and lock for your respective subjects !!!!!</b></marquee>

                        </div>--%>

                        <div class="box-body">
                            <asp:Panel ID="pnlSelection" runat="server">
                                <fieldset>


                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label>Session</label>
                                                <asp:DropDownList ID="ddlSession" runat="server" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" Font-Bold="true" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Subject Type</label>
                                                <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged"
                                                    TabIndex="1">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                                    Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="selcourse">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-md-12" id="TRCourses" runat="server">
                                        <div class="col-md-12">
                                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">

                                                <asp:ListView ID="lvCourse" runat="server" Visible="false">
                                                    <LayoutTemplate>


                                                        <table class="table table-hover table-stripped table-responsive">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Course Name
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                    </ItemTemplate>

                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                        <div>
                                            <asp:Label ID="lblStatus" Visible="false" runat="server" Style="color: red; font: larger; margin-right: 912px;"></asp:Label>
                                        </div>
                                        <br />

                                        <div class="col-md-12">

                                            <%--<asp:GridView ID="GVEntryStatus" runat="server" AutoGenerateColumns="false" OnPreRender="GVEntryStatus_PreRender" OnRowDataBound="GVEntryStatus_RowDataBound"
                                                CssClass="table table-striped table-bordered table-hover"
                                                BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="4" CellSpacing="2">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="COURSE NAME" HeaderStyle-Width="350">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("COURSENAME") %>'
                                                                CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("BATCHNO")+ "+" + Eval("EXAMNO")+"+"+Eval("FLDNAME")+"+"+Eval("EXAMNAME") %>'
                                                                OnClick="lnkbtnCourse_Click" ToolTip='<%# Eval("COURSENO")%>' />
                                                            <asp:HiddenField ID="hdnfld_courseno" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                            <asp:HiddenField ID="hdnsem" runat="server" Value='<%# Eval("semesterno")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="SEM" HeaderText="SEM" ItemStyle-Width="35" HeaderStyle-Height="30%" ItemStyle-Font-Size="92%" ItemStyle-BorderStyle="None" />

                                                    <asp:BoundField DataField="EXAMNAME" HeaderText="EXAM NAME" ItemStyle-Width="130" HeaderStyle-Height="30%" ItemStyle-Font-Size="92%" ItemStyle-BorderStyle="None" />
                                                    <asp:BoundField DataField="MARK_ENTRY_STATUS" HeaderText="STATUS" ItemStyle-Width="80" HeaderStyle-Height="30%" ItemStyle-Font-Size="92%" ItemStyle-BorderStyle="None" ItemStyle-Font-Bold="true" />
                                                    

                                                     <asp:TemplateField HeaderText="PRINT" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnPrint" runat="server" CssClass="btn btn-success" OnClick="lbtnPrint_Click" CommandArgument='<%# Eval("COURSENO")+","+Eval("COURSENAME")+","+Eval("semesterno")+","+Eval("SECTIONNO")+","+Eval("EXAMNO")+","+Eval("EXAMNAME") %>'><i class="fa fa-print" aria-hidden="true"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="OK" HeaderStyle-Width="20">
                                                        <ItemTemplate>
                                                            <span><%# Eval("SEM")%></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    

                                                </Columns>
                                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                                <HeaderStyle BackColor="#6b99c7" Font-Bold="True" ForeColor="White" CssClass="bg-light-blue" />
                                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                                <RowStyle BackColor="White" ForeColor="Black" />
                                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>--%>

                                            <div runat="server" id="Div_ExamNameList" visible="false">
                                                <hr />
                                                <table class="table table-hover table-bordered">
                                                    <thead style="background-color: #3c8dbc; color: Black;">
                                                        <tr>
                                                            <th>
                                                                <center>SR.</center>
                                                            </th>
                                                            <th>
                                                                <center>SUBJECT NAME</center>
                                                            </th>
                                                            <th>
                                                                <center>SEM</center>
                                                            </th>
                                                            <th>
                                                                <center>SEC</center>
                                                            </th>
                                                            <th style="width: 20%">
                                                                <center>EXAM NAME</center>
                                                            </th>
                                                            <th style="width: 10%">
                                                                <center>STATUS</center>
                                                            </th>
                                                            <%--<th style="width:10%"><center>LOCK DATE</center></th>--%>
                                                            <th>
                                                                <center>PRINT</center>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rptExamName" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <center><%# Container.ItemIndex + 1 %></center>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblcname" runat="server" Text='<%# Eval("COURSENAME") %>' CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("SECTIONNO")+ "+" + Eval("EXAMNO")+"+"+Eval("FLDNAME")+"+"+Eval("EXAMNAME") %>' OnClick="lnkbtnCourse_Click" ToolTip='<%# Eval("COURSENO")%>'></asp:Label>
                                                                        <%--<asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("COURSENAME") %>' CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("SECTIONNO")+ "+" + Eval("EXAMNO")+"+"+Eval("FLDNAME")+"+"+Eval("EXAMNAME") %>' OnClick="lnkbtnCourse_Click" ToolTip='<%# Eval("COURSENO")%>' />--%>
                                                                        <%--<Label ID="lblcname" runat="server" Text='<%# Eval("COURSENAME") %>' CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("SECTIONNO")+ "+" + Eval("EXAMNO")+"+"+Eval("FLDNAME")+"+"+Eval("EXAMNAME") %>' OnClick="lnkbtnCourse_Click" ToolTip='<%# Eval("COURSENO")%>'></Label>--%>
                                                                        <asp:HiddenField ID="hdnfld_courseno" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                                        <asp:HiddenField ID="hdnsem" runat="server" Value='<%# Eval("semesterno")%>' />
                                                                        <asp:HiddenField ID="hdsec" runat="server" Value='<%# Eval("SECTIONNO")%>' />
                                                                        <asp:HiddenField ID="hdexam" runat="server" Value='<%# Eval("EXAMNO")%>' />
                                                                        <asp:HiddenField ID="hdexamname" runat="server" Value='<%# Eval("EXAMNAME")%>' />
                                                                         <asp:HiddenField ID="hdfldname" runat="server" Value='<%# Eval("FLDNAME")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <center><%#Eval("SEMESTERNAME") %></center>
                                                                    </td>
                                                                    <td>
                                                                        <center><%#Eval("SECTIONNAME") %></center>
                                                                    </td>
                                                                    <td>
                                                                        <center><%#Eval("EXAMNAME") %></center>
                                                                    </td>
                                                                    <td runat="server" style="display: none;">
                                                                        <%--<%#Eval("MARK_ENTRY_STATUS") %>--%>
                                                                        <%--   <center>--%>
                                                                        <asp:Label ID="lblCompleted" runat="server" Text="COMPLETED" Style="font-family: Calibri; color: white; font-size: 12px; font-weight: bold; background-color: green; padding: 5px; text-align: center; box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);" Visible='<%# Regex.Split(Eval("MARK_ENTRY_STATUS").ToString(),"#")[0] == "1" ? true : false %>'></asp:Label>
                                                                        <asp:Label ID="lblIncomplete" runat="server" Text="IN PROGRESS" Style="font-family: Calibri; color: black; font-size: 12px; font-weight: bold; background-color: orange; padding: 5px; text-align: center; box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);" Visible='<%# Regex.Split(Eval("MARK_ENTRY_STATUS").ToString(),"#")[0] == "0" ? true : false %>'></asp:Label>
                                                                        <%--  </center>--%>
                                                                    </td>
                                                                    <td>
                                                                        <%-- <center>
                                                                            <span><%# Regex.Split(Eval("MARK_ENTRY_STATUS").ToString(),"#")[1] %></span>
                                                                        </center>--%>
                                                                        <asp:Label Text='<%# Eval("MARK_ENTRY_STATUS")%>' ID="lblStatus" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <%-- <center>--%>
                                                                        <asp:LinkButton ID="lbtnPrint" runat="server" CssClass="btn btn-default" OnClick="lbtnPrint_Click" CommandArgument='<%# Eval("COURSENO")+","+Eval("COURSENAME")+","+Eval("semesterno")+","+Eval("SECTIONNO")+","+Eval("EXAMNO")+","+Eval("EXAMNAME")+","+Eval("FLDNAME") %>'><i class="fa fa-print" aria-hidden="true"></i></asp:LinkButton>
                                                                        <%--</center>--%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>

                                        </div>
                                    </div>

                                </fieldset>

                            </asp:Panel>

                            <div class="col-md-12">
                                <asp:Panel ID="pnlMarkEntry" runat="server">

                                    <div class="col-12">
                                        <div class="row">
                                            <%--  <div class="col-md-9">--%>
                                            <div id="Div1" class="col-md-3" runat="server">
                                                <div class="label-dynamic">
                                                    <label>Session</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession2" runat="server" AppendDataBoundItems="true" CssClass="form-control" Font-Bold="true">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-6">
                                                <div class="label-dynamic">
                                                    <label>Course Name</label>
                                                </div>
                                                <asp:Label ID="lblCourse" Style="font-size: 12px;" runat="server" Font-Bold="true" CssClass="label label-default"></asp:Label>
                                                <asp:HiddenField ID="hdfSection" runat="server" />
                                                <asp:HiddenField ID="hdfBatch" runat="server" />
                                            </div>

                                            <div class="col-md-3">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Exam</label>
                                                </div>
                                                <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged"
                                                    ValidationGroup="show">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Label ID="lblSubExamName" runat="server" Text="Sub Exam :" CssClass="label" Style="color: black; font-size: 14px; padding-left: 0px"></asp:Label>
                                                <asp:DropDownList ID="ddlSubExam" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlSubExam_SelectedIndexChanged"
                                                    ValidationGroup="show" Enabled="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExam"
                                                    Display="None" ErrorMessage="Please Select Exam" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="show"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="col-md-3">
                                                <asp:Label ID="lblsort" runat="server" Text="Sort By :" CssClass="label" Style="color: black; font-size: 14px; padding-left: 0px"></asp:Label>
                                                <asp:DropDownList ID="ddlSort" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control"
                                                    ValidationGroup="show">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">PRN No</asp:ListItem>
                                                    <asp:ListItem Value="2">Roll No</asp:ListItem>
                                                    <asp:ListItem Value="3">Student Name</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>




                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <%-- <center>--%>
                                        <asp:Button ID="btnShow" runat="server" CssClass="btn btn-info" Font-Bold="True" OnClick="btnShow_Click"
                                            Text="Show" ValidationGroup="show" Style="margin-top: 20px" />

                                        <asp:Button ID="btnBack" runat="server" Font-Bold="true" OnClick="btnBack_Click" Text="Back" CssClass="btn btn-warning" Style="margin-top: 20px" />

                                        <asp:Button ID="btnSave" runat="server" Visible="false" Font-Bold="true" OnClientClick="this.disabled=true;" UseSubmitBehavior="false"
                                            OnClick="btnSave_Click" Text="Save" CssClass="btn btn-success btnSaveEnabled" ValidationGroup="val" Style="margin-top: 20px" />

                                        <asp:Button ID="btnLock" runat="server" Visible="false" Font-Bold="true"
                                            OnClick="btnLock_Click" OnClientClick="return showLockConfirm(this,'val');" Text="Lock"
                                            CssClass="btn btn-danger" Style="margin-top: 20px" />

                                        <asp:Button ID="btnCancel2" runat="server" Font-Bold="true" OnClick="btnCancel2_Click"
                                            Text="Cancel" CssClass="btn btn-warning" Visible="False" Style="margin-top: 20px" />

                                        <asp:Button ID="btnMIDReport" runat="server" Font-Bold="true" Text="MID Report" CssClass="btn btn-info"
                                            OnClick="btnTAReport_Click" Visible="False" Height="26px" Style="margin-top: 20px" />

                                        <asp:Button ID="btnConsolidateReport" runat="server" Font-Bold="true" Visible="False"
                                            Text="Consolidate Report" CssClass="btn btn-info" OnClick="btnConsolidateReport_Click" Style="margin-top: 20px" />

                                        <asp:Button ID="btnPrintReport" runat="server" Font-Bold="true" OnClick="btnPrintReport_Click"
                                            Text="Print" CssClass="btn btn-primary" Visible="False" Style="margin-top: 20px" />

                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="show"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        <%--</%--center--%>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblStudents" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                            </div>
                            <%--<div class="col-md-4">
                                            <fieldset class="fieldset" style="padding: 5px; color: Green">
                                                <legend class="legend">Note</legend>
                                                <span style="font-weight: bold; color: Red;">Please Save and Lock for Final Submission of Marks
                                                </span>
                                                <br />
                                                <b>Enter :<br />
                                                    "-1" for Absent Student<br />
                                                    "-2" for Not Eligible Student<br />
                                                    "-3" for WithDraw Student<br />
                                                    "-4" for Drop Student<br />
                                                </b>
                                            </fieldset>
                                        </div>--%>
                            <div class="col-md-3">
                                <asp:Repeater ID="rptMarkCodes" runat="server">
                                    <HeaderTemplate>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <h3 style="margin-top: 5px; margin-bottom: 5px">Note</h3>
                                                <span style="font-weight: bold; color: Red; font-size: 12px">Please Save and Lock for Final Submission of Marks</span>
                                            </div>
                                            <div class="panel-body" style="background-color: whitesmoke">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <strong><%# Eval("CODE_VALUE")%></strong> - <%# Eval("CODE_DESC")%><br />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </div>
                                                    </div>
                                    </FooterTemplate>
                                </asp:Repeater>

                            </div>
                            <div class="box-footer col-md-12">
                                <p class="text-center">
                                </p>

                                <div class="col-md-12" id="tdStudent" runat="server">
                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                                        <asp:Panel ID="pnlStudGrid" runat="server" Visible="false">
                                            <div id="demo-grid" class="vista-grid">
                                                <div class="titlebar">
                                                    <h4>Enter Marks for following Students</h4>
                                                </div>
                                                <asp:HiddenField ID="hfdMaxMark" runat="server" />
                                                <asp:HiddenField ID="hfdMinMark" runat="server" />

                                                <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered myCss" BackColor="#ffffff" BorderStyle="None" HeaderStyle-CssClass="GridHeader" OnRowDataBound="gvStudent_RowDataBound">
                                                    <HeaderStyle CssClass="bg-light-blue" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="1%" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-Height="15px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PRN No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("REGNO") %>' ToolTip='<%# Bind("IDNO") %>'
                                                                    Font-Size="11pt"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ROLL NO." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAdmNo" runat="server" Text='<%# Bind("ROLLNO") %>' ToolTip='<%# Bind("IDNO") %>'
                                                                    Font-Size="11pt"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="STUDNAME" HeaderText="Student Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                        </asp:BoundField>
                                                        <%--EXAM MARK ENTRY--%>
                                                        <asp:TemplateField HeaderText="MARKS" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%--<asp:TextBox ID="txtMarks" CssClass="MarkValidation form-control" runat="server" Text='<%# Bind("SMARK") %>' Width="80px"
                                                                                Enabled='<%# (Eval("LOCK").ToString() == "True") ? false : true %>'
                                                                                MaxLength="5" Font-Bold="true" Style="text-align: center;box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);" />--%>
                                                                <asp:TextBox ID="txtMarks" CssClass="form-control" runat="server" Text='<%# Bind("SMARK") %>' Width="80px"
                                                                    Enabled='<%# (Eval("LOCK").ToString() == "True") ? false : true %>'
                                                                    MaxLength="5" Font-Bold="true" Style="text-align: center; box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);" />
                                                                <asp:Label ID="lblMarks" runat="server" Text='<%# Bind("SMAX") %>' ToolTip='<%# Bind("LOCK") %>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblMinMarks" runat="server" Text='<%# Bind("SMIN") %>' Visible="false"></asp:Label>

                                                                <%--<ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                                                                ValidChars="0123456789-" TargetControlID="txtMarks">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>--%>

                                                                <%-- <asp:CompareValidator ID="comTutMarks" ValueToCompare='<%# Bind("SMAX") %>' ControlToValidate="txtMarks"
                                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                                                                ValidationGroup="val" Text="*"></asp:CompareValidator>--%>
                                                                <%--<ajaxToolKit:ValidatorCalloutExtender TargetControlID="comTutMarks" ID="cecomTutMarks"
                                                                                runat="server">
                                                                            </ajaxToolKit:ValidatorCalloutExtender>--%>
                                                                <%--<asp:CompareValidator ID="cvAbsentStud" ValueToCompare="-1" ControlToValidate="txtMarks"
                                                                                Operator="NotEqual" Type="Double" runat="server" ErrorMessage="-1 for absent student"
                                                                                ValidationGroup="val1" Text="*">
                                                                            </asp:CompareValidator>--%>
                                                                <%--<ajaxToolKit:ValidatorCalloutExtender TargetControlID="cvAbsentStud" ID="vceAbsentStud"
                                                                                runat="server">
                                                                            </ajaxToolKit:ValidatorCalloutExtender>--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="10%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                                    <HeaderStyle BackColor="#6b99c7" Font-Bold="True" ForeColor="Black" />
                                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                            </div>
                                        </asp:Panel>
                                    </asp:Panel>
                                </div>
                            </div>

                            </fieldset>

                                </asp:Panel>

                        </div>
                    </div>

                </div>
            </div>
            </div>


            <div id="divMsg" runat="server">
            </div>
            <asp:HiddenField ID="hdfDegree" runat="server" Value="" />

            <%--<button onclick="topFunction()" id="myBtnPageScrollUp" title="Go to top">Top</button>--%>
            <button id="myBtnPageScrollUp" title="Go to top"><i class="fa fa-arrow-up" aria-hidden="true"></i></button>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="myModal33" class="modal fade" role="dialog" data-backdrop="static">
        <asp:Panel ID="pnlOTP" runat="server">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header bg-primary text-center">
                        Enter OTP
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div style="z-index: 1; position: absolute; top: 50%; left: 600px;">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpanle1"
                                DynamicLayout="true" DisplayAfter="0">
                                <ProgressTemplate>
                                    <div style="width: 120px; padding-left: 5px">
                                        <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                                        <p class="text-success"><b>Loading..</b></p>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <asp:UpdatePanel ID="UpdOTP" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class=" form-group col-md-12">
                                    <asp:Label ID="lblOTP" runat="server" Visible="false" ForeColor="Green" Font-Bold="true"></asp:Label>
                                </div>
                                <div class=" form-group col-md-12" style="background-color: floralwhite">
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtOTP" runat="server" Width="75%" CssClass="form-control" MaxLength="20" placeholder="Enter the OTP here..."></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <span class="input-group-btn">
                                            <asp:Button ID="btnOTPLockMarks" runat="server" Text="Verify OTP & Lock Marks" ValidationGroup="otp" CssClass="btn btn-primary" Font-Bold="true" OnClick="btnOTPLockMarks_Click" />
                                            <asp:RequiredFieldValidator ID="rfvOTP" runat="server" ControlToValidate="txtOTP"
                                                ErrorMessage="Please Enter the OTP" Display="None" ValidationGroup="otp"></asp:RequiredFieldValidator>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="otp"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </span>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnOTPLockMarks" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                    <div class="modal-footer">
                        <p class="text-center" id="keep" style="font-weight: 600;"></p>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

    <%--Modal Starts Here--%>
    <div class="modal fade" id="PrintModal" role="dialog">
        <asp:UpdatePanel ID="updPopUp" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" style="color: red; font-weight: bolder">&times;</button>
                            <h4 class="modal-title" style="color: #3c8dbc;"><i class="fa fa-print" aria-hidden="true"></i>Print Report</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-8 col-md-offset-2">
                                    <label>Subject Name :</label><br />
                                    <asp:Label ID="lbl_SubjectName" runat="server" BackColor="WhiteSmoke"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-8 col-md-offset-2">
                                    <asp:Label ID="lbl_Exam_Print" runat="server" Text="Exam :" CssClass="label" Style="color: black; font-size: 14px; padding-left: 0px"></asp:Label>
                                    <asp:DropDownList ID="ddlExamPrint" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlExamPrint_SelectedIndexChanged"
                                        ValidationGroup="show">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-8 col-md-offset-2">
                                    <asp:Label ID="lbl_SubExam_Print" runat="server" Text="Sub Exam :" CssClass="label" Style="color: black; font-size: 14px; padding-left: 0px"></asp:Label>
                                    <asp:DropDownList ID="ddlSubExamPrint" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlSubExamPrint_SelectedIndexChanged"
                                        ValidationGroup="show" Enabled="false">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnPrintFront" runat="server" Text="Print" CssClass="btn btn-success" OnClick="btnPrintFront_Click" Enabled="false" />
                            <asp:Button ID="btnPrintAll" runat="server" Text="PrintAll" CssClass="btn btn-success" OnClick="btnPrintAll_Click" Enabled="true" />
                        </div>
                    </div>

                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlExamPrint" />
                <asp:AsyncPostBackTrigger ControlID="btnPrintFront" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--Modal Ends Here--%>


    <script language="javascript" type="text/javascript">

        function showLockConfirm(btn, group) {
            debugger;
            var validate = false;
            if (Page_ClientValidate(group)) {

                validate = ValidatorOnSubmit();
            }
            if (validate)    //show div only if page is valid and ready to submit
            {
                //var ret = confirm('Do you really want to lock marks for selected exam?\n\nOnce Locked it cannot be modified or changed.');
                var ret = true;
                if (ret == true) {
                    var ret2 = confirm('You are about to lock entered marks, be sure before locking.\n\nOnce Locked it cannot be modified or changed. \n\nClick OK to Continue....');
                    if (ret2 == true) {
                        validate = true;
                    }
                    else
                        validate = false;
                }
                else
                    validate = false;
            }
            var counter = 60;
            myVar = setInterval(function () {
                if (counter >= 0) {
                    document.getElementById("keep").innerHTML = "Your Popup will be close in " + counter + " Sec";
                }
                if (counter == 0) {
                    $("#myModal33").hide();
                    $(".modal-backdrop").removeClass("in");
                    $(".modal-backdrop").hide();
                }
                counter--;
                return false;
            }, 1000)

            return validate;

        }
    </script>

    <%--    <script>
        $(document).ready(function () {

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

                $(".MarkValidation").keypress(function (e) {
                    if (((e.which != 46 || (e.which == 46 && $(this).val() == '')) || $(this).val().indexOf('.') != -1) && (e.which < 48 || e.which > 57)) {
                        e.preventDefault();
                        //$(this).val("Digits Only").show().fadeOut("slow");
                        $(this).fadeOut("slow").css("border", "1px solid red");
                        $(this).fadeIn("slow");
                    }
                    else {
                        $(this).css("border", "1px solid #3c8dbc");
                    }
                }).on('paste', function (e) {
                    e.preventDefault();
                });

                $(".MarkValidation").focusout(function () {
                    debugger;
                    $(this).css("border", "1px solid #d2d6de"); 
                    var MaxMarks = $(".MaxMarks").html().split(':')[1].slice(0, -1).trim();
                    if (parseInt($(this).val()) == 90) {
                        if (parseInt($(this).val()) > MaxMarks) {
                            alert('Marks should not greater than Max Marks !!');
                            $(this).val('');
                            $(this).focus();
                        }
                    }
                });

                $(".MarkValidation").keyup(function () {
                    if (parseFloat($(this).val().trim()) > parseFloat($('input[id$=hfdMaxMark]').val().trim())) {
                        if (("902").indexOf($(this).val()) == -1 && ("903").indexOf($(this).val()) == -1 && ("904").indexOf($(this).val()) == -1 && ("905").indexOf($(this).val()) == -1 && ("906").indexOf($(this).val()) == -1) {
                            alert('Marks should not greater than Max Marks !!');
                            $(this).val('');
                        }
                    }
                    else if (parseFloat($(this).val().trim()) < parseFloat($('input[id$=hfdMinMark]').val().trim())) {
                        alert('Marks should not less than Min Marks !!');
                        $(this).val('');
                    }
                });

            });

        });
        
    </script>--%>

    <script type="text/javascript">
        //jq1833 = jQuery.noConflict();
        $(document).ready(function () {

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

                $("#ctl00_ContentPlaceHolder1_gvStudent .form-control").keypress(function (e) {
                    if (((e.which != 46 || (e.which == 46 && $(this).val() == '')) || $(this).val().indexOf('.') != -1) && (e.which < 48 || e.which > 57)) {
                        e.preventDefault();
                        //$(this).val("Digits Only").show().fadeOut("slow");
                        $(this).fadeOut("slow").css("border", "1px solid red");
                        $(this).fadeIn("slow");
                    }
                    else {
                        $(this).css("border", "1px solid #3c8dbc");
                    }
                }).on('paste', function (e) {
                    e.preventDefault();
                });

                $("#ctl00_ContentPlaceHolder1_gvStudent .form-control").focusout(function () {

                    debugger;

                    $(this).css("border", "1px solid #d2d6de");
                    var MaxMarks = parseFloat($('input[id$=hfdMaxMark]').val().trim());//$(".MaxMarks").html().split(':')[1].slice(0, -1).trim();
                    //alert('hi Beerla');
                    //alert(MaxMarks);
                    //if (parseInt($(this).val()) == 90) {
                    if (parseInt($(this).val()) == 90 || parseInt($(this).val()) == 9 || parseInt($(this).val()) == 6) {
                        if (parseInt($(this).val()) > MaxMarks) {
                            alert('Marks should not greater than Max Marks !!');
                            $(this).val('');
                            $(this).focus();
                        }
                    }
                });

                $("#ctl00_ContentPlaceHolder1_gvStudent .form-control").keyup(function () {
                    debugger

                    if (parseFloat($(this).val().trim()) > parseFloat($('input[id$=hfdMaxMark]').val().trim())) {
                       
                        if (("902").indexOf($(this).val()) == -1 && ("903").indexOf($(this).val()) == -1 && ("904").indexOf($(this).val()) == -1 && ("905").indexOf($(this).val()) == -1 && ("906").indexOf($(this).val()) == -1) {
                            alert('Marks should not greater than Max Marks !!');
                            $(this).val('');
                        }
                       
                    }
                    //else if (parseFloat($(this).val().trim()) < parseFloat($('input[id$=hfdMinMark]').val().trim())) {
                    //    alert('Marks should not less than Min Marks !!');
                    //    $(this).val('');
                    //}
                });

            });

        });

    </script>

    <script>
        // When the user scrolls down 20px from the top of the document, show the button
        window.onscroll = function () { scrollFunction() };

        function scrollFunction() {
            if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
                document.getElementById("myBtnPageScrollUp").style.display = "block";
            } else {
                document.getElementById("myBtnPageScrollUp").style.display = "none";
            }
        }

        //// When the user clicks on the button, scroll to the top of the document
        //function topFunction() {
        //    document.body.scrollTop = 0;
        //    document.documentElement.scrollTop = 0;
        //}
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $("#myBtnPageScrollUp").click(function () {
                $('html, body').animate({
                    scrollTop: $("#myDiv").offset().top
                }, 2000);
            });

            //$(".btnSaveEnabled").click(function () {
            //  $(<%=btnSave.ClientID%>).prop('disabled', true);
            // return true;
            // });

        });
    </script>

</asp:Content>
