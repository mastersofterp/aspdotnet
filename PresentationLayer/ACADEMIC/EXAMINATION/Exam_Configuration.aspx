<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Exam_Configuration.aspx.cs" Inherits="Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .show-error .switch label {
            background: #3c8dbc;
        }
        .show-error .switch label:hover {
            background: #3c8dbc;
        }
        .switch.Size label {
            width: 120px !important;
        }
        .switch.Size input:checked + label:after {
            transform: translateX(106px);
        }
        #ctl00_ContentPlaceHolder1_divpop {
            display: none;
        }
        .tab-inp .select2-container--default {
            width: 200px !important;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Configuration Details</h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1">College Deatils</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2">Login Details</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3">Facilities</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_4">Table Status</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_5">Mark Entry</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_6">Other</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="col-lg-10 col-md-9 col-12">
                                            <div class="col-12 p-0">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>College Code</label>
                                                        </div>
                                                        <input type="text" class="form-control">
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Govt.</label>
                                                        </div>
                                                        <input type="text" class="form-control">
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                                        <div class="label-dynamic">
                                                            <label>College Name</label>
                                                        </div>
                                                        <input type="text" class="form-control">
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                                        <div class="label-dynamic">
                                                            <label>College Addres</label>
                                                        </div>
                                                        <textarea class="form-control" rows="2"></textarea>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Phone</label>
                                                        </div>
                                                        <input type="text" class="form-control">
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>E-Mail</label>
                                                        </div>
                                                        <input type="text" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group box-profile col-lg-2 col-md-3 col-12 pl-sm-0">
                                            <div class="label-dynamic">
                                                <label>College Logo</label>
                                            </div>
                                            <img src="Images/nophoto.jpg" alt="clg-logo" style="height: 100px; width: 90px; border: 1px solid #d2d6de;" />
                                            <input type="file" class="mt-1" name="filename2">
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Financial Year From</label>
                                            </div>
                                            <input type="date" class="form-control">
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Financial Year To</label>
                                            </div>
                                            <input type="date" class="form-control">
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Define Late Fee(Institute Fee)</label>
                                            </div>
                                            <input type="text" class="form-control">
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Faculty User Type</label>
                                            </div>
                                            <input type="text" class="form-control">
                                        </div>
                                        <div class="col-12 text-center mt-3">
                                            <button type="button" class="btn btn-primary">Submit</button>
                                            <button type="button" class="btn btn-warning">Cancel</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="tab_2">
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12 show-error">
                                            <div class="label-dynamic">
                                                <label>Login Options</label>
                                            </div>
                                            <div class="switch form-inline Size">
                                                <input type="checkbox" id="Checkbox1" name="switch" checked />
                                                <label data-on="Captcha Based" data-off="OTP Based" for="Checkbox1"></label>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Login Failure Attempt</label>
                                            </div>
                                            <input type="text" class="form-control">
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Allow Logout Popup</label>
                                            </div>
                                            <select class="form-control" data-select2-enable="true">
                                                <option>Please Select</option>
                                                <option>Yes</option>
                                                <option>No</option>
                                            </select>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Popup Duration(in Seconds)</label>
                                            </div>
                                            <input type="text" class="form-control" placeholder="10">
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Active msg after Login</label>
                                            </div>
                                            <asp:CheckBox ID="chkpopup" runat="server" />
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divpop" runat="server">
                                            <div class="label-dynamic">
                                                <label>Active POPUP After Login</label>
                                            </div>
                                            <textarea class="form-control" rows="2"></textarea>
                                        </div>
                                        <div class="col-12 mt-3 text-center">
                                            <button type="button" class="btn btn-primary">Submit</button>
                                            <button type="button" class="btn btn-warning">Cancel</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="tab_3">
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Communication Facility</label>
                                            </div>
                                            <div class="form-check-inline">
                                                <label class="form-check-label">
                                                    <input type="radio" class="form-check-input" name="a">EMAIL
                                                </label>
                                            </div>
                                            <div class="form-check-inline">
                                                <label class="form-check-label">
                                                    <input type="radio" class="form-check-input" name="a">SMS
                                                </label>
                                            </div>
                                            <div class="form-check-inline">
                                                <label class="form-check-label">
                                                    <input type="radio" class="form-check-input" name="a" checked>BOTH( EMAIL &amp; SMS)
                                                </label>
                                            </div>
                                            <div class="form-check-inline">
                                                <label class="form-check-label">
                                                    <input type="radio" class="form-check-input" name="a">None
                                                </label>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Email Service ID</label>
                                            </div>
                                            <input type="text" class="form-control">
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>PASSWORD</label>
                                            </div>
                                            <input type="text" class="form-control">
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>SMS Service ID</label>
                                            </div>
                                            <input type="text" class="form-control">
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>PASSWORD</label>
                                            </div>
                                            <input type="text" class="form-control">
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>User Profile Sender Name</label>
                                            </div>
                                            <input type="text" class="form-control">
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>User Profile Subject</label>
                                            </div>
                                            <input type="text" class="form-control">
                                        </div>
                                        <div class="col-12 mt-3 text-center">
                                            <button type="button" class="btn btn-primary">Submit</button>
                                            <button type="button" class="btn btn-warning">Cancel</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="tab_4">
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12 show-error">
                                            <div class="label-dynamic">
                                                <label>Time Table Status</label>
                                            </div>
                                            <div class="switch form-inline Size">
                                                <input type="checkbox" id="rdTableStatus" name="switch" checked />
                                                <label data-on="Horizontal" data-off="Vertical" for="rdTableStatus"></label>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Course Registration before Time Table</label>
                                            </div>
                                            <asp:CheckBox ID="chkCRBTimeTable" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-12 mb-2">
                                            <div class="card">
                                                <div class="card-header"><b>Data Table Button Settings</b></div>
                                                <div class="card-body">
                                                    <div class="form-check-inline">
                                                        <label class="form-check-label">
                                                            <input type="checkbox" class="form-check-input" value="" checked>Copy
                                                        </label>
                                                    </div>
                                                    <div class="form-check-inline">
                                                        <label class="form-check-label">
                                                            <input type="checkbox" class="form-check-input" value="" checked>Excel
                                                        </label>
                                                    </div>
                                                    <div class="form-check-inline">
                                                        <label class="form-check-label">
                                                            <input type="checkbox" class="form-check-input" value="" checked>Pdf
                                                        </label>
                                                    </div>
                                                    <div class="form-check-inline">
                                                        <label class="form-check-label">
                                                            <input type="checkbox" class="form-check-input" value="">Print
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-12 mb-2">
                                            <div class="card">
                                                <div class="card-header"><b>Department Filter</b></div>
                                                <div class="card-body" style="padding: 8px 15px;">
                                                    <div class="form-check-inline">
                                                        <label class="form-check-label">
                                                            <input type="radio" class="form-check-input" name="df">Yes
                                                        </label>
                                                    </div>
                                                    <div class="form-check-inline">
                                                        <label class="form-check-label">
                                                            <input type="radio" class="form-check-input" name="df" checked>No
                                                        </label>
                                                    </div>
                                                    <div class="form-check-inline tab-inp">
                                                        <select class="form-control" data-select2-enable="true" id="sel1" style="width: 200px !important;">
                                                            <option>None Selected</option>
                                                            <option>Option 1</option>
                                                            <option>Option 2</option>
                                                            <option>Option 3</option>
                                                            <option>Option 4</option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-3 text-center">
                                        <button type="button" class="btn btn-primary">Submit</button>
                                        <button type="button" class="btn btn-warning">Cancel</button>
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="tab_5">
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>IA Consolidated Marks (<span style="color: red">With Average</span>)</label>
                                            </div>
                                            <asp:TextBox ID="txtIAMarks" runat="server" CssClass="form-control" MaxLength="1"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>PCA Consolidated Marks (<span style="color: red">With Average</span>)</label>
                                            </div>
                                            <asp:TextBox ID="txtPCAMarks" runat="server" CssClass="form-control" MaxLength="1"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Admin Level Marks Entry </label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdminLevelMarksEntry" TabIndex="2" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Please Select Admin Level Marks Entry">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Update Old Exam Data Migration </label>
                                            </div>
                                            <asp:DropDownList ID="ddlUpdMigrationExamData" TabIndex="3" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Please Select Update Old Exam Data Migration">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <b><asp:Label ID="lblDecodeNumOrEnrollNo" Text="End Sem by Enrollment No. / Roll No. Wise" runat="server"></asp:Label></b><br />
                                            <asp:CheckBox ID="chkDecodeNumOrEnrollNo" Text="" onclick="chkEndSembyEnrollOrDecode(this)" runat="server" />
                                        </div>
                                        <div class="col-12 mt-3 text-center">
                                            <button type="button" class="btn btn-primary">Submit</button>
                                            <button type="button" class="btn btn-warning">Cancel</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="tab_6">
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Enrollment/ Registration No.</label>
                                            </div>
                                            <div class="form-check-inline">
                                                <label class="form-check-label">
                                                    <input type="checkbox" class="form-check-input" value="" checked>Manual Enrollment/Registration No. of Student
                                                </label>
                                            </div>
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12 show-error">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Enrollment/ Registration No.</label>
                                            </div>
                                            <div class="switch form-inline Size">
                                                <input type="checkbox" id="rdEnrollment" name="switch" checked />
                                                <label data-on="Manual" data-off="Automatic" for="rdEnrollment"></label>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-2 col-md-4 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Reset Counter</label>
                                            </div>
                                            <div class="switch form-inline">
                                                <input type="checkbox" id="chkResetCounter" name="switch" checked />
                                                <label data-on="Yes" data-off="No" for="chkResetCounter"></label>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-4 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Feedback Compulsory for fees</label>
                                            </div>
                                            <div class="switch form-inline">
                                                <input type="checkbox" id="rdFeedback" name="switch" checked />
                                                <label data-on="Yes" data-off="No" for="rdFeedback"></label>
                                            </div>
                                        </div>
                                        
                                        <div class="form-group col-lg-3 col-md-4 col-12">
                                            <div class="label-dynamic">
                                                <label>Active Multiple College</label>
                                            </div>
                                            <div class="form-check-inline">
                                                <label class="form-check-label">
                                                    <input type="checkbox" class="form-check-input" value="" checked>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Back Days Allow for Attendence</label>
                                            </div>
                                            <input type="text" class="form-control">
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Receipt Cancelation</label>
                                            </div>
                                            <asp:DropDownList ID="ddlReceiptCancel" TabIndex="3" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Please Select Update Old Exam Data Migration">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-12 mt-3 text-center">
                                            <button type="button" class="btn btn-primary">Submit</button>
                                            <button type="button" class="btn btn-warning">Cancel</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            $("#ctl00_ContentPlaceHolder1_chkpopup").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_divpop").show();
                } else {
                    $("#ctl00_ContentPlaceHolder1_divpop").hide();
                }
            });
        });

        function chkEndSembyEnrollOrDecode(chk) {
            if (chk.checked == true) {
                //  lbl.value = chk.checked ? "End Sem by Decode No. Wise" : "End Sem by Enrollment No. / Roll No. Wise";
                $('#<%= lblDecodeNumOrEnrollNo.ClientID %>').text("End Sem by Decode No. Wise");
            } else {
                $('#<%= lblDecodeNumOrEnrollNo.ClientID %>').text("End Sem Mark Entry Enrollment No. / Roll No. Wise");
                //document.getElementById('ctl00_ContentPlaceHolder1_lblDecodeNumOrEnrollNo').value = "";
            }
        }
    </script>
</asp:Content>

