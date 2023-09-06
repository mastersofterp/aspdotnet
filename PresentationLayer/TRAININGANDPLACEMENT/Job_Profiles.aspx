<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Job_Profiles.aspx.cs" Inherits="EXAMINATION_Projects_Job_Profiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        $(function () {
            showPopup = function () {
                debugger;
                $("#myModal").modal('show');
                return true;
            }
        });
    </script>
    <style>
        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 90px;
        }

        .badge-warning {
            color: #fff !important;
        }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }

        .company-logo img {
            width: 26px;
        }

        input[type=checkbox], input[type=radio] {
            margin: 0px 5px 0 0;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><span>Job Profile</span></h3>
                    <asp:HiddenField runat="server" ID="hdshedno" />
                </div>

                <div class="box-body">
                    <%-- <div class="col-12 mt-3">
                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Logo</th>
                                    <th>Company Name</th>
                                    <th>Location</th>
                                    <th>Job Type</th>
                                    <th>Job Role</th>
                                    <th>Status</th>
                                    <th>Job Details</th>
                                    <th>Round Details</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="company-logo">
                                        <img src="../../IMAGES/SLIIT_logo.png" alt="logo" />
                                    </td>
                                    <td>MastersoftErp Solution Pvt. Ltd.</td>
                                    <td>Nagpur</td>
                                    <td>Job Type</td>
                                    <td>Job Role</td>
                                    <td><span class="badge badge-warning">Not Applied</span></td>
                                    <td class="text-center"><i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#Details_Veiw"></i></td>
                                    <td class="text-center"><i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#View_Details"></i></td>
                                </tr>
                                <%--<tr>
                                    <td class="company-logo">
                                        <img src="../../IMAGES/SLIIT_logo.png" alt="logo" />
                                    </td>
                                    <td>MastersoftErp Solution Pvt. Ltd.</td>
                                    <td>Nagpur</td>
                                    <td>Job Type</td>
                                    <td>Job Role</td>
                                    <td><span class="badge badge-success">Applied</span></td>
                                    <td>-</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="company-logo">
                                        <img src="../../IMAGES/SLIIT_logo.png" alt="logo" />
                                    </td>
                                    <td>MastersoftErp Solution Pvt. Ltd.</td>
                                    <td>Nagpur</td>
                                    <td>Job Type</td>
                                    <td>Job Role</td>
                                    <td><span class="badge badge-success">Selected</span></td>
                                    <td>-</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="company-logo">
                                        <img src="../../IMAGES/SLIIT_logo.png" alt="logo" />
                                    </td>
                                    <td>MastersoftErp Solution Pvt. Ltd.</td>
                                    <td>Nagpur</td>
                                    <td>Job Type</td>
                                    <td>Job Role</td>
                                    <td><span class="badge badge-danger">Rejected</span></td>
                                    <td>-</td>
                                    <td></td>
                                </tr>--%>
                    <%--</tbody>
                        </table>
                    </div>--%>
                </div>


                <div class="box-body">
                    <asp:UpdatePanel ID="updPanel" runat="server">
                        <ContentTemplate>
                            <div class="col-12 mt-3">
                                <asp:ListView ID="lvJobProfile" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <%--<th>Logo</th>--%>
                                                    <th>Company Name</th>
                                                    <th>Location</th>
                                                    <th>Job Type</th>
                                                    <th>Job Role</th>
                                                    <th>Status</th>
                                                    <th>Job Details</th>
                                                    <th>Round Details</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <%-- <td class="company-logo">
                                         <asp:Image ID="Image1" runat="server" ImageUrl='<%# "data:image/jpg;base64," + Convert.ToBase64String((byte[])Eval("LOGO")) %>' Height="30" Width="30" ToolTip='<%# Eval("COMPID") %>'/>
                                       
                                    </td>--%>
                                            <td><%# Eval("COMPNAME")%></td>
                                            <td><%# Eval("CITY")%></td>
                                            <td><%# Eval("JOBTYPE")%></td>
                                            <td><%# Eval("JOBROLETYPE")%></td>
                                            <td><%--<span class="badge badge-success">Applied</span>--%>
                                                <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("STUDSTAUS")%>'></asp:Label>

                                            </td>
                                            <%--<td class="text-center"><i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#Details_Veiw" onclick=""> </i></td>--%>
                                            <td class="text-center">
                                                <asp:LinkButton ID="lnkcmpdetails" runat="server" CssClass="fa fa-eye" CommandArgument='<%# Eval("SCHEDULENO") %>' OnClick="lnkcmpdetails_Click"></asp:LinkButton></td>
                                            <td class="text-center"><%--<i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#View_Details">--%>
                                                <asp:LinkButton ID="lnkrdetails" runat="server" CssClass="fa fa-eye" OnClick="lnkrdetails_Click" ToolTip='<%# Eval("SCHEDULENO") %>'></asp:LinkButton></td>
                                            <asp:HiddenField ID="hdscheduleno" runat="server" Value='<%# Eval("SCHEDULENO") %>' />
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lvJobProfile" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <!-- Company Details View Modal -->
    <asp:UpdatePanel ID="updPop" runat="server">
        <ContentTemplate>
            <div class="modal" id="Details_Veiw" >
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Company Details</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body pl-0 pr-0">
                            <asp:UpdatePanel ID="upnlJobProfile" runat="server">
                                <ContentTemplate>
                                    <div class="col-12 mb-3">

                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <div class="sub-heading">
                                                    <h5>Job Details</h5>
                                                </div>
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Company Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblCompanyName" runat="server" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Job Type :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblJobType" runat="server" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Job Role :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblJobRole" runat="server" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Placement Mode :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblPlacementMode" runat="server" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Location :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblLocation" runat="server" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-12 mt-3 mt-md-0">
                                                <div class="sub-heading">
                                                    <h5>Schedule Details</h5>
                                                </div>
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Schedule Date (From-To) :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDateFromTo" runat="server" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Venue :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblVenue" runat="server" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Last Date to Apply :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblLastDateApply" runat="server" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-12 mt-3">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Job Description :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblJobDescription" runat="server" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-12 mt-3">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Eligibility :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEligibility" runat="server" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-12 mt-3">
                                                <div class="sub-heading">
                                                    <h5>Salary/Stipend</h5>
                                                </div>
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Amount :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblAmount" runat="server" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Currency :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblCurrency" runat="server" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Interval :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblInterval" runat="server" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-12 mt-3">
                                                <div class="sub-heading">
                                                    <h5>Selection Criteria</h5>
                                                </div>
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Rounds :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblRound1" runat="server" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Description :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDescription" runat="server" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <asp:Panel ID="pnljob" runat="server">
                                            <div class="row mt-3">
                                                <div class="form-group col-lg-6 col-md-12 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label class="mb-0">Upload Resume <small style="color: #ff0000;">(Upload Pdf Only - Max. size 5mb)</small></label>
                                                    </div>
                                                    <%--<input type="file" id="myfile" name="myfile">--%>
                                                    <asp:FileUpload ID="fuResume" runat="server" />
                                                    <asp:RequiredFieldValidator ID="requpload" runat="server" ControlToValidate="fuResume" Display="None" ValidationGroup="abc" CssClass="color-red" ErrorMessage="Please Upload Resume"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-12 col-12">
                                                    <div class="label-dynamic">
                                                        <label></label>
                                                    </div>
                                                    <asp:CheckBox ID="chkinfo" runat="server" Text="I hereby declared that all inforfation is correct" />
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <asp:Panel ID="pnlhide" runat="server">
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnApply" runat="server" CssClass="btn btn-outline-info" OnClientClick="javascript:return Validate()" OnClick="btnApply_Click" ValidationGroup="abc">Apply</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" data-dismiss="modal">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="abc" ShowMessageBox="true" ShowSummary="false"
                                                DisplayMode="List" />
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnApply" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                s
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



    <!-- Status Details View Modal -->
    <div class="modal" id="View_Details">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">View Details</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body pl-0 pr-0">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Application Details</h5>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Status :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblStatus" runat="server" Text="Approved" Font-Bold="true"></asp:Label>
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>Date :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblAppDate" runat="server" Text="12-10-2021" Font-Bold="true"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item"><b></b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblSelectedoffer" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-lg-6 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Remark :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblAppRemark" runat="server" Text="" Font-Bold="true"></asp:Label></a>

                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 mt-3">
                        <div class="sub-heading">
                            <h5>Round Details</h5>
                        </div>
                        <%-- <div class="table table-responsive">
                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                <thead class="bg-light-blue">
                                    <tr>
                                        <th>Round Name</th>
                                        <th>Date</th>
                                        <th>Status</th>
                                        <th>Remark</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>1</td>
                                        <td>20-01-2022</td>
                                        <td><span class="badge badge-success">Selected</span></td>
                                        <td>Remark Detail Here</td>
                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td>20-01-2022</td>
                                        <td><span class="badge badge-danger">Rejected</span></td>
                                        <td>Remark Detail Here</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>--%>
                        <asp:UpdatePanel ID="upnlappldetails" runat="server">
                            <ContentTemplate>
                                <div class="col-12 mt-3">
                                    <asp:ListView ID="lvappldetails" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Round Name</th>
                                                        <th>Date</th>
                                                        <th>Status</th>
                                                        <th>Remark</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("ROUNDNAME")%></td>
                                                <td><%# Eval("OFFER_DATE","{0: dd/MM/yyyy}")%></td>
                                                <td><span class="badge badge-success">Selected</span></td>
                                                <td>You Have Successfully Passed This Round Process.</td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Offer Details</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Offer Description</label>
                                        </div>
                                        <asp:TextBox ID="txtOfferDescription" runat="server" Visible="true" TextMode="MultiLine" CssClass="form-control" ReadOnly="true"></asp:TextBox><%--Enabled="false"--%>
                                    </div>
                                   
                                    <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="divremark">
                                        <div class="label-dynamic">
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtRemarkOffer" runat="server" Visible="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:HiddenField id="hdofferDescription" runat="server" Value=""/>--%>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divstatus">
                                        <div class="label-dynamic">
                                            <label><span style="color: red;">*</span>Status</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Accepted</asp:ListItem>
                                            <asp:ListItem Value="2">Rejected</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lvJobProfile" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <asp:Panel ID="pnlrounds" runat="server" >
                        <div class="col-12">

                            <div class="row" >

                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divRounds">
                                    <div class="label-dynamic">
                                        <label>Download Attachement</label>
                                    </div>
                                   
                                    <asp:LinkButton ID="lnkdownload" runat="server" CssClass="fa fa-download " aria-hidden="true" OnClick="lnkdownload_Click" ToolTip='<%# Eval("RESUME") %>'></asp:LinkButton>
                                </div>


                                <div class="col-12 btn-footer mt-3" runat="server" id="divbutton">
                                    <asp:LinkButton ID="btnSubmitOffer" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitOffer_Click" OnClientClick="return validateRoundDetails();">Submit</asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelOffer" runat="server" CssClass="btn btn-outline-danger">Cancel</asp:LinkButton>
                                </div>

                            </div>

                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <script>
        function Show() {
            //$("#Details_Veiw").Show();
            $("#Details_Veiw").modal('show');
        }
        function Close() {
            //$("#Details_Veiw").Show();
            alert("Close");
            $("#Details_Veiw").modal('hide');
        }
    </script>
    <script>


        function Showround() {
            //$("#Details_Veiw").Show();
            $("#View_Details").modal('show');
        }
        //function Validate() {
        //    alert("A");
        //    var uploadfile = document.getElementById('fuResume');
        //    if ($('#fuResume').val() == "") {
        //        alert("Please Upload Resume!");
        //        return false;
        //    }
        //}


        //$(document).ready(function () {
        //    alert('A');
        //    $('#btnApply').click(function () {
        //        if ($('#fuResume').val() == "") {
        //            alert("Please Upload Resume!");
        //            return false;
        //        }
        //    });
        //});




        function validateRoundDetails() {

            var ddljobtype = $("[id$=ddlStatus]").attr("id");
            var ss = document.getElementById('<%=ddlStatus.ClientID%>').value;

            if (ss == '0') {

                alert('Please Select Status.', 'Warning!');
                $(ddljobtype).focus();
                return false;
            }
        }
    </script>


</asp:Content>

