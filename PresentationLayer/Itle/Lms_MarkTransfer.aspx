<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Lms_MarkTransfer.aspx.cs" Inherits="Lms_MarkTransfer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>

     <link href="../CSS/master.css" rel="stylesheet" />

    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>



    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">MARK TRANSFER</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlMarkTransfer" runat="server">
                        <div class="col-12">
                            <asp:Panel ID="pnlSyllabus" runat="server">
                                <div class="row">

                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Session :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-3 col-md-6 col-12 mb-2">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Current Date :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCurrdate" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-12 mb-3">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Course Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCourseName" runat="server" Font-Bold="True"></asp:Label>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Tr1" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Subject Name</label>
                                        </div>
                                        <asp:TextBox ID="txtSyllabus" runat="server" CssClass="form-control" TabIndex="1"
                                            ToolTip="Subject Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSyllabus"
                                            ErrorMessage="Enter the Syllabus Name" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-4 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Sub Exam</label>
                                </div>
                               
                                        <%--<asp:ListBox ID="ddlexam" runat="server" AppendDataBoundItems="true"   CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>  
                                   
                                      --%>                    
                                                       
                                <asp:DropDownList ID="ddlexam" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                    AutoPostBack="true" CssClass="form-control" ToolTip="Select Exam" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                                    ControlToValidate="ddlexam" ErrorMessage="Select SubExam Name" InitialValue="0"
                                    ValidationGroup="add"></asp:RequiredFieldValidator>


                            </div>
                            <div class="col-lg-4 col-md-6 col-12 mt-4 mb-4">
                                <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-primary" ToolTip="Click here to Show" OnClick="btnshow_Click" />
                                <asp:Button ID="btntransfer" runat="server" Text="Transfer" CssClass="btn btn-primary" ToolTip="Click here to Transfer" OnClick="btntransfer_Click"/>
                            </div>
                        </div>
                    </div>

                    <div class="col-12" id="ExamMapStudentList" runat="server" >
                            <div class="sub-heading">
                                <h5>Student List </h5>
                            </div>
                            <div class="table-responsive">
                                <asp:Panel ID="studlist" runat="server">
                                    <asp:ListView ID="lvstudentlist" runat="server">
                                        <LayoutTemplate>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                             <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sub Exam</th>
                                                        <th>RRN</th>
                                                        <th>Student Name</th>
                                                        <th>Mark</th>
                                                       
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
                                                     <asp:Label ID="lblsubexamname" runat="server" Text='<%# Eval("EXAM_NAME") %>'></asp:Label>
                                              <%--     <asp:HiddenField  ID="hdnsubexamno" runat="server" Value='<%# Eval("SUBEXAM_NO") %>'/>
                                        --%>        </td>
                                                <td>
                                                    <asp:Label ID="lbltesttype" runat="server" Text='<%# Eval("ROLLNO") %>'></asp:Label>
                                                <%--    <asp:HiddenField ID="hdntesttype" runat="server" Value='<%# Eval("TEST_CODE") %>' />
                                           --%>     </td>
                                                <td>
                                                     <asp:Label ID="lbltest" runat="server" Text='<%# Eval("STUD_NAME") %>'></asp:Label>
                                              <%--       <asp:HiddenField  ID="hdntestno" runat="server" Value='<%# Eval("TEST_NO") %>'/>                                                              
                                      --%>          </td>
                                                <td>
                                                     <asp:Label ID="lblweightage" runat="server" Text='<%# Eval("STUD_FINAL_MARK") %>'></asp:Label>
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
                           
                        </div>
                    </div>

                    <%-- list view--%>
                </div>




            </div>
        </div>

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

            var _source;

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
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });


    </script>
</asp:Content>

