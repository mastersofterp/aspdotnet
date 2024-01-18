<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="MarkEntryforIA_Admin_CPU.aspx.cs" Inherits="ACADEMIC_EXAMINATION_MarkEntryforIA_Admin_CPU" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpanle1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                       <%-- this.Page.Form.Enctype = "multipart/form-data";--%>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updpanle1" runat="server">
        <ContentTemplate>
             </ContentTemplate>
         <Triggers>
                                            <asp:PostBackTrigger ControlID="btnBlankDownld" />
                                            <asp:PostBackTrigger ControlID="btnUpload" />
                                            <asp:PostBackTrigger ControlID="btnCancel1" />
                                          <%--  <asp:AsyncPostBackTrigger ControlID="btnUpload" />--%>
                                            <%--<asp:AsyncPostBackTrigger ControlID="FuBrowse" />--%>
                                        </Triggers>
    </asp:UpdatePanel>
            <div class="row" id="myDiv">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Exam Mark Entry</h3>
                             <%--<asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>--%>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlSelection" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                   <sup></sup>
                                                <label>Scheme</label>
                                            </div>
                                            <asp:DropDownList ID="ddlscheme" runat="server" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                        </div>--%>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                  <sup>* </sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                   <sup></sup>
                                                <label>Scheme</label>
                                            </div>
                                            <asp:DropDownList ID="ddlscheme" runat="server" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                  <sup>* </sup>
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
                                        <%--ADDED BY GAURAV SUBJECT FILTER NON MANDETORY--%>
                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <%-- <label>Course</label>--%>
                                                <label>Course</label>
                                            </div>
                                            <asp:DropDownList ID="ddlcourse" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlcourse_SelectedIndexChanged"
                                                TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                    </div>



                                    </div>
                                </div>

                                <div class="col-md-12" id="TRCourses" runat="server">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvCourse" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Course Name
                                                            </th>
                                                            <asp:HiddenField ID="hdfSemNo" runat="server" />
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

                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblStatus" Visible="false" runat="server" Style="color: red;"></asp:Label>
                                    </div>

                                    <div class="col-12">
                                        <div runat="server" id="Div_ExamNameList" visible="false">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>SR.
                                                        </th>
                                                        <th>SUBJECT NAME
                                                        </th>
                                                        <th>SEM
                                                        </th>
                                                        <th>SEC
                                                        </th>
                                                        <th>EXAM NAME
                                                        </th>
                                                        <th>STATUS
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
                                                                    <%--<asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("COURSENAME") %>' CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("BATCHNO")+ "+" + Eval("EXAMNO")+"+"+Eval("FLDNAME")+"+"+Eval("EXAMNAME") %>' OnClick="lnkbtnCourse_Click" ToolTip='<%# Eval("COURSENO")%>' />--%>
                                                                    <asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("COURSENAME") %>' CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("SECTIONNO")+ "+" + Eval("EXAMNO")+"+"+Eval("FLDNAME")+"+"+Eval("EXAMNAME") %>' OnClick="lnkbtnCourse_Click" ToolTip='<%# Eval("COURSENO")%>' />
                                                                    <asp:HiddenField ID="hdnfld_courseno" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                                    <asp:HiddenField ID="hdnsem" runat="server" Value='<%# Eval("semesterno")%>' />
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
                            </asp:Panel>

                            <asp:Panel ID="pnlMarkEntry" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12 col-md-9 pl-0 pr-0">
                                            <div class="row">
                                                <div id="Div1" class="form-group col-lg-4 col-md-6 col-12" runat="server">
                                                    <div class="label-dynamic">
                                                        <label>Session</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession2" runat="server" AppendDataBoundItems="true" CssClass="form-control" Font-Bold="true">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-8 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Course Name</label>
                                                    </div>
                                                    <asp:Label ID="lblCourse" Style="font-size: 12px;" runat="server" Font-Bold="true" CssClass="label label-default"></asp:Label>
                                                    <asp:HiddenField ID="hdfSection" runat="server" />
                                                    <asp:HiddenField ID="hdfBatch" runat="server" />
                                                </div>

                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Exam</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged"
                                                        ValidationGroup="show">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    
                                                    <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExam"
                                                        Display="None" ErrorMessage="Please Select Exam" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <asp:Label ID="lblSubExamName" runat="server" Text="Sub Exam"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubExam" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSubExam_SelectedIndexChanged"
                                                        ValidationGroup="show" Enabled="false">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="rfvSubExam" runat="server" ControlToValidate="ddlSubExam"
                                                        Display="None" ErrorMessage="Please Select Sub Exam" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <asp:Label ID="lblsort" runat="server" Text="Sort By"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSort" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                        ValidationGroup="show">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">PRN No</asp:ListItem>
                                                        <asp:ListItem Value="2">Roll No</asp:ListItem>
                                                        <asp:ListItem Value="3">Student Name</asp:ListItem>
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="rfvSort" runat="server" ControlToValidate="ddlSort"
                                                        Display="None" ErrorMessage="Please Select Sort" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Font-Bold="True" OnClick="btnShow_Click"
                                                    Text="Show" ValidationGroup="show" />

                                                <asp:Button ID="btnSave" runat="server" Visible="false" OnClientClick="this.disabled=true;" UseSubmitBehavior="false"
                                                    OnClick="btnSave_Click" Text="Save" CssClass="btn btn-primary" ValidationGroup="val" />

                                                <asp:Button ID="btnLock" runat="server" Visible="false" Font-Bold="true"
                                                    OnClick="btnLock_Click" OnClientClick="return showLockConfirm(this,'val');" Text="Lock"
                                                    CssClass="btn btn-primary" />
                                                

                                                <asp:Button ID="btnMIDReport" runat="server" Text="MID Report" CssClass="btn btn-info"
                                                    OnClick="btnTAReport_Click" Visible="False" />

                                                <asp:Button ID="btnConsolidateReport" runat="server" Visible="False"
                                                    Text="Consolidate Report" CssClass="btn btn-info" OnClick="btnConsolidateReport_Click" />

                                                <asp:Button ID="btnPrintReport" runat="server" OnClick="btnPrintReport_Click"
                                                    Text="Print" CssClass="btn btn-info" Visible="False" />

                                                <asp:LinkButton ID="lnkExcekImport" runat="server" Visible="false" OnClick="lnkExcekImport_Click" Text="Import Mark Entry Excel" onClientClick="validateField();" CssClass="btn btn-info"></asp:LinkButton>
                                                <%--Added on 03082022 --%>
                                                <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn btn-warning" />
                                                <asp:Button ID="btnCancel2" runat="server" OnClick="btnCancel2_Click"
                                                    Text="Cancel" CssClass="btn btn-warning" Visible="False" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="show"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                <%--</%--center--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-12">
                                            <div class=" note-div">
                                                <asp:Repeater ID="rptMarkCodes" runat="server">
                                                    <HeaderTemplate>

                                                        <h5 class="heading">Note </h5>
                                                        <p style="color: #ff0000;">Please Save and Lock for Final Submission of Marks</p>

                                                        <div class="panel-body" style="background-color: whitesmoke">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span><strong><%# Eval("CODE_VALUE")%></strong> - <%# Eval("CODE_DESC")%></span></p>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" visible="false" id="pnlUP" runat="server">
                                    <%--<asp:UpdatePanel ID="updImport" runat="server" UpdateMode="Always">
                                        <ContentTemplate>--%>
                                            <div class="sub-heading">
                                                <h5>Import Mark Entry Excel</h5>
                                            </div>

                                            <asp:Panel ID="Panel3" runat="server">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Excel File</label>
                                                    </div>
                                                  <%--  <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="uplFileUpload">
                                                    <ContentTemplate>
                                                    <asp:FileUpload ID="FuBrowse" runat="server" ToolTip="Select file to Import" TabIndex="14" />
                                                    </ContentTemplate>  
                                                         
                                                                                                    
                                                    </asp:UpdatePanel>--%>

                                             <%--    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="uplFileUpload">
                                                    <ContentTemplate>--%>
                                                    <asp:FileUpload ID="FuBrowse" runat="server" EnableViewState="true" ToolTip="Select file to Import" TabIndex="14" />
                                                  <%--  </ContentTemplate>                                                       
                                                    </asp:UpdatePanel>--%>
                                                </div>

                                                <div class="form-group">
                                                    <asp:LinkButton ID="btnBlankDownld" runat="server" OnClick="btnBlankDownld_Click" TabIndex="17" CssClass="btn btn-primary" ValidationGroup="show"
                                                        Text="Blank" ToolTip="Click to download blank excel format file" onClientClick="validateField();" Enabled="true"> Download Blank Excel Sheet For Mark Entry</asp:LinkButton>
                                                    <%--    <asp:LinkButton ID="btnUpload" runat="server" ValidationGroup="upload" OnClick="btnUpload_Click" CssClass="btn btn-primary margin"
                                                                    TabIndex="16" Text="Upload" ToolTip="Click to Upload" Enabled="true" Width="200px"><i class="fa fa-upload"> Upload Excel</i></asp:LinkButton>--%>
                                                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" OnClick="btnUpload_Click" 
                                                        TabIndex="16" Text="Upload Excel" ToolTip="Click to Upload" Enabled="true"></asp:Button>
                                                    <asp:Button ID="btnCancel1" runat="server" TabIndex="18" Text="Clear" ToolTip="Click To Clear" CssClass="btn btn-warning" OnClick="btnCancel1_Click" />
                                                </div>
                                               <%-- OnClientClick="return ValidateUpload();"--%>

                                            </asp:Panel>
                                            </fieldset>
                                     <%--   </ContentTemplate>
                                       
                                    </asp:UpdatePanel>--%>

                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblStudents" runat="server" Font-Bold="true"></asp:Label>
                                </div>


                                <div class="col-12" id="tdStudent" runat="server">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:Panel ID="pnlStudGrid" runat="server" Visible="false">
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Enter Marks for following Students</h5>
                                                </div>
                                                <asp:HiddenField ID="hfdMaxMark" runat="server" />
                                                <asp:HiddenField ID="hfdMinMark" runat="server" />

                                                <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered nowrap" HeaderStyle-CssClass="GridHeader" OnRowDataBound="gvStudent_RowDataBound">
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
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>


            <div id="divMsg" runat="server">
            </div>
            <asp:HiddenField ID="hdfDegree" runat="server" Value="" />

            <%--<button onclick="topFunction()" id="myBtnPageScrollUp" title="Go to top">Top</button>--%>
            <button id="myBtnPageScrollUp" title="Go to top"><i class="fa fa-arrow-up" aria-hidden="true"></i></button>
       

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
    
 <%--   <script>
        function ValidateUpload() {
            var fileUpload = document.getElementById("<%=FuBrowse.ClientID %>").value;

            if (fileUpload != '') {
                var valid_extensions = /(.xls|.xlsx)$/i;
                if (valid_extensions.test(fileUpload)) {
                    return true;
                }
                else {
                    alert('Only supports .xls or .xlsx files !');
                    return false;
                }
            }
            else {
                alert('Select file to upload !');
                return false;
            }
        }
    </script>--%>
    

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

          function isvalidExam() {
              debugger;
              var uid;
              var temp = document.getElementById("<%=ddlExam.ClientID %>");
            uid = temp.value;
            if (uid == 0) {
                return ("Please Select Exam" + "\n");
            }
            else {
                return "";
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
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
    .btn-primary {}
</style>
</asp:Content>

