<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="NewAdmBranchChange.aspx.cs" Inherits="ACADEMIC_NewAdmBranchChange" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            Autocomplete();
        }

        function Autocomplete() {
            $(function () {
                $(".tb").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: "../HealthService.asmx/GetData_BranchChange",
                            data: "{'data': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) {; return data; },
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        value: item
                                    }
                                }))
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(textStatus);
                            }
                        });
                    },
                    minLength: 3
                });
            });
        }
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Branch Change</h3>
                        </div>

                        <%-- <div class="box-body">
                            <div class="col-12">
                                <div class="row">--%>
                        <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <%-- <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Reg. No.</label>
                                        </div>
                                        <asp:TextBox ID="txtStudent" runat="server" CssClass="form-control" />
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search Criteria</label>
                                        </div>
                                        <asp:DropDownList runat="server" class="form-control" ID="ddlSearch1" AutoPostBack="true" AppendDataBoundItems="true"
                                            data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>--%>
                        <asp:UpdatePanel ID="updEdit" runat="server">
                            <ContentTemplate>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Search Criteria</label>
                                            </div>
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true"
                                                data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                            <asp:Panel ID="pnltextbox" runat="server">
                                                <div id="divtxt" runat="server" style="display: block">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Search String</label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudent" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlDropdown" runat="server">
                                                <div id="divDropDown" runat="server" style="display: block">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Search String</label>
                                                    </div>
                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </asp:Panel>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:LinkButton ID="lnkNote" runat="server" OnClick="lnkNote_Click">Click Here For Branch Change Excel Report</asp:LinkButton>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divParmenter" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select Criteria</label>
                                            </div>
                                            <asp:RadioButton ID="rdoAdmBatch" Text="Adm Batch" AutoPostBack="true"
                                                GroupName="ReportType" OnCheckedChanged="rdoAdmBatch_CheckedChanged" TabIndex="1" runat="server" />&nbsp&nbsp&nbsp
                                        <asp:RadioButton ID="rdoAcdYear" Text="Academic Year" AutoPostBack="true"
                                            GroupName="ReportType" TabIndex="1" runat="server" OnCheckedChanged="rdoAcdYear_CheckedChanged" />&nbsp&nbsp&nbsp
                                        </div>

                                        <div class="form-group col-md-3 col-sm-3 col-3" id="divAcdYear" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Academic Year</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="show" 
                                                OnSelectedIndexChanged="ddlAcdYear_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-md-3 col-sm-3 col-3" id="divAdmBatch" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Admission Batch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="show" 
                                                OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnShow_Click" ValidationGroup="Show" />
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtStudent" Display="None"
                                        ErrorMessage="Please Enter Enroll Number" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtStudent" Display="None"
                                        ErrorMessage="Please Enter Roll Number" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                    <asp:Button ID="btnexcelBranchChange" runat="server" Text="Branch Change Excel Report"
                                        CssClass="btn btn-info" OnClick="btnexcelBranchChange_Click" />
                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Visible="false" />
                                    <%--<asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return submitPopup(this.name);" CssClass="btn btn-primary" />--%>
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
                                                        <div class="table-responsive">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Sr. No.
                                                                        </th>
                                                                        <th>Name
                                                                        </th>
                                                                        <th>Adm. Status
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
                                                        </div>
                                                    </asp:Panel>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Container.DataItemIndex + 1%>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                            OnClick="lnkId_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAdmcan" Font-Bold="true" runat="server" ForeColor='<%# Eval("ADMCANCEL").ToString().Equals("ADMITTED")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' Text='<%# Eval("ADMCANCEL")%>'></asp:Label>
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


                                <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label></label>
                                    </div>
                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" ValidationGroup="Show" />
                                                <asp:RequiredFieldValidator ID="rfvRollNumber" runat="server"
                                                    ControlToValidate="txtStudent" Display="None"
                                                    ErrorMessage="Please Enter Enroll Number" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="rfvRollNumber_submit" runat="server"
                                                    ControlToValidate="txtStudent" Display="None"
                                                    ErrorMessage="Please Enter Roll Number" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <asp:Label ID="lblPreSession" runat="server" Font-Bold="true" Visible="false" />
                                </div>--%>

                                <div class="form-group col-lg-6 col-md-12 col-12">
                                    <asp:Label ID="lblNote" Font-Bold="true" Visible="false" Text="Note: Please do not refresh page Or Do not search new student once you processed demand for current student." Style="color: red;" runat="server" SkinID="lblmsg"></asp:Label>
                                </div>
                                <%--</div>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div id="divdata" runat="server" visible="false">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label>
                                                    <asp:Label ID="lblAdmbatch" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                    <asp:Label ID="lblYear" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Degree :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Roll No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRollNo" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>

                                        </ul>
                                    </div>

                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Reg. No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>

                                            <li class="list-group-item"><b>Current Branch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>College Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblColg" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-2 col-md-6 col-12">
                                        <asp:Image ID="imgEmpPhoto" runat="server" ImageUrl="~/IMAGES/nophoto.jpg" Height="90px" Width="90px" Style="text-align: center;" />
                                    </div>
                                </div>

                                <div class="row mt-4">
                                    <div class="form-group col-lg-12 col-md-12 col-12" style="display:none">
                                        <%--<asp:RadioButton AutoPostBack="true" name="rdoOpt" value="0" ID="rdBranchChange" OnCheckedChanged="rdBranchChange_CheckedChanged" Font-Bold="true" Text="Branch Change" runat="server" GroupName="fees" />&nbsp;&nbsp;&nbsp;--%>
                                        <asp:RadioButton AutoPostBack="true" name="rdoOpt" value="1" ID="rdWithoutFee" OnCheckedChanged="rdWithoutFee_CheckedChanged" Font-Bold="true" Text="Branch Change if Fees Not Paid" runat="server" GroupName="fees" />&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton AutoPostBack="true" name="rdoOpt" value="2" ID="rdWithFee" OnCheckedChanged="rdWithFee_CheckedChanged" Font-Bold="true" Text="Branch Change After Fees Paid" runat="server" GroupName="fees" />
                                    </div>
                                </div>

                                <asp:Panel ID="pnlBranchChange" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="dvClgName" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College Name </label>
                                            </div>

                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" InitialValue="0"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="dvNewDegree" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>New Degree </label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ToolTip="Please Select Degree" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="dvBranch" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>New Branch </label>
                                            </div>

                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" onChange='return ShowConfirm(this);'
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                Display="None" ErrorMessage="Please Select Branch" InitialValue="0"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-3 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Generated New Reg No </label>
                                            </div>
                                            <asp:CheckBox ID="chkRegno" runat="server" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="dvNewRollNo" runat="server" style="display: none">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Generated New Reg No </label>
                                            </div>
                                            <asp:TextBox ID="txtNewRegNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Remark </label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"
                                                CssClass="form-control" Rows="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvfRemark" runat="server"
                                                ControlToValidate="txtRemark" ErrorMessage="Please Enter Remark"
                                                ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-lg-3 col-md-4 col-12" id="dvCurrentBranchFees" runat="server">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Current Branch Fees :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblCurrentfeess" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="col-lg-3 col-md-4 col-12" id="dvPaidFees" runat="server">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Paid Fees :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblPaidFees" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="col-lg-3 col-md-4 col-12" runat="server" id="divnewbranchfee">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>New Branch Fees :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblNewBranchFee" runat="server" Font-Bold="True" Visible="false" Text="0"></asp:Label>
                                                        <asp:Label ID="lblExcessAmt" runat="server" Font-Bold="True" Visible="false" Text="0"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer mt-3">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Visible="false"
                                            ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-warning" Visible="false"
                                            Text="Cancel" OnClientClick="if ( ! RollBackConfirmation()) return false;" /><br />

                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                                    </div>
                                </asp:Panel>

                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblMsg" Font-Bold="true" runat="server" SkinID="lblmsg"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <%-- </div>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnexcelBranchChange" />

        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server" />
    <script type="text/javascript">
        function ShowConfirm(yourDropDown) {
            debugger;
            //If this is true - Perform your PostBack manually          
            //var isSimpleBranchChecked = $('#<%=rdWithoutFee.ClientID %>').is(":checked");
            //var isSimpleBranchCheckedwithfee = $('#<%=rdWithFee.ClientID %>').is(":checked");
            //if (isSimpleBranchChecked || isSimpleBranchCheckedwithfee) {
                if (confirm("New demand will be create for selected branch. Are you sure?")) {
                    __doPostBack('ddlcollege', '');
                    return true;
                }
                else {
                    yourDropDown.selectedIndex = 0;//(yourDropDown.value == "No") ? 0 : 1;
                 
                    $('#<%=btnSubmit.ClientID %>').hide();    
                    document.getElementById('<%=btnSubmit.ClientID %>').style.visibility = "Hidden";
                   // $("#btnSubmit").hide();
                    return false;
                }
            //}
        }

        function RollBackConfirmation() {
            return confirm("ALERT: Created new branch demand will be RollBack. Are you sure?");
        }

        function Validate() {
            debugger
            var rbText;
            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            //svar rbText="NAME";
            if (rbText == "IDNO" || rbText == "Mobile") {
                var char = (event.which) ? event.which : event.keyCode;
                if (char >= 48 && char <= 57)
                    return true;
                else
                    return false;
            }
            else if (rbText == "NAME") {
                var char = (event.which) ? event.which : event.keyCode;
                if ((char >= 65 && char <= 90 || char == 35) || (char >= 97 && char <= 122))
                    return true;
                else
                    return false;
            }
        }
    </script>
</asp:Content>

