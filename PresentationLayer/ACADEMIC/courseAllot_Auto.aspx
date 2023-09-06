<%@ Page Language="C#" AutoEventWireup="true" CodeFile="courseAllot_Auto.aspx.cs" Inherits="ACADEMIC_courseAllot_Auto" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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

    <asp:UpdatePanel runat="server" ID="updpnl">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SUBJECT TEACHER ALLOTMENT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch </label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="3"
                                            CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch." ValidationGroup="course"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme </label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme." ValidationGroup="course"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" TabIndex="5">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester." InitialValue="0" ValidationGroup="course">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Section </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" TabIndex="6">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section." InitialValue="0" ValidationGroup="course">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged" TabIndex="7">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                            Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Theory/Practical/Tutorial </label>
                                        </div>
                                        <asp:DropDownList ID="ddltheorypractical" runat="server" AppendDataBoundItems="true" TabIndex="8" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddltheorypractical_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Theory</asp:ListItem>
                                            <asp:ListItem Value="2">Practical</asp:ListItem>
                                            <asp:ListItem Value="3">Tutorial</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvLTP" runat="server" ControlToValidate="ddltheorypractical"
                                    Display="None" ErrorMessage="Please Select Theory or Practical or Tutorial Type course" InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course Name </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" TabIndex="9">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Course." ValidationGroup="course">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Teacher from Department </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDeptName" runat="server" TabIndex="10" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Teacher Name </label>
                                        </div>
                                        <asp:DropDownList ID="ddlTeacher" runat="server" AppendDataBoundItems="true" TabIndex="11" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTeacher" runat="server" ControlToValidate="ddlTeacher"
                                            Display="None" InitialValue="0" ValidationGroup="course" ErrorMessage="Please Select Teacher."></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Intake </label>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtIntake" CssClass="form-control" MaxLength="3"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvtxtIntake" runat="server" ControlToValidate="txtIntake"
                                    Display="None" ValidationGroup="course" ErrorMessage="Please Enter Intake."></asp:RequiredFieldValidator>--%>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteIntake" runat="server" TargetControlID="txtIntake" FilterType="Custom" FilterMode="ValidChars"
                                            ValidChars="0123456789" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <label>Total Student(s)</label>
                                        </div>
                                        <asp:TextBox ID="txtTot" runat="server" CssClass="watermarked" Enabled="False"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-md-6" id="dvSchemewithSelectedCourse" runat="server" visible="false">
                                        <label>Selected Course Offered in below Scheme</label>
                                        <p class="text-center">
                                            &nbsp;<asp:Panel ID="Panel2" runat="server" Height="200px" Width="550px" ScrollBars="Vertical">
                                                <asp:ListView ID="lvSchemewithSelectedCourse" runat="server">
                                                    <LayoutTemplate>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="width: 30px">
                                                                <span style="font: bold; font-size: medium; color: InfoText">*</span>
                                                                <asp:Label Font-Size="Medium" runat="server" ID="lblSchemeWithSelectedCourse"
                                                                    ToolTip='<%# Eval("SCHEMENO") %>' Text='<%# Eval("SCHEMENAME") %>'></asp:Label><br />
                                                                <asp:HiddenField ID="hdfCourseNo" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                    </div>
                                    <div class="col-md-6" id="dvAdt" runat="server" visible="false">
                                        <label>&nbsp;</label>
                                        <div>
                                            <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#demo" style="padding-top: 10px">Additional Teachers ></button>
                                        </div>
                                        <%--  <div id="demo" class="collapse">
    Lorem ipsum dolor sit amet, consectetur adipisicing elit,
    sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam,
    quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.
  </div>--%>
                                        <div id="demo" class="collapse">
                                            &nbsp;<asp:Panel ID="pnlList" runat="server" Height="200px" Width="550px" ScrollBars="Vertical">
                                                <asp:ListView ID="lvAdTeacher" runat="server">
                                                    <LayoutTemplate>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="width: 30px">
                                                                <asp:CheckBox ID="chkIDNo" runat="server" ToolTip='<%# Eval("UA_NO") %>' Text='<%# Eval("UA_FULLNAME") %>' /><br />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-2">
                                                <label>Monday</label>
                                                <asp:TextBox ID="txtMon" runat="server"></asp:TextBox>
                                                <label id="lbl_mon" runat="server">Room</label>
                                                <asp:CheckBoxList runat="server" ID="ddlRoomMon"></asp:CheckBoxList>

                                            </div>

                                            <div class="form-group col-md-2">
                                                <label>Tuesday</label>
                                                <asp:TextBox ID="txtTue" runat="server"></asp:TextBox>
                                                <label id="lbl_tue" runat="server">Room</label>
                                                <asp:CheckBoxList runat="server" ID="ddlRoomTue"></asp:CheckBoxList>

                                            </div>
                                            <div class="form-group col-md-2">
                                                <label>Wednesday</label>
                                                <asp:TextBox ID="txtWed" runat="server"></asp:TextBox>
                                                <label id="lbl_wed" runat="server">Room</label>
                                                <asp:CheckBoxList runat="server" ID="ddlRoomWed"></asp:CheckBoxList>
                                            </div>
                                            <div class="form-group col-md-2">
                                                <label>Thursday</label>
                                                <asp:TextBox ID="txtThu" runat="server"></asp:TextBox>
                                                <label id="lbl_thur" runat="server">Room</label>
                                                <asp:CheckBoxList runat="server" ID="ddlRoomThur"></asp:CheckBoxList>
                                            </div>
                                            <div class="form-group col-md-2">
                                                <label>Friday</label>
                                                <asp:TextBox ID="txtFri" runat="server"></asp:TextBox>
                                                <label id="lbl_fri" runat="server">Room</label>
                                                <asp:CheckBoxList runat="server" ID="ddlRoomFri"></asp:CheckBoxList>
                                            </div>
                                            <div class="form-group col-md-2">
                                                <label>Saturday</label>
                                                <asp:TextBox ID="txtSat" runat="server"></asp:TextBox>
                                                <label id="lbl_sat" runat="server">Room</label>
                                                <asp:CheckBoxList runat="server" ID="ddlRoomSat"></asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <br />
                                            <label>Report in</label>
                                            <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Horizontal" TabIndex="12">
                                                <asp:ListItem Selected="True" Value="pdf">Adobe Reader</asp:ListItem>
                                                <asp:ListItem Value="xls">MS-Excel</asp:ListItem>
                                                <asp:ListItem Value="doc">MS-Word</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div id="dvCount" runat="server" class="form-group col-md-12" visible="false">
                                            <p class="text-center">
                                                <asp:Label ID="lbltotcount" runat="server" Font-Bold="true"></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnAd" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="course"
                                    OnClick="btnAd_Click" TabIndex="13" Visible="false" />
                                <asp:Button ID="btnGenerate" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="course"
                                    OnClick="btnGenerate_Click" TabIndex="13" />
                                <asp:Button ID="btnPrint" runat="server" Text="Report" ValidationGroup="course"
                                    CausesValidation="False" OnClick="btnPrint_Click" CssClass="btn btn-info" TabIndex="14" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click"
                                    CausesValidation="False" CssClass="btn btn-warning" TabIndex="15" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="course"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
                            </div>

                            <div id="dvCourse" runat="server" class="col-12" visible="false">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvCourse" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Course Allotment</h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action </th>
                                                            <th>Edit </th>
                                                            <th>Course </th>
                                                            <th>Course Name </th>
                                                            <th>Subject Type </th>
                                                            <%-- <th>Theory/Practical/Tutorial </th>--%>
                                                            <th>Section </th>
                                                            <th>Teacher Name </th>
                                                            <th>Additional Teacher </th>
                                                            <%--<th>Intake </th>--%>
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
                                                <td>
                                                    <asp:ImageButton ID="btnDelete" runat="server" AlternateText='<%# Eval("UA_NO") %>' CausesValidation="False" CommandArgument='<%# Eval("COURSENO") %>' ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" OnClientClick="return ConfirmDelete();" ToolTip="Delete Record" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText='<%# Eval("UA_NO") %>' CausesValidation="False" CommandArgument='<%# Eval("COURSENO") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td><%# Eval("CCODE")%></td>
                                                <td><%# Eval("COURSE_NAME")%></td>
                                                <td><%# Eval("SUBNAME")%>
                                                    <asp:HiddenField ID="hdfthpr" runat="server" Value='<%# Eval("TH_PR")%>' />
                                                </td>
                                                <%--td><%# Eval("THE_PRE")%>
                                                        <asp:HiddenField ID="hdfsubid" runat="server" Value='<%# Eval("subid")%>' />
                                                    </td>--%><td><%#Eval("SECTIONNAME") %>
                                                        <asp:HiddenField ID="hdfSecNo" runat="server" Value='<%# Eval("SECTIONNO")%>' />
                                                    </td>
                                                <td><%# Eval("UA_FULLNAME")%></td>
                                                <%-- <td><%# GetAdTeachers(Eval("ADTEACHER"))%></td>--%>
                                                <td><%# Eval("ADNAMES")%></td>

                                                <%-- <td><%# Eval("INTAKE")%></td>--%>
                                                <asp:HiddenField ID="hdnintake" runat="server" Value='<%# Eval("INTAKE")%>' />
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



    <%--  Enable the button so it can be played again --%>
    <%-- <th style="width: 40%">
                                                    Additional Teacher
                                                </th>--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" AlternateText="Warning" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-warning" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
        function ConfirmDelete() {
            var ret = confirm('Do You Want To Delete Selected Subject Allotment ???');
            if (ret == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <div runat="server" id="divMsg">
    </div>
</asp:Content>
