<%@ Page Title="" Language="C#" MasterPageFile="~/BlankMasterPage.master" AutoEventWireup="true" CodeFile="SubExamCreation.aspx.cs" Inherits="ACADEMIC_MASTERS_SubExamCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updGrade" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGrade"
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

    <%-- <link href="../../DATA_TABLE/css/buttons.bootstrap.min.css" rel="stylesheet" />
    <script src="../../DATA_TABLE/jss/jquery.dataTables.min.js"></script>
    <script src="../../DATA_TABLE/jss/dataTables.bootstrap.min.js"></script>
    <script src="../../DATA_TABLE/jss/dataTables.buttons.min.js"></script>
    <script src="../../DATA_TABLE/jss/buttons.bootstrap.min.js"></script>
    <script src="../../DATA_TABLE/jss/buttons.colVis.min.js"></script>
    <script src="../../DATA_TABLE/jss/buttons.html5.min.js"></script>
    <script src="../../DATA_TABLE/jss/buttons.print.min.js"></script>
    <script src="../../DATA_TABLE/jss/jszip.min.js"></script>
    <script src="../../DATA_TABLE/jss/pdfmake.min.js"></script>
    <script src="../../DATA_TABLE/jss/vfs_fonts.js"></script>--%>


    <%--<style>
        .txtboxClass {
            font-size: inherit;
            font-family: inherit;
            padding: 5px 12px;
            letter-spacing: normal;
            background: #fff !important;
            color: black;
            border-radius: 5px;
            font-weight: 400;
            border-left: 6px solid #3c8dbc !important;
        }
    </style>--%>

    <asp:UpdatePanel ID="updGrade" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hftimeslot" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">SUB EXAM CREATION</h3>--%>
                             <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-md-12">
                                        <%--<asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Exam Pattern</label>--%>
                                            <asp:Label ID="lblDYddlExamPattern" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamPattern" runat="server" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" OnSelectedIndexChanged="ddlExamPattern_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlExamPattern" runat="server" ControlToValidate="ddlExamPattern"
                                            Display="None" ErrorMessage="Please Select Exam Pattern" ValidationGroup="submit" InitialValue="0"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Exam Name</label>--%>
                                            <label><asp:Label ID="lblDYddlExamName" runat="server" Font-Bold="true"></asp:Label> </label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamName" runat="server" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlExamName_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlExamName" runat="server" ControlToValidate="ddlExamName"
                                            Display="None" ErrorMessage="Please Select Exam Name" ValidationGroup="submit" InitialValue="0"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Subject Type</label>--%>
                                            <label><asp:Label ID="lblDYddlSubtype" runat="server" Font-Bold="true"></asp:Label> </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubID" runat="server" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSubID_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSubID"
                                            Display="None" ErrorMessage="Please Select Subject Type" ValidationGroup="submit" InitialValue="0"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                           <%-- <label>Sub Exam Name</label>--%>
                                            <label><asp:Label ID="lblDYtxtSubExamName" runat="server" Font-Bold="true"></asp:Label></label>
                                        </div>
                                        <asp:TextBox ID="txtsubExamName" runat="server" TabIndex="4" CssClass="form-control"
                                            ToolTip="Please Enter Sub Exam Name " placeholder="Enter Sub Exam Name." />
                                        <asp:RequiredFieldValidator ID="rfvGradeName" runat="server" ControlToValidate="txtsubExamName"
                                            Display="None" ErrorMessage="Please Enter Sub Exam Name" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Field Name</label>--%>
                                            <label><asp:Label ID="lblDYtxtFieldName" runat="server" Font-Bold="true"></asp:Label></label>
                                        </div>
                                        <asp:TextBox ID="txtFieldName" runat="server" TabIndex="4" CssClass="form-control"
                                            MaxLength="10" ToolTip="Please Enter Field Name " placeholder="Enter Field name." />
                                        <asp:RequiredFieldValidator ID="rfvtxtFieldName" runat="server" ControlToValidate="txtFieldName"
                                            Display="None" ErrorMessage="Please Enter Field Name" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Max Mark</label>--%>
                                            <label>Max Mark</label>
                                        </div>
                                        <asp:TextBox ID="txtMaxMark" runat="server" TabIndex="5" CssClass="form-control"
                                            MaxLength="10" ToolTip="Please Enter Max mark " placeholder="Enter Max mark." />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMaxMark"
                                            Display="None" ErrorMessage="Please Enter Max mark" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fltname" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtMaxMark" />
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>Status</label>--%>
                                            <label><asp:Label ID="lblDYStatus" runat="server"></asp:Label></label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActivetimeslot" name="switch" checked />
                                            <label data-on="Active" tabindex="5" class="newAddNew Tab" data-off="Inactive" for="rdActivetimeslot"></label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Fixed</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoFixed" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvfixed" runat="server" ControlToValidate="rdoFixed"
                                            Display="None" ErrorMessage="Please Select The Fixed" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                    TabIndex="5" OnClick="btnSave_Click" CssClass="btn btn-primary" OnClientClick="return validateSubexam();" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    TabIndex="6" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvGradeType" runat="server" OnItemDataBound="lvGradeType_ItemDataBound1">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="mytable">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action</th>
                                                        <th>Pattern Name</th>
                                                        <th>Exam Name</th>
                                                        <th>Subject Type</th>
                                                        <th>Sub Exam Name</th>
                                                        <%--   <th>Field Name</th>--%>
                                                        <th>Max Mark</th>
                                                        <th>Fixed</th>
                                                        <th>Status</th>

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
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("SUBEXAMNO") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("PATTERN_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("EXAMNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("SUBNAME")%>
                                                  
                                            </td>
                                            <td>
                                                <%# Eval("SUBEXAMNAME")%>
                                            </td>
                                            <%-- <td>
                                                <%# Eval("FLDNAME")%>
                                            </td>--%>

                                            <td>
                                                <%# Eval("MAXMARK")%>
                                            </td>
                                            <td>
                                                <%# Eval("FIXED")%>
                                            </td>
                                            <td>

                                                <%--<asp:CheckBox runat="server" ID="Chkstatus" ToolTip='<%# Eval("STATUS")%>' /> --%>
                                                <asp:Label ID="lblActive1" Text='<%# Eval("ACTIVESTATUS")%>' ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>

                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



    <script>
        function setsubexam(val) {

            $('#rdActivetimeslot').prop('checked', val);
            // $('#hftimeslot').val($('#rdActivetimeslot').prop('checked'));
        }

        function validateSubexam() {
            //alert("he");
            $('#hftimeslot').val($('#rdActivetimeslot').prop('checked'));

            var exmpattern = $("[id$=ddlExamPattern]").attr("id");
            var exmpattern = document.getElementById(exmpattern);
            if (exmpattern.value == 0) {
                alert('Please Select Exam Pattern', 'Warning!');
                $(exmpattern).focus();
                return false;
            }

            var exmname = $("[id$=ddlExamName]").attr("id");
            var exmname = document.getElementById(exmname);
            if (exmname.value == 0) {
                alert('Please Select Exam Name', 'Warning!');
                $(exmname).focus();
                return false;
            }

            var subtype = $("[id$=ddlSubID]").attr("id");
            var subtype = document.getElementById(subtype);
            if (subtype.value == 0) {
                alert('Please Select Subject type', 'Warning!');
                $(subtype).focus();
                return false;
            }

            var subexamname = $("[id$=txtsubExamName]").attr("id");
            var subexamname = document.getElementById(subexamname);
            if (subexamname.value == 0) {
                alert('Please Enter Sub Exam Name', 'Warning!');
                $(subexamname).focus();
                return false;
            }

            //var fieldname = $("[id$=txtFieldName]").attr("id");
            //var fieldname = document.getElementById(fieldname);
            //if (fieldname.value == 0) {
            //    alert('Please Enter Field Name', 'Warning!');
            //    $(fieldname).focus();
            //    return false;
            //}
            var MaxMark = $("[id$=txtMaxMark]").attr("id");
            var MaxMark = document.getElementById(MaxMark);
            if (MaxMark.value == 0) {
                alert('Please Enter MaxMark ', 'Warning!');
                $(MaxMark).focus();
                return false;

            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    // alert("hi");
                    validateSubexam();
                });
            });
        });

    </script>
</asp:Content>

