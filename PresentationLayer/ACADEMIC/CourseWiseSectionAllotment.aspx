<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CourseWiseSectionAllotment.aspx.cs" ViewStateEncryptionMode="Always" EnableViewStateMac="true" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_CourseWiseSectionAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <style>
        .Searchfilter {
            font-size: 15px !important;
            padding: 0.375rem 0.75rem !important;
            display: block !important;
            width: 100% !important;
            height: 42px !important;
            background-color: transparent !important;
            border: 1px solid #ced4da !important;
            border-radius: 0.25rem !important;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out !important;
            margin-left: -15px !important;
            margin-bottom: 5px !important;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlSection"
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

    <asp:UpdatePanel ID="updpnlSection" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" Font-Bold="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"
                                            TabIndex="7" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse" SetFocusOnError="true"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Course." ValidationGroup="course">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <%--    <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>
                                        <%-- Added By Vipul Tichakule on date 16/02/2024 --%>
                                        <asp:ListBox runat="server" ID="ddlClgname" AppendDataBoundItems="true"
                                            SelectionMode="Multiple" CssClass="form-control multi-select-demo" ></asp:ListBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme.">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlColg" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged"
                                            ToolTip="Please Select Institute.">
                                        </asp:DropDownList>



                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlColg"
                                            Display="None" ErrorMessage="Please Select Institute" ValidationGroup="course"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control" data-select2-enable="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch." ValidationGroup="course"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme." ValidationGroup="course"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <%--  <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" TabIndex="6" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>

                                        <%-- Added By Vipul Tichakule on date 16/02/2024 --%>
                                        <asp:ListBox runat="server" ID="ddlSem" SelectionMode="Multiple" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                            OnSelectedIndexChanged="ddlSem_SelectedIndexChanged"></asp:ListBox>

                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Semester." InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>


                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" TabIndex="8" Text="Show Students" ValidationGroup="course"
                                    CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="9" Enabled="false" OnClick="btnSubmit_Click"
                                    Text="Submit" CssClass="btn btn-primary" ValidationGroup="course" />
                                <asp:Button ID="btnReport" runat="server" TabIndex="9" OnClick="btnReport_Click"
                                    Text="Report (Excel)" CssClass="btn btn-primary" ValidationGroup="course" />
                                <asp:Button ID="btnClear" runat="server" CausesValidation="false" Text="Clear" TabIndex="10" CssClass="btn btn-warning" OnClick="btnClear_Click" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="course"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>


                            <div class="col-md-12">
                                <div class="row" runat="server" id="MainDivSection" visible="false">

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivSection" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>--%>
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"
                                            TabIndex="10" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivBatch" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>--%>
                                            <label>Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="11" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivTutorialBatch" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>--%>
                                            <label>Tutorial Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTutorialBatch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="11" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>



                                    <%-- Added By Vipul Tichakule on date 16/02/2024 --%>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivSortBy" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <%--<asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>--%>
                                            <label>Sort By:</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSortBy" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged"
                                            TabIndex="10" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">RegNo</asp:ListItem>
                                            <asp:ListItem Value="2">Name</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>



                                    <div class="form-group col-lg-6 col-md-6 col-12" id="DivRange" runat="server">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Range From</label>
                                        </div>
                                        <div class="form-inline">
                                            <asp:TextBox ID="txtEnrollFrom" runat="server" CssClass="form-control" ValidationGroup="EnrollText" TabIndex="7" />
                                            <asp:RequiredFieldValidator ID="rfvEnrollFrom" runat="server" ControlToValidate="txtEnrollFrom"
                                                Display="None" ErrorMessage="Please Select Registration No From Range" ValidationGroup="Range"></asp:RequiredFieldValidator>

                                            &nbsp;&nbsp;
                                            <label>To</label>
                                            &nbsp;&nbsp;

                                            <asp:TextBox ID="txtEnrollTo" runat="server" CssClass="form-control" ValidationGroup="EnrollText" TabIndex="8" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEnrollTo"
                                                Display="None" ErrorMessage="Please Select Registration No To Range" ValidationGroup="Range"></asp:RequiredFieldValidator>&nbsp;&nbsp;
                                          
                                            <asp:Button ID="btnConfirm" runat="server" TabIndex="9" CssClass="btn btn-primary mt-2 ml-5" Text="Confirm Students" OnClick="btnConfirm_Click" ValidationGroup="Range" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Range"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>


                                    </div>


                                    <%-- end --%>
                                </div>
                            </div>


                            <div class="col-12">
                                <asp:Panel ID="pnlStudent" runat="server" Visible="false">
                                    <asp:ListView ID="lvStudents" runat="server">
                                        <%-- OnItemDataBound="lvStudents_ItemDataBound"--%>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Students List</h5>
                                            </div>
                                            <div class="row mb-1">
                                                <div class="col-lg-2 col-md-6 offset-lg-7">
                                                    <%-- <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>--%>
                                                </div>

                                                <%--  <div class="col-lg-3 col-md-6">
                                                    <div class="input-group sea-rch">
                                                        <input type="text" id="FilterData" class="form-control" placeholder="Search" />
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-search"></i>
                                                        </div>
                                                    </div>
                                                </div>--%>

                                                <div class="col-lg-3 col-md-6">
                                                    <div>
                                                        <input type="text" id="FilterData" class="Searchfilter" placeholder="Search" onkeyup="SearchFunction()" />
                                                        <%-- <div class="input-group-addon"> <i class="fa fa-search"></i> </div>--%>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="table-responsive" style="height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="divlistType">
                                                    <thead class="bg-light-blue" style="position: sticky; top: 0; z-index: 1; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th style="text-align: center;">Sr No.</th>
                                                            <th>
                                                                <asp:CheckBox ID="cbHead" Text="Select All" runat="server" onclick="totAllSubjects(this)" ToolTip="Select/Select all" />
                                                            </th>
                                                            <th>Roll No.</th>
                                                            <th>Student Name</th>
                                                            <th>Section</th>
                                                            <th>Batch Name</th>
                                                            <th>Tutorial Batch Name</th>
                                                            <th>Semester </th>
                                                            <th>Scheme</th>
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
                                                <td style="text-align: center;">
                                                    <%#Container.DataItemIndex+1 %>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbRow" Text='<%# Container.DataItemIndex + 1 %>' runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNO")%>' /></td>
                                                <td>
                                                     <asp:Label ID="lblREGNO" runat="server"> <%# Eval("REGNO")%></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSTUDNAME" runat="server"> <%# Eval("STUDNAME")%></asp:Label>
                                                </td>
                                                <td>
                                                    <%--<asp:DropDownList ID="ddlsec" runat="server" AppendDataBoundItems="true" TabIndex="8" ToolTip='<%# Eval("SECTIONNO")%>'>
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>BATCHNAME
                                                    </asp:DropDownList>--%>
                                                    <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("SECTIONNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBatchName" runat="server" Text='<%# Eval("BATCHNAME")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblTutBatchName" runat="server" Text='<%# Eval("TH_BATCHNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblsemester" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblScheme" runat="server" Text='<%# Eval("SCHEMENAME")%>' ToolTip='<%# Eval("SCHEMENO")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:AsyncPostBackTrigger ControlID="btnConfirm" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            var count = 0;
            for (i = 0; i < document.forms[0].elements.length; i++) {
                // alert("check1");
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;
                        //count++;
                    }
                    else
                        e.checked = false;
                }
            }
            if (headchk.checked == true) {
                count = $('[id*=divlistType] td').closest("tr").length;
            }

        }
    </script>

    <script>
        function SearchFunction() {
            var input, filter, table, tr, td, i, txtValue, td1, td2;
            var Tcount = 0;
            var Pcount = 0;
            var ODcount = 0;
            var totalcount = 0;
            var regnoflag = 0;
            var rollnoflag = 0;
            var namefalg = 0;
            var tdval = 0;
            var td1val = 0;
            var td2val = 0;

            input = document.getElementById("FilterData");
            filter = input.value.toLowerCase();
            table = document.getElementById("divlistType");
            trRow = table.getElementsByTagName("tr");

            for (i = 0; i < trRow.length; i++) {

                td = trRow[i].getElementsByTagName("td")[2]; // 3- check RRNO column
                td1 = trRow[i].getElementsByTagName("td")[3]; // 1- check Name column
                //td2 = trRow[i].getElementsByTagName("td")[4]; // 2- check roll column

                if (td) {
                    if (!isNaN(filter - 0)) {
                        var tdval = 1;
                    }
                    //RRNO search
                    if (regnoflag == 0 && rollnoflag == 0 && tdval == 1) {
                        txtValue = td.innerText;
                        if (txtValue.toLowerCase().indexOf(filter) > -1) {
                            namefalg = 1;
                            Tcount++;
                            //var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_cbRow");
                            //var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_hdfLeaveStatus");
                            //if (e != null) {
                            //    if (e.checked == true) {
                            //        Pcount++;
                            //    }
                            //    if (e.checked == false && e1.value == 1) {
                            //        ODcount++;
                            //    }
                            //}

                            trRow[i].style.display = "";

                        }
                        else {
                            trRow[i].style.display = "none";
                        }
                    }

                    //Name search
                    if (namefalg == 0 && rollnoflag == 0) {
                        txtValue = td1.innerText;
                        if (txtValue.toLowerCase().indexOf(filter) > -1) {
                            regnoflag = 1;
                            Tcount++;
                            var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_cbRow");
                            var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_hdfLeaveStatus");
                            //if (e != null) {
                            //    if (e.checked == true) {
                            //        Pcount++;
                            //    }
                            //    if (e.checked == false && e1.value == 1) {
                            //        ODcount++;
                            //    }
                            //}

                            trRow[i].style.display = "";

                        }
                        else {
                            trRow[i].style.display = "none";
                        }
                    }

                    //Roll No search
                    //if (namefalg == 0 && regnoflag == 0) {
                    //    txtValue = td2.textContent || td2.innerText;
                    //    if (txtValue.toLowerCase().indexOf(filter) > -1) {
                    //        rollnoflag = 1;
                    //        Tcount++;
                    //        var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_cbRow");
                    //        var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_hdfLeaveStatus");
                    //        //if (e != null) {
                    //        //    if (e.checked == true) {
                    //        //        Pcount++;
                    //        //    }
                    //        //    if (e.checked == false && e1.value == 1) {
                    //        //        ODcount++;
                    //        //    }
                    //        //}

                    //        trRow[i].style.display = "";

                    //    }
                    //    else {
                    //        trRow[i].style.display = "none";
                    //    }
                    //}

                }
            }

        }
    </script>


    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>

</asp:Content>
