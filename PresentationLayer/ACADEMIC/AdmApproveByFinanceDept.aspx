<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdmApproveByFinanceDept.aspx.cs" Inherits="ACADEMIC_AdmApproveByFinanceDept" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
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

    <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">ADMISSION APPROVAL</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Admission Batch</label>--%>
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmbatch_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="true" ValidationGroup="Show"
                                            InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="true" ValidationGroup="Report"
                                            InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>School/Institute Name</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClg" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlClg_SelectedIndexChanged" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvClg" runat="server" ControlToValidate="ddlClg"
                                            Display="None" ErrorMessage="Please Select School/Institute" SetFocusOnError="true" ValidationGroup="Show" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Department Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="True" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" ErrorMessage="Please Select Department" SetFocusOnError="true" ValidationGroup="Show" InitialValue="0" />--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Degree</label>--%>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Programme/Branch</label>--%>
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/Branch" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Students" OnClick="btnShow_Click"
                                    TabIndex="5" ValidationGroup="Show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="6" Text="Submit" ToolTip="Click to Submit" Visible="false"
                                    class="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="Submit" />
                                <asp:Button ID="btnReport" runat="server" TabIndex="7" Text="Report"
                                    class="btn btn-info" OnClick="btnReport_Click" ValidationGroup="Report" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="8" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Report" />
                            </div>

                            <div class="col-12" id="divstudentdetail" style="display: block" visible="false" runat="server">
                                <asp:ListView ID="lvStudentDetail" runat="server" OnItemDataBound="lvStudentDetail_ItemDataBound">
                                    <LayoutTemplate>
                                        <div id="divlvFeeItems" class="vista-grid">
                                            <div class="sub-heading">
                                                <h5>Student List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudent">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" ToolTip="Select All" Text="Select All" /></th>
                                                        <th>
                                                            <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>
                                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>
                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>Student Name</th>
                                                        <th>Category</th>
                                                        <th>Payment Type</th>
                                                        <th>Amount</th>
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
                                                <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                <asp:HiddenField ID="hidStudentNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("CODE")%>
                                            </td>
                                            <td>
                                                <%# Eval("SHORTNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME")%>
                                                <asp:HiddenField ID="HidSemesterNo" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("CATEGORY")%>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPayType" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <%--<asp:ListItem Value="1">Please Select1</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 10%">
                                                <asp:TextBox ID="txtAmount" runat="server" Enabled="false" Text='<%# Eval("AMOUNT")%>'> </asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function SelectAll(headchk) {
            var frm = document.forms[0];
            var tbl = document.getElementById('tblStudent');
            var chkHead = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentDetail_cbHead');


            for (i = 0; i < tbl.rows.length - 1; i++) {
                var chkRow = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentDetail_ctrl' + i + '_cbRow');
                //alert(chkRow)
                if (chkHead.checked == true)
                    chkRow.checked = true;
                else
                    chkRow.checked = false;
            }
        }
    </script>
</asp:Content>

