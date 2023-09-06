<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AdmissionCancellation.aspx.cs" Inherits="Academic_AdmissionCancellation" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_lvStudent_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <script type="text/javascript" charset="utf-8">
        //$(document).ready(function () {

        //    $(".display").dataTable({
        //        "bJQueryUI": true,
        //        "sPaginationType": "full_numbers"
        //    });

        //});

        function RunThisAfterEachAsyncPostback() {
            $(function () {
                $("#<%=txtFromDate.ClientID%>").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy',
                    yearRange: '1975:' + getCurrentYear()
                });
            });

            function getCurrentYear() {
                var cDate = new Date();
                return cDate.getFullYear();
            }
        }

    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist" style="display: none;">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tabLC" tabindex="1">ADMISSION CANCELLATION</a>
                            </li>
                            <li class="nav-item" style="display: none">
                                <a class="nav-link" data-toggle="tab" href="#tabBC" tabindex="2">RE-ADMISSION</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tabLC">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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

                                <asp:UpdatePanel ID="pnlFeeTable" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>
                                                            <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none" style="display: none">
                                                    <div class="label-dynamic">
                                                        <label></label>
                                                    </div>
                                                    <asp:RadioButton ID="rdoStudName" runat="server" OnCheckedChanged="rdoStudName_CheckedChanged" AutoPostBack="true" Text="Name" GroupName="search"
                                                        TabIndex="2" />
                                                    <asp:RadioButton ID="rdoRegno" runat="server" OnCheckedChanged="rdoRegno_CheckedChanged" AutoPostBack="true" Text="Reg No." GroupName="search"
                                                        Checked="true" TabIndex="1" />
                                                </div>
                                                <div class="col-12">

                                                    <%--Search Pannel Start by Swapnil --%>
                                                    <div id="myModal2" role="dialog" runat="server">
                                                        <div>
                                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updEdit"
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

                                                        <asp:UpdatePanel ID="updEdit" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-12">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <label>Search Criteria</label>
                                                                            </div>

                                                                            <%--onchange=" return ddlSearch_change();"--%>
                                                                            <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <%-- <asp:ListItem>Please Select</asp:ListItem>
                                                        <asp:ListItem>BRANCH</asp:ListItem>
                                                        <asp:ListItem>ENROLLMENT NUMBER</asp:ListItem>
                                                        <asp:ListItem>REGISTRATION NUMBER</asp:ListItem>
                                                        <asp:ListItem>FatherName</asp:ListItem>
                                                        <asp:ListItem>IDNO</asp:ListItem>
                                                        <asp:ListItem>MOBILE NUMBER</asp:ListItem>
                                                        <asp:ListItem>MotherName</asp:ListItem>
                                                        <asp:ListItem>NAME</asp:ListItem>
                                                        <asp:ListItem>ROLLNO</asp:ListItem>
                                                        <asp:ListItem>SEMESTER</asp:ListItem>--%>
                                                                            </asp:DropDownList>

                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">


                                                                            <asp:Panel ID="pnltextbox" runat="server">
                                                                                <div id="divtxt" runat="server" style="display: block">
                                                                                    <div class="label-dynamic">
                                                                                        <label>Search String</label>
                                                                                    </div>
                                                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                                                </div>
                                                                            </asp:Panel>

                                                                            <asp:Panel ID="pnlDropdown" runat="server">
                                                                                <div id="divDropDown" runat="server" style="display: block">
                                                                                    <div class="label-dynamic">
                                                                                        <%-- <label id="lblDropdown"></label>--%>
                                                                                        <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                                                    </div>
                                                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </asp:Panel>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-12 btn-footer">
                                                                        <%-- OnClientClick="return submitPopup(this.name);"--%>
                                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />
                                                                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                                                    </div>
                                                                    <div class="col-12 btn-footer">
                                                                        <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                                                    </div>

                                                                    <div class="col-12">
                                                                        <asp:Panel ID="pnlLV" runat="server">
                                                                            <asp:ListView ID="lvStudent" runat="server">
                                                                                <LayoutTemplate>
                                                                                    <div>
                                                                                        <div class="sub-heading">
                                                                                            <h5>Student List</h5>
                                                                                        </div>
                                                                                        <asp:Panel ID="Panel2" runat="server">
                                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                                                <thead class="bg-light-blue">
                                                                                                    <tr>
                                                                                                        <th>Name
                                                                                                        </th>
                                                                                                        <th style="display: none">IdNo
                                                                                                        </th>
                                                                                                        <th>
                                                                                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                                        </th>
                                                                                                        <th>
                                                                                                            <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                                        </th>
                                                                                                        <th>
                                                                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                                                        </th>
                                                                                                        <th>Father Name
                                                                                                        </th>
                                                                                                        <th>Mother Name
                                                                                                        </th>
                                                                                                        <th>Mobile No.
                                                                                                        </th>
                                                                                                    </tr>
                                                                                                </thead>
                                                                                                <tbody>
                                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </div>

                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                                                OnClick="lnkId_Click"></asp:LinkButton>
                                                                                        </td>
                                                                                        <td style="display: none">
                                                                                            <%# Eval("idno")%>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("SEMESTERNO")%>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("FATHERNAME") %>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("MOTHERNAME") %>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%#Eval("STUDENTMOBILE") %>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </asp:Panel>
                                                                    </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <%--Search Pannel End--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Reg No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="form-control"
                                                        ToolTip="Enter text to search." TabIndex="1" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <label></label>
                                                    </div>
                                                    <asp:Button ID="btnSearch2" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                        ValidationGroup="search" TabIndex="4" CssClass="btn btn-primary" />
                                                    <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtSearchText"
                                                        Display="None" ErrorMessage="Please enter Reg No." SetFocusOnError="true"
                                                        ValidationGroup="search" />
                                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="search" />
                                                </div>

                                                <div class="col-lg-3 col-md-6 col-12 mt-3" id="divTotalAmount" runat="server" visible="false">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Total Amount :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblAmtTotal" runat="server" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12 mt-3" id="divPaidAmount" runat="server" visible="false">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Amount Paid :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblAmtPaids" runat="server" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>


                                            <div class="col-md-12 table table-responsive">
                                                <asp:ListView ID="lvSearchResults" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Student Details</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Select</th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label></th>
                                                                    <th>Name</th>
                                                                    <th>DOB</th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label></th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label></th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label></th>
                                                                    <th>Admission Date</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <EmptyDataTemplate></EmptyDataTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="rdoSelectRecord" runat="server" ToolTip='<%# Eval("IDNO") %>' value='<%# Eval("IDNO") %>' OnCheckedChanged="rdoSelectRecord_CheckedChanged1" />
                                                                <%--<asp:RadioButton ID="rdoSelectRecord" runat="server" value='<%# Eval("IDNO") %>'/>--%>
                                                                <%--<input id="rdoSelectRecord" name="Student" type="radio" value='<%# Eval("IDNO") %>' />--%></td>
                                                            <td><%# Eval("REGNO")%>
                                                                <asp:Label ID="lblRegno" runat="server" Text='<%# Eval("REGNO")%>' ToolTip='<%# Eval("NAME") %>' Visible="false"></asp:Label></td>
                                                            </td>

                                                            <td><%# Eval("NAME") %></td>
                                                            <td><%# (Eval("DOB").ToString() != string.Empty) ? ((DateTime)Eval("DOB")).ToShortDateString() : "-" %></td>
                                                            <td><%# Eval("DEGREENO")%>
                                                                <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREENO")%>' ToolTip='<%# Eval("SHORTNAME")%>' Visible="false"></asp:Label>
                                                                <asp:HiddenField ID="hdnBranchno" runat="server" Value='<%# Eval("BRANCHNO")%>' />
                                                                <%-- <asp:label ID="lblCollege" runat="server" Text='<%# Eval("COLLEGE_NAME")%>' ToolTip='<%# Eval("COLLEGE_ID")%>' Visible="false"></asp:label>
                                                                --%>  </td>

                                                            <td><%# Eval("SHORTNAME")%></td>
                                                            <td><%# Eval("SEMESTERNAME")%></td>
                                                            <td><%# (Eval("ADMDATE").ToString() != string.Empty) ? ((DateTime)Eval("ADMDATE")).ToShortDateString() : "-" %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>



                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-12 col-12" id="divRemark" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <label>Reason of Cancelling Student Admission : <span style="color: red">(Maximum limit 300 characters)</span></label>
                                                        </div>
                                                        <asp:TextBox ID="txtRemark" Rows="4" TextMode="MultiLine" CssClass="form-control" MaxLength="300"
                                                            runat="server" ValidationGroup="cancel1" />
                                                        <asp:RequiredFieldValidator ID="rfvCancel" runat="server" ControlToValidate="txtRemark" ErrorMessage="Please Enter Remark" InitialValue="0"
                                                            Display="None" SetFocusOnError="true" ValidationGroup="cancel1"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-12 col-12" id="divFileUpd" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <label>Attach File</label>
                                                        </div>
                                                        <asp:FileUpload ID="fuFile" runat="server" ToolTip="Select file to upload" TabIndex="2" />
                                                        <span style="color: red">Note : Upload the Files only with following formats: .PDF and Size should be upto 100kb.</span>
                                                    </div>
                                                </div>


                                                <div class="row">
                                                    <div class="col-md-6" id="divbreakof" runat="server" visible="false">
                                                        <asp:CheckBox ID="chkbreak" runat="server" onclick="chkmonth(this);" />
                                                        <b>Break Of Study  </b>
                                                        &nbsp;
                                                    <asp:CheckBox ID="chkdiscontinue" runat="server" onclick="chkdis(this);" Checked="true" />
                                                        <b>DisContinue  </b>
                                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>
                                                    </div>
                                                    <div class="col-md-12" id="divMonthyear" runat="server" style="display: none">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label><%--<span style="color: red;">*</span>--%> Month</label>
                                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label><%--<span style="color: red;">*</span>--%> Year</label>
                                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnCancelAdmission" runat="server" OnClick="btnCancelAdmission_Click" OnClientClick="return validation();" Text="Cancel Admission" Visible="false" ValidationGroup="cancel1" CssClass="btn btn-warning" />
                                                <%--<asp:Button ID="btnCancelAdmission" runat="server" OnClientClick="CancelAdmission(); return false" Text="Cancel Admission" Visible="false" CssClass="btn btn-primary" />--%>
                                                <asp:Button ID="btnCancelAdmissionSlip" runat="server" OnClick="btnCancelAdmissionSlip_Click" Text="Cancel Admission Slip" Visible="false" Enabled="false" CssClass="btn btn-warning" />
                                                <asp:HiddenField ID="hdcount" runat="server" />
                                                <asp:HiddenField ID="hdrecount" runat="server" />
                                                <asp:ValidationSummary ID="valCancelAdmission" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="cancel1" />
                                            </div>

                                            <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Cancellation Report</h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="trDegree" runat="server" visible="True">
                                                    <div class="label-dynamic">
                                                        <%--<label>Degree</label>--%>
                                                        <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="5"
                                                        ToolTip="Please Select Degree">
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                            Display="None" ErrorMessage="Please Select degree" SetFocusOnError="True" ValidationGroup="Cancel"
                                                            InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="trBranch" runat="server">
                                                    <div class="label-dynamic">
                                                        <%--<label>Branch</label>--%>
                                                        <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAdmBranch" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True" TabIndex="6" ToolTip="Please Select Branch">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="trFrmDate" runat="server" visible="True">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>From Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar text-blue" id="imgFromDate"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"
                                                            ValidationGroup="Cancel" TabIndex="7" ToolTip="Please Select Date"></asp:TextBox>

                                                        <ajaxToolKit:CalendarExtender ID="ceFromdate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            PopupButtonID="imgFromDate" TargetControlID="txtFromDate" />
                                                        <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server"
                                                            TargetControlID="txtFromDate" Mask="99/99/9999" MessageValidatorTip="true"
                                                            MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                            ErrorTooltipEnabled="True" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="trToDate" runat="server" visible="True">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>To Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar text-blue" id="imgToDate"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" ValidationGroup="Cancel"
                                                            TabIndex="9" ToolTip="Please Select Date"></asp:TextBox>

                                                        <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            PopupButtonID="imgToDate" TargetControlID="txtToDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server"
                                                            TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true"
                                                            MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server"
                                                            ControlExtender="meeToDate" ControlToValidate="txtToDate" Display="None"
                                                            EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter To Date"
                                                            InvalidValueBlurredMessage="Invalid Date"
                                                            InvalidValueMessage="To Date is Invalid (Enter mm-dd-yyyy Format)"
                                                            TooltipMessage="Please Enter To Date" ValidationGroup="Cancel" />
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click"
                                                        Text="Report" ValidationGroup="Cancel" TabIndex="11" CssClass="btn btn-info" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                                        OnClick="btnCancel_Click" TabIndex="12" CssClass="btn btn-warning" />
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFromDate"
                                                        Display="None" ErrorMessage="Please enter from date" SetFocusOnError="true"
                                                        ValidationGroup="Cancel" ID="rfvFromDate" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server"
                                                        ControlExtender="meeFromDate" ControlToValidate="txtFromDate" Display="None"
                                                        EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date"
                                                        InvalidValueBlurredMessage="Invalid Date"
                                                        InvalidValueMessage="From Date is Invalid (Enter DD-MM-YYYY Format)"
                                                        SetFocusOnError="True" TooltipMessage="Please Enter From Date"
                                                        ValidationGroup="Cancel" />
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtToDate"
                                                        Display="None" ErrorMessage="Please enter to date" SetFocusOnError="true"
                                                        ValidationGroup="Cancel" Width="10%" ID="rfvToDate" />
                                                    <asp:ValidationSummary ID="ValCancelSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="Cancel" />
                                                </div>
                                            </div>
                                            </div>

                                        </div>
                                        <div id="divMsg" runat="server">
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnCancelAdmission" />
                                        <asp:PostBackTrigger ControlID="btnCancelAdmissionSlip" />
                                        <asp:PostBackTrigger ControlID="btnSearch" />
                                        <asp:PostBackTrigger ControlID="btnReport" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="tabBC" style="display: none">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updReAdmit"
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

                                <asp:UpdatePanel ID="updReAdmit" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>RE-ADMISSION</h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-2 col-md-12 col-12">
                                                    <asp:RadioButton ID="rdoRegno1" runat="server" OnCheckedChanged="rdoRegno1_CheckedChanged" AutoPostBack="true" Text="Reg No." GroupName="search"
                                                        Checked="true" />
                                                    <asp:RadioButton ID="rdoStudName1" runat="server" OnCheckedChanged="rdoStudName1_CheckedChanged" AutoPostBack="true" Text="Name" GroupName="search" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <asp:TextBox ID="txtSearchText1" runat="server" CssClass="form-control"
                                                        ToolTip="Enter text to search." />
                                                </div>

                                                <div class="form-group col-lg-1 col-md-6 col-12">
                                                    <asp:Button ID="btnSearch1" runat="server" Text="Search" OnClick="btnSearch1_Click"
                                                        ValidationGroup="search1" TabIndex="4" CssClass="btn btn-primary" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearchText1"
                                                        Display="None" ErrorMessage="Please Enter Registration No." SetFocusOnError="true"
                                                        ValidationGroup="search1" Width="10%" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="search1" />
                                                </div>

                                                <div class="col-lg-3 col-md-6 col-12" style="display: none">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Total Amount :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblTAmt" runat="server" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Amount Paid :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblAmtPaid" runat="server" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Re-Admission Report</h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="DivDegree1" runat="server" visible="True">
                                                    <div class="label-dynamic">
                                                        <label>Degree</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree1" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlDegree1_SelectedIndexChanged"
                                                        ToolTip="Please Select Degree">
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                        Display="None" ErrorMessage="Please Select degree" SetFocusOnError="True" ValidationGroup="Cancel"
                                                        InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="DivBranch1" runat="server">
                                                    <div class="label-dynamic">
                                                        <label>Branch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranch1" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True" ToolTip="Please Select Branch">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="DivFromDate1" runat="server" visible="True">
                                                    <div class="label-dynamic">
                                                        <label>From Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar text-blue" id="imgFromDate1"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDate1" runat="server" CssClass="form-control"
                                                            ValidationGroup="Readmit" ToolTip="Please Select Date"></asp:TextBox>

                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            PopupButtonID="imgFromDate1" TargetControlID="txtFromDate1" />
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                            TargetControlID="txtFromDate1" Mask="99/99/9999" MessageValidatorTip="true"
                                                            MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                            ErrorTooltipEnabled="True" />
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="DivToDate1" runat="server" visible="True">
                                                    <div class="label-dynamic">
                                                        <label>To Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar text-blue" id="imgToDate1"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtToDate1" runat="server" CssClass="form-control" ValidationGroup="Readmit"
                                                            ToolTip="Please Select Date"></asp:TextBox>

                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            PopupButtonID="imgToDate1" TargetControlID="txtToDate1">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                                            TargetControlID="txtToDate1" Mask="99/99/9999" MessageValidatorTip="true"
                                                            MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                            ControlExtender="meeToDate" ControlToValidate="txtToDate1" Display="None"
                                                            EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter To Date"
                                                            InvalidValueBlurredMessage="Invalid Date"
                                                            InvalidValueMessage="To Date is Invalid (Enter dd-mm-yyyy Format)"
                                                            TooltipMessage="Please Enter To Date" ValidationGroup="Readmit" />
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btbnReport1" runat="server" OnClick="btbnReport1_Click"
                                                        Text="Report" ValidationGroup="Readmit" CssClass="btn btn-info" />
                                                    <asp:Button ID="btnCancel1" runat="server" Text="Cancel"
                                                        OnClick="btnCancel1_Click" CssClass="btn btn-warning" />
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFromDate1"
                                                        Display="None" ErrorMessage="Please Enter From date" SetFocusOnError="true"
                                                        ValidationGroup="Readmit" ID="RequiredFieldValidator2" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                                        ControlExtender="meeFromDate" ControlToValidate="txtFromDate1" Display="None"
                                                        EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date"
                                                        InvalidValueBlurredMessage="Invalid Date"
                                                        InvalidValueMessage="From Date is Invalid (Enter mm-dd-yyyy Format)"
                                                        SetFocusOnError="True" TooltipMessage="Please Enter From Date"
                                                        ValidationGroup="Readmit" />
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtToDate1"
                                                        Display="None" ErrorMessage="Please enter to date" SetFocusOnError="true"
                                                        ValidationGroup="Readmit" ID="RequiredFieldValidator3" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="Readmit" />
                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <asp:UpdatePanel ID="updlv" runat="server">
                                                    <ContentTemplate>
                                                        <asp:ListView ID="lvReAdmit" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Search Results</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResultsforReadmit">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Select</th>
                                                                            <th>Reg No.</th>
                                                                            <th>Name</th>
                                                                            <th>DOB</th>
                                                                            <th>Degree</th>
                                                                            <th>Branch</th>
                                                                            <th>Sem.</th>
                                                                            <th>Admission Date</th>
                                                                            <th>Upload Readmission doc.</th>
                                                                            <th>Upload</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <EmptyDataTemplate></EmptyDataTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <%-- <input id="rdoSelectRecord" name="Student" type="radio" value='<%# Eval("IDNO") %>' /></td>--%>
                                                                        <asp:RadioButton ID="rdoSelectRecord" runat="server" AutoPostBack="false" value='<%# Eval("IDNO") %>' ToolTip='<%# Eval("IDNO") %>' OnCheckedChanged="rdoSelectRecord_CheckedChanged" />

                                                                    <td><%# Eval("REGNO")%></td>
                                                                    <td><%# Eval("NAME") %></td>
                                                                    <td><%# (Eval("DOB").ToString() != string.Empty) ? ((DateTime)Eval("DOB")).ToShortDateString() : "-" %></td>
                                                                    <td><%# Eval("DEGREENO")%></td>
                                                                    <td><%# Eval("SHORTNAME")%></td>
                                                                    <td><%# Eval("SEMESTERNAME")%></td>
                                                                    <td><%# (Eval("ADMDATE").ToString() != string.Empty) ? ((DateTime)Eval("ADMDATE")).ToShortDateString() : "-" %></td>
                                                                    <td>
                                                                        <asp:FileUpload ID="fuFile" runat="server" />
                                                                        <asp:HiddenField ID="hdnFile" runat="server" />

                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnSubmit" runat="server" Text="Upload" ValidationGroup="Submit" OnClick="btnSubmit_Click" Width="80px" Height="35px"
                                                                            CssClass="btn btn-success" />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <%--<asp:AsyncPostBackTrigger ControlID="rdoSelectRecord" EventName="rdoSelectRecord_CheckedChanged" />--%>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>

                                            <div class="col-12" id="divReAdmit" runat="server" visible="false" style="display: none">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="sub-heading">
                                                            <h5>Re-Admission Details</h5>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Admission Batch</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAdmBatch2" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch2"
                                                            Display="None" ErrorMessage="Please Select Admission Batch" ValidationGroup="Admission"
                                                            InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>College</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCollege2" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege2_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvCollege2" runat="server" ControlToValidate="ddlCollege2"
                                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="Admission"
                                                            InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Degree</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegree2" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                            ToolTip="Please Select Degree" OnSelectedIndexChanged="ddlDegree2_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree2"
                                                            Display="None" ErrorMessage="Please Select Degree" ValidationGroup="Admission"
                                                            InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Branch</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBranch2" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvBranch2" runat="server" ControlToValidate="ddlBranch2" Display="None"
                                                            ErrorMessage="Please Select Branch" ValidationGroup="Admission" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Semester</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSemester2" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                            ToolTip="Please Select Semester">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSemester2"
                                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="Admission"
                                                            InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Reason for Re-Admitting Student</label>
                                                        </div>
                                                        <asp:TextBox ID="txtRemark1" Rows="2" TextMode="MultiLine" MaxLength="300" runat="server" />
                                                        <asp:RequiredFieldValidator ID="rfvRemark1" runat="server" ControlToValidate="txtRemark1"
                                                            Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="Admission"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnReAdmit" runat="server" OnClick="btnReAdmit_Click" Text="Re-Admit" Visible="false" OnClientClick="return ConfirmReAdmit();" ValidationGroup="Admission" CssClass="btn btn-primary" />

                                                    <asp:Button ID="btnReAdmissionSlip" runat="server" OnClick="btnReAdmissionSlip_Click" Text="Re-Admission Slip" Visible="false" Enabled="false" CssClass="btn btn-primary" />

                                                    <asp:Button ID="btnCancel_ReAdm" runat="server" OnClick="btnCancel_ReAdm_Click" Text="Clear" Visible="false" CssClass="btn btn-warning" />
                                                    <asp:ValidationSummary ID="rfv2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="Admission" />
                                                </div>
                                            </div>
                                            <div id="divMsg1" runat="server">
                                            </div>
                                        </div>
                                        <asp:HiddenField runat="server" ID="hdnOrgId" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="btnReAdmit" />--%>
                                        <%--<asp:PostBackTrigger ControlID="btnReAdmissionSlip" />--%>
                                        <%--<asp:PostBackTrigger ControlID="btnSearch1" />--%>
                                        <asp:PostBackTrigger ControlID="lvReAdmit" />

                                    </Triggers>

                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--Search Box Script Start--%>
    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
           // $("#<%= pnltextbox.ClientID %>").hide();

            $("#<%= pnlDropdown.ClientID %>").hide();
        });
        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please Select Search Criteria.')
                $(e).focus();
                return false;
            }

            else {


                if (rbText == "ddl") {
                    var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        return true;
                        //$("#<%= divpanel.ClientID %>").hide();
                        $("#<%= pnltextbox.ClientID %>").hide();

                    }
                }
                else if (rbText == "BRANCH") {

                    if (searchtxt == "Please Select") {
                        alert('Please Select Branch..!');

                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data to Search.');
                        //$(searchtxt).focus();
                        document.getElementById('<%=txtSearch.ClientID %>').focus();
                        return false;
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }




    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearch.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;

        if (rbText == "IDNO" || rbText == "Mobile") {

            var char = (event.which) ? event.which : event.keyCode;
            if (char >= 48 && char <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122)) {
                return true;
            }
            else {
                return false;
            }

        }
    }
    </script>
    <%--Search Box Script End--%>


    <script type="text/javascript" lang="javascript">

        function ConfirmReAdmit() {
            var ret = confirm('Are you sure you want to Re-admit this Student?');
            if (ret == true)
                return true;
            else
                return false;
        }
        function CancelAdm() {
            var ret = confirm('Are you sure you want to cancel this Student Admission?');
            if (ret == true)
                return true;
            else
                return false;
        }

    </script>

    <script type="text/javascript" language="javascript">
        function ShowClearance() {
            try {
                var recValue = GetSelectedRecord();

                if (recValue != "")
                    __doPostBack("ShowClearance", recValue);
                else
                    alert("Please select a student record.");
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function GetSelectedRecord() {
            var recordValue = "";
            try {
                var tbl = document.getElementById('tblSearchResults');
                if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                    for (i = 1; i < tbl.rows.length; i++) {
                        var dataRow = tbl.rows[i];
                        var dataCell = dataRow.firstChild;
                        var rdo = dataCell.firstChild;
                        if (rdo.checked) {
                            recordValue = rdo.value;
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return recordValue;
        }


        function CancelAdmission() {
            try {

                var recValue = GetSelectedRecord();
                if (recValue != "") {
                    if (document.getElementById('<%= txtRemark.ClientID %>').value.trim() != "") {
                        if (confirm("Are you sure you want to cancel this student's admission."))
                            __doPostBack("CancelAdmission", "");
                    }
                    else {
                        alert("Please enter reason for canceling the student's admission.");
                        document.getElementById('<%= txtRemark.ClientID %>').focus();
                    }
                }
                else if (recValue == "") {
                    alert("Please select a student");
                }

            }
            catch (e) {
                alert("Error: " + e.description);
            }
            // return false;
        }


        //added by srikanth 13-4-2020
        function ReAdmission() {

            var recordValue = "";
            try {
                debugger;
                var tbl = document.getElementById('tblSearchResultsforReadmit');
                if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                    for (i = 1; i < tbl.rows.length; i++) {
                        var dataRow = tbl.rows[i];
                        var dataCell = dataRow.firstChild;
                        var rdo = dataCell.firstChild;
                        if (rdo.checked) {
                            recordValue = rdo.value;
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return recordValue;

        }
        //added by srikanth 13-4-2020
        function ReAdmission() {
            try {

                var recValue = GetSelectedRecordForReAdmit();
                if (recValue != "") {
                    if ((document.getElementById('<%=txtRemark1.ClientID %>').value.trim() != "") && (document.getElementById('<%=ddlAdmBatch2.SelectedIndex %>').value != 0) && (document.getElementById('<%=ddlDegree2.SelectedIndex %>').value != 0) && (document.getElementById('<%=ddlSemester2.SelectedIndex %>').value != 0)) {
                        if (confirm("Are you sure you want to Re-Admit this Student ?"))
                            __doPostBack("ReAdmission", "");
                    }
                    else {
                        if (document.getElementById('<%=txtRemark1.ClientID %>').value.trim() = "") {
                            alert("Please Enter Remark for ReAdmitting the Student");
                            document.getElementById('<%= txtRemark1.ClientID %>').focus();
                        }
                        if (document.getElementById('<%=ddlAdmBatch2.SelectedIndex %>').value = 0) {
                            alert("Please Select Admission Batch");
                            document.getElementById('<%= ddlAdmBatch2.SelectedIndex %>').focus();
                        }
                        if (document.getElementById('<%=ddlDegree2.SelectedIndex %>').value = 0) {
                            alert("Please Select Degree");
                            document.getElementById('<%= ddlDegree2.SelectedIndex %>').focus();
                        }
                        if (document.getElementById('<%=ddlSemester2.SelectedIndex %>').value != 0) {
                            alert("Please Select Semester");
                            document.getElementById('<%= ddlSemester2.SelectedIndex %>').focus();
                        }
                    }
                }
                else if (recValue == "") {
                    alert("Please select a student");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function validation() {
            var hdncount = document.getElementById('<%=hdcount.ClientID%>');
            var count = 0;
            for (var i = 0; i < hdncount.value; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvSearchResults_ctrl' + i + '_rdoSelectRecord');
                if (chk.type == 'checkbox') {
                    if (chk.checked) {
                        count++;
                    }
                }
            }
            if (count > 0) {
                var remark = document.getElementById('<%= txtRemark.ClientID%>').value;
                if (remark == '') {
                    alert('Please Enter Remark.');
                    return false;
                }
                else {
                    if (ValidateDDL()) {
                        var ret = confirm('Are You Sure To Cancel Selected Student Admission');
                        if (ret == true)
                            return true;
                        else
                            return false;
                    }
                    else {

                        return false;
                    }
                }
            }
            else {
                alert('Please Check Student for Cancel Admission');
                return false;
            }
        }
    </script>


    <script type="text/javascript" lang="javascript">
        function chkmonth(val) {
            var chk = document.getElementById("<%= chkbreak.ClientID%>");
             var chkdis = document.getElementById("<%=chkdiscontinue.ClientID%>");
             if (chk.type == "checkbox") {
                 if (val.checked == true) {
                     document.getElementById("<%= divMonthyear.ClientID%>").style.display = 'block';
                    document.getElementById("<%=chkdiscontinue.ClientID%>").disabled = true;
                    alert(chkdiscontinue);
                    document.getElementById("<%= chkdiscontinue.ClientID%>").style.display = 'none';
                }
                else
                    document.getElementById("<%= divMonthyear.ClientID%>").style.display = 'none';
                document.getElementById("<%=chkdiscontinue.ClientID%>").disabled = false;
            }

        }
        function chkdis(v) {
            var chkdis = document.getElementById("<%=chkdiscontinue.ClientID%>");
            if (chkdis.type == "checkbox") {
                if (v.checked == true) {
                    document.getElementById("<%=chkbreak.ClientID%>").disabled = true;
                    alert(chkdiscontinue);
                    document.getElementById("<%= chkdiscontinue.ClientID%>").style.display = 'none';
                }
                else
                    document.getElementById("<%= divMonthyear.ClientID%>").style.display = 'none';
                document.getElementById("<%=chkbreak.ClientID%>").disabled = false;
            }
        }
    </script>


     <script>
         function ValidateDDL() {

             if ($("#ctl00_ContentPlaceHolder1_chkbreak").is(":checked")) {

                 var ddlMonth = $("[id$=ddlMonth]").attr("id");
                 var ddlMonth = document.getElementById(ddlMonth);
                 if (ddlMonth.value == 0) {
                     alert('Please Select Month', 'Warning!');
                     //$(txtweb).css('border-color', 'red');
                     $(ddlMonth).focus();
                     return false;
                 }
                 //else {
                 //    return true;
                 //}
                 var ddlYear = $("[id$=ddlYear]").attr("id");
                 var ddlYear = document.getElementById(ddlYear);
                 if (ddlYear.value == 0) {
                     alert('Please Select Year', 'Warning!');
                     //$(txtweb).css('border-color', 'red');
                     $(ddlYear).focus();
                     return false;
                 }
                 else {
                     return true;
                 }
             }
             else {
                 return true;
             }
         }
    </script>
</asp:Content>
