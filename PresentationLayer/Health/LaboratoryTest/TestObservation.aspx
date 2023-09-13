<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TestObservation.aspx.cs" Inherits="Health_LaboratoryTest_TestObservation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    </div>--%>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TEST OBSERVATION</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlTestObservation" runat="server">
                                <div class="col-12">
                                    <%--  <div class="sub-heading">
                                        <h5>Test Observation</h5>
                                    </div>--%>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Search Patient</label>
                                            </div>
                                            <div class="input-group date">
                                                <asp:TextBox ID="txtPatientName" runat="server" CssClass="form-control"
                                                    MaxLength="100" ToolTip="Search Patient"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPatientName" runat="server" ControlToValidate="txtPatientName"
                                                    Display="None" ErrorMessage="Please Search Patient Name" SetFocusOnError="true"
                                                    ValidationGroup="Submit" />

                                                <a class="input-group-addon" href="#" title="Search Patient Details" data-toggle="modal" data-target="#divdemo2">
                                                    <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" Width="15px" />
                                                    <asp:HiddenField ID="hfPatientName" runat="server" />
                                                </a>


                                            </div>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Patient's Visit Date</label>
                                            </div>
                                            <asp:DropDownList ID="ddlVisitDate" runat="server" AutoPostBack="true" TabIndex="2"
                                                AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Patient's Visit Date"
                                                OnSelectedIndexChanged="ddlVisitDate_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvVDt" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please select patients visit date."
                                                ValidationGroup="Submit" ControlToValidate="ddlVisitDate" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Test Sample Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="Image1">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtSampleDt" runat="server" CssClass="form-control" ToolTip="Enter Test Sampple Date"
                                                    TabIndex="3" Style="z-index: 0;"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtSampleDt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtSampleDt" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                    ControlToValidate="txtSampleDt" IsValidEmpty="false" ErrorMessage="Please Enter Valid Test Sample Date."
                                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    Display="None" Text="*" ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Test Due Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="Image2">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDueDt" runat="server" CssClass="form-control" ToolTip="Enter Test Due Date"
                                                    TabIndex="3" Style="z-index: 0;"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtDueDt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDueDt" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender2"
                                                    ControlToValidate="txtDueDt" IsValidEmpty="false" ErrorMessage="Please Enter Valid Test Due Date."
                                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    Display="None" Text="*" ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Test Title</label>
                                            </div>
                                            <asp:DropDownList ID="ddlTitle" runat="server" AppendDataBoundItems="true" TabIndex="5"
                                                AutoPostBack="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Test Title"
                                                OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please Select Test Title." ControlToValidate="ddlTitle"
                                                ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Reference By</label>
                                            </div>
                                            <asp:Label ID="lblRefBy" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trDependent" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Dependent Name</label>
                                            </div>
                                            <asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <%-- <div class="col-12">
                                <div class="row">--%>
                            <asp:Panel ID="pnlTestContentList" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvContent" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Test Content List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>SR. NO.
                                                            </th>
                                                            <%--<th>GROUP NAME
                                                                    </th>--%>
                                                            <th>CONTENT NAME
                                                            </th>
                                                            <th>NORMAL VALUES
                                                            </th>
                                                            <th>UNIT
                                                            </th>
                                                            <th>PATIENT VALUES
                                                            </th>
                                                            <th>RESULT
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
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SRNO") %>' ToolTip="Serial Number" />
                                                    <asp:HiddenField ID="hdnContentNo" runat="server" Value='<%# Eval("CONTENTNO") %>' />
                                                </td>
                                                <%-- <td>
                                                            <asp:Label ID="lblGrpName" runat="server" Text='<%# Eval("GROUP_NAME") %>' />
                                                        </td>--%>
                                                <td>
                                                    <asp:Label ID="lblContentName" runat="server" Text='<%# Eval("CONTENT_NAME") %>' ToolTip="Test Content Name" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblNorValue" runat="server" Text='<%# Eval("NORMAL_RANGE") %>' ToolTip="Test Normal Range" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("UNIT") %>' ToolTip="Test Unit" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPatientValue" runat="server" Text='<%# Eval("PATIENT_VALUE") %>' CssClass="form-control"
                                                        MaxLength="80" TabIndex="6" ToolTip="Enter Patient Value"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbePValues" runat="server"
                                                        FilterType="Custom,LowerCaseLetters,UpperCaseLetters, Numbers"
                                                        TargetControlID="txtPatientValue" ValidChars="%.-/\  ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemark" runat="server" Text='<%# Eval("REMARK") %>' CssClass="form-control"
                                                        MaxLength="100" TabIndex="7" ToolTip="Enter Remark"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </div>
                            </asp:Panel>
                            <%--</div>
                            </div>--%>

                            <asp:Panel ID="pnlCommonRemark" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Add Common Remark</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Common Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtCRemark" runat="server" CssClass="form-control" MaxLength="300"
                                                TextMode="MultiLine" TabIndex="8" ToolTip="Enter Common Remark"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCRemark" runat="server"
                                                FilterType="Custom,LowerCaseLetters,UpperCaseLetters, Numbers" TargetControlID="txtCRemark"
                                                ValidChars="%.-/\  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="9"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-outline-primary" ToolTip="Click here to Submit" CausesValidation="true" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="10"
                                    CssClass="btn btn-outline-danger" ToolTip="Click here to Reset" CausesValidation="false" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlObservationList" runat="server">
                                    <asp:ListView ID="lvObservation" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Observation Entry List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>

                                                            <th>EDIT
                                                            </th>
                                                            <th>PATIENT NAME
                                                            </th>
                                                            <th>TEST TITLE
                                                            </th>
                                                            <th>TEST SAMPLE DATE
                                                            </th>
                                                            <th>TEST DUE DATE
                                                            </th>
                                                            <th>COMMON REMARK
                                                            </th>
                                                            <th>PRINT
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
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                        CommandArgument='<%# Eval("OBSERNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("PATIENT_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TITLE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TEST_SAMPLE_DT", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TEST_DUE_DT", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("COMMON_REMARK")%>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-outline-info" Text="Print" CommandArgument='<%# Eval("OBSERNO") %>' OnClick="btnPrint_Click" />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                                <%-- <div class="vista-grid_datapager">
                                    <div class="text-center">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvObservation" PageSize="10"
                                            OnPreRender="dpPager_PreRender">
                                            <Fields>
                                                <asp:NumericPagerField ButtonCount="3" ButtonType="Link" />
                                            </Fields>
                                        </asp:DataPager>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divdemo2" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Search</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="col-md-12">
                                <div class="form-group col-md-12">
                                    <label>Search Criteria:</label>
                                    <br />
                                    <asp:RadioButtonList ID="rbPatient" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Emp. Code" Value="EmployeeCode" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Emp. Name" Value="EmployeeName"></asp:ListItem>
                                        <asp:ListItem Text="Student Name" Value="StudentName"></asp:ListItem>
                                        <asp:ListItem Text="Student RegNo" Value="StudentRegNo"></asp:ListItem>
                                        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>Search String :</label>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" ToolTip="Enter Search String"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-12">
                                    <p class="text-center">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info"
                                            OnClientClick="return submitPopup(this.name);" ToolTip="Click here to Search" />
                                        <asp:Button ID="btnCanceModal" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                            OnClientClick="return ClearSearchBox()" ToolTip="Click here to Reset" OnClick="btnCanceModal_Click" />
                                        <asp:Button ID="Button1" runat="server" Text="Close" class="btn btn-default" data-dismiss="modal"
                                            AutoPostBack="True" CssClass="btn btn-primary" />
                                    </p>
                                </div>
                                <div class="form-group col-md-12">
                                    <asp:UpdatePanel ID="updEdit" runat="server">
                                        <ContentTemplate>
                                            <%--<asp:Panel ID="pnlSearch" runat="server" ScrollBars="Auto" Height="300px">--%>
                                            <%--<asp:Panel ID="pnlSearch" runat="server" ScrollBars="Auto" Height="300px" Width="520px">--%>
                                            <asp:ListView ID="lvEmp" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <%--<h4 class="box-title">Login Details
                                                        </h4>--%>
                                                        <asp:Panel ID="pnlSearch" runat="server" ScrollBars="Vertical" Height="300px">
                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                    <tr id="trHeader" runat="server" class="bg-light-blue">
                                                                        <th>Name
                                                                        </th>
                                                                        <th>Employee Code
                                                                        </th>
                                                                        <th>Department
                                                                        </th>
                                                                        <th>Designation
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
                                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name")%>' CommandArgument='<%# Eval("IDNo")%>'
                                                                OnClick="lnkId_Click"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PFILENO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUBDEPT")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUBDESIG")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <%--</asp:Panel>--%>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                </div>
            </div>

        </div>
    </div>

    <script type="text/javascript" language="javascript">

        function submitPopup(btnSearch) {
            //debugger;
            var rbText;
            var searchtxt;

            if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_0').checked == true)
                rbText = "EmployeeCode";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_1').checked == true)
                rbText = "EmployeeName";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_2').checked == true)
                rbText = "StudentName";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_3').checked == true)
                rbText = "StudentRegNo";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_4').checked == true)
                rbText = "Other";

            searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
            __doPostBack(btnSearch, rbText + ',' + searchtxt);
            return true;
        }

        function ClearSearchBox() {
            document.getElementById('<%=txtSearch.ClientID %>').value = '';
        }

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }

        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }

        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../images/collapse_blue.jpg";
            }
        }

    </script>

</asp:Content>

