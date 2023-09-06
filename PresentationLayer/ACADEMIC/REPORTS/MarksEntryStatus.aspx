<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MarksEntryStatus.aspx.cs" Inherits="Academic_REPORTS_MarksEntryNotDone" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><b>MARKS ENTRY STATUS</b></h3>
                     <div class="pull-right">
                   <div style="color: Red; font-weight: bold;">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>
                </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                   <ContentTemplate>
                    <div class="box-body">
                        <fieldset>
                            <%--<legend>Selection Criteria</legend>--%>
                            <div class="form-group col-md-4">
                                <label><span style="color:red">*</span>Session </label>
                                <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="True" ToolTip="Session Name">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvsess" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </div>
                             <div class="form-group col-md-4">
                                <label>Institute Name </label>
                                  <asp:DropDownList ID="ddlCollegeName" runat="server" TabIndex="2" AppendDataBoundItems="True"
                                            ToolTip="Please  Select Institute" AutoPostBack="True" OnSelectedIndexChanged="ddlCollegeName_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-4">
                                <label><span style="color:red">*</span>Degree </label>
                                <asp:DropDownList ID="ddlDegree" ToolTip="Degree Name" runat="server" TabIndex="3" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </div>
                             <div class="form-group col-md-4">
                                <label><span style="color:red">*</span>Branch </label>
                                <asp:DropDownList ID="ddlBranch" runat="server" ToolTip="Branch Name" AppendDataBoundItems="True" TabIndex="4" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label><span style="color:red">*</span>Scheme </label>
                                    <asp:DropDownList ID="ddlScheme" runat="server" ToolTip="Scheme Name" AppendDataBoundItems="True" TabIndex="5"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvProgram" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlScheme" Display="None" ErrorMessage="Please Select Scheme"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label><span style="color:red">*</span>Semester </label>
                                 <asp:DropDownList ID="ddlSemester" runat="server" ToolTip="Semester Name" AppendDataBoundItems="True" TabIndex="6"
                                            ValidationGroup="Show" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </div>
                           <%-- <div class="form-group col-md-4">
                                <label>Exam Type </label>
                                 <asp:DropDownList ID="ddlExamType" runat="server" ToolTip="Semester Name" AppendDataBoundItems="True" AutoPostBack="True" TabIndex="6"
                                            ValidationGroup="Show">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Internal Exam</asp:ListItem>
                                            <asp:ListItem Value="2">External Exam</asp:ListItem>
                                            <asp:ListItem Value="3">Both</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExamType" runat="server" ControlToValidate="ddlExamType"
                                            Display="None" ErrorMessage="Please Select Exam Type" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Course </label>
                                 <asp:DropDownList ID="ddlCourse" runat="server" ToolTip="Course Name" TabIndex="7"
                                            AppendDataBoundItems="True" ValidationGroup="Show">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        
                            </div>--%>
                           
                        </fieldset>
                    </div>
                    <div class="box-footer">
                                <p class="text-center">
                                     <asp:Button ID="btnCourseReport" runat="server" Text="Course wise Report" TabIndex="8"
                                    ValidationGroup="Show" OnClick="btnCourseReport_Click" CssClass="btn btn-info"/>
                                       <asp:Button ID="btnExcel" runat="server" Visible="false" Text="Excel Report" ValidationGroup="report" TabIndex="9"
                                            OnClick="btnExcel_Click" CssClass="btn btn-info"/>
                                       <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="9" OnClick="btnCancel_Click" CssClass="btn btn-danger"/>
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                                </p>
                                 <div id="divMsg" runat="server">
    </div>
                            </div>
                   </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


        
    
                   
   
</asp:Content>
