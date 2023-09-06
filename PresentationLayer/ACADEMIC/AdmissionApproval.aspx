<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdmissionApproval.aspx.cs" Inherits="ACADEMIC_AdmissionApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
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
                        <div id="div1" runat="server"></div>
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
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" AppendDataBoundItems="True" TabIndex="1" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmbatch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch." SetFocusOnError="true" ValidationGroup="Show"
                                            InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClg" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlClg_SelectedIndexChanged" TabIndex="2" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvClg" runat="server" ControlToValidate="ddlClg"
                                            Display="None" ErrorMessage="Please Select School/Institute." SetFocusOnError="true" ValidationGroup="Show" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="ddlDepartment" runat="server" SelectionMode="Multiple" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                                            CssClass="form-control multi-select-demo" AppendDataBoundItems="true" TabIndex="3" AutoPostBack="true"></asp:ListBox>
                                        <%-- <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" TabIndex="3" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" ErrorMessage="Please Select Department" SetFocusOnError="true" ValidationGroup="Show" InitialValue="0" />--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" ValidationGroup="report" data-select2-enable="true"
                                            CssClass="form-control" TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" SetFocusOnError="true" ValidationGroup="Show"
                                            InitialValue="0" />--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" data-select2-enable="true"
                                            AutoPostBack="True" TabIndex="5">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                            ToolTip="Please Select Status" TabIndex="6">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Approved</asp:ListItem>
                                            <asp:ListItem Value="2">Rejected</asp:ListItem>
                                            <asp:ListItem Value="3">Pending</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStatus"
                                            Display="None" ErrorMessage="Please Select Status." SetFocusOnError="True"
                                            ValidationGroup="Show" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Student" OnClick="btnShow_Click"
                                    TabIndex="7" ValidationGroup="Show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnReport" runat="server" Text="Document Status Report" OnClick="btnReport_Click"
                                    TabIndex="8" ValidationGroup="Show" CssClass="btn btn-info" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="9" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" TabIndex="13" />
                            </div>

                            <div class="col-12" id="divstudentdetail" style="display: block" visible="false" runat="server">
                                <div class="sub-heading">
                                    <h5>Student List</h5>
                                </div>
                                <asp:ListView ID="lvStudentDetail" runat="server" OnItemDataBound="lvStudentDetail_ItemDataBound">
                                    <LayoutTemplate>
                                        <div id="divlvFeeItems" class="vista-grid">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudent">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr.No</th>
                                                        <th>Student Name</th>
                                                        <th>
                                                            <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>
                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>Email ID</th>
                                                        <th>Mobile No</th>
                                                        <th>Status</th>
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
                                            <td><%# Container.DataItemIndex +1 %></td>
                                            <td>
                                                <asp:HyperLink ID="hlkstudentdetail" runat="server" Target="_blank" Text='<%# Eval("STUDNAME") %>'
                                                    NavigateUrl='<%# Eval("LINK_NAME") + "&id=" + Eval("IDNO") %>' />
                                            </td>
                                            <%--<td>
                                                <%# Eval("STUDNAME")%>
                                            </td>--%>
                                            <td>
                                                <%# Eval("LONGNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("EMAILID")%>
                                            </td>
                                            <td>
                                                <%# Eval("MOBILENO")%>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblStatus"></asp:Label>
                                                <asp:Label runat="server" ID="Label1" Value='<%# Eval("STATUS") %>' Visible="false"></asp:Label>
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
            <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
        });
    </script>
</asp:Content>

