<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Degree_Completion_Criteria.aspx.cs" Inherits="ACADEMIC_Degree_Completion_Criteria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>
    <style>
        @media (max-width: 767px) {
            #myModal .modal-dialog {
                max-width: 90%;
            }
        }

        @media (min-width: 768px) and (max-width: 991px) {
            #myModal .modal-dialog {
                max-width: 70%;
            }
        }

        @media (min-width: 992px) {
            #myModal .modal-dialog {
                max-width: 50%;
            }
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">DEGREE COMPLETION CRITERIA CHECK</h3>
                </div>
                <div class="box-body">
                    <div>
                        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updstudenteligibility"
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
                    <asp:UpdatePanel ID="updstudenteligibility" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College & Scheme</label>
                                            <%--<asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" CssClass="form-control" TabIndex="0" AppendDataBoundItems="true"
                                            data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ErrorMessage="Please Select Institute" ControlToValidate="ddlClgname" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlsession" runat="server" ErrorMessage="Please Select Session" ControlToValidate="ddlSession" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddsem" runat="server" ErrorMessage="Please Select Semester" ControlToValidate="ddlSem" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Student Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudentType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlStudentType_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular</asp:ListItem>
                                            <asp:ListItem Value="1">Backlog</asp:ListItem>
                                            <asp:ListItem Value="2">PhotoCopy/Revaluation</asp:ListItem>
                                            <asp:ListItem Value="3">Redo/Resit</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlstudenttype" runat="server" ErrorMessage="Please Select Student Type" ControlToValidate="ddlStudentType" Display="None" SetFocusOnError="True" InitialValue="-1" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading"><i class="fa fa-star" aria-hidden="true"></i>Note</h5>
                                            <p>
                                                <span>1) Select Student To Get Eligibility Status Of the Student.</span><br />
                                                <span>2)
                                                    <h7 style="color: green">Green :-</h7>
                                                    Active Links For Course Category Wise Eligibility</span><br />
                                                <span>3)
                                                    <h7 style="color: red">Red :-</h7>
                                                    DeActive Links</span>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=" col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" ValidationGroup="Show" OnClick="btnShow_Click"
                                    Text="Show" CssClass="btn btn-primary" TabIndex="0" />
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                                    Text="Check Eligibility" CssClass="btn btn-primary" TabIndex="0" />
                                <asp:Button ID="btnLock" runat="server" OnClick="btnLock_Click"
                                    Text="Lock" CssClass="btn btn-primary" TabIndex="0" />
                                <asp:Button ID="btnUnlock" runat="server" Visible="true" OnClick="btnUnlock_Click"
                                    Text="UnLock" CssClass="btn btn-primary" ToolTip="Unlock" TabIndex="0" />
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                            </div>

                            <div class="col-12 mt-3">
                                <asp:Panel ID="Panel" runat="server">
                                    <asp:ListView ID="lvstudentlist" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Degree Completion Criteria List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkhead" runat="server" onClick="return SelectAll(this);" /></th>
                                                        <th>Reg. No. </th>
                                                        <th>Student Name </th>
                                                        <th>Course Category-Wise Status </th>
                                                        <th>Eligibility Status</th>
                                                        <th>Lock Status</th>
                                                        <th>Date </th>
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
                                                    <asp:CheckBox ID="chkbody" runat="server" CssClass="itemCheckbox" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblregno" runat="server" Text='<%# Eval("REGNO")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblstudentname" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label></td>
                                                <td>
                                                    <asp:LinkButton runat="server" OnClick="lnkbtncoursecategory_Click" ID="lnkbtncoursecategory" Text="View Course Category Wise Eligibity" CommandArgument='<%# Eval("IDNO")%>' class="newAddNew" CausesValidation="false" TabIndex="0" Enabled='<%# Eval("ELIGIBILITY_STATUS").ToString() == "-" ? false : true %>' ForeColor='<%# Eval("ELIGIBILITY_STATUS").ToString() == "-" ? System.Drawing.Color.Red : System.Drawing.Color.Green %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbleligibilty" runat="server" Text='<%# Eval("ELIGIBILITY_STATUS")%>' ToolTip='<%# Eval("ELIGIBILITY")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbllockstatus" runat="server" Text='<%# Eval("LOCK")%>' ToolTip='<%# Eval("LOCK_STATUS")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbldate" runat="server" Text='<%# (Eval("LOCK_DATE").ToString() != string.Empty) ? Eval("LOCK_DATE","{0:dd-MMM-yyyy}") : "-".ToString()%>'></asp:Label></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <%--Model Couse Category wise starts here --%>
                    <div class="modal fade" id="CouserCategorymodel" role="dialog">
                        <asp:UpdatePanel ID="updPopUp" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-dialog modal-lg">
                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header">

                                            <h4 class="modal-title" style="font-weight: 600;">Course Category Wise Criteria</h4>
                                            <button type="button" class="close" data-dismiss="modal" style="color: red; font-weight: bolder">&times;</button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Min Credit Required :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblcredit" runat="server" Text='' Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Credit Obtained :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblobtaincredit" runat="server" Text='' Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Min Grade Required:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblgrade" runat="server" Text='' Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Grade Obtained :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblobtaingarde" runat="server" Text='' Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-12 col-md-12 col-12" id="divcoursenotregisted" runat="server">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Course Required To Completed Degree :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblcoursename" runat="server" Text='' Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3">
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <asp:ListView ID="lvcoursecatelist" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Course Category List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Sr.No.</th>
                                                                        <th>Course Category</th>
                                                                        <th>Min. Credit</th>
                                                                        <th>Obtain Credit</th>
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
                                                                    <%# Container.DataItemIndex+1 %>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblcoursecategory" runat="server" Text='<%# Eval("CATEGORYNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblcoursemincredit" runat="server" Text='<%# Eval("MIN_CREDITS")%>'></asp:Label></td>

                                                                <td>
                                                                    <asp:Label ID="lblobtaincredit1" runat="server" Text='<%# Eval("CREDITS")%>'></asp:Label></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                            <div class="col-12 mt-5">
                                                <div class="col-lg-12 col-md-12 col-12 btn-footer" style="align-content: center" id="div2" runat="server">
                                                     <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item">
                                                    <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="btn btn-primary" Text="Detail Report" />
                                                     </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="modal-footer"></div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <%--Model Couse Category Ends here--%>
                </div>
            </div>
        </div>
    </div>


    <script language="javascript" type="text/javascript">
        function SelectAll(headchk) {
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


</asp:Content>


