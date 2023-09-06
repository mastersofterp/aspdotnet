<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UploadSyllabus.aspx.cs" Inherits="ACADEMIC_UploadSyllabus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <div id="dvMain" runat="server">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                            </div>

                            <div class="box-body">
                                <div class="col-12" id="divGeneralInfo" style="display: block;">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Selection Criteria</h5>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Degree</label>--%>
                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="None" ErrorMessage="Please select Degree" ControlToValidate="ddlDegree" ValidationGroup="teacherallot" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Branch</label>--%>
                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="None" ErrorMessage="Please select Branch" ControlToValidate="ddlBranch" ValidationGroup="teacherallot" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Scheme</label>--%>
                                                <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlScheme" CssClass="form-control" data-select2-enable="true" runat="server" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="None" ErrorMessage="Please select Scheme" ControlToValidate="ddlScheme" ValidationGroup="teacherallot" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Semester</label>--%>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="None" ErrorMessage="Please select Semester" ControlToValidate="ddlSemester" ValidationGroup="teacherallot" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-4 col-md-12 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:FileUpload ID="fuSyllabusUpload" runat="server" TabIndex="2" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True"
                                                ErrorMessage="Please Choose file for Upload Syllabus" ControlToValidate="fuSyllabusUpload"
                                                Display="None" ValidationGroup="teacherallot" />
                                            <span style="color: red">Note:Upload only Doc,Pdf and Excel file.</span>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btnUpload_Click" ValidationGroup="teacherallot" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="teacherallot" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlconfiguration" runat="server">
                                        <asp:ListView ID="lvlist" Visible="false" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Syllabus List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divdegreelist">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="text-align: center;">Action
                                                            </th>
                                                            <th>File Name
                                                            </th>
                                                            <th>
                                                                <%--Scheme Name--%>
                                                                <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <th>
                                                                <%--Semester Name--%>
                                                                <asp:Label ID="lblDYtxtSemName" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <th>Attachment
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>

                                            <ItemTemplate>
                                                <asp:UpdatePanel runat="server" ID="UpdateDel">
                                                    <ContentTemplate>
                                                        <tr>
                                                            <td style="text-align: center;">
                                                                <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/IMAGES/delete.gif" CommandArgument='<%# Eval("USNO") %>'
                                                                    AlternateText="Delete Record" OnClientClick="return UserDeleteConfirmation();" OnClick="btnDel_Click"
                                                                    TabIndex="6" />
                                                            </td>
                                                            <td>
                                                                <%# Eval("FILE_NAME") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SCHEMENAME") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SEMESTERNAME") %>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download"
                                                                    ToolTip='<%# Eval("FILE_NAME")%>' CommandArgument='<%# Eval("SEMESTERNO")%>' OnClick="lnkDownload_Click">
                                                                </asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnDel" />
                                                    </Triggers>
                                                </asp:UpdatePanel>

                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to delete this Syllabus?"))
                return true;
            else
                return false;
        }
    </script>

</asp:Content>


