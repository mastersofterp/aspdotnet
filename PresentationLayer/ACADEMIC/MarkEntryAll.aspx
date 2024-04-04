<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="MarkEntryAll.aspx.cs" Inherits="Academic_MarkEntryAll" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <div id="divMsg" runat="server">
            </div>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Mark Entry <small class="text-capitalize">(Selection Criteria for Marks Entry)</small> </h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlSelection" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="true"
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trdegree" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="True" ValidationGroup="show" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trbranch" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Branch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="True" ValidationGroup="show" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                                Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trscheme" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Scheme</label>
                                            </div>
                                            <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="show" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                                Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trSemester" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                                AutoPostBack="True" ValidationGroup="show" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trExam" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Exam</label>
                                            </div>
                                            <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="show" AutoPostBack="true" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExam"
                                                Display="None" ErrorMessage="Please Select Exam" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                              
                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvCourse" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Course List</h5>
                                                </div>
                                                <table class="table table-hover table-stripped" style="width: 100%" id="divsessionlist">
                                                   <%-- class="table table-striped table-bordered nowrap display" --%>
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                        <tr>
                                                            <th>Course Name
                                                            </th>
                                                             <th>
                                                                Print
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
                                                        <asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("COURSENAME") %>'
                                                            CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("BATCHNO") + "+" + Eval("SEMESTERNO") %>' ToolTip='<%# Eval("COURSENO")%>'
                                                            OnClick="lnkbtnCourse_Click" />
                                                    </td>
                                                       <td>
                                                                        <%-- <center>--%>
                                                                        <%--<asp:LinkButton ID="lbtnPrint" runat="server" CssClass="btn btn-default" OnClick="lbtnPrint_Click" CommandArgument='<%# Eval("COURSENO")+","+Eval("COURSENAME")+","+Eval("semesterno")+","+Eval("SECTIONNO") %>'><i class="fa fa-print" aria-hidden="true"></i></asp:LinkButton>--%>
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-default" OnClick="lbtnPrint_Click" CommandArgument='<%# Eval("COURSENO")+","+Eval("SECTIONNO")+","+Eval("semesterno")+","+Eval("COURSENAME") %>'><i class="fa fa-print" aria-hidden="true"></i></asp:LinkButton>
                                                                      
                                                             <%--</center>--%>
                                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="altitem">
                                                    <td>
                                                        <asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("COURSENAME") %>'
                                                            CommandArgument='<%# Eval("SECTIONNO") + "+" + Eval("BATCHNO") + "+" + Eval("SEMESTERNO")%>' ToolTip='<%# Eval("COURSENO")%>'
                                                            OnClick="lnkbtnCourse_Click" />

                                                    </td>
                                                       <td>
                                                                        <%-- <center>--%>

                                                                        <%--<asp:LinkButton ID="lbtnPrint" runat="server" CssClass="btn btn-default" OnClick="lbtnPrint_Click" CommandArgument='<%# Eval("COURSENO")+","+Eval("COURSENAME")+","+Eval("semesterno")+","+Eval("SECTIONNO") %>'><i class="fa fa-print" aria-hidden="true"></i></asp:LinkButton>--%>
                                                                        <asp:LinkButton ID="lbtnPrint" runat="server" CssClass="btn btn-default" OnClick="lbtnPrint_Click" CommandArgument='<%# Eval("COURSENO")+","+Eval("SECTIONNO")+","+Eval("semesterno")+","+Eval("COURSENAME") %>'><i class="fa fa-print" aria-hidden="true"></i></asp:LinkButton>
                                                           
                                                                                                                                  <%--</center>--%>
                                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlMarkEntry" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trSessionName" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession2" runat="server" AppendDataBoundItems="true" Font-Bold="true">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trCourseName" visible="false">
                                            <div class="label-dynamic">
                                                <label>Course Name</label>
                                            </div>
                                          <%--  <asp:Label ID="lblCourse" runat="server" Font-Bold="true" />--%>
                                            <asp:HiddenField ID="hdfSection" runat="server" />
                                            <asp:HiddenField ID="hdfBatch" runat="server" />
                                            <asp:HiddenField ID="hdfSchemeNo" runat="server" />
                                            <asp:HiddenField ID="hdfExamType" runat="server" />
                                           
                                            <asp:HiddenField ID="hdfSemester" runat="server" />
                                            
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Session :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSession" runat="server" Font-Bold="true" />
                                                    </a>
                                                </li>

                                                <li class="list-group-item"><b>Course :</b>
                                                    <a class="sub-label">
                                                          <asp:Label ID="lblCourse" runat="server" Font-Bold="true" />
                                                         <asp:HiddenField ID="hdfRule" runat="server" />
                                                        <asp:HiddenField ID="hdfCourseTotal" runat="server" />   
                                                        <asp:HiddenField ID="hdfMinPassMark" runat="server" />
                                                        <asp:HiddenField ID="hdfMaxCourseMarks" runat="server" />
                                                        <asp:HiddenField ID="hdfMaxCourseMarks_I" runat="server" />
                                                        <asp:HiddenField ID="hdfMinPassMark_I" runat="server" />
                                                        <asp:HiddenField ID="hdfSubid" runat="server" />
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="form-group col-lg-6 col-md-12 col-12">
                                            <div class=" note-div">
                                                <h5 class="heading">Note </h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span><b>902</b> for Absent</span></p>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span><b>903</b> for Malpractice</span></p>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span><b>905</b> for I Grade</span></p>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Please Save and Lock for Final Submission of Marks</span></p>
                                            </div>
                                        </div>

                                       <%--  <fieldset class="fieldset" style="padding: 5px; color: Green">
                                        <p class="text-green text-center">
                                            <strong>Please Enter : 902 for Absent | 903 for UFM | 904 for Detention | 905 for Incomplete </strong>
                                        </p>
                                        <p class="text-red text-center">
                                            <strong><i class="fa fa-pencil"></i>NOTE:</strong>

                                            <strong>Please Save and Lock for Final Submission of Marks and Final Grade Allotment.</strong>
                                        </p>
                                    </fieldset>--%>

                                        

                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnBack" runat="server" Font-Bold="true" OnClick="btnBack_Click"
                                        Text="BACK" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnSave" runat="server" Enabled="false" Font-Bold="true"
                                        OnClick="btnSave_Click" Text="SAVE" CssClass="btn btn-primary d-none" />                                  
                                    <asp:Button ID="btnLastSave" runat="server" Font-Bold="true"
                                        OnClick="btnLastSave_Click" Text="SAVE" CssClass="btn btn-success" /> <%--Text="LAST SAVE" --%>
                                      <asp:Button ID="btnLock" runat="server" Enabled="false" Font-Bold="true"
                                        OnClick="btnLock_Click" OnClientClick="return showLockConfirm();" Text="LOCK"
                                        CssClass="btn btn-danger" />
                                    <asp:Button ID="btnExcelReport" runat="server" Font-Bold="true"
                                        Text="EXCEL REPORT" CssClass="btn btn-info " OnClick="btnExcelReport_Click" />

                                    <asp:Button ID="btnReport" runat="server" Font-Bold="true" Text="Format-I" CssClass="btn btn-info"
                                        OnClick="btnReport_Click" />
                                    <asp:Button ID="btnGraphReport" runat="server" Font-Bold="true"
                                        Text="SHOW GRAPH" CssClass="btn btn-info d-none" OnClick="btnGraphReport_Click" />
                                    <asp:Button ID="btnPrint" runat="server" Font-Bold="true" Text="PRINT" CssClass="btn btn-info d-none"
                                        OnClientClick="return PrintPanel();" />

                                    <asp:Button ID="btnCancel2" runat="server" Font-Bold="true" OnClick="btnCancel2_Click"
                                        Text="Cancel" CssClass="btn btn-warning" Visible="False" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlStudGrid" runat="server" Visible="false">
                                        <div id="demo-grid">
                                            <div class="panel panel-primary">
                                                <div class="sub-heading"><h5>Enter Marks for following Students</h5></div>

                                                <div class="panel-body padd-0">
                                                    <div class="table-responsive">
                                                         <asp:HiddenField ID="hfdMaxMark" runat="server" />
                                            <asp:HiddenField ID="hfdMinMark" runat="server" />
                                                        <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            class="table table-bordered table-hover table-fixed">
                                                            <HeaderStyle />
                                                            <AlternatingRowStyle />
                                                            <Columns>

                                                                 <asp:TemplateField HeaderText="SR. NO."
                                                                    ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Bind("IDNO") %>'
                                                                            Font-Size="9pt" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="9%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="RRN"
                                                                    ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("REGNO") %>' ToolTip='<%# Bind("IDNO") %>'
                                                                            Font-Size="9pt" />
                                                                        <%--<asp:HiddenField ID="hdfIDNO" runat="server" Value='<%# Bind("IDNO") %>' />--%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="9%" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="STUDNAME" HeaderText="NAME" ItemStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="45%">
                                                                    <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                                </asp:BoundField>


                                                                <%--EXAM MARK ENTRY--%>
                                                                <%--<asp:TemplateField HeaderText="Minor-1" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtT1Marks" runat="server" Text='<%# Bind("T1MARK") %>' Width="50px"
                                                                                                Font-Bold="true" Style="text-align: center" />
                                                                                            <asp:Label ID="lblT1Marks" runat="server" Text='<%# Bind("T1MAX") %>' ToolTip='<%# Bind("LOCKT1") %>'
                                                                                                Visible="false" />
                                                                                            <asp:Label ID="lblT1MinMarks" runat="server" Text="0" Visible="false" />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle Width="6%" />
                                                                                    </asp:TemplateField>--%>

                                                                <asp:TemplateField HeaderText="INTERNAL" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>

                                                                        <asp:TextBox ID="txtTAMarks" runat="server" Text='<%# Bind("INTERMARK") %>'
                                                                            Font-Bold="true" Style="text-align: center" Enabled="false"/>
                                                                        <%--<asp:Label ID="lblTAMarks" runat="server" Text='<%# Bind("INTERMARK") %>' ToolTip='<%# Bind("LOCKS2") %>'
                                                                            Visible="true" />
                                                                        <asp:Label ID="lblTAMinMarks" runat="server" Text='<%# Bind("S2MIN") %>' Visible="false" />--%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="10%" />
                                                                </asp:TemplateField>

                                                                <%--<asp:TemplateField HeaderText="Minor-2" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtT2Marks" runat="server" Text='<%# Bind("T2MARK") %>' Width="50px"
                                                                                                Font-Bold="true" Style="text-align: center" />
                                                                                            <asp:Label ID="lblT2Marks" runat="server" Text='<%# Bind("T2MAX") %>' ToolTip='<%# Bind("LOCKT2") %>'
                                                                                                Visible="false" />
                                                                                            <asp:Label ID="lblT2MinMarks" runat="server" Text="0" Visible="false" />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle Width="6%" />
                                                                                    </asp:TemplateField>--%>

                                                              <%--  <asp:TemplateField HeaderText="MINOR" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtTotMarks" runat="server" Text='<%# Bind("S1MARK") %>'
                                                                            Font-Bold="true" Style="text-align: center" />
                                                                        <asp:Label ID="lblTotMarks" runat="server" Text='<%# Bind("S1MAX") %>' ToolTip='<%# Bind("LOCKS1") %>'
                                                                            Visible="false" />
                                                                        <asp:Label ID="lblTotMinMarks" runat="server" Text='<%# Bind("S1MIN") %>' Visible="false" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="10%" />
                                                                </asp:TemplateField>--%>

                                                               <%-- <asp:TemplateField HeaderText="TH" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtESMarks" runat="server" Text='<%# Bind("EXTERMARK") %>'
                                                                            Font-Bold="true" Style="text-align: center" />
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtESMarks" runat="server" FilterType="Custom"
                                                                            ValidChars="0123456789." TargetControlID="txtESMarks">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                        <asp:Label ID="lblESMarks" runat="server" Text='<%# Bind("MAXMARKS_E") %>' ToolTip='<%# Bind("LOCKE") %>'
                                                                            Visible="false" />
                                                                        <asp:Label ID="lblESMinMarks" runat="server" Text='<%# Bind("MINMARKS") %>' Visible="false" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="10%" />
                                                                </asp:TemplateField>--%>

                                                                <asp:TemplateField HeaderText="TH" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>

                                                            <asp:TextBox ID="txtESMarks" runat="server" Text='<%# Bind("EXTERMARK") %>' Width="80px" 
                                                                            MaxLength="5" Font-Bold="true"  Style="text-align: center;box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);"  />
                                                            <%--onkeyup="return CheckMark(this);"--%>
                                                                      <%--  <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtESMarks" runat="server" FilterType="Custom"
                                                                            ValidChars="0123456789." TargetControlID="txtESMarks">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>--%>
                                                                        <asp:Label ID="lblESMarks" runat="server" Text='<%# Bind("MAXMARKS_E") %>' ToolTip='<%# Bind("LOCKE") %>'
                                                                            Visible="false" />
                                                                        <asp:Label ID="lblESMinMarks" runat="server" Text='<%# Bind("MINMARKS") %>' Visible="false" />
                                                              <asp:HiddenField ID="hdfGreaterVal" runat="server" Value='<%# Bind("MAXMARKS_E") %>' />
                                                            <asp:HiddenField ID="hdfConversion" runat="server" />

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                       <%--<asp:TemplateField HeaderText="TH" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMarks" runat="server" Text='<%# Bind("EXTERMARK") %>' Width="80px"
                                                              
                                                                MaxLength="5" Font-Bold="true" Style="text-align: center; box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);" onkeyup="return CheckMark(this);" OnTextChanged="txtMarks_TextChanged" />
                                                            <asp:Label ID="lblMarks" runat="server" Text='<%# Bind("MAXMARKS_E") %>' ToolTip='<%# Bind("LOCKE") %>'
                                                                Visible="false" />
                                                            <asp:Label ID="lblMinMarks" runat="server" Text='<%# Bind("MINMARKS") %>' Visible="false" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789." TargetControlID="txtMarks"></ajaxToolKit:FilteredTextBoxExtender>                                                           
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle />
                                                    </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="ESEM-PR" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtESPRMarks" runat="server" Text='<%# Bind("S4MARK") %>'
                                                                            Font-Bold="true" Style="text-align: center" />
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtESPRMarks" runat="server" FilterType="Custom"
                                                                            ValidChars="0123456789." TargetControlID="txtESPRMarks">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                        <asp:Label ID="lblESPRMarks" runat="server" Text='<%# Bind("S4MAX") %>' ToolTip='<%# Bind("LOCKS4") %>'
                                                                            Visible="false" />
                                                                        <asp:Label ID="lblESPRMinMarks" runat="server" Text='<%# Bind("S4MIN") %>' Visible="false" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="6%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TOTAL MARKS (100 Marks)" Visible="true" ItemStyle-Width="6%" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtTotMarksAll" runat="server" Text='<%# Bind("MARKTOT") %>' Enabled="false"
                                                                            Font-Bold="true" Style="text-align: center" />
                                                                        <asp:HiddenField ID="hidTotMarksAll" runat="server" Value='<%# Bind("MARKTOT") %>' />

                                                                    </ItemTemplate>

                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="6%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TOTAL %" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtTotPer" runat="server" Text='<%# Bind("SCALEDN_PERCENT") %>' Enabled="false"
                                                                            Font-Bold="true" Style="text-align: center" />
                                                                        <asp:HiddenField ID="hidTotPer" runat="server" Value='<%# Bind("SCALEDN_PERCENT") %>' />
                                                                         

                                                                    </ItemTemplate>

                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="6%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="GRADE" Visible="true" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtGrade" runat="server" Text='<%# Bind("GRADE") %>' Enabled="false"
                                                                            Font-Bold="true" Style="text-align: center" />
                                                                        <asp:HiddenField ID="hidGrade" runat="server" Value='<%# Bind("GRADE") %>' />
                                                                          <asp:HiddenField ID="hidGradePoint" runat="server" />
                                                                    </ItemTemplate>

                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="6%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="GD POINT" Visible="false" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtGradePoint" runat="server" Text='<%# Bind("GDPOINT") %>' Enabled="false"
                                                                            Font-Bold="true" Style="text-align: center" />
                                                                      
                                                                    </ItemTemplate>

                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="1%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-lg-12 col-md-12 col-12">
                                            <asp:ListView ID="lvGrades" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="lvGrades" runat="server">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th align="center">Grade
                                                                </th>
                                                                <th align="center">Max
                                                                </th>
                                                                <th align="center">Min
                                                                </th>
                                                                <th align="center">Point
                                                                </th>
                                                                <th align="center">Total
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td align="center">
                                                            <asp:TextBox ID="txtGrades" runat="server" Text='<%# Bind("GRADE") %>' Font-Bold="true" Enabled="false"
                                                                CssClass="form-control" Style="text-align: center" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtMax" runat="server" Text='<%# Bind("MAXMARK") %>' CssClass="form-control" MaxLength="5"
                                                                Style="text-align: center" onkeyUP="changeMaxMarksRange(this);" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtMin" runat="server" Text='<%# Bind("MINMARK") %>' CssClass="form-control"  MaxLength="5"
                                                                Style="text-align: center" onkeyUP="changeMinMarksRange(this);" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtGradePoints" runat="server" Text='<%# Bind("GRADEPOINT") %>' Font-Bold="true" 
                                                                Enabled="false" CssClass="form-control" Style="text-align: center" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalStudent" runat="server" Text='<%# Bind("TOTAL_STU") %>' Font-Bold="true"
                                                                Enabled="false" CssClass="form-control" Style="text-align: center" />
                                                            <asp:HiddenField ID="hidTotalStudent" runat="server" Value='<%# Bind("TOTAL_STU") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                <div class="col-12 mt-3 d-none">
                                    <div class="row">
                                        <div class="col-lg-4 col-md-6 col-12 d-none">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Scheme :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblScheme" runat="server" Font-Bold="true" />
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12 d-none">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Course :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblCourses" runat="server" Font-Bold="true" />
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Order By</label>
                                            </div>
                                            <asp:RadioButton ID="rdoRollno" runat="server" GroupName="Status"
                                                Text="ROLL NO" Font-Bold="true"
                                                OnCheckedChanged="rdoRollno_CheckedChanged" AutoPostBack="true" Checked="true" />
                                            <asp:RadioButton ID="rdoTotalmarks" runat="server" GroupName="Status" Text="TOTAL MARKS"
                                                Font-Bold="true" OnCheckedChanged="rdoTotalmarks_CheckedChanged" AutoPostBack="true" />
                                            <asp:RadioButton ID="rdoSection" runat="server" GroupName="Status" Text="SECTION & ROLL NO"
                                                Font-Bold="true" OnCheckedChanged="rdoSection_CheckedChanged" AutoPostBack="true" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trTitle" visible="false">
                                            <div class="label-dynamic">
                                                <label>Title</label>
                                            </div>
                                            <asp:TextBox runat="server" MaxLength="60" ID="txtTitle"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTitle" runat="server" FilterType="Custom"
                                                InvalidChars="!@#$%^&*()_+-=\|';,./:? /<>" TargetControlID="txtTitle">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-12 col-md-12 col-12" runat="server" id="trlstudent" visible="false">
                                            <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                                        </div>

                                        <div class="col-12 d-none">
                                            <div class="row">
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <table class="table table-bordered table-hover table-striped">
                                                        <tr>
                                                            <td>TOTAL STUDENTS 
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalAllStudent" runat="server" Text='' Font-Bold="true"
                                                                    Enabled="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>MARKS TOTAL 
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMarksTotal" runat="server" Text='' Font-Bold="true"
                                                                    Enabled="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>AVERAGE 
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAverage" runat="server" Text='' Font-Bold="true"
                                                                    Enabled="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>STD DEV AVG 
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSda" runat="server" Text='0' Font-Bold="true"
                                                                    Enabled="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>STD DEV 
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSd" runat="server" Text='0' Font-Bold="true"
                                                                    Enabled="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>SIGMA 
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSigma" runat="server" Text='0' Font-Bold="true"
                                                                    Enabled="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelReport" />
        </Triggers>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">

        function ControlGrade(txt) {
            var id = 0;
            id = document.getElementById('txtMax').value;
            alert(id);
        }

        //This Method for Theory Marks.  
        //function validateMarkTH(metxt, maxmrk, minmark, txttot, txtmid, txttotall, totper, txtgrade, txtgradepoint, hidTotMarks, hidTotPer, hidGrade, hidGradePoint, Scale, totalmarks)
        function validateMarkTH(metxt, maxmrk, minmark, txttot, txtmid, txttotall, totper, txtgrade, txtgradepoint, hidTotMarks, hidTotPer, hidGrade, hidGradePoint, Scale, totalmarks, temp1, temp2, temp3, temp4) {
            //alert('Hi');

            if (temp4 == '0') {
                validateMark1(temp1, temp1, temp1, temp1, temp2, temp3, '', '1', '0')
            }
            if (metxt.value != '') {
                if (metxt.value != '' & (Number(metxt.value) > maxmrk)) {
                    var totfail = 0;
                    var totPfail = 0;
                    var totP1fail = 0;
                    var totalmmarks = 0;
                    var totalmid = 0;

                    totalmid = txtmid.value;
                    totalmmarks = txttot.value;

                    if (Number(metxt.value) == 401 || Number(metxt.value) == 402 || Number(metxt.value) == 403) {
                        if (Number(metxt.value) == 401 || Number(metxt.value) == 402 || Number(metxt.value) == 403) {
                            if (Number(txttot.value) == 401 || Number(txttot.value) == 402 || Number(txttot.value) == 403) {
                                if (Number(txtmid.value) == 401 || Number(txtmid.value) == 402 || Number(txtmid.value) == 403) {
                                    totfail = 0;
                                    totPfail = 0.00;
                                    txttotall.value = totfail;
                                    hidTotMarks.value = totfail;
                                    totper.value = totPfail;
                                    hidTotPer.value = totPfail;
                                    var dataRows32 = null;

                                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                        dataRows32 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                    }

                                    if (dataRows32.length == "10") {
                                        if (Number(metxt.value) == 401) {
                                            txtgrade.value = 'F';
                                            hidGrade.value = 'F';
                                        }
                                        if (Number(metxt.value) == 402) {
                                            txtgrade.value = 'W';
                                            hidGrade.value = 'W';
                                        }
                                        if (Number(metxt.value) == 403) {
                                            txtgrade.value = 'I';
                                            hidGrade.value = 'I';
                                        }
                                        txtgradepoint.value = 0;
                                        hidGradePoint.value = 0;
                                        var dataRows44 = null;
                                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                            dataRows44 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                                        }

                                        for (i = 0; i < dataRows44.length - 1; i++) {
                                            MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                            MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                            GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                            Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                            document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                            document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                        }
                                        studTotal();
                                    }
                                    else {
                                        if (dataRows32.length == "11") {
                                            if (Number(metxt.value) == 401) {
                                                txtgrade.value = 'FF';
                                                hidGrade.value = 'FF';
                                            }
                                            if (Number(metxt.value) == 402) {
                                                txtgrade.value = 'FA';
                                                hidGrade.value = 'FA';
                                            }
                                            if (Number(metxt.value) == 403) {
                                                txtgrade.value = 'I';
                                                hidGrade.value = 'I';
                                            }
                                            txtgradepoint.value = 0;
                                            hidGradePoint.value = 0;
                                            var dataRows45 = null;
                                            if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                                dataRows45 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                            }

                                            for (i = 0; i < dataRows45.length - 1; i++) {
                                                MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                                MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                                GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                                Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                            }
                                            studTotal();
                                        }
                                    }
                                }
                                else {
                                    totfail = Math.round(parseFloat(totalmid / 2).toFixed(2));
                                    txttotall.value = totfail;
                                    hidTotMarks.value = totfail;
                                    if (Scale == '0') {
                                        totPfail = totalmarks * totfail / totalmarks;
                                        totPfail = parseFloat(totPfail).toFixed(2);

                                    }
                                    else {
                                        totPfail = Scale * totfail / totalmarks;
                                        totPfail = parseFloat(totPfail).toFixed(2);

                                    }
                                    totper.value = totPfail;
                                    hidTotPer.value = totPfail;
                                    var dataRows33 = null;

                                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                        dataRows33 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                    }

                                    if (dataRows33.length == "10") {
                                        if (Number(metxt.value) == 401) {
                                            txtgrade.value = 'F';
                                            hidGrade.value = 'F';
                                        }
                                        if (Number(metxt.value) == 402) {
                                            txtgrade.value = 'W';
                                            hidGrade.value = 'W';
                                        }
                                        if (Number(metxt.value) == 403) {
                                            txtgrade.value = 'I';
                                            hidGrade.value = 'I';
                                        }
                                        txtgradepoint.value = 0;
                                        hidGradePoint.value = 0;
                                        var dataRows46 = null;
                                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                            dataRows46 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                        }

                                        for (i = 0; i < dataRows46.length - 1; i++) {
                                            MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                            MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                            GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                            Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                            document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                            document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                        }
                                        studTotal();
                                    }
                                    else {
                                        if (dataRows33.length == "11") {
                                            if (Number(metxt.value) == 401) {
                                                txtgrade.value = 'FF';
                                                hidGrade.value = 'FF';
                                            }
                                            if (Number(metxt.value) == 402) {
                                                txtgrade.value = 'FA';
                                                hidGrade.value = 'FA';
                                            }
                                            if (Number(metxt.value) == 403) {
                                                txtgrade.value = 'I';
                                                hidGrade.value = 'I';
                                            }
                                            txtgradepoint.value = 0;
                                            hidGradePoint.value = 0;
                                            var dataRows47 = null;
                                            if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                                dataRows47 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                            }

                                            for (i = 0; i < dataRows47.length - 1; i++) {
                                                MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                                MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                                GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                                Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                            }
                                            studTotal();
                                        }
                                    }

                                }
                            }
                            if (Number(txtmid.value) == 401 || Number(txtmid.value) == 402 || Number(txtmid.value) == 403) {
                                if (Number(txttot.value) == 401 || Number(txttot.value) == 402 || Number(txttot.value) == 403) {
                                    totfail = 0;
                                    totPfail = 0.00;
                                    txttotall.value = totfail;
                                    hidTotMarks.value = totfail;
                                    totper.value = totPfail;
                                    hidTotPer.value = totPfail;
                                    var dataRows34 = null;
                                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                        dataRows34 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                                    }

                                    if (dataRows34.length == "10") {
                                        if (Number(metxt.value) == 401) {
                                            txtgrade.value = 'F';
                                            hidGrade.value = 'F';
                                        }
                                        if (Number(metxt.value) == 402) {
                                            txtgrade.value = 'W';
                                            hidGrade.value = 'W';
                                        }
                                        if (Number(metxt.value) == 403) {
                                            txtgrade.value = 'I';
                                            hidGrade.value = 'I';
                                        }
                                        txtgradepoint.value = 0;
                                        hidGradePoint.value = 0;
                                        var dataRows48 = null;
                                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                            dataRows48 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                        }

                                        for (i = 0; i < dataRows48.length - 1; i++) {
                                            MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                            MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                            GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                            Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                            document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                            document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                        }
                                        studTotal();
                                    }
                                    else {
                                        if (dataRows34.length == "11") {
                                            if (Number(metxt.value) == 401) {
                                                txtgrade.value = 'FF';
                                                hidGrade.value = 'FF';
                                            }
                                            if (Number(metxt.value) == 402) {
                                                txtgrade.value = 'FA';
                                                hidGrade.value = 'FA';
                                            }
                                            if (Number(metxt.value) == 403) {
                                                txtgrade.value = 'I';
                                                hidGrade.value = 'I';
                                            }
                                            txtgradepoint.value = 0;
                                            hidGradePoint.value = 0;
                                            studTotal();
                                        }
                                    }
                                }
                                else {
                                    totfail = Math.round(parseFloat(totalmmarks).toFixed(2));
                                    txttotall.value = totfail;
                                    hidTotMarks.value = totfail;
                                    if (Scale == '0') {
                                        totPfail = totalmarks * totfail / totalmarks;
                                        totPfail = parseFloat(totPfail).toFixed(2);

                                    }
                                    else {
                                        totPfail = Scale * totfail / totalmarks;
                                        totPfail = parseFloat(totPfail).toFixed(2);

                                    }
                                    totper.value = totPfail;
                                    hidTotPer.value = totPfail;
                                    var dataRows35 = null;
                                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                        dataRows35 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                                    }

                                    if (dataRows35.length == "10") {
                                        if (Number(metxt.value) == 401) {
                                            txtgrade.value = 'F';
                                            hidGrade.value = 'F';
                                        }
                                        if (Number(metxt.value) == 402) {
                                            txtgrade.value = 'W';
                                            hidGrade.value = 'W';
                                        }
                                        if (Number(metxt.value) == 403) {
                                            txtgrade.value = 'I';
                                            hidGrade.value = 'I';
                                        }
                                        txtgradepoint.value = 0;
                                        hidGradePoint.value = 0;
                                        var dataRows49 = null;
                                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                            dataRows49 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                        }

                                        for (i = 0; i < dataRows49.length - 1; i++) {
                                            MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                            MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                            GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                            Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                            document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                            document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                        }
                                        studTotal();
                                    }
                                    else {
                                        if (dataRows35.length == "11") {
                                            if (Number(metxt.value) == 401) {
                                                txtgrade.value = 'FF';
                                                hidGrade.value = 'FF';
                                            }
                                            if (Number(metxt.value) == 402) {
                                                txtgrade.value = 'FA';
                                                hidGrade.value = 'FA';
                                            }
                                            if (Number(metxt.value) == 403) {
                                                txtgrade.value = 'I';
                                                hidGrade.value = 'I';
                                            }
                                            txtgradepoint.value = 0;
                                            hidGradePoint.value = 0;
                                            var dataRows50 = null;
                                            if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                                dataRows50 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                            }

                                            for (i = 0; i < dataRows50.length - 1; i++) {
                                                MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                                MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                                GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                                Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                            }
                                            studTotal();
                                        }
                                    }
                                }
                            }
                            if (Number(txtmid.value) != 401 && Number(txttot.value) != 401) {
                                //mahesh
                                // alert(txtmi.value);
                                if (Number(txtmid.value) != 403 && Number(txttot.value) != 403) {
                                    if (Number(txtmid.value) != 402 && Number(txttot.value) != 402) {
                                        totfail = Math.round((parseFloat(totalmid / 2) + parseFloat(totalmmarks)).toFixed(2));
                                        txttotall.value = totfail;
                                        hidTotMarks.value = totfail;
                                        if (Scale == '0') {

                                            totPfail = totalmarks * totfail / totalmarks;
                                            totPfail = parseFloat(totPfail).toFixed(2);

                                        }
                                        else {
                                            totPfail = Scale * totfail / totalmarks;
                                            totPfail = parseFloat(totPfail).toFixed(2);

                                        }
                                        totper.value = totPfail;
                                        hidTotPer.value = totPfail;
                                        var dataRows36 = null;
                                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                            dataRows36 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                                        }

                                        if (dataRows36.length == "10") {
                                            if (Number(metxt.value) == 401) {
                                                txtgrade.value = 'F';
                                                hidGrade.value = 'F';
                                            }
                                            if (Number(metxt.value) == 402) {
                                                txtgrade.value = 'W';
                                                hidGrade.value = 'W';
                                            }
                                            if (Number(metxt.value) == 403) {
                                                txtgrade.value = 'I';
                                                hidGrade.value = 'I';
                                            }
                                            txtgradepoint.value = 0;
                                            hidGradePoint.value = 0;
                                            var dataRows51 = null;
                                            if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                                dataRows51 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                            }

                                            for (i = 0; i < dataRows51.length - 1; i++) {
                                                MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                                MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                                GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                                Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                            }
                                            studTotal();
                                        }
                                        else {
                                            if (dataRows36.length == "11") {

                                                if (Number(metxt.value) == 401) {
                                                    txtgrade.value = 'FF';
                                                    hidGrade.value = 'FF';
                                                }
                                                if (Number(metxt.value) == 402) {
                                                    txtgrade.value = 'FA';
                                                    hidGrade.value = 'FA';
                                                }
                                                if (Number(metxt.value) == 403) {
                                                    txtgrade.value = 'I';
                                                    hidGrade.value = 'I';
                                                }
                                                txtgradepoint.value = 0;
                                                hidGradePoint.value = 0;
                                                var dataRows60 = null;
                                                if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                                    dataRows60 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                                }

                                                for (i = 0; i < dataRows60.length - 1; i++) {
                                                    MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                                    MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                                    GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                                    Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                                }
                                                studTotal();
                                            }
                                        }

                                    }

                                }
                            }
                        }

                    }
                    else {
                        alert("Please Enter Marks in the Range of 0 to " + maxmrk.toString() + ' and Note : 401 for Absent and 402 for withheld and 403 for Copy case ');
                        metxt.value = '';
                    }
                    IsNumericTest(metxt);
                }
                else {

                    var me = 0;
                    var tot = 0;
                    var totP = 0;
                    var totP1 = 0;
                    var totalmmarks = 0;
                    var totalmid = 0;

                    totalmid = txtmid.value;
                    totalmmarks = txttot.value;
                    me = metxt.value;
                    Number(txttot.value) == 401
                    if (Number(txttot.value) != 401 && Number(txttot.value) != 402 && Number(txttot.value) != 403 && Number(txtmid.value) != 401 && Number(txtmid.value) != 402 && Number(txtmid.value) != 403) {
                        tot = Math.round((parseFloat(totalmmarks) + parseFloat(totalmid / 2) + parseFloat(me / 2)).toFixed(2));

                    }
                    else {
                        if (Number(txttot.value) == 401 || Number(txttot.value) == 402 || Number(txttot.value) == 403) {
                            if (Number(txtmid.value) == 401 || Number(txtmid.value) == 402 || Number(txtmid.value) == 403) {
                                tot = Math.round((parseFloat(me / 2)).toFixed(2));
                            }
                            else {
                                tot = Math.round((parseFloat(totalmid / 2) + parseFloat(me / 2)).toFixed(2));
                            }
                        }
                        else {
                            if (Number(txtmid.value) == 401 || Number(txtmid.value) == 402 || Number(txtmid.value) == 403) {
                                tot = Math.round((parseFloat(totalmmarks) + parseFloat(me / 2)).toFixed(2));
                            }
                            else {
                                tot = Math.round((parseFloat(totalmmarks) + parseFloat(totalmid / 2) + parseFloat(me / 2)).toFixed(2));
                            }
                        }
                    }
                    txttotall.value = tot;
                    hidTotMarks.value = tot;
                    if (Scale == '0') {
                        totP = totalmarks * tot / totalmarks;
                        totP = parseFloat(totP).toFixed(2);

                        totP1 = Math.round(totP);
                    }
                    else {
                        totP = Scale * tot / totalmarks;
                        totP = parseFloat(totP).toFixed(2);

                        totP1 = Math.round(totP);
                    }
                    totper.value = totP;
                    hidTotPer.value = totP;
                    var MaxAmt = 0;
                    var MinAmt = 0;
                    var dataRows = null;
                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                        dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                    }
                    for (i = 0; i < dataRows.length - 1; i++) {
                        MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                        MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                        GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                        Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                        if (totP1 >= MinAmt && totP1 <= MaxAmt) {
                            //                if(examType=='1')
                            //                {

                            txtgrade.value = GradeName;
                            hidGrade.value = GradeName;
                            txtgradepoint.value = Point;
                            hidGradePoint.value = Point;
                            //                }
                            //                else
                            //                {
                            //                if(schemeNo>='9')
                            //                {

                            //                txtgrade.value=GradeName;
                            //                hidGrade.value=GradeName;
                            //                txtgradepoint.value=Point;
                            //                hidGradePoint.value=Point;
                            //                }
                            //                else
                            //                {
                            //                if(GradeName=='FF')
                            //                {
                            //                
                            //                txtgrade.value=GradeName;
                            //                hidGrade.value=GradeName;
                            //                txtgradepoint.value=Point;
                            //                hidGradePoint.value=Point;
                            //                }
                            //                else
                            //                {
                            //        
                            //                txtgrade.value='DD';
                            //                hidGrade.value='DD';
                            //                txtgradepoint.value='4';
                            //                hidGradePoint.value='4';
                            //                }
                            //                }
                            //                }
                        }
                    }

                    studTotal();

                }

            }
            else {
                txttotall.value = '';
                totper.value = '';
                txtgrade.value = '';
                txtgradepoint.value = '';
                hidTotMarks.value = '';
                hidTotPer.value = '';
                hidGrade.value = '';
                hidGradePoint.value = '';
                var dataRows = null;
                if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                }
                for (i = 0; i < dataRows.length - 1; i++) {
                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                }
                studTotal()
            }

        }
        //This Method for Calculating Students which done mark entry 
        //function studTotal() {
        //    debugger
        //    var Grade1 = null;
        //    var Grade2 = null;
        //    var totStud = 0;
        //    var countstud = 0;
        //    var sumtotstud = 0;
        //    var sum = 0;

        //    var dataRows = null;
        //    if (document.getElementById('ctl00_ContentPlaceHolder1_gvStudent') != null) {
        //        dataRows = document.getElementById('ctl00_ContentPlaceHolder1_gvStudent').getElementsByTagName('tr');
        //    }

        //    if (dataRows == null)
        //    {
        //        return
        //    }

        //    for (j = 2; j <= dataRows.length; j++) {
        //        if (j < 10)//for less than 10 length of Students.. 
        //        {
        //            Grade1 = document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade');

        //            var dataRows1 = null;
        //            if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
        //                dataRows1 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');                        
        //            }
        //            for (k = 0; k < dataRows1.length - 1; k++) {
        //                Grade2 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtGrades');
        //                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value = 0;
        //                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_hidTotalStudent').value = 0;
        //                // Here Check GDPOINT of lvgrade & lvStudent 
        //                if (Grade1.value != null && Grade2.value != null) {
        //                    if (Grade1.value == Grade2.value) {


        //                        totStud = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value.trim());

        //                        countstud = totStud + 1;

        //                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value = countstud;
        //                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_hidTotalStudent').value = countstud;
        //                    }

        //                }
        //            }
        //        }
        //        else {
        //            //for graeter than 10 length of Students.. 
        //            Grade1 = document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl' + j + '_txtGrade');

        //            var dataRows2 = null;
        //            if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
        //                dataRows2 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

        //            }
        //            for (k = 0; k < dataRows2.length - 1; k++) {
        //                Grade2 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtGrades');
        //                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value = 0;
        //                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_hidTotalStudent').value = 0;
        //                if (Grade1.value != null && Grade2.value != null) {
        //                    if (Grade1.value == Grade2.value) {
        //                        totStud = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value.trim());

        //                        countstud = totStud + 1;

        //                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value = countstud;
        //                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_hidTotalStudent').value = countstud;
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    var dataRows3 = null;
        //    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
        //        dataRows3 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

        //    }

        //    for (k = 0; k < dataRows3.length - 1; k++) {
        //        sumtotstud = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value.trim());
        //        sum = sumtotstud + sum;
        //        document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAllStudent').value = sum;
        //    }

        //}
        //This Method is for Practical marks entry
        function validateMark(metxtPr, maxmrkPR, minmarkPr, totper, txtgrade, txtgradepoint, hidTotPer, hidGrade, hidGradePoint, Scale, totalmarks) {


            if (metxtPr.value != '') {
                if (metxtPr.value != '' & (Number(metxtPr.value) > maxmrkPR)) {
                    var totfail = 0;
                    var totPfail = 0;
                    var totP1fail = 0;
                    if (Number(metxtPr.value) == 401 || Number(metxtPr.value) == 402 || Number(metxtPr.value) == 403) {
                        totPfail = 0.00;

                        totper.value = totPfail;
                        hidTotPer.value = totPfail;

                        var dataRows40 = null;
                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                            dataRows40 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                        }
                        //Here Check Length of grades & put grade 'F' when length 8 & grade 'FF' when Length 9.
                        if (dataRows40.length == "10") {
                            if (Number(metxtPr.value) == 401) {
                                txtgrade.value = 'F';
                                hidGrade.value = 'F';
                            }
                            if (Number(metxtPr.value) == 402) {
                                txtgrade.value = 'W';
                                hidGrade.value = 'W';
                            }
                            if (Number(metxtPr.value) == 403) {
                                txtgrade.value = 'I';
                                hidGrade.value = 'I';
                            }

                            txtgradepoint.value = 0;
                            hidGradePoint.value = 0;
                            var dataRows41 = null;
                            if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                dataRows41 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                            }

                            for (i = 0; i < dataRows41.length - 1; i++) {
                                MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                            }

                            studTotal();
                        }
                        else {
                            if (dataRows40.length == "11") {
                                if (Number(metxtPr.value) == 401) {
                                    txtgrade.value = 'FF';
                                    hidGrade.value = 'FF';
                                }
                                if (Number(metxtPr.value) == 402) {
                                    txtgrade.value = 'FA';
                                    hidGrade.value = 'FA';
                                }
                                if (Number(metxtPr.value) == 403) {
                                    txtgrade.value = 'I';
                                    hidGrade.value = 'I';
                                }
                                txtgradepoint.value = 0;
                                hidGradePoint.value = 0;
                                var dataRows42 = null;
                                if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                    dataRows42 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                                }

                                for (i = 0; i < dataRows42.length - 1; i++) {
                                    MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                    MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                    GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                    Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                }

                                studTotal();
                            }
                        }
                    }
                    else {
                        alert("Please Enter Marks in the Range of 0 to " + maxmrkPR.toString() + ' and Note : 401 for Absent and 402 for Withheld and 403 for Copycase');
                        metxtPr.value = '';
                    }
                    IsNumericTest(metxtPr);
                }
                else {
                    var me = 0;
                    var totP = 0;
                    var totP1 = 0;
                    me = Math.round(parseFloat(metxtPr.value).toFixed(2));
                    //Here Check Scaling 
                    if (Scale == '0') {
                        totP = totalmarks * me / totalmarks;
                        totP = parseFloat(totP.toFixed(2));
                        totP1 = Math.round(totP);
                    }
                    else {
                        totP = Scale * me / totalmarks;
                        totP = parseFloat(totP).toFixed(2);
                        totP1 = Math.round(totP);
                    }

                    totper.value = totP;
                    hidTotPer.value = totP;
                    var MaxAmt = 0;
                    var MinAmt = 0;
                    var dataRows = null;
                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                        dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                    }

                    for (i = 0; i < dataRows.length - 1; i++) {
                        MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                        MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                        GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                        Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();

                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                        //Here Check range and allot grade . 
                        if (totP1 >= MinAmt && totP1 <= MaxAmt) {
                            //                if(examType=='1')
                            //                {

                            txtgrade.value = GradeName;
                            hidGrade.value = GradeName;
                            txtgradepoint.value = Point;
                            hidGradePoint.value = Point;
                            //                }
                            //                else
                            //                {
                            //                if(schemeNo>='9')
                            //                {
                            //                
                            //                txtgrade.value=GradeName;
                            //                hidGrade.value=GradeName;
                            //                txtgradepoint.value=Point;
                            //                hidGradePoint.value=Point;
                            //                }
                            //                else
                            //                {
                            //                if(GradeName=='FF')
                            //                {
                            //                
                            //                txtgrade.value=GradeName;
                            //                hidGrade.value=GradeName;
                            //                txtgradepoint.value=Point;
                            //                hidGradePoint.value=Point;
                            //                }
                            //                else
                            //                {
                            //                 
                            //                txtgrade.value='DD';
                            //                hidGrade.value='DD';
                            //                txtgradepoint.value='4';
                            //                hidGradePoint.value='4';
                            //                }
                            //                }
                            //                }
                        }
                    }
                    //This is for calculating Students of mark entry done.
                    studTotal();

                }

            }
            else {

                totper.value = '';
                txtgrade.value = '';
                txtgradepoint.value = '';
                hidTotPer.value = '';
                hidGrade.value = '';
                hidGradePoint.value = '';
                var dataRows = null;
                if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                }
                for (i = 0; i < dataRows.length - 1; i++) {
                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                    document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_hidTotalStudent').value = 0;
                }
                studTotal()
            }
        }


        function IsNumericTest(txt) {
            var ValidChars = "0123456789.";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);
                //if (mChar == '.') cnt++;
                //if (cnt > 1) { alert("Please Check the value."); txt.value = ""; return }
                //else
                    if (ValidChars.indexOf(mChar) == -1) {
                        num = false;
                        txt.value = '';
                        alert("Please enter Numeric values only")
                        txt.select();
                        txt.focus();
                    }
            }
            return num;
        }
        function IsNumeric(txt) {
            var ValidChars = "0123456789.";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);

                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }

        function showLockConfirm() {
            var ret = confirm('Do you really want to lock marks for selected exam?');
            if (ret == true)
                return true;
            else
                return false;
        }
        //This method for changing Max Marks Range.
        function changeMaxMarksRange(txt) {

            X1 = Number(txt.value);
            var dataRows20 = null;
            if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                dataRows20 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

            }

            xMax1 = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl0_txtMax').value);

            //Here Check Grades lv length 
            if (dataRows20.length == "10") {
                xMin1 = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl6_txtMin').value);

            }
            else {
                if (dataRows20.length == "11") {
                    xMin1 = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl7_txtMin').value);

                }
            }
            //Here Check entered range is in between max and min  range or not.
            if (X1 < xMax1 & X1 > xMin1) {
                var dataRows7 = null;
                if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                    dataRows7 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                }

                for (k = 0; k < dataRows7.length - 1; k++) {

                    xMax = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMax').value);
                    xMin = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value);
                    if (X1 == xMax) {
                        if (k == 0) {
                            break;
                        }
                        else {
                            if (Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMax').value) <= Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value)) {
                                if (Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value) > 9) {
                                    if (Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMax').value) >= 10) {
                                        alert("No. Should not Less then  (" + xMin + ") ")
                                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (k - 1) + '_txtMin').value = 0;
                                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMax').value = 0;
                                        break;
                                    }
                                    else {
                                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (k - 1) + '_txtMin').value = X1 + 1;
                                        break;
                                    }
                                }
                                else {
                                    if (Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value) < 10) {
                                        if (Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMax').value) <= Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value)) {
                                            alert("No. Should not Less then  (" + xMin + ") ")
                                            break;
                                        }
                                        else {
                                            document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (k - 1) + '_txtMin').value = X1 + 1;
                                            break;
                                        }
                                    }
                                    else {
                                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (k - 1) + '_txtMin').value = X1 + 1;
                                        break;
                                    }
                                }
                            }
                            else {
                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (k - 1) + '_txtMin').value = X1 + 1;
                                break;
                            }

                        }
                    }
                }
                AdjustGrades();
            }
            else {
                alert("Please Enter Value Between " + xMin1 + " To " + xMax1 + " ");
                var dataRows10 = null;
                if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                    dataRows10 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                }

                for (k = 0; k < dataRows10.length - 1; k++) {

                    xMax = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMax').value);
                    xMin = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value);
                    // Here If entered value is out of range then do it's  min  max  null
                    if (Number(xMax) > -1) {//Added Mahesh Dt.25/07/2019
                        if (X1 == xMax) {
                            //document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMax').value = null;
                            var vmin = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value);
                            var vmax = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (k + 1) + '_txtMax').value);
                            alert(vmin);
                            if (vmin != 0) {
                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value = null;
                            }
                            if (vmax > -1) {
                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (k + 1) + '_txtMax').value = null;
                            }
                            //document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (k - 1) + '_txtMin').value = 0;
                        }
                    }
                }
            }

        }
        //This method for changing Min Marks Range. 
        //function changeMinMarksRange(txt) {
        //    X1 = Number(txt.value);

        //    var dataRows20 = null;
        //    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
        //        dataRows20 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
        //    }

        //    xMax1 = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl0_txtMax').value);
        //    //Here Check Grades lv length 
        //    if (dataRows20.length == "10") {
        //        xMin1 = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl6_txtMin').value);
        //    }
        //    else {
        //        if (dataRows20.length == "11") {
        //            xMin1 = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl7_txtMin').value);
        //        }
        //    }
        //    //Here Check entered range is in between max and min  range or not.
        //    if (X1 < xMax1 & X1 > xMin1) {
        //        var dataRows8 = null;
        //        if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
        //            dataRows8 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
        //        }
        //        for (k = 0; k < dataRows8.length - 1; k++) {
        //            xMax = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMax').value);
        //            xMin = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value);

        //            if (X1 == xMin) {
        //                if (k == 7) // if first record ie. 'S' grade
        //                {
        //                    break;
        //                }
        //                else {
        //                    //Here Checking of Entered Range is correct or not in max & min values.

        //                    if (Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value) >= Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMax').value)) {
        //                        alert("No. Should not Greater then  (" + xMax + ") ")
        //                        if (Number(xMax) > -1) {//Added Mahesh Dt.25/07/2019
        //                            var vmin = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value);
        //                            var vmax = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (k + 1) + '_txtMax').value);
        //                            if (vmin != 0) {
        //                                alert(vmin);
        //                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value = null;
        //                            }
        //                            if (vmax > -1) {
        //                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (k + 1) + '_txtMax').value = null;
        //                            }
        //                        }
        //                        break;
        //                    }
        //                    else {

        //                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (k + 1) + '_txtMax').value = X1 - 1;
        //                        break;
        //                    }
        //                }

        //            }
        //        }
        //        //Calling method AdjustGrades(); .
        //        AdjustGrades();
        //    }
        //    else {
        //        alert("Please Enter Value Between " + xMin1 + " To " + xMax1 + "  ");
        //        var dataRows10 = null;
        //        if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
        //            dataRows10 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
        //        }

        //        for (k = 0; k < dataRows10.length - 1; k++) {

        //            xMax = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMax').value);
        //            xMin = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value);
        //            // Here If entered value is out of range then do it's  min  max  null
        //            if (Number(xMax) > -1) {//Added Mahesh Dt.25/07/2019
        //                if (X1 == xMin) {
        //                    //document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value = null;
        //                    var vmin = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value);
        //                    var vmax = Number(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (k + 1) + '_txtMax').value);
        //                    if (vmin != 0) {
        //                        alert(vmin);
        //                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtMin').value = null;
        //                    }
        //                    if (vmax > -1) {
        //                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (k + 1) + '_txtMax').value = null;
        //                    }
        //                    //document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (k + 1) + '_txtMax').value = 0;
        //                }
        //            }
        //        }
        //    }
        //}



      //  function changeMinMarksRange(txt) {
      //
      //      debugger
      //      var dataRowsmark = null;
      //
      //      if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
      //          dataRowsmark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
      //          for (j = 0; j < dataRowsmark.length - 1; j++) {
      //              debugger
      //              //var MaxMark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax');
      //              //var MinMark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin');
      //
      //              var MaxMark1 = 0;
      //              var MinMark1 = 0;
      //              var AssGrade;
      //              var GDpoint;
      //
      //
      //              MaxMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + j + '_txtMax').value.trim());
      //              MinMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + j + '_txtMin').value.trim());
      //              AssGrade = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + j + '_txtGrades').value.trim();
      //              // GDpoint = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim());
      //
      //
      //              var grid = document.getElementById("<=gvStudent.ClientID%>");
      //              for (var i = 0; i < grid.rows.length - 1; i++) {
      //                  //var row = $(this).closest("tr");
      //                  var Num = $("input[id*=txtTotPer]")
      //                  var MainGd = $("input[id*=txtGrade]")
      //                  var txtESEMarks = $("input[id*=txtESMarks]")
      //                  var per = Num[i];
      //                  var GradeAssign = MainGd[i];
      //                  var abs = txtESEMarks[i];
      //                  //alert(GradeAssign.value);
      //                  // alert(X1);
      //                  // alert(xMax1);                
      //                  //alert(per.value);
      //                  if (parseFloat(MaxMark) >= parseFloat(per.value) && parseFloat(MinMark) <= parseFloat(per.value)) {
      //
      //                      if (abs.value > 901) {
      //                      }
      //                      else {
      //                          GradeAssign.value = AssGrade;
      //                      }
      //                  }
      //              }
      //
      //          }
      //      }
      //  }

