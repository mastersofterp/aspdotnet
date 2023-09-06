<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Condonation_Approval.aspx.cs" Inherits="ACADEMIC_Condonation_Approval" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlAAPaList .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdApproved"
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
    <asp:UpdatePanel ID="UpdApproved" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlScheme" ValidationGroup="Show" Display="None"
                                            ErrorMessage="Please Select School/Scheme" InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollegesession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCollegesession_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlCollegesession" ValidationGroup="Show" Display="None"
                                            ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Course</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShowapprovedstud" runat="server" CssClass=" btn btn-info"
                                    ValidationGroup="Show" Text="Show Student List" OnClick="btnShowapprovedstud_Click" />
                                <%--     <asp:Button ID="btnExcel" runat="server"  CssClass=" btn btn-info"
                                    ValidationGroup="Show" Text="Student Excel List"  OnClick="btnExcel_Click" Visible="false" />--%>
                                <asp:Button ID="btnsavestatus" runat="server" CssClass="btn btn-info" Text="Submit" Visible="false" OnClick="btnsavestatus_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" Text="Cancel"
                                    OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="VSCaution" runat="server" ValidationGroup="Show" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" />
                            </div>
                            <asp:Panel ID="pnlAAPaList" runat="server" Visible="false">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Status</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                        <asp:ListItem Value="2">Reject</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <asp:ListView ID="lvAAPath" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Student List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />
                                                    </th>
                                                    <th>Regno</th>
                                                    <th>Name</th>
                                                    <th>Degree</th>
                                                    <th>Branch</th>
                                                    <th>Semester</th>
                                                    <th>Coursename</th>
                                                    <th>Attendance</th>
                                                    <th>Range</th>
                                                    <th>Shortage</th>
                                                    <th>Status</th>
                                                    <th>Reason For Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("IDNO")%>' Enabled='<%#(Eval("STATUS").ToString())== "Approved" ?  false : true %>' />
                                                <asp:HiddenField ID="hdfcourseno" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                <asp:HiddenField ID="hdfsemesterno" runat="server" Value='<%# Eval("semesterno")%>' />
                                            </td>
                                            <td>
                                                <%# Eval("REGNO") %>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME") %>
                                            </td>
                                            <td>
                                                <%# Eval("DEGREENAME") %>
                                            </td>
                                            <td>
                                                <%# Eval("LONGNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("COURSE_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("ATTENDANCE")%>
                                            </td>
                                            <td>
                                                <%# Eval("RANGE")%>
                                            </td>
                                            <td>
                                                <%# Eval("SHORTAGE")%>
                                            </td>
                                            <td>
                                                <%# Eval("STATUS")%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtreason" runat="server" Text='<%# Eval("REASON")%>' CssClass="form-control" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <%--  <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvAAPath$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvAAPath$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>
</asp:Content>

