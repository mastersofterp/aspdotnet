<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="BulkSemesterPromotion.aspx.cs" Inherits="ACADEMIC_BulkSemesterPromotion" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upddetails"
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
        <asp:UpdatePanel ID="upddetails" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <%--<h3 class="box-title">BULK SEMESTER PROMOTION</h3>--%>
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlClgname"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlClgname"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme" ValidationGroup="Report">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Semester</label>--%>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Institute Name</label>--%>
                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged" ToolTip="Please Select Institute">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Degree</label>--%>
                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Branch</label>--%>
                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Scheme</label>--%>
                                                <asp:Label ID="lblDYddlScheme" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="teacherallot">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Session</label>--%>
                                                <%-- <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>--%>
                                                <label>Session For Semester Promotion</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvsession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Session For Semester Promotion" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvSessionno" runat="server" ControlToValidate="ddlSession"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select Session For Semester Promotion" ValidationGroup="Excel"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select Session For Semester Promotion" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Academic Year For Semester Promotion</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAcdYear"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Academic Year For Semester Promotion" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" TabIndex="7" Text="Show" ValidationGroup="teacherallot"
                                        CssClass="btn btn-primary" ToolTip="SHOW" OnClick="btnShow_Click" />
                                    <asp:Button ID="btnSave" runat="server" TabIndex="8" Text="Save" ValidationGroup="teacherallot"
                                        CssClass="btn btn-primary" OnClick="btnSave_Click" ToolTip="SAVE" />
                                    <asp:Button ID="btnPromotedList" runat="server" Text="Promoted Student List(Excel)" CssClass="btn btn-info"
                                        OnClick="btnPromotedList_Click" ValidationGroup="Report" />
                                    <asp:Button ID="btnNotPromotedList" runat="server" Text="Not Promoted Student List(Excel)" CssClass="btn btn-info"
                                        OnClick="btnNotPromotedList_Click" ValidationGroup="Report" />
                                    <asp:Button ID="btnPromotedReport" runat="server" Text="Promoted Students Report" Visible="false"
                                        class="btn btn-info" ValidationGroup="Report" OnClick="btnPromotedReport_Click" />
                                    <asp:Button ID="btnNotPromotedReport" runat="server" Text="Not Promoted Students Report" Visible="false"
                                        class="btn btn-info" ValidationGroup="Report" OnClick="btnNotPromotedReport_Click" />
                                    <asp:Button ID="btnClear" runat="server" TabIndex="9" Text="Clear" CssClass="btn btn-warning"
                                        OnClick="btnClear_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <asp:ValidationSummary ID="vsPromo" runat="server" ValidationGroup="Excel" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Report" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" />
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Total Selected</label>
                                            </div>
                                            <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                                Style="text-align: center;" Font-Bold="True"></asp:TextBox>
                                            <%--  Reset the sample so it can be played again --%>
                                            <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                                WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />
                                            <asp:HiddenField ID="hftot" runat="server" />
                                        </div>
                                        <div class="form-group col-lg-8 col-md-12 col-12">
                                            <div class=" note-div">
                                                <h5 class="heading">Note</h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Please Select Session for  "Promoted Student List"  and  "Not Promoted Student List" Report.</span>  </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                                    <asp:ListView ID="lvStudent" runat="server" OnItemDataBound="lvStudent_ItemDataBound">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Select Student</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHeadReg" runat="server" Text="Select" ToolTip="Register/Register all" onclick="SelectAll(this,1,'chkRegister');" />
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
                                                        <th>Eligible
                                                        </th>
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
                                                    <asp:CheckBox ID="chkRegister" runat="server" ToolTip="Click to select this student for semester promotion" Enabled='<%# (Convert.ToInt32((Eval("PROMOTED_SEM")) ) == 0 ? true :false) %>'
                                                        Text='<%# (Convert.ToInt32((Eval("PROMOTED_SEM")) ) == 0 ?  " " : "PROMOTED" )%>'
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
                                                <td>
                                                    <asp:Label runat="server" ID="lblstatus" Text='<%# Eval("statusno") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>

                                <div class="col-12" runat="server" visible="false">
                                    <asp:Panel ID="Panel2" runat="server"></asp:Panel>
                                    <asp:ListView ID="LVStudentDetails" runat="server" OnItemDataBound="lvStudent_ItemDataBound">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Select Student</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHeadReg" runat="server" Text="Select" ToolTip="Register/Register all" onclick="SelectAll(this,1,'chkRegister');" />
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
                                                        <th>Eligible
                                                        </th>
                                                        <th>1ST YEAR
                                                        </th>
                                                        <th>2ND YEAR
                                                        </th>
                                                        <th>3RD YEAR
                                                        </th>
                                                        <th>4TH YEAR
                                                        </th>
                                                        <th>5TH YEAR
                                                        </th>
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
                                                    <asp:CheckBox ID="chkRegister" runat="server" ToolTip="Click to select this student for semester promotion" Enabled='<%# (Convert.ToInt32((Eval("PROMOTED_SEM")) ) == 0 ? true :false) %>'
                                                        Text='<%# (Convert.ToInt32((Eval("PROMOTED_SEM")) ) == 0 ?  " " : "PROMOTED" )%>'
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
                                                <td>
                                                    <asp:Label runat="server" ID="lblstatus" Text='<%# Eval("statusno") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("1ST YEAR") %>
                                                </td>
                                                <td>
                                                    <%# Eval("2ND YEAR") %>
                                                </td>
                                                <td>
                                                    <%# Eval("3RD YEAR") %>
                                                </td>
                                                <td>
                                                    <%# Eval("4TH YEAR") %>
                                                </td>
                                                <td>
                                                    <%# Eval("5TH YEAR") %>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnClear" />
                <asp:PostBackTrigger ControlID="btnShow" />
                <asp:PostBackTrigger ControlID="btnPromotedList" />
                <asp:PostBackTrigger ControlID="btnNotPromotedList" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <script type="text/javascript" lang="javascript">

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }

        function SelectAll(headchk) {
            debugger;
            var frm = document.forms[0]
            var txtTotStud = document.getElementById('<%= txtTotStud.ClientID %>');
            var hdfTot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {

                    if (headchk.checked == true) {

                        e.checked = true;
                        txtTotStud.value = hdfTot.value;
                    }
                    else {
                        e.checked = false;
                        txtTotStud.value = 0;
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


    </script>
    <script type="text/javascript" lang="javascript">

        function functionConfirm() {
            alert("You can't select semester greater than or equal to student's final semester!!!");
        }
    </script>

</asp:Content>
