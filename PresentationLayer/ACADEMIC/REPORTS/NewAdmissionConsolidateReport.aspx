<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="NewAdmissionConsolidateReport.aspx.cs" Inherits="ACADEMIC_REPORTS_NewAdmissionConsolidateReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
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

    <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">New Admission Consolidate Report</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Admission Batch</label>--%>
                                            <asp:Label ID="blDYddlAdmType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" data-select2-enable="true" CssClass="form-control" runat="server" AppendDataBoundItems="True" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="true" ValidationGroup="Show"
                                            InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoAdmission" OnSelectedIndexChanged="rdoAdmission_SelectedIndexChanged" AutoPostBack="true" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">&nbsp;Generate Report &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;Send Email &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-12 text-center" id="divReport" runat="server" visible="false">
                                        <asp:RadioButton ID="rdoOverall" runat="server" GroupName="Report" Text="Overall Admission Status" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoInfoPending" runat="server" GroupName="Report" Text="Information Fill Up Status" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoApprovalPending" runat="server" GroupName="Report" Text="Approval Status by HOD" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoApprovalByFinance" runat="server" GroupName="Report" Text="Approval Status by Finance dept." />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoPaymentPending" runat="server" GroupName="Report" Text="Payment Status By Student" />

                                        <div class="col-12 btn-footer mt-3">
                                            <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click"
                                                TabIndex="10" ValidationGroup="Show" CssClass="btn btn-info" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="11" CssClass="btn btn-warning" />

                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="Show" TabIndex="12" />
                                        </div>
                                    </div>

                                    <div class="form-group col-12 text-center" id="divEmailSMS" runat="server" visible="false">
                                        <asp:RadioButton ID="rdoInfoIncomplete" runat="server" GroupName="Report" Text="Student Information Incomplete" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoNotApproveHOD" runat="server" GroupName="Report" Text=" HOD Not Approved" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoNotApproveFinance" runat="server" GroupName="Report" Text="Finance Dept. Not Approved" Visible="false" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoStudPaymentPending" runat="server" GroupName="Report" Text="Payment Not Done by Student" />

                                        <div class="col-12 btn-footer mt-3">
                                            <asp:Button ID="BtnShow" runat="Server" Text="Show" ValidationGroup="Show" OnClick="BtnShow_Click"
                                                CssClass="btn btn-primary" TabIndex="13" />
                                            <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="11" CssClass="btn btn-warning" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="Show" TabIndex="12" />
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <asp:ListView ID="lvStudents" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Students List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudent">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center">
                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" ToolTip="Select All" Text="Select All" />
                                                        </th>
                                                        <th style="text-align: center">Sr.No.</th>
                                                        <th>Student Name</th>
                                                        <th>Degree</th>
                                                        <th>Branch</th>
                                                        <th>Semester</th>
                                                        <th>Mobile No.</th>
                                                        <th>Email Id</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr id="Tr1" runat="server">
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO") %>' /></td>
                                                <td style="text-align: center"><%# Container.DataItemIndex + 1 %></td>
                                                <td>
                                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREENAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("LONGNAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTERNAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("MOBILENO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>

                                <asp:Panel ID="Panel2" runat="server" Visible="false">
                                    <asp:ListView ID="lvHOD" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>HOD List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblHOD">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center">
                                                            <asp:CheckBox ID="cbHeadHod" runat="server" onclick="SelectAllHod(this)" ToolTip="Select All" Text="Select All" />
                                                        </th>
                                                        <th style="text-align: center">Sr.No.</th>
                                                        <th>School/College</th>
                                                        <th>Department</th>
                                                        <th>HOD Name</th>
                                                        <th>Mobile No.</th>
                                                        <th>Email Id</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr id="Tr1" runat="server">
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbRowHod" runat="server" ToolTip='<%# Eval("UA_NO") %>' /></td>
                                                <td style="text-align: center"><%# Container.DataItemIndex + 1 %></td>
                                                <td>
                                                    <asp:Label ID="lblCollege" runat="server" Text='<%# Eval("COLLEGE_NAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDept" runat="server" Text='<%# Eval("DEPTNAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblhodName" runat="server" Text='<%# Eval("UA_FULLNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblHodMobile" runat="server" Text='<%# Eval("UA_MOBILE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblHodEmail" runat="server" Text='<%# Eval("UA_EMAIL") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12 mt-4" id="divSendOptions" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <label>Send Email</label>
                                        </div>
                                        <asp:RadioButtonList ID="rbNet" runat="server" Visible="false" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbNet_SelectedIndexChanged" AutoPostBack="true" TabIndex="10">
                                            <asp:ListItem Value="1">SMS &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">Email</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSubject" runat="server">
                                        <div class="label-dynamic" id="lblSubject">
                                            <sup>* </sup>
                                            <label>Subject</label>
                                        </div>
                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" TabIndex="11" AutoComplete="OFF" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic" id="lblMessage">
                                            <sup>* </sup>
                                            <label>Message</label>
                                        </div>
                                        <asp:TextBox ID="txtMatter" runat="server" AutoComplete="OFF" TextMode="MultiLine" CssClass="form-control" TabIndex="12"></asp:TextBox>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSendSMS" runat="server" Text="Send Email" Visible="false" OnClick="btnSendSMS_Click" CssClass="btn btn-primary" TabIndex="14" />
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Show" />
                                        <%--  <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Email" />--%>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">

        function SelectAll(headchk) {
            var frm = document.forms[0];
            var tbl = document.getElementById('tblStudent');
            var chkHead = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_cbHead');


            for (i = 0; i < tbl.rows.length - 1; i++) {
                var chkRow = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl' + i + '_cbRow');
                //alert(chkRow)
                if (chkHead.checked == true)
                    chkRow.checked = true;
                else
                    chkRow.checked = false;
            }
        }

        function SelectAllHod(headchk) {
            var frm = document.forms[0];
            var tbl = document.getElementById('tblHOD');
            var chkHead = document.getElementById('ctl00_ContentPlaceHolder1_lvHOD_cbHeadHod');


            for (i = 0; i < tbl.rows.length - 1; i++) {
                var chkRow = document.getElementById('ctl00_ContentPlaceHolder1_lvHOD_ctrl' + i + '_cbRowHod');
                //alert(chkRow)
                if (chkHead.checked == true)
                    chkRow.checked = true;
                else
                    chkRow.checked = false;
            }
        }
    </script>

    <script>
        function EmailHiding(rdo) {

            var selectedvalue = $("#" + rdo.id + " input:radio:checked").val();
            if (selectedvalue == "1") {
                document.getElementById("lblSubject").style.display = 'none';
                document.getElementById("lblMessage").style.display = 'block';
                document.getElementById("<%=txtMatter.ClientID%>").style.marginBottom = '10px';
                document.getElementById("<%=txtMatter.ClientID%>").style.display = 'block';
                document.getElementById("<%=txtSubject.ClientID%>").style.display = 'none';
                document.getElementById("<%=txtSubject.ClientID%>").style.marginBottom = '10px';
                document.getElementById("<%=btnSendSMS.ClientID%>").style.display = 'block';
            }
            else if (selectedvalue == "2") {
                document.getElementById("lblSubject").style.display = 'block';
                document.getElementById("lblMessage").style.display = 'block';
                document.getElementById('<%=txtMatter.ClientID%>').style.display = 'block';
                  document.getElementById('<%=txtSubject.ClientID%>').style.display = 'block';
                  document.getElementById("<%=txtMatter.ClientID%>").style.marginBottom = '10px';
                  document.getElementById("<%=txtSubject.ClientID%>").style.marginBottom = '10px';
                  document.getElementById("<%=btnSendSMS.ClientID%>").style.display = 'block';
              }
      }



    </script>

</asp:Content>

