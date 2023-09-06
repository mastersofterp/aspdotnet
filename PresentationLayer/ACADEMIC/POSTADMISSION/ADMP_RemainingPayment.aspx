<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMP_RemainingPayment.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMP_RemainingPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <style>
        .EU_DataTable {
            border: 1px solid #e5e5e5;
        }

            .EU_DataTable th, .EU_DataTable td {
                padding: 5px 8px;
            }
    </style>

    <asp:UpdatePanel ID="upRemainingPayment" runat="server">
        <ContentTemplate>
            <div class="row">
                <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Remaining Payment Student List</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch </label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmissionBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Program Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlProgramType" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlProgramType_SelectedIndexChanged" TabIndex="2" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            <asp:ListItem Value="1">UG</asp:ListItem>
                                            <asp:ListItem Value="2">PG</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Program/Branch </label>
                                        </div>
                                        <asp:ListBox ID="lstProgram" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6" AutoPostBack="true"></asp:ListBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Student List" TabIndex="5" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                              <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="5" CssClass="btn btn-primary" Visible="false"  OnClick="btnSubmit_Click"  />
                               <%--   <asp:Button ID="btnGenerateAdmissionNote" runat="server" Text="Generate Admission Note" TabIndex="6" CssClass="btn btn-primary" Visible="false" />
                                <asp:Button ID="btnSendEMail" runat="server" Text="Send E-Mail" TabIndex="7" CssClass="btn btn-primary" Visible="false" />
                                <asp:Button ID="btnPrintAdmissionNote" runat="server" Text="Print Admission Note" TabIndex="8" CssClass="btn btn-primary" Visible="false" />--%>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="9" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                            </div>                        
                           
                            <asp:Panel ID="pnlGV1" runat="server" Visible="false">
                               <asp:ListView ID="lvStudent" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tbllist">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th><asp:CheckBox ID="chkAll" runat="server" onclick="totAllSubjects(this);" /></th>
                                                             <th>Application No</th>
                                                            <th>Student Name</th>
                                                             <th>Degree Name</th>
                                                            <th>Branch Name</th>                                                           
                                                             <th>Email Id</th>
                                                            <th>Mobile No</th>
                                                             <th>Fees Demand</th>  
                                                            <th>Fees Paid</th>                                                             
                                                            <th>Out. Fees</th>                                                                                                                    
                                                            <th>Status</th>   
                                                            <%-- <th style="width: 10%">Status</th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>                                            
                                               <asp:CheckBox ID="chkRecon" runat="server"  Checked='<%#  Convert.ToString(Eval("PaymentStatus"))=="Paid" ? true: false %>' Enabled='<%#  Convert.ToString(Eval("PaymentStatus"))=="Paid" ? false: true %>' />
                                            </td>
                                          <%--  <td><%# Eval("APPLICATION_ID") %></td>--%>

                                            <td><asp:Label ID="lblApplicationId"  runat="server" Text='<%# Eval("APPLICATION_ID") %>' /></td>
                                            <td><%# Eval("STUDENTNAME") %></td>
                                            <td><%# Eval("DEGREENAME") %></td>
                                            <td><%# Eval("BranchName") %></td>                                            
                                            <td><%# Eval("EMAILID") %></td>
                                            <td><%# Eval("MOBILENO") %></td>                                            
                                            <td><asp:Label ID="lblDemand"  runat="server" Text='<%# Eval("Demand") %>' /></td>
                                            <td><asp:Label ID="lblPaidAmt"  runat="server" Text='<%# Eval("PaidAmount") %>' /></td>
                                             <td><asp:Label ID="lblOutStandingAmt"  runat="server" Text='<%# Eval("OutStandingAmt") %>' /></td>
                                             <td><asp:Label ID="lblPaymentStatus"  runat="server" Text='<%# Eval("PaymentStatus") %>'   Font-Bold="true"
                                               ForeColor='<%# Convert.ToString(Eval("PaymentStatus"))=="Paid"?System.Drawing.Color.Green: 
                                                 Convert.ToString(Eval("PaymentStatus"))=="Allowed"?System.Drawing.Color.Blue :System.Drawing.Color.Red %>'  /></td>

                                            <asp:HiddenField ID="hdnUserNo" Visible="false" runat="server" Value='<%# Eval("USERNO") %>' />
                                            <asp:HiddenField ID="hdnDegreeNo" Visible="false" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                            <asp:HiddenField ID="hdnBranchNo" Visible="false" runat="server" Value='<%# Eval("BRANCHNO") %>' />

                                          <%--  <td><%# Eval("PaidAmount") %></td>
                                            <td><%# Eval("Demand") %></td>                                            
                                            <td><%# Eval("OutStandingAmt") %></td> --%>
                                                                                                                                                                                                          
                                        <%--    <asp:HiddenField ID="hdnUserNo" Visible="false" runat="server" Value='<%# Eval("USERNO") %>' />
                                            <asp:HiddenField ID="hdnACNO" Visible="false" runat="server" Value='<%# Eval("ACNO") %>' />
                                            <asp:HiddenField ID="hdnAttendanceNO" Visible="false" runat="server" Value='<%# Eval("ATTENDANCE_NO") %>' />      --%>                                                                              
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
      <script type="text/javascript">
          function ToAllPayment(headchk) {
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
              }
              catch (err) {
                  alert("Error : " + err.message);
              }
          }
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
        <script type="text/javascript">
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
                }
                catch (err) {
                    alert("Error : " + err.message);
                }
            }
    </script>
</asp:Content>

