<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="WithheldEntry.aspx.cs" Inherits="ACADEMIC_WithheldEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updWithheld"
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
    <asp:UpdatePanel ID="updWithheld" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">WITHHELD STUDENT ENTRY</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Action</label>
                                        </div>
                                        <div class="">
                                            <asp:RadioButtonList ID="rdoWithHeld" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rdoWithHeld_SelectedIndexChanged"
                                                AppendDataBoundItems="True" TabIndex="1">
                                                <asp:ListItem Value="1" Selected="True">WithHeld Entry</asp:ListItem>
                                                <asp:ListItem Value="2">Withheld Entry report</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                       
                                    </div>


                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                             <%--<label>College & Scheme</label>--%>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="save" data-select2-enable="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged"  >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="Show"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="report"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                         <%--   <label>Session</label>--%>
                                              <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" Font-Bold="True" CssClass="form-control" TabIndex="2" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" ValidationGroup="Show"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvSess" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" ValidationGroup="report"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                           <%-- <asp:Label ID="	lblDYddlSemester_Tab2" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" data-select2-enable="true"
                                            TabIndex="6" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester0" runat="server"
                                            ControlToValidate="ddlSem" Display="None" ErrorMessage="Please Select Semester"
                                            InitialValue="0" SetFocusOnError="True" ValidationGroup=""></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvSemester1" runat="server" ControlToValidate="ddlSem" Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                   
                                    <div id="regno" runat="server" class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>RRN/Reg.No.</label>
                                        </div>
                                        <asp:TextBox ID="txtStudent" runat="server" ToolTip="REG.NO." TabIndex="7"
                                            CssClass="form-control" MaxLength="20" />
                                        <asp:RequiredFieldValidator ID="rfvRegNo" runat="server" ControlToValidate="txtStudent"
                                            Display="None" ErrorMessage="Please Enter Reg. No. "
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvRegNo0" runat="server" ControlToValidate="txtStudent" Display="None" ErrorMessage="Please Enter Reg. No. " ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                   
                                    <div class="col-12">

                                        <asp:Panel ID="pnlRpt" runat="server" Visible="false" class="row d-none">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Degree</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" TabIndex="3" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                    Display="None" ErrorMessage="Please Select Degree" ValidationGroup=""
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Branch</label>
                                                </div>
                                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                    CssClass="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="4" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvbr" runat="server" ControlToValidate="ddlBranch" Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Scheme</label>
                                                </div>
                                                <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" TabIndex="5" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvscheme" runat="server" ControlToValidate="ddlScheme" Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" ValidationGroup="" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>



                                        </asp:Panel>




                                    </div>

                                      

                                </div>
                            </div>
                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click"
                                    TabIndex="8" ValidationGroup="Show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Report"
                                    ValidationGroup="report" Visible="False" CssClass="btn btn-primary" TabIndex="9" />
                                <asp:Button ID="btnClear" runat="server" CausesValidation="false" OnClick="btnClear_Click"
                                    Text="Clear" CssClass="btn btn-warning"  TabIndex="10" />
                                <asp:ValidationSummary ID="valsumReport" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="Show" DisplayMode="List" />

                                <asp:ValidationSummary ID="vssess" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                                <div>
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Style="color: Red;"></asp:Label>
                                </div>

                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlStudInfo" runat="server" Visible="false" DefaultButton="btnSubmit" CssClass="row">
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
                                            <li class="list-group-item"><b>Class Roll :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblClass" runat="server" Text="" Font-Bold="true"></asp:Label>
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
                                            <li class="list-group-item"><b>Section Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSection" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>

                                            </li>
                                            <li class="list-group-item"><b>Exam Seat No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSeatNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Scheme :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblScheme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSemester" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 mt-4">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>WithHeld For</label>
                                        </div>
                                        <asp:DropDownList ID="ddlRemark" runat="server" TabIndex="11"
                                            ValidationGroup="Save" AppendDataBoundItems="True" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRegistered" runat="server" ControlToValidate="ddlRemark"
                                            Display="None" ErrorMessage="Please Select WithHeld For" InitialValue="-1"
                                            ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click"
                                            ValidationGroup="Save" TabIndex="12" />
                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                            CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="13" />
                                        <asp:Button ID="btnRelease" runat="server" Text="Release" CssClass="btn btn-success" ValidationGroup="" visible="false" OnClick="btnRelease_Click" TabIndex="14" />
                                        <asp:ValidationSummary ID="valsumReport0" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="Save" />
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:ListView ID="lvWithHeld" runat="server">
                                    <LayoutTemplate>

                                         <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <th>Reg No.
                                                    </th>
                                                    <th>Seat No.
                                                    </th>
                                                    <th>Semester
                                                    </th>
                                                    <th>Remark
                                                    </th>
                                                    <th>
                                                        Release/Not Release
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
                                                <asp:ImageButton ID="btnEditWithHeld" runat="server" OnClick="btnEditWithHeld_Click"
                                                    CommandArgument='<%# Eval("IDNO")%>' ImageUrl="~/Images/edit.png" ToolTip="Edit Record" />
                                            </td>
                                            <td>
                                                <%# Eval("REGNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("SEATNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("REMARK")%>
                                            </td>
                                            <td>
                                                <%# Eval("RELEASE")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>

            <script type="text/javascript" language="javascript">

                function IsNumeric(txt) {
                    var ValidChars = "0123456789";
                    var num = true;
                    var mChar;

                    for (i = 0 ; i < txt.value.length && num == true; i++) {
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

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="rdoWithHeld" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
