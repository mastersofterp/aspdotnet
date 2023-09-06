<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Intermediate_Grade_Release.aspx.cs" Inherits="ACADEMIC_EXAMINATION_Intermediate_Grade_Release" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .grade .table sup, .grade-list .table sup {
            color: #212529 !important;
        }

        .grade .table > tbody > tr > td {
            padding: 3px 8px;
        }

        .grade .table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5;
        }

        .fa-check {
            color: green;
        }
    </style>
    <asp:UpdatePanel ID="updGradeRelease" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Intermediate Grade Release</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12 col-md-12 col-lg-6">
                                        <div class="row">
                                            <div class="form-group col-lg-12 col-md-8 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>School - Scheme </label>
                                                </div>
                                                <asp:DropDownList ID="ddlSchool" runat="server" CssClass="form-control" ToolTip="Please Select School" AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="ddlcollege" runat="server" ControlToValidate="ddlSchool"
                                                    Display="None" ErrorMessage="Please Select College." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Session </label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" ToolTip="Please Select Session" AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="ddlSession1" runat="server" ControlToValidate="ddlSession"
                                                    Display="None" ErrorMessage="Please Select College." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Course </label>
                                                </div>
                                                <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" ToolTip="Please Select Course" AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" TabIndex="3">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="ddlCourse1" runat="server" ControlToValidate="ddlCourse"
                                                    Display="None" ErrorMessage="Please Select College." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Grade Release </label>
                                                </div>
                                                <asp:DropDownList ID="ddlGradeRelease" runat="server" CssClass="form-control" ToolTip="Please Select Grade Realse" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="4">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="ddlGrade1" runat="server" ControlToValidate="ddlGradeRelease"
                                                    Display="None" ErrorMessage="Please Select Grade Release." InitialValue="0" ></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <%--//Data Add--%>
                                    </div>

                                    <div class="col-12 col-md-12 col-lg-6 grade">
                                        <div class="table-responsive">
                                            <asp:ListView ID="lvGrade" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Grade Table</h5>
                                                    </div>
                                                    <table class="table table-bordered table-striped">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Grade </th>
                                                                <th>Min.</th>
                                                                <th>Max.</th>
                                                                <th>Total Students</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("GRADE_NAME") %></td>
                                                        <td><asp:TextBox ID="txtGrademin" runat="server" Text='<%# Eval("MINMARK")%>'  onkeypress="return isNumber(event)"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txtGradeMax" runat="server" Text='<%# Eval("MAXMARK")%>' onkeypress="return isNumber(event)"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txtTotalStud" runat="server" Text='<%# Eval("TOTAL_STU")%>'   onkeypress="return isNumber(event)"></asp:TextBox></td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" ValidationGroup="submit" />
                                <asp:Button ID="btnAllotGrade" runat="server" Text="Allot Grade" CssClass="btn btn-primary" OnClick="btnAllotGrade_Click" />
                                <asp:Button ID="btnPublish" runat="server" Text="Publish" CssClass="btn btn-primary" OnClick="btnPublish_Click" />
                                <asp:Button ID="btnUnPublish" runat="server" Text="Un-Publish" CssClass="btn btn-primary" OnClick="btnUnPublish_Click" />
                               <%-- <asp:Button ID="btnLock" runat="server" Text="Lock" CssClass="btn btn-primary" />--%>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                            </div>

                            <div class="col-12 mt-3 grade-list">
                                <asp:ListView ID="lvGradeList" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Grade Release List</h5>
                                        </div>
                                        <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable">
                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                    <tr>
                                                        <th>
                                                        <asp:CheckBox ID="chkAll" runat="server" onclick="totAllSubjects(this);"/>Action</th>
                                                        <th>Student ID</th>
                                                        <th>Student Name</th>
                                                        <th>Grade</th>
                                                        <th>Publish</th>
                                                        <%--<th>Lock</th>--%>
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
                                            <asp:CheckBox ID="chkAction" runat="server" /></td>
                                            <asp:HiddenField ID="hdfregno" runat="server" Value='<%# Eval("REGNO") %>' />
                                            <td><%# Eval("REGNO") %></td>
                                            <td><%# Eval("STUDNAME") %></td>
                                            <td><%# Eval("GRADE") %></td>
                                            <td><%# Eval("GRADELOCK") %></td>
                                            
                                            <%--<td>-</td>--%>
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
    <%--</div>--%>
      <script language="javascript" type="text/javascript">
          function totAll(headchk) {
              var frm = document.forms[0]
              for (i = 0; i < document.forms[0].elements.length; i++) {
                  var e = frm.elements[i];
                  if (e.type == 'checkbox') {
                      if (headchk.checked == true)
                          e.checked = true;
                      else
                          e.checked = false;
                  }
              }
          }
    </script>
    <script>

          function totAllSubjects(headchk) {
              debugger;
              var sum = 0;
              var frm = document.forms[0]
              try {
                  for (i = 0; i < document.forms[0].elements.length; i++) {
                      var e = frm.elements[i];
                      if (e.type == 'checkbox') {
                          if (headchk.checked == true) {
                              // SumTotal();
                              // var j = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + i + '_lblAmt').innerText);
                              //// alert(j);
                              // sum += parseFloat(j);
                              if (e.disabled == false) {
                                  e.checked = true;
                              }
                          }
                          else {
                              if (e.disabled == false) {
                                  e.checked = false;
                                  headchk.checked = false;
                              }
                          }

                          // x = sum.toString();
                      }

                  }
                  //if (headchk.checked == true) {
                  //    // SumTotal();
                  //}
                  //else {
                  //    // SumTotal();
                  //}
              }
              catch (err) {
                  alert("Error : " + err.message);
              }
          }
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

 
