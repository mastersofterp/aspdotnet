<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="EndSemExamMarkEntry.aspx.cs" Inherits="Academic_MarkEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
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
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>

            <style>
                table#ctl00_ContentPlaceHolder1_gvStudent_ctl02_cecomTutMarks_popupTable {
                    left: 980px !important;
                    top: 85px !important;
                }
            </style>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>EXAM MARK ENTRY BY ADMIN</b></h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlMarkEntry" runat="server">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="row">
                                                <div class="col-lg-4 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Institute Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlcollege" TabIndex="1" runat="server" CssClass="form-control" AppendDataBoundItems="true" ToolTip="Please Select Institute" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlcollege"
                                                        Display="None" ErrorMessage="Please Select Institute Name." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcollege"
                                                        Display="None" ErrorMessage="Please Select Institute Name." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="MarksModifyReport"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Session</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession" TabIndex="2" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                        Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfvSessionMarksEntryReport" runat="server" ControlToValidate="ddlSession"
                                                        Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="MarksModifyReport"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Degree</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddldegree" TabIndex="3" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddldegree_SelectedIndexChanged" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvdegree" runat="server" ControlToValidate="ddldegree"
                                                        Display="None" ErrorMessage="Please Select Degree." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Branch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlbranch" TabIndex="4" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvbranch" runat="server" ControlToValidate="ddlbranch"
                                                        Display="None" ErrorMessage="Please Select Branch." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Scheme</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlscheme" TabIndex="5" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvscheme" runat="server" ControlToValidate="ddlscheme"
                                                        Display="None" ErrorMessage="Please Select Scheme." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlsemester" TabIndex="6" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlsemester_SelectedIndexChanged" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvsemester" runat="server" ControlToValidate="ddlsemester"
                                                        Display="None" ErrorMessage="Please Select Semester." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Subject Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="True"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged" data-select2-enable="true"
                                                        TabIndex="7" CssClass="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                                        Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="true"
                                                        ValidationGroup="show">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Course Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                                        CssClass="form-control" AutoPostBack="True" TabIndex="8" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                                        Display="None" ErrorMessage="Please Select Course Name." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12 form-group" runat="server" id="DIVEXAM">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Exam Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                                        CssClass="form-control" TabIndex="9" AutoPostBack="True" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExam"
                                                        Display="None" ErrorMessage="Please Select Exam Name." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12 form-group" id="divSubExamName" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Sub Exam Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubExamName" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                                        CssClass="form-control" TabIndex="9" AutoPostBack="True" OnSelectedIndexChanged="ddlSubExamName_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSubExamName" runat="server" ControlToValidate="ddlSubExamName"
                                                        Display="None" ErrorMessage="Please Select Sub Exam Name." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class=" note-div" style="margin-top: 22px">
                                                <h5 class="heading">Note </h5>
                                                <%-- <p><i class="fa fa-star" aria-hidden="true"></i><span>Session is mandatory for generating Marks Entry Report</span>  </p>--%>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span style="color: red; font-weight: bold">Please Save and Lock for Final Submission.</span>  </p>
                                                <p>
                                                    <i class="fa fa-star" aria-hidden="true"></i><span><span style="color: green; font-weight: bold">(902) for Absent  </span>
                                                        <br />
                                                        <span style="color: green; margin-left: 30px; font-weight: bold">(903) for UFM </span>
                                                        <br />
                                                        <span style="color: green; margin-left: 30px; font-weight: bold">(904) for Detention </span>
                                                        <br />
                                                        <span style="color: green; margin-left: 30px; font-weight: bold">(905) for Incomplete </span>
                                                        <br />
                                                        <span style="color: green; margin-left: 30px; font-weight: bold">(906) for Withdrawl </span></span>
                                                </p>
                                            </div>

                                        </div>
                                        <%-- <div class="col-md-4" style="margin-top: 25px">
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">

                                    <asp:Button ID="btnShow" runat="server" TabIndex="10" Font-Bold="True" OnClick="btnShow_Click"
                                        Text="Show Student" ValidationGroup="show"
                                        CssClass="btn btn-primary" />
                                    <asp:Button ID="btnSave" runat="server" TabIndex="12" Enabled="false" Font-Bold="true" CausesValidation="false"
                                        OnClick="btnSave_Click" Text="Save" CssClass="btn btn-success btnSaveEnabled" ValidationGroup="val" />

                                    <asp:Button ID="btnLock" runat="server" TabIndex="13" Enabled="false" Font-Bold="true"
                                        OnClick="btnLock_Click" OnClientClick="return showLockConfirm(this,'val');" Text="Lock"
                                        CssClass="btn btn-danger" />

                                     <asp:Button ID="btnGrade" runat="server" TabIndex="14" Enabled="false" Font-Bold="true" Visible="false"
                                        OnClick="btnGrade_Click" OnClientClick="return showGradeConfirm(this,'val');" Text="Generate Grade"
                                        CssClass="btn btn-success btnSaveEnabled" />

                                      <asp:Button ID="btnReGrade" runat="server" TabIndex="15" Enabled="false" Font-Bold="true" Visible="false"
                                        OnClick="btnReGrade_Click" OnClientClick="return showGradeConfirm(this,'val');" Text="ReGenerate Grade"
                                        CssClass="btn btn-success btnSaveEnabled" />

                                    <asp:Button ID="btnUnlock" runat="server" TabIndex="14" Font-Bold="true"
                                        Text="Unlock" Enabled="false" Style="display: none;"
                                        CssClass="btn btn-warning" OnClick="btnUnlock_Click" OnClientClick="return showUnLockConfirm(this,'val');" />

                                    <asp:Button ID="btnReport" TabIndex="15" runat="server" Font-Bold="true" Text="Report" CssClass="btn btn-info"
                                        OnClick="btnReport_Click" Enabled="false" Visible="False" />

                                    <asp:Button ID="btnMarksModifyReport" TabIndex="15" runat="server" Font-Bold="true" ValidationGroup="MarksModifyReport" 
                                        Text="Marks Entry Report" CssClass="btn btn-info" OnClick="btnMarksModifyReport_Click" Visible="false" />

                                    <asp:Button ID="btnfinalmarkentry" TabIndex="16" runat="server" Font-Bold="true" ValidationGroup="MarksModifyReport" 
                                        Text="Final TH/PR Marks Entry Report" CssClass="btn btn-info" OnClick="btnfinalmarkentry_Click" Visible="false" />
                                        <asp:Button ID="btnmarkexcel" TabIndex="17" runat="server" Font-Bold="true" ValidationGroup="MarksModifyReport" 
                                        Text="Marks Entry Report Excel" CssClass="btn btn-info" OnClick="btnmarkexcel_Click" Visible="false" />

                                    <asp:Button ID="btnCancel2" runat="server" TabIndex="17" Font-Bold="true" OnClick="btnCancel2_Click"
                                        Text="Cancel" CssClass="btn btn-warning" />
                                    <br />
                                    <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                                    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="show" DisplayMode="List"
                                         ShowSummary="false" ShowMessageBox="true" />
                                    <asp:ValidationSummary runat="server" ID="ValidationSummary2" ValidationGroup="MarksModifyReport" DisplayMode="List"
                                         ShowSummary="false" ShowMessageBox="true" />


                                </div>
                                <div class="col-12" id="tdStudent" runat="server">
                                    <asp:Panel ID="pnlStudGrid" runat="server" Visible="False">
                                        <div id="divNotes">
                                            <%--<span style="color: red">* Checkbox selection is mandatory only for Save Marks.</span>--%>
                                            <div class="sub-heading">
                                                <h5>Enter Marks for following Students</h5>
                                            </div>
                                        </div>
                                        <div style="overflow: auto; width: 100%">

                                            <%--   <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-hover table-bordered">
                                                <HeaderStyle CssClass="bg-light-blue" />--%>

                                            <asp:HiddenField ID="hfdMaxMark" runat="server" />
                                            <asp:HiddenField ID="hfdMinMark" runat="server" />
                                            <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered myCssSS"
                                                BackColor="#ffffff" BorderStyle="None" HeaderStyle-CssClass="GridHeader">
                                                <HeaderStyle CssClass="bg-light-blue" />
                                                <RowStyle Height="0px" />
                                                <Columns>
                                                    <%--<asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkMarks" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <%-- <asp:TemplateField HeaderText="Sr.No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="1%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Height="15px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Registration No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("REGNO") %>' ToolTip='<%# Bind("IDNO") %>'
                                                                Font-Size="9pt" />
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                    <%--  <asp:BoundField DataField="ROLLNO" HeaderText="Roll NO.">
                                                        <ItemStyle />
                                                    </asp:BoundField>--%>
                                                    <asp:BoundField DataField="STUDNAME" HeaderText="Student Name" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                        <ItemStyle />
                                                    </asp:BoundField>

                                                   <%-- <asp:TemplateField HeaderText="Internal Marks" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInternal" runat="server" Text='<%# Bind("INTERMARK") %>'
                                                                Font-Size="9pt" />
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Internal Marks" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblInternal" runat="server" Text='<%# Bind("INTERMARK") %>' Width="80px" CssClass="active"
                                                                Enabled="false" MaxLength="5" Font-Bold="true" Style="text-align: center; box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);"/>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MARKS" Visible="false" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMarks" runat="server" Text='<%# Bind("SMARK") %>' Width="80px"
                                                                Enabled='<%# (Eval("LOCK").ToString() == "True") ? false : true %>'
                                                                MaxLength="5" Font-Bold="true" Style="text-align: center; box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);" onblur="return CheckMark(this);" />
                                                            <asp:Label ID="lblMarks" runat="server" Text='<%# Bind("SMAX") %>' ToolTip='<%# Bind("LOCK") %>'
                                                                Visible="false" />
                                                            <asp:Label ID="lblMinMarks" runat="server" Text='<%# Bind("SMIN") %>' Visible="false" />
                                                            <%-- <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txtMarks">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comTutMarks" ValueToCompare='<%# Bind("SMAX") %>' ControlToValidate="txtMarks"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                                                ValidationGroup="val" Text="*"></asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comTutMarks" ID="cecomTutMarks"
                                                                runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>
                                                            <asp:CompareValidator ID="cvAbsentStud" ValueToCompare="-1" ControlToValidate="txtMarks"
                                                                Operator="NotEqual" Type="Double" runat="server" ErrorMessage="-1 for absent student"
                                                                ValidationGroup="val1" Text="*">
                                                            </asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="cvAbsentStud" ID="vceAbsentStud"
                                                                runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                 <%--   <asp:TemplateField HeaderText="Total Marks" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtTotal" runat="server" Text='<%# Bind("MARKTOT") %>' Font-Size="9pt" />
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Total Marks" Visible="false" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTotal" runat="server" Text='<%# Bind("MARKTOT") %>' Width="80px" class="mybab"
                                                                Enabled='<%# (Eval("LOCK").ToString() == "True") ? false : true %>'
                                                                MaxLength="3" Font-Bold="true" Style="text-align: center; box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);" onkeyup="return CheckMark(this);" />
                                                            

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                   

                                                      <asp:TemplateField HeaderText="Grade" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrade" runat="server" Text='<%# Bind("GRADE") %>' Font-Bold="true"
                                                                Font-Size="9pt" />
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Answer Sheet No" Visible="false" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                               <asp:TextBox ID="txtANS" runat="server" Text='<%# Bind("CONTROLSHEET_NO") %>' Width="140px"
                                                               MaxLength="15" Font-Bold="true" Style="text-align: center; box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);" onkeypress="return isNumberKey(event)" />

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
          <Triggers>
            <asp:PostBackTrigger ControlID="btnmarkexcel" />
        </Triggers>
    </asp:UpdatePanel>


    <div id="divMsg" runat="server">
    </div>
    <asp:HiddenField ID="hdfDegree" runat="server" Value="" />
    <script language="javascript" type="text/javascript">

        function CheckMark(id) {
            
            //alert(id.value);
            if (id.value < 0) {
                alert(id.value)
                if (id.value == -1 || id.value == -2 || id.value == -3 || id.value == -4)  /// || id.value == -3
                {
                    //alert("You have marked student as ABSENT.");
                }
                else {
                    alert("Marks Below Zero are Not allowed. Only -1 ,-2,-3 and -4 can be entered.");
                    id.value = '';
                    id.focus();
                    return false;
                }
            }
            
            if(parseFloat(id.value)==902 || parseFloat(id.value)==903 || parseFloat(id.value)==904||parseFloat(id.value)==905|| parseFloat(id.value)==906)
            {

            

            }
            else if(parseFloat(id.value) > parseFloat($('input[id$=hfdMaxMark]').val().trim())) {
                alert('Marks should not greater than Max Marks !!');
                id.value = '';
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
                    if (parseInt($(this).val()) == 90) {
                        if (parseInt($(this).val()) > MaxMarks) {
                            alert('Marks should not greater than Max Marks !!');
                            $(this).val('');
                            $(this).focus();
                        }
                    }
                });

                $("#ctl00_ContentPlaceHolder1_gvStudent .aspNetDisabled").keyup(function () {
                    debugger

                    if (parseFloat($(this).val().trim()) > parseFloat($('input[id$=hfdMaxMark]').val().trim()))
                    {

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
      <SCRIPT type="text/javascript">
          function isNumberKey(evt) {
              var charCode = (evt.which) ? evt.which : evt.keyCode;
              if (charCode > 31 && (charCode < 48 || charCode > 57))
                  return false;
              return true;
          }
   </SCRIPT>
</asp:Content>
