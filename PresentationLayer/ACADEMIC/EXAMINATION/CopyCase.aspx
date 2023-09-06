<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CopyCase.aspx.cs" Inherits="ACADEMIC_EXAMINATION_CopyCase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">COPY CASE (UFM)</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row" id="trpunish" runat="server" visible="false">

                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select For Punishment Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamPunish" runat="server" AppendDataBoundItems="True" Font-Bold="True" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Coursewise</asp:ListItem>
                                            <asp:ListItem Value="2">Examwise</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPunish" runat="server" ControlToValidate="ddlExamPunish"
                                            Display="None" ErrorMessage="Please Select For Type of Punishment" ValidationGroup="rpt"
                                            InitialValue="0" Visible="False"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session </label>
                                        </div>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" ValidationGroup="report"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvSessionno" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="rpt"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester " ValidationGroup="report"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvSemesterno" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="rpt"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Reg. No</label>
                                        </div>
                                        <asp:TextBox ID="txtStudent" runat="server" ToolTip="REG.NO." TabIndex="3" />&nbsp;<asp:RequiredFieldValidator
                                            ID="rfvRegNo" runat="server" ControlToValidate="txtStudent" Display="None" ErrorMessage="Please Enter Reg. No. "
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvReg" runat="server" ControlToValidate="txtStudent"
                                            Display="None" ErrorMessage="Please Enter Reg. No." ValidationGroup="rpt"></asp:RequiredFieldValidator>

                                    </div>


                                </div>
                            </div>
                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click"
                                    TabIndex="3" ValidationGroup="rpt" />
                                <asp:Label ID="lblMsgD" runat="server" Style="color: Red;" Font-Bold="True"></asp:Label>
                                <asp:ValidationSummary ID="vsCopyCase" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="rpt" />

                            </div>

                            <div class="col-12" id="pnlStudInfo" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudent" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Degree Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDegreeName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>

                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Branch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranch" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Scheme :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblScheme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>

                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Degree No :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="DEGREENO" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSemester" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                </div>
                                <div class="row mt-4">
                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course</label>
                                        </div>
                                        <asp:DropDownList ID="ddlRegistered" runat="server" AppendDataBoundItems="True" TabIndex="12" CssClass="form-control"
                                            ValidationGroup="report" data-select2-enable="true">
                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource4">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRegistered" runat="server" ControlToValidate="ddlRegistered"
                                            Display="None" ErrorMessage="Please Select Registered Course" InitialValue="0"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-3 col-md-6 col-12 form-group" id="trcourse" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Punishment For Coursewise</label>
                                        </div>
                                        <asp:DropDownList ID="ddlPunishment" runat="server" AppendDataBoundItems="True" TabIndex="12" CssClass="form-control"
                                            ValidationGroup="report" data-select2-enable="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPCourse" runat="server" ControlToValidate="ddlPunishment"
                                            Display="None" ErrorMessage="Please Select Coursewise Punishment" InitialValue="0"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="col-lg-3 col-md-6 col-12 form-group" id="trexam" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Punishment For Exams</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" TabIndex="13"
                                            ValidationGroup="report">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPExam" runat="server" ControlToValidate="ddlExam"
                                            Display="None" ErrorMessage="Please Select Sessionwise Punishment" InitialValue="0"
                                            ValidationGroup="report" Visible="False"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Punishment Remark </label>
                                        </div>
                                        <asp:TextBox ID="txtPunishment" runat="server" Height="50px" TabIndex="14" TextMode="MultiLine"
                                            CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPunishment" runat="server" ControlToValidate="txtPunishment"
                                            Display="None" ErrorMessage="Please Enter Punishment Remark" ValidationGroup="report"></asp:RequiredFieldValidator>


                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblMsg" runat="server" SkinID="lblmsg"></asp:Label>

                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" TabIndex="15"
                                        Text="Submit" ValidationGroup="report" CssClass="btn btn-primary" />

                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                                        TabIndex="16" Text="Cancel" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="VSCopy" runat="server" ShowMessageBox="True" ShowSummary="False"
                                        ValidationGroup="report" />

                                </div>

                            </div>
                            <div class="col-12" id="ListHead" runat="server" visible="false">
                                <div id="Div1" runat="server">
                                    <asp:Panel ID="pnlStudent" runat="server">
                                        <asp:ListView ID="lvCopyCase" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>COPY CASE STUDENT LIST</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Action
                                                            </th>
                                                            <th>Session
                                                            </th>
                                                            <th>Degree
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <%--<th>Section
                                                        </th>--%>
                                                            <th>CourseName
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
                                                        <asp:ImageButton ID="btnEditCopy" runat="server" CommandArgument='<%# Eval("COPYNO")%>'
                                                            ImageUrl="~/Images/edit.png" ToolTip="Edit Record" OnClick="btnEditCopy_Click" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("SESSION_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEGREENAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>
                                                    </td>
                                                    <%-- <td>
                                                <%# Eval("SECTIONNAME")%>
                                            </td>--%>
                                                    <td>
                                                        <%# Eval("COURSENAME") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="bg-light-red">

                                                    <td>
                                                        <asp:ImageButton ID="btnEditCopy" runat="server" CommandArgument='<%# Eval("COPYNO")%>'
                                                            ImageUrl="~/Images/edit.png" ToolTip="Edit Record" OnClick="btnEditCopy_Click" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("SESSION_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEGREENAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SECTIONNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("COURSENAME") %>
                                                    </td>
                                                </tr>

                                            </AlternatingItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>



    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">
        function getRegNo(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode == 13) {
                if (checkRegNo() == true) {
                    var btn = document.getElementById('<%= btnShow.ClientID %>');
                    //__doPostBack('ctl00$ContentPlaceHolder1$Button1','');
                    __doPostBack(btn.name, '');
                }
            }

            document.getElementById('ctl00_ContentPlaceHolder1_lblMsg').innerHTML = '';
        }

        function checkRegNo() {
            var txtReg = document.getElementById('<%= txtStudent.ClientID %>');
            //var ddlSession=document.getElementById('<%=ddlSession.ClientID %>')
            if (txtReg.value == '' | txtReg.value == null) {
                txtReg.focus();
                alert('Please Enter Reg.No.');
                return false;
            }
                //            else if (ddlSession.Option == '' | ddlSession. == null) {
                //            alert('Please Select Session.');
                //            return false;
                //            
                //            
                //            }
            else {
                var ret = confirm('Confirm Reg.No. : ' + txtReg.value);
                if (ret == true)
                    return true;
                else {
                    txtReg.focus();
                    return false;
                }
            }
        }


        function IsNumeric(txt) {
            var ValidChars = "0123456789";
            var num = true;
            var mChar; 

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Error! Only Numeric Values Are Allowed for Reg No.")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }

    </script>

</asp:Content>


