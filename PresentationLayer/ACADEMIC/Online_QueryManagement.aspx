<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Online_QueryManagement.aspx.cs" Inherits="ACADEMIC_Online_QueryManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updatepanel1"
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

    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ONLINE QUERY MANAGEMENT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Category of Queries</label>
                                        </div>
                                        <asp:DropDownList ID="ddlFormCategory" TabIndex="1" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlFormCategory_SelectedIndexChanged"
                                            ValidationGroup="submit">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvFormCategory" runat="server" ErrorMessage="Please Select Form Category" InitialValue="0" ControlToValidate="ddlFormCategory" ValidationGroup="submit" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" id="dvListView">
                                <asp:ListView ID="lvStudentQuery" runat="server" OnItemDataBound="lvStudentQuery_ItemDataBound" OnPagePropertiesChanged="lvStudentQuery_PagePropertiesChanged">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Student Query List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%"  id="stud_query">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <%--<th>Application Id</th>--%>
                                                    <th>Applicant Name </th>
                                                    <th>Email Id</th>
                                                    <th>Contact Number</th>
                                                    <th>Query Status </th>
                                                    <th>Action</th>
                                                    <th>ApplicationID / Degree - Branch</th>
                                                </tr>
                                                <%-- <tr>
                                                    <td>
                                                        <asp:DataPager runat="server" ID="DataPager" style="text-align:right;font-weight:bold;color:darkblue;" PageSize="15">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                                <asp:NumericPagerField  PreviousPageText="<<" NextPageText=">>"></asp:NumericPagerField>
                                                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </td>
                                                </tr>--%>
                                            </thead>
                                            <tbody>
                                                <tr id="itemplaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <%--<td>
                                                <asp:label id="lbluser" runat="server" text='<%# Eval("username")%>' Tooltip='<%# Eval("userno")%>' />
                                                <asp:hiddenfield id="hdnuser" runat="server" value='<%# Eval("userno")%>' />
                                            </td>--%>

                                            <td>
                                                <asp:Label ID="lblfirstname" runat="server" Text='<%# Eval("firstname")%>' ToolTip='<%# Eval("firstname")%>' />
                                            </td>

                                            <td>
                                                <asp:Label ID="lblemailid" runat="server" Text='<%# Eval("emailid")%>' ToolTip='<%# Eval("emailid")%>' />
                                            </td>

                                            <td>
                                                <asp:Label ID="lblmobile" runat="server" Text='<%# Eval("mobile")%>' ToolTip='<%# Eval("mobile")%>' />
                                            </td>

                                            <td>
                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("feedback_status")%>' ToolTip='<%# Eval("feedback_status")%>' />
                                            </td>
                                            <td>
                                                <asp:Button runat="server" Text="Reply" CssClass="btn btn-primary" ID="btnPriview" CommandName='<%# Eval("userno")%>' ToolTip='<%# Eval("query_category")%>' OnClick="btnPriview_Click" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("APPLICATIONID_DEGREE_BRANCH").ToString().Replace("#", "<br />")%>' ToolTip='<%# Eval("APPLICATIONID_DEGREE_BRANCH")%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <%-- <asp:ListView ID="lvStudentQuery" runat="server" OnItemDataBound="lvStudentQuery_ItemDataBound" OnPagePropertiesChanged="lvStudentQuery_PagePropertiesChanged">
                                <LayoutTemplate>
                                    <div id="listViewGrid" class="demo-grid">
                                        <h4 style="text-shadow: 2px 2px 3px #0b93f8;"><b>Student Query List</b></h4>
                                        <table class="table table-hover table-striped table-bordered" id="divStudentQueryList">
                                            <thead>
                                                <tr class="bg-light-blue">                                                       
                                                    <th>Action</th>
                                                    <th>Application ID</th>
                                                    <th>Applicant Name </th>
                                                    <th>Email ID</th>
                                                    <th>Contact Number</th>
                                                    <th>Query Status </th>
                                                    <th>Unlock App.</th>
                                                    <th>Action</th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DataPager runat="server" ID="DataPager" style="text-align:right;font-weight:bold;color:darkblue;" PageSize="15">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                                <asp:NumericPagerField  PreviousPageText="<<" NextPageText=">>"></asp:NumericPagerField>
                                                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </LayoutTemplate>

                                <ItemTemplate>
                                    <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                        <td>
                                            <asp:ImageButton ID="rdbfeedback" runat="server" CommandName="ViewDetails1" CommandArgument='<%# Eval("QUERY_CATEGORY")%>' AlternateText='<%# Eval("USERNO")%>' ToolTip='<%# Eval("QUERYNO")%>' OnCommand="rdbfeedback_Command" ImageUrl="~/IMAGES/reply.jpg" />
                                        </td>

                                        <td>
                                            <asp:Label ID="lblUser" runat="server" Text='<%# Eval("USERNAME")%>' ToolTip='<%# Eval("USERNO")%>' />
                                            <asp:HiddenField ID="hdnUser" runat="server" Value='<%# Eval("USERNO")%>' />
                                        </td>

                                        <td>
                                                <asp:Label ID="lblFirstname" runat="server" Text='<%# Eval("FIRSTNAME")%>' ToolTip='<%# Eval("FIRSTNAME")%>' />
                                        </td>

                                        <td>
                                            <asp:Label ID="lblEmailId" runat="server" Text='<%# Eval("EMAILID")%>' ToolTip='<%# Eval("EMAILID")%>' />
                                        </td>

                                        <td>
                                            <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("MOBILE")%>' ToolTip='<%# Eval("MOBILE")%>' />
                                        </td>

                                        <td>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("FEEDBACK_STATUS")%>' ToolTip='<%# Eval("FEEDBACK_STATUS")%>' />
                                        </td>

                                        <td>
                                            <asp:Button ID="btnUnlock" runat="server" Text="Unlock" Visible='<%# Eval("CONFIRM_STATUS").ToString() == "1" ? true : false %>' CommandArgument='<%# Eval("USERNO") %>' OnClick="btnUnlock_Click" ToolTip='<%# Eval("USERNAME")%>' />
                                        </td>
                                            
                                        <td>
                                                <asp:Button runat="server" Text="Reply" ID="btnPriview" CommandName='<%# Eval("USERNO")%>' ToolTip='<%# Eval("QUERY_CATEGORY")%>' OnClick="btnPriview_Click" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <div class="row">
        <div class="modal fade" id="myModal1" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content chat-app">
                    <div class="modal-header">
                        <h4 class="modal-title"><b>Reply For Query</b> <%--<i class="fa fa-question-circle"></i>--%></h4>
                        <span class="text-right">
                            <button type="button" class="close" data-dismiss="modal">&times;</button></span>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="modal-body" style="background-color: floralwhite">
                                <div style="z-index: 1; position: absolute; left: 300px;">

                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                        DynamicLayout="true" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <div style="width: 120px; padding-left: 5px">
                                                <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                                                <p class="text-success"><b>Loading..</b></p>
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                </div>
                                <div>
                                    <asp:ListView ID="lvFeedbackReply" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid">
                                                <div id="tblStudents">
                                                    <div id="itemPlaceholder" runat="server" />
                                                </div>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserReply" runat="server" Text='<%# Eval("QUERY_DETAILS")%>' CssClass="chat-reply user-reply" />
                                            <asp:Label ID="lblAdminReply" runat="server" Text='<%# Eval("QUERY_REPLY")%>' CssClass="chat-reply admin-reply" />
                                        </ItemTemplate>
                                    </asp:ListView>

                                </div>
                            </div>

                            <div class="modal-footer">
                                <div class="input-group chat-message">
                                    <asp:TextBox ID="txtFeedback" runat="server" TextMode="MultiLine" CssClass="form-group" placeholder="Type your message here..."></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFeedback"
                                        ErrorMessage="Please Enter Your Answer" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>
                                <div>
                                    <span class="input-group-btn">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Send" CssClass="btn btn-success"
                                            ValidationGroup="submit" OnClick="btnSubmit_Click" />
                                    </span>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <asp:DropDownList ID="ddlStatus" CssClass="form-group" runat="server"  data-select2-enable="true">
                                        <asp:ListItem Selected="True" Value="1">Open</asp:ListItem>
                                        <asp:ListItem Value="2">Close</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="text-center" style="display: none;">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" data-dismiss="modal" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="38px" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="submit" DisplayMode="List" />

                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                  
                                                        <strong>
                                                            <asp:Label ID="lblStatus1" runat="server"></asp:Label></strong>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function showPopup() {
            $('#myModal1').modal('show');
        }
    </script>

    <script type="text/javascript">
        function validatePage() {
            //Executes all the validation controls associated with group1 validaiton Group1. 
            var flag = window.Page_ClientValidate('vTask');
            if (flag)
                //Executes all the validation controls which are not associated with any validation group. 
                flag = window.Page_ClientValidate();
            if (!Page_IsValid) {
                $find('mpeScheduleTask').hide();
            }
            return flag;
        }
    </script>
       <script>
           $(document).ready(function () {
               var table = $('#stud_query').DataTable({
                   responsive: true,
                   lengthChange: true,
                   scrollY: 320,
                   scrollX: true,
                   scrollCollapse: true,
                   paging: false,
                   // ordering : false,
                   bSort: false, //for sorting disabled
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
                                   return $('#stud_query').DataTable().column(idx).visible();
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
                                               return $('#stud_query').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
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
                                               return $('#stud_query').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },

                           ]
                       }
                   ],
                   "bDestroy": true,
               });
           });
           var parameter = Sys.WebForms.PageRequestManager.getInstance();
           parameter.add_endRequest(function () {
               $(document).ready(function () {
                   var table = $('#stud_query').DataTable({
                       responsive: true,
                       lengthChange: true,
                       scrollY: 320,
                       scrollX: true,
                       scrollCollapse: true,
                       paging: false, // Added by Gaurav for Hide pagination
                       // ordering: false,
                       bSort: false,
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
                                       return $('.display').DataTable().column(idx).visible();
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
                                                  return $('#stud_query').DataTable().column(idx).visible();
                                              }
                                          },
                                          format: {
                                              body: function (data, column, row, node) {
                                                  var nodereturn;
                                                  if ($(node).find("input:text").length > 0) {
                                                      nodereturn = "";
                                                      nodereturn += $(node).find("input:text").eq(0).val();
                                                  }
                                                  else if ($(node).find("input:checkbox").length > 0) {
                                                      nodereturn = "";
                                                      $(node).find("input:checkbox").each(function () {
                                                          if ($(this).is(':checked')) {
                                                              nodereturn += "On";
                                                          } else {
                                                              nodereturn += "Off";
                                                          }
                                                      });
                                                  }
                                                  else if ($(node).find("a").length > 0) {
                                                      nodereturn = "";
                                                      $(node).find("a").each(function () {
                                                          nodereturn += $(this).text();
                                                      });
                                                  }
                                                  else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                      nodereturn = "";
                                                      $(node).find("span").each(function () {
                                                          nodereturn += $(this).text();
                                                      });
                                                  }
                                                  else if ($(node).find("select").length > 0) {
                                                      nodereturn = "";
                                                      $(node).find("select").each(function () {
                                                          var thisOption = $(this).find("option:selected").text();
                                                          if (thisOption !== "Please Select") {
                                                              nodereturn += thisOption;
                                                          }
                                                      });
                                                  }
                                                  else if ($(node).find("img").length > 0) {
                                                      nodereturn = "";
                                                  }
                                                  else if ($(node).find("input:hidden").length > 0) {
                                                      nodereturn = "";
                                                  }
                                                  else {
                                                      nodereturn = data;
                                                  }
                                                  return nodereturn;
                                              },
                                          },
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
                                                  return $('#stud_query').DataTable().column(idx).visible();
                                              }
                                          },
                                          format: {
                                              body: function (data, column, row, node) {
                                                  var nodereturn;
                                                  if ($(node).find("input:text").length > 0) {
                                                      nodereturn = "";
                                                      nodereturn += $(node).find("input:text").eq(0).val();
                                                  }
                                                  else if ($(node).find("input:checkbox").length > 0) {
                                                      nodereturn = "";
                                                      $(node).find("input:checkbox").each(function () {
                                                          if ($(this).is(':checked')) {
                                                              nodereturn += "On";
                                                          } else {
                                                              nodereturn += "Off";
                                                          }
                                                      });
                                                  }
                                                  else if ($(node).find("a").length > 0) {
                                                      nodereturn = "";
                                                      $(node).find("a").each(function () {
                                                          nodereturn += $(this).text();
                                                      });
                                                  }
                                                  else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                      nodereturn = "";
                                                      $(node).find("span").each(function () {
                                                          nodereturn += $(this).text();
                                                      });
                                                  }
                                                  else if ($(node).find("select").length > 0) {
                                                      nodereturn = "";
                                                      $(node).find("select").each(function () {
                                                          var thisOption = $(this).find("option:selected").text();
                                                          if (thisOption !== "Please Select") {
                                                              nodereturn += thisOption;
                                                          }
                                                      });
                                                  }
                                                  else if ($(node).find("img").length > 0) {
                                                      nodereturn = "";
                                                  }
                                                  else if ($(node).find("input:hidden").length > 0) {
                                                      nodereturn = "";
                                                  }
                                                  else {
                                                      nodereturn = data;
                                                  }
                                                  return nodereturn;
                                              },
                                          },
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
    <%--<script>
          $(document).ready(function () {

              bindDataTable();
              Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
          });

          function bindDataTable() {
              var myDT = $('#divStudentQueryList').DataTable({

              });
          }
     </script>--%>
</asp:Content>

