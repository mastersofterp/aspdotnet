<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CreateEmailTemplate.aspx.cs" Inherits="ACADEMIC_CreateEmailTemplate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%=Page.ResolveUrl("~/plugins/TinyMce/jquery.tinymce.min.js") %>'></script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Create Template</h3>
                </div>

                <div id="Tabs" role="tabpanel">
                    <div id="divqualification">
                        <div class="col-12">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" runat="server" id="etemplate" data-toggle="tab" href="#tab_1">Email Template</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" runat="server" onclick="changeDiv();" id="btnmodal" data-toggle="tab" href="#tab_2">Data Field</a>
                                    </li>
                                </ul>

                                <div class="tab-content" id="my-tab-content">
                                    <%--  TAB :Email Template  --%>
                                    <div class="tab-pane active"  id="tab_1">
                                        <div class="box-body">
                                            <asp:UpdatePanel ID="updBasicDetails" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:HiddenField ID="hfdCursorPos" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
                                                    <div class="col-12 mt-3">
                                                        <div class="row">
                                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Template Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtTemplateName" runat="server" TabIndex="1" MaxLength="30" CssClass="form-control" />
                                                                <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtTemplateName"
                                                                    Display="None" ErrorMessage="Please enter template name" SetFocusOnError="True"
                                                                    ValidationGroup="EmailTemplate"></asp:RequiredFieldValidator>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                                    FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|.:-&&quot;0123456789'" TargetControlID="txtTemplateName" />
                                                            </div>
                                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Subject</label>
                                                                </div>
                                                                <asp:TextBox ID="txtSubject" runat="server" TabIndex="2" MaxLength="30" CssClass="form-control" />
                                                                <asp:RequiredFieldValidator ID="rfvtxtEmailSubject" runat="server" ControlToValidate="txtSubject"
                                                                    Display="None" ErrorMessage="Please enter email subject" SetFocusOnError="True"
                                                                    ValidationGroup="EmailTemplate"></asp:RequiredFieldValidator>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                    FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|.:-&&quot;0123456789'" TargetControlID="txtSubject" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-3 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Template Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlTemplateType" runat="server" TabIndex="3" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlTemplateType_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlTemplateType" runat="server" ControlToValidate="ddlTemplateType"
                                                                    Display="None" ErrorMessage="Please select template type" SetFocusOnError="True"
                                                                    InitialValue="0" ValidationGroup="EmailTemplate"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-3 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>User Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlUserType" runat="server" TabIndex="4" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUserType"
                                                                    Display="None" ErrorMessage="Please select user type" SetFocusOnError="True"
                                                                    InitialValue="0" ValidationGroup="EmailTemplate"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-3 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Page For Template</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlPageForTemplate" runat="server" TabIndex="5" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlUserType"
                                            Display="None" ErrorMessage="Please select page for template" SetFocusOnError="True"
                                            InitialValue="0" ValidationGroup="EmailTemplate"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-3 col-12">
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
                                                                <asp:DropDownList ID="ddlDataField" runat="server" TabIndex="6" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlDataField" runat="server" ControlToValidate="ddlDataField"
                                                                    Display="None" ErrorMessage="Please select data field" SetFocusOnError="True"
                                                                    InitialValue="0" ValidationGroup="AddDataField"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-2 col-md-2 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label></label>
                                                                </div>

                                                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClientClick="validate();setCursor();getCursor()" ValidationGroup="AddDataField"
                                                                    TabIndex="7" CssClass="btn btn-primary" />
                                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="AddDataField" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Template Body</label>
                                                                </div>

                                                                <asp:TextBox ID="txtTemplateBody" runat="server" TextMode="MultiLine" ClientIDMode="Static" TabIndex="8"></asp:TextBox>

                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="validate();" ValidationGroup="EmailTemplate"
                                                            TabIndex="8" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="9" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" DisplayMode="List" ValidationGroup="EmailTemplate" />
                                                    </div>
                                                </ContentTemplate>

                                                <Triggers>

                                                    <asp:PostBackTrigger ControlID="ddlTemplateType" />
                                                    <asp:PostBackTrigger ControlID="btnAdd" />
                                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                                    <asp:PostBackTrigger ControlID="btnCancel" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                            <div class="col-12 mt-3">
                                                <div class="row justify-content-center">
                                                    <div class="table-responsive">

                                                        <asp:Panel ID="pnlTemplate" runat="server" Visible="false">
                                                            <asp:ListView ID="lvTemplateDetails" runat="server" ItemPlaceholderID="itemPlaceholder">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit</th>
                                                                                <th>Template Name</th>
                                                                                <th>Subject</th>
                                                                                <th>Template Type</th>
                                                                                <th>User Type</th>
                                                                                <th>Page For Template</th>
                                                                                <th>Data Field</th>
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
                                                                                    <asp:ImageButton ID="btnEdit" CssClass="fas fa-edit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                                        CommandArgument='<%# Eval("CREATETEMP_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                                        TabIndex="9" OnClick="btnEdit_Click" />
                                                                                </td>

                                                                                <td>
                                                                                    <%# Eval("TEMP_NAME") %>
                                                                                </td>
                                                                                <td><%# Eval("TEMP_SUBJECT") %></td>
                                                                                <td><%# Eval("TEMPLATETYPE") %></td>
                                                                                <td><%# Eval("USERDESC") %></td>
                                                                                <td><%# Eval("AL_Link") %></td>

                                                                                <td><%# Eval("DATAFIELDDISPLAY") %></td>

                                                                                <td>
                                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("ACTIVESTATUS") %>' ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="btnEdit" />
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

                                    <%--  TAB:Data Field  --%>
                                    <div class="tab-pane fade"  id="tab_2">
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
                                                                <asp:DropDownList ID="ddltemplate" ToolTip="Template Type" runat="server" TabIndex="1" ValidationGroup="AddTemplate" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddltemplate_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddltemplate" runat="server" ControlToValidate="ddltemplate"
                                                                    Display="None" ErrorMessage="Please Select Template Field" SetFocusOnError="True"
                                                                    InitialValue="0" ValidationGroup="AddTemplate"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-4 col-md-4 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
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
                                                            TabIndex="4" CssClass="btn btn-primary" OnClick="btnDfieldSubmit_Click" OnClientClick="return clientFunction();" />
                                                        <asp:Button ID="btncancel_DField" runat="server" Text="Cancel" TabIndex="5" CssClass="btn btn-warning" OnClick="btnDatafieldCancel_Click" />
                                                        <asp:ValidationSummary ID="vsDataField" runat="server" ValidationGroup="AddTemplate"
                                                            DisplayMode="List" ShowMessageBox="True" ShowSummary="false" />
                                                    </div>
                                                </ContentTemplate>

                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="ddltemplate" />
                                                    <asp:PostBackTrigger ControlID="btnadd_DField" />
                                                    <asp:PostBackTrigger ControlID="btncancel_DField" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                            <div class="col-12 mt-3">
                                                <div class="row justify-content-center">
                                                    <div class="table-responsive">

                                                        <asp:Panel ID="pnlfieldtemplate" runat="server" Visible="false">
                                                            <asp:ListView ID="datafield_listview" OnItemCommand="datafield_listview_ItemCommand" runat="server" ItemPlaceholderID="itemPlaceholder">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit</th>
                                                                                <th>SR NO</th>
                                                                                <th>TEMPLATE TYPE</th>
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
                                                                                        CommandArgument='<%# Eval("TEMPDATAFIELDCONFIGID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                                        TabIndex="9" />
                                                                                </td>

                                                                                <td>
                                                                                    <%# Eval("SRNO") %>
                                                                                </td>
                                                                                <td><%# Eval("TEMPLATETYPE") %></td>
                                                                                <td><%# Eval("DATAFIELDDISPLAY") %></td>
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
                </div>

                <div id="popup" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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

    <script>
        $(document).ready(function () {
            LoadTinyMCE();
        });


    </script>

    <script>
        var tinyBookmark;
        function setCursor() {
            if ($("#ctl00_ContentPlaceHolder1_ddlDataField option:selected").text() == "" || $("#ctl00_ContentPlaceHolder1_ddlDataField option:selected").text() == "Please Select") {
                //alert($("#ctl00_ContentPlaceHolder1_ddlDataField option:selected").text());
                return;
            }


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

            $('#txtTemplateBody').tinymce({
                script_url: '<%= ResolveUrl("~/plugins/TinyMce/tinymce.min.js") %>',
                placeholder: 'Enter the course content here ...',
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
            var ctl = document.getElementById('txtTemplateBody');
            var startPos = ctl.selectionStart;
            var endPos = ctl.selectionEnd;
            alert(startPos + ", " + endPos);
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
</asp:Content>

