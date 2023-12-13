<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="MarkEntryforIA_External_CC.aspx.cs" Inherits="Academic_MarkEntryforIA_External_CC" Title="" %>

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
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .newheader {
            position: sticky!important;
            z-index: 1;
            top: 0;
            background: #fff !important;
            box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;
        }
    </style>
    <asp:UpdatePanel ID="updpanle1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row" id="myDiv">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">External Grade/Mark Entry</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlSelection" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Subject Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
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
                                <div id="TRCourses" runat="server">
                                    <div class="col-12">
                                        <asp:Panel ID="Panel1" runat="server">
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
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                        <div>
                                            <asp:Label ID="lblStatus" Visible="false" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-12" runat="server" id="Div_ExamNameList" visible="false">
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>SR.</th>
                                                    <th>SUBJECT NAME</th>
                                                    <th>SEM </th>
                                                    <th>SEC</th>
                                                    <th>SUBEXAM NAME</th>
                                                    <th>STATUS</th>
                                                    <th>PRINT </th>
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
                                                                <asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("COURSENAME") %>' CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("SECTIONNO")+ "+" + Eval("SUBEXAMNO")+"+"+Eval("FLDNAME")+"+"+Eval("SUBEXAMNAME")+"+"+Eval("FLDNAME2")+"+"+Eval("EXAMNO") %>' OnClick="lnkbtnCourse_Click" ToolTip='<%# Eval("COURSENO")%>' />
                                                                <asp:HiddenField ID="hdnfld_courseno" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                                <asp:HiddenField ID="hdnsem" runat="server" Value='<%# Eval("semesterno")%>' />
                                                            </td>
                                                            <td><%#Eval("SEMESTERNAME") %> </td>
                                                            <td>
                                                                <%#Eval("SECTIONNAME") %> </td>
                                                            <td><%#Eval("SUBEXAMNAME") %>  </td>
                                                            <td>
                                                                <asp:Label Text='<%# Eval("MARK_ENTRY_STATUS")%>' ID="lblStatus" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="text-align: center">
                                                                <asp:LinkButton ID="lbtnPrint" runat="server" OnClick="lbtnPrint_Click" CommandArgument='<%# Eval("COURSENO")+","+Eval("COURSENAME")+","+Eval("semesterno")+","+Eval("SECTIONNO")+","+Eval("EXAMNO")+","+Eval("EXAMNAME")+","+Eval("FLDNAME") %>'><i class="fa fa-print" aria-hidden="true"></i></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12">
                                <asp:Panel ID="pnlMarkEntry" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession2" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" Font-Bold="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-9 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Course Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblCourse" runat="server" Font-Bold="true" ToolTip='<%# Eval("SECTIONNO") + "+" + Eval("SECTIONNO")+ "+" + Eval("SUBEXAMNO")+"+"+Eval("FLDNAME")+"+"+Eval("SUBEXAMNAME") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdfSection" runat="server" />
                                                        <asp:HiddenField ID="hdfBatch" runat="server" />
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <%--<div class="col-lg-6 col-md-12 col-12">--%>
                                        <div class="col-lg-12 col-md-12 col-12">
                                            <div class="row">
                                                <%--<div class="form-group col-lg-6 col-md-6 col-12">--%>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>
                                                            <asp:Label ID="lblSubExamName" runat="server" Text="Sub Exam"></asp:Label></label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubExam" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlSubExam_SelectedIndexChanged"
                                                        ValidationGroup="show" data-select2-enable="true" Enabled="false">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlSubExam"
                                                        Display="None" ErrorMessage="Please Select Exam" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>
                                                <%--<div class="form-group col-lg-6 col-md-6 col-12">--%>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>
                                                            <asp:Label ID="lblsort" runat="server" Text="Sort By"></asp:Label>
                                                        </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSort" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                        ValidationGroup="show">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">PRN No</asp:ListItem>
                                                        <asp:ListItem Value="2">Roll No</asp:ListItem>
                                                        <asp:ListItem Value="3">Student Name</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <%--<div class="form-group col-lg-12 col-md-12 col-12">--%>
                                                <div class="form-group col-lg-6 col-md-12 col-12">
                                                    <div class="sub-heading">
                                                        <h5>Activity Details</h5>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Start Date :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblStartD" runat="server" Font-Bold="true"></asp:Label>
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>End Date :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblEndD" runat="server" Font-Bold="true"></asp:Label></a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-12 col-md-6 col-12 mb-3" runat="server" id="divnote">
                                            <asp:Repeater ID="rptMarkCodes" runat="server">
                                                <HeaderTemplate>
                                                    <div class=" note-div">
                                                        <h5 class="heading">Note</h5>
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Please Save and Lock for Final Submission of Marks</span> </p>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div style="display: inline-block; margin-right: 10px;">
                                                        <strong><%# Eval("CODE_VALUE")%></strong> - <%# Eval("CODE_DESC")%>
                                                    </div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Font-Bold="True" OnClick="btnShow_Click"
                                            Text="Show" ValidationGroup="show" />

                                        <asp:Button ID="btnSave" runat="server" Visible="false" OnClientClick="this.disabled=true;" UseSubmitBehavior="false"
                                            OnClick="btnSave_Click" Text="Save" CssClass="btn btn-primary btnSaveEnabled" ValidationGroup="val" />
                                        <asp:Button ID="btnPublish" runat="server" Visible="false" OnClick="btnPublish_Click" OnClientClick="return showPublishConfirm(this,'val');" Text="Publish" CssClass="btn btn-warning"></asp:Button>
                                        <asp:Button ID="btnLock" runat="server" Visible="false" OnClick="btnLock_Click" OnClientClick="return showLockConfirm(this,'val');" Text="Lock" CssClass="btn btn-warning"></asp:Button>
                                        <asp:Button ID="lnkExcekImport" runat="server" Visible="true" OnClick="lnkExcekImport_Click" Text="Import Mark Entry Excel" OnClientClick="validateField();" CssClass="btn btn-info"></asp:Button>

                                        <asp:Button ID="btnPrintReport" runat="server" OnClick="btnPrintReport_Click" Text="Print" CssClass="btn btn-primary" Visible="False" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="show"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn btn-warning" />
                                    </div>
                                    <div class="col-12 btn-footer mt-2">
                                        <asp:Label ID="lblStudents" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-12" visible="false" id="pnlUP" runat="server">
                                <asp:UpdatePanel ID="updImport" runat="server">
                                    <ContentTemplate>
                                        <div class="sub-heading">
                                            <h5>Import Mark Entry Excel</h5>
                                        </div>
                                        <asp:Panel ID="Panel3" runat="server">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Excel File</label>
                                                </div>
                                                <asp:FileUpload ID="FuBrowse" runat="server" oolTip="Select file to Import" TabIndex="14" />
                                            </div>
                                            <div class="form-group">
                                                <asp:LinkButton ID="btnBlankDownld" runat="server" OnClick="btnBlankDownld_Click" TabIndex="17" CssClass="btn btn-primary" ValidationGroup="show"
                                                    Text="Blank" ToolTip="Click to download blank excel format file" OnClientClick="validateField();" Enabled="true"> Download Blank Excel Sheet For Mark Entry</asp:LinkButton>
                                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" OnClick="btnUpload_Click"
                                                    TabIndex="16" Text="Upload Excel" ToolTip="Click to Upload" Enabled="true" />
                                                <asp:Button ID="btnCancel1" runat="server" TabIndex="18" Text="Clear" ToolTip="Click To Clear" CssClass="btn btn-warning" OnClick="btnCancel1_Click" />
                                            </div>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnBlankDownld" />
                                        <asp:PostBackTrigger ControlID="btnUpload" />
                                        <asp:PostBackTrigger ControlID="btnCancel1" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-12 mt-3" id="tdStudent" runat="server">
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:Panel ID="pnlStudGrid" runat="server" Visible="false">
                                        <div id="demo-grid" class="vista-grid">
                                            <div class="sub-heading">
                                                <h5>
                                                    <%--Enter Marks for following Students--%>
                                                    <asp:Label runat="server" ID="lblGridHeading" Text=""></asp:Label>
                                                </h5>
                                            </div>
                                            <asp:HiddenField ID="hfdMaxMark" runat="server" />
                                            <asp:HiddenField ID="hfdMinMark" runat="server" />
                                            <div class="table-reponsive" style="height: 440px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered nowrap" BackColor="#ffffff" BorderStyle="None" HeaderStyle-CssClass="GridHeader" OnRowDataBound="gvStudent_RowDataBound">
                                                    <HeaderStyle CssClass="bg-light-blue newheader" />
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
                                                        <asp:TemplateField HeaderText="MARKS" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMarks" CssClass="form-control" runat="server" Text='<%# Bind("SMARK") %>' Width="80px"
                                                                    Enabled='<%# (Eval("LOCK").ToString() == "True") ? false : true %>'
                                                                    MaxLength="5" Font-Bold="true" Style="text-align: center; box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);" />
                                                                <asp:Label ID="lblMarks" runat="server" Text='<%# Bind("SMAX") %>' ToolTip='<%# Bind("LOCK") %>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblMinMarks" runat="server" Text='<%# Bind("SMIN") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GRADE" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlgrademarks" runat="server" AppendDataBoundItems="True" CssClass="form-control" CausesValidation="true" TabIndex="1">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attendance In %" Visible="false" ItemStyle-Width="10%" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAttendance" CssClass="form-control" runat="server" Enabled="false" Width="80px"
                                                                    MaxLength="5" Font-Bold="true" Style="text-align: center; box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);" />
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
                                        </div>
                                    </asp:Panel>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
            <asp:HiddenField ID="hdfDegree" runat="server" Value="" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="PrintModal" role="dialog">
        <asp:UpdatePanel ID="updPopUp" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" style="color: #3c8dbc;"><i class="fa fa-print" aria-hidden="true"></i>Print Report</h4>
                            <button type="button" class="close" data-dismiss="modal" style="color: red; font-weight: bolder">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="col-lg-12 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Subject Name  :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lbl_SubjectName" runat="server"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>
                                                <asp:Label ID="lbl_Exam_Print" runat="server" Text="Exam"></asp:Label>
                                            </label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamPrint" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlExamPrint_SelectedIndexChanged"
                                            ValidationGroup="show">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>
                                                <asp:Label ID="lbl_SubExam_Print" runat="server" Text="Sub Exam"></asp:Label>
                                            </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubExamPrint" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSubExamPrint_SelectedIndexChanged"
                                            ValidationGroup="show" Enabled="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnPrintFront" runat="server" Text="Print" CssClass="btn btn-info" OnClick="btnPrintFront_Click" Enabled="false" />
                            <asp:Button ID="btnPrintAll" runat="server" Text="PrintAll" CssClass="btn btn-info" OnClick="btnPrintAll_Click" Enabled="true" Visible="false" />
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


        function showPublishConfirm(btn, group) {
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
                    var ret2 = confirm('You are about to Publish entered marks, be sure before Publishing. \n\nClick OK to Continue....');
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
    <script type="text/javascript">
        $(document).ready(function () {

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

                $("#ctl00_ContentPlaceHolder1_gvStudent .form-control").keypress(function (e) {
                    if (((e.which != 46 || (e.which == 46 && $(this).val() == '')) || $(this).val().indexOf('.') != -1) && (e.which < 48 || e.which > 57)) {
                        e.preventDefault();
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
                    var MaxMarks = parseFloat($('input[id$=hfdMaxMark]').val().trim());
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
                });

            });

        });

    </script>

    <script>
        window.onscroll = function () { scrollFunction() };
        function scrollFunction() {
            if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
                document.getElementById("myBtnPageScrollUp").style.display = "block";
            } else {
                document.getElementById("myBtnPageScrollUp").style.display = "none";
            }
        }
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
    <script>
        function validateField() {
            debugger;
            var summary = "";
            summary += isvalidExam();
            summary += isvalidSubExam();

            if (summary != "") {
                alert(summary);
                return false;
            }
            else {
                return true;
            }
        }
        function isvalidSubExam() {
            debugger;
            var uid;
            var temp = document.getElementById("<%=ddlSubExam.ClientID %>");
            uid = temp.value;
            if (uid == 0) {
                return ("Please Select SubExam" + "\n");
            }
            else {
                return "";
            }
        }
    </script>
</asp:Content>
