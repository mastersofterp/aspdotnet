<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Itle_Check_Descriptive_Test.aspx.cs" Inherits="Itle_Itle_Check_Descriptive_Test"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }

        #ctl00_ContentPlaceHolder1_pnllvQuestionsList .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">CHECKING DESCRIPTIVE TYPE TEST</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlCheck" runat="server">
                        <div class="col-12">
                            <asp:Panel ID="pnlDescriptive" runat="server">
                                <div class="row">

                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Session :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSession" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Current Date :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCurrdate" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-12 mt-2 mb-2">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Course Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCourseName" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>


                                    <div class="col-lg-5 col-md-6 col-12" id="trStudName" runat="server" visible="false">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudName" runat="server"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-5 col-md-6 col-12" id="trTestName" runat="server" visible="false">
                                        <asp:Label ID="lblTestName" runat="server"></asp:Label>
                                    </div>


                                    <div class="col-lg-6 col-md-6 col-12" id="trQuestion" runat="server" visible="false">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Question :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblQuestion" runat="server" Font-Bold="true" ForeColor="#0033cc"></asp:Label>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12" id="Div1" runat="server" visible="false">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Question :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="#0033cc"></asp:Label>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12" id="trAnswer" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Answer</label>
                                        </div>
                                        <asp:Panel ID="pnlReplyDesc" runat="server" BorderColor="Navy" BorderWidth="3px" Heigh="100px">
                                            <div style="height: 100px; background-color: #D1EDD1; padding-top: 25px; padding-left: 5px; padding-left: 5px;">
                                                <div id="divRepDesc" runat="server" style="height: 95%; padding-left: 10px; padding-top: 5px; padding-right: 5px; padding-bottom: 5px; background-color: White; border-radius: 25px; overflow: auto">
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12" id="trTotalMarks" runat="server" visible="false">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Total Marks :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblTotalMarks" runat="server"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trMarkObtained" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Marks Obtained</label>
                                        </div>
                                        <asp:TextBox ID="txtMarks" onkeyup="validateNumeric(this);" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trRemark" visible="false">
                                        <div class="label-dynamic">
                                            <label>Remark</label>
                                        </div>
                                        <div runat="server" id="tdRemark">
                                            <asp:TextBox ID="txtRemark" runat="server" MaxLength="100" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

                                        </div>
                                        <asp:CheckBox ID="chkChecked" runat="server" Text="Checked" Visible="false"></asp:CheckBox>

                                    </div>

                                    <div class="col-12 btn-footer mt-3 mb-3">
                                        <asp:HyperLink ID="linkBtnBack" runat="server" CssClass="btn btn-primary" Visible="false" NavigateUrl="~/ITLE/Itle_Check_Descriptive_Test.aspx">Back</asp:HyperLink>
                                    </div>

                                    <div class="col-12 btn-footer mt-3 mb-3" runat="server" id="trButtons" visible="false">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary"
                                                    OnClick="btnCancel_Click" ToolTip="CLick here to Go Back"></asp:Button>
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary"
                                                    OnClick="btnSubmit_Click" autopostback="true"
                                                    ValidationGroup="submit" ToolTip="Click here to Submit"></asp:Button>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnBack" />
                                                <asp:PostBackTrigger ControlID="btnSubmit" />
                                            </Triggers>
                                        </asp:UpdatePanel>


                                        <div class="col-12">
                                            <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                        <div class="col-12" id="pnlStudentList" runat="server" visible="false">
                            <div class="sub-heading">
                                <h5>Student List</h5>
                            </div>
                            <asp:Panel ID="pnllvStudentList" runat="server">
                                <asp:ListView ID="lvStudent" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr No</th>
                                                    <th>Action</th>
                                                    <th>Roll No</th>
                                                    <th>Student</th>
                                                    <th>Status</th>
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
                                                <asp:ImageButton ID="btnStudCheck" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("IDNO") %>'
                                                    AlternateText="Edit Record" ToolTip='<%# Eval("STUDNAME")%>' OnClick="btnStudCheck_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("ROLL_NO")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>                                                                 
                                            </td>
                                            <td>
                                                <asp:Image ID="imgStatus" runat="server" alt="Checked" ImageUrl="~/IMAGES/check1.jpg"
                                                    Width="15px" Height="15px" class='<%# (Eval("STATUS").ToString() =="1") ? "show_img" : "hide_img"%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <p class="text-center text-bold">

                                            <asp:Label ID="lblEmptyMsg" runat="server" Text="No Records Found"></asp:Label>
                                        </p>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>

                        <div class="col-12" id="pnlQuestions" runat="server" visible="false">
                            <div class="sub-heading">
                                <h5>Question Set</h5>
                            </div>

                            <asp:Panel ID="pnllvQuestionsList" runat="server">
                                <asp:ListView ID="lvQuestions" runat="server" OnItemDataBound="lvQuestions_ItemDataBound">
                                    <LayoutTemplate>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>

                                                    <th>Q.No</th>
                                                    <th>Action</th>
                                                    <th>Questions</th>
                                                    <th>Status</th>
                                                    <th>Total Marks</th>
                                                    <th>Marks Obtained</th>
                                                    <th>Checked</th>
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
                                                <asp:ImageButton ID="btnQuestion" runat="server" ImageUrl="~/images/edit1.png" CommandArgument='<%# Eval("QUESTIONNO") %>'
                                                    ToolTip='<%# Eval("QUESTIONTEXT")%>' AlternateText='<%# Eval("QUESTION_MARKS")%>'
                                                    OnClick="btnQuestion_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("QUESTIONTEXT")%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbStatus" runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <%# Eval("QUESTION_MARKS")%>                                                                   
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMarksObtained" runat="server" Text='<%# Eval("MARKS_OBTAINED")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Image ID="imgStatus" runat="server" alt="Checked" ImageUrl="~/IMAGES/check1.jpg"
                                                    Width="15px" Height="15px" class='<%# (Eval("CHECKED").ToString() == "1")? "show_img": "hide_img" %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <p class="text-center text-bold">

                                            <asp:Label ID="lblEmptyMsg" runat="server" Text="No Records Found"></asp:Label>
                                        </p>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>

                        <div class="col-12 mt-3" id="dvTestList" runat="server">
                            <div class="sub-heading">
                                <h5>Descriptive Test List</h5>
                            </div>
                            <asp:Panel ID="pnlCheckTestList" runat="server">
                                <asp:ListView ID="lvCheckTest" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>

                                                    <th>Sr No</th>
                                                    <th>Test Name</th>
                                                    <th>Total Questions</th>
                                                    <th>Total Marks</th>
                                                    <th>Check Test</th>
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
                                                <%# Eval("TESTNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("TOTQUESTION")%>
                                            </td>
                                            <td>
                                                <%# Eval("TOTAL")%>                                                                
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnCheckTest" runat="server" CommandArgument='<%# Eval("TESTNO")%>'
                                                    ImageUrl="~/IMAGES/search.png" OnClick="imgbtnCheckTest_Click"></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <p class="text-center text-bold">
                                            <asp:Label ID="lblEmptyMsg" runat="server" Text="No Records Found"></asp:Label>
                                        </p>
                                    </EmptyDataTemplate>
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

    <%--  Enable the button so it can be played again --%>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

    </script>

    <style type="text/css">
        .hide_img {
            display: none;
        }

        .show_img {
            display: block;
        }
    </style>
</asp:Content>
