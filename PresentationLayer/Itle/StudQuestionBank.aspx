<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudQuestionBank.aspx.cs" Inherits="Itle_StudQuestionBank" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../includes/modalbox.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>
    <link href="../CSS/master.css" rel="stylesheet" />

    <link href="../CSS/jquery-ui.css" rel="stylesheet" />
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
    <script src="../jquery/jquery-1.9.1.js" type="text/javascript"></script>

    <script type="text/javascript">

        function validate() {
            document.getElementById('txtQuestion').focus();
        }
        function MathEditor() {
            window.open("ITLE_MathEditor.aspx", "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=500, left=500, width=800, height=400");

        }
    </script>

    <script type="text/javascript">

        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>


    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STUDENT QUESTION BANK</h3>
                </div>
                <div>
                    <form role="form">
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlQuestionBankMaster" runat="server">
                                    <div class="form-group col-md-12">
                                        <asp:Panel ID="pnlAdd" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Add/Edit Student Question Bank</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Panel ID="pnlCourse" runat="server">
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">
                                                                            <div class="text-center">
                                                                                <asp:Label ID="lblDeleteStatus" runat="server" Font-Bold="true"
                                                                                    Text="Can't Delete, Question Is Selected For Test" Visible="false"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-3">
                                                                                <label>Course Name :</label>
                                                                            </div>
                                                                            <div class="col-md-6">
                                                                                <asp:Label ID="lblCourseName" Font-Bold="true" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <div class="form-group">
                                                            <div class="col-md-12">
                                                                <div class="col-md-3">
                                                                    <label>Select the Type of Question :</label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:RadioButtonList ID="rbnObjectiveDescriptive" Font-Bold="true" AutoPostBack="true"
                                                                        runat="server" RepeatDirection="Horizontal" TabIndex="1" ToolTip="Select Question Type" Width="216px" OnSelectedIndexChanged="rbnObjectiveDescriptive_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Objective" Value="O" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Descriptive" Value="D"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="form-group col-md-12">
                                                            <div class="col-md-3">
                                                                <label><span style="color: Red">*</span>Enter Topic Name :</label>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtNewTopic" runat="server" CssClass="form-control" TabIndex="2"
                                                                    ToolTip="Enter Topic Name"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvTopic" runat="server" ControlToValidate="txtNewTopic"
                                                                    Display="None" ErrorMessage="Please Enter Topic For Question" SetFocusOnError="true"
                                                                    ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="trTextImage" runat="server">
                                                        <div class="form-group">
                                                            <div class="col-md-12">
                                                                <div class="col-md-3">
                                                                    <label>Select an option :</label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:RadioButtonList ID="rdbtnList" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" ToolTip="Select An Option" TabIndex="6" Width="181px" OnSelectedIndexChanged="rdbtnList_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Text" Value="T" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Image" Value="I"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="form-group col-md-12">
                                                            <div class="col-md-3">
                                                                <label><span style="color: Red">*</span>Question :</label>
                                                            </div>
                                                            <div class="col-md-9">
                                                                <%--<asp:TextBox ID="txtQuestion" runat="server" Height="80px" TextMode="MultiLine" Width="300px">
                                                                    </asp:TextBox>--%>
                                                                <CKEditor:CKEditorControl ID="txtQuestion" runat="server" Height="300" TabIndex="7"
                                                                    BasePath="~/ckeditor"></CKEditor:CKEditorControl>
                                                                <%--<FTB:FreeTextBox ID="txtQuestion" runat="server" Height="180px" Width="450px" 
                                                                    TabIndex="4" Focus ="true" Text="&nbsp;">
                                                                    </FTB:FreeTextBox>--%>
                                                                <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" ControlToValidate="txtQuestion"
                                                                    Display="None" ErrorMessage="Please Enter Question" SetFocusOnError="true" ValidationGroup="Submit">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <div class="col-md-3">
                                                                <label></label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <label id="lblInsertSymbol" onclick="javascript:InsertSymbol('divKeyBoard')"
                                                                    style="font-weight: bold; color: Blue" tabindex="6">
                                                                    Click here to insert Symbols</label>
                                                                <img id="btnSymbol" runat="server" alt="Insert Symbol" visible="true" src="../images/RedOmega.jpeg" tabindex="8"
                                                                    style="width: 20px; height: 20px" onclick="javascript:InsertSymbol('divKeyBoard')" />
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:Button ID="btnMathEditor" runat="server" Text="Math Equation" CssClass="btn-primary"
                                                                    OnClientClick="MathEditor();" TabIndex="9" ToolTip="Click here for Math Equation" />
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:Image ID="imgQuestions" runat="server" TabIndex="10" Height="128px" Width="128px" onclick="DisplayQuestionInWidnow();" />
                                                                <%--<asp:Image ID="imgQuestions" runat="server" Height="128px" Width="128px" 
                                                                    onclick="DisplayQuestionInWidnow();" />--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <div class="col-md-3">
                                                                <label></label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <label></label>
                                                            </div>
                                                            <div class="col-md-3">
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
                                                    </div>

                                                    <div class="form-group" id="divKeyBoard" style="display: none">
                                                        <div class="col-md-12">
                                                            <div class="col-md-3">
                                                                <label></label>
                                                            </div>
                                                            <div class="col-md-9">
                                                                <iframe src="Virtual_Keyboard.htm" width="95%" height="450px"></iframe>
                                                                <%-- <iframe src="Virtual_Keyboard.htm" width="95%" height="450px"></iframe>--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Answer1" runat="server">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-12">
                                                                <div class="col-md-3">
                                                                    <label><span style="color: Red">*</span>Answer 1 :</label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <CKEditor:CKEditorControl ID="txtAnswer1" runat="server" Height="50" BasePath="~/ckeditor"
                                                                        AutoParagraph="false" IgnoreEmptyParagraph="true" CssClass="form-control" TabIndex="8"
                                                                        ToolbarStartupExpanded="false"></CKEditor:CKEditorControl>
                                                                    <%--  <FTB:FreeTextBox ID="txtAnswer1" runat="server" Height="80px" Width="170px" TabIndex="4"
                                                                    EnableToolbars="false" EnableHtmlMode="false" >
                                                                </FTB:FreeTextBox>--%>
                                                                    <asp:RequiredFieldValidator ID="rfvtxtAnswer1" runat="server" ControlToValidate="txtAnswer1"
                                                                        Display="None" ErrorMessage="Please Enter Answer1" SetFocusOnError="true"
                                                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="col-md-3">
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
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Answer2" runat="server">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-12">
                                                                <div class="col-md-3">
                                                                    <label><span style="color: Red">*</span>Answer 2 :</label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <CKEditor:CKEditorControl ID="txtAnswer2" runat="server" Height="50" BasePath="~/ckeditor" TabIndex="9"
                                                                        AutoParagraph="false" IgnoreEmptyParagraph="true" ToolbarStartupExpanded="false"></CKEditor:CKEditorControl>

                                                                    <asp:RequiredFieldValidator ID="rfvAnswer2" runat="server" ControlToValidate="txtAnswer2"
                                                                        Display="None" ErrorMessage="Please Enter Answer2" SetFocusOnError="true"
                                                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="col-md-3">
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
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Answer3" runat="server">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-12">
                                                                <div class="col-md-3">
                                                                    <label>Answer 3 :</label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <CKEditor:CKEditorControl ID="txtAnswer3" runat="server" Height="50" BasePath="~/ckeditor" TabIndex="10"
                                                                        AutoParagraph="false" IgnoreEmptyParagraph="true" ToolbarStartupExpanded="false"></CKEditor:CKEditorControl>
                                                                    <%-- <FTB:FreeTextBox ID="txtAnswer3" runat="server" Height="80px" Width="170px" TabIndex="4"
                                                                            EnableToolbars="false" EnableHtmlMode="false">
                                                                        </FTB:FreeTextBox>--%>
                                                                </div>
                                                                <div class="col-md-3">
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
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Answer4" runat="server">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-12">
                                                                <div class="col-md-3">
                                                                    <label>Answer 4 :</label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <CKEditor:CKEditorControl ID="txtAnswer4" runat="server" Height="50" BasePath="~/ckeditor" TabIndex="11"
                                                                        AutoParagraph="false" IgnoreEmptyParagraph="true" ToolbarStartupExpanded="false"></CKEditor:CKEditorControl>
                                                                    <%--<FTB:FreeTextBox ID="txtAnswer4" runat="server" Height="80px" Width="170px" TabIndex="4"
                                                                    EnableToolbars="false" EnableHtmlMode="false">
                                                                </FTB:FreeTextBox>--%>
                                                                </div>
                                                                <div class="col-md-3">
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
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Answer5" runat="server">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-12">
                                                                <div class="col-md-3">
                                                                    <label>Answer 5 :</label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <CKEditor:CKEditorControl ID="txtAnswer5" runat="server" Height="50" BasePath="~/ckeditor" TabIndex="12"
                                                                        AutoParagraph="false" IgnoreEmptyParagraph="true" ToolbarStartupExpanded="false"></CKEditor:CKEditorControl>
                                                                    <%--<FTB:FreeTextBox ID="txtAnswer5" runat="server" Height="80px" Width="170px" TabIndex="4"
                                                                    EnableToolbars="false" EnableHtmlMode="false">
                                                                </FTB:FreeTextBox>--%>
                                                                </div>
                                                                <div class="col-md-3">
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
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Answer6" runat="server">
                                                        <div class="form-group">
                                                            <div class="form-group col-md-12">
                                                                <div class="col-md-3">
                                                                    <label>Answer 6 :</label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <CKEditor:CKEditorControl ID="txtAnswer6" runat="server" Height="50" BasePath="~/ckeditor" TabIndex="13"
                                                                        AutoParagraph="false" IgnoreEmptyParagraph="true" ToolbarStartupExpanded="false"></CKEditor:CKEditorControl>
                                                                    <%--<FTB:FreeTextBox ID="txtAnswer6" runat="server" TabIndex="4" Height="80px" Width="170px"
                                                                    EnableToolbars="false" EnableHtmlMode="false">
                                                                </FTB:FreeTextBox>--%>
                                                                </div>
                                                                <div class="col-md-3">
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
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <div class="text-center">
                                                                <asp:Button ID="btnSubmit" runat="server" Text="Save" ValidationGroup="Submit" TabIndex="16"
                                                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" />
                                                                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="17"
                                                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                    ShowSummary="false" ValidationGroup="Submit" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <div class="col-sm-12" id="grid">
                                            <div class="row" style="border: solid 1px #CCCCCC">
                                                <div style="font-weight: bold; background-color: #72A9D3; color: white" class="panel-heading">Questions List</div>
                                                <div class="table-responsive">
                                                    <table class="customers">
                                                        <%--    <tr style="font-weight: bold; background-color: #808080; color: white">
                                                            <th style="width: 1%; padding-left: 8px; text-align: left">SrNo</th>
                                                            <th style="width: 5%; padding-left: 8px; text-align: left">Action</th>
                                                            <th style="width: 34%; text-align: left">Question Text</th>
                                                            <th style="width: 8%; text-align: left">Topic</th>
                                                            <th style="width: 8%; text-align: left">Option1</th>
                                                            <th style="width: 8%; text-align: left">Option2</th>
                                                            <th style="width: 8%; text-align: left">Option3</th>
                                                            <th style="width: 8%; text-align: left">Option4</th>
                                                            <th style="width: 8%; text-align: left">Option5</th>
                                                            <th style="width: 8%; text-align: left">Option6</th>
                                                        </tr>--%>
                                                    </table>
                                                </div>
                                                <div class="DocumentList">
                                                    <asp:Panel ID="pnllvView" runat="server" ScrollBars="Vertical" Height="400px" BackColor="#FFFFFF">
                                                        <asp:ListView ID="lvQuestions" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="demo-grid">
                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr style="font-weight: bold; background-color: #808080; color: white">
                                                                                <th style="width: 1%; padding-left: 8px; text-align: left">SrNo</th>
                                                                                <th style="width: 5%; padding-left: 8px; text-align: left">Action</th>
                                                                                <th style="width: 34%; text-align: left">Question Text</th>
                                                                                <th style="width: 8%; text-align: left">Topic</th>
                                                                                <th style="width: 8%; text-align: left">Option1</th>
                                                                                <th style="width: 8%; text-align: left">Option2</th>
                                                                                <th style="width: 8%; text-align: left">Option3</th>
                                                                                <th style="width: 8%; text-align: left">Option4</th>
                                                                                <th style="width: 8%; text-align: left">Option5</th>
                                                                                <th style="width: 8%; text-align: left">Option6</th>
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
                                                                    <td style="width: 1%; padding-left: 8px;">
                                                                        <%# Container.DataItemIndex + 1%>
                                                                    </td>

                                                                    <td style="width: 5%; padding-left: 8px;">
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit1.gif"
                                                                            CommandArgument='<%# Eval("QUESTIONNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="19" OnClick="btnEdit_Click" />&nbsp;&nbsp;
                                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" TabIndex="20"
                                                                            CommandArgument='<%# Eval("QUESTIONNO") %>'
                                                                            ToolTip="Delete Record" OnClientClick="showConfirmDel(this); return false;" OnClick="btnDelete_Click" />
                                                                    </td>
                                                                    <td style="width: 34%; text-align: left">
                                                                        <%# Eval("QUESTIONTEXT")%>
                                                                    </td>
                                                                    <td style="width: 8%; text-align: left">
                                                                        <%# Eval("TOPIC") %>
                                                                    </td>

                                                                    <td style="width: 8%; text-align: left">
                                                                        <%# Eval("ANS1TEXT")%>
                                                                    </td>
                                                                    <td style="width: 8%; text-align: left">
                                                                        <%# Eval("ANS2TEXT")%>
                                                                    </td>
                                                                    <td style="width: 8%; text-align: left">
                                                                        <%# Eval("ANS3TEXT")%>
                                                                    </td>
                                                                    <td style="width: 8%; text-align: left">
                                                                        <%# Eval("ANS4TEXT")%>
                                                                    </td>
                                                                    <td style="width: 8%; text-align: left">
                                                                        <%# Eval("ANS5TEXT")%>
                                                                    </td>
                                                                    <td style="width: 8%; text-align: left">
                                                                        <%# Eval("ANS6TEXT")%>
                                                                    </td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                        </div>
                    </form>
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
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
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

