<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PublishResult.aspx.cs" Inherits="ACADEMIC_PublishResult" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPublishResult"
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
    <asp:UpdatePanel runat="server" ID="updPublishResult">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PUBLISH RESULTS</h3>
                        </div>
                        <div class="box-body">

                            <asp:UpdatePanel ID="updUpdate" runat="server">
                                <ContentTemplate>

                                    <div class="col-12">
                                        <div class="row">

                                              <div class="col-lg-3 col-md-6 col-12 form-group d-none">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Scheme</label>
                                                </div>
                                                <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:requiredfieldvalidator id="rfvscheme" runat="server" controltovalidate="ddlscheme"
                                                    display="none" initialvalue="0" errormessage="please select scheme" validationgroup=""></asp:requiredfieldvalidator>
                                            </div>


                                              <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>College</label>
                                                    <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                                    ValidationGroup="save" data-select2-enable="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                                    Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="report">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Session</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12 form-group d-none">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>School/Institute Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="True" AutoPostBack="True" ToolTip="Please Select Institute"
                                                    OnSelectedIndexChanged="ddlColg_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvcolg" runat="server" ControlToValidate="ddlColg"
                                                    Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12 form-group d-none">
                                                <div class="label-dynamic">
                                                    <%--<sup>* </sup>--%>
                                                    <label>Degree</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12 form-group d-none">
                                                <div class="label-dynamic">
                                                    <%-- <sup>* </sup>--%>
                                                    <label>Programme/Branch</label>
                                                </div>
                                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>--%>
                                            </div>



                                            <div class="col-lg-3 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Semester</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12 form-group d-none">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Course</label>
                                                </div>

                                                <asp:RadioButtonList ID="rdoselect" runat="server" RepeatDirection="Horizontal" AppendDataBoundItems="True"
                                                    AutoPostBack="True" OnSelectedIndexChanged="rdoselect_SelectedIndexChanged" TabIndex="1">
                                                    <asp:ListItem Selected="True" Value="1">Internal Mark Result</asp:ListItem>
                                                    <asp:ListItem Value="2">Final Result</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12 form-group" id="trinternal" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Internal Exam </label>
                                                </div>

                                                <asp:DropDownList ID="ddlInternal" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlInternal_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvInternalExam" runat="server" ControlToValidate="ddlInternal"
                                                    Display="None" ErrorMessage="Please Select Internal Exam" InitialValue="-1" ValidationGroup="report"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12 form-group" id="trexmtype" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Exam Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlExam_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="0">Regular Exam</asp:ListItem>
                                                    <asp:ListItem Value="1">Re-Exam</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvexam" runat="server" ControlToValidate="ddlExam"
                                                    Display="None" ErrorMessage="Please Select Exam" InitialValue="-1" ValidationGroup="report"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12 form-group d-none">
                                                <label>Section</label>
                                                <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12 form-group d-none">
                                                <label>Student Status</label>
                                                <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="1">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="0">Regular </asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12 form-group d-none">
                                                <label>Remark for Unpublish Result</label>
                                                <asp:TextBox ID="txtRemark" runat="server" ToolTip="Please Enter Remark"
                                                    TextMode="MultiLine" MaxLength="200" TabIndex="1"></asp:TextBox>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12 form-group d-none">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Exam Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlExamName" runat="server" AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlExamName_SelectedIndexChanged" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <%--<asp:ListItem Value="1">MID SEM </asp:ListItem>--%>
                                                    <asp:ListItem Value="2">END SEM</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="ddlExamName"
                                                    Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Exam Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlExamType" runat="server" data-select2-enable="true" TabIndex="1" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="0">Regular</asp:ListItem>
                                                    <asp:ListItem Value="1">Backlog</asp:ListItem>
                                                    <asp:ListItem Value="2">PhotoCopy/Revaluation</asp:ListItem>
                                                    <asp:ListItem Value="3">Redo/Resit</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvExamType" runat="server" ControlToValidate="ddlExamType"
                                                    Display="None" ErrorMessage="Please Select Exam Type" InitialValue="-1" ValidationGroup="report"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Publish Date</label>
                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar" id="imgCalDateOfPublish"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDateOfPublish" runat="server"
                                                        TabIndex="1" ToolTip="Please Enter Publish Date" />
                                                    <%-- <asp:Image ID="imgCalDateOfPublish" runat="server" ImageUrl="~/IMAGES/calendar.gif" Style="cursor: pointer"
                                        Height="16px" />--%>
                                                    <asp:RequiredFieldValidator ID="rfvDateOfPublish" runat="server" ControlToValidate="txtDateOfPublish"
                                                        Display="None" ErrorMessage="Please Enter Date Of Publish" SetFocusOnError="True"
                                                        ValidationGroup="report"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="ceDateOfPublish" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDateOfPublish" PopupButtonID="imgCalDateOfPublish"
                                                        Enabled="True">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeDateOfPublish" runat="server" TargetControlID="txtDateOfPublish"
                                                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevDateOfPublish" runat="server" ControlExtender="meeDateOfPublish" ControlToValidate="txtDateOfPublish"
                                                        IsValidEmpty="False" EmptyValueMessage="" InvalidValueMessage="Publish date is invalid"
                                                        EmptyValueBlurredText="" InvalidValueBlurredMessage="" Display="Dynamic" ValidationGroup="report" />
                                                </div>
                                            </div>

                                            <div class="col-lg-3 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <label>Date of Examination </label>
                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar" id="imgExamDate"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtExamDate" runat="server"
                                                        TabIndex="1" ToolTip="Please Enter Exam Date" />

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExamDate"
                                                        Display="None" ErrorMessage="Please Enter Date Of Exam" SetFocusOnError="True"
                                                        ValidationGroup="examdate"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtExamDate" PopupButtonID="imgExamDate"
                                                        Enabled="True">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtExamDate"
                                                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtExamDate"
                                                        IsValidEmpty="False" EmptyValueMessage="" InvalidValueMessage="Exam date is invalid"
                                                        EmptyValueBlurredText="" InvalidValueBlurredMessage="" Display="Dynamic" ValidationGroup="examdate" />

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" TabIndex="1"
                                            Text="Show Student" ValidationGroup="report" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnPublish" runat="server" Enabled="False" CssClass="btn btn-info" TabIndex="1"
                                            OnClick="btnPublish_Click" Text="Publish Result" ValidationGroup="report" />
                                        <ajaxToolKit:ConfirmButtonExtender ID="PublichCon" runat="server"
                                            ConfirmText="Do You Want to Really Publish This Result?"
                                            TargetControlID="btnPublish">
                                        </ajaxToolKit:ConfirmButtonExtender>
                                        <asp:Button ID="btnUnpublish" runat="server" Enabled="False" CssClass="btn btn-info" TabIndex="1"
                                            OnClick="btnUnpublish_Click" Text="Unpublish Result" ValidationGroup="report" />
                                        <ajaxToolKit:ConfirmButtonExtender ID="UnpublichCon" runat="server"
                                            ConfirmText="Do You Want to Really Unpublish This Result?"
                                            TargetControlID="btnUnpublish">
                                        </ajaxToolKit:ConfirmButtonExtender>
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" TabIndex="1"
                                            Text="Cancel" CssClass="btn btn-warning" CausesValidation="false" />
                                        <asp:Button ID="btnReport" Visible="false" runat="server" OnClick="btnReport_Click" TabIndex="1"
                                            Text="Publish Report" CssClass="btn btn-info" />
                                        <asp:Button ID="btnReportsemester" runat="server" Visible="false" TabIndex="1"
                                            OnClick="btnReportsemester_Click" Text="SemesterWise Chart" CssClass="btn btn-info" />
                                        <asp:Button ID="btnReportDepart" runat="server" OnClick="btnReportDepart_Click" Visible="false" TabIndex="1"
                                            Text="DepartmentWise Chart" CssClass="btn btn-info" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="reportgraph" />
                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="examdate" />
                                    </div>
                                    <div style="display: none">
                                        Total Students Selected:
                                <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="unwatermarked"
                                    Style="text-align: center;" BackColor="#FFFFCC" Font-Bold="True"
                                    Font-Size="Small" ForeColor="#000066"></asp:TextBox>
                                        <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                            WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />
                                        <asp:HiddenField ID="hftot" runat="server" />
                                    </div>
                                    <div class="col-12" runat="server" id="divStudentRecord" visible="false">
                                        <div class="sub-heading">
                                            <h5>Student List</h5>
                                        </div>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:ListView ID="lvStudent" runat="server" OnItemDataBound="lvStudent_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkStudent" runat="server" ToolTip='<%# Eval("IDNO") %>' onClick="totSubjects(this);"
                                                                GroupName="BoxChk" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("REGNO")%>
                                                          <%--  <asp:Label ID="lblStudname" runat="server" Visible="false" ToolTip='<%# Eval("UA_NO")%>'></asp:Label>--%>

                                                            <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("UA_NO") %>' />
                                                        </td>

                                                        <td>
                                                            <%# Eval("STUDNAME")%>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblstatus" ToolTip='<%# (Eval("PUB_STATUS"))%>' Text='<%# (Eval("PUB_STATUS"))%>' ForeColor='<%# (Convert.ToString(Eval("PUB_STATUS") )== "Published" ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <LayoutTemplate>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>
                                                                    <asp:CheckBox ID="cbRows" runat="server" ToolTip="Check Record" onClick="SelectAll(this);" />
                                                                </th>
                                                                <th>Reg. No.
                                                                </th>
                                                                <th>Student Name
                                                                </th>
                                                                <th>Status
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtTotStud" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>







    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }

        function SelectAll(chkbox) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudent_ctrl' + i + '_chkStudent');
                if (lst.type == 'checkbox') {
                    if (chkbox.checked == true)
                        lst.checked = true;
                    else
                        lst.checked = false;
                }

            }

            if (chkbox.checked == true) {
                txtTot.value = hftot.value;
            }
            else {
                txtTot.value = 0;
            }
        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
            if (txtTot == 0) {
                alert('Please Check atleast one student ');
                return false;
            }
            else
                return true;
        }
    </script>

</asp:Content>
