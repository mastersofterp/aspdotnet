<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HostelInOutRequests.aspx.cs" Inherits="HOSTEL_GATEPASS_HostelInOutRequests" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<script runat="server">

</script>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .custom-modal-dialog {
            max-width: 800px; /* Set the desired width here */
        }
        .btn-primary {
            margin-left: 21px;
        }
        .btn-danger {
            margin-left: 19px;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">  <%--lblDynamicPageTitle Added By Himanshu tamrakar 23-02-2024--%>
                    <h3 class="box-title" style="text-transform:uppercase;" ><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Apply Date </label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtApplyDate" runat="server" TabIndex="1" ToolTip="Enter Date" AutoPostBack="true" CssClass="form-control" />
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtApplyDate" PopupButtonID="imgFromDate" Enabled="true" />
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtApplyDate"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                        MaskType="Date" ErrorTooltipEnabled="false" />
                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" EmptyValueMessage="Please enter Apply Date."
                                        ControlExtender="MaskedEditExtender1" ControlToValidate="txtApplyDate" IsValidEmpty="false"
                                        InvalidValueMessage="Apply Date  is invalid" Display="None" TooltipMessage="Input a date"
                                        ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                        ValidationGroup="submit" SetFocusOnError="true" />
                                </div>
                            </div>
                            <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    
                                    <label>InMate Code</label>
                                </div>
                                <asp:TextBox ID="txtInMateCode" runat="server" TabIndex="2" ToolTip="Enter InMate Code" AutoPostBack="true" CssClass="form-control" />
                            </div>--%>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Purpose </label>
                                </div>
                                <asp:DropDownList ID="ddlPurpose" runat="server" TabIndex="3" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" />
                                <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ControlToValidate="ddlPurpose"
                                    Display="None" ErrorMessage="Please Select Purpose" ValidationGroup="Show" SetFocusOnError="True"
                                    InitialValue="0" />

                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">

                                    <label>Gate Pass Code</label>
                                </div>
                                <asp:TextBox ID="txtGatePassCode" runat="server" TabIndex="4" ToolTip="Enter Gate Pass Code" AutoPostBack="true" CssClass="form-control" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup><label>From Date </label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtOutDate" runat="server" TabIndex="5" ToolTip="Enter Out Date" AutoPostBack="true" CssClass="form-control" />
                                    <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtOutDate" PopupButtonID="imgFromDate" Enabled="true" />
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtOutDate"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                        MaskType="Date" ErrorTooltipEnabled="false" />
                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" EmptyValueMessage="Please Enter From Date."
                                        ControlExtender="MaskedEditExtender1" ControlToValidate="txtOutDate" IsValidEmpty="false"
                                        InvalidValueMessage="Out Date  is invalid" Display="None" TooltipMessage="Input a date"
                                        ErrorMessage="Please Select Out Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                        ValidationGroup="search" SetFocusOnError="true" />
                                </div>
                            </div>
                        </div>
                        <div class="row" runat="server">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup><label>To Date </label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtInDate" runat="server" TabIndex="5" ToolTip="Enter Out Date" AutoPostBack="true" CssClass="form-control" />
                                    <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtInDate" PopupButtonID="imgFromDate" Enabled="true" />
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtInDate"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                        MaskType="Date" ErrorTooltipEnabled="false" />
                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" EmptyValueMessage="Please Enter To Date."
                                        ControlExtender="MaskedEditExtender1" ControlToValidate="txtInDate" IsValidEmpty="false"
                                        InvalidValueMessage="In Date  is invalid" Display="None" TooltipMessage="Input a In Date"
                                        ErrorMessage="Please Select In Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                        ValidationGroup="search" SetFocusOnError="true" />
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic ">
                                    <label>Status </label>
                                </div>
                                <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="3" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="True">
                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                    <asp:ListItem Value="A" Text="Approve"></asp:ListItem>
                                    <asp:ListItem Value="R" Text="Reject"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Pending"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RfvStatus" runat="server" ControlToValidate="ddlStatus"
                                    Display="None" ValidationGroup="Show"
                                    InitialValue="0" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="row">
                                
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="30%" OnClick="btnSearch_Click" ValidationGroup="search"
                                    CssClass="btn btn-info" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" Width="30%" OnClick="btnBack_Click"
                                    CssClass="btn btn-danger" />
                                <asp:ValidationSummary ID="search" DisplayMode="List" runat="server" ValidationGroup="search" ShowMessageBox="true" ShowSummary="false" />
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                
                            </div>
                        </div>
                    </div>
                                <div class="col-12">
                <asp:ListView ID="lvRequests" runat="server">
                    <LayoutTemplate>
                        <div id="demo-grid">
                            <div class="sub-heading">
                                <h5>Requests List</h5>
                            </div>
                            <table class="table table-striped table-bordered display dt-responsive " style="width: 100%">
                                <thead class="bg-light-blue">
                                    <tr>
                                        <th style="width: 2%;">
                                            <%--<asp:CheckBox ID="chkAll" runat="server" onclick="return SelectAll(this);" />--%>
                                            SELECT</th>
                                        <th style="width: 10%;">STUDENT NAME</th>
                                        <th style="width: 5%;">PURPOSE</th>
                                        <th style="width: 5%;">OUT DATE</th>
                                        <th style="width: 5%;">IN DATE</th>
                                        <th style="width: 5%;">REQUESTED DATE</th>
                                        <th style="width: 7%;">FINAL STATUS</th>
                                        <th style="width: 7%;">CONFIRM WITH PARENTS</th>
                                        <th style="width:6%;">NEXT APPROVAL</th>
                                        <%--<th style="width:5%;">COMMENT</th>--%>
                                        <th style="width: 5%;">GATE PASS</th>
                                        <th style="width: 5%;">More Details</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server" />
                                </tbody>
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkApprove" runat="server" OnCheckedChanged="chkApprove_CheckedChanged" AutoPostBack="true"  />
                            </td>
                            <td>
                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("REGNO")+" - "+Eval("STUDNAME") %>'></asp:Label>
                                <asp:HiddenField ID="hdnidno" runat="server" Value='<%# Eval("IDNO") %>' />
                                <asp:HiddenField ID="hdnhgpid" runat="server" Value='<%# Eval("HGP_ID") %>' />
                            </td>
                            <td>
                                <%# Eval("PURPOSE_NAME") %>
                            </td>
                            <td>
                                <%# Eval("OUT_TIME","{0:dd/MM/yyyy hh:mm tt}") %>
                            </td>
                            <td>
                                <%# Eval("IN_TIME","{0:dd/MM/yyyy hh:mm tt}") %>
                            </td>
                            <td>
                                <%# Eval("APPLY_DATE","{0:dd/MM/yyyy hh:mm tt}") %>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Value="P">Pending</asp:ListItem>
                                    <asp:ListItem Value="A">Approved</asp:ListItem>
                                    <asp:ListItem Value="R">Reject</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="HdnStatus" runat="server" Value='<%# Eval("FINAL_STATUS") %>' />

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlparentapproval" runat="server">
                                    <asp:ListItem Value=" ">Pending</asp:ListItem>
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="HdnFirstApproval" runat="server" Value='<%# Eval("FIRST_APPROVAL_STATUS") %>' />
                            </td>
                            <td>
                                <%# Eval("NEXT_APPROVAL") %>
                            </td>
                            <%--<td>
                    <textarea id="txtComment" runat="server" wrap="soft"></textarea>
                </td>--%>
                            <td>
                                <asp:Label ID="lblGatepassnno" runat="server" Text='<%# (Eval("HOSTEL_GATE_PASS_NO").ToString())=="" ? "..." : Eval("HOSTEL_GATE_PASS_NO") %>'></asp:Label>   
                            </td>
                            <td>
                                <div class="dropdown">
                                    <asp:Button ID="btnmore" runat="server" class="btn dropdown-toggle" data-toggle="dropdown" Text="More" CommandArgument='<%# Eval("IDNO") %>' />
                                    <div class="dropdown-menu">
                                        <a id="btnViewDetails" class="dropdown-item" style="font-size: 10px" data-toggle="modal" data-target="#myModal3" onclick='<%# "return fetchDataFromServer(" + Eval("IDNO") + "," + Eval("HGP_ID") + ");" %>'>View Details</a>
                                        <a id="btnInmate" class="dropdown-item" style="font-size: 10px" data-toggle="modal" data-target="#myModal1" onclick='<%# "return fetchInMateData(" + Eval("IDNO") + ");" %>'>Contact Details</a>

                                    </div>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                <div class="text-center" >
                    <asp:Button ID="btnParentSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" Visible="false" OnClick="btnParentSubmit_Click"  />
                </div>
            </div>

                </div>

            </div>
        </div>
    </div>
        <div class="modal fade" id="myModal3" data-backdrop="static">
            <div class="modal-dialog custom-modal-dialog">
                <!-- Add the custom class 'custom-modal-dialog' -->
                <div class="modal-content">

                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title">Hostel Gate Pass Request List Details.</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div id="MoreDetails" class="modal-body MoreDetails">
                    </div>

                    <!-- Modal footer -->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    </div>

                </div>
            </div>
        </div>
    <div class="modal fade" id="myModal1" data-backdrop="static">
        <div class="modal-dialog custom-modal-dialog">
            <!-- Add the custom class 'custom-modal-dialog' -->
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Parents/Guardian Contact Details</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div id="Inmatedetails" class="modal-body Inmatedetails">
                </div>

                <!-- Modal footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>

            </div>
        </div>
    </div>

    <%--<div class="modal fade" id="myModal2" data-backdrop="static">
    <div class="modal-dialog custom-modal-dialog"> <!-- Add the custom class 'custom-modal-dialog' -->
        <div class="modal-content">

            <!-- Modal Header -->
            <div class="modal-header">
                <h4 class="modal-title">Attach Screen Shot</h4>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>

            <!-- Modal body -->
            <div class="modal-body">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Uploaded File Name</th>
                            <th>Upload File</th>
                            <th>File Name </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td id="uploadedFileName">No file uploaded</td>
                            <td>
                                <asp:FileUpload ID="fileInput" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtFilename" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <!-- Modal footer -->
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary"  onclick='<%# "return UploadFile(" + Eval("IDNO") + "," + Eval("HGP_ID") + ");" %>'>Upload</button>
            </div>

        </div>
    </div>
</div>--%>

    <div id="divMsg" runat="server">
    </div>
    
    <script type="text/javascript" language="javascript">
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
    <script type="text/javascript">
        function fetchDataFromServer(idno, hgpid) {
            var ObjHReq = {
                Idno: idno,
                Hgpid: hgpid
            };

            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("~/HOSTEL/GATEPASS/HostelInOutRequests.aspx/GetData") %>',
            data: JSON.stringify({ ObjHReq: ObjHReq }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                function cDate(jSonDate) {

                    if (jSonDate == null) {
                        var date = "";
                    }
                    else {
                        var dateString = jSonDate.substr(6);
                        var currentTime = new Date(parseInt(dateString));
                        var month = ('0' + (currentTime.getMonth() + 1)).slice(-2);
                        var day = ('0' + (currentTime.getDate())).slice(-2);
                        var year = currentTime.getFullYear();
                        var date = day + "/" + month + "/" + year;
                    }
                    return date;
                };


                var html = "<p style='font-size:15px;'><b>Request Details</b></p><table class='table table-striped table-bordered'><thead><tr><th>Apply Date</th><th>Remark</th><th>Mode</th><th>Infrom to</th></tr></thead><tbody>";

                for (var i = 0; i < data.d.length; i++) {
                    var formattedDate = cDate(data.d[i].Applydate);
                    var firstappdate = cDate(data.d[i].Firstapprovaldate);
                    var secondappdate = cDate(data.d[i].Secondapprovaldate);
                    var thirdappdate = cDate(data.d[i].Thirdapprovaldate);
                    var fourthappdate = cDate(data.d[i].Fourthapprovaldate);
                    var firstApprovalStatus = data.d[i].Firstrapprovalstatus;
                    var SecondApprovalStatus = data.d[i].Secondapprovalstatus;
                    var ThirdApprovalStatus = data.d[i].Thirdapprovalstatus;
                    var FourthApprovalStatus = data.d[i].Fourthapprovalstatus;

                    // Determine the text color based on the status  Cond Added By Himanshu Tmk 12/03/2024
                    var ftextColor = (firstApprovalStatus === 'Pending') ? 'red' : (firstApprovalStatus === 'Approved') ? 'green' : (firstApprovalStatus === 'Direct Approved By Admin') ? 'green' : 'red';
                    var stextColor = (SecondApprovalStatus === 'Pending') ? 'red' : (SecondApprovalStatus === 'Approved') ? 'green' : (firstApprovalStatus === 'Direct Approved By Admin') ? 'green' : 'red';;
                    var ttextColor = (ThirdApprovalStatus === 'Pending') ? 'red' : (ThirdApprovalStatus === 'Approved') ? 'green' : (firstApprovalStatus === 'Direct Approved By Admin') ? 'green' : 'red';
                    var fotextColor = (FourthApprovalStatus === 'Pending') ? 'red' : (FourthApprovalStatus === 'Approved') ? 'green' : (firstApprovalStatus === 'Direct Approved By Admin') ? 'green' : 'red';



                    html += "<tr>";
                    html += "<td><label>" + formattedDate + "</label></td>";
                    html += "<td><label>" + data.d[i].Remarks + "</label></td>";
                    html += "<td><label>Online</label></td>";
                    html += "<td><label>" + data.d[i].Infromto + "</label></td>";
                    html += "</tr>";
                    html += "</tbody></table>";
                    html += "</br></br><p style='font-size:15px;'><b>Approval Details</b></p><table class='table table-striped table-bordered'><thead><tr><th>Type</th><th>Approver Name</th><th>Designation</th><th>Approve Date</th><th>Status</th></tr></thead><tbody>";
                    //html += "<tr><td><label>First Approval</label></td><td><label>" + data.d[i].Firstapprovername + "</label></td><td><label>" + data.d[i].Firstapprovaldesignation + "</label></td><td><label>" + firstappdate + "</label></td><td><label style=''color :" + ftextColor + "'>" + data.d[i].Firstrapprovalstatus + "</label></td></tr>";
                    //html += "<tr><td><label>Second Approval</label></td><td><label>" + data.d[i].Secondapprovername + "</label></td><td><label>" + data.d[i].Secondapprovaldedignation + "</label></td><td><label>" + secondappdate + "</label></td><td><label style='color: " + stextColor + "'>" + data.d[i].Secondapprovalstatus + "</label></td></tr>";
                    //html += "<tr><td><label>Third Approval</label></td><td><label>" + data.d[i].Thirdapprovarname + "</label></td><td><label>" + data.d[i].Thirdapprovaldesignation + "</label></td><td><label>" + thirdappdate + "</label></td><td><label style='color: " + ttextColor + "'>" + data.d[i].Thirdapprovalstatus + "</label></td></tr>";
                    //html += "<tr><td><label>Fourth Approval</label></td><td><label>" + data.d[i].Fourthapprovarname + "</label></td><td><label>" + data.d[i].FourthApprovalDesignation + "</label></td><td><label>" + fourthappdate + "</label></td><td><label style='color: " + fotextColor + "'>" + data.d[i].Fourthapprovalstatus + "</label></td></tr>";
                    html += "<tr><td><label>First Approval</label></td><td><label>" + data.d[i].Firstapprovername + "</label></td><td><label>" + data.d[i].Firstapprovaldesignation + "</label></td><td><label>" + firstappdate + "</label></td><td><label style='color: " + ftextColor + "'>" + data.d[i].Firstrapprovalstatus + "</label></td></tr>";
                    html += "<tr><td><label>Second Approval</label></td><td><label>" + data.d[i].Secondapprovername + "</label></td><td><label>" + data.d[i].Secondapprovaldedignation + "</label></td><td><label>" + secondappdate + "</label></td><td><label style='color: " + stextColor + "'>" + data.d[i].Secondapprovalstatus + "</label></td></tr>";
                    html += "<tr><td><label>Third Approval</label></td><td><label>" + data.d[i].Thirdapprovarname + "</label></td><td><label>" + data.d[i].Thirdapprovaldesignation + "</label></td><td><label>" + thirdappdate + "</label></td><td><label style='color: " + ttextColor + "'>" + data.d[i].Thirdapprovalstatus + "</label></td></tr>";
                    html += "<tr><td><label>Fourth Approval</label></td><td><label>" + data.d[i].Fourthapprovarname + "</label></td><td><label>" + data.d[i].FourthApprovalDesignation + "</label></td><td><label>" + fourthappdate + "</label></td><td><label style='color: " + fotextColor + "'>" + data.d[i].Fourthapprovalstatus + "</label></td></tr>";

                    html += "</tbody></table>";
                }




                // Update the inner HTML of the "MoreDetails" element
                document.getElementById("MoreDetails").innerHTML = html;
            },
            error: function (xhr, status, error) {
                // Handle errors, e.g., display an error message
                console.log(error);
            }
        });
    }
    </script>
    <script type="text/javascript">
        function fetchInMateData(idno) {
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("~/HOSTEL/GATEPASS/HostelInOutRequests.aspx/GetInMate") %>',
            data: JSON.stringify({ idno: idno }), // Pass the idno as an object
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var html = "<table class='table table-striped table-bordered'><thead><tr><th>Type</th><th>Name</th><th>Contact</th><th>Email</th></tr></thead><tbody>";
                for (var i = 0; i < data.d.length; i++) {
                    // Helper function to handle undefined data
                    function formatData(value) {
                        return value == undefined ? '-' : value;
                    }

                    html += "<tr><td><label>Student</label></td><td><label>" + formatData(data.d[i].Studentname) + "</label></td><td><label>" + formatData(data.d[i].Studentmobile) + "</label></td><td><label>" + formatData(data.d[i].Studentemail) + "</label></td></tr>";
                    html += "<tr><td><label>Father</label></td><td><label>" + formatData(data.d[i].Fathername) + "</label></td><td><label>" + formatData(data.d[i].Fathermobile) + "</label></td><td><label>" + formatData(data.d[i].Fatheremail) + "</label></td></tr>";
                    html += "<tr><td><label>Mother</label></td><td><label>" + formatData(data.d[i].MotherName) + "</label></td><td><label>" + formatData(data.d[i].Mothermobile) + "</label></td><td><label>" + formatData(data.d[i].Motheremail) + "</label></td></tr>";
                }
                // Update the inner HTML of the "Inmatedetails" element
                document.getElementById("Inmatedetails").innerHTML = html;
            },
            error: function (xhr, status, error) {
                // Handle errors, e.g., display an error message
                console.log(error);
            }
        });
    }
    </script>
    <%--<%--<script type="text/javascript">
    $(document).ready(function () {
        $("#uploadButton").click(function () {
            var fileInput = document.getElementById("imageFile");
            var imagePathInput = document.getElementById("imagePath");
            var imageNameInput = document.getElementById("imageName");

            if (fileInput.files.length > 0) {
                var file = fileInput.files[0];

                if (file.size <= 5 * 1024 * 1024 && /\.(jpg|jpeg)$/i.test(file.name)) {
                    var formData = new FormData();
                    formData.append("imageFile", file);

                    $.ajax({
                        url: '<%= ResolveUrl("~/HOSTEL/GATEPASS/HostelInOutRequests.aspx/AttachedDocuments") %>', // Your server-side handler
                        type: "POST",
                        data: formData,
                        contentType: false,
                        processData: false,
                        success: function (data) {
                            if (data.startsWith("success")) {
                                var fileData = data.split(":");
                                var imagePath = fileData[1];
                                var imageName = fileData[2];

                                imagePathInput.value = imagePath;
                                imageNameInput.value = imageName;

                                // Save image path and name to the database using another AJAX request
                                saveImageData(imagePath, imageName);
                            } else {
                                $("#message").text("Error: " + data);
                            }
                        }
                    });
                } else {
                    $("#message").text("Invalid file format or file size exceeds 5MB.");
                }
            } else {
                $("#message").text("Please select an image to upload.");
            }
        });

        function saveImageData(imagePath, imageName) {
            // Perform another AJAX request to save the image path and name to the database
            // Customize this part based on your database structure
            $.ajax({
                type: "POST",
                url: "SaveImageData.ashx", // Your server-side handler for saving data
                data: { imagePath: imagePath, imageName: imageName },
                success: function (response) {
                    if (response === "success") {
                        $("#message").text("Image uploaded and data saved.");
                    } else {
                        $("#message").text("Error saving data: " + response);
                    }
                },
                error: function (xhr, status, error) {
                    // Handle errors
                    $("#message").text("Error saving data: " + error);
                }
            });
        }
    });