// COMMENTED BY NARESH BEERLA ON 30032022 
   //     function changeMinMarksRange(txt) {
   //
   //         debugger
   //
   //         X1 = Number(txt.value);
   //         var dataRowsmark = null;
   //
   //         if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
   //             dataRowsmark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
   //             for (j = 0; j < dataRowsmark.length - 1; j++) {
   //                 debugger
   //                 //var MaxMark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax');
   //                 //var MinMark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin');
   //
   //                 var MaxMark1 = 0;
   //                 var MinMark1 = 0;
   //                 var AssGrade;
   //                 var GDpoint;
   //                 var AssValu = 0;
   //
   //
   //                 MaxMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + j + '_txtMax').value.trim());
   //                 MinMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + j + '_txtMin').value.trim());
   //                 AssGrade = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + j + '_txtGrades').value.trim();
   //                 // GDpoint = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim());
   //
   //                 if (parseFloat(MinMark) == X1) {
   //                     AssValu = Number(X1 - 1);
   //                     document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (j + 1) + '_txtMax').value = parseFloat(AssValu);
   //                 }
   //
   //                 var grid = document.getElementById("<%=gvStudent.ClientID%>");
   //                 for (var i = 0; i < grid.rows.length - 1; i++) {
   //
   //                     var Num = $("input[id*=txtTotPer]")
   //                     var MainGd = $("input[id*=txtGrade]")
   //                     var txtESEMarks = $("input[id*=txtESMarks]")
   //                     var per = Num[i];
   //                     var GradeAssign = MainGd[i];
   //                     var abs = txtESEMarks[i];
   //
   //                     if (parseFloat(MaxMark) >= parseFloat(per.value) && parseFloat(MinMark) <= parseFloat(per.value)) {
   //
   //                         if (abs.value > 901) {
   //                         }
   //                         else {
   //                             GradeAssign.value = AssGrade;
   //                         }
   //                     }
   //                 }
   //
   //             }
   //         }
   //         Resave();
   //     }

        // ENDS HERE BY NARESH BEERLA 

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ADDED NEW SCRIPT BY NARESH BEERLA ON 30032022~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
        function changeMinMarksRange(txt) {

            debugger

            X1 = Number(txt.value);
            var dataRowsmark = null;
            var percnt;
            if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                dataRowsmark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                for (j = 0; j < dataRowsmark.length - 1; j++) {
                    debugger
                    //var MaxMark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax');
                    //var MinMark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin');

                    var MaxMark1 = 0;
                    var MinMark1 = 0;
                    var AssGrade;
                    var GDpoint;
                    var AssValu = 0;


                    MaxMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + j + '_txtMax').value.trim());
                    MinMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + j + '_txtMin').value.trim());
                    AssGrade = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + j + '_txtGrades').value.trim();
                    GDpoint = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + j + '_txtGradePoints').value.trim());

                    if (X1 == 0) {
                    }
                    else if (parseFloat(MinMark) == X1) {
                        AssValu = Number(X1 - 0.01);
                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + (j + 1) + '_txtMax').value = parseFloat(AssValu);
                    }

                    var grid = document.getElementById("<%=gvStudent.ClientID%>");
                    for (var i = 0; i < grid.rows.length - 1; i++) {

                        var Num = $("input[id*=txtTotPer]")
                        var MainGd = $("input[id*=txtGrade]")
                        var txtESEMarks = $("input[id*=txtESMarks]")
                        var GradePoint = $("input[id*=hidGradePoint]");
                        var txtTot = $("input[id*=txtTotMarksAll]");
                        var txtinternal = $("input[id*=txtTAMarks]");  // ADDED ON 11042022 FOR LAW
                        //var percnt = $("input[id*=txtTotPer]");
                        var per = Num[i];
                        var GradeAssign = MainGd[i];
                        var abs = txtESEMarks[i];
                        var gpoint = GradePoint[i];
                        var txtTotMark = txtTot[i];
                        var INT = txtinternal[i]; // ADDED ON 11042022 FOR LAW


                        var hdfCourseTotal = document.getElementById('ctl00_ContentPlaceHolder1_hdfCourseTotal').value;
                        var hdfMinPassMark = document.getElementById('ctl00_ContentPlaceHolder1_hdfMinPassMark').value;
                        var hdfMaxCourseMarks = document.getElementById('ctl00_ContentPlaceHolder1_hdfMaxCourseMarks').value; //40 
                        var hdfMaxCourseMarks_I = document.getElementById('ctl00_ContentPlaceHolder1_hdfMaxCourseMarks_I').value; // INT MAXMRK // ADDED ON 11042022 FOR LAW
                        var hdfMinPassMark_I = document.getElementById('ctl00_ContentPlaceHolder1_hdfMinPassMark_I').value; //INT MIN PASSMRK  // ADDED ON 11042022 FOR LAW

                        var calPer = ((txtTotMark.value * 100) / hdfCourseTotal).toFixed(2);

                        var obtper = ((Number(abs.value) * 100) / 100).toFixed(2);

                        if (INT.value <= 0) {
                            var InterMarkPer = 0;
                        }
                        else {
                            var InterMarkPer = parseFloat((parseFloat(INT.value) * 100) / parseFloat(hdfMaxCourseMarks_I));
                        }
                       // var InterMarkPer = parseFloat((parseFloat(INT.value) * 100) / parseFloat(hdfMaxCourseMarks_I));  // ADDED ON 11042022 FOR LAW

                        var Extpassing = parseFloat((parseFloat(hdfMaxCourseMarks) * parseFloat(hdfMinPassMark)) / 100);
                        //percnt.val(calPer);
                        per.value = (calPer);
                        if (parseFloat(MaxMark) >= parseFloat(per.value) && parseFloat(MinMark) <= parseFloat(per.value)) {

                            if (abs.value > 901 && abs.value < 906) {
                            }
                            else {
                                //    GradeAssign.value = AssGrade;
                                //    gpoint.value = GDpoint;
                                //}

                                //if (parseFloat(obtper) < parseFloat(hdfMinPassMark)) {
                                // if (parseFloat(obtper) < parseFloat(Extpassing)) {       
                                // ADDED ON 11042022 FOR LAW BELOW CONDITION AND COMMENTED ABOVE 
                                if (parseFloat(obtper) < parseFloat(Extpassing) || parseFloat(InterMarkPer) < parseFloat(hdfMinPassMark_I)) {
                                    if (GDpoint == 0) {
                                        GradeAssign.value = AssGrade;
                                        gpoint.value = GDpoint;
                                    }
                                }
                                // if (parseFloat(obtper) >= parseFloat(Extpassing)) {
                                // ADDED ON 11042022 FOR LAW BELOW CONDITION AND COMMENTED ABOVE
                                if (parseFloat(obtper) >= parseFloat(Extpassing) && parseFloat(InterMarkPer) >= parseFloat(hdfMinPassMark_I)) {
                                    //   alert(percnt.val());
                                    if (per.value <= MaxMark && per.value >= MinMark) {
                                        GradeAssign.value = AssGrade;
                                        gpoint.value = GDpoint;
                                        //   alert(hidGradePoint.val());
                                        //   studTotal();
                                        // return
                                    }
                                }
                            }
                        }
                    }

                }
            }
            Resave();

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ADDED BY NARESH BEERLA TO CALCULATE THE TOTAL STUDENTS COUNT AS PER GRADE ON 31032022~~~~~~~~~~~~~~~~~~~~~~//
            var dataRows1 = null;
            if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                dataRows1 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
            }
            for (k = 0; k < dataRows1.length - 1; k++) {
                //  Grade2 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtGrades');
                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value = 0;
                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_hidTotalStudent').value = 0;
            }
            studTotal();
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ENDS HERE BY NARESH BEERLA TO CALCULATE THE TOTAL STUDENTS COUNT AS PER GRADE ON 31032022~~~~~~~~~~~~~~~~~~~~~~//
        }


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ENDS HERE NEW SCRIPT BY NARESH BEERLA ON 30032022~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
        // This Method Adjust or Re-entered Grades Every time whenever marks entered.
        function AdjustGrades() {

            var dataRows4 = null;
            if (document.getElementById('ctl00_ContentPlaceHolder1_gvStudent') != null) {
                dataRows4 = document.getElementById('ctl00_ContentPlaceHolder1_gvStudent').getElementsByTagName('tr');
            }


            // For loop For student Gridview list 
            for (j = 2; j <= dataRows4.length; j++) {

                if (j < 10)//This if check length less than 10
                {

                    var totP = 0;
                    var EsemMark = 0;
                    var subId = 0;
                    //"ctl00_ContentPlaceHolder1_
                    //subId = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_hdfSubid').value.trim());

                    //Added Mahesh on Dt.08/06/2019
                    if (document.getElementById('<%= hdfSubid.ClientID %>') != null) {
                        subId = document.getElementById('<%= hdfSubid.ClientID %>').value;
                    }

                    //"ctl00_ContentPlaceHolder1_gvStudent_ctl03_txtTotPer
                    totP = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtTotPer').value.trim());
                    if (subId == 1) {
                        EsemMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtESMarks').value.trim());
                    }
                    if (subId == 2) {
                        EsemMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtESPRMarks').value.trim());
                    }


                    if (EsemMark != '401' && EsemMark != '402' && EsemMark != '403') {
                        if (totP != '') {

                            var MaxAmt = 0;
                            var MinAmt = 0;
                            var dataRows5 = null;


                            if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                dataRows5 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                            }

                            //This For Loop of lvGrades check range of tot percent & fill Grades & grade Point  of that range which will match.
                            for (i = 0; i < dataRows5.length - 1; i++) {
                                MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();
                                //This is fill zero every time before filling grades
                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                //if condition for checking min - max range of tot percent .


                                if (totP >= MinAmt && totP <= MaxAmt) {


                                    //mahesh temp block
                                    // ExamType = document.getElementById('ctl00_ContentPlaceHolder1_hdfExamType').value.trim();
                                    //                                    SchemeNo = document.getElementById('ctl00_ContentPlaceHolder1_hdfSchemeNo').value.trim();
                                    //                                  if(ExamType=='1')
                                    //                                    {

                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = GradeName;
                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = GradeName;

                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = Point;
                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = Point;


                                    //                                    }
                                    //                            else
                                    //                                    {
                                    //                                  if(SchemeNo>='9')
                                    //                                    {
                                    //                
                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = GradeName;
                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = GradeName;

                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = Point;
                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = Point;
                                    //                                    }
                                    //                                    else
                                    //                                    {
                                    //                                    if(GradeName=='FF')
                                    //                                    {
                                    //                
                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = GradeName;
                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = GradeName;

                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = Point;
                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = Point;
                                    //                                     }
                                    //                                    else
                                    //                                    {
                                    //                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = 'DD';
                                    //                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = 'DD';

                                    //                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = '4';
                                    //                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = '4';
                                    //                                    }
                                    //                                    }
                                    //                                    }

                                }
                            }
                        }
                        else {
                            //break;
                        }
                    }

                }
                else// This for above 10 length
                {
                    var totP = 0;
                    var subId = 0;

                    //mahesh temp bolck
                    // subId = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_hdfSubid').value.trim());
                    //Added Mahesh on Dt.08/06/2019
                    if (document.getElementById('<%= hdfSubid.ClientID %>') != null) {
                        subId = document.getElementById('<%= hdfSubid.ClientID %>').value;
                    }


                    totP = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl' + j + '_txtTotPer').value.trim());

                    if (subId == 1) {
                        EsemMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl' + j + '_txtESMarks').value.trim());
                    }
                    if (subId == 2) {
                        EsemMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl' + j + '_txtESPRMarks').value.trim());
                    }


                    if (EsemMark != '401' && EsemMark != '402' && EsemMark != '403') {

                        if (totP != "") {
                            var MaxAmt = 0;
                            var MinAmt = 0;
                            var dataRows6 = null;

                            if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                                dataRows6 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                            }

                            //This For Loop of lvGrades check range of tot percent & fill Grades & grade Point  of that range which will match.
                            for (i = 0; i < dataRows6.length - 1; i++) {

                                MaxAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                                MinAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                                GradeName = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                                Point = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim();
                                //This is fill zero every time before filling grades
                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtTotalStudent').value = 0;
                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_hidTotalStudent').value = 0;
                                //if condition for checking min - max range of tot percent .


                                if (totP >= MinAmt && totP <= MaxAmt) {
                                    //temp block mahesh
                                    //                                   ExamType = document.getElementById('ctl00_ContentPlaceHolder1_hdfExamType').value.trim();
                                    //                                   SchemeNo = document.getElementById('ctl00_ContentPlaceHolder1_hdfSchemeNo').value.trim();
                                    //                                  if(ExamType=='1')
                                    //                                    {
                                    //               
                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl' + j + '_txtGrade').value = GradeName;
                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl' + j + '_hidGrade').value = GradeName;

                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl' + j + '_txtGradePoint').value = Point;
                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl' + j + '_hidGradePoint').value = Point;
                                    //                                    }
                                    //                            else
                                    //                                    {
                                    //                                  if(SchemeNo>='9')
                                    //                                    {
                                    //                
                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = GradeName;
                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = GradeName;

                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = Point;
                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = Point;
                                    //                                    }
                                    //                                    else
                                    //                                    {
                                    //                                    if(GradeName=='FF')
                                    //                                    {
                                    //                
                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = GradeName;
                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = GradeName;

                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = Point;
                                    //                                    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = Point;
                                    //                                     }
                                    //                                    else
                                    //                                    {
                                    //                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade').value = 'DD';
                                    //                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGrade').value = 'DD';

                                    //                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGradePoint').value = '4';
                                    //                                 document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_hidGradePoint').value = '4';
                                    //                                    }
                                    //                                    }
                                    //                                    }
                                }
                            }


                        }
                        else {
                            //break;
                        }
                    }
                }
            }
            studTotal();
        }
    </script>


    <script type="text/javascript">
        if (document.layers) {
            //Capture the MouseDown event.
            document.captureEvents(Event.MOUSEDOWN);

            //Disable the OnMouseDown event handler.
            document.onmousedown = function () {
                return false;
            };
        }
        else {
            //Disable the OnMouseUp event handler.
            document.onmouseup = function (e) {
                if (e != null && e.type == "mouseup") {
                    //Check the Mouse Button which is clicked.
                    if (e.which == 2 || e.which == 3) {
                        //If the Button is middle or right then disable.
                        return false;
                    }
                }
            };
        }

        //Disable the Context Menu event.
        document.oncontextmenu = function () {
            return false;
        };

        //shortcut.add("Ctrl+Shift+A", function () {
        //    return false;
        //});

        document.onkeydown = function (e) {

            if (e.keyCode == 123) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'I'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'J'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'Q'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'U'.charCodeAt(0)) {
                return false;
            }

            if (e.ctrlKey && e.shiftKey && e.keyCode == 'C'.charCodeAt(0)) {
                return false;
            }
        }
        //document.body.onclick = disableclick()
    </script>

    <script language="javascript" type="text/javascript">
        function validateMark1(metxt, txt, txt2, txt3, maxmrk, minmark, txttot, col) {

            //            if (metxt.value != '' ) {

            //TA MARK
            if (col == 1 && metxt != null) {

                if (metxt.value != '' & (Number(metxt.value) > maxmrk || Number(metxt.value) < minmark) || Number(metxt.value) == 902 || Number(metxt.value) == 903 || Number(metxt.value) == 904) {
                    //if (Number(metxt.value) == 401 || Number(metxt.value) == 402 || Number(metxt.value) == 403)
                    //{ }
                    if (Number(metxt.value) == 902 || Number(metxt.value) == 903 || Number(metxt.value) == 904)
                    { }
                    else {
                        //alert("Please Enter Marks in the Range of " + minmark.toString() + " to " + maxmrk.toString() + ' & Note : 401 for Absent');
                        alert("Please Enter Marks in the Range of " + minmark.toString() + " to " + maxmrk.toString());
                        metxt.value = '';
                        metxt.select();
                        metxt.focus();
                    }
                }
                else if (metxt.value != '' & (Number(metxt.value) > maxmrk || Number(metxt.value) < 0)) {

                    if (Number(metxt.value) == 902 || Number(metxt.value) == 903 || Number(metxt.value) == 904)
                    { }
                    else {
                        alert("Please Enter Marks in the Range of 0 to " + maxmrk.toString() + ' & Note : 902 for Absent  and 903 for UFM Copy case and 904 for withheld');
                        metxt.value = '';
                        metxt.select();
                        metxt.focus();
                    }
                }
            }

            //                //TA-PR MARK
            //                if (col == 4 && txt3 != null) {
            //                    if (txt3.value != '' & (Number(txt3.value) > maxmrk || Number(txt3.value) < minmark) || Number(txt3.value) == 401) {
            //                        //if (Number(txt3.value) == 401)
            //                        //{ }
            //                        //else 
            //                        {
            //                            // alert("Please Enter Marks in the Range of " + minmark.toString() + " to " + maxmrk.toString() + ' & Note : 401 for Absent');
            //                            alert("Please Enter Marks in the Range of " + minmark.toString() + " to " + maxmrk.toString());
            //                            txt3.value = '';
            //                            //metxt.select();
            //                            //metxt.focus();
            //                        }
            //                    }
            //                }

            //Test 1 MARK
            //if (col == 2 && txt != '') {
            //    if (Number(txt.value) > maxmrk) {
            //        if (Number(txt.value) == 401 || Number(txt.value) == 402 || Number(txt.value) == 403)
            //        { }
            //        else {
            //            alert("Please Enter Marks in the Range of 0  to " + maxmrk.toString() + ' & Note : 401 for Absent and 402 for withheld and 403 for Copy case ');
            //            txt.value = '';
            //            txt.select();
            //            txt.focus();
            //        }
            //    }
            //}


            //Test 2 MARK
            if (col == 3 && txt2 != '') {
                if (Number(txt2.value) > maxmrk) {
                    if (Number(txt2.value) == 401 || Number(txt2.value) == 402 || Number(txt2.value) == 403)
                    { }
                    else {
                        alert("Please Enter Marks in the Range of 0  to " + maxmrk.toString() + ' & Note : 401 for Absent and 402 for withheld and 403 for Copy case ');
                        txt2.value = '';
                        txt2.select();
                        txt2.focus();
                    }
                }
            }


            if (col == 2 || col == 3) {

                if ((txt.value.length < 1) && (txt2.value.length < 1)) {
                    txttot.value = "";
                }
                else {


                    if (txt2 == '') {

                        if (txt.value == 401 || txt.value == 402 || txt.value == 403)
                            txttot.value = "0";
                        else
                            txttot.value = txt.value;
                    }
                    else if (txt == '') {

                        if (txt2.value == 401 || txt2.value == 402 || txt2.value == 403)
                            txttot.value = "0";
                        else
                            txttot.value = txt2.value;
                    }
                    else if (txt != '' && txt2 != '') {
                        var mark1, mark2;

                        if (txt.value == 401 || txt.value == 402 || txt.value == 403)
                            mark1 = parseInt("0");
                        else
                            if (isNaN(Number(txt.value)))
                                mark1 = 0;
                            else
                                mark1 = Number(txt.value)

                        if (txt2.value == 401 || txt2.value == 402 || txt2.value == 403)
                            mark2 = parseInt("0");
                        else
                            if (isNaN(Number(txt2.value)))
                                mark2 = 0;
                            else
                                mark2 = Number(txt2.value)

                        txttot.value = Number(mark1 + mark2).toFixed(2);

                    }


                }
            }


            if (col == 2 || col == 3 || col == 1)
                IsNumericTest(metxt);
            else
                //check for numeric
                IsNumeric(metxt);
            //            }


        }

        function IsNumericTest(txt) {
            var ValidChars = "0123456789.";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);
                //if (mChar == '.') cnt++;
                //if (cnt > 1) { alert("Please Check the value."); txt.value = ""; return }
                //else
                    if (ValidChars.indexOf(mChar) == -1) {
                        num = false;
                        txt.value = '';
                        alert("Please enter Numeric values only")
                        txt.select();
                        txt.focus();
                    }
            }
            return num;
        }
        function IsNumeric(txt) {
            var ValidChars = "0123456789";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);

                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }

        function showLockConfirm() {
            var ret = confirm('Do you really want to lock marks for selected exam?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlStudGrid.ClientID %>");
            var printWindow = window.open('', '', 'height=5000,width=1000');
            printWindow.document.write('<html><head><title>Marks Entry Page</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>

    <script type="text/javascript">
       // setTimeout("CallButton()", 300000)
       // function CallButton() {
       //     document.getElementById("<%=btnLastSave.ClientID %>").click();
       //     setTimeout("CallButton()", 300000)
        // }

// COMMENTED BY NARESH BEERLA ON 30032022
    //    function Resave() {
    //
    //        debugger
    //
    //        var grid = document.getElementById("<%=gvStudent.ClientID%>");
    //        for (var i = 0; i < grid.rows.length - 1; i++) {
    //
    //            var intmarks = $("input[id*=txtTAMarks]");
    //            var marks = $("input[id*=txtESMarks]");
    //            var conversion = $("input[id*=hdfConversion]");
    //            var GradePoint = $("input[id*=hidGradePoint]");
    //            var TotMark = $("input[id*=txtTotMarksAll]");
    //            var TotPer = $("input[id*=txtTotPer]");
    //            var obt = marks[i];
    //            var INT = intmarks[i];
    //            var hidConversion = conversion[i];
    //            var hidGradePoint = GradePoint[i];
    //            var txtTotMark = TotMark[i];
    //            var percnt = TotPer[i];
    //
    //            var Rule = document.getElementById('ctl00_ContentPlaceHolder1_hdfRule').value;
    //            var hdfCourseTotal = document.getElementById('ctl00_ContentPlaceHolder1_hdfCourseTotal').value;
    //            var hdfMinPassMark = document.getElementById('ctl00_ContentPlaceHolder1_hdfMinPassMark').value;
    //
    //
    //            var subid = document.getElementById('ctl00_ContentPlaceHolder1_hdfSubid').value;
    //
    //            //  alert(document.getElementById('ctl00_ContentPlaceHolder1_hdfSubid').value);
    //            var obtper = ((Number(obt.value) * 100) / 100).toFixed(2);
    //
    //            var Conversion = ((Number(obt.value) * Rule) / 100).toFixed(2);
    //            //   alert(Conversion);
    //            if (subid != 4) {
    //                // alert('hi' + subid);
    //                Conversion = Math.ceil(Conversion);
    //            }
    //
    //            //   var ans = ((obtMark / maxMark) * 100).toFixed(2);
    //            var sum = Number(Conversion) + Number(INT.value);
    //            //  alert(sum);
    //            sum.toFixed(2);
    //
    //            //txtTotMark.val(sum);
    //            var finaltot = Math.round(sum);
    //            txtTotMark.value = (finaltot);
    //            hidConversion.value = (Conversion);
    //            var calPer = ((txtTotMark.value * 100) / hdfCourseTotal).toFixed(2);
    //            percnt.value = (calPer);
    //        }
    //    }

        // ENDS HERE BY NARESH BEERLA ON 30032022

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ADDED NEW SCRIPT BY NARESH BEERLA ON 30032022 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        function Resave() {

            debugger
            //Update the Quantity TextBox.
            // var obt = $(this).val(quantity);

            //Calculate and update Row Total.


            var grid = document.getElementById("<%=gvStudent.ClientID%>");
            for (var i = 0; i < grid.rows.length - 1; i++) {

                //var Num = $("input[id*=txtTotPer]")
                //var MainGd = $("input[id*=txtGrade]")
                //var txtESEMarks = $("input[id*=txtESMarks]")
                //var per = Num[i];
                //var GradeAssign = MainGd[i];
                //var abs = txtESEMarks[i];

                //if (parseFloat(MaxMark) >= parseFloat(per.value) && parseFloat(MinMark) <= parseFloat(per.value)) {

                //    if (abs.value > 901) {
                //    }
                //    else {
                //        GradeAssign.value = AssGrade;
                //    }
                //}

                var intmarks = $("input[id*=txtTAMarks]");
                var marks = $("input[id*=txtESMarks]");
                var conversion = $("input[id*=hdfConversion]");
                var GradePoint = $("input[id*=hidGradePoint]");
                var TotMark = $("input[id*=txtTotMarksAll]");
                var TotPer = $("input[id*=txtTotPer]");
                var grd = $("input[id*=txtGrade]");
                var obt = marks[i];
                var INT = intmarks[i];
                var hidConversion = conversion[i];
                var hidGradePoint = GradePoint[i];
                var txtTotMark = TotMark[i];
                var percnt = TotPer[i];
                var Grade = grd[i];

                var Rule = document.getElementById('ctl00_ContentPlaceHolder1_hdfRule').value;
                var hdfCourseTotal = document.getElementById('ctl00_ContentPlaceHolder1_hdfCourseTotal').value;
                var hdfMinPassMark = document.getElementById('ctl00_ContentPlaceHolder1_hdfMinPassMark').value;
                var hdfMaxCourseMarks = document.getElementById('ctl00_ContentPlaceHolder1_hdfMaxCourseMarks').value; //40 
                var hdfMinPassMark_I = document.getElementById('ctl00_ContentPlaceHolder1_hdfMinPassMark_I').value; //40  // ADDED ON 11042022 FOR LAW

                if (parseInt(obt.value) > 901 && parseInt(obt.value) < 906) {
                    hidConversion.value = "0.00";
                    hidGradePoint.value = "0.00";
                    txtTotMark.value = INT.value;
                    var per = ((txtTotMark.value * 100) / hdfCourseTotal).toFixed(2);
                    percnt.value = per;
                }
                else {

                    //var row = $(this).closest("tr");
                    //var totalMarks = $(this).closest('tr').find('td').eq(6).text();
                    //var INT = $("[id*=txtTAMarks]", row).val();
                    //var obt = $("[id*=txtESMarks]", row).val();

                    var subid = document.getElementById('ctl00_ContentPlaceHolder1_hdfSubid').value;

                    var obtper = ((Number(obt.value) * 100) / 100).toFixed(2);
                    // alert(Rule);
                    // var Conversion = (Number(obt.val()) * Rule) / 100;
                    var Conversion = ((Number(obt.value) * Rule) / 100).toFixed(2);
                    //   alert(Conversion);
                    if (subid != 4) {
                        // alert('hi' + subid);
                        Conversion = Math.ceil(Conversion);
                    }

                    //   var ans = ((obtMark / maxMark) * 100).toFixed(2);
                    var sum = Number(Conversion) + Number(INT.value);
                    //  alert(sum);
                    sum.toFixed(2);

                    //var finaltot = Math.round(sum);
                    //txtTotMark.value = (finaltot);
                    var finaltot = Math.ceil(sum);  // ADDED CEILING FOR MARK TOTAL AS PER GOWDHAM ON DT 28032022
                    txtTotMark.value=(finaltot);
                    hidConversion.value = (Conversion);
                    // alert(txtTotMark.value);
                    var calPer = ((txtTotMark.value * 100) / hdfCourseTotal).toFixed(2);

                    percnt.value = (calPer);
                    //    alert((calPer));


                    //var dataRowsmark = null;

                    //if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                    //    dataRowsmark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                    //    for (i = 0; i < dataRowsmark.length - 1; i++) {
                    //        debugger
                    //        //var MaxMark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax');
                    //        //var MinMark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin');

                    //        var MaxMark1 = 0;
                    //        var MinMark1 = 0;
                    //        var AssGrade;
                    //        var GDpoint;


                    //        MaxMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                    //        MinMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                    //        AssGrade = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                    //        GDpoint = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim());
                    //        if (parseFloat(obtper) < parseFloat(hdfMinPassMark)) {
                    //            //25 < 40
                    //            //gdpoint = 0 value = grade ;
                    //            if (GDpoint == 0) {
                    //                //  Grade.value = (AssGrade);
                    //                hidGradePoint.value = (GDpoint);
                    //                //  alert(hidGradePoint.val());
                    //                //  studTotal();
                    //                //   return
                    //            }

                    //            //grade.val(AssGrade.select(MinMark=0));
                    //            //return
                    //        }

                    //        if (parseFloat(obtper) >= parseFloat(hdfMinPassMark)) {
                    //            //   alert(percnt.val());
                    //            if (parseFloat(percnt.value) <= parseFloat(MaxMark) && parseFloat(percnt.value) >= parseFloat(MinMark)) {
                    //                //   Grade.value = (AssGrade)
                    //                hidGradePoint.value = (GDpoint);
                    //                //   alert(hidGradePoint.val());
                    //                //   studTotal();
                    //                //    return
                    //            }
                    //        }
                    //            //else if (percnt.val() <= MaxMark && percnt.val() >= MinMark) {
                    //            //    Grade.val(AssGrade)
                    //            //    return
                    //            //}
                    //            //else if (percnt.val() <= MaxMark && percnt.val() >= MinMark) {
                    //            //    Grade.val(AssGrade)
                    //            //    return
                    //            //}
                    //        else {
                    //            //Grade.val(AssGrade)
                    //            // Grade.value = '';

                    //        }

                    //    }
                    //    // studTotal();
                    //}

                }
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ENDS HERE NEW SCRIPT BY NARESH BEERLA ON 30032022 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

    </script>

    <script language="javascript" type="text/javascript">

        //This Method for Calculating Students which done mark entry 
        function studTotal() {
            debugger
            var Grade1 = null;
            var Grade2 = null;
            var totStud = 0;
            var countstud = 0;
            var sumtotstud = 0;
            var sum = 0;

            var dataRows = null;
            if (document.getElementById('ctl00_ContentPlaceHolder1_gvStudent') != null) {
                dataRows = document.getElementById('ctl00_ContentPlaceHolder1_gvStudent').getElementsByTagName('tr');
            }

            if (dataRows == null) {
                return
            }

            for (j = 2; j <= dataRows.length; j++) {
               // document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value = 0;
                //   document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_hidTotalStudent').value = 0;
           //     totStud = 0;
                if (j < 10)//for less than 10 length of Students.. 
                {
                    Grade1 = document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + j + '_txtGrade');
                    
                    var dataRows1 = null;
                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                        dataRows1 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                    }
                    for (k = 0; k < dataRows1.length - 1; k++) {
                        Grade2 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtGrades');
                        
                        // Here Check GDPOINT of lvgrade & lvStudent 
                        if (Grade1.value != null && Grade2.value != null) {
                            if (Grade1.value == Grade2.value) {


                                totStud = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value.trim());

                                countstud = totStud + 1;

                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value = countstud;
                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_hidTotalStudent').value = countstud;
                            }

                        }
                    }
                }
                else {
                    //for graeter than 10 length of Students.. 
                    Grade1 = document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl' + j + '_txtGrade');

                    var dataRows2 = null;
                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                        dataRows2 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

                    }
                    for (k = 0; k < dataRows2.length - 1; k++) {
                        Grade2 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtGrades');
                       
                        if (Grade1.value != null && Grade2.value != null) {
                            if (Grade1.value == Grade2.value) {
                                totStud = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value.trim());

                                countstud = totStud + 1;

                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value = countstud;
                                document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_hidTotalStudent').value = countstud;
                            }
                        }
                    }
                }

            }
            var dataRows3 = null;
            if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                dataRows3 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');

            }

            for (k = 0; k < dataRows3.length - 1; k++) {
                sumtotstud = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value.trim());
                sum = sumtotstud + sum;
                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAllStudent').value = sum;
            }

        }



        function CheckMark(id) {
            //alert(id.value);
            if (id.value < 0) {
                if (id.value == -1 || id.value == -2 || id.value == -3 || id.value == -4)  /// || id.value == -3
                {
                    //alert("You have marked student as ABSENT.");
                }
                else {
                    alert("Marks Below Zero are Not allowed. Only -1 ,-2,-3 and -4 can be entered.");
                    id.value = '';
                    id.focus();
                }
            }
        }

        function validateMark(txttut, txtmidsem, txtCT, txtCE, txtatt, txttot, col) {

            var mark1, mark2, mark3, mark4, mark5, total;

            if (txttut.value == "") {
                txttot.value = "";
                mark1 = 0
            }
            else {
                mark1 = txttut.value;

            }
            if (txtmidsem.value == "") {
                txttot.value = "";
                mark2 = 0;
            }
            else {
                mark2 = txtmidsem.value;

            }
            if (txtCT.value == "") {
                txttot.value = "";
                mark3 = 0;
            }
            else {
                mark3 = txtCT.value;

            }
            if (txtCE.value == "") {
                txttot.value = "";
                mark4 = 0;
            }
            else {
                mark4 = txtCE.value;

            }
            if (txtatt.value == "") {
                txttot.value = "";
                mark5 = 0;
            }
            else {
                mark5 = txtatt.value;

            }
            if (col == 2) {
                total = parseFloat(mark2) + parseFloat(mark3) + parseFloat(mark5);
            }
            else if (col == 1) {
                total = parseFloat(mark2) + parseFloat(mark3) + parseFloat(mark5) + parseFloat(mark1) + parseFloat(mark4);
            }
            else if (col == 3) {
                total = parseFloat(mark4);
            }
            else if (col == 4) {
                total = parseFloat(mark2) + parseFloat(mark3) + parseFloat(mark5) + parseFloat(mark1);
            }
            txttot.value = total;
        }

        function showLockConfirm(btn, group) {
            debugger;
            var validate = false;
            if (Page_ClientValidate(group)) {
                validate = ValidatorOnSubmit();
            }
            if (validate)    //show div only if page is valid and ready to submit
            {
                var ret = confirm('Do you really want to lock marks for selected exam?\n\nOnce Locked it cannot be modified or changed.');
                if (ret == true) {
                    var ret2 = confirm('You are about to lock entered marks, be sure before locking.\n\nClick OK to Continue....');
                    if (ret2 == true) {
                        validate = true;
                    }
                    else
                        validate = false;
                }
                else
                    validate = false;
            }
            return validate;
        }

        function showUnLockConfirm(btn, group) {
            debugger;
            var validate = false;
            if (Page_ClientValidate(group)) {
                validate = ValidatorOnSubmit();
            }
            if (validate)    //show div only if page is valid and ready to submit
            {
                var ret = confirm('Do you really want to unlock marks for selected exam?');
                if (ret == true) {
                    //                    var ret2 = confirm('You are about to unlock entered marks, be sure before locking.\n\nClick OK to Continue....');
                    //                    if (ret2 == true) {
                    validate = true;
                }
                    //                    else
                    //                        validate = false;
                    //                }
                else
                    validate = false;
            }
            return validate;
        }

        function showLockConfirm_old() {
            var ret = confirm('Do you really want to lock marks for selected exam?');
            if (ret == true)
                return true;
            else
                return false;
        }

        function showGradeConfirm(btn, group) {
            debugger;
            var validate = false;
            if (Page_ClientValidate(group)) {
                validate = ValidatorOnSubmit();
            }
            if (validate)    //show div only if page is valid and ready to submit
            {
                var ret = confirm('Do you really want to Generate Grade for selected exam?\n\nOnce Grade Generated it cannot be modified or changed.');
                if (ret == true) {
                    var ret2 = confirm('You are about to Generate Grade, be sure before generating grade.\n\nClick OK to Continue....');
                    if (ret2 == true) {
                        validate = true;
                    }
                    else
                        validate = false;
                }
                else
                    validate = false;
            }
            return validate;
        }
    </script>

    <script type="text/javascript">
        //jq1833 = jQuery.noConflict();
        $(document).ready(function () {

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                debugger;

                //$("#ctl00_ContentPlaceHolder1_gvStudent .form-control").change(function () {
                //    //document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value = 0;
                //    //document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_hidTotalStudent').value = 0;
                //    var dataRows1 = null;
                //    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                //        dataRows1 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                //    }
                //    for (k = 0; k < dataRows1.length - 1; k++) {
                //      //  Grade2 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtGrades');
                //        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value = 0;
                //        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_hidTotalStudent').value = 0;
                //    }
                //    studTotal();
                //});



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

                    var Grade;
                    var percnt;

                    $(this).css("border", "1px solid #d2d6de");
                    var MaxMarks = parseFloat($('input[id$=hfdMaxMark]').val().trim());//$(".MaxMarks").html().split(':')[1].slice(0, -1).trim();
                    //alert('hi Beerla');
                    //alert(MaxMarks);
                    if (parseInt($(this).val()) == 90) {
                        if (parseInt($(this).val()) > MaxMarks) {
                            alert('Marks should not greater than Max Marks !!');
                            $(this).val('');
                            $(this).focus();
                        }
                    }


                    var dataRows1 = null;
                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                        dataRows1 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                    }
                    for (k = 0; k < dataRows1.length - 1; k++) {
                        //  Grade2 = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtGrades');
                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_txtTotalStudent').value = 0;
                        document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + k + '_hidTotalStudent').value = 0;
                    }
                    studTotal();




                });

               

                $("#ctl00_ContentPlaceHolder1_gvStudent .form-control").keyup(function () {
                    debugger

                    
                    var quantity = ($.trim($(this).val()));
                    if (isNaN(quantity)) {
                        quantity = 0;
                    }

                    //Update the Quantity TextBox.
                   var obt= $(this).val(quantity);

                    //Calculate and update Row Total.
                   var row = $(this).closest("tr");
                   var totalMarks = $(this).closest('tr').find('td').eq(6).text();
                   var INT = $("[id*=txtTAMarks]", row).val();                 
                   var Rule = document.getElementById('ctl00_ContentPlaceHolder1_hdfRule').value;
                   var hdfCourseTotal = document.getElementById('ctl00_ContentPlaceHolder1_hdfCourseTotal').value;
                   var hdfMinPassMark = document.getElementById('ctl00_ContentPlaceHolder1_hdfMinPassMark').value;
                   var hdfMaxCourseMarks = document.getElementById('ctl00_ContentPlaceHolder1_hdfMaxCourseMarks').value; //40 
                   var hdfMaxCourseMarks_I = document.getElementById('ctl00_ContentPlaceHolder1_hdfMaxCourseMarks_I').value;  // INT MAX MARK ADDED ON 11042022 FOR LAW
                   var hdfMinPassMark_I = document.getElementById('ctl00_ContentPlaceHolder1_hdfMinPassMark_I').value; //40 // INT MIN PASSMARK ADDED ON 11042022 FOR LAW

                   var subid = document.getElementById('ctl00_ContentPlaceHolder1_hdfSubid').value;

                 //  alert(document.getElementById('ctl00_ContentPlaceHolder1_hdfSubid').value);
                   var obtper = ((Number(obt.val()) * 100) / 100).toFixed(2);
                    //var InterMarkPer = parseFloat((parseFloat(INT) * 100) / parseFloat(hdfMaxCourseMarks_I)); //  ADDED ON 11042022 FOR LAW
                   if (INT <= 0) {
                       var InterMarkPer = 0;
                   }
                   else {
                       var InterMarkPer = parseFloat((parseFloat(INT) * 100) / parseFloat(hdfMaxCourseMarks_I));
                   }


                   var Extpassing = parseFloat((parseFloat(hdfMaxCourseMarks) * parseFloat(hdfMinPassMark)) / 100);

                    // alert(Rule);
                  // var Conversion = (Number(obt.val()) * Rule) / 100;
                    var Conversion = ((Number(obt.val()) * Rule) / 100).toFixed(2);
                    //   alert(Conversion);
                    if (subid != 4) {
                       // alert('hi' + subid);
                        Conversion = Math.ceil(Conversion);
                    }

                 //   var ans = ((obtMark / maxMark) * 100).toFixed(2);
                    var sum = Number(Conversion) + Number(INT);
                    //  alert(sum);
                    sum.toFixed(2);
                    var grid = document.getElementById("<%= gvStudent.ClientID%>");  
                    for (var i = 0; i < grid.rows.length - 1; i++) {

                        var txtTotMark = $("input[id*=txtTotMarksAll]", row)
                        //txtTotMark.val(sum);
                        //var finaltot = Math.round(sum);
                        var finaltot = Math.ceil(sum);  // ADDED CEILING FOR MARK TOTAL AS PER GOWDHAM ON DT 28032022
                        txtTotMark.val(finaltot);
                        var hidConversion = $("input[id*=hdfConversion]", row);
                        var hidGradePoint = $("input[id*=hidGradePoint]", row);
                        hidConversion.val(Conversion);

                        //var calPer = ((txtTotMark.val() / 100) * 100).toFixed(2);

                        var calPer = ((txtTotMark.val() * 100) / hdfCourseTotal).toFixed(2);

                        //alert(calPer);
                        //var percnt = $("input[id*=txtTotPer]", row)
                        //percnt.val(calPer);

                        percnt = $("input[id*=txtTotPer]", row)
                        percnt.val(calPer);

                        //var Grade = $("input[id*=txtGrade]", row)
                        Grade = $("input[id*=txtGrade]", row);

                        var GreaterMark = $("input[id*=hdfGreaterVal]", row)
                        if (parseInt(obt.val()) > parseInt(GreaterMark.val())) {
                            obt.val('');
                            txtTotMark.val('');
                            percnt.val('');
                            Grade.val('');
                            alert('Value Can Not Be Greater Than Maximum Marks');
                            return

                        }

                        //if (parseInt(obt.val()) <= parseInt(GreaterMark.val())) {
                        //    //102 <= 100
                        //}
                        //else {
                        //    obt.val('');
                        //    txtTotMark.val('');
                        //    percnt.val('');
                        //    Grade.val('');
                        //    alert('Marks Can Not Be Null or Greater Than Maximum Marks ');
                        //    return
                        //}

                    }

                    var dataRowsmark = null;

                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                        dataRowsmark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                        for (i = 0; i < dataRowsmark.length - 1; i++) {
                            debugger
                            //var MaxMark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax');
                            //var MinMark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin');

                            var MaxMark1 = 0;
                            var MinMark1 = 0;
                            var AssGrade;
                            var GDpoint;

                         
                            MaxMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                            MinMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin').value.trim());
                            AssGrade = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGrades').value.trim();
                            GDpoint = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtGradePoints').value.trim());
                            // if (parseFloat(obtper) < parseFloat(Extpassing)) {
                            // ADDED ON 11042022 FOR LAW AND COMMENTED ABOVE
                            if ((parseFloat(obtper) < parseFloat(Extpassing) || parseFloat(InterMarkPer) < parseFloat(hdfMinPassMark_I))) {
                                //25 < 40
                                //gdpoint = 0 value = grade ;
                                if (GDpoint == 0) {
                                    Grade.val(AssGrade);
                                    hidGradePoint.val(GDpoint);
                                    //  alert(hidGradePoint.val());
                                 //  studTotal();
                                    return
                                }
                                
                                //grade.val(AssGrade.select(MinMark=0));
                                //return
                            }

                            //  if (parseFloat(obtper) >= parseFloat(Extpassing)) {
                            // ADDED ON 11042022 FOR LAW AND COMMENTED ABOVE
                            if (((parseFloat(obtper) >= parseFloat(Extpassing)) && (parseFloat(InterMarkPer) >= parseFloat(hdfMinPassMark_I)))) {
                             //   alert(percnt.val());
                                if (percnt.val() <= MaxMark && percnt.val() >= MinMark) {
                                    Grade.val(AssGrade)
                                    hidGradePoint.val(GDpoint);
                                    //   alert(hidGradePoint.val());
                                //   studTotal();
                                    return
                                }
                            }
                                //else if (percnt.val() <= MaxMark && percnt.val() >= MinMark) {
                                //    Grade.val(AssGrade)
                                //    return
                                //}
                                //else if (percnt.val() <= MaxMark && percnt.val() >= MinMark) {
                                //    Grade.val(AssGrade)
                                //    return
                                //}
                            else {
                                //Grade.val(AssGrade)
                                Grade.val('')

                            }

                        }
                       // studTotal();
                    }

                        


                        //if(percnt.val() <= 100 && percnt.val() >= 91)
                        //{
                        //    Grade.val('A+')
                        //}
                        //else if(percnt.val() <= 90 && percnt.val() >= 80)
                        //{
                        //    Grade.val('A')
                        //}
                        //else if (percnt.val() <= 79 && percnt.val() >= 60) {
                        //    Grade.val('B')
                        //}
                        //else
                        //{
                        //    Grade.val('')
                        //}

                        //var dataRows = null;
                        //if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {

                        //    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                   // }
                        //for (i = 0; i < dataRows.length - 1; i++) {

                        //    //var Grade = $("input[id*=txtGrades]", row);
                        //    //var MaxMarks = $("input[id*=txtMax]", row);
                        //    //var MinMarks = $("input[id*=txtGrades]", row);

                        //    //var Grade = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax');
                        //    var MaxMark = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax');
                        //    var MinMarks = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin');

                        //   // alert(Grade.val());
                        //    alert(MaxMarks);
                        //    alert(MinMarks);

                        //}
                    

                       

                        //var dataRows = null;
                        //if (document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades') != null) {
                        //    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_lvGrades').getElementsByTagName('tr');
                        
                        //for (i = 0; i < dataRows.length - 1; i++) {
                        //    debugger
                        //    var MaxMark=document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax');
                        //    var MinMark=document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMin');
                        //    //if (percnt.val() < MaxMark.val() && percnt.val() > MinMark.val())
                        //    //{
                        //    //    var txtTotMark = $("input[id*=txtTotMarksAll]", row)
                        //    //    txtTotMark.val('A+');
                        //    //}
                        //    alert(MaxMark);
                        //    alert(MinMark);

                        //}
                        
                    

                   
                   // document.getElementById('ctl00$ContentPlaceHolder1$gvStudent$ctl16$txtTotMarksAll').value = sum;
                  
                    //for (k = 0; k < 60 - 1; k++) {
                    //  var  sum1 = sum + 0;
                    //    document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl' + k + '_txtTotMarksAll').value = sum1;
                    //}

                    //alert(INT);
                    //alert(sum);

                
                 //   studTotal();
                });
              //  studTotal();
            });

        });

    </script>
</asp:Content>
