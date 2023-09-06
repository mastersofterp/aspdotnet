<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PhdExaminerTracker.aspx.cs" Inherits="Academic_PhdExaminerAllotment" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .bor-der {
            border-bottom: 1px solid #eee;
        }
    </style>

    <script type="text/javascript">

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('#id1').dataTable({
                paging: false,
                searching: true,
                bDestroy: true
            });
        });
    </script>
    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            $(function () {
                $("#<%=txtDateOfJoining.ClientID%>").datepicker({
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
            Autocomplete();
        }
    </script>

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
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

    <asp:Panel ID="pnDisplay" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div2" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">PHD EXAMINER TRACKER </h3>
                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                    </div>

                    <div class="box-body">
                        <asp:Panel ID="pnlsearch" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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

                                                </asp:DropDownList>

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">


                                                <asp:Panel ID="pnltextbox" runat="server">
                                                    <div id="divtxt" runat="server" style="display: block">
                                                        <div class="label-dynamic">
                                                            <label>Search String</label>
                                                        </div>
                                                        <asp:TextBox ID="txtsearchstu" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
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
                                            <asp:Button ID="btnsearchstu" runat="server" Text="Search" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" OnClick="btnsearchstu_Click" />
                                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" OnClick="btnClose_Click" />
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Label ID="Label3" runat="server" SkinID="lblmsg" />
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <asp:Panel ID="pnlLV" runat="server">
                                            <asp:ListView ID="liststudent" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <div class="sub-heading">
                                                            <h5>Student List</h5>
                                                        </div>
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Name
                                                                        </th>
                                                                        <th style="display: none">IdNo
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true">RRN</asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true">BRANCH</asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true">SEMESTER</asp:Label>
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
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="liststudent" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>

                        <div id="divGeneralInfo" style="display: block;" class="col-md-12" runat="server">
                            <asp:Panel ID="pnlId" runat="server">
                                <div class="form-group col-sm-4">
                                    <label>ID No.</label>
                                    <div class="input-group date">
                                        <asp:TextBox ID="txtIDNo" runat="server" class="form-control" TabIndex="1" Enabled="False" />
                                        <%--  Enable the button so it can be played again --%>
                                        <a href="#" title="Search Student for Modification" onclick="Modalbox.show($('divdemo2'), {title: this.title, width: 600,overlayClose:false});return false;"></a>
                                        <div class="input-group-addon">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/IMAGES/search.png" TabIndex="1" data-toggle="modal" data-target="#myModal"
                                                AlternateText="Search" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                        <asp:Panel ID="pnldetails" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12" id="dvgeneral" runat="server">
                                        <div class="sub-heading">
                                            <h5>General Info</h5>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>ID. No. </label>
                                        </div>
                                        <asp:TextBox ID="txtRegNo" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Please Enter Roll No."
                                            Enabled="false" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Enrollment No. </label>
                                        </div>
                                        <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Please Enter Enrollment No."
                                            Enabled="false" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Student Name </label>
                                        </div>
                                        <asp:TextBox ID="txtStudentName" runat="server" MaxLength="150" TabIndex="3" ToolTip="Please Enter Student name"
                                            Enabled="false" CssClass="form-control" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                            FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtStudentName" />
                                        <asp:RequiredFieldValidator ID="rfvStudentName" runat="server" ControlToValidate="txtStudentName"
                                            Display="None" ErrorMessage="Please Enter Name" SetFocusOnError="True" TabIndex="8"
                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Father's Name </label>
                                        </div>
                                        <asp:TextBox ID="txtFatherName" runat="server" TabIndex="4" ToolTip="Please Enter Father's Name"
                                            Enabled="false" CssClass="form-control" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtFatherName" />
                                        <asp:RequiredFieldValidator ID="rfvtxtFatherName" runat="server" ControlToValidate="txtFatherName"
                                            Display="None" ErrorMessage="Please Enter Father Name" SetFocusOnError="True"
                                            ValidationGroup="Academic" Visible="False"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Date of Joining </label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtDateOfJoining" runat="server" TabIndex="14" ToolTip="Please Enter Date Of Joining"
                                                Enabled="false" CssClass="form-control" />
                                            <ajaxToolKit:CalendarExtender ID="ceDateOfJoining" runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" PopupButtonID="txtDateOfJoining1" TargetControlID="txtDateOfJoining">
                                            </ajaxToolKit:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Department </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepatment" runat="server" AppendDataBoundItems="True" TabIndex="15"
                                            Enabled="false" ToolTip="Please Select Department" CssClass="form-control" data-select2-enable="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Status </label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="True" Enabled="false" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Full Time</asp:ListItem>
                                            <asp:ListItem Value="2">Part Time</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Status Category </label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatusCat" runat="server" AppendDataBoundItems="True" TabIndex="15"
                                            Enabled="false" ToolTip="Please Select Status Category" CssClass="form-control" data-select2-enable="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Admission Batch </label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="True" TabIndex="15"
                                            Enabled="false" ToolTip="Please Select Admission Batch" CssClass="form-control" data-select2-enable="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Total No. of credits </label>
                                        </div>
                                        <asp:TextBox ID="txtTotCredits" runat="server" TabIndex="2" ToolTip="Please Enter Total No. of Credits."
                                            Enabled="false" CssClass="form-control" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="nodgc" runat="server">
                                        <div class="label-dynamic">
                                            <label>No. of DGC Member </label>
                                        </div>
                                        <asp:DropDownList ID="ddlNdgc" runat="server" TabIndex="15"
                                            ToolTip="Please Select No.Of DGC" CssClass="form-control" data-select2-enable="true" Enabled="false"
                                            AutoPostBack="True">
                                            <asp:ListItem Selected="True" Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Supervisor </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSupervisor" runat="server" AppendDataBoundItems="True" TabIndex="15"
                                            Enabled="false" ToolTip="Please Select Supervisor" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" />

                                        <asp:DropDownList ID="ddlSupervisorrole" runat="server" AutoPostBack="true" AppendDataBoundItems="True"
                                            Enabled="false" TabIndex="15" ToolTip="Please Select Supervisor role" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="J">Jointly</asp:ListItem>
                                            <asp:ListItem Value="S">Singly</asp:ListItem>
                                            <asp:ListItem Value="T">Multiple</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Area of Research </label>
                                        </div>
                                        <asp:TextBox ID="txtResearch" runat="server" Enabled="false" CssClass="form-control unwatermarked" TextMode="MultiLine"> </asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Jointid" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Joint Supervisor </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCoSupervisor" runat="server" AppendDataBoundItems="True"
                                            Visible="false" TabIndex="15" ToolTip="Please Select Co-Supervisor" CssClass="form-control" data-select2-enable="true" />
                                        <asp:Label ID="lblJointSupevisor" runat="server"></asp:Label>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="tdremark" runat="server">
                                        <div class="label-dynamic">
                                            <label>Remark for Cancellation(Dean) </label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control"> </asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <asp:ListView ID="lvUpload" runat="server" Visible="true">
                                            <LayoutTemplate>
                                                <div id="demo-grid" class="vista-grid">
                                                    <div class="titlebar">
                                                        <%-- <h4>
                                                                    <label class="label label-default">Synopsis List</label></h4>--%>
                                                    </div>
                                                    <table id="example1" class="table table-bordered table-hover text-center">
                                                        <tr class="bg-light-blue">
                                                            <th>Documents Details
                                                            </th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkDownloadDoc" runat="server" OnClick="lnkDownloadDoc_Click"
                                                            class="form-control" Text='<%# Eval("FILENAME") %>' CommandArgument='<%# Eval("IDNO") %>'>
                                                        </asp:LinkButton>
                                                        <asp:HiddenField ID="hdfFilename" runat="server" Value='<%#Eval("PATH") %> ' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        <div id="divremark" runat="server" style="color: Red; font-size: medium;" visible="false" class="form-group col-md-12">
                                            <b>DEAN Reject Student</b>
                                        </div>

                                        <div id="div1" runat="server" style="color: Red; font-size: medium;" class="form-group col-md-12" visible="false">
                                            <b>Student Is Eligible For Thesis Submission</b>
                                        </div>

                                        <div id="divConfirm" runat="server" style="color: Green; display: none; font-size: medium;" visible="true"
                                            class="form-group col-md-12">
                                            <b>Student is Eligible For PhD Synopsis</b>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </asp:Panel>

                        <div class="col-12" id="dvexaminerdetails" runat="server">
                            <div class="row">
                                <div class="col-12" id="trDGC" runat="server">
                                    <div class="sub-heading">
                                        <h5>EXAMINER DETAILS</h5>
                                    </div>
                                </div>
                            </div>

                            <div id="SupervisorExaminer" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <%--Examiner 1 --%>
                                        <div class="row">
                                            <div class="form-group col-lg-12 col-md-12 col-12 bor-der" id="divExaminer1" runat="server">
                                                <div class="label-dynamic">
                                                    <label>
                                                        <asp:Label ID="lblStatus1" Font-Bold="true" runat="server" Text=""> </asp:Label>
                                                        <asp:Label ID="lblExaminer1" runat="server" Text="Examiner1"></asp:Label>
                                                    </label>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-12 col-sm-4 col-lg-1 mt-2">
                                                        <asp:CheckBox ID="chkExaminer1" runat="server" />
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-1">
                                                        <asp:TextBox ID="txtprexaminer1" runat="server" MaxLength="2"
                                                            PlaceHolder="Examiner Priority" CssClass="form-control">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-3">
                                                        <asp:DropDownList ID="ddlExaminer1" runat="server" AppendDataBoundItems="True"
                                                            ToolTip="Please Select Examiner1" OnSelectedIndexChanged="ddlExaminer1_SelectedIndexChanged"
                                                            AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-12 col-sm-4 col-lg-2 pl-lg-0 pr-lg-0">
                                                        <asp:FileUpload ID="fuEx1" runat="server" ToolTip="Select file to upload" accept=".pdf" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="file Required"
                                                            ControlToValidate="fuEx1" ValidationGroup="submit"
                                                            runat="server" Display="Dynamic" ForeColor="Red" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                            ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.PDF|.pdf)$"
                                                            ControlToValidate="fuEx1" runat="server" ForeColor="Red"
                                                            ErrorMessage="Please select a valid PDF File ." ValidationGroup="submit"
                                                            Display="Dynamic" />
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-2">
                                                        <asp:TextBox ID="txtMobile1" runat="server" TabIndex="2" CssClass="form-control" ToolTip="Mobile  No." Enabled="false" />
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-3">
                                                        <asp:TextBox ID="txtemail1" runat="server" TabIndex="2" ToolTip="EmailId" CssClass="form-control" Enabled="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <%--Examiner 2 --%>
                                            <div class="form-group col-lg-12 col-md-12 col-12 bor-der" id="divExaminer2" runat="server">
                                                <div class="label-dynamic">
                                                    <label>
                                                        <asp:Label ID="lblStatus2" runat="server" Font-Bold="true" Text=""> </asp:Label>
                                                        <asp:Label ID="lblExaminer2" runat="server" Text="Examiner2"></asp:Label>
                                                    </label>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-12 col-sm-4 col-lg-1 mt-2">
                                                        <asp:CheckBox ID="chkExaminer2" runat="server" />
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-1">
                                                        <asp:TextBox ID="txtprExaminer2" runat="server" MaxLength="2"
                                                            PlaceHolder="Examiner Priority" CssClass="form-control">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-3">
                                                        <asp:DropDownList ID="ddlExaminer2" runat="server" CssClass="form-control" data-select2-enable="true"
                                                            OnSelectedIndexChanged="ddlExaminer2_SelectedIndexChanged"
                                                            AppendDataBoundItems="True" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-12 col-sm-4 col-lg-2 pl-lg-0 pr-lg-0">
                                                        <asp:FileUpload ID="fuEx2" runat="server" ToolTip="Select file to upload" accept=".pdf" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="file Required"
                                                            ControlToValidate="fuEx2" ValidationGroup="submit"
                                                            runat="server" Display="Dynamic" ForeColor="Red" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                            ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.PDF|.pdf)$"
                                                            ControlToValidate="fuEx2" runat="server" ForeColor="Red"
                                                            ErrorMessage="Please select a valid PDF File ." ValidationGroup="submit"
                                                            Display="Dynamic" />
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-2">
                                                        <asp:TextBox ID="txtMobile2" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Mobile  No." Enabled="false" />
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-3">
                                                        <asp:TextBox ID="txtemail2" runat="server" CssClass="form-control" TabIndex="2" ToolTip="EmailId" Enabled="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <%--Examiner 3 --%>
                                            <div class="form-group col-lg-12 col-md-12 col-12 bor-der" id="divExaminer3" runat="server">
                                                <div class="label-dynamic">
                                                    <label>
                                                        <asp:Label ID="lblStatus3" Font-Bold="true" runat="server" Text=""> </asp:Label>
                                                        <asp:Label ID="lblExaminer3" runat="server" Text="Examiner3"></asp:Label>
                                                    </label>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-12 col-sm-4 col-lg-1 mt-2">
                                                        <asp:CheckBox ID="chkExaminer3" runat="server" />
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-1">
                                                        <asp:TextBox ID="txtprExaminer3" runat="server" MaxLength="2"
                                                            PlaceHolder="Examiner Priority" CssClass="form-control">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-3">
                                                        <asp:DropDownList ID="ddlExaminer3" runat="server" CssClass="form-control" data-select2-enable="true"
                                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlExaminer3_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-12 col-sm-4 col-lg-2 pl-lg-0 pr-lg-0">
                                                        <asp:FileUpload ID="fuEx3" runat="server" ToolTip="Select file to upload" accept=".pdf" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="file Required"
                                                            ControlToValidate="fuEx3" ValidationGroup="submit"
                                                            runat="server" Display="Dynamic" ForeColor="Red" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.PDF|.pdf)$"
                                                            ControlToValidate="fuEx3" runat="server" ForeColor="Red" ErrorMessage="Please select a valid PDF File ." ValidationGroup="submit"
                                                            Display="Dynamic" />
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-2">
                                                        <asp:TextBox ID="txtMobile3" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Mobile  No." Enabled="false" />
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-3">
                                                        <asp:TextBox ID="txtemail3" runat="server" CssClass="form-control" TabIndex="2" ToolTip="EmailId" Enabled="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <%--Examiner 4 --%>
                                            <div class="form-group col-lg-12 col-md-12 col-12 bor-der" id="divExaminer4" runat="server">
                                                <div class="label-dynamic">
                                                    <label>
                                                        <asp:Label ID="lblStatus4" Font-Bold="true" runat="server" Text=""> </asp:Label>
                                                        <asp:Label ID="lblExaminer4" runat="server" Text="Examiner4">  </asp:Label>
                                                    </label>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-12 col-sm-4 col-lg-1 mt-2">
                                                        <asp:CheckBox ID="chkExaminer4" runat="server" />
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-1">
                                                        <asp:TextBox ID="txtprExaminer4" runat="server" MaxLength="2"
                                                            PlaceHolder="Examiner Priority" CssClass="form-control">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-3">
                                                        <asp:DropDownList ID="ddlExaminer4" runat="server" CssClass="form-control" data-select2-enable="true"
                                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlExaminer4_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-12 col-sm-4 col-lg-2 pl-lg-0 pr-lg-0">
                                                        <asp:FileUpload ID="fuEx4" runat="server" ToolTip="Select file to upload" accept=".pdf" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="file Required"
                                                            ControlToValidate="fuEx4" ValidationGroup="submit"
                                                            runat="server" Display="Dynamic" ForeColor="Red" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.PDF|.pdf)$"
                                                            ControlToValidate="fuEx4" runat="server" ForeColor="Red" ErrorMessage="Please select a valid PDF File ." ValidationGroup="submit"
                                                            Display="Dynamic" />
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-2">
                                                        <asp:TextBox ID="txtMobile4" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Mobile  No." Enabled="false" />
                                                    </div>
                                                    <div class="form-group col-12 col-sm-4 col-lg-3">
                                                        <asp:TextBox ID="txtemail4" runat="server" CssClass="form-control" TabIndex="2" ToolTip="EmailId" Enabled="false" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="col-12 btn-footer" id="dvbuttons" runat="server">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click"
                                TabIndex="131" Text="Synopsis Invitation" ValidationGroup="Academic" />
                            <asp:Button ID="btnApproval" runat="server" CssClass="btn btn-primary" OnClick="btnApproval_Click" TabIndex="131"
                                Text="Synopsis Approval" Visible="false" />

                            <asp:Button ID="btnThsSub" runat="server" CssClass="btn btn-primary" OnClick="btnThsSub_Click" TabIndex="131"
                                Text="Thesis Invitation" ValidationGroup="Academic" Visible="false" />
                            <asp:Button ID="btnThsApp" runat="server" CssClass="btn btn-primary" OnClick="btnThsApp_Click" TabIndex="131"
                                Text="Thesis Approval" Visible="false" />

                            <asp:Button ID="btndrctApp" runat="server" CssClass="btn btn-primary" OnClick="btndrctApp_Click" TabIndex="131"
                                Text="Director Final Approval" Visible="false" />
                            <asp:Button ID="btnApply" runat="server" CssClass="btn btn-primary" OnClick="btnApply_Click" TabIndex="134" Text="Apply Student List" />

                            <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-info" OnClick="btnExcel_Click" TabIndex="134" Text="Tracker Details Excel" />
                            <asp:Button ID="btnExaminerReport" runat="server" CssClass="btn btn-info" OnClick="btnExaminerReport_Click"
                                TabIndex="134" Text="External Examiner Report" Visible="false" />

                            <asp:Button ID="btnReject" runat="server" CssClass="btn btn-warning" OnClick="btnReject_Click"
                                TabIndex="134" Text="Synopsis Reject" Visible="false" />
                            <asp:Button ID="btnThsReject" runat="server" CssClass="btn btn-warning"
                                OnClick="btnThsReject_Click" TabIndex="134" Text="Thesis Reject" Visible="false" />

                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="132" Text="Cancel" />

                            <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Academic"
                                DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Academic" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript" lang="javascript">
        $(document).ready(function () {
            debugger
            $("#<%= pnltextbox.ClientID %>").hide();

            $("#<%= pnlDropdown.ClientID %>").hide();
        });
        function submitPopupsearch(btnsearchstu) {

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
                        __doPostBack(btnsearchstu, rbText + ',' + searchtxt);
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
                        __doPostBack(btnsearchstu, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtsearchstu.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data to Search.');
                        //$(searchtxt).focus();
                        document.getElementById('<%=txtsearchstu.ClientID %>').focus();
                        return false;
                    }
                    else {
                        __doPostBack(btnsearchstu, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtsearchstu.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtsearchstu.ClientID %>').value = '';
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

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
