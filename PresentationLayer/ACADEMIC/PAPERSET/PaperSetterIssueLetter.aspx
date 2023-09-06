<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PaperSetterIssueLetter.aspx.cs" Inherits="ACADEMIC_PaperSetterIssueLetter"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../../JAVASCRIPTS/jquery.min_1.js" type="text/javascript"></script>--%>

    <%--<script src="../../JAVASCRIPTS/jquery-ui.min_1.js" type="text/javascript"></script>--%>

    <div class="col-md-12">
        <div style="z-index: 1; position: absolute; top: 100px; left: 600px;">
            <asp:UpdateProgress ID="updProg3" runat="server" AssociatedUpdatePanelID="upIssueletter"
                DisplayAfter="0">
                <ProgressTemplate>
                    <div style="width: 120px; background-color: Transparent; padding-left: 5px">
                        <img src="../../IMAGES/ajax-loader.gif" alt="Loading" />
                        Please Wait..
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>

    <asp:UpdatePanel ID="upIssueletter" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">PAPER SETTER ISSUE LETTER </h3>
                        </div>
                        <%--<div style="color: Red; font-weight: bold">
                            &#160;Note : * marked fields are Mandatory
                        </div>--%>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                       <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession3" runat="server" AutoPostBack="true" data-select2-enable="true"
                                            AppendDataBoundItems="True" TabIndex="1" ToolTip="Please Select session" OnSelectedIndexChanged="ddlSession3_OnSelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSession3" runat="server" ControlToValidate="ddlSession3"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <%--<div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"
                                            AppendDataBoundItems="True" TabIndex="1" ToolTip="Please Select ddlScheme">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>--%>
                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester3" TabIndex="2" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                            AutoPostBack="true" ToolTip="Please Select Semester" OnSelectedIndexChanged="ddlSemester3_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSemester3"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <%-- <span style="color: red;">*</span>--%>
                                            <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse3" runat="server" AppendDataBoundItems="True"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCourse3_SelectedIndexChanged" data-select2-enable="true"
                                            TabIndex="3" ToolTip="Please Select Course">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--   <asp:RequiredFieldValidator ID="rfvddlSemester0" runat="server"
                                    ControlToValidate="ddlCourse3" Display="None" InitialValue="0"
                                    ErrorMessage="Please Select Course Name" SetFocusOnError="True"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit3" ValidationGroup="Show" runat="server" CssClass="btn btn-primary" Text="Generate Letter" Visible="false"
                                    OnClick="btnSubmit3_Click" Font-Bold="true" />&nbsp;

                                <asp:Button ID="btnNotRcvdLetter" ValidationGroup="Show" runat="server" CssClass="btn btn-primary" Text="Pending Letter"
                                    OnClick="btnNotRcvdLetter_Click" Font-Bold="true" />&nbsp;
                            
                                <asp:Button ID="btnSubmit4" ValidationGroup="Show" runat="server" CssClass="btn btn-primary" Text="Paper Setter Order"
                                    OnClick="btnSubmit4_Click" Font-Bold="true" />&nbsp;
                            
                                <asp:ValidationSummary ID="vsshow3a" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />

                                <asp:Button ID="btnClear3" CausesValidation="false" runat="server" Font-Bold="true" CssClass="btn btn-warning"
                                    OnClick="btnClear3_Click" Text="Clear" />
                            </div>
                        </div>

                        <div class="box-footer">
                            <div class="col-md-12 col-md-offset-3">
                            </div>

                            <div class="col-12 mt-4 ">
                                <asp:Panel ID="pnlList3" runat="server">

                                    <asp:ListView ID="lvLetter" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Paper Setter List</h5>
                                            </div>
                                            <div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Semester
                                                            </th>
                                                            <th>Code
                                                            </th>

                                                            <th>Course
                                                            </th>
                                                            <th>Approved Paper Setter
                                                            </th>
                                                            <th>Faculty Type
                                                            </th>
                                                            <th>Select
                                                            </th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER")%>' ToolTip='<%# Eval("SEMESTERNO")%>'></asp:Label>

                                                    <%-- <asp:Label ID="lblSchemetype" ToolTip="Scheme Type" runat="server" Text='<%# Eval("SCHEMETYPE") %>' />--%>
                                                </td>
                                                <td>
                                                    <%--<asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREENAME") %>' />--%>
                                                    <asp:Label ID="lblCcode" runat="server" Text='<%# Eval("CCODE")%>' ToolTip='<%# Eval("CCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%--<asp:Label ID="lblCourseName" ToolTip="Course name" runat="server" Text='<%# Eval("COURSE_NAME") %>' />--%>
                                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("COURSE_NAME")%>' ToolTip='<%# Eval("CCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%--<asp:Label ID="lblCCode" ToolTip="Cours code" runat="server" Text='<%# Eval("CCODE") %>' />--%>
                                                    <asp:Label ID="lblFaculty" runat="server" ForeColor='<%#Eval("CANCEL").ToString().ToLower()=="true"? System.Drawing.Color.Red : System.Drawing.Color.Black %>'
                                                        Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("APPROVED")%>' Width="80%"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFacType" runat="server" Text='<%# Eval("FAC")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkFac" ToolTip='<%# Eval("APPROVED")%>' Checked='<%#Eval("APPROVED").ToString()=="0" || Eval("CANCEL").ToString().ToLower()=="true"?false:true%>'
                                                        runat="server" />
                                                    <%--  <asp:CheckBox ID="chkFac" ToolTip='<%# Eval("APPROVED")%>' Checked='<%#Eval("APPROVED").ToString()=="0" || Eval("CANCEL").ToString().ToLower()=="true"?false:true%>'
                                                        Enabled='<%#Eval("APPROVED").ToString()== "0" ?false:Eval("RECEIVED").ToString().ToLower()=="true"? false:Eval("CANCEL").ToString().ToLower()=="true"? false:true %>'
                                                        runat="server" />--%>
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


    </asp:UpdatePanel>


    <div id="divMsg" runat="server">
    </div>
</asp:Content>
