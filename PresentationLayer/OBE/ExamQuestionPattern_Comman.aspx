<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ExamQuestionPattern_Comman.aspx.cs" Inherits="OBE_ExamQuestionPattern" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
        .form-control {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../Content/jquery.dataTables.js" lang="javascript" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                //ExamQuestionPaper.Init();
            });
        });
    </script>
    <%--<style>
        .dropdown-menu > .active > a, .dropdown-menu > .active > a:focus, .dropdown-menu > .active > a:hover {
            color: #5A738E !important;
            background-color: #eee;
        }

        .checkbox input[type="checkbox" ] {
            opacity: 1 !important;
        }

        .auto-style3 {
            height: 41px;
        }

        .auto-style5 {
            height: 23px;
        }
    </style>--%>

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
                <div class="col-md-12 col-sm-12 col-12" id="divBody">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Exam Question Pattern</h3>
                        </div>

                        <div class="box-body">

                            <div runat="server" id="divMain">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Question Pattern Name </label>
                                            </div>
                                            <asp:TextBox ID="txtQuestionPatternName" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfPatternName" runat="server" ControlToValidate="txtQuestionPatternName"
                                                Display="None" ErrorMessage="Please Enter Question Pattern Name" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Maximum Marks</label>
                                            </div>
                                            <asp:TextBox ID="txtPatternMarks" runat="server" CssClass="form-control" MaxLength="3"  TabIndex="2"></asp:TextBox>
                                            <asp:HiddenField ID="hdnPatternId" runat="server" Value='<%# Eval("QuestionPatternId")%>' />
                                            <asp:RequiredFieldValidator ID="rfPatternMarks" runat="server" ControlToValidate="txtPatternMarks"
                                                Display="None" ErrorMessage="Please Enter  Maximum Marks" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftePatternMarks" runat="server"
                                                FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789." TargetControlID="txtPatternMarks">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:HiddenField ID="hdUserId" runat="server" Value="0" />
                                        </div>

                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                        <label>Status</label>

                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                            <label data-on="Active" tabindex="5" class="newAddNew Tab" data-off="Inactive" for="rdActive"></label>
                                             <asp:HiddenField ID="hfStatus" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmitPatten" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmitPatten_Click" TabIndex="16" ValidationGroup="Submit" OnClientClick="return funStatus();" />
                                    <asp:Button ID="btnCancelPattern" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel1_Click" TabIndex="17" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" />
                                </div>

                            </div>

                            <div class="col-12">
                                <div id="divSubject" runat="server" style="overflow: scroll; height: 400px;" visible="false">
                                    <div class="table responsive">
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblSubject">
                                            <thead class="bg-light-blue" id="headSubj">
                                                <tr>
                                                    <th>Edit</th>
                                                    <th>Configure</th>
                                                    <th>Pattern Name</th>
                                                    <th>Marks</th>
                                                     <th>Status</th>

                                                </tr>
                                            </thead>
                                            <tbody id="Tbody1">
                                                <asp:Repeater ID="rptCourse" runat="server" OnItemDataBound="rptCourse_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="lnkEdit" CommandArgument='<%# Eval("QuestionPatternId") %>' CommandName="Edit" runat="server" OnClick="lnkEdit_Click" ToolTip='<%# Eval("QuestionPatternId")%>' TabIndex="4"><i class="fas fa-edit"></i></asp:LinkButton>
                                                                <asp:HiddenField ID="hdnSrno1" runat="server" Value='<%# Eval("QuestionPatternId")%>' />
                                                            </td>
                                                            <td>    
                                                                <asp:LinkButton ID="lnkConfigure" CommandArgument='<%# Eval("QuestionPatternId") %>' CommandName="Edit" runat="server" OnClick="lnkConfigure" ToolTip='<%# Eval("QuestionPatternId")%>' TabIndex="4"><i class="fa fa-cog" aria-hidden="true"></i></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSubjectName" runat="server" Text='<%# Eval("QuestionPatternName") %>' />
                                                                <asp:HiddenField ID="hdnSchemeSubjectId" runat="server" Value='<%# Eval("QuestionPatternId")%>' />
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lblMarks" runat="server" Text='<%# Eval("MARKS") %>' />

                                                            </td>
                                                             <td>
                                                                     <asp:Label ID="lblstatus"  Text='<%# Eval("ACTIVESTATUS")%>' ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server" />

                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                            <tbody id="subjBody"></tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div id="divlowerpanel" runat="server" visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Create Exam Question Pattern</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" id="divAddPattern" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Subject Name </label>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlCirriculumn" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Selected="True">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Exam Name </label>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlExamName" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Selected="True">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Question Level </label>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlQueLevel" TabIndex="8" CssClass="form-control" data-select2-enable="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlQueLevel_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">1st Level</asp:ListItem>
                                                <asp:ListItem Value="2">2nd Level</asp:ListItem>
                                               <%-- <asp:ListItem Value="3">3rd Level</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtAreaDiscription" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdPatternId" runat="server" />
                                            <asp:HiddenField ID="hdExamPatternSubId" runat="server" />
                                            <asp:HiddenField ID="hdnSectionId" runat="server"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlQueLevel"
                                                Display="None" ErrorMessage="Please Enter Question Level" ValidationGroup="Submit_temp"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Parent Question </label>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlParentQuestion" TabIndex="8" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlParentQuestion_SelectedIndexChanged" AutoPostBack="true"  >
                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Question Or With </label>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlQueOrWith" TabIndex="8" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlQueOrWith_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Display Question No. </label>
                                            </div>
                                            <asp:TextBox ID="txtQuestionNo" runat="server" CssClass="form-control" TabIndex="7"></asp:TextBox>
                                            <%--<input type="text" id="txtMaximumMarks" onkeypress="return isNumberKey(event)" placeholder="Maximum Marks" disabled="disabled" name="txtMaximumMarks" class="form-control" />--%>
                                           
                                            
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Question Description </label>
                                            </div>
                                            <asp:TextBox ID="txtQueDescription" runat="server" CssClass="form-control" TabIndex="7" placeholder="Question Description"></asp:TextBox>
                                            <%--<input type="text" id="txtMaximumMarks" onkeypress="return isNumberKey(event)" placeholder="Maximum Marks" disabled="disabled" name="txtMaximumMarks" class="form-control" />--%>
                                        </div>
                                        <div id="idQmark" runat="server" class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Question Marks </label>
                                            </div>
                                            <asp:TextBox ID="txtQuestionMarks" runat="server" CssClass="form-control" TabIndex="7" ClientIDMode="Static"></asp:TextBox>
                                           
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteValue" runat="server"
                                                FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789." TargetControlID="txtQuestionMarks">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 " id="idminimum" runat="server" visible="false"><%--ADDED ON 311022--%>
                                            <div class="label-dynamic">
                                                <label>Attempt Minimum </label>
                                            </div>
                                            <asp:TextBox ID="txtAttemptMinimum" runat="server" CssClass="form-control" visible="false" TabIndex="7"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789" TargetControlID="txtAttemptMinimum">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="idoutof" runat="server" visible="false"> <%--ADDED ON 311022--%>
                                            <div class="label-dynamic">
                                                <label>Out Of Questions </label>
                                            </div>
                                            <asp:TextBox ID="txtOutOfQuestion" runat="server" visible="false" CssClass="form-control" TabIndex="7"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789" TargetControlID="txtOutOfQuestion">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer" id="dvSubmitData" runat="server">
                                        <asp:HiddenField ID="txtconformmessageValue" runat="server" />
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSave_Click" TabIndex="16" ClientIDMode="Static" />
                                        <asp:Button ID="btnLock" runat="server" CssClass="btn btn-primary" Text="Lock" OnClick="btnLock_Click" TabIndex="17" ClientIDMode="Static" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" TabIndex="18" />
                                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-warning" Text="Back" OnClick="btnBack_Click" TabIndex="19" />
                                       
                                    </div>

                                    <div class="form-group col-lg-6 col-md-12 col-12" id="divFooter" runat="server">
                                        <div class="note-div" id="P1" runat="server">
                                            <h5 class="heading">Note </h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>For edit question please click on question number from the List</span> </p>
                                        </div>
                                    </div>

                                    <input type="hidden" id="hdSchemeSubjectId" value="0" />
                                    <input type="hidden" id="hdExamPatternMappingId" value="0" />
                                    <input type="hidden" id="hdSectionId" value="0" />
                                </div>

                                <div id="divPatternDetails" runat="server" class="col-12">
                                    <div id="dvQuestionPaper" runat="server" style="overflow: scroll; height: 250px;">
                                        <div class="sub-heading">
                                            <h5>Exam Question Pattern</h5>
                                        </div>
                                        <div class="table responsive">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblPatternDetails">
                                                <thead class="bg-light-blue" id="Thead1">
                                                    <tr>
                                                        <%-- @*<th style="display:none">Action</th>*@--%>
                                                        <th class="auto-style1">Question No.</th>
                                                        <th class="auto-style1">Delete
                                                        </th>
                                                        <th class="auto-style1">Pattern Name : 
                                                      &nbsp 
                                                                            <asp:Label ID="lblPatternName" runat="server"></asp:Label>

                                                        </th>
                                                        <%--<th width="5%" class="auto-style3">Attempt</th>--%>
                                                        <th class="auto-style1">
                                                            <asp:Label ID="lblMarks" runat="server" ClientIDMode="Static"></asp:Label>
                                                            &nbsp Marks</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="Tbody2">
                                                    <asp:Repeater ID="rptExamQuestion" runat="server" OnItemDataBound="rptExamQuestion_ItemDataBound">
                                                        <ItemTemplate>

                                                            <tr>
                                                                <td>
                                                                    <%-- <asp:LinkButton ID="lnkPatternEdit" CommandArgument='<%# Eval("QuestionPatternSubID") %>' CommandName="Edit" runat="server" OnClick="lnkPatternEdit" ToolTip='<%# Eval("QuestionPatternSubID")%>' TabIndex="4" Text='<%#String.Format("{0} {1} {2}",Eval("Level1Que"),Eval("Level2Que"),Eval("Level3Que"))%>'><center> </center></asp:LinkButton>--%>
                                                                    <%-- <asp:Label ID="lblQuestionPattern" runat="server" Text='<%#String.Format("{0} {1} {2}",Eval("Level1Que"),Eval("Level2Que"),Eval("Level3Que"))%>'/>--%>
                                                                    <div style="text-align: left">
                                                                        <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("QuestionPatternSubID") %>' CommandName="Edit" runat="server" OnClick="lnkPatternEdit" ToolTip='<%# Eval("QuestionPatternSubID")%>' TabIndex="4" Text='<%#String.Format("{0}",Eval("Level1Que"))%>' Enabled='<%#Convert.ToInt32(Eval("ISLOCKED"))==1?false:true %>' CssClass="clsQueNo"></asp:LinkButton>
                                                                        <asp:HiddenField ID="hdfLevel" runat="server" Value='<%# Eval("Level_Id") %>' />
                                                                    </div>

                                                                    <div style="text-align:center">
                                                                        <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("QuestionPatternSubID") %>' CommandName="Edit" runat="server" OnClick="lnkPatternEdit" ToolTip='<%# Eval("QuestionPatternSubID")%>' TabIndex="4" Text='<%#String.Format("{0}",Eval("Level2Que"))%>' Enabled='<%#Convert.ToInt32(Eval("ISLOCKED"))==1?false:true %>'></asp:LinkButton>
                                                                        <asp:HiddenField ID="hfd" runat="server" Value='<%# Eval("Level_Id") %>' />
                                                                    </div>
                                                                    <div >
                                                                        <asp:LinkButton ID="LinkButton3" CommandArgument='<%# Eval("QuestionPatternSubID") %>' CommandName="Edit" runat="server" OnClick="lnkPatternEdit" ToolTip='<%# Eval("QuestionPatternSubID")%>' TabIndex="4" Text='<%#String.Format("{0}",Eval("Level3Que"))%>'><center> </center></asp:LinkButton>
                                                                    </div>
                                                                    <asp:HiddenField ID="hdSubQuePatternId" runat="server" Value='<%# Eval("QuestionPatternSubID")%>' />
                                                                    <asp:HiddenField ID="hfdLv3" runat="server" Value='<%# Eval("Level_Id") %>' />

                                                                </td>
                                                                <td >
                                                                    <asp:ImageButton ID="btnDeleteQuestion" runat="server" OnClick="btnDeleteQuestion_Click" Enabled='<%#Convert.ToInt32(Eval("ISLOCKED"))==1?false:true %>'
                                                                        CommandArgument='<%# Eval("QuestionPatternSubID") %>' ImageUrl="~/images/delete.gif" ToolTip='<%# Eval("QuestionPatternSubID") %>' OnClientClick="javascript:ConfirmMessage();" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblExamName" runat="server" Text='<%# Eval("Que_Description") %>' />
                                                                    <asp:HiddenField ID="hfdExamName" runat="server" Value='<%# Eval("Level_Id") %>' />
                                                                    <%-- <asp:HiddenField ID="ExamPatternMappingId" runat="server" Value='<%# Eval("ExamPatternMappingId")%>' />--%>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblQuestionMarks" runat="server" Text='<%# Eval("QUESTION_MARKS") %>' CssClass="clsMarks" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblSolveNoOfQuestion" Visible="false" runat="server" Text='<%#String.Format("{0} / {1}",Eval("Solve_no_of_question"),Eval("No_of_question"))%>' />
                                                                </td>


                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                                <%--   <tbody id="subjBody"></tbody>--%>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <%--    <script src="Scripts/ObeTransaction/ExamQuestionPaper.js"></script>--%>

     <script>
         function setstatus(val) {

             $('#rdActive').prop('checked', val);
             // $('#hftimeslot').val($('#rdActivetimeslot').prop('checked'));
         }
         function funStatus() {
             $('#hfStatus').val(Number($('#rdActive').prop('checked')));
         }
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
             $(function () {
                 $('#btnSubmitPatten').click(function () {
                     // alert("hi");
                     funStatus();
                 });
             });
         });


    </script>
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
            if (mainmax != markssum) {
                return 3;
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

            ExamQuestionPaper.Init();

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

    <script>
        $(function () {
            $('#btnLock').click(function () {
                var fMark = parseFloat($('#lblMarks').text());
                var tMark = 0;
                for (var i = 0; i < $('.clsMarks').length; i++) {
                    tMark += parseFloat($('.clsMarks').eq(i).text());
                }

                if (tMark < fMark) {
                    alert('Please Create Pattern of ' + fMark + ' Marks.');
                    return false;
                }

                if (confirm('Are you sure to Lock this Pattern ?'))
                    return true;
                else
                    return false;
            });

            $('#btnSave').click(function () {
                debugger
                var fMark = parseFloat($('#lblMarks').text());
                var tMark = 0;
                var lvlid = 0;
                var qNo;
                var j = 0;
                var k = 0;
                var blank = 0;
                var prevVal = 0;
                var arrMarks = new Array();
                var mLen = $('.clsMarks').length - 1;
                for (var i = mLen; i >= 0; i--) {
                    while (j < 3) {
                        qNo = $('.clsMarks').eq(i).closest('tr').find('td').eq(0).find('div').eq(j).text().trim();
                        j++;
                        if (qNo != '') {
                            if (j == 1) {
                                
                                arrMarks[k] = parseFloat($('.clsMarks').eq(i).text());//qNo;
                                blank = 0;
                                k++;
                                break;
                            }
                        }
                        else {
                            blank = blank + 1;
                        }
                    }

                    if (blank == 3) {
                        arrMarks[k] = 0;
                        k++;
                    }

                    j = 0;
                }

                for (var i = 0; i < arrMarks.length; i++) {
                    if (arrMarks[i] != 0) {
                        prevVal = arrMarks[i];
                        tMark += arrMarks[i];
                    }
                    else {
                        tMark -= prevVal;
                    }
                }

                var QMark = 0;
                tMark += parseFloat($('#txtQuestionMarks').val());
                QMark += parseFloat($('#txtQuestionMarks').val());
                lvlid += parseFloat($('#ddlQueLevel').val());
                debugger
                var txtmin=0;
                var txtoutof=0;
                //if (lvlid == 1)
                //{
                    if (QMark > fMark)
                    {
                        alert('Question Marks can not greater than ' + fMark + ' Marks.');
                        return false;
                    }

                //}
                    //var orwith = parseFloat($('#ddlQueOrWith').val());//added on 03082023
                   // if (orwith < 1)
                   // {
                    if (lvlid == 1)
                    {
                        if (tMark > fMark)
                        {
                            if (orwith > 1)
                            {
                            }
                            else
                            {
                                alert('Question Total should not Greater than Maximum Marks.');
                                return false;
                            }
                         }
                     }
                   // }
            });

        });




        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnLock').click(function () {
                    var fMark = parseFloat($('#lblMarks').text());
                    var tMark = 0;
                    for (var i = 0; i < $('.clsMarks').length; i++) {
                        tMark += parseFloat($('.clsMarks').eq(i).text());
                    }

                    if (tMark < fMark) {
                        alert('Please Create Pattern of ' + fMark + ' Marks.');
                        return false;
                    }

                    if (confirm('Are you sure to Lock this Pattern ?'))
                        return true;
                    else
                        return false;
                });

                $('#btnSave').click(function () {
                    debugger
                    var fMark = parseFloat($('#lblMarks').text());
                    var tMark = 0;
                    var qNo;
                    var j = 0;
                    var k = 0;
                    var blank = 0;
                    var prevVal = 0;
                    var arrMarks = new Array();
                    var mLen = $('.clsMarks').length - 1;
                    for (var i = mLen; i >= 0; i--) {
                        while (j < 3) {
                            //qNo = $('.clsMarks').eq(i).closest('tr').find('td').eq(0).find('div').eq(j).text().trim();
                            qNo = $('.clsMarks').eq(i).closest('tr').find('td').eq(0).find('div').eq(j).text().trim();
                            j++;
                            if (qNo != '') {
                                if (j == 1) {
                                    debugger
                                    if (localStorage.getItem("lstQueNo") != $('.clsQueNo').eq(i).attr('title')) {
                                        arrMarks[k] = parseFloat($('.clsMarks').eq(i).text());//qNo;
                                        blank = 0;
                                        k++;
                                        break;
                                    }
                                }
                            }
                            else {
                                blank = blank + 1;
                            }
                        }

                        if (blank == 3) {
                            arrMarks[k] = 0;
                            k++;
                        }

                        j = 0;
                    }

                    for (var i = 0; i < arrMarks.length; i++) {
                        if (arrMarks[i] != 0) {
                            prevVal = arrMarks[i];
                            tMark += arrMarks[i];
                        }
                        else {
                            tMark -= prevVal;
                        }
                    }

                    if ($('#ddlQueLevel').val() == 1) {
                        if ($('#txtQuestionMarks').val() == 0) {
                            alert('Question Mark should not be Zero');
                            return false;
                        }
                        tMark += parseFloat($('#txtQuestionMarks').val());
                    }
                  
                    var orwith = parseFloat($('#ddlQueOrWith').val());//added on 03082023
                    if (orwith < 1)
                    {
                        if (tMark > fMark) {
                            alert('Question Total (' + tMark + ') should not Greater than Maximum Marks (' + fMark + ').');
                            return false;
                        }
                    }
                    localStorage.setItem("lstQueNo", "0");
                });

                $('.clsQueNo').click(function () {
                    localStorage.setItem("lstQueNo", $(this).attr('title'));
                });

            });
        });
    </script>
    <script>
        function ConfirmMessage() {
            var selectedvalue = confirm("Do you want to Delete Record?");
            if (selectedvalue) {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "No";
            }
        }

    </script>





</asp:Content>

