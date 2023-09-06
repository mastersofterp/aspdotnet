<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
     CodeFile="ExamTypeMaster.aspx.cs" Inherits="ACADEMIC_EXAMINATION_ExamTypeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
    <asp:UpdatePanel ID="updGrade" runat="server">
        <ContentTemplate>
               <asp:HiddenField ID="hfExamtype" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">EXAM TYPE</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>

                                    <div class="form-group col-md-3">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Type Name</label>
                                        </div>
                                        <asp:TextBox ID="txtExamtype" runat="server" TabIndex="1" placeholder="Enter Exam Type Name              "
                                            MaxLength="100" ToolTip="Please Enter Exam Type Name" Width="100%" />
                                        <asp:RequiredFieldValidator ID="rfvExamtype" runat="server" ControlToValidate="txtExamtype"
                                            Display="None" ErrorMessage="Please Enter Exam Type Name" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-2">
                                    <label>Status</label>

                                    <div class="switch form-inline">
                                        <input type="checkbox" id="rdActivegarde" name="switch" checked tabindex="2" />
                                        <label data-on="Active" tabindex="4" class="newAddNew Tab" data-off="Inactive" for="rdActivegarde"></label>
                                    </div>
                                </div>
                                    </div>
                                </div>


                                      <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="Save"
                                    TabIndex="3"  CssClass="btn btn-primary" OnClientClick="return validateGrade();" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    TabIndex="4"  CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Save"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            

                            <div class="col-12">
                                <table class="table table-striped table-bordered display " style="width: 100%">
                                    <asp:Repeater ID="lvExamtype" runat="server">
                                        <HeaderTemplate>
                                            <div class="sub-heading">
                                                <h5>Exam Type Name List</h5>
                                            </div>

                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <%--<th>Grade Type
                                                    </th>--%>
                                                    <th>Exam Type Name
                                                    </th>
                                                    <th>
                                                        Status
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%--<tr id="itemPlaceholder" runat="server" />--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("EXAM_TYPENO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEdit_Click" />
                                                </td>
                                               <%-- <td>
                                                    <%# Eval("GRADE_TYPE")%>
                                                </td>--%>
                                                <td>
                                                    <%# Eval("EXAM_TYPE")%>
                                                </td>
                                                <td>
                                                     <asp:Label ID="lblActiveExamType" Text='<%# Eval("ACTIVESTATUS")%>' ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>

                        </div>


                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

     <%-- Exam Type Master--%>
    <script>
        function settimeslot(val) {

            $('#rdActivegarde').prop('checked', val);
            // $('#hftimeslot').val($('#rdActivetimeslot').prop('checked'));
        }

        function validateGrade() {
            //alert("he");
            $('#hfExamtype').val($('#rdActivegarde').prop('checked'));

            var Gradename = $("[id$=txtExamtype]").attr("id");
            var Gradename = document.getElementById(Gradename);
            if (Gradename.value == 0) {
                alert('Please Enter Exam Type Name', 'Warning!');
                $(Gradename).focus();
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    // alert("hi");
                    validateGrade();
                });
            });
        });

    </script>

</asp:Content>


