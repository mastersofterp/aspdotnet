<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Improv_ExamRegistration.aspx.cs" Inherits="ACADEMIC_Improv_ExamRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="dvMain" runat="server">


        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">IMPROVEMENT EXAM REGISTRATION</h3>
                        <br />
                    </div>

                    <div class="box-body">
                        <div id="pnlSearch" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <div id="divenroll" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>PRN No</label>
                                        </div>
                                        <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control" ToolTip="Enter text to search." TabIndex="1" MaxLength ="15"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="txtEnrollno" />
                                        <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtEnrollno"
                                            Display="None" ErrorMessage="Please Enter RRN No." SetFocusOnError="true"
                                            ValidationGroup="search" />

                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="search" />
                                <asp:Button ID="btnSearch" runat="server" Text="Show" ValidationGroup="search" CssClass="btn btn-primary" TabIndex="2" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" ValidationGroup="Show" TabIndex="3" OnClick="btnClear_Click"
                                    CssClass="btn btn-warning" />
                            </div>
                        </div>

                        <div class="col-12" id="divCourses" runat="server" visible="false">

                            <asp:UpdatePanel ID="updatepnl" runat="server">
                                <ContentTemplate>
                                    <%--<ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopup" runat="server"
                                        TargetControlID="div" PopupControlID="div"
                                        OkControlID="btnOk" OnOkScript="okClick();"
                                        CancelControlID="btnNo" OnCancelScript="cancelClick();" BackgroundCssClass="modalBackground" />
                                    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                                        <div style="text-align: center">
                                            <table>
                                                <tr>

                                                    <td>&nbsp;&nbsp;
                            <h6>Are You Sure You Want To Proceed ? After Done You Never Make Any Changes </h6>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:Button ID="btnOk" runat="server" Text="Yes" CssClass="btn btn-primary" />
                                                        <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-warning" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>--%>
                                    <div class="row">

                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>PRN No :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="true" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Student Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblName" runat="server" Font-Bold="true" /></a>
                                                </li>
                                               <li class="list-group-item"><b>Degree / Branch :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Year :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblYear" runat="server" Font-Bold="true" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Semester/Trimester :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="true" /></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">

                                                
                                                <li class="list-group-item"><b>Scheme :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblScheme" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Admission Batch :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="true" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Is Processing Fee:</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblfessapplicable" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item "><b>Subjects Fee :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblTotalExamFee" runat="server" Font-Bold="true" /></a>
                                                    <asp:Label ID="lblOrderID" runat="server" CssClass="data_label" Visible="false">0.00</asp:Label>

                                                </li>
                                                <li class="list-group-item"><b>Total Fee (Processing Fee + Subjects Fee) : </b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblFinalTotal" runat="server" Font-Bold="true" /></a>
                                                </li>

                                                <asp:HiddenField ID="hdfCategory" runat="server" />
                                                <asp:HiddenField ID="hdfDegreeno" runat="server" />

                                            </ul>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <%--<asp:Button ID="btnShow" runat="server" Text="Show Courses" ValidationGroup="backsem" TabIndex="6"
                                                CssClass="btn btn-primary" OnClick="btnShow_Click" />--%>
                                            <%--<asp:Button ID="btnReport" runat="server" Text="Print Reciept" TabIndex="7"
                                                ValidationGroup="backsem" CssClass="btn btn-primary" Visible="false" OnClick="btnReport_Click" />--%>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="backsem"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>

                                        <div class="col-12">

                                            <asp:Panel ID="pnlCourse" runat="server">

                                                <asp:ListView ID="lvCourse" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Courses List</h5>
                                                        </div>
                                                        <div style="width: 100%; overflow: auto">
                                                            <table id="tblBacklogSubjects" class="table table-striped table-bordered nowrap">
                                                                <thead>
                                                                    <tr>
                                                                        <th >

                                                                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                                                                            Select All

                                                                        </th>
                                                                        <%-- <th>Course Code
                                                                </th>--%>
                                                                        <th >Course Name
                                                                        </th>
                                                                        <th style="text-align: center">CREDIT
                                                                        </th>
                                                                        <th style="text-align: center">Course Type
                                                                    <%--Theory/Prac--%>
                                                                        </th>
                                                                        <%-- <th style="text-align: center">Grade--%>
                                                                        <%--Theory/Prac--%>
                                                                        <%--</th>--%>
                                                                        <%--<th style="text-align: center">Exam Type
                                                                   
                                                                        </th>--%>
                                                                        <th id="BatchTheory1" style="text-align: center">Exam Fees
                                                                    <%--Theory/Prac--%>
                                                                            <%--<th id="BatchTheory1">Mode of Exam </th>--%>
                                                                        </th>
                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </thead>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr id="trCurRow" class="item">
                                                            <td>
                                                                <%--<asp:CheckBox ID="chkAccept" runat="server" AutoPostBack="true" Checked='<%# Eval("EXAM_REGISTERED").ToString().Equals("1") %>' OnCheckedChanged="chkAccept_CheckedChanged"/>--%>
                                                                <asp:CheckBox ID="chkAccept" runat="server" AutoPostBack="true" OnCheckedChanged="chkAccept_CheckedChanged" />
                                                               <%-- Checked='<%# Convert.ToBoolean(Eval("EXAM_REGISTERED") = 0 ? false : true) %>'--%>
                                                            </td>
                                                            <%-- <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>'  />
                                                    </td>--%>
                                                            <td>
                                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                                <asp:HiddenField ID="hdfExamRegistered" runat="server" Value='<%# Eval("EXAM_REGISTERED") %>' />
                                                                <%--<asp:HiddenField ID="hdfStudRegistered" runat="server" Value='<%# Eval("STUD_EXAM_REGISTERED") %>' />--%>
                                                            </td>
                                                            <td style="text-align: center">
                                                                <asp:Label ID="lblcredits" runat="server" Text=' <%# Eval("CREDITS") %>' />
                                                                <%-- <%# Eval("SEMESTER") %>--%>
                                                            </td>
                                                            <td align="center">
                                                                <%-- <%# (Eval("SUBID").ToString()) == "1" ? "Theory" : "Practical" %>--%>
                                                                <%# Eval("SUBNAME") %>
                                                            </td>
                                                            <%--  <td align="center">
                                                        <%# Eval("GRADE") %>
                                                    </td>--%>
                                                           <%-- <td align="center">
                                                                <asp:Label ID="lblExamType" runat="server" Text='<%# Eval("EXAM_TYPE") %>' ToolTip='<%# Eval("EXAM_TYPENO")%>' />
                                                            </td>--%>
                                                            <td align="center">
                                                                <asp:Label ID="lblAmt" runat="server" Text=' <%# Eval("FEE") %>' />
                                                                <%--  <%# Eval("FEE") %>--%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>

                                                </asp:ListView>

                                            </asp:Panel>

                                        </div>


                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSearch" />
                                    <asp:PostBackTrigger ControlID="btnCancel" />
                                    <asp:PostBackTrigger ControlID="btnSearch" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnPay" runat="server" Text="Submit & Pay Online" CausesValidation="false"
                                    Font-Bold="true" CssClass="btn btn-success" TabIndex="2" OnClientClick="return showConfirm();" OnClick="btnPay_Click" />

                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CausesValidation="false"
                                    Font-Bold="true" CssClass="btn btn-success" TabIndex="3" OnClick="btnSubmit_Click" />

                                <asp:Button ID="btnPrintRegSlip" runat="server" Text="Report"
                                    CssClass="btn btn-primary" TabIndex="3" OnClick="btnPrintRegSlip_Click" />

                                 <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="4"
                                    CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancel_Click" />

                            </div>
                        </div>

                    </div>


                </div>
            </div>
        </div>

        <div id="divMsg" runat="server">
        </div>

    </div>

     <script type="text/javascript">
         function showConfirm() {
             var ret = confirm('Are You Sure You Want To Proceed ? After Done You Never Make Any Changes');
             if (ret == true) {
                 validate = true;
             }
             else
                 validate = false;
             return validate;
         }

    </script>

    <%--<script type="text/javascript">
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirm(source) {
            this._source = source;
            this._popup = $find('mdlPopup');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>--%>
</asp:Content>


