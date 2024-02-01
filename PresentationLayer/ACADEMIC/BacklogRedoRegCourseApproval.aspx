<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" ViewStateEncryptionMode="Always" EnableViewStateMac="true"
    CodeFile="BacklogRedoRegCourseApproval.aspx.cs" Inherits="ACADEMIC_BacklogRedoRegCourseApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlALLStudent .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_pnlCoreStudent .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_pnlElectStudent .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_pnlGlobalStudent .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="uplReg"
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

    <asp:UpdatePanel ID="uplReg" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary" id="divCourses" runat="server">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Text="Backlog Redo Course Registration Approval "></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12" id="divpnlSearch" runat="server">
                                <div class="row">

                                      <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>Session Name</label>--%>  <sup>*</sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="1" AutoPostBack="true" 
                                            OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>College</label>--%>
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ToolTip="Please Select School/Institute." AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>Session Name</label>--%> <%-- <sup>*</sup>--%>
                                            <asp:Label ID="lblDYddlDepartment" runat="server" Font-Bold="true">Department</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="True" TabIndex="3" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Department" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>Degree Name</label>--%>  <sup>*</sup>
                                            <asp:Label ID="lblDegree" runat="server" Text="Degree" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" TabIndex="4" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>Branch Name</label>--%>  <sup>*</sup>
                                            <asp:Label ID="lblddlBranch" runat="server" Text="Branch" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" TabIndex="5" CssClass="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true"
                                            data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>Branch Name</label>--%>  <sup>*</sup>
                                            <asp:Label ID="lblddlSemester" runat="server" Text="Semester" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" TabIndex="6" CssClass="form-control" 
                                            data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Students List" TabIndex="5" OnClick="btnShow_Click"
                                    ValidationGroup="Show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary"
                                    TabIndex="6" ValidationGroup="Show" />
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" TabIndex="7" OnClick="btnCancel_Click"
                                    CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                            </div>

                            <div class="col-12" id="tblInfo" runat="server">
                                <div class="row">
                                    <div class="col-md-12 mt-3">
                                        <asp:Panel ID="pnlALLStudent" runat="server">
                                            <asp:ListView ID="lvApproveCourse" runat="server" OnItemDataBound="lvApproveCourse_ItemDataBound">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblApproveCourse">
                                                        <thead class="bg-light-blue" style="top: -15px !important;">
                                                            <tr>
                                                                <th style="width: 20px;">Sr.No.</th>
                                                                <th>
                                                                    <asp:CheckBox ID="cbApproveAll" runat="server" Text="" ToolTip="Approve all" onclick="SelectAll(this,1,'cbApprove');" />
                                                                    Approve all
                                                                </th>
                                                                <%--<th>Edit</th>--%>
                                                                <th>PRN No.</th>
                                                                <th>Student Name</th>
                                                                <th>Registration Status By Stud</th>
                                                                <th>
                                                                    <asp:Label runat="server" ID="thHODArrpve" Text="HOD Approval Status" />
                                                                </th>
                                                                <th id="thPayment" runat="server">Payment Status</th>
                                                                <th id="thDeanApprove" runat="server">DEAN Approval Status</th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trCurRow">
                                                        <td><%# Container.DataItemIndex + 1 %></td>
                                                        <td>
                                                            <asp:CheckBox ID="cbApprove" runat="server" Value='<%# Eval("IDNO") %>' ToolTip="Click to select this Course for Approval"
                                                                onclick="ChkHeader(1,'cbHeadReg','cbApprove');" />
                                                        </td>
                                                        <%--<td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit1.gif"
                                                                CommandArgument='<%# Eval("IDNO")%>' AlternateText="Edit Record" ToolTip='<%# Eval("IDNO")%>'
                                                                OnClick="btnEdit_Click" />
                                                        </td>--%>
                                                        <td>
                                                            <asp:Label ID="lblIDNO" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("STUDNAME") %>' />
                                                        </td>
                                                        <td>
                                                            <%-- <asp:Label ID="lblStudRegisterStatus" runat="server" Text='NA' />--%>
                                                            <asp:Label ID="lblStudCouresReg" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("STUD_COURSE_REG") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                                Text='<%# (Convert.ToInt32(Eval("STUD_COURSE_REG") )> 0 ?  "Yes" : "No" )%>' />
                                                        </td>
                                                        <td>
                                                            <%-- <asp:Label ID="lblHODApproveStatus" runat="server" Text='<%# Eval("HOD_APPROVAL_STATUS") %>' />--%>
                                                            <asp:Label ID="lblHODApproveStatus" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("HOD_APPROVAL_STATUS") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                                Text='<%# (Convert.ToInt32(Eval("HOD_APPROVAL_STATUS") )> 0 ?  "Approved" : "Pending" )%>' />

                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPayment" runat="server" Text='<%# (Convert.ToInt32(Eval("PAY_STATUS") )> 0 ?  "Done" : "Not Done" )%>' ForeColor='<%# (Convert.ToInt32(Eval("PAY_STATUS") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>' />
                                                        </td>

                                                        <td>
                                                            <%--<asp:Label ID="lblDeanApproveStatus" runat="server" Text='<%# Eval("DEAN_APPROVAL_STATUS") %>' />--%>
                                                            <asp:Label ID="lblDeanApproveStatus" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("DEAN_APPROVAL_STATUS") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                                Text='<%# (Convert.ToInt32(Eval("DEAN_APPROVAL_STATUS") )> 0 ?  "Approved" : "Pending" )%>' />
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>

                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblmsg" runat="server" Style="color: Red; font-weight: bold" Text=""></asp:Label>
                                </div>
                            </div>

                        </div>
                        <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                        <div id="divMsg" runat="server">
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        //        //To change the colour of a row on click of check box inside the row..
        //        $("tr :checkbox").live("click", function() {
        //        $(this).closest("tr").css("background-color", this.checked ? "#FFFFD2" : "#FFFFFF");
        //        });

        function SelectAll(headerid, headid, chk) {
            var tbl = '';
            var list = '';
            if (headid == 1) {
                tbl = document.getElementById('tblApproveCourse');
                list = 'lvApproveCourse';
            }

            try {
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk;
                        if (headerid.checked) {
                            document.getElementById(chkid).checked = true;
                        }
                        else {
                            document.getElementById(chkid).checked = false;
                        }
                        chkid = '';
                    }
                }
            }
            catch (e) {
                alert(e);
            }
        }

        function ChkHeader(chklst, head, chk) {
            try {
                var headid = '';
                var tbl = '';
                var list = '';
                var chkcnt = 0;
                if (chklst == 1) {
                    tbl = document.getElementById('tblApproveCourse');
                    headid = 'ctl00_ContentPlaceHolder1_lvApproveCourse_' + head;
                    list = 'lvApproveCourse';
                }

                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk);
                        if (chkid.checked)
                            chkcnt++;
                    }
                }
                //if (chkcnt > 0)
                //    document.getElementById(headid).checked = true;
                //else
                //    document.getElementById(headid).checked = false;
            }
            catch (e) {
                alert(e);
            }
        }

        //function HideColumn(orgID) {
        //    if (orgID != 2) {
        //        tbl = document.getElementById('tblApproveCourse');
        //        var dataCols = tbl.getElementsByTagName('th');
        //        alert(dataCols[6].id);
        //        dataCols[6].style.visibility = 'hidden';
        //    }
        //}
    </script>

</asp:Content>
