<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="QuestionBankMaster.aspx.cs" Inherits="ITLE_QuestionBankMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>
    <script src="../plugins/ckeditor/config.js"></script>
    <%-- <script src="../includes/modalbox.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>
    <link href="../CSS/master.css" rel="stylesheet" />--%>
    <%-- <link href="../CSS/ModalPopup.css" rel="stylesheet" />--%>
    <%--  <link href="../CSS/jquery-ui.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        //This is used for virtual keyboard
        function InsertSymbol(divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";

            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
            }
        }
    </script>
    <%--  <script src="../jquery/jquery-1.9.1.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        window.onload = function () {
            document.getElementById('<%=txtTopic.ClientID%>').focus();
        }
        function validate() {
            document.getElementById('txtQuestion').focus();
        }
        function MathEditor() {
            window.open("ITLE_MathEditor.aspx", "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=500, left=500, width=800, height=400");

        }

        function checkForComma(keyCode) {
            if (event.keyCode == 188) {
                debugger;
                alert('Comma(,)Not allowed');

                document.getElementById('<%=txtNewTopic.ClientID%>').value = "";
            }
        }
    </script>

    <script type="text/javascript">

        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
        .input-group .input-group-addon {
            padding: 6px 12px;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">QUESTION BANK</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlQuestionBankMaster" runat="server">
                        <asp:Panel ID="pnlAdd" runat="server">

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlCourse" runat="server">
                                        <div class="col-12 mb-3">
                                            <div class="row">
                                                <div class="col-12 btn-footer">
                                                    <asp:Label ID="lblDeleteStatus" runat="server" Font-Bold="true"
                                                        Text="Can't Delete, Question Is Selected For Test" Visible="false"></asp:Label>
                                                </div>
                                                <div class="col-lg-12 col-md-12 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Course Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblCourseName" Font-Bold="true" runat="server"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">

                                            <label>Select the Type of Question</label>
                                        </div>
                                        <asp:RadioButtonList ID="rbnObjectiveDescriptive" Font-Bold="true" AutoPostBack="true"
                                            runat="server" RepeatDirection="Horizontal" TabIndex="1" ToolTip="Select Question Type"
                                            OnSelectedIndexChanged="rbnObjectiveDescriptive_SelectedIndexChanged" Width="216px">
                                            <asp:ListItem Text="Objective" Value="O" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Descriptive" Value="D"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Enter Topic Name</label>
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtNewTopic" runat="server" CssClass="form-control" TabIndex="2"
                                                ToolTip="Enter Topic Name" onkeyup="return checkForComma(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTopic" runat="server" ControlToValidate="txtNewTopic"
                                                Display="None" ErrorMessage="Please Enter Topic For Question" SetFocusOnError="true"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            <div class="input-group-addon">
                                                <%--<i id="dvcal2" runat="server" class="fa fa-calendar text-blue"></i>--%>
                                                <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/search-svg.png" Style="width: 85%;"
                                                    OnClientClick="return ShowModalPopup()" ToolTip="Search Exisiting Topic" TabIndex="3" />
                                                <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                                                <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mpe" runat="server"
                                                    PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground">
                                                </ajaxToolKit:ModalPopupExtender>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopupTopic" Style="display: none; background-color: #fff; box-shadow: 0 0 8px #636060; padding: 15px;">
                                        <div class="sub-heading">
                                            <h5>Search Topic</h5>
                                        </div>
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class="label-dynamic">
                                                <label>Enter Existing Topic Name</label>
                                            </div>
                                            <asp:TextBox ID="txtTopic" runat="server" AutoPostBack="true" TabIndex="4"
                                                ToolTip="Enter Existing Topic Name" CssClass="form-control"
                                                OnTextChanged="txtTopic_TextChanged"></asp:TextBox>
                                            <asp:HiddenField ID="hfTopic" runat="server" />
                                            <ajaxToolKit:AutoCompleteExtender ID="txtTopic_AutoCompleteExtender" runat="server"
                                                Enabled="True" TargetControlID="txtTopic"
                                                CompletionSetCount="6" OnClientItemSelected="IAmSelected" OnClientShowing="clientShowing"
                                                ServiceMethod="GetTopicName" MinimumPrefixLength="1" CompletionInterval="1000"
                                                CompletionListCssClass="autocomplete_completionListElement" UseContextKey="true"
                                                CompletionListItemCssClass="autocomplete_listItem"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </ajaxToolKit:AutoCompleteExtender>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnHide" runat="server" Text="Exit" CssClass="btn btn-primary" TabIndex="5"
                                                OnClientClick="return HideModalPopup()" ToolTip="Click here to Exit" />
                                        </div>
                                    </asp:Panel>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trTextImage" runat="server">
                                        <div class="label-dynamic">
                                            <label>Select an option</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdbtnList" AutoPostBack="true" runat="server" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rdbtnList_SelectedIndexChanged" ToolTip="Select An Option" TabIndex="6" Width="181px">
                                            <asp:ListItem Text="Text" Value="T" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Image" Value="I"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-12">
                                        <div class="form-group col-lg-12 col-md-12 col-12 pl-0 pr-0">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Question</label>
                                            </div>
                                            <CKEditor:CKEditorControl ID="txtQuestion" runat="server" Height="200" CssClass="form-control" TabIndex="7"
                                                BasePath="~/plugins/ckeditor"></CKEditor:CKEditorControl>
                                            <%--<FTB:FreeTextBox ID="txtQuestion" runat="server" Height="180px" Width="450px" 
                                                TabIndex="4" Focus ="true" Text="&nbsp;">
                                                </FTB:FreeTextBox>--%>
                                            <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" ControlToValidate="txtQuestion"
                                                Display="None" ErrorMessage="Please Enter Question" SetFocusOnError="true" ValidationGroup="Submit">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-lg-8 col-md-8 col-12">
                                                <div class="label-dynamic">
                                                    <label id="lblInsertSymbol" onclick="javascript:InsertSymbol('divKeyBoard')" tabindex="6">Click here to insert Symbols</label>
                                                    <img id="btnSymbol" runat="server" alt="Insert Symbol" visible="true" src="../Images/RedOmega.jpeg" tabindex="8"
                                                        style="width: 20px; height: 20px" onclick="javascript:InsertSymbol('divKeyBoard')" />
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-4 col-12 text-center">
                                                <asp:Button ID="btnMathEditor" runat="server" Text="Math Equation" CssClass="btn btn-primary"
                                                    OnClientClick="MathEditor();" TabIndex="9" ToolTip="Click here for Math Equation" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-4 col-md-8 col-12">
                                            <div class="label-dynamic">
                                            </div>
                                            <asp:Image ID="imgQuestions" runat="server" TabIndex="10" Height="128px" Width="128px" onclick="DisplayQuestionInWidnow();" />
                                            <%--<asp:Image ID="imgQuestions" runat="server" Height="128px" Width="128px" 
                                                                    onclick="DisplayQuestionInWidnow();" />--%>
                                            <asp:FileUpload ID="fuQuestion" runat="server" onChange="LoadQuestion()" TabIndex="11" />
                                            <script type="text/javascript">
                                                function readURLSign(input) {
                                                    if (input.files && input.files[0]) {
                                                        var reader = new FileReader();

                                                        reader.onload = function (e) {
                                                            $('#ctl00_ContentPlaceHolder1_imgQuestions').attr('src', e.target.result);
                                                        }

                                                        reader.readAsDataURL(input.files[0]);
                                                    }
                                                }

                                                $("#ctl00_ContentPlaceHolder1_fuQuestion").change(function () {
                                                    readURLSign(this);
                                                });


                                            </script>
                                        </div>

                                    </div>
                                    <div class="form-group col-lg-12 col-md-12 col-12" id="divKeyBoard" style="display: none">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <iframe src="Virtual_Keyboard.htm" width="100%" height="230px"></iframe>

                                    </div>

                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12" id="Answer1" runat="server">
                                        <div class="label-dynamic">

                                            <label>Answer 1 </label>
                                        </div>
                                        <CKEditor:CKEditorControl ID="txtAnswer1" runat="server" Height="50" BasePath="~/plugins/ckeditor"
                                            AutoParagraph="false" IgnoreEmptyParagraph="true" CssClass="form-control" TabIndex="8"
                                            ToolbarStartupExpanded="false"></CKEditor:CKEditorControl>
                                        <%--  <FTB:FreeTextBox ID="txtAnswer1" runat="server" Height="80px" Width="170px" TabIndex="4"
                                                                    EnableToolbars="false" EnableHtmlMode="false" >
                                                                </FTB:FreeTextBox>--%>
                                        <asp:RequiredFieldValidator ID="rfvtxtAnswer1" runat="server" ControlToValidate="txtAnswer1"
                                            Display="None" ErrorMessage="Please Enter Answer1" SetFocusOnError="true"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <asp:Image ID="imgAnswer1" runat="server" Height="128px" Width="128px"
                                            onclick="DisplayAns1InWidnow();" />
                                        <asp:FileUpload ID="fuAnswer1" runat="server" onChange="LoadAns1()" TabIndex="12" />

                                        <script type="text/javascript">
                                            function readURLAnswer1(input) {

                                                if (input.files && input.files[0]) {
                                                    var reader = new FileReader();

                                                    reader.onload = function (e) {
                                                        $('#ctl00_ContentPlaceHolder1_imgAnswer1').attr('src', e.target.result);
                                                    }

                                                    reader.readAsDataURL(input.files[0]);
                                                }
                                            }

                                            $("#ctl00_ContentPlaceHolder1_fuAnswer1").change(function () {
                                                readURLAnswer1(this);
                                            });


                                        </script>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12" id="Answer2" runat="server">
                                        <div class="label-dynamic">

                                            <label>Answer 2 </label>
                                        </div>
                                        <CKEditor:CKEditorControl ID="txtAnswer2" runat="server" Height="50" BasePath="~/plugins/ckeditor" TabIndex="9"
                                            AutoParagraph="false" IgnoreEmptyParagraph="true" ToolbarStartupExpanded="false"></CKEditor:CKEditorControl>

                                        <asp:RequiredFieldValidator ID="rfvAnswer2" runat="server" ControlToValidate="txtAnswer2"
                                            Display="None" ErrorMessage="Please Enter Answer2" SetFocusOnError="true"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <asp:Image ID="imgAnswer2" runat="server" Height="128px" Width="128px"
                                            onclick="DisplayAns2InWidnow();" />
                                        <asp:FileUpload ID="fuAnswer2" runat="server" onChange="LoadAns2()" />

                                        <script type="text/javascript">
                                            function readURLAnswer2(input) {

                                                if (input.files && input.files[0]) {
                                                    var reader = new FileReader();

                                                    reader.onload = function (e) {
                                                        $('#ctl00_ContentPlaceHolder1_imgAnswer2').attr('src', e.target.result);
                                                    }

                                                    reader.readAsDataURL(input.files[0]);
                                                }
                                            }

                                            $("#ctl00_ContentPlaceHolder1_fuAnswer2").change(function () {
                                                readURLAnswer2(this);
                                            });


                                        </script>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12" id="Answer3" runat="server">
                                        <div class="label-dynamic">
                                            <label>Answer 3 </label>
                                        </div>
                                        <CKEditor:CKEditorControl ID="txtAnswer3" runat="server" Height="50" BasePath="~/plugins/ckeditor" TabIndex="10"
                                            AutoParagraph="false" IgnoreEmptyParagraph="true" ToolbarStartupExpanded="false"></CKEditor:CKEditorControl>
                                        <%-- <FTB:FreeTextBox ID="txtAnswer3" runat="server" Height="80px" Width="170px" TabIndex="4"
                                                                            EnableToolbars="false" EnableHtmlMode="false">
                                                                        </FTB:FreeTextBox>--%>
                                        <asp:Image ID="imgAnswer3" runat="server" Height="128px" Width="128px"
                                            onclick="DisplayAns3InWidnow();" />
                                        <asp:FileUpload ID="fuAnswer3" onChange="LoadAns3()" runat="server" />
                                        <script type="text/javascript">
                                            function readURLAnswer3(input) {

                                                if (input.files && input.files[0]) {
                                                    var reader = new FileReader();

                                                    reader.onload = function (e) {
                                                        $('#ctl00_ContentPlaceHolder1_imgAnswer3').attr('src', e.target.result);
                                                    }

                                                    reader.readAsDataURL(input.files[0]);
                                                }
                                            }

                                            $("#ctl00_ContentPlaceHolder1_fuAnswer3").change(function () {
                                                readURLAnswer3(this);
                                            });


                                        </script>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12" id="Answer4" runat="server">
                                        <div class="label-dynamic">
                                            <label>Answer 4</label>
                                        </div>
                                        <CKEditor:CKEditorControl ID="txtAnswer4" runat="server" Height="50" BasePath="~/plugins/ckeditor" TabIndex="11"
                                            AutoParagraph="false" IgnoreEmptyParagraph="true" ToolbarStartupExpanded="false"></CKEditor:CKEditorControl>
                                        <%--<FTB:FreeTextBox ID="txtAnswer4" runat="server" Height="80px" Width="170px" TabIndex="4"
                                                                    EnableToolbars="false" EnableHtmlMode="false">
                                                                </FTB:FreeTextBox>--%>
                                        <asp:Image ID="imgAnswer4" runat="server" Height="128px" Width="128px"
                                            onclick="DisplayAns4InWidnow();" />
                                        <asp:FileUpload ID="fuAnswer4" onChange="LoadAns4()" runat="server" />

                                        <script type="text/javascript">
                                            function readURLAnswer4(input) {

                                                if (input.files && input.files[0]) {
                                                    var reader = new FileReader();

                                                    reader.onload = function (e) {
                                                        $('#ctl00_ContentPlaceHolder1_imgAnswer4').attr('src', e.target.result);
                                                    }

                                                    reader.readAsDataURL(input.files[0]);
                                                }
                                            }

                                            $("#ctl00_ContentPlaceHolder1_fuAnswer4").change(function () {
                                                readURLAnswer4(this);
                                            });


                                        </script>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12" id="Answer5" runat="server">
                                        <div class="label-dynamic">
                                            <label>Answer 5 </label>
                                        </div>
                                        <CKEditor:CKEditorControl ID="txtAnswer5" runat="server" Height="50" BasePath="~/plugins/ckeditor" TabIndex="12"
                                            AutoParagraph="false" IgnoreEmptyParagraph="true" ToolbarStartupExpanded="false"></CKEditor:CKEditorControl>
                                        <%--<FTB:FreeTextBox ID="txtAnswer5" runat="server" Height="80px" Width="170px" TabIndex="4"
                                                                    EnableToolbars="false" EnableHtmlMode="false">
                                                                </FTB:FreeTextBox>--%>
                                        <asp:Image ID="imgAnswer5" runat="server" Height="128px" Width="128px"
                                            onclick="DisplayAns5InWidnow();" />
                                        <asp:FileUpload ID="fuAnswer5" onChange="LoadAns5()" runat="server" />

                                        <script type="text/javascript">
                                            function readURLAnswer5(input) {

                                                if (input.files && input.files[0]) {
                                                    var reader = new FileReader();

                                                    reader.onload = function (e) {
                                                        $('#ctl00_ContentPlaceHolder1_imgAnswer5').attr('src', e.target.result);
                                                    }

                                                    reader.readAsDataURL(input.files[0]);
                                                }
                                            }

                                            $("#ctl00_ContentPlaceHolder1_fuAnswer5").change(function () {
                                                readURLAnswer5(this);
                                            });


                                        </script>
                                    </div>


                                    <div class="form-group col-lg-6 col-md-6 col-12" id="Answer6" runat="server">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Answer 6</label>
                                        </div>
                                        <CKEditor:CKEditorControl ID="txtAnswer6" runat="server" Height="50" BasePath="~/plugins/ckeditor" TabIndex="13"
                                            AutoParagraph="false" IgnoreEmptyParagraph="true" ToolbarStartupExpanded="false"></CKEditor:CKEditorControl>
                                        <%--<FTB:FreeTextBox ID="txtAnswer6" runat="server" TabIndex="4" Height="80px" Width="170px"
                                                                    EnableToolbars="false" EnableHtmlMode="false">
                                                                </FTB:FreeTextBox>--%>
                                        <asp:Image ID="imgAnswer6" runat="server" Height="128px" Width="128px"
                                            onclick="DisplayAns6InWidnow();" />
                                        <%--<asp:Image ID="imgAnswer6" runat="server" Height="128px" Width="128px"
                                                                    onclick="DisplayAns6InWidnow();" />--%>
                                        <asp:FileUpload ID="fuAnswer6" onChange="LoadAns6()" runat="server" />

                                        <script type="text/javascript">
                                            function readURLAnswer6(input) {

                                                if (input.files && input.files[0]) {
                                                    var reader = new FileReader();

                                                    reader.onload = function (e) {
                                                        $('#ctl00_ContentPlaceHolder1_imgAnswer6').attr('src', e.target.result);
                                                    }

                                                    reader.readAsDataURL(input.files[0]);
                                                }
                                            }

                                            $("#ctl00_ContentPlaceHolder1_fuAnswer6").change(function () {
                                                readURLAnswer6(this);
                                            });


                                        </script>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="CorrectAnwer" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Correct Answer</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCorrectAns" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="14"
                                            AppendDataBoundItems="true" ToolTip="Select Correct Answer">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Answer 1</asp:ListItem>
                                            <asp:ListItem Value="2">Answer 2</asp:ListItem>
                                            <asp:ListItem Value="3">Answer 3</asp:ListItem>
                                            <asp:ListItem Value="4">Answer 4</asp:ListItem>
                                            <asp:ListItem Value="5">Answer 5</asp:ListItem>
                                            <asp:ListItem Value="6">Answer 6</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCorrectAns" runat="server" ControlToValidate="ddlCorrectAns"
                                            Display="None" InitialValue="0" ErrorMessage="Please Enter Correct Answer" SetFocusOnError="true"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="MarksOnQuestion" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Select Marks for the Question </label>
                                        </div>
                                        <asp:DropDownList ID="ddlQuestionMarks" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="15"
                                            AppendDataBoundItems="true" ToolTip="Select Marks for the Question">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1"> 1</asp:ListItem>
                                            <asp:ListItem Value="2"> 2</asp:ListItem>
                                            <asp:ListItem Value="3"> 3</asp:ListItem>
                                            <asp:ListItem Value="4"> 4</asp:ListItem>
                                            <asp:ListItem Value="5"> 5</asp:ListItem>
                                            <asp:ListItem Value="6"> 6</asp:ListItem>
                                            <asp:ListItem Value="7"> 7</asp:ListItem>
                                            <asp:ListItem Value="8"> 8</asp:ListItem>
                                            <asp:ListItem Value="9"> 9</asp:ListItem>
                                            <asp:ListItem Value="10"> 10</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvQuestionMarks" runat="server" ControlToValidate="ddlQuestionMarks"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Marks for the Answer of this Question"
                                            SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <div class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Save" ValidationGroup="Submit" TabIndex="16"
                                        CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                    &nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" TabIndex="17"
                                        CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Submit" />
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="text-center">
                                    <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#660066"></asp:Label>
                                </div>

                            </div>


                        </asp:Panel>
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Questions List</h5>
                            </div>

                            <asp:Panel ID="pnllvView" runat="server">
                                <asp:ListView ID="lvQuestions" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>SrNo</th>
                                                    <th>Action</th>
                                                    <th>Question Text</th>
                                                    <th>Topic</th>
                                                    <th>Q.Marks</th>
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
                                                <%# Container.DataItemIndex + 1%>
                                            </td>

                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit1.png"
                                                    CommandArgument='<%# Eval("QUESTIONNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="19"
                                                    OnClick="btnEdit_Click" />&nbsp;&nbsp;
                                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" TabIndex="20"
                                                                            CommandArgument='<%# Eval("QUESTIONNO") %>'
                                                                            ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                            </td>
                                            <td>
                                                <%# Eval("QUESTIONTEXT")%>
                                            </td>
                                            <td>
                                                <%# Eval("TOPIC") %>
                                            </td>

                                            <td>
                                                <%# Eval("MARKS_FOR_QUESTION")%>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div class="text-center">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" />
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                    <div class="text-center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                    </div>
                </div>
            </div>
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
    </script>

    <script type="text/javascript">
        function LoadImage() {
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").src = document.getElementById("ctl00_ContentPlaceHolder1_fuQuestion").value;
        }
        function LoadQuestion() {
            var ctrlUpload = document.getElementById("ctl00_ContentPlaceHolder1_fuQuestion");
            if (ctrlUpload.value == '') {
                alert("Select a file first!");
                ctrlUpload.focus();
                return false;
            }
            var extensionList = new Array(".jpg", ".gif");
            var extension = ctrlUpload.value.slice(ctrlUpload.value.indexOf(".")).toLowerCase();
            for (var i = 0; i < extensionList.length; i++) {
                if (extensionList[i] == extension) {

                    //document.getElementById("ctl00_ContentPlaceHolder1_imgQuestions").src = document.getElementById("ctl00_ContentPlaceHolder1_fuQuestion").value;

                    return true;

                }
                else {
                    //alert("You can only upload below File Type:\n\n" + extensionList.join("\n"));
                    //document.getElementById("ctl00_ContentPlaceHolder1_imgQuestions").src=null;
                    ctrlUpload.focus();
                    return false;
                }
            }

            var size = document.getElementById("ctl00_ContentPlaceHolder1_fuQuestion").fileSize;

            //if(size<=20000)
            //{
            //return true;
            //}
            //else
            //{
            //alert("You can only upload below File upto 15kb only");

            //return false;
            //}

        }


        function LoadAns1() {
            //  var size = document.getElementById("ctl00_ContentPlaceHolder1_fuAnswer1").fileSize;

            var ctrlUpload = document.getElementById("ctl00_ContentPlaceHolder1_fuAnswer1");

            //Does the user browse or select a file or not
            if (ctrlUpload.value == '') {
                alert("Select a file first!");
                ctrlUpload.focus();
                return false;
            }

            //Extension List for validation. Add your required extension here with comma separator
            var extensionList = new Array(".jpg", ".gif");
            //Get Selected file extension
            var extension = ctrlUpload.value.slice(ctrlUpload.value.indexOf(".")).toLowerCase();

            //Check file extension with your required extension list.
            for (var i = 0; i < extensionList.length; i++) {
                if (extensionList[i] == extension) {
                    // if(size<=2000)
                    //{
                    //  //document.getElementById("ctl00_ContentPlaceHolder1_imgAnswer1").src = document.getElementById("ctl00_ContentPlaceHolder1_fuAnswer1").value;

                    return true;
                    //}
                    //else
                    //{
                    //alert("You can upload file upto 20kb only");
                    //return false;
                    //}
                }
                else {
                    //alert("You can only upload below File Type:\n\n" + extensionList.join("\n"));
                    return false;

                }
            }
            //document.getElementById("ctl00_ContentPlaceHolder1_imgQuestions").src=null;
            ctrlUpload.focus();



        }
        function LoadAns2() {
            // var size = document.getElementById("ctl00_ContentPlaceHolder1_imgAnswer2").fileSize;

            var ctrlUpload = document.getElementById("ctl00_ContentPlaceHolder1_fuAnswer2");

            //Does the user browse or select a file or not
            if (ctrlUpload.value == '') {
                alert("Select a file first!");
                ctrlUpload.focus();
                return false;
            }

            var extensionList = new Array(".jpg", ".gif");
            var extension = ctrlUpload.value.slice(ctrlUpload.value.indexOf(".")).toLowerCase();

            //Check file extension with your required extension list.
            for (var i = 0; i < extensionList.length; i++) {
                if (extensionList[i] == extension) {
                    //if(size<=2000)
                    //{
                    //  document.getElementById("ctl00_ContentPlaceHolder1_imgAnswer2").src = document.getElementById("ctl00_ContentPlaceHolder1_fuAnswer2").value;

                    return true;
                    //}
                }
                else {
                    //alert("You can only upload below File Type:\n\n" + extensionList.join("\n"));
                    //document.getElementById("ctl00_ContentPlaceHolder1_imgQuestions").src=null;
                    ctrlUpload.focus();
                    return false;
                }
            }

        }

        function LoadAns3() {
            //  var size = document.getElementById("ctl00_ContentPlaceHolder1_imgAnswer3").fileSize;

            var ctrlUpload = document.getElementById("ctl00_ContentPlaceHolder1_fuAnswer3");

            //Does the user browse or select a file or not
            if (ctrlUpload.value == '') {
                alert("Select a file first!");
                ctrlUpload.focus();
                return false;
            }

            //Extension List for validation. Add your required extension here with comma separator
            var extensionList = new Array(".jpg", ".gif");
            //Get Selected file extension
            var extension = ctrlUpload.value.slice(ctrlUpload.value.indexOf(".")).toLowerCase();

            //Check file extension with your required extension list.
            for (var i = 0; i < extensionList.length; i++) {
                if (extensionList[i] == extension) {
                    //if(size<=2000)
                    //{
                    //                document.getElementById("ctl00_ContentPlaceHolder1_imgAnswer3").src = document.getElementById("ctl00_ContentPlaceHolder1_fuAnswer3").value;

                    return true;

                    //}
                }
                else {
                    //alert("You can only upload below File Type:\n\n" + extensionList.join("\n"));
                    ctrlUpload.focus();
                    return false;
                }
            }
            //document.getElementById("ctl00_ContentPlaceHolder1_imgQuestions").src=null;


        }
        function LoadAns4() {
            // var size = document.getElementById("ctl00_ContentPlaceHolder1_imgAnswer4").fileSize;

            var ctrlUpload = document.getElementById("ctl00_ContentPlaceHolder1_fuAnswer4");

            //Does the user browse or select a file or not
            if (ctrlUpload.value == '') {
                alert("Select a file first!");
                ctrlUpload.focus();
                return false;
            }

            //Extension List for validation. Add your required extension here with comma separator
            var extensionList = new Array(".jpg", ".gif");
            //Get Selected file extension
            var extension = ctrlUpload.value.slice(ctrlUpload.value.indexOf(".")).toLowerCase();

            //Check file extension with your required extension list.
            for (var i = 0; i < extensionList.length; i++) {
                if (extensionList[i] == extension) {
                    //if(size<=2000)
                    //{
                    //           document.getElementById("ctl00_ContentPlaceHolder1_imgAnswer4").src = document.getElementById("ctl00_ContentPlaceHolder1_fuAnswer4").value;

                    return true;
                    //}
                }
                else {
                    //alert("You can only upload below File Type:\n\n" + extensionList.join("\n"));
                    //document.getElementById("ctl00_ContentPlaceHolder1_imgQuestions").src=null;
                    ctrlUpload.focus();
                    return false;
                }
            }

        }

        function LoadAns5() {
            // var size = document.getElementById("ctl00_ContentPlaceHolder1_imgAnswer4").fileSize;

            var ctrlUpload = document.getElementById("ctl00_ContentPlaceHolder1_fuAnswer5");

            //Does the user browse or select a file or not
            if (ctrlUpload.value == '') {
                alert("Select a file first!");
                ctrlUpload.focus();
                return false;
            }

            //Extension List for validation. Add your required extension here with comma separator
            var extensionList = new Array(".jpg", ".gif");
            //Get Selected file extension
            var extension = ctrlUpload.value.slice(ctrlUpload.value.indexOf(".")).toLowerCase();

            //Check file extension with your required extension list.
            for (var i = 0; i < extensionList.length; i++) {
                if (extensionList[i] == extension) {
                    //if(size<=2000)
                    //{
                    //           document.getElementById("ctl00_ContentPlaceHolder1_imgAnswer4").src = document.getElementById("ctl00_ContentPlaceHolder1_fuAnswer4").value;

                    return true;
                    //}
                }
                else {
                    // alert("You can only upload below File Type:\n\n" + extensionList.join("\n"));
                    //document.getElementById("ctl00_ContentPlaceHolder1_imgQuestions").src=null;
                    ctrlUpload.focus();
                    return false;
                }
            }

        }

        function LoadAns6() {
            // var size = document.getElementById("ctl00_ContentPlaceHolder1_imgAnswer4").fileSize;

            var ctrlUpload = document.getElementById("ctl00_ContentPlaceHolder1_fuAnswer6");

            //Does the user browse or select a file or not
            if (ctrlUpload.value == '') {
                alert("Select a file first!");
                ctrlUpload.focus();
                return false;
            }

            //Extension List for validation. Add your required extension here with comma separator
            var extensionList = new Array(".jpg", ".gif");
            //Get Selected file extension
            var extension = ctrlUpload.value.slice(ctrlUpload.value.indexOf(".")).toLowerCase();

            //Check file extension with your required extension list.
            for (var i = 0; i < extensionList.length; i++) {
                if (extensionList[i] == extension) {
                    //if(size<=2000)
                    //{
                    //           document.getElementById("ctl00_ContentPlaceHolder1_imgAnswer4").src = document.getElementById("ctl00_ContentPlaceHolder1_fuAnswer4").value;

                    return true;
                    //}
                }
                else {
                    //alert("You can only upload below File Type:\n\n" + extensionList.join("\n"));
                    //document.getElementById("ctl00_ContentPlaceHolder1_imgQuestions").src=null;
                    ctrlUpload.focus();
                    return false;
                }
            }
        }

        function IAmSelected(source, eventArgs) {
            //var idno=eventArgs.get_value().split("*");
            // var Name=idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtTopic').value = Name[0];
            document.getElementById('ctl00_ContentPlaceHolder1_txtNewTopic').value = Name[0];
            // document.getElementById('ctl00_ContentPlaceHolder1_hfTopic').value = idno[1];            
        }




        function DisplayQuestionInWidnow() {
            var img = document.getElementById('<%= imgQuestions.ClientID %>').src;
            var l = (screen.width - 400) / 2;
            var t = (screen.height - 400) / 2;

            html = "<HTML><HEAD><TITLE>Question1</TITLE>"
        + "</HEAD><BODY LEFTMARGIN=0 "
        + "MARGINWIDTH=0 TOPMARGIN=0 MARGINHEIGHT=0><CENTER>"
        + "<IMG src='"
        + img
        + "' BORDER=0 NAME=image "
        + "onload='window.resizeTo(250,250)'>"
        + "</CENTER>"
        + "</BODY></HTML>";
            popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,left=200,top=200,menuBar=0,scrollbars=0,resizable=1');
            popup.document.open();
            popup.document.write(html);
            popup.document.focus();
            popup.document.close();
        }

        function DisplayAns1InWidnow() {
            var img = document.getElementById('<%= imgAnswer1.ClientID %>').src;
            var l = (screen.width - 475) / 2;
            var t = (screen.height - 500) / 2;

            html = "<HTML><HEAD><TITLE>Answer1</TITLE>"
        + "</HEAD><BODY LEFTMARGIN=0 "
        + "MARGINWIDTH=0 TOPMARGIN=0 MARGINHEIGHT=0><CENTER>"
        + "<IMG src='"
        + img
        + "' BORDER=0 NAME=image "
         + "onload='window.resizeTo(document.image.width,document.image.height)'>"
        + "</CENTER>"
        + "</BODY></HTML>";
            popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,left=200,top=200,menuBar=0,scrollbars=0,resizable=1');
            popup.document.open();
            popup.document.write(html);
            popup.document.focus();
            popup.document.close();
            popup.document.close();
        }

        function DisplayAns2InWidnow() {
            var img = document.getElementById('<%= imgAnswer2.ClientID %>').src;

            html = "<HTML><HEAD><TITLE>Answer2</TITLE>"
        + "</HEAD><BODY LEFTMARGIN=0 "
        + "MARGINWIDTH=0 TOPMARGIN=0 MARGINHEIGHT=0><CENTER>"
        + "<IMG src='"
        + img
        + "' BORDER=0 NAME=image "
        + "onload='window.resizeTo(document.image.width,document.image.height)'>"
        + "</CENTER>"
        + "</BODY></HTML>";
            popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,left=200,top=200,menuBar=0,scrollbars=0,resizable=1');
            popup.document.open();
            popup.document.write(html);
            popup.document.focus();
            popup.document.close();
        }

        function DisplayAns3InWidnow() {
            var img = document.getElementById('<%= imgAnswer3.ClientID %>').src;

            html = "<HTML><HEAD><TITLE>Answer3</TITLE>"
        + "</HEAD><BODY LEFTMARGIN=0 "
        + "MARGINWIDTH=0 TOPMARGIN=0 MARGINHEIGHT=0><CENTER>"
        + "<IMG src='"
        + img
        + "' BORDER=0 NAME=image "
        + "onload='window.resizeTo(document.image.width,document.image.height)'>"
        + "</CENTER>"
        + "</BODY></HTML>";
            popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,menuBar=0,left=200,top=200,scrollbars=0,resizable=1');
            popup.document.open();
            popup.document.write(html);
            popup.document.focus();
            popup.document.close();
        }

        function DisplayAns4InWidnow() {
            var img = document.getElementById('<%= imgAnswer4.ClientID %>').src;

            html = "<HTML><HEAD><TITLE>Answer4</TITLE>"
        + "</HEAD><BODY LEFTMARGIN=0 "
        + "MARGINWIDTH=0 TOPMARGIN=0 MARGINHEIGHT=0><CENTER>"
        + "<IMG src='"
        + img
        + "' BORDER=0 NAME=image "
        + "onload='window.resizeTo(document.image.width,document.image.height)'>"
        + "</CENTER>"
        + "</BODY></HTML>";
            popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,left=200,top=200,menuBar=0,scrollbars=0,resizable=1');
            popup.document.open();
            popup.document.write(html);
            popup.document.focus();
            popup.document.close();
        }

        function DisplayAns5InWidnow() {
            var img = document.getElementById('<%= imgAnswer5.ClientID %>').src;

            html = "<HTML><HEAD><TITLE>Answer5</TITLE>"
        + "</HEAD><BODY LEFTMARGIN=0 "
        + "MARGINWIDTH=0 TOPMARGIN=0 MARGINHEIGHT=0><CENTER>"
        + "<IMG src='"
        + img
        + "' BORDER=0 NAME=image "
        + "onload='window.resizeTo(document.image.width,document.image.height)'>"
        + "</CENTER>"
        + "</BODY></HTML>";
            popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,left=200,top=200,menuBar=0,scrollbars=0,resizable=1');
            popup.document.open();
            popup.document.write(html);
            popup.document.focus();
            popup.document.close();
        }

        function DisplayAns6InWidnow() {
            var img = document.getElementById('<%= imgAnswer6.ClientID %>').src;

            html = "<HTML><HEAD><TITLE>Answer6</TITLE>"
        + "</HEAD><BODY LEFTMARGIN=0 "
        + "MARGINWIDTH=0 TOPMARGIN=0 MARGINHEIGHT=0><CENTER>"
        + "<IMG src='"
        + img
        + "' BORDER=0 NAME=image "
        + "onload='window.resizeTo(document.image.width,document.image.height)'>"
        + "</CENTER>"
        + "</BODY></HTML>";
            popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,left=200,top=200,menuBar=0,scrollbars=0,resizable=1');
            popup.document.open();
            popup.document.write(html);
            popup.document.focus();
            popup.document.close();
        }

        function CheckFileExtension() {
            var ctrlUpload = document.getElementById('<%=fuQuestion.ClientID%>');

            //Does the user browse or select a file or not
            if (ctrlUpload.value == '') {
                alert("Select a file first!");
                ctrlUpload.focus();
                return false;
            }

            //Extension List for validation. Add your required extension here with comma separator
            var extensionList = new Array(".JPG", ".GIF");
            //Get Selected file extension
            var extension = ctrlUpload.value.slice(ctrlUpload.value.indexOf(".")).toLowerCase();

            //Check file extension with your required extension list.
            for (var i = 0; i < extensionList.length; i++) {
                if (extensionList[i] == extension)
                    return true;
            }
            //alert("You can only upload below File Type:\n\n" + extensionList.join("\n"));
            ctrlUpload.focus();
            return false;
        }





        function DoPreview() {
            //            document.getElementById("ctl00_ContentPlaceHolder1_imgQuestions").src = document.getElementById("ctl00_ContentPlaceHolder1_fuQuestion").value;
            var filename = document.getElementById("ctl00_ContentPlaceHolder1_fuQuestion").value;
            var Img = new Image();
            if (navigator.appName == "Netscape") {
                alert("Previews do not work in Netscape.");
            }
            else {
                Img = document.getElementById('<%= imgQuestions.ClientID %>').src;

                Img.src = filename;
                // document.images[0].src = Img.src;
            }
        }
    </script>

    <script type="text/javascript">


        google_ad_client = "ca-pub-5840374944558242";
        /* 728x90 Linguanaut */
        google_ad_slot = "9050810175";
        google_ad_width = 728;
        google_ad_height = 90;

    </script>

    <script type="text/javascript">
        // Call showToolBarDiv() when editor get the focus
        editor.on('focus', function (event) {
            showToolBarDiv(event);
        });
        // Call hideToolBarDiv() when editor loses the focus
        editor.on('blur', function (event) {
            hideToolBarDiv(event);
        });


        //Whenever CKEditor get focus. We will show the toolbar DIV.
        function showToolBarDiv(event) {
            // Select the correct toolbar DIV and show it.
            //'event.editor.name' returns the name of the DIV receiving focus.
            $('#' + event.editor.name + 'cke_top_ctl00_ContentPlaceHolder1_txtQuestion" ').show();
        }

        //Whenever CKEditor loses focus, We will hide the corresponding toolbar DIV.
        function hideToolBarDiv(event) {
            // Select the correct toolbar DIV and hide it.
            //'event.editor.name' returns the name of the DIV receiving focus.
            $('#' + event.editor.name + 'cke_top_ctl00_ContentPlaceHolder1_txtQuestion" ').hide();
        }

    </script>

    <script type="text/javascript">

        function HideCKEditorsToolbars() {

            alert('sdfd');

            CKEDITOR.replace('cke_top_ctl00_ContentPlaceHolder1_txtQuestion', { toolbarStartupExpanded: true });

        }
    </script>

    <script type="text/javascript">
        function ShowModalPopup() {
            $find("mpe").show();
            return false;
        }
        function HideModalPopup() {
            $find("mpe").hide();
            return false;
        }
    </script>

    <style type="text/css">
        x; -h; t n: center; font-weight: bold;
        }
    </style>
</asp:Content>
