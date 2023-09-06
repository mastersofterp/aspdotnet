<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Intermediate_Grade_Configuration.aspx.cs" Inherits="ACADEMIC_EXAMINATION_Intermediate_Grade_Configuration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>


    <style>
        .fa-plus {
            color: green;
            border: 1px solid green;
            border-radius: 50%;
            padding: 6px 8px;
            font-size: 16px;
        }

        .fa-edit, .fa-eye {
            color: #4c6ef5;
            font-size: 14px;
        }

        #tbladd .table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5;
        }
    </style>
    <asp:UpdatePanel ID="updGradeConfiguration" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Intermediate Grade Configuration</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School - Scheme </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchool" runat="server" CssClass="form-control" ToolTip="Please Select School/College" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="ddlcollege" runat="server" ControlToValidate="ddlSchool"
                                            Display="None" ErrorMessage="Please Select College." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Please select Session" AppendDataBoundItems="true" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="ddlSession1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select College." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <%--ddlSession--%>
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Course" AppendDataBoundItems="true" TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="ddlCourse1" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" ErrorMessage="Please Select College." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>


                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlAssessment" runat="server">
                                <div class="col-12 ">
                                    <div id="tbladd">
                                        <asp:ListView ID="lvAssessment" runat="server">
                                            <LayoutTemplate>

                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable">
                                                        <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                            <tr>

                                                                <th>Sr No</th>
                                                                <th>Grade Release Name</th>
                                                                <th>Assessment Component </th>
                                                                <th>Out Of Marks</th>
                                                                <th>Grade</th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                            <asp:HiddenField ID="hfsrno" runat="server" Value='<%# Container.DataItemIndex + 1 %>' />
                                                            <asp:HiddenField ID="hfdValue" runat="server" Value="0" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGradeReleaseName" runat="server" CssClass="form-control"  placeholder="Please Enter" TabIndex="4" OnTextChanged="txtGradeReleaseName_TextChanged"></asp:TextBox></td>
                                                        <td>
                                                            <asp:ListBox ID="lstAss" runat="server" CssClass="form-control multi-select-demo" TabIndex="5" SelectionMode="Multiple"></asp:ListBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtOutOfMarks" runat="server" CssClass="form-control" placeholder="Please Enter"  TabIndex="6" onkeypress="return isNumber(event)"></asp:TextBox></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlGradeType" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Please select Session" AppendDataBoundItems="true" TabIndex="7" >
                                                              <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                              <asp:ListItem Value="1">GRADE-1</asp:ListItem>
                                                              <asp:ListItem Value="2">GRADE-2</asp:ListItem>
                                                              <asp:ListItem Value="3">GRADE-3</asp:ListItem>
                                                              <asp:ListItem Value="4">GRADE-4</asp:ListItem>
                                                              <asp:ListItem Value="5">GRADE-5</asp:ListItem>
                                                             </asp:DropDownList>
                                                        </td>
                                                         
                                                        <%--placeholder="25"--%>
                                                    </tr>

                                                </tbody>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnadd_Click" TabIndex="8" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click1" TabIndex="9" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="10" />
                                

                            </div>

                            <asp:Panel ID="pnlSession" runat="server" Visible="false">
                                <div class="col-12 mt-3">
                                    <div class="sub-heading">
                                        <h5>Grade Configuration List</h5>
                                    </div>
                                    
                                    <asp:ListView ID="lvComponent" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center;">Action
                                                        </th>
                                                        <th>School - Scheme</th>
                                                        <th>Session</th>
                                                        <th>Course</th>
                                                        <th>Details</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" TabIndex="10" ImageUrl="~/Images/edit.png"
                                                        AlternateText="Edit Record" ToolTip="Edit Record" CommandArgument='<%# Eval("ID")%>'
                                                        OnClick="btnEdit_Click" />
                                                       <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' Visible="false" ></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("COLLEGE_NAME") %>                                                   
                                                </td>
                                                <td>
                                                    <%# Eval("SESSION_NAME")%>

                                                </td>
                                                <td>
                                                    <%# Eval("COURSE_NAME")%>

                                                </td>

                                                <td>
                                                  <%--  <asp:UpdatePanel runat="server" ID="updpnl">
                                                        <ContentTemplate>--%>
                                                         
                                                            
                                                    <asp:Button runat="server" ID="btnshow" Text="Details" CssClass="btn btn-primary" data-toggle="modal" TabIndex="11" data-target="#myModal_view" OnClick="btnshow_Click" />
                                                    
                                                     <%--</ContentTemplate>
                                                        <Triggers>
                                                          <%--  <asp:AsyncPostBackTrigger ControlID="btnshow" EventName="Click" />--%>
                                                          <%--<asp:PostBackTrigger ControlID="btnshow" />

                                                      </Triggers>
                                                    </asp:UpdatePanel>--%>
                                                    <%--<asp:Button ID="btnDetails" runat="server" Text="Details" CssClass="btn btn-primary" data-toggle="modal" data-target="#myModal_view"  OnClick="btnDetails_Click" />--%>
                                                    
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <!-- The Modal -->

         
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnadd"
                EventName="Click" />
           <asp:AsyncPostBackTrigger ControlID="lvComponent" />
           <asp:AsyncPostBackTrigger ControlID="lvAssessment" />
            
        </Triggers>
    </asp:UpdatePanel>
    
       <div class="modal fade" id="myModal_view">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Grade Configuration Details </h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>

                        <!-- Modal body -->
                        <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
                        <div class="modal-body">
                            <div class="col-12">
                                <asp:ListView ID="lvlist" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-bordered table-striped">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr No</th>
                                                    <th>Grade Release Name</th>
                                                    <th>Assessment Component </th>
                                                    <th>Out Of Marks</th>
                                                     <th>Grade</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />

                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <%--<tbody>--%>

                                            <tr>
                                                <td style="text-align: center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hfdValue1" runat="server" Value="0" />
                                                      <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' Visible="false" ></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtgradenew" runat="server" CssClass="form-control" TabIndex="12" placeholder="Please Enter"></asp:TextBox></td>
                                                <td>
                                                    <asp:ListBox ID="lstAssnew" runat="server" CssClass="form-control multi-select-demo" TabIndex="13" SelectionMode="Multiple"></asp:ListBox>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOutOfMarksnew" runat="server" CssClass="form-control" TabIndex="14" placeholder="Please Enter"  onkeypress="return isNumber(event)"></asp:TextBox>

                                                </td>
                                                <td>
                                                     <asp:DropDownList ID="ddlGradeType1" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Please select Session" AppendDataBoundItems="true" TabIndex="15" >
                                                              <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                              <asp:ListItem Value="1">GRADE-1</asp:ListItem>
                                                              <asp:ListItem Value="2">GRADE-2</asp:ListItem>
                                                              <asp:ListItem Value="3">GRADE-3</asp:ListItem>
                                                              <asp:ListItem Value="4">GRADE-4</asp:ListItem>
                                                              <asp:ListItem Value="5">GRADE-5</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>

                                            </tr>

                                        <%--</tbody>--%>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>
                        </div>
                    </ContentTemplate>
    </asp:UpdatePanel>
                    </div>
                </div>
            </div>

    <script language="javascript" type="text/javascript">

        function checkSessionList() {
            var ddl = document.getElementById('<%= ddlSession.ClientID %>');

            if (ddl.value == "0") {
                alert('Please Select Session from Session List for Modifying');
                return false;
            }
            else
                return true;
        }
    </script>

    <!-- ========= Daterange Picker With Full Functioning Script added by gaurav 21-05-2021 ========== -->


    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }
        function SetStatStart(val) {
            $('#rdStart').prop('checked', val);
        }

        function validate() {

            $('#hfdActive').val($('#rdActive').prop('checked'));
            $('#hfdStart').val($('#rdStart').prop('checked'));



            var rfvCollege = (document.getElementById("<%=ddlSchool.ClientID%>").innerHTML);

            var ddlCollege = $("[id$=ddlSchool]").attr("id");
            var ddlCollege = document.getElementById(ddlCollege);
            if (ddlCollege.value == 0) {
                alert('Please Select ' + rfvCollege + ' .\n', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(ddlCollege).focus();
                return false;
            }

            var idtxtweb = $("[id$=txtSLongName]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Session Long Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

            var idtxtweb = $("[id$=txtSShortName]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Session Short Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

            var idtxtweb = $("[id$=txtacadyear]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Academic Year', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }
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


    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>
     <script language="javascript" type="text/javascript">
         function isNumber(evt) {
             evt = (evt) ? evt : window.event;
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                 return false;
             }
             return true;
         }
   </script>


</asp:Content>

