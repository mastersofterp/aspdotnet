<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentTopperList.aspx.cs" Inherits="ACADEMIC_REPORTS_StudentTopperList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlExam"
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
    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-12">
                                        <asp:RadioButtonList ID="rdoPurpose" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdoPurpose_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1"> Topper List &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2"> Merit List &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="3"> Programme/Branch wise Final Merit List  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:ListItem>
                                            <asp:ListItem Value="4">Coursewise Topper List </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College & Scheme</label>
                                            <%--<asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="save" data-select2-enable="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSession" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="divClg" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ValidationGroup="Summary" ToolTip="Please Select Institute"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup=""
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>


                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="divDegree" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0"
                                            ValidationGroup=""></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="divBranch" runat="server">
                                        <div class="label-dynamic">
                                            <sup id="spBranch" runat="server">* </sup>
                                            <label>Programme/Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="divScheme" runat="server">
                                        <div class="label-dynamic">
                                            <sup id="spScheme" runat="server">* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="True" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSemester" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 " id="divAdmission" runat="server">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="True" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ID="rvfadmbatch" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>--%>
                                    </div>




                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divMerit" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Topper List/ Merit List on basis of </label>
                                        </div>
                                        <asp:DropDownList ID="ddlMeritList" runat="server"
                                            AutoPostBack="True" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%--<asp:ListItem Value="1">Marks</asp:ListItem>--%>
                                            <asp:ListItem Value="2">SGPA</asp:ListItem>
                                            <asp:ListItem Value="3">CGPA</asp:ListItem>
                                            <asp:ListItem Value="4">Percentage</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvMeritList" runat="server" ControlToValidate="ddlMeritList"
                                            Display="None" ErrorMessage="Please Select Merit list on basis" InitialValue="0"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divCourse" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Courses </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server"
                                            AppendDataBoundItems="True" data-select2-enable="true">
                                            <asp:ListItem Value="0"> Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcourses" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" ErrorMessage="Please Select Courses" InitialValue="0"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div id="divRange" class="form-group col-lg-3 col-md-6 col-12 " runat="server">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Top Range</label>
                                        </div>
                                        <asp:TextBox ID="txtTopcnt" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divReport" runat="server">
                                        <label>Report In:</label>
                                        <asp:RadioButtonList ID="rdoReportType" runat="server"
                                            RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rdoReportType_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="pdf">Adobe Reader &nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="xls">MS-Excel &nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="doc" style="display: none">MS-Word &nbsp;&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="report"
                                    OnClick="btnReport_Click" CssClass="btn btn-info" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Clear" CausesValidation="false" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="vs" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="report" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server"></div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
