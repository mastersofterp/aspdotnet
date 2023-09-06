<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="BulkSemPromoWithoutCri.aspx.cs" Inherits="ACADEMIC_BulkSemesterPromotionWithoutCriteria" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upddetails1"
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

    <asp:Panel ID="pnlMain" runat="server">
        <asp:UpdatePanel ID="upddetails1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <%--<h3 class="box-title">BULK SEMESTER PROMOTION WITHOUT CRITERIA</h3>--%>
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server"  Font-Bold="true"></asp:Label></h3>
                            </div>

                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--  <label>College & Scheme</label>--%>
                                                <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                                ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                                Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Semester</label>--%>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"
                                                CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="teacherallot">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" SetFocusOnError="true"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>School /Institute Name</label>--%>
                                                <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="teacherallot"
                                                CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlColg"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select School /Institute Name" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                               <%-- <label>Degree</label>--%>
                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="teacherallot"
                                                CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Programme/Branch</label>--%>
                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4" ValidationGroup="teacherallot"
                                                CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Programme/Branch" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Scheme</label>--%>
                                                <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>

                                            </div>
                                            <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="teacherallot">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>
                                        
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session For Semester Promotion</label>
                                               <%-- <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvsession" runat="server" ControlToValidate="ddlSession" 
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Session For Semester Promotion" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                              <label>Academic Year For Semester Promotion</label>                                               
                                            </div>
                                            <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="teacherallot" CssClass="form-control" OnSelectedIndexChanged="ddlAcdYear_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAcademicYear" runat="server" ControlToValidate="ddlAcdYear"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Academic Year For Semester Promotion" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:RadioButtonList ID="rblSemPromotion" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" AutoPostBack="true" OnSelectedIndexChanged="rblSemPromotion_SelectedIndexChanged">
                                                <asp:ListItem Value="0">&nbsp;Semester Promotion&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="1">&nbsp;Cancel Semester Promotion</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfvSemPromotion" runat="server" ControlToValidate="rblSemPromotion"
                                                Display="None" ErrorMessage="Please Select Option" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="true"></asp:Label>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" TabIndex="7" Text="Show" ValidationGroup="teacherallot"
                                        CssClass="btn btn-primary" ToolTip="SHOW" OnClick="btnShow_Click" />
                                    <asp:Button ID="btnSave" runat="server" TabIndex="8" Text="Semester Promotion" ValidationGroup="teacherallot"
                                        CssClass="btn btn-primary" OnClick="btnSave_Click" ToolTip="SAVE" />
                                    <asp:Button ID="btnReportSemPromotion" runat="server" TabIndex="8" Text="Semester Promoted Report" ValidationGroup="teacherallot" Visible="false"
                                        CssClass="btn btn-info" OnClick="btnReportSemPromotion_Click" ToolTip="Semester Promoted Report" />
                                    <asp:Button ID="btnReportsemnotpromotion" runat="server" TabIndex="9" Text="Semester Promotion Not Completed Report" Visible="false" ValidationGroup="teacherallot"  CssClass="btn btn-info" OnClick="btnReportsemnotpromotion_Click" ToolTip="Semester Promotion Not Completed Report" />
                                    <asp:Button ID="btnCancelSemPromotion" runat="server" TabIndex="8" Text="Cancel Semester Promotion" ValidationGroup="teacherallot"
                                        CssClass="btn btn-primary" OnClick="btnCancelSemPromotion_Click" />
                                    <asp:Button ID="btnCancelSemPromoReport" runat="server" TabIndex="8" Text="Cancel Semester Promoted Report" ValidationGroup="teacherallot" Visible="false"
                                        CssClass="btn btn-info" OnClick="btnCancelSemPromoReport_Click" ToolTip="Cancel Semester Promoted Report" />
                                    <asp:Button ID="btnClear" runat="server" TabIndex="9" Text="Clear" CssClass="btn btn-warning" OnClick="btnClear_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Total Selected</label>
                                            </div>
                                            <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="false" CssClass="form-control"
                                                Style="text-align: center;" Font-Bold="True" Font-Size="Small"></asp:TextBox>
                                            <%--  Reset the sample so it can be played again --%>
                                            <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                                WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />
                                            <asp:HiddenField ID="hftot" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                                    <asp:ListView ID="lvStudent" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Select Student</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHeadReg" runat="server" Text="Select" ToolTip="Register/Register all" onclick="SelectAll(this);" />
                                                        </th>
                                                        <th><%--Reg. no.--%>
                                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Current sem.
                                                        </th>
                                                        <th>Promoted Sem.
                                                        </th>
                                                        <%--  <th>Eligible
                                                        </th>--%>
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
                                                    <asp:CheckBox ID="chkRegister" runat="server" ToolTip="Click to select this student for semester promotion"
                                                        Text='<%# ( (Convert.ToInt32((Eval("PROMOTED_SEM")) ) == 0) || (Convert.ToInt32((Eval("PROMOTED_SEM")) ) == Convert.ToInt32((Eval("SEMESTERNO")) )) ?  " " : "PROMOTED" )%>'
                                                        ForeColor="Green" Font-Bold="true"
                                                        onClick="totSubjects(this);" />
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblregno" Text='<%# Eval("regno")%>' ToolTip='<%# Eval("idno")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblstudname" Text='<%# Eval("studname")%>' ToolTip='<%# Eval("sectionno")%>'></asp:Label>
                                                </td>

                                                <td>
                                                    <%# Eval("semesterno") %>
                                                </td>
                                                <td>
                                                    <%# Eval("promoted_sem") %>
                                                </td>
                                                <%--<td>--%>
                                                    <%--  <asp:Label runat="server" ID="lblstatus" Text='<%# Eval("statusno") %>'></asp:Label>--%>
                                               <%-- </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divMsg" runat="server"></div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnCancelSemPromotion" />
                <asp:PostBackTrigger ControlID="btnClear" />
                <asp:PostBackTrigger ControlID="btnShow" />
                <asp:PostBackTrigger ControlID="btnReportSemPromotion" />
                <asp:PostBackTrigger ControlID="btnReportsemnotpromotion" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- <script type="text/javascript">
        function totAllSubjects(headchk) {
            alert("a");
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

        function SelectAll(headchk) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {

                    if (headchk.checked == true) {

                        e.checked = true;

                    }
                    else {
                        e.checked = false;
                    }
                }

            }

            if (headchk.checked == true) {
                txtTot.value = hdfTot.value;
                txtCredits.value = hdfCredits.value;
            }
            else {
                txtTot.value = 0;
                txtCredits.value = 0;
            }
        }

    </script>--%>

    <script type="text/javascript">
        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }

        function SelectAll(chk) {
            debugger;
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudent_ctrl' + i + '_chkRegister');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                        txtTot.value = hftot.value;
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0;
                    }
                }

            }
        }

    </script>

    <script type="text/javascript">

        function functionConfirm() {
            alert("You can't select semester greater than or equal to student's final semester!!!");
        }
    </script>

</asp:Content>
