<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentResult.aspx.cs" Inherits="ACADEMIC_EXAMINATION_StudentResult" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updUpdate"
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
    <div runat="server" id="divDetails">

        <asp:UpdatePanel ID="updUpdate" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div id="div2" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">STUDENT RESULT</h3>
                            </div>
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divRegistrationNo" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Registration No</label>
                                            </div>
                                            <asp:TextBox CssClass="form-control" runat="server" ID="txtRegistrationNo" OnTextChanged="txtRegistrationNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ControlToValidate="txtRegistrationNo"
                                                Display="None" ErrorMessage="Please Enter Registration No." InitialValue="" ValidationGroup="report"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-12 col-12" id="spanNote" runat="server" visible="false">
                                            <div class=" note-div">
                                                <h5 class="heading">Note</h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>AB - <b>ABSENT</b>, CC - <b>COPY CASE</b>, WR - <b>WITHDRAW </b>, DR - <b>DROP </b>, * - GRACE </span></p>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblNote" runat="server"  Text="No Published Result Found !!!!" Visible="false" ForeColor="Red" Font-Bold="true" Font-Size="Large"></asp:Label>
                                    <asp:Button ID="btnShow" runat="server" Text="Show Details" CssClass="btn btn-primary" OnClick="btnShow_Click"
                                        ValidationGroup="report" />
                                    <asp:Button ID="btnViewResult" Visible="false" runat="server" OnClick="btnViewResult_Click"
                                        Text="View Result" CssClass="btn btn-info" ValidationGroup="report" />
                                    <asp:Button ID="btnPrint" Visible="false" runat="server" OnClick="btnPrint_Click"
                                        Text="Print Result" CssClass="btn btn-info" ValidationGroup="report" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />
                                </div>
                                <div class="col-12">

                                    <div id="divStudDetails" runat="server" visible="false" class="row">
                                        <div class="col-lg-3 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Reg. No :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblRegNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Session:</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSession" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>

                                        </div>
                                        <div class="col-lg-3 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>SGPA :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSGPA" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlStudent" runat="server">
                                        <asp:ListView ID="lvStudent" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("COURSE")%>
                                                    </td>

                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>
                                                    </td>
                                                    <td style="text-align: center; display: none">
                                                        <%# Eval("S1MARK")%>
                                                    </td>
                                                    <td style="text-align: center; display: none">
                                                        <%# Eval("S2MARK")%>
                                                    </td>
                                                    <td style="text-align: center; display: none">
                                                        <%# Eval("S3MARK")%>
                                                    </td>
                                                    <td style="text-align: center; display: none">
                                                        <%# Eval("S3MARK_SCALEDOWN")%>
                                                    </td>
                                                    <td style="text-align: center; display: none">
                                                        <%# Eval("EXTERMARK")%>
                                                    </td>
                                                    <td style="text-align: center; display: none">
                                                        <%# Eval("S6MARK")%>
                                                    </td>
                                                    <td style="text-align: center; display: none">
                                                        <%# Eval("S7MARK")%>
                                                    </td>
                                                    <td style="text-align: center; display: none">
                                                        <%# Eval("MARKTOT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CREDITS")%> 
                                                    </td>
                                                    <td>
                                                        <%# Eval("GRADE")%> 
                                                    </td>
                                                    <td>
                                                        <%# Eval("GDPOINT")%> 
                                                    </td>
                                                </tr>

                                            </ItemTemplate>
                                            <LayoutTemplate>

                                                <%--<h4>Course List</h4>--%>
                                                <table class="table table-striped table-bordered display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">

                                                            <th>Subject Name
                                                            </th>

                                                            <th style="text-align: center">Semester
                                                            </th>
                                                            <th style="text-align: center; display: none">Assignment
                                                            </th>
                                                            <th style="text-align: center; display: none">Attendance
                                                            </th>
                                                            <th style="text-align: center; display: none">MID SEM
                                                            </th>
                                                            <th style="text-align: center; display: none">MID SEM Scaledown
                                                            </th>
                                                            <th style="text-align: center; display: none">END SEM
                                                            </th>
                                                            <th style="text-align: center; display: none">Internal Assessment
                                                            </th>
                                                            <th style="text-align: center; display: none">External Assessment
                                                            </th>
                                                            <th style="text-align: center; display: none">Total Marks
                                                            </th>
                                                            <th>Credits
                                                            </th>
                                                            <th>Grade
                                                            </th>
                                                            <th>GD Points
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                            </LayoutTemplate>
                                        </asp:ListView>

                                    </asp:Panel>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPrint" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <div id="divMsg" runat="server">
    </div>
    <script>
        function showhide() {
            document.getElementById('<%=btnShow.ClientID%>').style.display = "block";
            document.getElementById('<%=btnViewResult.ClientID%>').style.display = "none";
            document.getElementById('<%=btnPrint.ClientID%>').style.display = "none";
            document.getElementById('<%=pnlStudent.ClientID%>').style.display = "none";
            document.getElementById('<%=divStudDetails.ClientID%>').style.display = "none";
        }

        function call() {
            __doPostBack("showhide", "");
        }

    </script>

</asp:Content>

