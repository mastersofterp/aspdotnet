<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CoursewiseLevel.aspx.cs" Inherits="ACADEMIC_CoursewiseLevel" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlLevel"
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

    <asp:UpdatePanel ID="pnlLevel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">LEVEL CREATION / UPDATION</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Term</label>
                                        </div>
                                        <asp:Label ID="lblSession" runat="server" Font-Bold="true" Visible="false" />
                                        <asp:DropDownList ID="ddlTerm" runat="server" CssClass="form-control" data-select2-enable="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTerm" runat="server" ControlToValidate="ddlTerm"
                                            Display="None" ErrorMessage="Please Select Term" InitialValue="0" ValidationGroup="Submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Level Name</label>
                                        </div>
                                        <asp:TextBox ID="txtLevel" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvfLevelName" runat="server"
                                            ControlToValidate="txtLevel" Display="None"
                                            ErrorMessage="Enter  Level Name" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:TextBox ID="txtAdmBatch" runat="server" TabIndex="2" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtAdmBatch" Display="None"
                                            ErrorMessage="Enter  Admission Batch" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CvAdmBatch" runat="server"
                                            ControlToValidate="txtAdmBatch" Display="None"
                                            ErrorMessage="Admission Batch should be in  Integers only"
                                            Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                                            ValidationGroup="Submit"></asp:CompareValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>No Of Courses</label>
                                        </div>
                                        <asp:TextBox ID="txtNoOfCourses" runat="server" TabIndex="3" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvfNoOfCourses" runat="server"
                                            ControlToValidate="txtNoOfCourses" Display="None"
                                            ErrorMessage="Enter No of Courses" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CvNoOfCourses" runat="server"
                                            ControlToValidate="txtNoOfCourses" Display="None"
                                            ErrorMessage="Number of Courses should be in  Integers only"
                                            Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                                            ValidationGroup="Submit"></asp:CompareValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Total Theory Sub</label>
                                        </div>
                                        <asp:TextBox ID="txtTheory" runat="server" TabIndex="4" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTotalTheory" runat="server"
                                            ControlToValidate="txtTheory" Display="None"
                                            ErrorMessage="Enter Total Theory Subjects" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CvTotalTheory" runat="server"
                                            ControlToValidate="txtTheory" Display="None"
                                            ErrorMessage="Total Theory Subjects should be in  Integers only"
                                            Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                                            ValidationGroup="Submit"></asp:CompareValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>
                                                Total Pratical
                                                <asp:Label ID="lblDivision" runat="server" Font-Bold="True" Font-Size="Small" Text="" /></label>
                                        </div>
                                        <asp:TextBox ID="txtPractical" runat="server" TabIndex="5" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTotalPracticals" runat="server"
                                            ControlToValidate="txtPractical" Display="None"
                                            ErrorMessage="Enter Total Practical Subjects" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CvPracticals" runat="server"
                                            ControlToValidate="txtPractical" Display="None"
                                            ErrorMessage="Total Practicals should be in Integers Only"
                                            Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                                            ValidationGroup="Submit"></asp:CompareValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Total Theory Marks</label>
                                        </div>
                                        <asp:TextBox ID="txtThMarks" runat="server" MaxLength="3" TabIndex="6" CssClass="form-control"></asp:TextBox>
                                        <asp:CompareValidator ID="CvTotThmarks" runat="server"
                                            ControlToValidate="txtThMarks" Display="None"
                                            ErrorMessage="Total theory Marks should be in integers only"
                                            Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                                            ValidationGroup="Submit"></asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="rfvTotThMarks" runat="server"
                                            ControlToValidate="txtThMarks" Display="None"
                                            ErrorMessage="Enter Total Theory Marks" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Total Practical Marks</label>
                                        </div>
                                        <asp:TextBox ID="txtPrMarks" runat="server" MaxLength="3" TabIndex="7" CssClass="form-control"></asp:TextBox>

                                        <asp:CompareValidator ID="CvTotPracMarks" runat="server"
                                            ControlToValidate="txtPrMarks" Display="None"
                                            ErrorMessage="Total Practical marks should be in integers  only"
                                            Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                                            ValidationGroup="Submit"></asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="rfvTotPractmarks" runat="server"
                                            ControlToValidate="txtPrMarks" Display="None"
                                            ErrorMessage="Enter Total Practical Marks " SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="8" ValidationGroup="Submit"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-primary" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    TabIndex="9" OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                            <div class="col-12">
                                <asp:ListView ID="lvLevelCreation" runat="server">
                                    <EmptyDataTemplate>
                                        <center>
                         <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></center>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Level Creation List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>Level Name
                                                    </th>
                                                    <th>Admission Batch
                                                    </th>
                                                    <th>No. Of Courses
                                                    </th>
                                                    <th>Theory
                                                    </th>
                                                    <th>Practical
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
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png"
                                                    CommandArgument='<%# Eval("LEVELNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%#Eval("LEVEL_DESC")%>
                                            </td>
                                            <td>
                                                <%#Eval("ADMBATCH")%>
                                            </td>
                                            <td>
                                                <%#Eval("NO_COURSES")%>
                                            </td>
                                            <td>
                                                <%#Eval("CP_TH")%>
                                            </td>
                                            <td>
                                                <%#Eval("CP_PR")%>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png"
                                                    CommandArgument='<%# Eval("LEVELNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%#Eval("LEVEL_DESC")%>
                                            </td>
                                            <td>
                                                <%#Eval("ADMBATCH")%>
                                            </td>
                                            <td>
                                                <%#Eval("NO_COURSES")%>
                                            </td>
                                            <td>
                                                <%#Eval("CP_TH")%>
                                            </td>
                                            <td>
                                                <%#Eval("CP_PR")%>
                                            </td>

                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

