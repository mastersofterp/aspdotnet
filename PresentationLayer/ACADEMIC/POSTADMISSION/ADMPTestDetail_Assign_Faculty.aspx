<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPTestDetail_Assign_Faculty.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMPTestDetail_Assign_Faculty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
        #ctl00_ContentPlaceHolder1_DataPager1 a {
            background-color: #fff;
            border: 1px solid;
            padding: 6px 10px;
            border-radius: 19px;
            text-decoration: none;
        }

        #ctl00_ContentPlaceHolder1_NumberDropDown {
            padding-top: 0.25rem;
            padding-bottom: 0.25rem;
            padding-left: 0.5rem;
            font-size: .845rem;
            border-radius: 0.25rem;
            border: 1px solid #ccc;
        }
        /*.default-c .paginate_button.page-item.active a {
        }*/

        #ctl00_ContentPlaceHolder1_DataPager1 span {
            background-color: #0d70fd;
            border: 1px solid #fff;
            padding: 6px 10px;
            border-radius: 19px;
            text-decoration: none;
            color: #fff;
        }
    </style>

    <asp:UpdatePanel ID="updtestdetails" runat="server">
        <ContentTemplate>
            <div>
                <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            <asp:HiddenField ID="hfdUserNo" runat="server" />
                        </div>

                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Admission Batch</sup></label>
                                    </div>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlAdmBatch" ValidationGroup="TestScore" AutoPostBack="true" AppendDataBoundItems="true"
                                        data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch" ValidationGroup="TestScore"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Admission Batch">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Program Type</label>
                                    </div>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlProgramType" ValidationGroup="TestScore" AutoPostBack="true" AppendDataBoundItems="true"
                                        data-select2-enable="true" OnSelectedIndexChanged="ddlProgramType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvProgramType" runat="server" ControlToValidate="ddlProgramType" ValidationGroup="TestScore"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Program Type">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Test</label>
                                    </div>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlTestName" ValidationGroup="TestScore" AppendDataBoundItems="true"
                                        data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTestName" runat="server" ControlToValidate="ddlTestName" ValidationGroup="TestScore"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Test Name">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Verifying User</label>
                                    </div>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlFaculty" ValidationGroup="TestScore" AppendDataBoundItems="true"
                                        data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlFaculty" runat="server" ControlToValidate="ddlFaculty" ValidationGroup="TestScore"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Verifying User">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Payment Status</label>
                                    </div>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlPaymentStatus" ValidationGroup="TestScore" AppendDataBoundItems="true"
                                        data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Paid</asp:ListItem>
                                        <asp:ListItem Value="2">Unpaid</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 btn-footer" id="buttonSection" runat="server">
                            <asp:LinkButton ID="btnShow" runat="server" ValidationGroup="TestScore" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnAssign" runat="server" Text="Assign" CssClass="btn btn-outline-success" OnClick="btnAssign_Click" Visible="false" />
                            <asp:ValidationSummary ID="vsFacultyAssign" runat="server" DisplayMode="List"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="TestScore" />
                        </div>

                        <div class="col-md-12">
                            <asp:Panel ID="pnlTestScore" runat="server" Visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-9 col-md-6 col-6">
                                        <asp:Label ID="lblshow" runat="server" Text="Show "></asp:Label>
                                        <asp:DropDownList ID="NumberDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="NumberDropDown_SelectedIndexChanged">
                                             <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                            <asp:ListItem Text="200" Value="200"></asp:ListItem>
                                            <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                            <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                            <asp:ListItem Text="1500" Value="1500"></asp:ListItem>
                                            <asp:ListItem Text="2000" Value="2000"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblentries" runat="server" Text=" entries"></asp:Label>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-6" style="text-align: right">
                                        <input type="text" id="FilterData" class="form-control" placeholder="Search" />
                                        <div class="input-group-addon d-none">
                                            <i class="fa fasss-search"></i>
                                        </div>
                                    </div>
                                </div>

                                <asp:ListView ID="lvTestScore" runat="server" Visible="true" OnPagePropertiesChanging="lvTestScore_PagePropertiesChanging">
                                    <LayoutTemplate>
                                        <div class="table-responsive" style="height: 400px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                            <table class="table table-striped table-bordered nowrap" id="tbltest">
                                                <thead class="bg-light-blue" style="vertical-align:top">
                                                    <tr>
                                                        <th>
                                                            SR NO.
                                                        </th>
                                                        <th runat="server" id="thcheck">
                                                            <asp:CheckBox ID="chkAll" onclick="checkAll(this)" Text="" runat="server" />
                                                        </th>
                                                        <th style="width: 150px;">Candidate <br />Name
                                                        </th>
                                                        <th>Username
                                                        </th>
                                                        <th>Mobile No.
                                                        </th>
                                                        <th style="width: 50px;">Application
                                                        <br />
                                                            Category
                                                        </th>
                                                        <th>Application No.<br />
                                                            /Registration No.
                                                        </th>
                                                        <th>Verifying
                                                        <br />
                                                            User
                                                        </th>
                                                        <th>Payment
                                                        <br />
                                                            Status
                                                        </th>
                                                        <th>Verified By
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                             <%# Container.DataItemIndex + 1%> 
                                            </td>
                                            <td id="Td1" runat="server">
                                                <asp:CheckBox ID="chkIsVerify" runat="server" TabIndex="1" Checked='<%# Eval("VERIFYING_FACULTY").ToString() == "0" ? false : true %>' />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hfdScoreId" runat="server" Value='<%# Eval("SCOREID") %>' />
                                                <asp:Label ID="lblCandiateName" runat="server" Text='<%#Eval("CANDIDATE_NAME") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("USERNAME") %>' Enabled="false" TabIndex="1"></asp:Label>
                                                <asp:HiddenField ID="hfdUserNo" runat="server" Value='<%# Eval("USERNO") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MOBILENO") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblApplicationcategory" runat="server" Text='<%#Eval("APPLICATION_CATEGORY") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFacultyName" runat="server" Text='<%# Eval("VERIFYING_USER") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPaymentStatus" runat="server" Text='<%# Eval("PAYMENT_STATUS") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblVerifiedBy" runat="server" Text='<%# Eval("VERIFIED_BY") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div style="text-align: left; margin-top: 0px;">
                                    <asp:DataPager ID="DataPager2" runat="server" PagedControlID="lvTestScore" PageSize="200">
                                        <Fields>
                                            <asp:TemplatePagerField>
                                                <PagerTemplate>
                                                    <b>Showing
                                                                <asp:Label runat="server" ID="CurrentPageLabel"
                                                                    Text="<%# Container.StartRowIndex+1 %>" />
                                                        to
                                                                <asp:Label runat="server" ID="TotalPagesLabel"
                                                                      Text="<%# Convert.ToInt32(Container.StartRowIndex+ Container.PageSize) > Convert.ToInt32(Container.TotalRowCount) ? Convert.ToInt32(Container.TotalRowCount):Convert.ToInt32(Container.StartRowIndex+ Container.PageSize) %>"/>
                                                        ( of
                                                                <asp:Label runat="server" ID="TotalItemsLabel"
                                                                    Text="<%# Container.TotalRowCount%>" />
                                                        records)
                                                                <br />
                                                    </b>
                                                </PagerTemplate>
                                            </asp:TemplatePagerField>
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                                <div style="text-align: right; margin-top: 0px;">
                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvTestScore" PageSize="200">
                                        <Fields>
                                            <asp:NumericPagerField />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </asp:Panel>

                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function checkAll(source) {
            var checkboxes = document.querySelectorAll('tbody input[type="checkbox"]');
            for (var i = 0; i < checkboxes.length; i++) {
                if (!checkboxes[i].disabled) {
                    checkboxes[i].checked = source.checked;
                }
            }
        }
    </script>
    <script>
        function toggleSearch(searchBar, table) {
            var tableBody = table.querySelector('tbody');
            var allRows = tableBody.querySelectorAll('tr');
            var val = searchBar.value.toLowerCase();
            allRows.forEach((row, index) => {
                var insideSearch = row.innerHTML.trim().toLowerCase();
            //console.log('data',insideSearch.includes(val),'searchhere',insideSearch);
            if (insideSearch.includes(val)) {
                row.style.display = 'table-row';
            }
            else {
                row.style.display = 'none';
            }

        });
        }

        function test5() {
            var searchBar5 = document.querySelector('#FilterData');
            var table5 = document.querySelector('#tbltest');
            //console.log(allRows);
            //searchBar5.addEventListener('focusout', () => {
            searchBar5.addEventListener('keyup', () => {
                toggleSearch(searchBar5, table5);
        });

        $(".saveAsExcel").click(function () {
            let UserCall = prompt("Please Enter Password:");
            var ExcelDetails = '<%=Session["ExcelDetails"] %>';
            if (UserCall == null || UserCall == "") {
                return false;
            } else {
                if(UserCall == ExcelDetails)
                {

                }
                else {
                    alert('Password is not matched !!!')
                    return false;
                }
            }
            //if (confirm('Do You Want To Apply for New Program?') == true) {
            //    return false;
            //}
            var workbook = XLSX.utils.book_new();
            var allDataArray = [];
            allDataArray = makeTableArray(table5, allDataArray);
            var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
            workbook.SheetNames.push("Test");
            workbook.Sheets["Test"] = worksheet;
            XLSX.writeFile(workbook, "LeadData.xlsx");
        });
        }

        function makeTableArray(table, array) {
            var allTableRows = table.querySelectorAll('tr');
            allTableRows.forEach(row => {
                var rowArray = [];
            if (row.querySelector('td')) {
                var allTds = row.querySelectorAll('td');
                allTds.forEach(td => {
                    if (td.querySelector('span')) {
                        rowArray.push(td.querySelector('span').textContent);
            }
            else if (td.querySelector('input')) {
                rowArray.push(td.querySelector('input').value);
            }
            else if (td.querySelector('select')) {
                rowArray.push(td.querySelector('select').value);
            }
            else if (td.innerText) {
                rowArray.push(td.innerText);
            }
            else{
                rowArray.push('');
            }
        });
        }
        if (row.querySelector('th')) {
            var allThs = row.querySelectorAll('th');
            allThs.forEach(th => {
                if (th.textContent) {
                    rowArray.push(th.textContent);
        }
        else {
            rowArray.push('');
        }
        });
        }
        // console.log(allTds);

        array.push(rowArray);
        });
        return array;
        }

    </script>
</asp:Content>


