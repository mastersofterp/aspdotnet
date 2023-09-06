<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Lms_ExamMapping.aspx.cs" Inherits="Itle_Lms_ExamMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   

    
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
                    <h3 class="box-title">EXAM MAPPING</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlSyllabusMaster" runat="server">
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
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Sub Exam</label>
                                        </div>
                                        <asp:DropDownList ID="ddlexam" runat="server" AppendDataBoundItems="true" TabIndex="1" 
                                                            AutoPostBack="true" CssClass="form-control" ToolTip="Select Exam" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                                            ControlToValidate="ddlexam" ErrorMessage="Select SubExam Name" InitialValue="0"
                                            ValidationGroup="add"></asp:RequiredFieldValidator>
                                       
                                         
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Test Type</label>
                                        </div>
                                        <asp:RadioButtonList ID="rbtTestType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtTestType_SelectedIndexChanged"
                                        Font-Bold="true" >
                                        <asp:ListItem Value="A" Text="Assignment" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="D" Text="Descriptive"></asp:ListItem>
                                        <asp:ListItem Value="O" Text="Objective"></asp:ListItem>
                                    </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvrbtest" runat="server"
                                            ControlToValidate="rbtTestType" ErrorMessage="Please Enter Topic Name"
                                            ValidationGroup="add" ></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Test</label>
                                        </div>
                                        <asp:DropDownList ID="ddltest" runat="server" AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true"
                                                            AutoPostBack="true" CssClass="form-control" ToolTip="Select Test"  >
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="ddltest" ErrorMessage="Select Test Name" InitialValue="0"
                                            ValidationGroup="add">*</asp:RequiredFieldValidator>
                                       
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Weightage</label>
                                        </div>
                                        <asp:TextBox ID="txtweightage" runat="server" CssClass="form-control" TabIndex="2"
                                            ToolTip="Enter Weightage"></asp:TextBox>
                                          <ajaxToolKit:FilteredTextBoxExtender ID="FiBoxE7" runat="server" FilterType="Custom, Numbers" TargetControlID="txtweightage" ValidChars=".0123456789"></ajaxToolKit:FilteredTextBoxExtender>

                                                                                                                      
                                        
                                       <%-- <asp:RequiredFieldValidator ID="Rfvweigtage" runat="server" ControlToValidate="txtweightage"
                                            ErrorMessage="Please Select Test" ValidationGroup="add" Display="none"></asp:RequiredFieldValidator>--%>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                        ControlToValidate="txtweightage" Display="none"
                                                                        ErrorMessage="Enter Weightage " SetFocusOnError="True"
                                                                        ValidationGroup="add"></asp:RequiredFieldValidator>
                                       
                                    </div>

                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:UpdatePanel ID="UpdExams" runat="server">
                                        <ContentTemplate>
                                             <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnadd_Click"
                                                 ToolTip="Click here to Add" TabIndex="8"  ValidationGroup="add"/>
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-warning" OnClick="btnClear_Click"
                                                 ToolTip="Click here to Reset" TabIndex="7" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ValidationGroup="add"
                                                ShowSummary="false" DisplayMode="List"  />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmit" />
                                            <asp:PostBackTrigger ControlID="btnClear" />
                                            <asp:PostBackTrigger ControlID="btnadd" />
                                           
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12">
                                    <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                </div>

                            </asp:Panel>
                        </div>

                        <div class="col-12" id="EXAMMAPLIST" runat="server" visible="false">
                            <div class="sub-heading">
                                <h5>Exam List</h5>
                            </div>
                            <div class="table-responsive">
                                <asp:Panel ID="exammap" runat="server">
                                    <asp:ListView ID="lvexammapping" runat="server">
                                        <LayoutTemplate>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                             <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Remove</th>
                                                        <th>Sub Exam</th>
                                                        <th>Test Type</th>
                                                        <th>Test</th>
                                                        <th>Weightage %</th>
                                                       
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
                                                    <asp:ImageButton ID="btnremovemapping" runat="server" CommandArgument='<%# Eval("Exam_SRNO") %>'
                                                      ImageUrl="~/images/delete.png" ToolTip="Remove Record"  OnClick="btnremovemapping_Click" />
                                                </td>
                                                <td>
                                                     <asp:Label ID="lblsubexamname" runat="server" Text='<%# Eval("SUBEXAM_NAME") %>'></asp:Label>
                                                   <asp:HiddenField  ID="hdnsubexamno" runat="server" Value='<%# Eval("SUBEXAM_NO") %>'/>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbltesttype" runat="server" Text='<%# Eval("TEST_TYPE") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdntesttype" runat="server" Value='<%# Eval("TEST_CODE") %>' />
                                                </td>
                                                <td>
                                                     <asp:Label ID="lbltest" runat="server" Text='<%# Eval("TEST") %>'></asp:Label>
                                                     <asp:HiddenField  ID="hdntestno" runat="server" Value='<%# Eval("TEST_NO") %>'/>                                                              
                                                </td>
                                                <td>
                                                     <asp:Label ID="lblweightage" runat="server" Text='<%# Eval("WEIGHTAGE") %>'></asp:Label>
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

                         
                        <div class="col-12" id="Div1" runat="server" >
                            <div class="sub-heading">
                                <h5>Exam Mapping List</h5>
                            </div>
                            <div class="table-responsive">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="MainMappingList" runat="server">
                                        <LayoutTemplate>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit</th>
                                                        <th>Session</th>
                                                        <th>Course</th>
                                                        <th>Test</th>
                                                        <th>Weightage %</th>
                                                       
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("SR_NO") %>'
                                                      ImageUrl="~/Images/edit.png" ToolTip="Remove Record"  OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                     <asp:Label ID="lblsubexamname" runat="server" Text='<%# Eval("SESSION_NAME") %>'></asp:Label>
                                                      </td>
                                                <td>
                                                    <asp:Label ID="lbltesttype" runat="server" Text='<%# Eval("COURSE_NAME") %>'></asp:Label>
                                                    </td>
                                                <td>
                                                     <asp:Label ID="lbltest" runat="server" Text='<%# Eval("TEST") %>'></asp:Label>
                                                       </td>
                                                <td>
                                                     <asp:Label ID="lblweightage" runat="server" Text='<%# Eval("WEIGHTAGE") %>'></asp:Label>
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
                            <div class="col-12 btn-footer"  id="BTNSUB" runat="server">
                              <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="6" 
                                                 ValidationGroup="submit" ToolTip="Click here to Submit"  OnClick="btnSubmit_Click" />
                                </div>
                        </div>
                    </asp:Panel>
                </div>
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

</asp:Content>


