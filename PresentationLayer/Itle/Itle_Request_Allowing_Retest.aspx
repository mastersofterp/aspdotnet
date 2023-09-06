<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Itle_Request_Allowing_Retest.aspx.cs" Inherits="Itle_Itle_Request_Allowing_Retest" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updRequest"
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
    <asp:UpdatePanel ID="updRequest" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">REQUEST FOR ALLOWING RETEST</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlCourse" runat="server">
                                    <div class="row">

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trSession" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Session Term</label>
                                            </div>
                                            <asp:Label ID="lblSession" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" Class="form-control" data-select2-enable="true" runat="server" ToolTip="Select Session"
                                                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                                ErrorMessage="Please Select Session" ValidationGroup="submit" Display="None" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                            <div class="col-12" id="DivCourseList" runat="server" visible="false">
                                <div class="sub-heading">
                                    <h5>Course List</h5>
                                </div>
                                <asp:Panel ID="pnlAllowRetest" runat="server">
                                    <asp:ListView ID="lvCourse" runat="server" DataKeyNames="CourseNo" OnItemDataBound="lvCourse_ItemDataBound">
                                        <LayoutTemplate>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Select</th>
                                                        <th>Course Name</th>
                                                        <th>Subject Type</th>
                                                        <th>Test Name</th>
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
                                                    <asp:CheckBox ID="chkCourseName" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSubname" runat="server" Text=' <%# Eval("COURSENAME")%>' ToolTip='<%# Eval("COURSENO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSubjecttype" runat="server" Text=' <%# Eval("SUBNAME")%>' ToolTip='<%# Eval("SUBID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTest" runat="server" CssClass="form-control" AppendDataBoundItems="true" />
                                                    <asp:HiddenField ID="hdnLECTURE" runat="server" Value='<%# Eval("LECTURE")%>' />
                                                    <asp:HiddenField ID="hdnTHEORY" runat="server" Value='<%# Eval("THEORY")%>' />
                                                    <asp:HiddenField ID="hdnPRACTICAL" runat="server" Value='<%# Eval("PRACTICAL")%>' />
                                                    <asp:HiddenField ID="hdnSTUD_IDNO" runat="server" Value='<%# Eval("idno")%>' />
                                                    <asp:HiddenField ID="hdnSTUD_NAME" runat="server" Value='<%# Eval("studname")%>' />
                                                    <asp:HiddenField ID="hdnROLLNO" runat="server" Value='<%# Eval("roll_no")%>' />
                                                    <asp:HiddenField ID="hdnBRANCHNO" runat="server" Value='<%# Eval("branchno")%>' />
                                                    <asp:HiddenField ID="hdnYEAR" runat="server" Value='<%# Eval("BATCHNAME")%>' />
                                                    <asp:HiddenField ID="hdnCREDITS" runat="server" Value='<%# Eval("credits")%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>

                            </div>

                            <asp:Panel ID="pnlButton" runat="server">
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                                        Text="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" ValidationGroup="submit" />
                                    <asp:Button ID="btnallowrequest" runat="server" OnClick="btnallowrequest_Click" CssClass="btn btn-primary"
                                        Text="Allow Request Certificate" ToolTip="Click here for Allow Request Certificate" Visible="false" />

                                    <asp:Button ID="btnMedicalreport" runat="server" CssClass="btn btn-info" Visible="false"
                                        OnClick="btnMedicalreport_Click" Text="Medical Certificate" ToolTip="Click here for Medical Certificate" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                        OnClick="btnCancel_Click" ToolTip="Click here to Reset" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />

                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
</asp:Content>
