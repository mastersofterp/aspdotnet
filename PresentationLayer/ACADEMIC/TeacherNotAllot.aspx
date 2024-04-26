<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TeacherNotAllot.aspx.cs" Inherits="Academic_REPORTS_MarksEntryNotDone" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTeacher"
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

    <asp:UpdatePanel ID="updTeacher" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">TEACHER NOT ALLOTED</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-3 col-3">
                                        <div id="aDIV" runat="server">
                                            <div>
                                                <label>Report</label>
                                            </div>
                                            <asp:DropDownList ID="ddlReport" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged"
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Course Teacher not Alloted</asp:ListItem>
                                                <asp:ListItem Value="2">Teacher Student not Alloted</asp:ListItem>
                                                <asp:ListItem Value="3">Course Teacher Alloted</asp:ListItem>
                                                <asp:ListItem Value="4">Teacher Not Tagged</asp:ListItem>

                                                <asp:ListItem Value="5">Programs/Branches Not Yet Registered</asp:ListItem>

                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlHideShow" runat="server" Visible="false">
                                    <div class="row">

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Session</label>--%>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="true" TabIndex="2" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <%--   <sup>* </sup>--%>
                                                <%--<label>Scheme</label>--%>
                                                <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlClgScheme" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                ValidationGroup="teacherreport" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlClgScheme_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--  <asp:RequiredFieldValidator ID="rfvddlScheme" runat="server" ControlToValidate="ddlClgScheme" SetFocusOnError="true"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme" ValidationGroup="teacherreport">
                                        </asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <%-- <sup>* </sup>--%>
                                                <%--<label>Degree</label>--%>
                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="true" TabIndex="2"
                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" Visible="false"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvDegree2" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Branch</label>--%>
                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="true" TabIndex="3">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" Visible="false"
                                                Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <%-- <sup>* </sup>--%>
                                                <%--<label>Semester</label>--%>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="true" TabIndex="4">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-12 d-none">
                                            <asp:RadioButtonList ID="rblAllotment" runat="server" AppendDataBoundItems="true" CssClass="col-6" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1">Course Teacher not Alloted</asp:ListItem>
                                                <asp:ListItem Value="2">Teacher Student not Alloted</asp:ListItem>
                                                <asp:ListItem Value="3">Course Teacher Alloted</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Test</label>
                                            </div>
                                            <asp:DropDownList ID="ddlTest" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="true" TabIndex="2">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                <asp:ListItem Value="0">Test I & II</asp:ListItem>
                                                <asp:ListItem Value="1">Test I</asp:ListItem>
                                                <asp:ListItem Value="2">Test II</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTest" runat="server" ControlToValidate="ddlTest"
                                                Display="None" ErrorMessage="Please Select Test" InitialValue="-1" SetFocusOnError="True"
                                                ValidationGroup="report"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </asp:Panel>
<<<<<<< HEAD
                                 <asp:Panel ID="pnlHideShow1" runat="server" Visible="false">
                                    <div class="row">

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session</label>
                                                <%--<asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlSessionH" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="true" TabIndex="2">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSessionH"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </asp:Panel>
=======
>>>>>>> eb55f393 ([ENHANCEMENT] [54221] [TEACHER NOT TAGGED])
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Panel ID="pnlfooter" runat="server" Visible="false">
                                    <asp:Button ID="btnReport1" runat="server" Text="Report (Excel)" ValidationGroup="teacherreport" TabIndex="5"
                                        OnClick="btnReport1_Click" CssClass="btn btn-info" />
                                    <asp:Button ID="btnReport2" runat="server" Text="**Marks Entry Not Done"
                                        ValidationGroup="report" OnClick="btnReport2_Click" Visible="false" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="6" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="teacherreport" />
                                </asp:Panel>
                            </div>

                            <div class="form-group col-lg-5 col-md-12 col-12 d-none">
                                <div class=" note-div">
                                    <h5 class="heading">Note (Please Select)</h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Teacher Not Alloted - <span style="color: green; font-weight: bold">Session->Degree</span></span>  </p>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Mark Entry Not Done - <span style="color: green; font-weight: bold">Session->Degree->Term</span></span></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport1" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
    <script>
        function validateRBL() {
            var selectval = $("[id*=ctl00_ContentPlaceHolder1_rblAllotment]").val();
            if (selectval == "0") {

            }
        }
    </script>
</asp:Content>
