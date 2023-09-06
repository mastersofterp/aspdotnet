<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="LetterTemplate.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_LetterTemplate"
    ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%=Page.ResolveUrl("~/plugins/TinyMce/jquery.tinymce.min.js") %>'></script>

    <div>
        <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                        <%--                                <asp:Label ID="lblDynamicPageTitle" runat="server" Text="LETTER TEMPLATE"></asp:Label>--%>
                    </h3>
                </div>

                <div id="Tabs" role="tabpanel">
                    <div id="divqualification">
                        <div class="col-12">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" runat="server" id="etemplate" data-toggle="tab" href="#tab_1">Letter Template</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" runat="server" onclick="changeDiv();" id="btnmodal" data-toggle="tab" href="#tab_2">Data Field</a>
                                    </li>
                                </ul>

                                <div class="tab-content" id="my-tab-content">
                                    <%--  TAB :Email Template  --%>
                                    <div class="tab-pane active" id="tab_1">
                                        <div class="box-body">
                                            <asp:UpdatePanel ID="updlettertemplate" runat="server">
                                                <ContentTemplate>
                                                    <asp:HiddenField ID="hdnActive" runat="server" ClientIDMode="Static" />
                                                    <div class="col-12">
                                                        <%--  <div class="col-12 mt-3">--%>
                                                        <div class="row">
                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Letter Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlLetterType" runat="server" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlLetterType_SelectedIndexChanged"
                                                                    AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlLetterType" runat="server" ControlToValidate="ddlLetterType"
                                                                    Display="None" ErrorMessage="Please Select Letter Type" SetFocusOnError="True"
                                                                    InitialValue="0" ValidationGroup="LetterTemplate"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlLetterType"
                                                                    Display="None" ErrorMessage="Please Select Letter Type" SetFocusOnError="True"
                                                                    InitialValue="0" ValidationGroup="ShowSample"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <asp:Button ID="btnshow" runat="server" Text="Show Sample" OnClick="btnshow_Click"
                                                                    TabIndex="1" CssClass="btn btn-primary" ValidationGroup="ShowSample" />
                                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="ShowSample" />
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Letter Template Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtLetterTemplateName" runat="server" TabIndex="1" MaxLength="100" CssClass="form-control" placeholder="" onkeypress="return alpha(event)" />
                                                                <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtLetterTemplateName"
                                                                    Display="None" ErrorMessage="Please Enter Letter Template Name" SetFocusOnError="True"
                                                                    ValidationGroup="LetterTemplate"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%-- <sup>* </sup>--%>
                                                                    <label>Short Description</label>
                                                                </div>
                                                                <asp:TextBox ID="txtShortDesc" runat="server" TabIndex="1" MaxLength="100" CssClass="form-control" placeholder="" onkeypress="return alpha(event)" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Data Field</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDataField" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlDataField" runat="server" ControlToValidate="ddlDataField"
                                                                    Display="None" ErrorMessage="Please Select Data Field" SetFocusOnError="True"
                                                                    InitialValue="0" ValidationGroup="AddDataField"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-2 col-md-2 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label></label>
                                                                </div>
                                                                <%--OnClientClick="ShowSelection();" "textbox(); OnClick="btnAdd_Click"--%>
                                                                <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="AddDataField"
                                                                    TabIndex="1" CssClass="btn btn-primary" OnClientClick="validate();setCursor();getCursor()" />
                                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="AddDataField" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Status</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="chkActive" name="switch" checked />
                                                                    <label data-on="Active" data-off="InActive" for="chkActive"></label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-12">
                                                                <asp:TextBox ID="txtLetterText" runat="server" TextMode="MultiLine" ClientIDMode="Static" TabIndex="1">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="validate();"
                                                                TabIndex="1" CssClass="btn btn-primary" ValidationGroup="LetterTemplate" />
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                                ShowSummary="false" DisplayMode="List" ValidationGroup="LetterTemplate" />
                                                        </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="ddlLetterType" />
                                                    <asp:PostBackTrigger ControlID="btnAdd" />
                                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                                    <asp:PostBackTrigger ControlID="btnCancel" />
                                                    <asp:PostBackTrigger ControlID="btnshow" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="col-12 mt-3">
                                                <div class="row justify-content-center">
                                                    <div class="table-responsive">
                                                        <asp:Panel ID="pnlLetterTemplate" runat="server" Visible="false">
                                                            <asp:ListView ID="lvLetterTemplate" runat="server" ItemPlaceholderID="itemPlaceholder">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit</th>
                                                                                <th>Letter Template Name</th>
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
                                                                                    <asp:ImageButton ID="btnEditLetterTemplate" CssClass="fas fa-edit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                                        CommandArgument='<%# Eval("LETTER_TEMPLATE_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                                        TabIndex="1" OnClick="btnEditLetterTemplate_Click" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("LETTER_TEMPLATE_NAME") %>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="btnEditLetterTemplate" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--  TAB:Data Field  --%>
                                <div class="tab-pane fade" id="tab_2">
                                    <div class="box-body">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" ClientIDMode="Static" />
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-4 col-md-4 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Template Type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddllettertypeid" ToolTip="Template Type" runat="server" TabIndex="1" ValidationGroup="AddTemplate" AutoPostBack="true" OnSelectedIndexChanged="ddltemplate_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddltemplate" runat="server" ControlToValidate="ddllettertypeid"
                                                                Display="None" ErrorMessage="Please Select Letter Template Field" SetFocusOnError="True"
                                                                InitialValue="0" ValidationGroup="AddTemplate"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-4 col-md-4 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Display Data Field</label>
                                                            </div>
                                                            <asp:TextBox ID="txtdisplayfield" ToolTip="Display Data Field" runat="server" TabIndex="2" MaxLength="30" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="rfvtxtDisplayDataField" runat="server" ControlToValidate="txtdisplayfield"
                                                                Display="None" ErrorMessage="Please Enter Display Data Field" ValidationGroup="AddTemplate" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-4 col-md-4 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Data Field</label>
                                                            </div>
                                                            <asp:TextBox ID="txtdatafield" ToolTip="Data Field" runat="server" TabIndex="3" MaxLength="30" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="rfvtxttxtdatafield" runat="server" ControlToValidate="txtdatafield"
                                                                Display="None" ErrorMessage="Please Enter Data Field" ValidationGroup="AddTemplate" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:LinkButton ID="btnadd_DField" runat="server" Text="Submit" ValidationGroup="AddTemplate"
                                                        TabIndex="4" CssClass="btn btn-primary" OnClientClick="return clientFunction();" OnClick="btnFieldSubmit_Click" />
                                                    <asp:Button ID="btncancel_DField" runat="server" Text="Cancel" OnClick="btnDatafieldCancel_Click" TabIndex="5" CssClass="btn btn-warning" />
                                                    <asp:ValidationSummary ID="vsDataField" runat="server" ValidationGroup="AddTemplate"
                                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="false" />
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="ddllettertypeid" />
                                                <asp:PostBackTrigger ControlID="btnadd_DField" />
                                                <asp:PostBackTrigger ControlID="btncancel_DField" />
                                            </Triggers>
                                        </asp:UpdatePanel>


                                        <div class="col-12 mt-3">
                                            <div class="row justify-content-center">
                                                <div class="table-responsive">

                                                    <asp:Panel ID="pnlfieldtemplate" runat="server" Visible="false">
                                                        <asp:ListView OnItemCommand="datafield_listview_ItemCommand" ID="datafield_listview" runat="server" ItemPlaceholderID="itemPlaceholder">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Edit</th>
                                                                            <th>SR NO</th>
                                                                            <th>LETTER TEMPLATE NAME</th>
                                                                            <th>DISPLAY DATA FIELD</th>
                                                                            <th>DATA FIELD</th>

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
                                                                                <asp:ImageButton ID="btnEdit2" CssClass="fas fa-edit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                                    CommandArgument='<%# Eval("LETTER_DATAFIELD_CONFIGID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                                    TabIndex="9" />
                                                                            </td>

                                                                            <td>
                                                                                <%# Eval("SRNO") %>
                                                                            </td>
                                                                            <td><%# Eval("LETTER_TEMPLATE_TYPE") %></td>
                                                                            <td><%# Eval("DATAFIELD_DISPLAY") %></td>
                                                                            <td><%# Eval("DATAFIELD") %></td>

                                                                        </tr>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnEdit2" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>

                                                </div>


                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="popup" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="modal" id="myModalPopUp" data-backdrop="static">
                                <div class="modal-dialog modal-md">
                                    <div class="modal-content">
                                        <div class="modal-body pl-0 pr-0 pl-lg-2 pr-lg-2">
                                            <div class="col-12 mt-3">
                                                <h5 class="heading">Please Enter Developer's Password</h5>
                                                <div class="row">
                                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                                        <label>PASSWORD</label>
                                                        <asp:Label ID="lblPass" runat="server" Text="ybc@123" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtPass" TextMode="Password" runat="server" TabIndex="1" ToolTip="Please Enter Password" AutoComplete="new-password"
                                                            MaxLength="50" CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="req_password" runat="server" ErrorMessage="Password Required !" ControlToValidate="txtPass"
                                                            Display="None" ValidationGroup="password"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                                    </div>
                                                    <div class="btn form-group col-lg-12 col-md-12 col-12">
                                                        <asp:Button ID="btnConnect" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="1" CssClass="btn btn-outline-primary"
                                                            runat="server" Text="Submit" OnClick="btnConnect_Click" ValidationGroup="password" />
                                                        <asp:Button ID="btnCancel1" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="2" CssClass="btn btn-danger"
                                                            runat="server" Text="Cancel" OnClick="btnCancel1_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnConnect" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <%-- </div>--%>



    <!-- The Modal- Preview Form Button In ListView -->


    <div class="modal" id="modalShowSample" data-backdrop="false" style="background-color: rgba(0,0,0,0.6);">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="SampleName" runat="server"></asp:Label>
                        Sample</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body p-2" style="overflow: scroll; height: 450px;">
                    <div class="col-12">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdnSampleText" runat="server" />
                                <p id="PPreviewVerify" class="p-3" runat="server"></p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <!-- Modal footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-danger" data-dismiss="modal">Close</button>
                </div>

            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            LoadTinyMCE();
            sampltext();
        });
    </script>
    <script>
        var tinyBookmark;
        function setCursor() {
            if ($("#ctl00_ContentPlaceHolder1_ddlDataField option:selected").text() == "" || $("#ctl00_ContentPlaceHolder1_ddlDataField option:selected").text() == "Please Select") {
                //alert($("#ctl00_ContentPlaceHolder1_ddlDataField option:selected").text());
                return;
            }

            var bm = tinyMCE.activeEditor.selection.getBookmark();
            tinymce.activeEditor.selection.setContent("[" + $("#ctl00_ContentPlaceHolder1_ddlDataField option:selected").text() + "]");//'<strong>Some contents</strong>');
            tinyMCE.activeEditor.selection.moveToBookmark(bm);
        }

        function getCursor() {
            console.log(tinyBookmark);
            tinyMCE.activeEditor.focus()
            tinyMCE.activeEditor.selection.moveToBookmark(tinyBookmark);
        }
        function sampltext() {
            $('#txtSampleText').tinymce({
                script_url: '<%= ResolveUrl("~/plugins/TinyMce/tinymce.min.js") %>',
                placeholder: 'Enter the letter body content here ...',
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
        function LoadTinyMCE() {
            $('#txtLetterText').tinymce({
                script_url: '<%= ResolveUrl("~/plugins/TinyMce/tinymce.min.js") %>',
                placeholder: 'Enter the letter body content here ...',
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

        function SetStatus(val) {
            $('#chkActive').prop('checked', val);
        }

        function validate() {
            $('#hdnActive').val($('#chkActive').prop('checked'));
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });

        function ModalPopupShowSample() {
            var htmltext = $("#<%=hdnSampleText.ClientID %>").val();
            $('#ctl00_ContentPlaceHolder1_PPreviewVerify').prop('innerHTML', htmltext);
            $('#modalShowSample').modal('show');
        }



        function alpha(e) {
            var k;
            document.all ? k = e.keyCode : k = e.which;
            if ((k > 64 && k < 91) || (k > 96 && k < 123) || (k == 32))
                return true;
            else
                return false;
        }

    </script>
    <script type="text/javascript">
        var temp = "2";
        function changeDiv() {
            var name = $("#HiddenField1").val();
            if (name == 1) {
                $('#myModalPopUp').modal('show');
            }
            else if (name == 0 && temp == 2) {
                $('#myModalPopUp').modal('show');
                temp = 0;
            }
            else {
                $('#myModalPopUp').modal('hide');
            }
        }

    </script>
     <script>

         function TabShow(tabName) {
             //alert('hii')
             //var tabName = "tab_2";
             $('#Tabs a[href="#' + tabName + '"]').tab('show');
             $("#Tabs a").click(function () {
                 $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
             });
         }
    </script>
</asp:Content>
