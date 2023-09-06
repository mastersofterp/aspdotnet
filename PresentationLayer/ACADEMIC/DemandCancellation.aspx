<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DemandCancellation.aspx.cs" Inherits="ACADEMIC_DemandCancellation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_lvStudent_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_pnlALLDemands .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <%--<script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
        function ClearSearchBox(btnclear) {
            document.getElementById('<%=txtSearch.ClientID %>').value = '';
             __doPostBack(btnclear, '');
             return true;
         }
    </script>

    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>--%>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <div id="myModal1" class="modal fade" role="dialog" style="display: none">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="updEdit" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Search</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search by</label>
                                        </div>
                                        <asp:RadioButton ID="rdoName" Checked="true" runat="server" Text="Name" GroupName="edit" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoEnrollmentNo" runat="server" Text="Registration No." GroupName="edit" />
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Programme/Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlYear" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" />
                                    </div>

                                    <div class="col-12">
                                        <%--<asp:ListView ID="lvStudent" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Search Results</h5>
                                                </div>
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Name
                                                                </th>
                                                                <th>Registration. No.
                                                                </th>
                                                                <th>Degree
                                                                </th>
                                                                <th>Programme/Branch
                                                                </th>
                                                                <th>Year
                                                                </th>
                                                                <th>Semester
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </asp:Panel>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("NAME") %>' CommandArgument='<%# Eval("IDNO") %>'
                                                            OnClick="lnkId_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ENROLLNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CODE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SHORTNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("YEARNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>--%>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <div class="col-12 btn-footer">
                                <input id="btnSearch" type="button" value="Search" onclick="SubmitSearch(this.id);" class="btn btn-primary" />
                                <%--          <input id="btnClear" type="button" value="Clear Text" class="btn btn-warning" onclick="javascript:document.getElementById('<%=txtSearch.ClientID %>    ').value = '';" />--%>
                                <asp:Button ID="btnClear" runat="server" Text="Cancel" OnClientClick="return ClearSearchBox(this.name)" CssClass="btn btn-warning" />
                                <asp:Button ID="btnCloseModalbox" runat="server" Text="Close" OnClientClick="return ClearSearchBox(this.name)" CssClass="btn btn-danger" data-dismiss="modal" />
                                <%--          <input id="btnCloseModalbox" type="button" value="Close" data-dismiss="modal" class="btn btn-default" />    --%>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12" style="display: none">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Search Student</label>
                                </div>
                                <div class="input-group">
                                    <asp:TextBox ID="txtEnrollNo" runat="server" MaxLength="15" placeholder="Enter Registration No."
                                        TabIndex="1" />
                                    <div class="input-group-addon">
                                        <a href="#" title="Search Student for Fee Payment" data-toggle="modal" data-target="#myModal1"
                                            style="background-color: White">
                                            <asp:Image ID="imgSearch" runat="server" ImageUrl="~/Images/search-svg.png" />
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" TabIndex="2"
                                    ValidationGroup="studSearch" CssClass="btn btn-primary" />
                            </div>
                            <asp:RequiredFieldValidator ID="valEnrollNo" runat="server" ControlToValidate="txtEnrollNo"
                                Display="None" ErrorMessage="Please enter registration number." SetFocusOnError="true"
                                ValidationGroup="studSearch" />
                            <asp:ValidationSummary ID="valSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="studSearch" />
                        </div>
                    </div>

                    <div class="col-12">
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

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Search Criteria</label>
                                                </div>

                                                <%--onchange=" return ddlSearch_change();"--%>
                                                <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSearch" InitialValue="0"
                                                    Display="None" ErrorMessage="Please select search string from the given list"
                                                    SetFocusOnError="true" ValidationGroup="search" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">


                                                <asp:Panel ID="pnltextbox" runat="server">
                                                    <div id="divtxt" runat="server" style="display: block">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Search String</label>
                                                        </div>
                                                        <asp:TextBox ID="txtSearchStr" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvSearchtring" runat="server" ControlToValidate="txtSearchStr" InitialValue="" Enabled="false"
                                                            Display="None" ErrorMessage="Please Enter search string in the given text box"
                                                            SetFocusOnError="true" ValidationGroup="search" />
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlDropdown" runat="server" Visible="false">
                                                    <div id="divDropDown" runat="server" style="display: block">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%-- <label id="lblDropdown"></label>--%>
                                                            <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvDDL" runat="server" ControlToValidate="ddlDropdown" InitialValue="0" Enabled="false"
                                                            Display="None" ErrorMessage="Please select search string from the given list"
                                                            SetFocusOnError="true" ValidationGroup="search" />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <%-- OnClientClick="return submitPopup(this.name);"--%>
                                            <asp:Button ID="btnSearchStu" runat="server" Text="Search" OnClick="btnSearchStu_Click"
                                                CssClass="btn btn-primary" ValidationGroup="search" /><%-- OnClientClick="return submitPopup(this.name);" --%>
                                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                            <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="search" />
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
                    </div>

                    <asp:UpdatePanel ID="updAllDemand" runat="server">
                        <ContentTemplate>
                            <div id="divAllDemands" runat="server" visible="false" class="col-12">
                                <div class="sub-heading" style="display: none">
                                    <h5>Fee Demands</h5>
                                </div>
                                <asp:Panel ID="pnlALLDemands" runat="server">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                        <asp:Repeater ID="lvAllDemands" runat="server" OnItemDataBound="lvAllDemands_ItemDataBound">
                                            <HeaderTemplate>
                                                <div class="sub-heading">
                                                    <h5>Available Fee Demands</h5>
                                                </div>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Name
                                                        </th>
                                                        <th>Registration. No.
                                                        </th>
                                                        <th>Degree
                                                        </th>
                                                        <th>Programme/Branch
                                                        </th>
                                                        <th>Year
                                                        </th>
                                                        <th>Sem.
                                                        </th>
                                                        <th>Acad. Batch
                                                        </th>
                                                        <th>Receipt_Code
                                                        </th>
                                                        <th>Payment Cat.
                                                        </th>
                                                        <th>Demand Amount
                                                        </th>
                                                        <th>Demand Status
                                                        </th>
                                                        <th>Remark
                                                        </th>
                                                        <th>Cancel Demand
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%--<tr id="itemPlaceholder" runat="server" />--%>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <tr>
                                                    <td>
                                                        <%# Eval("NAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ENROLLNMENTNO") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CODE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SHORTNAME")%>
                                                    </td>
                                                    <td>
                                                            <%# Eval("YEARNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>
                                                        <asp:Label ID="lblSem" runat="server" Visible="false" ToolTip='<%# Eval("SEMESTERNO") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BATCHNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("RECIEPT_CODE")%>
                                                        <asp:Label ID="lblRecieptCode" runat="server" Visible="false" ToolTip='<%# Eval("RECIEPT_CODE") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PAYTYPENAME")%>
                                                        <asp:Label ID="lblPayTypeNo" runat="server" Visible="false" ToolTip='<%# Eval("PAYTYPENO") %>'></asp:Label>

                                                    </td>
                                                    <td style="text-align: center;">
                                                        <%# Eval("TOTAL_AMT")%>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <%# Eval("DEMAND_STATUS")%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtremark" Rows="4" TextMode="MultiLine" MaxLength="400" Columns="30" Width="120"
                                                        runat="server" />

                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="btnDelete" runat="server" Text="" CommandArgument='<%# Eval("DM_NO") %>' CommandName='<%# Eval("IDNO") %>'
                                                            ToolTip="Cancel Demand" OnClientClick="return showConfirm();" OnClick="btnDeleteDemand_Click" Visible="false" />
                                                        <%--<asp:Label ID="lblTxt" runat="server" Visible="false" Text=""></asp:Label>--%>

                                                        <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("DM_NO") %>' CommandName='<%# Eval("IDNO") %>'
                                                            ToolTip="Cancel Demand" OnClientClick="return showConfirm();" OnClick="btnDeleteDemand_Click" Visible="false" />
                                                        <asp:Label ID="lblTxt" runat="server" Visible="false" Text=""></asp:Label>--%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:Panel>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" language="javascript">

        function showConfirm() {
            var ret = confirm('Do you want to really cancel demand!');
            if (ret == true)
                return true;
            else
                return false;
        }

        function ShowModalbox() {
            try {
                Modalbox.show($('divModalboxContent'), { title: 'Search Student for Fee Demand cancellation', width: 600, overlayClose: false, slideDownDuration: 0.1, slideUpDuration: 0.1, afterLoad: SetFocus });
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return;
        }

        function SetFocus() {
            try {
                document.getElementById('<%= txtSearch.ClientID %>').focus();
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function SubmitSearch(btnsearch) {
            try {
                var searchParams = "";
                if (document.getElementById('<%= rdoName.ClientID %>').checked) {
                    searchParams = "Name=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                    searchParams += ",EnrollNo=";
                }
                else if (document.getElementById('<%= rdoEnrollmentNo.ClientID %>').checked) {
                    searchParams = "EnrollNo=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                    searchParams += ",Name=";
                }
            searchParams += ",DegreeNo=" + document.getElementById('<%= ddlDegree.ClientID %>').options[document.getElementById('<%= ddlDegree.ClientID %>').selectedIndex].value;
                searchParams += ",BranchNo=" + document.getElementById('<%= ddlBranch.ClientID %>').options[document.getElementById('<%= ddlBranch.ClientID %>').selectedIndex].value;
                searchParams += ",YearNo=" + document.getElementById('<%= ddlYear.ClientID %>').options[document.getElementById('<%= ddlYear.ClientID %>').selectedIndex].value;
                searchParams += ",SemNo=" + document.getElementById('<%= ddlSem.ClientID %>').options[document.getElementById('<%= ddlSem.ClientID %>').selectedIndex].value;

                __doPostBack(btnsearch, searchParams);
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return;
        }


    </script>

    <%--Search Box Script Start--%>
    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            //$("#<%= pnltextbox.ClientID %>").show();

            // $("#<%= pnlDropdown.ClientID %>").hide();
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
                        // $("#<%= pnltextbox.ClientID %>").show();

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
                    searchtxt = document.getElementById('<%=txtSearchStr.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data to Search.');
                        //$(searchtxt).focus();
                        document.getElementById('<%=txtSearchStr.ClientID %>').focus();
                        return false;
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        $("#<%= pnltextbox.ClientID %>").show();

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

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122) || (char = 32)) {
                return true;
            }
            else {
                return false;
            }

        }
    }
    </script>
    <%--Search Box Script End--%>
       <script type="text/javascript">
           $(function () {
               $(':text').bind('keydown', function (e) {
                   //on keydown for all textboxes prevent from postback
                   if (e.target.className != "searchtextbox") {
                       if (e.keyCode == 13) { //if this is enter key
                           document.getElementById('<%=btnSearchStu.ClientID%>').click();
                        e.preventDefault();
                        return true;
                    }
                    else
                        return true;
                }
                else
                    return true;
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $(':text').bind('keydown', function (e) {
                    //on keydown for all textboxes
                    if (e.target.className != "searchtextbox") {
                        if (e.keyCode == 13) { //if this is enter key
                            document.getElementById('<%=btnSearchStu.ClientID%>').click();
                            e.preventDefault();
                            return true;
                        }
                        else
                            return true;
                    }
                    else
                        return true;
                });
            });

        });
    </script>


    <div id="divMsg" runat="server">
    </div>

</asp:Content>