</script>--%>
    <script type="text/javascript">
        $(function () {
            var reader = new FileReader();
            var fileName;
            var contentType;
            $('#fileInput').change(function () {
                if (typeof (FileReader) != "undefined") {
                    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.png)$/;
                    $($(this)[0].files).each(function () {
                        var file = $(this);
                        if (regex.test(file[0].name.toLowerCase())) {
                            fileName = file[0].name;
                            contentType = file[0].type;
                            reader.readAsDataURL(file[0]);
                        } else {
                            alert(file[0].name + " is not a valid image file.");
                            return false;
                        }
                    });
                } else {
                    alert("This browser does not support HTML5 FileReader.");
                }
            });

            function UploadFile(Idno, Hgpid) {
                var byteData = reader.result;
                byteData = byteData.split(';')[1].replace("base64,", "");
                var obj = {};
                obj.idno = Idno;
                obj.hgpid = Hgpid;
                obj.Data = byteData;
                obj.Name = fileName;
                $.ajax({
                    type: "POST",
                    url: '<%= ResolveUrl("~/HOSTEL/GATEPASS/HostelInOutRequests.aspx/SaveImage") %>',
                data: '{data : ' + JSON.stringify(obj) + ' }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    alert(r.d);
                },
                error: function (r) {
                    alert(r.responseText);
                },
                failure: function (r) {
                    alert(r.responseText);
                }
            });
            return false;
        };
    });
    </script>



    <script src="https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/responsive/2.2.0/js/dataTables.responsive.min.js"></script>
    <script src="https://cdn.datatables.net/responsive/2.2.0/js/responsive.bootstrap.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>

</asp:Content>

