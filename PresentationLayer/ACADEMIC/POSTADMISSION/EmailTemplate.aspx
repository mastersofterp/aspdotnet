<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EmailTemplate.aspx.cs" Inherits="ACADEMIC_EmailTemplate" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%=Page.ResolveUrl("~/plugins/TinyMce/jquery.tinymce.min.js") %>'></script>
    <asp:UpdatePanel ID="updBasicDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hfdCursorPos" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Email Template</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Template Name</label>
                                        </div>
                                        <asp:TextBox ID="txtTemplateName" runat="server" TabIndex="1" MaxLength="100" CssClass="form-control" placeholder="" />
                                        <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtTemplateName"
                                            Display="None" ErrorMessage="Please enter Template Name" SetFocusOnError="True"
                                            ValidationGroup="EmailTemplate"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Email Subject</label>
                                        </div>
                                        <asp:TextBox ID="txtEmailSubject" runat="server" TabIndex="2" MaxLength="100" CssClass="form-control" placeholder="" />
                                        <asp:RequiredFieldValidator ID="rfvtxtEmailSubject" runat="server" ControlToValidate="txtEmailSubject"
                                            Display="None" ErrorMessage="Please enter Email Subject" SetFocusOnError="True"
                                            ValidationGroup="EmailTemplate"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Template Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTemplateType" runat="server" TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="ddlTemplateType_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlTemplateType" runat="server" ControlToValidate="ddlTemplateType"
                                            Display="None" ErrorMessage="Please select Template Type" SetFocusOnError="True"
                                            InitialValue="0" ValidationGroup="EmailTemplate"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Status</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActiveTemplate" name="switch" checked />
                                            <label data-on="Active" data-off="InActive" for="rdActiveTemplate"></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Data Field</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDataField" runat="server" TabIndex="4" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDataField" runat="server" ControlToValidate="ddlDataField"
                                            Display="None" ErrorMessage="Please select Data Field" SetFocusOnError="True"
                                            InitialValue="0" ValidationGroup="AddDataField"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-2 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <%--OnClientClick="ShowSelection();" "textbox(); OnClick="btnAdd_Click"--%>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClientClick="validate();setCursor();getCursor()" ValidationGroup="AddDataField"
                                            TabIndex="6" CssClass="btn btn-primary" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" DisplayMode="List" ValidationGroup="AddDataField" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-12">
                                       <%-- <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course Outline </label>
                                        </div>--%>
                                        <%--onkeydown="ShowSelection();" onclick="textbox()"--%>
                                        <asp:TextBox ID="txtCourseOutline" runat="server" TextMode="MultiLine" ClientIDMode="Static" TabIndex="5"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvtxtCourseOutline" runat="server" ControlToValidate="txtCourseOutline"
                                            Display="None" ErrorMessage="Please enter Course Outline" SetFocusOnError="True"
                                            ValidationGroup="EmailTemplate"></asp:RequiredFieldValidator>--%>
                                    </div>

                                </div>

                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="validate();" OnClick="btnSubmit_Click" ValidationGroup="EmailTemplate"
                                    TabIndex="6" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="7" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="EmailTemplate" />
                            </div>

                            <div class="col-12 mt-3">
                                <div class="row justify-content-center">

                                    <%--<div class="sub-heading">
                                        <h5>Participation Level List</h5>
                                    </div>--%>
                                    <div class="table-responsive">

                                        <asp:Panel ID="pnlTemplate" runat="server" Visible="false">
                                            <asp:ListView ID="lvTemplateDetails" runat="server" ItemPlaceholderID="itemPlaceholder">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit</th>
                                                                <th>Template Name</th>
                                                                <th>Status</th>
                                                            </tr>
                                                        </thead>

                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <asp:UpdatePanel runat="server" ID="updTemplate">
                                                        <ContentTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%--<asp:LinkButton ID="btnEditTemplate" runat="server" CssClass="fas fa-edit" OnClick="btnEditTemplate_Click" CommandArgument='<%#Eval("TEMPLATEID") %>'
                                                                        CommandName="Edit"></asp:LinkButton>--%>
                                                                    <asp:ImageButton ID="btnEditTemplate" CssClass="fas fa-edit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                        CommandArgument='<%# Eval("TEMPLATEID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                        OnClick="btnEditTemplate_Click" TabIndex="8" />
                                                                </td>

                                                                <td>
                                                                    <%# Eval("TEMPLATENAME") %>
                                                                </td>
                                                                <td>
                                                                    <%-- <span class="badge badge-success"> <%# Eval("ACTIVE_STATUS") %></span>--%>
                                                                    <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnEditTemplate" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>

                                    </div>

                                    <%--<div class="col-lg-12 col-md-12 col-12">
                                        <table id="Table1" class="table table-striped table-bordered" style="width: 100%;">
                                            <thead>
                                                <tr>
                                                    <th>Edit</th>
                                                    <th>Template Name</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton3" class="newAddNew Tab" runat="server" ImageUrl="~/Images/edit.png" AlternateText="Edit Record" ToolTip="Edit Record" /></td>
                                                    <td>Vinod Roy</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton1" class="newAddNew Tab" runat="server" ImageUrl="~/Images/edit.png" AlternateText="Edit Record" ToolTip="Edit Record" /></td>
                                                    <td>Vinod Roy</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>--%>
                                    <%--
                                        <div class="col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course Outline </label>
                                        </div>
                                        <asp:TextBox ID="txtCourseOutline" runat="server" TextMode="MultiLine" ClientIDMode="Static" TabIndex="8"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtCourseOutline" runat="server" ControlToValidate="txtCourseOutline"
                                            Display="None" ErrorMessage="Please enter Course Outline" SetFocusOnError="True"
                                            ValidationGroup="EmailTemplate"></asp:RequiredFieldValidator>
                                    </div>
                                    --%>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="ddlTemplateType" EventName="SelectedIndexChanged" />--%>

            <asp:PostBackTrigger ControlID="ddlTemplateType" />
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        $(document).ready(function () {
            LoadTinyMCE();
        });

        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //prm.add_endRequest(function () {
        //    LoadTinyMCE();


        //});

    </script>

    <script>
        var tinyBookmark;
        function setCursor() {
            if ($("#ctl00_ContentPlaceHolder1_ddlDataField option:selected").text() == "" || $("#ctl00_ContentPlaceHolder1_ddlDataField option:selected").text() == "Please Select") {
                //alert($("#ctl00_ContentPlaceHolder1_ddlDataField option:selected").text());
                return;
            }
            /*
            tinyBookmark = tinyMCE.activeEditor.selection.getBookmark(2,true);//getBookmark(2);
            alert(JSON.stringify(tinyBookmark));//get all start and end
            //alert(JSON.stringify(tinyBookmark['start']));
            //$('#hfdCursorPos').val(JSON.stringify(tinyBookmark));//
            $('#hfdCursorPos').val(JSON.stringify(tinyBookmark['start']));//
            */


            // Stores a bookmark of the current selection
            var bm = tinyMCE.activeEditor.selection.getBookmark();
            // Inserts some HTML contents at the current selection
            tinymce.activeEditor.selection.setContent("[" + $("#ctl00_ContentPlaceHolder1_ddlDataField option:selected").text() + "]");//'<strong>Some contents</strong>');
            ////tinyMCE.activeEditor.setContent(tinymce.activeEditor.getContent() + 'Some new content');
            // Restore the selection bookmark
            tinyMCE.activeEditor.selection.moveToBookmark(bm);
        }

        function getCursor() {
            console.log(tinyBookmark);
            tinyMCE.activeEditor.focus()
            tinyMCE.activeEditor.selection.moveToBookmark(tinyBookmark); 
        }
        function LoadTinyMCE() {
            //alert('hi');

            $('#txtCourseOutline').tinymce({
                script_url: '<%= ResolveUrl("~/plugins/TinyMce/tinymce.min.js") %>',
                placeholder: 'Enter the email body content here ...',
                height: 300,
                menubar: 'file edit view insert format tools table tc help',
                plugins: [
                  'advlist autolink lists link image charmap print preview anchor',
                  'searchreplace visualblocks code fullscreen',
                  'insertdatetime media table paste code help wordcount'
                ],
                toolbar: 'undo redo | formatselect | ' +
                'bold italic backcolor | alignleft aligncenter ' +
                'alignright alignjustify | bullist numlist outdent indent | ' +
                'removeformat | help',
                content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }',
                //encoding: 'xml'
                //init_instance_callback: function (editor) {
                //    editor.on('mouseup', function (e) {
                //        alert('okoko');
                //    });
                //}
            });   
        }

        

    </script>
    <script>
        function SetParticipation(val) {

            $('#rdActiveTemplate').prop('checked', val);

        }
        function validate() {


            $('#hfdActive').val($('#rdActiveTemplate').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
    </script>
    <script>
        function textbox() {
            alert('hi');
            var ctl = document.getElementById('txtCourseOutline');
            var startPos = ctl.selectionStart;
            var endPos = ctl.selectionEnd;
            alert(startPos + ", " + endPos);
        }
        <%--function ShowSelection() {
            var textComponent = document.getElementById('txtCourseOutline');
            var selectedText;

            if (textComponent.selectionStart !== undefined) { // Standards-compliant version
                var startPos = textComponent.selectionStart;
                var endPos = textComponent.selectionEnd;
                selectedText = textComponent.value.substring(startPos, endPos);
            }
            else if (document.selection !== undefined) { // Internet Explorer version
                textComponent.focus();
                var sel = document.selection.createRange();
                selectedText = sel.text;
            }

            alert("You selected: " + selectedText);
        }--%>

        /*function getCaretPosition(ctrl) {
            var cursorPosition = $('#txtCourseOutline').prop("selectionEnd");
            alert(cursorPosition);
            //alert($("#ctl00_ContentPlaceHolder1_ddlDataField option:selected").text());// $('#ctl00_ContentPlaceHolder1_ddlDataField').val());
            //tinymce.get('#txtCourseOutline').execCommand('insertHTML', false, "[" + $("#ctl00_ContentPlaceHolder1_ddlDataField option:selected").text() + "]");

            
            // IE < 9 Support 
            if (document.selection) {
                ctrl.focus();
                var range = document.selection.createRange();
                var rangelen = range.text.length;
                range.moveStart('character', -ctrl.value.length);
                var start = range.text.length - rangelen;
                alert(ctrl.selectionStart + "++++" + rangelen);
                return {
                    'start': start,
                    'end': start + rangelen
                };
            } // IE >=9 and other browsers
            else if (ctrl.selectionStart || ctrl.selectionStart == '0') {
                alert(ctrl.selectionStart+"---"+ rangelen);
                return {
                    'start': ctrl.selectionStart,
                    'end': ctrl.selectionEnd
                };
            } else {
                return {
                    'start': 0,
                    'end': 0
                };
            }
            
        }*/
    </script>
</asp:Content>

