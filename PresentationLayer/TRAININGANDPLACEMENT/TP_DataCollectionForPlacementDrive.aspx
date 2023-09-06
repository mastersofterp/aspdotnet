<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TP_DataCollectionForPlacementDrive.aspx.cs" Inherits="TRAININGANDPLACEMENT_TP_DataCollectionForPlacementDrive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TP Data Collection For Placement Drive</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" AutoPostBack="true" runat="server"
                                            AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree " SetFocusOnError="true" InitialValue="0"
                                            ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                           <asp:RequiredFieldValidator ID="rfcBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch " SetFocusOnError="true" InitialValue="0"
                                            ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                             <asp:RequiredFieldValidator ID="rfcSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester " SetFocusOnError="true" InitialValue="0"
                                            ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>CGPA</label>
                                        </div>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>No.of Backlog Allowed </label>
                                        </div>
                                        <asp:TextBox ID="txtBacklog" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Attempt</label>
                                        </div>
                                        <asp:TextBox ID="txtAttempt" runat="server" MaxLength="6"
                                            CssClass="form-control" onkeyup="validateNumeric(this);"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>ADM Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>


                                          <asp:RequiredFieldValidator ID="rfcAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" ErrorMessage="Please Select ADM Batch " SetFocusOnError="true" InitialValue="0"
                                            ValidationGroup="submit" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="View" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="View_Click" ValidationGroup="submit"/>
                                <asp:Button ID="btnList" runat="server" Text="Export To Excel" ValidationGroup="Report" CssClass="btn btn-primary" OnClick="btnList_Click"/>
                                <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="Button1_Click"/>

                                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlSession" runat="server">
                                    <asp:ListView ID="lvStudentPlacementDetail" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>

                                                        <th>IDNO
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Branch
                                                        </th>
                                                        <th>Degree
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>CGPA
                                                        </th>
                                                       
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
                                                    <%# Eval("IDNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDENT_FULL_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BRANCH_NAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("DEGREE_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTER_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CGPA")%>
                                                </td>
                                               
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList"/>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

