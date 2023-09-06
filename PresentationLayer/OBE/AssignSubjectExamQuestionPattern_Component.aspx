<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AssignSubjectExamQuestionPattern_Component.aspx.cs" Inherits="OBE_AssignSubjectExamQuestionPattern" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../newbootstrap/js/jquery-3.3.1.min.js"></script>
    <link href="../Itle_JQuery/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../Itle_JQuery/bootstrap-multiselect.js"></script>
    <link href="../Itle_JQuery/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../Itle_JQuery/bootstrap-multiselect.js"></script>
    <script src="../Content/jquery.dataTables.js" lang="javascript" type="text/javascript"></script>--%>
    <%-- <link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multiselect/bootstrap-multiselect.js"></script>--%>

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <style>
        .table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5 !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                //ExamQuestionPaper.Init();
            });
        });
    </script>

    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>

    <script type="text/javascript">
        function changedropdown(ddlBranch) {

            debugger
            //__doPostBack('<%= rptExamQuestion.ClientID  %>', '');
            //var txtcontrolid=ddlcontrolid.id.replace("dropdownlist_serverId","textbox_serverId");

            var txtcontrolid = ddlBranch.id.replace("MyDropDown", "MyTextBox");
            if ($('#' + ddlBranch).val() == '1')
                $('#' + txtcontrolid).attr('disabled', true);
            else
                $('#' + txtcontrolid).attr('disabled', false);
        }
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updEdit"
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

    <asp:UpdatePanel ID="updEdit" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Exam Question Paper</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="dvSession" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session Name</label>
                                        </div>
                                        <asp:DropDownList runat="server" ID="ddlSession" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Selected="True">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdUserId" runat="server" Value="0" />
                                    </div>
                                    <div class="form-group col-lg-8 col-md-8 col-12" id="dvCourse" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course Name</label>
                                        </div>
                                        <asp:DropDownList runat="server" ID="ddlCourseno" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCourseno_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                    </div>

                                </div>
                            </div>

                            <div id="divBody">
                                <div class="col-12" id="divSubject" runat="server" visible="false">
                                    <div class="table responsive">
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSubject">
                                            <thead class="bg-light-blue" id="headSubj">
                                                <tr>
                                                    <%-- @*<th style="display:none">Action</th>*@--%>
                                                    <th>Edit</th>
                                                    <th>Create</th>
                                                    <th>Subject Name</th>
                                                    <th>Exam Name</th>
                                                    <th>Section</th>
                                                    <th>Status</th>

                                                    <%--<th>View</th>--%>
                                                    <%-- <th>Download</th>--%>
                                                </tr>
                                            </thead>
                                            <tbody id="Tbody1">
                                                <asp:Repeater ID="rptCourse" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="lnkEdit" CommandArgument='<%# Eval("SchemeSubjectId") %>' CommandName="Edit" runat="server" OnClick="lnkEdit_Click" ToolTip='<%# Eval("SchemeSubjectId")%>' TabIndex="4"><i class="fas fa-edit"></i></asp:LinkButton>
                                                                <asp:HiddenField ID="hdnSrno1" runat="server" Value='<%# Eval("srno")%>' />
                                                            </td>

                                                            <td>
                                                                <asp:LinkButton ID="lnkCreate" CommandArgument='<%# Eval("SchemeSubjectId") %>' CommandName="Create" runat="server" OnClick="lnkCreate_Click" ToolTip='<%# Eval("SchemeSubjectId")%>' TabIndex="3"><i class="fas fa-plus"></i></asp:LinkButton>

                                                                <%-- <asp:LinkButton runat="server" ID="btnRun" Text="<i class='fa fa-plus-square'></i> Search" CssClass="btn-success" />--%>
                                                                <%-- <asp:ImageButton runat="server"  Text="<i class='fa fa-home'></i>  Id="CreateImageButton" />--%>
                                                                <%--<td><center><i  style="cursor:pointer; color:green; font-size:18px" aria-hidden="true" class="fa fa-plus-square" title="CREATE" onclick="return OpenPanel(' + item.ExamPatternMappingId + ',' + item.SchemeSubjectId + ',' + item.SectionId + ',' + item.srno + ',0)"></i></center><input type="hidden" id="hdcreateschname_' + item.ExamPatternMappingId + '_' + item.SchemeSubjectId + '_' + item.SectionId + '" value="' + item.SchemeMappingName.trim() + '" /><input type="hidden" id="hdcreateexmName_' + item.ExamPatternMappingId + '_' + item.SchemeSubjectId + '_' + item.SectionId + '" value="' + item.ExamName.trim() + '" /><input type="hidden" id="hdcreateSecName_' + item.ExamPatternMappingId + '_' + item.SchemeSubjectId + '_' + item.SectionId + '" value="' + item.SectionName.trim() + '" /></td>';--%>
                                                                <asp:HiddenField ID="hdnfld_SchemeSubjectId" runat="server" Value='<%# Eval("SchemeSubjectId")%>' />
                                                                <asp:HiddenField ID="hdnSrno" runat="server" Value='<%# Eval("srno")%>' />
                                                                <%--   <asp:HiddenField ID="hdnSrno" runat="server" Value="0" />--%>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lblSubjectName" runat="server" Text='<%# Eval("SchemeMappingName") %>' />
                                                                <asp:HiddenField ID="hdnSchemeSubjectId" runat="server" Value='<%# Eval("SchemeSubjectId")%>' />
                                                                <asp:HiddenField ID="hdnQuestionPaperId" runat="server" Value='<%# Eval("QuestionPaperId")%>' />
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lblExamName" runat="server" Text='<%# Eval("ExamName") %>' />
                                                                <asp:HiddenField ID="ExamPatternMappingId" runat="server" Value='<%# Eval("ExamPatternMappingId")%>' />

                                                            </td>

                                                            <%-- <asp:Label ID="lblSection" runat="server" Text='<%# Eval("SectionName") %>'/>--%>
                                                            <td>
                                                                <%#Eval("SectionName") %>
                                                            </td>
                                                            <asp:HiddenField ID="hdnSectionName" runat="server" Value='<%# Eval("SectionName")%>' />
                                                            <asp:HiddenField ID="hdnSectionId" runat="server" Value='<%# Eval("SectionId")%>' />


                                                            <td>
                                                                <%--   <center><%#Eval("Status") %></center>--%>

                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' />

                                                            </td>

                                                            <%-- <td>
                                                                            <asp:LinkButton ID="lnkView" CommandArgument='<%# Eval("SchemeSubjectId") %>' CommandName="View" runat="server" OnClick="lnkView_Click" ToolTip='<%# Eval("SchemeSubjectId")%>' TabIndex="5"><center><img src="../IMAGES/view.gif" alt="" /></center></asp:LinkButton>
                                                                      
                                                                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("SchemeSubjectId")%>' />
                                                                            <asp:HiddenField ID="hdSrno2" runat="server" Value='<%# Eval("srno")%>' />
                                                                    </td>--%>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                            <tbody id="subjBody"></tbody>
                                        </table>
                                    </div>
                                </div>

                                <div class="col-12" id="tdStudent" runat="server">
                                    <asp:Panel ID="pnlStudGrid" runat="server" Visible="false" ScrollBars="Auto">
                                    </asp:Panel>
                                </div>

                                <div id="divlowerpanel" runat="server" visible="false">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Create Exam Question Paper</h5>
                                                </div>
                                                <input type="hidden" id="hdSchemeSubjectId" value="0" />
                                                <input type="hidden" id="hdExamPatternMappingId" value="0" />
                                                <input type="hidden" id="hdSectionId" value="0" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Subject Name</label>
                                                </div>
                                                <asp:DropDownList runat="server" ID="ddlCirriculumn" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" >
                                                    <asp:ListItem Selected="True">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Exam Name</label>
                                                </div>
                                                <asp:DropDownList runat="server" ID="ddlExamName" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Selected="True">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Question Paper Name</label>
                                                </div>
                                                <asp:TextBox ID="txtQuestionPaperName" runat="server" CssClass="form-control" TabIndex="6" OnTextChanged="txtQuestionPaperName_TextChanged"></asp:TextBox>
                                                <asp:HiddenField ID="hdQuestionPaperId" runat="server" />
                                                <%-- <input type="text" id="txtQuestionPaperName" placeholder="Question Paper Name" name="txtQuestionPaperName" class="form-control" />--%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Maximum Marks</label>
                                                </div>
                                                <asp:TextBox ID="txtMaximumMarks" runat="server" CssClass="form-control" Enabled="true" TabIndex="7" OnTextChanged="txtMaximumMarks_TextChanged" AutoPostBack="true" MaxLength="6" ></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="rfvMaxMark" runat="server" ControlToValidate="txtMaximumMarks"
                                            Display="None" ErrorMessage="Please Enter Maximum Mark.." ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                         <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMaxMarks" runat="server" ValidChars=".0123456789"
                                                        TargetControlID="txtMaximumMarks">
                                                    </ajaxToolKit:FilteredTextBoxExtender> 


                                                <%--<input type="text" id="txtMaximumMarks" onkeypress="return isNumberKey(event)" placeholder="Maximum Marks" disabled="disabled" name="txtMaximumMarks" class="form-control" />--%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Question Pattern</label>
                                                </div>
                                                <asp:DropDownList runat="server" ID="ddlPattern" TabIndex="8" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPattern_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtAreaDiscription" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                                <asp:HiddenField ID="hdSchemeSubjectIdDDl" runat="server" />
                                                <asp:HiddenField ID="hdExamPatternMappingIdDDl" runat="server" />
                                                <asp:HiddenField ID="hdnSectionId" runat="server" />
                                                <%--<textarea id="txtAreaDiscription" name="txtAreaDiscription" class="form-control text-box multi-line " style="height:40px"></textarea>--%>
                                            </div>



                                                   <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Weightage</label>
                                                </div>
                                                <asp:TextBox ID="txtWeightage" runat="server" CssClass="form-control" Enabled="true" TabIndex="9"  AutoPostBack="true" MaxLength="6" ></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWeightage"
                                            Display="None" ErrorMessage="Please Enter Weightage.." ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                         <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars=".0123456789"
                                                        TargetControlID="txtWeightage">
                                                    </ajaxToolKit:FilteredTextBoxExtender> 

                                            </div>
                                                   <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Internal Marks</label>
                                                </div>
                                                <asp:TextBox ID="txtinternal" runat="server" CssClass="form-control" Enabled="false" TabIndex="10"  AutoPostBack="true" MaxLength="6" ></asp:TextBox>
                                               

                                            </div>
                                                   <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>External Marks</label>
                                                </div>
                                                <asp:TextBox ID="txtExternal" runat="server" CssClass="form-control" Enabled="false" TabIndex="11"  AutoPostBack="true" MaxLength="6" ></asp:TextBox>
                                               

                                            </div>
                                                   <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Total Marks</label>
                                                </div>
                                                <asp:TextBox ID="txttot" runat="server" CssClass="form-control" Enabled="false" TabIndex="11"  AutoPostBack="true" MaxLength="6" ></asp:TextBox>
                                               

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="divSection" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <label>CO/Bloom map from section</label>
                                                </div>
                                                <asp:DropDownList runat="server" ID="ddlSection" TabIndex="9" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Selected="True">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-12 mt-4" id="dvQuestionPaper" runat="server">
                                        <div class="table ">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="Table1">
                                                <thead class="bg-light-blue" id="Thead1">
                                                    <tr>
                                                        <th>Question</th>
                                                        <th>CO Category</th>
                                                        <th>Revised Bloom Category</th>
                                                        <%--<th>Attempt</th>--%>
                                                        <th>Question OR With</th>
                                                        <th>Marks</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="Tbody2">
                                                    <asp:Repeater ID="rptExamQuestion" runat="server" OnItemDataBound="rptExamQuestion_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblQuestionPattern" runat="server" Text='<%# Eval("QuestionNumber") %>' />
                                                                    <asp:HiddenField ID="hdQuePatternId" runat="server" Value='<%# Eval("QuestionPatternSubID")%>' />

                                                                    <asp:HiddenField ID="hdPaperQuestionsId" runat="server" Value='<%# Eval("PaperQuestionsId") %>' />

                                                                    <%--<asp:HiddenField ID="hdLevelId" runat="server" Value='<%# Eval("Level_Id") %>' />
                                                                                    <asp:HiddenField ID="hdGroupId" runat="server" Value='<%# Eval("GroupID") %>' />--%>

                                                                    <asp:HiddenField ID="hdLevelId" runat="server" Value='<%# Eval("LVLID") %>' />
                                                                    <asp:HiddenField ID="hdGroupId" runat="server" Value='<%# Eval("GRPID") %>' />


                                                                    <asp:HiddenField ID="hdParentQue" runat="server" Value='<%# Eval("Parent_Question")%>' />
                                                                    <asp:HiddenField ID="hdMarkEntry" runat="server" Value='<%# Eval("Level_Id") %>' />
                                                                    <asp:HiddenField ID="hdQuestionPatternSubID" runat="server" Value='<%# Eval("QuestionPatternSubID") %>' />
                                                                    <asp:HiddenField ID="HdSecOr" runat="server" Value='<%# Eval("Que_Or_With")%>' />
                                                                    <asp:Label ID="lblOR" runat="server" Text="" Visible="false" />
                                                                </td>

                                                                <td>
                                                                    <asp:ListBox ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="9" CssClass="form-control multi-select-demo" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                                                        SelectionMode="multiple" ValidationGroup="report"></asp:ListBox>

                                                                    <%--onchange="changedropdown(this)" --%>

                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBranch" Display="None"
                                                                        ErrorMessage="Please Select CO" SetFocusOnError="true" ValidationGroup="AdmDetail" InitialValue="0" />
                                                                </td>
                                                                <td>

                                                                    <asp:ListBox ID="ddlBloomCategory" runat="server" AppendDataBoundItems="true" TabIndex="10" CssClass="form-control multi-select-demo"
                                                                        SelectionMode="multiple" ValidationGroup="report" AutoPostBack="true" Width="15%"></asp:ListBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblOrWith" runat="server" Text='<%# Eval("OrQuestionNumber") %>' />
                                                                    <asp:HiddenField ID="hdnQueOrWith" runat="server" Value='<%# Eval("Que_Or_With")%>' />
                                                                </td>
                                                                <td>

                                                                    <asp:Label ID="lblMarks" runat="server" Text='<%# Eval("QUESTION_MARKS") %>' />

                                                                    <asp:Label ID="lblSolveNoOfQuestion" Visible="false" runat="server" Text='<%#String.Format("{0} / {1}",Eval("Solve_no_of_question"),Eval("No_of_question"))%>' />
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div id="Div2" class="checkbox checkbox-primary" runat="server">
                                                    <asp:CheckBox ID="IsLock" runat="server" Checked="false" TabIndex="14" Text="Lock Exam Question Paper" />
                                                    <%-- <input type="checkbox" tabindex="3" id="IsLock" name="IsLock" checked="checked" />--%>
                                                    <label for="IsLock">&nbsp;</label>
                                                </div>
                                                <input type="hidden" id="groupcounts" value="0" />
                                                <input type="hidden" id="maxgroup" value="0" />
                                                <input type="hidden" id="hdeditCount" value="0" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="checkbox checkbox-primary">
                                                    <asp:CheckBox ID="IsActive" runat="server" TabIndex="15" Visible="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer" id="dvSubmitData" runat="server" visible="false">
                                        <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="Submit" ValidationGroup="submit"  OnClick="btnSave_Click" TabIndex="16" Enabled="true" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        <asp:Button ID="btnReport" runat="server" class="btn btn-info" Text="Report" TabIndex="16" OnClick="btnReport_Click" Visible="false" />
                                        <asp:Button ID="btnBack" runat="server" class="btn btn-warning" Text="Back" OnClick="btnBack_Click" TabIndex="18" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" TabIndex="17" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">

        function quemaxvalcheck(id) {
            debugger;
            //alert(id.value);
            var tvbl = "#Tbody2";
            var num = 0;
            var tblLength = $("#Tbody2 >tr").length;
            var s = [];
            //for (var i = 0; i < tblLength; i++)
            //{
            s.push(parseFloat(id.value));
            //}
            for (var i = 0; i < s.length; i++) {
                num += s[i];

            }

            var mainmax = $("#ctl00_ContentPlaceHolder1_txtMaximumMarks").val();

            var markssum = 0;
            var AndOrArr = new Array();
            var AndOr = 0;
            var IdArr = new Array();
            var CntrlId;
            if (mainmax == "") {
                alert('Enter max marks for question pattern!', 'Warning!');
                return 1;
            }



            var total = 0;

            for (var i = 0; i < AndOrArr.length; i++) {
                total += AndOrArr[i] << 0;

            }

            if (total > parseInt(markssum)) {
                alert('Sum of Compulsory Question Max Marks must be equal to Question Pattern Total Marks', 'Warning!');

                return 3;
            } else {

            }
            if (parseInt(mainmax) < total) {
                var cId = IdArr[IdArr.length - 1];
                // alert("Marks sum can not be greater than question Pattern Total Marks");
                alert('Marks sum can not be greater than question Pattern Total Marks!', 'Warning!');
                $("#" + cId).val('');
                $("#" + cId).focus();
                return 2;
                //$("#"+txtid).focus();
            }

        }
    </script>

    <script language="javascript" type="text/javascript">
        function btnAddDIV_onclick() {
            var mydiv = document.createElement("div");
            var myinput = document.createElement("input");
            var type = document.createAttribute("type");
            myinput.setAttribute(type, "text");
            mydiv.appendChild(myinput);
            var mainDIV = document.getElementById("mainDIV");
            mainDIV.appendChild(mydiv);
        }
    </script>

    <script>

        $(document).ready(function () {

            //AssignSubjectExamQuestionPattern.Init();

            function IsFloatOnly(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode;
                if (charCode == 46 && evt.srcElement.value.split('.').length > 1) {
                    return false;
                }
                if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            };
            jQuery(document).on('keyup', function (evt) {
                if (evt.keyCode == 27) {
                    alert('Esc key pressed.');
                }
            });
        });
    </script>

    <script>
        var expanded = false;
        function showCheckBoxes() {

            var checkboxes = document.getElementById("checkboxes");
            if (!expanded) {
                checkboxes.style.display = "block";
                expanded = true;

            } else {
                checkboxes.style.display = "none";
                expanded = false;

            }
        }
    </script>

    <script type="text/javascript">
        function dropdowncheckboxes() {
            debugger
            var dataTable = '[{';
            var dataTable1 = '[{';
            $("#Table1 TBODY2 TR")
            {
                debugger

                var totsum = 0;
                var totalMarks = 0;
                var CourseRegistrationDetailsId = $(this).find($('[id^=hdfCourseRegistrationDetailsId]')).val();
                var ExamPatternMappingId = $(this).find($('[id^=hdfExamPatternMappingId]')).val();


            }

            //$('#Table1 > TBODY2  > TR').each(function () {
            //    $(this).find("td").each(function () {
            //        debugger
            //        var totsum = 0;
            //        var totalMarks = 0;
            //        var CourseRegistrationDetailsId = $(this).find($('[id^=hdfCourseRegistrationDetailsId]')).val();
            //        var ExamPatternMappingId = $(this).find($('[id^=hdfExamPatternMappingId]')).val();
            //    });
            //});

            $("#Table1 TBODY2 TR").each(function () {
                debugger
                var totsum = 0;
                var totalMarks = 0;
                var CourseRegistrationDetailsId = $(this).find($('[id^=hdfCourseRegistrationDetailsId]')).val();
                var ExamPatternMappingId = $(this).find($('[id^=hdfExamPatternMappingId]')).val();

                var IsLock = true;
                var ActiveStatus = true;

                $(this).find("td").each(function () {

                    var lenghtext = $(this).find('.quetext').length;
                    if (lenghtext > 0) {
                        var quemarks = ($(this).find("input[type=text]").val());

                        var questionno = $(this).find($('[id^=hdfPaperQuestionsId]')).val();
                        var submarks = $("#txtsubmarks").val();

                        if (quemarks == "" || quemarks == null) {
                            mrcount++;

                            $(this).find("input[type=text]").css("background-color", "#E5CCFF")

                            //$(this).find("input[type=text]").addClass('focus');
                            //return;
                        }
                        else {

                            $(this).find("input[type=text]").removeClass('focus');
                        }


                        totsum += Number(quemarks);

                        if (dataTable == '[{') {
                            dataTable = dataTable + '"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","PaperQuestionsId": "' + questionno + '","quemarks": "' + quemarks + '","IsLock": "' + IsLock + '","ActiveStatus": "' + ActiveStatus + '"}'
                        }
                        else {

                            dataTable = dataTable + ',{"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","PaperQuestionsId": "' + questionno + '","quemarks": "' + quemarks + '","IsLock": "' + IsLock + '","ActiveStatus": "' + ActiveStatus + '"}'

                        }

                    }
                    var TotLeng = $(this).find('.MarksTotCls').length;
                    if (TotLeng > 0) {

                        totalMarks = ($(this).find("input[type=text]").val());
                        if (totalMarks == "" || totalMarks == null) {
                            mrcount++;

                            $(this).find("input[type=text]").addClass('focus');
                            //return;
                        }
                        else {

                            $(this).find("input[type=text]").removeClass('focus');
                         
                        }
                    }

                    var Mxmrk = $(this).find($('[id^=hdMaxMarks]')).val();

                    //var Mx2;
                    //if (Mxmrk > 0) {
                    //    Mx2 = Mxmrk.replace("/", ".00");
                    //}


                    if (parseFloat(quemarks) > parseFloat(Mxmrk)) {
                        MaxMarks++;

                        $(this).find("input[type=text]").addClass('focus');
                        //return;
                    }
                    else {

                        // $(this).find("input[type=text]").removeClass('focus');
                    }
                    //}
                });



                if (dataTable1 == '[{') {
                    dataTable1 = dataTable1 + '"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","MarksObtained": "' + totalMarks + '"}'
                }
                else {

                    dataTable1 = dataTable1 + ',{"CourseRegistrationDetailsId": "' + CourseRegistrationDetailsId + '","ExamPatternMappingId": "' + ExamPatternMappingId + '","MarksObtained": "' + totalMarks + '"}'
                }

            });

            dataTable = dataTable + ']';
            dataTable1 = dataTable1 + ']';

            var MarkEntryList = JSON.parse(dataTable);
            var TotMarksList = JSON.parse(dataTable1);
            var LockStatus = 1;
            $(".loader-area, .loader").fadeOut('slow');



            if (MaxMarks > 0) {

                alert('Marks can not be greater than its maximum limits marks ');

            }

            else {

                saveData(MarkEntryList, TotMarksList, LockStatus);
            }
        }
    </script>

    <script type="text/javascript">
        function EnableSaveButton() {
            document.getElementById("<%=btnSave.ClientID %>").disabled = true;
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
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>
</asp:Content>

