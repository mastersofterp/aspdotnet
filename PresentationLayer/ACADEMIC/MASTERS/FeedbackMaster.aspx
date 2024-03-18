<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FeedbackMaster.aspx.cs" Inherits="ACADEMIC_MASTERS_FeedbackMaster" Title="" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

    <asp:UpdatePanel ID="updGrade" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red" Visible="false"></asp:Label>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Feedback Type</label>
                                        </div>
                                        <asp:TextBox ID="txtFeedbackName" runat="server" TabIndex="1" CssClass="form-control"
                                            MaxLength="70" ToolTip="Please Enter Feedback Type " placeholder="Enter Feedback Type here." />
                                        <asp:RequiredFieldValidator ID="rfvFeedbackName" runat="server" ControlToValidate="txtFeedbackName"
                                            Display="None" ErrorMessage="Please Enter Feedback Type" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Feedback Mode</label>
                                        </div>
                                        <asp:DropDownList ID="ddlfeedbackmode" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control"
                                            data-select2-enable="true">
                                            <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlfeedbackmode"
                                            Display="None" ErrorMessage="Please Select Feedback Mode" ValidationGroup="submit" InitialValue="0"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Active Status</label>
                                        </div>
                                        <asp:CheckBox ID="chkActiveStatus" runat="server" TextAlign="Left" Checked="true" />
                                    </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                        <%--<asp:Button ID="btnNote" runat="server" Text="Add Note " ToolTip="Add Note" Visible="false" ValidationGroup="submit1"
                                            TabIndex="2" CssClass="btn btn-primary" OnClick="btnNote_Click" />--%>

                                        <asp:LinkButton ID="btnNote" runat="server" Text="Add Note" CssClass="btn btn-primary"  OnClick="btnNote_Click"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Course Type : </label>
                                        </div>

                                        <asp:RadioButton ID="rdoTheory" runat="server" Text="Theory" GroupName="act_status"
                                            TabIndex="3" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoPractical" runat="server" Text="Practical" GroupName="act_status"
                                            TabIndex="3" />
                                        <asp:RadioButton ID="rdoNone1" runat="server" Text="None" GroupName="act_status" Checked="true" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Choice For : </label>
                                        </div>

                                        <asp:RadioButton ID="rdoStudent" runat="server" Text="Student" GroupName="act_faculty"
                                            TabIndex="4" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoFaculty" runat="server" Text="Faculty" GroupName="act_faculty"
                                            TabIndex="4" />
                                        <asp:RadioButton ID="rdoNone2" runat="server" Text="None" GroupName="act_faculty" Checked="true" />

                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                    TabIndex="2" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    TabIndex="3" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <asp:Repeater ID="lvFeedback" runat="server">
                                        <HeaderTemplate>
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action</th>
                                                    <%-- <th>Grade Type</th>--%>
                                                    <th>Feedback Type</th>
                                                    <th>Feedback Mode</th>
                                                    <th>Course Type</th>
                                                    <th>Choice For</th>
                                                    <th>Active Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("FEEDBACK_NO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEdit_Click" />
                                                </td>
                                                <%-- <td>
                                                        <%# Eval("GRADE_TYPE")%>
                                                    </td>--%>
                                                <td>
                                                    <%# Eval("FEEDBACK_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FEEDBACK_MODE_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("COURSE_TYPE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CHOISE_FOR")%>
                                                </td>
                                                <td>
                                                    <%# Eval("IS_ACTIVE")%>
                                                </td>
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

   <%-- Added by sakshi M on 14-03-2024--%>
     <!-- The Modal -->
    <div class="modal" id="myModalCourse" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" style="height:500px; width:720px">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">FeedBack Note</h4>
                    <button type="button" class="close" data-dismiss="modal"></button>
                </div>
                <!-- Modal body -->
                <div class="modal-body" >
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                                <asp:Panel ID="Panel1" runat="server" >
                                            <div class="row">
                                                    <FTB:FreeTextBox ID="ftbDesc" runat="Server" Height="200px" Width="700px" TabIndex="1" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ftbDesc"
                                                        Display="None" ErrorMessage="Please Enter Feedback Note." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                </asp:Panel>
                       
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <!-- Modal footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Add</button>
                </div>
                <asp:HiddenField ID="hdfDyanamicTabId" runat="server" />
            </div>
        </div>
    </div>

     <script>
         $(function () {
             $("#btnNote").click(function () {
                 showModal();
             });
         });

         function showModal() {
             $("#myModalCourse").modal('show');
         }
    </script>
</asp:Content>


