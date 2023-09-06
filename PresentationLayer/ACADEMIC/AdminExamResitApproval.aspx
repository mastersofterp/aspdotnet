<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdminExamResitApproval.aspx.cs" Inherits="ACADEMIC_AdminExamResitApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="dvMain" runat="server">


        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">RE MAJOR EXAM REGISTRATION</h3>
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
                                        <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control" ToolTip="Enter text to search." TabIndex="1" MaxLength="15"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="txtEnrollno" />
                                        <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtEnrollno"
                                            Display="None" ErrorMessage="Please Enter PRN No." SetFocusOnError="true"
                                            ValidationGroup="search" />

                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="search" />
                                <asp:Button ID="btnSearch" runat="server" Text="Show" ValidationGroup="search" CssClass="btn btn-primary" TabIndex="1" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" ValidationGroup="Show" TabIndex="1" OnClick="btnClear_Click"
                                    CssClass="btn btn-warning" />
                            </div>
                        </div>

                        <div class="col-12" id="divCourses" runat="server" visible="false">

                            <asp:UpdatePanel ID="updatepnl" runat="server">
                                <ContentTemplate>

                                    <div class="row">



                                         <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Student Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblName" runat="server" Font-Bold="true" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>RRN No :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="true" /></a>
                                        </li>                                      
                                     
                                   
                             
                                            <%--<ul class="list-group list-group-unbordered">--%>
                                                <li class="list-group-item"><b>Year :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblYear" runat="server" Font-Bold="true" />
                                                    </a>
                                                </li>

                                                <li class="list-group-item"><b>Scheme :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblScheme" runat="server" Font-Bold="true" /></a>
                                                </li>

                                                <li class="list-group-item"><b>Admission Batch :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="true" />
                                                    </a>
                                                </li>


                                            </ul>
                                        </div>
                                        
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">                                    
                                     

                                       <li class="list-group-item"><b>Is Processing Fee:</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblfessapplicable" runat="server" Font-Bold="true" /></a>
                                        </li>
                                        <li class="list-group-item"><b>Subjects Fee :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblTotalExamFee" runat="server" Font-Bold="true" /></a>
                                            <asp:Label ID="lblOrderID" runat="server" CssClass="data_label" Visible="false">0.00</asp:Label>
                                         
                                        </li>
                                         <li class="list-group-item" ><b>Total Fee :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="FinalTotal" runat="server" Font-Bold="true" /></a>
                                        </li>
                                     
                                        
                                    </ul>
                                </div>

                                        <div class="form-group col-lg-2 col-md-6 col-12 mt-4">
                                            <div class="label-dynamic">
                                                <label style="font-weight: 600; font-size:14px">   Semester</label></div>
                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12 mt-4">
                                            <div class="label-dynamic">
                                                <%--   <label>Semester/Trimester </label>
                                                    <sup>*</sup>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Semester." AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            </asp:DropDownList>
                                             <asp:HiddenField ID="hdfCategory" runat="server" />
                                                    <asp:HiddenField ID="hdfDegreeno" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">

                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="backsem"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>


                                    <asp:Panel ID="pnlFailCourse" runat="server" Visible="false">
                                       
                                            <asp:ListView ID="lvFailCourse" runat="server" OnItemDataBound="lvFailCourse_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Courses List</h5>
                                                    </div>
                                                    <div style="width: 100%; overflow: auto">
                                                        <table id="tblBacklogSubjects" class="table table-striped table-bordered nowrap">
                                                            <thead>
                                                                <tr>
                                                                    <th>
                                                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" /><%----%>
                                                                    Select All
                                                                    </th>
                                                                    <th>Course Code
                                                                    </th>
                                                                    <th>Course Name
                                                                    </th>
                                                                    <th style="text-align: center">SEMESTER
                                                                    </th>
                                                                    <th style="text-align: center">Course Type
                                                                    </th>
                                                                    <th style="text-align: center">GRADE
                                                                    </th>
                                                                    <th id="BatchTheory1" style="text-align: center">Exam Fees
                                                                   
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
                                                            <%--<asp:CheckBox ID="chkAccept" runat="server" AutoPostBack="true"--%>
                                                                <%--Checked='<%# Eval("ABSENT_LOG").ToString().Equals("1") %>' />--%>
                                                             <asp:CheckBox ID="chkAccept" runat="server" AutoPostBack="true" OnCheckedChanged="chkAccept_CheckedChanged"
                                                                />
                                                        </td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                            <asp:HiddenField ID="hdfExistingMark" runat="server" Value='<%# Eval("EXTERMARK") %>' />
                                                            <asp:HiddenField ID="hdfapplycourse" runat="server" Value='<%# Eval("APPLYCOURSE") %>' />
                                                            <asp:HiddenField ID="hdfapplycoursedone" runat="server" Value='<%# Eval("ABSENT_LOG") %>' />


                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblsem" runat="server" Text=' <%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTER")%>' />
                                                        </td>
                                                        <td align="center">
                                                            <%# Eval("SUBNAME") %>
                                                        </td>
                                                        <td align="center">
                                                            <%# Eval("GRADE") %>
                                                        </td>
                                                        <td align="center">
                                                        <asp:Label ID="lblAmt" runat="server" Text=' <%# Eval("FEE") %>'  ToolTip='<%# Eval("FEE")%>' />
                                                   
                                                         
                                                    </td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                       
                                        <div  id="divbtn" runat="server" class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" CausesValidation="false" OnClick="btnSubmit_Click"
                                                Font-Bold="true" CssClass="btn btn-success" TabIndex="1" OnClientClick="return showConfirm();" />
                                         <%--  <asp:Button ID="btnsubmitwithnofee" runat="server" Text="Submit Without Fees" CausesValidation="false" OnClick="btnsubmitwithnofee_Click"
                                                Font-Bold="true" CssClass="btn btn-success" TabIndex="1" Visible="false" OnClientClick="return showConfirmWithnofee();" />--%>
                                             <asp:Button ID="btnapprove" runat="server" Text="Approval" CausesValidation="false" OnClick="btnApprove_Click"
                                                Font-Bold="true" CssClass="btn btn-success" TabIndex="1" Visible="false" />
                                                    <asp:Button ID="btnsubmitwithnofee" runat="server" Text="Submit Without Fees" CausesValidation="false" OnClick="btnsubmitwithnofee_Click"
                                                Font-Bold="true" CssClass="btn btn-success" TabIndex="1" Visible="false" OnClientClick="return showConfirmWithnofee();" />
                                              <asp:Button ID="btnreport" runat="server" Text="Report" CausesValidation="false" OnClick="btnreport_Click"
                                                Font-Bold="true" CssClass="btn btn-success" TabIndex="1" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" Visible="true"
                                                CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancel_Click" />
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSearch" />
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                    <asp:PostBackTrigger ControlID="btnCancel" />
                                    <asp:PostBackTrigger ControlID="ddlSemester" />
                                     <asp:PostBackTrigger ControlID="btnreport" />

                                </Triggers>
                            </asp:UpdatePanel>
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
            var ret = confirm('Are You Sure You Want To Proceed ?');
            if (ret == true) {
                validate = true;
            }
            else
                validate = false;
            return validate;
        }
        function showConfirmWithnofee() {
            var msg = confirm('Are you Sure You Want To Proceed Without Payment ?')
            if (msg == true) {
                validate = true;
            }
            else {

                validate = false;
                return validate
            }
        }



    </script>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            debugger
            $(document).ready(function () {
                var table = $('#my-table').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#my-table').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                    {
                                        extend: 'copyHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#my-table').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#my-table').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#my-table').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });
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

