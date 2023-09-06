<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkExamFormGeneration.aspx.cs" Inherits="ACADEMIC_EXAMINATION_BulkExamFormGeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updtime"
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
    <asp:UpdatePanel ID="updtime" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Bulk Exam Form Generation</h3>
                        </div>
                        <div class="box-body" runat="server" id="divBody">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College & Scheme</label>
                                            <%--<asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="show" data-select2-enable="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label><span style="color: red">*</span>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true" ToolTip="Please Select Session">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-3 d-none">
                                        <label><span style="color: red">*</span>College Name</label>
                                        <asp:DropDownList ID="ddlColg" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged" ToolTip="Please Select College">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvColg" runat="server" ControlToValidate="ddlColg"
                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3 d-none">
                                        <label><span style="color: red">*</span>Degree</label>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="3"
                                            ToolTip="Please Select Degree" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3 d-none">
                                        <label><span style="color: red">*</span>Branch</label>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AppendDataBoundItems="True" TabIndex="4"
                                            ToolTip="Please Select Branch" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3 d-none">
                                        <label><span style="color: red;">*</span>Scheme</label>
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" TabIndex="5" AppendDataBoundItems="true"
                                            ToolTip="Please Select Scheme" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Scheme"
                                            ValidationGroup=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label><span style="color: red">*</span>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" ToolTip="Please Select Semester" TabIndex="6">
                                            <%--AutoPostBack="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged"--%>
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label><span style="color: red">*</span>Type</label>
                                        </div>

                                        <asp:RadioButtonList ID="rdlist" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdlist_SelectedIndexChanged">

                                            <asp:ListItem Value="0">Pending</asp:ListItem>
                                            <asp:ListItem Value="1">Registerd</asp:ListItem>
                                            <asp:ListItem Value="2">All</asp:ListItem>

                                        </asp:RadioButtonList>


                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="margin-top: 30px;">
                                        <asp:CheckBox ID="chkprint" runat="server" Enabled="false" AutoPostBack="true" OnCheckedChanged="chkprint_CheckedChanged" Text="Check for Print Exam Form" />
                                    </div>

                                </div>
                            </div>
                            <%-- <div class="col-md-3" style="margin-top: 25px; display: none;">
                                <asp:RadioButtonList ID="rbRegEx" runat="server" RepeatDirection="Horizontal" TabIndex="777" AutoPostBack="true" >
                                    <asp:ListItem Value="0" Selected="True">&nbsp;Regular Student &nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="1">&nbsp;Ex Student</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>--%>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Student" TabIndex="7"
                                    ToolTip="Shows Students under Selected Criteria." OnClick="btnShow_Click" ValidationGroup="show" CssClass="btn btn-primary" />

                                <asp:Button ID="btnGenerateExamForm" runat="server" Text="Generate Exam Form" TabIndex="8"
                                    ValidationGroup="show" OnClientClick="return ConfirmToGenerate(this);" OnClick="btnGenerateExamForm_Click" CssClass="btn btn-primary" CausesValidation="false" Enabled="false" />
                                <asp:Button ID="btnPrint" runat="server" Text="Print Exam Form" TabIndex="8"
                                    ValidationGroup="show" OnClick="btnPrint_Click" CssClass="btn btn-primary" CausesValidation="false" Enabled="false" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="11"
                                    ToolTip="Cancel Selected under Selected Criteria." CausesValidation="false" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <%-- <asp:Button ID="btnPrintReport" runat="server" Text="Print Registration Slip" TabIndex="999" CssClass="btn btn-info"
                                   ToolTip="Print Card under Selected Criteria." ValidationGroup="show" />

                                <asp:Button ID="btnLock" runat="server" Text="Lock"  TabIndex="9" Visible="false"
                                    ValidationGroup="show" CssClass="btn btn-primary" />

                                <asp:Button ID="btnUnlock" runat="server" Text="Unlock"  TabIndex="10" Visible="false"
                                    ValidationGroup="show" CssClass="btn btn-primary" />

                                --%>
                            </div>

                            <div class="col-md-2">
                                <label style="display: none;">Total Selected</label>
                                <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False"
                                    Style="text-align: center;" Font-Bold="True" Font-Size="Small" Visible="false"></asp:TextBox>
                                <%--  Reset the sample so it can be played again --%>
                                <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                    WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />
                                <asp:HiddenField ID="hftot" runat="server" />
                            </div>

                            <%--OnPreRender="dpPager_PreRender"--%>
                            <div class="col-md-12">
                                <asp:Panel ID="Panel1" runat="server" Style="padding-left: 10px;" Width="98%">
                                    <%--<table>
                                        <tr>
                                            <td runat="server" style="text-align: right">
                                                <div class="vista-grid_datapager">
                                                    <asp:DataPager ID="dpPager" runat="server" Visible="false" PagedControlID="lvStudentRecords"  OnPreRender="dpPager_PreRender">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField FirstPageText="First" PreviousPageText="Prev" ButtonType="Link"
                                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                                            <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                            <asp:NextPreviousPagerField LastPageText="Last" NextPageText="Next" ButtonType="Link"
                                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>--%>
                                    <asp:ListView ID="lvStudentRecords" Visible="false" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid" style="overflow: auto; height: 400px;">
                                                <div class="box-header">
                                                    <h3 class="box-title new-header-lv" style="margin-left: -5px; font-size: 16px; font-weight: bold; margin-top: -2px;">Student List</h3>
                                                </div>

                                                <table id="tblStudent" class="display table table-hover table-bordered">
                                                    <thead>
                                                        <tr id="Tr1" class="bg-light-blue">
                                                            <th>
                                                                <asp:CheckBox ID="chkIdentityCard" runat="server" OnClick="checkAllCheckbox(this);" ToolTip="Select or Deselect All Records" />
                                                            </th>
                                                            <th>Enrollment No.
                                                            </th>
                                                            <th>Student Name
                                                            </th>
                                                            <th>Payment Type
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Status
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
                                                    <asp:CheckBox ID="chkreport" runat="server" ToolTip='<%# Eval("idno") %>' />
                                                    <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td><%# Eval("REGNO")%>
                                                    <asp:HiddenField ID="hdfAdmbatch" runat="server" Value='<%# Eval("ADMBATCH") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPayType" runat="server" Text='<%# Eval("PAYTYPENAME")%>' />
                                                    <asp:HiddenField ID="hdfPtype" runat="server" Value='<%# Eval("PTYPE") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                    <asp:HiddenField ID="hdfSem" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("GENERATED_STATUS")%>' ToolTip='<%# Eval("STATUSCOUNT")%>'
                                                        ForeColor='<%#(Convert.ToInt32(Eval("STATUSCOUNT"))==0 ? System.Drawing.Color.Red : System.Drawing.Color.Green)%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>


                            <div class="col-12" id="pnlDept" runat="server" visible="False">
                                <div class="form-group col-md-4">
                                    <label>Session</label>
                                    <asp:DropDownList ID="ddlSessionReg" runat="server" data-select2-enable="true" AppendDataBoundItems="true">
                                        <%--AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"--%>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2">
                                    <label>Total Student</label>
                                    <asp:Label ID="lblTotal" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="form-group col-md-2">
                                    <label>Registered Student </label>
                                    <asp:Label ID="lblRegistered" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="form-group col-md-2">
                                    <label>Remaining Student </label>
                                    <asp:Label ID="lblPending" runat="server" Font-Bold="true"></asp:Label>
                                </div>


                            </div>

                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="show" DisplayMode="List" />
                            <div id="divMsg" runat="server">
                            </div>

                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGenerateExamForm" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvStudentRecords$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvStudentRecords$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        if (headchk.checked == false)
                            e.checked = false;
                }
            }
        }

    </script>
    <script type="text/javascript">
        function SelectAll(chk) {
            debugger;
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                        txtTot.value = hftot.value;
                    }
                    else {

                        lst.checked = false;
                        txtTot.value = 0;
                    }
                }

            }
        }
        //$(document).ready(function () {

        //    bindDataTable();
        //    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        //});

        //function bindDataTable() {
        //    var myDT = $('#tblStudent').DataTable({
        //        //scrollX: 'true'
        //        //"pageLength": 10
        //    });
        //}
        function ConfirmToGenerate(me) {
            if (me != null) {
                var chk = confirm("Are You Sure To Generate Exam Form.?");
                {
                    if (chk == true) {
                        return true;
                    }
                    else { return false; }
                }
            }
        }

        function chkprint(chk) {
            debugger;
            try {
                var count = $("[id*=tblStudent] td").closest("tr").length;
                if (chk.checked) {
                    $('[id*=tblStudent]  input:checkbox:checked').prop('checked', false);
                    $('[id*=tblStudent]  input:checkbox').removeAttr("disabled", false);
                    $("#ctl00_ContentPlaceHolder1_btnGenerateExamForm").disabled;
                    $("#ctl00_ContentPlaceHolder1_btnPrint").removeAttr("disabled", false);
                } else {
                    for (var i = 0; i < count; i++) {
                        var status = document.getElementById("ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl" + i + "_lblStatus").innerText;
                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl" + i + "_chkreport");
                        if (status == "Generated") {
                            document.getElementById("ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl" + i + "_chkreport").checked = true;
                            document.getElementById("ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl" + i + "_chkreport").disabled = true;
                        }
                    }
                    $("#ctl00_ContentPlaceHolder1_btnPrint").disabled;
                    $("#ctl00_ContentPlaceHolder1_btnGenerateExamForm").removeAttr("disabled", false);
                }
            }
            catch (err) {
                alert("Error : " + err.message);
            }
        }


    </script>
</asp:Content>
