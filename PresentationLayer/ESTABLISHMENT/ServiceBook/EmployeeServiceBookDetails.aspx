<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeServiceBookDetails.aspx.cs" Inherits="ESTABLISHMENT_SERVICEBOOK_EmployeeServiceBookDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>--%>

    <script type="text/javascript">
        ////On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
    <style>
        .list-group .list-group-item .sub-label {
            margin-left: 1rem!important;
            float: none;
        }
    </style>

    <style>
        .accordion-button {
            background: #eee;
            padding-top: 5px;
            margin-bottom: 10px;
            cursor: pointer;
        }

        .sub-heading {
            padding-bottom: 0px;
        }

            .sub-heading h5 {
                margin-bottom: 5px;
            }

        .more-less {
            float: right;
            color: #053769;
            display: inline-block;
            margin-top: 3px;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div22" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">EMPLOYEE SERVICE BOOK DETAILS</h3>
                </div>

                <div class="box-body">
                    <div class="colapse-panel" id="accordion">
                        <div class="col-12">
                            <asp:Label ID="Label1" SkinID="Errorlbl" runat="server"></asp:Label>
                            <asp:Label ID="Label2" runat="server" SkinID="lblmsg"></asp:Label>
                        </div>

                        <asp:Panel ID="pnlAdd" runat="server" Visible="false">

                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divEmployeeSearch" aria-expanded="true" aria-controls="collapseOne">
                                        <i class="more-less fas fa-minus"></i>
                                        <div class="sub-heading">
                                            <h5>Select Criteria
                                            </h5>
                                            <%--<asp:Image ID="ImageSearch" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                                    onclick="javascript:toggleExpansion(this,'divEmployeeSearchModify')" /></span></h5>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="divEmployeeSearch" class="collapse show mt-4" data-parent="#accordion">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Order By</label>
                                            </div>
                                            <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                                runat="server" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">IdNo</asp:ListItem>
                                                <asp:ListItem Value="2">Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Employee Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvselect" runat="server" ControlToValidate="ddlEmployee"
                                                Display="None" ErrorMessage="Please Select Employee" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary"
                                                ValidationGroup="Submit" />
                                            <asp:ValidationSummary ID="valsumAuthor" runat="server" ValidationGroup="Submit"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <%-- ======================== Select Criteria ===============--%>

                        <asp:Panel ID="pnlpersonal" runat="server" Visible="false">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divGeneralInfo" aria-expanded="true" aria-controls="collapsetwo">
                                        <i class="more-less fas fa-plus"></i>
                                        <div class="sub-heading">
                                            <h5>Personal Memoranda
                                            </h5>
                                            <%--<asp:Image ID="ImageSearch" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                                    onclick="javascript:toggleExpansion(this,'divEmployeeSearchModify')" /></span></h5>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divGeneralInfo" class="col-12 collapse  mt-4" data-parent="#accordion">
                                <div class="row">
                                    <div class="col-10">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblName" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Fathers Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblFatherName" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Marks of Identification 1 :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblMarksofIdentification1" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Joining Date :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDOJ" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Mobile Phone No.:</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblPhoneNumber" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Height :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblHeight" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label></a>
                                                    </li>



                                                </ul>
                                            </div>

                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">

                                                    <li class="list-group-item"><b>Mother Name  :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblMotherName" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Date of Birth  :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblBirthDate" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Marks of Identification 2 :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblMarksofIdentification2" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Whats App no. :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblWhatsAppno" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label></a>
                                                    </li>

                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <asp:Image ID="imgEmpPhoto" runat="server" TabIndex="4" BorderColor="White" ImageUrl="~/Images/nophoto.jpg" Height="70%" Width="90%" />
                                    </div>
                                </div>

                                <%-- -------------------------------------------------------------%>
                                <div class="row mt-3">
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Country :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCountry" runat="server" Text="" TabIndex="4" ToolTip="Country"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Taluka :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblTaluka" runat="server" Text="" TabIndex="5" ToolTip="Taluka"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Pincode :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblPresentPncode" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label></a>
                                            </li>


                                            <li class="list-group-item"><b>Aicte no :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblAicteno" runat="server" Text="" TabIndex="4" ToolTip="Aicte no"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Passport no:</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblPassport" runat="server" Text="" TabIndex="4" ToolTip="Passport no"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>State :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblPresentState" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>District:</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDistrict" runat="server" Text="" TabIndex="4" ToolTip="District"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Blood Group :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBloodGroup" runat="server" Text="" TabIndex="4" ToolTip="Blood Group"></asp:Label>
                                                </a>
                                            </li>

                                            <li class="list-group-item"><b>Pan no :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblPanno" runat="server" Text="" TabIndex="3" ToolTip="Pan no"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>City :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblPresentCity" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Present Address :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblPresentAddress" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label>
                                                </a>
                                            </li>

                                            <li class="list-group-item"><b>Aadhar no:</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblAadharno" runat="server" Text="" TabIndex="4" ToolTip="Aadhar no"></asp:Label>
                                                </a>
                                            </li>

                                        </ul>
                                    </div>
                                </div>
                                <%------------------------------------------------------------------%>


                                <%-----------------------------------------------------------------%>

                                <%-------------------------------------------------------------------------%>
                                <div class="row mt-2">


                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Staff Type :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStaffType" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Appointment Category :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblAppointmentCategory" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Present Designation :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDesignation" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label>
                                                </a>
                                            </li>

                                            <li class="list-group-item"><b>Personal Mail ID:</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblEmail" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Present Department  :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDeparteMent" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Official/Alternate Email ID :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblOffiEmail" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label></a>
                                            </li>

                                        </ul>
                                    </div>
                                </div>
                                <%-------------------------------------------------------------------------%>
                                <div class="row mt-2">
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Nationality :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblNationality" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item" hidden><b>Caste :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCaste" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label>
                                                </a>
                                            </li>

                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Religion:</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblReligion" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">

                                            <li class="list-group-item"><b>Category :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCategory" runat="server" Text="" TabIndex="3" ToolTip="Title"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>

                                </div>
                            </div>
                        </asp:Panel>


                        <%-- ================================================================== Personal Memoranda--%>



                        <asp:Panel ID="pnlfamily" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divFamilyInfo" aria-expanded="true" aria-controls="collapsethree">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--<div class="sub-heading">
                                            <h5>Family Particulars
                                            </h5>
                                            <%--<asp:Image ID="ImageSearch" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                                    onclick="javascript:toggleExpansion(this,'divEmployeeSearchModify')" /></span></h5>--%>
                                        <%-- </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Family Particulars
                                                </h5>
                                                <%--<asp:Image ID="ImageSearch" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                                    onclick="javascript:toggleExpansion(this,'divEmployeeSearchModify')" /></span></h5>--%>
                                            </div>
                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblFamily" runat="server"></asp:Label></h5>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                                <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                                <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                                <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                                <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="form-group col-md-12">--%>
                            <div class="col-12 collapse" data-parent="#accordion" id="divFamilyInfo">
                                <div class="table-responsive mt-4">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table2">
                                        <%--<asp:Repeater ID="Rep_Familyinfo" runat="server">--%>
                                        <asp:ListView ID="Rep_Familyinfo" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Family Particular Details</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Employee Name
                                                            </th>
                                                            <th>Family Member
                                                            </th>
                                                            <th>Relation
                                                            </th>
                                                            <th>DOB
                                                            </th>
                                                            <th>Age
                                                            </th>
                                                            <th id="divFolder" runat="server">Attachment
                                                            </th>
                                                            <th id="divBlob" runat="server">Attachment
                                                            </th>
                                                            <th>Action
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
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("memname") %>
                                                    </td>

                                                    <td>
                                                        <%# Eval("relation") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("dob", "{0:dd/MM/yyyy}")%>                                                                  
                                                    </td>
                                                    <td>
                                                        <%# Eval("age")%>                                                                   
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("FNNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnFamilyParticularPreview" runat="server" OnClick="imgbtnFamilyParticularPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnFamilyParticularPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("FNNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" OnClick="btnApproval_Click" CommandName='<%# Eval("IDNO")%>' />
                                                        <asp:Button ID="btnReject" runat="server" Text="Reject" CommandArgument='<%# Eval("FNNO")%>' TabIndex="2"
                                                            ToolTip="Reject" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO")%>' OnClick="btnReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </table>
                                    <div id="DivLeaveCardNoRecord" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: Red;">
                                        No records to display.
                                    </div>

                                </div>
                            </div>
                        </asp:Panel>


                        <%-- ================================================================== Family Particulars--%>

                        <asp:Panel ID="pnlnomination" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divNaminationInfo" aria-expanded="true" aria-controls="collapsefour">
                                        <i class="more-less fas fa-plus"></i>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Nomination
                                                </h5>
                                                <%--<asp:Image ID="ImageSearch" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                                    onclick="javascript:toggleExpansion(this,'divEmployeeSearchModify')" /></span></h5>--%>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblNom" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <%--<div class="form-group col-md-12">--%>
                            <div class="col-12 collapse mt-4" data-parent="#accordion" id="divNaminationInfo">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap  " style="width: 100%" id="table3">
                                        <%--<asp:Repeater ID="Rep_Nomination" runat="server">--%>
                                        <asp:ListView ID="Rep_Nomination" runat="server">
                                            <%--<HeaderTemplate>--%>
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Nomination Details</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Employee Name
                                                            </th>
                                                            <th>Nomin.For
                                                            </th>
                                                            <th>Nominee
                                                            </th>
                                                            <th>Relation
                                                            </th>
                                                            <th>Percentage
                                                            </th>
                                                            <th>DOB
                                                            </th>
                                                            <th>Age
                                                            </th>
                                                            <th id="divFolder" runat="server">Attachment
                                                            </th>
                                                            <th id="divBlob" runat="server">Attachment
                                                            </th>
                                                            <th>Action
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                                <%--</HeaderTemplate>--%>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NOMINITYPE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("name")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("relation")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("per")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("dob", "{0:dd/MM/yyyy}")%>                                                                  
                                                    </td>
                                                    <td>
                                                        <%# Eval("Age")%>                                                                 
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("NFNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnNominPreview" runat="server" OnClick="imgbtnNominPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnNominPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnNomiApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("nfno")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" OnClick="btnNomiApproval_Click" CommandName='<%# Eval("IDNO")%>' />
                                                        <asp:Button ID="btnNomiReject" runat="server" Text="Reject" CommandArgument='<%# Eval("nfno")%>' TabIndex="2"
                                                            ToolTip="Reject" CssClass="btn btn-primary" OnClick="btnNomiReject_Click" CommandName='<%# Eval("IDNO")%>' />
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                        </tbody>                                                         
                                        </FooterTemplate>--%>
                                            <%-- </asp:Repeater>--%>
                                        </asp:ListView>
                                    </table>
                                    <div id="Div2" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                            <%--</div>--%>
                        </asp:Panel>


                        <%-- ================================================================== Nomination--%>

                        <div class="col-12 mt-3" id="divImage" runat="server" visible="false">
                            <div class="sub-heading">
                                <h5>Certificate Upload</h5>
                                <%-- <div class="box-tools pull-right">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                            onclick="javascript:toggleExpansion(this,'divCertiInfo')" />
                                    </div>--%>
                            </div>
                        </div>
                        <div class="col-12 " id="divpanimage" runat="server" visible="false">
                            <asp:Panel ID="pnlcerti" runat="server">
                                <%--<div class="form-group col-md-12">--%>

                                <div class="table-responsive mt-4" id="divCertiInfo " style="display: block;">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table4">
                                        <asp:Repeater ID="Rep_ImageUpload" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Image Type
                                                        </th>
                                                        <th>Image
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("imagetype") %>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank"
                                                            NavigateUrl='<%# GetFileNamePath(Eval("FileName"))%>'><%# Eval("FileName")%>
                                                        </asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>                                                        
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <div id="Div3" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>

                                <%--</div>--%>
                            </asp:Panel>
                        </div>

                        <%-- ================================================================== Image Upload--%>

                        <asp:Panel ID="pnlquali" runat="server">
                            <%--<div class="form-group col-md-12">--%>
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divQualiInfo" aria-expanded="true" aria-controls="collapsefive">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--<asp:Image ID="ImageSearch" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                                    onclick="javascript:toggleExpansion(this,'divEmployeeSearchModify')" /></span></h5>--%>

                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Qualification
                                                </h5>
                                                <%--<asp:Image ID="ImageSearch" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                                    onclick="javascript:toggleExpansion(this,'divEmployeeSearchModify')" /></span></h5>--%>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblQuali" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divQualiInfo" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap  " style="width: 100%" id="table5">
                                        <%--<asp:Repeater ID="Rep_Qualification" runat="server">--%>
                                        <asp:ListView ID="Rep_Qualification" runat="server">
                                            <%--<HeaderTemplate>--%>
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Qualification Details</h5>
                                                </div>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Level Name
                                                        </th>
                                                        <th>Exam Pass
                                                        </th>
                                                        <th>University Name
                                                        </th>
                                                        <th>Institute Name
                                                        </th>
                                                        <th>Passing Year
                                                        </th>
                                                        <th>Specialization
                                                        </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
                                                        </th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                                <%--<tbody>--%>
                                            </LayoutTemplate>
                                            <%--</HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("QUALILEVELNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ExamName") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("UNIVERSITY_NAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("inst")%>                                                                   
                                                    </td>
                                                    <td>
                                                        <%# Eval("passyear") %>                                                                   
                                                    </td>
                                                    <td>
                                                        <%# Eval("speci") %>
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("QNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnQualiPreview" runat="server" OnClick="imgbtnQualiPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnQualiPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnQualiApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("QNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" OnClick="btnQualiApproval_Click" CommandName='<%# Eval("IDNO")%>' />
                                                        <asp:Button ID="btnQualiReject" runat="server" Text="Reject" CommandArgument='<%# Eval("QNO")%>' TabIndex="2"
                                                            ToolTip="Reject" CssClass="btn btn-primary" OnClick="btnQualiReject_Click" CommandName='<%# Eval("IDNO")%>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </table>
                                    <%--<FooterTemplate>
                                            </tbody>                                                       
                                        </FooterTemplate>--%>
                                    <%--</asp:Repeater>--%>

                                    <div id="Div4" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                            <%--</div>--%>
                        </asp:Panel>


                        <%-- ================================================================== Qualification--%>


                        <asp:Panel ID="pnldept" runat="server">

                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#dept_examination" aria-expanded="true" aria-controls="collapsesix">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--<asp:Image ID="ImageSearch" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                                    onclick="javascript:toggleExpansion(this,'divEmployeeSearchModify')" /></span></h5>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Department Examination
                                                </h5>
                                                <%--<asp:Image ID="ImageSearch" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                                    onclick="javascript:toggleExpansion(this,'divEmployeeSearchModify')" /></span></h5>--%>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblDept" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="dept_examination" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="table6">
                                        <asp:ListView ID="Rep_DeptExam" runat="server">
                                            <LayoutTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Name Of Exam
                                                        </th>
                                                        <th>Reg.No
                                                        </th>
                                                        <th>YOP
                                                        </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
                                                        </th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("EXAM")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REGNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PASSYEAR")%>
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("DENO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtndeptExamPreview" runat="server" OnClick="imgbtndeptExamPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtndeptExamPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnDepartApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("DENO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO")%>' OnClick="btnDepartApproval_Click" />
                                                        <asp:Button ID="btnDepartReject" runat="server" Text="Reject" CommandArgument='<%# Eval("DENO")%>' TabIndex="2"
                                                            ToolTip="Reject" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO")%>' OnClick="btnDepartReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </table>
                                    <div id="Div5" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                        <%-- ==================================================================Department Examination --%>

                        <div class="col-12 mt-3" id="divExtra" runat="server" visible="false">
                            <div class="sub-heading">
                                <h5>Extra Curriculum</h5>
                                <%-- <div class="box-tools pull-right">
                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                            onclick="javascript:toggleExpansion(this,'divExtracrrInfo')" />
                                    </div>--%>
                            </div>
                        </div>
                        <div class="col-12 mt-4" id="pnldivExtra" runat="server" visible="false">
                            <asp:Panel ID="pnlcurr" runat="server">
                                <div class="table-responsive" id="divExtracrrInfo">
                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="table7">
                                        <asp:Repeater ID="Rep_ExtCurr" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>

                                                        <th>INTERESTS
                                                        </th>
                                                        <th>HOBBIES
                                                        </th>
                                                        <th>ORGANISED BY
                                                        </th>
                                                        <th>INVOLVED BY
                                                        </th>
                                                        <th>COUNSELLING
                                                        </th>
                                                        <th>MONITORING ACTIVITY
                                                        </th>
                                                        <th>COMMITTEE
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>

                                                    <td>
                                                        <%# Eval("ACT_INTEREST") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ACT_HOBBIES") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ACT_ORG") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ACT_INVOLVE")%>
                                                                    
                                                    </td>
                                                    <td>
                                                        <%# Eval("ACT_COUNCEL_MENTOR") %>
                                                                    
                                                    </td>
                                                    <td>
                                                        <%# Eval("ACT_MONITOR") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ACT_MEMBER") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>                                                        
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <div id="Div6" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>

                        <%-- ================================================================== Extra Curriculum--%>

                        <div class="col-12 mt-3" id="divLab" runat="server" visible="false">
                            <div class="sub-heading">
                                <h5>Lab Participation</h5>
                                <%--<div class="box-tools pull-right">
                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                            onclick="javascript:toggleExpansion(this,'divLabInfo')" />
                                    </div>--%>
                            </div>
                        </div>

                        <div class="col-12 mt-4" id="pnldivLab" runat="server" visible="false">
                            <asp:Panel ID="pnllab" runat="server">
                                <div class="table-responsive" id="divLabInfo">
                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="table8">
                                        <asp:Repeater ID="Rep_LabParticipate" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>TITLE NAME
                                                        </th>
                                                        <th>PARTICIPATE DETAILS
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("DETAILS") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TITLE") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>                                                         
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <div id="Div7" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>

                        <%-- ================================================================== Lab Participation--%>

                        <div class="col-12 mt-3" id="divProject" runat="server" visible="false">
                            <div class="sub-heading">
                                <h5>Project Guidance</h5>
                                <%--<div class="box-tools pull-right">
                    <asp:Image ID="Image8" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                        onclick="javascript:toggleExpansion(this,'divProjInfo')" />
                </div>--%>
                            </div>
                        </div>
                        <div class="col-12 mt-4" id="pnldivProject" runat="server" visible="false">
                            <asp:Panel ID="pnlprojgiud" runat="server">
                                <div class="table-responsive" id="divProjInfo">
                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="table9">
                                        <asp:Repeater ID="Rep_ProjectGuide" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>

                                                        <th>TITLE NAME
                                                        </th>
                                                        <th>PROJECT GUIDANCE DETAILS
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("DETAILS") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TITLE") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>                                                            
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <div id="Div8" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>

                        <%-- ==================================================================Project Guidance --%>

                        <div class="col-12 mt-3" id="divHonar" runat="server" visible="false">
                            <div class="sub-heading">
                                <h5>Honor/Awards</h5>
                            </div>
                        </div>
                        <div class="col-12 mt-4" id="pnldivHonar" runat="server" visible="false">
                            <asp:Panel ID="pnlhonor" runat="server">
                                <div class="table-responsive" id="divHonorInfo">
                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="table10">
                                        <asp:Repeater ID="Rep_Award" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>TITLE OF PROGRAM
                                                        </th>
                                                        <th>DATE
                                                        </th>
                                                        <th>DETAILS
                                                        </th>
                                                        <th>FILE NAME
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("TITLE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DATE", "{0:dd/MM/yyyy}") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DESCRIPTION") %>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"),Eval("SRNO"),Eval("IDNO"))%>'><%# Eval("FILENAME")%></asp:HyperLink>
                                                    </td>
                                                    <%-- <td>
                                                                <%# Eval("FILENAME")%>                                                                   
                                                            </td> --%>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>                                                      
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <div id="Div9" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>

                        <%-- ================================================================== Honor/Awards--%>


                        <asp:Panel ID="pnlprevexp" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divPrevExpInfo" aria-expanded="false" aria-controls="collapseEleven">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--   <div class="sub-heading">
                                            <h5>Previous Experience</h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Previous Experience
                                                </h5>
                                                <%--<asp:Image ID="ImageSearch" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                                    onclick="javascript:toggleExpansion(this,'divEmployeeSearchModify')" /></span></h5>--%>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblPrevExp" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divPrevExpInfo" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="table11">
                                        <asp:ListView ID="Rep_PrevExp" runat="server">
                                            <LayoutTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>From Date
                                                        </th>
                                                        <th>To Date 
                                                        </th>
                                                        <th>Institution
                                                        </th>
                                                        <th>Reason
                                                        </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
                                                        </th>
                                                        <th id="divFolder1" runat="server">University Attachment
                                                        </th>
                                                        <th id="divBlob1" runat="server">University Attachment
                                                        </th>
                                                        <th id="divFolder2" runat="server">PG Attachment
                                                        </th>
                                                        <th id="divBlob2" runat="server">PG Attachment
                                                        </th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("fdt","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("tdt","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("inst")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("termination")%>
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("PSNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>

                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnprevExpPreview" runat="server" OnClick="imgbtnprevExpPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnprevExpPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td id="tdFolder1" runat="server">
                                                        <asp:HyperLink ID="lnkDownload1" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("UNIVERSITYATACHMENT"),Eval("PSNO"),Eval("IDNO"))%>'><%# Eval("UNIVERSITYATACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob1" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnprevExpUniPreview" runat="server" OnClick="imgbtnprevExpUniPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("UNIVERSITYATACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("UNIVERSITYATACHMENT") %>' Visible='<%# Convert.ToString(Eval("UNIVERSITYATACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnprevExpUniPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>

                                                    <td id="tdFolder2" runat="server">
                                                        <asp:HyperLink ID="lnkDownload2" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("PGTATTACHMENT"),Eval("PSNO"),Eval("IDNO"))%>'><%# Eval("PGTATTACHMENT")%></asp:HyperLink>
                                                    </td>

                                                    <td style="text-align: center" id="tdBlob2" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnprevExpPGPreview" runat="server" OnClick="imgbtnprevExpPGPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("PGTATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("PGTATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("PGTATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnprevExpPGPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnprevexpApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("psno")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnprevexpApproval_Click" />
                                                        <asp:Button ID="btnprevexpReject" runat="server" Text="Reject" CommandArgument='<%# Eval("psno")%>' TabIndex="2"
                                                            ToolTip="Reject" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnprevexpReject_Click" />
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </table>
                                    <div id="Div10" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                        <%-- ================================================================== Previous Experience--%>



                        <asp:Panel ID="pnladmresp" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divAdmnRespInfo" aria-expanded="false" aria-controls="collapsetwelve">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--       <div class="sub-heading">
                                            <h5>Administrative Responsibilities
                                                        <%--<span>
                                                        <img id="img2" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divBankDetails1')" /></span>--%>

                                        <%--</h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Administrative Responsibilities
                                                </h5>
                                                <%--<asp:Image ID="ImageSearch" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                                    onclick="javascript:toggleExpansion(this,'divEmployeeSearchModify')" /></span></h5>--%>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblAdminResp" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divAdmnRespInfo" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="table12">
                                        <%--<asp:Repeater ID="Rep_AdminResponse" runat="server">--%>
                                        <asp:ListView ID="Rep_AdminResponse" runat="server">
                                            <%-- <HeaderTemplate>--%>
                                            <LayoutTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Responsibility
                                                        </th>
                                                        <th>Organization
                                                        </th>
                                                        <th>From Date
                                                        </th>
                                                        <th>To Date
                                                        </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
                                                        </th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                                <%--</HeaderTemplate>--%>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Responsibility")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ORGANIZATION")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FROMDATE","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TODATE", "{0:dd/MM/yyyy}")%>
                                                                    
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("ADMINTRXNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnAdminPreview" runat="server" OnClick="imgbtnAdminPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnAdminPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnadmrespApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("ADMINTRXNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnadmrespApproval_Click" />
                                                        <asp:Button ID="btnadmrespReject" runat="server" Text="Reject" CommandArgument='<%# Eval("ADMINTRXNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnadmrespReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <%--<FooterTemplate>
                                            </tbody>                                                          
                                        </FooterTemplate>
                                    </asp:Repeater>--%>
                                        </asp:ListView>
                                    </table>
                                    <div id="Div11" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                        <%-- ======================= Administrative Responsibilities--%>


                        <div class="col-12 colapse-heading" id="divInvited" runat="server" visible="false">
                            <div class="row">
                                <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#pnldivInvited" aria-expanded="false" aria-controls="collapsethirteen">
                                    <i class="more-less fas fa-plus"></i>
                                    <div class="sub-heading">
                                        <h5>Invited Talks 
                                        </h5>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="col-12 collapse mt-4" id="pnldivInvited" runat="server" data-parent="#accordion" visible="false">
                            <asp:Panel ID="pnlinvitedtalk" runat="server">

                                <div class="table-responsive " id="divInvitedInfo">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table13">
                                        <asp:Repeater ID="Rep_InvitedTalks" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>

                                                        <th>SUBJECT
                                                        </th>
                                                        <th>ORGANIZATION
                                                        </th>
                                                        <th>DURATION
                                                        </th>
                                                        <th>DATE OF TALK
                                                        </th>
                                                        <th>REMARK
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("SUBJECT") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("VENU") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DURATION") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DATEOFTALK", "{0:dd/MM/yyyy}")%>
                                                                    
                                                    </td>
                                                    <td>
                                                        <%# Eval("REMARK") %>                                                                    
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>                                                     
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <div id="Div12" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>

                        <%-- ==================================================================Invited Talks --%>

                        <%-- ================================================================== Research and Consultancy--%>



                        <asp:Panel ID="pnlpublication" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divPublicationInfo" aria-expanded="false" aria-controls="collapsefourteen">
                                        <i class="more-less fas fa-plus"></i>
                                        <%-- <div class="sub-heading">
                                            <h5>Publication Details</h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Publication Details
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblPubDetails" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divPublicationInfo" class="col-12 collapse mt-4" data-parent="#accordion">

                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="table21">
                                        <%--<asp:Repeater ID="Rep_Publication" runat="server">--%>
                                        <asp:ListView ID="Rep_Publication" runat="server">
                                            <%-- <HeaderTemplate>--%>
                                            <LayoutTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Publication Type
                                                        </th>
                                                        <th>Title
                                                        </th>
                                                        <th>Year
                                                        </th>
                                                        <th>Month
                                                        </th>
                                                        <th id="divFolder" runat="server">Uploaded File
                                                        </th>
                                                        <th id="divBlob" runat="server">Uploaded File
                                                        </th>
                                                        <th>Action
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                                <%--  </HeaderTemplate>--%>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PUBLICATION_TYPE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TITLE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Year") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MONTH") %>
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("PUBTRXNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnPublicationPreview" runat="server" OnClick="imgbtnPublicationPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnPublicationPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnpublicationApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("PUBTRXNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnpublicationApproval_Click" />
                                                        <asp:Button ID="btnpublicationReject" runat="server" Text="Reject" CommandArgument='<%# Eval("PUBTRXNO")%>' TabIndex="2"
                                                            ToolTip="Reject" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnpublicationReject_Click" />
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                            </tbody>                                                        
                                        </FooterTemplate>
                                    </asp:Repeater>--%>
                                        </asp:ListView>
                                    </table>
                                    <div id="Div35" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                        <%-- ============================== Publication Details--%>


                        <asp:Panel ID="pnltraining" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divTrainingInfo" aria-expanded="false" aria-controls="collapsefifteen">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--    <div class="sub-heading">
                                            <h5>Training Attended
                                                        <%--<span>
                                                        <img id="img5" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divPayDetails1')" /></span>--%>

                                        <%-- </h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Training Attended
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblTrainingAttend" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divTrainingInfo" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="table14">
                                        <%--<asp:Repeater ID="Rep_Tarining" runat="server">--%>
                                        <asp:ListView ID="Rep_Tarining" runat="server">
                                            <%--<HeaderTemplate>--%>
                                            <LayoutTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Course
                                                        </th>
                                                        <th>Institute
                                                        </th>
                                                        <th>From Date
                                                        </th>
                                                        <th>To Date
                                                        </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
                                                        </th>
                                                        <th>Action
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                                <%--</HeaderTemplate>--%>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("course") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("inst") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("fdt","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("tdt","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("TNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnTrainingPreview" runat="server" OnClick="imgbtnTrainingPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnTrainingPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btntrainingApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("tno")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btntrainingApproval_Click" />
                                                        <asp:Button ID="btntrainingReject" runat="server" Text="Reject" CommandArgument='<%# Eval("tno")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btntrainingReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                            </tbody>                                                          
                                        </FooterTemplate>
                                    </asp:Repeater>--%>
                                        </asp:ListView>
                                    </table>
                                    <div id="Div13" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                        <%-- ================================================================== Guest Lecture (As a Resource Person)--%>

                        <asp:Panel ID="PanelGuest" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#div33" aria-expanded="false" aria-controls="collapsesixteen">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--      <div class="sub-heading">
                                            <h5>Guest Lecture (As a Resource Person)
                                            </h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Guest Lecture (As a Resource Person)
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblGuestLec" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="div33" class="col-12 collapse mt-4" data-parent="#accordion">

                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="table17">
                                        <%--<asp:Repeater ID="Repeater_Guest" runat="server">--%>
                                        <asp:ListView ID="Repeater_Guest" runat="server">
                                            <%-- <HeaderTemplate>--%>
                                            <LayoutTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Subject
                                                        </th>
                                                        <th>Venue
                                                        </th>
                                                        <th>Duration
                                                        </th>
                                                        <th>Date of talk
                                                        </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
                                                        </th>
                                                        <th>Action
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                                <%--</HeaderTemplate>--%>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUBJECT") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("VENU") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DURATION")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DATEOFTALK","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("INVTRXNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnGuestPreview" runat="server" OnClick="imgbtnGuestPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnGuestPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGuestApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("INVTRXNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnGuestApproval_Click" />
                                                        <asp:Button ID="btnGuessReject" runat="server" Text="Reject" CommandArgument='<%# Eval("INVTRXNO")%>' TabIndex="2"
                                                            ToolTip="Reject" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnGuessReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        <%--<FooterTemplate>
                                            </tbody>                                                          
                                        </FooterTemplate>
                                    </asp:Repeater>--%>
                                    </table>
                                    <div id="Div34" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                        <%-- ================================================================== Guest Lecture (As a Resource Person)--%>

                        <%-- ================================================================== Training Attended--%>


                        <asp:Panel ID="pnltraincond" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divTraincondInfo" aria-expanded="false" aria-controls="collapseseventeen">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--      <div class="sub-heading">
                                            <h5>Training Conducted
                                              
                                            </h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Training Conducted
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblTrainCond" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divTraincondInfo" class="col-12 collapse mt-4" data-parent="#accordion">

                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table15">
                                        <asp:Repeater ID="Rep_TrainingConduct" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Program/Training
                                                        </th>
                                                        <th>Institute
                                                        </th>
                                                        <th>From Date
                                                        </th>
                                                        <th>To Date</th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("course") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("inst") %>                                                                   
                                                    </td>
                                                    <td>
                                                        <%# Eval("fdt","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("tdt","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btntraincondApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("tno")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btntraincondApproval_Click" />
                                                        <asp:Button ID="btntrainingcondReject" runat="server" Text="Reject" CommandArgument='<%# Eval("tno")%>' TabIndex="2"
                                                            ToolTip="Reject" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btntrainingcondReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>                                                          
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <div id="Div14" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                        <%-- ================================================================== Training Conducted--%>
                        <%-- ================================================================== Consultancy--%>


                        <asp:Panel ID="pnlConsultancy" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divConsultancy" aria-expanded="false" aria-controls="collapseeighteen">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--  <div class="sub-heading">
                                            <h5>Consultancy
                                                        <%--<span>
                                                        <img id="img5" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divPayDetails1')" /></span>--%>

                                        <%--</h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Consultancy 
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblConsultancy" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divConsultancy" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table45">
                                        <asp:Repeater ID="Repeater_Consultancy" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Title
                                                        </th>
                                                        <th>Name of Organization
                                                        </th>
                                                        <th>From Date
                                                        </th>
                                                        <th>To Date</th>
                                                        <th>Duration
                                                        </th>
                                                        <th>Amount
                                                        </th>
                                                        <th>Nature of work
                                                        </th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TITLE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Name_Of_Org") %>                                                                   
                                                    </td>
                                                    <td>
                                                        <%# Eval("From_Date", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("To_Date", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Duration") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Amount_Earned")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Nature_of_work")%>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnConsultancyApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("SCNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnConsultancyApproval_Click" />
                                                        <asp:Button ID="btnConsultancyReject" runat="server" Text="Reject" CommandArgument='<%# Eval("SCNO")%>' TabIndex="2"
                                                            ToolTip="Reject" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnConsultancyReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>                                                          
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <div id="Div1" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                        <%-- ================================================================== Consultancy--%>
                        <%-- ================================================================== Accomplishment--%>

                        <asp:Panel ID="PanelAccomplishment" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#div23" aria-expanded="false" aria-controls="collapseNineteen">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--  <div class="sub-heading">
                                            <h5>Accomplishment--%>
                                        <%--<span>
                                                        <img id="img5" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divPayDetails1')" /></span>--%>

                                        <%--</h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Accomplishment 
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblAccomplishment" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="div23" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table50">
                                        <asp:Repeater ID="Repeater_Accomplishment" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Award Name
                                                        </th>
                                                        <th>Organization Address
                                                        </th>
                                                        <th>Date Received
                                                        </th>
                                                        <th>Amount</th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AwardName") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ORG_ADDRESS") %>                                                                   
                                                    </td>
                                                    <td>
                                                        <%# Eval("DOA", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AMOUNT_REC")%>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnAccomplishmentApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("ACNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnAccomplishmentApproval_Click" />
                                                        <asp:Button ID="btnAccomplishmentReject" runat="server" Text="Reject" CommandArgument='<%# Eval("ACNO")%>' TabIndex="2"
                                                            ToolTip="Reject" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnAccomplishmentReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>                                                          
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <div id="Div24" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <%-- ================================================================== Accomplishment--%>

                        <asp:Panel ID="pnlmember" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divMemberInfo" aria-expanded="false" aria-controls="collapsetwenty">
                                        <i class="more-less fas fa-plus"></i>
                                        <%-- <div class="sub-heading">
                                            <h5>Membership in Professional Society--%>
                                        <%--<span>
                                                        <img id="img5" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divPayDetails1')" /></span>--%>

                                        <%--</h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Membership in Professional Society 
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblMem" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divMemberInfo" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <asp:Repeater ID="Rep_Membership" runat="server">
                                        <HeaderTemplate>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table16">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Name of the professional body
                                                        </th>
                                                        <th>Membership No.
                                                        </th>
                                                        <th>Memebrship Type
                                                        </th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("EMPNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("NAME_PROF_BODY") %>
                                                </td>

                                                <td>
                                                    <%# Eval("MemberShipNo") %>
                                                </td>
                                                <td>
                                                    <%# Eval("MemberShipType")%>
                                                                    
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnmemberApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("MPNO")%>' TabIndex="2"
                                                        ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnmemberApproval_Click" />
                                                    <asp:Button ID="btnmemberReject" runat="server" Text="Reject" CommandArgument='<%# Eval("MPNO")%>' TabIndex="2"
                                                        ToolTip="Reject" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnmemberReject_Click" />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody></table>                                                            
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <div id="Div16" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                        <%-- ================================================================== Membership--%>
                        <%-- ================================================================== Funded Project--%>


                        <asp:Panel ID="PanelFunded" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#div25" aria-expanded="false" aria-controls="collapseNine">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--<div class="sub-heading">
                                            <h5>Funded Project--%>
                                        <%--<span>
                                                        <img id="img5" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divPayDetails1')" /></span>--%>

                                        <%--</h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Funded Project 
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblFund" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="div25" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table30">
                                        <asp:Repeater ID="Repeater_Funded" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Project Title
                                                        </th>
                                                        <th>Funding Agency Name
                                                        </th>
                                                        <th>Agency Category
                                                        </th>
                                                        <th>Role</th>
                                                        <th>Grant Sanctioned
                                                        </th>
                                                        <th>Project Status
                                                        </th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Project_Title") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Funding_Name") %>                                                                   
                                                    </td>
                                                    <td>
                                                        <%# Eval("Agency_Category")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Role")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Fund_Received")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Project_Status")%>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnFundedApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("SFNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnFundedApproval_Click" />
                                                        <asp:Button ID="btnFundedReject" runat="server" Text="Reject" CommandArgument='<%# Eval("SFNO")%>' TabIndex="2"
                                                            ToolTip="Reject" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnFundedReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>                                                          
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <div id="Div27" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <%-- ================================================================== Funded Project--%>
                        <%-- ================================================================== Patent--%>


                        <asp:Panel ID="PanelPatent" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#div28" aria-expanded="false" aria-controls="collapsetwentyone">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--<div class="sub-heading">
                                            <h5>Patent--%>
                                        <%--<span>
                                                        <img id="img5" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divPayDetails1')" /></span>--%>

                                        <%--</h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Patent  
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblPatent" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="div28" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table32">
                                        <asp:Repeater ID="Repeater_Patent" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Title of the Patent
                                                        </th>
                                                        <th>Applicant /Assignee Name
                                                        </th>
                                                        <th>Role
                                                        </th>
                                                        <th>Category</th>
                                                        <th>Status
                                                        </th>
                                                        <th>Withdrawn
                                                        </th>
                                                        <th>Date
                                                        </th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Title_Patent") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Applicant_Name") %>                                                                   
                                                    </td>
                                                    <td>
                                                        <%# Eval("Role")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Category")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Status")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Withdrawn")%> 
                                                    </td>
                                                    <td>
                                                        <%# Eval("DATE", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnPatentApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("PCNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnPatentApproval_Click" />
                                                        <asp:Button ID="btnPatentReject" runat="server" Text="Reject" CommandArgument='<%# Eval("PCNO")%>' TabIndex="2"
                                                            ToolTip="Reject" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnPatentReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>                                                          
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <div id="Div29" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                        <%-- ================================================================== Patent--%>
                        <%-- ================================================================== Institute Experiences--%>

                        <asp:Panel ID="PanelInstituteExperiences" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#div30" aria-expanded="false" aria-controls="collapsetwentytwo">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--<div class="sub-heading">
                                            <h5>Institute Experiences
                                                     
                                            </h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Institute Experiences  
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblInst" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="div30" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table38">
                                        <%--<asp:Repeater ID="Repeater_InstituteExperiences" runat="server">--%>
                                        <asp:ListView ID="Repeater_InstituteExperiences" runat="server">
                                            <%--<HeaderTemplate>--%>
                                            <LayoutTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Department
                                                        </th>
                                                        <th>Designation
                                                        </th>
                                                        <th>Nature of App
                                                        </th>
                                                        <th>IsCurrent</th>
                                                        <th>Duration
                                                        </th>
                                                        <th>Start Date
                                                        </th>
                                                        <th>End Date
                                                        </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
                                                        </th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                                <%--</HeaderTemplate>--%>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUBDEPT") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUBSDESIG") %>                                                                   
                                                    </td>
                                                    <td>
                                                        <%# Eval("NatureofAppointment")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Iscurrent")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Duration")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("START_DATE", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("EndDate", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("SVCNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnInstExpPreview" runat="server" OnClick="imgbtnInstExpPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnInstExpPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnInstituteExperiencesApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("SVCNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnInstituteExperiencesApproval_Click" />
                                                        <asp:Button ID="btnIntituteExpreiencesReject" runat="server" Text="Reject" CommandArgument='<%# Eval("SVCNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnIntituteExpreiencesReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <%--<FooterTemplate>
                                            </tbody>                                                          
                                        </FooterTemplate>
                                    </asp:Repeater>--%>
                                        </asp:ListView>
                                    </table>
                                    <div id="Div31" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <%-- ================================================================== Institute Experiences--%>

                        <%-- ================================================================== Loans & Advance--%>


                        <asp:Panel ID="PanelLoans" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#div15" aria-expanded="false" aria-controls="collapsetwentythree">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--<div class="sub-heading">
                                            <h5>Loans & Advance--%>
                                        <%--<span>
                                                        <img id="img5" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divPayDetails1')" /></span>--%>

                                        <%--</h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Loans & Advance 
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblLoan" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="div15" class="col-12 collapse" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap mt-4" style="width: 100%" id="table36">
                                        <%--<asp:Repeater ID="Repeater_Loans" runat="server">--%>
                                        <asp:ListView ID="Repeater_Loans" runat="server">
                                            <%--<HeaderTemplate>--%>
                                            <LayoutTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Lon.Name
                                                        </th>
                                                        <th>Ord. No.
                                                        </th>
                                                        <th>Amt.
                                                        </th>
                                                        <th>ROI</th>
                                                        <th>No.of inst.
                                                        </th>
                                                        <th>Loan Date
                                                        </th>
                                                        <th id="divFolder" runat="server">Undertaking Document
                                                        </th>
                                                        <th id="divBlob" runat="server">Undertaking Document
                                                        </th>
                                                        <th id="divFolder1" runat="server">Affidavit Document
                                                        </th>
                                                        <th id="divBlob1" runat="server">Affidavit Document
                                                        </th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </LayoutTemplate>
                                            <%-- </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("LOANNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("orderno") %>                                                                   
                                                    </td>
                                                    <td>
                                                        <%# Eval("amount")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("interest")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("instal")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("loandt", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("LNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnLoanUndPreview" runat="server" OnClick="imgbtnLoanUndPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnLoanUndPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td id="tdFolder1" runat="server">
                                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("AFFIDAVITATTACH"),Eval("LNO"),Eval("IDNO"))%>'><%# Eval("AFFIDAVITATTACH")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob1" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnLoanAffidPreview" runat="server" OnClick="imgbtnLoanAffidPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("AFFIDAVITATTACH") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("AFFIDAVITATTACH") %>' Visible='<%# Convert.ToString(Eval("AFFIDAVITATTACH"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnLoanAffidPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnLoansApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("lno")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnLoansApproval_Click" />
                                                        <asp:Button ID="btnLoansReject" runat="server" Text="Reject" CommandArgument='<%# Eval("lno")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnLoansReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <%--<FooterTemplate>
                                            </tbody>                                                          
                                        </FooterTemplate>
                                    </asp:Repeater>--%>
                                        </asp:ListView>
                                    </table>
                                    <div id="Div17" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <%-- ================================================================== Loans & Advance--%>
                        <%-- ================================================================== Pay Revision--%>

                        <asp:Panel ID="PanelRevision" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#div18" aria-expanded="false" aria-controls="collapsetwentyfour">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--  <div class="sub-heading">
                                            <h5>Pay Revision--%>
                                        <%--<span>
                                                        <img id="img5" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divPayDetails1')" /></span>--%>

                                        <%--</h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Pay Revision 
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblPayRev" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="div18" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table37">
                                        <%--<asp:Repeater ID="Repeater_Revision" runat="server">--%>
                                        <asp:ListView ID="Repeater_Revision" runat="server">
                                            <%--<HeaderTemplate>--%>
                                            <LayoutTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Designation</th>
                                                        <th>Scale </th>
                                                        <th>Amt. </th>
                                                        <th>FDate</th>
                                                        <th>TDate </th>
                                                        <th>Type </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
                                                        </th>
                                                        <th>Action </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                                <%--</HeaderTemplate>--%>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("subdesig") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("scale") %>                                                                   
                                                    </td>
                                                    <td>
                                                        <%# Eval("fdt","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("tdt","{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Type")%>
                                                    </td>
                                                    <td></td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("PRNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnPayRevPreview" runat="server" OnClick="imgbtnPayRevPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnPayRevPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnRevisionApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("PRNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnRevisionApproval_Click" />
                                                        <asp:Button ID="btnRevisionReject" runat="server" Text="Reject" CommandArgument='<%# Eval("PRNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnRevisionReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <%--<FooterTemplate>
                                            </tbody>                                                          
                                        </FooterTemplate>
                                    </asp:Repeater>--%>
                                        </asp:ListView>
                                    </table>
                                    <div id="Div19" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <%-- ================================================================== Pay Revision--%>

                        <%-- ================================================================== Transaction Type Details (Increment/Termination)--%>


                        <asp:Panel ID="PanelIncrement" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#div20" aria-expanded="false" aria-controls="collapsetwentyfive">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--<div class="sub-heading">
                                            <h5>Increment/Termination--%>
                                        <%--<span>
                                                        <img id="img5" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divPayDetails1')" /></span>--%>

                                        <%--</h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Increment/Termination
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblIncrement" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="div20" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table39">
                                        <%--<asp:Repeater ID="Repeater_Increment" runat="server">--%>
                                        <asp:ListView ID="Repeater_Increment" runat="server">
                                            <%--<HeaderTemplate>--%>
                                            <LayoutTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Ord.Eff.Date
                                                        </th>
                                                        <th>Dept
                                                        </th>
                                                        <th>Desig.
                                                        </th>
                                                        <th>Ord.No.</th>
                                                        <th>Ord.Date
                                                        </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
                                                        </th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </LayoutTemplate>
                                            <%--</HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("OrdEffDt", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("subdept")%>                                                                
                                                    </td>
                                                    <td>
                                                        <%# Eval("SubDesig")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("OrderNo")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ORDERDT", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("TRNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnIncrementPreview" runat="server" OnClick="imgbtnIncrementPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnIncrementPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnIncrementApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("TRNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnIncrementApproval_Click" />
                                                        <asp:Button ID="btnIncrementReject" runat="server" Text="Reject" CommandArgument='<%# Eval("TRNO")%>' TabIndex="2"
                                                            ToolTip="Reject" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnIncrementReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                            </tbody>                                                          
                                        </FooterTemplate>
                                    </asp:Repeater>--%>
                                        </asp:ListView>
                                    </table>
                                    <div id="Div21" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <%-- ================================================================== Transaction Type Details (Increment/Termination)--%>
                        <%-- ================================================================== Matters/Memos--%>

                        <asp:Panel ID="PanelMatters" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#div26" aria-expanded="false" aria-controls="collapsetwentysix">
                                        <i class="more-less fas fa-plus"></i>
                                        <%--<div class="sub-heading">
                                            <h5>Matters Details </h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Matters Details
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblMatters" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="div26" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table1">
                                        <%--<asp:Repeater ID="Repeater_Matters" runat="server">--%>
                                        <asp:ListView ID="Repeater_Matters" runat="server">
                                            <%--<HeaderTemplate>--%>
                                            <LayoutTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Head.Topic
                                                        </th>
                                                        <th>Date
                                                        </th>
                                                        <th>Matter
                                                        </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
                                                        </th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <%--</HeaderTemplate>--%>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("heading")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("edt", "{0:dd/MM/yyyy}")%>                                                         
                                                    </td>
                                                    <td>
                                                        <%# Eval("matter")%>
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("MNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnMattersPreview" runat="server" OnClick="imgbtnMattersPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnMattersPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnMattersApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("mno")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnMattersApproval_Click" />
                                                        <asp:Button ID="btnMattersReject" runat="server" Text="Reject" CommandArgument='<%# Eval("mno")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnMattersReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                            </tbody>                                                          
                                        </FooterTemplate>
                                    </asp:Repeater>--%>
                                        </asp:ListView>
                                    </table>
                                    <div id="Div32" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                        <%-- ================================================================== Miscellaneous Information--%>

                        <%-- ================================================================== Academic Responsibility--%>

                        <asp:Panel ID="Panel1" runat="server">
                            <div class="col-12 colapse-heading">
                                <div class="row">
                                    <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#div36" aria-expanded="false" aria-controls="collapsetwentysix">
                                        <i class="more-less fas fa-plus"></i>
                                        <div class="row">
                                            <div class="sub-heading col-md-6 ">
                                                <h5>Academic Responsibility
                                                </h5>
                                            </div>

                                            <div class=" sub-heading  col-md-6">
                                                <h5>Pending Count :
                                                    <asp:Label ID="lblAcademic" runat="server"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="div36" class="col-12 collapse mt-4" data-parent="#accordion">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table18">
                                        <asp:ListView ID="lvAcademic" runat="server">
                                            <LayoutTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Responsibility
                                                        </th>
                                                        <th>Department / Institute Level
                                                        </th>
                                                        <th>From Date
                                                        </th>
                                                        <th>To Date
                                                        </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
                                                        </th>
                                                        <th>Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("EMPNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("RESPONSIBILITY")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPTLEVEL")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FROMDATE", "{0:dd/MM/yyyy}")%>                                                         
                                                    </td>
                                                    <td>
                                                        <%# Eval("TODATE", "{0:dd/MM/yyyy}")%>                                                         
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNameAcademicPath(Eval("ATTACHMENT"),Eval("ACDNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnAcademicPreview" runat="server" OnClick="imgbtnAcademicPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="imgbtnAcademicPreview" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnAcademicApproval" runat="server" Text="Approve" CommandArgument='<%# Eval("ACDNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnAcademicApproval_Click" />
                                                        <asp:Button ID="btnAcademicReject" runat="server" Text="Reject" CommandArgument='<%# Eval("ACDNO")%>' TabIndex="2"
                                                            ToolTip="Approve" CssClass="btn btn-primary" CommandName='<%# Eval("IDNO") %>' OnClick="btnAcademicReject_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </table>
                                    <div id="Div37" runat="server" visible="false" style="padding: 20px 20px; text-align: center; color: #000;">
                                        No records to display.
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                        <%-- ================================================================== Miscellaneous Information--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">

        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../Images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../Images/collapse_blue.jpg";
            }
        }
    </script>



    <script>
        function toggleIcon(e) {
            $(e.target)
                .prev('.colapse-heading')
                .find(".more-less")
                .toggleClass('fa-minus fa-plus');
        }
        $('.colapse-panel').on('hide.bs.collapse', toggleIcon);
        $('.colapse-panel').on('show.bs.collapse', toggleIcon);
    </script>






    <script>
        $(document).ready(function () {
            $(document).on("click", ".Emp-Name-Class", function () {
                sessionStorage.setItem("divGeneralInfo-status", "open");
            });

            var xy = sessionStorage.getItem("divGeneralInfo-status");
            if (xy == "open") {
                $(".divGeneralInfo").addClass("d-block");
            }
            else {
                $(".divGeneralInfo").removeClass("d-block");
            }
        });
    </script>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $(document).on("click", ".Emp-Name-Class", function () {
                    sessionStorage.setItem("divGeneralInfo-status", "open");

                });

                var xy = sessionStorage.getItem("divGeneralInfo-status");
                if (xy == "open") {
                    $(".divGeneralInfo").addClass("d-block");
                }
                else {
                    $(".divGeneralInfo").removeClass("d-block");
                }
            });
        });
    </script>
</asp:Content>

