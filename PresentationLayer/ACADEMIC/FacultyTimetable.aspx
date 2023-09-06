<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FacultyTimetable.aspx.cs" Inherits="Academic_FacultyTimetable"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTimeTable"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updTimeTable" runat="server">
        <ContentTemplate>
        <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <%--<h3 class="box-title"><b>FACULTYWISE TIME TABLE REPORT</b></h3>--%>
                     <div class="box-header with-border">
                                <%--<h3 class="box-title">Define Activity for Session</h3>--%>
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            </div>
                                    <div class="pull-right">
                       
                </div> 
                </div>
                <div class="box-body">

                                        <div class="form-group col-md-4">
                                            <%--<label><span style="color:red;">*</span> Session</label>--%>
                                            <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>College</label>--%>
                                                        <asp:Label ID="lblDYddlColgSession" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                            <asp:DropDownList ID="ddlSessionAuto" runat="server" AppendDataBoundItems="True"
                                                AutoPostBack="true" TabIndex="1" Font-Bold="True"
                                                OnSelectedIndexChanged="ddlSessionAuto_SelectedIndexChanged1" data-select2-enable="true" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSessionauto" runat="server" ControlToValidate="ddlSessionAuto"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="fac"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4" id="trFaculty" runat="server">
                                            <label><span style="color:red;">*</span> Faculty</label>

                                            <asp:DropDownList ID="ddlFaculty" runat="server" TabIndex="2" AppendDataBoundItems="True" AutoPostBack="true"
                                                ValidationGroup="auto" ToolTip="faculty" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" data-select2-enable="true" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvFaculty" runat="server" ControlToValidate="ddlFaculty"
                                                Display="None" ErrorMessage="Please Select Faculty" ValidationGroup="fac" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label><span style="color:red;" id="schemeSP" runat="server"></span></label>
                                            <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>College</label>--%>
                                                        <asp:Label ID="lblDYddlScheme" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                            <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" data-select2-enable="true" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Scheme"
                                                ValidationGroup="fac"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <%--<label>Semester</label>--%>
                                             <div class="label-dynamic">
                                                        <%--<sup>* </sup>--%>
                                                        <%--<label>College</label>--%>
                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="3"
                                                AppendDataBoundItems="True" ToolTip="Semester" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged1" data-select2-enable="true" >
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <%--<label>Section</label>--%>
                                             <div class="label-dynamic">
                                                        <%--<sup>* </sup>--%>
                                                        <%--<label>College</label>--%>
                                                        <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                            <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" data-select2-enable="true" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-12">
                                            <p class="text-center">
                                                <asp:Button ID="btnFacultyReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnFacultyReport_Click"
                                                    ValidationGroup="fac" />
                                                <asp:Button ID="btnFacultyCancel" runat="server"
                                                        Text="Cancel" CssClass="btn btn-danger" OnClick="btnFacultyCancel_Click" />
                                                <asp:ValidationSummary ID="vsAuto" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="fac" />
                                            </p>
                                        </div>  
                          
                        </div> 
                        <div id="divMsg" runat="server">
                        </div>
                </div>
            </div>
        </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFacultyReport" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
